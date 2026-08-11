using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.telasRelatorio
{
    public partial class parmRelatorioVendas : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                string strSql = "";
                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.DataBind();

                if (int.Parse(Session["nivel"].ToString()) == 0)
                {
                    txtVendedor.Attributes.Add("readonly", "true");
                    txtVendedor.Text = mdlfuncoes.Consulta_CodVendedorAtivo_Usuario(Session["usuario"].ToString()).ToString();
                }

                strSql = "select StatPedVendaCod, StatPedVendaDescr from STAT_PED_VENDA";

                mdlfuncoes.PreencheDropList(chkList, strSql, "");

                /*chkList.DataSource = mdlfuncoes.Consulta_ListaStatus_Ped_Venda();
                chkList.DataTextField = "StatPedVendaDescr";
                chkList.DataValueField = "StatPedVendaCod";
                chkList.DataBind();*/

                btnGerar.Attributes.Add("onclick", "javascript:return validaDados();");
            }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            Session["empresa"] = drpEmpresa.SelectedItem.Value;
            Session["dataInicial"] = mdlfuncoes.FormataData(txtDataInicial.Text);
            Session["datafinal"] = mdlfuncoes.FormataData(txtDataFinal.Text);
            Session["vendedor"] = txtVendedor.Text;

            string dados = "";
            int aux = 0;

            for (int i = 0; i < chkList.Items.Count; i++)
            {
                if (chkList.Items[i].Selected)
                {
                    if (aux == 0)
                        dados = dados + "'" + chkList.Items[i].Value.ToString() + "'";
                    else
                        dados = dados + ", '" + chkList.Items[i].Value.ToString() + "'";
                    aux = aux + 1;
                }
            }

            Session["status"] = dados;

            if (aux != 0)
            {
                Response.Redirect("../relatorios/frmRelatorioVendas.aspx?indmnu=3");
            }
            else {
                Response.Write("<script>alert(\"Ao menos um item deve ser selecionado.\")</script>");
            }
        }
    }
}