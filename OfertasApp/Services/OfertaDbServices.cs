using OfertasApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertasApp.Services
{
    public class OfertaDbServices : DbServices<Oferta>
    {
        public OfertaDbServices(OfertaDataContext ofertaDataContext) : base(ofertaDataContext)
        {

        }
    }
}
