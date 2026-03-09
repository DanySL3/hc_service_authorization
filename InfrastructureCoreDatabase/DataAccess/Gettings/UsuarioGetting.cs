using Domain.Entities;
using Domain.Entities.Agencia;
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

        public async Task<List<ConsultarDetalleUsuarioEntity>> buscarUsuario(int usuario_id, string documento_numero, string nombre)
        {
            //usuario

            var query = db.Usuarios
                    .Where(u => u.Isactive == true);

            if (usuario_id != 0)
            {
                query = query.Where(u => u.Id == usuario_id);
            }

            if (!string.IsNullOrWhiteSpace(documento_numero))
            {
                query = query.Where(u => u.DocumentoNumero!.ToUpper().Contains(documento_numero.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(u => u.Nombre.ToUpper().Contains(nombre.ToUpper()));
            }

            var usuarios = await query
                .Join(db.CargoUsuarios.Where(cu => cu.Isactive == true),
                    u => u.Id,
                    cu => cu.UsuarioId,
                    (u, cu) => new { u, cu })
                .Join(db.Cargos,
                    temp => temp.cu.CargoId,
                    c => c.Id,
                    (temp, c) => new { temp.u, c })
                .Select(x => new ConsultarDetalleUsuarioEntity
                {
                    usuario_id = x.u.Id,
                    usuario_estado_id = x.u.UsuarioEstadoId,
                    correo = x.u.Correo == null ? "" : x.u.Correo,
                    cargo_id = x.c.Id,
                    cargo = x.c.Cargo1,
                    nombre = x.u.Nombre,
                    usuario = x.u.Usuario1,
                    documento_numero = x.u.DocumentoNumero ?? "",
                    usuario_estado = x.u.UsuarioEstadoId == 1 ? "Vigente" : 
                                     x.u.UsuarioEstadoId == 2 ? "Suspendido" : "Bloqueado"
                })
                .ToListAsync();

            if (usuarios == null) return null;

            //agencias

            var agenciasUsuario = await db.AgenciaUsuarios
                 .Where(x => x.UsuarioId == usuario_id && x.Isactive == true && x.AgenciaId == 0)
                 .FirstOrDefaultAsync();

            foreach (var usuario in usuarios)
            {
                if (agenciasUsuario != null)
                {
                    usuario.lstAgencias = await db.Agencia
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
                    usuario.lstAgencias = await db.AgenciaUsuarios
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
            }

            return usuarios;
        }

        public async Task<List<ListarCargosEntity>> listarCargos()
        {
            var datos = await db.Cargos.
                    Where(x => x.Isactive == true).
                    Select(x => new ListarCargosEntity
                    {
                        cargo_id = x.Id,
                        cargo_padre_id = x.CargoPadreId ?? 0,
                        nombre = x.Cargo1

                    }).ToListAsync();


            datos = datos.OrderBy(x => x.cargo_padre_id).ToList();

            return datos;
        }

        public async Task<List<ListaPrivilegiosEntity>> listarAccesos(int sistema_id, int usuario_id, string documento_numero)
        {
            var accesos = await db.Database.SqlQuery<ListaPrivilegiosEntity>(
                $"""

                SELECT
                    A.id as usuario_id,
                    A.nombre,
                    A.documento_numero,
                    COALESCE(CAST(A.correo AS CHAR(50)), '') AS correo,
                    COALESCE(CAST(G.cargo AS CHAR(50)), '') AS cargo,
                    C.id AS sistema_id,
                    C.nombre AS sistema,
                    COALESCE(E.id, 0) AS perfil_id,
                    COALESCE(E.perfil, '') AS perfil,
                    COALESCE(CAST(D.fecha_inicio AS CHAR(10)), CAST(B.fecha_inicio AS CHAR(10))) AS fechaIniAcceso,
                    COALESCE(CAST(D.fecha_fin AS CHAR(10)), 'Acceso total habilitado.') AS fechaFinAcceso
                FROM Usuario A
                INNER JOIN sistema_usuario B ON
                    B.usuario_id = A.id
                    AND B.isactive = true
                    AND CAST(NOW() AS DATE) >= CAST(B.fecha_inicio AS DATE)
                    AND (B.fecha_fin IS NULL OR CAST(NOW() AS DATE) <= CAST(B.fecha_fin AS DATE))
                INNER JOIN Sistema C ON C.id = B.sistema_id
                LEFT JOIN perfil_usuario D ON D.sistema_id = B.sistema_id AND D.usuario_id = B.usuario_id AND D.isactive = true
                LEFT JOIN Perfil E ON E.id = D.perfil_id
                LEFT JOIN cargo_usuario F ON A.id = F.usuario_id
                LEFT JOIN cargo G on F.cargo_id = G.id
                WHERE 
                    A.isactive = true
                   AND ({sistema_id} = 0 OR B.sistema_id = {sistema_id})
                   AND ({usuario_id} = 0 OR A.id = {usuario_id})
                   AND ({documento_numero} = '' OR A.documento_numero LIKE CONCAT('%' || {documento_numero} || '%'))
                ORDER BY A.id desc

                
                """).ToListAsync();

            return accesos;
        }

        public async Task<paginationEntity<ListarUsuarioEntity>> listarUsuarios(int index, int cantidad)
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
                    END AS usuario_estado,
                    COUNT(*) OVER() AS total_count
                FROM usuario A
                INNER JOIN cargo_usuario B
                    ON A.id = B.usuario_id
                   AND B.isactive = true
                INNER JOIN cargo C
                    ON B.cargo_id = C.id
                WHERE A.isactive = true
                ORDER BY A.id desc
                offset {index * cantidad} limit {cantidad}
                
                """).ToListAsync();

            var datos = new paginationEntity<ListarUsuarioEntity>
            {
                page_index = index,
                page_size = cantidad,
                total_count = usuarios.FirstOrDefault()?.total_count ?? 0,
                data = usuarios
            };

            return datos;
        }
    }
}
