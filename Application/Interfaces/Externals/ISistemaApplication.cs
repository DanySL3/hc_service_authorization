using Application.Adapters.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Externals
{
    public interface ISistemaApplication
    {
        public Task<DataResponse> listarSistemas();

        public Task<DataResponse> listarSistemasUsuario(int usuario_id);

        public Task<DataResponse> obtenerIdentiticador(int sistema_codigo);
    }
}
