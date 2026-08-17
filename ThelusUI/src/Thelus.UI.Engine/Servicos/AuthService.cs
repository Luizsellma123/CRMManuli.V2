using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Engine.Servicos
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly LayoutStateService _layoutState;
        private readonly IMenuService _menuService;
        private readonly IJSRuntime _js; // Injetado para ler os Cookies no Blazor

        // Evento para notificar componentes (ex: MainLayout) sobre mudanças de login/logout
        public event Action OnChange;

        public bool UsuarioEstaLogado { get; private set; } = false;
        public string NomeUsuario { get; private set; } = string.Empty;
        public string Token { get; private set; } = string.Empty;
        public int IdUsuario { get; private set; }

        public AuthService(HttpClient http, LayoutStateService layoutState, IMenuService menuService, IJSRuntime js)
        {
            _http = http;
            _layoutState = layoutState;
            _menuService = menuService;
            _js = js;
        }

        /// <summary>
        /// Reidrata a sessão do Razor lendo os cookies gerados pelo Web Forms
        /// </summary>
        public async Task InicializarSessaoAsync()
        {
            try
            {
                // Lê os cookies do navegador via JSInterop
                var cookieString = await _js.InvokeAsync<string>("eval", "document.cookie");

                if (!string.IsNullOrEmpty(cookieString))
                {
                    var cookies = cookieString.Split(';');
                    foreach (var c in cookies)
                    {
                        var kvp = c.Trim().Split('=');
                        if (kvp.Length == 2)
                        {
                            var chave = kvp[0].Trim();
                            var valor = Uri.UnescapeDataString(kvp[1].Trim());

                            if (chave.Equals("usuario", StringComparison.OrdinalIgnoreCase))
                            {
                                NomeUsuario = valor;
                                _layoutState.UserName = valor;
                            }

                            if (chave.Equals("IDUsuario", StringComparison.OrdinalIgnoreCase) && int.TryParse(valor, out int id))
                            {
                                IdUsuario = id;
                            }
                        }
                    }
                }

                // Se encontrou o cookie do usuário criado pelo Web Forms, ativa a sessão
                UsuarioEstaLogado = !string.IsNullOrEmpty(NomeUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao reidratar sessão via cookies: {ex.Message}");
                UsuarioEstaLogado = false;
            }

            NotificarMudancaEstado();
        }

        public async Task<bool> LoginAsync(string usuario, string senha, int empresaId = 1)
        {
            try
            {
                var payload = new LoginRequestDto
                {
                    Usuario = usuario,
                    Senha = senha,
                    EmpresaId = empresaId
                };

                var response = await _http.PostAsJsonAsync("api/auth/login", payload);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                    if (resultado != null && resultado.Sucesso)
                    {
                        UsuarioEstaLogado = true;
                        NomeUsuario = string.IsNullOrWhiteSpace(resultado.NomeUsuario) ? usuario : resultado.NomeUsuario;
                        Token = resultado.Token;

                        var idsDoBanco = resultado.IdsMenuPermitidos ?? new List<int>();

                        _layoutState.UserName = NomeUsuario;
                        _layoutState.SetMenu(await _menuService.ObterMenuFiltradoAsync(idsDoBanco));

                        NotificarMudancaEstado();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao realizar requisição de login: {ex.Message}");
            }

            return false;
        }

        public void Logout()
        {
            UsuarioEstaLogado = false;
            NomeUsuario = string.Empty;
            Token = string.Empty;
            _layoutState.SetMenu(new List<MenuItem>());

            NotificarMudancaEstado();
        }

        private void NotificarMudancaEstado() => OnChange?.Invoke();
    }
}