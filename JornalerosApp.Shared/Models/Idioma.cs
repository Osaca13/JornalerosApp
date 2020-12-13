using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class Idioma
    {
        public string IdPersona { get; set; }
        public string IdIdioma { get; set; }
        public string Idioma1 { get; set; }
        public string LeerEscribirEscuchar { get; set; }

        public virtual Curriculum IdPersonaNavigation { get; set; }
    }
}
