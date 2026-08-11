using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Controladoria.Fretes
{
    public partial class CenarioCidadesWebForm : System.Web.UI.Page
    {
        FretesClass CenarioFrete = new FretesClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../FreteWebForm.aspx?indmnu=3");
        }

        protected void PlanilhaButton_Click(object sender, EventArgs e)
        {
            //Deixando avisos invisiveis (para não misturar com operações passadas)
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;
            if (DocumentoFileUpload.HasFile)
            {

                //Pegamos informação do arquivo
                string stipoArquivo = Path.GetExtension(DocumentoFileUpload.PostedFile.FileName).ToLower();
                string NomeArquivo = "Planilha";
                string FileName = (NomeArquivo + stipoArquivo);

                switch (stipoArquivo.ToUpper())
                {
                    default:
                        try
                        {

                            string[] arquivos = Directory.GetFiles(Server.MapPath("~") + "\\Controladoria\\Cenarios\\", FileName);

                            DocumentoFileUpload.SaveAs(Server.MapPath("~") + "\\Controladoria\\Cenarios\\" + FileName);
                            CenarioFrete.endereco = "Controladoria\\Cenarios\\" + FileName;
                            CarregaPlanilha();
                        }
                        catch
                        {
                            //Mensagem de erro caso arquivo não seja gravado
                            string fatalerror = ("Não foi possível carregar o arquivo ");
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(fatalerror, true);
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                        }
                        break;

                }

            }
            else
            {
                //Mensagem de erro caso arquivo não seja gravado
                string error = ("Por favor, envie um arquivo antes de tentar carrega-lo");
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(error, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void CarregaPlanilha()
        {
            string erro = "";

            try
            {
                //string arquivo = "E:\\Projetos\\CRM\\VendasWeb\\Controladoria\\Cenarios\\TestePlanilha.xlsx";
                string arquivo = Server.MapPath("~/Controladoria/Cenarios/Planilha.xlsx");
                string strConexao = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=0\"", arquivo);
                using (OleDbConnection conn = new OleDbConnection(strConexao))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    //Cria o objeto dataset para receber o conteúdo do arquivo Excel
                    DataSet output = new DataSet();
                    foreach (DataRow row in dt.Rows)
                    {
                        // obtem o noma da planilha corrente
                        string sheet = row["TABLE_NAME"].ToString();
                        // obtem todos as linhas da planilha corrente
                        OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                        cmd.CommandType = CommandType.Text;
                        // copia os dados da planilha para o datatable
                        DataTable outputTable = new DataTable(sheet);
                        //Adiciona a datatable
                        output.Tables.Add(outputTable);
                        new OleDbDataAdapter(cmd).Fill(outputTable);
                    }
                    DataColumnCollection collection = output.Tables[0].Columns;
                    //Verificando se a tabela contém os campos padrões
                    if (collection.Contains("Empresa") && collection.Contains("Cidade") && collection.Contains("ValorFrete"))
                    {
                        FreteGridView.DataSource = output.Tables[0];
                        FreteGridView.DataBind();
                        FreteMultiView.Visible = true;
                        Session["tabelaExcel"] = output.Tables[0];
                    }

                    else
                    {
                        string error = ("O arquivo enviado é inválido, por favor, verifique se as colunas estão com os nomes corretos");
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(error, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }
        }

        protected void BancoButton_Click(object sender, EventArgs e)
        {
            if (CenarioTextbox.Text != null)
            {
                if (FreteMultiView.Visible == true)
                {
                    //Conferindo se checkbox esta selecionada
                    CenarioFrete.nome = CenarioTextbox.Text;
                    CenarioFrete.tabela = (DataTable)Session["tabelaExcel"];
                    CenarioFrete.Salva_Cenario_Cidade();
                    string resultado = CenarioFrete.Salva_Cenario_Tabela_Cidade();
                    if (resultado != "erro")
                    {
                        if (PadraoCheck.Checked == true)
                        {
                            CenarioFrete.Define_Padrao_Cidade();
                        }
                        string sucess = ("A planilha foi enviada com sucesso");
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(sucess, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }
                    else
                    {
                        string error = ("Ocorreu um erro no envio da planilha, por favor verifique se ela esta dentro dos padrões");
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(error, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }
                }
                else
                {
                    //Mensagem de erro caso o arquivo não esteja carregado
                    string error = ("Por favor, carregue um arquivo antes de salva-lo;");
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(error, true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }

            else
            {
                //Mensagem de erro caso o campo nome esteja vazio
                string error = ("Por favor, dê um nome ao cenário antes de salva-lo");
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(error, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void PadraoButton_Click(object sender, EventArgs e)
        {
            //Buscando planilha padrão
            string DocEntPathArq = Server.MapPath("~/Controladoria/Cenarios/Padrao.xlsx");

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

        protected void FreteGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            FreteGridView.PageIndex = e.NewPageIndex;
            CarregaPlanilha();
        }
    }
}