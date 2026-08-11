using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Recebimento
{
    public partial class AnexosWebForm : System.Web.UI.Page
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

            StatusDropDownList.DataSource = objRecebimento.ConsultaStatus();
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataBind();
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["objRecebimento"] != null)
            {
                objRecebimento = (RecebimentoClass)Session["objRecebimento"];

                objRecebimento.CarregaRecebimento();

                EmpresaDropDownList.SelectedValue = objRecebimento.IDEmpresa.ToString();

                IDRecebimentoTextBox.Text = objRecebimento.IDRecebimento.ToString();

                StatusDropDownList.SelectedValue = objRecebimento.IDStatus.ToString();

                DataTextBox.Text = objRecebimento.DataCriacao.ToString("yyyy-MM-dd");

                CarregaDadosGrid();
            }
        }

        protected string CarregaDadosDaTela()
        {
            try
            {
                objRecebimento.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);

                objRecebimento.IDRecebimento = Convert.ToInt32(IDRecebimentoTextBox.Text);

                objRecebimento.IDUsuarioLogado = Convert.ToInt32(Session["IDUsuario"]);

                objRecebimento.DescricaoArquivo = DescricaoTextBox.Text;

                if (objRecebimento.DescricaoArquivo == "") return "Informe a descrição do arquivo.";

                if (ArquivoFileUpload.HasFile == false) return "Selecione o arquivo a ser anexado.";

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

            if (erro == "") erro = objRecebimento.GravaArquivoServidor(ArquivoFileUpload);

            if (erro == "") erro = objRecebimento.GravaDadosAnexos();

            if (erro == "")
            {
                DescricaoTextBox.Text = "";

                ArquivoFileUpload.Dispose();

                CarregaDadosGrid();
            }

            ApresentaMensagem(erro);
        }

        protected void ExcluirAnexoLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";            

            CarregaDadosDaTela();

            objRecebimento.IDAnexo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDAnexoCRM")).Text);

            objRecebimento.NomeArquivo = ((Label)((Control)sender).FindControl("NomeArquivoLabel")).Text;

            objRecebimento.DescricaoArquivo = ((Label)((Control)sender).FindControl("DescricaoLabel")).Text;

            erro = objRecebimento.ExcluiDadosAnexosServidor();

            if (erro == "") erro = objRecebimento.ExcluiDadosAnexos();

            if (erro == "")
            {
                DescricaoTextBox.Text = "";

                ArquivoFileUpload.Dispose();

                CarregaDadosGrid();
            }

            ApresentaMensagem(erro);
        }

        protected void BaixarLinkButton_Click(object sender, EventArgs e)
        {
            string Caminho = Convert.ToString(((Label)((Control)sender).FindControl("CaminhoDestinoLabel")).Text);

            string NomeArquivo = Convert.ToString(((Label)((Control)sender).FindControl("NomeArquivoLabel")).Text);

            byte[] bytesInStream = System.IO.File.ReadAllBytes(Caminho);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename=" + NomeArquivo + "");
            Response.BinaryWrite(bytesInStream);
            Response.End();
        }

        public void CarregaDadosGrid()
        {
            AnexosGridView.DataSource = objRecebimento.RecuperaDadosAnexos();
            AnexosGridView.DataBind();
            AnexosMultiView.Visible = true;
        }

        protected void AnexosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AnexosGridView.PageIndex = e.NewPageIndex;
            CarregaDadosGrid();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Recebimento/DetalheWebForm.aspx?indmnu=5");
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