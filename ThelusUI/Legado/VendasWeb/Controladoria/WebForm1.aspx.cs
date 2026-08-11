using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using VendasWeb.classes;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Controladoria
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();
        }

        public void carregaDados()
        {

            string erro = "";

            try
            {
                gravaDocumento();

                //string arquivo = "E:\\Projetos\\CRM\\VendasWeb\\Controladoria\\Cenarios\\TestePlanilha.xlsx";
                string arquivo = Server.MapPath("~/Controladoria/Cenarios/TestePlanilha.xlsx");
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
                        output.Tables.Add(outputTable);
                        new OleDbDataAdapter(cmd).Fill(outputTable);
                    }

                    PedidosPeriodoGridView.DataSource = output.Tables[0];
                    PedidosPeriodoGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            carregaDados();
        }

        public void gravaDocumento()
        {
            //obtem o tamanho do arquivo
            int tamanho = FileUploadArquivo.PostedFile.ContentLength;
            //cria um array de bytes para armazenar os dados binários da imagem
            byte[] imgbyte = new byte[tamanho];
            //armazena a imagem selecinada na memória
            HttpPostedFile img = FileUploadArquivo.PostedFile;
            //define os dados binários
            img.InputStream.Read(imgbyte, 0, tamanho);

            //Pegando Informações do Arquivo
            FileInfo infoarquivo = new FileInfo(FileUploadArquivo.FileName);
            //Criando Caminho do arquivo
            string arquivo = Server.MapPath("~/Controladoria/Cenarios/TestePlanilha.xlsx");
            string pastaArquivo = arquivo;

            //Pegando informações do caminho do arquivo criado
            FileInfo arquivoServidor = new FileInfo(pastaArquivo);

            //Verificando se o arquivo existe
            if (arquivoServidor.Exists == true)
            {
                File.Delete(pastaArquivo);

            }


            //Salvamos o arquivo
            FileUploadArquivo.PostedFile.SaveAs(pastaArquivo);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            gravaDocumento();
        }
    }
}