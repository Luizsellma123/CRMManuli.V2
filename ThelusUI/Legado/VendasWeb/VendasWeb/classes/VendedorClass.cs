using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class VendedorClass : clsConexao
    {
        public string UsuCod { get; set; }
        public string VendCod { get; set; }
        public string VendStat { get; set; }
        public string VendNome { get; set; }
        public string VendEntPrinc { get; set; }
        public string EntCod { get; set; }
        public string TipoOperacao { get; set; }
        public Boolean VendEntPrincBit { get; set; }
        public string TodosCodigos { get; set; }
        public string Vendedor { get; set; }

        public int CodExpectativa { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
        public string UserLinhaProdutoLista { get; set; }
        public double QtdJaneiro { get; set; }
        public double QtdFevereiro { get; set; }
        public double QtdMarco { get; set; }
        public double QtdAbril { get; set; }
        public double QtdMaio { get; set; }
        public double QtdJunho { get; set; }
        public double QtdJulho { get; set; }
        public double QtdAgosto { get; set; }
        public double QtdSetembro { get; set; }
        public double QtdOutubro { get; set; }
        public double QtdNovembro { get; set; }
        public double QtdDezembro { get; set; }
        public int QuantidadeInativosVendedor { get; set; }
        public string VendClasseCod { get; set; }
        public int CodGestores { get; set; }

        public string Status { get; set; }
        public string UF { get; set; }
        public string Regiao { get; set; }
        public string Cidade { get; set; }
        public string DataInicial { get; set; }
        public string AnoPesquisa { get; set; }

        public string DataFinal { get; set; }

        public funcoes mdlfuncoes { get; set; }

        public string TrocaCarteiraVendedor(string DoCodigo, string ParaCodigo, string Status, string UsuCod)
        {
            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    string retorno = "";
                    funcoes mdlFuncoes = new funcoes();
                    DataTable outputTable = new DataTable();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_TROCA_VENDEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@DeVendCod", SqlDbType.VarChar, 30, "DeVendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@ParaVendCod", SqlDbType.VarChar, 30, "ParaVendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendStat", SqlDbType.VarChar, 30, "VendStat"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 50, "UsuCod"));


                    dbCommand.Parameters["@DeVendCod"].Value = DoCodigo;
                    dbCommand.Parameters["@ParaVendCod"].Value = ParaCodigo;
                    dbCommand.Parameters["@VendStat"].Value = Status;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            retorno = row["MENSAGEM"].ToString(); ;
                        }


                    }
                    else
                    {
                        retorno = "Erro TrocaCarteiraVendedor";
                    }

                    return retorno;

                }
            }
            catch
            {
                return "Erro TrocaCarteiraVendedor. Verificar com o Suporte.";
            }
        }

        public bool Consulta_vendedor_VendCod()
        {
            DataTable outputTable = new DataTable();
            Boolean retorno = false;

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_consulta_vendedor_VendCod", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 200, "VendCod"));


                dbCommand.Parameters["@VendCod"].Value = VendCod;


                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {


                        this.VendNome = row["VendNome"].ToString();


                    }

                    retorno = true;
                }
                else
                {

                    this.VendNome = "Vendedor não Localizado";
                    retorno = false;
                }

                dataReader.Close();


                return retorno;
            }

        }

        public DataTable Consulta_Vendedor_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_MOSTRA_VEND_ENT", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));


                dbCommand.Parameters["@EntCod"].Value = EntCod;



                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public DataTable Consulta_Vendedor()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_consulta_vendedor_VendCod", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 200, "VendCod"));
                dbCommand.Parameters.Add(new SqlParameter("@VendNome", SqlDbType.VarChar, 800, "VendNome"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 200, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@TodosCodigos", SqlDbType.VarChar, 1, "TodosCodigos"));

                dbCommand.Parameters["@VendCod"].Value = VendCod;
                dbCommand.Parameters["@VendNome"].Value = VendNome;
                dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                dbCommand.Parameters["@TodosCodigos"].Value = TodosCodigos;//Utilizado para indicar se roda a consulta com UNION ou NAo


                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }


        public void Consulta_Codigo_Vendedor_UsuCod()
        {

            string strSQL = "";

            DataTable dadosTable = new DataTable();
            mdlfuncoes = new funcoes();
            strSQL = "select VendCod from VENDEDOR where UsuCod='" + UsuCod + "'";


            dadosTable = mdlfuncoes.Executa_DataTable(strSQL, "consulta_Vendedor_UsuCod VendedorClass.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    VendCod = row["VendCod"].ToString();
                }
            }


        }

        public string Incluir_Vend_Ent()
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

                    dbCommand = new SqlCommand("USER_SP_INSERE_VEND_ENT", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 300, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 100, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendEntPrinc", SqlDbType.VarChar, 10, "VendEntPrinc"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@VendEntPrinc"].Value = VendEntPrinc;
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
                        Retorno = "Erro na Funcao Incluir_Vend_Ent";
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir_Vend_Ent. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Altera_Vend_Ent()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_VEND_ENT", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 300, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 300, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendEntPrinc", SqlDbType.VarChar, 10, "VendEntPrinc"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@VendEntPrinc"].Value = VendEntPrinc;
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
                        Retorno = "Erro na Funcao Altera_Vend_Ent";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Altera_Vend_Ent. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Remove_Cond_Vend_Ent()
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

                    dbCommand = new SqlCommand("USER_SP_REMOVE_VEND_ENT", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 300, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 300, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendEntPrinc", SqlDbType.VarChar, 10, "VendEntPrinc"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@VendEntPrinc"].Value = VendEntPrinc;
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
                        Retorno = "Erro na Funcao Remove_Cond_Vend_Ent";
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Remove_Cond_Vend_Ent. Contactar o Suporte!";
            }
            return Retorno;
        }

        public DataTable Listar_Vendedores()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_CRM_Listar_Vendedores", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@Vendedor", SqlDbType.VarChar, 31, "Vendedor"));

                dbCommand.Parameters["@Vendedor"].Value = Vendedor;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }
        }

        public bool Consulta_vendedor_Por_Codigo()
        {
            DataTable outputTable = new DataTable();
            Boolean retorno = false;

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_Consulta_Vendedor_Por_Codigo", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 200, "VendCod"));

                dbCommand.Parameters["@VendCod"].Value = VendCod;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        this.VendNome = row["VendNome"].ToString();
                    }

                    retorno = true;
                }
                else
                {
                    this.VendNome = "Vendedor não Localizado";
                    retorno = false;
                }

                dataReader.Close();


                return retorno;
            }
        }

        public DataTable Consulta_Familia_Produto()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_Lista_Familia_Produto", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
                dataReader.Close();
            }
            return outputTable;
        }

        public DataTable Listar_Expectativas()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_Listar_Expectativa_Pedidos_Vendedor", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 7, "VendCod"));
                dbCommand.Parameters.Add(new SqlParameter("@Ano", SqlDbType.VarChar, 4, "Ano"));

                dbCommand.Parameters["@VendCod"].Value = VendCod;
                dbCommand.Parameters["@Ano"].Value = AnoPesquisa;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }

        /*public string Salvar_Expectativa()
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

                    dbCommand = new SqlCommand("user_sp_Salvar_Expectativa_Pedidos_Vendedor", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 7, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Mes", SqlDbType.VarChar, 2, "Mes"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ano", SqlDbType.VarChar, 4, "Ano"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserLinhaProdutoLista", SqlDbType.VarChar, 100, "UserLinhaProdutoLista"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdExpectativa", SqlDbType.Decimal, 30, "QtdExpectativa"));

                    dbCommand.Parameters["@Codigo"].Value = CodExpectativa;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@Mes"].Value = Mes;
                    dbCommand.Parameters["@Ano"].Value = Ano;
                    dbCommand.Parameters["@UserLinhaProdutoLista"].Value = UserLinhaProdutoLista;
                    dbCommand.Parameters["@QtdExpectativa"].Value = QtdExpectativa;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Salvar_Expectativa. Contactar o Suporte!";
            }

            return Retorno;
        }*/

        public string Alterar_Expectativa()
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

                    dbCommand = new SqlCommand("user_sp_Alterar_Expectativa_Pedidos_Vendedor", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdJaneiro", SqlDbType.Decimal, 0, "QtdJaneiro"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdFevereiro", SqlDbType.Decimal, 0, "QtdFevereiro"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdMarco", SqlDbType.Decimal, 0, "QtdMarco"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdAbril", SqlDbType.Decimal, 0, "QtdAbril"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdMaio", SqlDbType.Decimal, 0, "QtdMaio"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdJunho", SqlDbType.Decimal, 0, "QtdJunho"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdJulho", SqlDbType.Decimal, 0, "QtdJulho"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdAgosto", SqlDbType.Decimal, 0, "QtdAgosto"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdSetembro", SqlDbType.Decimal, 0, "QtdSetembro"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdOutubro", SqlDbType.Decimal, 0, "QtdOutubro"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdNovembro", SqlDbType.Decimal, 0, "QtdNovembro"));
                    dbCommand.Parameters.Add(new SqlParameter("@QtdDezembro", SqlDbType.Decimal, 0, "QtdDezembro"));

                    dbCommand.Parameters["@Codigo"].Value = CodExpectativa;
                    dbCommand.Parameters["@QtdJaneiro"].Value = QtdJaneiro;
                    dbCommand.Parameters["@QtdFevereiro"].Value = QtdFevereiro;
                    dbCommand.Parameters["@QtdMarco"].Value = QtdMarco;
                    dbCommand.Parameters["@QtdAbril"].Value = QtdAbril;
                    dbCommand.Parameters["@QtdMaio"].Value = QtdMaio;
                    dbCommand.Parameters["@QtdJunho"].Value = QtdJunho;
                    dbCommand.Parameters["@QtdJulho"].Value = QtdJulho;
                    dbCommand.Parameters["@QtdAgosto"].Value = QtdAgosto;
                    dbCommand.Parameters["@QtdSetembro"].Value = QtdSetembro;
                    dbCommand.Parameters["@QtdOutubro"].Value = QtdOutubro;
                    dbCommand.Parameters["@QtdNovembro"].Value = QtdNovembro;
                    dbCommand.Parameters["@QtdDezembro"].Value = QtdDezembro;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Alterar_Expectativa. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Excluir_Expectativa()
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

                    dbCommand = new SqlCommand("user_sp_Excluir_Expectativa_Pedidos_Vendedor", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));

                    dbCommand.Parameters["@Codigo"].Value = CodExpectativa;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Excluir_User_TB_GestoresClasses. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable Listar_Classes_Vendedores()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_Lista_Classe_Vendedor", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 31, "UsuCod"));

                dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }

        public DataTable Listar_User_TB_GestoresClasses()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_Listar_User_TB_GestoresClasses", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 7, "VendCod"));

                dbCommand.Parameters["@VendCod"].Value = VendCod;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }

        public string Salvar_User_TB_GestoresClasses()
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

                    dbCommand = new SqlCommand("user_sp_Salvar_User_TB_GestoresClasses", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 7, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 100, "VendClasseCod"));

                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Salvar_User_TB_GestoresClasses. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Excluir_User_TB_GestoresClasses()
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

                    dbCommand = new SqlCommand("user_sp_Excluir_User_TB_GestoresClasses", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));

                    dbCommand.Parameters["@Codigo"].Value = CodGestores;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Excluir_User_TB_GestoresClasses. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable Listar_Relatorio_Gerencial()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_CRM_Relatorio_Gerencial", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));
                dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 1, "Status"));
                dbCommand.Parameters.Add(new SqlParameter("@UF", SqlDbType.VarChar, 2, "UF"));
                dbCommand.Parameters.Add(new SqlParameter("@Regiao", SqlDbType.VarChar, 15, "Regiao"));
                dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.VarChar, 15, "Cidade"));
                dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 8000, "VendClasseCod"));
                dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 19, "DataInicial"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 19, "DataFinal"));

                dbCommand.Parameters["@VendCod"].Value = VendCod;
                dbCommand.Parameters["@Status"].Value = Status;
                dbCommand.Parameters["@UF"].Value = UF;
                dbCommand.Parameters["@Regiao"].Value = Regiao;
                dbCommand.Parameters["@Cidade"].Value = Cidade;
                dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod;
                dbCommand.Parameters["@DataInicial"].Value = DataInicial;
                dbCommand.Parameters["@DataFinal"].Value = DataFinal;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }

        public string Concatena_classe_Vendedor()
        {
            string clasVend = "";

            DataTable dadosTable = new DataTable();

            dadosTable = Listar_Classes_Vendedores();

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    clasVend += (string)Convert.ToString(row["vendClasseCod"]) + "|";
                }
            }

            return clasVend;
        }


        #region Utilizado na Tela QtdClienteVendedor

        public DataTable Consulta_Vend_Classe_UsuCod()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_CONSULTA_VEND_CLASSE_USUCOD", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));

                dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }


        public DataTable Lista_Vendedor_Crm_Quantidade_Vendedor()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_LISTA_VENDEDOR_CRM_Quantidade_Vendedor", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 8000, "VendClasseCod"));
                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 150, "VendCod"));
                dbCommand.Parameters.Add(new SqlParameter("@VendNome", SqlDbType.VarChar, 150, "VendNome"));

                dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod;
                dbCommand.Parameters["@VendCod"].Value = VendCod;
                dbCommand.Parameters["@VendNome"].Value = VendNome;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }

        public DataTable Altera_Vendedor_Crm_Quantidade_Vendedor()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_INSERE_VENDEDOR_CRM_Quantidade_Vendedor", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 150, "VendCod"));
                dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.VarChar, 150, "Quantidade"));

                dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                dbCommand.Parameters["@VendCod"].Value = VendCod;
                dbCommand.Parameters["@Quantidade"].Value = QuantidadeInativosVendedor;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }

        #endregion

    }
}