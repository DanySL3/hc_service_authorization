using Domain.Entities.Autenticacion;
using Domain.Entities.Perfil;
using Domain.Entities.Usuario;
using Domain.Enums;
using Domain.Interfaces.Getting;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCoreDatabase.DataAccess.Gettings
{
    public class PerfilGetting : IPerfilGettingInfrastructure
    {
        private readonly EntityFrameworkContext db;

        public PerfilGetting(EntityFrameworkContext _db)
        {
            db = _db;
        }

        public async Task<List<DatosPerfilEntity>> listarPerfiles(int sistema_id)
        {
            var perfiles = new List<DatosPerfilEntity>();

            perfiles = await db.Perfils.
                Where(x => x.Isactive == true && (x.SistemaId  == null || x.SistemaId == sistema_id)).
                Select(x => new DatosPerfilEntity
                {
                    perfil_id = x.Id,
                    perfil = x.Perfil1,
                    descripcion = x.Descripcion ?? "",
                    codigo = x.Codigo,
                    esPrincipal = false

                }).ToListAsync();
            

            return perfiles;
        }

        public async Task<List<DatosPerfilEntity>> obtenerPerfilUsuario(int usuario_id, int idSistema)
        {
            //perfil

            var perfiles = new List<DatosPerfilEntity>();

            var fecha = DateOnly.FromDateTime(DateTime.Now);

            perfiles = await db.PerfilUsuarios
                .Where(u =>
                    u.UsuarioId == usuario_id &&
                    u.Isactive == true &&
                    u.SistemaId == idSistema &&
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
                        descripcion = p.Descripcion ?? "",
                        codigo = p.Codigo,
                        esPrincipal = false
                    })
                .ToListAsync();

 

            return perfiles;

        }

        public async Task<List<DatosPerfilEntity>> obtenerPerfilesFaltantes(int sistema_id, int usuario_id)
        {
            var perfiles = await db.Database.SqlQuery<DatosPerfilEntity>(
                $"""

                SELECT
                    a.id AS perfil_id,
                    a.perfil AS perfil,
                    a.codigo AS codigo,
                    '' AS descripcion,
                    FALSE AS esPrincipal
                FROM perfil a
                LEFT JOIN perfil_usuario b
                    ON a.id = b.perfil_id
                    AND b.usuario_id = {usuario_id}
                    AND b.sistema_id = {sistema_id}
                    AND b.isactive = true
                    AND (now() >= b.fecha_inicio AND (b.fecha_fin is null or now() <= b.fecha_fin))
                WHERE
                    b.id IS NULL
                    AND a.isactive = true
                    AND (a.sistema_id is null or a.sistema_id = {sistema_id})
                
                """).ToListAsync();

            return perfiles;
        }
    }
}
