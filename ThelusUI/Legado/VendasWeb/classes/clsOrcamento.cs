using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;
using VendasWeb.AprovarOrcamento;

namespace VendasWeb
{
    public class clsOrcamento : clsConexao

    {


        public string UsuCod { get; set; }
        public string EmpCod { get; set; }
        public string EmpNome { get; set; }
        public string EmpCodConsulta { get; set; }
        public string PedVendaNum { get; set; }
        public string PedVendaNumConsulta { get; set; }
        public string Situacao { get; set; }
        public string EntCode { get; set; }
        public string EntCod { get; set; }
        public string Entidade { get; set; }
        public string EntNome { get; set; }
        public string EntCpfCgc { get; set; }
        public string EntNat { get; set; }
        public string VendCod { get; set; }
        public string VendNome { get; set; }
        public string UfSigla { get; set; }
        public string Alcada { get; set; }
        public string AlcadaPrincipal { get; set; }
        public string Historico { get; set; }
        public string DataPrevisao { get; set; }
        public string OutrosDados { get; set; }
        public string Concluido { get; set; }
        public string PedVendaStatDescr { get; set; }


        public string AprovadoPrincipal { get; set; }
        public string StatusLogisitica { get; set; }
        public string PagadorFrete { get; set; }


        public string AprovadoSupervisor { get; set; }
        public string AprovadoRegional { get; set; }
        public string AprovadoDiretoria { get; set; }
        public string AprovadoControladoria { get; set; }
        public string RetornaVendedor { get; set; }
        public double TotalPedido { get; set; }
        public string quantidadeVolumes { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal PercentualFrete { get; set; }
        public decimal pesoBruto { get; set; }
        public string transportadora { get; set; }
        public string NomeTransportador { get; set; }
        public string LocalEmbarque { get; set; }
        public string CondicaoPagamento { get; set; }
        public string NaturezaOperacao { get; set; }
        public string Cidade { get; set; }
        public string PrazoMedio { get; set; }
        public string HistoricoPedido { get; set; }
        public string InscricaoEstadual { get; set; }
        public string EnquadramentoTirbutario { get; set; }

        //Campos SAp
        public string NumeroEsbocoSAP { get; set; }

        public string DataInicial { get; set; }
        public string DataFinal { get; set; }


        public DataTable Consulta_Liberacoes_Orcamento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_Liberacoes_Pedidos", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Entidade", SqlDbType.VarChar, 8000, "Entidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Situacao", SqlDbType.VarChar, 150, "Situacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Concluido", SqlDbType.VarChar, 20, "Concluido"));

                    dbCommand.Parameters.Add(new SqlParameter("@AprovadoPrincipal", SqlDbType.VarChar, 20, "AprovadoPrincipal"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@Alcada", SqlDbType.VarChar, 500, "Alcada"));

                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 20, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 20, "DataFinal"));


                    dbCommand.Parameters["@UsuCod"].Value = this.UsuCod;
                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCodConsulta;
                    dbCommand.Parameters["@Entidade"].Value = this.Entidade;
                    dbCommand.Parameters["@Situacao"].Value = this.Situacao;
                    dbCommand.Parameters["@Concluido"].Value = this.Concluido;

