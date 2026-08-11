using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace VendasWeb
{
    public class clasRelatorios : GerencialVendas.clsConexao
    {
        public static SqlConnection dbConnection = new SqlConnection();

        public DataTable relatorioTabelaDinamica(string empresa, string natureza, string produto, string linha, string dataInicial,
            string dataFinal, string entidade, string subFamilia)
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_SelecionaTabelaDinamica", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 30, "Empresa"));
                dbCommand.Parameters.Add(new SqlParameter("@Natureza", SqlDbType.VarChar, 30, "Natureza"));
                dbCommand.Parameters.Add(new SqlParameter("@ProdNome", SqlDbType.VarChar, 30, "ProdNome"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 30, "LinhaProduto"));
                dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 10, "DataInicial"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 10, "DataFinal"));
                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                dbCommand.Parameters.Add(new SqlParameter("@SubFamilia", SqlDbType.VarChar, 30, "SubFamilia"));

                dbCommand.Parameters["@Empresa"].Value = empresa;
                dbCommand.Parameters["@Natureza"].Value = natureza;
                dbCommand.Parameters["@ProdNome"].Value = produto;
                dbCommand.Parameters["@LinhaProduto"].Value = linha;
                dbCommand.Parameters["@DataInicial"].Value = dataInicial;
                dbCommand.Parameters["@DataFinal"].Value = dataFinal;
                dbCommand.Parameters["@EntCod"].Value = entidade;
                dbCommand.Parameters["@SubFamilia"].Value = subFamilia;

                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 320;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();
            }
            return outputTable;
        }

        public DataTable relatorioTabelaDinamicaFitasa(string empresa, string natureza, string produto, string linha, string dataInicial,
            string dataFinal, string entidade, string subFamilia)
        {
            DataTable outputTable = new DataTable();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_SelecionaTabelaDinamicaFaturadosFitasa", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 30, "Empresa"));
                dbCommand.Parameters.Add(new SqlParameter("@Natureza", SqlDbType.VarChar, 30, "Natureza"));
                dbCommand.Parameters.Add(new SqlParameter("@ProdNome", SqlDbType.VarChar, 30, "ProdNome"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 30, "LinhaProduto"));
                dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 10, "DataInicial"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 10, "DataFinal"));
                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                dbCommand.Parameters.Add(new SqlParameter("@SubFamilia", SqlDbType.VarChar, 30, "SubFamilia"));

                dbCommand.Parameters["@Empresa"].Value = empresa;
                dbCommand.Parameters["@Natureza"].Value = natureza;
                dbCommand.Parameters["@ProdNome"].Value = produto;
                dbCommand.Parameters["@LinhaProduto"].Value = linha;
                dbCommand.Parameters["@DataInicial"].Value = dataInicial;
                dbCommand.Parameters["@DataFinal"].Value = dataFinal;
                dbCommand.Parameters["@EntCod"].Value = entidade;
                dbCommand.Parameters["@SubFamilia"].Value = subFamilia;

                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 320;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();
            }
            return outputTable;
        }

        public DataTable relatorioTabelaDinamicaFaturados(string empresa, string natureza, string produto, string linha, string dataInicial,
            string dataFinal, string entidade, string subFamilia)
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_SelecionaTabelaDinamicaFaturados", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 30, "Empresa"));
                dbCommand.Parameters.Add(new SqlParameter("@Natureza", SqlDbType.VarChar, 30, "Natureza"));
                dbCommand.Parameters.Add(new SqlParameter("@ProdNome", SqlDbType.VarChar, 30, "ProdNome"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 30, "LinhaProduto"));
                dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 10, "DataInicial"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 10, "DataFinal"));
                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                dbCommand.Parameters.Add(new SqlParameter("@SubFamilia", SqlDbType.VarChar, 30, "SubFamilia"));

                dbCommand.Parameters["@Empresa"].Value = empresa;
                dbCommand.Parameters["@Natureza"].Value = natureza;
                dbCommand.Parameters["@ProdNome"].Value = produto;
                dbCommand.Parameters["@LinhaProduto"].Value = linha;
                dbCommand.Parameters["@DataInicial"].Value = dataInicial;
                dbCommand.Parameters["@DataFinal"].Value = dataFinal;
                dbCommand.Parameters["@EntCod"].Value = entidade;
                dbCommand.Parameters["@SubFamilia"].Value = subFamilia;

                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 320;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();
            }
            return outputTable;
        }

        public void copiaPedido(string empresa, string numeroPedido, out DataTable outputTable, out DataTable outItemDadosTable, out DataTable outItemDadosTableFoto)
        {
            string caminho = "\\\\192.168.0.2\\Sap\\Imagens\\";
            string nome = "";

            funcoes mdlfuncoes = new funcoes();
            MemoryStream ms = new MemoryStream();


            string strSQL = "";

            strSQL = "SELECT * FROM [USER_VW_CabecarioCopiaPedido] WHERE (([EmpCod] ='" + empresa.ToString() + "') AND ([PedVendaNum] = '" + numeroPedido.ToString() + "'))";

            outputTable = mdlfuncoes.Executa_DataTable(strSQL, "copiaPedido - clasRelatorio.cs");

            //strSQL = "SELECT CAST(ROUND(ItPedVendaQtd, 2) as numeric(14, 2)) as ItPedVendaQtd,CAST(ROUND(ItPedVendaValFinal/ItPedVendaQtd, 2) as numeric(14, 2)) as ItPedVendaUnidMedCodImp, [ItPedVendaUnidMedCod], [ProdCodEstr], [ItPedVendaTexto], CAST(ROUND( [ItPedVendaValUnit], 2) as numeric(14, 2)) as ItPedVendaValUnit, CAST(ROUND([ItPedVendaValTot], 2) as numeric(14,2)) as ItPedVendaValTot, CAST(ROUND([ItPedVendaValFinal], 2) as numeric(14,2)) as ItPedVendaValFinal FROM [ITEM_PED_VENDA] WHERE (([EmpCod] = '" + empresa.ToString() + "') AND ([PedVendaNum] = '" + numeroPedido.ToString() + "'))";
            strSQL = "SELECT CAST(ROUND(CPI.Quantidade, 2) as numeric(14, 2)) as ItPedVendaQtd, ";
            strSQL += "CAST(ROUND(((CPI.Quantidade * CPI.PrecoUnitario) + ISNULL(CPII.ValorImposto, 0)) / CPI.Quantidade, 2) as numeric(14, 2)) as ItPedVendaUnidMedCodImp, ";
            strSQL += "CP.UnidadeVenda ItPedVendaUnidMedCod, CP.CodigoProdutoSAP ProdCodEstr, CP.Nome ItPedVendaTexto, CAST(ROUND(CPI.PrecoUnitario, 2) as numeric(14, 2)) as ItPedVendaValUnit, CAST(ROUND((CPI.Quantidade * CPI.PrecoUnitario), 2) as numeric(14, 2)) as ItPedVendaValTot, CAST(ROUND(((CPI.Quantidade * CPI.PrecoUnitario) + ISNULL(CPII.ValorImposto, 0)), 2) as numeric(14, 2)) as ItPedVendaValFinal ";
            strSQL += "FROM CRM_PEDIDO_ITENS AS CPI INNER JOIN CRM_PRODUTO AS CP ON CPI.IDProduto = CP.IDProduto ";
            strSQL += "LEFT OUTER JOIN CRM_PEDIDO_ITENS_IMPOSTOS AS CPII ON CPII.IDEmpresa = CPI.IDEmpresa AND ";
            strSQL += "CPII.IDPedido = CPI.IDPedido AND CPI.IDItem = CPII.IDItem AND Imposto = 'IPI' ";
            strSQL += "WHERE CPI.IDEmpresa = '" + empresa.ToString() + "' AND CPI.IDPedido = '" + numeroPedido.ToString() + "' ";
            outItemDadosTable = mdlfuncoes.Executa_DataTable(strSQL, "copiaPedido - clasRelatorio.cs");

            strSQL = "select CP.CodigoProdutoSAP ProdCodEstr, CP.ImagemProduto from CRM_PEDIDO_ITENS CPI INNER JOIN CRM_PRODUTO CP ON CP.IDProduto=CPI.IDProdutoCliche ";
            strSQL += "WHERE CPI.IDPedido = " + numeroPedido.ToString() + " and CPI.IDEmpresa = " + empresa.ToString();
            strSQL += "UNION ";
            strSQL += "select CP.CodigoProdutoSAP ProdCodEstr, CP.ImagemProduto from CRM_PEDIDO_ITENS CPI INNER JOIN CRM_PRODUTO CP ON CP.IDProduto=CPI.IDProdutoArruela ";
            strSQL += "WHERE CPI.IDPedido = " + numeroPedido.ToString() + " and CPI.IDEmpresa = " + empresa.ToString();

            outItemDadosTableFoto = mdlfuncoes.Executa_DataTable(strSQL, "copiaPedido - clasRelatorio.cs");

            if (outItemDadosTableFoto.Rows.Count > 0)
            {
                outItemDadosTableFoto.Columns.Add("ProdFoto", typeof(System.Byte[]));

                foreach (DataRow row in outItemDadosTableFoto.Rows)
                {
                    nome = row["ImagemProduto"].ToString();
                    System.Drawing.Image img = Bitmap.FromFile(caminho + nome);
                    byte[] b = ConvertImageToByteArray(img, ImageFormat.Png);
                    row["ProdFoto"] = b;
                }
            }
        }

        private static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, ImageFormat formatOfImage)
        {
            byte[] Ret;

            try
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }

            return Ret;
        }


        public DataTable RelatorioAgendaVisitaDetalhe(int AGENDA_VISITA_ID)
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                using (DataTable outputTable = new DataTable())
                {
                    using (SqlCommand dbCommand = new SqlCommand("User_SP_Agenda_Visita_ID", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@AGENDA_VISITA_ID", SqlDbType.Int, 0, "AGENDA_VISITA_ID"));

                        dbCommand.Parameters["@AGENDA_VISITA_ID"].Value = AGENDA_VISITA_ID;

                        //Aumentando o timeout do command
                        dbCommand.CommandTimeout = 999999;
                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);

                            return outputTable;
                        }
                    }
                }
            }
        }



        public DataTable relatorioTabelaPrecoExICMS()
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                using (DataTable outputTable = new DataTable())
                {
                    using (SqlCommand dbCommand = new SqlCommand("USER_SP_REL_TABELA_PRECO_EX_ICMS", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                        
                        dbCommand.Parameters["@EmpCod"].Value = "1";
                        
                        //Aumentando o timeout do command
                        dbCommand.CommandTimeout = 999999;
                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);

                            return outputTable;
                        }
                    }
                }
            }
        }

        public DataTable relatorioTabelaPrecoManausLocal()
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                using (DataTable outputTable = new DataTable())
                {
                    using (SqlCommand dbCommand = new SqlCommand("USER_SP_REL_TABELA_PRECO_MANAUS_LOCAL", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                        
                        dbCommand.Parameters["@EmpCod"].Value = "2";
                        
                        //Aumentando o timeout do command
                        dbCommand.CommandTimeout = 999999;
                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);

                            return outputTable;
                        }
                    }
                }
            }
        }

        public DataTable relatorioTabelaPrecoManausNacional()
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                using (DataTable outputTable = new DataTable())
                {
                    using (SqlCommand dbCommand = new SqlCommand("USER_SP_REL_TABELA_PRECO_MANAUS_NACIONAL", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));

                        dbCommand.Parameters["@EmpCod"].Value = "2";

                        //Aumentando o timeout do command
                        dbCommand.CommandTimeout = 999999;
                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);

                            return outputTable;
                        }
                    }
                }
            }
        }



        public DataTable relatorioCalendario(string UsuCod, string UsuCodFiltro, DateTime DataInicio, DateTime DataFinal, string IDAgendamentoFiltro,
        string EntNomeFant, string EntNome, string EntCod, string EntCpfCgc)
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                using (DataTable outputTable = new DataTable())
                {
                    using (SqlCommand dbCommand = new SqlCommand("user_sp_crm_relatorio_Agendamento", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                        dbCommand.Parameters.Add(new SqlParameter("@UsuCodFiltro", SqlDbType.VarChar, 8000, "UsuCodFiltro"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataInicio", SqlDbType.DateTime, 0, "DataInicio"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 0, "DataFinal"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAgendamentoFiltro", SqlDbType.VarChar, 8000, "IDAgendamentoFiltro"));
                        dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 150, "EntNomeFant"));
                        dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 150, "EntNome"));
                        dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 150, "EntCod"));
                        dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 150, "EntCpfCgc"));

                        dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                        dbCommand.Parameters["@UsuCodFiltro"].Value = UsuCodFiltro;
                        dbCommand.Parameters["@DataInicio"].Value = DataInicio;
                        dbCommand.Parameters["@DataFinal"].Value = DataFinal;
                        dbCommand.Parameters["@IDAgendamentoFiltro"].Value = IDAgendamentoFiltro;
                        dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant;
                        dbCommand.Parameters["@EntNome"].Value = EntNome;
                        dbCommand.Parameters["@EntCod"].Value = EntCod;
                        dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;





                        //Aumentando o timeout do command
                        dbCommand.CommandTimeout = 999999;
                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);

                            return outputTable;
                        }
                    }
                }
            }
        }


    }
}