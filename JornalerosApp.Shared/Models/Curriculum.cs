using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public class Curriculum
    {
        public Curriculum()
        {
            Experiencia = new HashSet<Experiencia>();
            Formacion = new HashSet<Formacion>();
            Idioma = new HashSet<Idioma>();
        }

        public string IdCurriculum { get; set; }
        public string IdPersona { get; set; }
        public string TramitarPermisoTrabajo { get; set; }
        public string Movilidad { get; set; }
        public string AlojamientoPropio { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual ICollection<Experiencia> Experiencia { get; set; }
        public virtual ICollection<Formacion> Formacion { get; set; }
        public virtual ICollection<Idioma> Idioma { get; set; }
    }
}
