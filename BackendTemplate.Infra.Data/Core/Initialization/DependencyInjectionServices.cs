using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Infra.CrossCode;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using BackendTemplate.Infra.Data.Core.UnitOfWorks;
using BackendTemplate.Domain.Core.i18n;
using BackendTemplate.Infra.Data.i18n;
using FluentValidation;

namespace BackendTemplate.Infra.Data.Core.Initialization
{
    public static class DependencyInjectionServices
    {
        public static void AddInfraDataDependencies(this IServiceCollection services)
        {
            services.AddUnitOfWork();
            services.AddGlobalization();
            services.AddRepositories();

            //Mapeamento realizado por Reflection.
            //Basta que a interface do repositório herde de IBaseRepository
            //services.AddScoped<IDummyRepository, DummyRepository>();
            
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            IEnumerable<Type> assemblyTypes = typeof(DependencyInjectionServices).Assembly.GetNoAbstractTypes();
            services.AddImplementations(ServiceLifetime.Scoped, typeof(IBaseRepository), assemblyTypes);
        }

        private static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddGlobalization(this IServiceCollection services)
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            services.AddScoped<IGlobalizationResource, GlobalizationResource>();

            services.AddLocalization(o => o.ResourcesPath = GlobalizationResource.GetResourceNamespacePath());
            services.Configure<RequestLocalizationOptions>(o =>
            {
                var supportedCultures = new[] {
                    new CultureInfo("pt-BR"),
                    new CultureInfo("en-US")
                };

                o.DefaultRequestCulture = new RequestCulture("pt-BR");

                o.SupportedCultures = supportedCultures;
                o.SupportedUICultures = supportedCultures;

                o.FallBackToParentCultures = true;
                o.FallBackToParentUICultures = true;

                o.RequestCultureProviders = new IRequestCultureProvider[] {
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });
        }
    }
}
