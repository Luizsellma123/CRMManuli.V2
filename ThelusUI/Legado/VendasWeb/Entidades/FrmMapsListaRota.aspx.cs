using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{
    public partial class FrmMapsListaRota : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        List<clsEntidades> ListObjEntidadesRotas = new List<clsEntidades>();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                Atualiza_Grid();
            }
        }


        protected void VoltarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmCarteira.aspx?indmnu=2");
        }

        protected void ExibirMapaLinkButton_Click(object sender, EventArgs e)
        {
            string Erro = "";

            if (Session["ListObjEntidadesRotas"] != null)
            {
                ListObjEntidadesRotas = (List<clsEntidades>)Session["ListObjEntidadesRotas"];

                if(ListObjEntidadesRotas.Count < 2)
                {
                    Erro = "Selecione  ao Menos duas Entidades";
                }
            }
            else
            {
                Erro = "Selecione  ao Menos duas Entidades";
            }

            if (Erro == "")
            {
                Response.Redirect("FrmMapsRota.aspx?indmnu=2");
            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RemoverButton_Click(object sender, EventArgs e)
        {

            clsEntidades ObjclsEntidadesAux =  new clsEntidades();
            
            ObjclsEntidadesAux.EntCod = ((Label)((Control)sender).FindControl("EntCodLabel")).Text;
            ObjclsEntidadesAux.EntNome = ((Label)((Control)sender).FindControl("EntNomeLabel")).Text;

            if (Session["ListObjEntidadesRotas"] != null)
            {
                ListObjEntidadesRotas = (List<clsEntidades>)Session["ListObjEntidadesRotas"];

                ListObjEntidadesRotas = ListObjEntidadesRotas.Where(R => R.EntCod != ObjclsEntidadesAux.EntCod).ToList().OrderBy(Ord => Ord.OrdenRoterizacao).ToList();


                Session["ListObjEntidadesRotas"] = ListObjEntidadesRotas;
            }


            Atualiza_Grid();

        }

        public void Atualiza_Grid()
        {
            //Verifica se a Session de Entidades para Criar rota esta criad
            if (Session["ListObjEntidadesRotas"] != null)
            {
                //Pega Valores
                ListObjEntidadesRotas = (List<clsEntidades>)Session["ListObjEntidadesRotas"];

                RoterizacaoGridView.DataSource = ListObjEntidadesRotas.ToList().OrderBy(R => R.OrdenRoterizacao);
                RoterizacaoGridView.DataBind();
                
                
                


            }
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            Session["ListObjEntidadesRotas"] = null;
            Response.Redirect("FrmCarteira.aspx?indmnu=2");

        }

        protected void OrdenRoterizacaoTextBox_TextChanged(object sender, EventArgs e)
        {

            clsEntidades ObjclsEntidadesAux = new clsEntidades();

            ObjclsEntidadesAux.EntCod = ((Label)((Control)sender).FindControl("EntCodLabel")).Text;
            ObjclsEntidadesAux.EntNome = ((Label)((Control)sender).FindControl("EntNomeLabel")).Text;
            ObjclsEntidadesAux.OrdenRoterizacao = Convert.ToInt32(((TextBox)((Control)sender).FindControl("OrdenRoterizacaoTextBox")).Text);

            if (Session["ListObjEntidadesRotas"] != null)
            {
                ListObjEntidadesRotas = (List<clsEntidades>)Session["ListObjEntidadesRotas"];

                //Remove Antigo
                ListObjEntidadesRotas = ListObjEntidadesRotas.Where(R => R.EntCod != ObjclsEntidadesAux.EntCod).ToList();

                //Adiciona Novo
                ListObjEntidadesRotas.Add(ObjclsEntidadesAux);

                //Atualiza o List
                ListObjEntidadesRotas = ListObjEntidadesRotas.OrderBy(Ord => Ord.OrdenRoterizacao).ToList();


                Session["ListObjEntidadesRotas"] = ListObjEntidadesRotas;
            }


            Atualiza_Grid();

        }

        

    }
}