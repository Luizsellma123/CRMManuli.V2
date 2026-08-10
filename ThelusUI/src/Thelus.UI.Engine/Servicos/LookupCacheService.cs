using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Thelus.UI.Engine.Servicos
{
    public class LookupCacheService
    {
        private readonly HttpClient _http;
        private readonly Dictionary<string, (List<Dictionary<string, object>> Data, DateTime Timestamp)> _cache = new();
        private readonly TimeSpan _ttl = TimeSpan.FromMinutes(10); // Tempo de vida do cache

        public LookupCacheService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Dictionary<string, object>>> GetLookupAsync(string lookupKey)
        {
            if (string.IsNullOrEmpty(lookupKey)) return new();

            // Verifica se existe em cache e se ainda está válido dentro do TTL
            if (_cache.TryGetValue(lookupKey, out var entry))
            {
                if (DateTime.Now - entry.Timestamp < _ttl)
                {
                    return entry.Data;
                }
            }

            try
            {
                // Busca na API genérica
                var data = await _http.GetFromJsonAsync<List<Dictionary<string, object>>>($"api/generic/{lookupKey}");
                if (data != null)
                {
                    _cache[lookupKey] = (data, DateTime.Now);
                    return data;
                }
            }
            catch
            {
                // Se falhar a rede, retorna o cache expirado se houver, para não quebrar a tela
                if (_cache.TryGetValue(lookupKey, out var oldEntry))
                {
                    return oldEntry.Data;
                }
            }

            return new();
        }
    }
}