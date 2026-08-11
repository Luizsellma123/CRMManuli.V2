using System;
using System.Text;
using System.Data;
using CRMAPI.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CRMAPI.Classes.RastreioPedido
{
    public class RastreioAguiaSul : RastreioPedido
    {
        public RastreioAguiaSul(RastreiaPedidoModel objRastreiaPedidoModel) : base(objRastreiaPedidoModel)
        {
            this.IDTransportador = 1;
        }

        public override string GravaDados()
        {
            string json = Chama_API_Json(MontaJson(Carrega_KeyNfe_SAP()));

            if (json.Length > 0)
            {
                if (json.Substring(0, 5) != "Erro:")
                {
                    RetornoAPIAguiaSul objRetornoAPIAguiaSul = jsonconv.ConverteJSonParaObject<RetornoAPIAguiaSul>(json);

                    if (objRetornoAPIAguiaSul.documento.tracking.Count > 0)
                    {
                        foreach (RetornoAPIAguiaSul.trackingClass tracking in objRetornoAPIAguiaSul.documento.tracking)
                        {
                            CarregaDataHistorico(tracking.data_hora);

                            CarregaHistorico(tracking.cidade, tracking.descricao);

                            CarregaCodigoOcorrencia(tracking.ocorrencia, tracking.descricao);

                            GRAVA_HISTORICO_RASTREIO_PEDIDOS();
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
                return "Não teve retorno da API da Aguia Sul";
            }
        }

        public void CarregaDataHistorico(string data_hora)
        {
            data_hora = data_hora.Replace("T", " ");

            this.DataHistorico = data_hora;
        }

        public void CarregaHistorico(string cidade, string descricao)
        {
            StringBuilder Historico = new StringBuilder();

            Historico.AppendLine("<b>Unidade:</b> " + cidade + " <br>");

            Historico.AppendLine(descricao);

            this.Historico = Historico.ToString();
        }

        public void CarregaCodigoOcorrencia(string texto, string descricao)
        {
            string padrao = @"\(([^)]+)\)";

            Match correspondencia = Regex.Match(texto, padrao);

            if (correspondencia.Success)
                this.CodigoOcorrencia = correspondencia.Groups[1].Value;

            if (this.CodigoOcorrencia == "67")
            {
                this.PrevisaoEntrega = descricao.Substring((descricao.Length - 9), 8);

                this.PrevisaoEntrega = Convert.ToDateTime(this.PrevisaoEntrega).ToString("yyyy-MM-dd");
            }
            else
            {
                this.PrevisaoEntrega = "";
            }
        }

        private string MontaJson(string KeyNfe)
        {
            StringBuilder json = new StringBuilder();

            json.AppendLine("{");

            json.AppendLine("\"chave_nfe\": \"" + KeyNfe + "\"");

            json.AppendLine("}");

            return json.ToString();
        }

        public class RetornoAPIAguiaSul
        {
            public bool success { get; set; }

            public string message { get; set; }

            public documentoClass documento { get; set; }

            public class documentoClass
            {
                public headerClass header { get; set; }

                public List<trackingClass> tracking { get; set; }
            }

            public class headerClass
            {
                public string remetente { get; set; }

                public string destinatario { get; set; }

                public string message { get; set; }

                public string pedido { get; set; }
            }

            public class trackingClass
            {
                public string data_hora { get; set; }

                public string dominio { get; set; }

                public string filial { get; set; }

                public string cidade { get; set; }

                public string ocorrencia { get; set; }

                public string descricao { get; set; }

                public string tipo { get; set; }

                public string data_hora_efetiva { get; set; }

                public string nome_recebedor { get; set; }

                public string nro_doc_recebedor { get; set; }
            }
        }

        private string Carrega_KeyNfe_SAP()
        {
            try
            {                
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("Select OINV.BPLId, OINV.CardCode, ");
                stringSQL.AppendLine("OINV.DocEntry, Process.KeyNfe");
                stringSQL.AppendLine("from OINV");
                stringSQL.AppendLine("INNER JOIN ORDR");
                stringSQL.AppendLine("  ON OINV.CardCode=ORDR.CardCode");
                stringSQL.AppendLine("INNER JOIN DBInvOne..Process Process");
                stringSQL.AppendLine("  ON Process.DocEntry=OINV.DocEntry");
                //OINV.Serial = Recebe o Número Serial da NFE
                stringSQL.AppendLine("where OINV.Serial='" + NumeroNotaFiscal + "'");
                //ORDR.DocEntry = Recebe o Número do Pedido SAP
                stringSQL.AppendLine("and ORDR.DocEntry=" + NumeroPedidoSAP + "");
                //ORDR.BPLId = Recebe o código da empresa
                stringSQL.AppendLine("and ORDR.BPLId=" + IDEmpresa + "");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                if (ConsultaSAP.Rows.Count > 0)
                {
                    foreach (DataRow row in ConsultaSAP.Rows)
                    {
                        return row["KeyNfe"].ToString();
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