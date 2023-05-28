using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PruebaTecnica.Models
{
    public class Jwt
    {
        static ApiContext _context;
        public Jwt(ApiContext context)
        {
            _context = context;
        }
        
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Subject { get; set; }

        public static dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if(identity == null)
                {
                    return new
                    {
                        succes = false,
                        message = "Verificar token",
                        result = ""
                    };
                }
             var id = identity.Claims.FirstOrDefault(e => e.Type =="id").Value;

             var usuario = _context.Usuario.FirstOrDefault(u  => u.IdUsuario == int.Parse(id));

                return new
                {
                    success = true,
                    message = "Token valido",
                    result = usuario
                };
            }
            catch(Exception ex) {

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
