using Microsoft.Extensions.Configuration;

namespace Thelus.Core.Config
{
    public static class ConfigurationManager
    {
        private static IConfiguration _configuration;

        /// <summary>
        /// Registra a instância do IConfiguration do Blazor/ASP.NET.
        /// </summary>
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Obtém a Connection String sem tentar ler arquivos físicos do disco no navegador.
        /// </summary>
        public static string GetConnectionString(string name = "DefaultConnection")
        {
            // 1. Tenta buscar via IConfiguration se foi inicializado
            if (_configuration != null)
            {
                var connStr = _configuration.GetConnectionString(name);
                if (!string.IsNullOrEmpty(connStr))
                {
                    return connStr;
                }
            }

            // 2. Retorna string vazia no Blazor Client em vez de estourar a exceção 'FileNotFoundException'
            return string.Empty;
        }
    }
}