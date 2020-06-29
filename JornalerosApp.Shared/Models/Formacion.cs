using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JornalerosApp.Shared.Models
{
    public partial class Formacion
    {
        [Key]
        public string IdFormacion { get; set; }
        public string IdCurriculum { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Centro { get; set; }
        public string Descripcion { get; set; }

        public virtual Curriculum IdCurriculumNavigation { get; set; }
    }
}
