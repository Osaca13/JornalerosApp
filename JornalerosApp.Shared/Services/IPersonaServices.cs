﻿using JornalerosApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JornalerosApp.Shared.Services
{
    public interface IPersonaServices
    {
        Task AddPersona(Persona persona);
        Task<List<Persona>> AllPersonas();
        void DeletePersona(int id);
        Task<List<Persona>> GetPersonaById(int id);
        void UpdatePersona(int id, Persona persona);

        void Save();
    }
}