using Domain.Entities.Autenticacion;
using Domain.Entities.Perfil;
using Domain.Entities.Usuario;
using Domain.Enums;
using Domain.Interfaces.Getting;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureCoreDatabase.DataAccess.Gettings
{
    public class PerfilGetting : IPerfilGettingInfrastructure
    {
        private readonly EntityFrameworkContext db;

        public PerfilGetting(EntityFrameworkContext _db)
        {
            db = _db;
        }

        public async Task<List<DatosPerfilEntity>> listarPerfiles()
        {
            var perfiles = new List<DatosPerfilEntity>();

            perfiles = await db.Perfils.
                Where(x => x.Isactive == true).
                Select(x => new DatosPerfilEntity
                {
                    perfil_id = x.Id,
                    perfil = x.Perfil1,
                    codigo = x.Codigo,
                    esPrincipal = false

                }).ToListAsync();
            

            return perfiles;
        }

        public async Task<List<DatosPerfilEntity>> obtenerPerfilUsuario(int usuario_id, int sistema_codigo)
        {
            //sistema

            var sistema = await db.Sistemas.
                Where(x => x.Codigo == sistema_codigo).
                Select(x => new DatosSistemaEntity
                {
                    sistema_id = x.Id

                }).FirstOrDefaultAsync();

            //perfil

            var perfiles = new List<DatosPerfilEntity>();

            var fecha = DateOnly.FromDateTime(DateTime.Now);

            perfiles = await db.PerfilUsuarios
                .Where(u =>
                    u.UsuarioId == usuario_id &&
                    u.Isactive == true &&
                    u.SistemaId == sistema.sistema_id &&
                    fecha >= u.FechaInicio &&
                    (u.FechaFin == null || fecha <= u.FechaFin)
                )
                .Join(
                    db.Perfils,
                    pu => pu.PerfilId,
                    p => p.Id,
                    (pu, p) => new DatosPerfilEntity
                    {
                        perfil_id = p.Id,
                        perfil = p.Perfil1 ?? "",
                        codigo = p.Codigo,
                        esPrincipal = false
                    })
                .ToListAsync();

 

            return perfiles;

        }

        public async Task<List<DatosPerfilEntity>> obtenerPerfilNoAsginado(int sistema_id, int usuario_id)
        {
            var perfiles = await db.Database.SqlQuery<DatosPerfilEntity>(
                $"""

                SELECT
                   	a.id AS perfil_id,
                   	a.perfil AS perfil,
                   	a.codigo AS codigo,
                   	FALSE AS esPrincipal
                FROM perfil a
                LEFT JOIN perfil_usuario b
                  ON a.id = b.perfil_id
                 AND b.usuario_id = {usuario_id}
                 AND b.sistema_id = {sistema_id}
                 AND b.isactive = true
                WHERE 
                  b.id IS NULL
                  OR now() < b.fecha_inicio
                  OR now() > b.fecha_fin
                  AND a.isactive = true
                
                """).ToListAsync();

            return perfiles;
        }
    }
}
