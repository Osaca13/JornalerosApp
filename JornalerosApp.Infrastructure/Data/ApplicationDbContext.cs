﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JornalerosApp.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JornalerosApp.Infrastructure.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
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
            modelBuilder.Entity<Curriculum>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.AlojamientoPropio).HasMaxLength(2);

                entity.Property(e => e.Disponibilidad).HasMaxLength(50);

                entity.Property(e => e.Movilidad).HasMaxLength(2);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithOne(p => p.Curriculum)
                    .HasForeignKey<Curriculum>(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Curriculum_Persona");
            });

            

            modelBuilder.Entity<EstudiosPorNiveles>(entity =>
            {
                entity.HasKey(e => e.IdEstudiosPorNiveles);

                entity.Property(e => e.IdEstudiosPorNiveles).HasMaxLength(150);

                entity.Property(e => e.Familia)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.NivelFormativo)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Titulacion)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Experiencia>(entity =>
            {
                entity.HasKey(e => e.IdExperiencia);

                entity.Property(e => e.DescripcionPuesto).HasColumnType("text");

                entity.Property(e => e.Empresa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.IdPersona)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Puesto).HasMaxLength(50);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Experiencia)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Experiencia_Curriculum");
            });

            modelBuilder.Entity<Formacion>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.Centro).HasMaxLength(50);

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithOne(p => p.Formacion)
                    .HasForeignKey<Formacion>(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Formacion_Curriculum");
            });

            modelBuilder.Entity<Idioma>(entity =>
            {
                entity.HasKey(e => e.IdIdioma);

                entity.Property(e => e.IdPersona)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Idioma1)
                    .IsRequired()
                    .HasColumnName("Idioma")
                    .HasMaxLength(50);

                entity.Property(e => e.LeerEscribirEscuchar).HasMaxLength(50);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Idioma)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Idioma_Curriculum");
            });

            modelBuilder.Entity<Localidades>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Localidad).HasMaxLength(150);

                entity.Property(e => e.Provincia).HasMaxLength(150);
            });

            modelBuilder.Entity<Nacionalidad>(entity =>
            {
                entity.HasKey(e => e.IdNacionalidad);

                entity.Property(e => e.IdPersona)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Nacionalidad)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nacionalidad_Persona");
            });

            

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermisos);

                entity.Property(e => e.IdPersona)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Permiso)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permiso_Persona");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.CochePropio).HasMaxLength(2);

                entity.Property(e => e.CorreoElectronico).HasMaxLength(50);

                entity.Property(e => e.Dni)
                    .HasColumnName("DNI")
                    .HasMaxLength(9)
                    .IsFixedLength();

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Imagen).HasColumnType("text");

                entity.Property(e => e.LugarResidencia).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PrimerApellido).HasMaxLength(50);

                entity.Property(e => e.ProvinciaResidencia).HasMaxLength(50);

                entity.Property(e => e.Sexo).HasMaxLength(50);
            });

            modelBuilder.Entity<RelacionMunicipioProvincia>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Cmun)
                    .HasColumnName("CMUN")
                    .HasMaxLength(255);

                entity.Property(e => e.Cpro)
                    .HasColumnName("CPRO")
                    .HasMaxLength(255);

                entity.Property(e => e.Dc)
                    .HasColumnName("DC")
                    .HasMaxLength(255);

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(255);

                entity.Property(e => e.Provincia)
                    .HasColumnName("PROVINCIA")
                    .HasMaxLength(255);
            });

            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
