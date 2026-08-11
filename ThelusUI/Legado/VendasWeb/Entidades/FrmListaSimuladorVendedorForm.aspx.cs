using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;


namespace VendasWeb.GerencialVendas
{
    public partial class FrmListaSimuladorVendedorForm : System.Web.UI.Page
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
                if(Session["SimSalva"] != null)
                {
                    simulador = (SimuladorClass)Session["SimSalva"];
                    DataTable outpout = new DataTable();
                    outpout = simulador.Pesquisa_Simulacao((string)Session["usuario"]);
                    SimulacoesGridView.DataSource = outpout;
                    SimulacoesGridView.PageIndex = simulador.PaginaSalva;
                    SimulacoesGridView.DataBind();
                    SimulacoesMultiView.Visible = true;
                    SimulacoesGridView.PageIndex = simulador.PaginaSalva;
                    EmpresaDropDownList.SelectedValue = simulador.SearchEmpresa;
                    SituacaoDropDown.SelectedValue = simulador.SearchSituacao;
                    if(simulador.SearchNomeCliente != "")
                    {
                        ClienteText.Value = simulador.SearchNomeCliente;
                    }
                    if (simulador.SearchIdsim != "")
                    {
                        TextSimulacao.Value = simulador.SearchIdsim;
                    }

                    Session["SimSalva"] = null;
                }

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            simulador.SearchEmpresa = EmpresaDropDownList.SelectedValue;
            simulador.SearchIdsim = TextSimulacao.Value;
            simulador.SearchNomeCliente = ClienteText.Value;
            simulador.SearchSituacao = SituacaoDropDown.SelectedValue;
            
            DataTable outpout = new DataTable();
            outpout = simulador.Pesquisa_Simulacao((string)Session["usuario"]);
            SimulacoesGridView.DataSource = outpout;
            SimulacoesGridView.DataBind();
            SimulacoesMultiView.Visible = true;
        }

        protected void SimularButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmSimuladorVendedor.aspx?indmnu=3");
        }

        protected void SimulacoesGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            SimulacoesGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void AcessarButton_Click(object sender, EventArgs e)
        {
            //Salvando pesquisa
            simulador.SearchEmpresa = EmpresaDropDownList.SelectedValue;
            simulador.SearchIdsim = TextSimulacao.Value;
            simulador.SearchNomeCliente = ClienteText.Value;
            simulador.SearchSituacao = SituacaoDropDown.SelectedValue;
            simulador.PaginaSalva = SimulacoesGridView.PageIndex;
            Session["SimSalva"] = simulador;

            simulador.IdSimulacao = ((Label)((Control)sender).FindControl("IdSimGrid")).Text;
            simulador.Armazena_Pesquisa();
            Session["AcessoSim"] = simulador;
            Response.Redirect("FrmSimuladorVendedor.aspx?indmnu=3");
        }
    }
}