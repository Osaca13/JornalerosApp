using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JornalerosApp.Shared.Models
{
    public partial class Permiso
    {
        [Key]
        public string IdPermisos { get; set; }
        public string IdPersona { get; set; }
        public string Tipo { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
