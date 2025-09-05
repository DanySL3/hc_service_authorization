using Application.Adapters.Internals;
using Application.Adapters.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Externals
{
    public interface IPerfilApplication
    {
        public Task<DataResponse> actualizarPerfil(ActualizarPerfilAdapter objModel, int usuario_id);

        public Task<DataResponse> eliminarPerfil(int perfil_id, int usuario_id);

        public Task<DataResponse> listarPerfiles();

        public Task<DataResponse> ObtenerPerfil(int usuario_id, int sistema_id, int sistema_codigo);

        public Task<DataResponse> ObtenerPerfilNoAsginado(int sistema_id, int usuario_id);

        public Task<DataResponse> registrarPerfil(RegitrarPerfilAdapter objModel, int usuario_id);

        public Task<DataResponse> registrarPrivilegios(RegistrarPrivilegiosPerfilAdapter objModel, int usuario_id);
    }
}
