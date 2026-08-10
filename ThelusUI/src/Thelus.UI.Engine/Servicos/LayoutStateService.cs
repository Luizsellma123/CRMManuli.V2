using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Engine.Servicos
{
    public class RightMenuItem
    {
        public string Text { get; set; }
        public string Url { get; set; } = "javascript:void(0);";
        public bool IsActive { get; set; }
        public Action OnClick { get; set; }
    }

    public class LayoutStateService
    {
        // =========================================================================
        // 1. DADOS DO LAYOUT PRINCIPAL (CABECALHO / LOGOS / TEMA / MENU ESQUERDO)
        // =========================================================================
        public string LogoLightUrl { get; set; } = "assets/images/logo-light.png";
        public string LogoDarkUrl { get; set; } = "assets/images/logo-dark.png";
        public string LogoSmUrl { get; set; } = "assets/images/logo-sm.png";
        public string CompanyName { get; set; } = "Thelus Engine";
        public string UserName { get; set; } = "Administrador";
        public string UserAvatarUrl { get; set; } = "assets/images/users/avatar-4.jpg";

        public List<MenuItem> MenuItens { get; set; } = new();
        public bool IsDarkMode { get; private set; } = false;

        // =========================================================================
        // 2. DADOS DO MENU LATERAL DIREITO (RIGHT SIDEBAR)
        // =========================================================================
        public bool ShowRightSidebar { get; private set; }
        public List<RightMenuItem> RightMenuItems { get; private set; } = new();

        // =========================================================================
        // 3. EVENTOS DE NOTIFICAÇÃO (Sinaliza que algo mudou na tela)
        // =========================================================================
        public event Action OnStateChanged;
        public event Action OnChange; // Mantido para compatibilidade total com telas antigas

        // =========================================================================
        // 4. MÉTODOS DO LAYOUT PRINCIPAL
        // =========================================================================
        public void SetUser(string name, string avatarUrl)
        {
            UserName = name;
            UserAvatarUrl = avatarUrl;
            NotifyStateChanged();
        }

        public void SetMenu(List<MenuItem> menuItens)
        {
            MenuItens = menuItens;
            NotifyStateChanged();
        }

        public async Task ToggleThemeAsync(IJSRuntime js)
        {
            IsDarkMode = !IsDarkMode;
            var themeName = IsDarkMode ? "dark" : "light";
            await js.InvokeVoidAsync("eval", $"document.documentElement.setAttribute('data-bs-theme', '{themeName}')");
            NotifyStateChanged();
        }

        // =========================================================================
        // 5. MÉTODOS DO MENU LATERAL DIREITO
        // =========================================================================
        public void EnableRightSidebar(List<RightMenuItem> items)
        {
            ShowRightSidebar = true;
            RightMenuItems = items;
            NotifyStateChanged();
        }

        public void DisableRightSidebar()
        {
            ShowRightSidebar = false;
            RightMenuItems.Clear();
            NotifyStateChanged();
        }

        // Dispara tanto o OnStateChanged quanto o OnChange
        private void NotifyStateChanged()
        {
            OnStateChanged?.Invoke();
            OnChange?.Invoke();
        }

        // Método para carregar os menus completos de teste
        public void CarregarMenusIniciais()
        {
            var menuCompleto = ObterEstruturaDeMenus();
            SetMenu(menuCompleto); // Neste momento, carrega TUDO sem filtrar
        }

        // Estrutura completa de Menus (Já mapeada com as permissões futuras)
        private List<MenuItem> ObterEstruturaDeMenus()
        {
            return new List<MenuItem>
        {
        new MenuItem { Title = "MENU PRINCIPAL", IsTitle = true },

        // MÓDULO: CADASTROS DE NEGÓCIO
        // MÓDULO: CADASTROS DE NEGÓCIO
        new MenuItem
        {
            IdMenu = 17,
            Title = "Cadastros",
            Icon = "bx bx-building-house",
            SubItems = new()
            {
                new MenuItem { IdMenu = 22, Title = "Empresas", Url = "/gerenciar/Empresa", EntityName = "Empresa" },
                new MenuItem { IdMenu = 26, Title = "Clientes", Url = "/gerenciar/Cliente", EntityName = "Cliente" }
            }
        },

        // MÓDULO: CONTROLE DE ACESSO (O que estamos construindo)
        new MenuItem
        {
            IdMenu = 15,
            Title = "Controle de Acesso",
            Icon = "bx bx-shield-quarter",
            SubItems = new()
            {
                new MenuItem { IdMenu = 27, Title = "Usuários", Url = "/gerenciar/Usuario", EntityName = "Usuario" },
                new MenuItem { IdMenu = 25, Title = "Perfis de Acesso", Url = "/gerenciar/Perfil", EntityName = "Perfil" }
            }
        }
        };
        }

        // 🟢 MÉTODOS FUTUROS (Quando ativarmos o filtro por usuário logado)
        public void CarregarMenusPorPermissao(Func<int, bool> checarPermissao)
        {
            var todosOsMenus = ObterEstruturaDeMenus();
            var menusFiltrados = new List<MenuItem>();

            foreach (var menu in todosOsMenus)
            {
                if (menu.IsTitle)
                {
                    menusFiltrados.Add(menu);
                    continue;
                }

                // Se o menu tem subitens, filtra os subitens permitidos pelo IdMenu
                if (menu.SubItems != null && menu.SubItems.Any())
                {
                    var subItensPermitidos = menu.SubItems
                        .Where(sub => sub.IdMenu == 0 || checarPermissao(sub.IdMenu))
                        .ToList();

                    // Se o pai tem permissão direta OU possui subitens permitidos, ele entra na árvore
                    if (subItensPermitidos.Any() || (menu.IdMenu > 0 && checarPermissao(menu.IdMenu)))
                    {
                        menusFiltrados.Add(new MenuItem
                        {
                            IdMenu = menu.IdMenu,
                            Title = menu.Title,
                            Icon = menu.Icon,
                            IdIcone = menu.IdIcone,
                            EntityName = menu.EntityName,
                            SubItems = subItensPermitidos
                        });
                    }
                }
                else if (menu.IdMenu == 0 || checarPermissao(menu.IdMenu))
                {
                    menusFiltrados.Add(menu);
                }
            }

            SetMenu(menusFiltrados);
        }
    }
}