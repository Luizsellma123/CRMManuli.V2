using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Recebimento
{
    public partial class ListaWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        RecebimentoClass objRecebimento = new RecebimentoClass();

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

                CarregaDatas();

                CarregaCombos();

                BuscarLinkButton_Click(null, null);
            }
        }

        protected void CarregaDatas()
        {
            DataInicialTextBox.Text = ObjUtilClass.FormataDataSQL(ObjUtilClass.RetornaPrimeiroDiaMesAtual().ToString());

            DataFinalTextBox.Text = ObjUtilClass.FormataDataSQL(DateTime.Today.ToString());
        }

        protected void CarregaCombos()
        {
            string CodigoUsuario = "";
            CodigoUsuario = Session["usuario"].ToString();

            EmpresaDropDownList.DataSource = objRecebimento.ConsultaEmpresasUsuario(Convert.ToInt32(Session["IDUsuario"]));
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            objRecebimento.IDUsuarioLogado = Convert.ToInt32(Session["IDUsuario"]);
            SetorDropDownList.DataSource = objRecebimento.ConsultaSetoresUsuario();
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataBind();

            // Inclui "Todos" apenas se a consulta retornar 2 ou mais setores
            if (SetorDropDownList.Items.Count > 1)
            {
                SetorDropDownList.Items.Insert(0, new ListItem("Todos", "0"));
            }

            StatusDropDownList.DataSource = objRecebimento.ConsultaStatus();
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataBind();

            // Inclui "Todos" apenas se a consulta retornar 2 ou mais setores
            if (StatusDropDownList.Items.Count > 1)
            {
                StatusDropDownList.Items.Insert(0, new ListItem("Todos", "0"));
            }

            FornecedorDropDownList.DataSource = objRecebimento.ConsultaFornecedores();
            FornecedorDropDownList.DataValueField = "IDCliente";
            FornecedorDropDownList.DataTextField = "Cliente";
            FornecedorDropDownList.DataBind();

            // Inclui "Todos" apenas se a consulta retornar 2 ou mais setores
            if (FornecedorDropDownList.Items.Count > 1)
            {
                FornecedorDropDownList.Items.Insert(0, new ListItem("Todos", "0"));
            }

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
            objRecebimento.IDUsuarioLogado = Convert.ToInt32(Session["IDUsuario"]);
            objRecebimento.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            objRecebimento.IDUsuario = Convert.ToInt32(UsuariosDropDownList.SelectedValue);
            objRecebimento.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue);
            objRecebimento.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);
            objRecebimento.IDFornecedor = Convert.ToInt32(FornecedorDropDownList.SelectedValue);
            objRecebimento.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text);
            objRecebimento.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text);
             

            if (objRecebimento.DataFinal < objRecebimento.DataInicial)
            {
                return "A data final não pode ser menor que a data inicial.";
            }

            return "";
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                RecebimentoGridView.DataSource = objRecebimento.ConsultaListaRecebimentos();
                RecebimentoGridView.DataBind();
                RecebimentoMultiView.Visible = true;

                if (RecebimentoGridView.Rows.Count == 0)
                {
                    ApresentaMensagem("Nenhum registro encontrado.");
                }
            }
            else
            {
                ApresentaMensagem(erro);
            }
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            Session["objRecebimento"] = null;

            Response.Redirect("~/Recebimento/DetalheWebForm.aspx?indmnu=5");
        }

        protected void SelecionarGridViewLinkButton_Click(object sender, EventArgs e)
        {
            objRecebimento.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaGridViewLabel")).Text ?? "0");

            objRecebimento.IDRecebimento = Convert.ToInt32(((Label)((Control)sender).FindControl("IDRecebimentoGridViewLabel")).Text ?? "0");

            Session["objRecebimento"] = objRecebimento;

            Response.Redirect("~/Recebimento/DetalheWebForm.aspx?indmnu=5");
        }

        protected void RecebimentoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RecebimentoGridView.PageIndex = e.NewPageIndex;

            BuscarLinkButton_Click(null, null);
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