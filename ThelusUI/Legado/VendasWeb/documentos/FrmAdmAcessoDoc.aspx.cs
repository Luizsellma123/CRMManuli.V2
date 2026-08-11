using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb.documentos
{
    public partial class FrmAdmAcessoDoc : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        clsDocumentosWeb ObjDocumentosWeb = new clsDocumentosWeb();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                //Carrega Usuarios
                drpUsuario.DataSource = mdlfuncoes.Consulta_Usuario();
                drpUsuario.DataTextField = "Usucod";
                drpUsuario.DataValueField = "UsuCod";
                drpUsuario.DataBind();
                drpUsuario.Items.Insert(0, new ListItem("Todos", ""));
                drpUsuario.Focus();


                //Carrega Lista de Documentos
                drpDocumento.DataSource = ObjDocumentosWeb.Mostra_Documento();
                drpDocumento.DataTextField = "Nome";
                drpDocumento.DataValueField = "UserDocumentoID";
                drpDocumento.DataBind();
                drpDocumento.Items.Insert(0, new ListItem("Todos", ""));
                drpDocumento.Focus();


            }


        }

        protected void btnSalvar_Click1(object sender, EventArgs e)
        {
            ObjDocumentosWeb.Usucod = drpUsuario.SelectedValue;

            if (drpDocumento.SelectedValue != "")
            {
                ObjDocumentosWeb.UserDocumentoID = Convert.ToInt32(drpDocumento.SelectedValue);
            }
            else
            {
                ObjDocumentosWeb.UserDocumentoID = 0;
            }


            ObjDocumentosWeb.Insere_Documento_Usuario();

            btnListar_Click1(null, null);

            Response.Write("<script>alert(\"Documento Vinculado.\");</script>");


        }

        protected void btnListar_Click1(object sender, EventArgs e)
        {

            ObjDocumentosWeb.Usucod = drpUsuario.SelectedValue;

            if (drpDocumento.SelectedValue != "")
            {
                ObjDocumentosWeb.UserDocumentoID = Convert.ToInt32(drpDocumento.SelectedValue);
            }
            else
            {
                ObjDocumentosWeb.UserDocumentoID = 0;
            }

            DocumentoGridView.DataSource = ObjDocumentosWeb.Mostra_Documento_Usuario();
            DocumentoGridView.DataBind();
        }

        protected void RemoverButton_Click(object sender, EventArgs e)
        {

            ObjDocumentosWeb.Usucod = ((Label)((Control)sender).FindControl("UsuCodLabel")).Text;
            ObjDocumentosWeb.UserDocXUsuarioID = ((Label)((Control)sender).FindControl("UserDocXUsuarioIDLabel")).Text;

            ObjDocumentosWeb.Remove_Documento_Usuario();


            btnListar_Click1(null, null);

            Response.Write("<script>alert(\"Documento Removido.\");</script>");

        }


    }
}