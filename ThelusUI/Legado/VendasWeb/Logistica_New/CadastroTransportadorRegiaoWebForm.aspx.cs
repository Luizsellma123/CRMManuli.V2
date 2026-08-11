using System;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;
using System.Web.UI;

namespace VendasWeb.Logistica_New
{
    public partial class CadastroTransportadorRegiaoWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass objSessao = new SessionClass();
        LogisticaClass objLogistica = new LogisticaClass();
        ClienteClasse objCliente = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();

                CarregaGridView();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["CadastroTransportador"] != null)
                objLogistica = (LogisticaClass)Session["CadastroTransportador"];

            objLogistica.Filtro = objLogistica.IDTransportador.ToString();

            objLogistica.TipoFiltro = "Detalhe";

            DataTable ListaTransportador = objLogistica.RetornaListaTransportador();

            if (ListaTransportador.Rows.Count > 0)
            {
                foreach (DataRow row in ListaTransportador.Rows)
                {
                    IDTransportadorHiddenField.Value = row["IDTransportador"].ToString();
                    TransportadorTextBox.Text = row["Descricao"].ToString();
                }
            }
        }

        protected void CarregaGridView()
        {
            objLogistica.IDTransportador = Convert.ToInt32(IDTransportadorHiddenField.Value);

            objLogistica.IDRegiao = 0;

            objLogistica.CodigoRegiao = "";

            objLogistica.Descricao = "";

            GridView.DataSource = objLogistica.RetornaListaTransportadorRegiao();
            GridView.DataBind();
            MultiView.Visible = true;
        }

        protected void CarregaIDTransportador()
        {
            objLogistica.IDTransportador = Convert.ToInt32(IDTransportadorHiddenField.Value);
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            CarregaIDTransportador();

            if (CodigoRegiaoTextBox.Text == "")
                erro = "Informe a região.";
            else
                objLogistica.CodigoRegiao = CodigoRegiaoTextBox.Text;

            if (DescricaoRegiaoTextBox.Text == "")
                erro = "Informe a descrição da região.";
            else
                objLogistica.DescricaoRegiao = DescricaoRegiaoTextBox.Text;

            if (Session["IDUsuario"] != null) objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());

            if (erro == "") erro = objLogistica.GravaTransportadorRegiao();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void CarregaDadosGridView(object sender, EventArgs e)
        {
            CarregaIDTransportador();

            objLogistica.Descricao = TransportadorTextBox.Text;

            objLogistica.IDRegiao = Convert.ToInt32(((Label)((Control)sender).FindControl("IDRegiaoGridViewLabel")).Text);

            objLogistica.DescricaoRegiao = ((Label)((Control)sender).FindControl("RegiaoGridViewLabel")).Text;
        }

        protected void ExcluirGridViewLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosGridView(sender, e);

            string erro = objLogistica.ExcluiTransportadorRegiao();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void MunicipiosGridViewLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosGridView(sender, e);

            Session["CadastroTransportador"] = objLogistica;

            Response.Redirect("~/Logistica_New/CadastroTransportadorRegiaoMunicipiosWebForm.aspx?indmnu=5");
        }

        protected void ParametrosGridViewLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosGridView(sender, e);

            Session["CadastroTransportador"] = objLogistica;

            Response.Redirect("~/Logistica_New/CadastroTransportadorRegiaoParametrosWebForm.aspx?indmnu=5");
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
            Response.Redirect("~/Logistica_New/CadastroTransportadorDetalheWebForm.aspx?indmnu=5");
        }
    }
}