using ApiEmpleado.Models;

namespace ApiEmpleado.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<ICollection<Empleado>> GetEmpleados();
        Task<Empleado> GetEmpleadoById(int empleadoId);
        Task<bool> CreateEmpleado(Empleado empleado);
        Task<bool> UpdateEmpleado(Empleado empleado);
        Task<bool> DeleteEmpleado(Empleado empleado);
        Task<bool> EmpleadoExist(int empleadoId);
        Task<Departamento> GetDepartamentoFromEmpleado(int empleadoId);
        Task<bool> Save();
    }
}
