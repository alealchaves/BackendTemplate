using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace BackendTemplate.Api.Controllers.Core.Filter
{
    /// <summary>
    /// Configurações adicionais para swagger
    /// </summary>
    public class AdditionalSwaggerHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// Método para aplicar as configurações
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" },
                Required = false
            });
        }
    }
}
