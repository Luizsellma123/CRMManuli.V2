using System;
using System.Text;
using System.Data;
using CRMAPI.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CRMAPI.Classes.RastreioPedido
{
    public class RastreioSaoMiguel : RastreioPedido
    {
        public RastreioSaoMiguel(RastreiaPedidoModel objRastreiaPedidoModel) : base(objRastreiaPedidoModel)
        {
            this.IDTransportador = 4;
        }

        public override string GravaDados()
        {
            string json = Chama_API_Json_Com_Autenticacao(MontaJson());

            if (json.Length > 0)
            {
                if (json.Substring(0, 5) != "Erro:")
                {
                    json = json.Substring(1, json.Length - 2);

                    RetornoAPISaoMiguel objRetornoAPISaoMiguel = new RetornoAPISaoMiguel();

                    objRetornoAPISaoMiguel = jsonconv.ConverteJSonParaObject<RetornoAPISaoMiguel>(json);

                    if (objRetornoAPISaoMiguel.tracks.Count > 0)
                    {
                        foreach (Track track in objRetornoAPISaoMiguel.tracks)
                        {
                            GravaTrack(track);
                        }
                    }

                    return "";
                }
                else
                {
                    return json;
                }
            }
            else
            {
                return "Não teve retorno da API da São Miguel";
            }
        }

        private void GravaTrack(Track track)
        {
            this.DataHistorico = Convert.ToDateTime(track.date + " " + track.hour).ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder Historico = new StringBuilder();

            Historico.AppendLine(track.title + "<br>");

            if (track.control == "EMISSAO")
            {
                Historico.AppendLine("Saida da unidade ");

                if (track.additionals.Count > 0)
                {
                    foreach (Additionals additionals in track.additionals)
                    {
                        if (additionals.label == "Agência Remetente")
                        {
                            Historico.AppendLine(additionals.value);

                            Historico.AppendLine("em " + track.date + ", " + track.hour + ".");

                            break;
                        }
                    }

                    foreach (Additionals additionals in track.additionals)
                    {
                        if (additionals.label == "Previsão de Entrega")
                        {
                            Historico.AppendLine("Previsão de chegada no destino em " + additionals.value);

                            this.PrevisaoEntrega = Convert.ToDateTime(additionals.value).ToString("yyyy-MM-dd");

                            break;
                        }
                    }
                }
            }
            else
            {
                Historico.AppendLine(track.date + track.hour);

                this.PrevisaoEntrega = "";
            }

            this.Historico = Historico.ToString();

            if (track.control == null)
            {
                if (track.title == "Mercadoria em trânsito")
                    this.CodigoOcorrencia = "VIAGEM";
                if (track.title == "Boletim de ocorrência em atendimento" &&
                    track.title == "Boletim ocorrência de extravio")
                    this.CodigoOcorrencia = "OUTROS";
            }
            else
            {
                this.CodigoOcorrencia = track.control;
            }

            GRAVA_HISTORICO_RASTREIO_PEDIDOS();
        }

        private string MontaJson()
        {
            string TaxIdNum = "", KeyNfe = "", SeriesStr = "";

            #region Consulta SAP

            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("Select OINV.BPLId, ");
                stringSQL.AppendLine("OINV.CardCode, ");
                stringSQL.AppendLine("OINV.DocEntry, ");
                stringSQL.AppendLine("OBPL.TaxIdNum, ");
                stringSQL.AppendLine("OINV.SeriesStr,  ");
                stringSQL.AppendLine("Process.KeyNfe ");

                stringSQL.AppendLine("from OINV ");
                stringSQL.AppendLine("INNER JOIN ORDR  ");
                stringSQL.AppendLine("  ON OINV.CardCode=ORDR.CardCode ");
                stringSQL.AppendLine("INNER JOIN OBPL ");
                stringSQL.AppendLine("  ON OBPL.BPLId=OINV.BPLId ");
                stringSQL.AppendLine("INNER JOIN DBInvOne..Process Process");
                stringSQL.AppendLine("  ON Process.DocEntry=OINV.DocEntry ");

                //OINV.Serial = Recebe o Número Serial da NFE
                stringSQL.AppendLine("where OINV.Serial='" + NumeroNotaFiscal + "' ");
                //ORDR.DocEntry = Recebe o Número do Pedido SAP
                stringSQL.AppendLine("and ORDR.DocEntry=" + NumeroPedidoSAP + " ");
                //ORDR.BPLId = Recebe o código da empresa
                stringSQL.AppendLine("and ORDR.BPLId=" + IDEmpresa + " ");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                if (ConsultaSAP.Rows.Count > 0)
                {
                    foreach (DataRow row in ConsultaSAP.Rows)
                    {
                        TaxIdNum = row["TaxIdNum"].ToString();

                        KeyNfe = row["KeyNfe"].ToString();

                        SeriesStr = row["SeriesStr"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar a dados do SAP.");
            }

            if (TaxIdNum == "" || KeyNfe == "" || SeriesStr == "")
                throw new Exception("Não foi possivel encontrar dados no SAP.");

            #endregion

            #region

            VendasWeb.GerencialVendas.UtilClass objUtilClass = new VendasWeb.GerencialVendas.UtilClass();

            TaxIdNum = objUtilClass.RetornaApenasNumeros(TaxIdNum);

            StringBuilder json = new StringBuilder();

            json.AppendLine("{");

            json.AppendLine("   \"cpfcnpj\": \"" + TaxIdNum + "\",");

            json.AppendLine("   \"numberdocument\": \"" + KeyNfe + "\",");

            json.AppendLine("   \"serie\": \"" + SeriesStr + "\",");

            json.AppendLine("   \"isCte\": false,");

            json.AppendLine("   \"captcha\": ");

            json.AppendLine("       {");

            json.AppendLine("           \"solver\": \"\",");

            json.AppendLine("           \"textcaptcha\": \"\" ");

            json.AppendLine("       }");

            json.AppendLine("}");

            #endregion

            return json.ToString();
        }

        #region classes do Json

        public class RetornoAPISaoMiguel
        {
            public int id { get; set; }
            public string type { get; set; }
            public int number { get; set; }
            public string embark { get; set; }
            public string expectedDate { get; set; }
            public string key { get; set; }
            public Issuer issuer { get; set; }
            public List<Track> tracks { get; set; }
            public Recipient recipient { get; set; }
            public string dateandhourdelivery { get; set; }
        }

        public class Additional
        {
            public string voucher { get; set; }

            public string deliveryForecast { get; set; }

            public string label { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string localization { get; set; }
            public Addresses addresses { get; set; }
        }

        public class Addresses
        {
            public string address { get; set; }
            public string zipcode { get; set; }
            public string phone { get; set; }
            public string neighborhood { get; set; }
        }

        public class Additionals
        {
            public string icon { get; set; }
            public string label { get; set; }
            public string value { get; set; }
        }

        public class Issuer
        {
            public string name { get; set; }
            public long cpfcnpj { get; set; }
            public string city { get; set; }
            public string initials { get; set; }
        }

        public class Recipient
        {
            public string name { get; set; }
            public long cpfcnpj { get; set; }
            public string city { get; set; }
            public string initials { get; set; }
        }

        public class Track
        {
            public string title { get; set; }
            public string date { get; set; }
            public string hour { get; set; }
            public string icon { get; set; }
            public string control { get; set; }
            public string coloricon { get; set; }
            public Additional additional { get; set; }
            public List<Additionals> additionals { get; set; }
        }

        #endregion

    }
}