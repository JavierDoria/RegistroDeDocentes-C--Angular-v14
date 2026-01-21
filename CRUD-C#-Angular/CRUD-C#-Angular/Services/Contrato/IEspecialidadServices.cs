using CRUD_C__Angular.Models;

namespace CRUD_C__Angular.Services.Contrato
{
    public interface IEspecialidadServices
    {
        Task<List<Especialidad>> GetList();
    }
}
