using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.SAC
{
    public partial class AtividadesAnexoWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();
        SACClass ObjSAC = new SACClass();
        setor ObjSetor = new setor();

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

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            EmpresaDropDownList.Enabled = false;
            ClienteTextBox.Enabled = false;
            TicketTextBox.Enabled = false;
            SetorDropDownList.Enabled = false;
            AtividadeTextBox.Enabled = false;

            usuario ObjUsuario = new usuario();
            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            ObjSetor.Filtro = "";
            ObjSetor.Status = "";
            SetorDropDownList.DataSource = ObjSetor.ListaSetores();
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataBind();

            if (Session["AtividadesDetalhe"] != null) ObjSAC = (SACClass)Session["AtividadesDetalhe"];

            EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
            ClienteTextBox.Text = ObjSAC.Cliente;
            TicketTextBox.Text = ObjSAC.IDTicket.ToString();
            SetorDropDownList.SelectedValue = ObjSAC.IDSetor.ToString();
            AtividadeTextBox.Text = ObjSAC.IDAtividade.ToString();

            ObjSAC.Tela = "Lista";

            DescricaoTextBox.Text = "";
            ArquivoFileUpload.Dispose();

            CarregaDadosGrid();
        }

        protected void CarregaDadosGrid()
        {
            AnexosGridView.DataSource = ObjSAC.RetornaListaTicketsAnexo();
            AnexosGridView.DataBind();
            AnexosMultiView.Visible = true;
        }

        protected void AdicionarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["AtividadesDetalhe"] != null) ObjSAC = (SACClass)Session["AtividadesDetalhe"];

            ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            //ObjSAC.IDEmpresa
            //ObjSAC.IDTicket
            ObjSAC.DescricaoArquivo = DescricaoTextBox.Text;

            erro = ObjSAC.DescricaoArquivo == "" ? "Digite uma descrição para o arquivo." : "";
            if (erro == "") erro = ArquivoFileUpload == null ? "Faça upload do arquivo." : "";
            if (erro == "") erro = ObjSAC.GravaArquivoServidor(ArquivoFileUpload);
            if (erro == "") erro = ObjSAC.GravaDadosTicketAnexos();
            if (erro == "") ObjSAC.TipoHistorico = "Inclusao Anexo";
            if (erro == "") erro = ObjSAC.GravaTicketHistorico();
            if (erro == "") ObjSAC.NomeUsuario = Session["usuario"].ToString();
            if (erro == "") erro = ObjSAC.EnviaEmailAnexos();
            if (erro != "") ApresentaMensagem(erro);
            else CarregaDadosGrid();
        }

        protected void ExcluirAnexoLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["AtividadesDetalhe"] != null) ObjSAC = (SACClass)Session["AtividadesDetalhe"];

            ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            ObjSAC.IDAnexo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDAnexoLabel")).Text);
            ObjSAC.DescricaoArquivo = ((Label)((Control)sender).FindControl("DescricaoLabel")).Text;

            erro = ObjSAC.ExcluiDadosTicketAnexos();
            if (erro == "") ObjSAC.TipoHistorico = "Exclusao Anexo";
            if (erro == "") erro = ObjSAC.GravaTicketHistorico();
            if (erro == "") ObjSAC.NomeUsuario = Session["usuario"].ToString();
            if (erro == "") erro = ObjSAC.EnviaEmailAnexos();
            if (erro == "") erro = ObjSAC.ExcluiArquivoServidor();

            if (erro != "") ApresentaMensagem(erro);
            else CarregaDadosGrid();
        }

        protected void BaixarLinkButton_Click(object sender, EventArgs e)
        {
            string Caminho = ((Label)((Control)sender).FindControl("CaminhoDestinoLabel")).Text;
            string NomeArquivo = ((Label)((Control)sender).FindControl("ArquivoLabel")).Text;

            byte[] bytesInStream = System.IO.File.ReadAllBytes(Caminho);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename=" + NomeArquivo + "");
            Response.BinaryWrite(bytesInStream);
            Response.End();
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

        protected void AnexosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AnexosGridView.PageIndex = e.NewPageIndex;
            CarregaDadosGrid();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/AtividadesDetalheWebForm.aspx?indmnu=3");
        }

    }
}