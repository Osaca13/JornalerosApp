﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JornalerosApp.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JornalerosApp.Infrastructure.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Curriculum> Curriculum { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<EstudiosPorNiveles> EstudiosPorNiveles { get; set; }
        public virtual DbSet<Experiencia> Experiencia { get; set; }
        public virtual DbSet<Formacion> Formacion { get; set; }
        public virtual DbSet<Idioma> Idioma { get; set; }
        public virtual DbSet<Nacionalidad> Nacionalidad { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<RelacionMunicipioProvincia> RelacionMunicipioProvincia { get; set; }
        public virtual DbSet<RelacionOfertaPersona> RelacionOfertaPersona { get; set; }

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
                entity.HasKey(e => e.IdCurriculum);

                entity.Property(e => e.AlojamientoPropio).HasMaxLength(2);

                entity.Property(e => e.IdPersona)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Movilidad).HasMaxLength(2);

                entity.Property(e => e.Disponibilidad).HasMaxLength(50);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Curriculum)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Curriculum_Persona");
            });

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

                entity.Property(e => e.IdCurriculum)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Puesto).HasMaxLength(50);

                entity.HasOne(d => d.IdCurriculumNavigation)
                    .WithMany(p => p.Experiencia)
                    .HasForeignKey(d => d.IdCurriculum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Experiencia_Curriculum");
            });

            modelBuilder.Entity<Formacion>(entity =>
            {
                entity.HasKey(e => e.IdFormacion);

                entity.Property(e => e.Centro).HasMaxLength(50);

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.IdCurriculum)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCurriculumNavigation)
                    .WithMany(p => p.Formacion)
                    .HasForeignKey(d => d.IdCurriculum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Formacion_Curriculum");
            });

            modelBuilder.Entity<Idioma>(entity =>
            {
                entity.HasKey(e => e.IdIdioma);

                entity.Property(e => e.IdCurriculum)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Idioma1)
                    .IsRequired()
                    .HasColumnName("Idioma")
                    .HasMaxLength(50);

                entity.Property(e => e.LeerEscribirEscuchar).HasMaxLength(50);

                entity.HasOne(d => d.IdCurriculumNavigation)
                    .WithMany(p => p.Idioma)
                    .HasForeignKey(d => d.IdCurriculum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Idioma_Curriculum");
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

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Oferta_Empresa");
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

            modelBuilder.Entity<RelacionOfertaPersona>(entity =>
            {
                entity.HasKey(e => e.IdRelOfePer);

                entity.Property(e => e.IdOferta)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdPersona)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.IdOfertaNavigation)
                    .WithMany(p => p.RelacionOfertaPersona)
                    .HasForeignKey(d => d.IdOferta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelacionOfertaPersona_Oferta");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.RelacionOfertaPersona)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelacionOfertaPersona_Persona");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
