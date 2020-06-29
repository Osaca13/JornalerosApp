using JornalerosApp.Shared.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JornalerosApp.Shared.Services
{
    public interface IPersonaDbServices
    {
        Task<EntityEntry<Persona>> AddPersona(Persona persona);
        Task<List<Persona>> AllPersonas();
        Task<EntityEntry<Persona>> DeletePersona(string id);
        Task<Persona> GetPersonaById(string id);
        void Save();
        EntityEntry<Persona> UpdatePersona(string id, Persona persona);
    }
}