using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using System.IO;
using System.Web.UI.WebControls;

namespace VendasWeb.GerencialVendas
{
    public class DocEntidadeClass : clsConexao
    {
        public string EntCod { get; set; }
        public string DocEntPathArq { get; set; }
        public string DocEntObs { get; set; }
        public string UsuCod { get; set; }
        public byte[] DocEntImage { get; set; }
        public int DocEntSeq { get; set; }
        public string DocEntData { get; set; }

        public FileUpload ArquivoFileUpload { get; set; }
        public string ArquivoMsg { get; set; }
        public int USER_TB_Tipos_AnexosID { get; set; }
        public string NomeTipoAnexo { get; set; }

        public string Incluir_DocEntidade()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    dbCommand = new SqlCommand("USER_SP_INSERE_DOC_ENTIDADE", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 30, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@DocEntPathArq", SqlDbType.VarChar, 800, "DocEntPathArq"));
                    dbCommand.Parameters.Add(new SqlParameter("@DocEntObs", SqlDbType.VarChar, 800, "DocEntObs"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@DocEntImage", SqlDbType.Image, 0, "DocEntImage"));
                    dbCommand.Parameters.Add(new SqlParameter("@USER_TB_Tipos_AnexosID", SqlDbType.Int, 0, "USER_TB_Tipos_AnexosID"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@DocEntPathArq"].Value = DocEntPathArq;
                    dbCommand.Parameters["@DocEntObs"].Value = DocEntObs;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@DocEntImage"].Value = DocEntImage;
                    dbCommand.Parameters["@USER_TB_Tipos_AnexosID"].Value = USER_TB_Tipos_AnexosID;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    /*if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["msg"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Incluir_Endereco_Entrega";
                    }*/
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Incluir_Endereco_Entrega. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Remover_DocEntidade()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    dbCommand = new SqlCommand("USER_SP_REMOVER_DOC_ENTIDADE", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 30, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@DocEntSeq", SqlDbType.VarChar, 30, "DocEntSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@DocEntSeq"].Value = DocEntSeq;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["msg"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Remover_DocEntidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Remover_DocEntidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Doc_Excluir_Todos()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_Doc_Excluir_Todos", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Doc_Excluir_Todos. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable Consulta_DocEntidade()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_DOC_ENTIDADE", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 30, "EntCod"));           
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    
                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {

            }

            return outputTable;
        }

        public bool Salvar_Arquivo()
        {
            clsEntidades ObjEntidadesClass = new clsEntidades();
            bool retorno = false;

            if (ArquivoFileUpload.HasFile)
            {

                string stipoArquivo = Path.GetExtension(ArquivoFileUpload.PostedFile.FileName).ToLower();

                switch (stipoArquivo.ToUpper())
                {
                    case ".PDF":

                        try
                        {

                            //obtem o tamanho do arquivo
                            int tamanho = ArquivoFileUpload.PostedFile.ContentLength;
                            //cria um array de bytes para armazenar os dados binários da imagem
                            byte[] imgbyte = new byte[tamanho];
                            //armazena a imagem selecinada na memória
                            HttpPostedFile img = ArquivoFileUpload.PostedFile;
                            //define os dados binários
                            img.InputStream.Read(imgbyte, 0, tamanho);

                            /*UsuCartaoCnpjLabel.Text = "Arquivo: " + UsuCartaoCnpjFileUpload.FileName + " Carregado";
                            UsuCartaoCnpjLabel.ForeColor = System.Drawing.Color.Green;*/


                            #region Salvando Arquivo

                            //Pegando Informações do Arquivo
                            FileInfo infoarquivo = new FileInfo(ArquivoFileUpload.FileName);
                            //Criando Caminho do arquivo
                            string pastaArquivo = DocEntPathArq;

                            //Pegando informações do caminho do arquivo criado
                            FileInfo arquivoServidor = new FileInfo(pastaArquivo);

                            //Verificando se o arquivo existe
                            if (arquivoServidor.Exists == true)
                            {
                                File.Delete(pastaArquivo);

                            }


                            //Salvamos o arquivo
                            ArquivoFileUpload.PostedFile.SaveAs(pastaArquivo);



                            #endregion

                            /*Carregando Dados a Serem Salvos Futuramente*/
                            DocEntImage = imgbyte;
                            DocEntPathArq = pastaArquivo;

                            ObjEntidadesClass.DocEntImage = DocEntImage;


                            ObjEntidadesClass.Remove_DocEntidade(this);
                            ObjEntidadesClass.Adiciona_DocEntidade(this);

                            retorno = true;
                            ArquivoMsg = "Arquivo carregado com Sucesso!";

                        }
                        catch
                        {
                            ObjEntidadesClass.Remove_DocEntidade(this);
                            retorno = false;

                            ArquivoMsg = "Erro ao carregar o arquivo: " + ArquivoFileUpload.FileName;
                        }
                        break;

                    default:
                        retorno = false;

                        ArquivoMsg = "Erro Tipo de Arquivo invalido. Arquivos Validos: PDF";
                        break;

                }


            }



            return retorno;
        }

        public DataTable Consulta_Tipos_Anexos()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_Tipos_Anexos", dbConnection);


                    dbCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }



    }

}