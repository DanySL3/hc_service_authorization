using Domain.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Getting
{
    public interface IMenuGettingInfrastructure
    {
        public Task<List<DatosMenusEntity>> obtenerMenuUsuario(int perfil_id, int sistema_codigo);

        public Task<List<DatosMenusExternoEntity>> obtenerMenuExternos();

        public Task<List<DatosMenusExternoEntity>> obtenerMenuSistema(int perfil_id, int sistema_id);
    }
}
