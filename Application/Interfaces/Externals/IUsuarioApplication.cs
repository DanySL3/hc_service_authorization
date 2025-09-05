using Application.Adapters.Internals;
using Application.Adapters.Request;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Externals
{
    public interface IUsuarioApplication
    {
        public Task<DataResponse> listarUsuarios(int sistema_id);

        public Task<DataResponse> registrarPrivilegios(RegistrarPrivilegiosAdapter objModel, int usuario_id);

        public Task<DataResponse> cambiarContrasenia(ActualizarContraseniaAdapter objModel, int usuario_id);

        public Task<DataResponse> eliminarUsuario(int usuario_id, int usuario_modifica_id);

        public Task<DataResponse> resetearContrasenia(int usuario_modifica_id, int usuario_id);

        public Task<DataResponse> listarPrivilegios(int sistema_id, int usuario_id, string documento_numero);

        public Task<DataResponse> registrarUsuario(RegistrarUsuarioAdapter objModel, int usuario_id);

        public Task<DataResponse> actualizarUsuario(ActualizarUsuarioAdapter objModel, int usuario_id);

        public Task<DataResponse> listarCargos();

        public Task<DataResponse> buscarUsuario(int usuario_id, string documento_numero);

        public Task<DataResponse> suspenderUsuario(int usuario_modifica_id, int usuario_id);

        public Task<DataResponse> eliminarPrivilegios(int sistema_id, int idPerfil, int usuario_id, int usuario_modifica_id);

        public Task<DataResponse> activarUsuario(int usuario_modifica_id, int usuario_id);
    }
}
