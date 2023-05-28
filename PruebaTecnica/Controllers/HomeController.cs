using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public dynamic IniciarSesion([FromBody] Login dataCli)
        {
            

            string user = dataCli.Usuario.ToString();
            string password = dataCli.Password.ToString();

            var usuario = _context.Usuario.FirstOrDefault(e => e.UsuarioName== user && e.Password == password);

            if(usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales Incorrectas",
                    result = ""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                new Claim("id",usuario.IdUsuario.ToString()),
                new Claim("rol",usuario.Rol.ToString()),
                new Claim("usuarioName",usuario.UsuarioName.ToString())
            };

            byte[] bytes = Encoding.UTF8.GetBytes(jwt.Key);
            var key = new SymmetricSecurityKey(bytes);
            var inicio = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                signingCredentials: inicio

                );

            return new
            {
                success = true,
                message = "exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }
    }
}
