using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceSAP.ClassesWEBService;
using System.Text.RegularExpressions;
using VendasWeb.classes;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceCRM;
using Newtonsoft.Json;

namespace VendasWeb
{
    public class NegociacaoClasse : clsConexao
    {
        #region Propriedades do Filtro e Negócio

        public int? IDNegociacao { get; set; }
        public int? IDEmpresa { get; set; }
        public int? IDTabela { get; set; } = 146; // Valor padrão configurado na tabela
        public int? IDStatus { get; set; }
        public int? IDUsuarioSolicitante { get; set; }
        public int? IDPais { get; set; } = 30; // Valor padrão Brasil
        public int? IDEstado { get; set; }
        public int? IDMunicipio { get; set; }
        public int? IDCliente { get; set; }
        public int? IDVendedor { get; set; }
        public int? IDRegime { get; set; }
        public int? IDClassificacaoComercial { get; set; }
        public int? IDFreteNegociacao { get; set; }
        public int? IDValidadeNegociacao { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public bool ClienteNovo { get; set; }
        public string Cidade { get; set; }
        public string NomeCliente { get; set; }
        public string CondicaoPagamento { get; set; }
        public int IDItem { get; set; }
        public int IDProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal? QuantidadeConvertida { get; set; }
        public decimal ValorSimulador { get; set; }
        public decimal? ValorSimuladorM2 { get; set; }
        public decimal ValorSolicitado { get; set; }
        public decimal? ValorSolicitadoM2 { get; set; }
        public decimal PercentualDesconto { get; set; }

        // Filtros de Consulta
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Cliente { get; set; }

        #endregion

        #region Propriedades do Histórico
        public string Historico { get; set; }
        public int IDTipoHistorico { get; set; } = 10;     // Padrão: 10 (Negociação)
        public int IDEventoHistorico { get; set; } = 1;    // Padrão: 1 (Observações)
        public int IDCategoriaHistorico { get; set; } = 1; // Padrão: 1 (Criação)
        #endregion

        public NegociacaoClasse()
        {
        }

        public DataTable RetornaStatus()
        {
            DataTable dtStatus = new DataTable();
            int idTabela = 146;

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_GLOBAL_STATUS", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Adiciona o parâmetro esperado pela Stored Procedure
                        dbCommand.Parameters.Add("@IDTabela", SqlDbType.Int).Value = idTabela;

                        // Executa o comando e preenche o DataTable de forma otimizada
                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtStatus);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trate ou registre o erro conforme a necessidade da sua aplicação
                    throw new Exception("Erro ao buscar a lista de status: " + ex.Message, ex);
                }
            }

