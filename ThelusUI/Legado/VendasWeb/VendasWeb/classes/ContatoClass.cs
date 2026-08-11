using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace VendasWeb.GerencialVendas
{

    public class ContatoClass : clsConexao
    {

        public int ENTCONTATOID { get; set; }
        public string Empresa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string DDDTelefone { get; set; }
        public string Telefone { get; set; }

        public string DDDCelular { get; set; }
        public string Celular { get; set; }

        public string TipoContato { get; set; }
        public string Ramal { get; set; }
        public string EntCod { get; set; }
        public string Cargo { get; set; }
        public string UsuCod { get; set; }
        public string TipoOperacao { get; set; }

        public string Incluir_Contato()
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

                    dbCommand = new SqlCommand("USER_SP_INSERE_ENT_CONTATO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 300, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 300, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@DDDTelefone", SqlDbType.VarChar, 10, "DDDTelefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 20, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ramal", SqlDbType.VarChar, 20, "Ramal"));

                    dbCommand.Parameters.Add(new SqlParameter("@DDDCelular", SqlDbType.VarChar, 20, "DDDCeclular"));
                    dbCommand.Parameters.Add(new SqlParameter("@Celular", SqlDbType.VarChar, 20, "Celular"));

                    dbCommand.Parameters.Add(new SqlParameter("@TipoContato", SqlDbType.VarChar, 200, "TipoContato"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cargo", SqlDbType.VarChar, 500, "Cargo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 800, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                    dbCommand.Parameters["@Nome"].Value = Nome;
                    dbCommand.Parameters["@Email"].Value = Email;
                    dbCommand.Parameters["@DDDTelefone"].Value = DDDTelefone;
                    dbCommand.Parameters["@Telefone"].Value = Telefone;
                    dbCommand.Parameters["@Ramal"].Value = Ramal;
                    dbCommand.Parameters["@DDDCelular"].Value = DDDCelular;
                    dbCommand.Parameters["@Celular"].Value = Celular;
                    dbCommand.Parameters["@TipoContato"].Value = TipoContato;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@Cargo"].Value = Cargo;
                    dbCommand.Parameters["@Empresa"].Value = Empresa;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;


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
                        Retorno = "Erro na Funcao Incluir_Contato";
                    }*/


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir_Contato. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Alterar_Contato()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_ENT_CONTATO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 300, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 300, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@DDDTelefone", SqlDbType.VarChar, 10, "DDDTelefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 20, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ramal", SqlDbType.VarChar, 20, "Ramal"));

                    dbCommand.Parameters.Add(new SqlParameter("@DDDCelular", SqlDbType.VarChar, 20, "DDDCeclular"));
                    dbCommand.Parameters.Add(new SqlParameter("@Celular", SqlDbType.VarChar, 20, "Celular"));

                    dbCommand.Parameters.Add(new SqlParameter("@TipoContato", SqlDbType.VarChar, 200, "TipoContato"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cargo", SqlDbType.VarChar, 500, "Cargo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 800, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@ENTCONTATOID", SqlDbType.VarChar, 800, "ENTCONTATOID"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                    dbCommand.Parameters["@Nome"].Value = Nome;
                    dbCommand.Parameters["@Email"].Value = Email;
                    dbCommand.Parameters["@DDDTelefone"].Value = DDDTelefone;
                    dbCommand.Parameters["@Telefone"].Value = Telefone;
                    dbCommand.Parameters["@Ramal"].Value = Ramal;
                    dbCommand.Parameters["@DDDCelular"].Value = DDDCelular;
                    dbCommand.Parameters["@Celular"].Value = Celular;
                    dbCommand.Parameters["@TipoContato"].Value = TipoContato;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@Cargo"].Value = Cargo;
                    dbCommand.Parameters["@Empresa"].Value = Empresa;
                    dbCommand.Parameters["@ENTCONTATOID"].Value = ENTCONTATOID;
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
                        Retorno = "Erro na Funcao Alterar_Contato";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Alterar_Contato. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Remove_Contato()
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

                    dbCommand = new SqlCommand("USER_SP_REMOVER_ENT_CONTATO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@ENTCONTATOID", SqlDbType.VarChar, 300, "ENTCONTATOID"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@ENTCONTATOID"].Value = ENTCONTATOID;
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
                        Retorno = "Erro na Funcao Remove_Contato";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Remove_Contato. Contactar o Suporte!";
            }




            return Retorno;

        }

        public DataTable Consulta_Contato_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_CONSULTA_ENT_CONTATO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));


                dbCommand.Parameters["@EntCod"].Value = EntCod;



                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public string Excluir_Contato()
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

                    dbCommand = new SqlCommand("user_sp_Excluir_Todos_Ent_Contato", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Excluir_Contato. Contactar o Suporte!";
            }

            return Retorno;
        }
    }


}