using AutoMapper;
using JornalerosApp.Shared.Models;
using OfertasApp.Commands;
using OfertasApp.Responses;

namespace OfertasApp.Mapper
{
    public class OfertaMappingProfile : Profile
    {
        public OfertaMappingProfile()
        {
            CreateMap<Data.Oferta, CheckOutOfertaCommand>().ReverseMap();
            CreateMap<Data.Oferta, OfertaResponse>().ReverseMap();
        }
    }
}
