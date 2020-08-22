using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JornalerosApp.Data;
using JornalerosApp.Shared.Data;
using JornalerosApp.Shared.Entities;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace JornalerosApp.Services
{
    public class SQLDatabaseServices : ISQLDatabaseServices
    {
        private readonly ISqlDataAccess _dataAccess;

        public SQLDatabaseServices(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<ListaOferta>> OfertasPorEmpresa(string id)
        {
            //string sql = "select * from dbo.Persona";
            string sql = "SELECT Oferta.Titulo,Oferta.Descripcion,Empresa.NombreEmpresa,Oferta.JornadaReal FROM dbo.Oferta INNER JOIN dbo.Empresa ON Oferta.IdEmpresa = Empresa.IdEmpresa Where Empresa.IdEmpresa = '"+id+"'";
            return await _dataAccess.LoadData<ListaOferta, dynamic>(sql, new { });
        }

        public async Task<List<ListaOferta>> OfertasPorParametros(string actividad, string lugar)
        {
            //string sql = "select * from dbo.Persona";
            string sql = "SELECT Oferta.Titulo, Oferta.Descripcion,Empresa.NombreEmpresa,Oferta.JornadaReal FROM dbo.Oferta INNER JOIN dbo.Empresa ON Oferta.IdEmpresa = Empresa.IdEmpresa Where Oferta.LugarTrabajo = '"+ lugar +"' AND Oferta.Titulo = '"+actividad+" '";

            return await _dataAccess.LoadData<ListaOferta, dynamic>(sql, new { });
        }

        public Task AddPersona(Persona persona)
        {
            string sql = "INSERT INTO dbo.Persona (Nombre,PrimerApellido,DNI,FechaNacimiento,CorreoElectronico,CochePropio,Imagen,Sexo,LugarResidencia,ProvinciaResidencia) " +
              "VALUES (@Nombre, @PrimerApellido, @DNI, @FechaNacimiento, @CorreoElectronico, @CochePropio, @Imagen, @Sexo, @LugarResidencia, @ProvinciaResidencia)";

            return _dataAccess.SaveData<Persona>(sql, persona);
            
        }

        public void DeletePersona(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Persona>> GetPersonaById(int id)
        {
            var sql = "SELECT Nombre, PrimerApellido, DNI, FechaNacimiento, CorreoElectronico, CochePropio, Imagen, Sexo, LugarResidencia, ProvinciaResidencia  FROM dbo.Persona WHERE IdPersona = " + id;

            return _dataAccess.LoadData<Persona, dynamic>(sql, new { });
            

        }

        public void UpdatePersona(int id, Persona persona)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
