using Domain.Commons;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace InfrastructureCoreDatabase.DataAccess.Gettings
{
    public class UsuarioMethod : IUsuarioInfrastructure
    {
        private readonly EntityFrameworkContext db;
        private readonly IHelperCommon objHelperCommon;

        public UsuarioMethod(EntityFrameworkContext _db, IHelperCommon _objHelperCommon)
        {

            db = _db;
            objHelperCommon = _objHelperCommon;
        }

        public async Task<TransaccionEntity> actualizarUsuario(int cargo_id, string nombre, string documento_numero, string correo, string usuario, int usuario_id, List<int> agencia_ids, int usuario_modifica_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var usuarioRegistrado = db.Usuarios.Where(x => x.Correo == correo && x.Id != usuario_id).FirstOrDefault();

                    if (usuarioRegistrado != null) return new TransaccionEntity { Code = false, ID = 0, Message = "el correo ya se encuentra registrado" };

                    //usuario

                    var usuarioDB = db.Usuarios.Where(x => x.Id == usuario_id).FirstOrDefault();

                    if (usuarioDB == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de usuario" };

                    usuarioDB.Nombre = nombre;
                    usuarioDB.DocumentoNumero = documento_numero;
                    usuarioDB.Correo = correo;
                    usuarioDB.Usuario1 = usuario;
                    usuarioDB.Usermodifiedid = usuario_modifica_id;
                    usuarioDB.Updatedat = DateTime.Now;

                    //cargo

                    var cargo = db.CargoUsuarios.Where(x => x.UsuarioId == usuario_id).FirstOrDefault();

                    cargo.CargoId = cargo_id;
                    cargo.Usermodifiedid = usuario_modifica_id;
                    cargo.Updatedat = DateTime.Now;

                    //agencia

                    db.AgenciaUsuarios
                       .Where(a => a.Isactive == true && a.UsuarioId == usuario_id)
                       .ExecuteUpdate(setters => setters
                           .SetProperty(p => p.Isactive, false)
                           .SetProperty(p => p.Usermodifiedid, usuario_modifica_id)
                           .SetProperty(p => p.Updatedat, DateTime.Now)
                       );

                    foreach (var agencia_id in agencia_ids)
                    {
                        var agenciaUsuario = db.AgenciaUsuarios.Where(x => x.AgenciaId == agencia_id && x.UsuarioId == usuario_id).FirstOrDefault();

                        if (agenciaUsuario != null)
                        {
                            agenciaUsuario.Updatedat = DateTime.Now;
                            agenciaUsuario.Usermodifiedid = usuario_modifica_id;
                            agenciaUsuario.Isactive = true;
                        }
                        else
                        {
                            var agenciaADD = new AgenciaUsuario();

                            agenciaADD.AgenciaId = agencia_id;
                            agenciaADD.UsuarioId = usuario_id;
                            agenciaADD.Usercreatedid = usuario_modifica_id;

                            db.AgenciaUsuarios.Add(agenciaADD);
                        }
                    }

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = usuarioDB.Id, Message = "Se actualizó correctamente el usuario." };
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }

        public async Task<TransaccionEntity> cambiarContrasenia(string contrasenia_anterior, string password, int usuario_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var usuario = await db.Usuarios.Where(x => x.Id == usuario_id).FirstOrDefaultAsync();

                    if (usuario == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de usuario" };

                    if (objHelperCommon.checkPassword(usuario.Contrasenia ?? "", contrasenia_anterior))
                    {
                        
                        usuario.Contrasenia = password;
                        usuario.Updatedat = DateTime.Now;
                        usuario.Usermodifiedid = usuario_id;

                        db.SaveChanges();

                        dbTransactionScope.Complete();

                        return new TransaccionEntity { Code = true, ID = usuario.Id, Message = "Se actualizó correctamente la contraseña de usuario." };
                    }
                    else
                    {
                        return new TransaccionEntity { Code = false, ID = 0, Message = "la contraseña no corresponde al usuario." };
                    }
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }

        public async Task<TransaccionEntity> eliminarUsuario(int usuario_id, int usuario_modifica_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var usuario = db.Usuarios.Where(x => x.Id == usuario_id).FirstOrDefault();

                    if (usuario == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de usuario" };

                    usuario.Isactive = false;
                    usuario.Usermodifiedid = usuario_modifica_id;
                    usuario.Updatedat = DateTime.Now;

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = usuario.Id, Message = "Se eliminó correctamente el usuario." };
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }

        public async Task<TransaccionEntity> registrarAccesos(int sistema_id, int perfil_id, int usuario_id, string fecha_inicio_perfil,
                                                                  string fecha_fin_perfil, int usuario_modifica_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {

                    //sistema

                    var sistemaAsignado = db.SistemaUsuarios.FirstOrDefault(x => x.SistemaId == sistema_id &&
                                                                                 x.UsuarioId == usuario_id);

                    if (sistemaAsignado == null)
                    {
                        var sistema = new SistemaUsuario
                        {
                            SistemaId = sistema_id,
                            FechaFin = null,
                            FechaInicio = DateOnly.FromDateTime(DateTime.Now),
                            UsuarioId = usuario_id,
                            Usercreatedid = usuario_modifica_id
                        };

                        db.SistemaUsuarios.Add(sistema);
                    }
                    else
                    {
                        sistemaAsignado.Usermodifiedid = usuario_modifica_id;
                        sistemaAsignado.Updatedat = DateTime.Now;
                        sistemaAsignado.FechaFin = null;
                        sistemaAsignado.FechaInicio = DateOnly.FromDateTime(DateTime.Now);
                        sistemaAsignado.Isactive = true;
                    }

                    //perfil

                    var perfilAsignado = db.PerfilUsuarios.FirstOrDefault(x => x.SistemaId == sistema_id &&
                                                                               x.UsuarioId == usuario_id && x.PerfilId == perfil_id);

                    if (perfilAsignado == null)
                    {

                        var perfiles = new PerfilUsuario
                        {
                            SistemaId = sistema_id,
                            PerfilId = perfil_id,
                            FechaFin = string.IsNullOrEmpty(fecha_fin_perfil) ? null : DateOnly.ParseExact(fecha_fin_perfil, "yyyy-MM-dd"),
                            FechaInicio = string.IsNullOrEmpty(fecha_inicio_perfil) ? DateOnly.FromDateTime(DateTime.Now) : DateOnly.ParseExact(fecha_inicio_perfil, "yyyy-MM-dd"),
                            UsuarioId = usuario_id,
                            Usercreatedid = usuario_modifica_id
                        };

                        db.PerfilUsuarios.Add(perfiles);

                    }
                    else
                    {
                        perfilAsignado.Usermodifiedid = usuario_modifica_id;
                        perfilAsignado.PerfilId = perfil_id;
                        perfilAsignado.Updatedat = DateTime.Now;
                        perfilAsignado.FechaInicio = string.IsNullOrEmpty(fecha_inicio_perfil) ? DateOnly.FromDateTime(DateTime.Now) : DateOnly.ParseExact(fecha_inicio_perfil, "yyyy-MM-dd");
                        perfilAsignado.FechaFin = string.IsNullOrEmpty(fecha_fin_perfil) ? null : DateOnly.ParseExact(fecha_fin_perfil, "yyyy-MM-dd");
                        perfilAsignado.Isactive = true;
                    }
                    

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = 0, Message = "Se registró correctamente los privilegios." };
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }

        public async Task<TransaccionEntity> registrarUsuario(int cargo_id, string nombre, string documento_numero, string correo, string usuario, List<int> agencia_ids, string password, int usuario_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var usuarioRegistrado = db.Usuarios.Where(x => x.Correo == correo).FirstOrDefault();

                    if (usuarioRegistrado != null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de usuario" };

                    var usuarioDB = new Usuario();

                    usuarioDB.Nombre = nombre;
                    usuarioDB.DocumentoNumero = documento_numero;
                    usuarioDB.DocumentoTipoId = (int)documentoTipoEnum.DNI;
                    usuarioDB.Correo = correo;
                    usuarioDB.Usuario1 = usuario;
                    usuarioDB.Nombre = nombre;
                    usuarioDB.Contrasenia = password;
                    usuarioDB.Usercreatedid = usuario_id;

                    db.Usuarios.Add(usuarioDB);

                    db.SaveChanges();

                    //cargo

                    var cargo = new CargoUsuario();

                    cargo.CargoId = cargo_id;
                    cargo.UsuarioId = usuarioDB.Id;
                    cargo.Usercreatedid = usuario_id;

                    db.CargoUsuarios.Add(cargo);

                    //agencias

                    var agenciasByUsuario = agencia_ids.Select(id => new AgenciaUsuario
                    {
                        UsuarioId = usuarioDB.Id,
                        AgenciaId = id,
                        Usercreatedid = usuario_id

                    }).ToList();

                    db.AgenciaUsuarios.AddRange(agenciasByUsuario);

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = usuarioDB.Id, Message = "Se registró correctamente el usuario." };
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }

        public async Task<TransaccionEntity> resetearContrasenia(int usuario_modifica_id, int usuario_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var usuario = db.Usuarios.Where(x => x.Id == usuario_id).FirstOrDefault();

                    if (usuario == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de usuario" };

                    usuario.Contrasenia = objHelperCommon.hashPassword(usuario.DocumentoNumero);
                    usuario.Usermodifiedid = usuario_modifica_id;
                    usuario.Updatedat = DateTime.Now;

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = usuario.Id, Message = "Se registró correctamente el usuario." };
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }

        public async Task<TransaccionEntity> suspenderUsuario(int usuario_modifica_id, int usuario_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var usuario = db.Usuarios.Where(x => x.Id == usuario_id).FirstOrDefault();

                    if (usuario == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de usuario" };

                    usuario.UsuarioEstadoId = (int)usuarioEstadoEnum.suspendido;
                    usuario.Usermodifiedid = usuario_modifica_id;
                    usuario.Updatedat = DateTime.Now;

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = usuario.Id, Message = "La suspensión del usuario se realizó con éxito." };
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }

        public async Task<TransaccionEntity> activarUsuario(int usuario_idModifica, int usuario_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var usuario = db.Usuarios.Where(x => x.Id == usuario_id).FirstOrDefault();

                    if (usuario == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de usuario" };

                    usuario.UsuarioEstadoId = (int)usuarioEstadoEnum.vigente;
                    usuario.Usermodifiedid = usuario_idModifica;
                    usuario.Updatedat = DateTime.Now;

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = usuario.Id, Message = "El usuario ha sido activado satisfactoriamente." };
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }

        public async Task<TransaccionEntity> eliminarAccesos(int sistema_id, int perfil_id, int usuario_id, int usuario_idModifica)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var usuario = db.Usuarios.Where(x => x.Id == usuario_id).FirstOrDefault();

                    if (usuario == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de usuario" };

                    if(perfil_id == 0)
                    {
                        var sistemaUsuario = db.SistemaUsuarios.Where(x => x.UsuarioId == usuario_id && x.SistemaId == sistema_id).FirstOrDefault();

                        if (sistemaUsuario == null) return new TransaccionEntity { Code = false, ID = 0, Message = "el privilegio a eliminar no existe" };

                        sistemaUsuario.Isactive = false;
                        sistemaUsuario.Usermodifiedid = usuario_idModifica;
                        sistemaUsuario.Updatedat = DateTime.Now;

                    }
                    else
                    {
                        var perfilUsuario = db.PerfilUsuarios.Where(x => x.UsuarioId == usuario_id && x.PerfilId == perfil_id && x.SistemaId == sistema_id).FirstOrDefault();

                        if (perfilUsuario == null) return new TransaccionEntity { Code = false, ID = 0, Message = "el privilegio a eliminar no existe" };

                        perfilUsuario.Isactive = false;
                        perfilUsuario.Usermodifiedid = usuario_idModifica;
                        perfilUsuario.Updatedat = DateTime.Now;
                    }

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = usuario.Id, Message = "Los privilegios fuerón eliminados." };
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "";

                    var ruta = Regex.Match(ex.StackTrace ?? "", @"([^\\/]+\.cs):line \d+");

                    var archivo = ruta.Success ? ruta.Value : "Sin ubicación";

                    throw new Exception($"A transaction error: {ex.Message} | {inner}, in {archivo}");
                }
            }
        }
    }
}
