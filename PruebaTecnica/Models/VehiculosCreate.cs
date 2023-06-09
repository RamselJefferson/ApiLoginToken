namespace PruebaTecnica.Models
{
    public class VehiculosCreate
    {
        public string VehDecripcion { get; set; }
        public int? Año { get; set; }
        public int ModId { get; set; }
        public int MarId { get; set; }
        public decimal? Precio { get; set; }
        public string? Estatus { get; set; }
    }
}
