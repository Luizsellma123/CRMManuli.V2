using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.Core.Config;
using Thelus.Core.Dados;
using Thelus.UI.Engine.Modelos;

namespace Thelus.Core.Servicos
{
    public class GlobalServico : IEntityService
    {
        private readonly DatabaseAccess _db;

        // Identificador da entidade no motor da Engine
        public string EntityName => "global";

        public GlobalServico()
        {
            try
            {
                _db = new DatabaseAccess();
            }
            catch
            {
                _db = null;
            }
        }

        public async Task<List<dynamic>> ObterListagemAsync(FiltroConsulta filtro = null)
        {
            string recurso = filtro?.EntityName?.ToLower() ?? "";

            // 1. STATUS GLOBAIS / TABELAS DE SISTEMA (Exige explicitamente "global-status-{id}")
            if (recurso.StartsWith("global-status-"))
            {
                var partes = recurso.Split('-');
                // Pega a última parte que traz o ID da tabela (ex: global-status-146 -> "146")
                if (partes.Length == 3 && int.TryParse(partes[2], out int idTabela))
                {
                    string sqlStatus = @"
                SELECT 
                    IDStatus  AS Id, 
                    Descricao AS Descricao 
                FROM CRM_STATUS 
                WHERE IDTabela = @IdTabela AND Ativo = 1 
                ORDER BY Descricao";

                    return await ExecutarConsultaAsync(sqlStatus, new { IdTabela = idTabela }, "status", idTabela);
                }
            }

            // 2. SWITCH PARA LOOKUPS GLOBAIS DIVERSOS (Demais recursos do GlobalServico)
            // Remove o prefixo "global-" se presente (ex: "global-tipos-frete" -> "tipos-frete")
            string chaveLimpa = recurso.StartsWith("global-") ? recurso.Substring(7) : recurso;

            string sql = chaveLimpa switch
            {
                "log-operacoes" => @"
            SELECT 
                IDLog AS Id, 
                DescricaoOperacao AS Descricao 
            FROM CRM_LOG_TIPO 
            ORDER BY DescricaoOperacao",

                "tipos-frete" => @"
            SELECT 
                Codigo AS Id, 
                Descricao AS Descricao 
            FROM CRM_TIPO_FRETE 
            ORDER BY Descricao",

                "regimes-tributarios" => @"
            SELECT 
                IDRegime AS Id, 
                Descricao AS Descricao 
            FROM CRM_REGIME_TRIBUTARIO 
            ORDER BY Descricao",

                _ => null
            };

            if (!string.IsNullOrEmpty(sql))
            {
                return await ExecutarConsultaAsync(sql, null, chaveLimpa);
            }

            return new List<dynamic>();
        }

        private async Task<List<dynamic>> ExecutarConsultaAsync(string sql, object parametros = null, string tipoRecurso = "", int? extraId = null)
        {
            try
            {
                if (_db != null)
                {
                    var dados = await _db.QueryAsync<dynamic>(sql, parametros);
                    if (dados != null && dados.Count > 0) return dados;
                }
            }
            catch (Exception ex)
            {
                // Tratamento silencioso de exceção de conexão/query
            }

            // Fallback de Contingência / Mock para desenvolvimento
            return TratarFallback(tipoRecurso, extraId);
        }

        private List<dynamic> TratarFallback(string recurso, int? extraId = null)
        {
            if (recurso == "status")
            {
                return new List<dynamic>
                {
                    new { Id = 1, Descricao = "Aberto (Mock)" },
                    new { Id = 2, Descricao = "Pendente (Mock)" },
                    new { Id = 3, Descricao = "Aprovado (Mock)" }
                };
            }

            return recurso switch
            {
                "log-operacoes" => new List<dynamic> { new { Id = 1, Descricao = "Inclusão de Registro" } },
                "tipos-frete" => new List<dynamic> { new { Id = "CIF", Descricao = "CIF" }, new { Id = "FOB", Descricao = "FOB" } },
                _ => new List<dynamic>()
            };
        }

        public async Task<object> ObterPorIdAsync(int id) => null;
        public async Task<ResultadoOperacao> SalvarAsync(object item) => ResultadoOperacao.Falha("Operação não permitida.");
    }
}