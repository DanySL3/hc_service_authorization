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
    public class MenuCase : IMenuApplication
    {
        private readonly IResponseHelper objResponseHelper;
        private readonly IMenuGettingInfrastructure menuGettingInfrastructure;

        public MenuCase(IMenuGettingInfrastructure _menuGettingInfrastructure)
        {
            menuGettingInfrastructure = _menuGettingInfrastructure;
            objResponseHelper = new ResponseHelper();
        }

        public async Task<DataResponse> obtenerMenuUsuario(int perfil_id, int sistema_id, int sistema_codigo)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();

            if (sistema_id < 0)
                lstErrores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de sistema"), Field = "sistema_id" });

            if (lstErrores.Any())
                return objResponseHelper.errorList(lstErrores);


            //ejecución de petición

            var datos = await menuGettingInfrastructure.obtenerMenuUsuario(perfil_id, sistema_id, sistema_codigo);

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> obtenerMenuExternos()
        {

            //ejecución de petición

            var datos = await menuGettingInfrastructure.obtenerMenuExternos();

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> obtenerMenuSistema(int perfil_id, int sistema_id)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();

            if (sistema_id <= 0)
                lstErrores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de sistema"), Field = "sistema_id" });

            if (perfil_id <= 0)
                lstErrores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de perfil"), Field = "perfil_id" });

            if (lstErrores.Any())
                return objResponseHelper.errorList(lstErrores);


            //ejecución de petición

            var datos = await menuGettingInfrastructure.obtenerMenuSistema(perfil_id, sistema_id);

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }
    }
}
