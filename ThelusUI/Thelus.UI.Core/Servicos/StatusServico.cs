using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Thelus.Core.Config;
using Thelus.Core.Dados;
using Thelus.UI.Engine.Modelos;
using Thelus.UI.Model.Entidades;

namespace Thelus.Core.Servicos
{
    public class StatusServico : IEntityService
    {
        private readonly DatabaseAccess _db;

        public StatusServico()
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

        // Identificador correspondente à LookupKey usada nos campos (ex: "Status")
        public string EntityName => "status";

        // 1. LISTAR REGISTROS COM QUERY CUSTOMIZADA
        public async Task<List<dynamic>> ObterListagemAsync(FiltroConsulta filtro = null)
        {
            try
            {
                string connStr = ConfigurationManager.GetConnectionString();

                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    // Query customizada filtrando apenas os status da tabela de contexto (ex: IDTabela = 33)
                    string sql = @"
                        SELECT 
                            IdStatus    AS IdStatus,
                            Descricao   AS Descricao,
                            IDTabela    AS IDTabela
                        FROM [STATUS]
                        WHERE IDTabela = 33";

                    List<StatusModel> dados = await _db.QueryAsync<StatusModel>(sql);

                    if (dados != null && dados.Count > 0)
                    {
                        return dados.Cast<dynamic>().ToList();
                    }
                }
            }
            catch
            {
                // Fallback em caso de indisponibilidade de banco
            }

            // FALLBACK / MOCK
            var mockList = new List<StatusModel>
            {
                new StatusModel { IdStatus = 1, Descricao = "Ativo", IDTabela = 33 },
                new StatusModel { IdStatus = 2, Descricao = "Inativo", IDTabela = 33 }
            };

            return mockList.Cast<dynamic>().ToList();
        }

        // 2. OBTER REGISTRO POR ID
        public async Task<dynamic> ObterPorIdAsync(int id)
        {
            try
            {
                string connStr = ConfigurationManager.GetConnectionString();

                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    string sql = @"
                        SELECT 
                            IdStatus, Descricao, IDTabela 
                        FROM [STATUS] 
                        WHERE IdStatus = @Id AND IDTabela = 33";

                    var status = await _db.QueryFirstOrDefaultAsync<StatusModel>(sql, new { Id = id });
                    if (status != null) return status;
                }
            }
            catch
            {
                // Tratamento de exceção de conexão
            }

            var listagem = await ObterListagemAsync();
            return listagem.FirstOrDefault(x => ((StatusModel)x).IdStatus == id);
        }

        // 3. GRAVAR / SALVAR REGISTRO
        public async Task<ResultadoOperacao> SalvarAsync(object item)
        {
            if (item == null)
            {
                return ResultadoOperacao.Falha("Nenhum dado foi fornecido para a gravação.");
            }

            StatusModel status = null;

            if (item is StatusModel s)
            {
                status = s;
            }
            else if (item is JsonElement jsonElem)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                status = JsonSerializer.Deserialize<StatusModel>(jsonElem.GetRawText(), options);
            }

            if (status == null)
            {
                return ResultadoOperacao.Falha("Os dados do status estão em um formato inválido.");
            }

            if (string.IsNullOrWhiteSpace(status.Descricao))
            {
                return ResultadoOperacao.Falha("O campo Descrição é obrigatório.");
            }

            try
            {
                string connStr = ConfigurationManager.GetConnectionString();

                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    // Garante que o registro pertença ao contexto correto
                    status.IDTabela = 33;

                    if (status.IdStatus == 0)
                    {
                        string sqlInsert = @"
                            INSERT INTO [STATUS] (Descricao, IDTabela)
                            VALUES (@Descricao, @IDTabela);";

                        int linhasAfetadas = await _db.ExecuteAsync(sqlInsert, status);

                        return linhasAfetadas > 0
                            ? ResultadoOperacao.OK("Status cadastrado com sucesso!")
                            : ResultadoOperacao.Falha("Não foi possível cadastrar o status.");
                    }
                    else
                    {
                        string sqlUpdate = @"
                            UPDATE [STATUS] SET 
                                Descricao = @Descricao, 
                                IDTabela = @IDTabela
                            WHERE IdStatus = @IdStatus;";

                        int linhasAfetadas = await _db.ExecuteAsync(sqlUpdate, status);

                        return linhasAfetadas > 0
                            ? ResultadoOperacao.OK("Status atualizado com sucesso!")
                            : ResultadoOperacao.Falha("Nenhum registro de status foi alterado.");
                    }
                }

                return ResultadoOperacao.Falha("Conexão com o banco de dados não configurada.");
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falha($"Erro ao salvar no banco de dados: {ex.Message}");
            }
        }
    }
}