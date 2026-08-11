using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{
    public partial class FrmQtdClienteVendedor : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        VendedorClass ObjVendedorClass = new VendedorClass();
        UtilClass ObjUtilClass = new UtilClass();


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

                this.ControlPainel.refreshVendedor();

                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                /*Carrega Combo com Classes que o Usuario tem Acesso*/
                ObjVendedorClass.UsuCod = Session["usuario"].ToString();
                ClasseDropDownList.DataSource = ObjVendedorClass.Consulta_Vend_Classe_UsuCod();
                ClasseDropDownList.DataTextField = "VendClasseDescr";
                ClasseDropDownList.DataValueField = "VendClasseCod";
                ClasseDropDownList.DataBind();
                ClasseDropDownList.Items.Insert(0, new ListItem("Todas", "0000000"));

            }

        }



        protected void VendedorGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            VendedorGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click1(null, null);
        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {

            ObjVendedorClass = new VendedorClass();
            ObjUtilClass = new UtilClass();

            switch (drpFiltro.SelectedValue)
            {
                case "1":
                    ObjVendedorClass.VendNome = txtFiltro.Text;
                    break;

                case "2":
                    ObjVendedorClass.VendCod = txtFiltro.Text;
                    break;


            }

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.VendClasseCod = ObjUtilClass.RecuperaDados_Select(ClasseDropDownList);


            VendedorGridView.DataSource = ObjVendedorClass.Lista_Vendedor_Crm_Quantidade_Vendedor();
            VendedorGridView.DataBind();
            VendedoresMultiView.Visible = true;

        }

        protected void QuantidadeInativosVendedorTextBox_TextChanged(object sender, EventArgs e)
        {
            ObjVendedorClass = new VendedorClass();
            ObjVendedorClass.VendCod = ((Label)((Control)sender).FindControl("VendCodLabel")).Text;

            if (((TextBox)((Control)sender).FindControl("QuantidadeInativosVendedorTextBox")).Text != "")
            {
                ObjVendedorClass.QuantidadeInativosVendedor = Convert.ToInt32(((TextBox)((Control)sender).FindControl("QuantidadeInativosVendedorTextBox")).Text);
            }
            else
            {
                ObjVendedorClass.QuantidadeInativosVendedor = 0;

            }

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.Altera_Vendedor_Crm_Quantidade_Vendedor();

           // Response.Write("<script>alert(\"Quantidade Atualizada!\");</script>");
        }



    }
}