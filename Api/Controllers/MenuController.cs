using Application.Helpers;
using Application.Interfaces.Externals;
using Application.Interfaces.Internals;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/authorization/")]
    [ApiController]

    public class MenuController : ControllerBase
    {
        private readonly IMenuApplication objMenuApplication;
        private readonly ILogger<MenuController> logger;
        private readonly IResponseHelper objResponseApp;
        private readonly IHttpContextAccessor httpContextAccessor;
        bool lDevelopment = true;

        public MenuController(IMenuApplication _objMenuApplication, ILogger<MenuController> _logger)
        {

            objMenuApplication = _objMenuApplication;
            logger = _logger;

            objResponseApp = new ResponseHelper();
            httpContextAccessor = new HttpContextAccessor();
            lDevelopment = Convert.ToBoolean(httpContextAccessor.HttpContext?.Items["lDevelopment"] ?? true);
        }

        [HttpGet]
        [Route("consultar-menus-usuario")]
        [Authorize]

        public async Task<IActionResult> ObtenerMenuUsuario()
        {
            try
            {
                int perfil_id = Convert.ToInt32(User.FindFirstValue("perfil_id"));

                int sistema_codigo = Convert.ToInt32(User.FindFirstValue("sistema_codigo"));

                var dataResponse = await objMenuApplication.obtenerMenuUsuario(perfil_id, sistema_codigo);

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
        [Route("consultar-menus-perfil")]
        [Authorize]

        public async Task<IActionResult> ObtenerMenuSistema([FromQuery] int sistema_id, [FromQuery] int perfil_id)
        {
            try
            {
                var dataResponse = await objMenuApplication.obtenerMenuSistema(perfil_id, sistema_id);

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
