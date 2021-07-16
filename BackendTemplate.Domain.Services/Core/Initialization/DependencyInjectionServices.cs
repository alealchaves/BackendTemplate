using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Infra.CrossCode;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BackendTemplate.Domain.Services.Initialization
{
    public static class DependencyInjectionServices
    {
        public static void AddServicesDependencies(this IServiceCollection services)
        {
            services.AddServices();
            services.AddValidators();

            //Mapeamento realizado por Reflection.
            //Basta que a interface do repositório herde de IBaseService ou IBaseValidator

            //services.AddTransient<IDummyGetPaginateService, DummyGetPaginateService>();
            //services.AddTransient<IDummyGetService, DummyGetService>();
            //services.AddTransient<IDummyCreateService, DummyCreateService>();
            //services.AddTransient<IDummyRemoveService, DummyRemoveService>();
            //services.AddTransient<IDummyRemoveAllService, DummyRemoveAllService>();
            //services.AddTransient<IDummyValidator, DummyValidator>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            IEnumerable<Type> assemblyTypes = typeof(DependencyInjectionServices).Assembly.GetNoAbstractTypes();
            services.AddImplementations(ServiceLifetime.Transient, typeof(IBaseService), assemblyTypes);
        }

        private static void AddValidators(this IServiceCollection services)
        {
            IEnumerable<Type> assemblyTypes = typeof(DependencyInjectionServices).Assembly.GetNoAbstractTypes();
            services.AddImplementations(ServiceLifetime.Transient, typeof(IBaseValidator), assemblyTypes);
        }
    }
}
