using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class HistoricosClass : clsConexao
    {
        public int IDCliente { get; set; }
        public string Historico { get; set; }
        public int IDTipoHistorico { get; set; }
        public int IDEvento { get; set; }
        public int IDCategoria { get; set; }
        public int IDUsuario { get; set; }
        public int IDChamado { get; set; }
        public int IDTicket { get; set; }
        public int IDHistorico { get; set; }
        public string Descricao { get; set; }

        public void RetornaHistoricosCliente()
        {
            //Limpa para não trazer lixo
            this.Historico = "";

            DataTable OBJData = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        OBJData.Load(dataReader);

                        if (OBJData.Rows.Count > 0)
                        {
                            foreach (DataRow row in OBJData.Rows)
                            {
                                //Carrega icones
                                Historico += "<div class=\"timeline-entry\"> <div class=\"timeline-stat\"> ";
                                Historico += "<div class=\"" + row["TimeLineButonClass"].ToString() + "\"><i class=\"" + row["TimeLineIconClass"].ToString() + "\"></i> ";

                                //Carrega data
                                Historico += "</div><div class=\"timeline-time\"><b>" + row["DataHistorico"].ToString() + "</b></div> " + "</div><div class=\"timeline-label\"> ";

                                //Carrega Título Historico
                                Historico += "<p class=\"mar-no pad-btm\"> <span class=\"" + row["TimeLineTituloClass"].ToString() + "\">" + row["DescricaoEvento"].ToString() + " " + row["DescricaoCategoria"].ToString();

                                //Carrega Corpo Histórico
                                Historico += "</span> por <a href=\"#\" class=\"btn-link btn-md text-semibold\"> ";
                                Historico += row["CodigoUsuario"].ToString() + "</a></p>";
                                Historico += "<div class=\"well well-xs mar-no\"> ";
                                Historico += row["Historico"].ToString();
                                Historico += "</div>";

                                //Fecha Historico
                                Historico += "</div></div>";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void RetornaHistoricosChamados()
        {
            //Limpa para não trazer lixo
            this.Historico = "";

            DataTable OBJData = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_CHAMADOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));

                    dbCommand.Parameters["@IDChamado"].Value = this.IDChamado;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        OBJData.Load(dataReader);

                        if (OBJData.Rows.Count > 0)
                        {
                            foreach (DataRow row in OBJData.Rows)
                            {
                                //Carrega icones
                                Historico += "<div class=\"timeline-entry\"> <div class=\"timeline-stat\"> ";
                                Historico += "<div class=\"" + row["TimeLineButonClass"].ToString() + "\"><i class=\"" + row["TimeLineIconClass"].ToString() + "\"></i> ";

                                //Carrega data
                                Historico += "</div><div class=\"timeline-time\"><b>" + row["DataHistorico"].ToString() + "</b></div> " + "</div><div class=\"timeline-label\"> ";

                                //Carrega Título Historico
                                Historico += "<p class=\"mar-no pad-btm\"> <span class=\"" + row["TimeLineTituloClass"].ToString() + "\">" + row["DescricaoEvento"].ToString() + " " + row["DescricaoCategoria"].ToString();

                                //Carrega Corpo Histórico
                                Historico += "</span> por <a href=\"#\" class=\"btn-link btn-md text-semibold\"> ";
                                Historico += row["CodigoUsuario"].ToString() + "</a></p>";
                                Historico += "<div class=\"well well-xs mar-no\"> ";
                                Historico += row["Historico"].ToString();
                                Historico += "</div>";

                                //Fecha Historico
                                Historico += "</div></div>";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public DataTable RetornaEventos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_EVENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoHistorico", SqlDbType.Int, 0, "IDTipoHistorico"));

                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDTipoHistorico"].Value = this.IDTipoHistorico;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable RetornaEventosCategorias()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_EVENTO_CATEGORIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoHistorico", SqlDbType.Int, 0, "IDTipoHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));

                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDTipoHistorico"].Value = this.IDTipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public string GravaHistoricoCliente()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_HISTORICO_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoHistorico", SqlDbType.Int, 0, "IDTipoHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 8000, "Historico"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@IDHistorico", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "IDHistorico", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDTipoHistorico"].Value = this.IDTipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;
                    dbCommand.Parameters["@IDCategoria"].Value = this.IDCategoria;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@Historico"].Value = this.Historico;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.IDHistorico = Convert.ToInt32((string)dbCommand.Parameters["@IDHistorico"].Value);

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do histórico do cliente";
                }
            }

            return erro;
        }

        public string GravaHistoricoChamado()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_HISTORICO_CHAMADO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoHistorico", SqlDbType.Int, 0, "IDTipoHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 8000, "Historico"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.IDChamado;
                    dbCommand.Parameters["@IDTipoHistorico"].Value = this.IDTipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;
                    dbCommand.Parameters["@IDCategoria"].Value = this.IDCategoria;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@Historico"].Value = this.Historico;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do histórico do chamado";
                }
            }

            return erro;
        }

        public string GRAVA_HISTORICO_RASTREIO_PEDIDOS
        (int IDEmpresa, int NumeroPedidoSAP, int NumeroNotaFiscal, int IDUsuario, string PrevisaoEntrega, string Tipo)
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_HISTORICO_RASTREIO_PEDIDOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroNotaFiscal", SqlDbType.Int, 0, "NumeroNotaFiscal"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipo", SqlDbType.Int, 0, "IDTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataHistorico", SqlDbType.VarChar, 8000, "DataHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 8000, "Historico"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.VarChar, 8000, "Tipo"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoOcorrencia", SqlDbType.VarChar, 8000, "CodigoOcorrencia"));

                    dbCommand.Parameters.Add(new SqlParameter("@PrevisaoEntrega", SqlDbType.VarChar, 8000, "PrevisaoEntrega"));

                    dbCommand.Parameters["@IDEmpresa"].Value = IDEmpresa;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = NumeroPedidoSAP;
                    dbCommand.Parameters["@NumeroNotaFiscal"].Value = NumeroNotaFiscal;

                    dbCommand.Parameters["@IDTipo"].Value = IDTipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = IDEvento;
                    dbCommand.Parameters["@IDCategoria"].Value = IDCategoria;

                    dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;
                    dbCommand.Parameters["@DataHistorico"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dbCommand.Parameters["@Historico"].Value = Historico;
                    dbCommand.Parameters["@Tipo"].Value = Tipo;

                    dbCommand.Parameters["@IDTransportador"].Value = 0;
                    dbCommand.Parameters["@CodigoOcorrencia"].Value = "";

                    dbCommand.Parameters["@PrevisaoEntrega"].Value = PrevisaoEntrega;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do histórico do chamado";
                }
            }

            return erro;
        }

        public string RetornaEventoDescricao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_EVENTO_Descricao", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoHistorico", SqlDbType.Int, 0, "IDTipoHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));

                    dbCommand.Parameters["@IDTipoHistorico"].Value = this.IDTipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Descricao"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return "";
        }

        public string RetornaEventoIDEvento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_EVENTO_IDEvento", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipo", SqlDbType.Int, 0, "IDTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));

                    dbCommand.Parameters["@IDTipo"].Value = this.IDTipoHistorico;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["IDEvento"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return "";
        }

        public string RetornaEventoCategoriaDescricao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_EVENTO_CATEGORIA_Descricao", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipo", SqlDbType.Int, 0, "IDTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));

                    dbCommand.Parameters["@IDTipo"].Value = this.IDTipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;
                    dbCommand.Parameters["@IDCategoria"].Value = this.IDCategoria;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Descricao"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return "";
        }

        public string RetornaEventoCategoriaIDCategoria()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_EVENTO_CATEGORIA_IDCategoria", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipo", SqlDbType.Int, 0, "IDTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));

                    dbCommand.Parameters["@IDTipo"].Value = this.IDTipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["IDCategoria"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return "";
        }
    }
}