using ApiEmpleado.Models;

namespace ApiEmpleado.Interfaces
{
    public interface IDepartamentoRepository
    {
        Task<ICollection<Departamento>> GetDepartamentos();
        Task<Departamento> GetDepartamentoById(int departamentoId);
        Task<bool> CreateDepartamento(Departamento departamento);
        Task<bool> UpdateDepartamento(Departamento departamento);
        Task<bool> DeleteDepartamento(Departamento departamento);
        Task<ICollection<Empleado>> GetEmpleadosFromDepartamento(int departamnetoid);
        Task<Departamento> GetDeartamentoWithEmpleadoById(int departamentoId);
        Task<bool> DepartamentoExist(int departamentoId);
        Task<bool> Save();
    }
}
