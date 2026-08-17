using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Net.Http;
using Thelus.Core.Servicos;
using Thelus.UI.Engine.Modelos;
using Thelus.UI.Engine.Servicos;
using Thelus.UI.Interface.Handlers; // Importante para localizar o UsuarioHeaderHandler
using Thelus.UI.Interface.Providers;
using Thelus.UI.Interface.Security;
using Thelus.UI.Model.Entidades;

namespace Thelus.UI.Interface.Config
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddThelusFramework(this IServiceCollection services, string apiBaseUrl)
        {
            // 1. Registra o Handler Interceptador HTTP
            services.AddTransient<UsuarioHeaderHandler>();

            // 2. Configura o HttpClient usando Factory e o Handler
            services.AddHttpClient("ThelusApi", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            })
            .AddHttpMessageHandler<UsuarioHeaderHandler>();

            // 3. Define o HttpClient padrão para ser injetado via DI na Engine e Serviços
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ThelusApi"));

            // Autenticação & Autorização
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, WasmAuthStateProvider>();
            services.AddScoped<AuthService>();

            // Serviços da Engine e Provedores
            services.AddScoped<IGenericEntityService, HttpGenericEntityService>();
            services.AddScoped<EntityServiceResolver>();
            services.AddScoped<IMenuProvider, CrmMenuProvider>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<LookupCacheService>();

            // Estado de Layout
            services.AddScoped(sp => new LayoutStateService
            {
                CompanyName = "Thelus Engine",
                UserName = "Visitante"
            });

            return services;
        }

        public static void RegisterFrameworkEntities()
        {
            EntityRegistry.Register<EmpresaTeste>("EmpresaTeste");
            EntityRegistry.Register<EmpresaTeste>("empresas");

            EntityRegistry.Register<UsuarioTeste>("Usuario");
            EntityRegistry.Register<UsuarioTeste>("UsuarioTeste");
            EntityRegistry.Register<UsuarioTeste>("usuarios");

            EntityRegistry.Register<StatusModel>("Status");
            EntityRegistry.Register<StatusModel>("status");

            EntityRegistry.Register<IndicadorTecnologiaInformacao>("Indicadores");
            EntityRegistry.Register<IndicadorTecnologiaInformacao>("indicadores");

            EntityRegistry.Register<Negociacao>("Negociacao");
            EntityRegistry.Register<Negociacao>("negociacao");
        }
    }
}