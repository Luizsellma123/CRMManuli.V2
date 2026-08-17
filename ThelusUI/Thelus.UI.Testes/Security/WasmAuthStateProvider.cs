using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Interface.Security
{
    public class WasmAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public WasmAuthStateProvider(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // 1. Lê os cookies gerados pelo Web Forms (VendasWeb)
                string usuario = await GetCookieAsync("usuario");
                string token = await GetCookieAsync("token");

                if (!string.IsNullOrEmpty(usuario))
                {
                    // 2. Chama a API para reidratar a sessão e as permissões
                    var response = await _httpClient.PostAsJsonAsync("api/auth/validar-sessao", new ValidarSessaoDto
                    {
                        Usuario = usuario,
                        Token = token
                    });

                    if (response.IsSuccessStatusCode)
                    {
                        // CORREÇÃO CRÍTICA: Força o JsonSerializer a ignorar maiúsculas/minúsculas no DTO
                        var jsonOptions = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>(jsonOptions);

                        if (result != null && result.Sucesso)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, result.NomeUsuario ?? usuario),
                                new Claim("Token", result.Token ?? "")
                            };

                            // Adiciona as claims "MenuPermitido" para os IDs recebidos da API
                            if (result.IdsMenuPermitidos != null)
                            {
                                foreach (var menuId in result.IdsMenuPermitidos)
                                {
                                    claims.Add(new Claim("MenuPermitido", menuId.ToString()));
                                }
                            }

                            var identity = new ClaimsIdentity(claims, "WebFormsCookieAuth");
                            var userPrincipal = new ClaimsPrincipal(identity);

                            return new AuthenticationState(userPrincipal);
                        }
                    }
                }
            }
            catch
            {
                // Em caso de falha de comunicação com a API, mantém o estado não autenticado
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Método auxiliar para forçar atualização no Blazor se necessário
        public void NotificarEstadoAlterado()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<string> GetCookieAsync(string cookieName)
        {
            try
            {
                string cookieString = await _jsRuntime.InvokeAsync<string>("eval", "document.cookie");
                if (string.IsNullOrEmpty(cookieString)) return null;

                var cookies = cookieString.Split(';');
                foreach (var cookie in cookies)
                {
                    var parts = cookie.Trim().Split('=');
                    if (parts.Length == 2 && parts[0] == cookieName)
                    {
                        return Uri.UnescapeDataString(parts[1]);
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }

    public class ValidarSessaoDto
    {
        public int EmpresaId { get; set; }
        public string Usuario { get; set; }
        public string Token { get; set; }
    }
}