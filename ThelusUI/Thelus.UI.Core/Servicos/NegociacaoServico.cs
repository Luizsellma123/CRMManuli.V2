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
    public class NegociacaoServico : IEntityService
    {
        private readonly DatabaseAccess _db;

        public NegociacaoServico()
        {
            try { _db = new DatabaseAccess(); } catch { _db = null; }
        }

        // Identificador principal da entidade
        public string EntityName => "negociacao";

        // 1. LISTAR REGISTROS OU ATENDER LOOKUPS DA ENTIDADE
        public async Task<List<dynamic>> ObterListagemAsync(FiltroConsulta filtro = null)
        {
            string recurso = filtro?.EntityName?.ToLower() ?? "";

            return recurso switch
            {
                "negociacao-usuarios" => await ObterUsuariosLookupAsync(),
                "negociacao-frete" => ObterFretesLookup(),
                "negociacao-clientes" => await ObterClientesLookupAsync(filtro),
                _ => await ObterNegociacoesAsync()
            };
        }

        private async Task<List<dynamic>> ObterUsuariosLookupAsync()
        {
            try
            {
                string connStr = ConfigurationManager.GetConnectionString();
                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    string sqlUsers = @"
                        SELECT 
                            IDUsuario AS Id, 
                            CASE 
                                WHEN Nome IS NULL OR LTRIM(RTRIM(CAST(Nome AS VARCHAR(MAX)))) = '' THEN CodigoUsuario
                                ELSE CAST(Nome AS VARCHAR(MAX))
                            END AS Descricao 
                        FROM CRM_CADASTRO_USUARIO 
                        ORDER BY 
                            CASE 
                                WHEN Nome IS NULL OR LTRIM(RTRIM(CAST(Nome AS VARCHAR(MAX)))) = '' THEN CodigoUsuario
                                ELSE CAST(Nome AS VARCHAR(MAX))
                            END";

                    var usuarios = await _db.QueryAsync<dynamic>(sqlUsers);
                    if (usuarios != null && usuarios.Count > 0) return usuarios;
                }
            }
            catch { }

            return new List<dynamic>
            {
                new { Id = "LUIZ", Descricao = "Luiz Carlos" },
                new { Id = "TODOS", Descricao = "Luiz/Todos" }
            };
        }

        private List<dynamic> ObterFretesLookup()
        {
            return new List<dynamic>
            {
                new { Id = "1", Descricao = "CIF" },
                new { Id = "2", Descricao = "FOB" },
                new { Id = "3", Descricao = "CIF até SP" }
            };
        }

        private async Task<List<dynamic>> ObterClientesLookupAsync(FiltroConsulta filtro = null)
        {
            try
            {
                string connStr = ConfigurationManager.GetConnectionString();
                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    string termo = filtro?.TermoBusca ?? string.Empty;

                    string sqlClientes = @"
                        SELECT TOP 50
                            IDCliente AS Id, 
                            RTRIM(LTRIM(ISNULL(CodigoClienteSAP, ''))) + ' - ' + NomeCliente + ' (CNPJ: ' + ISNULL(CNPJ, '') + ')' AS Descricao
                        FROM CRM_CLIENTE 
                        WHERE TipoCliente = 'C'";

                    var parametros = new Dictionary<string, object>();

                    if (!string.IsNullOrWhiteSpace(termo))
                    {
                        sqlClientes += " AND (NomeCliente LIKE @Termo OR CodigoClienteSAP LIKE @Termo OR CNPJ LIKE @Termo)";
                        parametros["Termo"] = $"%{termo}%";
                    }

                    sqlClientes += " ORDER BY Id";

                    var clientes = await _db.QueryAsync<dynamic>(sqlClientes, parametros);
                    if (clientes != null && clientes.Count > 0) return clientes;
                }
            }
            catch { }

            return new List<dynamic>();
        }

        private async Task<List<dynamic>> ObterNegociacoesAsync()
        {
            try
            {
                string connStr = ConfigurationManager.GetConnectionString();
                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    string sql = @"
                        SELECT 
                            n.IdNegociacao          AS IdNegociacao,
                            n.IdEmpresa             AS IdEmpresa,
                            n.Solicitante           AS Solicitante,
                            n.IdSituacao            AS IdSituacao,
                            n.Data                  AS Data,
                            n.DataInicio            AS DataInicio,
                            n.DataFim               AS DataFim,
                            n.Estado                AS Estado,
                            n.Cidade                AS Cidade,
                            n.IsNovo                AS IsNovo,
                            n.Cliente               AS Cliente,
                            n.CondicaoPagamento     AS CondicaoPagamento,
                            n.Vendedor              AS Vendedor,
                            n.Regime                AS Regime,
                            n.ClassificacaoComercial AS ClassificacaoComercial,
                            n.Frete                 AS Frete,
                            n.Validade              AS Validade,
                            n.Observacao            AS Observacao,
                            n.Historico             AS Historico
                        FROM CRM_NEGOCIACAO n
                        ORDER BY n.IdNegociacao DESC";

                    List<Negociacao> dados = await _db.QueryAsync<Negociacao>(sql);
                    if (dados != null && dados.Count > 0)
                    {
                        return dados.Cast<dynamic>().ToList();
                    }
                }
            }
            catch { }

            var mockList = new List<Negociacao>
            {
                new Negociacao { IdNegociacao = 1, IdEmpresa = 1, Solicitante = "Luiz Carlos", IdSituacao = 1, Cliente = "CLI0017804 - AFVAL - GESTAO DE RESIDUOS RECICLAVEIS LTDA", Data = DateTime.Now, Frete = "CIF" },
                new Negociacao { IdNegociacao = 2, IdEmpresa = 1, Solicitante = "Luiz Carlos", IdSituacao = 2, Cliente = "CLI0017804 - AFVAL - GESTAO DE RESIDUOS RECICLAVEIS LTDA", Data = DateTime.Now, Frete = "FOB" }
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
                            n.IdNegociacao, n.IdEmpresa, n.Solicitante, n.IdSituacao, 
                            n.Data, n.DataInicio, n.DataFim, n.Estado, n.Cidade, n.IsNovo, 
                            n.Cliente, n.CondicaoPagamento, n.Vendedor, n.Regime, 
                            n.ClassificacaoComercial, n.Frete, n.Validade, n.Observacao, n.Historico
                        FROM CRM_NEGOCIACAO n
                        WHERE n.IdNegociacao = @Id";

                    var negociacao = await _db.QueryFirstOrDefaultAsync<Negociacao>(sql, new { Id = id });
                    if (negociacao != null) return negociacao;
                }
            }
            catch { }

            var listagem = await ObterListagemAsync();
            return listagem.FirstOrDefault(x => x.IdNegociacao == id);
        }

        // 3. GRAVAR / SALVAR REGISTRO
        public async Task<ResultadoOperacao> SalvarAsync(object item)
        {
            if (item == null)
            {
                return ResultadoOperacao.Falha("Nenhum dado foi fornecido para a gravação.");
            }

            Negociacao negociacao = null;

            if (item is Negociacao n)
            {
                negociacao = n;
            }
            else if (item is JsonElement jsonElem)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                negociacao = JsonSerializer.Deserialize<Negociacao>(jsonElem.GetRawText(), options);
            }

            if (negociacao == null)
            {
                return ResultadoOperacao.Falha("Os dados da negociação estão em um formato inválido.");
            }

            if (string.IsNullOrWhiteSpace(negociacao.Cliente))
            {
                return ResultadoOperacao.Falha("O campo Cliente é obrigatório.");
            }

            if (negociacao.IdEmpresa <= 0)
            {
                return ResultadoOperacao.Falha("Selecione uma Empresa válida.");
            }

            if (negociacao.IdSituacao <= 0)
            {
                return ResultadoOperacao.Falha("Selecione uma Situação válida.");
            }

            try
            {
                string connStr = ConfigurationManager.GetConnectionString();

                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    if (negociacao.IdNegociacao == 0)
                    {
                        string sqlInsert = @"
                            INSERT INTO CRM_NEGOCIACAO 
                                (IdEmpresa, Solicitante, IdSituacao, Data, DataInicio, DataFim, Estado, Cidade, IsNovo, 
                                 Cliente, CondicaoPagamento, Vendedor, Regime, ClassificacaoComercial, Frete, Validade, Observacao, Historico)
                            VALUES 
                                (@IdEmpresa, @Solicitante, @IdSituacao, GETDATE(), @DataInicio, @DataFim, @Estado, @Cidade, @IsNovo, 
                                 @Cliente, @CondicaoPagamento, @Vendedor, @Regime, @ClassificacaoComercial, @Frete, @Validade, @Observacao, @Historico);";

                        int linhasAfetadas = await _db.ExecuteAsync(sqlInsert, negociacao);

                        return linhasAfetadas > 0
                            ? ResultadoOperacao.OK("Negociação cadastrada com sucesso!")
                            : ResultadoOperacao.Falha("Não foi possível cadastrar a negociação. Nenhuma linha foi afetada.");
                    }
                    else
                    {
                        string sqlUpdate = @"
                            UPDATE CRM_NEGOCIACAO SET 
                                IdEmpresa = @IdEmpresa, 
                                Solicitante = @Solicitante, 
                                IdSituacao = @IdSituacao, 
                                DataInicio = @DataInicio, 
                                DataFim = @DataFim, 
                                Estado = @Estado, 
                                Cidade = @Cidade, 
                                IsNovo = @IsNovo, 
                                Cliente = @Cliente, 
                                CondicaoPagamento = @CondicaoPagamento, 
                                Vendedor = @Vendedor, 
                                Regime = @Regime, 
                                ClassificacaoComercial = @ClassificacaoComercial, 
                                Frete = @Frete, 
                                Validade = @Validade, 
                                Observacao = @Observacao, 
                                Historico = @Historico
                            WHERE IdNegociacao = @IdNegociacao;";

                        int linhasAfetadas = await _db.ExecuteAsync(sqlUpdate, negociacao);

                        return linhasAfetadas > 0
                            ? ResultadoOperacao.OK("Negociação atualizada com sucesso!")
                            : ResultadoOperacao.Falha("Nenhum registro de negociação foi alterado.");
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