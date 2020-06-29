using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JornalerosApp.Shared.Models
{
    public partial class Idioma
    {
        [Key]
        public string IdIdioma { get; set; }
        public string Idioma1 { get; set; }
        public string IdCurriculum { get; set; }
        public string LeerEscribirEscuchar { get; set; }

        public virtual Curriculum IdCurriculumNavigation { get; set; }
    }
}
