using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Thelus.UI.Interface.Handlers
{
    public class UsuarioHeaderHandler : DelegatingHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public UsuarioHeaderHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 1. REGRA DE ESCAPE: Se for chamada de Login ou Validação de Sessão, NÃO tenta injetar o header de usuário
            string path = request.RequestUri?.AbsolutePath?.ToLower() ?? "";
            if (path.Contains("/api/auth/"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            // 2. Injeta o header apenas se ainda não existir
            if (!request.Headers.Contains("X-Usuario"))
            {
                string usuario = "LUIZ"; // Fallback padrão

                try
                {
                    var authStateProvider = _serviceProvider.GetService<AuthenticationStateProvider>();
                    if (authStateProvider != null)
                    {
                        // Obtém o estado atual sem forçar novo ciclo de validação
                        var authState = await authStateProvider.GetAuthenticationStateAsync();
                        var user = authState?.User;

                        if (user?.Identity?.IsAuthenticated == true)
                        {
                            var codigoFound = user.FindFirst("UsuCod")?.Value
                                           ?? user.FindFirst(ClaimTypes.Name)?.Value
                                           ?? user.Identity.Name;

                            if (!string.IsNullOrWhiteSpace(codigoFound))
                            {
                                usuario = codigoFound;
                            }
                        }
                    }
                }
                catch
                {
                    // Mantém o fallback caso o estado ainda esteja inicializando
                }

                request.Headers.Add("X-Usuario", usuario);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}