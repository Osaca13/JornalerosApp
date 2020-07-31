using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class RelacionOfertaPersona
    {
        public string IdRelOfePer { get; set; }
        public string IdPersona { get; set; }
        public string IdOferta { get; set; }

        public virtual Oferta IdOfertaNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
