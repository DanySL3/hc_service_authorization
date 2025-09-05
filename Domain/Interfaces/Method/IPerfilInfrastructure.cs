using Domain.Entities;
using Domain.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Method
{
    public interface IPerfilInfrastructure
    {
        public Task<TransaccionEntity> actualizarPerfil(int perfil_id, string perfil, string descripcion, int usuario_id);

        public Task<TransaccionEntity> eliminarPerfil(int perfil_id, int usuario_id);

        public Task<TransaccionEntity> registrarPerfil(string perfil, string descripcion, int usuario_id);

        public Task<TransaccionEntity> registrarPrivilegios(List<RegistrarMenuEntity> lstMenus, int perfil_id, int usuario_id);
    }
}
