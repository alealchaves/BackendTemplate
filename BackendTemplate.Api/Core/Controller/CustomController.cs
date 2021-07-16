using BackendTemplate.Domain.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendTemplate.Api.Core.Controller
{
    public abstract class CustomController : ControllerBase
    {
        /// <summary>
        /// Gera resultado sem resposta em ServiceResult baseado no AJAX Security do OWASP.
        /// <para>OWASP: https://cheatsheetseries.owasp.org/cheatsheets/AJAX_Security_Cheat_Sheet.html</para>
        /// </summary>
        /// <param name="serviceResult"></param>
        /// <returns>ObjectResult</returns>
        protected IActionResult Result(ServiceResult serviceResult)
        {
            var objectResult = new ObjectResult(serviceResult);
            objectResult.StatusCode = serviceResult.CodeId;
            return objectResult;
        }
    }
}
