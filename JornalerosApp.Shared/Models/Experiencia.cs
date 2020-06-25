using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class Experiencia
    {
        public int IdExperiencia { get; set; }
        public int IdCurriculum { get; set; }
        public string Empresa { get; set; }
        public string Puesto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string DescripcionPuesto { get; set; }

        public virtual Curriculum IdCurriculumNavigation { get; set; }
    }
}
