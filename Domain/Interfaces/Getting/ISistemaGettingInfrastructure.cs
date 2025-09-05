using Domain.Entities.Perfil;
using Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Getting
{
    public interface ISistemaGettingInfrastructure
    {
        public Task<List<DatosAplicacionEntity>> listarSistemas();

        public Task<List<DatosAplicacionEntity>> listarSistemasUsuario(int usuario_id);

        public Task<DatosSistemaEntity> obtenerIdentiticador(int sistema_codigo);
    }
}
