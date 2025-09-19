﻿using Domain.Entities.Agencia;
using Domain.Entities.Menu;
using Domain.Entities.Perfil;
using Domain.Enums;
using Domain.Interfaces.Getting;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCoreDatabase.DataAccess.Gettings
{
    public class AgenciaGetting : IAgenciaGettingInfrastructure
    {
        private readonly EntityFrameworkContext db;

        public AgenciaGetting(EntityFrameworkContext _db)
        {
            db = _db;
        }

        public async Task<List<DatosAgenciaEntity>> listarAgencia()
        {
            var agencias = await db.Agencia
                .Where(au => au.Isactive == true)
                .Select(x => new DatosAgenciaEntity
                {
                    agencia_id = x.Id,
                    nombre = x.Nombre.Trim(),
                    esPrincipal = false
                })
                .ToListAsync();

            return agencias;

        }

        public async Task<List<DatosAgenciaEntity>> listarAgenciasUsuario(int usuario_id, int sistema_codigo)
        {
            var agencias = new List<DatosAgenciaEntity>();

            //sistema

            var sistema = await db.Sistemas.
                Where(x => x.Codigo == sistema_codigo).
                Select(x => new DatosSistemaEntity
                {
                    sistema_id = x.Id

                }).FirstOrDefaultAsync();

            //agencias

            var agenciasUsuario = await db.AgenciaUsuarios
                .Where(x => x.UsuarioId == usuario_id && x.Isactive == true && x.AgenciaId == 0)
                .FirstOrDefaultAsync();

            if (agenciasUsuario != null)
            {
                agencias = await db.Agencia
                    .Where(b => b.Isactive == true)
                    .Select(b => new DatosAgenciaEntity
                    {
                        agencia_id = b.Id,
                        nombre = b.Nombre.Trim(),
                        esPrincipal = false
                    })
                    .ToListAsync();
            }
            else
            {
                agencias = await db.AgenciaUsuarios
                    .Where(x => x.UsuarioId == usuario_id && x.Isactive == true)
                    .Join(db.Agencia,
                          a => a.AgenciaId,
                          b => b.Id,
                          (a, b) => new DatosAgenciaEntity
                          {
                              agencia_id = a.AgenciaId,
                              nombre = b.Nombre.Trim(),
                              esPrincipal = false
                          })
                    .ToListAsync();
            }


            return agencias;
        }
    }
}
