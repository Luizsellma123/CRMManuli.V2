using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;


namespace VendasWeb.GerencialVendas
{
    public class MapsClass : clsConexao
    {

        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Icon { get; set; }

       
        #region Custom
        public string UsuCod { get; set; }
        public string EntCod { get; set; }
        public int GeoCodSolicitado { get; set; }
        public int GeoCodNaoMapeado { get; set; }
        public int TotalAtivo { get; set; }
        public int TotalInativo { get; set; }
        public int TotalProspectivo { get; set; }


        #endregion


        #region Rotas
        public decimal PartidaLatitude { get; set; }
        public decimal PartidaLongitude { get; set; }
        public decimal DestinoLatitude { get; set; }
        public decimal DestinoLongitude { get; set; }
        public List<MapsClass> PontosNoCaminho { get; set; }

        #endregion


        public DataTable Consulta_GeoCod_EntCod()
        {
            DataTable outputTable = new DataTable();

           

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    try
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand();

                        dbCommand = new SqlCommand("USER_SP_CONSULTA_GEOCODE_ENTCOD", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        int intContentLength = EntCod.Length;

                        dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.Text, 2147483647, "EntCod"));
                        dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 50, "UsuCod"));

                        dbCommand.Parameters["@EntCod"].Value = EntCod;
                        dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                        dbCommand.CommandTimeout = 999999;

                        SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                        SqlDataReader dataReader = dbCommand.ExecuteReader();
                        outputTable.Load(dataReader);

                    }
                    catch
                    {


                    }


                    return outputTable;
                }

            

        }

        public DataTable Consulta_GeoCod_EntCod_Rota()
        {
            DataTable outputTable = new DataTable();



            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                try
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_GEOCODE_ENTCOD_ROTA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    int intContentLength = EntCod.Length;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.Text, 2147483647, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 50, "UsuCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                    dbCommand.CommandTimeout = 999999;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                }
                catch
                {


                }


                return outputTable;
            }



        }

        public void Count_GeoCod_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_COUT_GEOCODE_ENTCOD", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;


                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 2147483647, "EntCod"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 50, "UsuCod"));

                dbCommand.Parameters["@EntCod"].Value = EntCod;
                dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        GeoCodSolicitado = Convert.ToInt32(row["GeoCodSolicitado"].ToString());
                        GeoCodNaoMapeado = Convert.ToInt32(row["GeoCodNaoMapeado"].ToString());
                        TotalAtivo = Convert.ToInt32(row["TotalAtivo"].ToString());
                        TotalInativo = Convert.ToInt32(row["TotalInativo"].ToString());
                        TotalProspectivo = Convert.ToInt32(row["TotalProspectivo"].ToString());
                      
                    }
                }


            }

        }


    }
}