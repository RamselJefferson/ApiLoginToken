using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string UsuarioName  { get; set; }

        public string Password { get; set; }

        public int Rol { get; set; }

        public string? Email { get; set; }
    }
}
