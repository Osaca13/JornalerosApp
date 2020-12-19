using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OfertasApp.Data
{
    public partial class OfertaDataContext : DbContext
    {
        public OfertaDataContext(DbContextOptions<OfertaDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Oferta> Oferta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.HasKey(e => e.IdOferta);

                entity.Property(e => e.Alojamiento)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ContinuidadIgualLabor)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ContinuidadOtraLabor)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FechaCaducidad).HasColumnType("date");

                entity.Property(e => e.FechaPublicacion).HasColumnType("date");

                entity.Property(e => e.IdEmpresa)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.JornadaReal).HasMaxLength(50);

                entity.Property(e => e.LugarTrabajo).HasMaxLength(50);

                entity.Property(e => e.Provincia).HasMaxLength(50);

                entity.Property(e => e.Salario).HasMaxLength(50);

                entity.Property(e => e.TipoContrato).HasMaxLength(50);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
