using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.Core.Dados;

namespace Thelus.Core.Servicos
{
    public class GenericEntityService
    {
        private readonly DatabaseAccess _db;

        public GenericEntityService(DatabaseAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// Realiza a busca dinâmica diretamente no banco de dados via Dapper.
        /// </summary>
        public async Task<List<dynamic>> ObterDadosGenericosAsync(FiltroConsulta filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro?.EntityName))
            {
                return new List<dynamic>();
            }

            string tabela = filtro.EntityName;

            // Monta a consulta SQL genérica para a tabela solicitada
            string sql = $"SELECT * FROM {tabela}";

            var parametros = new Dictionary<string, object>();

            return await _db.QueryAsync<dynamic>(sql, parametros);
        }
    }
}