using System;
using System.Data.SqlClient;
using System.Text;

namespace CRMAPI.Classes
{
    public class SQLUtilClass
    {
        public string MontarComandoExec(SqlCommand command)
        {
            var sb = new StringBuilder();
            sb.Append("EXEC ");
            sb.Append(command.CommandText);
            sb.Append(" ");

            for (int i = 0; i < command.Parameters.Count; i++)
            {
                var param = command.Parameters[i];

                if (param.Value == null || param.Value == DBNull.Value)
                {
                    sb.Append("NULL");
                }
                else
                {
                    // Trata valores diferentes (ex: strings precisam de aspas)
                    if (param.Value is string || param.Value is DateTime)
                    {
                        sb.Append("'");
                        sb.Append(param.Value.ToString().Replace("'", "''")); // Escapar aspas
                        sb.Append("'");
                    }
                    else
                    {
                        sb.Append(param.Value);
                    }
                }

                if (i < command.Parameters.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

    }
}