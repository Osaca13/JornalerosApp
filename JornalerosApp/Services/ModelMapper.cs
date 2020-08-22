using AutoMapper;
using JornalerosApp.Shared.Entities;
using JornalerosApp.Shared.Models;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<Persona, PersonaModel>().ReverseMap();          
            CreateMap<Empresa, EmpresaModel>().ReverseMap();
            CreateMap<Oferta, OfertaModel>().ReverseMap();            
        }

        public override string ProfileName => base.ProfileName;
    }
}
