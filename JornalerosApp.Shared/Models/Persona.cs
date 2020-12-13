using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace JornalerosApp.Shared.Models
{
    public partial class Persona
    {       
        public Persona()
        {
            
            Nacionalidad = new HashSet<Nacionalidad>();
            Permiso = new HashSet<Permiso>();
            RelacionOfertaPersona = new HashSet<RelacionOfertaPersona>();
        }

        [Key]
        [Required]
        public string IdPersona { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Nombre requerido")]
        [DataType(DataType.Text, ErrorMessage = "Texto no válido")]
        [StringLength(100, ErrorMessage ="Debe tener más de 2 y menos de 100 caracteres", MinimumLength =2)]
        public string Nombre { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Texto no válido")]
        [StringLength(100, ErrorMessage ="Debe tener más de 2 y menos de 100 caracteres", MinimumLength =2)]
        public string PrimerApellido { get; set; }
        
        [Required(AllowEmptyStrings = true, ErrorMessage ="Documento requerido")]
        [StringLength(9)]    
        [NifValidator(ErrorMessage ="Documento no válido")]
        public string Dni { get; set; }

        [DataType(DataType.Date, ErrorMessage ="Fecha no válida")]        
        public DateTime? FechaNacimiento { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Dirección de correo no válida")]
        [EmailAddress(ErrorMessage = "Dirección de correo no válida")]
        public string CorreoElectronico { get; set; }
        public string CochePropio { get; set; }
        public string Imagen { get; set; }
        public string Sexo { get; set; }
        public string LugarResidencia { get; set; }
        public string ProvinciaResidencia { get; set; }

        public virtual Curriculum Curriculum { get; set; }
        public virtual ICollection<Nacionalidad> Nacionalidad { get; set; }
        public virtual ICollection<Permiso> Permiso { get; set; }
        public virtual ICollection<RelacionOfertaPersona> RelacionOfertaPersona { get; set; }        
    }
}
