using Application.Adapters.Internals;
using Application.Helpers;
using Application.Interfaces.Externals;
using Application.Interfaces.Internals;
using Domain.Exceptions;
using Domain.Interfaces.Getting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Usecases
{
    public class AgenciaCase : IAgenciaApplication
    {
        private readonly IResponseHelper objResponseHelper;
        private readonly IAgenciaGettingInfrastructure objAgenciaGettingInfrastructure;

        public AgenciaCase(IAgenciaGettingInfrastructure _objAgenciaGettingInfrastructure)
        {
            objAgenciaGettingInfrastructure = _objAgenciaGettingInfrastructure;
            objResponseHelper = new ResponseHelper();
        }

        public async Task<DataResponse> listarAgenciasUsuario(int usuario_id, int sistema_codigo)
        {
            //ejecución de petición

            var datos = await objAgenciaGettingInfrastructure.listarAgenciasUsuario(usuario_id, sistema_codigo);

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> listarAgencia()
        {
            //ejecución de petición

            var datos = await objAgenciaGettingInfrastructure.listarAgencia();

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }
    }
}
