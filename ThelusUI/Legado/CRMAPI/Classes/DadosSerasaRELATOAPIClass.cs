using System;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;
using System.Globalization;
using System.Reflection;

namespace CRMAPI.Classes
{
    public class DadosSerasaRELATOAPIClass : clsConexao
    {
        int IDCliente { get; set; }
        int IDUsuario { get; set; }
        int IDAnalise { get; set; }

        DataTable outputTable = new DataTable();

        string erro = "";

        JsonSerasaRELATOAPIClass objJsonSerasaRELATOAPIClass;

        JsonSerasaRELATOAPIClass.Report report;

        SQLUtilClass objSQLUtilClass = new SQLUtilClass();

        public DadosSerasaRELATOAPIClass(int IDCliente, int IDUsuario, JsonSerasaRELATOAPIClass objJsonSerasaRELATOAPIClass)
        {
            this.IDCliente = IDCliente;

            this.IDUsuario = IDUsuario;

            this.objJsonSerasaRELATOAPIClass = objJsonSerasaRELATOAPIClass;

            //Como apenas esta sendo usado o RELATORIO_AVANCADO_PJ considera-se o primeiro apenas
            this.report = objJsonSerasaRELATOAPIClass.reports[0];
        }

        public string GravaAnalise()
        {
            erro = GravaAnaliseSerasa();

            if (erro == "") erro = GravaAnaliseSerasaIdentificacao();

            if (erro == "") erro = GravaAnaliseSerasaAntecessora();

            if (erro == "") erro = GravaAnaliseSerasaAtividade();

            if (erro == "") erro = GravaAnaliseSerasaEndereco();

            if (erro == "") erro = GravaAnaliseSerasaLocalizacao();

            if (erro == "") erro = GravaAnaliseSerasaPefin();

            if (erro == "") erro = GravaAnaliseSerasaRefin();

            if (erro == "") erro = GravaAnaliseSerasaDividasVencidas();

            if (erro == "") erro = GravaAnaliseSerasaChequeSemFundoCCF();

            if (erro == "") erro = GravaAnaliseSerasaProtestos();

            if (erro == "") erro = GravaAnaliseSerasaAcaoJudicial();

            if (erro == "") erro = GravaAnaliseSerasaFalenciaConcordata();

            if (erro == "") erro = GravaAnaliseSerasaConsultas();

            if (erro == "") erro = GravaAnaliseSerasaUltimasConsultas();

            if (erro == "") erro = GravaAnaliseSerasaDetalhesSocios();

            if (erro == "") erro = GravaAnaliseSerasaContSocUltatuCapsoci();

            if (erro == "") erro = GravaAnaliseSerasaInfAdiSoc();

            if (erro == "") erro = GravaAnaliseSerasaInfAdicSoc();

            if (erro == "") erro = GravaAnaliseSerasaInfAdicSocComp();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_Socios();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_pefin();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_refin();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_collectionRecords();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_check();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_notary();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_facts();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_bankrupts();

            if (erro == "") erro = GravaAnaliseSerasaDetalhesAdministradores();

            if (erro == "") erro = GravaAnaliseSerasaInfAdiSoc_Adm();

            if (erro == "") erro = GravaAnaliseSerasaConcentreResumo_Adm();

            if (erro == "") erro = GravaAnaliseSerasaHistPagQtdTit();

            if (erro == "") erro = GravaAnaliseSerasaHistPagamentos_CargaPontual();

            if (erro == "") erro = GravaAnaliseSerasaHistPagamentos_Carga_8_15();

            if (erro == "") erro = GravaAnaliseSerasaHistPagamentos_Carga_16_30();

            if (erro == "") erro = GravaAnaliseSerasaHistPagamentos_Carga_31_60();

            if (erro == "") erro = GravaAnaliseSerasaHistPagamentos_Carga_mais_60();

            if (erro == "") erro = GravaAnaliseSerasaHistPagamentos_Carga_A_Vista();

            if (erro == "") erro = GravaAnaliseSerasaHistPagamentos_Total_Mes();

            if (erro == "") erro = GravaAnaliseSerasaReferenciaisNegocios();

            if (erro == "") erro = GravaAnaliseSerasaRelFornecedorPeriodo();

            if (erro == "") erro = GravaAnaliseSerasaRelacionamentoFornecedor();

            if (erro == "") erro = GravaAnaliseSerasaAnotSocAdm_partners();

            if (erro == "") erro = GravaAnaliseSerasaAnotSocAdm_administrators();

            if (erro == "") erro = GravaAnaliseSerasaEvolCompromisso();

            if (erro == "") erro = GravaAnaliseSerasaEvolCompromissoFor();

            if (erro == "") erro = GravaAnaliseSerasaInscricaoEstadual();

            if (erro == "") erro = GravaAnaliseSerasaDadosControle();

            if (erro == "") erro = GravaAnaliseSerasaContabilizacao();

            if (erro != "") erro += ApagaTabelasCasoDeErro();

            return erro;
        }

        public string GravaAnaliseSerasa()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@FATOR", SqlDbType.Int, 0, "FATOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeProduto", SqlDbType.VarChar, 8000, "NomeProduto"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;
                    dbCommand.Parameters["@FATOR"].Value = 0;
                    dbCommand.Parameters["@NomeProduto"].Value = "RELATOAPI";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();

