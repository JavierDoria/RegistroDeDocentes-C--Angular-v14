using Microsoft.EntityFrameworkCore;
using CRUD_C__Angular.Models;
using CRUD_C__Angular.Services.Contrato;
using System.ComponentModel;

namespace CRUD_C__Angular.Services.Implementacion
{
    public class DocenteServices : IDocenteServices
    {
        private ProfesoresContext _dbContext;
        public DocenteServices(ProfesoresContext dbContext) 
        {
            _dbContext= dbContext;
        }
        public async Task<List<Docente>> GetList()
        {
            try
            {
                List<Docente> lista = new List<Docente>();
                lista = await _dbContext.Docentes.Include(dpt => dpt.IdEspecialidadNavigation).ToListAsync();
                return lista;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
        public async Task<Docente> Get(int idDocente)
        {
            try
            {
                Docente? encontrado = new Docente();

                encontrado = await _dbContext.Docentes.Include(dpt => dpt.IdEspecialidadNavigation).Where(e => e.IdDocente == idDocente).FirstOrDefaultAsync();
                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Docente> Add(Docente modelo)
        {
            try
            {
                _dbContext.Docentes.Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(Docente modelo)
        {
            try
            {
                _dbContext.Docentes.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Delete(Docente modelo)
        {
            try
            {
                _dbContext.Docentes.Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
