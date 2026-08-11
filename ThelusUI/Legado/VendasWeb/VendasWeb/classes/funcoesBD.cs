using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

struct ProcessaItemPedido
{
    public string UnidadeMedida; public double VALOR_INTERM; public double PERC_ACRESCIMO;
    public double PERC_DESCONTO; public string CLAS_FISCAL; public double PERC_II; public double PERC_IPI; public double PERC_FUNRURAL; public string CODIGO_TRIBUTACAO;
    public double PERC_ICMS_SUB; public double PERC_RED_ICMS_SUB; public double PERC_MARGEM_LUCRO; public double PERC_ICMS; public double PERC_RED_ICMS; public double VALOR_ICMS;
    public double BASE_ICMS; public double VALOR_ICMS_SUB; public double BASE_ICMS_SUB; public double VALOR_II; public double BASE_II; public double VALOR_IPI; public double BASE_IPI;
    public double VALOR_FUNRURAL; public double BASE_FUNRURAL; public double VALOR_DESCONTO; public double VALOR_ACRESCIMO; public double VALOR_PRECO_LISTA;
    public double QUANTIDADE_ATUAL; public string VCalculaPrecoListaOut; public string VCalculaIPIPrecoListaOut; public string VINDECONCODOUT; public double Vvalorcambioout;
    public double VALOR_TOTAL_OUT; public double QTD_IPI; public double QTD_UFESP; public double PERC_PIS; public double VALOR_PIS; public double BASE_PIS; public double PERC_COFINS;
    public double VALOR_COFINS; public double BASE_COFINS; public double VALOR_CUSTO; public double VMARKUP; public string VNATOPCODESTROUT; public double PERC_DESCGERAL_OUT;
    public double PERC_ACRESCGERAL_OUT; public double PERC_IRRF; public double VALOR_IRRF; public double BASE_IRRF; public string VCalculaIcmsZFM; public double PERCDESCICMSDIFALIQ;
    public double VALDESCICMSREDBASECALC; public string FPCLASSEENTCOD; public string FPPROGDESCCOD; public string FPPROGDESCCODPGC; public double FPMULTCOMPRA;
    public double FPMULTVENDA; public double FPENTFMVALOR; public string FPPRODAPLICAFM; public double FPVALORTABPV; public string FPICMSINCLUSO; public string FPISSINCLUSO;
    public string FPIPIINCLUSO; public string FPCOFINSINCLUSO; public string FPPISINCLUSO; public double FPVALPROMOCAO; public DateTime FPDATAINIPROM; public DateTime FPDATAFIMPROM;
    public double FPPERCCOFINSENT; public double FPPERCISSFORNEC; public double FPIMPCUSTO; public double FPIMPVENDA; public double FPVALUNITVENDALIQ; public double FPCUSTOUNITLIQ;
    public double FPPERCICMSCUSTO; public double FPPERCICMSVENDA; public double FPPERCISS; public double FPPERCPIS; public double FPPERCCOFINS; public double FPPERCIPI;
    public double FPVALCUSTOTABPV; public double PERC_RED_IPI; public double PERC_RED_COFINS; public double PERC_RED_PIS; public double PERC_RED_II; public string RED_IPI;
    public string RED_COFINS; public string RED_PIS; public string RED_II; public string RED_ICMS; public string RED_ICMS_SUB; public double FPPRODECVALOR; public string FPPRODAPLICAEC;
    public double PERCDIFERIMENTO; public double PERCCREDPRESUMIDO; public double PERCLIMCREDPRESUMIDO; public double PERCCALCVALRECOLHER; public double PERC_CSLLRF;
    public double VALOR_CSLLRF; public double BASE_CSLLRF; public double PERC_PISRF; public double VALOR_PISRF; public double BASE_PISRF; public double PERC_COFINSRF;
    public double VALOR_COFINSRF; public double BASE_COFINSRF; public double FPPERCACRESC; public double FPPERCDESC; public double FPPRECOLISTA; public double PERCICMSEXONERADO;
    public string TRIBBMODBCCOD; public string TRIBBMODBCSTCOD; public string TRIBIPICOD; public string TRIBCOFINSCOD; public string TRIBPISCOD; public double PERC_ICMS_OPER;
    public double BASE_ICMS_OPER; public double VALOR_ICMS_OPER; public double PRECO_VENDA_VAREJO; public double VALOR_SELO_CTRL; public double PERC_DESCPROG_OUT;
    public string TIPOFATCOD_OUT; public string CONFTRIBSIMPNACCOD; public double FPMARGLUCROST; public double FPVALICMSRETST; public double FPVALICMS; public double FPVALBASEICMSSTDAEGNRE;
    public double FPMARGLUCROSTDAEGNRE; public double FPPERCICMSSTDAEGNRE; public double FPPRECOLISTASTDAEGNRE; public double FPVALICMSSTDAEGNRE; public double FPVALICMSRETSTDAEGNRE;
    public string UTILIZA_MULTCOMPRA_OUT; public double FPQTDPAUTAICMS; public double FPVALPAUTAICMS; public double FPQTDPAUTAIPI; public double FPVALPAUTAIPI; public double FPQTDPAUTAPIS;
    public double FPVALPAUTAPIS; public double FPQTDPAUTACOFINS; public double FPVALPAUTACOFINS;
}

namespace VendasWeb
{
    public class funcoesBD : GerencialVendas.clsConexao
    {
        ProcessaItemPedido InfItem = new ProcessaItemPedido();

        //Metodo para executar leitura de dados
        public SqlDataReader ExecutaReader(string paramSQL)
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand(paramSQL, dbConnection);

                SqlDataReader dataReader = dbCommand.ExecuteReader();

