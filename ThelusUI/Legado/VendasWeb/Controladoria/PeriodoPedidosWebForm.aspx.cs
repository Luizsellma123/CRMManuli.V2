using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Controladoria
{
    public partial class PeriodoPedidosWebForm : System.Web.UI.Page
    {
        ControladoriaClass ObjControladoria = new ControladoriaClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
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
            PedidosPeriodoMultiView.Visible = true;

            DataTable outputTable = new DataTable();
            outputTable = ObjControladoria.Consulta_Periodos();

            PedidosPeriodoGridView.DataSource = outputTable;
            PedidosPeriodoGridView.DataBind();
        }

        protected void DataInicialTextBox_TextChanged(object sender, EventArgs e)
        {
            string EmpCod = "";
            string DataInicial = "";
            string DataFinal = "";

            EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            DataInicial = ((TextBox)((Control)sender).FindControl("DataInicialTextBox")).Text;
            DataFinal = ((TextBox)((Control)sender).FindControl("DataFinalTextBox")).Text;

            Atualiza_Periodos(EmpCod, DataInicial, DataFinal);
        }

        protected void DataFinalTextBox_TextChanged(object sender, EventArgs e)
        {
            string EmpCod = "";
            string DataInicial = "";
            string DataFinal = "";

            EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            DataInicial = ((TextBox)((Control)sender).FindControl("DataInicialTextBox")).Text;
            DataFinal = ((TextBox)((Control)sender).FindControl("DataFinalTextBox")).Text;

            Atualiza_Periodos(EmpCod, DataInicial, DataFinal);

        }

        public void Atualiza_Periodos(string EmpCod, string DataInicial, string DataFinal)
        {
            string erro = "";

            ObjControladoria.EmpCod = EmpCod;
            ObjControladoria.DataInicial = DataInicial;
            ObjControladoria.DataFinal = DataFinal;

            erro = ObjControladoria.Altera_Periodos();

            if (erro != "")
            {
                Session["Msg"] = erro;
            }
        }
    }
}