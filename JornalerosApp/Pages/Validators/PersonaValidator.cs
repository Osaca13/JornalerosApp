using FluentValidation;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Pages.Validators
{
    public class PersonaValidator : AbstractValidator<Persona>
    {       
        public PersonaValidator()
        {
           
            RuleFor(x => x.Nombre).NotEmpty().NotNull().Length(2,150).WithMessage("Nombre no debe estar vacío");
            RuleFor(x => x.PrimerApellido).Length(2,150).WithMessage("Debe contener más de dos caracteres");
            RuleFor(x => x.Dni).NotEmpty().Length(9, 9).WithMessage("Documento no válido");
                
            RuleFor(x => x.CorreoElectronico).EmailAddress().WithMessage("Dirección de correo no válida");
        }
    }
}
