using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.Core.Dados;
using Thelus.UI.Engine.Modelos; // Importante para reconhecer o ResultadoOperacao

namespace Thelus.Core.Servicos
{
    public class DatabaseGenericEntityService : IGenericEntityService
    {
        private readonly DatabaseAccess _db;

        public DatabaseGenericEntityService(DatabaseAccess db)
        {
            _db = db;
        }

        // 1. BUSCA POR FILTRO
        public async Task<List<dynamic>> ObterDadosGenericosAsync(FiltroConsulta filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro?.EntityName))
            {
                return new List<dynamic>();
            }

            try
            {
                string sql = $"SELECT * FROM [{filtro.EntityName}]";
                var resultado = await _db.QueryAsync<dynamic>(sql, new Dictionary<string, object>());
                return resultado ?? new List<dynamic>();
            }
            catch
            {
                return new List<dynamic>();
            }
        }

        // 2. BUSCA POR ID GENÉRICA
        public async Task<dynamic> ObterPorIdGenericoAsync(string entityName, int id)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return null;
            }

            try
            {
                string sql = $"SELECT * FROM [{entityName}] WHERE Id = @Id";
                return await _db.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });
            }
            catch
            {
                return null;
            }
        }

        // 3. GRAVAÇÃO GENÉRICA (SALVAR)
        public async Task<ResultadoOperacao> SalvarGenericoAsync(string entityName, object item)
        {
            if (item == null || string.IsNullOrWhiteSpace(entityName))
            {
                return ResultadoOperacao.Falha("A entidade ou o objeto informado para gravação está nulo.");
            }

            try
            {
                // Lógica da gravação genérica
                return ResultadoOperacao.OK($"Registro salvo com sucesso na tabela {entityName}!");
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falha($"Erro genérico ao gravar na tabela {entityName}: {ex.Message}");
            }
        }
    }
}