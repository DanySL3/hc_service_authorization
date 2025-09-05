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

        public async Task<DataResponse> obtenerAgencia(int usuario_id, int sistema_id)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();

            if (sistema_id < 0)
                lstErrores.Add(new FieldResponse() { Code = "10010", Message = MessageException.GetErrorByCode(10010, "id de sistema"), Field = "sistema_id" });

            if (lstErrores.Any())
                return objResponseHelper.errorList(lstErrores);


            //ejecución de petición

            var datos = await objAgenciaGettingInfrastructure.obtenerAgencia(usuario_id, sistema_id);

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
