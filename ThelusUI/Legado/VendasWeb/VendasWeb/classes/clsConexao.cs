using System;
using System.Collections.Generic;

using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class clsConexao
    {
       //VS13.39
       public static string strConec = "server=192.168.0.3; user id=sa; password='ssuark.dba'; database=manuli; application name=VendasWeb_1;";
       //public static string strConec = "server=192.168.0.3; user id=sa; password='ssuark.dba'; database=Base_Desenvolvimento; application name=VendasWeb";
       //public static string strConec = "server=192.168.0.3; user id=sa; password='ssuark.dba'; database=Base_DesenvNovo; application name=VendasWeb";
       //public static string strConec = "server=192.168.0.3; user id=sa; password='ssuark.dba'; database=BaseTI; application name=VendasWeb";
      

        public string getString()
        {
            return strConec;
        }

    }
}
