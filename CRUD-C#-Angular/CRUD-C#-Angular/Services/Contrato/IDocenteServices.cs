using CRUD_C__Angular.Models;
namespace CRUD_C__Angular.Services.Contrato
{
    public interface IDocenteServices
    {
        Task<List<Docente>> GetList();
        Task<Docente> Get(int idDocente);
        Task<Docente> Add(Docente modelo);
        Task<bool> Update(Docente modelo);
        Task<bool> Delete(Docente modelo);
    }
}
