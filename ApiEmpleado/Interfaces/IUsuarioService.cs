using ApiEmpleado.Models;

namespace ApiEmpleado.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> Register(string userName, string password);
        Task<string> Login(string userName, string password);
    }
}
