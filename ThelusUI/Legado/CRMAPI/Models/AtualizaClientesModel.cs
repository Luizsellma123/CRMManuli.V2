using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaClientesModel
    {
        public string CodigoClienteSAP { get; set; }

        List<ClienteClass> Clientes = new List<ClienteClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaClientes()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select ");
                stringSQL.AppendLine("ISNULL(OCRD.CardName,'') NomeCliente, ");
                stringSQL.AppendLine("ISNULL(OCRD.AliasName,'') NomeFantasia, ");
                stringSQL.AppendLine("ISNULL(OCRD.Fax,'') CNPJ,  ");
                stringSQL.AppendLine("ISNULL(OCRD.Phone1,'') Telefone, ");
                stringSQL.AppendLine("ISNULL(OCRD.E_Mail,'') Email, ");
                stringSQL.AppendLine("ISNULL(OCRD.Notes,'') ObservacaoSimples, ");
                stringSQL.AppendLine("ISNULL(OCRD.Free_text,'') ObservacaoCompleta, ");
                stringSQL.AppendLine("ISNULL(OCRD.SlpCode,0) CodigoVendedorSAP, ");
                stringSQL.AppendLine("ISNULL(OCRD.CardType,'') TipoCliente, ");
                stringSQL.AppendLine("ISNULL(OCRD.U_IB_NAT_JURIDICA,'') NaturezaJuridica, ");
                stringSQL.AppendLine("ISNULL(OCRD.U_TX_IndIEDest,'') IndicadorIndIEDest, ");
                stringSQL.AppendLine("ISNULL(OCRD.U_TX_IndNat,'') IndicadorNatureza, ");
                stringSQL.AppendLine("ISNULL(OCRD.U_TX_IndFinal,'') IndicadorOpConsumidor, ");
                stringSQL.AppendLine("ISNULL(OCRD.U_IB_Enquadr_Trib,'') EnquadramentoTributario, ");
                stringSQL.AppendLine("isnull(OCRD.U_IB_CartaIPI, '') CartaIPI, ");
                stringSQL.AppendLine("(CASE WHEN ");
                stringSQL.AppendLine("	ISNULL(CONVERT(VARCHAR(MAX),OCRD.U_IB_DataCartaIPI,103),'') = '01/01/1900' ");
                stringSQL.AppendLine("	THEN '' ");
                stringSQL.AppendLine("	ELSE ISNULL(CONVERT(VARCHAR(MAX),OCRD.U_IB_DataCartaIPI,103),'') END ");
                stringSQL.AppendLine(") DataCarta, ");
                stringSQL.AppendLine(" ");
                stringSQL.AppendLine("ISNULL(OCRD.U_TX_SN,'') SimplesNacional, ");
                stringSQL.AppendLine("ISNULL(OCRD.U_TX_ProdRural,'') ProdutorRural, ");
                stringSQL.AppendLine("ISNULL(OCRD.CardCode,'') CodigoClienteSAP, ");
                stringSQL.AppendLine("'' GrupoEconomico, ");
                stringSQL.AppendLine("ISNULL(OCRD.GroupCode,0) GrupoClientes, ");
                stringSQL.AppendLine("ISNULL(OCRD.AtcEntry,0) IdAnexoSAP, ");
                stringSQL.AppendLine("ISNULL(U_IB_CPOM,'') CPOM, ");
                stringSQL.AppendLine("isnull(CreditLine, 0) LimiteCredito, ");
                stringSQL.AppendLine("ISNULL(GroupNum,'') CondicaoPagamentoPadraoSAP, ");
                stringSQL.AppendLine("isnull(OCRD.U_MF_CLS_COM, '') ClassificacaoComercial ");              
                stringSQL.AppendLine("from OCRD ");
                stringSQL.AppendLine("where convert(date, isnull(OCRD.UpdateDate, OCRD.CreateDate)) = ");
                stringSQL.AppendLine("convert(date, getdate()) ");
                //stringSQL.AppendLine("where '' = '' ");
                stringSQL.AppendLine("and (OCRD.CardCode = '" + CodigoClienteSAP + "' or '' = '" + CodigoClienteSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                Clientes = objUtilClass.ConvertDataTable<ClienteClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os Clientes do SAP.");
            }
        }

        public string AtualizaClientes()
        {
            string erro = "";

            try
            {
                CarregaClientes();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (ClienteClass Cliente in Clientes)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLIENTE", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.NVarChar, 100, "@NomeCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeFantasia", SqlDbType.NText, 0, "NomeFantasia"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.NVarChar, 20, "CNPJ"));
                        dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.NVarChar, 20, "Telefone"));
                        dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100, "Email"));
                        dbCommand.Parameters.Add(new SqlParameter("@ObservacaoSimples", SqlDbType.NVarChar, 20, "ObservacaoSimples"));
                        dbCommand.Parameters.Add(new SqlParameter("@ObservacaoCompleta", SqlDbType.NText, 0, "ObservacaoCompleta"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoVendedorSAP", SqlDbType.Int, 0, "CodigoVendedorSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoCliente", SqlDbType.NVarChar, 30, "TipoCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@NaturezaJuridica", SqlDbType.NVarChar, 50, "NaturezaJuridica"));
                        dbCommand.Parameters.Add(new SqlParameter("@IndicadorIndIEDest", SqlDbType.NVarChar, 2, "IndicadorIndIEDest"));
                        dbCommand.Parameters.Add(new SqlParameter("@IndicadorNatureza", SqlDbType.NVarChar, 2, "IndicadorNatureza"));
                        dbCommand.Parameters.Add(new SqlParameter("@IndicadorOpConsumidor", SqlDbType.NVarChar, 2, "IndicadorOpConsumidor"));
                        dbCommand.Parameters.Add(new SqlParameter("@EnquadramentoTributario", SqlDbType.NVarChar, 50, "EnquadramentoTributario"));
                        dbCommand.Parameters.Add(new SqlParameter("@CartaIPI", SqlDbType.NVarChar, 10, "CartaIPI"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataCarta", SqlDbType.DateTime, 0, "DataCarta"));
                        dbCommand.Parameters.Add(new SqlParameter("@SimplesNacional", SqlDbType.NVarChar, 1, "SimplesNacional"));
                        dbCommand.Parameters.Add(new SqlParameter("@ProdutorRural", SqlDbType.NVarChar, 2, "ProdutorRural"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@GrupoEconomico", SqlDbType.NVarChar, 50, "GrupoEconomico"));
                        dbCommand.Parameters.Add(new SqlParameter("@GrupoClientes", SqlDbType.Int, 0, "GrupoClientes"));
                        dbCommand.Parameters.Add(new SqlParameter("@IdAnexoSAP", SqlDbType.Int, 0, "IdAnexoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CPOM", SqlDbType.VarChar, 50, "CPOM"));
                        dbCommand.Parameters.Add(new SqlParameter("@LimiteCredito", SqlDbType.Decimal, 0, "LimiteCredito"));
                        dbCommand.Parameters.Add(new SqlParameter("@CondicaoPagamentoPadraoSAP", SqlDbType.Int, 0, "CondicaoPagamentoPadraoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@ClassificacaoComercial", SqlDbType.VarChar, 8000, "ClassificacaoComercial"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@NomeCliente"].Value = Cliente.NomeCliente ?? "";
                        dbCommand.Parameters["@NomeFantasia"].Value = Cliente.NomeFantasia ?? "";
                        dbCommand.Parameters["@CNPJ"].Value = Cliente.CNPJ ?? "";
                        dbCommand.Parameters["@Telefone"].Value = Cliente.Telefone ?? "";
                        dbCommand.Parameters["@Email"].Value = Cliente.Email ?? "";
                        dbCommand.Parameters["@ObservacaoSimples"].Value = Cliente.ObservacaoSimples ?? "";
                        dbCommand.Parameters["@ObservacaoCompleta"].Value = Cliente.ObservacaoCompleta ?? "";
                        dbCommand.Parameters["@CodigoVendedorSAP"].Value = Cliente.CodigoVendedorSAP;
                        dbCommand.Parameters["@TipoCliente"].Value = Cliente.TipoCliente ?? "";
                        dbCommand.Parameters["@NaturezaJuridica"].Value = Cliente.NaturezaJuridica ?? "";
                        dbCommand.Parameters["@IndicadorIndIEDest"].Value = Cliente.IndicadorIndIEDest ?? "";
                        dbCommand.Parameters["@IndicadorNatureza"].Value = Cliente.IndicadorNatureza ?? "";
                        dbCommand.Parameters["@IndicadorOpConsumidor"].Value = Cliente.IndicadorOpConsumidor ?? "";
                        dbCommand.Parameters["@EnquadramentoTributario"].Value = Cliente.EnquadramentoTributario ?? "";
                        dbCommand.Parameters["@CartaIPI"].Value = Cliente.CartaIPI ?? "";

                        dbCommand.Parameters["@DataCarta"].Value = Cliente.DataCarta == "" ? "1900-01-01"
                            : Convert.ToDateTime(Cliente.DataCarta).ToString("yyyy-MM-dd");

                        dbCommand.Parameters["@SimplesNacional"].Value = Cliente.SimplesNacional ?? "";
                        dbCommand.Parameters["@ProdutorRural"].Value = Cliente.ProdutorRural ?? "";
                        dbCommand.Parameters["@CodigoClienteSAP"].Value = Cliente.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@GrupoEconomico"].Value = Cliente.GrupoEconomico ?? "";
                        dbCommand.Parameters["@GrupoClientes"].Value = Cliente.GrupoClientes;
                        dbCommand.Parameters["@IdAnexoSAP"].Value = Cliente.IdAnexoSAP;
                        dbCommand.Parameters["@CPOM"].Value = Cliente.CPOM ?? "";
                        dbCommand.Parameters["@LimiteCredito"].Value = Cliente.LimiteCredito;
                        dbCommand.Parameters["@CondicaoPagamentoPadraoSAP"].Value = Cliente.CondicaoPagamentoPadraoSAP;

                        dbCommand.Parameters["@ClassificacaoComercial"].Value = Cliente.ClassificacaoComercial;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = ex.ToString();

                erro = "Erro na importação dos Clientes.";
            }

            return erro;
        }
    }
}