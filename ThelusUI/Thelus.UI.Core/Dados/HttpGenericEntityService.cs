using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos; // Importante para reconhecer a classe ResultadoOperacao

namespace Thelus.Core.Servicos
{
    public class HttpGenericEntityService : IGenericEntityService
    {
        private readonly HttpClient _httpClient;

        public HttpGenericEntityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 1. LISTAGEM VIA HTTP
        public async Task<List<dynamic>> ObterDadosGenericosAsync(FiltroConsulta filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro?.EntityName))
            {
                return new List<dynamic>();
            }

            try
            {
                var resultado = await _httpClient.GetFromJsonAsync<List<dynamic>>($"api/generic/{filtro.EntityName}");
                return resultado ?? new List<dynamic>();
            }
            catch
            {
                return new List<dynamic>();
            }
        }

        // 2. BUSCA POR ID VIA HTTP
        public async Task<dynamic> ObterPorIdGenericoAsync(string entityName, int id)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return null;
            }

            try
            {
                return await _httpClient.GetFromJsonAsync<dynamic>($"api/generic/{entityName}/{id}");
            }
            catch
            {
                return null;
            }
        }

        // 3. GRAVAÇÃO VIA HTTP (RETORNANDO RESULTADOOPERACAO)
        public async Task<ResultadoOperacao> SalvarGenericoAsync(string entityName, object item)
        {
            if (item == null || string.IsNullOrWhiteSpace(entityName))
            {
                return ResultadoOperacao.Falha("A entidade ou o objeto para gravação está nulo.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/generic/{entityName}", item);

                // Tenta desserializar o ResultadoOperacao vindo da Web API (GenericController)
                var resultado = await response.Content.ReadFromJsonAsync<ResultadoOperacao>();

                if (resultado != null)
                {
                    return resultado;
                }

                if (response.IsSuccessStatusCode)
                {
                    return ResultadoOperacao.OK("Registro salvo com sucesso!");
                }

                return ResultadoOperacao.Falha("Não foi possível processar a resposta do servidor.");
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falha($"Erro de comunicação HTTP: {ex.Message}");
            }
        }
    }
}