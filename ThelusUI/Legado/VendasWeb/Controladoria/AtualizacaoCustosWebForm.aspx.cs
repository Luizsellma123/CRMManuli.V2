using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Controladoria
{
    public partial class AtualizacaoCustosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        CustosClass OBJCustos = new CustosClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";
            }
        }

        public void ModeloButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            //Get properties using reflection.
            DataSet ds = new DataSet("New_DataSet");

            DataTable dt = new DataTable("ModeloImportacao");
            //dt.Columns.Add("IDCusto");
            dt.Columns.Add("CodigoProduto");
            dt.Columns.Add("NomeProduto");
            dt.Columns.Add("Comprimento");
            dt.Columns.Add("Largura");
            dt.Columns.Add("FC");
            dt.Columns.Add("FCConvertido");
            dt.Columns.Add("Custo");
            dt.Columns.Add("Material");
            dt.Columns.Add("Percentual");
            dt.Columns.Add("PrazoProducao");

            ClienteClasse objClienteClasse = new ClienteClasse();

            foreach (DataRow row in objClienteClasse.CarregaClassificacaoComercial().Rows)
            {
                if (row["IDClassificacaoComercial"].ToString() != "0")
                    dt.Columns.Add(Regex.Replace(removerAcentos(row["Descricao"].ToString()), "[^0-9a-zA-Z]+", ""));
            }

            //Resolve problema: O Excel encontrou conteúdo ilegível / Invalid or corrupt file (unreadable content)
            for (int i = 0; i < 100; i++)
            {
                dt.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            }

            ds.Tables.Add(dt);

            MemoryStream stream = new MemoryStream();
            ExcelLibrary.DataSetHelper.CreateWorkbook(stream, ds);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Modelo.xls", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));

            stream.WriteTo(Response.OutputStream);

            Response.End();
        }

        public string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        protected void CarregaCustosLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            string extension = "";
            string nome = "";
            string Strsql = "";
            string CaminhoServidor = "";
            string LocalizacaoArquivo = "";
            string Conexao = "";


            try
            {
                //Verifica se tem arquivo anexo para enviar
                if (ArquivoFileUpload.HasFile == true)
                {
                    extension = System.IO.Path.GetExtension(ArquivoFileUpload.FileName);
                    nome = System.IO.Path.GetFileName(ArquivoFileUpload.FileName);

                    if (extension == ".csv")
                    {

                        CaminhoServidor = Server.MapPath("Cenarios") + "\\" + ArquivoFileUpload.FileName;

                        if (System.IO.File.Exists(CaminhoServidor))
                        {
                            System.IO.File.Delete(CaminhoServidor);
                        }

                        ArquivoFileUpload.SaveAs(CaminhoServidor);
                    }
                    else
                    {
                        erro = "Somente permitido com a extensão .CSV !";
                    }
                }
                else
                {
                    erro = "Favor selecionar um arquivo.";
                }

                if (erro == "")
                {
                    LocalizacaoArquivo = Server.MapPath("Cenarios") + "\\";
                    Strsql = "SELECT * FROM [" + ArquivoFileUpload.FileName + "] where CodigoProduto<>'' and CodigoProduto<>null";

                    //Criar Schema em tmepo de execução
                    CriaSchemaExecucao(LocalizacaoArquivo);

                    Conexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + LocalizacaoArquivo + ";" + "Extended Properties=\"text;HDR=YES;FMT=Delimited\"";

                    OleDbDataAdapter ConexaoCSV = new OleDbDataAdapter(Strsql, Conexao);


                    DataTable DadosCSV = new DataTable();
                    ConexaoCSV.Fill(DadosCSV);

                    Session["DadosCSV"] = DadosCSV;

                    CustosGridView.DataSource = DadosCSV;
                    CustosGridView.DataBind();
                    CustosMultiView.Visible = true;
                }
            }
            catch (Exception ex)
            {
                erro = "Ocorreu um erro na leitura do arquivo.";
            }

            //Trata se ocorreu algum erro
            if (erro != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void CustosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CustosGridView.PageIndex = e.NewPageIndex;
            AtualizaGrid();
        }

        public void AtualizaGrid()
        {
            DataTable DadosCSV = new DataTable();

            if (Session["DadosCSV"] != null)
            {
                DadosCSV = (DataTable)Session["DadosCSV"];

                CustosGridView.DataSource = DadosCSV;
                CustosGridView.DataBind();
                CustosMultiView.Visible = true;
            }
        }

        protected void AtualizarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            DataTable DadosCSV = new DataTable();

            if (Session["DadosCSV"] != null)
            {
                DadosCSV = (DataTable)Session["DadosCSV"];
                OBJCustos.Empresa = Convert.ToInt32(EmpresaDropDown.SelectedValue);
                OBJCustos.Dados = DadosCSV;

                erro = OBJCustos.gravaDadosPrincipais();

                if (erro == "") erro = OBJCustos.gravaClassificacaoComercial();

            }
            else
            {
                erro = "Favor carregar os dados antes de atualizar.";
            }

            //Se não deu erro atualiza no sistema.
            if (erro == "")
            {
                Session["Msg"] = "Atualização custos efetuada com sucesso!";
                Session["DadosCSV"] = null;
                Response.Redirect("AtualizacaoCustosWebForm.aspx?indmnu=3");
            }

            //Se tem algum erro mostra mensagem
            if (erro != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        public void CriaSchemaExecucao(string LocalizacaoArquivo)
        {
            //Deleta Schema.ini
            if (System.IO.File.Exists(LocalizacaoArquivo + "schema.ini"))
            {
                System.IO.File.Delete(LocalizacaoArquivo + "schema.ini");
            }

            FileInfo fileinfo = new FileInfo(ArquivoFileUpload.PostedFile.FileName);
            FileStream filestr = new FileStream(LocalizacaoArquivo + "schema.ini", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(filestr);

            writer.WriteLine("[" + fileinfo.Name + "]");
            writer.WriteLine("ColNameHeader=True");
            writer.WriteLine("Format=Delimited(;)");
            writer.WriteLine("Col1=CodigoProduto Text");
            writer.WriteLine("Col2=NomeProduto Text");
            writer.WriteLine("Col3=Comprimento Text");
            writer.WriteLine("Col4=Largura Text");
            writer.WriteLine("Col5=FC Double");
            writer.WriteLine("Col6=FCConvertido Double");
            writer.WriteLine("Col7=Custo Double");
            writer.WriteLine("Col8=Material Text");
            writer.WriteLine("Col9=Percentual Double");
            writer.WriteLine("Col10=PrazoProducao Text");
            writer.WriteLine("Col11=DISTRIBUIDOR Text");
            writer.WriteLine("Col12=INDUSTRIA Text");
            writer.WriteLine("Col13=REVENDA Text");
            writer.Close();
            writer.Dispose();
        }
    }
}