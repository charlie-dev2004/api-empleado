using ApiEmpleado.Data;
using ApiEmpleado.Interfaces;
using ApiEmpleado.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpleado.Repository
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly ApiContext _context;

        public EmpleadoRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEmpleado(Empleado empleado)
        {
            await _context.Empleados.AddAsync(empleado);
            return  await Save();
        }

        public async Task<bool> DeleteEmpleado(Empleado empleado)
        {
            _context.Empleados.Remove(empleado);
            return await Save();
        }
        public async Task<bool> EmpleadoExist(int empleadoId)
        {
            return await _context.Empleados.AnyAsync(c => c.Id == empleadoId);
        }
        public async Task<Empleado> GetEmpleadoById(int empleadoId)
        {
            return await _context.Empleados.FirstOrDefaultAsync(c => c.Id == empleadoId);
        }

        public async Task<ICollection<Empleado>> GetEmpleados()
        {
            return await _context.Empleados.ToListAsync();
        }

        public async Task<bool> Save()
        {
            int valor = await _context.SaveChangesAsync();
            bool result = valor > 0 ? true : false;
            return result;
        }

        public async Task<bool> UpdateEmpleado(Empleado empleado)
        {
            _context.Empleados.Update(empleado);
            return await Save(); 
        }
    }
}
