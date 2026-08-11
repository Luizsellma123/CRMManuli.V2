using System;
using System.Linq;
using System.Data;
using VendasWeb.classes;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;
using System.Collections.Generic;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSSaidaDadosSerasa : clsConexao
    {
        #region Campos

        public string Produto { get; set; }

        public int IDProduto { get; set; }

        public int IDConfiguracao { get; set; }

        public string NomeCampo { get; set; }

        public int PosicaoInicial { get; set; }

        public int PosicaoFinal { get; set; }

        public string Prefixo { get; set; }

        public int Tamanho { get; set; }

        public string Descricao { get; set; }

        #region List's das subclasses

        public List<EMPCONSULTA> EMPCONSULTA { get; set; }

        public List<IDENTIFICACAO> IDENTIFICACAO { get; set; }

        public List<ENDERECO> ENDERECO { get; set; }

        public List<LOCALIZACAO> LOCALIZACAO { get; set; }

        public List<CONTABILIZACAO> CONTABILIZACAO { get; set; }

        public List<ANTECESSORA> ANTECESSORA { get; set; }

        public List<ATIVIDADE> ATIVIDADE { get; set; }

        public List<INSESTADUAL> INSESTADUAL { get; set; }

        public List<FILIAIS> FILIAIS { get; set; }

        public List<CONCENTREGRAFIAS> CONCENTREGRAFIAS { get; set; }

        public List<CONTSOCIETARIOATUCAP> CONTSOCIETARIOATUCAP { get; set; }

        public List<CONTSOCIETARIODETSOC> CONTSOCIETARIODETSOC { get; set; }

        public List<QUADROADMINDET> QUADROADMINDET { get; set; }

        public List<PEFIN> PEFIN { get; set; }

        public List<REFIN> REFIN { get; set; }

        public List<CONCENTREACAOJUD> CONCENTREACAOJUD { get; set; }

        public List<CONCENTREPROTESTOS> CONCENTREPROTESTOS { get; set; }

        public List<CONCENTERCHSF> CONCENTERCHSF { get; set; }

        public List<CONCENTERCHSFCCF> CONCENTERCHSFCCF { get; set; }

        public List<CONCENTREDIVVENC> CONCENTREDIVVENC { get; set; }

        public List<CONCENTREPARTFALEN> CONCENTREPARTFALEN { get; set; }

        public List<CONCENTREFALENCONC> CONCENTREFALENCONC { get; set; }

        public List<CONSULTASERASA> CONSULTASERASA { get; set; }

        public List<ULTIMASCONSULTAS> ULTIMASCONSULTAS { get; set; }

        public List<HPCHISTPAG> HPCHISTPAG { get; set; }

        public List<HPCTOTEVCMP1> HPCTOTEVCMP1 { get; set; }

        public List<HPCTOTHITPAG1> HPCTOTHITPAG1 { get; set; }

        public List<HPCEVCPFOR> HPCEVCPFOR { get; set; }

        public List<HPCREFNEG> HPCREFNEG { get; set; }

        public List<HPCRELFOR> HPCRELFOR { get; set; }

        public List<RISKSCORINGPRINAD1> RISKSCORINGPRINAD1 { get; set; }

        public List<RISKSCORINGPRINAD2> RISKSCORINGPRINAD2 { get; set; }

        public List<INFRECHEQUE> INFRECHEQUE { get; set; }

        public List<HPCRELFORPER> HPCRELFORPER { get; set; }

        public List<CONCENTRERESUMO> CONCENTRERESUMO { get; set; }

        public List<INFRECHEQUEDET> INFRECHEQUEDET { get; set; }

        public List<INFADICSOCIOS> INFADICSOCIOS { get; set; }

        public List<ANSPCSCADQTD> ANSPCSCADQTD { get; set; }

        public List<INFADSOCNQUSOCCMP> INFADSOCNQUSOCCMP { get; set; }

        public List<INFADSOCNQUSOC> INFADSOCNQUSOC { get; set; }

        public List<MENSAGEM> MENSAGEM1 { get; set; }

        public List<MENSAGEM> MENSAGEM2 { get; set; }

        public List<MENSAGEM> MENSAGEM3 { get; set; }

        public List<MENSAGEM> MENSAGEM4 { get; set; }

        public List<MENSAGEM> MENSAGEM5 { get; set; }

        public List<MENSAGEM> MENSAGEM6 { get; set; }

        public List<MENSAGEM> MENSAGEM7 { get; set; }

        public List<MENSAGEM> MENSAGEM8 { get; set; }

        public List<FRASESALERTA> FRASESALERTA { get; set; }

        #region Estes não são usados

        public List<QUADROADMINDATATU> QUADROADMINDATATU { get; set; }

        public List<PARTICIPACOESDATATU> PARTICIPACOESDATATU { get; set; }

        public List<PARTICIPACOESDET> PARTICIPACOESDET { get; set; }

        public List<PARTICIPACOESPARTDET> PARTICIPACOESPARTDET { get; set; }

        public List<HPCVALORES> HPCVALORES { get; set; }

        public List<HPCANCOMPPAG1> HPCANCOMPPAG1 { get; set; }

        public List<INFADISOCIOSCOMP> INFADISOCIOSCOMP { get; set; }

        public List<ANSPCSCADINDINF> ANSPCSCADINDINF { get; set; }

        public List<ANSPCSCADINF1> ANSPCSCADINF1 { get; set; }

        public List<ANSPCSCADINFMSG> ANSPCSCADINFMSG { get; set; }

        #endregion

        #endregion

        #endregion

        public string GravaAnaliseSerasa(ClienteClasse ObjCliente)
        {
            DataTable outputTable = new DataTable();

            string erro = "";

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

                    dbCommand.Parameters["@IDCliente"].Value = ObjCliente.IDCliente;
                    dbCommand.Parameters["@IDUsuario"].Value = ObjCliente.IDUsuario;

                    if (RISKSCORINGPRINAD1 == null)
                    {
                        dbCommand.Parameters["@FATOR"].Value = 0;
                    }
                    else
                    {
                        foreach (RISKSCORINGPRINAD1 rISKSCORINGPRINAD1 in RISKSCORINGPRINAD1)
                        {
                            dbCommand.Parameters["@FATOR"].Value = Convert.ToInt32(rISKSCORINGPRINAD1.FATORRISKSCORING);
                        }
                    }

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                        ObjCliente.IDAnalise = Convert.ToInt32(row["IDAnalise"]);
                    }

                    if (erro == "") erro = GravaListsSubClasses(ObjCliente.IDCliente, ObjCliente.IDAnalise);
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        private string GravaListsSubClasses(int IDCliente, int IDAnalise)
        {
            string erro = "";

            try
            {
                // Adiciona à lista para depois percorrer as listas
                List<List<SuperClasseDadosSerasa>> Listas = new List<List<SuperClasseDadosSerasa>>()
                {
                    EMPCONSULTA?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    IDENTIFICACAO?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    ENDERECO?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    LOCALIZACAO?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONTABILIZACAO?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    ANTECESSORA?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    ATIVIDADE?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    INSESTADUAL?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    FILIAIS?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTREGRAFIAS?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONTSOCIETARIOATUCAP?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONTSOCIETARIODETSOC?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    QUADROADMINDET?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    PEFIN?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    REFIN?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTREACAOJUD?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTREPROTESTOS?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTERCHSF?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTERCHSFCCF?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTREDIVVENC?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTREPARTFALEN?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTREFALENCONC?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONSULTASERASA?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    ULTIMASCONSULTAS?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    HPCHISTPAG?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    HPCTOTEVCMP1?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    HPCTOTHITPAG1?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    HPCEVCPFOR?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    HPCREFNEG?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    HPCRELFOR?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    RISKSCORINGPRINAD1?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    RISKSCORINGPRINAD2?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    INFRECHEQUE?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    HPCRELFORPER?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    CONCENTRERESUMO?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    INFRECHEQUEDET?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    INFADICSOCIOS?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    ANSPCSCADQTD?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    INFADSOCNQUSOCCMP?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    INFADSOCNQUSOC?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    MENSAGEM1?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    MENSAGEM2?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    MENSAGEM3?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    MENSAGEM4?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    MENSAGEM5?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    MENSAGEM6?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    MENSAGEM7?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    MENSAGEM8?.Cast<SuperClasseDadosSerasa>()?.ToList(),
                    FRASESALERTA?.Cast<SuperClasseDadosSerasa>()?.ToList()
                };

                foreach (List<SuperClasseDadosSerasa> lista in Listas)
                {
                    if (lista != null)
                    {
                        foreach (SuperClasseDadosSerasa item in lista)
                        {
                            string erroGravaDados = item.GravaDados(IDCliente, IDAnalise);

                            if (erroGravaDados != "")
                                erro = item.GetType().Name + " - " + erroGravaDados;

                            if (erro != "")
                                item.ApagaTabelasCasoDeErro(IDCliente, IDAnalise);

                            if (!string.IsNullOrEmpty(erro)) break;
                        }
                    }

                    if (!string.IsNullOrEmpty(erro)) break;
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

    }
}