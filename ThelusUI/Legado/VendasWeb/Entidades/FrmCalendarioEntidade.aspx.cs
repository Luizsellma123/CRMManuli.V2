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
    public partial class FrmCalendarioEntidade : System.Web.UI.Page
    {


        CalendarEvent ObjCalendarEvent = new CalendarEvent();
        usuario ObjUsuarioClass = new usuario();
        VendedorClass ObjVendedorClass = new VendedorClass();
        SessionClass OBJSessao = new SessionClass();


        protected void Page_Load(object sender, EventArgs e)
        {

            


            #region Registrando as Picker


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "Picker();", true);


            #endregion





            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {

                Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                Session["Msg"] = null;
            }



            if (!IsPostBack)
            {

                /*Tratar Abrir e fechar Div*/
                collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse\" runat=\"server\">";


                //Atualiza Grid Com os Vendedores
                Atualiza_Select_Vendedores();


            }
           


        }

        protected void btnListar_Click(object sender, EventArgs e)
        {

            ClientesMultiView.Visible = true;
            ObjCalendarEvent = new CalendarEvent();

            switch (drpEntCod.SelectedValue.ToString())
            {
                case "1":
                    ObjCalendarEvent.EntNomeFant = txtFiltroEntCod.Text;
                    break;

                case "2":
                    ObjCalendarEvent.EntNome = txtFiltroEntCod.Text;
                    break;

                case "3":
                    ObjCalendarEvent.EntCod = txtFiltroEntCod.Text;
                    break;

                case "4":
                    ObjCalendarEvent.EntCpfCgc = txtFiltroEntCod.Text;
                    break;
            }


            ObjCalendarEvent.UsuCod = Session["usuarioAgendamento"].ToString();
            
           

            RecuperaDados_Select();//VendCod selecionados

            ListaEntidadeGridView.DataSource = ObjCalendarEvent.Consulta_Entidade_Agenda();
            ListaEntidadeGridView.DataBind();



        }


        protected void RecuperaDados_Select()
        {

            ObjCalendarEvent.VendCod = "";

            for (int i = 0; i < VendedoresSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (VendedoresSelect.Items[i].Selected == true)
                {
                    ObjCalendarEvent.VendCod += VendedoresSelect.Items[i].Value + ",";
                }
            }


        }

       
        protected void ListaEntidadeGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ListaEntidadeGridView.PageIndex = e.NewPageIndex;
            btnListar_Click(null, null);
        }


        protected void Atualiza_Select_Vendedores()
        {
            ObjVendedorClass.UsuCod = Session["usuarioAgendamento"].ToString();
            ObjVendedorClass.TodosCodigos = "ApenasVendedor";
            VendedoresSelect.DataSource = ObjVendedorClass.Consulta_Vendedor();
            VendedoresSelect.DataTextField = "VendNome";
            VendedoresSelect.DataValueField = "VendCod";
            VendedoresSelect.DataBind();

        }



        protected void SelecionarCheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            //Desmarca todos os check
            /*foreach (GridViewRow OldGridView in ListaEntidadeGridView.Rows)
            {
                //Seta todos como falso
                ((RadioButton)OldGridView.FindControl("SelecionarRadioButton")).Checked = false;
            }*/

            //marcando o RadioButton selecionado
            RadioButton RadioButton = (RadioButton)sender;
            GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            ((RadioButton)GridViewRow.FindControl("SelecionarRadioButton")).Checked = true;


            /*Pega o codigo da Entidade Selecionada*/
            ObjCalendarEvent = (CalendarEvent)Session["ObjCalendarEvent"];
            ObjCalendarEvent.EntCod = ((Label)((Control)sender).FindControl("EntCodLabel")).Text;



            /*Carrega em Session*/
            Session["ObjCalendarEvent"] = ObjCalendarEvent;


            Response.Redirect("FrmCalendario.aspx?indmnu=3");

        }

        protected void CancelarLinkButton_Click(object sender, EventArgs e)
        {
            Session["ObjCalendarEvent"] = null;
            Response.Redirect("FrmCalendario.aspx?indmnu=3");
        }


    }
}