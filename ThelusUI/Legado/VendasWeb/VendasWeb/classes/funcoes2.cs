using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace VendasWeb
{
    public class funcoes2 : classes.clsConexao
    {
            
        /*public Boolean ExecutaSQL(string paramQuery)
        {
            Boolean blnResultado;
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                SqlCommand dbCommand = new SqlCommand(paramQuery, dbConnection);                
                try
                {
                    dbCommand.Connection.Open();

                    dbCommand.ExecuteNonQuery();

                    blnResultado = true;
                }
                catch (Exception)
                {
                    blnResultado = false;
                }
            }
            return blnResultado;
        }

        public string FormataData(string data)
        {

            if (data != "")
            {
                string[] DataDig = data.Split('/');
                string Dia = DataDig[0];
                string Mes = DataDig[1];
                string Ano = DataDig[2];

                return Ano + '-' + Mes + '-' + Dia;
            }
            else
            {
                return "";
            }

        }

        public string FormataData2(string data)
        {
            if (data != "")
            {
                string[] DataDig = data.Split('-');
                string Ano = DataDig[0];
                string Mes = DataDig[1];
                string Dia = DataDig[2];

                return Dia + '/' + Mes + '/' + Ano;
            }
            else
            {
                return "";
            }

        }*/
    }
}