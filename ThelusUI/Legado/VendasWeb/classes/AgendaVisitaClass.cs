using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class AgendaVisitaClass : clsConexao
    {

        public int AGENDA_VISITA_ID { get; set; }
        public string AgendaStatus { get; set; }
        public string VendCod { get; set; }
        public string UsuCod { get; set; }
        public string UsuCodAux { get; set; }
        public string EntCod { get; set; }
        public string EntNome { get; set; }
        public string EntCpfCgc { get; set; }
        public string UfSigla { get; set; }
        public string CidNomeComp { get; set; }
        public string Telefone { get; set; }
        public string Observacao { get; set; }
        public DateTime DataVisita { get; set; }
        public string CondicaoCliente { get; set; }
        public string VendClasseCod { get; set; }
        public string TipoOperacao { get; set; }


        #region Campos Para Filtros
        public DateTime DataI { get; set; }
        public DateTime DataF { get; set; }
        #endregion

        public List<ProdutoVisitaClass> ListProdutoVisita { get; set; }
        private ProdutoVisitaClass ObjProdutoVisita { get; set; }


        public string INSERE_AGENDA()
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

                    dbCommand = new SqlCommand("USER_SP_INSERE_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@AgendaStatus", SqlDbType.VarChar, 150, "AgendaStatus"));    
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 50, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 50, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 150, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 150, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 150, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@UfSigla", SqlDbType.VarChar, 50, "UfSigla"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidNomeComp", SqlDbType.VarChar, 150, "CidNomeComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 50, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Observacao", SqlDbType.VarChar, 800, "Observacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataVisita", SqlDbType.DateTime, 0, "DataVisita"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondicaoCliente", SqlDbType.VarChar, 150, "CondicaoCliente"));
                    

                    dbCommand.Parameters["@AgendaStatus"].Value = AgendaStatus;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntNome"].Value = EntNome;
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;
                    dbCommand.Parameters["@UfSigla"].Value = UfSigla;
                    dbCommand.Parameters["@CidNomeComp"].Value = CidNomeComp;
                    dbCommand.Parameters["@Telefone"].Value = Telefone;
                    dbCommand.Parameters["@Observacao"].Value = Observacao;
                    dbCommand.Parameters["@DataVisita"].Value = DataVisita;
                    dbCommand.Parameters["@CondicaoCliente"].Value = CondicaoCliente;

                    

                    


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
                            AGENDA_VISITA_ID = Convert.ToInt32(row["AGENDA_VISITA_ID"]);



                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao INSERE_AGENDA";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao INSERE_AGENDA. Contactar o Suporte!";
            }




            return Retorno;

        }


        public string ALTERA_AGENDA()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@AGENDA_VISITA_ID", SqlDbType.Int, 0, "AGENDA_VISITA_ID"));
                    dbCommand.Parameters.Add(new SqlParameter("@AgendaStatus", SqlDbType.VarChar, 150, "AgendaStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 50, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 50, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 150, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 150, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 150, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@UfSigla", SqlDbType.VarChar, 50, "UfSigla"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidNomeComp", SqlDbType.VarChar, 150, "CidNomeComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 50, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Observacao", SqlDbType.VarChar, 800, "Observacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataVisita", SqlDbType.DateTime, 0, "DataVisita"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondicaoCliente", SqlDbType.VarChar, 150, "CondicaoCliente"));


                    dbCommand.Parameters["@AGENDA_VISITA_ID"].Value = AGENDA_VISITA_ID;
                    dbCommand.Parameters["@AgendaStatus"].Value = AgendaStatus;
                    dbCommand.Parameters["@AgendaStatus"].Value = AgendaStatus;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntNome"].Value = EntNome;
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;
                    dbCommand.Parameters["@UfSigla"].Value = UfSigla;
                    dbCommand.Parameters["@CidNomeComp"].Value = CidNomeComp;
                    dbCommand.Parameters["@Telefone"].Value = Telefone;
                    dbCommand.Parameters["@Observacao"].Value = Observacao;
                    dbCommand.Parameters["@DataVisita"].Value = DataVisita;
                    dbCommand.Parameters["@CondicaoCliente"].Value = CondicaoCliente;




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
                        Retorno = "Erro na Funcao ALTERA_AGENDA";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao ALTERA_AGENDA. Contactar o Suporte!";
            }




            return Retorno;

        }


        public string DELETA_AGENDA()
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

                    dbCommand = new SqlCommand("USER_SP_DELETA_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@AGENDA_VISITA_ID", SqlDbType.Int, 0, "AGENDA_VISITA_ID"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 50, "UsuCod"));
                    

                    dbCommand.Parameters["@AGENDA_VISITA_ID"].Value = AGENDA_VISITA_ID;
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
                        Retorno = "Erro na Funcao DELETA_AGENDA";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao DELETA_AGENDA. Contactar o Suporte!";
            }




            return Retorno;

        }


        public DataTable CONSULTA_AGENDA()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCodAux", SqlDbType.VarChar, 8000, "UsuCodAux"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 8000, "VendClasseCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataI", SqlDbType.DateTime, 0, "DataI"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataF", SqlDbType.DateTime, 0, "DataF"));
                    dbCommand.Parameters.Add(new SqlParameter("@AgendaStatus", SqlDbType.VarChar, 150, "AgendaStatus"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@UsuCodAux"].Value = UsuCodAux;
                    dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod;

                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@DataI"].Value = DataI;
                    dbCommand.Parameters["@DataF"].Value = DataF;
                    dbCommand.Parameters["@AgendaStatus"].Value = AgendaStatus;
                    
                    dbCommand.CommandTimeout = 9999999;

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

        public DataTable CONSULTA_AGENDA_GERAL()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_AGENDA_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCodAux", SqlDbType.VarChar, 8000, "UsuCodAux"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 8000, "VendClasseCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataI", SqlDbType.DateTime, 0, "DataI"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataF", SqlDbType.DateTime, 0, "DataF"));
                    dbCommand.Parameters.Add(new SqlParameter("@AgendaStatus", SqlDbType.VarChar, 150, "AgendaStatus"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@UsuCodAux"].Value = UsuCodAux;
                    dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod;

                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@DataI"].Value = DataI;
                    dbCommand.Parameters["@DataF"].Value = DataF;
                    dbCommand.Parameters["@AgendaStatus"].Value = AgendaStatus;

                    dbCommand.CommandTimeout = 9999999;

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


        public string MOSTRA_AGENDA()
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

                    dbCommand = new SqlCommand("USER_SP_MOSTRA_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@AGENDA_VISITA_ID", SqlDbType.Int, 0, "AGENDA_VISITA_ID"));
                    
                    dbCommand.Parameters["@AGENDA_VISITA_ID"].Value = AGENDA_VISITA_ID;
                    


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            AgendaStatus = row["AgendaStatus"].ToString();
                            VendCod = row["VendCod"].ToString();
                            UsuCod = row["UsuCod"].ToString();
                            EntCod = row["EntCod"].ToString();
                            EntNome = row["EntNome"].ToString();
                            EntCpfCgc = row["EntCpfCgc"].ToString();
                            UfSigla = row["UfSigla"].ToString();
                            CidNomeComp = row["CidNomeComp"].ToString();
                            Telefone = row["Telefone"].ToString();
                            Observacao = row["Observacao"].ToString();
                            DataVisita = Convert.ToDateTime(row["DataVisita"].ToString());
                            CondicaoCliente = row["CondicaoCliente"].ToString();
                            

                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao MOSTRA_AGENDA";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao MOSTRA_AGENDA. Contactar o Suporte!";
            }




            return Retorno;

        }

        public void MOSTRA_PRODUTO_VISITA_AGENDA_VISITA_ID()
        {
            DataTable DtProdutoVisita = new DataTable();
            ObjProdutoVisita = new ProdutoVisitaClass();
            ObjProdutoVisita.AGENDA_VISITA_ID = AGENDA_VISITA_ID;
            DtProdutoVisita = ObjProdutoVisita.MOSTRA_PRODUTO_VISITA_AGENDA_VISITA_ID();

            ListProdutoVisita = new List<ProdutoVisitaClass>();

            if (DtProdutoVisita.Rows.Count > 0)
            {
                foreach (DataRow row in DtProdutoVisita.Rows)
                {
                    ObjProdutoVisita = new ProdutoVisitaClass();

                    ObjProdutoVisita.PRODUTO_VISITA_ID = Convert.ToInt32(row["PRODUTO_VISITA_ID"].ToString());
                    ObjProdutoVisita.AGENDA_VISITA_ID = Convert.ToInt32(row["AGENDA_VISITA_ID"].ToString());
                    ObjProdutoVisita.ProdNome = row["ProdNome"].ToString();
                    ObjProdutoVisita.ProdCodEstr = row["ProdCodEstr"].ToString();
                    ObjProdutoVisita.ClasseQtd = row["ClasseQtd"].ToString();
                    ObjProdutoVisita.PrazoPotencialMesCorrente = row["PrazoPotencialMesCorrente"].ToString();
                    ObjProdutoVisita.PrazoPotencialMes1 = row["PrazoPotencialMes1"].ToString();
                    ObjProdutoVisita.PrazoPotencialMes3 = row["PrazoPotencialMes3"].ToString();
                    ObjProdutoVisita.PrazoPotencialMesSuperior = row["PrazoPotencialMesSuperior"].ToString();

                    ListProdutoVisita.Add(ObjProdutoVisita);

                }
            }
            

        }

        public void Adicionar_ProdutoVisita(ProdutoVisitaClass NovoObj)
        {
            
            //Verifica se esta instanciado
            if (this.ListProdutoVisita == null)
            {
                this.ListProdutoVisita = new List<ProdutoVisitaClass>();
            }


            int AuxID = 0;

            if (this.ListProdutoVisita != null)
            {
                if (this.ListProdutoVisita.Count > 0)
                {
                    AuxID = this.ListProdutoVisita.OrderBy(C => C.PRODUTO_VISITA_ID).First().PRODUTO_VISITA_ID;
                }
            }

            if (AuxID <= 0)
            {

                NovoObj.PRODUTO_VISITA_ID = AuxID - 1;
            }
            else
            {
                NovoObj.PRODUTO_VISITA_ID = (AuxID + 1) * -1;
            }



            this.ListProdutoVisita.Add(NovoObj);
        }

        public void Remover_ProdutoVisita(ProdutoVisitaClass Obj)
        {
            for (int i = 0; i < this.ListProdutoVisita.Count; i++)
            {
                if (this.ListProdutoVisita[i].PRODUTO_VISITA_ID == Obj.PRODUTO_VISITA_ID)
                {

                    if (Obj.PRODUTO_VISITA_ID < 0)
                    {
                        this.ListProdutoVisita.RemoveAt(i);
                    }
                    else{

                        Obj.TipoOperacao = "Remover";
                        this.ListProdutoVisita.RemoveAt(i);//Remove antigo
                        this.ListProdutoVisita.Add(Obj);//adiciona novo com operacao igual a remover
                    }
                   
                }
            }
        }

        public void Altera_ProdutoVisita(ProdutoVisitaClass Obj)
        {
            for (int i = 0; i < this.ListProdutoVisita.Count; i++)
            {
                if (this.ListProdutoVisita[i].PRODUTO_VISITA_ID == Obj.PRODUTO_VISITA_ID)
                {
                    if (Obj.TipoOperacao == "Alterar")
                    {
                        this.ListProdutoVisita.RemoveAt(i);//Remove antigo

                        if (Obj.PRODUTO_VISITA_ID < 0)
                        {
                            Obj.TipoOperacao = "Incluir";
                        }
                        this.ListProdutoVisita.Add(Obj);//adiciona novo com operacao igual 
                    }
                   
                }
            }
        }






    }
}