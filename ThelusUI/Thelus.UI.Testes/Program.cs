using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;
using Thelus.UI.Interface.Config;

namespace Thelus.UI.Interface
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Componente Raiz
            builder.RootComponents.Add<Thelus.UI.Interface.App>("#app");

            // Configuração dos Serviços do Framework e API
            builder.Services.AddThelusFramework("https://localhost:44337/");

            // Mapeamento e Registro das Entidades
            DependencyInjectionExtensions.RegisterFrameworkEntities();

            await builder.Build().RunAsync();
        }
    }
}