using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BackendTemplate.Api.Controllers.Core.Filter
{
    /// <summary>
    /// Logging Filter
    /// </summary>
    public class ApiLoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger<ApiLoggingFilter> logger;

        /// <summary>
        /// Constructor do ApiLoggingFilter
        /// </summary>
        /// <param name="logger"></param>
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// OnResultExecutionAsync event executado assincronamente antes da ação de resultado
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            logger.LogInformation(context.HttpContext.User.Identity.Name);

            return base.OnResultExecutionAsync(context, next);
        }
    }
}
