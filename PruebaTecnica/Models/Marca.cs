using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Modelos = new HashSet<Modelo>();
        }

        public int MarId { get; set; }
        public string? MarDecripcion { get; set; }

        public virtual ICollection<Modelo> Modelos { get; set; }
    }
}
