using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackendTemplate.Api.Core.Controller
{
    /// <summary>
    /// Controller de healthchecks
    /// </summary>
    [ApiController]
    public class HealthController : CustomController
    {
        private readonly ILogger<HealthController> logger;

        /// <summary>
        /// Construtor do HealthController
        /// </summary>
        /// <param name="logger"></param>
        public HealthController(ILogger<HealthController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Health check de deploy. Deve verificar o status de todos os serviços que a aplicação acessa
        /// </summary>
        /// <returns>ActionResult referente ao resultado</returns>
        [HttpGet, AllowAnonymous]
        [Route("healthcheck/deploy")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult DeployHealthCheck()
        {
            logger.LogInformation("Healthcheck Deploy Ok!");
            return Ok();
        }

        /// <summary>
        /// Health check básico. Apenas um ping no servidor
        /// </summary>
        /// <returns>ActionResult referente ao resultado</returns>
        [HttpGet, AllowAnonymous]
        [Route("healthcheck")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult HealthCheck()
        {
            return Ok();
        }
    }
}