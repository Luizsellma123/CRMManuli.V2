using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Thelus.Core.Servicos;

namespace Thelus.UI.API.Servicos
{
    public class SessaoUsuarioApi : ISessaoUsuario
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessaoUsuarioApi(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string ObterCodigoUsuario()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return "LUIZ";

            // 1. Tenta obter do Header customizado enviado pelo Blazor
            if (context.Request.Headers.TryGetValue("X-Usuario", out var usuarioHeader) && !string.IsNullOrWhiteSpace(usuarioHeader))
            {
                return usuarioHeader.ToString();
            }

            // 2. Tenta obter das Claims
            var usuarioClaim = context.User?.FindFirst("UsuCod")?.Value
                            ?? context.User?.FindFirst(ClaimTypes.Name)?.Value;

            if (!string.IsNullOrWhiteSpace(usuarioClaim))
            {
                return usuarioClaim;
            }

            // 3. Fallback para desenvolvimento local
            return "LUIZ";
        }
    }
}