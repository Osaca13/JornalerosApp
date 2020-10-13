using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<List<ListaOferta>> OfertasPorEmpresa(string id)
        {
            //string sql = "select * from dbo.Persona";
            string sql = "SELECT Oferta.Titulo,Oferta.Descripcion,Empresa.NombreEmpresa,Oferta.JornadaReal FROM dbo.Oferta INNER JOIN dbo.Empresa ON Oferta.IdEmpresa = Empresa.IdEmpresa Where Empresa.IdEmpresa = '" + id + "'";
            return await _dataAccess.LoadData<ListaOferta, dynamic>(sql, new { });
        }

        public async Task<Formacion> FormacionPorIdPersona(string id)
        {
            string sql = "SELECT Formacion.IdFormacion, Formacion.IdCurriculum, Formacion.Titulo, Formacion.FechaInicio, Formacion.FechaFin, Formacion.Centro, Formacion.Descripcion from dbo.Persona INNER JOIN dbo.Curriculum on Persona.IdPersona = Curriculum.IdPersona INNER JOIN dbo.Formacion on Curriculum.IdCurriculum = Formacion.IdCurriculum Where Persona.IdPersona =  '" + id + "'";
            var lista = await _dataAccess.LoadData<Formacion, dynamic>(sql, new { });
            return lista?.First();
        }

        public async Task<Formacion> FormacionPorId(string id)
        {
            string sql = "SELECT * From dbo.Formacion Where Formacion.IdFormacion = '" + id + "'";
            var lista = await _dataAccess.LoadData<Formacion, dynamic>(sql, new { });
            return lista != null ? (lista.Count > 0 ? lista.FirstOrDefault() : null) : null;
        }

        public async Task<List<ListaOferta>> OfertasPorParametros(string actividad, string lugar, string sector)
        {
            //string sql = "select * from dbo.Persona";
            string sql = "SELECT Oferta.Titulo, Oferta.Descripcion,Empresa.NombreEmpresa, Empresa.Actividad, Oferta.JornadaReal FROM dbo.Oferta INNER JOIN dbo.Empresa ON Oferta.IdEmpresa = Empresa.IdEmpresa Where Oferta.LugarTrabajo = '" + lugar + "' AND Oferta.Titulo = '" + actividad + " ' AND Empresa.Actividad = '" + sector + "'";

            return await _dataAccess.LoadData<ListaOferta, dynamic>(sql, new { });
        }

        public Task AddPersona(Persona persona)
        {
            string sql = "INSERT INTO dbo.Persona (Nombre,PrimerApellido,DNI,FechaNacimiento,CorreoElectronico,CochePropio,Imagen,Sexo,LugarResidencia,ProvinciaResidencia) " +
              "VALUES (@Nombre, @PrimerApellido, @DNI, @FechaNacimiento, @CorreoElectronico, @CochePropio, @Imagen, @Sexo, @LugarResidencia, @ProvinciaResidencia)";

            return _dataAccess.SaveData<Persona>(sql, persona);

        }

        public async Task<bool> DeleteFormacion(Formacion formacion)
        {
            string sql = "DELETE FROM dbo.Formacion WHERE Formacion.IdFormacion = '" + formacion.IdFormacion + "'";

            var result = await _dataAccess.SaveData<Formacion>(sql, formacion);
            return result == 1;

        }

        public async Task<bool> AddFormacion(Formacion formacion)
        {
            string sql = "INSERT INTO dbo.Formacion (IdFormacion,"
                         + " IdCurriculum, Titulo)"
                         + " VALUES ('"
                         + formacion.IdFormacion
                         + "', '"
                         + formacion.IdCurriculum
                         + "', '"
                         + formacion.Titulo
                         + "')";

            var result = await _dataAccess.SaveData<Formacion>(sql, formacion);
            return result == 1;
        }

        public async Task<bool> UpdateFormacion(Formacion formacion)
        {
            string sql = "UPDATE dbo.Formacion SET IdCurriculum = '" + formacion.IdCurriculum + "', Titulo = '" + formacion.Titulo + "', FechaInicio = '" + formacion.FechaInicio + "', FechaFin = '" + formacion.FechaFin + "', Centro = '" + formacion.Centro + "', Descripcion = '" + formacion.Descripcion + "' " +
                           "WHERE IdFormacion = '" + formacion.IdFormacion + "'";
            var result = await _dataAccess.SaveData<Formacion>(sql, formacion);
            return result == 1;
        }

        public async Task<Curriculum> GetCurriculumPorIdPersona(string Id)
        {
            string sql = "SELECT Curriculum.* from dbo.Persona INNER JOIN dbo.Curriculum on Persona.IdPersona = Curriculum.IdPersona Where Persona.IdPersona = '" + Id + "'";
            var result = await _dataAccess.LoadData<Curriculum, dynamic>(sql, new { });
            Curriculum curriculum;
            if (result != null)
            {
                curriculum = result.FirstOrDefault();
            }
            else
            {
                curriculum = new Curriculum { IdCurriculum = Guid.NewGuid().ToString(), IdPersona = Id };
                await _dataAccess.SaveData<Curriculum>(sql, curriculum);
            }
            return curriculum;
        }

        public async Task<bool> UpdateCurriculum(Curriculum curriculum)
        {
            string sql = "UPDATE dbo.Curriculum SET TramitarPermisoTrabajo = '" + curriculum.TramitarPermisoTrabajo + "',"
                + "Movilidad = '" + curriculum.Movilidad + "' ,"
                + "AlojamientoPropio = '" + curriculum.AlojamientoPropio + "' ) "
                + "WHERE Curriculum.IdCurriculum = '" + curriculum.IdCurriculum + "'";

            var result = await _dataAccess.SaveData<Curriculum>(sql, curriculum);
            return result == 1;
        }

        public async Task<string> PersonaFromIdFormacion(string id)
        {
            string sql = "SELECT Persona.IdPersona from dbo.Formacion INNER JOIN dbo.Curriculum on Formacion.IdCurriculum = Curriculum.IdCurriculum INNER JOIN dbo.Persona on Curriculum.IdPersona = Persona.IdPersona Where Formacion.IdFormacion = '" + id + "'";

            var result = await _dataAccess.LoadData<string, dynamic>(sql, new { });
            return result.FirstOrDefault();
        }

        public void DeletePersona(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Persona> GetPersonaById(string id)
        {
            try
            {
                var sql = "SELECT Nombre, PrimerApellido, DNI, FechaNacimiento, CorreoElectronico, CochePropio, Imagen, Sexo, LugarResidencia, ProvinciaResidencia  FROM dbo.Persona WHERE IdPersona = " + id;

                var lista = await _dataAccess.LoadData<Persona, dynamic>(sql, new { });

                return lista.Where(p => p.IdPersona == id).FirstOrDefault();
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                throw;
            }

        }

        public void Save()
        {

        }

        public void UpdatePersona(string id, Persona persona)
        {
            throw new NotImplementedException();
        }

        public async Task<Experiencia> GetExperiencia(string IdExperiencia)
        {
            string sql = "SELECT * FROM dbo.Experiencia WHERE IdExperiencia = '" + IdExperiencia + "'";
            List<Experiencia> result = await _dataAccess.LoadData<Experiencia, dynamic>(sql, new { });
            return result?.First();
        }
        public async Task<bool> AddExperiencia(Experiencia experiencia)
        {
            string sql = "INSERT INTO dbo.Experiencia (IdExperiencia,"
                         + " IdCurriculum, Empresa, Puesto, DescripcionPuesto)"
                         + " VALUES ('"
                         + experiencia.IdExperiencia
                         + "', '"
                         + experiencia.IdCurriculum
                         + "', '"
                         + experiencia.Empresa
                          + "', '"
                         + experiencia.Puesto
                          + "', '"
                         + experiencia.DescripcionPuesto
                         + "')"; 

            var result = await _dataAccess.SaveData<Experiencia>(sql, experiencia);
            return result == 1;
        }

        public async Task<bool> UpdateExperiencia(Experiencia experiencia)
        {
            string sql = "UPDATE dbo.Experiencia SET IdCurriculum = '" + experiencia.IdCurriculum + "', Puesto = '" + experiencia.Puesto + "', FechaInicio = '" + experiencia.FechaInicio + "', FechaFin = '" + experiencia.FechaFin + "', Empresa = '" + experiencia.Empresa + "', DescripcionPuesto = '" + experiencia.DescripcionPuesto + "' " +
                           "WHERE IdExperiencia = '" + experiencia.IdExperiencia + "'";
            var result = await _dataAccess.SaveData<Experiencia>(sql, experiencia);

            return result == 1;
        }

        public async Task<bool> DeleteExperiencia(Experiencia experiencia)
        {
            string sql = "DELETE FROM dbo.Experiencia WHERE IdExperiencia = '" + experiencia.IdExperiencia + "'";
            var result = await _dataAccess.SaveData<Experiencia>(sql, experiencia);

            return result == 1;
        }

        public async Task<List<Experiencia>> ExperienciaPorIdPersona(string id)
        {
            string sql = "SELECT Experiencia.IdExperiencia, Experiencia.IdCurriculum, Experiencia.Empresa, Experiencia.Puesto, Experiencia.FechaInicio, Experiencia.FechaFin, Experiencia.DescripcionPuesto from dbo.Persona INNER JOIN dbo.Curriculum on Persona.IdPersona = Curriculum.IdPersona INNER JOIN dbo.Experiencia on Curriculum.IdCurriculum = Experiencia.IdCurriculum Where Persona.IdPersona =  '" + id + "'";
            var lista = await _dataAccess.LoadData<Experiencia, dynamic>(sql, new { });
            return lista ?? null;
        }

        public async Task<List<EstudiosPorNiveles>> NivelesFormativo()
        {
            string sql = "SELECT * From dbo.EstudiosPorNiveles";
            var lista = await _dataAccess.LoadData<EstudiosPorNiveles, dynamic>(sql, new { });
            return lista ?? null;
        }

        public async Task<Permiso> PermisosPorIdPersona(string id)
        {
            string sql = "SELECT Permiso.IdPermisos, Permiso.IdPersona, Permiso.Tipo from dbo.Persona INNER JOIN dbo.Permiso on Persona.IdPersona = Permiso.IdPersona Where Persona.IdPersona =  '" + id + "'";
            var lista = await _dataAccess.LoadData<Permiso, dynamic>(sql, new { });
            return lista?.First();
        }

        public async Task<bool> UpdatePermisos(Permiso permiso)
        {
            string sql = "UPDATE dbo.Permiso SET Tipo = '" + permiso.Tipo + "' WHERE IdPermisos= '" + permiso.IdPermisos + "' AND IdPersona = '"+ permiso.IdPersona +"'";
            var result = await _dataAccess.SaveData<Permiso>(sql, permiso);
            return result == 1;
        }

        public async Task<bool> AddPermisos(Permiso permiso)
        {
            string sql = "INSERT INTO dbo.Permiso" +
                " (IdPermisos, IdPersona, Tipo)" +
                " VALUES ('"+permiso.IdPermisos+"', '"+permiso.IdPersona+"' , '"+permiso.Tipo+"')";
            var result = await _dataAccess.SaveData<Permiso>(sql, permiso);
            return result == 1;
        }
    }
}
