using AutoMapper;
using BackendTemplate.Api.Core.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTemplate.Api.Core.Initialization
{
    /// <summary>
    /// DependencyInjectionServices
    /// </summary>
    public static class DependencyInjectionServices
    {
        /// <summary>
        /// Executa a configuração da injeção de dependência para automapper
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
        }
    }
}
