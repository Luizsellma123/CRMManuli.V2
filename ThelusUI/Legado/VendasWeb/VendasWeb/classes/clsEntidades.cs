using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace VendasWeb.GerencialVendas
{

    public class clsEntidades : clsConexao
    {
        funcoes mdlFuncoes = new funcoes();
        enviarEmail ObjEnviarEmail = new enviarEmail();
        GerencialVendas.BDClass BDClass = new GerencialVendas.BDClass();

        public string consulta { get; set; }


        public string TipoOperacao { get; set; }
        public List<GerencialVendas.clsEntFone> ListEntFone { get; set; }
        public List<GerencialVendas.clsEntWeb> ListEntWeb { get; set; }
        public List<GerencialVendas.ContatoClass> ListContatoClass { get; set; }
        public List<GerencialVendas.DocEntidadeClass> ListDocEntidadeClass { get; set; }
        public List<GerencialVendas.clsCondPag> ListCondPag { get; set; }
        public List<GerencialVendas.VendedorClass> ListVendEnt { get; set; }
        public List<GerencialVendas.EntRelacionamentoClass> ListEntRelacionamentoclass { get; set; }
        public List<GerencialVendas.EntidadeCategoriaClass> ListEntCategoriaClass { get; set; }
        public List<GerencialVendas.EntConcorrenciaClass> ListEntConcorrenciaClass { get; set; }
        public List<GerencialVendas.EntPerfilDeConsumoClass> ListEntPerfilDeConsumoClass { get; set; }

        public EnderecoEntregaClass EnderecoEntregaClass { get; set; }
        public Boolean FinsFiscais { get; set; }
        public string UsuCod { get; set; }
        public string VendCod { get; set; }
        public string VendClasseCod { get; set; }
        public string NovoVendCod { get; set; }
        public string Origem { get; set; }
        public string Msg { get; set; }

        public string CNAE_P { get; set; }
        public string CNAE_S { get; set; }
        public string StatEntCompra { get; set; }
        public int OrdenRoterizacao { get; set; }

        public string AssuntoEmail { get; set; }
        //public int CodCategInterno { get; set; }


        #region Dados WorkFlow

        public string Prazo { get; set; }
        public string UsuEmail { get; set; }
        public string Remetente { get; set; }
        public string DescricaoEmail { get; set; }
        public string Texto { get; set; }
        public string OperacaoEmail { get; set; }

        #endregion

        #region Vareaveis para Consulta

        public string ProdCodEstr { get; set; }
        public string USERLINHAPRODUTOLISTA { get; set; }


        public int FaturamentoMedioFitaInicial { get; set; }
        public int FaturamentoMedioFitaFinal { get; set; }
        public int FaturamentoMedioStretchInicial { get; set; }
        public int FaturamentoMedioStretchFinal { get; set; }

        public string CodigoCategoria { get; set; }
        public string CodigoEvento { get; set; }


        public string PeriodoCompraInicial { get; set; }
        public string PeriodoCompraFinal { get; set; }


        #endregion

        //public string NovaLoja { get; set; }

        #region Tabela Entidade
        public string EmpCod { get; set; }
        public string EntCod { get; set; }
        public string TipoTratCod { get; set; }
        public string EntNome { get; set; }
        public string EntNomeFant { get; set; }
        public string AtivEconCodEstr { get; set; }
        public string OrigCodEstr { get; set; }
        public DateTime EntDesdeData { get; set; }
        public DateTime EntDataCad { get; set; }
        public DateTime NFDataEmis { get; set; }
        public string EntLograd { get; set; }
        public string EntEnder { get; set; }
        public string EntEnderNo { get; set; }
        public string EntEnderNoPI { get; set; }
        public string EntEnderComp { get; set; }
        public string EntBair { get; set; }
        public string CidCod { get; set; }
        public string EntCep { get; set; }
        public string EntTipoFJ { get; set; }
        public string EntCpfCgc { get; set; }
        public string EntRgIe { get; set; }
        public string EntRgOrgExped { get; set; }
        public string EntAgrop { get; set; }
        public string EntAgropInsc { get; set; }
        public string EntCxaPost { get; set; }
        public string RegCodEstr { get; set; }
        public string EntConceito { get; set; }
        public string CondPagCod { get; set; }
        public string EntAltCondPag { get; set; }
        public string TipoCobCod { get; set; }
        public DateTime EntDataAnivFund { get; set; }
        public string CargoCodEstr { get; set; }
        public string EntGenero { get; set; }
        public string EntComunEtiq { get; set; }
        public string EntComunMaladir { get; set; }
        public string EntComunEMail { get; set; }
        public string EntComunFaxmark { get; set; }
        public string EntComunTlmark { get; set; }
        public string EntStrDesc { get; set; }
        public decimal EntPercDesc { get; set; }
        public DateTime EntDataValidDesc { get; set; }
        public string EntStrAcresc { get; set; }
        public decimal EntPercAcresc { get; set; }
        public DateTime EntDataValidAcresc { get; set; }
        public DateTime EntContatoAposData { get; set; }
        public string BcoNum { get; set; }
        public string AgNum { get; set; }
        public string EntBcoAgCamCompens { get; set; }
        public string EntBcoAgCCorNum { get; set; }
        public decimal EntValLimCred { get; set; }
        public decimal SaldoLimiteCliente { get; set; }
        public decimal EntValLimDeb { get; set; }
        public string EntIdioma1 { get; set; }
        public string EntIdioma1Niv { get; set; }
        public string EntIdioma1Conhec { get; set; }
        public string EntIdioma2 { get; set; }
        public string EntIdioma2Niv { get; set; }
        public string EntIdioma2Conhec { get; set; }
        public string EntIdioma3 { get; set; }
        public string EntIdioma3Niv { get; set; }
        public string EntIdioma3Conhec { get; set; }
        public string EntIdioma4 { get; set; }
        public string EntIdioma4Niv { get; set; }
        public string EntIdioma4Conhec { get; set; }
        public string EntIdioma5 { get; set; }
        public string EntIdioma5Niv { get; set; }
        public string EntIdioma5Conhec { get; set; }
        public string EntNumMatchCode { get; set; }
        public string EntLocCobrancaOMesmo { get; set; }
        public string EntLocEntregaOMesmo { get; set; }
        public string EntTransporteOMesmo { get; set; }
        public string EntTranspCod { get; set; }
        public string EntOficializ { get; set; }
        public string EntNaturCidCod { get; set; }
        public string EntSegNacionPaisSigla { get; set; }
        public string EntPassapNum { get; set; }
        public DateTime EntPassapDataValid { get; set; }
        public string EntFumante { get; set; }
        public string EntAlergiaPor { get; set; }
        public string EntEstCivil { get; set; }
        public string EntVeget { get; set; }
        public string EntPrefSemAnim { get; set; }
        public string EntPrefSemCrianc { get; set; }
        public string EntNomePai { get; set; }
        public string EntNomeMae { get; set; }
        public string EntEmpPagaDespTotHospede { get; set; }
        public string EntPossuiFilho { get; set; }
        public string EntMoraCom { get; set; }
        public string EntGrauEscol { get; set; }
        public string EntHabitoLazer1 { get; set; }
        public string EntHabitoLazer2 { get; set; }
        public string EntHabitoLazer3 { get; set; }
        public string EntTexto { get; set; }
        public string NIVCOD { get; set; }
        public string NivNome { get; set; }
        public string EntInscSuframa { get; set; }
        public string EntNat { get; set; }
        public string EntInscIata { get; set; }
        public string EntInscEmbratur { get; set; }
        public string IndEconCod { get; set; }
        public string EntRespDesp { get; set; }
        public int EntQtdDepend { get; set; }
        public string EntUtilizaSoTabPvRelac { get; set; }
        public string ENTFATURAMOMESMO { get; set; }
        public string EntPermAgrupBoleto { get; set; }
        public DateTime EntDataUltBalancoPat { get; set; }
        public DateTime EntDataValidCartCnpjCpf { get; set; }
        public DateTime EntDataValidCertFazFed { get; set; }
        public DateTime EntDataValidCertFazEst { get; set; }
        public DateTime EntDataValidCertFazMun { get; set; }
        public DateTime EntDataValidCertDivAtivaUniao { get; set; }
        public DateTime EntDataValidCertFgts { get; set; }
        public DateTime EntDataValidCertInss { get; set; }
        public DateTime EntDataValidCertFalencConcord { get; set; }
        public DateTime EntDataValidCertRegProfis { get; set; }
        public decimal EntValCapSocial { get; set; }
        public decimal EntValLiqGeral { get; set; }
        public decimal EntValLiqSeca { get; set; }
        public decimal EntValLiqCorrente { get; set; }
        public decimal EntValGrauEndiv { get; set; }
        public decimal EntValFatorInsolv { get; set; }
        public string EntTextoHist { get; set; }
        public string EntStatCartCnpjCpf { get; set; }
        public string EntSitPrestServRefIss { get; set; }
        public string TurnoTrabCod { get; set; }
        public string EntBcoNumCaucao { get; set; }
        public string EntAgNumCaucao { get; set; }
        public string EntCtrlMovPedTur { get; set; }
        public string EntAplicaAcresValParcTabFin { get; set; }
        public string EntOptanteSimples { get; set; }
        public string ClasseEntCod { get; set; }
        public int EntQtdDiasEntrega { get; set; }
        public string EntHttpImage { get; set; }
        public string EntIncIpiBaseIcmsPad { get; set; }
        public decimal EntPercCofinsFormPv { get; set; }
        public string EntTurGeraFin { get; set; }
        public string EntNatGov { get; set; }
        public string EntNaoRetCofinsAmparPor { get; set; }
        public string EntNaoRetCsllAmparPor { get; set; }
        public string EntNaoRetPisAmparPor { get; set; }
        public string TipoEtapaCodEstr { get; set; }
        public string EntOptanteSimplesFed { get; set; }
        public string EntInscMunic { get; set; }
        public string EntCodGrupoBloq { get; set; }
        public decimal EntPercDescComisVendaDir { get; set; }
        public string EntCEI { get; set; }
        public string EntCalcIrrf { get; set; }
        public decimal EntFatorSegA { get; set; }
        public decimal EntFatorSegB { get; set; }
        public decimal EntFatorSegC { get; set; }
        public string NatJurCod { get; set; }
        public string ENTBCONUMDEPOSITO { get; set; }
        public string ENTAGNUMDEPOSITO { get; set; }
        public string NivCodIndireta { get; set; }
        public string EntExportArqFat { get; set; }
        public string EntIncluiIcmsPrecoVenda { get; set; }
        public string EntIncluiISSPrecoVenda { get; set; }
        public string EntObrigEntParcVendaWeb { get; set; }
        public string ENTCONSIDQTDMINFAT { get; set; }
        public string EntHabTelaEntVendaWeb { get; set; }
        public string EntCodAlt { get; set; }
        public string EntUtilizDescISS { get; set; }
        public string EntUtilizDescICMS { get; set; }
        public string EntDesconsidValMinIrrf { get; set; }
        public int USERBcoNum { get; set; }
        public string USERBcoNome { get; set; }
        public string USERAgencia { get; set; }
        public string USERCidUF { get; set; }
        public string USERCtaNum { get; set; }
        public string USERBcoNum2 { get; set; }
        public string StatEntCod { get; set; }
        public string StatEntComercial { get; set; }
        public string EntStatDescr { get; set; }
        public string EntHttpImageRelatorio { get; set; }
        public string EntVisualizaTelaEstqTerc { get; set; }
        public string EntVisualizaTelaUltVendas { get; set; }
        public string ENTTIPOVISUALPACOTE { get; set; }
        public string EntRegExportado { get; set; }
        public string EntVerificaBloqLib { get; set; }
        public string EquipCodEstr { get; set; }
        public string EntIgnoraHomForn { get; set; }
        public string EntIncluiValFretePrecoLista { get; set; }
        public string EntStatFreteVenda { get; set; }
        public string EntInfFinCodigo { get; set; }
        public string EntInfFinAvaliacao { get; set; }
        public string EntInfFinProblema { get; set; }
        public string EntInfFinCancelamento { get; set; }
        public string EntInfFinAdicional { get; set; }
        public string EntInfFinEmpRep { get; set; }
        public string EntInfMailingManual { get; set; }
        public string EntInfMailingConvite1 { get; set; }
        public string EntInfMailingConvite2 { get; set; }
        public string EntInfMailingCracha1 { get; set; }
        public string EntInfMailingCracha2 { get; set; }
        public string EntInfMailingCracha3 { get; set; }
        public string EntInfMailingCracha4 { get; set; }
        public string EntInfMailingNewsletter { get; set; }
        public string EntInfMailingEtiquetas { get; set; }
        public string EntInfMailingOutrasInf1 { get; set; }
        public string EntInfMailingOutrasInf2 { get; set; }
        public string ENTRECEBENEWSLETTER { get; set; }
        public int CIDSIGLAGDS { get; set; }//Prazo De Entreda - PrazoEntega
        #endregion

        #region Tabela Entidade1
        public int UserShelfLife { get; set; }
        public string UserOutrosCondPagCod { get; set; }
        public decimal UserPrevisaoFaturamentoMes { get; set; }
        public decimal UserValorPrimeiraCompra { get; set; }

        public string EntCnh { get; set; }
        public string EntAgReguladoAnp { get; set; }
        public string EntCodInstalacaoAnp { get; set; }
        public string CondPagCodPag { get; set; }
        public string EntAplicDesc { get; set; }
        public string EntAplicAcresc { get; set; }
        public string EntPermAcresFinancReservTur { get; set; }
        public string EntPermDescEspecReservTur { get; set; }
        public string EntCodGrupo { get; set; }
        public string EntNomeProd { get; set; }
        public string EntNomeProdAlt { get; set; }
        public string EntCRM { get; set; }
        public int ENTQTDDIASATRASO { get; set; }
        public string EntLocColetaOMesmo { get; set; }
        public string EntPosGuia { get; set; }
        public DateTime EntPercCobReent { get; set; }
        public string EntTpDtEscalaCresc { get; set; }
        public string EntTpPerEscalaCresc { get; set; }
        public string EntEscalaCrescDedICMSST { get; set; }
        public string EntEscalaCrescDedIPI { get; set; }
        public string EntEscalaCrescFxCod { get; set; }
        public string EntVisMontCargaSeparada { get; set; }
        public string EntVerificValLimiteCond { get; set; }
        public string EntNumBeneficio { get; set; }
        public string EntVisualDadBancOnLine { get; set; }
        public string EntIntObjVenda { get; set; }
        public string EntIntComis { get; set; }
        public decimal EntLongitudeDecimal { get; set; }
        public string EntLongitudePadrao { get; set; }
        public decimal EntLatitudeDecimal { get; set; }
        public string EntLatitudePadrao { get; set; }
        public string EntDaeGnrePagar { get; set; }
        public string EntNumContaDeposito { get; set; }
        public string EntNumANTT { get; set; }
        public string EntCertificada { get; set; }
        public string EntVisPocket { get; set; }
        public string EntPathRelatOrc { get; set; }
        public string EntPathRelatPed { get; set; }
        public decimal EntPercCargTribMed { get; set; }
        public string AgNumBolMP { get; set; }
        public string BcoNumBolMP { get; set; }
        public int EntQtdDiasVendBoletoMP { get; set; }
        public string EntCodAgenInteg { get; set; }
        public string EntUtilWTVB2B { get; set; }
        public string EntGeraComisWTV { get; set; }
        public string EntDeclaracaoEntrg { get; set; }
        public string ExcPisCofinsCod { get; set; }
        public string EntCalcRetINSRF59405 { get; set; }
        public string EntPadDedPISCOFINSParc { get; set; }
        public string EntMotDesonICMS { get; set; }
        public string ENTCONVENIO { get; set; }
        public string EntQtdDiasVencBoletoMP { get; set; }
        public string EntFormFat { get; set; }
        public string EntFormGerDup { get; set; }
        public decimal EntMargErroLongLatGPS { get; set; }
        public string UserEntFinalidadeProduto { get; set; }

        public string UserTipoTributacao { get; set; }
        public string UserSuspencaoIPI { get; set; }
        public string UserDiferimentoICMS { get; set; }
        public string UserDiferimentoPIS { get; set; }
        public string UserDiferimentoCOFINS { get; set; }
        #endregion

        #region Fiscal
        public int Codigo { get; set; }
        public string ObsLogistica { get; set; }
        public string UsuCartaoCnpj { get; set; }
        public string UsuSintegra { get; set; }
        public string Suframa { get; set; }
        public string userTipoTributacao { get; set; }
        public string userSuspencaoIPI { get; set; }
        public string userDiferimentoICMS { get; set; }
        public string userDiferimentoPIS { get; set; }
        public string userDiferimentoCofins { get; set; }
        #endregion

        #region Campos de Endereco

        public string CepCod { get; set; }
        public string CepSeq { get; set; }
        public string CepNomeLoc { get; set; }
        public string TipoLogradAbrev { get; set; }
        public string CepEnderLoc { get; set; }
        public string CepEnderLocComp { get; set; }
        public string CepCidCodCorreio { get; set; }
        public string CepCidCodCorreioSubord { get; set; }
        public string CepBair1 { get; set; }
        public string CepBairComp1 { get; set; }
        public string CepBair2 { get; set; }
        public string CepBairComp2 { get; set; }
        public string CepSitLoc { get; set; }
        public string CepTabOrig { get; set; }
        public string CIDCOD { get; set; }
        public string CIDNOME { get; set; }
        public string CidNomeComp { get; set; }
        public string CIDNATUR { get; set; }
        public string UFSIGLA { get; set; }
        public string CIDDDD { get; set; }
        public string UFMASCINSCEST { get; set; }
        public string UFMASCINSCAGROP { get; set; }
        public string UFNATUR { get; set; }
        public string PAISSIGLA { get; set; }
        public string PAISNACION { get; set; }
        public string REGCODESTR { get; set; }

        #endregion

        #region Regime Especial
        public string RegEspecNum { get; set; }
        #endregion

        #region Tabela USER_TB_ENT_INDICACAO
        public string TipoIndicacao { get; set; }
        public string Descricao { get; set; }
        #endregion

        #region Tabela de Preco
        public string TabPVCod { get; set; }
        #endregion

        #region Objeto
        public string objcodestrniv { get; set; }
        public string ObjCodEstr { get; set; }
        public string ObjCodEstr1 { get; set; }
        public string ObjCodEstr2 { get; set; }
        public string ObjCodEstr3 { get; set; }
        #endregion

        #region Ent_Categ
        public string CategCodEstr { get; set; }
        #endregion

        #region Perfil_Consumo_Cliente
        public string LinhaConsumoCliente { get; set; }
        public double QuantidadeConsumoCliente { get; set; }
        public string DescricaoConsumoCliente { get; set; }
        #endregion

        #region Ent Relacionamento
        public string DescricaoRelacionamento { get; set; }
        public string DataRelacionamento { get; set; }
        #endregion

        #region Concorrencia_Cliente
        public string NomeConcorrente { get; set; }
        public string ObservacaoConcorrente { get; set; }
        #endregion


        #region Anexos
        public string DocEntPathArq { get; set; }
        public string DocEntObs { get; set; }
        public byte[] DocEntImage { get; set; }
        public int USER_TB_Tipos_AnexosID { get; set; }
        #endregion

        #region Funcoes Gerais Entidades
        public string Incluir_Entidade_Geral()
        {

            string Retorno = "";

            if (EntCod == null)
                EntCod = "";

            if (EntInscSuframa == null)
                EntInscSuframa = "";

            if (CondPagCodPag == null)
                CondPagCodPag = "";

            if (CondPagCod == null)
                CondPagCod = "";

            if (RegEspecNum == null)
                RegEspecNum = "";

            if (objcodestrniv == null)
                objcodestrniv = "";

            if (EntTexto == null)
                EntTexto = "";

            if (EntTextoHist == null)
                EntTextoHist = "";

            if (NIVCOD == null)
                NIVCOD = "";

            if (EntNat == null)
                EntNat = "";

            if (TipoCobCod == null)
                TipoCobCod = "";

            if (EntInscMunic == null)
                EntInscMunic = "";

            if (StatEntCod == null)
                StatEntCod = "";

            if (EntStatDescr == null)
                EntStatDescr = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_INSERE_ENTIDADE_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 100, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 40, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntLograd", SqlDbType.VarChar, 10, "EntLograd"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnder", SqlDbType.VarChar, 40, "EntEnder"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNo", SqlDbType.VarChar, 6, "EntEnderNo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNoPI", SqlDbType.VarChar, 5, "EntEnderNoPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderComp", SqlDbType.VarChar, 40, "EntEnderComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntBair", SqlDbType.VarChar, 30, "EntBair"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 8, "CidCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCep", SqlDbType.VarChar, 9, "EntCep"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTipoFJ", SqlDbType.VarChar, 10, "EntTipoFJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 14, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntInscSuframa", SqlDbType.VarChar, 50, "EntInscSuframa"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCodPag", SqlDbType.VarChar, 50, "CondPagCodPag"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCod", SqlDbType.VarChar, 50, "CondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@RegEspecNum", SqlDbType.VarChar, 30, "RegEspecNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 30, "VendCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@objcodestrniv", SqlDbType.VarChar, 100, "objcodestrniv"));

                    dbCommand.Parameters.Add(new SqlParameter("@EntTexto", SqlDbType.VarChar, 8000, "EntTexto"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTextoHist", SqlDbType.VarChar, 8000, "EntTextoHist"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntRgIe", SqlDbType.VarChar, 8000, "EntRgIe"));

                    dbCommand.Parameters.Add(new SqlParameter("@CategCodEstr", SqlDbType.VarChar, 100, "CategCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@NIVCOD", SqlDbType.VarChar, 100, "NIVCOD"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNat", SqlDbType.VarChar, 100, "EntNat"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoCobCod", SqlDbType.VarChar, 100, "TipoCobCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@EntInscMunic", SqlDbType.VarChar, 100, "EntInscMunic"));

                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 10, "StatEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntStatDescr", SqlDbType.VarChar, 50, "EntStatDescr"));

                    dbCommand.Parameters.Add(new SqlParameter("@ObsLogistica", SqlDbType.VarChar, 8000, "ObsLogistica"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCartaoCNPJ", SqlDbType.VarChar, 3, "UsuCartaoCNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuSintegra", SqlDbType.VarChar, 3, "UsuSintegra"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntNome"].Value = EntNome;
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant;
                    dbCommand.Parameters["@EntLograd"].Value = EntLograd;
                    dbCommand.Parameters["@EntEnder"].Value = EntEnder;
                    dbCommand.Parameters["@EntEnderNo"].Value = EntEnderNo;
                    dbCommand.Parameters["@EntEnderNoPI"].Value = EntEnderNoPI;
                    dbCommand.Parameters["@EntEnderComp"].Value = EntEnderComp;
                    dbCommand.Parameters["@EntBair"].Value = EntBair;
                    dbCommand.Parameters["@CidCod"].Value = CidCod;
                    dbCommand.Parameters["@EntCep"].Value = EntCep;
                    dbCommand.Parameters["@EntTipoFJ"].Value = EntTipoFJ;
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;
                    dbCommand.Parameters["@EntInscSuframa"].Value = EntInscSuframa;
                    dbCommand.Parameters["@CondPagCodPag"].Value = CondPagCodPag;
                    dbCommand.Parameters["@CondPagCod"].Value = CondPagCod;

                    dbCommand.Parameters["@RegEspecNum"].Value = RegEspecNum;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;

                    dbCommand.Parameters["@objcodestrniv"].Value = objcodestrniv;

                    dbCommand.Parameters["@EntTexto"].Value = EntTexto;
                    dbCommand.Parameters["@EntTextoHist"].Value = EntTextoHist;
                    dbCommand.Parameters["@EntRgIe"].Value = EntRgIe;

                    dbCommand.Parameters["@CategCodEstr"].Value = CategCodEstr;
                    dbCommand.Parameters["@NIVCOD"].Value = NIVCOD;
                    dbCommand.Parameters["@EntNat"].Value = EntNat;
                    dbCommand.Parameters["@TipoCobCod"].Value = TipoCobCod;
                    dbCommand.Parameters["@EntInscMunic"].Value = EntInscMunic;

                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod;
                    dbCommand.Parameters["@EntStatDescr"].Value = EntStatDescr;

                    dbCommand.Parameters["@ObsLogistica"].Value = ObsLogistica;
                    dbCommand.Parameters["@UsuCartaoCNPJ"].Value = UsuCartaoCnpj;
                    dbCommand.Parameters["@UsuSintegra"].Value = UsuSintegra;

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
                        Retorno = "Erro na Funcao Incluir_Entidade_Geral";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Incluir_Entidade_Geral. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Altera_Entidade_Geral()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_ENTIDADE_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 100, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 40, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntLograd", SqlDbType.VarChar, 10, "EntLograd"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnder", SqlDbType.VarChar, 40, "EntEnder"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNo", SqlDbType.VarChar, 6, "EntEnderNo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNoPI", SqlDbType.VarChar, 5, "EntEnderNoPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderComp", SqlDbType.VarChar, 40, "EntEnderComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntBair", SqlDbType.VarChar, 30, "EntBair"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 8, "CidCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCep", SqlDbType.VarChar, 9, "EntCep"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTipoFJ", SqlDbType.VarChar, 10, "EntTipoFJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 14, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntInscSuframa", SqlDbType.VarChar, 50, "EntInscSuframa"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCodPag", SqlDbType.VarChar, 50, "CondPagCodPag"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCod", SqlDbType.VarChar, 50, "CondPagCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@RegEspecNum", SqlDbType.VarChar, 30, "RegEspecNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 30, "VendCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@objcodestrniv", SqlDbType.VarChar, 100, "objcodestrniv"));

                    dbCommand.Parameters.Add(new SqlParameter("@EntTexto", SqlDbType.VarChar, 8000, "EntTexto"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTextoHist", SqlDbType.VarChar, 8000, "EntTextoHist"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntRgIe", SqlDbType.VarChar, 8000, "EntRgIe"));

                    dbCommand.Parameters.Add(new SqlParameter("@CategCodEstr", SqlDbType.VarChar, 100, "CategCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@NIVCOD", SqlDbType.VarChar, 100, "NIVCOD"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNat", SqlDbType.VarChar, 100, "EntNat"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoCobCod", SqlDbType.VarChar, 100, "TipoCobCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntInscMunic", SqlDbType.VarChar, 100, "EntInscMunic"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 100, "StatEntCod"));


                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntNome"].Value = EntNome;
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant;
                    dbCommand.Parameters["@EntLograd"].Value = EntLograd;
                    dbCommand.Parameters["@EntEnder"].Value = EntEnder;
                    dbCommand.Parameters["@EntEnderNo"].Value = EntEnderNo;
                    dbCommand.Parameters["@EntEnderNoPI"].Value = EntEnderNoPI;
                    dbCommand.Parameters["@EntEnderComp"].Value = EntEnderComp;
                    dbCommand.Parameters["@EntBair"].Value = EntBair;
                    dbCommand.Parameters["@CidCod"].Value = CidCod;
                    dbCommand.Parameters["@EntCep"].Value = EntCep;
                    dbCommand.Parameters["@EntTipoFJ"].Value = EntTipoFJ;
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;
                    dbCommand.Parameters["@EntInscSuframa"].Value = EntInscSuframa;
                    dbCommand.Parameters["@CondPagCodPag"].Value = CondPagCodPag;
                    dbCommand.Parameters["@CondPagCod"].Value = CondPagCod;

                    dbCommand.Parameters["@RegEspecNum"].Value = RegEspecNum;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@objcodestrniv"].Value = objcodestrniv;
                    dbCommand.Parameters["@EntTexto"].Value = EntTexto;
                    dbCommand.Parameters["@EntTextoHist"].Value = EntTextoHist;
                    dbCommand.Parameters["@EntRgIe"].Value = EntRgIe;
                    dbCommand.Parameters["@CategCodEstr"].Value = CategCodEstr;
                    dbCommand.Parameters["@NIVCOD"].Value = NIVCOD;
                    dbCommand.Parameters["@EntNat"].Value = EntNat;
                    dbCommand.Parameters["@TipoCobCod"].Value = TipoCobCod;
                    dbCommand.Parameters["@EntInscMunic"].Value = EntInscMunic;
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod;


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
                        Retorno = "Erro na Funcao Altera_Entidade_Geral";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Altera_Entidade_Geral. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable Consulta_Stat_Ent(string TipoEntidade)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_Consulta_Stat_Ent", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@TipoEntidade", SqlDbType.VarChar, 50, "TipoEntidade"));

                    dbCommand.Parameters["@TipoEntidade"].Value = TipoEntidade;

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

        public DataTable Consulta_Stat_Ent_Comercial()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_Consulta_Stat_Ent_Comercial", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


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


        public DataTable Consulta_Stat_Compra()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_Consulta_Stat_Compra", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


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

        public DataTable Consulta_Categoria_Entidade_Geral(string TipoEntidade)
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_CATEGORIA_ENTIDADE_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoEntidade", SqlDbType.VarChar, 50, "TipoEntidade"));

                    dbCommand.Parameters["@TipoEntidade"].Value = TipoEntidade;

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

        public string Consulta_Categoria_Entidade_Selecionada(string TipoEntidade, string EntCod)
        {
            string CategEnt = "";
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_CATEGORIA_ENTIDADE_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoEntidade", SqlDbType.VarChar, 50, "TipoEntidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 10, "EntCod"));

                    dbCommand.Parameters["@TipoEntidade"].Value = TipoEntidade;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    CategEnt = Convert.ToString(dbCommand.ExecuteScalar());
                }
            }
            catch
            {

            }
            return CategEnt;
        }

        public string Consulta_Categoria_Usuario(string TipoEntidade, string EntCod)
        {
            string CategEnt = "";
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_CATEGORIA_ENTIDADE_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoEntidade", SqlDbType.VarChar, 50, "TipoEntidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 10, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 31, "UsuCod"));

                    dbCommand.Parameters["@TipoEntidade"].Value = TipoEntidade;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                    CategEnt = Convert.ToString(dbCommand.ExecuteScalar());
                }
            }
            catch
            {

            }
            return CategEnt;
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
                    Console.WriteLine("Erro no método Lista_Categoria_Usuario_Logado");
                }

                return GrpUsuCod;
            }
        }

        public DataTable Consulta_Vendedor_Entidade_Geral(string TipoEntidade)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_VENDEDOR_ENTIDADE_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoEntidade", SqlDbType.VarChar, 50, "TipoEntidade"));

                    dbCommand.Parameters["@TipoEntidade"].Value = TipoEntidade;

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


        public DataTable Consulta_Condicao_Recebimento_Entidade_Geral(string TipoEntidade)
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_CONDICAO_RECEBIMENTO_ENTIDADE_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoEntidade", SqlDbType.VarChar, 50, "TipoEntidade"));

                    dbCommand.Parameters["@TipoEntidade"].Value = TipoEntidade;

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


        public string Envia_Email_Entidade()
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

                    dbCommand = new SqlCommand("User_SP_Consulta_Email_Cadastro_Entidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoOperacao", SqlDbType.VarChar, 7, "TipoOperacao"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@TipoOperacao"].Value = OperacaoEmail;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            UsuEmail = row["UsuEmail"].ToString();

                            //Texto Email
                            ObjEnviarEmail.Remetente = this.Remetente;
                            ObjEnviarEmail.Descricao = this.DescricaoEmail;
                            ObjEnviarEmail.Texto = this.Texto;

                            //Enviar Email 
                            ObjEnviarEmail.EmailDestinatario = UsuEmail;
                            ObjEnviarEmail.enviarEmails();
                        }
                    }
                    else
                    {
                        //Nenum Email Cadastrado//Texto Email
                        ObjEnviarEmail.Remetente = "Problemas com Email no Cadastro de Entidades";
                        ObjEnviarEmail.Descricao = this.DescricaoEmail;
                        ObjEnviarEmail.Texto = Texto = this.Texto;

                        //Enviar Email 
                        ObjEnviarEmail.EmailDestinatario = "jackson@athelus.com.br";
                        ObjEnviarEmail.enviarEmails();
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Envia_Email_Entidade. Contactar o Suporte!";
            }
            return Retorno;
        }

        public void AdicionarCategoria(EntidadeCategoriaClass NewCategoria)
        {
            //Verifica se esta instanciado
            if (this.ListEntCategoriaClass == null)
            {
                this.ListEntCategoriaClass = new List<EntidadeCategoriaClass>();
            }

            this.ListEntCategoriaClass.Add(NewCategoria);
        }

        public void RemoverCategoria(EntidadeCategoriaClass Categoria)
        {
            for (int i = 0; i < this.ListEntCategoriaClass.Count; i++)
            {
                if (this.ListEntCategoriaClass[i].Codigo == Categoria.Codigo)
                {
                    this.ListEntCategoriaClass.RemoveAt(i);
                }
            }
        }

        public void AlteraCategoria(EntidadeCategoriaClass Categoria)
        {
            for (int i = 0; i < this.ListEntCategoriaClass.Count; i++)
            {
                if (this.ListEntCategoriaClass[i].CategCodEstr == Categoria.CategCodEstr)
                {
                    this.ListEntCategoriaClass.RemoveAt(i);//Remove Antigo
                    this.ListEntCategoriaClass.Add(Categoria);//Adiciona Novo
                }
            }
        }


        public void AdicionarRelacionamento(EntRelacionamentoClass NewRelacionamento)
        {
            //Verifica se esta instanciado
            if (this.ListEntRelacionamentoclass == null)
            {
                this.ListEntRelacionamentoclass = new List<EntRelacionamentoClass>();
            }

            this.ListEntRelacionamentoclass.Add(NewRelacionamento);
        }

        public void RemoverRelacionamento(EntRelacionamentoClass Relacionamento)
        {
            for (int i = 0; i < this.ListEntRelacionamentoclass.Count; i++)
            {
                if (this.ListEntRelacionamentoclass[i].Codigo == Relacionamento.Codigo)
                {
                    this.ListEntRelacionamentoclass.RemoveAt(i);
                }
            }
        }

        public void AdicionarAnexo(DocEntidadeClass NewAnexo)
        {
            //Verifica se esta instanciado
            if (this.ListDocEntidadeClass == null)
            {
                this.ListDocEntidadeClass = new List<DocEntidadeClass>();
            }

            this.ListDocEntidadeClass.Add(NewAnexo);
        }

        public void RemoverAnexo(DocEntidadeClass Anexo)
        {
            for (int i = 0; i < this.ListDocEntidadeClass.Count; i++)
            {
                if (this.ListDocEntidadeClass[i].DocEntSeq == Anexo.DocEntSeq)
                {
                    this.ListDocEntidadeClass.RemoveAt(i);
                }
            }
        }

        public void AdicionarConcorrencia(EntConcorrenciaClass NewConcorrencia)
        {
            //Verifica se esta instanciado
            if (this.ListEntConcorrenciaClass == null)
            {
                this.ListEntConcorrenciaClass = new List<EntConcorrenciaClass>();
            }

            this.ListEntConcorrenciaClass.Add(NewConcorrencia);
        }

        public void RemoverConcorrencia(EntConcorrenciaClass Concorrencia)
        {
            for (int i = 0; i < this.ListEntConcorrenciaClass.Count; i++)
            {
                if (this.ListEntConcorrenciaClass[i].Codigo == Concorrencia.Codigo)
                {
                    this.ListEntConcorrenciaClass.RemoveAt(i);
                }
            }
        }

        public void AdicionarPerfil(EntPerfilDeConsumoClass NewPerfil)
        {
            //Verifica se esta instanciado
            if (this.ListEntPerfilDeConsumoClass == null)
            {
                this.ListEntPerfilDeConsumoClass = new List<EntPerfilDeConsumoClass>();
            }

            this.ListEntPerfilDeConsumoClass.Add(NewPerfil);
        }

        public void RemoverPerfil(EntPerfilDeConsumoClass Perfil)
        {
            for (int i = 0; i < this.ListEntPerfilDeConsumoClass.Count; i++)
            {
                if (this.ListEntPerfilDeConsumoClass[i].Codigo == Perfil.Codigo)
                {
                    this.ListEntPerfilDeConsumoClass.RemoveAt(i);
                }
            }
        }


        public void AdicionarContato(ContatoClass NewContato)
        {
            //Verifica se esta instanciado
            if (this.ListContatoClass == null)
            {
                this.ListContatoClass = new List<ContatoClass>();
            }

            this.ListContatoClass.Add(NewContato);
        }

        public void RemoverContato(ContatoClass Contato)
        {
            for (int i = 0; i < this.ListContatoClass.Count; i++)
            {
                if (this.ListContatoClass[i].ENTCONTATOID == Contato.ENTCONTATOID)
                {
                    /*if (Contato.ENTCONTATOID > 0)
                    {*/
                        if (this.ListContatoClass[i].ENTCONTATOID == Contato.ENTCONTATOID)
                        {
                            this.ListContatoClass.RemoveAt(i);//Remove contato antigo
                            //this.ListContatoClass.Add(Contato);//adiciona o contato novo com operacao igual a remover
                        }
                    /*}
                    else
                    {
                        if (this.ListContatoClass[i].ENTCONTATOID == Contato.ENTCONTATOID)
                        {
                            this.ListContatoClass.RemoveAt(i);
                        }
                    }*/
                }
            }
        }

        public void AlteraContato(ContatoClass Contato)
        {
            for (int i = 0; i < this.ListContatoClass.Count; i++)
            {
                if (Contato.ENTCONTATOID > 0)
                {
                    if (this.ListContatoClass[i].ENTCONTATOID == Contato.ENTCONTATOID)
                    {
                        //this.ListContatoClass[i].TipoOperacao = "Alterar";//Caso ja estaja gravado em Banco
                        Contato.TipoOperacao = "Alterar";
                        this.ListContatoClass.RemoveAt(i);//Remove Antigo
                        this.ListContatoClass.Add(Contato);//Adiciona Novo
                    }
                }
                else
                {
                    if (this.ListContatoClass[i].ENTCONTATOID == Contato.ENTCONTATOID)
                    {
                        //this.ListContatoClass[i].TipoOperacao = "Incluir";//Caso nao esteja gravado em banco
                        Contato.TipoOperacao = "Incluir";
                        this.ListContatoClass.RemoveAt(i);//Remove Antigo
                        this.ListContatoClass.Add(Contato);//Adiciona Novo
                    }
                }
            }
        }


        public void AdicionarEmail(clsEntWeb NewEmail)
        {
            //Verifica se esta instanciado
            if (this.ListEntWeb == null)
            {
                this.ListEntWeb = new List<clsEntWeb>();
            }

            this.ListEntWeb.Add(NewEmail);
        }

        public void AdicionarEntFone(clsEntFone NewEntFone)
        {
            //Verifica se esta instanciado
            if (this.ListEntFone == null)
            {
                this.ListEntFone = new List<clsEntFone>();
            }

            this.ListEntFone.Add(NewEntFone);
        }

        public void Busca_Endereco()
        {
            DataTable outputTable = new DataTable();

            if (CepCod != null)
            {
                outputTable = mdlFuncoes.Consulta_CEP(CepCod);

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        CepCod = row["CepCod"].ToString();
                        CepSeq = row["CepSeq"].ToString();
                        CepNomeLoc = row["CepNomeLoc"].ToString();
                        TipoLogradAbrev = row["TipoLogradAbrev"].ToString();
                        CepEnderLoc = row["CepEnderLoc"].ToString();
                        CepEnderLocComp = row["CepEnderLocComp"].ToString();
                        CidCod = row["CidCod"].ToString();
                        CepCidCodCorreio = row["CepCidCodCorreio"].ToString();
                        CepCidCodCorreioSubord = row["CepCidCodCorreioSubord"].ToString();
                        CepBair1 = row["CepBair1"].ToString();
                        CepBairComp1 = row["CepBairComp1"].ToString();
                        CepBair2 = row["CepBair2"].ToString();
                        CepBairComp2 = row["CepBairComp2"].ToString();
                        CepSitLoc = row["CepSitLoc"].ToString();
                        CepTabOrig = row["CepTabOrig"].ToString();
                        CIDCOD = row["CIDCOD"].ToString();
                        CIDNOME = row["CIDNOME"].ToString();
                        CIDNATUR = row["CIDNATUR"].ToString();
                        UFSIGLA = row["UFSIGLA"].ToString();
                        CIDDDD = row["CIDDDD"].ToString();
                        UFMASCINSCEST = row["UFMASCINSCEST"].ToString();
                        UFMASCINSCAGROP = row["UFMASCINSCAGROP"].ToString();
                        UFNATUR = row["UFNATUR"].ToString();
                        PAISSIGLA = row["PAISSIGLA"].ToString();
                        PAISNACION = row["PAISNACION"].ToString();
                        REGCODESTR = row["REGCODESTR"].ToString();
                    }
                }
            }
        }

        public string Consulta_Cidade()
        {
            string Retorno = "";
            DataTable outputTable = new DataTable();

            outputTable = mdlFuncoes.Mostra_Cidade(CidCod);

            if (outputTable.Rows.Count > 0)
            {
                foreach (DataRow row in outputTable.Rows)
                {
                    Retorno = row["CIDZONAFRANCA"].ToString();
                }
            }
            else
            {
                Retorno = "Não";
            }

            return Retorno;
        }

        public void Gera_Codigo_Entidade()
        {
            DataTable outputTable = new DataTable();

            outputTable = mdlFuncoes.Gera_Codigo("ENTIDADE");

            if (outputTable.Rows.Count > 0)
            {
                foreach (DataRow row in outputTable.Rows)
                {
                    EntCod = row["Codigo"].ToString();
                }
            }
        }

        public DataTable Consulta_Canal()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_consulta_Canal", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable Consulta_Segmento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_consulta_Segmento", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@objcodestrniv", SqlDbType.VarChar, 20, "objcodestrniv"));

                    dbCommand.Parameters["@objcodestrniv"].Value = objcodestrniv;

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

        public DataTable Consulta_Condicao_Recebimento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_CONDICOES_PAGAMENTO_RECEBIMENTO", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 100, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    dbCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable Consulta_Tipo_Cobranca()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_TIPO_COBRANCA", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 100, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    dbCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable Consulta_Tabela_Preco_Ativa()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_TABELA_PRECO_ATIVA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 20, "EmpCod"));

                    dbCommand.Parameters["@EmpCod"].Value = "2";

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

        public bool ValidarInscricaoEstadual(string pUF, string pInscr)
        {
            bool retorno = false;
            string strBase;
            string strBase2;
            string strOrigem;
            string strDigito1;
            string strDigito2;
            int intPos;
            int intValor;
            int intSoma = 0;
            int intResto;
            int intNumero;
            int intPeso = 0;

            strBase = "";
            strBase2 = "";
            strOrigem = "";

            if ((pInscr.Trim().ToUpper() == "ISENTO"))
                return true;

            for (intPos = 1; intPos <= pInscr.Trim().Length; intPos++)
            {
                if ((("0123456789P".IndexOf(pInscr.Substring((intPos - 1), 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    strOrigem = (strOrigem + pInscr.Substring((intPos - 1), 1));
            }

            switch (pUF.ToUpper())
            {
                case "AC":
                    #region

                    strBase = (strOrigem.Trim() + "00000000000").Substring(0, 11);

                    if (strBase.Substring(0, 2) == "01")
                    {
                        intSoma = 0;
                        intPeso = 4;

                        for (intPos = 1; (intPos <= 11); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPeso == 1) intPeso = 9;

                            intSoma += intValor * intPeso;

                            intPeso--;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                        intSoma = 0;
                        strBase = (strOrigem.Trim() + "000000000000").Substring(0, 12);
                        intPeso = 5;

                        for (intPos = 1; (intPos <= 12); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPeso == 1) intPeso = 9;

                            intSoma += intValor * intPeso;
                            intPeso--;
                        }

                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                        strBase2 = (strBase.Substring(0, 12) + strDigito2);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    #endregion

                    break;

                case "AL":

                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "24"))
                    {
                        //24000004-8
                        //98765432
                        intSoma = 0;
                        intPeso = 9;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            intSoma += intValor * intPeso;
                            intPeso--;
                        }

                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);

                        strDigito1 = ((intResto == 10) ? "0" : Convert.ToString(intResto)).Substring((((intResto == 10) ? "0" : Convert.ToString(intResto)).Length - 1));

                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "AM":

                    #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    intPeso = 9;

                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                        intSoma += intValor * intPeso;
                        intPeso--;
                    }

                    intResto = (intSoma % 11);

                    if (intSoma < 11)
                        strDigito1 = (11 - intSoma).ToString();
                    else
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;
                    #endregion

                    break;

                case "AP":

                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intPeso = 9;

                    if ((strBase.Substring(0, 2) == "03"))
                    {
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            intSoma += intValor * intPeso;
                            intPeso--;
                        }

                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);

                        strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));

                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "BA":

                    #region

                    if (strOrigem.Length == 8)
                        strBase = (strOrigem.Trim() + "00000000").Substring(0, 8);
                    else if (strOrigem.Length == 9)
                        strBase = (strOrigem.Trim() + "00000000").Substring(0, 9);

                    if ((("0123458".IndexOf(strBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 8)
                    {
                        #region

                        intSoma = 0;

                        for (intPos = 1; (intPos <= 6); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPos == 1) intPeso = 7;

                            intSoma += intValor * intPeso;
                            intPeso--;
                        }


                        intResto = (intSoma % 10);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));


                        strBase2 = strBase.Substring(0, 7) + strDigito2;

                        if (strBase2 == strOrigem)
                            retorno = true;

                        if (retorno)
                        {
                            intSoma = 0;
                            intPeso = 0;

                            for (intPos = 1; (intPos <= 7); intPos++)
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                                if (intPos == 7)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));

                                if (intPos == 1) intPeso = 8;

                                intSoma += intValor * intPeso;
                                intPeso--;
                            }


                            intResto = (intSoma % 10);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));

                            strBase2 = (strBase.Substring(0, 6) + strDigito1 + strDigito2);

                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }

                        #endregion
                    }
                    else if ((("679".IndexOf(strBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 8)
                    {
                        #region

                        intSoma = 0;

                        for (intPos = 1; (intPos <= 6); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPos == 1) intPeso = 7;

                            intSoma += intValor * intPeso;
                            intPeso--;
                        }


                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));


                        strBase2 = strBase.Substring(0, 7) + strDigito2;

                        if (strBase2 == strOrigem)
                            retorno = true;

                        if (retorno)
                        {
                            intSoma = 0;
                            intPeso = 0;

                            for (intPos = 1; (intPos <= 7); intPos++)
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                                if (intPos == 7)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));

                                if (intPos == 1) intPeso = 8;

                                intSoma += intValor * intPeso;
                                intPeso--;
                            }


                            intResto = (intSoma % 11);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                            strBase2 = (strBase.Substring(0, 6) + strDigito1 + strDigito2);

                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }

                        #endregion
                    }
                    else if ((("0123458".IndexOf(strBase.Substring(1, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 9)
                    {
                        #region
                        /* Segundo digito */
                        //1000003
                        //8765432
                        intSoma = 0;


                        for (intPos = 1; (intPos <= 7); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPos == 1) intPeso = 8;

                            intSoma += intValor * intPeso;
                            intPeso--;
                        }

                        intResto = (intSoma % 10);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));

                        strBase2 = strBase.Substring(0, 8) + strDigito2;

                        if (strBase2 == strOrigem)
                            retorno = true;

                        if (retorno)
                        {
                            //1000003 6
                            //9876543 2
                            intSoma = 0;
                            intPeso = 0;

                            for (intPos = 1; (intPos <= 8); intPos++)
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                                if (intPos == 8)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));

                                if (intPos == 1) intPeso = 9;

                                intSoma += intValor * intPeso;
                                intPeso--;
                            }


                            intResto = (intSoma % 10);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                            strBase2 = (strBase.Substring(0, 7) + strDigito1 + strDigito2);

                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }

                        #endregion
                    }

                    #endregion

                    break;

                case "CE":

                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = 0;

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));

                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion

                    break;

                case "DF":

                    #region

                    strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);

                    if ((strBase.Substring(0, 3) == "073"))
                    {
                        intSoma = 0;
                        intPeso = 2;

                        for (intPos = 11; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 9))
                                intPeso = 2;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 11) + strDigito1);
                        intSoma = 0;
                        intPeso = 2;

                        for (intPos = 12; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 9))
                                intPeso = 2;
                        }

                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 12) + strDigito2);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "ES":

                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion

                    break;

                case "GO":

                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((("10,11,15".IndexOf(strBase.Substring(0, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    {
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }

                        intResto = (intSoma % 11);

                        if ((intResto == 0))
                            strDigito1 = "0";
                        else if ((intResto == 1))
                        {
                            intNumero = int.Parse(strBase.Substring(0, 8));
                            strDigito1 = (((intNumero >= 10103105) && (intNumero <= 10119997)) ? "1" : "0").Substring(((((intNumero >= 10103105) && (intNumero <= 10119997)) ? "1" : "0").Length - 1));
                        }
                        else
                            strDigito1 = Convert.ToString((11 - intResto)).Substring((Convert.ToString((11 - intResto)).Length - 1));

                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "MA":

                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "12"))
                    {
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "MT":
                    #region

                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intSoma = 0;
                    intPeso = 2;

                    for (intPos = 10; intPos >= 1; intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 9))
                            intPeso = 2;
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 10) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion

                    break;
                case "MS":
                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "28"))
                    {
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "MG":

                    #region

                    strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                    strBase2 = (strBase.Substring(0, 3) + ("0" + strBase.Substring(3, 8)));
                    intNumero = 2;

                    string strSoma = "";

                    for (intPos = 1; (intPos <= 12); intPos++)
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intNumero = ((intNumero == 2) ? 1 : 2);
                        intValor = (intValor * intNumero);

                        intSoma = (intSoma + intValor);
                        strSoma += intValor.ToString();
                    }

                    intSoma = 0;

                    //Soma -se os algarismos, não o produto
                    for (int i = 0; i < strSoma.Length; i++)
                    {
                        intSoma += int.Parse(strSoma.Substring(i, 1));
                    }

                    intValor = int.Parse(strBase.Substring(8, 2));
                    strDigito1 = (intValor - intSoma).ToString();

                    strBase2 = (strBase.Substring(0, 11) + strDigito1);

                    if ((strBase2 == strOrigem.Substring(0, 12)))
                        retorno = true;

                    if (retorno)
                    {
                        intSoma = 0;
                        intPeso = 3;

                        for (intPos = 1; (intPos <= 12); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            if (intPeso < 2)
                                intPeso = 11;

                            intSoma += (intValor * intPeso);
                            intPeso--;
                        }

                        intResto = (intSoma % 11);
                        intValor = 11 - intResto;
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                        strBase2 = (strBase.Substring(0, 12) + strDigito2);

                        if (strBase2 == strOrigem)
                            retorno = true;
                    }

                    #endregion

                    break;

                case "PA":

                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "15"))
                    {
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "PB":
                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = 0;

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion

                    break;

                case "PE":
                    #region

                    strBase = (strOrigem.Trim() + "00000000000000").Substring(0, 14);
                    intSoma = 0;
                    intPeso = 2;

                    for (intPos = 7; (intPos >= 1); intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 9))
                            intPeso = 2;
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = (intValor - 10);

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 7) + strDigito1);

                    if ((strBase2 == strOrigem.Substring(0, 8)))
                        retorno = true;

                    if (retorno)
                    {
                        intSoma = 0;
                        intPeso = 2;

                        for (intPos = 8; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 9))
                                intPeso = 2;
                        }

                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);

                        if ((intValor > 9))
                            intValor = (intValor - 10);

                        strDigito2 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito2);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "PI":
                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion
                    break;

                case "PR":
                    #region

                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intSoma = 0;
                    intPeso = 2;

                    for (intPos = 8; (intPos >= 1); intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 7))
                            intPeso = 2;
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    intSoma = 0;
                    intPeso = 2;

                    for (intPos = 9; (intPos >= 1); intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 7))
                            intPeso = 2;
                    }

                    intResto = (intSoma % 11);
                    strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase2 + strDigito2);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion
                    break;

                case "RJ":
                    #region

                    strBase = (strOrigem.Trim() + "00000000").Substring(0, 8);
                    intSoma = 0;
                    intPeso = 2;

                    for (intPos = 7; (intPos >= 1); intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);

                        if ((intPeso > 7))
                            intPeso = 2;
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 7) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion
                    break;

                case "RN": //Verficar com 10 digitos
                    #region

                    if (strOrigem.Length == 9)
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    else if (strOrigem.Length == 10)
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 10);

                    if ((strBase.Substring(0, 2) == "20") && strBase.Length == 9)
                    {
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }

                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto > 9) ? "0" : Convert.ToString(intResto)).Substring((((intResto > 9) ? "0" : Convert.ToString(intResto)).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    else if (strBase.Length == 10)
                    {
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 9); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (11 - intPos));
                            intSoma = (intSoma + intValor);
                        }

                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto > 10) ? "0" : Convert.ToString(intResto)).Substring((((intResto > 10) ? "0" : Convert.ToString(intResto)).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "RO":
                    #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    strBase2 = strBase.Substring(3, 5);
                    intSoma = 0;

                    for (intPos = 1; (intPos <= 5); intPos++)
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intValor = (intValor * (7 - intPos));
                        intSoma = (intSoma + intValor);
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = (intValor - 10);

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;
                    #endregion
                    break;


                case "RR":
                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);

                    if ((strBase.Substring(0, 2) == "24"))
                    {
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = intValor * intPos;
                            intSoma += intValor;
                        }

                        intResto = (intSoma % 9);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "RS":
                    #region

                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intNumero = int.Parse(strBase.Substring(0, 3));

                    if (((intNumero > 0) && (intNumero < 468)))
                    {
                        intSoma = 0;
                        intPeso = 2;

                        for (intPos = 9; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 9))
                                intPeso = 2;
                        }

                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);

                        if ((intValor > 9))
                            intValor = 0;

                        strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;

                case "SC":
                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }

                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;
                    #endregion

                    break;

                case "SE":
                    #region

                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;

                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }

                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);

                    if ((intValor > 9))
                        intValor = 0;

                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion

                    break;

                case "SP":
                    #region

                    if ((strOrigem.Substring(0, 1) == "P"))
                    {
                        strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                        strBase2 = strBase.Substring(1, 8);
                        intSoma = 0;
                        intPeso = 1;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso == 2))
                                intPeso = 3;

                            if ((intPeso == 9))
                                intPeso = 10;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + (strDigito1 + strBase.Substring(10, 3)));
                    }
                    else
                    {
                        strBase = (strOrigem.Trim() + "000000000000").Substring(0, 12);
                        intSoma = 0;
                        intPeso = 1;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso == 2))
                                intPeso = 3;

                            if ((intPeso == 9))
                                intPeso = 10;
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + (strDigito1 + strBase.Substring(9, 2)));
                        intSoma = 0;
                        intPeso = 2;

                        for (intPos = 11; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);

                            if ((intPeso > 10))
                                intPeso = 2;
                        }

                        intResto = (intSoma % 11);
                        strDigito2 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase2 + strDigito2);
                    }

                    if ((strBase2 == strOrigem))
                        retorno = true;

                    #endregion

                    break;

                case "TO":
                    #region

                    strBase = (strOrigem.Trim() + "00000000000").Substring(0, 11);

                    if ((("01,02,03,99".IndexOf(strBase.Substring(2, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    {
                        strBase2 = (strBase.Substring(0, 2) + strBase.Substring(4, 6));
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }

                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 10) + strDigito1);

                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }

                    #endregion

                    break;
            }
            return retorno;
        }

        public void ConsultaTipoOperacao(string TelaWeb)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_OPERACAO_CADASTRO_ENTIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@TelaWeb", SqlDbType.VarChar, 100, "TelaWeb"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 100, "EntCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@TelaWeb"].Value = TelaWeb;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            TipoOperacao = row["TipoOperacao"].ToString();
                        }
                    }
                    else
                    {
                        TipoOperacao = "Consulta";
                    }
                }
            }
            catch
            {

            }
        }

        public bool ConsultaPassagemPorStatus(string StatEntCod)
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_PASSAGEM_POR_STATUS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 100, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 10, "StatEntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    if (outputTable.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public string Alterar_Status_Entidade(string StatEntCod)
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

                    dbCommand = new SqlCommand("USER_SP_ATUALIZAR_STATU_ENTIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 100, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 100, "StatEntCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod;

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
                        Retorno = "Erro Funcao Alterar_Status_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro Funcao Alterar_Status_Entidade";
            }

            return Retorno;
        }

        public string Alterar_Status_Entidade_Cadastro_Incompleto()
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

                    dbCommand = new SqlCommand("USER_SP_ATUALIZAR_STATU_ENTIDADE_CADASTRO_INCOMPLETO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 100, "EntCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;

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
                        Retorno = "Erro Funcao Alterar_Status_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro Funcao Alterar_Status_Entidade";
            }

            return Retorno;
        }

        public void AdicionarCondPag(clsCondPag NewCodPag)
        {
            //Verifica se esta instanciado
            if (this.ListCondPag == null)
            {
                this.ListCondPag = new List<clsCondPag>();
            }

            this.ListCondPag.Add(NewCodPag);
        }

        public void RemoverCondPag(clsCondPag CondPag)
        {
            for (int i = 0; i < this.ListCondPag.Count; i++)
            {
                if (this.ListCondPag[i].Codigo == CondPag.Codigo)
                {
                    this.ListCondPag.RemoveAt(i);
                }
            }
        }

        public void AdicionarVendEnt(VendedorClass NewVendEnt)
        {
            //Verifica se esta instanciado
            if (this.ListVendEnt == null)
            {
                this.ListVendEnt = new List<VendedorClass>();
            }

            this.ListVendEnt.Add(NewVendEnt);
        }

        public void RemoveVendEnt(VendedorClass VendEnt)
        {
            for (int i = 0; i < this.ListVendEnt.Count; i++)
            {
                if (this.ListVendEnt[i].VendCod == VendEnt.VendCod)
                {
                    if (VendEnt.TipoOperacao == "Remover")
                    {
                        this.ListVendEnt.RemoveAt(i);//Remove antigo
                        this.ListVendEnt.Add(VendEnt);//adiciona novo com operacao igual a remover
                    }
                    else
                    {
                        this.ListVendEnt.RemoveAt(i);
                    }
                }
            }
        }

        public void AlteraVendEntPrincipal(VendedorClass VendEnt)
        {
            /*Percorre Lista*/
            for (int i = 0; i < this.ListVendEnt.Count; i++)
            {
                /*Verifica se é o codigo passado*/
                if (this.ListVendEnt[i].VendCod == VendEnt.VendCod)
                {
                    ///Caso seja deixa como principal
                    if (this.ListVendEnt[i].TipoOperacao == "Incluir")
                    {

                        /*this.ListVendEnt.RemoveAt(i);//Remove antigo
                        this.ListVendEnt.Add(VendEnt);//adiciona novo com operacao igual a remover*/
                        this.ListVendEnt[i].VendEntPrinc = "Sim";
                        this.ListVendEnt[i].VendEntPrincBit = true;

                    }
                    else
                    {
                        this.ListVendEnt[i].TipoOperacao = "Alterar";
                        this.ListVendEnt[i].VendEntPrinc = "Sim";
                        this.ListVendEnt[i].VendEntPrincBit = true;
                    }
                }
                else //Caso nao seja deixa como Não principal
                {
                    if (this.ListVendEnt[i].TipoOperacao == "Incluir")
                    {

                        this.ListVendEnt[i].VendEntPrinc = "Não";
                        this.ListVendEnt[i].VendEntPrincBit = false;
                    }
                    else
                    {
                        if (this.ListVendEnt[i].TipoOperacao != "Remover")
                        {
                            this.ListVendEnt[i].TipoOperacao = "Alterar";
                            this.ListVendEnt[i].VendEntPrinc = "Não";
                            this.ListVendEnt[i].VendEntPrincBit = false;
                        }
                    }
                }
            }
        }


        public void RemoverVendEnt(VendedorClass VendEnt)
        {
            for (int i = 0; i < this.ListVendEnt.Count; i++)
            {
                if (this.ListVendEnt[i].VendCod == VendEnt.VendCod)
                {
                    if (VendEnt.TipoOperacao == "Remover")
                    {
                        this.ListVendEnt.RemoveAt(i);//Remove antigo
                        this.ListVendEnt.Add(VendEnt);//adiciona novo com operacao igual a remover
                    }
                    else
                    {
                        this.ListVendEnt.RemoveAt(i);
                    }
                }
            }
        }

        public string Consulta_Prazo_Entrega_ENTCOD()
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

                    dbCommand = new SqlCommand("USER_CONSULTA_PRAZO_ENTREGA_ENTCOD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            CIDSIGLAGDS = Convert.ToInt32(row["Prazo"].ToString());
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Consulta_Prazo_Entrega_ENTCOD";
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Consulta_Prazo_Entrega_ENTCOD. Contactar o Suporte!";
            }




            return Retorno;

        }



        public string Valida_Quantidade_Entidades_Inativas_Por_Vendedor()
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

                    dbCommand = new SqlCommand("user_sp_valida_quantidades_inativos", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 100, "VendCod"));


                    dbCommand.Parameters["@VendCod"].Value = NovoVendCod;

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
                        Retorno = "Erro na Funcao Valida_Quantidade_Entidades_Inativas_Por_Vendedor";
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Valida_Quantidade_Entidades_Inativas_Por_Vendedor. Contactar o Suporte!";
            }
            return Retorno;
        }

        #endregion


        #region Funcoes Cadastro de Clientes
        public string Incluir_Entidade()
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
                    if (TipoOperacao == "Inclusão")
                    {
                        dbCommand = new SqlCommand("USER_SP_INSERE_ENTIDADE", dbConnection);
                    }
                    else
                    {
                        dbCommand = new SqlCommand("user_sp_CRM_Altera_Entidade", dbConnection);
                    }

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));

                    if (TipoOperacao == "Inclusão")
                        dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, ParameterDirection.Output, false, 0, 0, "EntCod", DataRowVersion.Default, null));
                    else
                        dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 100, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 40, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntLograd", SqlDbType.VarChar, 10, "EntLograd"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnder", SqlDbType.VarChar, 40, "EntEnder"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNo", SqlDbType.VarChar, 6, "EntEnderNo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNoPI", SqlDbType.VarChar, 5, "EntEnderNoPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderComp", SqlDbType.VarChar, 40, "EntEnderComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntBair", SqlDbType.VarChar, 30, "EntBair"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 8, "CidCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCep", SqlDbType.VarChar, 9, "EntCep"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTipoFJ", SqlDbType.VarChar, 10, "EntTipoFJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 14, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntInscSuframa", SqlDbType.VarChar, 50, "EntInscSuframa"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCod", SqlDbType.VarChar, 50, "CondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@RegEspecNum", SqlDbType.VarChar, 30, "RegEspecNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 30, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntOptanteSimplesFed", SqlDbType.VarChar, 10, "EntOptanteSimplesFed"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNat", SqlDbType.VarChar, 50, "EntNat"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNatGov", SqlDbType.VarChar, 50, "EntNatGov"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserEntFinalidadeProduto", SqlDbType.VarChar, 100, "UserEntFinalidadeProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntLocEntregaOMesmo", SqlDbType.VarChar, 30, "EntLocEntregaOMesmo"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoIndicacao", SqlDbType.VarChar, 100, "TipoIndicacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 500, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@objcodestrniv", SqlDbType.VarChar, 100, "objcodestrniv"));
                    dbCommand.Parameters.Add(new SqlParameter("@TabPVCod", SqlDbType.VarChar, 150, "TabPVCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTexto", SqlDbType.VarChar, 8000, "EntTexto"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTextoHist", SqlDbType.VarChar, 8000, "EntTextoHist"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntRgIe", SqlDbType.VarChar, 8000, "EntRgIe"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoCobCod", SqlDbType.VarChar, 100, "TipoCobCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserPrevisaoFaturamentoMes", SqlDbType.Decimal, 0, "UserPrevisaoFaturamentoMes"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserValorPrimeiraCompra", SqlDbType.Decimal, 0, "UserValorPrimeiraCompra"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 10, "StatEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntStatDescr", SqlDbType.VarChar, 50, "EntStatDescr"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserOutrosCondPagCod", SqlDbType.VarChar, 250, "UserOutrosCondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CategCodEstr", SqlDbType.VarChar, 250, "CategCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntValLimCred", SqlDbType.Decimal, 0, "EntValLimCred"));
                    dbCommand.Parameters.Add(new SqlParameter("@Msg", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "Msg", DataRowVersion.Default, null));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntNome"].Value = EntNome;
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant;
                    dbCommand.Parameters["@EntLograd"].Value = EntLograd;
                    dbCommand.Parameters["@EntEnder"].Value = EntEnder;
                    dbCommand.Parameters["@EntEnderNo"].Value = EntEnderNo;
                    dbCommand.Parameters["@EntEnderNoPI"].Value = EntEnderNoPI;
                    dbCommand.Parameters["@EntEnderComp"].Value = EntEnderComp;
                    dbCommand.Parameters["@EntBair"].Value = EntBair;
                    dbCommand.Parameters["@CidCod"].Value = CidCod;
                    dbCommand.Parameters["@EntCep"].Value = EntCep;
                    dbCommand.Parameters["@EntTipoFJ"].Value = EntTipoFJ;
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;
                    dbCommand.Parameters["@EntInscSuframa"].Value = EntInscSuframa;
                    dbCommand.Parameters["@CondPagCod"].Value = CondPagCod;
                    dbCommand.Parameters["@RegEspecNum"].Value = RegEspecNum;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@EntOptanteSimplesFed"].Value = EntOptanteSimplesFed;
                    dbCommand.Parameters["@EntNat"].Value = EntNat;
                    dbCommand.Parameters["@EntNatGov"].Value = EntNatGov;
                    dbCommand.Parameters["@UserEntFinalidadeProduto"].Value = UserEntFinalidadeProduto;
                    dbCommand.Parameters["@EntLocEntregaOMesmo"].Value = EntLocEntregaOMesmo;
                    dbCommand.Parameters["@TipoIndicacao"].Value = TipoIndicacao;
                    dbCommand.Parameters["@Descricao"].Value = Descricao;
                    dbCommand.Parameters["@objcodestrniv"].Value = objcodestrniv;
                    dbCommand.Parameters["@TabPVCod"].Value = TabPVCod;
                    dbCommand.Parameters["@EntTexto"].Value = EntTexto;
                    dbCommand.Parameters["@EntTextoHist"].Value = EntTextoHist;
                    dbCommand.Parameters["@EntRgIe"].Value = EntRgIe;
                    dbCommand.Parameters["@TipoCobCod"].Value = TipoCobCod;
                    dbCommand.Parameters["@UserPrevisaoFaturamentoMes"].Value = UserPrevisaoFaturamentoMes;
                    dbCommand.Parameters["@UserValorPrimeiraCompra"].Value = UserValorPrimeiraCompra;
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod;
                    dbCommand.Parameters["@EntStatDescr"].Value = EntStatDescr;
                    dbCommand.Parameters["@UserOutrosCondPagCod"].Value = UserOutrosCondPagCod;
                    dbCommand.Parameters["@CategCodEstr"].Value = "";
                    dbCommand.Parameters["@EntValLimCred"].Value = EntValLimCred;
                    dbCommand.Parameters["@Msg"].Value = Msg;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                        EntCod = (string)dbCommand.Parameters["@EntCod"].Value;
                    }


                }
            }
            catch
            {
                Retorno = "Erro na Funcao Incluir_Entidade. Contactar o Suporte!";
            }

            return Retorno;

        }

        public string Exclui_Entidade()
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

                    dbCommand = new SqlCommand("USER_SP_ELIMINA_ENTIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));




                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();





                }
            }
            catch
            {

                Retorno = "Erro na Funcao Exclui_Entidade. Contactar o Suporte!";
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

                    dbCommand = new SqlCommand("User_SP_Consulta_Entidade_2", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 800, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 100, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 5, "StatEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntComercial", SqlDbType.VarChar, 150, "StatEntComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@UFSIGLA", SqlDbType.VarChar, 10, "UFSIGLA"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 8000, "CidCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@ProdCodEstr", SqlDbType.VarChar, 8000, "ProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@USERLINHAPRODUTOLISTA", SqlDbType.VarChar, 200, "USERLINHAPRODUTOLISTA"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoCategoria", SqlDbType.VarChar, 100, "CodigoCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoEvento", SqlDbType.VarChar, 100, "CodigoEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 80000, "VendClasseCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@CNAE_P", SqlDbType.VarChar, 80000, "CNAE_P"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNAE_S", SqlDbType.VarChar, 80000, "CNAE_S"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCompra", SqlDbType.VarChar, 80000, "StatEntCompra"));

                    

                    //dbCommand.Parameters.Add(new SqlParameter("@PeriodoCompraInicial", SqlDbType.VarChar, 10, "PeriodoCompraInicial"));
                    //dbCommand.Parameters.Add(new SqlParameter("@PeriodoCompraFinal", SqlDbType.VarChar, 10, "PeriodoCompraFinal"));



                    dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant ?? "";
                    dbCommand.Parameters["@EntNome"].Value = EntNome ?? "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc ?? "";
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod ?? "";
                    dbCommand.Parameters["@VendCod"].Value = VendCod ?? "";
                    dbCommand.Parameters["@StatEntComercial"].Value = StatEntComercial ?? "";
                    dbCommand.Parameters["@UFSIGLA"].Value = UFSIGLA ?? "";
                    dbCommand.Parameters["@CidCod"].Value = CidCod ?? "";

                    dbCommand.Parameters["@ProdCodEstr"].Value = ProdCodEstr ?? "";
                    dbCommand.Parameters["@USERLINHAPRODUTOLISTA"].Value = USERLINHAPRODUTOLISTA ?? "";
                    dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod ?? "";
                    dbCommand.Parameters["@CNAE_P"].Value = CNAE_P ?? "";
                    dbCommand.Parameters["@CNAE_S"].Value = CNAE_S ?? "";
                    dbCommand.Parameters["@StatEntCompra"].Value = StatEntCompra ?? "";

                    
                    //dbCommand.Parameters["@PeriodoCompraInicial"].Value = PeriodoCompraInicial ?? "";
                    //dbCommand.Parameters["@PeriodoCompraFinal"].Value = PeriodoCompraFinal ?? "";


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

        public DataTable Consulta_Entidade_Carteira()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_SP_Consulta_Entidade_4", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 800, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 100, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 5, "StatEntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntComercial", SqlDbType.VarChar, 150, "StatEntComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@UFSIGLA", SqlDbType.VarChar, 10, "UFSIGLA"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 8000, "CidCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@ProdCodEstr", SqlDbType.VarChar, 8000, "ProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@USERLINHAPRODUTOLISTA", SqlDbType.VarChar, 200, "USERLINHAPRODUTOLISTA"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoCategoria", SqlDbType.VarChar, 100, "CodigoCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoEvento", SqlDbType.VarChar, 100, "CodigoEvento"));
                    //dbCommand.Parameters.Add(new SqlParameter("@PeriodoCompraInicial", SqlDbType.VarChar, 10, "PeriodoCompraInicial"));
                    //dbCommand.Parameters.Add(new SqlParameter("@PeriodoCompraFinal", SqlDbType.VarChar, 10, "PeriodoCompraFinal"));



                    dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant ?? "";
                    dbCommand.Parameters["@EntNome"].Value = EntNome ?? "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc ?? "";
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod ?? "";
                    dbCommand.Parameters["@VendCod"].Value = VendCod ?? "";
                    dbCommand.Parameters["@StatEntComercial"].Value = StatEntComercial ?? "";
                    dbCommand.Parameters["@UFSIGLA"].Value = UFSIGLA ?? "";
                    dbCommand.Parameters["@CidCod"].Value = CidCod ?? "";

                    dbCommand.Parameters["@ProdCodEstr"].Value = ProdCodEstr ?? "";
                    dbCommand.Parameters["@USERLINHAPRODUTOLISTA"].Value = USERLINHAPRODUTOLISTA ?? "";

                    //dbCommand.Parameters["@PeriodoCompraInicial"].Value = PeriodoCompraInicial ?? "";
                    //dbCommand.Parameters["@PeriodoCompraFinal"].Value = PeriodoCompraFinal ?? "";


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

        public DataTable Mostra_Perfil_Comercial_Produto()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_mostra_perfil_comercial_produto", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";



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

        public DataTable Mostra_Duplicatas()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_mostra_duplicata", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";



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

        public DataTable Consulta_Cod_Pag_Pedidos_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_Consulta_Cond_Pag_Ped_Venda_Ent", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));


                dbCommand.Parameters["@EntCod"].Value = EntCod;



                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }


        public DataTable Lista_Total_Vendido_Semestre_Familia_Produto()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_CRM_Total_Vendido_Periodo_Entidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Periodo", SqlDbType.VarChar, 50, "Periodo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));

                    dbCommand.Parameters["@Periodo"].Value = "Semestre";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";



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

        public DataTable Lista_Total_Vendido_Eternidade_Familia_Produto()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_CRM_Total_Vendido_Periodo_Entidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Periodo", SqlDbType.VarChar, 50, "Periodo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));

                    dbCommand.Parameters["@Periodo"].Value = "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";



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


        public DataTable Consulta_Entidade_Acessos()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_SP_Consulta_Entidade_Acesso", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));


                    dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";



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

        public DataTable Consulta_Transportadora()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_consulta_transportadora", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 800, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 100, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidNome", SqlDbType.VarChar, 500, "CidNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@UfSigla", SqlDbType.VarChar, 100, "UfSigla"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant ?? "";
                    dbCommand.Parameters["@EntNome"].Value = EntNome ?? "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc ?? "";
                    dbCommand.Parameters["@CidNome"].Value = CIDNOME ?? "";
                    dbCommand.Parameters["@UfSigla"].Value = UFSIGLA ?? "";




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

        public DataTable Consulta_Holding()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_consulta_holding", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@NivCod", SqlDbType.VarChar, 100, "NivCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@NivNome", SqlDbType.VarChar, 800, "NivNome"));

                    dbCommand.Parameters["@NivCod"].Value = NIVCOD ?? "";
                    dbCommand.Parameters["@NivNome"].Value = NivNome ?? "";




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

        public DataTable Consulta_Entidade_Detalhe()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_SP_Conulta_Entidade_Detalhe_Web", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";


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

        public string Mostra_Entidade()
        {
            string Retorno = "";
            int CodCategInterno = 0;

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_MOSTRA_ENTIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            EmpCod = "1";
                            EntNome = row["EntNome"].ToString();
                            EntNomeFant = row["EntNomeFant"].ToString();
                            EntLograd = row["EntLograd"].ToString();
                            EntEnder = row["EntEnder"].ToString();
                            EntEnderNo = row["EntEnderNo"].ToString();
                            EntEnderNoPI = row["EntEnderNoPI"].ToString();
                            EntEnderComp = row["EntEnderComp"].ToString();
                            EntBair = row["EntBair"].ToString();
                            CidCod = row["CidCod"].ToString();
                            EntCep = row["EntCep"].ToString();
                            CepCod = row["EntCep"].ToString();
                            EntTipoFJ = row["EntTipoFJ"].ToString();
                            EntCpfCgc = row["EntCpfCgc"].ToString();
                            EntInscSuframa = row["EntInscSuframa"].ToString();
                            CondPagCod = row["CondPagCod"].ToString();
                            RegEspecNum = row["RegEspecNum"].ToString();

                            EntOptanteSimplesFed = row["EntOptanteSimplesFed"].ToString();
                            EntNat = row["EntNat"].ToString();
                            EntNatGov = row["EntNatGov"].ToString();
                            UserEntFinalidadeProduto = row["UserEntFinalidadeProduto"].ToString();
                            EntLocEntregaOMesmo = row["EntLocEntregaOMesmo"].ToString();
                            TipoIndicacao = row["TipoIndicacao"].ToString();
                            Descricao = row["Descricao"].ToString();

                            objcodestrniv = row["objcodestrniv"].ToString();
                            ObjCodEstr1 = row["ObjCodEstr01"].ToString();//Campos Utilizados apenas para Separacao de Niveis
                            ObjCodEstr2 = row["ObjCodEstr02"].ToString();
                            ObjCodEstr3 = row["ObjCodEstr03"].ToString();

                            TabPVCod = row["TabPVCod"].ToString();
                            EntTexto = row["EntTexto"].ToString();
                            EntTextoHist = row["EntTextoHist"].ToString();
                            EntRgIe = row["EntRgIe"].ToString();
                            StatEntCod = row["StatEntCod"].ToString();
                            EntStatDescr = row["EntStatDescr"].ToString();

                            CategCodEstr = row["CategCodEstr"].ToString();
                            EntInscMunic = row["EntInscMunic"].ToString();
                            TipoCobCod = row["TipoCobCod"].ToString();
                            UserPrevisaoFaturamentoMes = Convert.ToDecimal(row["UserPrevisaoFaturamentoMes"].ToString());
                            UserValorPrimeiraCompra = Convert.ToDecimal(row["UserValorPrimeiraCompra"].ToString());

                            EntValLimCred = Convert.ToDecimal(row["EntValLimCred"].ToString());
                            SaldoLimiteCliente = Convert.ToDecimal(row["LimiteDisponivel"].ToString());
                            ENTQTDDIASATRASO = Convert.ToInt32(row["ENTQTDDIASATRASO"].ToString());
                            NIVCOD = row["NIVCOD"].ToString();
                            CondPagCodPag = row["CondPagCodPag"].ToString();
                            UserOutrosCondPagCod = row["UserOutrosCondPagCod"].ToString();

                            EntTransporteOMesmo = row["EntTransporteOMesmo"].ToString();
                            EntTranspCod = row["EntTranspCod"].ToString();
                            EntStatFreteVenda = row["EntStatFreteVenda"].ToString();

                            UserShelfLife = Convert.ToInt32(row["UserShelfLife"].ToString());

                            EntDataCad = Convert.ToDateTime(row["EntDataCad"].ToString());
                            StatEntComercial = row["StatEntComercial"].ToString();
                            NFDataEmis = Convert.ToDateTime(row["NFDataEmis"].ToString());


                            UsuCartaoCnpj = row["UsuCartaoCnpj"].ToString();
                            UsuSintegra = row["UsuSintegra"].ToString();




                            ListEntFone = new List<clsEntFone>();
                            ListEntWeb = new List<clsEntWeb>();
                            ListContatoClass = new List<ContatoClass>();
                            ListDocEntidadeClass = new List<DocEntidadeClass>();
                            ListCondPag = new List<clsCondPag>();
                            ListVendEnt = new List<VendedorClass>();
                            ListEntRelacionamentoclass = new List<EntRelacionamentoClass>();
                            ListEntCategoriaClass = new List<EntidadeCategoriaClass>();
                            ListEntConcorrenciaClass = new List<EntConcorrenciaClass>();
                            ListEntPerfilDeConsumoClass = new List<EntPerfilDeConsumoClass>();




                            #region  Consuta Categoria Secundaria

                            DataTable outputTableCnae2 = new DataTable();
                            EntidadeCategoriaClass ObjEntidadeCategoriaClass = new EntidadeCategoriaClass();

                            ObjEntidadeCategoriaClass.EntCod = EntCod;
                            outputTableCnae2 = ObjEntidadeCategoriaClass.Consulta_Categora();

                            //Percorrendo retorno
                            if (outputTableCnae2.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntCnae2 in outputTableCnae2.Rows)
                                {
                                    ObjEntidadeCategoriaClass = new EntidadeCategoriaClass();

                                    ObjEntidadeCategoriaClass.EntCod = rowEntCnae2["EntCod"].ToString();
                                    ObjEntidadeCategoriaClass.CategCodEstr = rowEntCnae2["CategCodEstr"].ToString();
                                    ObjEntidadeCategoriaClass.Categoria = rowEntCnae2["Categoria"].ToString();
                                    ObjEntidadeCategoriaClass.Codigo = CodCategInterno;

                                    CodCategInterno = CodCategInterno + 1;

                                    AdicionarCategoria(ObjEntidadeCategoriaClass);
                                    
                                }
                            }
                            #endregion



                            #region  Consuta Relacionamento

                            DataTable outputTableRelacionamento = new DataTable();
                            EntRelacionamentoClass ObjEntRelacionamentoClass = new EntRelacionamentoClass();

                            ObjEntRelacionamentoClass.EntCod = EntCod;
                            outputTableRelacionamento = ObjEntRelacionamentoClass.Consulta_Relacionamento_EntCod();

                            //Percorrendo retorno
                            if (outputTableRelacionamento.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntRelacionamento in outputTableRelacionamento.Rows)
                                {
                                    ObjEntRelacionamentoClass = new EntRelacionamentoClass();

                                    ObjEntRelacionamentoClass.Codigo = Convert.ToInt32(rowEntRelacionamento["Codigo"].ToString());
                                    ObjEntRelacionamentoClass.EntCod = rowEntRelacionamento["EntCod"].ToString();
                                    ObjEntRelacionamentoClass.Descricao = rowEntRelacionamento["Descricao"].ToString();
                                    ObjEntRelacionamentoClass.Data = rowEntRelacionamento["Data"].ToString();

                                    //Adcionando List
                                    AdicionarRelacionamento(ObjEntRelacionamentoClass);
                                }
                            }
                            #endregion

                            #region  Consuta Perfil de Consumo

                            DataTable outputTablePerfilDeConsumo = new DataTable();
                            EntPerfilDeConsumoClass ObjEntPerfilDeConsumoClass = new EntPerfilDeConsumoClass();

                            ObjEntPerfilDeConsumoClass.EntCod = EntCod;
                            outputTablePerfilDeConsumo = ObjEntPerfilDeConsumoClass.Consulta_Perfil_Consumo_EntCod();

                            //Percorrendo retorno
                            if (outputTablePerfilDeConsumo.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntPerfilDeConsumo in outputTablePerfilDeConsumo.Rows)
                                {
                                    ObjEntPerfilDeConsumoClass = new EntPerfilDeConsumoClass();

                                    ObjEntPerfilDeConsumoClass.Codigo = Convert.ToInt32(rowEntPerfilDeConsumo["Codigo"].ToString());
                                    ObjEntPerfilDeConsumoClass.Linha = rowEntPerfilDeConsumo["Linha"].ToString();
                                    ObjEntPerfilDeConsumoClass.Quantidade = Convert.ToDouble(rowEntPerfilDeConsumo["Quantidade"].ToString());
                                    ObjEntPerfilDeConsumoClass.Descricao = rowEntPerfilDeConsumo["Descricao"].ToString();

                                    AdicionarPerfil(ObjEntPerfilDeConsumoClass);

                                }
                            }
                            #endregion

                            #region  Consuta Concorrencia

                            DataTable outputTableConcorrencia = new DataTable();
                            EntConcorrenciaClass ObjEntConcorrenciaClass = new EntConcorrenciaClass();

                            ObjEntConcorrenciaClass.EntCod = EntCod;
                            outputTableConcorrencia = ObjEntConcorrenciaClass.Consulta_Concorrencia_EntCod();

                            //Percorrendo retorno
                            if (outputTableConcorrencia.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntConcorrencia in outputTableConcorrencia.Rows)
                                {
                                    ObjEntConcorrenciaClass = new EntConcorrenciaClass();

                                    ObjEntConcorrenciaClass.Codigo = Convert.ToInt32(rowEntConcorrencia["Codigo"].ToString());
                                    ObjEntConcorrenciaClass.EntCod = rowEntConcorrencia["EntCod"].ToString();
                                    ObjEntConcorrenciaClass.NomeConcorrente = rowEntConcorrencia["NomeConcorrente"].ToString();
                                    ObjEntConcorrenciaClass.ObservacaoConcorrente = rowEntConcorrencia["ObservacaoConcorrente"].ToString();

                                    AdicionarConcorrencia(ObjEntConcorrenciaClass);

                                }
                            }
                            #endregion

                            #region  Consuta Anexos

                            DataTable outputTableAnexo = new DataTable();
                            DocEntidadeClass DocEntidadeClass = new DocEntidadeClass();

                            DocEntidadeClass.EntCod = EntCod;
                            outputTableAnexo = DocEntidadeClass.Consulta_DocEntidade();

                            //Percorrendo retorno
                            if (outputTableAnexo.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntAnexo in outputTableAnexo.Rows)
                                {
                                    DocEntidadeClass = new DocEntidadeClass();

                                    DocEntidadeClass.DocEntSeq = Convert.ToInt32(rowEntAnexo["DocEntSeq"].ToString());
                                    DocEntidadeClass.DocEntPathArq = rowEntAnexo["DocEntPathArq"].ToString();
                                    DocEntidadeClass.UsuCod = rowEntAnexo["UsuCod"].ToString();
                                    DocEntidadeClass.USER_TB_Tipos_AnexosID = Convert.ToInt32(rowEntAnexo["USER_TB_Tipos_AnexosID"].ToString());
                                    DocEntidadeClass.DocEntObs = rowEntAnexo["DocEntObs"].ToString();
                                    DocEntidadeClass.NomeTipoAnexo = rowEntAnexo["NomeTipoAnexo"].ToString();
                                    DocEntidadeClass.DocEntData = rowEntAnexo["DocEntData"].ToString();
                                    DocEntidadeClass.DocEntImage = ((byte[])rowEntAnexo["DocEntImage"]);


                                    AdicionarAnexo(DocEntidadeClass);

                                }
                            }
                            #endregion

                            #region  Consuta EntWeb
                            /*Consulta todos os Contado da Tabela Ent Web e Carrega no List da Entidade*/
                            DataTable outputTableEntWebTemp = new DataTable();
                            clsEntWeb ObjEntWebTemp = new clsEntWeb();

                            ObjEntWebTemp.EntCod = EntCod;
                            outputTableEntWebTemp = ObjEntWebTemp.Consulta_EntWeb_EntCod();

                            //Percorrendo retorno
                            if (outputTableEntWebTemp.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntWeb in outputTableEntWebTemp.Rows)
                                {
                                    ObjEntWebTemp = new clsEntWeb();

                                    ObjEntWebTemp.EntCod = rowEntWeb["EntCod"].ToString();
                                    ObjEntWebTemp.EntWebSeq = Convert.ToInt32(rowEntWeb["EntWebSeq"].ToString());
                                    ObjEntWebTemp.EntWebTipo = rowEntWeb["EntWebTipo"].ToString();
                                    ObjEntWebTemp.EntWebWWW = rowEntWeb["EntWebWWW"].ToString();
                                    ObjEntWebTemp.EntWebEMail = rowEntWeb["EntWebEMail"].ToString();
                                    ObjEntWebTemp.EntWebEMailPrinc = rowEntWeb["EntWebEMailPrinc"].ToString();
                                    ObjEntWebTemp.EntWebEMailPedComp = rowEntWeb["EntWebEMailPedComp"].ToString();
                                    ObjEntWebTemp.EntWebRecebeEmailOcor = rowEntWeb["EntWebRecebeEmailOcor"].ToString();
                                    ObjEntWebTemp.EntWebDisparaEmailAgenda = rowEntWeb["EntWebDisparaEmailAgenda"].ToString();
                                    ObjEntWebTemp.EntWebEmailNFe = rowEntWeb["EntWebEmailNFe"].ToString();
                                    ObjEntWebTemp.EntWebEmailNFSe = rowEntWeb["EntWebEmailNFSe"].ToString();

                                    //Adcionando Email
                                    AdicionarEmail(ObjEntWebTemp);

                                }
                            }
                            #endregion

                            #region  Consuta EntFone
                            /*Consulta todos os Contado da Tabela Ent Fone e Carrega no List da Entidade*/
                            DataTable outputTableEntFoneTemp = new DataTable();
                            clsEntFone ObjEntFoneTemp = new clsEntFone();

                            ObjEntFoneTemp.EntCod = EntCod;
                            outputTableEntFoneTemp = ObjEntFoneTemp.Consulta_EntFone_EntCod();

                            //Percorrendo retorno
                            if (outputTableEntFoneTemp.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntFone in outputTableEntFoneTemp.Rows)
                                {
                                    ObjEntFoneTemp = new clsEntFone();

                                    ObjEntFoneTemp.EntCod = rowEntFone["EntCod"].ToString();
                                    ObjEntFoneTemp.EntFoneSeq = Convert.ToInt32(rowEntFone["EntFoneSeq"].ToString());
                                    ObjEntFoneTemp.EntFoneTipo = rowEntFone["EntFoneTipo"].ToString();
                                    ObjEntFoneTemp.EntFoneDDI = rowEntFone["EntFoneDDI"].ToString();
                                    ObjEntFoneTemp.EntFoneDDD = rowEntFone["EntFoneDDD"].ToString();
                                    ObjEntFoneTemp.EntFoneNum = rowEntFone["EntFoneNum"].ToString();
                                    ObjEntFoneTemp.EntFoneRamalBip = rowEntFone["EntFoneRamalBip"].ToString();
                                    ObjEntFoneTemp.EntFoneRamalBipNum = rowEntFone["EntFoneRamalBipNum"].ToString();
                                    ObjEntFoneTemp.EntFonePrinc = rowEntFone["EntFonePrinc"].ToString();


                                    //Adcionando EntFone
                                    AdicionarEntFone(ObjEntFoneTemp);

                                }
                            }
                            #endregion

                            #region  Consuta Contato
                            /*Consulta todos os Contado da Tabela USER_TB_ENT_CONTATO e Carrega no List da Entidade*/
                            DataTable outputTableContato = new DataTable();
                            ContatoClass ObjContatoClass = new ContatoClass();

                            ObjContatoClass.EntCod = EntCod;
                            outputTableContato = ObjContatoClass.Consulta_Contato_EntCod();

                            //Percorrendo retorno
                            if (outputTableContato.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntContato in outputTableContato.Rows)
                                {
                                    ObjContatoClass = new ContatoClass();

                                    ObjContatoClass.EntCod = rowEntContato["EntCod"].ToString();
                                    ObjContatoClass.Nome = rowEntContato["Nome"].ToString();
                                    ObjContatoClass.Email = rowEntContato["Email"].ToString();
                                    ObjContatoClass.DDDTelefone = rowEntContato["DDDTelefone"].ToString();
                                    ObjContatoClass.Telefone = rowEntContato["Telefone"].ToString();
                                    ObjContatoClass.Ramal = rowEntContato["Ramal"].ToString();
                                    ObjContatoClass.DDDCelular = rowEntContato["DDDCelular"].ToString();
                                    ObjContatoClass.Celular = rowEntContato["Celular"].ToString();
                                    ObjContatoClass.TipoContato = rowEntContato["TipoContato"].ToString();
                                    ObjContatoClass.Cargo = rowEntContato["Cargo"].ToString();
                                    ObjContatoClass.Empresa = rowEntContato["Empresa"].ToString();
                                    ObjContatoClass.ENTCONTATOID = Convert.ToInt32(rowEntContato["ENTCONTATOID"].ToString());

                                    //Adcionando Contato
                                    AdicionarContato(ObjContatoClass);
                                }
                            }
                            #endregion

                            #region  Consuta Endereco Entrega
                            DataTable outputTableEnderecoEntrega = new DataTable();
                            EnderecoEntregaClass ObjEnderecoEntregaClass = new EnderecoEntregaClass();

                            ObjEnderecoEntregaClass.EntCod = EntCod;
                            outputTableEnderecoEntrega = ObjEnderecoEntregaClass.Consulta_EnderecoEntrega_EntCod();

                            //Percorrendo retorno
                            if (outputTableEnderecoEntrega.Rows.Count > 0)
                            {
                                foreach (DataRow rowEndereEntrega in outputTableEnderecoEntrega.Rows)
                                {
                                    EnderecoEntregaClass = new EnderecoEntregaClass();

                                    EnderecoEntregaClass.EntCod = rowEndereEntrega["EntCod"].ToString();
                                    EnderecoEntregaClass.EnderEntSeq = Convert.ToInt32(rowEndereEntrega["EnderEntSeq"].ToString());
                                    EnderecoEntregaClass.EnderEntEntrega = rowEndereEntrega["EnderEntEntrega"].ToString();
                                    EnderecoEntregaClass.EnderEntNome = rowEndereEntrega["EnderEntNome"].ToString();
                                    EnderecoEntregaClass.EnderEnt = rowEndereEntrega["EnderEnt"].ToString();
                                    EnderecoEntregaClass.EnderEntNo = rowEndereEntrega["EnderEntNo"].ToString();
                                    EnderecoEntregaClass.EnderEntNoPI = rowEndereEntrega["EnderEntNoPI"].ToString();
                                    EnderecoEntregaClass.EnderEntComp = rowEndereEntrega["EnderEntComp"].ToString();
                                    EnderecoEntregaClass.EnderEntBair = rowEndereEntrega["EnderEntBair"].ToString();
                                    EnderecoEntregaClass.CidCod = rowEndereEntrega["CidCod"].ToString();
                                    EnderecoEntregaClass.EnderEntCep = rowEndereEntrega["EnderEntCep"].ToString();
                                    EnderecoEntregaClass.EnderEntTipoFJ = rowEndereEntrega["EnderEntTipoFJ"].ToString();
                                    EnderecoEntregaClass.EnderEntCpfCgc = rowEndereEntrega["EnderEntCpfCgc"].ToString();
                                    EnderecoEntregaClass.EnderEntFoneSeq = Convert.ToInt32(rowEndereEntrega["EnderEntFoneSeq"].ToString());
                                    EnderecoEntregaClass.EnderEntFoneDDD = rowEndereEntrega["EnderEntFoneDDD"].ToString();
                                    EnderecoEntregaClass.EnderEntFoneNum = rowEndereEntrega["EnderEntFoneNum"].ToString();
                                    EnderecoEntregaClass.EnderEntFoneRamalBip = rowEndereEntrega["EnderEntFoneRamalBip"].ToString();
                                    EnderecoEntregaClass.EnderEntFoneRamalBipNum = rowEndereEntrega["EnderEntFoneRamalBipNum"].ToString();
                                    EnderecoEntregaClass.EnderEntEMail = rowEndereEntrega["EnderEntEMail"].ToString();
                                    EnderecoEntregaClass.EnderEntContato = rowEndereEntrega["EnderEntContato"].ToString();


                                }
                            }
                            #endregion

                            #region  Consuta CondPag Ent
                            /*Consulta todas as condições de Pagamento da Entidade*/
                            DataTable outputTableCondPagEnt = new DataTable();
                            clsCondPag ObjCondPAg = new clsCondPag();

                            ObjCondPAg.EntCod = EntCod;
                            outputTableCondPagEnt = ObjCondPAg.Consulta_Cod_Pag_EntCod();

                            //Percorrendo retorno
                            if (outputTableCondPagEnt.Rows.Count > 0)
                            {
                                foreach (DataRow rowCondPagEnt in outputTableCondPagEnt.Rows)
                                {
                                    ObjCondPAg = new clsCondPag();
                                    ObjCondPAg.EntCod = rowCondPagEnt["EntCod"].ToString();
                                    ObjCondPAg.CondPagCod = rowCondPagEnt["CondPagCod"].ToString();
                                    ObjCondPAg.CondPagNome = rowCondPagEnt["CondPagNome"].ToString();
                                    ObjCondPAg.Condicao = rowCondPagEnt["CondPagNome"].ToString();

                                    //Adcionando 
                                    AdicionarCondPag(ObjCondPAg);
                                    

                                }
                            }
                            #endregion

                            #region  Consuta Vendedores Entidade
                            /*Consulta todos os Vendedor da Entidade*/
                            DataTable outputTableVendEnt = new DataTable();
                            VendedorClass ObjVendedor = new VendedorClass();

                            ObjVendedor.EntCod = EntCod;
                            outputTableVendEnt = ObjVendedor.Consulta_Vendedor_EntCod();

                            //Percorrendo retorno
                            if (outputTableVendEnt.Rows.Count > 0)
                            {
                                foreach (DataRow rowVendCod in outputTableVendEnt.Rows)
                                {
                                    ObjVendedor = new VendedorClass();
                                    ObjVendedor.VendCod = rowVendCod["VendCod"].ToString();
                                    ObjVendedor.VendNome = rowVendCod["VendNome"].ToString();
                                    ObjVendedor.VendEntPrinc = rowVendCod["VendEntPrinc"].ToString();
                                    ObjVendedor.VendEntPrincBit = rowVendCod["VendEntPrincBit"].ToString() != "1" ? false : true;

                                    //Seta o Vendedor Principal(Apenas utilizado na tela Principal)
                                    if (ObjVendedor.VendEntPrinc == "Sim")
                                    {
                                        VendCod = row["VendCod"].ToString();
                                    }


                                    //Adcionando 
                                    AdicionarVendEnt(ObjVendedor);


                                }
                            }
                            #endregion

                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Mostra_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Mostra_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Mostra_Entidade_EntCpfCgc()
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

                    dbCommand = new SqlCommand("USER_SP_MOSTRA_ENTIDADE_EntCpfCgc", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 50, "EntCpfCgc"));

                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            EmpCod = "1";
                            EntCod = row["EntCod"].ToString();
                            EntNome = row["EntNome"].ToString();
                            EntNomeFant = row["EntNomeFant"].ToString();
                            EntLograd = row["EntLograd"].ToString();
                            EntEnder = row["EntEnder"].ToString();
                            EntEnderNo = row["EntEnderNo"].ToString();
                            EntEnderNoPI = row["EntEnderNoPI"].ToString();
                            EntEnderComp = row["EntEnderComp"].ToString();
                            EntBair = row["EntBair"].ToString();
                            CidCod = row["CidCod"].ToString();
                            EntCep = row["EntCep"].ToString();
                            CepCod = row["EntCep"].ToString();
                            EntTipoFJ = row["EntTipoFJ"].ToString();
                            EntCpfCgc = row["EntCpfCgc"].ToString();
                            EntInscSuframa = row["EntInscSuframa"].ToString();
                            CondPagCod = row["CondPagCod"].ToString();
                            RegEspecNum = row["RegEspecNum"].ToString();

                            EntOptanteSimplesFed = row["EntOptanteSimplesFed"].ToString();
                            EntNat = row["EntNat"].ToString();
                            EntNatGov = row["EntNatGov"].ToString();
                            UserEntFinalidadeProduto = row["UserEntFinalidadeProduto"].ToString();
                            EntLocEntregaOMesmo = row["EntLocEntregaOMesmo"].ToString();
                            TipoIndicacao = row["TipoIndicacao"].ToString();
                            Descricao = row["Descricao"].ToString();

                            objcodestrniv = row["objcodestrniv"].ToString();
                            ObjCodEstr1 = row["ObjCodEstr01"].ToString();//Campos Utilizados apenas para Separacao de Niveis
                            ObjCodEstr2 = row["ObjCodEstr02"].ToString();
                            ObjCodEstr3 = row["ObjCodEstr03"].ToString();

                            TabPVCod = row["TabPVCod"].ToString();
                            EntTexto = row["EntTexto"].ToString();
                            EntTextoHist = row["EntTextoHist"].ToString();
                            EntRgIe = row["EntRgIe"].ToString();
                            StatEntCod = row["StatEntCod"].ToString();
                            EntStatDescr = row["EntStatDescr"].ToString();

                            CategCodEstr = row["CategCodEstr"].ToString();
                            EntInscMunic = row["EntInscMunic"].ToString();
                            TipoCobCod = row["TipoCobCod"].ToString();
                            UserPrevisaoFaturamentoMes = Convert.ToDecimal(row["UserPrevisaoFaturamentoMes"].ToString());
                            UserValorPrimeiraCompra = Convert.ToDecimal(row["UserValorPrimeiraCompra"].ToString());

                            EntValLimCred = Convert.ToDecimal(row["EntValLimCred"].ToString());
                            //SaldoLimiteCliente = Convert.ToDecimal(row["SaldoLimiteCliente"].ToString());
                            ENTQTDDIASATRASO = Convert.ToInt32(row["ENTQTDDIASATRASO"].ToString());
                            NIVCOD = row["NIVCOD"].ToString();
                            CondPagCodPag = row["CondPagCodPag"].ToString();
                            UserOutrosCondPagCod = row["UserOutrosCondPagCod"].ToString();

                            EntTransporteOMesmo = row["EntTransporteOMesmo"].ToString();
                            EntTranspCod = row["EntTranspCod"].ToString();
                            EntStatFreteVenda = row["EntStatFreteVenda"].ToString();

                            UserShelfLife = Convert.ToInt32(row["UserShelfLife"].ToString());

                            EntDataCad = Convert.ToDateTime(row["EntDataCad"].ToString());
                            StatEntComercial = row["StatEntComercial"].ToString();
                            NFDataEmis = Convert.ToDateTime(row["NFDataEmis"].ToString());

                            UFSIGLA = row["UfSigla"].ToString();
                            CidNomeComp = row["CidNomeComp"].ToString();

                            #region  Consuta EntFone
                            /*Consulta todos os Contado da Tabela Ent Fone e Carrega no List da Entidade*/
                            DataTable outputTableEntFoneTemp = new DataTable();
                            clsEntFone ObjEntFoneTemp = new clsEntFone();

                            ObjEntFoneTemp.EntCod = EntCod;
                            outputTableEntFoneTemp = ObjEntFoneTemp.Consulta_EntFone_EntCod();

                            //Percorrendo retorno
                            if (outputTableEntFoneTemp.Rows.Count > 0)
                            {
                                foreach (DataRow rowEntFone in outputTableEntFoneTemp.Rows)
                                {
                                    ObjEntFoneTemp = new clsEntFone();

                                    ObjEntFoneTemp.EntCod = rowEntFone["EntCod"].ToString();
                                    ObjEntFoneTemp.EntFoneSeq = Convert.ToInt32(rowEntFone["EntFoneSeq"].ToString());
                                    ObjEntFoneTemp.EntFoneTipo = rowEntFone["EntFoneTipo"].ToString();
                                    ObjEntFoneTemp.EntFoneDDI = rowEntFone["EntFoneDDI"].ToString();
                                    ObjEntFoneTemp.EntFoneDDD = rowEntFone["EntFoneDDD"].ToString();
                                    ObjEntFoneTemp.EntFoneNum = rowEntFone["EntFoneNum"].ToString();
                                    ObjEntFoneTemp.EntFoneRamalBip = rowEntFone["EntFoneRamalBip"].ToString();
                                    ObjEntFoneTemp.EntFoneRamalBipNum = rowEntFone["EntFoneRamalBipNum"].ToString();
                                    ObjEntFoneTemp.EntFonePrinc = rowEntFone["EntFonePrinc"].ToString();


                                    //Adcionando EntFone
                                    AdicionarEntFone(ObjEntFoneTemp);

                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Mostra_Entidade_EntCpfCgc";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Mostra_Entidade_EntCpfCgc. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Altera_Entidade()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_ENTIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 100, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 40, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntLograd", SqlDbType.VarChar, 10, "EntLograd"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnder", SqlDbType.VarChar, 40, "EntEnder"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNo", SqlDbType.VarChar, 6, "EntEnderNo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderNoPI", SqlDbType.VarChar, 5, "EntEnderNoPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntEnderComp", SqlDbType.VarChar, 40, "EntEnderComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntBair", SqlDbType.VarChar, 30, "EntBair"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 8, "CidCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCep", SqlDbType.VarChar, 9, "EntCep"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTipoFJ", SqlDbType.VarChar, 10, "EntTipoFJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 14, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntInscSuframa", SqlDbType.VarChar, 50, "EntInscSuframa"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 30, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntRgIe", SqlDbType.VarChar, 8000, "EntRgIe"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntNome"].Value = EntNome;
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant;
                    dbCommand.Parameters["@EntLograd"].Value = EntLograd;
                    dbCommand.Parameters["@EntEnder"].Value = EntEnder;
                    dbCommand.Parameters["@EntEnderNo"].Value = EntEnderNo;
                    dbCommand.Parameters["@EntEnderNoPI"].Value = EntEnderNoPI;
                    dbCommand.Parameters["@EntEnderComp"].Value = EntEnderComp;
                    dbCommand.Parameters["@EntBair"].Value = EntBair;
                    dbCommand.Parameters["@CidCod"].Value = CidCod;
                    dbCommand.Parameters["@EntCep"].Value = EntCep;
                    dbCommand.Parameters["@EntTipoFJ"].Value = EntTipoFJ;
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc;
                    dbCommand.Parameters["@EntInscSuframa"].Value = EntInscSuframa;

                    dbCommand.Parameters["@VendCod"].Value = VendCod;
                    dbCommand.Parameters["@EntRgIe"].Value = EntRgIe;

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
                        Retorno = "Erro na Funcao Altera_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Altera_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Altera_Fiscal_Entidade()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_FISCAL_ENTIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntOptanteSimplesFed", SqlDbType.VarChar, 10, "EntOptanteSimplesFed"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNat", SqlDbType.VarChar, 50, "EntNat"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNatGov", SqlDbType.VarChar, 50, "EntNatGov"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserEntFinalidadeProduto", SqlDbType.VarChar, 100, "UserEntFinalidadeProduto"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntOptanteSimplesFed"].Value = EntOptanteSimplesFed;
                    dbCommand.Parameters["@EntNat"].Value = EntNat;
                    dbCommand.Parameters["@EntNatGov"].Value = EntNatGov;
                    dbCommand.Parameters["@UserEntFinalidadeProduto"].Value = UserEntFinalidadeProduto;

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
                        Retorno = "Erro na Funcao Altera_Fiscal_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Altera_Fiscal_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Alterando_Informacoes_Entidade()
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

                    dbCommand = new SqlCommand("USER_SP_ATUALIZA_ENTIDADE_INFORMACOES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@objcodestrniv", SqlDbType.VarChar, 100, "objcodestrniv"));
                    dbCommand.Parameters.Add(new SqlParameter("@TabPVCod", SqlDbType.VarChar, 100, "TabPVCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCod", SqlDbType.VarChar, 50, "CondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoIndicacao", SqlDbType.VarChar, 100, "TipoIndicacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 500, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoCobCod", SqlDbType.VarChar, 100, "TipoCobCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserPrevisaoFaturamentoMes", SqlDbType.Decimal, 0, "UserPrevisaoFaturamentoMes"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserValorPrimeiraCompra", SqlDbType.Decimal, 0, "UserValorPrimeiraCompra"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntValLimCred", SqlDbType.Decimal, 0, "EntValLimCred"));
                    dbCommand.Parameters.Add(new SqlParameter("@ENTQTDDIASATRASO", SqlDbType.Int, 0, "ENTQTDDIASATRASO"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserOutrosCondPagCod", SqlDbType.VarChar, 250, "UserOutrosCondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CategCodEstr", SqlDbType.VarChar, 250, "CategCodEstr"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@objcodestrniv"].Value = objcodestrniv;
                    dbCommand.Parameters["@TabPVCod"].Value = TabPVCod;
                    dbCommand.Parameters["@CondPagCod"].Value = ""; //CondPagCod; A Condicao de Pagamento, em caso de alteracao eh tratada na tela Holding
                    dbCommand.Parameters["@TipoIndicacao"].Value = TipoIndicacao;
                    dbCommand.Parameters["@Descricao"].Value = Descricao;
                    dbCommand.Parameters["@TipoCobCod"].Value = TipoCobCod;
                    dbCommand.Parameters["@UserPrevisaoFaturamentoMes"].Value = UserPrevisaoFaturamentoMes;
                    dbCommand.Parameters["@UserValorPrimeiraCompra"].Value = UserValorPrimeiraCompra;
                    dbCommand.Parameters["@EntValLimCred"].Value = EntValLimCred;
                    dbCommand.Parameters["@ENTQTDDIASATRASO"].Value = ENTQTDDIASATRASO;
                    dbCommand.Parameters["@UserOutrosCondPagCod"].Value = UserOutrosCondPagCod;
                    dbCommand.Parameters["@CategCodEstr"].Value = CategCodEstr;

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
                        Retorno = "Erro na Funcao Alterando_Informacoes_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Alterando_Informacoes_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Altera_Logistica_Entidade()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_LOGISTICA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 100, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTransporteOMesmo", SqlDbType.VarChar, 100, "EntTransporteOMesmo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTranspCod", SqlDbType.VarChar, 100, "EntTranspCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntStatFreteVenda", SqlDbType.VarChar, 100, "EntStatFreteVenda"));
                    dbCommand.Parameters.Add(new SqlParameter("@UserShelfLife", SqlDbType.Int, 0, "UserShelfLife"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntTransporteOMesmo"].Value = EntTransporteOMesmo;
                    dbCommand.Parameters["@EntTranspCod"].Value = EntTranspCod;
                    dbCommand.Parameters["@EntStatFreteVenda"].Value = EntStatFreteVenda;
                    dbCommand.Parameters["@UserShelfLife"].Value = UserShelfLife;

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
                        Retorno = "Erro na Funcao Altera_Logistica_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Altera_Logistica_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Adiciona_DocEntidade(DocEntidadeClass NewDocumento)
        {
            if (this.ListDocEntidadeClass == null)
            {
                this.ListDocEntidadeClass = new List<DocEntidadeClass>();
            }

            this.ListDocEntidadeClass.Add(NewDocumento);

            return "";
        }

        public string Remove_DocEntidade(DocEntidadeClass Documento)
        {
            if (this.ListDocEntidadeClass != null)
            {
                for (int i = 0; i < this.ListDocEntidadeClass.Count(); i++)
                {
                    if (this.ListDocEntidadeClass[i].DocEntObs == Documento.DocEntObs)
                    {
                        this.ListDocEntidadeClass.RemoveAt(i);
                    }
                }
            }

            return "";
        }

        public string Atualizar_Historico_Entidade()
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

                    dbCommand = new SqlCommand("USER_SP_ATUALIZA_Historico_ENTIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntTextoHist", SqlDbType.VarChar, 8000, "EntTextoHist"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntTextoHist"].Value = EntTextoHist;

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
                        Retorno = "Erro na Funcao Atualizar_Historico_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Atualizar_Historico_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public void Consulta_Documentos_Entidade()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();
            DocEntidadeClass ObjDocEntidadeClass = new DocEntidadeClass();
            this.ListDocEntidadeClass = new List<DocEntidadeClass>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_DOC_ENTIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    //Percorrendo retorno
                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow rowDoc in outputTable.Rows)
                        {
                            ObjDocEntidadeClass = new DocEntidadeClass();
                            ObjDocEntidadeClass.DocEntSeq = Convert.ToInt32(rowDoc["DocEntSeq"].ToString());
                            ObjDocEntidadeClass.DocEntPathArq = rowDoc["DocEntPathArq"].ToString();
                            ObjDocEntidadeClass.DocEntObs = rowDoc["DocEntObs"].ToString();
                            ObjDocEntidadeClass.UsuCod = rowDoc["UsuCod"].ToString();
                            ObjDocEntidadeClass.DocEntData = rowDoc["DocEntData"].ToString();

                            Adiciona_DocEntidade(ObjDocEntidadeClass);
                        }
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Consulta_Documentos_Entidade. Contactar o Suporte!";
            }
        }


        public string Altera_Holding()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_HOLDING", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@NIVCOD", SqlDbType.VarChar, 100, "NIVCOD"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@NIVCOD"].Value = NIVCOD;

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
                        Retorno = "Erro na Funcao Alterando_Informacoes_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Alterando_Informacoes_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }


        public string Altera_CondPagCod_Entidade()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_CondPagCod_EntCod", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCod", SqlDbType.VarChar, 100, "CondPagCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@CondPagCod"].Value = CondPagCod;

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
                        Retorno = "Erro na Funcao Altera_CondPagCod_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Altera_CondPagCod_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }


        public String Lista_Vendedor_Logado()
        {
            string strSql = "";
            string VendCod = "";
            strSql = "SELECT VendCod FROM Vendedor WHERE VendStat = 'Ativo' and UsuCod = '" + UsuCod.ToString() + "'";

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

                        VendCod = Convert.ToString(dbCommand.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no método Lista_Vendedor_Logado");
                }

                return VendCod;
            }
        }


        public DataTable Lista_Categoria_Usuario()
        {
            string strSql = "";
            strSql = "SELECT distinct Cat.CategNome, Cat.CategCodEstr FROM GRP_X_USUARIO Gus join CATEGORIA Cat on Cat.CategNome = Gus.GrpUsuCod  WHERE GrpUsuCod like 'Entidade%'";

            BDClass.Metodo = "Lista_Categoria_Usuario";
            BDClass.strSql = strSql;
            return BDClass.Executa_DataTable();
        }

        public string Lista_Categoria_Entidade()
        {
            string strSql = "";
            strSql = "SELECT Cat.CategNome, Cat.CategCodEstr FROM GRP_X_USUARIO Gus join CATEGORIA Cat on Cat.CategNome = Gus.GrpUsuCod join ENT_CATEG Eca on Eca.CategCodEstr = Cat.CategCodEstr  WHERE GrpUsuCod like 'Entidade%' and Eca.EntCod = '" + EntCod.ToString() + "'";

            BDClass.Metodo = "Lista_Categoria_Usuario";
            BDClass.strSql = strSql;
            return BDClass.Executa_DataTable_String();
        }


        #endregion


        #region Funcoes Funcionarios
        public DataTable Consulta_Funcionario()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_consulta_funcionario", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 800, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 100, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 5, "StatEntCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant ?? "";
                    dbCommand.Parameters["@EntNome"].Value = EntNome ?? "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc ?? "";
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod ?? "";



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

        #endregion


        #region Funcoes SAC
        public DataTable Consulta_SAC()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_consulta_sac", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 800, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 100, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 5, "StatEntCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant ?? "";
                    dbCommand.Parameters["@EntNome"].Value = EntNome ?? "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc ?? "";
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod ?? "";



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

        #endregion



        #region Funcoes Fornecedores

        public DataTable Consulta_Fornecedor()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_consulta_fornecedor", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 800, "EntNomeFant"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 100, "EntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatEntCod", SqlDbType.VarChar, 5, "StatEntCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                    dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant ?? "";
                    dbCommand.Parameters["@EntNome"].Value = EntNome ?? "";
                    dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";
                    dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc ?? "";
                    dbCommand.Parameters["@StatEntCod"].Value = StatEntCod ?? "";



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

        #endregion


        public string Envia_Email_Alteracao_Entidade()
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

                    dbCommand = new SqlCommand("User_sp_Relatorio_Alteracao_Entidade_Email", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 25, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Assunto", SqlDbType.VarChar, 100, "Assunto"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@Assunto"].Value = AssuntoEmail;

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
                        Retorno = "Erro na Funcao Altera_CondPagCod_Entidade";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Altera_CondPagCod_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public String Lista_Usuario_Vendedor()
        {
            string strSql = "";
            string UsuCod = "";
            strSql = "SELECT UsuCod FROM Vendedor WHERE VendCod = '" + VendCod.ToString() + "'";

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

                        UsuCod = Convert.ToString(dbCommand.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no método Lista_Usuario_Vendedor");
                }

                return UsuCod;
            }
        }

    }

}