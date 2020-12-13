using AutoMapper;
using JornalerosApp.Application.Commands;
using JornalerosApp.Application.Responses;
using JornalerosApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JornalerosApp.Application.Mapper
{
    public class PersonaMappingProfile : Profile
    {
        public PersonaMappingProfile()
        {
            CreateMap<Persona, CheckOutPersonaCommand>().ReverseMap();
            CreateMap<Persona, PersonaResponse>().ReverseMap();
        }
    }
}
