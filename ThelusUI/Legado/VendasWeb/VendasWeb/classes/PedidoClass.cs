using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class PedidoClass : clsConexao
    {
        public string EmpCod { get; set; }
        public string UsuCod { get; set; }
        public string PedVendaTipo { get; set; }
        public string PedVendaStatDescr { get; set; }
        public int Nivel { get; set; }
        public string valorConsulta { get; set; }
        public string PedVendaNum { get; set; }

        public string EntCod { get; set; }
        public string EntNome { get; set; }
        public string NfNum { get; set; }

        public string EmpNome { get; set; }
        public string EntCpfCgc { get; set; }
        public string PedVendaData { get; set; }
        public string NFHoraSaida { get; set; }
        public string EntEnderCompleto { get; set; }
        public string EntBair { get; set; }
        public string CidNome { get; set; }
        public string UfSigla { get; set; }
        public string EntCep { get; set; }
        public string CondPagCod { get; set; }
        public string CondPagPedVendaNome { get; set; }
        public string PedVendaNatOpProd { get; set; }
        public string NatOpNome { get; set; }
        public string VendCod { get; set; }
        public string VendNome { get; set; }
        public string PedVendaValMerc { get; set; }
        public string PedVendaValIpiCalc { get; set; }
        public string PedVendaValIcms { get; set; }
        public string IcmsDiferido { get; set; }
        public string IcmsDevido { get; set; }
        public string PedVendaValTotal { get; set; }
        public string EntCodTransp { get; set; }
        public string EntNomeTransp { get; set; }
        public string PedVendaStatFrete { get; set; }
        public string PedVendaTexto { get; set; }
        public string PedVendaTextoHist { get; set; }
        public string ItensFormatados { get; set; }
        public string ClicheFormatados { get; set; }


        public DataTable Lista_Pedidos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Webvendas_Listar_Pedidos", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 5, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 31, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaTipo", SqlDbType.VarChar, 30, "PedVendaTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaStatDescr", SqlDbType.VarChar, 50, "PedVendaStatDescr"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nivel", SqlDbType.Int, 0, "Nivel"));
                    dbCommand.Parameters.Add(new SqlParameter("@valorConsulta", SqlDbType.VarChar, 31, "valorConsulta"));

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 150, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@NfNum", SqlDbType.VarChar, 150, "NfNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@UsuCod"].Value = this.UsuCod;
                    dbCommand.Parameters["@PedVendaTipo"].Value = this.PedVendaTipo;
                    dbCommand.Parameters["@PedVendaStatDescr"].Value = this.PedVendaStatDescr;
                    dbCommand.Parameters["@Nivel"].Value = this.Nivel;
                    dbCommand.Parameters["@valorConsulta"].Value = this.valorConsulta;

                    dbCommand.Parameters["@EntCod"].Value = this.EntCod;
                    dbCommand.Parameters["@EntNome"].Value = this.EntNome;
                    dbCommand.Parameters["@NfNum"].Value = this.NfNum;
                    dbCommand.Parameters["@PedVendaNum"].Value = this.PedVendaNum;


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

        public DataTable Consulta_Pedido()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Webvendas_consulta_Pedido", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 150, "PedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 150, "EmpCod"));

                    dbCommand.Parameters["@PedVendaNum"].Value = this.PedVendaNum;
                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                EmpCod = row["EmpCod"].ToString();
                               
                                PedVendaTipo = row["PedVendaTipo"].ToString(); ;
                                PedVendaStatDescr = row["PedVendaStatDescr"].ToString();
                                PedVendaNum = row["PedVendaNum"].ToString();

                                EntCod = row["EntCod"].ToString();
                                EntNome = row["EntNome"].ToString();
                                EmpNome = row["EmpNome"].ToString();
                                EntCpfCgc = row["EntCpfCgc"].ToString();
                                PedVendaData = row["PedVendaData"].ToString();
                                NFHoraSaida = row["NFHoraSaida"].ToString();
                                EntEnderCompleto = row["EntEnderCompleto"].ToString();
                                EntBair = row["EntBair"].ToString();
                                CidNome = row["CidNome"].ToString();
                                UfSigla = row["UfSigla"].ToString();
                                EntCep = row["EntCep"].ToString();
                                CondPagCod = row["CondPagCod"].ToString();
                                CondPagPedVendaNome = row["CondPagPedVendaNome"].ToString();
                                PedVendaNatOpProd = row["PedVendaNatOpProd"].ToString();
                                NatOpNome = row["NatOpNome"].ToString();
                                VendCod = row["VendCod"].ToString();
                                VendNome = row["VendNome"].ToString();
                                PedVendaValMerc = row["PedVendaValMerc"].ToString();
                                PedVendaValIpiCalc = row["PedVendaValIpiCalc"].ToString();
                                PedVendaValIcms = row["PedVendaValIcms"].ToString();
                                IcmsDiferido = row["IcmsDiferido"].ToString();
                                IcmsDevido = row["IcmsDevido"].ToString();
                                PedVendaValTotal = row["PedVendaValTotal"].ToString();
                                EntCodTransp = row["EntCodTransp"].ToString();
                                EntNomeTransp = row["EntNomeTransp"].ToString();
                                PedVendaStatFrete = row["PedVendaStatFrete"].ToString();
                                PedVendaTexto = row["PedVendaTexto"].ToString();
                                PedVendaTextoHist = row["PedVendaTextoHist"].ToString();
                                ItensFormatados = row["ItensFormatados"].ToString();
                                ClicheFormatados = row["ClicheFormatados"].ToString();

                            }
                        }
                    }
                }
            }
            catch
            {

            }

            return outputTable;
        }

        public DataTable Lista_Item_Pedido()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Webvendas_Listar_Item_Pedido", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 5, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 7, "PedVendaNum"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@PedVendaNum"].Value = this.PedVendaNum;

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
    }
}