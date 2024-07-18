using BackendTemplate.Api.Core.Controller;
using BackendTemplate.Domain.Interfaces.PerfilInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BackendTemplate.Api.Controllers
{
    /// <summary>
    /// Controller de Perfil
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : CustomController
    {
        private readonly ILogger<PerfilController> logger;

        /// <summary>
        /// Construtor do UsuarioController
        /// </summary>
        /// <param name="logger"></param>
        public PerfilController(ILogger<PerfilController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Lista os usuarios
        /// </summary>        
        /// <returns></returns>
        [HttpPost("listar")]
        [HttpOptions("listar")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> Listar(
            [FromServices] IPerfilSelectFacade facade)
        {
            var result = await facade.Select();
            return Result(result);
        }     
    }
}
