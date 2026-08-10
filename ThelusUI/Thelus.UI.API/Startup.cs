using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thelus.Core.Dados;
using Thelus.Core.Config;
using Thelus.Core.Servicos;

namespace Thelus.UI.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Inicializa o gerenciador global para o Core conseguir ler a ConnectionString
            ConfigurationManager.Initialize(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 1. REGISTRO DA POLÍTICA DE CORS (LIBERA O ACESSO DO BLAZOR)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazor", policy =>
                {
                    policy.WithOrigins("https://localhost:44395", "http://localhost:44395")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Thelus.UI.API", Version = "v1" });
            });

            // =========================================================================
            // REGISTROS DA ENGINE E BANCO DE DADOS NA API
            // =========================================================================
            services.AddScoped<DatabaseAccess>();
            services.AddScoped<IEntityService, UsuarioServico>();

            services.AddScoped<IGenericEntityService, DatabaseGenericEntityService>();
            services.AddScoped<EntityServiceResolver>();

            // 2. REGISTRO DO SERVIÇO DE AUTENTICAÇÃO DO CORE
            services.AddScoped<IAuthCoreService, AuthCoreService>();
            // =========================================================================
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thelus.UI.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // 2. ATIVAÇÃO DO CORS (DEVE FICAR OBRIGATORIAMENTE ENTRE UseRouting E UseAuthorization)
            app.UseCors("AllowBlazor");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}