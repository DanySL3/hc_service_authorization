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
    public class SistemaCase : ISistemaApplication
    {
        private readonly IResponseHelper objResponseHelper;
        private readonly ISistemaGettingInfrastructure objSistemaGettingInfrastructure;

        public SistemaCase(ISistemaGettingInfrastructure _objSistemaGettingInfrastructure)
        {
            objSistemaGettingInfrastructure = _objSistemaGettingInfrastructure;
            objResponseHelper = new ResponseHelper();
        }

        public async Task<DataResponse> listarSistemas()
        {
            //ejecución de petición

            var datos = await objSistemaGettingInfrastructure.listarSistemas();

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> listarSistemasUsuario(int usuario_id)
        {

            //ejecución de petición

            var datos = await objSistemaGettingInfrastructure.listarSistemasUsuario(usuario_id);

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> obtenerIdentiticador(int sistema_codigo)
        {
            //ejecución de petición

            var datos = await objSistemaGettingInfrastructure.obtenerIdentiticador(sistema_codigo);

            if (datos == null)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }
    }
}
