using ApiEmpleado.Data;
using ApiEmpleado.Interfaces;
using ApiEmpleado.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpleado.Repository
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ApiContext _context;
        public DepartamentoRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateDepartamento(Departamento departamento)
        {
            await _context.Departamentos.AddAsync(departamento);
            return await Save();
        }

        public async Task<bool> DeleteDepartamento(Departamento departamento)
        {
            _context.Departamentos.Remove(departamento);
            return await Save();
        }

        public async Task<bool> DepartamentoExist(int departamentoId)
        {
            return await _context.Departamentos.AnyAsync(c => c.Id == departamentoId);
        }

        public async Task<Departamento> GetDepartamentoById(int departamentoId)
        {
            return await _context.Departamentos.FindAsync(departamentoId); 
        }

        public async Task<Departamento> GetDeartamentoWithEmpleadoById(int departamentoId)
        {
            return await _context.Departamentos.Where(c => c.Id == departamentoId).Include(e => e.Empleados).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Departamento>> GetDepartamentos()
        {
            return await _context.Departamentos.ToListAsync();
        }

        public async Task<ICollection<Empleado>> GetEmpleadosFromDepartamento(int departamentoId)
        {
            return await _context.Departamentos.Where(d => d.Id == departamentoId).Select(c => c.Empleados).FirstOrDefaultAsync();
        }
        public async Task<bool> Save()
        {
            int valor = await _context.SaveChangesAsync();
            bool result = valor > 0 ? true : false;
            return result;
        }

        public async Task<bool> UpdateDepartamento(Departamento departamento)
        {
            _context.Departamentos.Update(departamento);
            return await Save();
        }
    }
}
