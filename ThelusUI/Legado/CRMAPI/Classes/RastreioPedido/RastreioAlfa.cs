using System;
using System.IO;
using System.Text;
using System.Data;
using CRMAPI.Models;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace CRMAPI.Classes.RastreioPedido
{
    public class RastreioAlfa : RastreioPedido
    {
        public RastreioAlfa(RastreiaPedidoModel objRastreiaPedidoModel) : base(objRastreiaPedidoModel)
        {
            this.IDTransportador = 2;
        }

        public override string GravaDados()
        {

            string XML = Chama_API(Monta_EnderecoAPI());

            if (XML.Length > 0)
            {
                if (XML.Substring(0, 5) != "Erro:")
                {
                    WsRastreamento objWsRastreamento = XMLParaObj(XML);

                    if (objWsRastreamento.Rst.RstStatus.Text == "")
                    {
                        GravaDadosNF(objWsRastreamento);

                        GravaDadosEmbarque(objWsRastreamento);

                        GravaDadosEntNF(objWsRastreamento);
                    }
                    else
                    {
                        return objWsRastreamento.Rst.RstStatus.Text;
                    }
                }
                else
                {
                    return XML;
                }
            }
            else
            {
                return "Não teve retorno da API da Alfa";
            }

            return "";
        }

        private void GravaDadosNF(WsRastreamento objWsRastreamento)
        {
            this.DataHistorico = objWsRastreamento.Rst.NF.NFData.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder Historico = new StringBuilder();

            {
                Historico.AppendLine("Seu CT-e foi emitido e sua mercadoria está sendo preparada para transporte. <br>");

                Historico.Append("Saida da unidade " + objWsRastreamento.Rst.NF.NFInicio + " em ");

                Historico.Append(objWsRastreamento.Rst.NF.NFData.ToString("dd/MM/yyyy") + ". ");

                Historico.Append("Previsão de chegada na unidade " + objWsRastreamento.Rst.NF.NFFim + " em ");

                Historico.Append(objWsRastreamento.Rst.NF.NFDataPrevista.ToString("dd/MM/yyyy") + ".");
            }

            this.Historico = Historico.ToString();

            this.CodigoOcorrencia = "1";

            this.PrevisaoEntrega = objWsRastreamento.Rst.NF.NFDataPrevista.ToString("yyyy-MM-dd");

            GRAVA_HISTORICO_RASTREIO_PEDIDOS();
        }

        private void GravaDadosEmbarque(WsRastreamento objWsRastreamento)
        {
            if (objWsRastreamento.Rst.Embarque.EmbNF.Count > 0)
            {
                foreach (EmbNF embNF in objWsRastreamento.Rst.Embarque.EmbNF)
                {
                    this.DataHistorico = embNF.EmbChegada.Replace("--", "");

                    StringBuilder Historico = new StringBuilder();

                    {
                        Historico.AppendLine("<b>Unidade:</b> " + embNF.EmbDestino + " <br>");

                        Historico.Append("Saida da unidade " + embNF.EmbOrigem + " em ");

                        Historico.Append(Convert.ToDateTime(embNF.EmbSaida.Replace("--", "")).ToString("dd/MM/yyyy") + ". ");

                        Historico.Append("Previsão de chegada na unidade " + embNF.EmbDestino + " em ");

                        Historico.Append(Convert.ToDateTime(embNF.EmbChegada.Replace("--", "")).ToString("dd/MM/yyyy") + ", ");

                        Historico.Append(embNF.EmbChegada.Replace("--", "").Substring((embNF.EmbChegada.Length - 7), 5) + "h.");
                    }

                    this.Historico = Historico.ToString();

                    this.CodigoOcorrencia = "2";

                    this.PrevisaoEntrega = "";
                }

                GRAVA_HISTORICO_RASTREIO_PEDIDOS();
            }
        }

        private void GravaDadosEntNF(WsRastreamento objWsRastreamento)
        {
            this.DataHistorico = objWsRastreamento.Rst.Entrega.EntNF.EntData.ToString("yyyy-MM-dd");

            this.DataHistorico += " ";

            this.DataHistorico += objWsRastreamento.Rst.Entrega.EntNF.EntHora.ToString("HH:mm");

            StringBuilder Historico = new StringBuilder();

            {
                Historico.Append("Seu item foi entregue em ");

                Historico.AppendLine(objWsRastreamento.Rst.Entrega.EntNF.EntData.ToString("yyyy-MM-dd") + " <br>");

                Historico.Append("Destinatário: " + objWsRastreamento.Rst.Entrega.EntNF.EntNome);

                if (objWsRastreamento.Rst.Entrega.EntNF.EntSetor == "")
                    Historico.Append(" - " + "Administrativo");
                else
                    Historico.Append(" - " + objWsRastreamento.Rst.Entrega.EntNF.EntSetor);
            }

            this.Historico = Historico.ToString();

            this.CodigoOcorrencia = "3";

            this.PrevisaoEntrega = "";

            GRAVA_HISTORICO_RASTREIO_PEDIDOS();
        }

        public WsRastreamento XMLParaObj(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(WsRastreamento));

            WsRastreamento objWsRastreamento = new WsRastreamento();

            using (StringReader reader = new StringReader(xml))
            {
                objWsRastreamento = (WsRastreamento)serializer.Deserialize(reader);
            }

            return objWsRastreamento;
        }

        private string Monta_EnderecoAPI()
        {
            StringBuilder EnderecoAPI = new StringBuilder();

            EnderecoAPI.Append(this.EnderecoAPI);

            EnderecoAPI.Append("?idr=");

            EnderecoAPI.Append(this.ChaveAPI);

            EnderecoAPI.Append("&merNF=");

            EnderecoAPI.Append(this.NumeroNotaFiscal);

            return EnderecoAPI.ToString();
        }

        #region classes do XML

        [XmlRoot(ElementName = "rstStatus")]
        public class RstStatus
        {

            [XmlAttribute(AttributeName = "stsCd")]
            public int StsCd { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "rem")]
        public class Rem
        {

            [XmlAttribute(AttributeName = "remCnpj")]
            public double RemCnpj { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "emiTransp")]
        public class EmiTransp
        {

            [XmlAttribute(AttributeName = "transpCnpj")]
            public double TranspCnpj { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "emi")]
        public class Emi
        {

            [XmlElement(ElementName = "rem")]
            public Rem Rem { get; set; }

            [XmlElement(ElementName = "emiTransp")]
            public EmiTransp EmiTransp { get; set; }

            [XmlElement(ElementName = "emiUnid")]
            public string EmiUnid { get; set; }
        }

        [XmlRoot(ElementName = "NF")]
        public class NF
        {

            [XmlElement(ElementName = "NFCtrc")]
            public int NFCtrc { get; set; }

            [XmlElement(ElementName = "NFCtrValor")]
            public double NFCtrValor { get; set; }

            [XmlElement(ElementName = "NFData")]
            public DateTime NFData { get; set; }

            [XmlElement(ElementName = "NFDataPrevista")]
            public DateTime NFDataPrevista { get; set; }

            [XmlElement(ElementName = "NFDest")]
            public string NFDest { get; set; }

            [XmlElement(ElementName = "NFInicio")]
            public string NFInicio { get; set; }

            [XmlElement(ElementName = "NFFim")]
            public string NFFim { get; set; }

            [XmlElement(ElementName = "NFCidade")]
            public string NFCidade { get; set; }

            [XmlAttribute(AttributeName = "nro")]
            public int Nro { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "embNF")]
        public class EmbNF
        {

            [XmlElement(ElementName = "embOrigem")]
            public string EmbOrigem { get; set; }

            [XmlElement(ElementName = "embDestino")]
            public string EmbDestino { get; set; }

            [XmlElement(ElementName = "embSaida")]
            public string EmbSaida { get; set; }

            [XmlElement(ElementName = "embChegada")]
            public string EmbChegada { get; set; }
        }

        [XmlRoot(ElementName = "embarque")]
        public class Embarque
        {

            [XmlElement(ElementName = "embNF")]
            public List<EmbNF> EmbNF { get; set; }
        }

        [XmlRoot(ElementName = "entS")]
        public class EntS
        {

            [XmlElement(ElementName = "entSaida")]
            public string EntSaida { get; set; }
        }

        [XmlRoot(ElementName = "entNF")]
        public class EntNF
        {

            [XmlElement(ElementName = "entData")]
            public DateTime EntData { get; set; }

            [XmlElement(ElementName = "entHora")]
            public DateTime EntHora { get; set; }

            [XmlElement(ElementName = "entNome")]
            public string EntNome { get; set; }

            [XmlElement(ElementName = "entSetor")]
            public string EntSetor { get; set; }

            [XmlElement(ElementName = "entComprovante")]
            public string EntComprovante { get; set; }
        }

        [XmlRoot(ElementName = "entrega")]
        public class Entrega
        {

            [XmlElement(ElementName = "entS")]
            public EntS EntS { get; set; }

            [XmlElement(ElementName = "entNF")]
            public EntNF EntNF { get; set; }
        }

        [XmlRoot(ElementName = "rst")]
        public class Rst
        {

            [XmlElement(ElementName = "rstStatus")]
            public RstStatus RstStatus { get; set; }

            [XmlElement(ElementName = "emi")]
            public Emi Emi { get; set; }

            [XmlElement(ElementName = "NF")]
            public NF NF { get; set; }

            [XmlElement(ElementName = "embarque")]
            public Embarque Embarque { get; set; }

            [XmlElement(ElementName = "entrega")]
            public Entrega Entrega { get; set; }

            [XmlAttribute(AttributeName = "versao")]
            public double Versao { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "wsRastreamento")]
        public class WsRastreamento
        {

            [XmlElement(ElementName = "rst")]
            public Rst Rst { get; set; }
        }

        #endregion

        private string Carrega_TaxIdNum_SAP()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("Select TaxIdNum ");
                stringSQL.AppendLine("from OBPL");
                //ORDR.BPLId = Recebe o código da empresa
                stringSQL.AppendLine("and OBPL.BPLId=" + IDEmpresa + "");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                if (ConsultaSAP.Rows.Count > 0)
                {
                    foreach (DataRow row in ConsultaSAP.Rows)
                    {
                        return row["TaxIdNum"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar a KeyNfe do SAP.");
            }

            return "";
        }
    }
}