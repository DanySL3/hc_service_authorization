using Domain.Entities.Autenticacion;
using Domain.Entities.Perfil;
using Domain.Entities.Sistema;
using Domain.Enums;
using Domain.Interfaces.Getting;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCoreDatabase.DataAccess.Gettings
{
    public class SistemaGetting : ISistemaGettingInfrastructure
    {
        private readonly EntityFrameworkContext db;

        public SistemaGetting(EntityFrameworkContext _db)
        {
            db = _db;
        }
        public async Task<List<DatosAplicacionEntity>> listarSistemas()
        {
            var sistema = await db.Sistemas.
                    Select(x => new DatosAplicacionEntity
                    {
                        sistema_uuid = x.Uuid,
                        sistema_id = x.Id,
                        nombre = x.Nombre,
                        icono = "",
                        url = "",
                        codigo = 0

                    }).ToListAsync();

            return sistema;
        }

        public async Task<List<DatosAplicacionEntity>> listarSistemasUsuario(int usuario_id)
        {
            var fecha = DateOnly.FromDateTime(DateTime.Now);

            var sistemas = await db.SistemaUsuarios
                .Where(u => u.UsuarioId == usuario_id && u.IsActive == true)
                .Join(
                    db.Sistemas,
                    pu => pu.SistemaId,
                    p => p.Id,
                    (pu, p) => new { pu, p }
                )
                .Where(x =>
                    x.p.IsActive == true &&
                    fecha >= x.pu.FechaInicio &&
                    (x.pu.FechaFin == null || fecha <= x.pu.FechaFin)
                )
                .Select(x => new DatosAplicacionEntity
                {
                    sistema_uuid = x.p.Uuid,
                    sistema_id = x.p.Id,
                    nombre = x.p.Nombre,
                    icono = x.p.Icon ?? "",
                    url = x.p.Url ?? "",
                    codigo = 0
                })
                .ToListAsync();
                    

            return sistemas;
        }
    }
}