                        IDAnalise = Convert.ToInt32(row["IDAnalise"]);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaIdentificacao()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_IDENTIFICACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@RAZAO", SqlDbType.VarChar, 8000, "RAZAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDCGCR", SqlDbType.VarChar, 8000, "CDCGCR"));
                    dbCommand.Parameters.Add(new SqlParameter("@NOMEFANTASIA", SqlDbType.VarChar, 8000, "NOMEFANTASIA"));
                    dbCommand.Parameters.Add(new SqlParameter("@NIRE", SqlDbType.VarChar, 8000, "NIRE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPSOC", SqlDbType.VarChar, 8000, "TPSOC"));
                    dbCommand.Parameters.Add(new SqlParameter("@OPCAOTRIBUTARIA", SqlDbType.VarChar, 8000, "OPCAOTRIBUTARIA"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDTPSC", SqlDbType.VarChar, 8000, "CDTPSC"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L010102";
                    dbCommand.Parameters["@IDINF"].Value = "1";
                    dbCommand.Parameters["@BCFIC"].Value = "1";
                    dbCommand.Parameters["@TPINF"].Value = "2";

                    dbCommand.Parameters["@RAZAO"].Value = report?.identificationReport?.companyName ?? "";
                    dbCommand.Parameters["@CDCGCR"].Value = report?.identificationReport?.documentNumber ?? "";
                    dbCommand.Parameters["@NOMEFANTASIA"].Value = report?.identificationReport?.companyAlias ?? "";
                    dbCommand.Parameters["@NIRE"].Value = report?.identificationReport?.nireNumber ?? "";
                    dbCommand.Parameters["@TPSOC"].Value = report?.identificationReport?.partnership ?? "";
                    dbCommand.Parameters["@OPCAOTRIBUTARIA"].Value = report?.identificationReport?.taxOption ?? "";
                    dbCommand.Parameters["@CDTPSC"].Value = report?.identificationReport?.legalNatureCode ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaAntecessora()
        {
            erro = "";

            try
            {
                foreach (JsonSerasaRELATOAPIClass.PredecessorList Predecessor in report?.identificationReport?.predecessorList)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ANTECESSORA", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                        dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                        dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                        dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                        dbCommand.Parameters.Add(new SqlParameter("@RAZAO", SqlDbType.VarChar, 8000, "RAZAO"));
                        dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                        dbCommand.Parameters.Add(new SqlParameter("@DTMTANTEC", SqlDbType.VarChar, 8000, "DTMTANTEC"));
                        dbCommand.Parameters.Add(new SqlParameter("@FILLER", SqlDbType.VarChar, 8000, "FILLER"));

                        dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                        dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                        dbCommand.Parameters["@PREFIXO"].Value = "L010102";
                        dbCommand.Parameters["@IDINF"].Value = "1";
                        dbCommand.Parameters["@BCFIC"].Value = "1";
                        dbCommand.Parameters["@TPINF"].Value = "2";

                        dbCommand.Parameters["@RAZAO"].Value = Predecessor?.predecessorName ?? "";
                        dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                        dbCommand.Parameters["@DTMTANTEC"].Value = Predecessor?.predecessorDate?.Replace("-", "") ?? "";
                        dbCommand.Parameters["@FILLER"].Value = "";

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }

                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaAtividade()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ATIVIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DATAFUND", SqlDbType.VarChar, 8000, "DATAFUND"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATACNPJ", SqlDbType.VarChar, 8000, "DATACNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@RAMOATV", SqlDbType.VarChar, 8000, "RAMOATV"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDSA", SqlDbType.VarChar, 8000, "CDSA"));
                    dbCommand.Parameters.Add(new SqlParameter("@NREMP", SqlDbType.VarChar, 8000, "NREMP"));
                    dbCommand.Parameters.Add(new SqlParameter("@PCCOMPRA", SqlDbType.VarChar, 8000, "PCCOMPRA"));
                    dbCommand.Parameters.Add(new SqlParameter("@PCVENDAS", SqlDbType.VarChar, 8000, "PCVENDAS"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRFIL", SqlDbType.VarChar, 8000, "NRFIL"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTFIL", SqlDbType.VarChar, 8000, "QTFIL"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNAE", SqlDbType.VarChar, 8000, "CNAE"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTINDOPER", SqlDbType.VarChar, 8000, "DTINDOPER"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L010103";
                    dbCommand.Parameters["@IDINF"].Value = "1";
                    dbCommand.Parameters["@BCFIC"].Value = "1";
                    dbCommand.Parameters["@TPINF"].Value = "3";

                    dbCommand.Parameters["@DATAFUND"].Value = report?.identificationReport?.companyFoundation?.Replace("-", "") ?? "";
                    dbCommand.Parameters["@DATACNPJ"].Value = report?.identificationReport?.companyRegisterDate?.Replace("-", "") ?? "";
                    dbCommand.Parameters["@RAMOATV"].Value = report?.identificationReport?.economicActivity ?? "";
                    dbCommand.Parameters["@CDSA"].Value = report?.identificationReport?.serasaActiveCode ?? "";
                    dbCommand.Parameters["@NREMP"].Value = report?.identificationReport?.numberEmployees ?? "";
                    dbCommand.Parameters["@PCCOMPRA"].Value = Convert.ToInt32(Convert.ToDecimal(report?.identificationReport?.importPurchases ?? "0")).ToString();
                    dbCommand.Parameters["@PCVENDAS"].Value = Convert.ToInt32(Convert.ToDecimal(report?.identificationReport?.exportSales ?? "0")).ToString();
                    dbCommand.Parameters["@NRFIL"].Value = report?.identificationReport?.branchOffices ?? "";
                    dbCommand.Parameters["@QTFIL"].Value = report?.identificationReport?.branchOffices ?? "";
                    dbCommand.Parameters["@CNAE"].Value = report?.identificationReport?.cnae ?? "";
                    dbCommand.Parameters["@DTINDOPER"].Value = report?.identificationReport?.updateDate?.Replace("-", "") ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaEndereco()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ENDERECO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@ENDER", SqlDbType.VarChar, 8000, "ENDER"));
                    dbCommand.Parameters.Add(new SqlParameter("@BAIRRO", SqlDbType.VarChar, 8000, "BAIRRO"));
                    dbCommand.Parameters.Add(new SqlParameter("@ENDEREC", SqlDbType.VarChar, 8000, "ENDEREC"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L010103";
                    dbCommand.Parameters["@IDINF"].Value = "1";
                    dbCommand.Parameters["@BCFIC"].Value = "1";
                    dbCommand.Parameters["@TPINF"].Value = "3";

                    dbCommand.Parameters["@ENDER"].Value = (report?.identificationReport?.address?.addressLine + report?.identificationReport?.address?.district) ?? "";
                    dbCommand.Parameters["@BAIRRO"].Value = report?.identificationReport?.address?.district ?? "";
                    dbCommand.Parameters["@ENDEREC"].Value = report?.identificationReport?.address?.addressLine ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaLocalizacao()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_LOCALIZACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@CIDADE", SqlDbType.VarChar, 8000, "CIDADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@UF", SqlDbType.VarChar, 8000, "UF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CEP", SqlDbType.VarChar, 8000, "CEP"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDDDD", SqlDbType.VarChar, 8000, "CDDDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRTEL1", SqlDbType.VarChar, 8000, "NRTEL1"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRFAX1", SqlDbType.VarChar, 8000, "NRFAX1"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDEB1", SqlDbType.VarChar, 8000, "CDEB1"));
                    dbCommand.Parameters.Add(new SqlParameter("@HOME", SqlDbType.VarChar, 8000, "HOME"));
                    dbCommand.Parameters.Add(new SqlParameter("@EMAIL", SqlDbType.VarChar, 8000, "EMAIL"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L010104";
                    dbCommand.Parameters["@IDINF"].Value = "1";
                    dbCommand.Parameters["@BCFIC"].Value = "1";
                    dbCommand.Parameters["@TPINF"].Value = "4";

                    dbCommand.Parameters["@CIDADE"].Value = report?.identificationReport?.address?.city ?? "";
                    dbCommand.Parameters["@UF"].Value = report?.identificationReport?.address?.state ?? "";
                    dbCommand.Parameters["@CEP"].Value = report?.identificationReport?.address?.zipCode ?? "";
                    dbCommand.Parameters["@CDDDD"].Value = report?.identificationReport?.phone?.areaCode ?? "";
                    dbCommand.Parameters["@NRTEL1"].Value = report?.identificationReport?.phone?.phoneNumber ?? "";
                    dbCommand.Parameters["@NRFAX1"].Value = "";
                    dbCommand.Parameters["@CDEB1"].Value = "";
                    dbCommand.Parameters["@HOME"].Value = report?.identificationReport?.companyUrl ?? "";
                    dbCommand.Parameters["@EMAIL"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaPefin()
        {
            erro = "";

            try
            {
                if (report.negativeData?.pefin?.pefinResponse != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.PefinResponse pefinResponse in report.negativeData?.pefin?.pefinResponse)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_PEFIN", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@legalNatureId", SqlDbType.VarChar, 8000, "legalNatureId"));

                            dbCommand.Parameters.Add(new SqlParameter("@QTDEOCOR", SqlDbType.VarChar, 8000, "QTDEOCOR"));
                            dbCommand.Parameters.Add(new SqlParameter("@ULTOCOR", SqlDbType.VarChar, 8000, "ULTOCOR"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAPEF", SqlDbType.VarChar, 8000, "DATAPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@TITULOPEF", SqlDbType.VarChar, 8000, "TITULOPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@AVALPEF", SqlDbType.VarChar, 8000, "AVALPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@VALOR", SqlDbType.VarChar, 8000, "VALOR"));
                            dbCommand.Parameters.Add(new SqlParameter("@CONTRA", SqlDbType.VarChar, 8000, "CONTRA"));
                            dbCommand.Parameters.Add(new SqlParameter("@ORIGEM", SqlDbType.VarChar, 8000, "ORIGEM"));
                            dbCommand.Parameters.Add(new SqlParameter("@FILIAL", SqlDbType.VarChar, 8000, "FILIAL"));
                            dbCommand.Parameters.Add(new SqlParameter("@PRACAPEF", SqlDbType.VarChar, 8000, "PRACAPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@DISTRPEF", SqlDbType.VarChar, 8000, "DISTRPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@VARAPEF", SqlDbType.VarChar, 8000, "VARAPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATASUBPEF", SqlDbType.VarChar, 8000, "DATASUBPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@PROCPEF", SqlDbType.VarChar, 8000, "PROCPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDNATUPEF", SqlDbType.VarChar, 8000, "CDNATUPEF"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                            dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                            dbCommand.Parameters.Add(new SqlParameter("@QTDEVALO", SqlDbType.VarChar, 8000, "QTDEVALO"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L040101";
                            dbCommand.Parameters["@IDINF"].Value = "4";
                            dbCommand.Parameters["@BCFIC"].Value = "1";
                            dbCommand.Parameters["@TPINF"].Value = "1";

                            dbCommand.Parameters["@legalNatureId"].Value = pefinResponse?.legalNatureId ?? "";

                            dbCommand.Parameters["@QTDEOCOR"].Value = report?.negativeData?.pefin?.summary?.count ?? "";
                            dbCommand.Parameters["@ULTOCOR"].Value = "0";
                            dbCommand.Parameters["@DATAPEF"].Value = pefinResponse?.occurrenceDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@TITULOPEF"].Value = pefinResponse?.legalNature ?? "";
                            dbCommand.Parameters["@AVALPEF"].Value = (pefinResponse?.principal ?? "") == "true" ? "S" : "N";
                            dbCommand.Parameters["@VALOR"].Value = pefinResponse?.amount ?? "";
                            dbCommand.Parameters["@CONTRA"].Value = pefinResponse?.contractId ?? "";
                            dbCommand.Parameters["@ORIGEM"].Value = pefinResponse?.creditorName ?? "";
                            dbCommand.Parameters["@FILIAL"].Value = pefinResponse?.federalUnit ?? "";
                            dbCommand.Parameters["@PRACAPEF"].Value = "";
                            dbCommand.Parameters["@DISTRPEF"].Value = "";
                            dbCommand.Parameters["@VARAPEF"].Value = "";
                            dbCommand.Parameters["@DATASUBPEF"].Value = "";
                            dbCommand.Parameters["@PROCPEF"].Value = "";
                            dbCommand.Parameters["@CDNATUPEF"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = report?.identificationReport?.documentNumber ?? "";
                            dbCommand.Parameters["@MSGSUBJUD"].Value = "";
                            dbCommand.Parameters["@QTDEVALO"].Value = report?.negativeData?.pefin?.summary?.balance ?? "";
                            dbCommand.Parameters["@RESERVADOSERASA2"].Value = "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaRefin()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.refin?.refinResponse != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.RefinResponse refinResponse in report.negativeData.refin.refinResponse)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_REFIN", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@legalNatureId", SqlDbType.VarChar, 8000, "legalNatureId"));

                            dbCommand.Parameters.Add(new SqlParameter("@QTDEOCOR", SqlDbType.VarChar, 8000, "QTDEOCOR"));
                            dbCommand.Parameters.Add(new SqlParameter("@ULTOCOR", SqlDbType.VarChar, 8000, "ULTOCOR"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAREF", SqlDbType.VarChar, 8000, "DATAREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@TITULOREF", SqlDbType.VarChar, 8000, "TITULOREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@AVALREF", SqlDbType.VarChar, 8000, "AVALREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@VALOR", SqlDbType.VarChar, 8000, "VALOR"));
                            dbCommand.Parameters.Add(new SqlParameter("@CONTRA", SqlDbType.VarChar, 8000, "CONTRA"));
                            dbCommand.Parameters.Add(new SqlParameter("@ORIGEM", SqlDbType.VarChar, 8000, "ORIGEM"));
                            dbCommand.Parameters.Add(new SqlParameter("@FILIAL", SqlDbType.VarChar, 8000, "FILIAL"));
                            dbCommand.Parameters.Add(new SqlParameter("@PRACAREF", SqlDbType.VarChar, 8000, "PRACAREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@DISTRREF", SqlDbType.VarChar, 8000, "DISTRREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@VARAREF", SqlDbType.VarChar, 8000, "VARAREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATASUBREF", SqlDbType.VarChar, 8000, "DATASUBREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@PROCREF", SqlDbType.VarChar, 8000, "PROCREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDNATUREF", SqlDbType.VarChar, 8000, "CDNATUREF"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                            dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                            dbCommand.Parameters.Add(new SqlParameter("@QTDEVALO", SqlDbType.VarChar, 8000, "QTDEVALO"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L040701";
                            dbCommand.Parameters["@IDINF"].Value = "4";
                            dbCommand.Parameters["@BCFIC"].Value = "7";
                            dbCommand.Parameters["@TPINF"].Value = "1";

                            dbCommand.Parameters["@legalNatureId"].Value = refinResponse?.legalNatureId ?? "";

                            dbCommand.Parameters["@QTDEOCOR"].Value = report?.negativeData?.refin?.summary?.count ?? "";
                            dbCommand.Parameters["@ULTOCOR"].Value = "0";
                            dbCommand.Parameters["@DATAREF"].Value = refinResponse?.occurrenceDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@TITULOREF"].Value = refinResponse?.legalNature ?? "";
                            dbCommand.Parameters["@AVALREF"].Value = (refinResponse?.principal ?? "") == "true" ? "S" : "N";
                            dbCommand.Parameters["@VALOR"].Value = refinResponse?.amount ?? "";
                            dbCommand.Parameters["@CONTRA"].Value = refinResponse?.contractId ?? "";
                            dbCommand.Parameters["@ORIGEM"].Value = refinResponse?.creditorName ?? "";
                            dbCommand.Parameters["@FILIAL"].Value = refinResponse?.federalUnit ?? "";
                            dbCommand.Parameters["@PRACAREF"].Value = "";
                            dbCommand.Parameters["@DISTRREF"].Value = "";
                            dbCommand.Parameters["@VARAREF"].Value = "";
                            dbCommand.Parameters["@DATASUBREF"].Value = "";
                            dbCommand.Parameters["@PROCREF"].Value = "";
                            dbCommand.Parameters["@CDNATUREF"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = report?.identificationReport?.documentNumber ?? "";
                            dbCommand.Parameters["@MSGSUBJUD"].Value = "";
                            dbCommand.Parameters["@QTDEVALO"].Value = report?.negativeData?.refin?.summary?.balance ?? "";
                            dbCommand.Parameters["@RESERVADOSERASA2"].Value = "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaDividasVencidas()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.collectionRecords?.collectionRecordsResponse != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.CollectionRecordsResponse collectionRecordsResponse
                        in report.negativeData.collectionRecords.collectionRecordsResponse)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_DIVIDAS_VENCIDAS", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@legalNatureId", SqlDbType.VarChar, 8000, "legalNatureId"));

                            dbCommand.Parameters.Add(new SqlParameter("@OCORDIV", SqlDbType.VarChar, 8000, "OCORDIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATADIV", SqlDbType.VarChar, 8000, "DATADIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@MODALI", SqlDbType.VarChar, 8000, "MODALI"));
                            dbCommand.Parameters.Add(new SqlParameter("@MOEDDIV", SqlDbType.VarChar, 8000, "MOEDDIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@VALODIV", SqlDbType.VarChar, 8000, "VALODIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@TITULODIV", SqlDbType.VarChar, 8000, "TITULODIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@INSTFI", SqlDbType.VarChar, 8000, "INSTFI"));
                            dbCommand.Parameters.Add(new SqlParameter("@LOCALDIV", SqlDbType.VarChar, 8000, "LOCALDIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDNATUDIV", SqlDbType.VarChar, 8000, "CDNATUDIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                            dbCommand.Parameters.Add(new SqlParameter("@PRACADIV", SqlDbType.VarChar, 8000, "PRACADIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@DISTRDIV", SqlDbType.VarChar, 8000, "DISTRDIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@VARADIV", SqlDbType.VarChar, 8000, "VARADIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATASUBDIV", SqlDbType.VarChar, 8000, "DATASUBDIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@PROCDIV", SqlDbType.VarChar, 8000, "PROCDIV"));
                            dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L040701";
                            dbCommand.Parameters["@IDINF"].Value = "4";
                            dbCommand.Parameters["@BCFIC"].Value = "7";
                            dbCommand.Parameters["@TPINF"].Value = "1";

                            dbCommand.Parameters["@legalNatureId"].Value = collectionRecordsResponse?.legalNatureId ?? "";

                            dbCommand.Parameters["@OCORDIV"].Value = report?.negativeData?.collectionRecords?.summary?.count ?? "";
                            dbCommand.Parameters["@DATADIV"].Value = collectionRecordsResponse?.occurrenceDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@MODALI"].Value = "DEV";
                            dbCommand.Parameters["@MOEDDIV"].Value = "R$";
                            dbCommand.Parameters["@VALODIV"].Value = collectionRecordsResponse?.amount ?? "";
                            dbCommand.Parameters["@TITULODIV"].Value = collectionRecordsResponse?.legalNature ?? "";
                            dbCommand.Parameters["@INSTFI"].Value = collectionRecordsResponse?.creditorName ?? "";
                            dbCommand.Parameters["@LOCALDIV"].Value = (collectionRecordsResponse?.federalUnit ?? ""
                                                                     + collectionRecordsResponse?.city ?? "");
                            dbCommand.Parameters["@CDNATUDIV"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = report?.identificationReport?.documentNumber ?? "";
                            dbCommand.Parameters["@PRACADIV"].Value = "";
                            dbCommand.Parameters["@DISTRDIV"].Value = "";
                            dbCommand.Parameters["@VARADIV"].Value = "";
                            dbCommand.Parameters["@DATASUBDIV"].Value = "";
                            dbCommand.Parameters["@PROCDIV"].Value = "";
                            dbCommand.Parameters["@MSGSUBJUD"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA2"].Value = "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaChequeSemFundoCCF()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.check?.checkResponse != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.CheckResponse checkResponse in report.negativeData.check.checkResponse)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CHEQUE_SEM_FUNDO_CCF", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@OCOR", SqlDbType.VarChar, 8000, "OCOR"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATACCF", SqlDbType.VarChar, 8000, "DATACCF"));
                            dbCommand.Parameters.Add(new SqlParameter("@CHEQUE", SqlDbType.VarChar, 8000, "CHEQUE"));
                            dbCommand.Parameters.Add(new SqlParameter("@QTDE", SqlDbType.VarChar, 8000, "QTDE"));
                            dbCommand.Parameters.Add(new SqlParameter("@BANCOCCF", SqlDbType.VarChar, 8000, "BANCOCCF"));
                            dbCommand.Parameters.Add(new SqlParameter("@AGENCCCF", SqlDbType.VarChar, 8000, "AGENCCCF"));
                            dbCommand.Parameters.Add(new SqlParameter("@CIDACCF", SqlDbType.VarChar, 8000, "CIDACCF"));
                            dbCommand.Parameters.Add(new SqlParameter("@UFCCF", SqlDbType.VarChar, 8000, "UFCCF"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDNATUACHEI", SqlDbType.VarChar, 8000, "CDNATUACHEI"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L040901";
                            dbCommand.Parameters["@IDINF"].Value = "4";
                            dbCommand.Parameters["@BCFIC"].Value = "9";
                            dbCommand.Parameters["@TPINF"].Value = "1";

                            dbCommand.Parameters["@OCOR"].Value = report?.negativeData?.check?.summary?.count ?? "";
                            dbCommand.Parameters["@DATACCF"].Value = checkResponse?.occurrenceDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@CHEQUE"].Value = checkResponse?.checkNumber ?? "";
                            dbCommand.Parameters["@QTDE"].Value = report?.negativeData?.check?.summary?.count ?? "";
                            dbCommand.Parameters["@BANCOCCF"].Value = checkResponse?.bankName ?? "";
                            dbCommand.Parameters["@AGENCCCF"].Value = checkResponse?.bankAgencyId ?? "";
                            dbCommand.Parameters["@CIDACCF"].Value = checkResponse?.city ?? "";
                            dbCommand.Parameters["@UFCCF"].Value = checkResponse?.federalUnit ?? "";
                            dbCommand.Parameters["@CDNATUACHEI"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = report?.identificationReport?.documentNumber ?? "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaProtestos()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.notary?.notaryResponse != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.NotaryResponse notaryResponse in report.negativeData.notary.notaryResponse)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_PROTESTOS", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@OCORPROT", SqlDbType.VarChar, 8000, "OCORPROT"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAPROT", SqlDbType.VarChar, 8000, "DATAPROT"));
                            dbCommand.Parameters.Add(new SqlParameter("@MOEDPROT", SqlDbType.VarChar, 8000, "MOEDPROT"));
                            dbCommand.Parameters.Add(new SqlParameter("@VALOPROT", SqlDbType.VarChar, 8000, "VALOPROT"));
                            dbCommand.Parameters.Add(new SqlParameter("@CART", SqlDbType.VarChar, 8000, "CART"));
                            dbCommand.Parameters.Add(new SqlParameter("@CIDAPROT", SqlDbType.VarChar, 8000, "CIDAPROT"));
                            dbCommand.Parameters.Add(new SqlParameter("@UFPROT", SqlDbType.VarChar, 8000, "UFPROT"));
                            dbCommand.Parameters.Add(new SqlParameter("@PRACAPRO", SqlDbType.VarChar, 8000, "PRACAPRO"));
                            dbCommand.Parameters.Add(new SqlParameter("@DISTRPRO", SqlDbType.VarChar, 8000, "DISTRPRO"));
                            dbCommand.Parameters.Add(new SqlParameter("@VARAPRO", SqlDbType.VarChar, 8000, "VARAPRO"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAPRO", SqlDbType.VarChar, 8000, "DATAPRO"));
                            dbCommand.Parameters.Add(new SqlParameter("@PROCPRO", SqlDbType.VarChar, 8000, "PROCPRO"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDNATUPRO", SqlDbType.VarChar, 8000, "CDNATUPRO"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPANUEPROT", SqlDbType.VarChar, 8000, "TPANUEPROT"));
                            dbCommand.Parameters.Add(new SqlParameter("@DTANUEPROT", SqlDbType.VarChar, 8000, "DTANUEPROT"));
                            dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L040301";
                            dbCommand.Parameters["@IDINF"].Value = "4";
                            dbCommand.Parameters["@BCFIC"].Value = "3";
                            dbCommand.Parameters["@TPINF"].Value = "1";

                            dbCommand.Parameters["@OCORPROT"].Value = report?.negativeData?.notary.summary?.count ?? "";
                            dbCommand.Parameters["@DATAPROT"].Value = notaryResponse?.occurrenceDate.Replace("-", "") ?? "";
                            dbCommand.Parameters["@MOEDPROT"].Value = "R$";
                            dbCommand.Parameters["@VALOPROT"].Value = notaryResponse?.amount ?? "";
                            dbCommand.Parameters["@CART"].Value = notaryResponse?.officeNumber ?? "";
                            dbCommand.Parameters["@CIDAPROT"].Value = notaryResponse?.city ?? "";
                            dbCommand.Parameters["@UFPROT"].Value = notaryResponse?.federalUnit ?? "";
                            dbCommand.Parameters["@PRACAPRO"].Value = "";
                            dbCommand.Parameters["@DISTRPRO"].Value = "";
                            dbCommand.Parameters["@VARAPRO"].Value = "";
                            dbCommand.Parameters["@DATAPRO"].Value = "";
                            dbCommand.Parameters["@PROCPRO"].Value = "";
                            dbCommand.Parameters["@CDNATUPRO"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = report?.identificationReport?.documentNumber ?? "";
                            dbCommand.Parameters["@TPANUEPROT"].Value = "";
                            dbCommand.Parameters["@DTANUEPROT"].Value = "";
                            dbCommand.Parameters["@MSGSUBJUD"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA2"].Value = "";

                            string comandoExec = objSQLUtilClass.MontarComandoExec(dbCommand);

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaAcaoJudicial()
        {
            erro = "";

            try
            {
                if (report?.facts?.judgementFilings?.judgementFilingsResponse != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.JudgementFilingsResponse judgementFilingsResponse
                        in report.facts.judgementFilings.judgementFilingsResponse)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ACAO_JUDICIAL", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@OCORACAO", SqlDbType.VarChar, 8000, "OCORACAO"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAACAO", SqlDbType.VarChar, 8000, "DATAACAO"));
                            dbCommand.Parameters.Add(new SqlParameter("@NATU", SqlDbType.VarChar, 8000, "NATU"));
                            dbCommand.Parameters.Add(new SqlParameter("@AVALACAO", SqlDbType.VarChar, 8000, "AVALACAO"));
                            dbCommand.Parameters.Add(new SqlParameter("@MOEDACAO", SqlDbType.VarChar, 8000, "MOEDACAO"));
                            dbCommand.Parameters.Add(new SqlParameter("@VALOACAO", SqlDbType.VarChar, 8000, "VALOACAO"));
                            dbCommand.Parameters.Add(new SqlParameter("@DIST", SqlDbType.VarChar, 8000, "DIST"));
                            dbCommand.Parameters.Add(new SqlParameter("@VARAACAO", SqlDbType.VarChar, 8000, "VARAACAO"));
                            dbCommand.Parameters.Add(new SqlParameter("@CIDAACAO", SqlDbType.VarChar, 8000, "CIDAACAO"));
                            dbCommand.Parameters.Add(new SqlParameter("@UFACAO", SqlDbType.VarChar, 8000, "UFACAO"));
                            dbCommand.Parameters.Add(new SqlParameter("@PRACAACO", SqlDbType.VarChar, 8000, "PRACAACO"));
                            dbCommand.Parameters.Add(new SqlParameter("@DISTRACO", SqlDbType.VarChar, 8000, "DISTRACO"));
                            dbCommand.Parameters.Add(new SqlParameter("@VARAACO", SqlDbType.VarChar, 8000, "VARAACO"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAACO", SqlDbType.VarChar, 8000, "DATAACO"));
                            dbCommand.Parameters.Add(new SqlParameter("@PROCACO", SqlDbType.VarChar, 8000, "PROCACO"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDNATUACO", SqlDbType.VarChar, 8000, "CDNATUACO"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                            dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L040401";
                            dbCommand.Parameters["@IDINF"].Value = "4";
                            dbCommand.Parameters["@BCFIC"].Value = "4";
                            dbCommand.Parameters["@TPINF"].Value = "1";

                            dbCommand.Parameters["@OCORACAO"].Value = report?.facts?.judgementFilings?.summary?.count ?? "";
                            dbCommand.Parameters["@DATAACAO"].Value = judgementFilingsResponse?.occurrenceDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@NATU"].Value = judgementFilingsResponse?.legalNature ?? "";
                            dbCommand.Parameters["@AVALACAO"].Value = (judgementFilingsResponse?.principal ?? "") == "true" ? "S" : "N";
                            dbCommand.Parameters["@MOEDACAO"].Value = "R$";
                            dbCommand.Parameters["@VALOACAO"].Value = judgementFilingsResponse?.amount ?? "";
                            dbCommand.Parameters["@DIST"].Value = judgementFilingsResponse?.distributor ?? "";
                            dbCommand.Parameters["@VARAACAO"].Value = "";
                            dbCommand.Parameters["@CIDAACAO"].Value = judgementFilingsResponse?.city ?? "";
                            dbCommand.Parameters["@UFACAO"].Value = judgementFilingsResponse?.state ?? "";
                            dbCommand.Parameters["@PRACAACO"].Value = "";
                            dbCommand.Parameters["@DISTRACO"].Value = "";
                            dbCommand.Parameters["@VARAACO"].Value = "";
                            dbCommand.Parameters["@DATAACO"].Value = "";
                            dbCommand.Parameters["@PROCACO"].Value = "";
                            dbCommand.Parameters["@CDNATUACO"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = report?.identificationReport?.documentNumber ?? "";
                            dbCommand.Parameters["@MSGSUBJUD"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA2"].Value = "";

                            string comandoExec = objSQLUtilClass.MontarComandoExec(dbCommand);

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaFalenciaConcordata()
        {
            erro = "";

            try
            {
                if (report?.facts?.bankrupts?.bankruptsResponse != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.BankruptsResponse bankruptsResponse in report.facts.bankrupts.bankruptsResponse)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_FALENCIA_CONCORDATA", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@OCORFAC", SqlDbType.VarChar, 8000, "OCORFAC"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAFAC", SqlDbType.VarChar, 8000, "DATAFAC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TIPOFAC", SqlDbType.VarChar, 8000, "TIPOFAC"));
                            dbCommand.Parameters.Add(new SqlParameter("@ORIGEMFAC", SqlDbType.VarChar, 8000, "ORIGEMFAC"));
                            dbCommand.Parameters.Add(new SqlParameter("@VARAFAC", SqlDbType.VarChar, 8000, "VARAFAC"));
                            dbCommand.Parameters.Add(new SqlParameter("@CIDAFAC", SqlDbType.VarChar, 8000, "CIDAFAC"));
                            dbCommand.Parameters.Add(new SqlParameter("@UFFAC", SqlDbType.VarChar, 8000, "UFFAC"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDNATUFAC", SqlDbType.VarChar, 8000, "CDNATUFAC"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L040601";
                            dbCommand.Parameters["@IDINF"].Value = "4";
                            dbCommand.Parameters["@BCFIC"].Value = "6";
                            dbCommand.Parameters["@TPINF"].Value = "1";

                            dbCommand.Parameters["@OCORFAC"].Value = report?.facts?.bankrupts?.summary?.count ?? "";
                            dbCommand.Parameters["@DATAFAC"].Value = bankruptsResponse?.eventDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@TIPOFAC"].Value = bankruptsResponse?.eventType ?? "";
                            dbCommand.Parameters["@ORIGEMFAC"].Value = bankruptsResponse?.origin ?? "";
                            dbCommand.Parameters["@VARAFAC"].Value = bankruptsResponse?.varaCourt ?? "";
                            dbCommand.Parameters["@CIDAFAC"].Value = bankruptsResponse?.city ?? "";
                            dbCommand.Parameters["@UFFAC"].Value = bankruptsResponse?.state ?? "";
                            dbCommand.Parameters["@CDNATUFAC"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = report?.identificationReport?.documentNumber ?? "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConsultas()
        {
            erro = "";

            try
            {
                if (report?.facts?.inquiryCompanyResponse?.quantity?.historical != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Historical historical in report.facts.inquiryCompanyResponse.quantity.historical)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONSULTAS", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@ANOCONS", SqlDbType.VarChar, 8000, "ANO-CONS"));
                            dbCommand.Parameters.Add(new SqlParameter("@MESCONS", SqlDbType.VarChar, 8000, "MES-CONS"));
                            dbCommand.Parameters.Add(new SqlParameter("@MESDESCOM", SqlDbType.VarChar, 8000, "MES-DES-COM"));
                            dbCommand.Parameters.Add(new SqlParameter("@QTDCONS", SqlDbType.VarChar, 8000, "QTD-CONS"));
                            dbCommand.Parameters.Add(new SqlParameter("@QTDBCOCONS", SqlDbType.VarChar, 8000, "QTD-BCO-CONS"));
                            dbCommand.Parameters.Add(new SqlParameter("@INDBCOEMP", SqlDbType.VarChar, 8000, "IND-BCO-EMP"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L030101";
                            dbCommand.Parameters["@IDINF"].Value = "3";
                            dbCommand.Parameters["@BCFIC"].Value = "1";
                            dbCommand.Parameters["@TPINF"].Value = "1";

                            DateTime date = DateTime.ParseExact(historical.inquiryDate, "yyyy-MM", CultureInfo.InvariantCulture);
                            string monthName = date.ToString("MMMM", new CultureInfo("pt-BR"));
                            string monthAbbreviation = date.ToString("MMM", new CultureInfo("pt-BR")).ToUpper();

                            dbCommand.Parameters["@ANOCONS"].Value = date.ToString("yyyy") ?? "";
                            dbCommand.Parameters["@MESCONS"].Value = date.ToString("MM") ?? "";
                            dbCommand.Parameters["@MESDESCOM"].Value = monthAbbreviation ?? "";
                            dbCommand.Parameters["@QTDCONS"].Value = historical?.occurrences ?? "";
                            dbCommand.Parameters["@QTDBCOCONS"].Value = "";
                            dbCommand.Parameters["@INDBCOEMP"].Value = "A";
                            dbCommand.Parameters["@RESERVADO"].Value = "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaUltimasConsultas()
        {
            erro = "";

            try
            {
                if (report?.facts?.inquiryCompanyResponse?.results != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Result result in report.facts.inquiryCompanyResponse.results)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ULTIMAS_CONSULTAS", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@DATACONS", SqlDbType.VarChar, 8000, "DATACONS"));
                            dbCommand.Parameters.Add(new SqlParameter("@NMCONS", SqlDbType.VarChar, 8000, "NMCONS"));
                            dbCommand.Parameters.Add(new SqlParameter("@QTCONS", SqlDbType.VarChar, 8000, "QTCONS"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJCONS", SqlDbType.VarChar, 8000, "CNPJCONS"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADO2", SqlDbType.VarChar, 8000, "RESERVADO2"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L030102";
                            dbCommand.Parameters["@IDINF"].Value = "3";
                            dbCommand.Parameters["@BCFIC"].Value = "1";
                            dbCommand.Parameters["@TPINF"].Value = "2";

                            dbCommand.Parameters["@DATACONS"].Value = result?.occurrenceDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@NMCONS"].Value = result?.companyName ?? "";
                            dbCommand.Parameters["@QTCONS"].Value = result?.daysQuantity ?? "";
                            dbCommand.Parameters["@CNPJCONS"].Value = result?.companyDocumentId ?? "";
                            dbCommand.Parameters["@RESERVADO"].Value = "";
                            dbCommand.Parameters["@RESERVADO2"].Value = "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaDetalhesSocios()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.partners != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Partner partner in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.partners)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_DETALHES_SOCIOS", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@IDENTCS", SqlDbType.VarChar, 8000, "IDENTCS"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJCPFCS", SqlDbType.VarChar, 8000, "CNPJCPFCS"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJSEQCS", SqlDbType.VarChar, 8000, "CNPJSEQCS"));
                            dbCommand.Parameters.Add(new SqlParameter("@DIGCPFCS", SqlDbType.VarChar, 8000, "DIGCPFCS"));
                            dbCommand.Parameters.Add(new SqlParameter("@NOMESOCIOCS", SqlDbType.VarChar, 8000, "NOMESOCIOCS"));
                            dbCommand.Parameters.Add(new SqlParameter("@NACIONALCS", SqlDbType.VarChar, 8000, "NACIONALCS"));
                            dbCommand.Parameters.Add(new SqlParameter("@PERCAPCS", SqlDbType.VarChar, 8000, "PERCAPCS"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAENTRACS", SqlDbType.VarChar, 8000, "DATAENTRACS"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESTRISOCIO", SqlDbType.VarChar, 8000, "RESTRISOCIO"));
                            dbCommand.Parameters.Add(new SqlParameter("@PERVOTCS", SqlDbType.VarChar, 8000, "PERVOTCS"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDSITRF", SqlDbType.VarChar, 8000, "CDSITRF"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDSASOCIO", SqlDbType.VarChar, 8000, "CDSASOCIO"));
                            dbCommand.Parameters.Add(new SqlParameter("@SITUACCS", SqlDbType.VarChar, 8000, "SITUACCS"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L010109";
                            dbCommand.Parameters["@IDINF"].Value = "1";
                            dbCommand.Parameters["@BCFIC"].Value = "1";
                            dbCommand.Parameters["@TPINF"].Value = "9";

                            dbCommand.Parameters["@IDENTCS"].Value = partner?.kindPerson ?? "";
                            dbCommand.Parameters["@CNPJCPFCS"].Value = partner?.document ?? "";
                            dbCommand.Parameters["@CNPJSEQCS"].Value = partner?.documentSequence ?? "";
                            dbCommand.Parameters["@DIGCPFCS"].Value = partner?.documentDigit ?? "";
                            dbCommand.Parameters["@NOMESOCIOCS"].Value = partner?.name ?? "";
                            dbCommand.Parameters["@NACIONALCS"].Value = partner?.nationality ?? "";
                            dbCommand.Parameters["@PERCAPCS"].Value = partner?.percentageCapital ?? "";
                            dbCommand.Parameters["@DATAENTRACS"].Value = partner?.entryDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@RESTRISOCIO"].Value = partner?.restrictionIndicator == "true" ? "S" : "N";
                            dbCommand.Parameters["@PERVOTCS"].Value = partner?.percentageVotingCapital ?? "0";
                            dbCommand.Parameters["@CDSITRF"].Value = "0";
                            dbCommand.Parameters["@CDSASOCIO"].Value = partner?.idNumber ?? "";
                            dbCommand.Parameters["@SITUACCS"].Value = "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaContSocUltatuCapsoci()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONT_SOC_ULTATU_CAPSOCl", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DATAULTATCS", SqlDbType.VarChar, 8000, "DATAULTATCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VRCAPSOCCS", SqlDbType.VarChar, 8000, "VRCAPSOCCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VRCAPREACS", SqlDbType.VarChar, 8000, "VRCAPREACS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VRCAPAUTCS", SqlDbType.VarChar, 8000, "VRCAPAUTCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCDNACS", SqlDbType.VarChar, 8000, "DESCDNACS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCDCRAOCS", SqlDbType.VarChar, 8000, "DESCDCRAOCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCPARCS", SqlDbType.VarChar, 8000, "DESCPARCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPRETCS", SqlDbType.VarChar, 8000, "TIPRETCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@SITUACCAPTOTAL", SqlDbType.VarChar, 8000, "SITUACCAPTOTAL"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L010108";
                    dbCommand.Parameters["@IDINF"].Value = "1";
                    dbCommand.Parameters["@BCFIC"].Value = "1";
                    dbCommand.Parameters["@TPINF"].Value = "8";

                    JsonSerasaRELATOAPIClass.ShareCapital shareCapital = objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.shareCapital;

                    if (shareCapital == null) return "";

                    if (shareCapital?.updateDate == null) return "";

                    dbCommand.Parameters["@DATAULTATCS"].Value = shareCapital?.updateDate.Replace("-", "") ?? "";
                    dbCommand.Parameters["@VRCAPSOCCS"].Value = shareCapital?.capitalValue ?? "0";
                    dbCommand.Parameters["@VRCAPREACS"].Value = shareCapital?.realizedCapitalValue ?? "0";
                    dbCommand.Parameters["@VRCAPAUTCS"].Value = "0";
                    dbCommand.Parameters["@DESCDNACS"].Value = shareCapital?.origin ?? "";
                    dbCommand.Parameters["@DESCDCRAOCS"].Value = shareCapital?.control ?? "";
                    dbCommand.Parameters["@DESCPARCS"].Value = shareCapital?.nature ?? "";
                    dbCommand.Parameters["@TIPRETCS"].Value = "";
                    dbCommand.Parameters["@SITUACCAPTOTAL"].Value = "";

                    string comandoExec = objSQLUtilClass.MontarComandoExec(dbCommand);

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaInfAdiSoc()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.partners != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Partner partner in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.partners)
                    {
                        if (partner.kindPerson == "F")
                        {
                            using (SqlConnection dbConnection = new SqlConnection(strConec))
                            {
                                //Abre Conexao
                                dbConnection.Open();

                                SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_INF_ADI_SOC", dbConnection);

                                dbCommand.CommandType = CommandType.StoredProcedure;

                                dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                                dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                                dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                                dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                                dbCommand.Parameters.Add(new SqlParameter("@CPF", SqlDbType.VarChar, 8000, "CPF"));
                                dbCommand.Parameters.Add(new SqlParameter("@SQCPF", SqlDbType.VarChar, 8000, "SQCPF"));
                                dbCommand.Parameters.Add(new SqlParameter("@DGCPF", SqlDbType.VarChar, 8000, "DGCPF"));
                                dbCommand.Parameters.Add(new SqlParameter("@DTATU", SqlDbType.VarChar, 8000, "DTATU"));
                                dbCommand.Parameters.Add(new SqlParameter("@NMPF", SqlDbType.VarChar, 8000, "NMPF"));
                                dbCommand.Parameters.Add(new SqlParameter("@NRRGGL", SqlDbType.VarChar, 8000, "NRRGGL"));
                                dbCommand.Parameters.Add(new SqlParameter("@DTNS", SqlDbType.VarChar, 8000, "DTNS"));
                                dbCommand.Parameters.Add(new SqlParameter("@VINCULO", SqlDbType.VarChar, 8000, "VINCULO"));
                                dbCommand.Parameters.Add(new SqlParameter("@CDEBNSHG", SqlDbType.VarChar, 8000, "CDEBNSHG"));
                                dbCommand.Parameters.Add(new SqlParameter("@UFNS", SqlDbType.VarChar, 8000, "UFNS"));
                                dbCommand.Parameters.Add(new SqlParameter("@DDD", SqlDbType.VarChar, 8000, "DDD"));
                                dbCommand.Parameters.Add(new SqlParameter("@FONE", SqlDbType.VarChar, 8000, "FONE"));
                                dbCommand.Parameters.Add(new SqlParameter("@RAMAL", SqlDbType.VarChar, 8000, "RAMAL"));
                                dbCommand.Parameters.Add(new SqlParameter("@NMLG", SqlDbType.VarChar, 8000, "NMLG"));
                                dbCommand.Parameters.Add(new SqlParameter("@DSBR", SqlDbType.VarChar, 8000, "DSBR"));
                                dbCommand.Parameters.Add(new SqlParameter("@CDEBHG", SqlDbType.VarChar, 8000, "CDEBHG"));
                                dbCommand.Parameters.Add(new SqlParameter("@CDUF", SqlDbType.VarChar, 8000, "CDUF"));
                                dbCommand.Parameters.Add(new SqlParameter("@CDCE", SqlDbType.VarChar, 8000, "CDCE"));
                                dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                                dbCommand.Parameters.Add(new SqlParameter("@SITUAC", SqlDbType.VarChar, 8000, "SITUAC"));

                                dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                                dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                                dbCommand.Parameters["@PREFIXO"].Value = "L010117";
                                dbCommand.Parameters["@IDINF"].Value = "1";
                                dbCommand.Parameters["@BCFIC"].Value = "1";
                                dbCommand.Parameters["@TPINF"].Value = "17";

                                JsonSerasaRELATOAPIClass.ShareCapital shareCapital = objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.shareCapital;

                                dbCommand.Parameters["@CPF"].Value = partner?.document ?? "";
                                dbCommand.Parameters["@SQCPF"].Value = partner?.documentSequence ?? "";
                                dbCommand.Parameters["@DGCPF"].Value = partner?.documentDigit ?? "";
                                dbCommand.Parameters["@DTATU"].Value = shareCapital?.updateDate?.Replace("-", "") ?? "";
                                dbCommand.Parameters["@NMPF"].Value = partner?.name ?? "";
                                dbCommand.Parameters["@NRRGGL"].Value = "";
                                dbCommand.Parameters["@DTNS"].Value = partner?.birthDate?.Replace("-", "") ?? "";
                                dbCommand.Parameters["@VINCULO"].Value = partner?.relationship ?? "";
                                dbCommand.Parameters["@CDEBNSHG"].Value = "";
                                dbCommand.Parameters["@UFNS"].Value = "";
                                dbCommand.Parameters["@DDD"].Value = partner?.phone?.areaCode ?? "";
                                dbCommand.Parameters["@FONE"].Value = partner?.phone?.phoneNumber ?? "";
                                dbCommand.Parameters["@RAMAL"].Value = "";

                                dbCommand.Parameters["@NMLG"].Value = partner?.address?.addressLine ?? "";
                                dbCommand.Parameters["@DSBR"].Value = partner?.address?.district ?? "";
                                dbCommand.Parameters["@CDEBHG"].Value = partner?.address?.city ?? "";
                                dbCommand.Parameters["@CDUF"].Value = partner?.address?.state ?? "";
                                dbCommand.Parameters["@CDCE"].Value = partner?.address?.zipCode ?? "";

                                dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                                dbCommand.Parameters["@SITUAC"].Value = "C";

                                using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                                {
                                    outputTable.Load(dataReader);
                                }

                                foreach (DataRow row in outputTable.Rows)
                                {
                                    erro = row["Erro"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaInfAdicSoc()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.partners != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Partner partner in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.partners)
                    {
                        if (partner.kindPerson == "J")
                        {
                            using (SqlConnection dbConnection = new SqlConnection(strConec))
                            {
                                //Abre Conexao
                                dbConnection.Open();

                                SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_INF_ADIC_SOC", dbConnection);

                                dbCommand.CommandType = CommandType.StoredProcedure;

                                dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                                dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                                dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                                dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                                dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 8000, "CNPJ"));
                                dbCommand.Parameters.Add(new SqlParameter("@FILIAL", SqlDbType.VarChar, 8000, "FILIAL"));
                                dbCommand.Parameters.Add(new SqlParameter("@DGCNPJ", SqlDbType.VarChar, 8000, "DGCNPJ"));
                                dbCommand.Parameters.Add(new SqlParameter("@DTFUND", SqlDbType.VarChar, 8000, "DTFUND"));
                                dbCommand.Parameters.Add(new SqlParameter("@DTATU", SqlDbType.VarChar, 8000, "DTATU"));
                                dbCommand.Parameters.Add(new SqlParameter("@RAZAO", SqlDbType.VarChar, 8000, "RAZAO"));
                                dbCommand.Parameters.Add(new SqlParameter("@NMFT", SqlDbType.VarChar, 8000, "NMFT"));
                                dbCommand.Parameters.Add(new SqlParameter("@VINCULO", SqlDbType.VarChar, 8000, "VINCULO"));
                                dbCommand.Parameters.Add(new SqlParameter("@SITUAC", SqlDbType.VarChar, 8000, "SITUAC"));

                                dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                                dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                                dbCommand.Parameters["@PREFIXO"].Value = "L010119";
                                dbCommand.Parameters["@IDINF"].Value = "1";
                                dbCommand.Parameters["@BCFIC"].Value = "1";
                                dbCommand.Parameters["@TPINF"].Value = "19";

                                JsonSerasaRELATOAPIClass.ShareCapital shareCapital = objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.shareCapital;

                                dbCommand.Parameters["@CNPJ"].Value = partner?.document ?? "";
                                dbCommand.Parameters["@FILIAL"].Value = partner?.documentBranch ?? "";
                                dbCommand.Parameters["@DGCNPJ"].Value = partner?.documentDigit ?? "";
                                dbCommand.Parameters["@DTFUND"].Value = partner?.foundationDate?.Replace("-", "") ?? "";
                                dbCommand.Parameters["@DTATU"].Value = shareCapital?.updateDate?.Replace("-", "") ?? "";
                                dbCommand.Parameters["@RAZAO"].Value = partner?.name ?? "";
                                dbCommand.Parameters["@NMFT"].Value = partner?.name ?? "";
                                dbCommand.Parameters["@VINCULO"].Value = partner?.relationship ?? "";
                                dbCommand.Parameters["@SITUAC"].Value = "C";

                                using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                                {
                                    outputTable.Load(dataReader);
                                }

                                foreach (DataRow row in outputTable.Rows)
                                {
                                    erro = row["Erro"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaInfAdicSocComp()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.partners != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Partner partner in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.partners)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_INF_ADIC_SOC_COMP", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@NMLG", SqlDbType.VarChar, 8000, "NMLG"));
                            dbCommand.Parameters.Add(new SqlParameter("@DSBR", SqlDbType.VarChar, 8000, "DSBR"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDEBHG", SqlDbType.VarChar, 8000, "CDEBHG"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDUF", SqlDbType.VarChar, 8000, "CDUF"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDCE", SqlDbType.VarChar, 8000, "CDCE"));
                            dbCommand.Parameters.Add(new SqlParameter("@DDD", SqlDbType.VarChar, 8000, "DDD"));
                            dbCommand.Parameters.Add(new SqlParameter("@FONE", SqlDbType.VarChar, 8000, "FONE"));
                            dbCommand.Parameters.Add(new SqlParameter("@RAMAL", SqlDbType.VarChar, 8000, "RAMAL"));
                            dbCommand.Parameters.Add(new SqlParameter("@RAMO", SqlDbType.VarChar, 8000, "RAMO"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L010120";
                            dbCommand.Parameters["@IDINF"].Value = "1";
                            dbCommand.Parameters["@BCFIC"].Value = "1";
                            dbCommand.Parameters["@TPINF"].Value = "20";

                            dbCommand.Parameters["@NMLG"].Value = partner?.address?.addressLine ?? "";
                            dbCommand.Parameters["@DSBR"].Value = partner?.address?.district ?? "";
                            dbCommand.Parameters["@CDEBHG"].Value = partner?.address?.city ?? "";
                            dbCommand.Parameters["@CDUF"].Value = partner?.address?.state ?? "";
                            dbCommand.Parameters["@CDCE"].Value = partner?.address?.zipCode ?? "";

                            dbCommand.Parameters["@DDD"].Value = partner?.phone?.areaCode ?? "";
                            dbCommand.Parameters["@FONE"].Value = partner?.phone?.phoneNumber ?? "";
                            dbCommand.Parameters["@RAMAL"].Value = "";

                            dbCommand.Parameters["@RAMO"].Value = "";
                            dbCommand.Parameters["@CNPJCPF"].Value = partner?.document ?? "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_Socios()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.partners != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Partner partner in
                        objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.partners)
                    {
                        //string TOTALRES = "";

                        //{
                        //    decimal somaTOTALRES = 0;

                        //    foreach (JsonSerasaRELATOAPIClass.Debt debt in partner.debts)
                        //    {
                        //        somaTOTALRES += Convert.ToDecimal(debt?.summary?.balance.Replace(".", ","));
                        //    }

                        //    TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                        //}

                        foreach (JsonSerasaRELATOAPIClass.Debt debt in partner.debts)
                        {
                            using (SqlConnection dbConnection = new SqlConnection(strConec))
                            {
                                //Abre Conexao
                                dbConnection.Open();

                                SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                                dbCommand.CommandType = CommandType.StoredProcedure;

                                dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                                dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                                dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                                dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                                dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                                dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                                dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                                dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                                dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                                dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                                dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                                dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                                dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                                dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                                dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                                dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                                dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                                dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                                dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                                dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                                dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                                dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                                dbCommand.Parameters["@IDINF"].Value = "4";
                                dbCommand.Parameters["@BCFIC"].Value = "2";
                                dbCommand.Parameters["@TPINF"].Value = "2";

                                if ((debt?.summary?.count.ToString() ?? "0") == "0") continue;
                                if ((debt?.summary?.firstOccurrence ?? "") == "" && (debt?.summary?.lastOccurrence ?? "") == "") continue;

                                dbCommand.Parameters["@QTDERES"].Value = debt?.summary?.count.ToString() ?? "";
                                dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA(debt?.debtType, "DISC");
                                dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA(debt?.debtType, "NATUREZA");
                                dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                                dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.firstOccurrence ?? "", "MM");
                                dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.firstOccurrence ?? "", "yyyy");
                                dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                                dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.lastOccurrence ?? "", "MM");
                                dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.lastOccurrence ?? "", "yyyy");
                                dbCommand.Parameters["@MOED"].Value = "R$";
                                dbCommand.Parameters["@VALO"].Value = debt?.summary?.balance ?? "";
                                dbCommand.Parameters["@ORIG"].Value = "";
                                dbCommand.Parameters["@AGPR"].Value = "";
                                dbCommand.Parameters["@TOTALRES"].Value = debt?.summary?.balance ?? "";
                                dbCommand.Parameters["@CNPJCPF"].Value = partner?.document ?? "";

                                string comandoExec = objSQLUtilClass.MontarComandoExec(dbCommand);

                                using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                                {
                                    outputTable.Load(dataReader);
                                }

                                foreach (DataRow row in outputTable.Rows)
                                {
                                    erro = row["Erro"].ToString();

                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_pefin()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.pefin != null)
                {
                    string TOTALRES = "";

                    {
                        decimal somaTOTALRES = Convert.ToDecimal(report?.negativeData?.pefin?.summary?.balance?.Replace(".", ",") ?? "0");

                        TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                    }

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                        dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                        dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                        dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                        dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                        dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                        dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                        dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                        dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                        dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                        dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                        dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                        dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                        dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                        dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                        dbCommand.Parameters["@IDINF"].Value = "4";
                        dbCommand.Parameters["@BCFIC"].Value = "2";
                        dbCommand.Parameters["@TPINF"].Value = "2";

                        if ((report?.negativeData?.pefin?.summary?.count.ToString() ?? "0") == "0") return "";

                        if ((report?.negativeData?.pefin?.summary?.firstOccurrence ?? "") == ""
                         && (report?.negativeData?.pefin?.summary?.lastOccurrence ?? "") == "") return "";

                        dbCommand.Parameters["@QTDERES"].Value = report?.negativeData?.pefin?.summary?.count ?? "";
                        dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("COLLECTIONRECORDS", "DISC");
                        dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("COLLECTIONRECORDS", "NATUREZA");
                        dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.pefin?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.pefin?.summary?.firstOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.pefin?.summary?.firstOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.pefin?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.pefin?.summary?.lastOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.pefin?.summary?.lastOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MOED"].Value = "R$";
                        dbCommand.Parameters["@VALO"].Value = report?.negativeData?.pefin?.summary?.balance ?? "";
                        dbCommand.Parameters["@ORIG"].Value = "";
                        dbCommand.Parameters["@AGPR"].Value = "";
                        dbCommand.Parameters["@TOTALRES"].Value = TOTALRES ?? "";
                        dbCommand.Parameters["@CNPJCPF"].Value = report?.identificationReport?.documentNumber ?? "";

                        string comandoExec = objSQLUtilClass.MontarComandoExec(dbCommand);

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }

                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_refin()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.refin != null)
                {
                    string TOTALRES = "";

                    {
                        decimal somaTOTALRES = Convert.ToDecimal(report?.negativeData?.refin?.summary?.balance?.Replace(".", ",") ?? "0");

                        TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                    }

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                        dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                        dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                        dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                        dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                        dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                        dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                        dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                        dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                        dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                        dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                        dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                        dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                        dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                        dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                        dbCommand.Parameters["@IDINF"].Value = "4";
                        dbCommand.Parameters["@BCFIC"].Value = "2";
                        dbCommand.Parameters["@TPINF"].Value = "2";

                        if ((report?.negativeData?.refin?.summary?.count.ToString() ?? "0") == "0") return "";

                        if ((report?.negativeData?.refin?.summary?.firstOccurrence ?? "") == ""
                         && (report?.negativeData?.refin?.summary?.lastOccurrence ?? "") == "") return "";

                        dbCommand.Parameters["@QTDERES"].Value = report?.negativeData?.refin?.summary?.count ?? "";
                        dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("FINANCIAL", "DISC");
                        dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("FINANCIAL", "NATUREZA");
                        dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.refin?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.refin?.summary?.firstOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.refin?.summary?.firstOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.refin?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.refin?.summary?.lastOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.refin?.summary?.lastOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MOED"].Value = "R$";
                        dbCommand.Parameters["@VALO"].Value = report?.negativeData?.refin?.summary?.balance ?? "";
                        dbCommand.Parameters["@ORIG"].Value = "";
                        dbCommand.Parameters["@AGPR"].Value = "";
                        dbCommand.Parameters["@TOTALRES"].Value = TOTALRES ?? "";
                        dbCommand.Parameters["@CNPJCPF"].Value = report?.identificationReport?.documentNumber ?? "";

                        string comandoExec = objSQLUtilClass.MontarComandoExec(dbCommand);

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }

                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_collectionRecords()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.collectionRecords != null)
                {
                    string TOTALRES = "";

                    {
                        decimal somaTOTALRES = Convert.ToDecimal(report?.negativeData?.collectionRecords?.summary?.balance?.Replace(".", ",") ?? "0");

                        TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                    }

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                        dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                        dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                        dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                        dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                        dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                        dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                        dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                        dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                        dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                        dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                        dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                        dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                        dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                        dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                        dbCommand.Parameters["@IDINF"].Value = "4";
                        dbCommand.Parameters["@BCFIC"].Value = "2";
                        dbCommand.Parameters["@TPINF"].Value = "2";

                        if ((report?.negativeData?.collectionRecords?.summary?.count.ToString() ?? "0") == "0") return "";

                        if ((report?.negativeData?.collectionRecords?.summary?.firstOccurrence ?? "") == ""
                         && (report?.negativeData?.collectionRecords?.summary?.lastOccurrence ?? "") == "") return "";

                        dbCommand.Parameters["@QTDERES"].Value = report?.negativeData?.collectionRecords?.summary?.count ?? "";
                        dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("MARKET", "DISC");
                        dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("MARKET", "NATUREZA");
                        dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.collectionRecords?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.collectionRecords?.summary?.firstOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.collectionRecords?.summary?.firstOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.collectionRecords?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.collectionRecords?.summary?.lastOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.collectionRecords?.summary?.lastOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MOED"].Value = "R$";
                        dbCommand.Parameters["@VALO"].Value = report?.negativeData?.collectionRecords?.summary?.balance ?? "";
                        dbCommand.Parameters["@ORIG"].Value = "";
                        dbCommand.Parameters["@AGPR"].Value = "";
                        dbCommand.Parameters["@TOTALRES"].Value = TOTALRES ?? "";
                        dbCommand.Parameters["@CNPJCPF"].Value = report?.identificationReport?.documentNumber ?? "";

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }

                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_check()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.check != null)
                {
                    string TOTALRES = "";

                    {
                        decimal somaTOTALRES = Convert.ToDecimal(report?.negativeData?.check?.summary?.balance?.Replace(".", ",") ?? "0");

                        TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                    }

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                        dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                        dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                        dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                        dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                        dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                        dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                        dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                        dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                        dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                        dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                        dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                        dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                        dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                        dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                        dbCommand.Parameters["@IDINF"].Value = "4";
                        dbCommand.Parameters["@BCFIC"].Value = "2";
                        dbCommand.Parameters["@TPINF"].Value = "2";

                        if ((report?.negativeData?.check?.summary?.count.ToString() ?? "0") == "0") return "";

                        if ((report?.negativeData?.check?.summary?.firstOccurrence ?? "") == ""
                         && (report?.negativeData?.check?.summary?.lastOccurrence ?? "") == "") return "";

                        dbCommand.Parameters["@QTDERES"].Value = report?.negativeData?.check?.summary?.count ?? "";
                        dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("CHECKCCF", "DISC");
                        dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("CHECKCCF", "NATUREZA");
                        dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.check?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.check?.summary?.firstOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.check?.summary?.firstOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.check?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.check?.summary?.lastOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.check?.summary?.lastOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MOED"].Value = "R$";
                        dbCommand.Parameters["@VALO"].Value = report?.negativeData?.check?.summary?.balance ?? "";
                        dbCommand.Parameters["@ORIG"].Value = "";
                        dbCommand.Parameters["@AGPR"].Value = "";
                        dbCommand.Parameters["@TOTALRES"].Value = TOTALRES ?? "";
                        dbCommand.Parameters["@CNPJCPF"].Value = report?.identificationReport?.documentNumber ?? "";

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }

                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_notary()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.notary != null)
                {
                    string TOTALRES = "";

                    {
                        decimal somaTOTALRES = Convert.ToDecimal(report?.negativeData?.notary?.summary?.balance?.Replace(".", ",") ?? "0");

                        TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                    }

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                        dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                        dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                        dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                        dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                        dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                        dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                        dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                        dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                        dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                        dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                        dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                        dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                        dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                        dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                        dbCommand.Parameters["@IDINF"].Value = "4";
                        dbCommand.Parameters["@BCFIC"].Value = "2";
                        dbCommand.Parameters["@TPINF"].Value = "2";

                        if ((report?.negativeData?.notary?.summary?.count.ToString() ?? "0") == "0") return "";

                        if ((report?.negativeData?.notary?.summary?.firstOccurrence ?? "") == ""
                         && (report?.negativeData?.notary?.summary?.lastOccurrence ?? "") == "") return "";

                        dbCommand.Parameters["@QTDERES"].Value = report?.negativeData?.notary?.summary?.count ?? "";
                        dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("NOTARY", "DISC");
                        dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("NOTARY", "NATUREZA");
                        dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.notary?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.notary?.summary?.firstOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.notary?.summary?.firstOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.notary?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.notary?.summary?.lastOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.notary?.summary?.lastOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MOED"].Value = "R$";
                        dbCommand.Parameters["@VALO"].Value = report?.negativeData?.notary?.summary?.balance ?? "";
                        dbCommand.Parameters["@ORIG"].Value = "";
                        dbCommand.Parameters["@AGPR"].Value = "";
                        dbCommand.Parameters["@TOTALRES"].Value = TOTALRES ?? "";
                        dbCommand.Parameters["@CNPJCPF"].Value = report?.identificationReport?.documentNumber ?? "";

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }

                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_facts()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.facts != null)
                {
                    string TOTALRES = "";

                    {
                        decimal somaTOTALRES = Convert.ToDecimal(report?.negativeData?.facts?.judgementFilings?.summary?.balance?.Replace(".", ",") ?? "0");

                        TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                    }

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                        dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                        dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                        dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                        dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                        dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                        dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                        dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                        dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                        dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                        dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                        dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                        dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                        dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                        dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                        dbCommand.Parameters["@IDINF"].Value = "4";
                        dbCommand.Parameters["@BCFIC"].Value = "2";
                        dbCommand.Parameters["@TPINF"].Value = "2";

                        if ((report?.negativeData?.facts?.judgementFilings?.summary?.count.ToString() ?? "0") == "0") return "";

                        if ((report?.negativeData?.facts?.judgementFilings?.summary?.firstOccurrence ?? "") == ""
                         && (report?.negativeData?.facts?.judgementFilings?.summary?.lastOccurrence ?? "") == "") return "";

                        dbCommand.Parameters["@QTDERES"].Value = report?.negativeData?.facts?.judgementFilings?.summary?.count ?? "";
                        dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("JUDGEMENTFILINGS", "DISC");
                        dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("JUDGEMENTFILINGS", "NATUREZA");
                        dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.facts?.judgementFilings?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.facts?.judgementFilings?.summary?.firstOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.facts?.judgementFilings?.summary?.firstOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.facts?.judgementFilings?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.facts?.judgementFilings?.summary?.lastOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.facts?.judgementFilings?.summary?.lastOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MOED"].Value = "R$";
                        dbCommand.Parameters["@VALO"].Value = report?.negativeData?.facts?.judgementFilings?.summary?.balance ?? "";
                        dbCommand.Parameters["@ORIG"].Value = "";
                        dbCommand.Parameters["@AGPR"].Value = "";
                        dbCommand.Parameters["@TOTALRES"].Value = TOTALRES ?? "";
                        dbCommand.Parameters["@CNPJCPF"].Value = report?.identificationReport?.documentNumber ?? "";

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }

                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_bankrupts()
        {
            erro = "";

            try
            {
                if (report?.negativeData?.bankrupts != null)
                {
                    string TOTALRES = "";

                    {
                        decimal somaTOTALRES = Convert.ToDecimal(report?.negativeData?.bankrupts?.summary?.balance?.Replace(".", ",") ?? "0");

                        TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                    }

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                        dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                        dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                        dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                        dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                        dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                        dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                        dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                        dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                        dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                        dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                        dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                        dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                        dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                        dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                        dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                        dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                        dbCommand.Parameters["@IDINF"].Value = "4";
                        dbCommand.Parameters["@BCFIC"].Value = "2";
                        dbCommand.Parameters["@TPINF"].Value = "2";

                        if ((report?.negativeData?.bankrupts?.summary?.count.ToString() ?? "0") == "0") return "";

                        if ((report?.negativeData?.bankrupts?.summary?.firstOccurrence ?? "") == ""
                         && (report?.negativeData?.bankrupts?.summary?.lastOccurrence ?? "") == "") return "";

                        dbCommand.Parameters["@QTDERES"].Value = report?.negativeData?.bankrupts?.summary?.count ?? "";
                        dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("BANKRUPTSPATICIPATION", "DISC");
                        dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA("BANKRUPTSPATICIPATION", "NATUREZA");
                        dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.bankrupts?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.bankrupts?.summary?.firstOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.bankrupts?.summary?.firstOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.bankrupts?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                        dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.bankrupts?.summary?.lastOccurrence ?? "", "MM");
                        dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(report?.negativeData?.bankrupts?.summary?.lastOccurrence ?? "", "yyyy");
                        dbCommand.Parameters["@MOED"].Value = "R$";
                        dbCommand.Parameters["@VALO"].Value = report?.negativeData?.bankrupts?.summary?.balance ?? "";
                        dbCommand.Parameters["@ORIG"].Value = "";
                        dbCommand.Parameters["@AGPR"].Value = "";
                        dbCommand.Parameters["@TOTALRES"].Value = TOTALRES ?? "";
                        dbCommand.Parameters["@CNPJCPF"].Value = report?.identificationReport?.documentNumber ?? "";

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }

                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        protected string RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA(string debtType, string tipo)
        {
            string DISC = "", NATUREZA = "";

            switch (debtType)
            {
                case "FINANCIAL":
                    DISC = "REFIN";
                    NATUREZA = "01";
                    break;
                case "NOTARY":
                    DISC = "PROTESTO";
                    NATUREZA = "03";
                    break;
                case "JUDGEMENTFILINGS":
                    DISC = "ACAO JUDICIAL";
                    NATUREZA = "04";
                    break;
                case "BANKRUPTSPATICIPATION":
                    DISC = "PARTICIPACAO EM FALENCIA";
                    NATUREZA = "05";
                    break;
                case "MARKET":
                    DISC = "PEFIN";
                    NATUREZA = "07";
                    break;
                case "CHECKCCF":
                    DISC = "CHEQUE";
                    NATUREZA = "09";
                    break;
                case "COLLECTIONRECORDS":
                    DISC = "PEFIN";
                    NATUREZA = "10";
                    break;
                default:
                    DISC = "Falência/concordata";
                    NATUREZA = "06";
                    break;
            }

            if (tipo == "DISC") return DISC;
            else if (tipo == "NATUREZA") return NATUREZA;

            return "";
        }

        protected string RetornaAnaliseSerasaConcentreResumo_DataFormatacao(string data, string formatacao)
        {
            if (data != "")
            {
                DateTime Occurrence = DateTime.ParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                string OccurrenceMonthName = Occurrence.ToString("MMMM", new CultureInfo("pt-BR"));

                string monthAbbreviation = Occurrence.ToString("MMM", new CultureInfo("pt-BR")).ToUpper();

                if (formatacao == "monthAbbreviation") return monthAbbreviation ?? "";
                else if (formatacao == "MM") return Occurrence.ToString("MM") ?? "";
                else if (formatacao == "yyyy") return Occurrence.ToString("yyyy") ?? "";
            }

            return "";
        }

        public string GravaAnaliseSerasaDetalhesAdministradores()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.administrators != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Administrator administrator in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.administrators)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_DETALHES_ADMINISTRADORES", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@IDENTADM", SqlDbType.VarChar, 8000, "IDENTADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJCPFADM", SqlDbType.VarChar, 8000, "CNPJCPFADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJSEQADM", SqlDbType.VarChar, 8000, "CNPJSEQADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@DIGCPFADM", SqlDbType.VarChar, 8000, "DIGCPFADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@NOMEADM", SqlDbType.VarChar, 8000, "NOMEADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@CARGOADM", SqlDbType.VarChar, 8000, "CARGOADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@NACIONALADM", SqlDbType.VarChar, 8000, "NACIONALADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@ESTCIVILADM", SqlDbType.VarChar, 8000, "ESTCIVILADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAINIMANDATOADM", SqlDbType.VarChar, 8000, "DATAINIMANDATOADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAFIMMANDATOADM", SqlDbType.VarChar, 8000, "DATAFIMMANDATOADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESTRIADMI", SqlDbType.VarChar, 8000, "RESTRIADMI"));
                            dbCommand.Parameters.Add(new SqlParameter("@CARGOADMI", SqlDbType.VarChar, 8000, "CARGOADMI"));
                            dbCommand.Parameters.Add(new SqlParameter("@CDSITRF", SqlDbType.VarChar, 8000, "CDSITRF"));
                            dbCommand.Parameters.Add(new SqlParameter("@DATAENTRAADM", SqlDbType.VarChar, 8000, "DATAENTRAADM"));
                            dbCommand.Parameters.Add(new SqlParameter("@SITUACADM", SqlDbType.VarChar, 8000, "SITUACADM"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L010111";
                            dbCommand.Parameters["@IDINF"].Value = "1";
                            dbCommand.Parameters["@BCFIC"].Value = "1";
                            dbCommand.Parameters["@TPINF"].Value = "11";

                            dbCommand.Parameters["@IDENTADM"].Value = administrator?.kindPerson ?? "";
                            dbCommand.Parameters["@CNPJCPFADM"].Value = administrator?.document ?? "";
                            dbCommand.Parameters["@CNPJSEQADM"].Value = administrator?.documentSequence ?? "";
                            dbCommand.Parameters["@DIGCPFADM"].Value = administrator?.documentDigit ?? "";
                            dbCommand.Parameters["@NOMEADM"].Value = administrator?.name ?? "";
                            dbCommand.Parameters["@CARGOADM"].Value = administrator?.office ?? "";
                            dbCommand.Parameters["@NACIONALADM"].Value = administrator?.nationality ?? "";
                            dbCommand.Parameters["@ESTCIVILADM"].Value = administrator?.maritalStatus ?? "";
                            dbCommand.Parameters["@DATAINIMANDATOADM"].Value = administrator?.startDateTerm?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@DATAFIMMANDATOADM"].Value = administrator?.endDateTerm?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@RESTRIADMI"].Value = (administrator?.restrictionIndicator ?? "") == "true" ? "S" : "N";
                            dbCommand.Parameters["@CARGOADMI"].Value = "";
                            dbCommand.Parameters["@CDSITRF"].Value = "2";
                            dbCommand.Parameters["@DATAENTRAADM"].Value = administrator?.entryDate?.Replace("-", "") ?? "";
                            dbCommand.Parameters["@SITUACADM"].Value = "C";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaInfAdiSoc_Adm()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.administrators != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Administrator administrator in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.administrators)
                    {
                        if (administrator.kindPerson == "F")
                        {
                            using (SqlConnection dbConnection = new SqlConnection(strConec))
                            {
                                //Abre Conexao
                                dbConnection.Open();

                                SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_INF_ADI_SOC", dbConnection);

                                dbCommand.CommandType = CommandType.StoredProcedure;

                                dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                                dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                                dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                                dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                                dbCommand.Parameters.Add(new SqlParameter("@CPF", SqlDbType.VarChar, 8000, "CPF"));
                                dbCommand.Parameters.Add(new SqlParameter("@SQCPF", SqlDbType.VarChar, 8000, "SQCPF"));
                                dbCommand.Parameters.Add(new SqlParameter("@DGCPF", SqlDbType.VarChar, 8000, "DGCPF"));
                                dbCommand.Parameters.Add(new SqlParameter("@DTATU", SqlDbType.VarChar, 8000, "DTATU"));
                                dbCommand.Parameters.Add(new SqlParameter("@NMPF", SqlDbType.VarChar, 8000, "NMPF"));
                                dbCommand.Parameters.Add(new SqlParameter("@NRRGGL", SqlDbType.VarChar, 8000, "NRRGGL"));
                                dbCommand.Parameters.Add(new SqlParameter("@DTNS", SqlDbType.VarChar, 8000, "DTNS"));
                                dbCommand.Parameters.Add(new SqlParameter("@VINCULO", SqlDbType.VarChar, 8000, "VINCULO"));
                                dbCommand.Parameters.Add(new SqlParameter("@CDEBNSHG", SqlDbType.VarChar, 8000, "CDEBNSHG"));
                                dbCommand.Parameters.Add(new SqlParameter("@UFNS", SqlDbType.VarChar, 8000, "UFNS"));
                                dbCommand.Parameters.Add(new SqlParameter("@DDD", SqlDbType.VarChar, 8000, "DDD"));
                                dbCommand.Parameters.Add(new SqlParameter("@FONE", SqlDbType.VarChar, 8000, "FONE"));
                                dbCommand.Parameters.Add(new SqlParameter("@RAMAL", SqlDbType.VarChar, 8000, "RAMAL"));
                                dbCommand.Parameters.Add(new SqlParameter("@NMLG", SqlDbType.VarChar, 8000, "NMLG"));
                                dbCommand.Parameters.Add(new SqlParameter("@DSBR", SqlDbType.VarChar, 8000, "DSBR"));
                                dbCommand.Parameters.Add(new SqlParameter("@CDEBHG", SqlDbType.VarChar, 8000, "CDEBHG"));
                                dbCommand.Parameters.Add(new SqlParameter("@CDUF", SqlDbType.VarChar, 8000, "CDUF"));
                                dbCommand.Parameters.Add(new SqlParameter("@CDCE", SqlDbType.VarChar, 8000, "CDCE"));
                                dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                                dbCommand.Parameters.Add(new SqlParameter("@SITUAC", SqlDbType.VarChar, 8000, "SITUAC"));

                                dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                                dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                                dbCommand.Parameters["@PREFIXO"].Value = "L010117";
                                dbCommand.Parameters["@IDINF"].Value = "1";
                                dbCommand.Parameters["@BCFIC"].Value = "1";
                                dbCommand.Parameters["@TPINF"].Value = "17";

                                JsonSerasaRELATOAPIClass.ShareCapital shareCapital = objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.shareCapital;

                                dbCommand.Parameters["@CPF"].Value = administrator?.document ?? "";
                                dbCommand.Parameters["@SQCPF"].Value = administrator?.documentSequence ?? "";
                                dbCommand.Parameters["@DGCPF"].Value = administrator?.documentDigit ?? "";
                                dbCommand.Parameters["@DTATU"].Value = shareCapital?.updateDate?.Replace("-", "") ?? "";
                                dbCommand.Parameters["@NMPF"].Value = administrator?.name ?? "";
                                dbCommand.Parameters["@NRRGGL"].Value = "";
                                dbCommand.Parameters["@DTNS"].Value = administrator?.birthDate?.Replace("-", "") ?? "";
                                dbCommand.Parameters["@VINCULO"].Value = administrator?.relationship ?? "";
                                dbCommand.Parameters["@CDEBNSHG"].Value = "";
                                dbCommand.Parameters["@UFNS"].Value = "";

                                dbCommand.Parameters["@DDD"].Value = administrator?.phone?.areaCode ?? "";
                                dbCommand.Parameters["@FONE"].Value = administrator?.phone?.phoneNumber ?? "";
                                dbCommand.Parameters["@RAMAL"].Value = "";
                                dbCommand.Parameters["@NMLG"].Value = administrator?.address?.addressLine ?? "";
                                dbCommand.Parameters["@DSBR"].Value = administrator?.address?.district ?? "";
                                dbCommand.Parameters["@CDEBHG"].Value = administrator?.address?.city ?? "";
                                dbCommand.Parameters["@CDUF"].Value = administrator?.address?.state ?? "";
                                dbCommand.Parameters["@CDCE"].Value = administrator?.address?.zipCode ?? "";
                                dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                                dbCommand.Parameters["@SITUAC"].Value = "C";

                                using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                                {
                                    outputTable.Load(dataReader);
                                }

                                foreach (DataRow row in outputTable.Rows)
                                {
                                    erro = row["Erro"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaConcentreResumo_Adm()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.administrators != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Administrator administrator
                        in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.administrators)
                    {
                        //string TOTALRES = "";

                        //{
                        //    decimal somaTOTALRES = 0;

                        //    foreach (JsonSerasaRELATOAPIClass.Debt debt in administrator.debts)
                        //    {
                        //        somaTOTALRES += Convert.ToDecimal(debt?.summary?.balance.Replace(".", ","));
                        //    }

                        //    TOTALRES = somaTOTALRES.ToString().Replace(",", ".");
                        //}

                        foreach (JsonSerasaRELATOAPIClass.Debt debt in administrator.debts)
                        {
                            using (SqlConnection dbConnection = new SqlConnection(strConec))
                            {
                                //Abre Conexao
                                dbConnection.Open();

                                SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                                dbCommand.CommandType = CommandType.StoredProcedure;

                                dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                                dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                                dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                                dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                                dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                                dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                                dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                                dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                                dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                                dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                                dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                                dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                                dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                                dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                                dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                                dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                                dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                                dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                                dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                                dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                                dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                                dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                                dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                                dbCommand.Parameters["@IDINF"].Value = "4";
                                dbCommand.Parameters["@BCFIC"].Value = "2";
                                dbCommand.Parameters["@TPINF"].Value = "2";

                                if ((debt?.summary?.count?.ToString() ?? "0") == "0") continue;

                                if ((debt?.summary?.firstOccurrence ?? "") == "" && (debt?.summary?.lastOccurrence ?? "") == "") continue;

                                dbCommand.Parameters["@QTDERES"].Value = debt?.summary?.count?.ToString() ?? "";
                                dbCommand.Parameters["@DISC"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA(debt?.debtType, "DISC");
                                dbCommand.Parameters["@NATUREZA"].Value = RetornaAnaliseSerasaConcentreResumo_DISC_NATUREZA(debt?.debtType, "NATUREZA");
                                dbCommand.Parameters["@MESIDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.firstOccurrence ?? "", "monthAbbreviation");
                                dbCommand.Parameters["@MESI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.firstOccurrence ?? "", "MM");
                                dbCommand.Parameters["@ANOI"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.firstOccurrence ?? "", "yyyy");
                                dbCommand.Parameters["@MESFDES"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.lastOccurrence ?? "", "monthAbbreviation");
                                dbCommand.Parameters["@MESF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.lastOccurrence ?? "", "MM");
                                dbCommand.Parameters["@ANOF"].Value = RetornaAnaliseSerasaConcentreResumo_DataFormatacao(debt?.summary?.lastOccurrence ?? "", "yyyy");
                                dbCommand.Parameters["@MOED"].Value = "R$";
                                dbCommand.Parameters["@VALO"].Value = debt?.summary?.balance ?? "";
                                dbCommand.Parameters["@ORIG"].Value = "";
                                dbCommand.Parameters["@AGPR"].Value = "";
                                dbCommand.Parameters["@TOTALRES"].Value = debt?.summary?.balance ?? "";
                                dbCommand.Parameters["@CNPJCPF"].Value = administrator?.document ?? "";

                                using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                                {
                                    outputTable.Load(dataReader);
                                }

                                foreach (DataRow row in outputTable.Rows)
                                {
                                    erro = row["Erro"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaHistPagQtdTit()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.paymentHistory?.titlesQuantity != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.TitlesQuantity titlesQuantity
                        in objJsonSerasaRELATOAPIClass.optionalFeatures.advancedCommercialPaymentHistory.paymentHistory.titlesQuantity)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAG_QTDTIT", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@DESHIS", SqlDbType.VarChar, 8000, "DESHIS"));
                            dbCommand.Parameters.Add(new SqlParameter("@TOTCODHIS", SqlDbType.VarChar, 8000, "TOTCODHIS"));
                            dbCommand.Parameters.Add(new SqlParameter("@TOTDESCRHIS", SqlDbType.VarChar, 8000, "TOTDESCRHIS"));
                            dbCommand.Parameters.Add(new SqlParameter("@TOTQTDHISDE", SqlDbType.VarChar, 8000, "TOTQTDHISDE"));
                            dbCommand.Parameters.Add(new SqlParameter("@TOTQTDHISATE", SqlDbType.VarChar, 8000, "TOTQTDHISATE"));
                            dbCommand.Parameters.Add(new SqlParameter("@PERCHISDE", SqlDbType.VarChar, 8000, "PERCHISDE"));
                            dbCommand.Parameters.Add(new SqlParameter("@PERCHISATE", SqlDbType.VarChar, 8000, "PERCHISATE"));
                            dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L021108";
                            dbCommand.Parameters["@IDINF"].Value = "2";
                            dbCommand.Parameters["@BCFIC"].Value = "11";
                            dbCommand.Parameters["@TPINF"].Value = "8";

                            dbCommand.Parameters["@DESHIS"].Value = titlesQuantity?.name ?? "";
                            dbCommand.Parameters["@TOTCODHIS"].Value = titlesQuantity?.rangeCode ?? "";
                            dbCommand.Parameters["@TOTDESCRHIS"].Value = titlesQuantity?.range ?? "";
                            dbCommand.Parameters["@TOTQTDHISDE"].Value = titlesQuantity?.rangeValueFrom ?? "";
                            dbCommand.Parameters["@TOTQTDHISATE"].Value = titlesQuantity?.rangeValueTo ?? "";
                            dbCommand.Parameters["@PERCHISDE"].Value = titlesQuantity?.percentageFrom ?? "";
                            dbCommand.Parameters["@PERCHISATE"].Value = titlesQuantity?.percentageTo ?? "";
                            dbCommand.Parameters["@SEGINFO"].Value = "202";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = report?.identificationReport?.documentNumber ?? "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaHistPagamentos_CargaPontual()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXA", SqlDbType.VarChar, 8000, "TOTCODFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXA", SqlDbType.VarChar, 8000, "TOTDESCFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADE", SqlDbType.VarChar, 8000, "TOTVLRFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATE", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODMEDFAIXA", SqlDbType.VarChar, 8000, "TOTCODMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCMEDFAIXA", SqlDbType.VarChar, 8000, "TOTDESCMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXADE", SqlDbType.VarChar, 8000, "TOTMEDFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXAATE", SqlDbType.VarChar, 8000, "TOTMEDFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXADE", SqlDbType.VarChar, 8000, "TOTPERCFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXAATE", SqlDbType.VarChar, 8000, "TOTPERCFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTAVISTA", SqlDbType.VarChar, 8000, "TOTAVISTA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L023405";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "34";
                    dbCommand.Parameters["@TPINF"].Value = "5";

                    JsonSerasaRELATOAPIClass.Period punctual =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.paymentHistory?.monthDetail?.summary?.punctual;

                    if (punctual == null) return "";

                    dbCommand.Parameters["@DESCRICAO"].Value = punctual?.periodDescription ?? "";
                    dbCommand.Parameters["@TOTCODFAIXA"].Value = punctual?.totalValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXA"].Value = punctual?.totalValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADE"].Value = punctual?.totalValueFrom ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXAATE"].Value = punctual?.totalValueTo ?? "";
                    dbCommand.Parameters["@TOTCODMEDFAIXA"].Value = punctual?.averageValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCMEDFAIXA"].Value = punctual?.averageValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXADE"].Value = punctual?.historicalAverageRangeFrom ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXAATE"].Value = punctual?.historicalAverageRangeTo ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXADE"].Value = punctual?.percentageValueFrom ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXAATE"].Value = punctual?.percentageValueTo ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                    dbCommand.Parameters["@SEGINFO"].Value = "";
                    dbCommand.Parameters["@SUBGRUPO"].Value = "";
                    dbCommand.Parameters["@TOTAVISTA"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaHistPagamentos_Carga_8_15()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXA", SqlDbType.VarChar, 8000, "TOTCODFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXA", SqlDbType.VarChar, 8000, "TOTDESCFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADE", SqlDbType.VarChar, 8000, "TOTVLRFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATE", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODMEDFAIXA", SqlDbType.VarChar, 8000, "TOTCODMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCMEDFAIXA", SqlDbType.VarChar, 8000, "TOTDESCMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXADE", SqlDbType.VarChar, 8000, "TOTMEDFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXAATE", SqlDbType.VarChar, 8000, "TOTMEDFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXADE", SqlDbType.VarChar, 8000, "TOTPERCFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXAATE", SqlDbType.VarChar, 8000, "TOTPERCFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTAVISTA", SqlDbType.VarChar, 8000, "TOTAVISTA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L023405";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "34";
                    dbCommand.Parameters["@TPINF"].Value = "5";

                    JsonSerasaRELATOAPIClass.Period period8To15 =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.paymentHistory?.monthDetail?.summary?.period8To15;

                    if (period8To15 == null) return "";

                    dbCommand.Parameters["@DESCRICAO"].Value = period8To15?.periodDescription ?? "";
                    dbCommand.Parameters["@TOTCODFAIXA"].Value = period8To15?.totalValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXA"].Value = period8To15?.totalValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADE"].Value = period8To15?.totalValueFrom ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXAATE"].Value = period8To15?.totalValueTo ?? "";
                    dbCommand.Parameters["@TOTCODMEDFAIXA"].Value = period8To15?.averageValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCMEDFAIXA"].Value = period8To15?.averageValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXADE"].Value = period8To15?.historicalAverageRangeFrom ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXAATE"].Value = period8To15?.historicalAverageRangeTo ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXADE"].Value = period8To15?.percentageValueFrom ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXAATE"].Value = period8To15?.percentageValueTo ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                    dbCommand.Parameters["@SEGINFO"].Value = "";
                    dbCommand.Parameters["@SUBGRUPO"].Value = "";
                    dbCommand.Parameters["@TOTAVISTA"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaHistPagamentos_Carga_16_30()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXA", SqlDbType.VarChar, 8000, "TOTCODFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXA", SqlDbType.VarChar, 8000, "TOTDESCFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADE", SqlDbType.VarChar, 8000, "TOTVLRFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATE", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODMEDFAIXA", SqlDbType.VarChar, 8000, "TOTCODMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCMEDFAIXA", SqlDbType.VarChar, 8000, "TOTDESCMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXADE", SqlDbType.VarChar, 8000, "TOTMEDFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXAATE", SqlDbType.VarChar, 8000, "TOTMEDFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXADE", SqlDbType.VarChar, 8000, "TOTPERCFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXAATE", SqlDbType.VarChar, 8000, "TOTPERCFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTAVISTA", SqlDbType.VarChar, 8000, "TOTAVISTA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L023405";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "34";
                    dbCommand.Parameters["@TPINF"].Value = "5";

                    JsonSerasaRELATOAPIClass.Period period16To30 =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.paymentHistory?.monthDetail?.summary?.period16To30;

                    if (period16To30 == null) return "";

                    dbCommand.Parameters["@DESCRICAO"].Value = period16To30?.periodDescription ?? "";
                    dbCommand.Parameters["@TOTCODFAIXA"].Value = period16To30?.totalValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXA"].Value = period16To30?.totalValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADE"].Value = period16To30?.totalValueFrom ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXAATE"].Value = period16To30?.totalValueTo ?? "";
                    dbCommand.Parameters["@TOTCODMEDFAIXA"].Value = period16To30?.averageValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCMEDFAIXA"].Value = period16To30?.averageValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXADE"].Value = period16To30?.historicalAverageRangeFrom ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXAATE"].Value = period16To30?.historicalAverageRangeTo ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXADE"].Value = period16To30?.percentageValueFrom ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXAATE"].Value = period16To30?.percentageValueTo ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                    dbCommand.Parameters["@SEGINFO"].Value = "";
                    dbCommand.Parameters["@SUBGRUPO"].Value = "";
                    dbCommand.Parameters["@TOTAVISTA"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaHistPagamentos_Carga_31_60()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXA", SqlDbType.VarChar, 8000, "TOTCODFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXA", SqlDbType.VarChar, 8000, "TOTDESCFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADE", SqlDbType.VarChar, 8000, "TOTVLRFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATE", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODMEDFAIXA", SqlDbType.VarChar, 8000, "TOTCODMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCMEDFAIXA", SqlDbType.VarChar, 8000, "TOTDESCMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXADE", SqlDbType.VarChar, 8000, "TOTMEDFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXAATE", SqlDbType.VarChar, 8000, "TOTMEDFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXADE", SqlDbType.VarChar, 8000, "TOTPERCFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXAATE", SqlDbType.VarChar, 8000, "TOTPERCFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTAVISTA", SqlDbType.VarChar, 8000, "TOTAVISTA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L023405";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "34";
                    dbCommand.Parameters["@TPINF"].Value = "5";

                    JsonSerasaRELATOAPIClass.Period period31To60 =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.paymentHistory?.monthDetail?.summary?.period31To60;

                    if (period31To60 == null) return "";

                    dbCommand.Parameters["@DESCRICAO"].Value = period31To60?.periodDescription ?? "";
                    dbCommand.Parameters["@TOTCODFAIXA"].Value = period31To60?.totalValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXA"].Value = period31To60?.totalValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADE"].Value = period31To60?.totalValueFrom ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXAATE"].Value = period31To60?.totalValueTo ?? "";
                    dbCommand.Parameters["@TOTCODMEDFAIXA"].Value = period31To60?.averageValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCMEDFAIXA"].Value = period31To60?.averageValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXADE"].Value = period31To60?.historicalAverageRangeFrom ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXAATE"].Value = period31To60?.historicalAverageRangeTo ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXADE"].Value = period31To60?.percentageValueFrom ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXAATE"].Value = period31To60?.percentageValueTo ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                    dbCommand.Parameters["@SEGINFO"].Value = "";
                    dbCommand.Parameters["@SUBGRUPO"].Value = "";
                    dbCommand.Parameters["@TOTAVISTA"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaHistPagamentos_Carga_mais_60()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXA", SqlDbType.VarChar, 8000, "TOTCODFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXA", SqlDbType.VarChar, 8000, "TOTDESCFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADE", SqlDbType.VarChar, 8000, "TOTVLRFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATE", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODMEDFAIXA", SqlDbType.VarChar, 8000, "TOTCODMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCMEDFAIXA", SqlDbType.VarChar, 8000, "TOTDESCMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXADE", SqlDbType.VarChar, 8000, "TOTMEDFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXAATE", SqlDbType.VarChar, 8000, "TOTMEDFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXADE", SqlDbType.VarChar, 8000, "TOTPERCFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXAATE", SqlDbType.VarChar, 8000, "TOTPERCFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTAVISTA", SqlDbType.VarChar, 8000, "TOTAVISTA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L023405";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "34";
                    dbCommand.Parameters["@TPINF"].Value = "5";

                    JsonSerasaRELATOAPIClass.Period periodGT60 =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.paymentHistory?.monthDetail?.summary?.periodGT60;

                    if (periodGT60 == null) return "";

                    dbCommand.Parameters["@DESCRICAO"].Value = periodGT60?.periodDescription ?? "";
                    dbCommand.Parameters["@TOTCODFAIXA"].Value = periodGT60?.totalValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXA"].Value = periodGT60?.totalValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADE"].Value = periodGT60?.totalValueFrom ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXAATE"].Value = periodGT60?.totalValueTo ?? "";
                    dbCommand.Parameters["@TOTCODMEDFAIXA"].Value = periodGT60?.averageValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCMEDFAIXA"].Value = periodGT60?.averageValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXADE"].Value = periodGT60?.historicalAverageRangeFrom ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXAATE"].Value = periodGT60?.historicalAverageRangeTo ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXADE"].Value = periodGT60?.percentageValueFrom ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXAATE"].Value = periodGT60?.percentageValueTo ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                    dbCommand.Parameters["@SEGINFO"].Value = "";
                    dbCommand.Parameters["@SUBGRUPO"].Value = "";
                    dbCommand.Parameters["@TOTAVISTA"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaHistPagamentos_Carga_A_Vista()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXA", SqlDbType.VarChar, 8000, "TOTCODFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXA", SqlDbType.VarChar, 8000, "TOTDESCFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADE", SqlDbType.VarChar, 8000, "TOTVLRFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATE", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODMEDFAIXA", SqlDbType.VarChar, 8000, "TOTCODMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCMEDFAIXA", SqlDbType.VarChar, 8000, "TOTDESCMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXADE", SqlDbType.VarChar, 8000, "TOTMEDFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXAATE", SqlDbType.VarChar, 8000, "TOTMEDFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXADE", SqlDbType.VarChar, 8000, "TOTPERCFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXAATE", SqlDbType.VarChar, 8000, "TOTPERCFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTAVISTA", SqlDbType.VarChar, 8000, "TOTAVISTA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L023405";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "34";
                    dbCommand.Parameters["@TPINF"].Value = "5";

                    JsonSerasaRELATOAPIClass.Period spotPayment =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.paymentHistory?.monthDetail?.summary?.spotPayment;

                    if (spotPayment == null) return "";

                    dbCommand.Parameters["@DESCRICAO"].Value = spotPayment?.periodDescription ?? "";
                    dbCommand.Parameters["@TOTCODFAIXA"].Value = spotPayment?.totalValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXA"].Value = spotPayment?.totalValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADE"].Value = spotPayment?.totalValueFrom ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXAATE"].Value = spotPayment?.totalValueTo ?? "";
                    dbCommand.Parameters["@TOTCODMEDFAIXA"].Value = spotPayment?.averageValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCMEDFAIXA"].Value = spotPayment?.averageValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXADE"].Value = spotPayment?.historicalAverageRangeFrom ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXAATE"].Value = spotPayment?.historicalAverageRangeTo ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXADE"].Value = spotPayment?.percentageValueFrom ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXAATE"].Value = spotPayment?.percentageValueTo ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                    dbCommand.Parameters["@SEGINFO"].Value = "";
                    dbCommand.Parameters["@SUBGRUPO"].Value = "";
                    dbCommand.Parameters["@TOTAVISTA"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaHistPagamentos_Total_Mes()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXA", SqlDbType.VarChar, 8000, "TOTCODFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXA", SqlDbType.VarChar, 8000, "TOTDESCFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADE", SqlDbType.VarChar, 8000, "TOTVLRFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATE", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODMEDFAIXA", SqlDbType.VarChar, 8000, "TOTCODMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCMEDFAIXA", SqlDbType.VarChar, 8000, "TOTDESCMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXADE", SqlDbType.VarChar, 8000, "TOTMEDFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXAATE", SqlDbType.VarChar, 8000, "TOTMEDFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXADE", SqlDbType.VarChar, 8000, "TOTPERCFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXAATE", SqlDbType.VarChar, 8000, "TOTPERCFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTAVISTA", SqlDbType.VarChar, 8000, "TOTAVISTA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L023405";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "34";
                    dbCommand.Parameters["@TPINF"].Value = "5";

                    JsonSerasaRELATOAPIClass.Total total =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.paymentHistory?.monthDetail?.summary?.total;

                    if (total == null) return "";

                    dbCommand.Parameters["@DESCRICAO"].Value = total?.periodDescription ?? "";
                    dbCommand.Parameters["@TOTCODFAIXA"].Value = total?.totalValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXA"].Value = total?.totalValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADE"].Value = total?.totalValueFrom ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXAATE"].Value = total?.totalValueTo ?? "";
                    dbCommand.Parameters["@TOTCODMEDFAIXA"].Value = total?.averageValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCMEDFAIXA"].Value = total?.averageValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXADE"].Value = total?.historicalAverageRangeFrom ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXAATE"].Value = total?.historicalAverageRangeTo ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXADE"].Value = total?.percentageValueFrom ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXAATE"].Value = total?.percentageValueTo ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = "";
                    dbCommand.Parameters["@SEGINFO"].Value = "";
                    dbCommand.Parameters["@SUBGRUPO"].Value = "";
                    dbCommand.Parameters["@TOTAVISTA"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaReferenciaisNegocios()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.businessReferences?.businessReferencesList != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.BusinessReferencesList businessReferencesList
                        in objJsonSerasaRELATOAPIClass.optionalFeatures.advancedCommercialPaymentHistory.businessReferences.businessReferencesList)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_REFERENCIAIS_NEGOCIOS", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@POTENC", SqlDbType.VarChar, 8000, "POTENC"));
                            dbCommand.Parameters.Add(new SqlParameter("@AAAAMM", SqlDbType.VarChar, 8000, "AAAAMM"));
                            dbCommand.Parameters.Add(new SqlParameter("@CODFAIXAPOT", SqlDbType.VarChar, 8000, "CODFAIXAPOT"));
                            dbCommand.Parameters.Add(new SqlParameter("@DESCRFAIXAPOT", SqlDbType.VarChar, 8000, "DESCRFAIXAPOT"));
                            dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXADEPOT", SqlDbType.VarChar, 8000, "VLRFAIXADEPOT"));
                            dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXAATEPOT", SqlDbType.VarChar, 8000, "VLRFAIXAATEPOT"));
                            dbCommand.Parameters.Add(new SqlParameter("@CODFAIXAMED", SqlDbType.VarChar, 8000, "CODFAIXAMED"));
                            dbCommand.Parameters.Add(new SqlParameter("@DESCRFAIXAMED", SqlDbType.VarChar, 8000, "DESCRFAIXAMED"));
                            dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXADEMED", SqlDbType.VarChar, 8000, "VLRFAIXADEMED"));
                            dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXAATEMED", SqlDbType.VarChar, 8000, "VLRFAIXAATEMED"));
                            dbCommand.Parameters.Add(new SqlParameter("@SEG0INFO", SqlDbType.VarChar, 8000, "SEG0INFO"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADO-SERASA"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L021107";
                            dbCommand.Parameters["@IDINF"].Value = "2";
                            dbCommand.Parameters["@BCFIC"].Value = "11";
                            dbCommand.Parameters["@TPINF"].Value = "7";

                            dbCommand.Parameters["@POTENC"].Value = businessReferencesList?.businessDescription ?? "";

                            string AAAAMM = businessReferencesList?.yearPotentialDate ?? "";

                            string monthPotentialDate = businessReferencesList?.monthPotentialDate?.Replace("-", "") ?? "0";

                            if (Convert.ToInt32(monthPotentialDate) < 10)
                                AAAAMM += "0" + monthPotentialDate;
                            else
                                AAAAMM += monthPotentialDate;

                            dbCommand.Parameters["@AAAAMM"].Value = AAAAMM;
                            dbCommand.Parameters["@CODFAIXAPOT"].Value = businessReferencesList?.potentialValueRangeCode ?? "";
                            dbCommand.Parameters["@DESCRFAIXAPOT"].Value = businessReferencesList?.potentialValueRangeDescription ?? "";
                            dbCommand.Parameters["@VLRFAIXADEPOT"].Value = businessReferencesList?.potentialValueFrom ?? "";
                            dbCommand.Parameters["@VLRFAIXAATEPOT"].Value = businessReferencesList?.potentialValueTo ?? "";
                            dbCommand.Parameters["@CODFAIXAMED"].Value = businessReferencesList?.potentialMidrangeCode ?? "";
                            dbCommand.Parameters["@DESCRFAIXAMED"].Value = businessReferencesList?.potentialMidrangeDescription ?? "";
                            dbCommand.Parameters["@VLRFAIXADEMED"].Value = businessReferencesList?.potentialMidrangeValueFrom ?? "";
                            dbCommand.Parameters["@VLRFAIXAATEMED"].Value = businessReferencesList?.potentialMidrangeValueTo ?? "";
                            dbCommand.Parameters["@SEG0INFO"].Value = "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaRelFornecedorPeriodo()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.relationshipSuppliersPeriods?.relationshipSuppliersPeriodList != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.RelationshipSuppliersPeriodList relationshipSuppliersPeriod
                        in objJsonSerasaRELATOAPIClass.optionalFeatures.advancedCommercialPaymentHistory.relationshipSuppliersPeriods.relationshipSuppliersPeriodList)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_REL_FORNECEDOR_PERIODO", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@DESREL", SqlDbType.VarChar, 8000, "DESREL"));
                            dbCommand.Parameters.Add(new SqlParameter("@QTDREL", SqlDbType.VarChar, 8000, "QTDREL"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L020103";
                            dbCommand.Parameters["@IDINF"].Value = "2";
                            dbCommand.Parameters["@BCFIC"].Value = "1";
                            dbCommand.Parameters["@TPINF"].Value = "3";

                            dbCommand.Parameters["@DESREL"].Value = relationshipSuppliersPeriod?.relationshipPeriodDescription ?? "";
                            dbCommand.Parameters["@QTDREL"].Value = relationshipSuppliersPeriod?.relationshipSourceQuantity ?? "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaRelacionamentoFornecedor()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_RELACIONAMENTO_FORNECEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULT", SqlDbType.VarChar, 8000, "FT-CONSULT"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULTPERF", SqlDbType.VarChar, 8000, "FT-CONSULT-PERF"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULTEVOL", SqlDbType.VarChar, 8000, "FT-CONSULT-EVOL"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULPOTN", SqlDbType.VarChar, 8000, "FT-CONSUL-POTN"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULPOTV", SqlDbType.VarChar, 8000, "FT-CONSUL-POTV"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULTHIST", SqlDbType.VarChar, 8000, "FT-CONSULT-HIST"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO2", SqlDbType.VarChar, 8000, "RESERVADO2"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L020103";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "1";
                    dbCommand.Parameters["@TPINF"].Value = "3";

                    JsonSerasaRELATOAPIClass.Summary summary =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.relationshipSuppliersPeriods?.summary;

                    if (summary == null) return "";

                    dbCommand.Parameters["@FTCONSULT"].Value = summary?.sourcesTotal ?? "";
                    dbCommand.Parameters["@FTCONSULTPERF"].Value = summary?.paymentHistoryValuesSources ?? "";
                    dbCommand.Parameters["@FTCONSULTEVOL"].Value = summary?.evolutionCommitmentsSources ?? "";
                    dbCommand.Parameters["@FTCONSULPOTN"].Value = summary?.businessReferencesSources ?? "";
                    dbCommand.Parameters["@FTCONSULPOTV"].Value = summary?.spotPaymentBusinessReferencesSources ?? "";
                    dbCommand.Parameters["@RESERVADO"].Value = "";
                    dbCommand.Parameters["@FTCONSULTHIST"].Value = summary?.paymentHistorySources ?? "";
                    dbCommand.Parameters["@RESERVADO2"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaAnotSocAdm_partners()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.partners != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Partner partner in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.partners)
                    {
                        if (partner.relationship == "S" && partner.relationship == "D")
                        {

                            string CountTotal = "", BalanceTotal = "";

                            {
                                int somaCount = 0;

                                decimal somaBalance = 0;

                                foreach (JsonSerasaRELATOAPIClass.Debt debt in partner.debts)
                                {
                                    somaCount += Convert.ToInt32(debt?.summary?.count);

                                    somaBalance += Convert.ToDecimal(debt?.summary?.balance.Replace(".", ","));
                                }

                                CountTotal = somaCount.ToString();

                                BalanceTotal = somaBalance.ToString().Replace(",", ".");
                            }

                            foreach (JsonSerasaRELATOAPIClass.Debt debt in partner.debts)
                            {
                                using (SqlConnection dbConnection = new SqlConnection(strConec))
                                {
                                    //Abre Conexao
                                    dbConnection.Open();

                                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ANOT_SOC_ADM", dbConnection);

                                    dbCommand.CommandType = CommandType.StoredProcedure;

                                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                                    dbCommand.Parameters.Add(new SqlParameter("@SEQ", SqlDbType.VarChar, 8000, "SEQ"));
                                    dbCommand.Parameters.Add(new SqlParameter("@PESS", SqlDbType.VarChar, 8000, "PESS"));
                                    dbCommand.Parameters.Add(new SqlParameter("@DOC", SqlDbType.VarChar, 8000, "DOC"));
                                    dbCommand.Parameters.Add(new SqlParameter("@FIL", SqlDbType.VarChar, 8000, "FIL"));
                                    dbCommand.Parameters.Add(new SqlParameter("@DIG", SqlDbType.VarChar, 8000, "DIG"));
                                    dbCommand.Parameters.Add(new SqlParameter("@SEQSOC", SqlDbType.VarChar, 8000, "SEQ-SOC"));
                                    dbCommand.Parameters.Add(new SqlParameter("@VINC", SqlDbType.VarChar, 8000, "VINC"));
                                    dbCommand.Parameters.Add(new SqlParameter("@NOME", SqlDbType.VarChar, 8000, "NOME"));
                                    dbCommand.Parameters.Add(new SqlParameter("@QTANOT", SqlDbType.VarChar, 8000, "QTANOT"));
                                    dbCommand.Parameters.Add(new SqlParameter("@VRTOT", SqlDbType.VarChar, 8000, "VRTOT"));
                                    dbCommand.Parameters.Add(new SqlParameter("@DTRECE", SqlDbType.VarChar, 8000, "DTRECE"));
                                    dbCommand.Parameters.Add(new SqlParameter("@SITUAC", SqlDbType.VarChar, 8000, "SITUAC"));

                                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                                    dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                                    dbCommand.Parameters["@IDINF"].Value = "4";
                                    dbCommand.Parameters["@BCFIC"].Value = "2";
                                    dbCommand.Parameters["@TPINF"].Value = "2";

                                    dbCommand.Parameters["@SEQ"].Value = "";
                                    dbCommand.Parameters["@PESS"].Value = partner?.kindPerson ?? "";
                                    dbCommand.Parameters["@DOC"].Value = partner?.document ?? "";
                                    dbCommand.Parameters["@FIL"].Value = "";
                                    dbCommand.Parameters["@DIG"].Value = partner?.documentDigit ?? "";
                                    dbCommand.Parameters["@SEQSOC"].Value = "";
                                    dbCommand.Parameters["@VINC"].Value = partner?.relationship ?? "";
                                    dbCommand.Parameters["@NOME"].Value = partner?.name ?? "";
                                    dbCommand.Parameters["@QTANOT"].Value = CountTotal ?? "";
                                    dbCommand.Parameters["@VRTOT"].Value = BalanceTotal ?? "";
                                    dbCommand.Parameters["@DTRECE"].Value = "";
                                    dbCommand.Parameters["@SITUAC"].Value = "C";

                                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                                    {
                                        outputTable.Load(dataReader);
                                    }

                                    foreach (DataRow row in outputTable.Rows)
                                    {
                                        erro = row["Erro"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaAnotSocAdm_administrators()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.qsaCompleteReport?.administrators != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.Administrator administrator in objJsonSerasaRELATOAPIClass.optionalFeatures.qsaCompleteReport.administrators)
                    {
                        if (administrator.relationship == "A" && administrator.relationship == "D")
                        {
                            string CountTotal = "", BalanceTotal = "";

                            {
                                int somaCount = 0;

                                decimal somaBalance = 0;

                                foreach (JsonSerasaRELATOAPIClass.Debt debt in administrator.debts)
                                {
                                    somaCount += Convert.ToInt32(debt?.summary?.count);

                                    somaBalance += Convert.ToDecimal(debt?.summary?.balance.Replace(".", ","));
                                }

                                CountTotal = somaCount.ToString();

                                BalanceTotal = somaBalance.ToString().Replace(",", ".");
                            }

                            foreach (JsonSerasaRELATOAPIClass.Debt debt in administrator.debts)
                            {
                                using (SqlConnection dbConnection = new SqlConnection(strConec))
                                {
                                    //Abre Conexao
                                    dbConnection.Open();

                                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ANOT_SOC_ADM", dbConnection);

                                    dbCommand.CommandType = CommandType.StoredProcedure;

                                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                                    dbCommand.Parameters.Add(new SqlParameter("@SEQ", SqlDbType.VarChar, 8000, "SEQ"));
                                    dbCommand.Parameters.Add(new SqlParameter("@PESS", SqlDbType.VarChar, 8000, "PESS"));
                                    dbCommand.Parameters.Add(new SqlParameter("@DOC", SqlDbType.VarChar, 8000, "DOC"));
                                    dbCommand.Parameters.Add(new SqlParameter("@FIL", SqlDbType.VarChar, 8000, "FIL"));
                                    dbCommand.Parameters.Add(new SqlParameter("@DIG", SqlDbType.VarChar, 8000, "DIG"));
                                    dbCommand.Parameters.Add(new SqlParameter("@SEQSOC", SqlDbType.VarChar, 8000, "SEQ-SOC"));
                                    dbCommand.Parameters.Add(new SqlParameter("@VINC", SqlDbType.VarChar, 8000, "VINC"));
                                    dbCommand.Parameters.Add(new SqlParameter("@NOME", SqlDbType.VarChar, 8000, "NOME"));
                                    dbCommand.Parameters.Add(new SqlParameter("@QTANOT", SqlDbType.VarChar, 8000, "QTANOT"));
                                    dbCommand.Parameters.Add(new SqlParameter("@VRTOT", SqlDbType.VarChar, 8000, "VRTOT"));
                                    dbCommand.Parameters.Add(new SqlParameter("@DTRECE", SqlDbType.VarChar, 8000, "DTRECE"));
                                    dbCommand.Parameters.Add(new SqlParameter("@SITUAC", SqlDbType.VarChar, 8000, "SITUAC"));

                                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                                    dbCommand.Parameters["@PREFIXO"].Value = "L040202";
                                    dbCommand.Parameters["@IDINF"].Value = "4";
                                    dbCommand.Parameters["@BCFIC"].Value = "2";
                                    dbCommand.Parameters["@TPINF"].Value = "2";

                                    dbCommand.Parameters["@SEQ"].Value = "";
                                    dbCommand.Parameters["@PESS"].Value = administrator?.kindPerson ?? "";
                                    dbCommand.Parameters["@DOC"].Value = administrator?.document ?? "";
                                    dbCommand.Parameters["@FIL"].Value = "";
                                    dbCommand.Parameters["@DIG"].Value = administrator?.documentDigit ?? "";
                                    dbCommand.Parameters["@SEQSOC"].Value = "";
                                    dbCommand.Parameters["@VINC"].Value = administrator?.relationship ?? "";
                                    dbCommand.Parameters["@NOME"].Value = administrator?.name ?? "";
                                    dbCommand.Parameters["@QTANOT"].Value = CountTotal ?? "";
                                    dbCommand.Parameters["@VRTOT"].Value = BalanceTotal ?? "";
                                    dbCommand.Parameters["@DTRECE"].Value = "";
                                    dbCommand.Parameters["@SITUAC"].Value = "C";

                                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                                    {
                                        outputTable.Load(dataReader);
                                    }

                                    foreach (DataRow row in outputTable.Rows)
                                    {
                                        erro = row["Erro"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaEvolCompromisso()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_EVOL_COMPROMISSO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXAVV", SqlDbType.VarChar, 8000, "TOTCODFAIXAVV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXAVV", SqlDbType.VarChar, 8000, "TOTDESCFAIXAVV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADEVV", SqlDbType.VarChar, 8000, "TOTVLRFAIXADEVV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATEVV", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATEVV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXAAV", SqlDbType.VarChar, 8000, "TOTCODFAIXAAV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXAAV", SqlDbType.VarChar, 8000, "TOTDESCFAIXAAV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADEAV", SqlDbType.VarChar, 8000, "TOTVLRFAIXADEAV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATEAV", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATEAV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFXATM", SqlDbType.VarChar, 8000, "TOTCODFXATM"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESFXATM", SqlDbType.VarChar, 8000, "TOTDESFXATM"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFXADETM", SqlDbType.VarChar, 8000, "TOTVLRFXADETM"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFXAATETM", SqlDbType.VarChar, 8000, "TOTVLRFXAATETM"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L023406";
                    dbCommand.Parameters["@IDINF"].Value = "2";
                    dbCommand.Parameters["@BCFIC"].Value = "34";
                    dbCommand.Parameters["@TPINF"].Value = "6";

                    JsonSerasaRELATOAPIClass.Total total =
                        objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.evolutionCommitmentsSuppliers?.summary?.total;

                    if (total == null) return "";

                    dbCommand.Parameters["@DESCRICAO"].Value = total?.periodDescription ?? "";
                    dbCommand.Parameters["@TOTCODFAIXAVV"].Value = total?.overdueTotalRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXAVV"].Value = total?.totalValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADEVV"].Value = total?.overdueTotalFrom ?? "0";
                    dbCommand.Parameters["@TOTVLRFAIXAATEVV"].Value = total?.overdueTotalTo ?? "0";
                    dbCommand.Parameters["@TOTCODFAIXAAV"].Value = total?.upcomingValueRangeCode ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXAAV"].Value = total?.upcomingValueRangeDescription ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADEAV"].Value = total?.upcomingValueFrom ?? "0";
                    dbCommand.Parameters["@TOTVLRFAIXAATEAV"].Value = total?.upcomingValueTo ?? "0";
                    dbCommand.Parameters["@TOTCODFXATM"].Value = "";
                    dbCommand.Parameters["@TOTDESFXATM"].Value = "";
                    dbCommand.Parameters["@TOTVLRFXADETM"].Value = "0";
                    dbCommand.Parameters["@TOTVLRFXAATETM"].Value = "0";
                    dbCommand.Parameters["@SEGINFO"].Value = "0";
                    dbCommand.Parameters["@SUBGRUPO"].Value = "0";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaEvolCompromissoFor()
        {
            erro = "";

            try
            {
                if (objJsonSerasaRELATOAPIClass?.optionalFeatures?.advancedCommercialPaymentHistory?.evolutionCommitmentsSuppliers?.evolutionCommitmentsSuppliersList != null)
                {
                    foreach (JsonSerasaRELATOAPIClass.EvolutionCommitmentsSuppliersList evolutionCommitmentsSuppliersList
                        in objJsonSerasaRELATOAPIClass.optionalFeatures.advancedCommercialPaymentHistory.evolutionCommitmentsSuppliers.evolutionCommitmentsSuppliersList)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_EVOL_COMPROMISSO_FOR", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                            dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                            dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                            dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                            dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                            dbCommand.Parameters.Add(new SqlParameter("@ANOEVO", SqlDbType.VarChar, 8000, "ANO-EVO"));
                            dbCommand.Parameters.Add(new SqlParameter("@MESEVO", SqlDbType.VarChar, 8000, "MES-EVO"));
                            dbCommand.Parameters.Add(new SqlParameter("@MESDESE", SqlDbType.VarChar, 8000, "MES-DESE"));
                            dbCommand.Parameters.Add(new SqlParameter("@CODFAIXAVENC", SqlDbType.VarChar, 8000, "COD-FAIXA-VENC"));
                            dbCommand.Parameters.Add(new SqlParameter("@DESCRFAIXAVENC", SqlDbType.VarChar, 8000, "DESCR-FAIXA-VENC"));
                            dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXADEVENC", SqlDbType.VarChar, 8000, "VLR-FAIXA-DE-VENC"));
                            dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXAATEVENC", SqlDbType.VarChar, 8000, "VLR-FAIXA-ATE-VENC"));
                            dbCommand.Parameters.Add(new SqlParameter("@CODFAIXAAVEN", SqlDbType.VarChar, 8000, "COD-FAIXA-AVEN"));
                            dbCommand.Parameters.Add(new SqlParameter("@DESCRFAIXAAVEN", SqlDbType.VarChar, 8000, "DESCR-FAIXA-AVEN"));
                            dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXADEAVEN", SqlDbType.VarChar, 8000, "VLR-FAIXA-DE-AVEN"));
                            dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXAATEAVEN", SqlDbType.VarChar, 8000, "VLR-FAIXA-ATE-AVEN"));
                            dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADO-SERASA"));

                            dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                            dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                            dbCommand.Parameters["@PREFIXO"].Value = "L021106";
                            dbCommand.Parameters["@IDINF"].Value = "2";
                            dbCommand.Parameters["@BCFIC"].Value = "11";
                            dbCommand.Parameters["@TPINF"].Value = "6";

                            dbCommand.Parameters["@ANOEVO"].Value = evolutionCommitmentsSuppliersList?.yearCommitment ?? "";
                            dbCommand.Parameters["@MESEVO"].Value = evolutionCommitmentsSuppliersList?.monthCommitment ?? "";
                            dbCommand.Parameters["@MESDESE"].Value = evolutionCommitmentsSuppliersList?.descriptionMonthCommitment ?? "";
                            dbCommand.Parameters["@CODFAIXAVENC"].Value = evolutionCommitmentsSuppliersList?.trackCodeToExpire ?? "";
                            dbCommand.Parameters["@DESCRFAIXAVENC"].Value = evolutionCommitmentsSuppliersList?.trackDescriptionToExpire ?? "";
                            dbCommand.Parameters["@VLRFAIXADEVENC"].Value = evolutionCommitmentsSuppliersList?.valueCommitmentsDueFrom ?? "";
                            dbCommand.Parameters["@VLRFAIXAATEVENC"].Value = evolutionCommitmentsSuppliersList?.valueCommitmentsDueTo ?? "";
                            dbCommand.Parameters["@CODFAIXAAVEN"].Value = evolutionCommitmentsSuppliersList?.totalMonthRangeCode ?? "";
                            dbCommand.Parameters["@DESCRFAIXAAVEN"].Value = evolutionCommitmentsSuppliersList?.totalMonthRangeDescription ?? "";
                            dbCommand.Parameters["@VLRFAIXADEAVEN"].Value = evolutionCommitmentsSuppliersList?.totalMonthlyRangeValueFrom ?? "";
                            dbCommand.Parameters["@VLRFAIXAATEAVEN"].Value = evolutionCommitmentsSuppliersList?.totalMonthlyRangeValueTo ?? "";
                            dbCommand.Parameters["@RESERVADOSERASA"].Value = evolutionCommitmentsSuppliersList?.segmentInformation ?? "";

                            using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                            {
                                outputTable.Load(dataReader);
                            }

                            foreach (DataRow row in outputTable.Rows)
                            {
                                erro = row["Erro"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaInscricaoEstadual()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_INSCRICAO_ESTADUAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@INSCRICAOESTADUAL", SqlDbType.VarChar, 8000, "INSCRICAOESTADUAL"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L571001";
                    dbCommand.Parameters["@IDINF"].Value = "57";
                    dbCommand.Parameters["@BCFIC"].Value = "10";
                    dbCommand.Parameters["@TPINF"].Value = "1";

                    dbCommand.Parameters["@INSCRICAOESTADUAL"].Value = report?.identificationReport?.stateRegistration ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaDadosControle()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_DADOS_CONTROLE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@CDSITRF", SqlDbType.VarChar, 8000, "CDSITRF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DSSITRF", SqlDbType.VarChar, 8000, "DSSITRF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDCG", SqlDbType.VarChar, 8000, "CDCG"));
                    dbCommand.Parameters.Add(new SqlParameter("@INDFICHA", SqlDbType.VarChar, 8000, "INDFICHA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONTAB", SqlDbType.VarChar, 8000, "TRNCONTAB"));
                    dbCommand.Parameters.Add(new SqlParameter("@AREARESERVADA", SqlDbType.VarChar, 8000, "AREARESERVADA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT02", SqlDbType.VarChar, 8000, "TRNCONT02"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT03", SqlDbType.VarChar, 8000, "TRNCONT03"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT04", SqlDbType.VarChar, 8000, "TRNCONT04"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT05", SqlDbType.VarChar, 8000, "TRNCONT05"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT06", SqlDbType.VarChar, 8000, "TRNCONT06"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT07", SqlDbType.VarChar, 8000, "TRNCONT07"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT08", SqlDbType.VarChar, 8000, "TRNCONT08"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT09", SqlDbType.VarChar, 8000, "TRNCONT09"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT10", SqlDbType.VarChar, 8000, "TRNCONT10"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPRELATO", SqlDbType.VarChar, 8000, "TIPRELATO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TEMRECIPR", SqlDbType.VarChar, 8000, "TEMRECIPR"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPRELCOB", SqlDbType.VarChar, 8000, "TIPRELCOB"));
                    dbCommand.Parameters.Add(new SqlParameter("@DIASREST", SqlDbType.VarChar, 8000, "DIASREST"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDSITUNOV", SqlDbType.VarChar, 8000, "CDSITUNOV"));
                    dbCommand.Parameters.Add(new SqlParameter("@DSSITUNOV", SqlDbType.VarChar, 8000, "DSSITUNOV"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L010000";
                    dbCommand.Parameters["@IDINF"].Value = "01";
                    dbCommand.Parameters["@BCFIC"].Value = "00";
                    dbCommand.Parameters["@TPINF"].Value = "00";

                    dbCommand.Parameters["@CDSITRF"].Value = report?.identificationReport?.statusCode ?? "";
                    dbCommand.Parameters["@DSSITRF"].Value = "";
                    dbCommand.Parameters["@CDCG"].Value = report?.identificationReport?.documentNumber ?? "";
                    dbCommand.Parameters["@INDFICHA"].Value = "";
                    dbCommand.Parameters["@TRNCONTAB"].Value = "";
                    dbCommand.Parameters["@AREARESERVADA"].Value = "";
                    dbCommand.Parameters["@TRNCONT02"].Value = "";
                    dbCommand.Parameters["@TRNCONT03"].Value = "";
                    dbCommand.Parameters["@TRNCONT04"].Value = "";
                    dbCommand.Parameters["@TRNCONT05"].Value = "";
                    dbCommand.Parameters["@TRNCONT06"].Value = "";
                    dbCommand.Parameters["@TRNCONT07"].Value = "";
                    dbCommand.Parameters["@TRNCONT08"].Value = "";
                    dbCommand.Parameters["@TRNCONT09"].Value = "";
                    dbCommand.Parameters["@TRNCONT10"].Value = "";
                    dbCommand.Parameters["@TIPRELATO"].Value = "2";
                    dbCommand.Parameters["@TEMRECIPR"].Value = "N";
                    dbCommand.Parameters["@TIPRELCOB"].Value = "2";
                    dbCommand.Parameters["@DIASREST"].Value = "0";
                    dbCommand.Parameters["@CDSITUNOV"].Value = report?.identificationReport?.statusCode ?? "0";
                    dbCommand.Parameters["@DSSITUNOV"].Value = report?.identificationReport?.statusRegistration ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string GravaAnaliseSerasaContabilizacao()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONTABILIZACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@CICSUSER", SqlDbType.VarChar, 8000, "CICSUSER"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAEMIS", SqlDbType.VarChar, 8000, "DATA-EMIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@HORAEMIS", SqlDbType.VarChar, 8000, "HORA-EMIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJEDITADO", SqlDbType.VarChar, 8000, "CNPJ-EDITADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAULTATCONT", SqlDbType.VarChar, 8000, "DATA-ULTAT-CONT"));
                    dbCommand.Parameters.Add(new SqlParameter("@ORIGEMDADOS", SqlDbType.VarChar, 8000, "ORIGEM-DADOS"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRUTRG", SqlDbType.VarChar, 8000, "NRUTRG"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTUTRG", SqlDbType.VarChar, 8000, "DTUTRG"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = "L010101";
                    dbCommand.Parameters["@IDINF"].Value = "01";
                    dbCommand.Parameters["@BCFIC"].Value = "01";
                    dbCommand.Parameters["@TPINF"].Value = "01";

                    dbCommand.Parameters["@CICSUSER"].Value = "API";
                    dbCommand.Parameters["@DATAEMIS"].Value = "";
                    dbCommand.Parameters["@HORAEMIS"].Value = "";
                    dbCommand.Parameters["@RESERVADO"].Value = "";
                    dbCommand.Parameters["@CNPJEDITADO"].Value = "CNPJ: " + report?.identificationReport?.documentNumber ?? "";
                    dbCommand.Parameters["@DATAULTATCONT"].Value = report?.identificationReport?.updateDate?.Replace("-", "") ?? "";
                    dbCommand.Parameters["@ORIGEMDADOS"].Value = "";
                    dbCommand.Parameters["@NRUTRG"].Value = report?.identificationReport?.companyRegister ?? "";
                    dbCommand.Parameters["@DTUTRG"].Value = report?.identificationReport?.companyRegisterDate?.Replace("-", "") ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }

        public string ApagaTabelasCasoDeErro()
        {
            erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_ANALISE_SERASA_TABELAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") erro = "Erro " + MethodBase.GetCurrentMethod().Name + ": " + erro;

            return erro;
        }
    }
}