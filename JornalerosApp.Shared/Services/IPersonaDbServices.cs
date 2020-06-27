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
        void DeletePersona(int id);
        Task<Persona> GetPersonaById(int id);
        void Save();
        void UpdatePersona(int id, Persona persona);
    }
}