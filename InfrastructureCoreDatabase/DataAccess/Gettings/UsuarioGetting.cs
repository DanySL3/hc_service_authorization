using Domain.Entities.Autenticacion;
using Domain.Entities.Sistema;
using Domain.Entities.Usuario;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Getting;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCoreDatabase.DataAccess.Gettings
{
    public class UsuarioGetting : IUsuarioGettingInfrastructure
    {
        private readonly EntityFrameworkContext db;
        private readonly IHelperCommon helper;

        public UsuarioGetting(EntityFrameworkContext _db, IHelperCommon _helper)
        {
            db = _db;
            helper = _helper;
        }

        public async Task<ListarUsuarioEntity> buscarUsuario(int usuario_id, string documento_numero)
        {
            var usuario = await db.Database.SqlQuery<ListarUsuarioEntity>(
                $"""

                SELECT
                    A.id AS usuario_id,
                    B.cargo_id,
                    C.cargo,
                    A.nombre,
                    A.usuario,
                    A.documento_numero,
                    A.correo,
                    A.usuario_estado_id,
                    CASE
                        WHEN A.usuario_estado_id = 1 THEN 'Activo'
                        ELSE 'Suspendido'
                    END AS usuario_estado
                FROM usuario A
                INNER JOIN cargo_usuario B
                    ON A.id = B.usuario_id
                   AND B.isactive = true
                INNER JOIN cargo C
                    ON B.cargo_id = C.id
                WHERE
                    A.isactive = true
                    AND A.id = CASE WHEN {usuario_id} != 0 THEN {usuario_id} ELSE A.id END
                    AND A.documento_numero LIKE CONCAT('%', {documento_numero}, '%')
                
                """).FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<List<ListarCargosEntity>> listarCargos()
        {
            var datos = await db.Cargos.
                    Where(x => x.Isactive == true).
                    Select(x => new ListarCargosEntity
                    {
                        cargo_id = x.Id,
                        nombre = x.Cargo1

                    }).ToListAsync();

            return datos;
        }

        public async Task<List<ListaPrivilegiosEntity>> listarAccesos(int sistema_id, int usuario_id, string documento_numero)
        {
            var accesos = await db.Database.SqlQuery<ListaPrivilegiosEntity>(
                $"""

                SELECT
                    A.id as usuario_id,
                    A.nombre,
                    C.id AS sistema_id,
                    C.nombre AS sistema,
                    CAST(D.fecha_inicio AS CHAR(10)) AS fechaIniAcceso,
                    COALESCE(CAST(D.fecha_fin AS CHAR(10)), 'Sin limite') AS fechaFinAcceso,
                    E.id AS perfil_id,
                    E.perfil
                FROM Usuario A
                INNER JOIN sistema_usuario B ON
                    B.usuario_id = A.id
                    AND B.isactive = true
                    AND CAST(NOW() AS DATE) >= CAST(B.fecha_inicio AS DATE)
                    AND (B.fecha_fin IS NULL OR CAST(NOW() AS DATE) <= CAST(B.fecha_fin AS DATE))
                INNER JOIN Sistema C ON C.id = B.sistema_id
                INNER JOIN perfil_usuario D ON D.sistema_id = B.sistema_id AND D.usuario_id = B.usuario_id AND D.isactive = true
                INNER JOIN Perfil E ON E.id = D.perfil_id
                WHERE 
                    A.isactive = true
                   AND ({sistema_id} = 0 OR B.sistema_id = {sistema_id})
                   AND ({usuario_id} = 0 OR A.id = {usuario_id})
                   AND ({documento_numero} = '' OR A.documento_numero LIKE CONCAT('%' + {documento_numero} + '%'))
                ORDER BY A.id desc

                
                """).ToListAsync();

            return accesos;
        }

        public async Task<List<ListarUsuarioEntity>> listarUsuarios(int index, int cantidad)
        {
            var usuarios = await db.Database.SqlQuery<ListarUsuarioEntity>(
                $"""

                SELECT
                    A.id AS usuario_id,
                    B.cargo_id,
                    C.cargo,
                    A.nombre,
                    A.usuario,
                    A.documento_numero,
                    A.correo,
                    A.usuario_estado_id,
                    CASE
                        WHEN A.usuario_estado_id = 1 THEN 'Activo'
                        ELSE 'Suspendido'
                    END AS usuario_estado
                FROM usuario A
                INNER JOIN cargo_usuario B
                    ON A.id = B.usuario_id
                   AND B.isactive = true
                INNER JOIN cargo C
                    ON B.cargo_id = C.id
                WHERE A.isactive = true
                
                """).ToListAsync();

            return usuarios;
        }
    }
}
