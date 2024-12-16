using ApiEmpleado.DTOs;
using ApiEmpleado.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ApiEmpleado.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ApiEmpleado.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public EmpleadoController(IEmpleadoRepository empleadoRepository, IDepartamentoRepository departamentoRepository)
        {
            _empleadoRepository = empleadoRepository;
            _departamentoRepository = departamentoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpleado(EmpleadoDto empleadoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _departamentoRepository.DepartamentoExist(empleadoDto.IdDepartamento))
                return BadRequest($"El departamento con el id {empleadoDto.IdDepartamento} no existe.");

            var empleado = new Empleado
            {
                Nombre = empleadoDto.Nombre,
                PApellido = empleadoDto.PApellido,
                SApellido = empleadoDto.SApellido,
                Edad = empleadoDto.Edad,
                Telefono = empleadoDto.Telefono,
                IdDepartamento = empleadoDto.IdDepartamento
            };

            return Ok(await _empleadoRepository.CreateEmpleado(empleado));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpleados()
        {
            var empleados = await _empleadoRepository.GetEmpleados();

            if (empleados.IsNullOrEmpty())
                return BadRequest("No hay empleados registrados");
            return Ok(empleados); 
;       }

        [HttpGet("{empleadoId}")]
        public async Task<IActionResult> GetEmpleadoById(int empleadoId)
        {
            var empleado = await _empleadoRepository.GetEmpleadoById(empleadoId);

            if (empleado == null)
                return BadRequest("El empleado nose encuentra en la base de datos");

            return Ok(empleado);
        }

        [HttpPut("{empleadoId}")]
        public async Task<IActionResult> UpdateEmpleado(int empleadoId,EmpleadoDto empleadoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var empleado = await _empleadoRepository.GetEmpleadoById(empleadoId);

            if (empleado == null)
                return BadRequest("El empleado no se encuentra en la base de datos");

            empleado.Nombre = empleadoDto.Nombre;
            empleado.PApellido = empleadoDto.PApellido;
            empleado.SApellido = empleadoDto.SApellido;
            empleado.Edad = empleadoDto.Edad;
            empleado.Telefono = empleadoDto.Telefono;
            empleado.Id = empleadoDto.IdDepartamento;  

            return Ok(await _empleadoRepository.UpdateEmpleado(empleado));  
        }

        [HttpDelete("{empleadoId}")]
        public async Task<IActionResult> DeleteEmpledo(int empleadoId)
        {
            var empleado = await _empleadoRepository.GetEmpleadoById(empleadoId);

            if (empleado == null)
                return BadRequest("El empleado no se encuentra en la base de datos");

            return Ok(await _empleadoRepository.DeleteEmpleado(empleado));
        }

        [HttpGet("Departamento/{empleadoId}")]
        public async Task<IActionResult> GetDepartamentoFromEmpleado(int empleadoId)
        {
            var departamento = await _empleadoRepository.GetDepartamentoFromEmpleado(empleadoId);

            if (empleadoId == null)
                return BadRequest($"El empleado con el id {empleadoId} no existe.");

            return Ok(departamento);
        }
    }
}
