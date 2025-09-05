using Domain.Entities.Autenticacion;
using Domain.Entities.Perfil;
using Domain.Entities.Sistema;
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
                .Where(u => u.UsuarioId == usuario_id && u.Isactive == true)
                .Join(
                    db.Sistemas,
                    pu => pu.SistemaId,
                    p => p.Id,
                    (pu, p) => new { pu, p }
                )
                .Where(x =>
                    x.p.Isactive == true &&
                    fecha >= x.pu.FechaInicio &&
                    (x.pu.FechaFin == null || fecha <= x.pu.FechaFin)
                )
                .Select(x => new DatosAplicacionEntity
                {
                    sistema_id = x.p.Id,
                    nombre = x.p.Nombre,
                    icono = x.p.Icon ?? "",
                    url = x.p.Url ?? "",
                    codigo = x.p.Codigo
                })
                .ToListAsync();
                    

            return sistemas;
        }

        public async Task<DatosSistemaEntity> obtenerIdentiticador(int sistema_codigo)
        {
            var sistema = await db.Sistemas.
                    Where(x => x.Codigo == sistema_codigo).
                    Select(x => new DatosSistemaEntity
                    {
                        sistema_id = x.Id

                    }).FirstOrDefaultAsync();

            return sistema;
        }
    }
}