                    dbCommand.Parameters["@AprovadoPrincipal"].Value = this.AprovadoPrincipal;
                    dbCommand.Parameters["@PedVendaNum"].Value = this.PedVendaNumConsulta;
                    dbCommand.Parameters["@Alcada"].Value = this.Alcada;

                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;

        }

        public DataTable Consulta_Liberacoes_Orcamento_Logisitica()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_CONSULTA_CRM_Liberacoes_Pedidos_Logistica", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Entidade", SqlDbType.VarChar, 8000, "Entidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Situacao", SqlDbType.VarChar, 150, "Situacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Concluido", SqlDbType.VarChar, 20, "Concluido"));

                    dbCommand.Parameters.Add(new SqlParameter("@AprovadoPrincipal", SqlDbType.VarChar, 20, "AprovadoPrincipal"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));


                    dbCommand.Parameters["@UsuCod"].Value = this.UsuCod;
                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCodConsulta;
                    dbCommand.Parameters["@Entidade"].Value = this.Entidade;
                    dbCommand.Parameters["@Situacao"].Value = this.Situacao;
                    dbCommand.Parameters["@Concluido"].Value = this.Concluido;

                    dbCommand.Parameters["@AprovadoPrincipal"].Value = this.AprovadoPrincipal;
                    dbCommand.Parameters["@PedVendaNum"].Value = this.PedVendaNumConsulta;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;

        }


        public string Mostra_Liberacoes_Orcamento()
        {
            string Retorno = "";


            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_MOSTRA_CRM_Liberacoes_Pedidos", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EmpCod"].Value = EmpCod;
                    dbCommand.Parameters["@PedVendaNum"].Value = PedVendaNum;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            EmpCod = row["EmpCod"].ToString();
                            EmpNome = row["EmpNome"].ToString();
                            PedVendaNum = row["PedVendaNum"].ToString();
                            UfSigla = row["UfSigla"].ToString();
                            PedVendaStatDescr = row["PedVendaStatDescr"].ToString();
                            DataPrevisao = row["DataPrevisao"].ToString();
                            OutrosDados = row["OutrosDados"].ToString();
                            Concluido = row["Concluido"].ToString();
                            Situacao = row["Situacao"].ToString();
                            AprovadoSupervisor = row["AprovadoSupervisor"].ToString();
                            AprovadoRegional = row["AprovadoRegional"].ToString();
                            AprovadoDiretoria = row["AprovadoDiretoria"].ToString();
                            AprovadoControladoria = row["AprovadoControladoria"].ToString();
                            AprovadoPrincipal = row["AprovadoPrincipal"].ToString();
                            EntCod = row["EntCod"].ToString();
                            EntNome = row["EntNome"].ToString();
                            EntCpfCgc = row["EntCpfCgc"].ToString();
                            EntNat = row["EntNat"].ToString();
                            UsuCod = row["UsuCod"].ToString();
                            Alcada = row["Alcada"].ToString();
                            VendCod = row["VendCod"].ToString();
                            VendNome = row["VendNome"].ToString();
                            Historico = row["Historico"].ToString();
                            //AlcadaPrincipal = row["AlcadaPrincipal"].ToString();
                            AlcadaPrincipal = row["Alcada"].ToString();
                            StatusLogisitica = row["Logistica"].ToString();
                            TotalPedido = Convert.ToDouble(row["Total"]);
                            PagadorFrete = row["PedVendaStatFrete"].ToString();
                            transportadora = row["Transportador"].ToString();
                            quantidadeVolumes = row["QuantidadeVolumes"].ToString();
                            pesoBruto = Convert.ToDecimal(row["PesoBruto"].ToString());
                            ValorFrete = Convert.ToDecimal(row["ValorFrete"].ToString());
                            PercentualFrete = Convert.ToDecimal(row["PercentualFrete"].ToString());
                            LocalEmbarque = row["LocalEmbarque"].ToString();
                            NomeTransportador = row["NomeTransportador"].ToString();
                            CondicaoPagamento = row["CondPagCod"].ToString();
                            NaturezaOperacao = row["NatOpCodEstr"].ToString();
                            Cidade = row["CidNome"].ToString();
                            PrazoMedio = row["CondPagPrazoMed"].ToString();

                            HistoricoPedido = row["HistoricoPedido"].ToString();
                            InscricaoEstadual = row["InscricaoEstadual"].ToString();
                            EnquadramentoTirbutario = row["EnquadramentoTirbutario"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Mostra_Liberacoes_Orcamento";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Mostra_Liberacoes_Orcamento. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Mostra_Liberacoes_Orcamento_Logistica()
        {
            string Retorno = "";


            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_MOSTRA_CRM_Liberacoes_Pedidos_Logistica", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EmpCod"].Value = EmpCod;
                    dbCommand.Parameters["@PedVendaNum"].Value = PedVendaNum;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            EmpCod = row["EmpCod"].ToString();
                            EmpNome = row["EmpNome"].ToString();
                            PedVendaNum = row["PedVendaNum"].ToString();
                            UfSigla = row["UfSigla"].ToString();
                            PedVendaStatDescr = row["PedVendaStatDescr"].ToString();
                            DataPrevisao = row["DataPrevisao"].ToString();
                            OutrosDados = row["OutrosDados"].ToString();
                            Concluido = row["Concluido"].ToString();
                            Situacao = row["Situacao"].ToString();
                            AprovadoSupervisor = row["AprovadoSupervisor"].ToString();
                            AprovadoRegional = row["AprovadoRegional"].ToString();
                            AprovadoDiretoria = row["AprovadoDiretoria"].ToString();
                            AprovadoControladoria = row["AprovadoControladoria"].ToString();
                            AprovadoPrincipal = row["AprovadoPrincipal"].ToString();
                            EntCod = row["EntCod"].ToString();
                            EntNome = row["EntNome"].ToString();
                            EntCpfCgc = row["EntCpfCgc"].ToString();
                            EntNat = row["EntNat"].ToString();
                            UsuCod = row["UsuCod"].ToString();
                            Alcada = row["Alcada"].ToString();
                            VendCod = row["VendCod"].ToString();
                            VendNome = row["VendNome"].ToString();
                            Historico = row["Historico"].ToString();
                            //AlcadaPrincipal = row["AlcadaPrincipal"].ToString();
                            AlcadaPrincipal = row["Alcada"].ToString();
                            StatusLogisitica = row["Logistica"].ToString();
                            TotalPedido = Convert.ToDouble(row["Total"]);
                            PagadorFrete = row["PedVendaStatFrete"].ToString();
                            transportadora = row["Transportador"].ToString();
                            quantidadeVolumes = row["QuantidadeVolumes"].ToString();
                            pesoBruto = Convert.ToDecimal(row["PesoBruto"].ToString());
                            ValorFrete = Convert.ToDecimal(row["ValorFrete"].ToString());
                            PercentualFrete = Convert.ToDecimal(row["PercentualFrete"].ToString());
                            LocalEmbarque = row["LocalEmbarque"].ToString();
                            NomeTransportador = row["NomeTransportador"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Mostra_Liberacoes_Orcamento";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Mostra_Liberacoes_Orcamento. Contactar o Suporte!";
            }

            return Retorno;
        }


        public DataTable Consulta_Itens_Orcamento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    //SqlCommand dbCommand = new SqlCommand("user_sp_Lista_Pedidos_Bloqueados_Itens_novo", dbConnection);
                    SqlCommand dbCommand = new SqlCommand("CRM_sp_Lista_Pedidos_Bloqueados_Itens_Atualizado", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 10, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pedido", SqlDbType.VarChar, 30, "Pedido"));

                    dbCommand.Parameters["@Empresa"].Value = this.EmpCod;
                    dbCommand.Parameters["@Pedido"].Value = this.PedVendaNum;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return outputTable;

        }

        public bool Valida_Acesso_Liberacoes_Orcamento()
        {
            bool Retorno = false;


            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_VALIDA_ACESSO_CRM_Liberacoes_Pedidos", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));


                    dbCommand.Parameters["@EmpCod"].Value = EmpCod;
                    dbCommand.Parameters["@PedVendaNum"].Value = PedVendaNum;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = Convert.ToBoolean(row["Acesso"]);

                        }
                    }
                    else
                    {
                        Retorno = false;
                    }
                }
            }
            catch(Exception ex)
            {
                Retorno = false;
            }

            return Retorno;
        }



        public string Registra_Operacao_Orcamento()
        {
            string Retorno = "";


            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_OPERACAO_CRM_Liberacoes_Pedidos", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));

                    dbCommand.Parameters.Add(new SqlParameter("@Aprovado", SqlDbType.VarChar, 150, "Aprovado"));
                    dbCommand.Parameters.Add(new SqlParameter("@RetornaVendedor", SqlDbType.VarChar, 150, "RetornaVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 80000, "Historico"));


                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EmpCod"].Value = EmpCod;
                    dbCommand.Parameters["@PedVendaNum"].Value = PedVendaNum;

                    dbCommand.Parameters["@Aprovado"].Value = AprovadoPrincipal;
                    dbCommand.Parameters["@RetornaVendedor"].Value = this.RetornaVendedor;
                    dbCommand.Parameters["@Historico"].Value = Historico;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["Msg"].ToString();


                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Registra_Operacao_Orcamento";
                    }
                }
            }
            catch(Exception ex)
            {
                Retorno = "Erro na Funcao Registra_Operacao_Orcamento. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string recupera_Alcada(string usuario)
        {
            string Retorno = "";


            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_Recupera_Alcada", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));

                    dbCommand.Parameters["@UsuCod"].Value = usuario;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["Alcada"].ToString();

                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Recupera_Alcada";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Recupera_Alcada. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Atualiza_Fretes_Logistica()
        {
            string Retorno = "";


            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CRM_Atualiza_Logistica", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@Aprovado", SqlDbType.VarChar, 150, "Aprovado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 80000, "Historico"));
                    dbCommand.Parameters.Add(new SqlParameter("@transportadora", SqlDbType.VarChar, 10, "transportadora"));
                    dbCommand.Parameters.Add(new SqlParameter("@quantidadeVolumes", SqlDbType.VarChar, 8000, "quantidadeVolumes"));
                    dbCommand.Parameters.Add(new SqlParameter("@pesoBruto", SqlDbType.Decimal, 0, "pesoBruto"));
                    dbCommand.Parameters.Add(new SqlParameter("@LocalEmbarque", SqlDbType.VarChar, 8000, "LocalEmbarque"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorFrete", SqlDbType.Decimal, 0, "ValorFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@PercentualFrete", SqlDbType.Decimal, 0, "PercentualFrete"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EmpCod"].Value = EmpCod;
                    dbCommand.Parameters["@PedVendaNum"].Value = PedVendaNum;
                    dbCommand.Parameters["@Historico"].Value = Historico;

                    dbCommand.Parameters["@Aprovado"].Value = "Liberado";
                    dbCommand.Parameters["@transportadora"].Value = transportadora;
                    dbCommand.Parameters["@quantidadeVolumes"].Value = quantidadeVolumes;
                    dbCommand.Parameters["@pesoBruto"].Value = pesoBruto;
                    dbCommand.Parameters["@LocalEmbarque"].Value = LocalEmbarque;
                    dbCommand.Parameters["@ValorFrete"].Value = ValorFrete;
                    dbCommand.Parameters["@PercentualFrete"].Value = PercentualFrete;




                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["Msg"].ToString();

                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Registra_Operacao_Orcamento";
                    }
                }
            }
            catch (Exception Ex)
            {
                Retorno = "Erro na Funcao Registra_Operacao_Orcamento. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable Consulta_Transportadoras()
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_CRM_CONSULTA_TRANSPORTADORAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;
        }

        public string Gera_Cotacao_Logistica()
        {
            string Retorno = "";


            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CRM_GERA_COTACAO_FRETE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@transportadora", SqlDbType.VarChar, 10, "transportadora"));
                    dbCommand.Parameters.Add(new SqlParameter("@quantidadeVolumes", SqlDbType.VarChar, 8000, "quantidadeVolumes"));
                    dbCommand.Parameters.Add(new SqlParameter("@pesoBruto", SqlDbType.Decimal, 0, "pesoBruto"));
                    dbCommand.Parameters.Add(new SqlParameter("@LocalEmbarque", SqlDbType.VarChar, 8000, "LocalEmbarque"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));

                    dbCommand.Parameters["@EmpCod"].Value = EmpCod;
                    dbCommand.Parameters["@PedVendaNum"].Value = PedVendaNum;
                    dbCommand.Parameters["@transportadora"].Value = transportadora;
                    dbCommand.Parameters["@quantidadeVolumes"].Value = quantidadeVolumes;
                    dbCommand.Parameters["@pesoBruto"].Value = pesoBruto;
                    dbCommand.Parameters["@LocalEmbarque"].Value = LocalEmbarque;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["Msg"].ToString();

                        }
                    }
                    else
                    {
                        Retorno = "Erro ao gerar cotação de frete.";
                    }
                }
            }
            catch (Exception Ex)
            {
                Retorno = "Erro ao gerar cotação de frete. Contactar o Suporte!";
            }

            if (Retorno == "")
            {
                Retorno = "Cotação de frete enviada com sucesso.";
            }
            return Retorno;
        }

    }
}