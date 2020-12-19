using AutoMapper;
using EventBusRabbitMQ.Events;
using JornalerosApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresasApp.Mapping
{
    public class OfertaMapping : Profile
    {
        public OfertaMapping()
        {
            CreateMap<Oferta, OfertaCheckoutEvent>().ReverseMap();
        }
    }
}
