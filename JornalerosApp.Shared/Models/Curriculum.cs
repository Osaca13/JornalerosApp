using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class Curriculum
    {
        public Curriculum()
        {
            Experiencia = new HashSet<Experiencia>();
           
            Idioma = new HashSet<Idioma>();
        }

        public string IdCurriculum { get; set; }
        public string IdPersona { get; set; }
        public string Disponibilidad { get; set; }
        public string Movilidad { get; set; }
        public string AlojamientoPropio { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual ICollection<Experiencia> Experiencia { get; set; }
        public virtual Formacion Formacion { get; set; }
        public virtual ICollection<Idioma> Idioma { get; set; }
    }
}
