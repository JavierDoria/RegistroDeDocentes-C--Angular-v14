using CRUD_C__Angular.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CRUD_C__Angular.Services.Contrato;
using CRUD_C__Angular.Services.Implementacion;
using AutoMapper;
using CRUD_C__Angular.DTO;
using CRUD_C__Angular.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ProfesoresContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});
builder.Services.AddScoped<IEspecialidadServices, EspecialidadServices>();
builder.Services.AddScoped<IDocenteServices, DocenteServices>();

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region PETICIONES API REST
app.MapGet("especialidad/lista",async(
    IEspecialidadServices _especialidadServicio,
    IMapper _mapper
    )=>
{
    var listaEspecialidad = await _especialidadServicio.GetList();
    var listaEspecialidadDTO = _mapper.Map<List<EspecialidadDTO>>(listaEspecialidad);
    if (listaEspecialidadDTO.Count > 0)
        return Results.Ok(listaEspecialidadDTO);
    else
        return Results.NotFound();
});
//Docente Lista

app.MapGet("docente/lista", async (
    IDocenteServices _docenteServicio,
    IMapper _mapper
    ) =>
{
    var listaDocente = await _docenteServicio.GetList();
    var listaDocenteDTO = _mapper.Map<List<DocenteDTO>>(listaDocente);
    if (listaDocenteDTO.Count > 0)
        return Results.Ok(listaDocenteDTO);
    else
        return Results.NotFound("No hay registros");
});

//Guardar Docente
app.MapPost("/docente/guardar", async(
    DocenteDTO modelo,
    IDocenteServices _docenteServicio,
    IMapper _mapper
    ) => {
        var _docente  = _mapper.Map<Docente>(modelo);
        var _docenteCreado = await _docenteServicio.Add(_docente);
        if (_docenteCreado.IdDocente != 0)
            return Results.Ok(_mapper.Map<DocenteDTO>(_docenteCreado));
        else 
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
});

//Guardar Docente
app.MapPut("/docente/actualizar/{IdDocente}", async (
    int IdDocente,
    DocenteDTO modelo,
    IDocenteServices _docenteServicio,
    IMapper _mapper
    ) => {
        var _encontrado = await _docenteServicio.Get(IdDocente);
        if(_encontrado is null) return Results.NotFound();

        var _docente = _mapper.Map<Docente>(modelo);
        _encontrado.NombrePrimer = _docente.NombrePrimer;
        _encontrado.NombreSegundo = _docente.NombreSegundo;
        _encontrado.ApellidoPaterno = _docente.ApellidoPaterno;
        _encontrado.ApellidoMaterno = _docente.ApellidoPaterno;
        _encontrado.Correo = _docente.Correo;
        _encontrado.Dni = _docente.Dni;
        _encontrado.Edad = _docente.Edad;
        _encontrado.IdEspecialidad = _docente.IdEspecialidad;

        var respuesta = await _docenteServicio.Update(_encontrado);
        if(respuesta)
            return Results.Ok(_mapper.Map<DocenteDTO>(_encontrado));
        else 
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });
//Eliminar Docente
app.MapDelete("/docente/eliminar/{IdDocente}", async (
    int IdDocente,
     IDocenteServices _docenteServicio
    ) => {
        var _encontrado = await _docenteServicio.Get(IdDocente);
        if (_encontrado is null) return Results.NotFound();
        var respuesta = await _docenteServicio.Delete(_encontrado);
        if(respuesta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });
#endregion

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
