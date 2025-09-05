
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsuarioInfrastructure
    {
        public Task<TransaccionEntity> registrarPrivilegios(int sistema_id, int perfil_id, int usuario_id, string fecha_inicio_perfil, 
                                                            string fecha_fin_perfil, int agencia_id, int usuario_modifica_id);
        public Task<TransaccionEntity> actualizarUsuario(int cargo_id, string nombres, string documento_numero, 
                                                         string correo, string usuario, int usuario_id, int usuario_modifica_id);
        public Task<TransaccionEntity> registrarUsuario(int cargo_id, string nombres, string documento_numero, 
                                                        string correo, string usuario, string password, int usuario_id);

        public Task<TransaccionEntity> cambiarContrasenia(string contrasenia_anterior, string password, int usuario_id);

        public Task<TransaccionEntity> eliminarUsuario(int usuario_id, int usuario_modifica_id);

        public Task<TransaccionEntity> resetearContrasenia(int usuario_modifica_id, int usuario_id);

        public Task<TransaccionEntity> suspenderUsuario(int usuario_modifica_id, int usuario_id);

        public Task<TransaccionEntity> activarUsuario(int usuario_modifica_id, int usuario_id);

        public Task<TransaccionEntity> eliminarPrivilegios(int sistema_id, int perfil_id, int usuario_id, int usuario_modifica_id);
    }
}
