using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb.classes
{
    public class GraficoProjetosClass : clsConexao
    {
        public string id { get; set; }
        public string name { get; set; }
        public string actualStart { get; set; }
        public string actualEnd { get; set; }

        public List<GraficoProjetosChildrenClass> children { get; set; }

        public double MilliTimeStamp(DateTime TheDate)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = TheDate.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

            return ts.TotalMilliseconds;
        }

        public string Chamado { get; set; }
        public int IDStatus { get; set; }
        public int IDUsuarioSolicitante { get; set; }
        public int IDUsuarioResponsavel { get; set; }
        public int IDSetor { get; set; }
        public int IDPrioridadeProjeto { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public DataTable CarregaDadosGraficoProjeto()
        {
            DataTable outputTable = new DataTable();
            string AnteriorChildren = "";
            this.id = "1";
            this.name = "Projetos Manuli";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_GRAFICO_PROJETOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Chamado", SqlDbType.VarChar, 8000, "Chamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 0, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 0, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioSolicitante", SqlDbType.Int, 0, "IDUsuarioSolicitante"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));                    
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, "IDPrioridade"));                    

                    dbCommand.Parameters["@Chamado"].Value = this.Chamado;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    dbCommand.Parameters["@IDUsuarioSolicitante"].Value = this.IDUsuarioSolicitante;
                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridadeProjeto;                    

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                GraficoProjetosChildrenClass OBJProjetosChildren = new GraficoProjetosChildrenClass();
                                OBJProjetosChildren.id = this.id + "_" + row["IDChamado"].ToString();
                                OBJProjetosChildren.name = row["Assunto"].ToString();
                                OBJProjetosChildren.actualStart = this.MilliTimeStamp(Convert.ToDateTime(row["DataInicial"])).ToString();
                                OBJProjetosChildren.actualEnd = this.MilliTimeStamp(Convert.ToDateTime(row["DataFinal"])).ToString();
                                OBJProjetosChildren.progressValue = row["PercentualRealizado"].ToString() + "%";
                                OBJProjetosChildren.connectTo = "";
                                AnteriorChildren = OBJProjetosChildren.id;
                                OBJProjetosChildren.connectorType = "finish-start";

                                //Verifica se esta instanciado
                                if (this.children == null)
                                {
                                    this.children = new List<GraficoProjetosChildrenClass>();
                                }

                                //Seta anterior
                                if (this.children.Count > 0)
                                {
                                    //this.children[this.children.Count() - 1].connectTo = OBJProjetosChildren.id;
                                }

                                this.children.Add(OBJProjetosChildren);


                                //Atribui as datas inicial e final do projeto
                                if(this.actualStart == "" || this.actualStart == null)
                                {
                                    this.actualStart = this.MilliTimeStamp(Convert.ToDateTime(row["DataInicial"])).ToString();
                                }

                                //Atribui data final
                                this.actualEnd = this.MilliTimeStamp(Convert.ToDateTime(row["DataFinal"])).ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;

        }

        public string GeraGrafico()
        {
            string Grafico = "";
            string Dados = "";
            JsonConversao jsonconv = new JsonConversao();

            this.CarregaDadosGraficoProjeto();

            Dados = jsonconv.ConverteObjectParaJSon(this);

            Grafico = "<script>";

            //Seta Localidade Brasil
            Grafico += "anychart.format.inputLocale('pt-br'); ";
            Grafico += "anychart.format.outputLocale('pt-br'); ";

            Grafico += "anychart.onDocumentReady(function () { ";
            Grafico += "var data = [ ";
            Grafico += Dados;
            Grafico += "]; ";

            Grafico += "var treeData = anychart.data.tree(data, \"as-tree\"); ";
            Grafico += "var chart = anychart.ganttProject(); ";
            Grafico += "chart.data(treeData); ";

            //Altura da Linha
            Grafico += "chart.defaultRowHeight(20); ";
            Grafico += "chart.headerHeight(50); ";

            // set the width of data grid columns
            Grafico += "chart.dataGrid().column(0).width(10); ";
            Grafico += "chart.dataGrid().column(0).enabled(false); ";
            Grafico += "chart.dataGrid().column(1).width(450); ";
            Grafico += "chart.splitterPosition(\"40%\"); ";

            Grafico += "chart.getTimeline().scale().maximum(" + this.actualEnd + "); ";
            Grafico += "chart.container(\"grafico\"); ";
            Grafico += "chart.draw(); ";
            Grafico += "chart.fitAll(); ";

            Grafico += " }); ";

            Grafico += "</script>";

            //Retorna grafico
            return Grafico;
        }
    }
}