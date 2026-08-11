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
    public partial class OrdensDeServicoPrincipalWebForm : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                CarregaDadosECombos();
                CarregaGrid();
            }

            BloqueiaDesbloqueiaButtons();

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void BloqueiaDesbloqueiaButtons()
        {
            DataTable ObjDataTable = new DataTable();

            if (ObjProducao.StatusPrioridade == null || ObjProducao.StatusPrioridade == "")
            {
                ObjDataTable = ObjProducao.ListaOrdensServico();

                foreach (DataRow linha in ObjDataTable.Rows)
                {
                    ObjProducao.StatusPrioridade = linha["StatusPrioridade"].ToString();
                }
            }

            int count = 0;

            count = (int)Session["CarregaGrid"];

            if (ObjProducao.StatusPrioridade == "bloqueado")
            {
                GravarLinkButton.Enabled = false;
                this.OrdensServicoGridView.Columns[0].Visible = false;
                this.OrdensServicoGridView.Columns[7].Visible = false;
                this.OrdensServicoGridView.Columns[8].Visible = true;
                this.OrdensServicoGridView.Columns[9].Visible = false;
                this.OrdensServicoGridView.Columns[10].Visible = true;
                TipoDropDownList.Enabled = false;
            }

            if (OrdemServicoTextBox.Text != null && OrdemServicoTextBox.Text != "")
            {
                ObjProducao.OrdemServico = Convert.ToInt32(OrdemServicoTextBox.Text.ToString());
            }

            if (ObjProducao.Operacao != "inclusao")
            {
                ObjProducao.Validacao = ObjProducao.ValidaExistenciaProdutos();

                if (ObjProducao.Validacao == "SIM" && ObjProducao.StatusPrioridade == "desbloqueado" && count != 0)
                {
                    EnviarSAPButton.Enabled = true;
                }
                else
                {
                    EnviarSAPButton.Enabled = false;
                }

                if (ObjProducao.StatusPrioridade == "bloqueado")
                {
                    CancelarOSLinkButton.Enabled = false;
                    PrioridadeDropDownList.Enabled = false;
                }

                if (ObjProducao.StatusPrioridade != "bloqueado")
                {
                    CancelarOSLinkButton.Enabled = true;
                }

                GravarLinkButton.Enabled = false;
            }
            else
            {
                EnviarSAPButton.Enabled = false;

            }

            OrdensProdTextBox.Enabled = false;

            if (ObjProducao.OK == "OK")
            {
                GravarLinkButton.Enabled = false;
            }

        }

        protected void CarregaDadosECombos()
        {
            if (ObjProducao.Operacao == "inclusao")
            {
                //EMPRESA
                usuario ObjUsuario = new usuario();

                ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

                EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
                EmpresaDropDownList.DataTextField = "NomeEmpresa";
                EmpresaDropDownList.DataValueField = "IDEmpresa";
                EmpresaDropDownList.DataBind();

                // ORDEM SERVIÇO
                OrdemServicoTextBox.Text = "";
                OrdemServicoTextBox.Enabled = false;

                //EMISSOR
                EmissorTextBox.Text = Session["usuario"].ToString();
                EmissorTextBox.Enabled = false;

                //DATA EMISSÃO
                DataEmissaoTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

                //STATUS
                ObjProducao.IDStatus = 1;
                StatusDropDownList.DataSource = ObjProducao.RetornaListaStatusOrdensServico();
                StatusDropDownList.DataTextField = "Descricao";
                StatusDropDownList.DataValueField = "IDStatus";
                StatusDropDownList.DataBind();
                StatusDropDownList.Enabled = false;

                //PRIORIDADE
                PrioridadeDropDownList.DataSource = ObjProducao.RetornaListaPrioridadesOrdensServico();
                PrioridadeDropDownList.DataTextField = "Prioridade";
                PrioridadeDropDownList.DataValueField = "IDPrioridade";
                PrioridadeDropDownList.DataBind();
                PrioridadeDropDownList.SelectedValue = ObjProducao.IDPrioridade.ToString();
                PrioridadeDropDownList.Enabled = true;

                //TIPO
                TipoDropDownList.DataSource = ObjProducao.RetornaListaTiposOrdensServico();
                TipoDropDownList.DataTextField = "Tipo";
                TipoDropDownList.DataValueField = "IDTipoOrdemServico";
                TipoDropDownList.DataBind();
                TipoDropDownList.SelectedValue = "1";
            }
            else
            {
                usuario ObjUsuario = new usuario();

                ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
                ObjProducao.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

                EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
                EmpresaDropDownList.DataTextField = "NomeEmpresa";
                EmpresaDropDownList.DataValueField = "IDEmpresa";
                EmpresaDropDownList.DataBind();
                EmpresaDropDownList.Enabled = false;
                EmpresaDropDownList.SelectedValue = ObjProducao.IDEmpresa.ToString();

                // ORDEM SERVIÇO
                OrdemServicoTextBox.Text = ObjProducao.OrdemServico.ToString();
                OrdemServicoTextBox.Enabled = false;

                //EMISSOR
                EmissorTextBox.Text = ObjProducao.Emissor.ToString();
                EmissorTextBox.Enabled = false;

                //DATA EMISSÃO
                DataEmissaoTextBox.Text = (Convert.ToDateTime(ObjProducao.DataEmissao)).ToString("yyyy-MM-dd");
                DataEmissaoTextBox.Enabled = false;

                //STATUS
                StatusDropDownList.DataSource = ObjProducao.RetornaListaStatusOrdensServico();
                StatusDropDownList.DataTextField = "Descricao";
                StatusDropDownList.DataValueField = "IDStatus";
                StatusDropDownList.DataBind();
                StatusDropDownList.SelectedValue = ObjProducao.IDStatus.ToString();
                StatusDropDownList.Enabled = false;

                //PRIORIDADE
                PrioridadeDropDownList.DataSource = ObjProducao.RetornaListaPrioridadesOrdensServico();
                PrioridadeDropDownList.DataTextField = "Prioridade";
                PrioridadeDropDownList.DataValueField = "IDPrioridade";
                PrioridadeDropDownList.DataBind();
                PrioridadeDropDownList.SelectedValue = ObjProducao.IDPrioridade.ToString();
                PrioridadeDropDownList.Enabled = false;

                //TIPO
                TipoDropDownList.DataSource = ObjProducao.RetornaListaTiposOrdensServico();
                TipoDropDownList.DataTextField = "Tipo";
                TipoDropDownList.DataValueField = "IDTipoOrdemServico";
                TipoDropDownList.DataBind();
                TipoDropDownList.Enabled = false;

                //ORDENS PRODUÇÃO
                ObjProducao.IDEmpresa = Convert.ToInt32(ObjProducao.IDEmpresa.ToString());
                ObjProducao.OrdemServico = Convert.ToInt32(OrdemServicoTextBox.Text);
                ObjProducao.RecuperaOrdensProducao();
                OrdensProdTextBox.Text = ObjProducao.OrdensProducao.ToString();
                OrdensProdTextBox.Enabled = false;


            }
        }

        protected void CarregaGrid()
        {
            int count = 0;

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.ListaOrdensServicoProdutos();
            OrdensServicoGridView.DataSource = OBJDataTable;
            OrdensServicoGridView.DataBind();

            if (ObjProducao.Operacao != "inclusao")
            {
                OrdensServicoMultiView.Visible = true;
            }
            else
            {
                OrdensServicoMultiView.Visible = false;
            }

            count = OBJDataTable.Rows.Count;

            Session["CarregaGrid"] = count;
        }

        public void ValidaCamposPreenchidos()
        {
            Session["Msg"] = "";

            if (EmpresaDropDownList.SelectedValue == "" || EmpresaDropDownList.SelectedValue == null)
            {
                Session["Msg"] = "Escolha uma empresa";
            }
            else if (OrdemServicoTextBox.Text == "" || OrdemServicoTextBox.Text == null)
            {
                Session["Msg"] = "Digite uma ordem de Serviço";
            }
            else if (EmissorTextBox.Text == "" || EmissorTextBox.Text == null)
            {
                Session["Msg"] = "Digite um emissor";
            }
            else if (DataEmissaoTextBox.Text == "" || DataEmissaoTextBox.Text == null)
            {
                Session["Msg"] = "Escolha uma data";

                if (Convert.ToDateTime(DataEmissaoTextBox.Text.ToString()) < DateTime.Now)
                {
                    Session["Msg"] += "A data não poder ser anterior ao dia de hoje ";
                }
            }
            else if (StatusDropDownList.SelectedValue == "" || StatusDropDownList.SelectedValue == null)
            {
                Session["Msg"] = "Escolha um status";
            }
            else if (PrioridadeDropDownList.SelectedValue == "" || PrioridadeDropDownList.SelectedValue == null)
            {
                Session["Msg"] = "Escolha uma prioridade";
            }

            if (Convert.ToDateTime(DataEmissaoTextBox.Text.ToString()) < DateTime.Now)
            {
                Session["Msg"] += "e a data não poder ser anterior ao dia de hoje ";
            }

            //Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Session["Msg"].ToString(), true);
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            Session["Msg"] = null;
        }

        public void ApresentaMensagem(string erro)
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
                erro = "Sucesso na operação.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void GravarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (ObjProducao.Operacao != "inclusao")
            {
                ValidaCamposPreenchidos();

                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
                ObjProducao.IDEmissor = Convert.ToInt32(Session["IDUsuario"].ToString());
                ObjProducao.IDPrioridade = Convert.ToInt32(PrioridadeDropDownList.SelectedValue.ToString());
                ObjProducao.DataEmissao = Convert.ToDateTime(DataEmissaoTextBox.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
                //@IDOrdemServico
                //@IDTipoOrdemServico
                ObjProducao.IDEmissor = Convert.ToInt32(Session["IDUsuario"].ToString());
                ObjProducao.IDTipoOrdemServico = 1;
                ObjProducao.DataEmissao = Convert.ToDateTime(DataEmissaoTextBox.Text).ToString("dd/MM/yyyy");
                ObjProducao.IDPrioridade = Convert.ToInt32(PrioridadeDropDownList.SelectedValue.ToString());
                ObjProducao.CodigoUsuario = Session["usuario"].ToString();
                ObjProducao.Emissor = Session["usuario"].ToString();
            }

            erro = ObjProducao.GravaOrdensServico();

            ApresentaMensagem(erro);

            if (erro == "" && ObjProducao.Operacao != "inclusao")
            {
                ObjProducao.OK = "OK";
                Session["OrdensDeServico"] = ObjProducao;
                this.ProducaoOrdensServicoWebUserControl.DesbloqueiaButtons();
            }

            CarregaDadosECombos();
            CarregaGrid();
            BloqueiaDesbloqueiaButtons();

        }

        protected void EnviarSAPButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            ObjProducao.CodigoUsuario = Session["usuario"].ToString();
            ObjProducao.OrdemServico = Convert.ToInt32(OrdemServicoTextBox.Text);

            erro = ObjProducao.GeracaoOrdensProducao();

            //ApresentaMensagem(erro);

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
                Session["Msg"] = "Sucesso na inclusão da Ordem de Serviço " + ObjProducao.OrdemServico.ToString() + " . ";

                DataTable OBJDataTable = new DataTable();
                OBJDataTable = ObjProducao.VerificaStatusOP();

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow Row in OBJDataTable.Rows)
                    {
                        Session["Msg"] += "<br> Pedido " + Row["DocEntry"].ToString() + " " + Row["StatusPedido"].ToString() + ". Não gerado Ordem Produção. ";
                    }
                }

                VoltarButton_Click(null, null);
            }

            if (Session["Msg"] == null || erro == "")
            {
                ObjProducao.StatusPrioridade = "bloqueado";
                this.ProducaoOrdensServicoWebUserControl.DesbloqueiaButtons();

                ObjProducao.IDStatus = 2;
                CarregaDadosECombos();
                CarregaGrid();
                BloqueiaDesbloqueiaButtons();
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
            string erro = "";

            EscolheCamposGridView(sender, e);
            erro = ObjProducao.ExcluiProduto();
            ApresentaMensagem(erro);

            CarregaGrid();
            BloqueiaDesbloqueiaButtons();
        }

        protected void PlanejadaTextBox_TextChanged(object sender, EventArgs e)
        {
            EscolheCamposGridView(sender, e);
            ObjProducao.AtualizaListaProdutosOrdemServico();

            CarregaGrid();
        }

        protected void EstqCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ObjProducao.StatusPrioridade != "bloqueado")
            {
                EscolheCamposGridView(sender, e);
                ObjProducao.AtualizaListaProdutosOrdemServico();

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
            Response.Redirect("~/Producao/OrdensDeServicoWebForm.aspx?indmnu=3");
        }

        protected void CancelarOSLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            //EscolheCamposGridView(sender, e);
            ObjProducao.CodigoUsuario = Session["Usuario"].ToString();
            erro = ObjProducao.CancelarOrdemServico();

            if (erro == "")
            {
                ObjProducao.StatusPrioridade = "bloqueado";
                this.ProducaoOrdensServicoWebUserControl.DesbloqueiaButtons();
                BloqueiaDesbloqueiaButtons();

                if (Session["OrdensDeServico"] != null)
                {
                    ObjProducao = (producao)Session["OrdensDeServico"];
                }

                ObjProducao.IDStatus = 3;
                CarregaDadosECombos();
                CarregaGrid();
            }
            else
            {
                ApresentaMensagem(erro);
            }

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

                VoltarButton_Click(null, null);
            }
        }

    }
}