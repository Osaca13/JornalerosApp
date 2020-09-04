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

        Task<List<ListaOferta>> OfertasPorParametros(string actividad, string lugar, string sector);

        Task<List<Formacion>> FormacionPorIdPersona(string id);

        Task<Formacion> FormacionPorId(string id); 
        void DeletePersona(string id);

        Task<bool> DeleteFormacion(Formacion formacion);

        Task<bool> AddFormacion(Formacion formacion);

        Task<bool> UpdateFormacion(Formacion formacion);

        Task<Curriculum> GetCurriculumPorIdPersona(string Id);

        Task<bool> UpdateCurriculum(Curriculum curriculum);


        //Experiencia

        Task<bool> AddExperiencia(Experiencia experiencia);

        Task<bool> UpdateExperiencia(Experiencia experiencia);

        Task<List<Experiencia>> ExperienciaPorIdPersona(string id);

        Task<string> PersonaFromIdFormacion(string id);
        Task<Persona> GetPersonaById(string id);
        void UpdatePersona(string id, Persona persona);

        void Save();
    }
}