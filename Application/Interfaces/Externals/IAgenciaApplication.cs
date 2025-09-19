using Application.Adapters.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Externals
{
    public interface IAgenciaApplication
    {
        public Task<DataResponse> listarAgenciasUsuario(int usuario_id, int sistema_codigo);

        public Task<DataResponse> listarAgencia();
    }
}
