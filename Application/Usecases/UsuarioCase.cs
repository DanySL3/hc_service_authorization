using Application.Adapters.Internals;
using Application.Adapters.Request;
using Application.Helpers;
using Application.Interfaces.Externals;
using Application.Interfaces.Internals;
using Domain.Interfaces.Getting;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Usecases
{
    public class UsuarioCase : IUsuarioApplication
    {
        private readonly IResponseHelper objResponseHelper;
        private readonly IUsuarioGettingInfrastructure objUsuarioGettingInfrastructure;
        private readonly IUsuarioInfrastructure objUsuarioInfrastructure;
        private readonly IValidateFieldsHelper objValidateFieldsHelper;
        private readonly IHelperCommon objHelperCommon;

        public UsuarioCase(IUsuarioGettingInfrastructure _objUsuarioGettingInfrastructure, IUsuarioInfrastructure _objUsuarioInfrastructure, IHelperCommon _objHelperCommon)
        {
            objUsuarioGettingInfrastructure = _objUsuarioGettingInfrastructure;
            objUsuarioInfrastructure = _objUsuarioInfrastructure;
            objHelperCommon = _objHelperCommon;

            objResponseHelper = new ResponseHelper();
            objValidateFieldsHelper = new ValidateFieldsHelper();
        }

        public async Task<DataResponse> actualizarUsuario(ActualizarUsuarioAdapter objModel, int usuario_id)
        {
            //validación de campos

            var lstErrores = new List<FieldResponse>();
            var objVerificador = new ActualizarUsuarioValidator();
            bool lValido = objValidateFieldsHelper.dataValidate(objModel, lstErrores, objVerificador);

            if (!lValido)
                return objResponseHelper.errorList(lstErrores);


            //ejecución de petición

            var datos = await objUsuarioInfrastructure.actualizarUsuario(objModel.cargo_id, objModel.nombre, objModel.documento_numero, objModel.correo, objModel.usuario,
                                                                         objModel.usuario_id, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }


        public async Task<DataResponse> cambiarContrasenia(ActualizarContraseniaAdapter objModel, int usuario_id)
        {
            //validación de campos

            var errores = new List<FieldResponse>();
            var objVerificador = new ActualizarContraseniaValidator();
            bool lValido = objValidateFieldsHelper.dataValidate(objModel, errores, objVerificador);

            if (!lValido)
                return objResponseHelper.errorList(errores);


            var password = objHelperCommon.hashPassword(objModel.contrasenia_actual);

            //ejecución de petición

            var datos = await objUsuarioInfrastructure.cambiarContrasenia(objModel.contrasenia_anterior, password, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }


        public async Task<DataResponse> registrarPrivilegios(RegistrarPrivilegiosAdapter objModel, int usuario_id)
        {
            //validación de campos

            var errores = new List<FieldResponse>();
            var objVerificador = new RegistrarPrivilegiosValidator();
            bool lValido = objValidateFieldsHelper.dataValidate(objModel, errores, objVerificador);

            if (!lValido)
                return objResponseHelper.errorList(errores);

            //ejecución de petición

            var datos = await objUsuarioInfrastructure.registrarPrivilegios(objModel.sistema_id, objModel.perfil_id, objModel.usuario_id,
                                                                            objModel.fecha_inicio_perfil, objModel.fecha_fin_perfil,
                                                                            objModel.agencia_id, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }


        public async Task<DataResponse> registrarUsuario(RegistrarUsuarioAdapter objModel, int usuario_id)
        {
            string password = "";

            //validación de campos

            var errores = new List<FieldResponse>();
            var objVerificador = new RegistrarUsuarioValidator();
            bool lValido = objValidateFieldsHelper.dataValidate(objModel, errores, objVerificador);

            if (!lValido)
                return objResponseHelper.errorList(errores);

            //datos

            if (string.IsNullOrEmpty(objModel.contrasenia))
            {
                password = objHelperCommon.hashPassword(objModel.documento_numero);
            }
            else
            {
                password = objHelperCommon.hashPassword(objModel.contrasenia);
            }

            //ejecución de petición

            var datos = await objUsuarioInfrastructure.registrarUsuario(objModel.cargo_id, objModel.nombre, objModel.documento_numero, objModel.correo, objModel.usuario,
                                                                        password, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }



        public async Task<DataResponse> eliminarUsuario(int usuario_id, int usuario_modifica_id)
        {
            //validación de campos

            var errores = new List<FieldResponse>();

            if (usuario_id <= 0)
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de usuario"), Field = "usuario_id" });

            if (errores.Any())
                return objResponseHelper.errorList(errores);

            //ejecución de petición

            var datos = await objUsuarioInfrastructure.eliminarUsuario(usuario_id, usuario_modifica_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> resetearContrasenia(int usuario_modifica_id, int usuario_id)
        {
            //validación de campos

            var errores = new List<FieldResponse>();

            if (usuario_id <= 0)
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de usuario"), Field = "usuario_id" });

            if (errores.Any())
                return objResponseHelper.errorList(errores);

            //ejecución de petición

            var datos = await objUsuarioInfrastructure.resetearContrasenia(usuario_modifica_id, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }


        public async Task<DataResponse> listarPrivilegios(int sistema_id, int usuario_id, string documento_numero)
        {
            //validación de campos

            var errores = new List<FieldResponse>();

            if (usuario_id <= 0 && sistema_id <= 0 && string.IsNullOrEmpty(documento_numero))
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "campos de búsqueda"), Field = "datos de entrada" });

            if (errores.Any())
                return objResponseHelper.errorList(errores);

            //ejecución de petición

            var datos = await objUsuarioGettingInfrastructure.listarPrivilegios(sistema_id, usuario_id, documento_numero);

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> listarUsuarios(int sistema_id)
        {
            //ejecución de petición

            var datos = await objUsuarioGettingInfrastructure.listarUsuarios(sistema_id);

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> listarCargos()
        {
            //ejecución de petición

            var datos = await objUsuarioGettingInfrastructure.listarCargos();

            if (datos.Count == 0)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> buscarUsuario(int usuario_id, string documento_numero)
        {
            //validación de campos

            var errores = new List<FieldResponse>();

            if (usuario_id <= 0 && string.IsNullOrEmpty(documento_numero))
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "campos de búsqueda"), Field = "datos de entrada" });

            if (errores.Any())
                return objResponseHelper.errorList(errores);

            //ejecución de petición

            var datos = await objUsuarioGettingInfrastructure.buscarUsuario(usuario_id, documento_numero);

            if (datos == null)
                return objResponseHelper.emptyResponse();

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> suspenderUsuario(int usuario_modifica_id, int usuario_id)
        {
            //validación de campos

            var errores = new List<FieldResponse>();

            if (usuario_id <= 0)
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de usuario"), Field = "usuario_id" });

            if (errores.Any())
                return objResponseHelper.errorList(errores);


            //ejecución de petición

            var datos = await objUsuarioInfrastructure.suspenderUsuario(usuario_modifica_id, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> eliminarPrivilegios(int sistema_id, int perfil_id, int usuario_id, int usuario_modifica_id)
        {
            //validación de campos

            var errores = new List<FieldResponse>();

            if (usuario_id <= 0)
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de usuario"), Field = "usuario_id" });

            if (perfil_id <= 0)
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(10010, "id de perfil"), Field = "perfil_id" });

            if (sistema_id <= 0)
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de sistema"), Field = "sistema_id" });

            if (errores.Any())
                return objResponseHelper.errorList(errores);

            //ejecución de petición

            var datos = await objUsuarioInfrastructure.eliminarPrivilegios(sistema_id, perfil_id, usuario_id, usuario_modifica_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }

        public async Task<DataResponse> activarUsuario(int usuario_modifica_id, int usuario_id)
        {
            //validación de campos

            var errores = new List<FieldResponse>();

            if (usuario_id <= 0)
                errores.Add(new FieldResponse() { Code = "1010", Message = MessageException.GetErrorByCode(1010, "id de usuario"), Field = "usuario_id" });

            if (errores.Any())
                return objResponseHelper.errorList(errores);


            //ejecución de petición

            var datos = await objUsuarioInfrastructure.activarUsuario(usuario_modifica_id, usuario_id);

            if (datos.Code == false)
                return objResponseHelper.errorSimpleClient("2001", MessageException.GetErrorByCode(2001, datos.Message));

            return objResponseHelper.successResponse(datos);
        }
    }
}
