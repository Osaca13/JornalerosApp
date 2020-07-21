using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public class Experiencia
    {
        public string IdExperiencia { get; set; }
        public string IdCurriculum { get; set; }
        public string Empresa { get; set; }
        public string Puesto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string DescripcionPuesto { get; set; }

        public virtual Curriculum IdCurriculumNavigation { get; set; }
    }
}
