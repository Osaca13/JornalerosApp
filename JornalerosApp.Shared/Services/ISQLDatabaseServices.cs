using JornalerosApp.Shared.Entities;
using JornalerosApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JornalerosApp.Shared.Services
{
    public interface ISQLDatabaseServices
    {
        Task AddPersona(Persona persona);
        Task<List<ListaOferta>> OfertasPorEmpresa(string id);

        Task<List<ListaOferta>> OfertasPorParametros(string actividad, string lugar);
        void DeletePersona(int id);
        Task<List<Persona>> GetPersonaById(int id);
        void UpdatePersona(int id, Persona persona);

        void Save();
    }
}