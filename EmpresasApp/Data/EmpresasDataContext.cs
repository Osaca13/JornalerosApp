using System;
using EmpresasApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmpresasApp.Data
{
    public partial class EmpresasDataContext : DbContext
    {
        public EmpresasDataContext(DbContextOptions<EmpresasDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empresa> Empresa { get; set; }

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
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.Property(e => e.Cargo).HasMaxLength(50);

                entity.Property(e => e.CodigoPostal).HasMaxLength(50);

                entity.Property(e => e.CorreoElectronico)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Dirección).HasMaxLength(150);

                entity.Property(e => e.Nifcif)
                    .HasColumnName("NIFCIF")
                    .HasMaxLength(50);

                entity.Property(e => e.NombreContacto)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Provincia).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasColumnType("numeric(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