                return dataReader;
            }
        }

        //Metodo para executar leitura de dados retornando data datable
        /*public DataTable ExecutaDataTable(string paramSQL)
        {
            DataTable outputTable = new DataTable();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {                               
                SqlCommand dbCommand = new SqlCommand(paramSQL, dbConnection);
                dbConnection.Open();

                SqlDataReader dataReader = dbCommand.ExecuteReader();

                outputTable.Load(dataReader);

                dataReader.Close();                
            }
            return outputTable;
        }*/

        public Boolean ExecutaSQL(string paramQuery)
        {
            Boolean blnResultado;
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand(paramQuery, dbConnection);               
                try
                {
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

        //Metodo para formatar data dd/mm/aaaa em numero tipo aaaammdd
        public int FormataDataComparacao(string data)
        {

            if (data != "")
            {
                string[] DataDig = data.Split('/');
                string Dia = DataDig[0];
                string Mes = DataDig[1];
                string Ano = DataDig[2];

                data = Ano + Mes + Dia;

                return Convert.ToInt32(data);
            }
            else
            {
                return 0;
            }

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


        public string recuperaNumeroPedido(string Empresa)
        {
            string numeroPedido;
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                //Chama procedure para buscar número do pedido
                SqlCommand dbCommand = new SqlCommand("gerar_codigo", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 20, "empresa"));
                dbCommand.Parameters.Add(new SqlParameter("@tabela", SqlDbType.VarChar, 31, "tabela"));
                dbCommand.Parameters.Add(new SqlParameter("@codigo", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "codigo", DataRowVersion.Default, null));

                dbCommand.Parameters[0].Value = Empresa;
                dbCommand.Parameters[1].Value = "PED_VENDA";

                dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                dbCommand.ExecuteNonQuery();
                numeroPedido = ((int)dbCommand.Parameters["@codigo"].Value).ToString();

                numeroPedido = ("0000000").Substring(0, 7 - numeroPedido.Length) + numeroPedido;
            }
            return numeroPedido;
        }

        public string gravaPedido(string empCod, string PedVendaNum, string TipoPedVenda, string DataEmissao,
            string dataEntrega, string codEntidade, string tipVendaCod, string regCodEstr, string entNome,
            string usuario, string observacao, string historico, string TipoFrete, string transportadora,
            string natureza, string tabela, string operacao, string especie, string tipoOperacao, string codStatus,
            string descricaoStatus, double QuantidadeVolumes, string EspecieVolume, double PesoLiquido, double PesoBruto, string PedVendaNumPedEnt)
        {
            string strError = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("USER_WebVENDAS_PED_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@vEmpCod", SqlDbType.VarChar, 20, "vEmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNum", SqlDbType.VarChar, 10, "vPedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaTipo", SqlDbType.VarChar, 10, "vPedVendaTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@dDataEmissao", SqlDbType.DateTime, 10, "dDataEmissao"));
                    dbCommand.Parameters.Add(new SqlParameter("@dDataEntrega", SqlDbType.DateTime, 10, "dDataEntrega"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntCod", SqlDbType.VarChar, 7, "vEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vTipoVendaCod", SqlDbType.VarChar, 7, "vTipoVendaCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNatOpProd", SqlDbType.VarChar, 15, "vPedVendaNatOpProd"));
                    dbCommand.Parameters.Add(new SqlParameter("@nValorTotal", SqlDbType.Decimal, 0, "nValorTotal"));
                    dbCommand.Parameters.Add(new SqlParameter("@vRegCodEstr", SqlDbType.VarChar, 15, "vRegCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@vStatPedVendaCod", SqlDbType.VarChar, 7, "vStatPedVendaCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaStatDescr", SqlDbType.VarChar, 30, "vPedVendaStatDescr"));
                    dbCommand.Parameters.Add(new SqlParameter("@nPedVendaQtdVol", SqlDbType.Decimal, 0, "nPedVendaQtdVol"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaEspecVol", SqlDbType.VarChar, 20, "vPedVendaEspecVol"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaEntNomeDiv", SqlDbType.VarChar, 100, "vPedVendaEntNomeDiv"));
                    dbCommand.Parameters.Add(new SqlParameter("@vTipoLancCod", SqlDbType.VarChar, 10, "vTipoLancCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUsucod", SqlDbType.VarChar, 31, "vUsucod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUserObsPedido", SqlDbType.VarChar, 255, "vUserObsPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@vObsPedido", SqlDbType.VarChar, 10000, "vObsPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@vObsHistoricoPedido", SqlDbType.VarChar, 10000, "vObsHistoricoPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@nPercPIS", SqlDbType.Decimal, 0, "nPercPIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@nPercCOFINS", SqlDbType.Decimal, 0, "nPercCOFINS"));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfVendaStatFrete", SqlDbType.VarChar, 15, "vConfVendaStatFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUserPedRepCliente", SqlDbType.VarChar, 15, "vUserPedRepCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaTranspEntCod", SqlDbType.VarChar, 7, "vPedVendaTranspEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vBonif", SqlDbType.VarChar, 50, "vBonif"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntNat", SqlDbType.VarChar, 25, "vEntNat"));
                    dbCommand.Parameters.Add(new SqlParameter("@vTabPVCod", SqlDbType.VarChar, 15, "vTabPVCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUSERnotafiscalorigem", SqlDbType.VarChar, 10, "vUSERnotafiscalorigem"));
                    dbCommand.Parameters.Add(new SqlParameter("@nUSERCredBonif", SqlDbType.Decimal, 0, "nUSERCredBonif"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUSERtipoverbas", SqlDbType.VarChar, 20, "vUSERtipoverbas"));
                    dbCommand.Parameters.Add(new SqlParameter("@nUSERVerbasEventuais", SqlDbType.Decimal, 0, "nUSERVerbasEventuais"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaOperacao", SqlDbType.VarChar, 15, "vPedVendaOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaEspecie", SqlDbType.VarChar, 20, "vPedVendaEspecie"));
                    dbCommand.Parameters.Add(new SqlParameter("@vOperacao", SqlDbType.VarChar, 30, "vOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@nPedVendaPesoLiq", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 24, 9, "nPedVendaPesoLiq", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nPedVendaPesoBruto", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 24, 9, "nPedVendaPesoBruto", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTituloEmail", SqlDbType.VarChar, 300, ParameterDirection.Output, false, 0, 0, "vTituloEmail", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNumPedEnt", SqlDbType.VarChar, 40, "vPedVendaNumPedEnt"));

                    dbCommand.Parameters["@vEmpCod"].Value = empCod;
                    dbCommand.Parameters["@vPedVendaNum"].Value = PedVendaNum.ToString();
                    //dbCommand.Parameters["@vPedVendaTipo"].Value = drpTipo.SelectedItem.Value;
                    dbCommand.Parameters["@vPedVendaTipo"].Value = TipoPedVenda;
                    dbCommand.Parameters["@dDataEmissao"].Value = this.FormataData(DataEmissao);
                    //dbCommand.Parameters["@dDataEntrega"].Value = mdlfuncsFit.FormataData(txtDataEntrega.Text);
                    dbCommand.Parameters["@dDataEntrega"].Value = this.FormataData(dataEntrega.ToString());
                    dbCommand.Parameters["@vEntCod"].Value = codEntidade;
                    dbCommand.Parameters["@vTipoVendaCod"].Value = tipVendaCod;
                    dbCommand.Parameters["@vPedVendaNatOpProd"].Value = ""; //Será recupera na outra procedure
                    dbCommand.Parameters["@nValorTotal"].Value = 0; //Será calculado na outra procedure
                    dbCommand.Parameters["@vRegCodEstr"].Value = regCodEstr;
                    dbCommand.Parameters["@vStatPedVendaCod"].Value = codStatus; //Colocado inicialmente como análise crédito
                    dbCommand.Parameters["@vPedVendaStatDescr"].Value = descricaoStatus; //Colocado inicialmente como análise crédito
                    dbCommand.Parameters["@nPedVendaQtdVol"].Value = QuantidadeVolumes; //Será Atualizado Finaliza PedVenda
                    dbCommand.Parameters["@vPedVendaEspecVol"].Value = EspecieVolume; //Gravado com branco
                    dbCommand.Parameters["@vPedVendaEntNomeDiv"].Value = entNome;
                    dbCommand.Parameters["@vTipoLancCod"].Value = ""; //Será recuperado na procedure finaliza
                    dbCommand.Parameters["@vUsucod"].Value = usuario.ToString();
                    dbCommand.Parameters["@vUserObsPedido"].Value = ""; //Gravado em branco
                    dbCommand.Parameters["@vObsPedido"].Value = observacao;
                    dbCommand.Parameters["@vObsHistoricoPedido"].Value = historico;
                    dbCommand.Parameters["@nPercPIS"].Value = 1; //Valor é recalculado
                    dbCommand.Parameters["@nPercCOFINS"].Value = 1; //Valor é recalculado
                    dbCommand.Parameters["@vConfVendaStatFrete"].Value = TipoFrete;
                    dbCommand.Parameters["@vUserPedRepCliente"].Value = ""; //Gravado em Branco
                    dbCommand.Parameters["@vPedVendaTranspEntCod"].Value = transportadora;
                    dbCommand.Parameters["@vBonif"].Value = "";
                    dbCommand.Parameters["@vEntNat"].Value = natureza;
                    dbCommand.Parameters["@vTabPVCod"].Value = tabela;
                    dbCommand.Parameters["@vUSERnotafiscalorigem"].Value = "0";
                    dbCommand.Parameters["@nUSERCredBonif"].Value = "0";
                    dbCommand.Parameters["@vUSERtipoverbas"].Value = "0";
                    dbCommand.Parameters["@nUSERVerbasEventuais"].Value = "0";
                    dbCommand.Parameters["@vPedVendaOperacao"].Value = operacao;
                    dbCommand.Parameters["@vPedVendaEspecie"].Value = especie;
                    dbCommand.Parameters["@vOperacao"].Value = tipoOperacao;
                    dbCommand.Parameters["@nPedVendaPesoLiq"].Value = PesoLiquido;
                    dbCommand.Parameters["@nPedVendaPesoBruto"].Value = PesoBruto;
                    dbCommand.Parameters["@vPedVendaNumPedEnt"].Value = PedVendaNumPedEnt;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    strError = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch
                {
                    strError = "Erro na inserção do pedido";
                }
            }
            return strError;
        }

        public string[] gravaBancoItemPedido(string empCod, string PedVendaNum, string vendedor, string entidade,
            string natureza, string revenda, string codProduto, string empPais, string empUf, string entPais, string entUf,
            string condicao, string unidade, string operacao, string especie, int numseq, double quantidade, float valorItem,
            string codigoTabela, float valorFrete, string nomeProduto, string dataPedido, string tipoOperacao,
            string PedVendaNumPedEnt, int ItPedVendaNumSeq, string IPIInclusoICMS)
        {

            string strError = "";
            string[] retDados = new string[3];
            string clasRecDesp = "";
            int UnidMedPos = 0;

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                
                SqlCommand dbCommand = new SqlCommand("USER_WebVendas_REGRAS_NEGOCIO", dbConnection);
                try
                {                    
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@vEmpCod", SqlDbType.VarChar, 20, "vEmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNum", SqlDbType.VarChar, 7, "vPedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@vBonif", SqlDbType.VarChar, 50, "vBonif"));
                    dbCommand.Parameters.Add(new SqlParameter("@vCodRep", SqlDbType.VarChar, 7, "vCodRep"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntCod", SqlDbType.VarChar, 7, "vEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntNat", SqlDbType.VarChar, 25, "vEntNat"));
                    dbCommand.Parameters.Add(new SqlParameter("@vRevenda", SqlDbType.VarChar, 3, "vRevenda"));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdCodEstr", SqlDbType.VarChar, 30, "vProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEmpOptSimplesNac", SqlDbType.VarChar, 5, "vEmpOptSimplesNac"));
                    dbCommand.Parameters.Add(new SqlParameter("@dEmpDataAdesaoSimplesNac", SqlDbType.DateTime, 0, "dEmpDataAdesaoSimplesNac"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasAuxPaisSiglaOrig", SqlDbType.VarChar, 3, "vClasAuxPaisSiglaOrig"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasAuxPaisSiglaDest", SqlDbType.VarChar, 3, "vClasAuxPaisSiglaDest"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasAuxUfSiglaOrig", SqlDbType.VarChar, 2, "vClasAuxUfSiglaOrig"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasAuxUfSiglaDest", SqlDbType.VarChar, 2, "vClasAuxUfSiglaDest"));
                    dbCommand.Parameters.Add(new SqlParameter("@vTipoDescontoAplicado", SqlDbType.VarChar, 30, "vTipoDescontoAplicado"));
                    dbCommand.Parameters.Add(new SqlParameter("@nValorDescontoAplicado", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 2, "nValorDescontoAplicado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaOperacao", SqlDbType.VarChar, 15, "vPedVendaOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaEspecie", SqlDbType.VarChar, 20, "vPedVendaEspecie"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUsuCod", SqlDbType.VarChar, 31, ParameterDirection.Output, false, 0, 0, "vUsuCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTipoLancCod", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vTipoLancCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vStatPedVendaCod", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "vStatPedVendaCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaStatDescr", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "vPedVendaStatDescr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vCondPagCod", SqlDbType.VarChar, 7, "vCondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vCondPagNome", SqlDbType.VarChar, 80, ParameterDirection.Output, false, 0, 0, "vCondPagNome", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@bChecagem_ST", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "bChecagem_ST", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vNatOpCodEstr", SqlDbType.VarChar, 15, ParameterDirection.Output, false, 0, 0, "vNatOpCodEstr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nClasFiscPisPerc", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 3, "nClasFiscPisPerc", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iPisTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iPisTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nClasFiscCofinsPerc", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 3, "nClasFiscCofinsPerc", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iCofinsTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iCofinsTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nClasFiscIpiPerc", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 3, "nClasFiscIpiPerc", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iIpiTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iIpiTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTipoVendaCod", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "vTipoVendaCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vLocArmazCodEstr", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vLocArmazCodEstr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iIcmsTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iIcmsTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iIcmsStTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iIcmsStTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iIcmsRedTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iIcmsRedTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasFiscCod", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vClasFiscCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nReducaoICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "nReducaoICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nItPedVendaMargLucroST", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "nItPedVendaMargLucroST", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nAliquotaICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "nAliquotaICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nIndiceSubstTributaria", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "nIndiceSubstTributaria", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vRedIcmsSobre", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vRedIcmsSobre", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nPorcentoIcms", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "nPorcentoIcms", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nProdPesoLiq", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "nProdPesoLiq", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nProdPesoBruto", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "nProdPesoBruto", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNome", SqlDbType.VarChar, 80, ParameterDirection.Output, false, 0, 0, "vProdNome", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@cTribACod", SqlDbType.VarChar, 1, ParameterDirection.Output, false, 0, 0, "cTribACod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTribBCod", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vTribBCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vItPedVendaUnidMedCod", SqlDbType.VarChar, 7, "vItPedVendaUnidMedCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@sItPedVendaUnidMedPos", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "sItPedVendaUnidMedPos", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nReducaoICMSBaseCalculo", SqlDbType.Decimal, 10, ParameterDirection.Output, false, 2, 0, "nReducaoICMSBaseCalculo", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nReducaoICMSBaseValorImposto", SqlDbType.Decimal, 10, ParameterDirection.Output, false, 2, 0, "nReducaoICMSBaseValorImposto", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@cDescontoICMS_Gov", SqlDbType.Char, 3, ParameterDirection.Output, false, 0, 0, "cDescontoICMS_Gov", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNomeAlt1", SqlDbType.VarChar, 255, ParameterDirection.Output, false, 0, 0, "vProdNomeAlt1", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNomeAlt2", SqlDbType.VarChar, 255, ParameterDirection.Output, false, 0, 0, "vProdNomeAlt2", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNomeAlt3", SqlDbType.VarChar, 255, ParameterDirection.Output, false, 0, 0, "vProdNomeAlt3", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vUnidMedNome", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vUnidMedNome", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasseRecDespCodEstr", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "vClasseRecDespCodEstr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTribBModBCCod", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vTribBModBCCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTribBModBCSTCod", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vTribBModBCSTCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribCodSaidaIPI", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vConfTribCodSaidaIPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribTipoSaidaIPI", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vConfTribTipoSaidaIPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribCodPIS", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vConfTribCodPIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribTipoPIS", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vConfTribTipoPIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribCodCOFINS", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vConfTribCodCOFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribTipoCOFINS", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vConfTribTipoCOFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vRegCodEstr", SqlDbType.VarChar, 15, ParameterDirection.Output, false, 0, 0, "vRegCodEstr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nUSERCredBonif", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "nUSERCredBonif", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vUSERtipoverbas", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vUSERtipoverbas", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nUSERVerbasEventuais", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "nUSERVerbasEventuais", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nEntCpfCgc", SqlDbType.VarChar, 14, ParameterDirection.Output, false, 0, 0, "nEntCpfCgc", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTituloEmail", SqlDbType.VarChar, 300, ParameterDirection.Output, false, 0, 0, "vTituloEmail", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 3000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@dataPedido", SqlDbType.DateTime, 10, "dataPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@vtipoOperacao", SqlDbType.VarChar, 10, "vtipoOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vNatOpConfTribIpiCod", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vNatOpConfTribIpiCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vNatOpConfTribCofinsCod", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vNatOpConfTribCofinsCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vNatOpConfTribPisCod", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vNatOpConfTribPisCod", DataRowVersion.Default, null));

                    dbCommand.Parameters["@vEmpCod"].Value = empCod;
                    dbCommand.Parameters["@vPedVendaNum"].Value = PedVendaNum.ToString();
                    dbCommand.Parameters["@vBonif"].Value = "";
                    dbCommand.Parameters["@vCodRep"].Value = vendedor.ToString();
                    dbCommand.Parameters["@vEntCod"].Value = entidade.ToString();
                    dbCommand.Parameters["@vEntNat"].Value = natureza.ToString();

                    if (revenda == "0")
                        dbCommand.Parameters["@vRevenda"].Value = "Não";
                    else
                        dbCommand.Parameters["@vRevenda"].Value = "Sim";

                    dbCommand.Parameters["@vProdCodEstr"].Value = codProduto;
                    dbCommand.Parameters["@vEmpOptSimplesNac"].Value = "0";
                    dbCommand.Parameters["@dEmpDataAdesaoSimplesNac"].Value = "1900-01-01 00:00:00.000";//Setado com Zero
                    dbCommand.Parameters["@vClasAuxPaisSiglaOrig"].Value = empPais.ToString();
                    dbCommand.Parameters["@vClasAuxPaisSiglaDest"].Value = entPais.ToString();
                    dbCommand.Parameters["@vClasAuxUfSiglaOrig"].Value = empUf.ToString();
                    dbCommand.Parameters["@vClasAuxUfSiglaDest"].Value = entUf.ToString();
                    dbCommand.Parameters["@vClasAuxUfSiglaDest"].Value = entUf.ToString();
                    dbCommand.Parameters["@nValorDescontoAplicado"].Value = 0;
                    dbCommand.Parameters["@vTipoDescontoAplicado"].Value = "0";
                    dbCommand.Parameters["@vCondPagCod"].Value = condicao;
                    dbCommand.Parameters["@vItPedVendaUnidMedCod"].Value = unidade;
                    dbCommand.Parameters["@vPedVendaOperacao"].Value = operacao;
                    dbCommand.Parameters["@vPedVendaEspecie"].Value = especie;
                    dbCommand.Parameters["@vtipoOperacao"].Value = tipoOperacao.ToString();
                    dbCommand.Parameters["@dataPedido"].Value = this.FormataData(dataPedido);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    strError = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch
                {
                    strError = "Erro na inclusao do item";
                }


                if (strError == "")
                {
                    try
                    {
                        SqlCommand dbCommandAux = new SqlCommand("USER_WebVENDAS_ITEM_PED_VENDA", dbConnection);

                        dbCommandAux.CommandType = CommandType.StoredProcedure;

                        dbCommandAux.Parameters.Add(new SqlParameter("@vEmpCod", SqlDbType.VarChar, 20, "vEmpCod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vUsucod", SqlDbType.VarChar, 31, "vUsucod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vPedVendaNum", SqlDbType.VarChar, 10, "vPedVendaNum"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@sItPedVendaSeq", SqlDbType.SmallInt, 0, "sItPedVendaSeq"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vProdCodEstr", SqlDbType.VarChar, 30, "vProdCodEstr"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nItPedVendaQtd", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 24, 9, "nItPedVendaQtd", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nValor", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 2, "nValor", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@iIpiTributado", SqlDbType.Int, 0, "iIpiTributado"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nItPedVendaPercIpi", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 2, "nItPedVendaPercIpi", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nIpi_ST", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 3, "nIpi_ST", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vEntNat", SqlDbType.VarChar, 25, "vEntNat"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@iChecagem_ST", SqlDbType.Int, 0, "iChecagem_ST"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nNatOpCodEstr", SqlDbType.VarChar, 15, "nNatOpCodEstr"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@iPisTributado", SqlDbType.Int, 0, "iPisTributado"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nClasFiscPisPerc", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 3, "nClasFiscPisPerc", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@iCofinsTributado", SqlDbType.Int, 0, "iCofinsTributado"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nClasFiscCofinsPerc", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 3, "nClasFiscCofinsPerc", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vStatPedVendaCod", SqlDbType.VarChar, 7, "vStatPedVendaCod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vPedVendaStatDescr", SqlDbType.VarChar, 30, "vPedVendaStatDescr"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nRepresentante", SqlDbType.VarChar, 7, "nRepresentante"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vLocArmazCodEstr", SqlDbType.VarChar, 20, "vLocArmazCodEstr"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vTabPVCod", SqlDbType.VarChar, 15, "vTabPVCod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@iIcmsTributado", SqlDbType.Int, 0, "iIcmsTributado"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@iIcmsStTributado", SqlDbType.Int, 0, "iIcmsStTributado"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@iIcmsRedTributado", SqlDbType.Int, 0, "iIcmsRedTributado"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vClasFiscCod", SqlDbType.VarChar, 10, "vClasFiscCod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nReducaoICMSBaseCalculo", SqlDbType.Int, 0, "nReducaoICMSBaseCalculo"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nReducaoICMSBaseValorImposto", SqlDbType.Int, 0, "nReducaoICMSBaseValorImposto"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vClasAuxPaisSiglaOrig", SqlDbType.VarChar, 3, "vClasAuxPaisSiglaOrig"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vClasAuxPaisSiglaDest", SqlDbType.VarChar, 3, "vClasAuxPaisSiglaDest"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vClasAuxUfSiglaOrig", SqlDbType.VarChar, 2, "vClasAuxUfSiglaOrig"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vClasAuxUfSiglaDest", SqlDbType.VarChar, 2, "vClasAuxUfSiglaDest"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nPorcentoIcms", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 2, "nPorcentoIcms", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nReducaoICMS", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 4, "nReducaoICMS", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vItPedVendaUnidMedCod", SqlDbType.VarChar, 7, "vItPedVendaUnidMedCod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@sItPedVendaUnidMedPos", SqlDbType.Int, 0, "sItPedVendaUnidMedPos"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nProdPesoLiq", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 24, 9, "nProdPesoLiq", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nProdPesoBruto", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 24, 9, "nProdPesoBruto", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vProdNome", SqlDbType.VarChar, 80, "vProdNome"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@cTribACod", SqlDbType.Char, 1, "cTribACod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vTribBCod", SqlDbType.VarChar, 2, "vTribBCod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nItPedVendaMargLucroST", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 4, "nItPedVendaMargLucroST", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nAliquotaICMS", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 4, "nAliquotaICMS", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nIndiceSubstTributaria", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 4, "nIndiceSubstTributaria", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vProdNomeAlt1", SqlDbType.VarChar, 255, "vProdNomeAlt1"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vProdNomeAlt2", SqlDbType.VarChar, 255, "vProdNomeAlt2"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vProdNomeAlt3", SqlDbType.VarChar, 255, "vProdNomeAlt3"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vBonif", SqlDbType.VarChar, 50, "vBonif"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vRedIcmsSobre", SqlDbType.VarChar, 20, "vRedIcmsSobre"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vTribBModBCCod", SqlDbType.VarChar, 2, "vTribBModBCCod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vTribBModBCSTCod", SqlDbType.VarChar, 2, "vTribBModBCSTCod"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vConfTribCodSaidaIPI", SqlDbType.VarChar, 2, "vConfTribCodSaidaIPI"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vConfTribTipoSaidaIPI", SqlDbType.VarChar, 20, "vConfTribTipoSaidaIPI"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vConfTribCodPIS", SqlDbType.VarChar, 2, "vConfTribCodPIS"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vConfTribTipoPIS", SqlDbType.VarChar, 10, "vConfTribTipoPIS"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vConfTribCodCOFINS", SqlDbType.VarChar, 2, "vConfTribCodCOFINS"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vConfTribTipoCOFINS", SqlDbType.VarChar, 10, "vConfTribTipoCOFINS"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vClasseRecDespCodEstr", SqlDbType.VarChar, 30, "vClasseRecDespCodEstr"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@nEntCpfCgc", SqlDbType.VarChar, 14, "nEntCpfCgc"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@valorFrete", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 4, "valorFrete", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vPedVendaOperacao", SqlDbType.VarChar, 10, "vPedVendaOperacao"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vTituloEmail", SqlDbType.VarChar, 300, ParameterDirection.Output, false, 0, 0, "vTituloEmail", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 3000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vtipoOperacao", SqlDbType.VarChar, 10, "vtipoOperacao"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vItPedVendaNumPedEnt", SqlDbType.VarChar, 40, "vItPedVendaNumPedEnt"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@vItPedVendaNumSeq", SqlDbType.Int, 0, "ItPedVendaNumSeq"));
                        dbCommandAux.Parameters.Add(new SqlParameter("@ItPedVendaFPIPIBaseICMS", SqlDbType.VarChar, 10, "ItPedVendaFPIPIBaseICMS"));

                        dbCommandAux.Parameters["@vEmpCod"].Value = empCod;
                        dbCommandAux.Parameters["@vUsucod"].Value = (string)dbCommand.Parameters["@vUsuCod"].Value;
                        dbCommandAux.Parameters["@vPedVendaNum"].Value = PedVendaNum.ToString();
                        dbCommandAux.Parameters["@sItPedVendaSeq"].Value = numseq.ToString();
                        dbCommandAux.Parameters["@vProdCodEstr"].Value = codProduto.ToString();
                        dbCommandAux.Parameters["@nItPedVendaQtd"].Value = quantidade.ToString();
                        dbCommandAux.Parameters["@nValor"].Value = Convert.ToDecimal(valorItem);
                        dbCommandAux.Parameters["@iIpiTributado"].Value = (int)dbCommand.Parameters["@iIpiTributado"].Value;
                        dbCommandAux.Parameters["@nItPedVendaPercIpi"].Value = (Decimal)dbCommand.Parameters["@nClasFiscIpiPerc"].Value;
                        dbCommandAux.Parameters["@nIpi_ST"].Value = (int)dbCommand.Parameters["@bChecagem_ST"].Value;
                        dbCommandAux.Parameters["@vEntNat"].Value = natureza.ToString();
                        dbCommandAux.Parameters["@iChecagem_ST"].Value = (int)dbCommand.Parameters["@bChecagem_ST"].Value;
                        dbCommandAux.Parameters["@nNatOpCodEstr"].Value = (string)dbCommand.Parameters["@vNatOpCodEstr"].Value;
                        dbCommandAux.Parameters["@iPisTributado"].Value = (int)dbCommand.Parameters["@iPisTributado"].Value;
                        dbCommandAux.Parameters["@nClasFiscPisPerc"].Value = (Decimal)dbCommand.Parameters["@nClasFiscPisPerc"].Value;
                        dbCommandAux.Parameters["@iCofinsTributado"].Value = (int)dbCommand.Parameters["@iCofinsTributado"].Value;
                        dbCommandAux.Parameters["@nClasFiscCofinsPerc"].Value = (Decimal)dbCommand.Parameters["@nClasFiscCofinsPerc"].Value;
                        dbCommandAux.Parameters["@vStatPedVendaCod"].Value = (string)dbCommand.Parameters["@vStatPedVendaCod"].Value;
                        dbCommandAux.Parameters["@vPedVendaStatDescr"].Value = (string)dbCommand.Parameters["@vPedVendaStatDescr"].Value;
                        dbCommandAux.Parameters["@nRepresentante"].Value = vendedor.ToString();
                        dbCommandAux.Parameters["@vLocArmazCodEstr"].Value = (string)dbCommand.Parameters["@vLocArmazCodEstr"].Value;
                        dbCommandAux.Parameters["@vTabPVCod"].Value = codigoTabela.ToString();
                        dbCommandAux.Parameters["@iIcmsTributado"].Value = (int)dbCommand.Parameters["@iIcmsTributado"].Value;
                        dbCommandAux.Parameters["@iIcmsStTributado"].Value = (int)dbCommand.Parameters["@iIcmsStTributado"].Value;
                        dbCommandAux.Parameters["@iIcmsRedTributado"].Value = (int)dbCommand.Parameters["@iIcmsRedTributado"].Value;

                        if ((string)dbCommand.Parameters["@vNatOpCodEstr"].Value == "5.101.011" || (string)dbCommand.Parameters["@vNatOpCodEstr"].Value == "5.101.031")
                        {
                            dbCommandAux.Parameters["@vClasFiscCod"].Value = "0000258";
                        }
                        else
                        {
                            dbCommandAux.Parameters["@vClasFiscCod"].Value = (string)dbCommand.Parameters["@vClasFiscCod"].Value;
                        }
                        dbCommandAux.Parameters["@nReducaoICMSBaseCalculo"].Value = (Decimal)dbCommand.Parameters["@nReducaoICMSBaseCalculo"].Value;
                        dbCommandAux.Parameters["@nReducaoICMSBaseValorImposto"].Value = (Decimal)dbCommand.Parameters["@nReducaoICMSBaseValorImposto"].Value;
                        dbCommandAux.Parameters["@vClasAuxPaisSiglaOrig"].Value = empPais.ToString();
                        dbCommandAux.Parameters["@vClasAuxPaisSiglaDest"].Value = entPais.ToString();
                        dbCommandAux.Parameters["@vClasAuxUfSiglaOrig"].Value = empUf.ToString();
                        dbCommandAux.Parameters["@vClasAuxUfSiglaDest"].Value = entUf.ToString();
                        dbCommandAux.Parameters["@nPorcentoIcms"].Value = (Decimal)dbCommand.Parameters["@nPorcentoIcms"].Value;
                        dbCommandAux.Parameters["@nReducaoICMS"].Value = (Decimal)dbCommand.Parameters["@nReducaoICMS"].Value;
                        dbCommandAux.Parameters["@vItPedVendaUnidMedCod"].Value = unidade.ToString();
                        dbCommandAux.Parameters["@sItPedVendaUnidMedPos"].Value = (int)dbCommand.Parameters["@sItPedVendaUnidMedPos"].Value;
                        UnidMedPos = (int)dbCommand.Parameters["@sItPedVendaUnidMedPos"].Value;
                        dbCommandAux.Parameters["@nProdPesoLiq"].Value = (Decimal)dbCommand.Parameters["@nProdPesoLiq"].Value;
                        dbCommandAux.Parameters["@nProdPesoBruto"].Value = (Decimal)dbCommand.Parameters["@nProdPesoBruto"].Value;
                        //dbCommandAux.Parameters["@vProdNome"].Value = (string)dbCommand.Parameters["@vProdNome"].Value;
                        dbCommandAux.Parameters["@vProdNome"].Value = nomeProduto.ToString();
                        dbCommandAux.Parameters["@cTribACod"].Value = (string)dbCommand.Parameters["@cTribACod"].Value;
                        dbCommandAux.Parameters["@vTribBCod"].Value = (string)dbCommand.Parameters["@vTribBCod"].Value;
                        dbCommandAux.Parameters["@nItPedVendaMargLucroST"].Value = (Decimal)dbCommand.Parameters["@nItPedVendaMargLucroST"].Value;



                        if (dbCommand.Parameters["@nAliquotaICMS"].Value.Equals(System.DBNull.Value) != true)
                        {
                            dbCommandAux.Parameters["@nAliquotaICMS"].Value = (Decimal)dbCommand.Parameters["@nAliquotaICMS"].Value;
                        }
                        else
                        {
                            dbCommandAux.Parameters["@nAliquotaICMS"].Value = 0;
                        }


                        dbCommandAux.Parameters["@nIndiceSubstTributaria"].Value = (Decimal)dbCommand.Parameters["@nIndiceSubstTributaria"].Value;

                        dbCommandAux.Parameters["@vProdNomeAlt1"].Value = (string)dbCommand.Parameters["@vProdNomeAlt1"].Value;
                        dbCommandAux.Parameters["@vProdNomeAlt2"].Value = (string)dbCommand.Parameters["@vProdNomeAlt2"].Value;
                        dbCommandAux.Parameters["@vProdNomeAlt3"].Value = (string)dbCommand.Parameters["@vProdNomeAlt3"].Value;
                        dbCommandAux.Parameters["@vBonif"].Value = "";
                        dbCommandAux.Parameters["@vRedIcmsSobre"].Value = (string)dbCommand.Parameters["@vRedIcmsSobre"].Value;

                        if (dbCommand.Parameters["@vTribBModBCCod"].Value.Equals(System.DBNull.Value) != true)
                        {
                            dbCommandAux.Parameters["@vTribBModBCCod"].Value = (string)dbCommand.Parameters["@vTribBModBCCod"].Value;
                        }
                        else
                        {
                            dbCommandAux.Parameters["@vTribBModBCCod"].Value = System.DBNull.Value;
                        }
                        //dbCommandAux.Parameters["@vTribBModBCCod"].Value = (string)dbCommand.Parameters["@vTribBModBCCod"].Value;
                        dbCommandAux.Parameters["@vTribBModBCSTCod"].Value = (string)dbCommand.Parameters["@vTribBModBCSTCod"].Value;
                        dbCommandAux.Parameters["@vConfTribCodSaidaIPI"].Value = (string)dbCommand.Parameters["@vConfTribCodSaidaIPI"].Value;
                        dbCommandAux.Parameters["@vConfTribTipoSaidaIPI"].Value = (string)dbCommand.Parameters["@vConfTribTipoSaidaIPI"].Value;
                        dbCommandAux.Parameters["@vConfTribCodPIS"].Value = (string)dbCommand.Parameters["@vConfTribCodPIS"].Value;
                        dbCommandAux.Parameters["@vConfTribTipoPIS"].Value = (string)dbCommand.Parameters["@vConfTribTipoPIS"].Value;
                        dbCommandAux.Parameters["@vConfTribCodCOFINS"].Value = (string)dbCommand.Parameters["@vConfTribCodCOFINS"].Value;
                        dbCommandAux.Parameters["@vConfTribTipoCOFINS"].Value = (string)dbCommand.Parameters["@vConfTribTipoCOFINS"].Value;
                        dbCommandAux.Parameters["@valorFrete"].Value = valorFrete;

                        if (dbCommand.Parameters["@vClasseRecDespCodEstr"].Value.Equals(System.DBNull.Value) != true)
                        {
                            clasRecDesp = (string)dbCommand.Parameters["@vClasseRecDespCodEstr"].Value;
                            dbCommandAux.Parameters["@vClasseRecDespCodEstr"].Value = (string)dbCommand.Parameters["@vClasseRecDespCodEstr"].Value;
                        }
                        else
                        {
                            clasRecDesp = "";
                            dbCommandAux.Parameters["@vClasseRecDespCodEstr"].Value = "";
                        }

                        dbCommandAux.Parameters["@nEntCpfCgc"].Value = (string)dbCommand.Parameters["@nEntCpfCgc"].Value;
                        dbCommandAux.Parameters["@vPedVendaOperacao"].Value = operacao;
                        dbCommandAux.Parameters["@vtipoOperacao"].Value = tipoOperacao.ToString();
                        dbCommandAux.Parameters["@vItPedVendaNumPedEnt"].Value = PedVendaNumPedEnt.ToString();
                        dbCommandAux.Parameters["@vItPedVendaNumSeq"].Value = ItPedVendaNumSeq;
                        dbCommandAux.Parameters["@ItPedVendaFPIPIBaseICMS"].Value = IPIInclusoICMS;

                        dbCommandAux.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommandAux.ExecuteNonQuery();

                        //strError = (string)dbCommand.Parameters["@vTituloEmail"].Value;
                        strError = (string)dbCommandAux.Parameters["@vErro"].Value;
                    }
                    catch
                    {
                        strError = "Erro na inclusao do item";
                    }
                }
            }

            retDados[0] = strError;
            retDados[1] = clasRecDesp;
            retDados[2] = (string)Convert.ToString(UnidMedPos);

            return retDados;
        }

        public string finalizaPedido(string empCod, string PedVendaNum, string condicao, string nomeCondicao, string vendedor,
            string DataEmissao, string usuario, string entidade, string clasRecDesp, string operacao, string tipoOperacao)
        {

            string retErro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("USER_WebVENDAS_FINAL_PEDIDO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@vEmpCod", SqlDbType.VarChar, 20, "vEmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNum", SqlDbType.VarChar, 12, "vPedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasFiscCod", SqlDbType.VarChar, 10, "vClasFiscCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vBonif", SqlDbType.VarChar, 50, "vBonif"));
                    dbCommand.Parameters.Add(new SqlParameter("@vCondPag", SqlDbType.VarChar, 7, "vCondPag"));
                    dbCommand.Parameters.Add(new SqlParameter("@vCondPagNome", SqlDbType.VarChar, 80, "vCondPagNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@vCodRep", SqlDbType.VarChar, 7, "vCodRep"));
                    dbCommand.Parameters.Add(new SqlParameter("@dCondPagPedVendaDataBaseVenc", SqlDbType.DateTime, 0, "dCondPagPedVendaDataBaseVenc"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUsuCod", SqlDbType.VarChar, 31, "vUsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntCod", SqlDbType.VarChar, 7, "vEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEMailComercial", SqlDbType.VarChar, 50, "vEMailComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEMailContabil", SqlDbType.VarChar, 50, "vEMailContabil"));
                    dbCommand.Parameters.Add(new SqlParameter("@cDescontoICMS_Gov", SqlDbType.Char, 3, "cDescontoICMS_Gov"));
                    dbCommand.Parameters.Add(new SqlParameter("@vStatPedVendaCod", SqlDbType.VarChar, 7, "vStatPedVendaCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasseRecDespCodEstr", SqlDbType.VarChar, 30, "vClasseRecDespCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@nPorcentoIcms", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 2, "nPorcentoIcms", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaOperacao", SqlDbType.VarChar, 10, "vPedVendaOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vtipoOperacao", SqlDbType.VarChar, 10, "vtipoOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vTituloEmail", SqlDbType.VarChar, 300, ParameterDirection.Output, false, 0, 0, "vTituloEmail", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@vEmpCod"].Value = empCod;
                    dbCommand.Parameters["@vPedVendaNum"].Value = PedVendaNum.ToString();
                    dbCommand.Parameters["@vClasFiscCod"].Value = "";
                    dbCommand.Parameters["@vBonif"].Value = "";
                    dbCommand.Parameters["@vCondPag"].Value = condicao.ToString();
                    dbCommand.Parameters["@vCondPagNome"].Value = nomeCondicao.ToString();
                    dbCommand.Parameters["@vCodRep"].Value = vendedor.ToString();
                    dbCommand.Parameters["@dCondPagPedVendaDataBaseVenc"].Value = this.FormataData(DataEmissao);
                    dbCommand.Parameters["@vUsuCod"].Value = usuario.ToString();
                    dbCommand.Parameters["@vEntCod"].Value = entidade.ToString();
                    dbCommand.Parameters["@vEMailComercial"].Value = "";
                    dbCommand.Parameters["@vEMailContabil"].Value = "";
                    dbCommand.Parameters["@cDescontoICMS_Gov"].Value = 0;
                    dbCommand.Parameters["@vStatPedVendaCod"].Value = "1";//Sempre entra como análise de crédito 
                    dbCommand.Parameters["@vClasseRecDespCodEstr"].Value = clasRecDesp.ToString();
                    dbCommand.Parameters["@nPorcentoIcms"].Value = 0;
                    dbCommand.Parameters["@vPedVendaOperacao"].Value = operacao;
                    dbCommand.Parameters["@vtipoOperacao"].Value = tipoOperacao.ToString();

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
                    dbCommand.ExecuteNonQuery();

                    retErro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch
                {
                    retErro = "Erro na finalizacao do pedido";
                }
            }

            return retErro;
        }

        public string gravaComposicaoPedido(string empresa, string PedVendaNum, string usuario, int cont, string codigoProdutoPrincipal,
            string codigoProduto, string codigoAlternativo, string unidade, double quantidade, int unidadePos, string tipoOperacao)
        {
            string retErro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("USER_WebVendas_COMP_ITEM_PED_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@vEmpCod", SqlDbType.VarChar, 20, "vEmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUsuCod", SqlDbType.VarChar, 31, "vUsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNum", SqlDbType.VarChar, 7, "vPedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@sItPedVendaSeq", SqlDbType.SmallInt, 0, "sItPedVendaSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdCodEstr", SqlDbType.VarChar, 30, "vProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@vCompItPedVendaProdCodEstrIt", SqlDbType.VarChar, 30, "vCompItPedVendaProdCodEstrIt"));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNome", SqlDbType.VarChar, 80, "vProdNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@nItPedVendaQtd", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 24, 9, "nItPedVendaQtd", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vItPedVendaUnidMedCod", SqlDbType.VarChar, 7, "vItPedVendaUnidMedCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@sItPedVendaUnidMedPos", SqlDbType.Int, 0, "sItPedVendaUnidMedPos"));
                    dbCommand.Parameters.Add(new SqlParameter("@vLocArmazCodEstr", SqlDbType.VarChar, 20, "vLocArmazCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@vTituloEmail", SqlDbType.VarChar, 300, ParameterDirection.Output, false, 0, 0, "vTituloEmail", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vtipoOperacao", SqlDbType.VarChar, 10, "vtipoOperacao"));

                    dbCommand.Parameters["@vEmpCod"].Value = empresa;
                    dbCommand.Parameters["@vUsuCod"].Value = usuario;
                    dbCommand.Parameters["@vPedVendaNum"].Value = PedVendaNum.ToString();
                    dbCommand.Parameters["@sItPedVendaSeq"].Value = cont.ToString();
                    dbCommand.Parameters["@vProdCodEstr"].Value = codigoProdutoPrincipal;
                    dbCommand.Parameters["@vCompItPedVendaProdCodEstrIt"].Value = codigoProduto;
                    dbCommand.Parameters["@vProdNome"].Value = codigoAlternativo;
                    dbCommand.Parameters["@nItPedVendaQtd"].Value = quantidade;
                    dbCommand.Parameters["@vItPedVendaUnidMedCod"].Value = unidade;
                    dbCommand.Parameters["@sItPedVendaUnidMedPos"].Value = unidadePos;
                    dbCommand.Parameters["@vLocArmazCodEstr"].Value = "01.01.03";
                    dbCommand.Parameters["@vtipoOperacao"].Value = tipoOperacao.ToString();

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
                    dbCommand.ExecuteNonQuery();

                    //strError = (string)dbCommand.Parameters["@vTituloEmail"].Value;
                    retErro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch
                {
                    retErro = "Erro na composição do item do pedido";
                }
            }

            return retErro;
        }

        public string aprovaPedido(string Empresa, string pedido, string usucod, string entcod)
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("USER_SP_AprovaPedido", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@empcod", SqlDbType.VarChar, 15, "empcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pedido", SqlDbType.VarChar, 10, "Pedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUsuCod", SqlDbType.VarChar, 31, "vUsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntCod", SqlDbType.VarChar, 7, "vEntCod"));

                    dbCommand.Parameters[0].Value = Empresa;
                    dbCommand.Parameters[1].Value = pedido;
                    dbCommand.Parameters[2].Value = usucod;
                    dbCommand.Parameters[3].Value = entcod;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                }
                catch
                {
                    erro = "Erro na aprovacao do pedido.";
                }
            }

            return erro;
        }

        public string alteraSatusPedido(string Empresa, string pedido, string usucod, string entcod, string status, string descricao)
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("USER_SP_AlteraStatusPedido", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@empcod", SqlDbType.VarChar, 15, "empcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pedido", SqlDbType.VarChar, 10, "Pedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUsuCod", SqlDbType.VarChar, 31, "vUsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntCod", SqlDbType.VarChar, 7, "vEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@status", SqlDbType.VarChar, 7, "status"));
                    dbCommand.Parameters.Add(new SqlParameter("@descricaoStatus", SqlDbType.VarChar, 50, "descricaoStatus"));

                    dbCommand.Parameters[0].Value = Empresa;
                    dbCommand.Parameters[1].Value = pedido;
                    dbCommand.Parameters[2].Value = usucod;
                    dbCommand.Parameters[3].Value = entcod;
                    dbCommand.Parameters[4].Value = status;
                    dbCommand.Parameters[5].Value = descricao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                }
                catch
                {
                    erro = "Erro na alteracao do status pedido.";
                }
            }
            return erro;
        }

        public string excluiItens(string Empresa, string pedido, string codigoProduto, int numSeq)
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("USER_SP_ExcluiItensPedido", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@empcod", SqlDbType.VarChar, 15, "empcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pedido", SqlDbType.VarChar, 10, "Pedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@sItPedVendaSeq", SqlDbType.SmallInt, 0, "sItPedVendaSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdCodEstr", SqlDbType.VarChar, 30, "vProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@empcod"].Value = Empresa;
                    dbCommand.Parameters["@Pedido"].Value = pedido;
                    dbCommand.Parameters["@sItPedVendaSeq"].Value = numSeq;
                    dbCommand.Parameters["@vProdCodEstr"].Value = codigoProduto;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch
                {
                    erro = "Ocorreu um problema ao tentar alterar os itens";
                }
            }
            return erro;
        }

        public string ExecutaSqlReader(string paramSQL)
        {
            string strValue = "0";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand(paramSQL, dbConnection);
             
                SqlDataReader dataReader = dbCommand.ExecuteReader();
                if (dataReader.Read())
                {
                    strValue = Convert.ToString(dataReader[0]);
                }
                dataReader.Close();
            }
            return strValue;
        }

        public string testaRegrasItens(string empCod, string PedVendaNum, string vendedor, string entidade,
            string natureza, string revenda, string codProduto, string empPais, string empUf, string entPais, string entUf,
            string condicao, string unidade, string operacao, string especie, int cont, double quantidade, float valorItem,
            string codigoTabela, float valorFrete, string nomeProduto, string dataPedido)
        {
            string strError = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("USER_WebVendas_TESTA_REGRAS_NEGOCIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@vEmpCod", SqlDbType.VarChar, 20, "vEmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNum", SqlDbType.VarChar, 7, "vPedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@vBonif", SqlDbType.VarChar, 50, "vBonif"));
                    dbCommand.Parameters.Add(new SqlParameter("@vCodRep", SqlDbType.VarChar, 7, "vCodRep"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntCod", SqlDbType.VarChar, 7, "vEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEntNat", SqlDbType.VarChar, 25, "vEntNat"));
                    dbCommand.Parameters.Add(new SqlParameter("@vRevenda", SqlDbType.VarChar, 3, "vRevenda"));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdCodEstr", SqlDbType.VarChar, 30, "vProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@vEmpOptSimplesNac", SqlDbType.VarChar, 5, "vEmpOptSimplesNac"));
                    dbCommand.Parameters.Add(new SqlParameter("@dEmpDataAdesaoSimplesNac", SqlDbType.DateTime, 0, "dEmpDataAdesaoSimplesNac"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasAuxPaisSiglaOrig", SqlDbType.VarChar, 3, "vClasAuxPaisSiglaOrig"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasAuxPaisSiglaDest", SqlDbType.VarChar, 3, "vClasAuxPaisSiglaDest"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasAuxUfSiglaOrig", SqlDbType.VarChar, 2, "vClasAuxUfSiglaOrig"));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasAuxUfSiglaDest", SqlDbType.VarChar, 2, "vClasAuxUfSiglaDest"));
                    dbCommand.Parameters.Add(new SqlParameter("@vTipoDescontoAplicado", SqlDbType.VarChar, 30, "vTipoDescontoAplicado"));
                    dbCommand.Parameters.Add(new SqlParameter("@nValorDescontoAplicado", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 14, 2, "nValorDescontoAplicado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaOperacao", SqlDbType.VarChar, 15, "vPedVendaOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaEspecie", SqlDbType.VarChar, 20, "vPedVendaEspecie"));
                    dbCommand.Parameters.Add(new SqlParameter("@vUsuCod", SqlDbType.VarChar, 31, ParameterDirection.Output, false, 0, 0, "vUsuCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTipoLancCod", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vTipoLancCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vStatPedVendaCod", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "vStatPedVendaCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaStatDescr", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "vPedVendaStatDescr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vCondPagCod", SqlDbType.VarChar, 7, "vCondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vCondPagNome", SqlDbType.VarChar, 80, ParameterDirection.Output, false, 0, 0, "vCondPagNome", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@bChecagem_ST", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "bChecagem_ST", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vNatOpCodEstr", SqlDbType.VarChar, 15, ParameterDirection.Output, false, 0, 0, "vNatOpCodEstr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nClasFiscPisPerc", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 3, "nClasFiscPisPerc", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iPisTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iPisTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nClasFiscCofinsPerc", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 3, "nClasFiscCofinsPerc", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iCofinsTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iCofinsTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nClasFiscIpiPerc", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 3, "nClasFiscIpiPerc", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iIpiTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iIpiTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTipoVendaCod", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "vTipoVendaCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vLocArmazCodEstr", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vLocArmazCodEstr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iIcmsTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iIcmsTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iIcmsStTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iIcmsStTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@iIcmsRedTributado", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "iIcmsRedTributado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasFiscCod", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vClasFiscCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nReducaoICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "nReducaoICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nItPedVendaMargLucroST", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "nItPedVendaMargLucroST", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nAliquotaICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "nAliquotaICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nIndiceSubstTributaria", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "nIndiceSubstTributaria", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vRedIcmsSobre", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vRedIcmsSobre", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nPorcentoIcms", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "nPorcentoIcms", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nProdPesoLiq", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "nProdPesoLiq", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nProdPesoBruto", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "nProdPesoBruto", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNome", SqlDbType.VarChar, 80, ParameterDirection.Output, false, 0, 0, "vProdNome", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@cTribACod", SqlDbType.VarChar, 1, ParameterDirection.Output, false, 0, 0, "cTribACod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTribBCod", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vTribBCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vItPedVendaUnidMedCod", SqlDbType.VarChar, 7, "vItPedVendaUnidMedCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@sItPedVendaUnidMedPos", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "sItPedVendaUnidMedPos", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nReducaoICMSBaseCalculo", SqlDbType.Decimal, 10, ParameterDirection.Output, false, 2, 0, "nReducaoICMSBaseCalculo", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nReducaoICMSBaseValorImposto", SqlDbType.Decimal, 10, ParameterDirection.Output, false, 2, 0, "nReducaoICMSBaseValorImposto", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@cDescontoICMS_Gov", SqlDbType.Char, 3, ParameterDirection.Output, false, 0, 0, "cDescontoICMS_Gov", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNomeAlt1", SqlDbType.VarChar, 255, ParameterDirection.Output, false, 0, 0, "vProdNomeAlt1", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNomeAlt2", SqlDbType.VarChar, 255, ParameterDirection.Output, false, 0, 0, "vProdNomeAlt2", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vProdNomeAlt3", SqlDbType.VarChar, 255, ParameterDirection.Output, false, 0, 0, "vProdNomeAlt3", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vUnidMedNome", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vUnidMedNome", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vClasseRecDespCodEstr", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "vClasseRecDespCodEstr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTribBModBCCod", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vTribBModBCCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTribBModBCSTCod", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vTribBModBCSTCod", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribCodSaidaIPI", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vConfTribCodSaidaIPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribTipoSaidaIPI", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vConfTribTipoSaidaIPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribCodPIS", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vConfTribCodPIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribTipoPIS", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vConfTribTipoPIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribCodCOFINS", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "vConfTribCodCOFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vConfTribTipoCOFINS", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "vConfTribTipoCOFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vRegCodEstr", SqlDbType.VarChar, 15, ParameterDirection.Output, false, 0, 0, "vRegCodEstr", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nUSERCredBonif", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "nUSERCredBonif", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vUSERtipoverbas", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "vUSERtipoverbas", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nUSERVerbasEventuais", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "nUSERVerbasEventuais", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@nEntCpfCgc", SqlDbType.VarChar, 14, ParameterDirection.Output, false, 0, 0, "nEntCpfCgc", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vTituloEmail", SqlDbType.VarChar, 300, ParameterDirection.Output, false, 0, 0, "vTituloEmail", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 3000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@dataPedido", SqlDbType.DateTime, 10, "dataPedido"));

                    dbCommand.Parameters["@vEmpCod"].Value = empCod;
                    dbCommand.Parameters["@vPedVendaNum"].Value = PedVendaNum.ToString();
                    dbCommand.Parameters["@vBonif"].Value = "";
                    dbCommand.Parameters["@vCodRep"].Value = vendedor.ToString();
                    dbCommand.Parameters["@vEntCod"].Value = entidade.ToString();
                    dbCommand.Parameters["@vEntNat"].Value = natureza.ToString();

                    if (revenda == "0")
                        dbCommand.Parameters["@vRevenda"].Value = "Não";
                    else
                        dbCommand.Parameters["@vRevenda"].Value = "Sim";

                    dbCommand.Parameters["@vProdCodEstr"].Value = codProduto;
                    dbCommand.Parameters["@vEmpOptSimplesNac"].Value = "0";
                    dbCommand.Parameters["@dEmpDataAdesaoSimplesNac"].Value = "1900-01-01 00:00:00.000";//Setado com Zero
                    dbCommand.Parameters["@vClasAuxPaisSiglaOrig"].Value = empPais.ToString();
                    dbCommand.Parameters["@vClasAuxPaisSiglaDest"].Value = entPais.ToString();
                    dbCommand.Parameters["@vClasAuxUfSiglaOrig"].Value = empUf.ToString();
                    dbCommand.Parameters["@vClasAuxUfSiglaDest"].Value = entUf.ToString();
                    dbCommand.Parameters["@vClasAuxUfSiglaDest"].Value = entUf.ToString();
                    dbCommand.Parameters["@nValorDescontoAplicado"].Value = 0;
                    dbCommand.Parameters["@vTipoDescontoAplicado"].Value = "0";
                    dbCommand.Parameters["@vCondPagCod"].Value = condicao;
                    dbCommand.Parameters["@vItPedVendaUnidMedCod"].Value = unidade;
                    dbCommand.Parameters["@vPedVendaOperacao"].Value = operacao;
                    dbCommand.Parameters["@vPedVendaEspecie"].Value = especie;
                    dbCommand.Parameters["@dataPedido"].Value = dataPedido;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    //strError = (string)dbCommand.Parameters["@vTituloEmail"].Value;
                    strError = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch
                {
                    strError = "Erro na inclusao do item";
                }
            }
            return strError;
        }

        public string gravaComplemento(string Empresa, string pedido, string Embarque)
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("USER_SP_GravaComplemento", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@empcod", SqlDbType.VarChar, 15, "empcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pedido", SqlDbType.VarChar, 10, "Pedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntregaImediata", SqlDbType.VarChar, 10, "EntregaImediata"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters[0].Value = Empresa;
                    dbCommand.Parameters[1].Value = pedido;
                    dbCommand.Parameters[2].Value = Embarque;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch
                {
                    erro = "Ocorreu um problema ao gravar complemento.";
                }
            }
            return erro;
        }

        public void recuperaValorHexadecimal(string Empresa, string entidade, string produto, string unidadeMedida, string ClasFiscal, string NatOperacao, string especie, string operacao,
                                             string natureza, string siglaPaisEmpresa, string siglaPaisEntidade, string siglaEstadoEmpresa, string siglaEstadoEntidade, double valor,
                                             string IpiInclusoBase, string dataPedido, out double VALOR_TABPV, out double VALOR_PRODUTO, out string erro, out string TribBCod, out double AliqDiferimento, string tabelaPreco)
        {
            VALOR_TABPV = 0;
            VALOR_PRODUTO = 0;
            AliqDiferimento = 0;
            TribBCod = "";
            erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("PROCESSA_ITEM_PEDIDO ", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CODIGO_PRODUTO", SqlDbType.VarChar, 30, "CODIGO_PRODUTO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODIGO_CLASSIFICACAO", SqlDbType.VarChar, 7, "CODIGO_CLASSIFICACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODIGO_TRIBUT", SqlDbType.VarChar, 2, "CODIGO_TRIBUT"));
                    dbCommand.Parameters.Add(new SqlParameter("@IPI_INCLUSO", SqlDbType.VarChar, 5, "IPI_INCLUSO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODIGO_EMPRESA", SqlDbType.VarChar, 20, "CODIGO_EMPRESA"));
                    dbCommand.Parameters.Add(new SqlParameter("@QUANTIDADE", SqlDbType.Decimal, 0, "QUANTIDADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@UNIDADE", SqlDbType.Int, 0, "UNIDADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@UNITARIO", SqlDbType.Decimal, 0, "UNITARIO"));
                    dbCommand.Parameters.Add(new SqlParameter("@valor_base", SqlDbType.Decimal, 0, "valor_base"));
                    dbCommand.Parameters.Add(new SqlParameter("@valorinterm", SqlDbType.Decimal, 0, "valorinterm"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_TOTAL", SqlDbType.Decimal, 0, "VALOR_TOTAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_DESCGERAL", SqlDbType.Decimal, 0, "PERC_DESCGERAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_ACRESCGERAL", SqlDbType.Decimal, 0, "PERC_ACRESCGERAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_DESC", SqlDbType.Decimal, 0, "PERC_DESC"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_ACRESC", SqlDbType.Decimal, 0, "PERC_ACRESC"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_DESC", SqlDbType.Decimal, 0, "VALOR_DESC"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_ACRESC", SqlDbType.Decimal, 0, "VALOR_ACRESC"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCENTUAL_II", SqlDbType.Decimal, 0, "PERCENTUAL_II"));
                    dbCommand.Parameters.Add(new SqlParameter("@VAL_II", SqlDbType.Decimal, 0, "VAL_II"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCENTUAL_IPI", SqlDbType.Decimal, 0, "PERCENTUAL_IPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@VAL_IPI", SqlDbType.Decimal, 0, "VAL_IPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCENTUAL_FUNRURAL", SqlDbType.Decimal, 0, "PERCENTUAL_FUNRURAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@VAL_FUNRURAL", SqlDbType.Decimal, 0, "VAL_FUNRURAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODIGO_ENTIDADE", SqlDbType.VarChar, 7, "CODIGO_ENTIDADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@MARGEM_LUCRO", SqlDbType.Decimal, 0, "MARGEM_LUCRO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODIGO_TABPV", SqlDbType.VarChar, 15, "CODIGO_TABPV"));
                    dbCommand.Parameters.Add(new SqlParameter("@VTipoVendaCod", SqlDbType.VarChar, 7, "VTipoVendaCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VCalculaPrecoListaint", SqlDbType.VarChar, 5, "VCalculaPrecoListaint"));
                    dbCommand.Parameters.Add(new SqlParameter("@preco_lista", SqlDbType.Decimal, 0, "preco_lista"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLOCARMAZCODESTR", SqlDbType.VarChar, 20, "VLOCARMAZCODESTR"));
                    dbCommand.Parameters.Add(new SqlParameter("@VNATOPCODESTR", SqlDbType.VarChar, 15, "VNATOPCODESTR"));
                    dbCommand.Parameters.Add(new SqlParameter("@Vpedvendanum", SqlDbType.VarChar, 10, "Vpedvendanum"));
                    dbCommand.Parameters.Add(new SqlParameter("@prazo_medio", SqlDbType.Decimal, 0, "prazo_medio"));
                    dbCommand.Parameters.Add(new SqlParameter("@Vindeconcodin", SqlDbType.VarChar, 7, "Vindeconcodin"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATA", SqlDbType.DateTime, 0, "DATA"));
                    dbCommand.Parameters.Add(new SqlParameter("@Vvalorcambio", SqlDbType.Decimal, 0, "Vvalorcambio"));
                    dbCommand.Parameters.Add(new SqlParameter("@VINDECONCODITEMIN", SqlDbType.VarChar, 7, "VINDECONCODITEMIN"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAUFESP", SqlDbType.DateTime, 0, "DATAUFESP"));
                    dbCommand.Parameters.Add(new SqlParameter("@MODELONF", SqlDbType.VarChar, 5, "MODELONF"));
                    dbCommand.Parameters.Add(new SqlParameter("@SERIENF", SqlDbType.VarChar, 3, "SERIENF"));
                    dbCommand.Parameters.Add(new SqlParameter("@NUMERONF", SqlDbType.VarChar, 10, "NUMERONF"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCENTUAL_PIS", SqlDbType.Decimal, 0, "PERCENTUAL_PIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VAL_PIS", SqlDbType.Decimal, 0, "VAL_PIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCENTUAL_COFINS", SqlDbType.Decimal, 0, "PERCENTUAL_COFINS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VAL_COFINS", SqlDbType.Decimal, 0, "VAL_COFINS"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPO_ENTIDADE", SqlDbType.VarChar, 30, "TIPO_ENTIDADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPO_NOTA", SqlDbType.VarChar, 10, "TIPO_NOTA"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATACAMBIO", SqlDbType.DateTime, 0, "DATACAMBIO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAPEDENT", SqlDbType.DateTime, 0, "DATAPEDENT"));
                    dbCommand.Parameters.Add(new SqlParameter("@SHORTFORM", SqlDbType.Decimal, 0, "SHORTFORM"));
                    dbCommand.Parameters.Add(new SqlParameter("@VATUALIZACLASSE", SqlDbType.VarChar, 5, "VATUALIZACLASSE"));
                    dbCommand.Parameters.Add(new SqlParameter("@VPEGAPROMOCAO", SqlDbType.VarChar, 5, "VPEGAPROMOCAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@VMARGEMPAD", SqlDbType.Decimal, 0, "VMARGEMPAD"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTDCASADECVALUNIT", SqlDbType.Int, 0, "QTDCASADECVALUNIT"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_COTADO", SqlDbType.Decimal, 0, "VALOR_COTADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@COTADO", SqlDbType.VarChar, 10, "COTADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCENTUAL_ICMS_PAI", SqlDbType.Decimal, 0, "PERCENTUAL_ICMS_PAI"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCENTUAL_COFINS_PAI", SqlDbType.Decimal, 0, "PERCENTUAL_COFINS_PAI"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCENTUAL_PIS_PAI", SqlDbType.Decimal, 0, "PERCENTUAL_PIS_PAI"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_DESCGERALCUSTO", SqlDbType.Decimal, 0, "PERC_DESCGERALCUSTO"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_ACRESCGERALCUSTO", SqlDbType.Decimal, 0, "PERC_ACRESCGERALCUSTO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CHAMOU", SqlDbType.VarChar, 30, "CHAMOU"));
                    dbCommand.Parameters.Add(new SqlParameter("@OPERACAO", SqlDbType.VarChar, 15, "OPERACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@ESPECIE", SqlDbType.VarChar, 20, "ESPECIE"));
                    dbCommand.Parameters.Add(new SqlParameter("@LOJA", SqlDbType.VarChar, 5, "LOJA"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_DESCPROG", SqlDbType.Decimal, 0, "PERC_DESCPROG"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPOFATCOD", SqlDbType.VarChar, 15, "TIPOFATCOD"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTDMES", SqlDbType.Int, 0, "QTDMES"));
                    dbCommand.Parameters.Add(new SqlParameter("@FATCALCCOMODATO", SqlDbType.Decimal, 0, "FATCALCCOMODATO"));
                    dbCommand.Parameters.Add(new SqlParameter("@QUANTIDADE_PREVISTA", SqlDbType.Decimal, 0, "QUANTIDADE_PREVISTA"));
                    dbCommand.Parameters.Add(new SqlParameter("@UTILIZA_MULTCOMPRA", SqlDbType.VarChar, 10, "UTILIZA_MULTCOMPRA"));
                    dbCommand.Parameters.Add(new SqlParameter("@ACRESC_BENEFICIO_SIMP", SqlDbType.Decimal, 0, "ACRESC_BENEFICIO_SIMP"));

                    dbCommand.Parameters.Add(new SqlParameter("@UNIDADE_MEDIDA", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "UNIDADE_MEDIDA", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_PRODUTO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "VALOR_PRODUTO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_TABPV", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "VALOR_TABPV", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_INTERM", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "VALOR_INTERM", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_ACRESCIMO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_ACRESCIMO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_DESCONTO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_DESCONTO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@CLAS_FISCAL", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "CLAS_FISCAL", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_II", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_II", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_IPI", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_IPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_FUNRURAL", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_FUNRURAL", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@CODIGO_TRIBUTACAO", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "CODIGO_TRIBUTACAO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_ICMS_SUB", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_ICMS_SUB", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_RED_ICMS_SUB", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_RED_ICMS_SUB", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_MARGEM_LUCRO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_MARGEM_LUCRO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_ICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_ICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_RED_ICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_RED_ICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_ICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_ICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_ICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_ICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_ICMS_SUB", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_ICMS_SUB", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_ICMS_SUB", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_ICMS_SUB", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_II", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_II", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_II", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_II", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_IPI", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_IPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_IPI", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_IPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_FUNRURAL", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_FUNRURAL", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_FUNRURAL", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_FUNRURAL", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_DESCONTO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_DESCONTO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_ACRESCIMO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_ACRESCIMO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_PRECO_LISTA", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "VALOR_PRECO_LISTA", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@QUANTIDADE_ATUAL", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "QUANTIDADE_ATUAL", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VCalculaPrecoListaout", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "VCalculaPrecoListaout", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VCalculaIPIPrecoListaout", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "VCalculaIPIPrecoListaout", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VINDECONCODOUT", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "VINDECONCODOUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VvalorcambioOUT", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "VvalorcambioOUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_TOTAL_OUT", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_TOTAL_OUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@QTD_IPI", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "QTD_IPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@QTD_UFESP", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "QTD_UFESP", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_PIS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_PIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_PIS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_PIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_PIS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_PIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_COFINS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_COFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_COFINS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_COFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_COFINS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_COFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_CUSTO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "VALOR_CUSTO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VMARKUP", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "VMARKUP", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VNATOPCODESTROUT", SqlDbType.VarChar, 15, ParameterDirection.Output, false, 0, 0, "VNATOPCODESTROUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_DESCGERAL_OUT", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_DESCGERAL_OUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_ACRESCGERAL_OUT", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_ACRESCGERAL_OUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_IRRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_IRRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_IRRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_IRRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_IRRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "PERC_ICMS_SUB", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VCalculaIcmsZFM", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 10, 4, "VCalculaIcmsZFM", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCDESCICMSDIFALIQ", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERCDESCICMSDIFALIQ", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALDESCICMSDIFALIQ", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALDESCICMSDIFALIQ", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALDESCICMSREDBASECALC", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALDESCICMSREDBASECALC", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPCLASSEENTCOD", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "FPCLASSEENTCOD", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPROGDESCCOD", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "FPPROGDESCCOD", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPROGDESCCODPGC", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "FPPROGDESCCODPGC", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPMULTCOMPRA", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPMULTCOMPRA", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPMULTVENDA", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPMULTVENDA", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPENTFMVALOR", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPENTFMVALOR", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPRODAPLICAFM", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "FPPRODAPLICAFM", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALORTABPV", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPVALORTABPV", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPICMSINCLUSO", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "FPICMSINCLUSO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPISSINCLUSO", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "FPISSINCLUSO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPIPIINCLUSO", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "FPIPIINCLUSO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPCOFINSINCLUSO", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "FPCOFINSINCLUSO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPISINCLUSO", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "FPPISINCLUSO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALPROMOCAO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPVALPROMOCAO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPDATAINIPROM", SqlDbType.DateTime, 0, ParameterDirection.Output, false, 0, 0, "FPDATAINIPROM", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPDATAFIMPROM", SqlDbType.DateTime, 0, ParameterDirection.Output, false, 0, 0, "FPDATAFIMPROM", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCCOFINSENT", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCCOFINSENT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCISSFORNEC", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCISSFORNEC", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPIMPCUSTO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPIMPCUSTO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPIMPVENDA", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPIMPVENDA", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALUNITVENDALIQ", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPVALUNITVENDALIQ", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPCUSTOUNITLIQ", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPCUSTOUNITLIQ", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCICMSCUSTO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCICMSCUSTO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCICMSVENDA", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCICMSVENDA", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCISS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCISS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCPIS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCPIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCCOFINS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCCOFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCIPI", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCIPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALCUSTOTABPV", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPVALCUSTOTABPV", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_RED_IPI", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_RED_IPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_RED_COFINS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_RED_COFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_RED_PIS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_RED_PIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_RED_II", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_RED_II", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@RED_IPI", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "RED_IPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@RED_COFINS", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "RED_COFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@RED_PIS", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "RED_PIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@RED_II", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "RED_II", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@RED_ICMS", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "RED_ICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@RED_ICMS_SUB", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "RED_ICMS_SUB", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPRODECVALOR", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPRODECVALOR", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPRODAPLICAEC", SqlDbType.VarChar, 5, ParameterDirection.Output, false, 0, 0, "FPPRODAPLICAEC", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCDIFERIMENTO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERCDIFERIMENTO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCCREDPRESUMIDO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERCCREDPRESUMIDO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCLIMCREDPRESUMIDO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERCLIMCREDPRESUMIDO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCCALCVALRECOLHER", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERCCALCVALRECOLHER", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_CSLLRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_CSLLRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_CSLLRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_CSLLRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_CSLLRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_CSLLRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_PISRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_PISRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_PISRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_PISRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_PISRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_PISRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_COFINSRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_COFINSRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_COFINSRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_COFINSRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_COFINSRF", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_COFINSRF", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCACRESC", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCACRESC", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCDESC", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCDESC", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPRECOLISTA", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPPRECOLISTA", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCICMSEXONERADO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERCICMSEXONERADO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@TRIBBMODBCCOD", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "TRIBBMODBCCOD", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@TRIBBMODBCSTCOD", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "TRIBBMODBCSTCOD", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@TRIBIPICOD", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "TRIBIPICOD", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@TRIBCOFINSCOD", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "TRIBCOFINSCOD", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@TRIBPISCOD", SqlDbType.VarChar, 2, ParameterDirection.Output, false, 0, 0, "TRIBPISCOD", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_ICMS_OPER", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_ICMS_OPER", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@BASE_ICMS_OPER", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "BASE_ICMS_OPER", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_ICMS_OPER", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_ICMS_OPER", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PRECO_VENDA_VAREJO", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "PRECO_VENDA_VAREJO", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR_SELO_CTRL", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "VALOR_SELO_CTRL", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@PERC_DESCPROG_OUT", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "PERC_DESCPROG_OUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPOFATCOD_OUT", SqlDbType.VarChar, 15, ParameterDirection.Output, false, 0, 0, "TIPOFATCOD_OUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@CONFTRIBSIMPNACCOD", SqlDbType.VarChar, 3, ParameterDirection.Output, false, 0, 0, "CONFTRIBSIMPNACCOD", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPMARGLUCROST", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "FPMARGLUCROST", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALICMSRETST", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "FPVALICMSRETST", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "FPVALICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALBASEICMSSTDAEGNRE", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "FPVALBASEICMSSTDAEGNRE", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPMARGLUCROSTDAEGNRE", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPMARGLUCROSTDAEGNRE", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPERCICMSSTDAEGNRE", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 4, "FPPERCICMSSTDAEGNRE", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPPRECOLISTASTDAEGNRE", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPPRECOLISTASTDAEGNRE", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALICMSSTDAEGNRE", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "FPVALICMSSTDAEGNRE", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALICMSRETSTDAEGNRE", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 14, 2, "FPVALICMSRETSTDAEGNRE", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@UTILIZA_MULTCOMPRA_OUT", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "UTILIZA_MULTCOMPRA_OUT", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPQTDPAUTAICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPQTDPAUTAICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALPAUTAICMS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPVALPAUTAICMS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPQTDPAUTAIPI", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPQTDPAUTAIPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALPAUTAIPI", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPVALPAUTAIPI", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPQTDPAUTAPIS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPQTDPAUTAPIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALPAUTAPIS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPVALPAUTAPIS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPQTDPAUTACOFINS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPQTDPAUTACOFINS", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@FPVALPAUTACOFINS", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 24, 9, "FPVALPAUTACOFINS", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CODIGO_PRODUTO"].Value = produto;                    
                    dbCommand.Parameters["@CODIGO_CLASSIFICACAO"].Value = ClasFiscal;
                    dbCommand.Parameters["@CODIGO_TRIBUT"].Value = "";
                    dbCommand.Parameters["@IPI_INCLUSO"].Value = IpiInclusoBase;
                    dbCommand.Parameters["@CODIGO_EMPRESA"].Value = Empresa;
                    dbCommand.Parameters["@QUANTIDADE"].Value = 128;
                    dbCommand.Parameters["@UNIDADE"].Value = 1;
                    dbCommand.Parameters["@UNITARIO"].Value = -1;
                    dbCommand.Parameters["@valor_base"].Value = -1;
                    dbCommand.Parameters["@valorinterm"].Value = -1;
                    dbCommand.Parameters["@VALOR_TOTAL"].Value = valor;
                    dbCommand.Parameters["@PERC_DESCGERAL"].Value = 0;
                    dbCommand.Parameters["@PERC_ACRESCGERAL"].Value = 0;
                    dbCommand.Parameters["@PERC_DESC"].Value = 0;
                    dbCommand.Parameters["@PERC_ACRESC"].Value = 0;
                    dbCommand.Parameters["@VALOR_DESC"].Value = 0;
                    dbCommand.Parameters["@VALOR_ACRESC"].Value = 0;
                    dbCommand.Parameters["@PERCENTUAL_II"].Value = -1;
                    dbCommand.Parameters["@VAL_II"].Value = -1;
                    dbCommand.Parameters["@PERCENTUAL_IPI"].Value = -1;
                    dbCommand.Parameters["@VAL_IPI"].Value = -1;
                    dbCommand.Parameters["@PERCENTUAL_FUNRURAL"].Value = -1;
                    dbCommand.Parameters["@VAL_FUNRURAL"].Value = -1;
                    dbCommand.Parameters["@CODIGO_ENTIDADE"].Value = entidade;
                    dbCommand.Parameters["@MARGEM_LUCRO"].Value = -1;
                    dbCommand.Parameters["@CODIGO_TABPV"].Value = tabelaPreco;
                    dbCommand.Parameters["@VTipoVendaCod"].Value = "";
                    dbCommand.Parameters["@VCalculaPrecoListaint"].Value = "Não";
                    dbCommand.Parameters["@preco_lista"].Value = 0;
                    dbCommand.Parameters["@VLOCARMAZCODESTR"].Value = "01.01.03";
                    dbCommand.Parameters["@VNATOPCODESTR"].Value = NatOperacao;
                    dbCommand.Parameters["@prazo_medio"].Value = "0";
                    dbCommand.Parameters["@Vpedvendanum"].Value = "0132344";
                    dbCommand.Parameters["@Vindeconcodin"].Value = "0000001";
                    dbCommand.Parameters["@DATA"].Value = this.FormataData(dataPedido);
                    dbCommand.Parameters["@Vvalorcambio"].Value = -1;
                    dbCommand.Parameters["@VINDECONCODITEMIN"].Value = "";
                    dbCommand.Parameters["@DATAUFESP"].Value = this.FormataData(dataPedido);
                    dbCommand.Parameters["@MODELONF"].Value = "";
                    dbCommand.Parameters["@SERIENF"].Value = "";
                    dbCommand.Parameters["@NUMERONF"].Value = "";
                    dbCommand.Parameters["@PERCENTUAL_PIS"].Value = -1;
                    dbCommand.Parameters["@VAL_PIS"].Value = -1;
                    dbCommand.Parameters["@PERCENTUAL_COFINS"].Value = -1;
                    dbCommand.Parameters["@VAL_COFINS"].Value = -1;
                    dbCommand.Parameters["@TIPO_ENTIDADE"].Value = natureza;
                    dbCommand.Parameters["@TIPO_NOTA"].Value = "Saída";
                    dbCommand.Parameters["@DATACAMBIO"].Value = this.FormataData(dataPedido);
                    dbCommand.Parameters["@DATAPEDENT"].Value = this.FormataData(dataPedido);
                    dbCommand.Parameters["@SHORTFORM"].Value = 0;
                    dbCommand.Parameters["@VATUALIZACLASSE"].Value = "Sim";
                    dbCommand.Parameters["@VPEGAPROMOCAO"].Value = "Não";
                    dbCommand.Parameters["@VMARGEMPAD"].Value = 0;
                    dbCommand.Parameters["@QTDCASADECVALUNIT"].Value = 6;
                    dbCommand.Parameters["@VALOR_COTADO"].Value = 0;
                    dbCommand.Parameters["@COTADO"].Value = "Não";
                    dbCommand.Parameters["@PERCENTUAL_ICMS_PAI"].Value = -1;
                    dbCommand.Parameters["@PERCENTUAL_COFINS_PAI"].Value = -1;
                    dbCommand.Parameters["@PERCENTUAL_PIS_PAI"].Value = -1;
                    dbCommand.Parameters["@PERC_DESCGERALCUSTO"].Value = -1;
                    dbCommand.Parameters["@PERC_ACRESCGERALCUSTO"].Value = -1;
                    dbCommand.Parameters["@CHAMOU"].Value = "PED_VENDA1";
                    dbCommand.Parameters["@OPERACAO"].Value = operacao;
                    dbCommand.Parameters["@ESPECIE"].Value = especie;
                    dbCommand.Parameters["@LOJA"].Value = "Não";
                    dbCommand.Parameters["@PERC_DESCPROG"].Value = 0;
                    dbCommand.Parameters["@TIPOFATCOD"].Value = "";
                    dbCommand.Parameters["@QTDMES"].Value = 0;
                    dbCommand.Parameters["@FATCALCCOMODATO"].Value = 0;
                    dbCommand.Parameters["@QUANTIDADE_PREVISTA"].Value = 0;
                    dbCommand.Parameters["@UTILIZA_MULTCOMPRA"].Value = "Nenhum";
                    dbCommand.Parameters["@ACRESC_BENEFICIO_SIMP"].Value = 0;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 32000;

                    dbCommand.ExecuteNonQuery();

                    if (dbCommand.Parameters["@VALOR_TABPV"].Value.ToString() != "")
                        VALOR_TABPV = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_TABPV"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_PRODUTO"].Value.ToString() != "")                                               
                        VALOR_PRODUTO = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_PRODUTO"].Value, 2);

                    if (dbCommand.Parameters["@UNIDADE_MEDIDA"].Value.ToString() != "")
                        InfItem.UnidadeMedida = ((string)dbCommand.Parameters["@UNIDADE_MEDIDA"].Value);

                    if (dbCommand.Parameters["@VALOR_INTERM"].Value.ToString() != "")
                        InfItem.VALOR_INTERM = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_INTERM"].Value, 2);

                    if (dbCommand.Parameters["@PERC_ACRESCIMO"].Value.ToString() != "")
                        InfItem.PERC_ACRESCIMO = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_ACRESCIMO"].Value, 2);

                    if (dbCommand.Parameters["@PERC_DESCONTO"].Value.ToString() != "")
                        InfItem.PERC_DESCONTO = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_DESCONTO"].Value, 2);

                    if (dbCommand.Parameters["@CLAS_FISCAL"].Value.ToString() != "")
                        InfItem.CLAS_FISCAL = ((string)dbCommand.Parameters["@CLAS_FISCAL"].Value);

                    if (dbCommand.Parameters["@PERC_II"].Value.ToString() != "")
                        InfItem.PERC_II = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_II"].Value, 2);

                    if (dbCommand.Parameters["@PERC_IPI"].Value.ToString() != "")
                        InfItem.PERC_IPI = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_IPI"].Value, 2);

                    if (dbCommand.Parameters["@PERC_FUNRURAL"].Value.ToString() != "")
                        InfItem.PERC_FUNRURAL = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_FUNRURAL"].Value, 2);

                    if (dbCommand.Parameters["@PERC_ICMS"].Value.ToString() != "")
                        InfItem.PERC_ICMS = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_ICMS"].Value, 2);

                    if (dbCommand.Parameters["@CODIGO_TRIBUTACAO"].Value.ToString() != "")
                    {
                        InfItem.CODIGO_TRIBUTACAO = ((string)dbCommand.Parameters["@CODIGO_TRIBUTACAO"].Value);
                        TribBCod = InfItem.CODIGO_TRIBUTACAO;
                    }

                    if (dbCommand.Parameters["@PERCDIFERIMENTO"].Value.ToString() != "")
                    {
                        InfItem.PERCDIFERIMENTO = (double)Math.Round((decimal)dbCommand.Parameters["@PERCDIFERIMENTO"].Value, 2);
                        AliqDiferimento = InfItem.PERCDIFERIMENTO;
                    }
                    /*Mario 14/09/2015 Alteração para quando for venda com diferimento(Tributação 51) formar preço 
                     * do item adicionando a aliquota menos o percentual de diferimento da classificação fiscal. 
                     * No Apolo forma preço somente com a aliquota da classificação fiscal.*/
                    if (natureza != "Consumidor Contribuinte" && natureza != "Consumidor")
                    {
                        if (dbCommand.Parameters["@CODIGO_TRIBUTACAO"].Value.ToString() == "51")
                        {
                            VALOR_PRODUTO = (double)Math.Round((decimal)(VALOR_TABPV / ((100 - (InfItem.PERC_ICMS - (InfItem.PERC_ICMS * (InfItem.PERCDIFERIMENTO / 100)))) / 100)), 2);
                        }
                    }

                    if (dbCommand.Parameters["@PERC_ICMS_SUB"].Value.ToString() != "")
                        InfItem.PERC_ICMS_SUB = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_ICMS_SUB"].Value, 2);

                    if (dbCommand.Parameters["@PERC_RED_ICMS_SUB"].Value.ToString() != "")
                        InfItem.PERC_RED_ICMS_SUB = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_RED_ICMS_SUB"].Value, 2);

                    if (dbCommand.Parameters["@PERC_MARGEM_LUCRO"].Value.ToString() != "")
                        InfItem.PERC_MARGEM_LUCRO = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_MARGEM_LUCRO"].Value, 2);

                    if (dbCommand.Parameters["@PERC_RED_ICMS"].Value.ToString() != "")
                        InfItem.PERC_RED_ICMS = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_RED_ICMS"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_ICMS"].Value.ToString() != "")
                        InfItem.VALOR_ICMS = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_ICMS"].Value, 2);

                    if (dbCommand.Parameters["@BASE_ICMS"].Value.ToString() != "")
                        InfItem.BASE_ICMS = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_ICMS"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_ICMS_SUB"].Value.ToString() != "")
                        InfItem.VALOR_ICMS_SUB = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_ICMS_SUB"].Value, 2);

                    if (dbCommand.Parameters["@BASE_ICMS_SUB"].Value.ToString() != "")
                        InfItem.BASE_ICMS_SUB = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_ICMS_SUB"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_II"].Value.ToString() != "")
                        InfItem.VALOR_II = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_II"].Value, 2);

                    if (dbCommand.Parameters["@BASE_II"].Value.ToString() != "")
                        InfItem.BASE_II = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_II"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_IPI"].Value.ToString() != "")
                        InfItem.VALOR_IPI = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_IPI"].Value, 2);

                    if (dbCommand.Parameters["@BASE_IPI"].Value.ToString() != "")
                        InfItem.BASE_IPI = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_IPI"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_FUNRURAL"].Value.ToString() != "")
                        InfItem.VALOR_FUNRURAL = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_FUNRURAL"].Value, 2);

                    if (dbCommand.Parameters["@BASE_FUNRURAL"].Value.ToString() != "")
                        InfItem.BASE_FUNRURAL = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_FUNRURAL"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_DESCONTO"].Value.ToString() != "")
                        InfItem.VALOR_DESCONTO = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_DESCONTO"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_ACRESCIMO"].Value.ToString() != "")
                        InfItem.VALOR_ACRESCIMO = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_ACRESCIMO"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_PRECO_LISTA"].Value.ToString() != "")
                        InfItem.VALOR_PRECO_LISTA = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_PRECO_LISTA"].Value, 2);

                    if (dbCommand.Parameters["@QUANTIDADE_ATUAL"].Value.ToString() != "")
                        InfItem.QUANTIDADE_ATUAL = (double)Math.Round((decimal)dbCommand.Parameters["@QUANTIDADE_ATUAL"].Value, 2);

                    if (dbCommand.Parameters["@VCalculaPrecoListaout"].Value.ToString() != "")
                        InfItem.VCalculaPrecoListaOut = ((string)dbCommand.Parameters["@VCalculaPrecoListaout"].Value);

                    if (dbCommand.Parameters["@VCalculaIPIPrecoListaout"].Value.ToString() != "")
                        InfItem.VCalculaIPIPrecoListaOut = ((string)dbCommand.Parameters["@VCalculaIPIPrecoListaout"].Value);

                    if (dbCommand.Parameters["@VINDECONCODOUT"].Value.ToString() != "")
                        InfItem.VINDECONCODOUT = ((string)dbCommand.Parameters["@VINDECONCODOUT"].Value);

                    if (dbCommand.Parameters["@VvalorcambioOUT"].Value.ToString() != "")
                        InfItem.Vvalorcambioout = (double)Math.Round((decimal)dbCommand.Parameters["@VvalorcambioOUT"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_TOTAL_OUT"].Value.ToString() != "")
                        InfItem.VALOR_TOTAL_OUT = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_TOTAL_OUT"].Value, 2);

                    if (dbCommand.Parameters["@QTD_IPI"].Value.ToString() != "")
                        InfItem.QTD_IPI = (double)Math.Round((decimal)dbCommand.Parameters["@QTD_IPI"].Value, 2);

                    if (dbCommand.Parameters["@QTD_UFESP"].Value.ToString() != "")
                        InfItem.QTD_UFESP = (double)Math.Round((decimal)dbCommand.Parameters["@QTD_UFESP"].Value, 2);

                    if (dbCommand.Parameters["@PERC_PIS"].Value.ToString() != "")
                        InfItem.PERC_PIS = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_PIS"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_PIS"].Value.ToString() != "")
                        InfItem.VALOR_PIS = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_PIS"].Value, 2);

                    if (dbCommand.Parameters["@BASE_PIS"].Value.ToString() != "")
                        InfItem.BASE_PIS = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_PIS"].Value, 2);

                    if (dbCommand.Parameters["@PERC_COFINS"].Value.ToString() != "")
                        InfItem.PERC_COFINS = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_COFINS"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_COFINS"].Value.ToString() != "")
                        InfItem.VALOR_COFINS = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_COFINS"].Value, 2);

                    if (dbCommand.Parameters["@BASE_COFINS"].Value.ToString() != "")
                        InfItem.BASE_COFINS = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_COFINS"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_CUSTO"].Value.ToString() != "")
                        InfItem.VALOR_CUSTO = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_CUSTO"].Value, 2);

                    if (dbCommand.Parameters["@VMARKUP"].Value.ToString() != "")
                        InfItem.VMARKUP = (double)Math.Round((decimal)dbCommand.Parameters["@VMARKUP"].Value, 2);

                    if (dbCommand.Parameters["@VNATOPCODESTROUT"].Value.ToString() != "")
                        InfItem.VNATOPCODESTROUT = ((string)dbCommand.Parameters["@VNATOPCODESTROUT"].Value);

                    if (dbCommand.Parameters["@PERC_DESCGERAL_OUT"].Value.ToString() != "")
                        InfItem.PERC_DESCGERAL_OUT = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_DESCGERAL_OUT"].Value, 2);

                    if (dbCommand.Parameters["@PERC_ACRESCGERAL_OUT"].Value.ToString() != "")
                        InfItem.PERC_ACRESCGERAL_OUT = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_ACRESCGERAL_OUT"].Value, 2);

                    if (dbCommand.Parameters["@PERC_IRRF"].Value.ToString() != "")
                        InfItem.PERC_IRRF = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_IRRF"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_IRRF"].Value.ToString() != "")
                        InfItem.VALOR_IRRF = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_IRRF"].Value, 2);

                    if (dbCommand.Parameters["@BASE_IRRF"].Value.ToString() != "")
                        InfItem.BASE_IRRF = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_IRRF"].Value, 2);

                    if (dbCommand.Parameters["@VCalculaIcmsZFM"].Value.ToString() != "")
                        InfItem.VCalculaIcmsZFM = ((string)dbCommand.Parameters["@VCalculaIcmsZFM"].Value);
                                        
                    if(dbCommand.Parameters["@PERCDESCICMSDIFALIQ"].Value.ToString() != "")                    
                        InfItem.PERCDESCICMSDIFALIQ = (double)Math.Round((decimal)dbCommand.Parameters["@PERCDESCICMSDIFALIQ"].Value, 2);

                    if (dbCommand.Parameters["@VALDESCICMSREDBASECALC"].Value.ToString() != "") 
                        InfItem.VALDESCICMSREDBASECALC = (double)Math.Round((decimal)dbCommand.Parameters["@VALDESCICMSREDBASECALC"].Value, 2);

                    if (dbCommand.Parameters["@FPCLASSEENTCOD"].Value.ToString() != "") 
                        InfItem.FPCLASSEENTCOD = ((string)dbCommand.Parameters["@FPCLASSEENTCOD"].Value);

                    if (dbCommand.Parameters["@FPPROGDESCCOD"].Value.ToString() != "") 
                        InfItem.FPPROGDESCCOD = ((string)dbCommand.Parameters["@FPPROGDESCCOD"].Value);

                    if (dbCommand.Parameters["@FPPROGDESCCODPGC"].Value.ToString() != "") 
                        InfItem.FPPROGDESCCODPGC = ((string)dbCommand.Parameters["@FPPROGDESCCODPGC"].Value);

                    if (dbCommand.Parameters["@FPMULTCOMPRA"].Value.ToString() != "") 
                        InfItem.FPMULTCOMPRA = (double)Math.Round((decimal)dbCommand.Parameters["@FPMULTCOMPRA"].Value, 2);

                    if (dbCommand.Parameters["@FPMULTVENDA"].Value.ToString() != "") 
                        InfItem.FPMULTVENDA = (double)Math.Round((decimal)dbCommand.Parameters["@FPMULTVENDA"].Value, 2);

                    if (dbCommand.Parameters["@FPENTFMVALOR"].Value.ToString() != "")
                        InfItem.FPENTFMVALOR = (double)Math.Round((decimal)dbCommand.Parameters["@FPENTFMVALOR"].Value, 2);

                    if (dbCommand.Parameters["@FPPRODAPLICAFM"].Value.ToString() != "")
                        InfItem.FPPRODAPLICAFM = ((string)dbCommand.Parameters["@FPPRODAPLICAFM"].Value);

                    if (dbCommand.Parameters["@FPVALORTABPV"].Value.ToString() != "")
                        InfItem.FPVALORTABPV = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALORTABPV"].Value, 2);

                    if (dbCommand.Parameters["@FPICMSINCLUSO"].Value.ToString() != "")
                        InfItem.FPICMSINCLUSO = ((string)dbCommand.Parameters["@FPICMSINCLUSO"].Value);

                    if (dbCommand.Parameters["@FPISSINCLUSO"].Value.ToString() != "")
                        InfItem.FPISSINCLUSO = ((string)dbCommand.Parameters["@FPISSINCLUSO"].Value);

                    if (dbCommand.Parameters["@FPIPIINCLUSO"].Value.ToString() != "")
                        InfItem.FPIPIINCLUSO = ((string)dbCommand.Parameters["@FPIPIINCLUSO"].Value);

                    if (dbCommand.Parameters["@FPCOFINSINCLUSO"].Value.ToString() != "")
                        InfItem.FPCOFINSINCLUSO = ((string)dbCommand.Parameters["@FPCOFINSINCLUSO"].Value);

                    if (dbCommand.Parameters["@FPPISINCLUSO"].Value.ToString() != "")
                        InfItem.FPPISINCLUSO = ((string)dbCommand.Parameters["@FPPISINCLUSO"].Value);

                    if (dbCommand.Parameters["@FPVALPROMOCAO"].Value.ToString() != "")
                        InfItem.FPVALPROMOCAO = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALPROMOCAO"].Value, 2);

                    if (dbCommand.Parameters["@FPDATAINIPROM"].Value.ToString() != "")
                        InfItem.FPDATAINIPROM = ((DateTime)dbCommand.Parameters["@FPDATAINIPROM"].Value);

                    if (dbCommand.Parameters["@FPDATAFIMPROM"].Value.ToString() != "")
                        InfItem.FPDATAFIMPROM = ((DateTime)dbCommand.Parameters["@FPDATAFIMPROM"].Value);

                    if (dbCommand.Parameters["@FPPERCCOFINSENT"].Value.ToString() != "")
                        InfItem.FPPERCCOFINSENT = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCCOFINSENT"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCISSFORNEC"].Value.ToString() != "")
                        InfItem.FPPERCISSFORNEC = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCISSFORNEC"].Value, 2);

                    if (dbCommand.Parameters["@FPIMPCUSTO"].Value.ToString() != "")
                        InfItem.FPIMPCUSTO = (double)Math.Round((decimal)dbCommand.Parameters["@FPIMPCUSTO"].Value, 2);

                    if (dbCommand.Parameters["@FPIMPVENDA"].Value.ToString() != "")
                        InfItem.FPIMPVENDA = (double)Math.Round((decimal)dbCommand.Parameters["@FPIMPVENDA"].Value, 2);

                    if (dbCommand.Parameters["@FPVALUNITVENDALIQ"].Value.ToString() != "")
                        InfItem.FPVALUNITVENDALIQ = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALUNITVENDALIQ"].Value, 2);

                    if (dbCommand.Parameters["@FPCUSTOUNITLIQ"].Value.ToString() != "")
                        InfItem.FPCUSTOUNITLIQ = (double)Math.Round((decimal)dbCommand.Parameters["@FPCUSTOUNITLIQ"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCICMSCUSTO"].Value.ToString() != "")
                        InfItem.FPPERCICMSCUSTO = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCICMSCUSTO"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCICMSVENDA"].Value.ToString() != "")
                        InfItem.FPPERCICMSVENDA = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCICMSVENDA"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCISS"].Value.ToString() != "")
                        InfItem.FPPERCISS = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCISS"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCPIS"].Value.ToString() != "")
                        InfItem.FPPERCPIS = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCPIS"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCCOFINS"].Value.ToString() != "")
                        InfItem.FPPERCCOFINS = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCCOFINS"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCIPI"].Value.ToString() != "")
                        InfItem.FPPERCIPI = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCIPI"].Value, 2);

                    if (dbCommand.Parameters["@FPVALCUSTOTABPV"].Value.ToString() != "")
                        InfItem.FPVALCUSTOTABPV = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALCUSTOTABPV"].Value, 2);

                    if (dbCommand.Parameters["@PERC_RED_IPI"].Value.ToString() != "")
                        InfItem.PERC_RED_IPI = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_RED_IPI"].Value, 2);

                    if (dbCommand.Parameters["@PERC_RED_COFINS"].Value.ToString() != "")
                        InfItem.PERC_RED_COFINS = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_RED_COFINS"].Value, 2);

                    if (dbCommand.Parameters["@PERC_RED_PIS"].Value.ToString() != "")
                        InfItem.PERC_RED_PIS = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_RED_PIS"].Value, 2);

                    if (dbCommand.Parameters["@PERC_RED_II"].Value.ToString() != "")
                        InfItem.PERC_RED_II = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_RED_II"].Value, 2);

                    if (dbCommand.Parameters["@RED_IPI"].Value.ToString() != "")
                        InfItem.RED_IPI = ((string)dbCommand.Parameters["@RED_IPI"].Value);

                    if (dbCommand.Parameters["@RED_COFINS"].Value.ToString() != "")
                        InfItem.RED_COFINS = ((string)dbCommand.Parameters["@RED_COFINS"].Value);

                    if (dbCommand.Parameters["@RED_PIS"].Value.ToString() != "")
                        InfItem.RED_PIS = ((string)dbCommand.Parameters["@RED_PIS"].Value);

                    if (dbCommand.Parameters["@RED_II"].Value.ToString() != "")
                        InfItem.RED_II = ((string)dbCommand.Parameters["@RED_II"].Value);

                    if (dbCommand.Parameters["@RED_ICMS"].Value.ToString() != "")
                        InfItem.RED_ICMS = ((string)dbCommand.Parameters["@RED_ICMS"].Value);

                    if (dbCommand.Parameters["@RED_ICMS_SUB"].Value.ToString() != "")
                        InfItem.RED_ICMS_SUB = ((string)dbCommand.Parameters["@RED_ICMS_SUB"].Value);

                    if (dbCommand.Parameters["@FPPRODECVALOR"].Value.ToString() != "")
                        InfItem.FPPRODECVALOR = (double)Math.Round((decimal)dbCommand.Parameters["@FPPRODECVALOR"].Value, 2);

                    if (dbCommand.Parameters["@FPPRODAPLICAEC"].Value.ToString() != "")
                        InfItem.FPPRODAPLICAEC = ((string)dbCommand.Parameters["@FPPRODAPLICAEC"].Value);

                    if (dbCommand.Parameters["@PERCCREDPRESUMIDO"].Value.ToString() != "")
                        InfItem.PERCCREDPRESUMIDO = (double)Math.Round((decimal)dbCommand.Parameters["@PERCCREDPRESUMIDO"].Value, 2);

                    if (dbCommand.Parameters["@PERCLIMCREDPRESUMIDO"].Value.ToString() != "")
                        InfItem.PERCLIMCREDPRESUMIDO = (double)Math.Round((decimal)dbCommand.Parameters["@PERCLIMCREDPRESUMIDO"].Value, 2);

                    if (dbCommand.Parameters["@PERCCALCVALRECOLHER"].Value.ToString() != "")
                        InfItem.PERCCALCVALRECOLHER = (double)Math.Round((decimal)dbCommand.Parameters["@PERCCALCVALRECOLHER"].Value, 2);

                    if (dbCommand.Parameters["@PERC_CSLLRF"].Value.ToString() != "")
                        InfItem.PERC_CSLLRF = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_CSLLRF"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_CSLLRF"].Value.ToString() != "")
                        InfItem.VALOR_CSLLRF = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_CSLLRF"].Value, 2);

                    if (dbCommand.Parameters["@BASE_CSLLRF"].Value.ToString() != "")
                        InfItem.BASE_CSLLRF = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_CSLLRF"].Value, 2);

                    if (dbCommand.Parameters["@PERC_PISRF"].Value.ToString() != "")
                        InfItem.PERC_PISRF = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_PISRF"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_PISRF"].Value.ToString() != "")
                        InfItem.VALOR_PISRF = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_PISRF"].Value, 2);

                    if (dbCommand.Parameters["@BASE_PISRF"].Value.ToString() != "")
                        InfItem.BASE_PISRF = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_PISRF"].Value, 2);

                    if (dbCommand.Parameters["@PERC_COFINSRF"].Value.ToString() != "")
                        InfItem.PERC_COFINSRF = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_COFINSRF"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_COFINSRF"].Value.ToString() != "")
                        InfItem.VALOR_COFINSRF = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_COFINSRF"].Value, 2);

                    if (dbCommand.Parameters["@BASE_COFINSRF"].Value.ToString() != "")
                        InfItem.BASE_COFINSRF = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_COFINSRF"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCACRESC"].Value.ToString() != "")
                        InfItem.FPPERCACRESC = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCACRESC"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCDESC"].Value.ToString() != "")
                        InfItem.FPPERCDESC = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCDESC"].Value, 2);

                    if (dbCommand.Parameters["@FPPRECOLISTA"].Value.ToString() != "")
                        InfItem.FPPRECOLISTA = (double)Math.Round((decimal)dbCommand.Parameters["@FPPRECOLISTA"].Value, 2);

                    if (dbCommand.Parameters["@PERCICMSEXONERADO"].Value.ToString() != "")
                        InfItem.PERCICMSEXONERADO = (double)Math.Round((decimal)dbCommand.Parameters["@PERCICMSEXONERADO"].Value, 2);

                    if (dbCommand.Parameters["@TRIBBMODBCCOD"].Value.ToString() != "")
                        InfItem.TRIBBMODBCCOD = ((string)dbCommand.Parameters["@TRIBBMODBCCOD"].Value);

                    if (dbCommand.Parameters["@TRIBBMODBCSTCOD"].Value.ToString() != "")
                        InfItem.TRIBBMODBCSTCOD = ((string)dbCommand.Parameters["@TRIBBMODBCSTCOD"].Value);

                    if (dbCommand.Parameters["@TRIBIPICOD"].Value.ToString() != "")
                        InfItem.TRIBIPICOD = ((string)dbCommand.Parameters["@TRIBIPICOD"].Value);

                    if (dbCommand.Parameters["@TRIBCOFINSCOD"].Value.ToString() != "")
                        InfItem.TRIBCOFINSCOD = ((string)dbCommand.Parameters["@TRIBCOFINSCOD"].Value);

                    if (dbCommand.Parameters["@TRIBPISCOD"].Value.ToString() != "")
                        InfItem.TRIBPISCOD = ((string)dbCommand.Parameters["@TRIBPISCOD"].Value);

                    if (dbCommand.Parameters["@PERC_ICMS_OPER"].Value.ToString() != "")
                        InfItem.PERC_ICMS_OPER = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_ICMS_OPER"].Value, 2);

                    if (dbCommand.Parameters["@BASE_ICMS_OPER"].Value.ToString() != "")
                        InfItem.BASE_ICMS_OPER = (double)Math.Round((decimal)dbCommand.Parameters["@BASE_ICMS_OPER"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_ICMS_OPER"].Value.ToString() != "")
                        InfItem.VALOR_ICMS_OPER = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_ICMS_OPER"].Value, 2);

                    if (dbCommand.Parameters["@PRECO_VENDA_VAREJO"].Value.ToString() != "")
                        InfItem.PRECO_VENDA_VAREJO = (double)Math.Round((decimal)dbCommand.Parameters["@PRECO_VENDA_VAREJO"].Value, 2);

                    if (dbCommand.Parameters["@VALOR_SELO_CTRL"].Value.ToString() != "")
                        InfItem.VALOR_SELO_CTRL = (double)Math.Round((decimal)dbCommand.Parameters["@VALOR_SELO_CTRL"].Value, 2);

                    if (dbCommand.Parameters["@PERC_DESCPROG_OUT"].Value.ToString() != "")
                        InfItem.PERC_DESCPROG_OUT = (double)Math.Round((decimal)dbCommand.Parameters["@PERC_DESCPROG_OUT"].Value, 2);

                    if (dbCommand.Parameters["@TIPOFATCOD_OUT"].Value.ToString() != "")
                        InfItem.TIPOFATCOD_OUT = ((string)dbCommand.Parameters["@TIPOFATCOD_OUT"].Value);

                    if (dbCommand.Parameters["@CONFTRIBSIMPNACCOD"].Value.ToString() != "")
                        InfItem.CONFTRIBSIMPNACCOD = ((string)dbCommand.Parameters["@CONFTRIBSIMPNACCOD"].Value);

                    if (dbCommand.Parameters["@FPMARGLUCROST"].Value.ToString() != "")
                        InfItem.FPMARGLUCROST = (double)Math.Round((decimal)dbCommand.Parameters["@FPMARGLUCROST"].Value, 2);

                    if (dbCommand.Parameters["@FPVALICMSRETST"].Value.ToString() != "")
                        InfItem.FPVALICMSRETST = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALICMSRETST"].Value, 2);

                    if (dbCommand.Parameters["@FPVALICMS"].Value.ToString() != "")
                        InfItem.FPVALICMS = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALICMS"].Value, 2);

                    if (dbCommand.Parameters["@FPVALBASEICMSSTDAEGNRE"].Value.ToString() != "")
                        InfItem.FPVALBASEICMSSTDAEGNRE = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALBASEICMSSTDAEGNRE"].Value, 2);

                    if (dbCommand.Parameters["@FPMARGLUCROSTDAEGNRE"].Value.ToString() != "")
                        InfItem.FPMARGLUCROSTDAEGNRE = (double)Math.Round((decimal)dbCommand.Parameters["@FPMARGLUCROSTDAEGNRE"].Value, 2);

                    if (dbCommand.Parameters["@FPPERCICMSSTDAEGNRE"].Value.ToString() != "")
                        InfItem.FPPERCICMSSTDAEGNRE = (double)Math.Round((decimal)dbCommand.Parameters["@FPPERCICMSSTDAEGNRE"].Value, 2);

                    if (dbCommand.Parameters["@FPPRECOLISTASTDAEGNRE"].Value.ToString() != "")
                        InfItem.FPPRECOLISTASTDAEGNRE = (double)Math.Round((decimal)dbCommand.Parameters["@FPPRECOLISTASTDAEGNRE"].Value, 2);

                    if (dbCommand.Parameters["@FPVALICMSSTDAEGNRE"].Value.ToString() != "")
                        InfItem.FPVALICMSSTDAEGNRE = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALICMSSTDAEGNRE"].Value, 2);

                    if (dbCommand.Parameters["@FPVALICMSRETSTDAEGNRE"].Value.ToString() != "")
                        InfItem.FPVALICMSRETSTDAEGNRE = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALICMSRETSTDAEGNRE"].Value, 2);

                    if (dbCommand.Parameters["@UTILIZA_MULTCOMPRA_OUT"].Value.ToString() != "")
                        InfItem.UTILIZA_MULTCOMPRA_OUT = ((string)dbCommand.Parameters["@UTILIZA_MULTCOMPRA_OUT"].Value);

                    if (dbCommand.Parameters["@FPQTDPAUTAICMS"].Value.ToString() != "")
                        InfItem.FPQTDPAUTAICMS = (double)Math.Round((decimal)dbCommand.Parameters["@FPQTDPAUTAICMS"].Value, 2);

                    if (dbCommand.Parameters["@FPVALPAUTAICMS"].Value.ToString() != "")
                        InfItem.FPVALPAUTAICMS = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALPAUTAICMS"].Value, 2);

                    if (dbCommand.Parameters["@FPQTDPAUTAIPI"].Value.ToString() != "")
                        InfItem.FPQTDPAUTAIPI = (double)Math.Round((decimal)dbCommand.Parameters["@FPQTDPAUTAIPI"].Value, 2);

                    if (dbCommand.Parameters["@FPVALPAUTAIPI"].Value.ToString() != "")
                        InfItem.FPVALPAUTAIPI = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALPAUTAIPI"].Value, 2);

                    if (dbCommand.Parameters["@FPQTDPAUTAPIS"].Value.ToString() != "")
                        InfItem.FPQTDPAUTAPIS = (double)Math.Round((decimal)dbCommand.Parameters["@FPQTDPAUTAPIS"].Value, 2);

                    if (dbCommand.Parameters["@FPVALPAUTAPIS"].Value.ToString() != "")
                        InfItem.FPVALPAUTAPIS = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALPAUTAPIS"].Value, 2);

                    if (dbCommand.Parameters["@FPQTDPAUTACOFINS"].Value.ToString() != "")
                        InfItem.FPQTDPAUTACOFINS = (double)Math.Round((decimal)dbCommand.Parameters["@FPQTDPAUTACOFINS"].Value, 2);

                    if (dbCommand.Parameters["@FPVALPAUTACOFINS"].Value.ToString() != "")
                        InfItem.FPVALPAUTACOFINS = (double)Math.Round((decimal)dbCommand.Parameters["@FPVALPAUTACOFINS"].Value, 2);
                }
                catch
                {
                    erro = "Erro no calculo dos Impostos.";
                }
            }
        }

        public string atualizaHistorico(string empcod,string pedVendaNum,string novoHistorico)
        {
            string erro = "";
            string strSql = "";
                       
            funcoes mdlFuncoes = new funcoes();
            enviarEmail mdlEnviaEmail = new enviarEmail();

            strSql = "update TEXTO_PED_VENDA set PedVendaTextoHist ='" + novoHistorico + "' where PedVendaNum = '" + pedVendaNum + "' and EmpCod='" + empcod + "'";

            try
            {
                mdlFuncoes.ExecutaSQL(strSql);
            }
            catch
            {
                erro = "Erro ao atualizar Pedido.";
            }

            return erro;
        }

        public int BuscaPosicaoUnidadeMedida(string ProdCodEstr)
        {
            int UnidMedPos;
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                //Chama procedure para buscar número do pedido
                SqlCommand dbCommand = new SqlCommand("LOCALIZA_PRODUTO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@PRODUTOIN", SqlDbType.VarChar, 30, "PRODUTOIN"));
                dbCommand.Parameters.Add(new SqlParameter("@CHAMOU", SqlDbType.VarChar, 30, "CHAMOU"));
                dbCommand.Parameters.Add(new SqlParameter("@PRODUTOOUT", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "PRODUTOOUT", DataRowVersion.Default, null));
                dbCommand.Parameters.Add(new SqlParameter("@posicao", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "posicao", DataRowVersion.Default, null));
                dbCommand.Parameters.Add(new SqlParameter("@UNIDADE", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "UNIDADE", DataRowVersion.Default, null));
                dbCommand.Parameters.Add(new SqlParameter("@mensagem", SqlDbType.VarChar, 255, ParameterDirection.Output, false, 0, 0, "mensagem", DataRowVersion.Default, null));

                dbCommand.Parameters[0].Value = ProdCodEstr;
                dbCommand.Parameters[1].Value = "VENDAS";

                dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                dbCommand.ExecuteNonQuery();
                UnidMedPos = ((int)dbCommand.Parameters["@posicao"].Value);
            }

            return UnidMedPos;
        }

        public string gravaPedidoBloqueadoItem(string empresa, string PedVendaNum, string codigoProduto, float valorOriginal, int numseq, string operacao, string situacao)
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("USER_SP_GravaPedidoBloqueado", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@empcod", SqlDbType.VarChar, 15, "empcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pedido", SqlDbType.VarChar, 10, "Pedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@codigoProduto", SqlDbType.VarChar, 30, "codigoProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@valorOriginal", SqlDbType.Decimal, 0, "valorOriginal"));
                    dbCommand.Parameters.Add(new SqlParameter("@ItPedVendaSeq", SqlDbType.SmallInt, 0, "ItPedVendaSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 30, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@situacao", SqlDbType.VarChar, 30, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters[0].Value = empresa;
                    dbCommand.Parameters[1].Value = PedVendaNum;
                    dbCommand.Parameters[2].Value = codigoProduto;
                    dbCommand.Parameters[3].Value = valorOriginal;
                    dbCommand.Parameters[4].Value = numseq;
                    dbCommand.Parameters[5].Value = operacao;
                    dbCommand.Parameters[6].Value = situacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch
                {
                    erro = "Ocorreu um problema ao gravar tabela de bloqueados.";
                }

            }
            return erro;
        }


    }
}