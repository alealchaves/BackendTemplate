using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Infra.CrossCode;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BackendTemplate.Facade.Core.Initialization
{
    public static class DependencyInjectionServices
    {
        public static void AddFacadeDependencies(this IServiceCollection services)
        {
            services.AddFacades();

            //Mapeamento realizado por Reflection.
            //Basta que a interface do repositório herde de IBaseFacade

            //services.AddTransient<IDummyGetPaginateFacade, DummyGetPaginateFacade>();
            //services.AddTransient<IDummyGetFacade, DummyGetFacade>();
            //services.AddTransient<IDummyCreateFacade, DummyCreateFacade>();
            //services.AddTransient<IDummyRemoveFacade, DummyRemoveFacade>();
            //services.AddTransient<IDummyRemoveAllFacade, DummyRemoveAllFacade>();
        }

        private static void AddFacades(this IServiceCollection services)
        {
            IEnumerable<Type> assemblyTypes = typeof(DependencyInjectionServices).Assembly.GetNoAbstractTypes();
            services.AddImplementations(ServiceLifetime.Transient, typeof(IBaseFacade), assemblyTypes);
        }
    }
}
