using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Thelus.Core.Servicos;

namespace Thelus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuCoreService _menuCoreService;

        public MenuController(IMenuCoreService menuCoreService)
        {
            _menuCoreService = menuCoreService;
        }

        [HttpGet("obter-menus")]
        public async Task<IActionResult> ObterMenus()
        {
            var menus = await _menuCoreService.ObterMenusTabelaAsync();
            return Ok(menus);
        }
    }
}