using Application.Adapters.Request;
using Application.Helpers;
using Application.Interfaces.Externals;
using Application.Interfaces.Internals;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/authorization/")]
    [ApiController]
    //[Authorize]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplication usuarioApplication;
        private readonly ILogger<UsuarioController> logger;
        private readonly IResponseHelper objResponseHelper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private bool lDevelopment;

        public UsuarioController(IUsuarioApplication _usuarioApplication, ILogger<UsuarioController> _logger)
        {

            usuarioApplication = _usuarioApplication;
            logger = _logger;

            objResponseHelper = new ResponseHelper();
            httpContextAccessor = new HttpContextAccessor();
            lDevelopment = Convert.ToBoolean(httpContextAccessor.HttpContext?.Items["lDevelopment"]);
        }

        [HttpPost]
        [Route("registrar-usuario")]
        [Authorize]

        public async Task<IActionResult> registrarUsuario([FromBody] RegistrarUsuarioAdapter objModel)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.registrarUsuario(objModel, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpDelete]
        [Route("eliminar-usuario")]
        [Authorize]

        public async Task<IActionResult> eliminarUsuario([FromQuery] int usuario_id)
        {
            try
            {
                int usuario_modifica_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.eliminarUsuario(usuario_id, usuario_modifica_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpPut]
        [Route("actualizar-usuario")]
        [Authorize]

        public async Task<IActionResult> actualizarUsuario([FromBody] ActualizarUsuarioAdapter objModel)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.actualizarUsuario(objModel, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpGet]
        [Route("listar-usuarios")]
        [Authorize]

        public async Task<IActionResult> listarUsuarios([FromQuery] int sistema_id = 0)
        {
            try
            {
                var dataResponse = await usuarioApplication.listarUsuarios(sistema_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpGet]
        [Route("buscar-usuarios")]
        //[Authorize]

        public async Task<IActionResult> buscarUsuario([FromQuery] int usuario_id = 0, [FromQuery] string documento_numero = "")
        {
            try
            {
                var dataResponse = await usuarioApplication.buscarUsuario(usuario_id, documento_numero);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError("/api/identity/listar-privilegios: [error] " + ex.Message);

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpPut]
        [Route("resetear-contrasenia")]
        [Authorize]

        public async Task<IActionResult> resetearContrasenia([FromQuery] int usuario_id)
        {
            try
            {
                int usuario_modifica_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.resetearContrasenia(usuario_modifica_id, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpPut]
        [Route("actualizar-contrasenia")]
        [Authorize]

        public async Task<IActionResult> cambiarContrasenia([FromBody] ActualizarContraseniaAdapter objModel)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.cambiarContrasenia(objModel, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }


        [HttpPost]
        [Route("guardar-privilegios")]
        [Authorize]

        public async Task<IActionResult> registrarPrivilegios([FromBody] RegistrarPrivilegiosAdapter objModel)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.registrarPrivilegios(objModel, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpGet]
        [Route("listar-privilegios")]
        [Authorize]

        public async Task<IActionResult> listarPrivilegios([FromQuery] int sistema_id = 0,
                                                           [FromQuery] int usuario_id = 0,
                                                           [FromQuery] string documento_numero = "")
        {
            try
            {
                var dataResponse = await usuarioApplication.listarPrivilegios(sistema_id, usuario_id, documento_numero);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpDelete]
        [Route("eliminar-privilegios")]
        [Authorize]

        public async Task<IActionResult> actualizarPrivilegios([FromQuery] int sistema_id, [FromQuery] int perfil_id, [FromQuery] int usuario_id)
        {
            try
            {
                int usuario_modifica_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.eliminarPrivilegios(sistema_id, perfil_id, usuario_id, usuario_modifica_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpGet]
        [Route("listar-cargos")]
        //[Authorize]

        public async Task<IActionResult> listarCargos()
        {
            try
            {
                var dataResponse = await usuarioApplication.listarCargos();

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpPut]
        [Route("suspender-usuario")]
        [Authorize]

        public async Task<IActionResult> suspenderUsuario([FromQuery] int usuario_id)
        {
            try
            {
                int usuario_modifica_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.suspenderUsuario(usuario_modifica_id, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpPut]
        [Route("activar-usuario")]
        [Authorize]

        public async Task<IActionResult> activarUsuario([FromQuery] int usuario_id)
        {
            try
            {
                int usuario_modifica_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await usuarioApplication.activarUsuario(usuario_modifica_id, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseHelper.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }
    }
}
