using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Engine.Servicos
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly LayoutStateService _layoutState;
        private readonly IMenuService _menuService;

        // Evento para notificar componentes (ex: MainLayout) sobre mudanças de login/logout
        public event Action OnChange;

        public bool UsuarioEstaLogado { get; private set; } = false;
        public string NomeUsuario { get; private set; } = string.Empty;
        public string Token { get; private set; } = string.Empty;

        public AuthService(HttpClient http, LayoutStateService layoutState, IMenuService menuService)
        {
            _http = http;
            _layoutState = layoutState;
            _menuService = menuService;
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

                // Faz a chamada POST na API Backend na rota api/auth/login
                var response = await _http.PostAsJsonAsync("api/auth/login", payload);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                    if (resultado != null && resultado.Sucesso)
                    {
                        UsuarioEstaLogado = true;
                        NomeUsuario = string.IsNullOrWhiteSpace(resultado.NomeUsuario) ? usuario : resultado.NomeUsuario;
                        Token = resultado.Token;

                        // IDs devolvidos pela API a partir da consulta ao banco de dados
                        var idsDoBanco = resultado.IdsMenuPermitidos ?? new List<int>();

                        // Atualiza a UI e recalcula o menu dinâmico
                        _layoutState.UserName = NomeUsuario;
                        _layoutState.SetMenu(_menuService.ObterMenuFiltrado(idsDoBanco));

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

            // Dispara o evento avisando a UI que o usuário saiu
            NotificarMudancaEstado();
        }

        private void NotificarMudancaEstado() => OnChange?.Invoke();
    }
}