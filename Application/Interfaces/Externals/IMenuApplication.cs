using Application.Adapters.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Externals
{
    public interface IMenuApplication
    {
        public Task<DataResponse> obtenerMenuUsuario(int perfil_id, int sistema_id, int sistema_codigo);
        public Task<DataResponse> obtenerMenuExternos();
        public Task<DataResponse> obtenerMenuSistema(int perfil_id, int sistema_id);
    }
}
