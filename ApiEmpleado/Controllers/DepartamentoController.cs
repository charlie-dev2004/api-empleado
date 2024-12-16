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
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        public DepartamentoController(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartamentos()
        {
            var departamentos = await _departamentoRepository.GetDepartamentos();
            return Ok(departamentos);
        }

        [HttpGet("{departamentoId}")]
        public async Task<IActionResult> GetDepartamentosById(int departamentoId)
        {
            var departamento = await _departamentoRepository.GetDepartamentoById(departamentoId);

            if (departamento == null)
                return BadRequest($"El departamento con el id {departamentoId} no existe");

            return Ok(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartamento(DepartamentoDto departamentoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departamento = new Departamento
            {
                Nombre = departamentoDto.Nombre
            };

            return Ok(await _departamentoRepository.CreateDepartamento(departamento));
        }

        [HttpPut("{departamentoId}")]
        public async Task<IActionResult> UpdateDepartamento(int departamentoId, DepartamentoDto departamentoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departamento = await _departamentoRepository.GetDepartamentoById(departamentoId);

            if (departamento == null)
                return BadRequest($"El departamento con el id {departamentoId} no existe.");

            departamento.Nombre = departamentoDto.Nombre;

            return Ok(await _departamentoRepository.UpdateDepartamento(departamento));
        }

        [HttpDelete("{departamentoId}")]
        public async Task<IActionResult> DeleteDepartamento(int departamentoId)
        {
            var departamento = await _departamentoRepository.GetDeartamentoWithEmpleadoById(departamentoId);

            if(departamento == null)
                return BadRequest($"El departamento {departamentoId} no se encuentra en el sistema");

            return Ok(await _departamentoRepository.DeleteDepartamento(departamento));
        }

        [HttpGet("Empleados/{departamentoId}")]
        public async Task<IActionResult> GetEmpleadosFromDepartamento(int departamentoId)
        {
            if (!await _departamentoRepository.DepartamentoExist(departamentoId))
                return BadRequest($"El departamento {departamentoId} no se encuentra en el sistema");

            var empleados = await _departamentoRepository.GetEmpleadosFromDepartamento(departamentoId);

            if (empleados.IsNullOrEmpty())
                return BadRequest($"No se encuentran empleados en el departamento {departamentoId}");

            return Ok(empleados);
        }
    }

}
