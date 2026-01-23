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
    [Authorize]


    public class PerfilController : ControllerBase
    {
        private readonly IPerfilApplication objPerfilApplication;
        private readonly ILogger<PerfilController> logger;
        private readonly IResponseHelper objResponseApp;
        private readonly IHttpContextAccessor httpContextAccessor;
        private bool lDevelopment;

        public PerfilController(IPerfilApplication _objPerfilApplication, ILogger<PerfilController> _logger)
        {
            objPerfilApplication = _objPerfilApplication;
            logger = _logger;

            objResponseApp = new ResponseHelper();
            httpContextAccessor = new HttpContextAccessor();
            lDevelopment = Convert.ToBoolean(httpContextAccessor.HttpContext?.Items["lDevelopment"]);
        }


        [HttpGet]
        [Route("listar-perfiles-usuario")]

        public async Task<IActionResult> ObtenerPerfilUsuario([FromQuery] int idSistema)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuarioId"));

                var dataResponse = await objPerfilApplication.obtenerPerfilUsuario(usuario_id, idSistema);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }



        [HttpGet]
        [Route("listar-perfiles-faltantes")]

        public async Task<IActionResult> obtenerPerfilesFaltantes([FromQuery] int sistema_id, [FromQuery] int usuario_id)
        {
            try
            {
                var dataResponse = await objPerfilApplication.obtenerPerfilesFaltantes(sistema_id, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }


        [HttpPost]
        [Route("registrar-perfil-menus")]

        public async Task<IActionResult> registrarPrivilegios([FromBody] RegistrarPrivilegiosPerfilAdapter objModel)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuarioId"));

                var dataResponse = await objPerfilApplication.registrarPrivilegios(objModel, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }


        [HttpGet]
        [Route("listar-perfiles")]

        public async Task<IActionResult> listarPerfiles([FromQuery] int sistema_id)
        {
            try
            {
                var dataResponse = await objPerfilApplication.listarPerfiles(sistema_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");


                if (lDevelopment)
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }


        [HttpPost]
        [Route("registrar-perfil")]

        public async Task<IActionResult> registrarPerfil([FromBody] RegitrarPerfilAdapter objModel)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuarioId"));

                var dataResponse = await objPerfilApplication.registrarPerfil(objModel, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpPut]
        [Route("actualizar-perfil")]

        public async Task<IActionResult> actualizarPerfil([FromBody] ActualizarPerfilAdapter objModel)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuarioId"));

                var dataResponse = await objPerfilApplication.actualizarPerfil(objModel, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

        [HttpDelete]
        [Route("eliminar-perfil")]

        public async Task<IActionResult> eliminarPerfil([FromQuery] int perfil_id)
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuarioId"));

                var dataResponse = await objPerfilApplication.eliminarPerfil(perfil_id, usuario_id);

                return StatusCode(200, dataResponse);
            }
            catch (Exception ex)
            {
                logger.LogError($"{HttpContext.Request.Path}: [error] {ex.Message}");

                if (lDevelopment)
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(2000, ex.Message)));
                }
                else
                {
                    return StatusCode(500, objResponseApp.errorSimpleServidor("2000", MessageException.GetErrorByCode(500)));
                }
            }
        }

    }
}
