using System.Collections.Generic;
using System.Linq;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Engine.Servicos
{
    public interface IMenuService
    {
        List<MenuItem> ObterMenuFiltrado(IEnumerable<int> idsMenuPermitidos);
    }

    public class MenuService : IMenuService
    {
        private readonly IMenuProvider _menuProvider;

        public MenuService(IMenuProvider menuProvider)
        {
            _menuProvider = menuProvider;
        }

        public List<MenuItem> ObterMenuFiltrado(IEnumerable<int> idsMenuPermitidos)
        {
            var todosOsMenus = _menuProvider.ObterMenuItens();
            var idsPermitidosSet = new HashSet<int>(idsMenuPermitidos);

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