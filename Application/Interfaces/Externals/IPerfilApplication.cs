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

        public Task<DataResponse> listarPerfiles(int sistema_id);

        public Task<DataResponse> obtenerPerfilUsuario(int usuario_id, int idSistema);

        public Task<DataResponse> obtenerPerfilesFaltantes(int sistema_id, int usuario_id);

        public Task<DataResponse> registrarPerfil(RegitrarPerfilAdapter objModel, int usuario_id);

        public Task<DataResponse> registrarPrivilegios(RegistrarPrivilegiosPerfilAdapter objModel, int usuario_id);
    }
}
