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
    [Authorize]

    public class SistemaController : ControllerBase
    {
        private readonly ISistemaApplication objSistemaApplication;
        private readonly ILogger<SistemaController> logger;
        private readonly IResponseHelper objResponseHelper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private bool lDevelopment;

        public SistemaController(ISistemaApplication _objSistemaApplication, ILogger<SistemaController> _logger)
        {
            objSistemaApplication = _objSistemaApplication;
            logger = _logger;

            objResponseHelper = new ResponseHelper();
            httpContextAccessor = new HttpContextAccessor();
            lDevelopment = Convert.ToBoolean(httpContextAccessor.HttpContext?.Items["lDevelopment"]);
        }

        [HttpGet]
        [Route("listar-sistema-usuario")]

        public async Task<IActionResult> obtenerAplicacion()
        {
            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await objSistemaApplication.listarSistemasUsuario(usuario_id);

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
        [Route("listar-sistemas")]

        public async Task<IActionResult> listarSistemas()
        {
            try
            {
                var dataResponse = await objSistemaApplication.listarSistemas();

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
        [Route("consultar-identificador")]

        public async Task<IActionResult> obtenerIdentiticador([FromQuery] int sistema_codigo)
        {
            try
            {
                var dataResponse = await objSistemaApplication.obtenerIdentiticador(sistema_codigo);

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
