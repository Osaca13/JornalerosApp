using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class Idioma
    {
        public int IdIdioma { get; set; }
        public string Idioma1 { get; set; }
        public int IdCurriculum { get; set; }
        public string LeerEscribirEscuchar { get; set; }

        public virtual Curriculum IdCurriculumNavigation { get; set; }
    }
}
