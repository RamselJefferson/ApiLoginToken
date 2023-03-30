using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.ViewModel
{
    public class vwVehiculos
    {
        [Key]
        public int VehId { get; set; }
        public string? VehDecripcion { get; set; }
        public string? MarDecripcion { get; set; }
        public string? ModDescripcion { get; set; }

        
        public decimal Precio { get; set; }

        public int? Año { get; set; }

        public string? Estatus { get; set; }




    }
}
