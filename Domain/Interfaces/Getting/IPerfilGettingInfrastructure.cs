using Domain.Entities.Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Getting
{
    public interface IPerfilGettingInfrastructure
    {

        public Task<List<DatosPerfilEntity>> listarPerfiles();

        public Task<List<DatosPerfilEntity>> ObtenerPerfil(int usuario_id, int sistema_id, int sistema_codigo);

        public Task<List<DatosPerfilEntity>> ObtenerPerfilNoAsginado(int sistema_id, int usuario_id);
    }
}
