using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PruebaTecnica.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        static ApiContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("LoginMethod")]
        public dynamic IniciarSesion([FromBody] Login dataCli)
        {


            string user = dataCli.Usuario.ToString();
            string password = dataCli.Password.ToString();

            var usuario = _context.Usuario.FirstOrDefault(e => e.UsuarioName == user && e.Password == password);

            if (usuario == null)
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
                new Claim("idUsuario",usuario.IdUsuario.ToString()),
                new Claim("rol",usuario.Rol.ToString()),
                new Claim("usuarioName",usuario.UsuarioName.ToString())
            };

            byte[] bytes = Encoding.UTF8.GetBytes(jwt.Key);
            var key = new SymmetricSecurityKey(bytes);
            var inicio = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

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


        [HttpPost("ValidarToken")]
        public  dynamic ValidarToken(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return new
                    {
                        success = false,
                        message = "El token no puede estar vacío",
                        result = ""
                    };
                }
             
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                { 



                };

                // Intenta validar el token
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // Si la validación es exitosa, extrae el ID de usuario del token
                var id = claimsPrincipal.FindFirst("idUsuario").Value;

                // Realiza las acciones necesarias con el ID de usuario, por ejemplo, obtener información del usuario desde la base de datos
                var usuario = _context.Usuario.FirstOrDefault(u => u.IdUsuario == int.Parse(id));

                return new
                {
                    success = true,
                    message = "Token válido",
                    result = usuario
                };
            }
            catch (SecurityTokenException ex)
            {
                return new
                {
                    success = false,
                    message = "Token inválido: " + ex.Message,
                    result = ""
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message,
                    result = ""
                };
            }
        }
    }
}
