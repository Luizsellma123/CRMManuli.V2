using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Controladoria
{
    public partial class ListaSimuladorControladoria : System.Web.UI.Page
    {
        funcoes mdlFuncoes = new funcoes();
        SimuladorClass simulador = new SimuladorClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }


            if (!IsPostBack)
            {
                //Inserindo datasource para dropdown empresa
                EmpresaDropDownList.DataSource = mdlFuncoes.Consulta_Empresa(Session["usuario"].ToString());
                EmpresaDropDownList.DataValueField = "IDEmpresa";
                EmpresaDropDownList.DataTextField = "NomeEmpresa";
                EmpresaDropDownList.DataBind();

                //Inserindo valor padrão de dropdown empresa
                EmpresaDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                EmpresaDropDownList.Focus();

                //Criando Datatable que sera usada como datasource de SituacaoDropDown
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Situacao");
                dt.Columns.Add("Valor");
                DataRow p = dt.NewRow();
                p["Situacao"] = "Pendente";
                p["Valor"] = "1";

                DataRow a = dt.NewRow();
                a["Situacao"] = "Aprovado";
                a["Valor"] = "2";

                DataRow r = dt.NewRow();
                r["Situacao"] = "Reprovado";
                r["Valor"] = "3";

                dt.Rows.Add(p);
                dt.Rows.Add(a);
                dt.Rows.Add(r);

                //Inserindo itens no dropdown situação
                SituacaoDropDown.DataSource = dt;
                SituacaoDropDown.DataValueField = "Situacao";
                SituacaoDropDown.DataTextField = "Situacao";
                SituacaoDropDown.DataBind();

                SituacaoDropDown.Items.Insert(0, new ListItem("Selecione", ""));
                SituacaoDropDown.Focus();

                //Preenchendo os campos caso usuário acesse a pagina retornando da visualização do simulador
                if (Session["Pesquisasalva"] != null)
                {
                    simulador = (SimuladorClass)Session["PesquisaSalva"];
                    EmpresaDropDownList.SelectedValue = simulador.SearchEmpresa;
                    SituacaoDropDown.SelectedValue = simulador.SearchSituacao;
                    if (simulador.SearchNomeCliente != "")
                    {
                        ClienteText.Value = simulador.SearchNomeCliente;
                    }
                    if (simulador.SearchIdsim != "")
                    {
                        TextSimulacao.Value = simulador.SearchIdsim;
                    }
                    BuscarButton_Click(null, null);

                    Session["PesquisaSalva"] = null;
                }
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void SimulacoesGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            SimulacoesGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            simulador.SearchEmpresa = EmpresaDropDownList.SelectedValue;
            simulador.SearchIdsim = TextSimulacao.Value;
            simulador.SearchNomeCliente = ClienteText.Value;
            simulador.SearchSituacao = SituacaoDropDown.SelectedValue;
            simulador.SearchVendedor = VendedorText.Value;

            DataTable outpout = new DataTable();
            outpout = simulador.Pesquisa_Simulacao_Control();
            SimulacoesGridView.DataSource = outpout;

            //Determinando a pagina caso venha de um retorno
            if (Session["Pesquisasalva"] != null)
            {
                SimulacoesGridView.PageIndex = simulador.PaginaSalva;
            }

            SimulacoesGridView.DataBind();
            SimulacoesMultiView.Visible = true;
        }

        protected void AcessarButton_Click(object sender, EventArgs e)
        {
            simulador.SearchEmpresa = EmpresaDropDownList.SelectedValue;
            simulador.SearchIdsim = TextSimulacao.Value;
            simulador.SearchNomeCliente = ClienteText.Value;
            simulador.SearchSituacao = SituacaoDropDown.SelectedValue;
            simulador.PaginaSalva = SimulacoesGridView.PageIndex;
            Session["PesquisaSalva"] = simulador;

            simulador.IdSimulacao = ((Label)((Control)sender).FindControl("IdSimGrid")).Text;
            simulador.Armazena_Pesquisa();
            Session["SimControl"] = simulador;
            Response.Redirect("SimuladorAprovacaoWebForm.aspx?indmnu=3");
        }
    }
}