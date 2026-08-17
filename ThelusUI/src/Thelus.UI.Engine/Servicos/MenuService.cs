using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Engine.Servicos
{
    public interface IMenuService
    {
        Task<List<MenuItem>> ObterMenuFiltradoAsync(IEnumerable<int> idsMenuPermitidos);
        Task<List<MenuItem>> ObterMenuFiltradoAsync();
    }

    public class MenuService : IMenuService
    {
        private readonly IMenuProvider _menuProvider;
        private readonly AuthenticationStateProvider _authStateProvider;

        public MenuService(IMenuProvider menuProvider, AuthenticationStateProvider authStateProvider = null)
        {
            _menuProvider = menuProvider;
            _authStateProvider = authStateProvider;
        }

        // =========================================================================
        // MÉTODOS ASSÍNCRONOS PARA O BLAZOR WASM (Lê a Claim "MenuPermitido")
        // =========================================================================
        public async Task<List<MenuItem>> ObterMenuFiltradoAsync()
        {
            if (_authStateProvider == null)
            {
                return await ObterMenuFiltradoAsync(Enumerable.Empty<int>());
            }

            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                return new List<MenuItem>();
            }

            // Extrai os IDs de menu gravados no WasmAuthStateProvider ("MenuPermitido")
            var idsPermitidos = user.Claims
                                    .Where(c => c.Type == "MenuPermitido")
                                    .Select(c => int.TryParse(c.Value, out int id) ? id : 0)
                                    .Where(id => id > 0);

            return await ObterMenuFiltradoAsync(idsPermitidos);
        }

        public async Task<List<MenuItem>> ObterMenuFiltradoAsync(IEnumerable<int> idsMenuPermitidos)
        {
            // Consulta assíncrona do catálogo no banco/API via IMenuProvider
            var todosOsMenus = await _menuProvider.ObterMenuItensAsync();
            var idsPermitidosSet = new HashSet<int>(idsMenuPermitidos ?? Enumerable.Empty<int>());

            return FiltrarItens(todosOsMenus, idsPermitidosSet);
        }

        private List<MenuItem> FiltrarItens(List<MenuItem> itens, HashSet<int> idsPermitidos)
        {
            var resultado = new List<MenuItem>();

            foreach (var item in itens)
            {
                // Se for título de seção (ex: "MENU PRINCIPAL"), mantemos temporariamente
                if (item.IsTitle)
                {
                    resultado.Add(item);
                    continue;
                }

                // Se o item possui subitens, filtramos a árvore recursivamente
                if (item.SubItems != null && item.SubItems.Any())
                {
                    var subItensFiltrados = FiltrarItens(item.SubItems, idsPermitidos);

                    // O pai aparece se ele próprio for permitido OU se sobrou algum filho acessível dentro dele
                    bool paiTemPermissao = item.IdMenu == 0 || idsPermitidos.Contains(item.IdMenu);

                    if (subItensFiltrados.Any() || paiTemPermissao)
                    {
                        resultado.Add(new MenuItem
                        {
                            IdMenu = item.IdMenu,
                            Title = item.Title,
                            Icon = item.Icon,
                            IdIcone = item.IdIcone,
                            Url = item.Url,
                            EntityName = item.EntityName,
                            IsTitle = item.IsTitle,
                            SubItems = subItensFiltrados
                        });
                    }
                }
                else
                {
                    // Item folha: verifica se o usuário possui acesso direto ao IdMenu
                    if (item.IdMenu == 0 || idsPermitidos.Contains(item.IdMenu))
                    {
                        resultado.Add(item);
                    }
                }
            }

            // Limpeza inteligente: Remove títulos de seção que ficaram sem nenhum item filho visível
            return LimparTitulosVazios(resultado);
        }

        private List<MenuItem> LimparTitulosVazios(List<MenuItem> itens)
        {
            var itensLimpos = new List<MenuItem>();
            for (int i = 0; i < itens.Count; i++)
            {
                var atual = itens[i];
                if (atual.IsTitle)
                {
                    bool temFilhosAteProximoTitulo = false;
                    for (int j = i + 1; j < itens.Count; j++)
                    {
                        if (itens[j].IsTitle) break;
                        temFilhosAteProximoTitulo = true;
                        break;
                    }

                    if (temFilhosAteProximoTitulo)
                    {
                        itensLimpos.Add(atual);
                    }
                }
                else
                {
                    itensLimpos.Add(atual);
                }
            }
            return itensLimpos;
        }
    }
}