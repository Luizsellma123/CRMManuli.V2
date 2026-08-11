using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.Producao
{
    public partial class OrdensDeServicoEditarProdutosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao ObjProducao = new producao();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

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

            ValidaDadosSessao();

            BloqueiaDesbloqueiaButtons();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
                CarregaDadosDaTela();

                ObjProducao.IDPedido = 0;
                ObjProducao.NumeroPedidoCRM = 0;

                CarregaGrid();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void BloqueiaDesbloqueiaButtons()
        {
            if (ObjProducao.StatusPrioridade == "bloqueado")
            {
                this.OrdensServicoGridView.Columns[0].Visible = false;
                this.OrdensServicoGridView.Columns[6].Visible = false;
                this.OrdensServicoGridView.Columns[7].Visible = false;
            }

        }

        public void CarregaDadosDaTela()
        {
            if (ObjProducao.IDEmpresa != 0 && ObjProducao.IDEmpresa.ToString() != "")
            {
                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            }

            if (DataInicialTextBox.Text != "" && DataInicialTextBox.Text != null)
            {
                ObjProducao.DataInicial = DataInicialTextBox.Text.ToString();
            }
            else
            {
                ObjProducao.DataInicial = "";
            }

            if (DataFinalTextBox.Text != "" && DataFinalTextBox.Text != null)
            {
                ObjProducao.DataFinal = DataFinalTextBox.Text.ToString();
            }
            else
            {
                ObjProducao.DataFinal = "";
            }

            if (PedidoSAPTextBox.Text != "" && PedidoSAPTextBox.Text != null)
            {
                ObjProducao.NumeroPedidoSAP = Convert.ToInt32(PedidoSAPTextBox.Text);
            }
            else
            {
                ObjProducao.NumeroPedidoSAP = 0;
            }

            if (PedidoCRMTextBox.Text != "" && PedidoCRMTextBox.Text != null)
            {
                ObjProducao.NumeroPedidoCRM = Convert.ToInt32(PedidoCRMTextBox.Text);
            }
            else
            {
                ObjProducao.NumeroPedidoCRM = 0;
            }

            if (ClienteTextBox.Text != "" && ClienteTextBox.Text != null)
            {
                ObjProducao.Cliente = ClienteTextBox.Text.ToString();
            }
            else
            {
                ObjProducao.Cliente = "";
            }

            if (StatusDropDownList.SelectedValue != "" && StatusDropDownList.SelectedValue != null)
            {
                ObjProducao.Status = StatusDropDownList.SelectedValue.ToString();
            }
            else
            {
                ObjProducao.Status = "";
            }

        }

        public void CarregaDadosNaTela()
        {
            //EMPRESA
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.SelectedValue = ObjProducao.IDEmpresa.ToString();
            EmpresaDropDownList.Enabled = false;

            OrdemServicoTextBox.Text = ObjProducao.OrdemServico.ToString();
            OrdemServicoTextBox.Enabled = false;

            StatusDropDownList.Items.Insert(0, new ListItem("Todos", ""));
        }

        public void CarregaGrid()
        {
            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.RecuperaListaOrdensServicoEditarProdutos();
            OrdensServicoGridView.DataSource = OBJDataTable;
            OrdensServicoGridView.DataBind();
            OrdensServicoMultiView.Visible = true;

            if (ObjProducao.StatusPrioridade == "bloqueado")
            {
                this.OrdensServicoGridView.Columns[7].Visible = false;
                this.OrdensServicoGridView.Columns[8].Visible = true;
            }
        }

        public void ApresentaMensagem(string erro)
        {
            if (Session["Msg"] != null)
            {
                if (erro != "" && erro != null)
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }
                else
                {
                    //Retorna Mensagem de Sucesso
                    Session["Msg"] = "Sucesso na operação.";
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    Session["Msg"] = null;
                }
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

        }

        public void EscolheCamposGridView(object sender, EventArgs e)
        {
            ObjProducao.OrdemServico = Convert.ToInt32(((Label)((Control)sender).FindControl("IDOrdemServicoLabel")).Text);
            ObjProducao.IDPedido = Convert.ToInt32(((Label)((Control)sender).FindControl("PedidoLabel")).Text);
            ObjProducao.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
            ObjProducao.CodigoUsuario = Session["Usuario"].ToString();
            ObjProducao.Planejada = Convert.ToDecimal(((TextBox)((Control)sender).FindControl("PlanejadaTextBox")).Text);
            ObjProducao.Estoque = ((CheckBox)((Control)sender).FindControl("EstqCheckBox")).Checked;
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            EscolheCamposGridView(sender, e);
            ObjProducao.ExcluiProduto();

            ObjProducao.IDPedido = 0;
            ObjProducao.NumeroPedidoCRM = 0;

            CarregaGrid();
        }

        protected void PlanejadaTextBox_TextChanged(object sender, EventArgs e)
        {
            EscolheCamposGridView(sender, e);
            ObjProducao.AtualizaListaProdutosOrdemServico();

            ObjProducao.IDPedido = 0;
            ObjProducao.NumeroPedidoCRM = 0;

            CarregaGrid();
        }

        protected void EstqCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ObjProducao.StatusPrioridade != "bloqueado")
            {
                EscolheCamposGridView(sender, e);
                ObjProducao.AtualizaListaProdutosOrdemServico();

                ObjProducao.IDPedido = 0;
                ObjProducao.NumeroPedidoCRM = 0;

                CarregaGrid();
            }
        }

        protected void OrdensServicoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OrdensServicoGridView.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoPrincipalWebForm.aspx?indmnu=3");
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();
            CarregaGrid();
        }

        protected void ValidaDadosSessao()
        {
            if (Session["OrdensDeServico"] != null)
            {
                ObjProducao = (producao)Session["OrdensDeServico"];
            }
            else
            {
                Session["Msg"] = "A sua sessão expirou.";

                Response.Redirect("~/Producao/OrdensDeServicoWebForm.aspx?indmnu=3");
            }
        }

    }
}