using Domain.Entities.Menu;
using Domain.Entities.Perfil;
using Domain.Enums;
using Domain.Interfaces.Getting;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCoreDatabase.DataAccess.Gettings
{
    public class MenuGetting : IMenuGettingInfrastructure
    {
        private readonly EntityFrameworkContext db;

        public MenuGetting(EntityFrameworkContext _db)
        {
            db = _db;
        }

        public async Task<List<DatosMenusEntity>> obtenerMenuUsuario(int perfil_id, int sistema_codigo)
        {
            var menus = new List<DatosMenusEntity>();

            //sistema

            var sistema = await db.Sistemas.
                Where(x => x.Codigo == sistema_codigo).
                Select(x => new DatosSistemaEntity
                {
                    sistema_id = x.Id

                }).FirstOrDefaultAsync();

            //menus

            menus = await db.Menus
                .Where(m => m.Isactive == true && m.SistemaId == sistema!.sistema_id)
                .Join(db.MenuPerfils,
                    m => m.Id,
                    p => p.MenuId,
                    (m, p) => new { m, p }
                )
                .Where(mp => mp.p.PerfilId == perfil_id && mp.p.Isactive == true)
                .Select(mp => new DatosMenusEntity
                {
                    id = mp.m.Id,
                    menu_padre_id = mp.m.MenuPadreId ?? 0,
                    menu_tipo_id = mp.m.MenuTipoId,
                    menu = mp.m.Menu1 ?? "",
                    url = mp.m.Url ?? "",
                    icono = mp.m.Icon ?? "",
                    orden = mp.m.Orden ?? 0
                }).ToListAsync();
            

            return menus;
        }

        public async Task<List<DatosMenusExternoEntity>> ObtenerMenuPerfil(int perfil_id, int sistema_id)
        {
            var menus = new List<DatosMenusExternoEntity>();

            menus = await db.Menus
                    .Where(a => a.SistemaId == sistema_id)
                    .GroupJoin(
                        db.MenuPerfils.Where(b => b.PerfilId == perfil_id && b.Isactive == true),
                        a => a.Id,
                        b => b.MenuId,
                        (a, b) => new { a, b }
                    )
                    .SelectMany(
                        ab => ab.b.DefaultIfEmpty(),
                        (ab, b) => new DatosMenusExternoEntity
                        {
                            id = ab.a.Id,
                            menu_padre_id = ab.a.MenuPadreId ?? 0,
                            menu = ab.a.Menu1,
                            descripcion = "",
                            url = "",
                            lstMenuHijos = new List<DatosMenusExternoEntity>(),
                            esAsignado = b != null ? true : false
                        }
                    ).ToListAsync();


            return menus;
        }

        public async Task<List<DatosMenusExternoEntity>> obtenerMenuExternos()
        {
            var menus = new List<DatosMenusExternoEntity>();

            menus = await ObtenerMenusHijosExterno(0);

            return menus;
        }


        private async Task<List<DatosMenusExternoEntity>> ObtenerMenusHijosExterno(int idMenuPadre)
        {
            var menus = await db.MenuExternos
                            .Where(g => g.MenuPadreId == idMenuPadre && g.Isactive == true)
                            .OrderBy(g => g.Orden)
                            .ToListAsync();

            var menuHijos = menus.Select(menu => new DatosMenusExternoEntity
            {
                id = menu.Id,
                menu_padre_id = menu.MenuPadreId ?? 0,
                menu = menu.Menu,
                descripcion = menu.Descripcion ?? "",
                url = menu.Url,
                lstMenuHijos = ObtenerMenusHijosExterno(menu.Id).Result

            }).ToList();

            return menuHijos;
        }

    }
}
