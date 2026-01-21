using AutoMapper;
using CRUD_C__Angular.DTO;
using CRUD_C__Angular.Models;
using System.Globalization;
namespace CRUD_C__Angular.Utilidades
{
    public class AutomapperProfile : Profile
    {
        public  AutomapperProfile()
        {
            #region Especialidad
            CreateMap<Especialidad, EspecialidadDTO>().ReverseMap();
            #endregion

            #region Docente
            CreateMap<Docente, DocenteDTO>()
                .ForMember(destino => destino.NombreEspecialidad, opt => opt.MapFrom(origen => origen.IdEspecialidadNavigation.Nombre));
           

            CreateMap<DocenteDTO, Docente>().ForMember(destino => destino.IdEspecialidadNavigation, opt => opt.Ignore());
            #endregion
        }

    }
}
