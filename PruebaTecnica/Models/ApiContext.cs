using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace PruebaTecnica.Models
{
    public partial class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuarios> Usuario { get; set; }

        //public virtual DbSet<Marca> Marcas { get; set; } 
        //public virtual DbSet<Modelo> Modelos { get; set; }
        //public virtual DbSet<Vehiculo> Vehiculos { get; set; } 

        //public virtual DbSet<vwVehiculos> vwVehiculos { get; set; } 


        //public virtual DbSet<vwModelos> vwModelos { get; set; } 





       

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
