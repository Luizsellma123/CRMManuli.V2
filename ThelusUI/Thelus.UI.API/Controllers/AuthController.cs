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
    }
}