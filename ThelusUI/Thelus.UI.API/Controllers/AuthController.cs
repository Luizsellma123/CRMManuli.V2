using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Thelus.Core.Servicos;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthCoreService _authCoreService;

        public AuthController(IAuthCoreService authCoreService)
        {
            _authCoreService = authCoreService;
        }

        // Rota: POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var resultado = await _authCoreService.AutenticarAsync(request);

            return Ok(resultado);
        }

        // Rota: POST /api/auth/validar-sessao (Consumida pelo Blazor WASM ao ler os Cookies do VendasWeb)
        [HttpPost("validar-sessao")]
        public async Task<IActionResult> ValidarSessao([FromBody] ValidarSessaoDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Usuario))
            {
                return BadRequest("Usuário não informado.");
            }

            // Monta a requisição para o seu AuthCoreService consultar permissões/menus
            var loginRequest = new LoginRequestDto
            {
                EmpresaId = request.EmpresaId > 0 ? request.EmpresaId : 1, // Empresa padrão caso não informada
                Usuario = request.Usuario,
                Senha = "123456" // Tratativa padrão até o aceite do Token direto no AuthCoreService
            };

            var resultado = await _authCoreService.AutenticarAsync(loginRequest);

            if (!resultado.Sucesso)
            {
                return Unauthorized(resultado);
            }

            return Ok(resultado);
        }
    }

    public class ValidarSessaoDto
    {
        public int EmpresaId { get; set; }
        public string Usuario { get; set; }
        public string Token { get; set; }
    }
}