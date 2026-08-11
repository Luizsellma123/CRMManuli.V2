using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{

    public partial class FrmCondPag : System.Web.UI.Page
    {

        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        SessionClass OBJSessao = new SessionClass();
        GerencialVendas.clsCondPag ObjCondPag = new GerencialVendas.clsCondPag();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                if (Session["clsEntidades"] != null)
                {
                    //Descarrega session
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                    Atualizar_Grid();
                }
            }

        }




        public void Atualizar_Grid()
        {
            ObjCondPag.NIVCOD = ObjEntidadesClass.NIVCOD;
            ObjCondPag.EntCod = ObjEntidadesClass.EntCod;

            CondPagGridView.DataSource = ObjCondPag.Mostra_Cond_pag_Holding();
            CondPagGridView.DataBind();


        }

        protected void SelecionarButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            string retorno = "";
            string msg = "";


            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


            if (ObjEntidadesClass.ListCondPag != null)
            {

                for (int i = 0; i < ObjEntidadesClass.ListCondPag.Count; i++)
                {
                    if (ObjEntidadesClass.ListCondPag[i].TipoOperacao != "Remover")
                    {
                        count++;
                    }

                }


            }



            if (count < 2)
            {
                #region Carrega as Condições
                foreach (GridViewRow row in CondPagGridView.Rows)
                {

                    ObjCondPag = new GerencialVendas.clsCondPag();


                    ObjCondPag.CondPagCod = ((Label)row.FindControl("CondPagCodLabel")).Text;
                    ObjCondPag.CondPagNome = ((Label)row.FindControl("CondPagNomeLabel")).Text;
                    ObjCondPag.CondPagEntValAte = Convert.ToDecimal(9999999);
                    ObjCondPag.TipoOperacao = "Incluir";


                    CheckBox ch = (CheckBox)row.FindControl("SelecionarCheckBox");


                    //verifica se o check ta marcado ou nao
                    if (ch != null)
                    {
                        //Se estiver marcado remove acesso
                        if (ch.Checked)
                        {
                            if (count == 0)
                            {

                                count += 1;
                                ObjEntidadesClass.AdicionarCondPag(ObjCondPag);

                            }
                            else
                            {
                                if (count < 2)
                                {

                                    count += 1;
                                    ObjEntidadesClass.AdicionarCondPag(ObjCondPag);

                                }
                                else
                                {
                                    msg = "Limite de 2 duas condições atingidos!";
                                }
                            }
                        }
                    }

                }
                #endregion
            }
            else
            {
                msg = "Selecionar apenas 2 consições para uma Entidade";

            }

            if (count <= 0)
            {
                msg = "Nenhuma condição selecionada!";
            }



            if (msg == "")
            {

                Session["clsEntidades"] = ObjEntidadesClass;
                Response.Redirect("FrmHolding.aspx?indmnu=2");
            }
            else
            {

                Response.Write("<script>alert(\"" + msg + "\");</script>");
            }


        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmHolding.aspx?indmnu=2");
        }


    }


}