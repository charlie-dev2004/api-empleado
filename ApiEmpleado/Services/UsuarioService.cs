using ApiEmpleado.Data;
using ApiEmpleado.Interfaces;
using ApiEmpleado.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiEmpleado.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioService(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:secretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddHours(8),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<string> Login(string userName, string password)
        {
            if(await _context.Usuarios.AnyAsync(c => c.UserName == userName) == false)
                return "El usuario no se encuentra registrado";

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.UserName == userName);

            if (!BCrypt.Net.BCrypt.Verify(password, usuario.Password))
                return "Contraseña incorrecta";

            return CreateToken(usuario);
        }

        public async Task<bool> Register(string userName, string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            Usuario usuario = new Usuario
            {
                UserName = userName,
                Password = passwordHash
            };

            await _context.Usuarios.AddAsync(usuario);
            int valor = await _context.SaveChangesAsync();
            bool result = valor > 0 ? true : false;
            return result;
        }
    }
}
