using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.Controladoria
{
    public partial class PeriodoSimulacaoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ControladoriaClass ObjControladoria = new ControladoriaClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }

            if (!IsPostBack)
            {
                /*Tratar Abrir e fechar Div*/
                collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse\" runat=\"server\">";
                Atualiza_Grid();
            }

        }

        public void Atualiza_Grid()
        {
            SimulacaoPeriodoMultiView.Visible = true;

            DataTable outputTable = new DataTable();
            outputTable = ObjControladoria.Consulta_Periodos_Simulacao();

            SimulacaoPeriodoGridView.DataSource = outputTable;
            SimulacaoPeriodoGridView.DataBind();
        }

        public void Atualiza_Periodos(string DataInicial, string DataFinal)
        {
            string erro = "";

            //ObjControladoria.EmpCod = EmpCod;
            ObjControladoria.DataInicial = DataInicial;
            ObjControladoria.DataFinal = DataFinal;

            erro = ObjControladoria.Altera_Periodos_Simulacao();

            if (erro != "")
            {
                Session["Msg"] = erro;
            }
        }

        protected void DataInicialTextBox_TextChanged(object sender, EventArgs e)
        {
            //string EmpCod = "";
            string DataInicial = "";
            string DataFinal = "";

            //EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            DataInicial = ((TextBox)((Control)sender).FindControl("DataInicialTextBox")).Text;
            DataFinal = ((TextBox)((Control)sender).FindControl("DataFinalTextBox")).Text;

            Atualiza_Periodos(DataInicial, DataFinal);
        }

        protected void DataFinalTextBox_TextChanged(object sender, EventArgs e)
        {
            //string EmpCod = "";
            string DataInicial = "";
            string DataFinal = "";

            //EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            DataInicial = ((TextBox)((Control)sender).FindControl("DataInicialTextBox")).Text;
            DataFinal = ((TextBox)((Control)sender).FindControl("DataFinalTextBox")).Text;

            Atualiza_Periodos(DataInicial, DataFinal);

        }
    }
}