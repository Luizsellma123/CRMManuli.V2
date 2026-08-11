using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;
using System.Text;

namespace VendasWeb.classes
{
    public class ControladoriaClass : clsConexao
    {
        #region Campos

        public string EmpCod { get; set; }

        public string DataInicial { get; set; }

        public string DataFinal { get; set; }

        public DateTime PeriodoInicial { get; set; }

        public DateTime PeriodoFinal { get; set; }

        public string Usuario { get; set; }

        JsonConversao jsonconv = new JsonConversao();

        public int IDPosicaoDiaria { get; set; }

        public int IDEmpresa { get; set; }

        public string Status { get; set; }

        public string Cliente { get; set; }

        public int IDGrupo { get; set; }

        #endregion

        #region Métodos

        #region Períodos

        public DataTable Consulta_Periodos()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_PERIODO_PEDIDOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;


                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch (Exception ex)
            {


            }

            return outputTable;

        }

        public string Altera_Periodos()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_PERIODO_PEDIDO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 0, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 0, "DataFinal"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["msg"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Alterar Período.";
                    }
                }
            }
            catch (Exception ex)
            {
                Retorno = "Erro na Funcao Alterar Período. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable Consulta_Periodos_Simulacao()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_PERIODO_SIMULACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch (Exception ex)
            {


            }

            return outputTable;

        }

        public string Altera_Periodos_Simulacao()
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

                    dbCommand = new SqlCommand("CRM_SP_ALTERA_PERIODO_SIMULACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 0, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 0, "DataFinal"));

                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["msg"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Alterar Período Simulação.";
                    }
                }
            }
            catch (Exception ex)
            {
                Retorno = "Erro na Funcao Alterar Período Período Simulação. Contactar o Suporte!";
            }

            return Retorno;
        }

        #endregion

        #region CRM_POSICAO_DIARIA

        public DataTable Consulta_CRM_POSICAO_DIARIA()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@PeriodoInicial", SqlDbType.Date, 0, "PeriodoInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@PeriodoFinal", SqlDbType.Date, 0, "PeriodoFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 8000, "Usuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));

                    dbCommand.Parameters["@PeriodoInicial"].Value = this.PeriodoInicial;
                    dbCommand.Parameters["@PeriodoFinal"].Value = this.PeriodoFinal;
                    dbCommand.Parameters["@Usuario"].Value = this.Usuario ?? "";
                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public WSRetornoJSONClass Gera_Posicao_Diaria(int IDUsuario)
        {
            PosicaoDiariaJsonModel objPosicaoDiariaJsonModel = new PosicaoDiariaJsonModel();

            objPosicaoDiariaJsonModel.IDUsuario = IDUsuario.ToString();
            objPosicaoDiariaJsonModel.PeriodoInicial = PeriodoInicial.ToString();
            objPosicaoDiariaJsonModel.PeriodoFinal = PeriodoFinal.ToString();
            objPosicaoDiariaJsonModel.Automatico = "Não";

            string Json = jsonconv.ConverteObjectParaJSon(objPosicaoDiariaJsonModel);

            FuncoesAPIClass objFuncoesAPIClass = new FuncoesAPIClass();

            return objFuncoesAPIClass.GeraPosicaoDiaria(Json);
        }

        #region Graficos

        public DataTable Consulta_POSICAO_DIARIA_Consolidado_Faturamento_Pendentes(string TipoTotal)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_Consolidado_Faturamento_Pendentes", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoTotal", SqlDbType.VarChar, 8000, "TipoTotal"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@TipoTotal"].Value = TipoTotal;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Consulta_POSICAO_DIARIA_Consolidado_Faturamento(string TipoTotal)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_Consolidado_Faturamento", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoTotal", SqlDbType.VarChar, 8000, "TipoTotal"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@TipoTotal"].Value = TipoTotal;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Consulta_POSICAO_DIARIA_Consolidado_Custo_Medio(int Consolidado)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_Custo_Medio", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@Consolidado", SqlDbType.Int, 0, "Consolidado"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@Consolidado"].Value = Consolidado;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Consulta_POSICAO_DIARIA_Faturamento(string TipoTotal)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_Faturamento", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoTotal", SqlDbType.VarChar, 8000, "TipoTotal"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@TipoTotal"].Value = TipoTotal;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Consulta_POSICAO_DIARIA_Pendentes(string TipoTotal)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_Pendentes", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoTotal", SqlDbType.VarChar, 8000, "TipoTotal"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@TipoTotal"].Value = TipoTotal;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Consulta_POSICAO_DIARIA_Devolucoes(string TipoTotal)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_Devolucoes", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoTotal", SqlDbType.VarChar, 8000, "TipoTotal"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@TipoTotal"].Value = TipoTotal;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        #endregion

        public DataTable Consulta_POSICAO_DIARIA_PARM_EMAIL()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_PARM_EMAIL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        #region Excel

        public DataTable Consulta_CRM_POSICAO_DIARIA_FATURADOS(int Excel)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_CONSULTA_POSICAO_DIARIA_FATURADOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@Excel", SqlDbType.Int, 0, "Excel"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 8000, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@Excel"].Value = Excel;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Consulta_CRM_POSICAO_DIARIA_PENDENTES(int Excel)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_CONSULTA_POSICAO_DIARIA_PENDENTES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@Excel", SqlDbType.Int, 0, "Excel"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 8000, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@Excel"].Value = Excel;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Consulta_CRM_POSICAO_DIARIA_DEVOLUCOES(int Excel)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_CONSULTA_POSICAO_DIARIA_DEVOLUCOES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                    dbCommand.Parameters.Add(new SqlParameter("@Excel", SqlDbType.Int, 0, "Excel"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 8000, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                    dbCommand.Parameters["@Excel"].Value = Excel;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public string MontaTabelaHtmlDoExcel(DataTable Excel)
        {
            StringBuilder Tabela = new StringBuilder();

            Tabela.AppendLine(MontaTabelaHtmlDoExcelStyle());

            if (Excel.Rows.Count > 0)
            {
                Tabela.AppendLine("<table>");

                Tabela.AppendLine("");

                Tabela.AppendLine(MontaTabelaHtmlDoExcelColunas(Excel));

                Tabela.AppendLine("");

                Tabela.AppendLine(MontaTabelaHtmlDoExcelLinhas(Excel));

                Tabela.AppendLine("</table>");
            }

            return Tabela.ToString();
        }

        public string MontaTabelaHtmlDoExcelStyle()
        {
            StringBuilder Tabela = new StringBuilder();

            Tabela.AppendLine("<style>");

            Tabela.AppendLine("table {");

            Tabela.AppendLine("border-collapse:collapse;");

            Tabela.AppendLine("}");

            Tabela.AppendLine("th,td {");

            Tabela.AppendLine("border:1px solid black;");

            Tabela.AppendLine("padding:8px;");

            Tabela.AppendLine("}");

            Tabela.AppendLine("</style>");

            return Tabela.ToString();
        }

        public string MontaTabelaHtmlDoExcelColunas(DataTable Excel)
        {
            StringBuilder Tabela = new StringBuilder();

            if (Excel.Rows.Count > 0)
            {
                Tabela.AppendLine("<tr bgcolor=\"blue\" style=\"color: white;\">");

                foreach (DataColumn column in Excel.Columns)
                {
                    Tabela.AppendLine("<td><b>" + column.ToString() + "</b></td>");
                }

                Tabela.AppendLine("</tr>");
            }

            return Tabela.ToString();
        }

        public string MontaTabelaHtmlDoExcelLinhas(DataTable Excel)
        {
            StringBuilder Tabela = new StringBuilder();

            int colunas = Excel.Columns.Count;

            int linhas = Excel.Rows.Count;

            UtilClass objUtilClass = new UtilClass();

            if (Excel.Rows.Count > 0)
            {
                for (int i = 0; i < linhas; i++)
                {
                    Tabela.AppendLine("<tr>");

                    for (int j = 0; j < colunas; j++)
                    {
                        Tabela.Append("<td>");

                        if (j == 58) Tabela.Append("'");

                        string celula = Excel.Rows[i][j].ToString().Trim();

                        celula = objUtilClass.removerAcentos(celula);

                        Tabela.Append(celula);

                        if (j == 58) Tabela.Append("'");

                        Tabela.AppendLine("</td>");
                    }

                    Tabela.AppendLine("</tr>");
                }
            }

            return Tabela.ToString();
        }

        public DataTable CRM_SP_RETORNA_POSICAO_DIARIA_ESTRATIFICACAO_Resumo_tabela_Excel_Completo()
        {
            DataTable output = new DataTable();

            DataTable Faturado_Devolucao_Pendente = CRM_SP_RETORNA_POSICAO_DIARIA_ESTRATIFICACAO_Resumo_tabela_Excel_Faturado_Devolucao_Pendente();

            DataTable Consolidado = CRM_SP_RETORNA_POSICAO_DIARIA_ESTRATIFICACAO_Resumo_tabela_Excel_Consolidado();

            output = Faturado_Devolucao_Pendente;

            output.Merge(Consolidado);

            return output;
        }

        public DataTable CRM_SP_RETORNA_POSICAO_DIARIA_ESTRATIFICACAO_Resumo_tabela_Excel_Faturado_Devolucao_Pendente()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_ESTRATIFICACAO_Resumo_tabela_Excel_Faturado_Devolucao_Pendente", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable CRM_SP_RETORNA_POSICAO_DIARIA_ESTRATIFICACAO_Resumo_tabela_Excel_Consolidado()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_ESTRATIFICACAO_Resumo_tabela_Excel_Consolidado", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        #endregion

        public DataTable Consulta_POSICAO_DIARIA_FILTRO_GRUPOS()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_POSICAO_DIARIA_FILTRO_GRUPOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.CommandTimeout = 9999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        #endregion

        #endregion
    }
}