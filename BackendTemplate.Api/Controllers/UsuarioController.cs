using BackendTemplate.Api.Core.Controller;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BackendTemplate.Api.Controllers
{
    /// <summary>
    /// Controller de Usuario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : CustomController
    {
        private readonly ILogger<UsuarioController> logger;

        /// <summary>
        /// Construtor do UsuarioController
        /// </summary>
        /// <param name="logger"></param>
        public UsuarioController(ILogger<UsuarioController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Lista os usuarios
        /// </summary>        
        /// <returns></returns>
        [HttpGet("listar")]
        [HttpOptions("listar")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> Listar(
            [FromServices] IUsuarioSelectFacade facade)
        {
            var result = await facade.Select();
            return Result(result);
        }

        /// <summary>
        /// Lista os usuarios
        /// </summary>        
        /// <returns></returns>
        [HttpPut("AlterarSenha")]
        [HttpOptions("AlterarSenha")]
        //[Authorize(Roles = "Admin")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> AlterarSenha(
            [FromServices] IUsuarioUpdateFacade facade,
            AlterarSenhaRequest alterarSenhaRequest)
        {
            var result = await facade.AlterarSenha(alterarSenhaRequest);
            return Result(result);
        }
    }
}
