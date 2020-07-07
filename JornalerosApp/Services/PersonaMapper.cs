using AutoMapper;
using JornalerosApp.Shared.Models;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public class PersonaMapper : Profile
    {
        public PersonaMapper()
        {
            CreateMap<ItemCollection, Persona>();
        }

        public override string ProfileName => base.ProfileName;
    }
}
