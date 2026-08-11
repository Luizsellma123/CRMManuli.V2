using System;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class SuperClasseDadosSerasa : clsConexao
    {
        /*
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }
        */

        public string PREFIXO { get; set; }

        public string erro { get; set; }

        public DataTable outputTable { get; set; }

        public SuperClasseDadosSerasa()
        {
            erro = "";

            outputTable = new DataTable();
        }

        public void GeraPREFIXO(string LINHA, string IDINF, string BCFIC, string TPINF)
        {
            EliminaEspacos();

            PREFIXO = LINHA + IDINF + BCFIC + TPINF;
        }

        public virtual string GravaDados(int IDCliente = 0, int IDAnalise = 0)
        {
            return "";
        }

        public string EliminaEspacos()
        {
            string erro = "";

            try
            {
                Type tipoDaClasse = GetType();
                PropertyInfo[] propriedades = tipoDaClasse.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (var propriedade in propriedades)
                {
                    if (propriedade != null)
                    {
                        // Verifica se a propriedade é do tipo string
                        if (propriedade.PropertyType == typeof(string) && propriedade.CanWrite)
                        {
                            // Obtem o valor atual da propriedade
                            string valorAtual = (string)propriedade.GetValue(this, null);

                            // Modifica o valor da propriedade
                            if (valorAtual != null) propriedade.SetValue(this, valorAtual.TrimEnd(), null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return erro;
        }

        public string ApagaTabelasCasoDeErro(int IDCliente, int IDAnalise)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_ANALISE_SERASA_TABELAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

    }
}