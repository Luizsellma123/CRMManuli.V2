using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thelus.Core.Dados;
using Thelus.Core.Servicos;
using Thelus.UI.API.Servicos; // Adicionado para enxergar a SessaoUsuarioApi

namespace Thelus.UI.API.Config
{
    public static class ApiDependencyInjectionExtensions
    {
        public static IServiceCollection AddThelusApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Configuração de CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazor", policy =>
                {
                    policy.WithOrigins("https://localhost:44395", "http://localhost:44395")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // 2. Contexto HTTP e Sessão do Usuário
            services.AddHttpContextAccessor();
            services.AddScoped<ISessaoUsuario, SessaoUsuarioApi>();

            // 3. Acesso a Dados e Engine (Injeção Múltipla de IEntityService)
            services.AddScoped<DatabaseAccess>();
            services.AddScoped<IEntityService, UsuarioServico>();
            services.AddScoped<IEntityService, NegociacaoServico>();
            services.AddScoped<IEntityService, AcessoUsuarioServico>();

            services.AddScoped<IGenericEntityService, DatabaseGenericEntityService>();
            services.AddScoped<EntityServiceResolver>();

            // 4. Serviços de Negócio do Core
            services.AddScoped<IAuthCoreService, AuthCoreService>();
            services.AddScoped<IMenuCoreService, MenuCoreService>();

            return services;
        }
    }
}