using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.ViewModel
{
    public class vwModelos
    {
        [Key]
        public int ModId { get; set; }
        public string? MarDecripcion { get; set; }
        public string? ModDescripcion { get; set; }

        public int MarId { get; set; }
    }
}
