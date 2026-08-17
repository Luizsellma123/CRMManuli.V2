using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;
using Thelus.UI.Engine.Servicos;

namespace Thelus.UI.Interface.Providers
{
    public class CrmMenuProvider : IMenuProvider
    {
        private readonly HttpClient _http;

        public CrmMenuProvider(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MenuItem>> ObterMenuItensAsync()
        {
            try
            {
                var menus = await _http.GetFromJsonAsync<List<MenuItem>>("api/menu/obter-menus");
                return menus ?? new List<MenuItem>();
            }
            catch
            {
                return new List<MenuItem>();
            }
        }
    }
}