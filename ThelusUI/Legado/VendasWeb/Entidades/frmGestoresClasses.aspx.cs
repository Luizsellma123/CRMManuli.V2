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
    public partial class frmGestoresClasses : System.Web.UI.Page
    {
        VendedorClass ObjVendedorClass = new VendedorClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                VendNomeLabel.Text = Session["VendNome"].ToString();

                //Combo vendedor
                #region Combo classe
                ObjVendedorClass.UsuCod = Session["usuario"].ToString();
                ClasseVendedorDropDownList.DataSource = ObjVendedorClass.Listar_Classes_Vendedores();
                ClasseVendedorDropDownList.DataTextField = "VendClasseDescr";
                ClasseVendedorDropDownList.DataValueField = "vendClasseCod";
                ClasseVendedorDropDownList.DataBind();
                ClasseVendedorDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                #endregion

                atualizarGrid();
            }
        }

        protected void atualizarGrid()
        {
            ClasseVendedoresMultiView.Visible = true;

            ObjVendedorClass.VendCod = Session["VendCod"].ToString();

            ClasseVendedorGridView.DataSource = ObjVendedorClass.Listar_User_TB_GestoresClasses();
            ClasseVendedorGridView.DataBind();

            ClasseVendedorDropDownList.SelectedValue = "";
            ClasseVendedorDropDownList.Focus();
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            ObjVendedorClass.VendCod = Session["VendCod"].ToString();
            ObjVendedorClass.VendClasseCod = ClasseVendedorDropDownList.SelectedValue;

            ObjVendedorClass.Salvar_User_TB_GestoresClasses();

            atualizarGrid();
        }

        protected void ExcluirButton_Click(object sender, EventArgs e)
        {
            ObjVendedorClass.CodGestores = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);

            ObjVendedorClass.Excluir_User_TB_GestoresClasses();

            atualizarGrid();
        }

        protected void ClasseVendedorGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ClasseVendedorGridView.PageIndex = e.NewPageIndex;
            atualizarGrid();
        }
    }
}