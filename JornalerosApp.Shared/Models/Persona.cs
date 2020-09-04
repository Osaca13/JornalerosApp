using JornalerosApp.Shared.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JornalerosApp.Shared.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Curriculum = new HashSet<Curriculum>();
            Nacionalidad = new HashSet<Nacionalidad>();
            Permiso = new HashSet<Permiso>();
            RelacionOfertaPersona = new HashSet<RelacionOfertaPersona>();
        }

        public string IdPersona { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }

        [ValidacionNIF]
        [StringLength(9)]
        public string Dni { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string CorreoElectronico { get; set; }
        public string CochePropio { get; set; }
        public string Imagen { get; set; }
        public string Sexo { get; set; }
        public string LugarResidencia { get; set; }
        public string ProvinciaResidencia { get; set; }

        public virtual ICollection<Curriculum> Curriculum { get; set; }
        public virtual ICollection<Nacionalidad> Nacionalidad { get; set; }
        public virtual ICollection<Permiso> Permiso { get; set; }
        public virtual ICollection<RelacionOfertaPersona> RelacionOfertaPersona { get; set; }
    }
}
