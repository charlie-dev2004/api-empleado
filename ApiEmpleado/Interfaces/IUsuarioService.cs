using ApiEmpleado.Models;

namespace ApiEmpleado.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> Register(Usuario usuario);
        Task<string> Login(string userName, string password);
    }
}