            return dtStatus;
        }

        public DataTable RetornaUsuarios()
        {
            DataTable dtUsuarios = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_USUARIOS", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtUsuarios);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar a lista de usuários: " + ex.Message, ex);
                }
            }

            return dtUsuarios;
        }

        public DataTable RetornaFretes()
        {
            DataTable dtFretes = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_TIPO_FRETE", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtFretes);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar a lista de tipos de frete: " + ex.Message, ex);
                }
            }

            return dtFretes;
        }

        public DataTable RetornaEstados()
        {
            DataTable dtEstados = new DataTable();

            // Na sua SP o parâmetro é varchar(10), então passamos como string
            string idPais = "30";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ESTADOS", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Adiciona apenas o parâmetro obrigatório esperado pela Stored Procedure
                        dbCommand.Parameters.Add("@IDPais", SqlDbType.VarChar).Value = idPais;

                        // Executa o comando e preenche o DataTable de forma otimizada
                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtEstados);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trate ou registre o erro conforme a necessidade da sua aplicação
                    throw new Exception("Erro ao buscar a lista de estados: " + ex.Message, ex);
                }
            }

            return dtEstados;
        }

        public DataTable RetornaMunicipios(string IDEstado)
        {
            DataTable dtMunicipios = new DataTable();

            // Na sua SP o parâmetro é varchar(10), então passamos como string
            string idPais = "30";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_MUNICIPIOS", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Adiciona apenas o parâmetro obrigatório esperado pela Stored Procedure
                        dbCommand.Parameters.Add("@IDPais", SqlDbType.VarChar).Value = idPais;
                        dbCommand.Parameters.Add("@IDEstado", SqlDbType.VarChar).Value = IDEstado;

                        // Executa o comando e preenche o DataTable de forma otimizada
                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtMunicipios);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trate ou registre o erro conforme a necessidade da sua aplicação
                    throw new Exception("Erro ao buscar a lista de municipios: " + ex.Message, ex);
                }
            }

            return dtMunicipios;
        }

        public DataTable RetornaRegimes()
        {
            DataTable dtRegimes = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_REGIME_TRIBUTARIO", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Não precisa adicionar parâmetros, pois a procedure não os espera

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtRegimes);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Corrigida a mensagem de erro para referenciar a lista correta
                    throw new Exception("Erro ao buscar a lista de regimes tributários: " + ex.Message, ex);
                }
            }

            return dtRegimes;
        }

        public DataTable RetornaVendedores()
        {
            DataTable dtVendedores = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_VENDEDORES", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Executa o comando e preenche o DataTable de forma otimizada
                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtVendedores);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mensagem corrigida para referenciar a lista correta
                    throw new Exception("Erro ao buscar a lista de vendedores: " + ex.Message, ex);
                }
            }

            return dtVendedores;
        }

        public DataTable RetornaClassificacaoComercial()
        {
            DataTable dtClassificacao = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_CLASSIFICACAO_COMERCIAL", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Executa o comando e preenche o DataTable de forma otimizada
                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtClassificacao);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mensagem corrigida para referenciar a lista de classificação comercial
                    throw new Exception("Erro ao buscar a lista de classificação comercial: " + ex.Message, ex);
                }
            }

            return dtClassificacao;
        }

        public DataTable RetornaValidades()
        {
            DataTable dtValidades = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_VALIDADE", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Executa o comando e preenche o DataTable de forma otimizada
                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtValidades);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mensagem consistente com o padrão
                    throw new Exception("Erro ao buscar a lista de validades: " + ex.Message, ex);
                }
            }

            return dtValidades;
        }

        public DataTable RetornaProdutos()
        {
            DataTable dtProdutos = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_PRODUTOS", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Executa o comando e preenche o DataTable de forma otimizada
                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtProdutos);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mensagem consistente com o padrão
                    throw new Exception("Erro ao buscar a lista de produtos: " + ex.Message, ex);
                }
            }

            return dtProdutos;
        }

        public DataTable RetornaClientesPaginado(string filtro, int pagina, int linhasPorPagina)
        {
            DataTable dtClientes = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_CLIENTE_LISTAR_PAGINADO", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Passa o filtro, a página atual e a quantidade de linhas por página
                        // Usamos NVarChar para alinhar com o NVARCHAR da Stored Procedure
                        dbCommand.Parameters.Add("@Filtro", SqlDbType.NVarChar).Value = (object)filtro ?? DBNull.Value;
                        dbCommand.Parameters.Add("@Pagina", SqlDbType.Int).Value = pagina;
                        dbCommand.Parameters.Add("@LinhasPorPagina", SqlDbType.Int).Value = linhasPorPagina;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtClientes);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mantendo o padrão de log de erro da sua aplicação
                    throw new Exception("Erro ao buscar a lista de clientes paginada: " + ex.Message, ex);
                }
            }

            return dtClientes;
        }

        public DataTable RetornaDadosCliente(string filtro, int pagina, int linhasPorPagina)
        {
            DataTable dtCliente = new DataTable();

            // Converte o filtro recebido (IDCliente) para inteiro
            int idCliente = 0;
            int.TryParse(filtro, out idCliente);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_CLIENTE_DETALHES", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Passa o ID do Cliente para a procedure de detalhes da negociação
                        dbCommand.Parameters.Add("@IDCliente", SqlDbType.Int).Value = idCliente;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtCliente);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar os detalhes do cliente: " + ex.Message, ex);
                }
            }

            return dtCliente;
        }

        /// <summary>
        /// Consulta as negociações aplicando os filtros configurados nas propriedades da classe.
        /// </summary>
        /// <returns>DataTable preenchido com os dados do Grid e da Modal</returns>
        public DataTable ConsultarNegociacoesGrid()
        {
            DataTable dtNegociacoes = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_CONSULTAR_GRID", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Mapeamento dos parâmetros com tratamento de nulos/zerados
                        dbCommand.Parameters.AddWithValue("@IDEmpresa", IDEmpresa.HasValue && IDEmpresa.Value > 0 ? (object)IDEmpresa.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDUsuarioSolicitante", IDUsuarioSolicitante.HasValue && IDUsuarioSolicitante.Value > 0 ? (object)IDUsuarioSolicitante.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDStatus", IDStatus.HasValue && IDStatus.Value > 0 ? (object)IDStatus.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@DataInicio", DataInicio.HasValue ? (object)DataInicio.Value.Date : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@DataFim", DataFim.HasValue ? (object)DataFim.Value.Date : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDNegociacao", IDNegociacao.HasValue && IDNegociacao.Value > 0 ? (object)IDNegociacao.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDFreteNegociacao", IDFreteNegociacao.HasValue && IDFreteNegociacao.Value > 0 ? (object)IDFreteNegociacao.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@Cliente", !string.IsNullOrWhiteSpace(Cliente) ? (object)Cliente.Trim() : DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtNegociacoes);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao consultar negociações: " + ex.Message, ex);
                }
            }

            return dtNegociacoes;
        }

        #region Métodos de Persistência

        /// <summary>
        /// Insere ou altera a negociação no banco utilizando as propriedades da classe.
        /// </summary>
        /// <returns>DataTable contendo as colunas Sucesso, Mensagem e o IDNegociacao gerado/atualizado</returns>
        public DataTable Gravar()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_GRAVAR", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Mapeamento dos parâmetros utilizando as propriedades da própria instância
                        dbCommand.Parameters.AddWithValue("@IDNegociacao", IDNegociacao.HasValue && IDNegociacao.Value > 0 ? (object)IDNegociacao.Value : 0);
                        dbCommand.Parameters.AddWithValue("@IDEmpresa", IDEmpresa.HasValue && IDEmpresa.Value > 0 ? (object)IDEmpresa.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDTabela", IDTabela.HasValue && IDTabela.Value > 0 ? (object)IDTabela.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDStatus", IDStatus.HasValue && IDStatus.Value > 0 ? (object)IDStatus.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDUsuarioSolicitante", IDUsuarioSolicitante.HasValue && IDUsuarioSolicitante.Value > 0 ? (object)IDUsuarioSolicitante.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDPais", IDPais.HasValue && IDPais.Value > 0 ? (object)IDPais.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDEstado", IDEstado.HasValue && IDEstado.Value > 0 ? (object)IDEstado.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDMunicipio", IDMunicipio.HasValue && IDMunicipio.Value > 0 ? (object)IDMunicipio.Value : DBNull.Value);

                        // Regra do Cliente Novo: Se for ClienteNovo = true, envia DBNull para o IDCliente
                        if (ClienteNovo)
                        {
                            dbCommand.Parameters.AddWithValue("@IDCliente", DBNull.Value);
                        }
                        else
                        {
                            dbCommand.Parameters.AddWithValue("@IDCliente", IDCliente.HasValue && IDCliente.Value > 0 ? (object)IDCliente.Value : DBNull.Value);
                        }

                        dbCommand.Parameters.AddWithValue("@IDVendedor", IDVendedor.HasValue && IDVendedor.Value > 0 ? (object)IDVendedor.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDRegime", IDRegime.HasValue && IDRegime.Value > 0 ? (object)IDRegime.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDClassificacaoComercial", IDClassificacaoComercial.HasValue && IDClassificacaoComercial.Value > 0 ? (object)IDClassificacaoComercial.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDFreteNegociacao", IDFreteNegociacao.HasValue && IDFreteNegociacao.Value > 0 ? (object)IDFreteNegociacao.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDValidadeNegociacao", IDValidadeNegociacao.HasValue && IDValidadeNegociacao.Value > 0 ? (object)IDValidadeNegociacao.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@DataSolicitacao", DataSolicitacao.HasValue ? (object)DataSolicitacao.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@ClienteNovo", ClienteNovo);
                        dbCommand.Parameters.AddWithValue("@Cidade", !string.IsNullOrWhiteSpace(Cidade) ? (object)Cidade.Trim() : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@NomeCliente", !string.IsNullOrWhiteSpace(NomeCliente) ? (object)NomeCliente.Trim() : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@CondicaoPagamento", !string.IsNullOrWhiteSpace(CondicaoPagamento) ? (object)CondicaoPagamento.Trim() : DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao gravar a negociação: " + ex.Message, ex);
                }
            }

            return dtResultado;
        }

        #endregion

        /// <summary>
        /// Grava um novo registro de histórico utilizando as propriedades configuradas na classe.
        /// </summary>
        /// <param name="idNegociacao">ID da Negociação associada</param>
        /// <returns>DataTable com o resultado da operação e o IDHistorico gerado</returns>
        public DataTable GravarHistorico(int idNegociacao)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_HISTORICO_GRAVAR", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Mapeamento dinâmico utilizando os atributos da classe
                        dbCommand.Parameters.AddWithValue("@IDEmpresa", IDEmpresa.HasValue && IDEmpresa.Value > 0 ? (object)IDEmpresa.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDNegociacao", idNegociacao);
                        dbCommand.Parameters.AddWithValue("@IDUsuario", IDUsuarioSolicitante.HasValue && IDUsuarioSolicitante.Value > 0 ? (object)IDUsuarioSolicitante.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@Historico", !string.IsNullOrWhiteSpace(Historico) ? (object)Historico.Trim() : DBNull.Value);

                        // Passa os valores das propriedades da classe (flexível para outros eventos)
                        dbCommand.Parameters.AddWithValue("@IDTipo", IDTipoHistorico);
                        dbCommand.Parameters.AddWithValue("@IDEvento", IDEventoHistorico);
                        dbCommand.Parameters.AddWithValue("@IDCategoria", IDCategoriaHistorico);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao gravar histórico da negociação: " + ex.Message, ex);
                }
            }

            return dtResultado;
        }

        /// <summary>
        /// Busca os dados completos de uma negociação específica pela chave composta (IDEmpresa + IDNegociacao).
        /// </summary>
        public bool CarregarNegociacaoPorID(int idEmpresa, int idNegociacao)
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_OBTER_POR_ID", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.AddWithValue("@IDEmpresa", idEmpresa);
                        dbCommand.Parameters.AddWithValue("@IDNegociacao", idNegociacao);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];

                                this.IDEmpresa = Convert.ToInt32(dr["IDEmpresa"]);
                                this.IDNegociacao = Convert.ToInt32(dr["IDNegociacao"]);
                                this.IDStatus = dr["IDStatus"] != DBNull.Value ? Convert.ToInt32(dr["IDStatus"]) : (int?)null;
                                this.IDUsuarioSolicitante = dr["IDUsuarioSolicitante"] != DBNull.Value ? Convert.ToInt32(dr["IDUsuarioSolicitante"]) : (int?)null;
                                this.IDEstado = dr["IDEstado"] != DBNull.Value ? Convert.ToInt32(dr["IDEstado"]) : (int?)null;
                                this.IDMunicipio = dr["IDMunicipio"] != DBNull.Value ? Convert.ToInt32(dr["IDMunicipio"]) : (int?)null;
                                this.IDCliente = dr["IDCliente"] != DBNull.Value ? Convert.ToInt32(dr["IDCliente"]) : (int?)null;
                                this.IDVendedor = dr["IDVendedor"] != DBNull.Value ? Convert.ToInt32(dr["IDVendedor"]) : (int?)null;
                                this.IDRegime = dr["IDRegime"] != DBNull.Value ? Convert.ToInt32(dr["IDRegime"]) : (int?)null;
                                this.IDClassificacaoComercial = dr["IDClassificacaoComercial"] != DBNull.Value ? Convert.ToInt32(dr["IDClassificacaoComercial"]) : (int?)null;
                                this.IDFreteNegociacao = dr["IDFreteNegociacao"] != DBNull.Value ? Convert.ToInt32(dr["IDFreteNegociacao"]) : (int?)null;
                                this.IDValidadeNegociacao = dr["IDValidadeNegociacao"] != DBNull.Value ? Convert.ToInt32(dr["IDValidadeNegociacao"]) : (int?)null;

                                this.ClienteNovo = dr["ClienteNovo"] != DBNull.Value && Convert.ToBoolean(dr["ClienteNovo"]);
                                this.NomeCliente = dr["NomeCliente"] != DBNull.Value ? dr["NomeCliente"].ToString() : string.Empty;
                                this.Cidade = dr["Cidade"] != DBNull.Value ? dr["Cidade"].ToString() : string.Empty;
                                this.CondicaoPagamento = dr["CondicaoPagamento"] != DBNull.Value ? dr["CondicaoPagamento"].ToString() : string.Empty;

                                if (dr["DataSolicitacao"] != DBNull.Value)
                                    this.DataSolicitacao = Convert.ToDateTime(dr["DataSolicitacao"]);

                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao obter dados da negociação: " + ex.Message, ex);
                }
            }

            return false;
        }

        #region Métodos de Histórico

        /// <summary>
        /// Retorna o DataTable bruto contendo o histórico e metadados de design.
        /// </summary>
        public DataTable RetornaHistoricoNegociacao(int idEmpresa, int idNegociacao)
        {
            DataTable dtHistorico = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_NEGOCIACAO", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.AddWithValue("@IDEmpresa", idEmpresa);
                        dbCommand.Parameters.AddWithValue("@IDNegociacao", idNegociacao);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtHistorico);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar o histórico da negociação: " + ex.Message, ex);
                }
            }

            return dtHistorico;
        }

        /// <summary>
        /// Percorre o histórico e formata o conteúdo em um único bloco de texto concatenado e organizado.
        /// </summary>
        public string ObterHistoricoFormatadoTexto(int idEmpresa, int idNegociacao)
        {
            DataTable dt = RetornaHistoricoNegociacao(idEmpresa, idNegociacao);

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {
                string usuario = dr["NomeUsuario"] != DBNull.Value ? dr["NomeUsuario"].ToString() : dr["CodigoUsuario"].ToString();
                string data = dr["DataHistorico"] != DBNull.Value ? Convert.ToDateTime(dr["DataHistorico"]).ToString("dd/MM/yyyy HH:mm") : "";
                string evento = dr["DescricaoEvento"].ToString();
                string categoria = dr["DescricaoCategoria"].ToString();
                string texto = dr["Historico"].ToString();

                sb.AppendLine($"[ {data} - {usuario} | {evento} ({categoria}) ]");
                sb.AppendLine(texto);
                sb.AppendLine("----------------------------------------------------------------------------------");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// Consulta o histórico de faturamento do cliente no SAP Business One.
        /// </summary>
        public string ObterHistoricoFaturamentoSAP(string IDCliente)
        {
            string mensagemFaturamento = "Cliente sem Histórico de faturamento.";

            if (string.IsNullOrWhiteSpace(IDCliente))
                return mensagemFaturamento;

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_OBTER_HISTORICO_FATURAMENTO_SAP", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.AddWithValue("@IDCliente", IDCliente.Trim());

                        dbConnection.Open();
                        object result = dbCommand.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            mensagemFaturamento = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Em caso de erro de conexão com a base SAP, não trava a gravação da negociação
                    mensagemFaturamento = "Cliente sem Histórico de faturamento. (Falha ao consultar SAP: " + ex.Message + ")";
                }
            }

            return mensagemFaturamento;
        }

        public DataTable GravarItem()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_ITENS_GRAVAR", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Mapeamento utilizando os atributos da própria classe
                        dbCommand.Parameters.AddWithValue("@IDEmpresa", IDEmpresa.HasValue && IDEmpresa.Value > 0 ? (object)IDEmpresa.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDNegociacao", IDNegociacao.HasValue && IDNegociacao.Value > 0 ? (object)IDNegociacao.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDItem", IDItem > 0 ? (object)IDItem : 0);
                        dbCommand.Parameters.AddWithValue("@IDProduto", IDProduto > 0 ? (object)IDProduto : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@Quantidade", Quantidade);
                        dbCommand.Parameters.AddWithValue("@QuantidadeConvertida", QuantidadeConvertida.HasValue ? (object)QuantidadeConvertida.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@ValorSimulador", ValorSimulador);
                        dbCommand.Parameters.AddWithValue("@ValorSimuladorM2", ValorSimuladorM2.HasValue ? (object)ValorSimuladorM2.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@ValorSolicitado", ValorSolicitado);
                        dbCommand.Parameters.AddWithValue("@ValorSolicitadoM2", ValorSolicitadoM2.HasValue ? (object)ValorSolicitadoM2.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@PercentualDesconto", PercentualDesconto);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao gravar item da negociação: " + ex.Message, ex);
                }
            }

            return dtResultado;
        }

        public DataTable ExcluirItem()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_ITENS_EXCLUIR", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // Mapeamento dos parâmetros utilizando as propriedades da instância ativa
                        dbCommand.Parameters.AddWithValue("@IDEmpresa", IDEmpresa.HasValue && IDEmpresa.Value > 0 ? (object)IDEmpresa.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDNegociacao", IDNegociacao.HasValue && IDNegociacao.Value > 0 ? (object)IDNegociacao.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDItem", IDItem > 0 ? (object)IDItem : DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao excluir item da negociação: " + ex.Message, ex);
                }
            }

            return dtResultado;
        }

        /// <summary>
        /// Retorna os itens vinculados à negociação ativa para alimentar a GridView.
        /// </summary>
        public DataTable RetornaItensNegociacao()
        {
            DataTable dtItens = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("CRM_SP_NEGOCIACAO_ITENS_CONSULTAR", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.AddWithValue("@IDEmpresa", IDEmpresa.HasValue && IDEmpresa.Value > 0 ? (object)IDEmpresa.Value : DBNull.Value);
                        dbCommand.Parameters.AddWithValue("@IDNegociacao", IDNegociacao.HasValue && IDNegociacao.Value > 0 ? (object)IDNegociacao.Value : DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                        {
                            adapter.Fill(dtItens);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao consultar os itens da negociação: " + ex.Message, ex);
                }
            }

            return dtItens;
        }

    }

}
