using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class clsEntidadesOud : clsConexao
    {
        public string EntCod { get; set; }
        public string EmpCod { get; set; }
        public string UsuCod { get; set; }
        public string EntNome { get; set; }
        public string TipoTratCod { get; set; }

        public string EntNomeFant { get; set; }
        public string EntCep { get; set; }
        public string EntLograd { get; set; }
        public string EntEnder { get; set; }
        public string EntEnderNo { get; set; }
        public string EntEnderComp { get; set; }
        public string EntBair { get; set; }
        public string CidCod { get; set; }
        public string CidNome { get; set; }
        public string EntCxaPost { get; set; }
        public string EntTipoFJ { get; set; }
        public string EntCpfCgc { get; set; }
        public string StatEntCod { get; set; }
        public string EntStatDescr { get; set; }
        public string EntNat { get; set; }
        public string VendCod { get; set; }
        public string EntRgIe { get; set; }
        public string EntWebWWW { get; set; }
        public string EntWebEMail { get; set; }
        public string EntFoneDDD { get; set; }
        public string EntFoneNum { get; set; }
        public string CategCodEstr { get; set; }

        public string EntTextoHist { get; set; }
        public string TipoOperacao { get; set; }

        public DataTable Lista_Tipo_Tratamento()
        {
            string strSql = "";
            strSql = "select TipoTratCod, TipoTratCod as TipoTratNome from TIPO_TRATAMENTO order by TIPOTRATCOD";

            return Executa_DataTable(strSql, "Lista_Tipo_Tratamento");
        }

        public DataTable Lista_Tipo_Logradouro()
        {
            string strSql = "";
            strSql = "select TipoLogradAbrev, TipoLogradAbrev as TipoLogradNome from TIPO_LOGRAD ORDER BY TIPOLOGRADABREV";

            return Executa_DataTable(strSql, "Lista_Tipo_Logradouro");
        }

        public DataTable Lista_Status()
        {
            string strSql = "";
            strSql = "SELECT StatEntCod, StatEntDescr FROM STAT_ENT WHERE STATENTVISUALIZAENTIDADE = 'Sim'";

            if (TipoOperacao == "Consulta")
            {
                strSql += " and StatEntCod in ('01','05','06')";
            }

            return Executa_DataTable(strSql, "Lista_Status");
        }

        public DataTable Lista_Vendedor()
        {
            string strSql = "";
            strSql = "SELECT VendCod, VendNome FROM Vendedor WHERE VendStat = 'Ativo' order by vendnome";

            return Executa_DataTable(strSql, "Lista_Vendedor");
        }

        

        public DataTable Lista_Categoria()
        {
            string strSql = "";
            strSql = "select Cat.CategCodEstr, Cat.CategNome from categoria Cat where Cat.CategNome like 'Entidade%' and Cat.categCodEstr not in('20.60', '99') order by Cat.CategNome";

            return Executa_DataTable(strSql, "Lista_Categoria");
        }

        public String Lista_Categoria_Usuario_Logado()
        {
            string strSql = "";
            string GrpUsuCod = "";
            strSql = "select top 1 GrpUsuCod from GRP_X_USUARIO where GrpUsuCod like 'Entidade%' and GrpUsuCod not in('ENTIDADE-ADMINISTRADOR', 'ENTIDADE-COMPRAS', 'ENTIDADE-FORM') and UsuCod = '" + UsuCod.ToString() + "' order by GrpUsuCod desc";

            DataTable outputTable = new DataTable();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand(strSql, dbConnection))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Deixa o Timeout da consulta com cerca de 4 minutos
                        dbCommand.CommandTimeout = 340;

                        GrpUsuCod = Convert.ToString(dbCommand.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no método Lista_Vendedor_Logado");
                }

                return GrpUsuCod;
            }
        }

        public DataTable Executa_DataTable(String strSql, string Metodo)
        {
            DataTable outputTable = new DataTable();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand(strSql, dbConnection))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Deixa o Timeout da consulta com cerca de 4 minutos
                        dbCommand.CommandTimeout = 340;

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no método " + Metodo);
                }
                return outputTable;
            }
        }

        public string Entidade_Inserir()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("user_sp_Entidade_Inserir", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@TipoTratCod", SqlDbType.VarChar, 20, "TipoTratCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 100, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 40, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCep", SqlDbType.VarChar, 9, "EntCep"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntLograd", SqlDbType.VarChar, 10, "EntLograd"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnder", SqlDbType.VarChar, 40, "EntEnder"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNo", SqlDbType.VarChar, 6, "EntEnderNo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderComp", SqlDbType.VarChar, 40, "EntEnderComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntBair", SqlDbType.VarChar, 30, "EntBair"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 8, "CidCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidNome", SqlDbType.VarChar, 8, "CidNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCxaPost", SqlDbType.VarChar, 6, "EntCxaPost"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTipoFJ", SqlDbType.VarChar, 10, "EntTipoFJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 14, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 7, "StatEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntStatDescr", SqlDbType.VarChar, 40, "EntStatDescr"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNat", SqlDbType.VarChar, 25, "EntNat"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 7, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntRgIe", SqlDbType.VarChar, 15, "EntRgIe"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebWWW", SqlDbType.VarChar, 255, "EntWebWWW"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEMail", SqlDbType.VarChar, 50, "EntWebEMail"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneDDD", SqlDbType.VarChar, 6, "EntFoneDDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneNum", SqlDbType.VarChar, 20, "EntFoneNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@CategCodEstr", SqlDbType.VarChar, 30, "CategCodEstr")); 

                    dbCommand.Parameters["@TipoTratCod"].Value = TipoTratCod;
                    dbCommand.Parameters["@EntNome"].Value = EntNome;
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant;
                    dbCommand.Parameters["@EntCep"].Value = EntCep;
                    dbCommand.Parameters["@EntLograd"].Value = EntLograd;
                    dbCommand.Parameters["@EntEnder"].Value = EntEnder;
                    dbCommand.Parameters["@EntEnderNo"].Value = EntEnderNo;
                    dbCommand.Parameters["@EntEnderComp"].Value = EntEnderComp;
                    dbCommand.Parameters["@EntBair"].Value = EntBair;
                    dbCommand.Parameters["@CidCod"].Value = CidCod;
                    dbCommand.Parameters["@CidNome"].Value = CidNome;
                    dbCommand.Parameters["@EntCxaPost"].Value = EntCxaPost;
                    dbCommand.Parameters["@EntTipoFJ"].Value = EntTipoFJ;
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod;
                    dbCommand.Parameters["@EntStatDescr"].Value = EntStatDescr;
                    dbCommand.Parameters["@EntNat"].Value = EntNat;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@EntRgIe"].Value = EntRgIe;
                    dbCommand.Parameters["@EntWebWWW"].Value = EntWebWWW;
                    dbCommand.Parameters["@EntWebEMail"].Value = EntWebEMail;
                    dbCommand.Parameters["@EntFoneDDD"].Value = EntFoneDDD;
                    dbCommand.Parameters["@EntFoneNum"].Value = EntFoneNum;
                    dbCommand.Parameters["@CategCodEstr"].Value = CategCodEstr;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                }
                catch
                {
                    erro = "Erro ao inserir entidade!";
                }
            }

            return erro;
        }


        public DataTable Lista_Cidade()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Lista_Cidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Busca", SqlDbType.VarChar, 100, "Busca"));

                    dbCommand.Parameters["@Busca"].Value = "";

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

        public string Valida_CPF_CNPJ(string CPF_CNPJ)
        {
            DataTable outputTable = new DataTable();
            string Retorno = "";
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_VALIDA_CPF_CNPJ", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TEXTO", SqlDbType.VarChar, 30, "TEXTO"));

                    dbCommand.Parameters["@TEXTO"].Value = CPF_CNPJ;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["Validacao"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Validação do CPF/CNPJ";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Validação do CPF/CNPJ. Contactar o Suporte.";
            }
            return Retorno;
        }


        public DataTable Consulta_Entidade()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_consulta_entidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 800, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 100, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 5, "StatEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 150, "VendCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant ?? "";
                    dbCommand.Parameters["@EntNome"].Value = EntNome ?? "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc ?? "";
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod ?? "";
                    dbCommand.Parameters["@VendCod"].Value = VendCod ?? "";



                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.EntTextoHist = (string)Convert.ToString(row["EntTextoHist"]);


                        }
                    }


                }
            }
            catch
            {


            }

            return outputTable;

        }

        

        public DataTable Mostra_Entidade()
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_SP_Mostra_Entidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 800, "EntCpfCgc"));


                    dbCommand.Parameters["@EntNome"].Value = this.EntNome;
                    dbCommand.Parameters["@EntCpfCgc"].Value = this.EntCpfCgc;


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

        

        public string Insere_Historico_Entidade()
        {
            string Retorno = "";
            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {


                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    dbCommand = new SqlCommand("User_SP_Insere_Historico_Entidade", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 800, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 800, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTextoHist", SqlDbType.VarChar, 8000, "EntTextoHist"));

                    dbCommand.Parameters["@UsuCod"].Value = this.UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = this.EntCod;
                    dbCommand.Parameters["@EntTextoHist"].Value = this.EntTextoHist;


                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);



                    dataReader.Close();

                }
            }
            catch
            {
                Retorno = "Erro no Metodo Insere_Historico_Entidade";
            }

            return Retorno;

        }


        public void Entidade_Listar()
        {

            string strSQL = "";
            DataTable Entidade = new DataTable();

            strSQL = "select top 1 Ent.EntCod, EntNome, EntCpfCgc, EntBair, EntCep, EntEnder, EntEnderComp, EntEnderNo, ";
            strSQL = strSQL + "EntRgIe, EntNomeFant, CidCod, EntCxaPost, EntTipoFJ, EntNat, StatEntCod, EntStatDescr, ";
            strSQL = strSQL + "Efo.EntFoneDDI , Efo.EntFoneDDD, Efo.EntFoneNum, Ewe.EntWebEMail, Ewe.EntWebWWW, ";
            strSQL = strSQL + "Eca.CategCodEstr, Cat.CategNome, Vet.VendCod, Ven.VendNome ";
            strSQL = strSQL + "from Entidade Ent ";
            strSQL = strSQL + "left join ENT_FONE Efo on Efo.EntCod = Ent.EntCod and Efo.EntFonePrinc = 'Sim' ";
            strSQL = strSQL + "left join ENT_WEB Ewe on Ewe.EntCod = Ent.EntCod and Ewe.EntWebTipo = 'Comercial' ";
            strSQL = strSQL + "left join VEND_ENT Vet on Vet.EntCod = Ent.EntCod and Vet.VendEntPrinc = 'Sim' ";
            strSQL = strSQL + "left join VENDEDOR Ven on Ven.VendCod = Vet.VendCod ";
            strSQL = strSQL + "left join ENT_CATEG Eca on Eca.EntCod = Ent.EntCod ";
            strSQL = strSQL + "left join CATEGORIA Cat on Cat.CategCodEstr = Eca.CategCodEstr ";
            strSQL = strSQL + "where Eca.CategCodEstr not in('20.60', '99') and Cat.CategNome like 'Entidade%' ";
            strSQL = strSQL + "and Ent.EntCod='" + EntCod.ToString() + "'";

            Entidade = Executa_DataTable(strSQL, "Entidade_Listar");

            if (Entidade.Rows.Count > 0)
            {
                foreach (DataRow row in Entidade.Rows)
                {
                    this.EntNome = (string)row["EntNome"].ToString();
                    this.EntCpfCgc = (string)row["EntCpfCgc"].ToString();
                    this.EntBair = (string)row["EntBair"].ToString();
                    this.EntCep = (string)row["EntCep"].ToString();
                    this.EntEnder = (string)row["EntEnder"].ToString();
                    this.EntEnderComp = (string)row["EntEnderComp"].ToString();
                    this.EntEnderNo = (string)row["EntEnderNo"].ToString();
                    this.EntRgIe = (string)row["EntRgIe"].ToString();
                    this.EntNomeFant = (string)row["EntNomeFant"].ToString();
                    this.CidCod = (string)row["CidCod"].ToString();
                    this.EntCxaPost = (string)row["EntCxaPost"].ToString();
                    this.EntTipoFJ = (string)row["EntTipoFJ"].ToString();
                    this.EntNat = (string)row["EntNat"].ToString();
                    this.StatEntCod = (string)row["StatEntCod"].ToString();
                    this.EntStatDescr = (string)row["EntStatDescr"].ToString();
                    this.EntFoneDDD = (string)row["EntFoneDDD"].ToString();
                    this.EntFoneNum = (string)row["EntFoneNum"].ToString();
                    this.EntWebEMail = (string)row["EntWebEMail"].ToString();
                    this.EntWebWWW = (string)row["EntWebWWW"].ToString();
                    this.VendCod = (string)row["VendCod"].ToString();
                    this.CategCodEstr = (string)row["CategCodEstr"].ToString();
                }
            }
        }


    }
}