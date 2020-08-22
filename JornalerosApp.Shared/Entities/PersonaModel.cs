using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;

namespace JornalerosApp.Shared.Entities
{
    public class PersonaModel
    {
        public PersonaModel()
        {
            Curriculum = new Curriculum[] { };
            Nacionalidad = new Nacionalidad[] { };
            Permiso = new Permiso[] { };
            RelacionOfertaPersona = new RelacionOfertaPersona[] { };
        }

        public string IdPersona { get; set; }

        [Required]
        [StringLength(16)]
        public string Nombre { get; set; }

        public string PrimerApellido { get; set; }
        [Required]
        [ValidacionNIF(ErrorMessage = "Numero de documento incorrecto")]
        [StringLength(9, MinimumLength = 9)]
        public string Dni { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string CorreoElectronico { get; set; }

        [StringLength(5)]
        public string CochePropio { get; set; }
        public string Imagen { get; set; }
        public string Sexo { get; set; }
        public string LugarResidencia { get; set; }
        public string ProvinciaResidencia { get; set; }

        public Curriculum[] Curriculum { get; set; }
        public Nacionalidad[] Nacionalidad { get; set; }
        public Permiso[] Permiso { get; set; }
        public RelacionOfertaPersona[] RelacionOfertaPersona { get; set; }
    }
}

