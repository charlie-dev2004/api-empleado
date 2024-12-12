using ApiEmpleado.DTOs;
using ApiEmpleado.Interfaces;
using ApiEmpleado.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpleado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(UsuarioDto usuarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _usuarioService.Register(usuarioDto.UserName, usuarioDto.Password));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _usuarioService.Login(usuario.UserName, usuario.Password));
        }
    }
}
