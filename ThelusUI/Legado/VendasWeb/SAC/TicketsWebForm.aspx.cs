using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.SAC
{
    public partial class TicketsWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            ObjSAC.Tela = "Lista";
            ObjSAC.Filtro = "";
            SituacaoDropDownList.DataSource = ObjSAC.RetornaListaSituacaoTickets();
            SituacaoDropDownList.DataTextField = "Descricao";
            SituacaoDropDownList.DataValueField = "Codigo";
            SituacaoDropDownList.DataBind();
            SituacaoDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            ObjSAC.Solucao = "";
            SolucaoDropDownList.DataSource = ObjSAC.RetornaListaSolucoes();
            SolucaoDropDownList.DataTextField = "Descricao";
            SolucaoDropDownList.DataValueField = "IDSolucao";
            SolucaoDropDownList.DataBind();
            SolucaoDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            BuscarButton_Click(null, null);
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                ObjSAC.Tela = "Lista";
                SACGridView.DataSource = ObjSAC.RetornaListaTickets();
                SACGridView.DataBind();
                SACMultiView.Visible = true;
            }
            else
            {
                ApresentaMensagem(erro);
            }
        }

        protected void SelLinkButton_Click(object sender, EventArgs e)
        {
            ObjSAC.Operacao = "Alteracao";
            ObjSAC.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjSAC.IDTicket = Convert.ToInt32(((Label)((Control)sender).FindControl("TicketLabel")).Text);
            ObjSAC.IDCliente = Convert.ToInt32(((Label)((Control)sender).FindControl("IDClienteLabel")).Text);
            ObjSAC.Cliente = ((Label)((Control)sender).FindControl("ClienteLabel")).Text;
            ObjSAC.CodigoCliente = ((Label)((Control)sender).FindControl("CodigoClienteLabel")).Text;
            Session["TicketsDetalhe"] = ObjSAC;
            Response.Redirect("~/SAC/TicketsDetalheWebForm.aspx?indmnu=5");
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            Session["TicketsDetalhe"] = null;
            Response.Redirect("~/SAC/TicketEscolhaClienteWebForm.aspx?indmnu=5");
        }

        protected string CarregaDadosDaTela()
        {
            string erro = "";

            ObjSAC.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue ?? "0"); //se for nulo atribui zero

            erro = CarregaTicketDaTela();

            if (erro != "") return erro;

            ObjSAC.IDSituacao = Convert.ToInt32(SituacaoDropDownList.SelectedValue ?? "0"); //se for nulo atribui zero

            ObjSAC.AberturaInicial = AbInicialTextBox.Text == "" ? "" : Convert.ToDateTime(AbInicialTextBox.Text).ToString("yyyy-MM-dd");

            ObjSAC.AberturaFinal = AbFinalTextBox.Text == "" ? "" : Convert.ToDateTime(AbFinalTextBox.Text).ToString("yyyy-MM-dd");

            if (ObjSAC.AberturaInicial != "" && ObjSAC.AberturaFinal != "")
            {
                if (Convert.ToDateTime(ObjSAC.AberturaInicial) > Convert.ToDateTime(ObjSAC.AberturaFinal))
                {
                    return "A data de abertura final não pode ser maior que a inicial.";
                }
            }

            ObjSAC.FechamentoInicial = FecInicialTextBox.Text == "" ? "" : Convert.ToDateTime(FecInicialTextBox.Text).ToString("yyyy-MM-dd");

            ObjSAC.FechamentoFinal = FecFinalTextBox.Text == "" ? "" : Convert.ToDateTime(FecFinalTextBox.Text).ToString("yyyy-MM-dd");

            if (ObjSAC.FechamentoInicial != "" && ObjSAC.FechamentoFinal != "")
            {
                if (Convert.ToDateTime(ObjSAC.FechamentoInicial) > Convert.ToDateTime(ObjSAC.FechamentoFinal))
                {
                    return "A data de fechamento final não pode ser maior que a inicial.";
                }
            }

            erro = CarregaClienteDaTela();

            ObjSAC.IDSolucao = Convert.ToInt32(SolucaoDropDownList.SelectedValue);

            return erro;
        }

        protected string CarregaTicketDaTela()
        {
            string erro = "";

            ObjSAC.IDTicket = 0;
            ObjSAC.Ticket = "";

            try
            {
                ObjSAC.IDTicket = Convert.ToInt32(TicketTextBox.Text);
            }
            catch
            {
                erro = "erro";
            }

            if (erro == "erro")
            {
                ObjSAC.Ticket = TicketTextBox.Text;
                erro = "";
            }

            return erro;
        }

        protected string CarregaClienteDaTela()
        {
            string erro = "erro";

            ObjSAC.Cliente = "";

            string ClienteAux = ClienteTextBox.Text;

            ClienteAux = ClienteAux.Replace("/", "");

            ClienteAux = ClienteAux.Replace(".", "");

            ClienteAux = ClienteAux.Replace("-", "");

            if (ClienteAux.Length == 14)
            {
                try
                {
                    erro = "";
                    UInt64 teste = Convert.ToUInt64(ClienteAux);
                    ObjSAC.Cliente = ObjUtilClass.FormataCNPJCPF(ClienteAux);
                }
                catch
                {
                    erro = "erro";
                }
            }

            if (erro == "erro")
            {
                ObjSAC.Cliente = ClienteTextBox.Text;
                erro = "";
            }

            return erro;
        }

        protected void RelatorioPDFLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                try
                {
                    RelatorioCrystalClass objRelatorioCrystalClass = new RelatorioCrystalClass();

                    erro = objRelatorioCrystalClass.GeraRelatorioSACTickets(ObjSAC.IDEmpresa, ObjSAC.IDTicket, ObjSAC.IDSituacao, ObjSAC.AberturaInicial,
                          ObjSAC.AberturaFinal, ObjSAC.FechamentoInicial, ObjSAC.FechamentoFinal, ObjSAC.Cliente, ObjSAC.IDSolucao, ObjSAC.Ticket,
                          "PD", "Relatório Ticket's SAC");

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            ApresentaMensagem(erro);
        }

        protected void RelatorioExcelLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                try
                {
                    RelatorioCrystalClass objRelatorioCrystalClass = new RelatorioCrystalClass();

                    objRelatorioCrystalClass.GeraRelatorioSACTickets(ObjSAC.IDEmpresa, ObjSAC.IDTicket, ObjSAC.IDSituacao, ObjSAC.AberturaInicial,
                        ObjSAC.AberturaFinal, ObjSAC.FechamentoInicial, ObjSAC.FechamentoFinal, ObjSAC.Cliente, ObjSAC.IDSolucao, ObjSAC.Ticket,
                        "ED", "Relatório Ticket's SAC");

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            ApresentaMensagem(erro);
        }

        protected void SACGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SACGridView.PageIndex = e.NewPageIndex;
            CarregaDadosNaTela();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/HomeSACWebForm.aspx?indmnu=5");
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }
    }
}