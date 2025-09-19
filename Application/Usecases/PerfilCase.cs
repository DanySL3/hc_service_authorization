using Application.Adapters.Internals;
using Application.Adapters.Request;
using Application.Helpers;
using Application.Interfaces.Externals;
using Application.Interfaces.Internals;
using Domain.Entities.Menu;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Getting;
using Domain.Interfaces.Method;


namespace Application.Usecases
{
    public class PerfilCase : IPerfilApplication
    {
        private readonly IResponseHelper objResponseHelper;
        private readonly IPerfilGettingInfrastructure perfilGettingInfrastructure;
        private readonly IPerfilInfrastructure perfilInfrastructure;
        private readonly IValidateFieldsHelper objValidateFieldsHelper;

        public PerfilCase(IPerfilGettingInfrastructure _perfilGettingInfrastructure, IPerfilInfrastructure _perfilInfrastructure)
        {
            perfilGettingInfrastructure = _perfilGettingInfrastructure;
            perfilInfrastructure = _perfilInfrastructure;

            objResponseHelper = new ResponseHelper();
            objValidateFieldsHelper = new ValidateFieldsHelper();
        }


        public async Task<DataResponse> listarPerfiles()
        {

            //ejecución de petición

            var datos = await perfilGettingInfrastructure.listarPerfiles();

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> obtenerPerfilUsuario(int usuario_id, int sistema_codigo)
        {
            //ejecución de petición

            var datos = await perfilGettingInfrastructure.obtenerPerfilUsuario(usuario_id, sistema_codigo);

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> actualizarPerfil(ActualizarPerfilAdapter objModel, int usuario_id)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();
            var objVerificador = new ActualizarPerfilValidator();
            bool lValido = objValidateFieldsHelper.dataValidate(objModel, lstErrores, objVerificador);

            if (!lValido)
                return objResponseHelper.errorList(lstErrores);


            //ejecución de petición

            var datos = await perfilInfrastructure.actualizarPerfil(objModel.perfil_id, objModel.perfil, objModel.descripcion, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> eliminarPerfil(int perfil_id, int usuario_id)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();

            if (perfil_id <= 0)
                lstErrores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de perfil"), Field = "perfil_id" });

            if (lstErrores.Any())
                return objResponseHelper.errorList(lstErrores);


            //ejecución de petición

            var datos = await perfilInfrastructure.eliminarPerfil(perfil_id, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> registrarPerfil(RegitrarPerfilAdapter objModel, int usuario_id)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();
            var objVerificador = new RegitrarPerfilValidator();
            bool lValido = objValidateFieldsHelper.dataValidate(objModel, lstErrores, objVerificador);

            if (!lValido)
                return objResponseHelper.errorList(lstErrores);


            //ejecución de petición

            var datos = await perfilInfrastructure.registrarPerfil(objModel.perfil, objModel.descripcion, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> registrarPrivilegios(RegistrarPrivilegiosPerfilAdapter objModel, int usuario_id)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();
            var objVerificador = new RegistrarPrivilegiosPerfilValidator();
            bool lValido = objValidateFieldsHelper.dataValidate(objModel, lstErrores, objVerificador);

            if (!lValido)
                return objResponseHelper.errorList(lstErrores);

            //datos

            var lstMenus = new List<RegistrarMenuEntity>();

            if (objModel.lstMenu != null)
            {
                lstMenus = objModel.lstMenu.Select(x => new RegistrarMenuEntity
                {
                    menu_id = x.menu_id

                }).ToList();
            }

            //ejecución de petición

            var datos = await perfilInfrastructure.registrarPrivilegios(lstMenus, objModel.perfil_id, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> obtenerPerfilNoAsginado(int sistema_id, int usuario_id)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();

            if (sistema_id <= 0)
                lstErrores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de perfil"), Field = "perfil_id" });

            if (usuario_id <= 0)
                lstErrores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de usuario"), Field = "usuario_id" });

            if (lstErrores.Any())
                return objResponseHelper.errorList(lstErrores);


            //ejecución de petición

            var datos = await perfilGettingInfrastructure.obtenerPerfilNoAsginado(sistema_id, usuario_id);

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }
    }
}
