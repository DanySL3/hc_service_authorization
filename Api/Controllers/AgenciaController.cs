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
    //[Authorize]

    public class AgenciaController : ControllerBase
    {
        private readonly IAgenciaApplication consultaAgenciaApplication;
        private readonly ILogger<AgenciaController> logger;
        private readonly IResponseHelper objResponseApp;
        private readonly IHttpContextAccessor httpContextAccessor;
        private bool lDevelopment;

        public AgenciaController(IAgenciaApplication _consultaAgenciaApplication, ILogger<AgenciaController> _logger)
        {

            consultaAgenciaApplication = _consultaAgenciaApplication;
            logger = _logger;

            objResponseApp = new ResponseHelper();
            httpContextAccessor = new HttpContextAccessor();
            lDevelopment = Convert.ToBoolean(httpContextAccessor.HttpContext?.Items["lDevelopment"]);
        }

        [HttpGet]
        [Route("listar-agencias-usuario")]

        public async Task<IActionResult> obtenerAgencia([FromQuery] int sistema_id = 0)
        {

            try
            {
                int usuario_id = Convert.ToInt32(User.FindFirstValue("usuario_id"));

                var dataResponse = await consultaAgenciaApplication.obtenerAgencia(usuario_id, sistema_id);

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
        [Route("listar-agencias")]

        public async Task<IActionResult> listarAgencia()
        {
            try
            {
                var dataResponse = await consultaAgenciaApplication.listarAgencia();

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
