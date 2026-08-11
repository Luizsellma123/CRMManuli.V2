using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class ProducaoOrdensServicoWebUserControl : System.Web.UI.UserControl
    {
        producao ObjProducao = new producao();

        protected void Page_Load(object sender, EventArgs e)
        {
            DesbloqueiaButtons();
        }

        public void DesbloqueiaButtons()
        {
            IncluirProdutosLinkButton.Enabled = false;
            EditarProdutosLinkButton.Enabled = false;
            OrdensProducaoLinkButton.Enabled = false;

            if (Session["OrdensDeServico"] != null)
            {
                ObjProducao = (producao)Session["OrdensDeServico"];
            }

            if (ObjProducao.OK == "OK" || ObjProducao.Operacao == "alteracao")
            {
                IncluirProdutosLinkButton.Enabled = true;
                EditarProdutosLinkButton.Enabled = true;
                OrdensProducaoLinkButton.Enabled = true;
            }

            if (ObjProducao.StatusPrioridade == "bloqueado")
            {
                IncluirProdutosLinkButton.Enabled = false;
            }

            string ExisteOP = ObjProducao.VerificaExistenciaOrdensProducao();

            if (ExisteOP == "NAO")
            {
                OrdensProducaoLinkButton.Enabled = false;
            }
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoPrincipalWebForm.aspx?indmnu=3");
        }

        protected void IncluirProdutosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoIncluirProdutosWebForm.aspx?indmnu=3");
        }

        protected void EditarProdutosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoEditarProdutosWebForm.aspx?indmnu=3");
        }

        protected void OrdensProducaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoOrdensProducaoWebForm.aspx?indmnu=3");
        }
    }
}