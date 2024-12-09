using ApiEmpleado.DTOs;
using ApiEmpleado.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ApiEmpleado.Models;

namespace ApiEmpleado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoController(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpleado(EmpleadoDto empleadoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var empleado = new Empleado
            {
                Nombre = empleadoDto.Nombre,
                PApellido = empleadoDto.PApellido,
                SApellido = empleadoDto.SApellido,
                Edad = empleadoDto.Edad,
                Telefono = empleadoDto.Telefono
            };

            return Ok(await _empleadoRepository.CreateEmpleado(empleado));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpleados()
        {
            var empleados = await _empleadoRepository.GetEmpleados();

            if (empleados == null)
                return BadRequest("No hay empleados registrados");
            return Ok(empleados); 
;       }

        [HttpGet("{empleadoId}")]
        public async Task<IActionResult> GetEmpleadoById(int empleadoId)
        {
            if (await _empleadoRepository.EmpleadoExist(empleadoId) == false)
                return BadRequest("El empleado nose encuentra en la base de datos");

            return Ok(await _empleadoRepository.GetEmpleadoById(empleadoId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmpleado(int empleadoId,EmpleadoDto empleadoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _empleadoRepository.EmpleadoExist(empleadoId) == false)
                return BadRequest("El empleado no se encuentra en la base de datos");

            var empleado = await _empleadoRepository.GetEmpleadoById(empleadoId);

            empleado.Nombre = empleadoDto.Nombre;
            empleado.PApellido = empleadoDto.PApellido;
            empleado.SApellido = empleadoDto.SApellido;
            empleado.Edad = empleadoDto.Edad;
            empleado.Telefono = empleadoDto.Telefono;

            return Ok(await _empleadoRepository.UpdateEmpleado(empleado));  
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmpledo(int empleadoId)
        {
            if(await _empleadoRepository.EmpleadoExist(empleadoId) == false)
                return BadRequest("El empleado no se encuentra en la base de datos");

            var empleado = await _empleadoRepository.GetEmpleadoById(empleadoId);

            return Ok(await _empleadoRepository.DeleteEmpleado(empleado));
        }

    }
}
