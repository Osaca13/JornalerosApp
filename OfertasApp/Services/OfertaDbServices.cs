using Microsoft.EntityFrameworkCore;
using OfertasApp.Data;
using OfertasApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OfertasApp.Services
{
    public class OfertaDbServices : DbServices<Oferta>, IOfertaDbServices
    {
        public OfertaDbServices(OfertaDataContext ofertaDataContext) : base(ofertaDataContext)
        {

        }

        public async Task<IReadOnlyList<Oferta>> GetByTituloAsync(string titulo)
        {
            var data = _dbContext.Oferta;
            return await data.Where(x => x.Titulo == titulo).ToListAsync();
        }

        public async Task<Oferta> AddOfertaAsync(Oferta oferta)
        {
            try
            {
                _dbContext.Oferta.Add(oferta);
                await _dbContext.SaveChangesAsync();
                return oferta;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                throw;
            }            
        }
    }
}
