using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnica.Models
{
    public class JwtValidarToken
    {
        static ApiContext _context;
        static IConfiguration _configuration;
        public JwtValidarToken(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public static dynamic ValidarToken(string token)
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



            try
            {
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    // Añade otros parámetros de validación según tus necesidades
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal claimsPrincipal;

                // Intenta validar el token
                claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

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
