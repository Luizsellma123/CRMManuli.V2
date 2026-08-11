using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.PortalColaborador.Classes;

namespace VendasWeb.PortalColaborador.DocumentosGerais
{
    public partial class DocumentosGeraisWebForm : System.Web.UI.Page
    {
        DocumentosGeraisClass objDocumentosGeraisClass = new DocumentosGeraisClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

            if (!IsPostBack)
            {
                BuscarButton_Click(null, null);
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            objDocumentosGeraisClass.Descricao = DescricaoTextBox.Text.Trim();

            DocumentosGridView.DataSource = objDocumentosGeraisClass.CarregaListaDocumentos();
            DocumentosGridView.DataBind();
            DocumentosMultiView.Visible = true;
        }

        protected void BaixarLinkButton_Click(object sender, EventArgs e)
        {
            string Caminho = Convert.ToString(((Label)((Control)sender).FindControl("CaminhoArquivoLabel")).Text);
            string NomeArquivo = Convert.ToString(((Label)((Control)sender).FindControl("ArquivoLabel")).Text);

            byte[] bytesInStream = System.IO.File.ReadAllBytes(Caminho);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename=" + NomeArquivo + "");
            Response.BinaryWrite(bytesInStream);
            Response.End();
        }

        protected void DocumentosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DocumentosGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }
    }
}