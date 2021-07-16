using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace BackendTemplate.Api.Controllers.Core.Attribute
{
    /// <summary>
    /// RequiredAuthorization Attribute
    /// </summary>
    public class RequiredGlobalizationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Constructor do RequiredGlobalizationAttribute
        /// </summary>
        public RequiredGlobalizationAttribute() : base()
        {
        }

        /// <summary>
        /// OnActionExecuting event executado antes da execuão da action
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string culture = context.HttpContext.Request.Headers["culture"];
            CultureInfo cultureInfo;

            if (string.IsNullOrWhiteSpace(culture))
            {
                cultureInfo = CultureInfo.GetCultureInfo("pt-BR");
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            else
            {
                cultureInfo = CultureInfo.GetCultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
