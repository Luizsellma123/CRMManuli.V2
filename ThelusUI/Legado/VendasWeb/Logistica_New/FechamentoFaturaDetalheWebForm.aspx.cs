using System;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;
using System.Web.UI;
using VendasWeb.usercontrol;

namespace VendasWeb.Logistica_New
{
    public partial class FechamentoFaturaDetalheWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass objSessao = new SessionClass();
        LogisticaClass objLogistica = new LogisticaClass();
        DataTable FechamentoFaturaNotasDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["Logistica"] != null)
                objLogistica = (LogisticaClass)Session["Logistica"];

            IDEmpresaHiddenField.Value = objLogistica.IDEmpresa.ToString();
            IDFechamentoHiddenField.Value = objLogistica.Fechamento.ToString();

            CarregaCombos();

            StatusDropDownList.Enabled = false;
            FechamentoTextBox.Enabled = false;
            DataTextBox.Enabled = false;
            IdentificadoTextBox.Enabled = false;
            DiferencaTextBox.Enabled = false;

            UsuarioDropDownList.Enabled = false;

            if (objLogistica.Operacao == "Inclusao")
            {
                DataTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                EmpresaDropDownList.Enabled = true;

                if (Session["IDUsuario"] != null)
                    UsuarioDropDownList.SelectedValue = Session["IDUsuario"].ToString();
            }
            else if (objLogistica.Operacao == "Alteracao")
            {
                string Bloqueado = "";

                CancelarLinkButton.Enabled = true;

                DataTable FechamentoFatura = objLogistica.RetornaListaFechamentoFaturaDetalhe();

                if (FechamentoFatura.Rows.Count > 0)
                {
                    foreach (DataRow row in FechamentoFatura.Rows)
                    {
                        EmpresaDropDownList.SelectedValue = IDEmpresaHiddenField.Value;
                        StatusDropDownList.SelectedValue = row["IDStatus"].ToString();
                        FechamentoTextBox.Text = IDFechamentoHiddenField.Value;
                        CNPJTextBox.Text = row["CNPJ"].ToString();
                        VencimentoTextBox.Text = Convert.ToDateTime(row["Vencimento"]).ToString("yyyy-MM-dd");
                        DataTextBox.Text = Convert.ToDateTime(row["Data"]).ToString("yyyy-MM-dd");
                        ValorFaturaTextBox.Text = Convert.ToDecimal(row["ValorFatura"]).ToString("C");
                        FaturaTextBox.Text = row["Fatura"].ToString();
                        UsuarioDropDownList.SelectedValue = row["IDUsuario"].ToString();
                        Bloqueado = row["Bloqueado"].ToString();
                    }

                    CarregaGridView();

                    if (Bloqueado == "1")
                    {
                        VencimentoTextBox.Enabled = false;
                        ValorFaturaTextBox.Enabled = false;
                        FaturaTextBox.Enabled = false;

                        CancelarLinkButton.Enabled = false;
                        LimparDadosLinkButton.Enabled = false;
                        SalvarLinkButton.Enabled = false;
                        EnviarSAPLinkButton.Enabled = false;

                        BloqueiaExcluirGridViewLinkButton();
                    }
                }

                objLogistica.CNPJ = CNPJTextBox.Text;
                Session["Logistica"] = objLogistica;
            }
        }

        protected void CarregaCombos()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            objLogistica.TipoFiltro = "StatusDropDownList";
            objLogistica.Filtro = "";
            StatusDropDownList.DataSource = objLogistica.RetornaListaStatusFechamentoFatura();
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataBind();

            UsuarioDropDownList.DataSource = ObjUsuario.RetornaUsuarios();
            UsuarioDropDownList.DataTextField = "Nome";
            UsuarioDropDownList.DataValueField = "IDUsuario";
            UsuarioDropDownList.DataBind();
        }

        protected void CarregaDadosHiddenField()
        {
            objLogistica.IDEmpresa = Convert.ToInt32(IDEmpresaHiddenField.Value);
            objLogistica.Fechamento = Convert.ToInt32(IDFechamentoHiddenField.Value);
        }

        protected void CarregaGridView()
        {
            CarregaDadosHiddenField();

            FechamentoFaturaNotasDataTable = objLogistica.RetornaListaFechamentoFaturaNotas();

            GridView.DataSource = FechamentoFaturaNotasDataTable;
            GridView.DataBind();
            MultiView.Visible = true;

            if (FechamentoFaturaNotasDataTable.Rows.Count == 1)
            {
                foreach (DataRow row in FechamentoFaturaNotasDataTable.Rows)
                {
                    if (row["Empresa"].ToString() != "Não Identificado")
                    {
                        LimparDadosLinkButton.Enabled = true;
                        EnviarSAPLinkButton.Enabled = true;
                        CarregaIdentificado();
                        CarregaDiferenca(null, null);
                        if (objLogistica.Operacao == "Alteracao")
                            CNPJTextBox.Enabled = false;
                    }
                    else
                    {
                        BloqueiaExcluirGridViewLinkButton();
                        LimparDadosLinkButton.Enabled = false;
                    }
                }
            }
            else if (objLogistica.Operacao == "Alteracao")
            {
                CNPJTextBox.Enabled = false;
                LimparDadosLinkButton.Enabled = true;
                EnviarSAPLinkButton.Enabled = true;
                CarregaIdentificado();
                CarregaDiferenca(null, null);
            }

            if (StatusDropDownList.SelectedItem.Text == "Cancelado")
                CNPJTextBox.Enabled = false;
        }

        protected void CarregaDiferenca(object sender, EventArgs e)
        {
            ValorFaturaTextBox.Text = ValorFaturaTextBox.Text.Replace("R$", "");
            IdentificadoTextBox.Text = IdentificadoTextBox.Text.Replace("R$", "");

            if (ValorFaturaTextBox.Text == "") ValorFaturaTextBox.Text = "0";
            if (IdentificadoTextBox.Text == "") IdentificadoTextBox.Text = "0";

            try
            {
                decimal Diferenca = Convert.ToDecimal(ValorFaturaTextBox.Text)
                                  - Convert.ToDecimal(IdentificadoTextBox.Text);

                DiferencaTextBox.Text = Diferenca.ToString("C");
                ValorFaturaTextBox.Text = Convert.ToDecimal(ValorFaturaTextBox.Text).ToString("C");
                IdentificadoTextBox.Text = Convert.ToDecimal(IdentificadoTextBox.Text).ToString("C");
            }
            catch
            {
                ApresentaMensagem("O Valor da fatura só pode ser numérico.");
            }
        }

        protected void CarregaIdentificado()
        {
            IdentificadoTextBox.Text = "0";
            decimal Identificado = 0;

            foreach (DataRow row in FechamentoFaturaNotasDataTable.Rows)
            {
                if (row["Identificado"].ToString() == "Sim")
                {
                    Identificado += Convert.ToDecimal(row["Valor"].ToString());
                }
            }

            IdentificadoTextBox.Text = Identificado.ToString("C");
        }

        protected string CarregaDadosDaTela(string funcao)
        {
            CarregaDadosHiddenField();

            if (EmpresaDropDownList.SelectedValue == "")
                return "Escolha uma empresa.";
            else
                objLogistica.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);

            objLogistica.Fechamento = Convert.ToInt32(FechamentoTextBox.Text == "" ? "0" : FechamentoTextBox.Text);

            if (Session["IDUsuario"] != null)
                objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());

            if (funcao == "SalvarLinkButton_Click")
            {

                objLogistica.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);

                if (CNPJTextBox.Text == "")
                    return "Informe o CNPJ.";
                else
                    objLogistica.CNPJ = CNPJTextBox.Text;

                if (VencimentoTextBox.Text == "")
                    return "Informe o vencimento.";
                else
                    objLogistica.Vencimento = Convert.ToDateTime(VencimentoTextBox.Text).ToString("yyyy-MM-dd");

                objLogistica.Data = DateTime.Now.ToString("yyyy-MM-dd");

                if (ValorFaturaTextBox.Text == "")
                    return "Informe o valor da fatura.";
                else
                {
                    try
                    {
                        decimal ValorFatura = Convert.ToDecimal(ValorFaturaTextBox.Text.Replace("R$", ""));
                        objLogistica.ValorFatura = ValorFatura;
                    }
                    catch
                    {
                        return "O Valor da fatura só pode ser numérico.";
                    }
                }

                if (objLogistica.ValorFatura == 0) return "O valor da fatura não pode ser zero.";

                if (FaturaTextBox.Text == "")
                    return "Informe o número da fatura.";
                else
                    objLogistica.NumeroFatura = Convert.ToInt64(FaturaTextBox.Text);

                if (UsuarioDropDownList.SelectedValue == "")
                    return "Escolha um usuário.";
                else
                    objLogistica.IDUsuario = Convert.ToInt32(UsuarioDropDownList.SelectedValue);

            }

            return "";
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela("SalvarLinkButton_Click");

            if (erro == "") erro = objLogistica.GravaFechamentoFatura();

            if (erro == "")
            {
                Session["Logistica"] = objLogistica;
                CarregaDadosNaTela();
                this.FechamentoFaturaWebUserControl.Page_Load(null, null);
            }

            ApresentaMensagem(erro);
        }

        protected void CancelarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela("CancelarLinkButton_Click");

            if (erro == "") erro = objLogistica.CancelaFechamentoFatura();

            if (erro == "") CarregaDadosNaTela();

            ApresentaMensagem(erro);
        }

        protected void LimparDadosLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosHiddenField();

            objLogistica.IDNota = 0;

            string erro = objLogistica.ExcluiFechamentoFaturaNotas();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void ExcluirGridViewLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosHiddenField();

            objLogistica.IDNota = Convert.ToInt32(((Label)((Control)sender).FindControl("IDNotaGridViewLabel")).Text);

            string erro = objLogistica.ExcluiFechamentoFaturaNotas();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void BloqueiaExcluirGridViewLinkButton()
        {
            foreach (GridViewRow row in GridView.Rows)
            {
                LinkButton btn = row.FindControl("ExcluirGridViewLinkButton") as LinkButton;

                btn.Enabled = false;
            }
        }

        protected void EnviarSAPLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosHiddenField();

            objLogistica.CodigoUsuario = Session["usuario"].ToString();

            string erro = objLogistica.ImportaFechamentoFatura();

            if (erro == "")
                erro = objLogistica.AtualizaImportadoSAPFechamentoFaturaNotas();

            ApresentaMensagem(erro);
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;

            CarregaGridView();
        }

        protected void ApresentaMensagem(string erro)
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

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/FechamentoFaturaWebForm.aspx?indmnu=5");
        }
    }
}