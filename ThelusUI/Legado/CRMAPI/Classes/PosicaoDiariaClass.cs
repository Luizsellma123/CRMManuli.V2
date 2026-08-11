using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRMAPI.Models;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace CRMAPI.Classes
{
    public class PosicaoDiariaClass
    {
        public GeraPosicaoDiariaModel objGeraPosicaoDiariaModel { get; set; }

        private int IDPosicaoDiaria;

        ConexaoClass objComunicacaoCRM = new ConexaoClass();

        ComunicacaoSAPClass objComunicacaoSAP = new ComunicacaoSAPClass();

        UserStoredProcedureManuliFitasaClass objUSPMF;

        VendasWeb.classes.ControladoriaClass objControladoriaClass = new VendasWeb.classes.ControladoriaClass();

        public PosicaoDiariaClass(GeraPosicaoDiariaModel objGeraPosicaoDiariaModel)
        {
            this.objGeraPosicaoDiariaModel = objGeraPosicaoDiariaModel;

            objUSPMF = new UserStoredProcedureManuliFitasaClass
                (objGeraPosicaoDiariaModel.PeriodoInicial, objGeraPosicaoDiariaModel.PeriodoFinal);
        }

        public string CRM_SP_GRAVA_POSICAO_DIARIA()
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_POSICAO_DIARIA", dbConnection);

                    dbCommand.CommandTimeout = 500;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@PeriodoInicial", SqlDbType.Date, 0, "PeriodoInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@PeriodoFinal", SqlDbType.Date, 0, "PeriodoFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 1000, ParameterDirection.Output, false, 0, 0, "IDPosicaoDiaria", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = Convert.ToInt32(this.objGeraPosicaoDiariaModel.IDUsuario);
                    dbCommand.Parameters["@PeriodoInicial"].Value = Convert.ToDateTime(this.objGeraPosicaoDiariaModel.PeriodoInicial);
                    dbCommand.Parameters["@PeriodoFinal"].Value = Convert.ToDateTime(this.objGeraPosicaoDiariaModel.PeriodoFinal);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    IDPosicaoDiaria = (int)dbCommand.Parameters["@IDPosicaoDiaria"].Value;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro " + GetCurrentMethodName() + ".");
            }

            return "";
        }

        public string CRM_SP_GRAVA_POSICAO_DIARIA_FATURADOS()
        {
            string erro = "";

            DataTable USP_MF_DataTable = new DataTable();

            try
            {
                USP_MF_DataTable = objUSPMF.USP_MF_FATURAMENTO_NOTA_FISCAL();

                if (USP_MF_DataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in USP_MF_DataTable.Rows)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                            dbCommand.CommandTimeout = 500;

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@Erro", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "Erro", DataRowVersion.Default, null));

                            #region Declaração

                            dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoEmpresa", SqlDbType.Int, 0, "CodigoEmpresa"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeEmpresa", SqlDbType.VarChar, 8000, "NomeEmpresa"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoGrupoMateiral", SqlDbType.Int, 0, "CodigoGrupoMateiral"));
                            dbCommand.Parameters.Add(new SqlParameter("@GrupoMaterial", SqlDbType.VarChar, 8000, "GrupoMaterial"));
                            dbCommand.Parameters.Add(new SqlParameter("@NotaFiscal", SqlDbType.VarChar, 10, "NotaFiscal"));
                            dbCommand.Parameters.Add(new SqlParameter("@StatusNota", SqlDbType.VarChar, 10, "StatusNota"));
                            dbCommand.Parameters.Add(new SqlParameter("@NumeroPedido", SqlDbType.Int, 0, "NumeroPedido"));
                            dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.VarChar, 8000, "Cidade"));
                            dbCommand.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 4, "Estado"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCliente", SqlDbType.VarChar, 20, "CodigoCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.VarChar, 8000, "NomeCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 20, "CNPJ"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoVendedor", SqlDbType.Int, 0, "CodigoVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeVendedor", SqlDbType.VarChar, 8000, "NomeVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@ClasseVendedor", SqlDbType.Int, 0, "ClasseVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeClasse", SqlDbType.VarChar, 8000, "NomeClasse"));
                            dbCommand.Parameters.Add(new SqlParameter("@CaracTeristicasProduto", SqlDbType.VarChar, 8000, "CaracTeristicasProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@MesFaturamento", SqlDbType.Int, 0, "MesFaturamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@DataEmissao", SqlDbType.DateTime, 0, "DataEmissao"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoProduto", SqlDbType.VarChar, 100, "CodigoProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeProduto", SqlDbType.VarChar, 8000, "NomeProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@UnidadeVenda", SqlDbType.VarChar, 20, "UnidadeVenda"));
                            dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 0, "Quantidade"));
                            dbCommand.Parameters.Add(new SqlParameter("@QuantidadeConvertida", SqlDbType.Decimal, 0, "QuantidadeConvertida"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorUnitario", SqlDbType.Decimal, 0, "ValorUnitario"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorIPI", SqlDbType.Decimal, 0, "ValorIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalSemIPI", SqlDbType.Decimal, 0, "TotalSemIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalComIPI", SqlDbType.Decimal, 0, "TotalComIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalLinha", SqlDbType.Decimal, 0, "TotalLinha"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCFOP", SqlDbType.VarChar, 20, "CodigoCFOP"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCFOP", SqlDbType.VarChar, 8000, "NomeCFOP"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodiogCNAE", SqlDbType.VarChar, 20, "CodiogCNAE"));
                            dbCommand.Parameters.Add(new SqlParameter("@DescricaoCNAE", SqlDbType.VarChar, 8000, "DescricaoCNAE"));
                            dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.VarChar, 8000, "Bairro"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCondicaoPagamento", SqlDbType.Int, 0, "CodigoCondicaoPagamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCondicaoPagamento", SqlDbType.VarChar, 8000, "NomeCondicaoPagamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@ICMSItem", SqlDbType.Decimal, 0, "ICMSItem"));
                            dbCommand.Parameters.Add(new SqlParameter("@PercentualDiferimentoICMS", SqlDbType.Decimal, 0, "PercentualDiferimentoICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorDiferimentoICMS", SqlDbType.Decimal, 0, "ValorDiferimentoICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorReducaoBase", SqlDbType.Decimal, 0, "ValorReducaoBase"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorBaseICMS", SqlDbType.Decimal, 0, "ValorBaseICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@AlicotaBaseICMS", SqlDbType.Decimal, 0, "AlicotaBaseICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@PIS", SqlDbType.Decimal, 0, "PIS"));
                            dbCommand.Parameters.Add(new SqlParameter("@COFINS", SqlDbType.Decimal, 0, "COFINS"));
                            dbCommand.Parameters.Add(new SqlParameter("@TributacaoCST", SqlDbType.VarChar, 20, "TributacaoCST"));
                            dbCommand.Parameters.Add(new SqlParameter("@InscricaoEstadual", SqlDbType.VarChar, 8000, "InscricaoEstadual"));
                            dbCommand.Parameters.Add(new SqlParameter("@BaseICMS", SqlDbType.Decimal, 0, "BaseICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@PercentualICMS", SqlDbType.Decimal, 0, "PercentualICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalSemImpostos", SqlDbType.Decimal, 0, "TotalSemImpostos"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorTotalFreteComImpostos", SqlDbType.Decimal, 0, "ValorTotalFreteComImpostos"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoFrete", SqlDbType.Int, 0, "CodigoFrete"));
                            dbCommand.Parameters.Add(new SqlParameter("@Frete", SqlDbType.VarChar, 8000, "Frete"));
                            dbCommand.Parameters.Add(new SqlParameter("@Banco", SqlDbType.Int, 0, "Banco"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeBanco", SqlDbType.VarChar, 8000, "NomeBanco"));
                            dbCommand.Parameters.Add(new SqlParameter("@Agencia", SqlDbType.VarChar, 50, "Agencia"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoTransportadora", SqlDbType.VarChar, 20, "CodigoTransportadora"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeClienteFornecedor", SqlDbType.VarChar, 8000, "NomeClienteFornecedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@QuantidadeConvertidaPorKG", SqlDbType.Decimal, 0, "QuantidadeConvertidaPorKG"));
                            dbCommand.Parameters.Add(new SqlParameter("@ChaveNotaFiscal", SqlDbType.VarChar, 100, "ChaveNotaFiscal"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoNCM", SqlDbType.VarChar, 20, "CodigoNCM"));
                            dbCommand.Parameters.Add(new SqlParameter("@DescricaoNCM", SqlDbType.VarChar, 8000, "DescricaoNCM"));
                            dbCommand.Parameters.Add(new SqlParameter("@DCRE", SqlDbType.VarChar, 30, "DCRE"));

                            #endregion

                            #region Atribuição

                            dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                            dbCommand.Parameters["@CodigoEmpresa"].Value = Convert.ToInt32(row["CodigoEmpresa"]);
                            dbCommand.Parameters["@NomeEmpresa"].Value = row["NomeEmpresa"].ToString();
                            dbCommand.Parameters["@CodigoGrupoMateiral"].Value = Convert.ToInt32(row["CodigoGrupoMateiral"]);
                            dbCommand.Parameters["@GrupoMaterial"].Value = row["GrupoMaterial"].ToString();
                            dbCommand.Parameters["@NotaFiscal"].Value = row["NotaFiscal"].ToString();
                            dbCommand.Parameters["@StatusNota"].Value = row["StatusNota"].ToString();
                            dbCommand.Parameters["@NumeroPedido"].Value = Convert.ToInt32(row["NumeroPedido"]);
                            dbCommand.Parameters["@Cidade"].Value = row["Cidade"].ToString();
                            dbCommand.Parameters["@Estado"].Value = row["Estado"].ToString();
                            dbCommand.Parameters["@CodigoCliente"].Value = row["CodigoCliente"].ToString();
                            dbCommand.Parameters["@NomeCliente"].Value = row["NomeCliente"].ToString();
                            dbCommand.Parameters["@CNPJ"].Value = row["CNPJ"].ToString();
                            dbCommand.Parameters["@CodigoVendedor"].Value = Convert.ToInt32(row["CodigoVendedor"]);
                            dbCommand.Parameters["@NomeVendedor"].Value = row["NomeVendedor"].ToString();
                            dbCommand.Parameters["@ClasseVendedor"].Value = Convert.ToInt32(row["ClasseVendedor"]);
                            dbCommand.Parameters["@NomeClasse"].Value = row["NomeClasse"].ToString();
                            dbCommand.Parameters["@CaracTeristicasProduto"].Value = row["CaracTeristicasProduto"].ToString();
                            dbCommand.Parameters["@MesFaturamento"].Value = Convert.ToInt32(row["MesFaturamento"]);
                            dbCommand.Parameters["@DataEmissao"].Value = Convert.ToDateTime(row["DataEmissao"]);
                            dbCommand.Parameters["@CodigoProduto"].Value = row["CodigoProduto"].ToString();
                            dbCommand.Parameters["@NomeProduto"].Value = row["NomeProduto"].ToString();
                            dbCommand.Parameters["@UnidadeVenda"].Value = row["UnidadeVenda"].ToString();
                            dbCommand.Parameters["@Quantidade"].Value = Convert.ToDecimal(row["Quantidade"]);
                            dbCommand.Parameters["@QuantidadeConvertida"].Value = Convert.ToDecimal(row["QuantidadeConvertida"]);
                            dbCommand.Parameters["@ValorUnitario"].Value = Convert.ToDecimal(row["ValorUnitario"]);
                            dbCommand.Parameters["@ValorIPI"].Value = Convert.ToDecimal(row["ValorIPI"]);
                            dbCommand.Parameters["@TotalSemIPI"].Value = Convert.ToDecimal(row["TotalSemIPI"]);
                            dbCommand.Parameters["@TotalComIPI"].Value = Convert.ToDecimal(row["TotalComIPI"]);
                            dbCommand.Parameters["@TotalLinha"].Value = Convert.ToDecimal(row["TotalLinha"]);
                            dbCommand.Parameters["@CodigoCFOP"].Value = row["CodigoCFOP"].ToString();
                            dbCommand.Parameters["@NomeCFOP"].Value = row["NomeCFOP"].ToString();
                            dbCommand.Parameters["@CodiogCNAE"].Value = row["CodiogCNAE"].ToString();
                            dbCommand.Parameters["@DescricaoCNAE"].Value = row["DescricaoCNAE"].ToString();
                            dbCommand.Parameters["@Bairro"].Value = row["Bairro"].ToString();
                            dbCommand.Parameters["@CodigoCondicaoPagamento"].Value = Convert.ToInt32(row["CodigoCondicaoPagamento"]);
                            dbCommand.Parameters["@NomeCondicaoPagamento"].Value = row["NomeCondicao"].ToString();
                            dbCommand.Parameters["@ICMSItem"].Value = Convert.ToDecimal(row["ICMSItem"]);
                            dbCommand.Parameters["@PercentualDiferimentoICMS"].Value = Convert.ToDecimal(row["PercentualDiferimentoICMS"]);
                            dbCommand.Parameters["@ValorDiferimentoICMS"].Value = Convert.ToDecimal(row["ValorDiferimentoICMS"]);
                            dbCommand.Parameters["@ValorReducaoBase"].Value = Convert.ToDecimal(row["ValorReducaoBase"]);
                            dbCommand.Parameters["@ValorBaseICMS"].Value = Convert.ToDecimal(row["ValorBaseICMS"]);
                            dbCommand.Parameters["@AlicotaBaseICMS"].Value = Convert.ToDecimal(row["AlicotaBaseICMS"]);
                            dbCommand.Parameters["@PIS"].Value = Convert.ToDecimal(row["PIS"]);
                            dbCommand.Parameters["@COFINS"].Value = Convert.ToDecimal(row["COFINS"]);
                            dbCommand.Parameters["@TributacaoCST"].Value = row["TributacaoCST"].ToString();
                            dbCommand.Parameters["@InscricaoEstadual"].Value = row["InscricaoEstadual"].ToString();
                            dbCommand.Parameters["@BaseICMS"].Value = Convert.ToDecimal(row["BaseICMS"]);
                            dbCommand.Parameters["@PercentualICMS"].Value = Convert.ToDecimal(row["PercentualICMS"]);
                            dbCommand.Parameters["@TotalSemImpostos"].Value = Convert.ToDecimal(row["TotalSemImpostos"]);
                            dbCommand.Parameters["@ValorTotalFreteComImpostos"].Value = Convert.ToDecimal(row["ValorTotalFreteComImpostos"]);
                            dbCommand.Parameters["@CodigoFrete"].Value = Convert.ToInt32(row["CodigoFrete"]);
                            dbCommand.Parameters["@Frete"].Value = row["Frete"].ToString();
                            dbCommand.Parameters["@Banco"].Value = Convert.ToInt32(row["Banco"]);
                            dbCommand.Parameters["@NomeBanco"].Value = row["NomeBanco"].ToString();
                            dbCommand.Parameters["@Agencia"].Value = row["Agencia"].ToString();
                            dbCommand.Parameters["@CodigoTransportadora"].Value = row["CodigoTransportadora"].ToString();
                            dbCommand.Parameters["@NomeClienteFornecedor"].Value = row["CardName"].ToString();
                            dbCommand.Parameters["@QuantidadeConvertidaPorKG"].Value = Convert.ToDecimal(row["QuantidadeConvertidaPorKG"]);
                            dbCommand.Parameters["@ChaveNotaFiscal"].Value = row["ChaveNotaFiscal"].ToString();
                            dbCommand.Parameters["@CodigoNCM"].Value = row["CodigoNCM"].ToString();
                            dbCommand.Parameters["@DescricaoNCM"].Value = row["DescricaoNCM"].ToString();
                            dbCommand.Parameters["@DCRE"].Value = row["DCRE"].ToString();

                            #endregion

                            dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                            dbCommand.ExecuteNonQuery();

                            erro = (string)dbCommand.Parameters["@Erro"].Value;

                            if (erro != "")
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "")
                throw new Exception("Erro " + GetCurrentMethodName() + ".");

            return "";
        }

        public string CRM_SP_GRAVA_POSICAO_DIARIA_PENDENTES()
        {
            string erro = "";

            DataTable USP_MF_DataTable = new DataTable();

            try
            {
                objUSPMF.DocDateIni = Convert.ToDateTime(objUSPMF.DocDateFin).AddDays(-360).ToString();

                USP_MF_DataTable = objUSPMF.USP_MF_PEDIDOS_PENDENTES_CONV_CAMBIO();

                if (USP_MF_DataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in USP_MF_DataTable.Rows)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                            dbCommand.CommandTimeout = 500;

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@Erro", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "Erro", DataRowVersion.Default, null));

                            #region Declaração

                            dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoEmpresa", SqlDbType.Int, 0, "CodigoEmpresa"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeEmpresa", SqlDbType.VarChar, 8000, "NomeEmpresa"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoGrupoMateiral", SqlDbType.Int, 0, "CodigoGrupoMateiral"));
                            dbCommand.Parameters.Add(new SqlParameter("@GrupoMaterial", SqlDbType.VarChar, 8000, "GrupoMaterial"));
                            dbCommand.Parameters.Add(new SqlParameter("@DataPedido", SqlDbType.DateTime, 0, "DataPedido"));
                            dbCommand.Parameters.Add(new SqlParameter("@NumeroPedido", SqlDbType.Int, 0, "NumeroPedido"));
                            dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.VarChar, 8000, "Cidade"));
                            dbCommand.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 4, "Estado"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCliente", SqlDbType.VarChar, 20, "CodigoCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.VarChar, 8000, "NomeCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 20, "CNPJ"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoVendedor", SqlDbType.Int, 0, "CodigoVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeVendedor", SqlDbType.VarChar, 8000, "NomeVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@ClasseVendedor", SqlDbType.Int, 0, "ClasseVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeClasse", SqlDbType.VarChar, 8000, "NomeClasse"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoTransportadora", SqlDbType.VarChar, 20, "CodigoTransportadora"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeTransportadora", SqlDbType.VarChar, 8000, "NomeTransportadora"));
                            dbCommand.Parameters.Add(new SqlParameter("@CaracTeristicasProduto", SqlDbType.VarChar, 8000, "CaracTeristicasProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@OrdemProducao", SqlDbType.Int, 0, "OrdemProducao"));
                            dbCommand.Parameters.Add(new SqlParameter("@QuantidadeApontada", SqlDbType.Decimal, 28, "QuantidadeApontada"));
                            dbCommand.Parameters.Add(new SqlParameter("@DataSaida", SqlDbType.DateTime, 0, "DataSaida"));
                            dbCommand.Parameters.Add(new SqlParameter("@AnoEntrega", SqlDbType.VarChar, 10, "AnoEntrega"));
                            dbCommand.Parameters.Add(new SqlParameter("@MesEntrega", SqlDbType.VarChar, 10, "MesEntrega"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoProduto", SqlDbType.VarChar, 100, "CodigoProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeProduto", SqlDbType.VarChar, 8000, "NomeProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@UnidadeVenda", SqlDbType.VarChar, 20, "UnidadeVenda"));
                            dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 28, "Quantidade"));
                            dbCommand.Parameters.Add(new SqlParameter("@QuantidadeConvertida", SqlDbType.Decimal, 28, "QuantidadeConvertida"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorUnitario", SqlDbType.Decimal, 28, "ValorUnitario"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorIPI", SqlDbType.Decimal, 28, "ValorIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalSemIPI", SqlDbType.Decimal, 28, "TotalSemIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalComIPI", SqlDbType.Decimal, 28, "TotalComIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalLinha", SqlDbType.Decimal, 28, "TotalLinha"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCFOP", SqlDbType.VarChar, 20, "CodigoCFOP"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCFOP", SqlDbType.VarChar, 8000, "NomeCFOP"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodiogCNAE", SqlDbType.VarChar, 20, "CodiogCNAE"));
                            dbCommand.Parameters.Add(new SqlParameter("@DescricaoCNAE", SqlDbType.VarChar, 8000, "DescricaoCNAE"));
                            dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.VarChar, 8000, "Bairro"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCondicaoPagamento", SqlDbType.Int, 0, "CodigoCondicaoPagamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCondicaoPagamento", SqlDbType.VarChar, 8000, "NomeCondicaoPagamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@ICMSItem", SqlDbType.Decimal, 28, "ICMSItem"));
                            dbCommand.Parameters.Add(new SqlParameter("@ReducaoBaseCalculo", SqlDbType.Decimal, 28, "ReducaoBaseCalculo"));
                            dbCommand.Parameters.Add(new SqlParameter("@PIS", SqlDbType.Decimal, 28, "PIS"));
                            dbCommand.Parameters.Add(new SqlParameter("@COFINS", SqlDbType.Decimal, 28, "COFINS"));
                            dbCommand.Parameters.Add(new SqlParameter("@TributacaoCST", SqlDbType.VarChar, 20, "TributacaoCST"));
                            dbCommand.Parameters.Add(new SqlParameter("@InscricaoEstadual", SqlDbType.VarChar, 8000, "InscricaoEstadual"));
                            dbCommand.Parameters.Add(new SqlParameter("@BaseICMS", SqlDbType.Decimal, 28, "BaseICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@PercentualICMS", SqlDbType.Decimal, 28, "PercentualICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@PercentualDiferimentoICMS", SqlDbType.Decimal, 28, "PercentualDiferimentoICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorDiferimentoICMS", SqlDbType.Decimal, 28, "ValorDiferimentoICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalSemImpostos", SqlDbType.Decimal, 28, "TotalSemImpostos"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoFrete", SqlDbType.Int, 0, "CodigoFrete"));
                            dbCommand.Parameters.Add(new SqlParameter("@Frete", SqlDbType.VarChar, 8000, "Frete"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorTotalFreteComImpostos", SqlDbType.Decimal, 28, "ValorTotalFreteComImpostos"));
                            dbCommand.Parameters.Add(new SqlParameter("@StatusApontamento", SqlDbType.VarChar, 50, "StatusApontamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@EmbarqueImediato", SqlDbType.VarChar, 10, "EmbarqueImediato"));
                            dbCommand.Parameters.Add(new SqlParameter("@SituacaoFinanceiro", SqlDbType.VarChar, 100, "SituacaoFinanceiro"));

                            #endregion

                            #region Atribuição

                            dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                            dbCommand.Parameters["@CodigoEmpresa"].Value = Convert.ToInt32(row["CodigoEmpresa"]);
                            dbCommand.Parameters["@NomeEmpresa"].Value = row["NomeEmpresa"].ToString();
                            dbCommand.Parameters["@CodigoGrupoMateiral"].Value = Convert.ToInt32(row["CodigoGrupoMateiral"]);
                            dbCommand.Parameters["@GrupoMaterial"].Value = row["GrupoMaterial"].ToString();
                            dbCommand.Parameters["@DataPedido"].Value = Convert.ToDateTime(row["DataPedido"]);
                            dbCommand.Parameters["@NumeroPedido"].Value = Convert.ToInt32(row["NumeroPedido"]);
                            dbCommand.Parameters["@Cidade"].Value = row["Cidade"].ToString();
                            dbCommand.Parameters["@Estado"].Value = row["Estado"].ToString();
                            dbCommand.Parameters["@CodigoCliente"].Value = row["CodigoCliente"].ToString();
                            dbCommand.Parameters["@NomeCliente"].Value = row["NomeCliente"].ToString();
                            dbCommand.Parameters["@CNPJ"].Value = row["CNPJ"].ToString();
                            dbCommand.Parameters["@CodigoVendedor"].Value = Convert.ToInt32(row["CodigoVendedor"]);
                            dbCommand.Parameters["@NomeVendedor"].Value = row["NomeVendedor"].ToString();
                            dbCommand.Parameters["@ClasseVendedor"].Value = Convert.ToInt32(row["ClasseVendedor"]);
                            dbCommand.Parameters["@NomeClasse"].Value = row["NomeClasse"].ToString();
                            dbCommand.Parameters["@CodigoTransportadora"].Value = row["CodigoTransportadora"].ToString();
                            dbCommand.Parameters["@NomeTransportadora"].Value = row["NomeTransportadora"].ToString();
                            dbCommand.Parameters["@CaracTeristicasProduto"].Value = row["CaracTeristicasProduto"].ToString();
                            dbCommand.Parameters["@OrdemProducao"].Value = Convert.ToInt32(row["OrdemProducao"]);
                            dbCommand.Parameters["@QuantidadeApontada"].Value = Convert.ToDecimal(row["QuantidadeApontada"]);
                            dbCommand.Parameters["@DataSaida"].Value = Convert.ToDateTime(row["DataSaida"]);
                            dbCommand.Parameters["@AnoEntrega"].Value = row["AnoEntrega"].ToString();
                            dbCommand.Parameters["@MesEntrega"].Value = row["MesEntrega"].ToString();
                            dbCommand.Parameters["@CodigoProduto"].Value = row["CodigoProduto"].ToString();
                            dbCommand.Parameters["@NomeProduto"].Value = row["NomeProduto"].ToString();
                            dbCommand.Parameters["@UnidadeVenda"].Value = row["UnidadeVenda"].ToString();
                            dbCommand.Parameters["@Quantidade"].Value = Convert.ToDecimal(row["Quantidade"]);
                            dbCommand.Parameters["@QuantidadeConvertida"].Value = Convert.ToDecimal(row["QuantidadeConvertida"]);
                            dbCommand.Parameters["@ValorUnitario"].Value = Convert.ToDecimal(row["ValorUnitario"]);
                            dbCommand.Parameters["@ValorIPI"].Value = Convert.ToDecimal(row["ValorIPI"]);
                            dbCommand.Parameters["@TotalSemIPI"].Value = Convert.ToDecimal(row["TotalSemIPI"]);
                            dbCommand.Parameters["@TotalComIPI"].Value = Convert.ToDecimal(row["TotalComIPI"]);
                            dbCommand.Parameters["@TotalLinha"].Value = Convert.ToDecimal(row["TotalLinha"]);
                            dbCommand.Parameters["@CodigoCFOP"].Value = row["CodigoCFOP"].ToString();
                            dbCommand.Parameters["@NomeCFOP"].Value = row["NomeCFOP"].ToString();
                            dbCommand.Parameters["@CodiogCNAE"].Value = row["CodiogCNAE"].ToString();
                            dbCommand.Parameters["@DescricaoCNAE"].Value = row["DescricaoCNAE"].ToString();
                            dbCommand.Parameters["@Bairro"].Value = row["Bairro"].ToString();
                            dbCommand.Parameters["@CodigoCondicaoPagamento"].Value = Convert.ToInt32(row["CodigoCondicaoPagamento"]);
                            dbCommand.Parameters["@NomeCondicaoPagamento"].Value = row["NomeCondicaoPagamento"].ToString();
                            dbCommand.Parameters["@ICMSItem"].Value = Convert.ToDecimal(row["ICMSItem"]);
                            dbCommand.Parameters["@ReducaoBaseCalculo"].Value = Convert.ToDecimal(row["ReducaoBaseCalculo"]);
                            dbCommand.Parameters["@PIS"].Value = Convert.ToDecimal(row["PIS"]);
                            dbCommand.Parameters["@COFINS"].Value = Convert.ToDecimal(row["COFINS"]);
                            dbCommand.Parameters["@TributacaoCST"].Value = row["TributacaoCST"].ToString();
                            dbCommand.Parameters["@InscricaoEstadual"].Value = row["InscricaoEstadual"].ToString();
                            dbCommand.Parameters["@BaseICMS"].Value = Convert.ToDecimal(row["BaseICMS"]);
                            dbCommand.Parameters["@PercentualICMS"].Value = Convert.ToDecimal(row["PercentualICMS"]);
                            dbCommand.Parameters["@PercentualDiferimentoICMS"].Value = Convert.ToDecimal(row["PercentualDiferimentoICMS"]);
                            dbCommand.Parameters["@ValorDiferimentoICMS"].Value = Convert.ToDecimal(row["ValorDiferimentoICMS"]);
                            dbCommand.Parameters["@TotalSemImpostos"].Value = Convert.ToDecimal(row["TotalSemImpostos"]);
                            dbCommand.Parameters["@CodigoFrete"].Value = Convert.ToInt32(row["CodigoFrete"]);
                            dbCommand.Parameters["@Frete"].Value = row["Frete"].ToString();
                            dbCommand.Parameters["@ValorTotalFreteComImpostos"].Value = Convert.ToDecimal(row["ValorTotalFreteComImpostos"]);
                            dbCommand.Parameters["@StatusApontamento"].Value = row["StatusApontamento"].ToString();
                            dbCommand.Parameters["@EmbarqueImediato"].Value = row["EmbarqueImediato"].ToString();
                            dbCommand.Parameters["@SituacaoFinanceiro"].Value = row["SituacaoFinanceiro"].ToString();

                            #endregion

                            dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                            dbCommand.ExecuteNonQuery();

                            erro = (string)dbCommand.Parameters["@Erro"].Value;

                            if (erro != "")
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "")
                throw new Exception("Erro " + GetCurrentMethodName() + ".");

            return "";
        }

        public string CRM_SP_GRAVA_POSICAO_DIARIA_DEVOLUCOES()
        {
            string erro = "";

            DataTable USP_MF_DataTable = new DataTable();

            int NumeroPedido = 0;

            try
            {
                USP_MF_DataTable = objUSPMF.USP_MF_NOTAS_DEVOLUCAO();

                if (USP_MF_DataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in USP_MF_DataTable.Rows)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                        {
                            //Abre Conexao
                            dbConnection.Open();

                            SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                            dbCommand.CommandTimeout = 500;

                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.Add(new SqlParameter("@Erro", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "Erro", DataRowVersion.Default, null));

                            #region Declaração

                            dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "NumeroPedido"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoEmpresa", SqlDbType.Int, 0, "NumeroPedido"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeEmpresa", SqlDbType.VarChar, 8000, "NomeEmpresa"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoGrupoMateiral", SqlDbType.Int));
                            dbCommand.Parameters.Add(new SqlParameter("@GrupoMaterial", SqlDbType.VarChar, 8000, "GrupoMaterial"));
                            dbCommand.Parameters.Add(new SqlParameter("@NotaFiscal", SqlDbType.VarChar, 10, "NotaFiscal"));
                            dbCommand.Parameters.Add(new SqlParameter("@StatusNota", SqlDbType.VarChar, 10, "StatusNota"));
                            dbCommand.Parameters.Add(new SqlParameter("@NumeroPedido", SqlDbType.Int, 0, "NumeroPedido"));
                            dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.VarChar, 8000, "Cidade"));
                            dbCommand.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 4, "Estado"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCliente", SqlDbType.VarChar, 20, "CodigoCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.VarChar, 8000, "NomeCliente"));
                            dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 20, "CNPJ"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoVendedor", SqlDbType.Int, 0, "CodigoVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeVendedor", SqlDbType.VarChar, 8000, "NomeVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@ClasseVendedor", SqlDbType.Int, 0, "ClasseVendedor"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeClasse", SqlDbType.VarChar, 8000, "NomeClasse"));
                            dbCommand.Parameters.Add(new SqlParameter("@CaracTeristicasProduto", SqlDbType.VarChar, 8000, "CaracTeristicasProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@MesFaturamento", SqlDbType.Int, 0, "MesFaturamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@DataEmissao", SqlDbType.DateTime, 0, "DataEmissao"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoProduto", SqlDbType.VarChar, 100, "CodigoProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeProduto", SqlDbType.VarChar, 8000, "NomeProduto"));
                            dbCommand.Parameters.Add(new SqlParameter("@UnidadeVenda", SqlDbType.VarChar, 20, "UnidadeVenda"));
                            dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 0, "Quantidade"));
                            dbCommand.Parameters.Add(new SqlParameter("@QuantidadeConvertida", SqlDbType.Decimal, 0, "QuantidadeConvertida"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorUnitario", SqlDbType.Decimal, 0, "ValorUnitario"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorIPI", SqlDbType.Decimal, 0, "ValorIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalSemIPI", SqlDbType.Decimal, 0, "TotalSemIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalComIPI", SqlDbType.Decimal, 0, "TotalComIPI"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalLinha", SqlDbType.Decimal, 0, "TotalLinha"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCFOP", SqlDbType.VarChar, 20, "CodigoCFOP"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCFOP", SqlDbType.VarChar, 8000, "NomeCFOP"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodiogCNAE", SqlDbType.VarChar, 20, "CodiogCNAE"));
                            dbCommand.Parameters.Add(new SqlParameter("@DescricaoCNAE", SqlDbType.VarChar, 8000, "DescricaoCNAE"));
                            dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.VarChar, 8000, "Bairro"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoCondicaoPagamento", SqlDbType.Int, 0, "CodigoCondicaoPagamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeCondicaoPagamento", SqlDbType.VarChar, 8000, "NomeCondicaoPagamento"));
                            dbCommand.Parameters.Add(new SqlParameter("@ICMSItem", SqlDbType.Decimal, 0, "ICMSItem"));
                            dbCommand.Parameters.Add(new SqlParameter("@ReducaoBase", SqlDbType.Decimal, 0, "ReducaoBase"));
                            dbCommand.Parameters.Add(new SqlParameter("@PIS", SqlDbType.Decimal, 0, "PIS"));
                            dbCommand.Parameters.Add(new SqlParameter("@COFINS", SqlDbType.Decimal, 0, "COFINS"));
                            dbCommand.Parameters.Add(new SqlParameter("@TributacaoCST", SqlDbType.VarChar, 20, "TributacaoCST"));
                            dbCommand.Parameters.Add(new SqlParameter("@InscricaoEstadual", SqlDbType.VarChar, 8000, "InscricaoEstadual"));
                            dbCommand.Parameters.Add(new SqlParameter("@BaseICMS", SqlDbType.Decimal, 0, "BaseICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@PercentualICMS", SqlDbType.Decimal, 0, "PercentualICMS"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoFrete", SqlDbType.Int, 0, "CodigoFrete"));
                            dbCommand.Parameters.Add(new SqlParameter("@Frete", SqlDbType.VarChar, 8000, "Frete"));
                            dbCommand.Parameters.Add(new SqlParameter("@Banco", SqlDbType.Int, 0, "Banco"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeBanco", SqlDbType.VarChar, 8000, "NomeBanco"));
                            dbCommand.Parameters.Add(new SqlParameter("@Agencia", SqlDbType.VarChar, 50, "Agencia"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalSemImpostos", SqlDbType.Decimal, 0, "TotalSemImpostos"));

                            #endregion

                            #region Atribuição

                            dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;
                            dbCommand.Parameters["@CodigoEmpresa"].Value = Convert.ToInt32(row["CodigoEmpresa"]);
                            dbCommand.Parameters["@NomeEmpresa"].Value = row["NomeEmpresa"].ToString();
                            dbCommand.Parameters["@CodigoGrupoMateiral"].Value = Convert.ToInt32(row["CodigoGrupoMateiral"]);
                            dbCommand.Parameters["@GrupoMaterial"].Value = row["GrupoMaterial"].ToString();
                            dbCommand.Parameters["@NotaFiscal"].Value = row["NotaFiscal"].ToString();
                            dbCommand.Parameters["@StatusNota"].Value = row["StatusNota"].ToString();
                            dbCommand.Parameters["@NumeroPedido"].Value = Convert.ToInt32(row["NumeroPedido"]);
                            NumeroPedido = Convert.ToInt32(row["NumeroPedido"]);
                            dbCommand.Parameters["@Cidade"].Value = row["Cidade"].ToString();
                            dbCommand.Parameters["@Estado"].Value = row["Estado"].ToString();
                            dbCommand.Parameters["@CodigoCliente"].Value = row["CodigoCliente"].ToString();
                            dbCommand.Parameters["@NomeCliente"].Value = row["NomeCliente"].ToString();
                            dbCommand.Parameters["@CNPJ"].Value = row["CNPJ"].ToString();
                            dbCommand.Parameters["@CodigoVendedor"].Value = Convert.ToInt32(row["CodigoVendedor"]);
                            dbCommand.Parameters["@NomeVendedor"].Value = row["NomeVendedor"].ToString();
                            dbCommand.Parameters["@ClasseVendedor"].Value = Convert.ToInt32(row["ClasseVendedor"]);
                            dbCommand.Parameters["@NomeClasse"].Value = row["NomeClasse"].ToString();
                            dbCommand.Parameters["@CaracTeristicasProduto"].Value = row["CaracTeristicasProduto"].ToString();
                            dbCommand.Parameters["@MesFaturamento"].Value = Convert.ToInt32(row["MesFaturamento"]);
                            dbCommand.Parameters["@DataEmissao"].Value = Convert.ToDateTime(row["DataEmissao"]);
                            dbCommand.Parameters["@CodigoProduto"].Value = row["CodigoProduto"].ToString();
                            dbCommand.Parameters["@NomeProduto"].Value = row["NomeProduto"].ToString();
                            dbCommand.Parameters["@UnidadeVenda"].Value = row["UnidadeVenda"].ToString();
                            dbCommand.Parameters["@Quantidade"].Value = Convert.ToDecimal(row["Quantidade"]);
                            dbCommand.Parameters["@QuantidadeConvertida"].Value = Convert.ToDecimal(row["QuantidadeConvertida"]);
                            dbCommand.Parameters["@ValorUnitario"].Value = Convert.ToDecimal(row["ValorUnitario"]);
                            dbCommand.Parameters["@ValorIPI"].Value = Convert.ToDecimal(row["ValorIPI"]);
                            dbCommand.Parameters["@TotalSemIPI"].Value = Convert.ToDecimal(row["TotalSemIPI"]);
                            dbCommand.Parameters["@TotalComIPI"].Value = Convert.ToDecimal(row["TotalComIPI"]);
                            dbCommand.Parameters["@TotalLinha"].Value = Convert.ToDecimal(row["TotalLinha"]);
                            dbCommand.Parameters["@CodigoCFOP"].Value = row["CodigoCFOP"].ToString();
                            dbCommand.Parameters["@NomeCFOP"].Value = row["NomeCFOP"].ToString();
                            dbCommand.Parameters["@CodiogCNAE"].Value = row["CodiogCNAE"].ToString();
                            dbCommand.Parameters["@DescricaoCNAE"].Value = row["DescricaoCNAE"].ToString();
                            dbCommand.Parameters["@Bairro"].Value = row["Bairro"].ToString();
                            dbCommand.Parameters["@CodigoCondicaoPagamento"].Value = Convert.ToInt32(row["CodigoCondicaoPagamento"]);
                            dbCommand.Parameters["@NomeCondicaoPagamento"].Value = row["NomeCondicao"].ToString();
                            dbCommand.Parameters["@ICMSItem"].Value = Convert.ToDecimal(row["ICMSItem"]);
                            dbCommand.Parameters["@ReducaoBase"].Value = Convert.ToDecimal(row["ReducaoBaseCalculo"]);
                            dbCommand.Parameters["@PIS"].Value = Convert.ToDecimal(row["PIS"]);
                            dbCommand.Parameters["@COFINS"].Value = Convert.ToDecimal(row["COFINS"]);
                            dbCommand.Parameters["@TributacaoCST"].Value = row["TributacaoCST"].ToString();
                            dbCommand.Parameters["@InscricaoEstadual"].Value = row["InscricaoEstadual"].ToString();
                            dbCommand.Parameters["@BaseICMS"].Value = Convert.ToDecimal(row["BaseICMS"]);
                            dbCommand.Parameters["@PercentualICMS"].Value = Convert.ToDecimal(row["PercentualICMS"]);
                            dbCommand.Parameters["@CodigoFrete"].Value = Convert.ToInt32(row["CodigoFrete"]);
                            dbCommand.Parameters["@Frete"].Value = row["Frete"].ToString();
                            dbCommand.Parameters["@Banco"].Value = Convert.ToInt32(row["Banco"]);
                            dbCommand.Parameters["@NomeBanco"].Value = row["NomeBanco"].ToString();
                            dbCommand.Parameters["@Agencia"].Value = row["Agencia"].ToString();
                            dbCommand.Parameters["@TotalSemImpostos"].Value = Convert.ToDecimal(row["TotalSemImpostos"]);

                            #endregion

                            dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                            dbCommand.ExecuteNonQuery();

                            erro = (string)dbCommand.Parameters["@Erro"].Value;

                            if (erro != "")
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "")
                throw new Exception("Erro " + GetCurrentMethodName() + ".");

            return "";
        }

        public string CRM_SP_GRAVA_POSICAO_DIARIA_ESTRATIFICACAO()
        {
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                    dbCommand.CommandTimeout = 500;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Erro", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "Erro", DataRowVersion.Default, null));

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@Erro"].Value;
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "")
                throw new Exception("Erro " + GetCurrentMethodName() + ".");

            return "";
        }

        public string CRM_SP_GRAVA_POSICAO_DIARIA_ESTRATIFICACAO_BACKLOG()
        {
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                    dbCommand.CommandTimeout = 500;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Erro", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "Erro", DataRowVersion.Default, null));

                    dbCommand.Parameters.Add(new SqlParameter("@IDPosicaoDiaria", SqlDbType.Int, 0, "IDPosicaoDiaria"));

                    dbCommand.Parameters["@IDPosicaoDiaria"].Value = this.IDPosicaoDiaria;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@Erro"].Value;
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "")
                throw new Exception("Erro " + GetCurrentMethodName() + ".");

            return "";
        }

        static string GetCurrentMethodName()
        {
            // Obtém o stack trace
            StackTrace stackTrace = new StackTrace();

            // Obtém o método atual na pilha de chamadas
            var currentMethod = stackTrace.GetFrame(1).GetMethod();

            // Retorna o nome do método
            return currentMethod.Name;
        }

        public int GetIDPosicaoDiaria()
        {
            return IDPosicaoDiaria;
        }

        public string EnviaEmail()
        {
            string erro = "";

            if (objGeraPosicaoDiariaModel.Automatico == "Sim")
            {
                try
                {
                    VendasWeb.usuario objUsuario = new VendasWeb.usuario();

                    DataTable PosicaoDiariaEmails = objControladoriaClass.Consulta_POSICAO_DIARIA_PARM_EMAIL();

                    //Obs: feito para não repetir emails caso exista a possibilidade de terem repetidos
                    List<string> emails = new List<string>();

                    if (PosicaoDiariaEmails.Rows.Count > 0)
                    {
                        foreach (DataRow row in PosicaoDiariaEmails.Rows)
                        {
                            if (!emails.Contains(row["Email"].ToString()))
                                emails.Add(row["Email"].ToString());
                        }
                    }

                    objControladoriaClass.IDPosicaoDiaria = IDPosicaoDiaria;

                    if (emails.Count > 0)
                    {
                        foreach (string email in emails)
                        {
                            VendasWeb.enviarEmail OBJMail = new VendasWeb.enviarEmail();

                            OBJMail.Historico = "Historico";

                            OBJMail.FormataHTMLPosicaoDiaria(objControladoriaClass.IDPosicaoDiaria);

                            OBJMail.EmailDestinatario = email;

                            OBJMail.enviaEmailPosicaoDiariaFormatadoComAnexos();
                        }
                    }
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }

                if (erro != "")
                    throw new Exception("Erro no" + GetCurrentMethodName() + "(): " + erro);
            }

            return "";
        }
    }
}