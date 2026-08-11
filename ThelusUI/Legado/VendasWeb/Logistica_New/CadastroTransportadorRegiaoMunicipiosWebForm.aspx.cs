using System;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;
using System.Web.UI;

namespace VendasWeb.Logistica_New
{
    public partial class CadastroTransportadorRegiaoMunicipiosWebForm : System.Web.UI.Page
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

                CarregaCombos();

                CarregaGridView();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["CadastroTransportador"] != null)
                objLogistica = (LogisticaClass)Session["CadastroTransportador"];

            IDTransportadorHiddenField.Value = objLogistica.IDTransportador.ToString();

            TransportadorTextBox.Text = objLogistica.Descricao;

            IDRegiaoHiddenField.Value = objLogistica.IDRegiao.ToString();

            RegiaoTextBox.Text = objLogistica.DescricaoRegiao;
        }

        protected void CarregaCombos()
        {
            objCliente.IDPais = PaisDropDownList.SelectedValue;

            objCliente.IDEstado = "0";

            EstadoDropDownList.DataSource = objCliente.RetornaListaEstados();
            EstadoDropDownList.DataTextField = "Nome";
            EstadoDropDownList.DataValueField = "IDEstado";
            EstadoDropDownList.DataBind();

            EstadoDropDownList_SelectedIndexChanged(null, null);
        }

        protected void EstadoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            objCliente.IDPais = PaisDropDownList.SelectedValue;

            objCliente.IDEstado = EstadoDropDownList.SelectedValue;

            objCliente.IDMunicipio = "0";

            MunicipioDropDownList.DataSource = objCliente.RetornaListaMunicipios();
            MunicipioDropDownList.DataValueField = "IDMunicipio";
            MunicipioDropDownList.DataTextField = "NomeMunicipio";
            MunicipioDropDownList.DataBind();
        }

        protected void CarregaGridView()
        {
            objLogistica.IDTransportador = Convert.ToInt32(IDTransportadorHiddenField.Value);

            objLogistica.IDRegiao = Convert.ToInt32(IDRegiaoHiddenField.Value);

            objLogistica.IDPais = 0;

            objLogistica.IDEstado = 0;

            objLogistica.IDMunicipio = 0;

            GridView.DataSource = objLogistica.RetornaListaTransportadorRegiaoMunicipio();
            GridView.DataBind();
            MultiView.Visible = true;
        }

        protected void CarregaIDsHiddenField()
        {
            objLogistica.IDTransportador = Convert.ToInt32(IDTransportadorHiddenField.Value);

            objLogistica.IDRegiao = Convert.ToInt32(IDRegiaoHiddenField.Value);
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            CarregaIDsHiddenField();

            objLogistica.IDPais = Convert.ToInt32(PaisDropDownList.SelectedValue);

            objLogistica.IDEstado = Convert.ToInt32(EstadoDropDownList.SelectedValue);

            objLogistica.IDMunicipio = Convert.ToInt32(MunicipioDropDownList.SelectedValue);

            if (Session["IDUsuario"] != null) objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());

            if (erro == "") erro = objLogistica.GravaTransportadorRegiaoMunicipio();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void CarregaDadosGridView(object sender, EventArgs e)
        {
            CarregaIDsHiddenField();

            objLogistica.IDPais = Convert.ToInt32(((Label)((Control)sender).FindControl("IDPaisGridViewLabel")).Text);

            objLogistica.IDEstado = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEstadoGridViewLabel")).Text);

            objLogistica.IDMunicipio = Convert.ToInt32(((Label)((Control)sender).FindControl("IDMunicipioGridViewLabel")).Text);
        }

        protected void ExcluirGridViewLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosGridView(sender, e);

            if (Session["IDUsuario"] != null) objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());

            string erro = objLogistica.ExcluiTransportadorRegiaoMunicipio();

            if (erro == "") CarregaGridView();

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
            Response.Redirect("~/Logistica_New/CadastroTransportadorRegiaoWebForm.aspx?indmnu=5");
        }
    }
}