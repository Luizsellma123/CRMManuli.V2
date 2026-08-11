using System;
using System.Linq;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Recebimento
{
    public partial class DetalheWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        RecebimentoClass objRecebimento = new RecebimentoClass();
        bool UsuarioFazParteGrupoAdmRecebimentos;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaCombos();

                CarregaDadosNaTela();
            }
        }

        protected void CarregaCombos()
        {

            EmpresaDropDownList.DataSource = objRecebimento.ConsultaEmpresasUsuario(Convert.ToInt32(Session["IDUsuario"]));
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            objRecebimento.IDUsuarioLogado = Convert.ToInt32(Session["IDUsuario"]);
            SetorDropDownList.DataSource = objRecebimento.ConsultaSetoresUsuario();
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataBind();

            StatusDropDownList.DataSource = objRecebimento.ConsultaStatus();
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataBind();

            FornecedorDropDownList.DataSource = objRecebimento.ConsultaFornecedores();
            FornecedorDropDownList.DataValueField = "IDCliente";
            FornecedorDropDownList.DataTextField = "Cliente";
            FornecedorDropDownList.DataBind();

            objRecebimento.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue);
            UsuariosDropDownList.DataSource = objRecebimento.ConsultaUsuariosSetor();
            UsuariosDropDownList.DataValueField = "IDUsuario";
            UsuariosDropDownList.DataTextField = "Nome";
            UsuariosDropDownList.DataBind();
        }


        protected void CarregaDadosNaTela()
        {
            if (Session["objRecebimento"] != null)
            {
                objRecebimento = (RecebimentoClass)Session["objRecebimento"];

                objRecebimento.CarregaRecebimento();

                EmpresaDropDownList.SelectedValue = objRecebimento.IDEmpresa.ToString();

                EmpresaDropDownList.CssClass = "form-control";

                EmpresaDropDownList.Enabled = false;

                IDRecebimentoTextBox.Text = objRecebimento.IDRecebimento.ToString();

                StatusDropDownList.SelectedValue = objRecebimento.IDStatus.ToString();

                SetorDropDownList.SelectedValue = objRecebimento.IDSetor.ToString();

                UsuariosDropDownList.SelectedValue = objRecebimento.IDUsuario.ToString();

                if (objRecebimento.IDFornecedor > 0)
                    FornecedorDropDownList.SelectedValue = objRecebimento.IDFornecedor.ToString();

                ManualCheckBox.Checked = objRecebimento.Manual;

                CNPJTextBox.Text = objRecebimento.CNPJ;

                FornecedorTextBox.Text = objRecebimento.NomeFornecedor;

                NFTextBox.Text = objRecebimento.NumeroNotaFiscal;

                DataTextBox.Text = objRecebimento.DataCriacao.ToString("yyyy-MM-dd");

                ObservacaoTextBox.Text = objRecebimento.Observacao;

                //Campos do fornecedor só habilitados se for manual
                LiberaCamposFornecedor(objRecebimento.Manual);
            }
            else
            {
                //Escolher status como "Recebido" e Bloqueia
                //procura no propio DataSource do DropDownList qual é o IDStatus que tem a descrição "Recebido"
                StatusDropDownList.SelectedValue = StatusDropDownList.Items.Cast<ListItem>().FirstOrDefault(item => item.Text == "Recebido")?.Value;

                StatusDropDownList.CssClass = "form-control";

                StatusDropDownList.Enabled = false;

                LiberaCamposFornecedor(false);

                DataTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

                FornecedorDropDownList_SelectedIndexChanged(null, null);
            }
        }

        protected void LiberaCamposFornecedor(bool habilitar)
        {
            if (habilitar)
            {
                CNPJTextBox.Enabled = true;
                FornecedorDropDownList.Enabled = false;
                DivFornecedorDropDownList.Visible = false;
                DivFornecedorTextBox.Visible = true;
            }
            else
            {
                CNPJTextBox.Enabled = false;
                FornecedorDropDownList.Enabled = true;
                DivFornecedorDropDownList.Visible = true;
                DivFornecedorTextBox.Visible = false;
            }
        }

        protected void ManualCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LiberaCamposFornecedor(ManualCheckBox.Checked);
        }

        protected void FornecedorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IDFornecedor = Convert.ToInt32(FornecedorDropDownList.SelectedValue);

            if (IDFornecedor > 0)
            {
                ClienteClasse objCliente = new ClienteClasse();

                objCliente.IDCliente = IDFornecedor;

                objCliente.carregaDadosPrincipais();

                CNPJTextBox.Text = objCliente.CNPJCliente;

                FornecedorTextBox.Text = objCliente.NomeCliente;
            }
        }

        protected void SetorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            objRecebimento.IDUsuarioLogado = Convert.ToInt32(Session["IDUsuario"]);
            objRecebimento.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue);
            UsuariosDropDownList.DataSource = objRecebimento.ConsultaUsuariosSetor();
            UsuariosDropDownList.DataValueField = "IDUsuario";
            UsuariosDropDownList.DataTextField = "Nome";
            UsuariosDropDownList.DataBind();

            // Inclui "Todos" apenas se a consulta retornar 2 ou mais setores
            if (UsuariosDropDownList.Items.Count > 1)
            {
                UsuariosDropDownList.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        protected string CarregaDadosDaTela()
        {
            try
            {
                objRecebimento.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);

                if (objRecebimento.IDEmpresa == 0) return "Selecione a empresa.";

                objRecebimento.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);

                if (objRecebimento.IDStatus == 0) return "Selecione o status.";

                objRecebimento.IDRecebimento = Convert.ToInt32(IDRecebimentoTextBox.Text == "" ? "0" : IDRecebimentoTextBox.Text);

                if (string.IsNullOrEmpty(DataTextBox.Text)) return "Informe a data do recebimento.";

                objRecebimento.DataCriacao = Convert.ToDateTime(DataTextBox.Text);

                if (objRecebimento.DataCriacao == null) objRecebimento.DataCriacao = DateTime.Now;

                objRecebimento.IDUsuario = Convert.ToInt32(UsuariosDropDownList.SelectedValue);

                if (objRecebimento.IDUsuario == 0) return "Selecione o usuário.";

                objRecebimento.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue);

                if (objRecebimento.IDSetor == 0) return "Selecione o setor.";

                objRecebimento.Manual = ManualCheckBox.Checked;

                if (objRecebimento.Manual)
                    objRecebimento.IDFornecedor = 0;
                else
                    objRecebimento.IDFornecedor = Convert.ToInt32(FornecedorDropDownList.SelectedValue);

                objRecebimento.NomeFornecedor = FornecedorTextBox.Text;

                if (objRecebimento.Manual && string.IsNullOrEmpty(objRecebimento.NomeFornecedor))
                    return "Informe o nome do fornecedor.";

                objRecebimento.CNPJ = CNPJTextBox.Text;

                if (string.IsNullOrEmpty(objRecebimento.CNPJ)) return "Informe o CNPJ do fornecedor.";

                objRecebimento.NumeroNotaFiscal = NFTextBox.Text;

                if (string.IsNullOrEmpty(objRecebimento.NumeroNotaFiscal)) return "Informe o número da nota fiscal.";

                objRecebimento.Observacao = ObservacaoTextBox.Text;

                objRecebimento.IDUsuarioLogado = Convert.ToInt32(Session["IDUsuario"]);

                return "";
            }
            catch (Exception ex)
            {
                return "Erro ao carregar dados da tela: " + ex.Message;
            }
        }

        protected void GravarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                erro = objRecebimento.GravaRecebimento();

                if (erro == "")
                {
                    Session["objRecebimento"] = objRecebimento;

                    CarregaCombos();

                    CarregaDadosNaTela();

                    RecebimentoDetalheWebUserControl.LiberaNavegacao();
                }
            }

            ApresentaMensagem(erro);
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Recebimento/ListaWebForm.aspx?indmnu=5");
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
    }
}