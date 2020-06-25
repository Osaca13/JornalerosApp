using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class Nacionalidad
    {
        public int IdNacionalidad { get; set; }
        public int IdPersona { get; set; }
        public string Tipo { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
