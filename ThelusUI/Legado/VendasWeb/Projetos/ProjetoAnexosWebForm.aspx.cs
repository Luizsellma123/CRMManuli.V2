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

namespace VendasWeb.Chamados
{
    public partial class ProjetoAnexosWebForm : System.Web.UI.Page
    {
        ChamadoClass OBJChamado = new ChamadoClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (Session["OBJChamado"] != null)
            {
                //Descarega a session Financeiro
                OBJChamado = (ChamadoClass)Session["OBJChamado"];
            }

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                //Carrega Combos
                CarregaCombos();

                //Carrega dados na tela
                CarregaDadosNaTela();
            }

        }

        public void CarregaDadosNaTela()
        {
            //recupera dados principais da tela
            OBJChamado.RecuperaDadosPrincipais();

            SolicitanteDropDownList.SelectedValue = OBJChamado.IDUsuarioSolicitante.ToString();
            NumeroChamadoTextBox.Text = OBJChamado.NumeroChamado.ToString();

            //Carga Inicial
            CarregaDadosGrid();
        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            Resultado = OBJChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataSource = Resultado;
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();

        }

        public void CarregaDadosGrid()
        {
            AnexosGridView.DataSource = OBJChamado.RecuperaDadosAnexos();
            AnexosGridView.DataBind();
            AnexosMultiView.Visible = true;
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            OBJChamado.DescricaoArquivo = AssuntoBreveTextBox.Text;
            OBJChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);
            erro = OBJChamado.GravaArquivoServidor(ArquivoFileUpload);

            if (erro == "")
            {
                erro = OBJChamado.GravaDadosAnexosChamado();
            }

            if (erro == "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Arquivo anexado com sucesso!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                AssuntoBreveTextBox.Text = "";
                ArquivoFileUpload.Dispose();

                CarregaDadosGrid();

            }else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro("Erro na inclusão do chamado!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void AnexosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AnexosGridView.PageIndex = e.NewPageIndex;
            CarregaDadosGrid();
        }

        protected void BaixarLinkButton_Click(object sender, EventArgs e)
        {
            string Caminho = Convert.ToString(((Label)((Control)sender).FindControl("CaminhoDestinoLabel")).Text ?? "");
            string NomeArquivo = Convert.ToString(((Label)((Control)sender).FindControl("NomeArquivoLabel")).Text ?? "");

            byte[] bytesInStream = System.IO.File.ReadAllBytes(Caminho);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename="+ NomeArquivo + "");
            Response.BinaryWrite(bytesInStream);
            Response.End();

        }

        protected void ExcluirAnexoLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            int IDAnexo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDAnexoCRM")).Text ?? "0");
            string DescricaoArquivo = Convert.ToString(((Label)((Control)sender).FindControl("DescricaoLabel")).Text ?? "");

            OBJChamado.IDAnexo = IDAnexo;
            OBJChamado.DescricaoArquivo = DescricaoArquivo;
            erro = OBJChamado.ExcluiDadosAnexosChamado();

            if (erro == "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Arquivo excluído com sucesso!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                CarregaDadosGrid();
            }else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro("Erro na inclusão do chamado!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }

        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Projetos/ListaProjetosWebForm.aspx?indmnu=5");
        }
    }
}