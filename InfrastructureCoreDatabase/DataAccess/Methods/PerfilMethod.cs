using Domain.Entities;
using Domain.Entities.Menu;
using Domain.Interfaces.Method;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Transactions;

namespace InfrastructureCoreDatabase.DataAccess.Methods
{
    public class PerfilMethod : IPerfilInfrastructure
    {
        private readonly EntityFrameworkContext db;

        public PerfilMethod(EntityFrameworkContext _db)
        {
            db = _db;
        }

        public async Task<TransaccionEntity> actualizarPerfil(int perfil_id, string perfil, string descripcion, int usuario_id, int? sistemaId)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var perfilDB = db.Perfils.Where(x => x.Id == perfil_id).FirstOrDefault();

                    if (perfilDB == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de perfil" };

                    perfilDB.Perfil1 = perfil;
                    perfilDB.SistemaId = sistemaId;
                    perfilDB.Descripcion = descripcion;
                    perfilDB.Usermodifiedid = usuario_id;
                    perfilDB.Updatedat = DateTime.Now;

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = perfilDB.Id, Message = "Se actualizó correctamente el perfil." };
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

        public async Task<TransaccionEntity> eliminarPerfil(int perfil_id, int usuario_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var perfil = db.Perfils.Where(x => x.Id == perfil_id).FirstOrDefault();

                    if (perfil == null) return new TransaccionEntity { Code = false, ID = 0, Message = "no hay datos con el identificador de perfil" };

                    perfil.Isactive = false;
                    perfil.Usermodifiedid = usuario_id;
                    perfil.Updatedat = DateTime.Now;

                    db.PerfilUsuarios
                        .Where(a => a.Isactive == true && a.PerfilId == perfil_id)
                        .ExecuteUpdate(setters => setters
                            .SetProperty(p => p.Isactive, false)
                            .SetProperty(p => p.Usermodifiedid, usuario_id)
                            .SetProperty(p => p.Updatedat, DateTime.Now)
                        );

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = perfil.Id, Message = "Se eliminó correctamente el perfil." };
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

        public async Task<TransaccionEntity> registrarPerfil(string perfil, string descripcion, int usuario_id, int? sistemaId)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var perfilDB = new Perfil();

                    perfilDB.Perfil1 = perfil;
                    perfilDB.SistemaId = sistemaId;
                    perfilDB.Descripcion = descripcion;
                    perfilDB.Usercreatedid = usuario_id;
                    perfilDB.Codigo = db.Perfils.Max(x => x.Codigo) + 1;

                    db.Perfils.Add(perfilDB);

                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = perfilDB.Id, Message = "Se registró correctamente el perfil." };
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

        public async Task<TransaccionEntity> registrarPrivilegios(List<RegistrarMenuEntity> lstMenus, int perfil_id, int usuario_id)
        {
            using (var dbTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    if (lstMenus.Count > 0)
                    {

                        var perfilesDB = db.MenuPerfils
                            .Where(p => p.PerfilId == perfil_id)
                            .ToList();

                        foreach (var perfilDB in perfilesDB)
                        {
                            var perfilNuevo = lstMenus.FirstOrDefault(i => i.menu_id == perfilDB.MenuId);

                            if (perfilNuevo == null)
                            {
                                perfilDB.Isactive = false;
                            }
                            else
                            {
                                perfilDB.Isactive = true;
                                lstMenus.Remove(perfilNuevo);
                            }

                            perfilDB.Usermodifiedid = usuario_id;
                            perfilDB.Updatedat = DateTime.Now;
                        }

                        var nuevosMenus = lstMenus
                            .Select(x => new MenuPerfil
                            {
                                MenuId = x.menu_id,
                                PerfilId = perfil_id,
                                Usercreatedid = usuario_id,
                            })
                            .ToList();
                        
                        db.MenuPerfils.AddRange(nuevosMenus);
                    }
                    else
                    {

                        db.MenuPerfils
                            .Where(p => p.Id == perfil_id && p.Isactive == true)
                            .ExecuteUpdate(setters => setters
                                .SetProperty(p => p.Isactive, false)
                                .SetProperty(p => p.Usermodifiedid, usuario_id)
                                .SetProperty(p => p.Updatedat, DateTime.Now)
                            );

                    }


                    db.SaveChanges();

                    dbTransactionScope.Complete();

                    return new TransaccionEntity { Code = true, ID = 0, Message = "Se registró correctamente el perfil." };
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
