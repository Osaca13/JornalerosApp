using JornalerosApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JornalerosApp.Shared.Services
{
    public interface IPersonaServices
    {
        void AddPersona(Persona persona);
        Task<IEnumerable<Persona>> AllPersonas();
        void DeletePersona(int id);
        Persona GetPersonaById(int id);
        void UpdatePersona(int id, Persona persona);
    }
}