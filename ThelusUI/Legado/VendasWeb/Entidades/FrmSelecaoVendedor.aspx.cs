using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{
    public partial class FrmSelecaoVendedor : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsEntidades ObjEntidadesClass = new clsEntidades();
        VendedorClass ObjVendedorClass = new VendedorClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();


            if (!IsPostBack)
            {

                #region Recupera Vendedores

                DataTable OBJVendedores = new DataTable();
                ObjVendedorClass.UsuCod = Session["usuario"].ToString();
                ObjVendedorClass.TodosCodigos = "N";
                OBJVendedores = ObjVendedorClass.Consulta_Vendedor();
                
                //Exclui Vendedores desnecessários
                foreach (DataRow orow in OBJVendedores.Select())
                {
                    if (orow["IDVendedor"].ToString().Equals("-10") || orow["IDVendedor"].ToString().Equals("-20") || orow["IDVendedor"].ToString().Equals("12"))
                    {
                        OBJVendedores.Rows.Remove(orow);
                    }
                }

                VendedorGridView.DataSource = OBJVendedores;
                VendedorGridView.DataBind();
                
                #endregion

            }

        }

        protected void VendCodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string Retorno = "";
            string ValidarQuantidade = "Sim";
            ObjEntidadesClass = ((clsEntidades)Session["clsEntidades"]);

            /*Pega o codigo do Vendedo Selecionado*/
            ObjEntidadesClass.NovoVendCod = ((Label)((Control)sender).FindControl("VendCodLabel")).Text;

            /*Inicia Validacao se eh Usuario Controladoria, Se For não Valida Limite de Inativos Abaixo*/
            if (Session["AcessoDiretoria"] != null)
            {
                if(Session["AcessoDiretoria"] == "Sim")
                {
                    ValidarQuantidade = "Não";
                }
            }

            if (ValidarQuantidade == "Sim")
            {
                /*Verifica se a quantidade de Inativos é Menor que a Permitida - Ocorrencia Manuli 12567 - Lizier 10/10/2016 */
                //Retorno = ObjEntidadesClass.Valida_Quantidade_Entidades_Inativas_Por_Vendedor();
                Retorno = "";
            }
            if (Retorno == "")
            {
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama Proxima Tela*/
                Response.Redirect("FrmHistoricoCRM.aspx?indmnu=12");
            }
            else
            {

                Session["Msg"] = Retorno;
                Response.Redirect("FrmCarteira.aspx?indmnu=2");
            }
        }




        
    }
}