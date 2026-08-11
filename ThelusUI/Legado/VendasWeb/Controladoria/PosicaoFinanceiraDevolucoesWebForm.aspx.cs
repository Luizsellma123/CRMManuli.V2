using System;
using System.IO;
using System.Data;
using System.Text;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;

namespace VendasWeb.Controladoria
{
    public partial class PosicaoFinanceiraDevolucoesWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ControladoriaClass objControladoriaClass = new ControladoriaClass();

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
            if (Session["PosicaoFinanceiraDetalhe"] != null)
                objControladoriaClass = (ControladoriaClass)Session["PosicaoFinanceiraDetalhe"];

            PosicaoTextBox.Text = objControladoriaClass.IDPosicaoDiaria.ToString();

            UsuarioTextBox.Text = objControladoriaClass.Usuario;

            CarregaCombos();

            BuscarButton_Click(null, null);
        }

        protected void CarregaCombos()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            GrupoDropDownList.DataSource = objControladoriaClass.Consulta_POSICAO_DIARIA_FILTRO_GRUPOS();
            GrupoDropDownList.DataTextField = "TextoApresentacao";
            GrupoDropDownList.DataValueField = "CodigoSAP";
            GrupoDropDownList.DataBind();
        }

        protected void CarregaDadosDaTela()
        {
            objControladoriaClass.IDPosicaoDiaria = Convert.ToInt32(PosicaoTextBox.Text);

            objControladoriaClass.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);

            objControladoriaClass.Status = StatusDropDownList.SelectedItem.Text;

            objControladoriaClass.Cliente = ClienteTextBox.Text;

            objControladoriaClass.IDGrupo = Convert.ToInt32(GrupoDropDownList.SelectedValue);
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            PosicaoFinanceiraGridView.DataSource = objControladoriaClass.Consulta_CRM_POSICAO_DIARIA_DEVOLUCOES(0);
            PosicaoFinanceiraGridView.DataBind();
            PosicaoFinanceiraMultiView.Visible = true;
        }

        protected void ExcelCompletoLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            DataTable Excel = objControladoriaClass.Consulta_CRM_POSICAO_DIARIA_DEVOLUCOES(1);

            string tabelaHTML = objControladoriaClass.MontaTabelaHtmlDoExcel(Excel);

            SalvaTabelaHTMLComoExcel(tabelaHTML, "ConsolidadoGeralDevolucoes");
        }

        protected void SalvaTabelaHTMLComoExcel(string tabelaHTML, string nome)
        {
            MemoryStream stream = new MemoryStream();

            // Converta a string HTML em um fluxo de memória
            byte[] byteArray = Encoding.UTF8.GetBytes(tabelaHTML);
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Position = 0;

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=" + nome + ".xls");

            // Copie o conteúdo do fluxo de memória para o fluxo de resposta
            stream.WriteTo(Response.OutputStream);

            Response.End();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PosicaoFinanceiraResumoWebForm.aspx?indmnu=3");
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
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void PosicaoFinanceiraGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PosicaoFinanceiraGridView.PageIndex = e.NewPageIndex;

            BuscarButton_Click(null, null);
        }
    }
}