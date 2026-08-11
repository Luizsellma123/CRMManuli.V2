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
    public partial class frmVendedores : System.Web.UI.Page
    {
        VendedorClass ObjVendedorClass = new VendedorClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();


            if (!IsPostBack)
            {
                this.ControlPainel.refreshVendedor();
                
                ///*Tratar Abrir e fechar Div*/
                //collapseLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

            }


        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            //VendedoresMultiView.Visible = true;

            //ObjVendedorClass.Vendedor = txtFiltroVendCod.Text;

            //ListaVendedorGridView.DataSource = ObjVendedorClass.Listar_Vendedores();
            //ListaVendedorGridView.DataBind();
        }

        protected void SelecionarCheckedChanged(object sender, EventArgs e)
        {
            //CheckBox CheckBox = (CheckBox)sender;
            //GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            ////Desmarca todos os check
            //foreach (GridViewRow OldGridView in ListaVendedorGridView.Rows)
            //{
            //    //Seta todos como falso
            //    ((RadioButton)OldGridView.FindControl("SelecionarRadioButton")).Checked = false;
            //}

            ////marcando o RadioButton selecionado
            //RadioButton RadioButton = (RadioButton)sender;
            //GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            //((RadioButton)GridViewRow.FindControl("SelecionarRadioButton")).Checked = true;

            ///*Pega o codigo do vendedor Selecionado*/
            //ObjVendedorClass.VendCod = ((Label)((Control)sender).FindControl("VendCodLabel")).Text;
            //Session["VendCod"] = ObjVendedorClass.VendCod;

            //ObjVendedorClass.Consulta_vendedor_Por_Codigo();

            //Session["VendNome"] = ObjVendedorClass.VendNome;

            //this.ControlPainel.refreshVendedor();
        }

        protected void ListaVendedorGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            //ListaVendedorGridView.PageIndex = e.NewPageIndex;
            //btnListar_Click(null, null);
        }

    }
}