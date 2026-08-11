using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb
{
    public partial class FrmHistoricoPSIU : System.Web.UI.Page
    {

        DocumentoPSIUClass documento = new DocumentoPSIUClass();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
         if (Session["usuario"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

            if (!IsPostBack)
            {
                BuscarButton_Click(null, null);
            }

        }

        protected void BaixarButton_Click(object sender, EventArgs e)
        {
            //Pegando Caminho do Arquivo(para teste tirar o + "\\" +
            string DocEntPathArq = Server.MapPath("~") + "\\" + ((Label)((Control)sender).FindControl("UrlLabel")).Text;


            //Lendo e Criando arquivo para Download
            System.IO.FileStream fs = new System.IO.FileStream(DocEntPathArq, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] ar = new byte[(int)fs.Length];
            fs.Read(ar, 0, (int)fs.Length);
            fs.Close();

            //Pegando nome do Arquivo
            string fileName = Path.GetFileName(fs.Name);

            //Enviando requisicao de Download
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.ContentType = "application/octectstream";
            Response.BinaryWrite(ar);
            Response.End();
        }

        protected void PSIUGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            PSIUGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {

            //Variável para determinar se a data inicial é nula
            bool datanula = false;
            //VALIDAÇÃO DE DATA INICIAL
            try
            {
                documento.DataInicial = Convert.ToDateTime(DateTextbox.Text);
            }

            catch
            {
                documento.DataInicial = Convert.ToDateTime("01/01/2000");
                datanula = true;

            }

            //VALIDAÇÃO DE DATA FINAL (CASO NULA RECEBE UM VALOR IGUAL A DATA INICIAL)
            try
            {
                documento.DataFinal = Convert.ToDateTime(DateUntillTextbox.Text);
                documento.DataFinal = documento.DataFinal.AddDays(1);
                this.AtualizaGrid();

            }

            catch
            {
                if (datanula == false)
                {
                    documento.DataFinal = documento.DataInicial.AddDays(1);
                    this.AtualizaGrid();
                }
                else
                {
                    documento.DataFinal = Convert.ToDateTime("01/01/2100");
                    this.AtualizaGrid();
                }


            }

        }

        public void AtualizaGrid()
        {
            documento.nome = NomeArquivoText.Value;
            DataTable outpout = new DataTable();
            outpout = documento.Exibir_Documento();
            PSIUGridView.DataSource = outpout;
            PSIUGridView.DataBind();
            PSIUMultiView.Visible = true;



        }

    }
}