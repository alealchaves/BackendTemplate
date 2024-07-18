using BackendTemplate.Api.Controllers.Core.Filter;
using BackendTemplate.Api.Core.Initialization;
using BackendTemplate.Domain.Services.Initialization;
using BackendTemplate.Facade.Core.Initialization;
using BackendTemplate.Infra.Data;
using BackendTemplate.Infra.Data.Core.Initialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace BackendTemplate.Api
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "_defaultCorsPolicy";
        public IConfiguration _Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            //services.AddControllersWithViews().AddJsonOptions(
            // options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiLoggingFilter>(1);
                options.Filters.Add<ApiExceptionFilter>(2);
            });

            var migrationsAssembly = typeof(MyAppContext).Assembly.GetName().Name;
            var migrationTable = "__TemplateDBMigrationsHistory";
            services.AddDbContext<MyAppContext>(DbOptionsBuilder(migrationsAssembly, migrationTable));

            services.AddCors(options =>
            {
                options.AddPolicy(name: DefaultCorsPolicyName,
                                  builder =>
                                  {
                                      builder
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowAnyOrigin();
                                  });
            });

            services.AddOptions();

            services.AddRouting(options => options.LowercaseUrls = true);

            var JWTSecret = this._Configuration["JWTSecret"].ToString().Trim();
            var key = Encoding.ASCII.GetBytes(JWTSecret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            services.AddInfraDataDependencies();
            services.AddServicesDependencies();
            services.AddFacadeDependencies();
            services.AddAutoMapperDependencies();

            services.AddHttpContextAccessor();
            ConfigureSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(DefaultCorsPolicyName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendTemplate.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            bool HasStaticFiles = Convert.ToBoolean(_Configuration["HasStaticFiles"].ToString().Trim());
            if (HasStaticFiles)
            {
                if (env.WebRootPath != null)
                {
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "assets")),
                        RequestPath = "/assets"
                    });
                }

                app.UseSpaStaticFiles();

                app.MapWhen(x => !x.Request.Path.StartsWithSegments("/api"), builder =>
                {
                    builder.UseSpa(x => x.Options.SourcePath = "wwwroot");
                });
            }

            //Descomentar para ativar migration automático
            //using (var serviceScope = app.ApplicationServices.CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<MyAppContext>();
            //    context.Database.Migrate();
            //}
        }

        private Action<DbContextOptionsBuilder> DbOptionsBuilder(
            string migrationsAssembly, string migrationTable)
        {
            var devDBConnection = _Configuration["DevDBConnection"];

            return options =>
            {
                options.UseSqlServer(devDBConnection, b =>
                {
                    b.MigrationsAssembly(migrationsAssembly);
                    b.MigrationsHistoryTable(migrationTable);
                }).UseLazyLoadingProxies();
            };
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = _Configuration["SwaggerApiName"],
                    Version = "v1",
                    Description = _Configuration["SwaggerApiDescription"]
                });

                option.OperationFilter<AdditionalSwaggerHeaderFilter>();
            });
        }
    }
}
