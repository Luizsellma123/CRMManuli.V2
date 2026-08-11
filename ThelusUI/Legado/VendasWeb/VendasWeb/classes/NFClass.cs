using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class NFClass : clsConexao
    {
        public string EntCod { get; set; }
        public string EmpCod { get; set; }
        public string NfNum { get; set; }

        public DataTable Lista_NF()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Lista_NF_Entidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 30, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 150, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@NfNum", SqlDbType.VarChar, 10, "NfNum"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@EntCod"].Value = this.EntCod;
                    dbCommand.Parameters["@NfNum"].Value = this.NfNum;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;
        }

        public void PreencheDropList(System.Web.UI.WebControls.CheckBoxList drpList, string paramSQL, string itemAdicional)
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                using (SqlDataAdapter DataAdapter = new SqlDataAdapter(paramSQL, dbConnection))
                {
                    using (DataSet dtsetCombo = new DataSet())
                    {
                        drpList.Items.Clear();

                        //Roda a query
                        DataAdapter.Fill(dtsetCombo, "tbUsuarioMenu");
                        drpList.DataMember = "tbUsuarioMenu";
                        drpList.DataTextField = dtsetCombo.Tables[0].Columns[1].ColumnName;
                        drpList.DataValueField = dtsetCombo.Tables[0].Columns[0].ColumnName;
                        drpList.DataSource = dtsetCombo;
                        drpList.DataBind();

                        if (itemAdicional != "" || itemAdicional == null)
                        {
                            drpList.Items.Add(itemAdicional);
                            drpList.Items[drpList.Items.Count - 1].Value = itemAdicional;
                            drpList.Items[drpList.Items.Count - 1].Text = itemAdicional;
                        }
                    }
                }
            }
        }

    }
}