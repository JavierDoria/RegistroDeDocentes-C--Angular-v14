using Microsoft.EntityFrameworkCore;
using CRUD_C__Angular.Models;
using CRUD_C__Angular.Services.Contrato;

namespace CRUD_C__Angular.Services.Implementacion
{
    public class EspecialidadServices : IEspecialidadServices
    {
        private ProfesoresContext _dbContext;
        public EspecialidadServices(ProfesoresContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Especialidad>> GetList()
        {
            try
            {
                List<Especialidad>lista = new List<Especialidad>();
                lista = await _dbContext.Especialidad.ToListAsync();
                return lista;
            }
            catch (Exception ex) 
            { 
                throw ex;
            }
        }
    }
}
