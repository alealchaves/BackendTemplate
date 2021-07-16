using BackendTemplate.Domain.Core.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace BackendTemplate.Api.Controllers.Core.Filter
{
    /// <summary>
    /// Exception Filter
    /// </summary>
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// ExceptionToResult (validar necessidade)
        /// </summary>
        public object ExceptionToResult { get; private set; }
        private readonly ILogger<ApiExceptionFilter> logger;

        /// <summary>
        /// Constructor do ApiExceptionFilter
        /// </summary>
        /// <param name="logger"></param>
        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// OnException event disparado por excetions na aplicação
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var result = new ServiceResult<bool>(false);

            switch (context.Exception)
            {
                case ArgumentNullException ae:
                    var message = $"Attributo {ae.ToString()} é obrigatório";
                    result.AddError(StatusCodes.Status400BadRequest, "bad_request", message);
                    logger.LogError(StatusCodes.Status400BadRequest, message);
                    break;
                case ValidationException ve:
                    result.AddErrors(StatusCodes.Status400BadRequest, ve.Errors);
                    logger.LogWarning(StatusCodes.Status400BadRequest, ve.Message);
                    break;
                default:
                    result.AddError(StatusCodes.Status500InternalServerError, ErrorResultDetail.INTERNAL_ERROR);
                    logger.LogError(context.Exception, "Handler genérico");
                    break;
            }

            context.Result = new ObjectResult(result);
            context.HttpContext.Response.StatusCode = result.CodeId;
        }
    }
}
