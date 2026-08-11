using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WebVendas.Entidade
{
    public partial class FrmHistoricoEntidade : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        clsEntidades clsEntidades = new clsEntidades();

        protected void Page_Load(object sender, EventArgs e)
        {


            //Valida Acesso
            OBJSessao.ValidaAcesso();



            if (!IsPostBack)
            {
                if (Session["EntCod"] != null)
                {
                    clsEntidades.EntCod = Session["EntCod"].ToString();
                    clsEntidades.Mostra_Entidade();
                    txtEntTextoHist.Text = clsEntidades.EntTextoHist.ToString();
                }
            }  


        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            if (txtNovoHistorico.Text != "")
            {

                clsEntidades.UsuCod = Session["usuario"].ToString();
                clsEntidades.EntCod = Session["EntCod"].ToString();
                clsEntidades.EntTextoHist = txtNovoHistorico.Text;
                clsEntidades.Atualizar_Historico_Entidade();


                Session["EntCod"] = null;
                Response.Write("<script>alert(\"Historico Gravado com Sucesso!\");</script>");
                Response.Write("<script>window.location=\"FrmListaEntidade.aspx?indmnu=" + 2 + "\";</script>");

            }
            else
            {
                Response.Write("<script>alert(\"Insira um novo Histórico antes de Salvar!\");</script>");
                
            }

            

        }

        protected void CancelarButton_Click(object sender, EventArgs e)
        {
            Session["EntCod"] = null;
            Response.Redirect("FrmListaEntidade.aspx?indmnu=2");
        }
    }
}