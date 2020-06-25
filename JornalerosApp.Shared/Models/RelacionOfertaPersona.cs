using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class RelacionOfertaPersona
    {
        public int IdRelOfePer { get; set; }
        public int IdPersona { get; set; }
        public int IdOferta { get; set; }

        public virtual Oferta IdOfertaNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
