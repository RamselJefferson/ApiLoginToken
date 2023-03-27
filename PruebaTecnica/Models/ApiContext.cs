using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PruebaTecnica.ViewModel;

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

        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Modelo> Modelos { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        public virtual DbSet<vwVehiculos> vwVehiculos { get; set; } = null!;


        public virtual DbSet<vwModelos> vwModelos { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=movilbusiness.com.do;Database=Api;User Id=jdelacruz;password=ramsel2001;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.MarId)
                    .HasName("PK__Marcas__32E17527FD20092D");

                entity.Property(e => e.MarId).ValueGeneratedNever();

                entity.Property(e => e.MarDecripcion)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.HasKey(e => e.ModId)
                    .HasName("PK__Modelos__FB1F1787CA8A3019");

                entity.Property(e => e.ModId)
                    .ValueGeneratedNever()
                    .HasColumnName("ModID");

                entity.Property(e => e.ModDescripcion)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Mar)
                    .WithMany(p => p.Modelos)
                    .HasForeignKey(d => d.MarId)
                    .HasConstraintName("fk_Modelos_Marcas");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.VehId);

                entity.Property(e => e.Año)
                    .HasColumnType("datetime")
                    .HasColumnName("año");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.VehDecripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
