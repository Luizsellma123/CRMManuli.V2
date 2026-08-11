using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thelus.Core.Servicos;
using Thelus.UI.Engine.Modelos;
using Thelus.UI.Engine.Servicos;
using Thelus.UI.Testes.Modelos;
using Thelus.UI.Model.Entidades;

namespace Thelus.UI.Testes
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            // Se o App.razor estiver no próprio projeto Thelus.UI.Testes:
            builder.RootComponents.Add<Thelus.UI.Testes.App>("#app");

            // APONTE O BASEADDRESS PARA A SUA API
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44337/") });

            // =========================================================================
            // Vincula a interface à implementação HTTP
            // =========================================================================
            builder.Services.AddScoped<IGenericEntityService, HttpGenericEntityService>();
            builder.Services.AddScoped<EntityServiceResolver>();
            // =========================================================================

            // 1. Registra o Provedor de Menu específico
            builder.Services.AddScoped<IMenuProvider, MeuProjetoMenuProvider>();

            // 2. Registra o Serviço de Filtragem de Menus por Permissão
            builder.Services.AddScoped<IMenuService, MenuService>();

            // 3. Registra o serviço de Autenticação (AuthService)
            builder.Services.AddScoped<AuthService>();

            // 4. Registra o LayoutStateService inicial
            builder.Services.AddScoped<LayoutStateService>(sp =>
            {
                return new LayoutStateService
                {
                    CompanyName = "Thelus Engine",
                    UserName = "Visitante"
                };
            });

            // 5. Registre as Entidades no Engine Registry
            EntityRegistry.Register<EmpresaTeste>("EmpresaTeste");
            EntityRegistry.Register<EmpresaTeste>("empresas");

            EntityRegistry.Register<ClienteTeste>("ClienteTeste");
            EntityRegistry.Register<ClienteTeste>("clientes");

            EntityRegistry.Register<UsuarioTeste>("Usuario");
            EntityRegistry.Register<UsuarioTeste>("UsuarioTeste");
            EntityRegistry.Register<UsuarioTeste>("usuarios");

            EntityRegistry.Register<StatusModel>("Status");
            EntityRegistry.Register<StatusModel>("status");

            // Registro correto da Entidade de Indicadores
            EntityRegistry.Register<IndicadorTecnologiaInformacao>("Indicadores");
            EntityRegistry.Register<IndicadorTecnologiaInformacao>("indicadores");

            // 6. Registra o serviço de cache de lookups
            builder.Services.AddScoped<LookupCacheService>();

            await builder.Build().RunAsync();
        }
    }

    public class MeuProjetoMenuProvider : IMenuProvider
    {
        public List<MenuItem> ObterMenuItens()
        {
            return new()
            {
                // Divisor de Categoria Principal (OBRIGATÓRIO PARA O NIFTY)
                new MenuItem { Title = "MENU", IsTitle = true },

                new MenuItem
                {
                    IdMenu = 10,
                    Title = "Home",
                    Url = "/",
                    Icon = "fa fa-home fa-lg"
                },
                new MenuItem
                {
                    IdMenu = 11,
                    Title = "Indicadores",
                    Url = "/gerenciar/indicadores",
                    Icon = "fa fa-line-chart fa-lg",
                    EntityName = "Indicadores"
                },

                // Divisor de Categoria: CADASTROS
                new MenuItem { Title = "CADASTROS", IsTitle = true },

                new MenuItem
                {
                    IdMenu = 26,
                    Title = "Clientes",
                    Url = "/gerenciar/clientes",
                    Icon = "fa fa-child fa-lg",
                    EntityName = "Clientes"
                },
                new MenuItem
                {
                    IdMenu = 22,
                    Title = "Empresas",
                    Url = "/gerenciar/empresas",
                    Icon = "fa fa-building fa-lg",
                    EntityName = "Empresas"
                },

                // Divisor de Categoria: CONTROLE DE ACESSO
                new MenuItem { Title = "CONTROLE DE ACESSO", IsTitle = true },

                new MenuItem
                {
                    IdMenu = 27,
                    Title = "Usuários",
                    Url = "/gerenciar/usuarios",
                    Icon = "fa fa-users fa-lg",
                    EntityName = "Usuarios"
                },
                new MenuItem
                {
                    IdMenu = 25,
                    Title = "Perfis de Acesso",
                    Url = "/gerenciar/perfis",
                    Icon = "fa fa-shield fa-lg",
                    EntityName = "Perfis"
                }
            };
        }
    }
}