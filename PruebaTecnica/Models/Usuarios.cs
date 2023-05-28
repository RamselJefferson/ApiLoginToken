using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string UsuarioName { get; set; }
        public string Password { get; set; }

        public short Rol { get; set; }
    }
}
