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
                    A.usuario_id AS nCodigoCliente,
                    A.cNombre AS cNombre,
                    C.sistema_id,
                    C.cNombre AS cSistema,
                    CAST(D.dFecIni AS CHAR(10)) AS cFechaIniAcceso,
                    IFNULL(CAST(D.dFecFin AS CHAR(10)), 'Sin limite') AS cFechaFinAcceso,
                    E.idPerfil,
                    E.cPerfil AS cPrivilegio
                FROM Usuario A
                INNER JOIN SistemaByUsuario B ON 
                    B.usuario_id = A.usuario_id
                    AND B.lActivo = 1
                    AND (B.dFecIni IS NULL OR CAST(NOW() AS DATE) >= CAST(B.dFecIni AS DATE))
                    AND (B.dFecFin IS NULL OR CAST(NOW() AS DATE) <= CAST(B.dFecFin AS DATE))
                INNER JOIN Sistema C ON C.sistema_id = B.sistema_id
                INNER JOIN PerfilByUsuario D ON D.sistema_id = B.sistema_id AND D.usuario_id = B.usuario_id AND D.lVigente = 1
                INNER JOIN Perfil E ON E.idPerfil = D.idPerfil
                WHERE 
                    A.lActivo = 1
                    AND B.sistema_id = IF({sistema_id} != 0, {sistema_id}, B.sistema_id)
                    AND A.usuario_id = IF({usuario_id} != 0, {usuario_id}, A.usuario_id)
                    AND A.cNumeroDocumento LIKE CONCAT('%', {documento_numero}, '%') 
                ORDER BY A.usuario_id

                
                """).ToListAsync();

            return accesos;
        }

        public async Task<List<ListarUsuarioEntity>> listarUsuarios()
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
