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
        #region Propriedades do Filtro de Consulta

        public int? IDEmpresa { get; set; }
        public int? IDUsuarioSolicitante { get; set; }
        public int? IDStatus { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int? IDNegociacao { get; set; }
        public int? IDFreteNegociacao { get; set; }
        public string Cliente { get; set; }

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
    }
}
