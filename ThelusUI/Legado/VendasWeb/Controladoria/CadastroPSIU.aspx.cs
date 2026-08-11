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

namespace VendasWeb.Controladoria
{
    public partial class CadastroPSIU : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        DocumentoPSIUClass documento = new DocumentoPSIUClass();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            //Deixando avisos invisiveis (para não misturar com operações passadas)
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;
            if (DocumentoFileUpload.HasFile)
            {

                //Pegamos informação do arquivo
                string stipoArquivo = Path.GetExtension(DocumentoFileUpload.PostedFile.FileName).ToLower();
                string NomeArquivo = NomeArquivoText.Value;
                string FileName = DocumentoFileUpload.PostedFile.FileName;

                switch (stipoArquivo.ToUpper())
                {
                    default:
                        try
                        {

                            string[] arquivos = Directory.GetFiles(Server.MapPath("~") + "\\Controladoria\\PSIU\\", FileName);

                            if (arquivos.Count() == 0)
                            {

                                DocumentoFileUpload.SaveAs(Server.MapPath("~") + "\\Controladoria\\PSIU\\" + FileName);

                                documento.endereco = "Controladoria\\PSIU\\" + FileName;
                                documento.nome = NomeArquivo;


                                //Salvando Documento
                                documento.Insere_Documento();
                                string sucesso = ("O documento foi enviado com suceso");
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(sucesso, true);
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                            }
                            else
                            {
                                //Mensagem de erro caso arquivo já exista
                                string erronome = ("Já existe um arquivo chamado " + FileName);
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erronome, true);
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                            }




                        }
                        catch
                        {
                            //Mensagem de erro caso arquivo não seja gravado
                            string fatalerror = ("Não foi possível carregar o arquivo " + FileName);
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(fatalerror, true);
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                        }
                        break;

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

        protected void PSIUGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            PSIUGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
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

        protected void _Click(object sender, EventArgs e)
        {


            //Pegando Caminho do Arquivo(para teste tirar o + "\\" +
            string DocEntPathArq = Server.MapPath("~") + ((Label)((Control)sender).FindControl("UrlLabel")).Text;


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

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            //DEIXANDO AVISOS INVISIVEIS (PARA EVITAR MISTURAR COM OPERAÇÕES PASSADAS)
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            string Url = ((Label)((Control)sender).FindControl("UrlLabel")).Text;
            string ID = ((Label)((Control)sender).FindControl("IDLabel")).Text;
            string CaminhoLocal = Server.MapPath("~") + Url;
            FileInfo fi = new System.IO.FileInfo(CaminhoLocal);
            documento.Deleta_Documento(Convert.ToInt32(ID));
            try
            {
                fi.Delete();
            }
            catch
            {
            }
            string exclusao = "O arquivo foi excluido com sucesso";
            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(exclusao, true);
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            this.BuscarButton_Click(null, null);

        }
    }

}