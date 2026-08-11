using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class UCTabelaPreco : System.Web.UI.UserControl
    {

        CrmTabelaPrecoClass ObjCrmTabelaPrecoClass = new CrmTabelaPrecoClass();
        usuario Objusuario = new usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["ObjCrmTabelaPrecoClass"] != null)
                {
                    //Descarega a session da Entidade
                    ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                    if (ObjCrmTabelaPrecoClass.IDTabela > 0)
                    {
                        ObjCrmTabelaPrecoClass.ManutencaoTabelaPreco();
                    }
                    else
                    {
                        BloqueiaNavegacao();
                    }


                }
                else
                {
                    BloqueiaNavegacao();
                }


            }
        }



        public void BloqueiaNavegacao()
        {

            EmpresaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-industry fa-3x disabled";
            ProdutoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x disabled";




        }


        public void LiberaNavegacao()
        {

            EmpresaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-industry fa-3x";
            ProdutoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x";

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TabelaDePrecoDetalheWebForm.aspx?indmnu=2");
        }

        protected void EmpresaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TabelaDePrecoEmpresaWebForm.aspx?indmnu=2");
        }

        protected void ProdutoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TabelaDePrecoProdutoWebForm.aspx?indmnu=2");
        }

        protected void LinkButtonAtualizar_Click(object sender, EventArgs e)
        {
            ClienteClasse OBJCliente = new ClienteClasse();
            OBJCliente.AtualizacaoGeral();
        }
    }
}