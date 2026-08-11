using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;


namespace VendasWeb.Entidades
{
    public partial class FrmListaEntidade : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        clsEntidades clsEntidades = new clsEntidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            Atualizar_Grid();
        }



        public void Atualizar_Grid()
        {



            switch (drpEntCod.SelectedValue.ToString())
            {
                case "1":
                    clsEntidades.EntNomeFant = txtFiltroEntCod.Text;
                    break;

                case "2":
                    clsEntidades.EntNome = txtFiltroEntCod.Text;
                    break;

                case "3":
                    clsEntidades.EntCod = txtFiltroEntCod.Text;
                    break;

                case "4":
                    clsEntidades.EntCpfCgc = txtFiltroEntCod.Text;
                    break;
            }


          
            clsEntidades.UsuCod = "";//Pega de todos os usuarios
            clsEntidades.StatEntCod = "";
            clsEntidades.StatEntComercial = "";

            /***********************************************************************
               Atenção, a Função Consulta_Entidade é utilizada pela tela CERTEIRA
            ************************************************************************/
            EntidadeGridView.DataSource = clsEntidades.Consulta_Entidade();
            EntidadeGridView.DataBind();

        }

        protected void EntidadeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EntidadeGridView.PageIndex = e.NewPageIndex;
            Atualizar_Grid();
        }

        protected void HistoricoButton_Click(object sender, EventArgs e)
        {
            Session["EntCod"] = ((Label)((Control)sender).FindControl("ENTCODLabel")).Text;
            Response.Redirect("FrmHistoricoEntidade.aspx?indmnu=11");


        }
    }
}