using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.Entidades
{
    public partial class FrmAgendaVisita : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        public AgendaVisitaClass ObjAgendaVisitaClass = new AgendaVisitaClass();
        public clsEntidades ObjEntidadesClass = new clsEntidades();
        public DashBoardClass ObjDashBoardClass = new DashBoardClass();
        public funcoes mdlFuncoes = new funcoes();
        public UtilClass ObjUtil = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                Session["ObjAgendaVisitaClass"] = null;


                #region Combo de Filtro por Gestor/Classe/Vendedor
                ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
                ObjDashBoardClass.Consulta_Filtro_Gestor();

                switch (ObjDashBoardClass.Acesso)
                {
                    case "Total":
                        Carrega_Combo_Gestor();
                        break;
                    case "Gestor":
                        Carrega_Combo_Gestor();
                        break;
                    case "Vendedor":
                        Carrega_Combo_Vendedor();
                        GestorLabel.Visible = false;
                        GestorDropDownList.Visible = false;
                        GestorLinkButton.Visible = false;
                        GestorRequiredFieldValidator.Visible = false;


                        ClasseLabel.Visible = false;
                        ClasseDropDownList.Visible = false;
                        ClasseLinkButton.Visible = false;
                        ClasseRequiredFieldValidator.Visible = false;
                        break;
                }





                #endregion


            }
        }




        #region Necessario Combo de Filtro por Gestor/Classe/Vendedor

        protected void Carrega_Combo_Gestor()
        {
            ObjDashBoardClass = new DashBoardClass();

            ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
            GestorDropDownList.DataSource = ObjDashBoardClass.Consulta_Gestores();
            GestorDropDownList.DataTextField = "UsuCod";
            GestorDropDownList.DataValueField = "UsuCod";
            GestorDropDownList.DataBind();

            GestorDropDownList.Items.Insert(0, new ListItem("Todos", "0000000"));
            ClasseDropDownList.Items.Insert(0, new ListItem("Click em Buscar Classe!", ""));
            VendedorDropDownList.Items.Insert(0, new ListItem("Busque a Classe antes!", ""));


        }

        protected void GestorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjDashBoardClass = new DashBoardClass();

            ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
            ObjDashBoardClass.UsuCodAux = ObjUtil.RecuperaDados_Select(GestorDropDownList);
            ClasseDropDownList.DataSource = ObjDashBoardClass.Consulta_Classes_Gestores();
            ClasseDropDownList.DataTextField = "VendClasseDescr";
            ClasseDropDownList.DataValueField = "VendClasseCod";
            ClasseDropDownList.DataBind();
            ClasseDropDownList.Items.Insert(0, new ListItem("Todas", "0000000"));
            ClasseDropDownList.Focus();

            VendedorDropDownList.Items.Insert(0, new ListItem("Click em Buscar Vendedor", ""));


            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        protected void Carrega_Combo_Vendedor()
        {
            mdlFuncoes.Usucod = Session["usuario"].ToString();
            VendedorDropDownList.DataSource = mdlFuncoes.Consulta_Vendedor(Session["usuario"].ToString());
            VendedorDropDownList.DataTextField = "VendNome";
            VendedorDropDownList.DataValueField = "VendCod";
            VendedorDropDownList.DataBind();

            VendedorDropDownList.Items.Insert(0, new ListItem("Todos", "0000000"));

            VendedorDropDownList.Focus();
        }

        protected void ClasseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            ObjDashBoardClass = new DashBoardClass();
            ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
            ObjDashBoardClass.UsuCodAux = ObjUtil.RecuperaDados_Select(GestorDropDownList);
            ObjDashBoardClass.VendClasseCod = ObjUtil.RecuperaDados_Select(ClasseDropDownList);
            VendedorDropDownList.DataSource = ObjDashBoardClass.Consulta_Vendedor_Classes();
            VendedorDropDownList.DataTextField = "VendNome";
            VendedorDropDownList.DataValueField = "VendCod";
            VendedorDropDownList.DataBind();
            VendedorDropDownList.Items.Insert(0, new ListItem("Todos", "0000000"));
            VendedorDropDownList.Focus();

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        #endregion



        protected void NovaLinkButton_Click(object sender, EventArgs e)
        {

            Session["ObjAgendaVisitaClass"] = null;
            Response.Redirect("FrmAgendaVisitaDetalhe.aspx?indmnu=5");
        }


        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            ObjAgendaVisitaClass = new AgendaVisitaClass();


            ObjAgendaVisitaClass.VendCod = ObjUtil.RecuperaDados_Select(VendedorDropDownList);//VendedorDropDownList.SelectedValue;
            ObjAgendaVisitaClass.UsuCod = Session["usuario"].ToString();
            ObjAgendaVisitaClass.UsuCodAux = ObjUtil.RecuperaDados_Select(GestorDropDownList);
            ObjAgendaVisitaClass.VendClasseCod = ObjUtil.RecuperaDados_Select(ClasseDropDownList);


            if (DataITextBox.Text != "")
            {
                ObjAgendaVisitaClass.DataI = Convert.ToDateTime(DataITextBox.Text);
            }
            else
            {
                ObjAgendaVisitaClass.DataI = Convert.ToDateTime("2001-01-01");
            }

            if (DataFTextBox.Text != "")
            {
                ObjAgendaVisitaClass.DataF = Convert.ToDateTime(DataFTextBox.Text);
            }
            else
            {
                ObjAgendaVisitaClass.DataF = Convert.ToDateTime("2200-01-01");
            }


            ObjAgendaVisitaClass.AgendaStatus = AgendaStatusDropDownList.SelectedValue;


            
            

            AgendaGridView.DataSource = ObjAgendaVisitaClass.CONSULTA_AGENDA();
            AgendaGridView.DataBind();
            AgendasMultiView.Visible = true;
            

        }

        protected void AgendaGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            AgendaGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click(null, null);
        }

        protected void DetalheButton_Click(object sender, EventArgs e)
        {

            ObjAgendaVisitaClass = new AgendaVisitaClass();

            //Carregando dados do Grid
            ObjAgendaVisitaClass.AGENDA_VISITA_ID = Convert.ToInt32(((Label)((Control)sender).FindControl("Agenda_Visita_IDLabel")).Text);
            ObjAgendaVisitaClass.MOSTRA_AGENDA();
            ObjAgendaVisitaClass.MOSTRA_PRODUTO_VISITA_AGENDA_VISITA_ID();

            Session["ObjAgendaVisitaClass"] = ObjAgendaVisitaClass;

            //Redireciona
            Response.Redirect("FrmAgendaVisitaDetalhe.aspx?indmnu=5");

        }

        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            ObjAgendaVisitaClass = new AgendaVisitaClass();
            ObjAgendaVisitaClass.AGENDA_VISITA_ID = Convert.ToInt32(((Label)((Control)sender).FindControl("Agenda_Visita_IDLabel")).Text);
            ObjAgendaVisitaClass.MOSTRA_AGENDA();
            Session["ObjAgendaVisitaClass"] = ObjAgendaVisitaClass;


            //Abrir Nova Guia
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect", "window.open('../telasRelatorio/FrmRelAgendaVisita.aspx');", true);

        }

        protected void ImprimirLinkButton_Click(object sender, EventArgs e)
        {
            ObjAgendaVisitaClass = new AgendaVisitaClass();


            ObjAgendaVisitaClass.VendCod = ObjUtil.RecuperaDados_Select(VendedorDropDownList);//VendedorDropDownList.SelectedValue;
            ObjAgendaVisitaClass.UsuCod = Session["usuario"].ToString();
            ObjAgendaVisitaClass.UsuCodAux = ObjUtil.RecuperaDados_Select(GestorDropDownList);
            ObjAgendaVisitaClass.VendClasseCod = ObjUtil.RecuperaDados_Select(ClasseDropDownList);


            if (DataITextBox.Text != "")
            {
                ObjAgendaVisitaClass.DataI = Convert.ToDateTime(DataITextBox.Text);
            }
            else
            {
                ObjAgendaVisitaClass.DataI = Convert.ToDateTime("2001-01-01");
            }

            if (DataFTextBox.Text != "")
            {
                ObjAgendaVisitaClass.DataF = Convert.ToDateTime(DataFTextBox.Text);
            }
            else
            {
                ObjAgendaVisitaClass.DataF = DateTime.Now;
            }


            ObjAgendaVisitaClass.AgendaStatus = AgendaStatusDropDownList.SelectedValue;
            

            Session["ObjAgendaVisitaClass"] = ObjAgendaVisitaClass;

            //Abrir Nova Guia
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect", "window.open('../telasRelatorio/FrmRelAgendaVisitaGeral.aspx');", true);
        }

    }
}