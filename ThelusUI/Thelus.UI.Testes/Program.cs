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

            // 4. Registra o LayoutStateService inicial (sem montar o menu no boot)
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

            // 6. Registra o serviço de cache de lookups
            builder.Services.AddScoped<LookupCacheService>();

            await builder.Build().RunAsync();
        }
    }

    // Provedor de Menu expandido com os módulos de Cadastros e Controle de Acesso
    public class MeuProjetoMenuProvider : IMenuProvider
    {
        public List<MenuItem> ObterMenuItens()
        {
            return new()
            {
                new MenuItem { Title = "MENU PRINCIPAL", IsTitle = true },

                // MÓDULO: CADASTROS
                new MenuItem
                {
                    IdMenu = 17,
                    Title = "Cadastros",
                    Icon = "bx bx-folder",
                    SubItems = new()
                    {
                        new MenuItem { IdMenu = 26, Title = "Clientes", Url = "/gerenciar/clientes", EntityName = "Clientes" },
                        new MenuItem { IdMenu = 22, Title = "Empresas", Url = "/gerenciar/empresas", EntityName = "Empresas" }
                    }
                },

                // MÓDULO: CONTROLE DE ACESSO
                new MenuItem
                {
                    IdMenu = 15,
                    Title = "Controle de Acesso",
                    Icon = "bx bx-shield-quarter",
                    SubItems = new()
                    {
                        new MenuItem { IdMenu = 27, Title = "Usuários", Url = "/gerenciar/usuarios", EntityName = "Usuarios" },
                        new MenuItem { IdMenu = 25, Title = "Perfis de Acesso", Url = "/gerenciar/perfis", EntityName = "Perfis" }
                    }
                }
            };
        }
    }
}