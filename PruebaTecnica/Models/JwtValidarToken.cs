using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PruebaTecnica.Models
{
    public class JwtValidarToken
    {
        static ApiContext _context;
        public JwtValidarToken(ApiContext context)
        {
            _context = context;
        }

        public static dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity == null)
                {
                    return new
                    {
                        succes = false,
                        message = "Verificar token",
                        result = ""
                    };
                }
                var id = identity.Claims.FirstOrDefault(e => e.Type == "idUsuario").Value;

                var usuario = _context.Usuario.FirstOrDefault(u => u.IdUsuario == int.Parse(id));

                return new
                {
                    success = true,
                    message = "Token valido",
                    result = usuario
                };
            }
            catch (Exception ex)
            {

                return new
                {
                    success = true,
                    message = ex.Message,
                    result = ""
                };

            }
        }
    }
}
