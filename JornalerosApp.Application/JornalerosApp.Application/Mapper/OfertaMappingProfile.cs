using AutoMapper;
using JornalerosApp.Application.Commands;
using JornalerosApp.Application.Responses;
using JornalerosApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JornalerosApp.Application.Mapper
{
    public class OfertaMappingProfile : Profile
    {
        public OfertaMappingProfile()
        {
            CreateMap<Oferta, CheckOutOfertaCommand>().ReverseMap();
            CreateMap<Oferta, OfertaResponse>().ReverseMap();
        }
    }
}
