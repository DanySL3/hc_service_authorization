using Domain.Entities.Agencia;
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

        public async Task<List<DatosAgenciaEntity>> obtenerAgencia(int usuario_id, int sistema_id)
        {
            var agencias = new List<DatosAgenciaEntity>();

            agencias = await db.AgenciaUsuarios
                .Where(x => x.UsuarioId == usuario_id && x.Isactive == true)
                .Join(db.Agencia,
                    a => a.AgenciaId,
                    b => b.Id,
                    (a, b) => new { a, b }
                )
                .Select(x => new DatosAgenciaEntity 
                {
                    agencia_id = x.a.Id,
                    nombre = x.b.Nombre.Trim(),
                    esPrincipal = x.a.Esprincipal
                })
                .ToListAsync();

            return agencias;
        }
    }
}
