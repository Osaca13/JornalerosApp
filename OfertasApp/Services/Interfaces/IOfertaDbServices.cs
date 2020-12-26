using OfertasApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfertasApp.Services.Interfaces
{
    public interface IOfertaDbServices 
    {
        Task<IReadOnlyList<Oferta>> GetByTituloAsync(string titulo);

        Task<Oferta> AddOfertaAsync(Oferta oferta);
        
    }
}