using System;
using System.Text;
using System.Data;
using CRMAPI.Models;
using System.Collections.Generic;

namespace CRMAPI.Classes.RastreioPedido
{
    public class RastreioTranSanches : RastreioPedido
    {
        public string DocDate { get; set; }

        public RetornoTokenLoginAPITranSanches objRetornoTokenLoginAPITranSanches;

        VendasWeb.WEBServiceSAP.ClassesWEBService.JsonConversao jsonconv = new VendasWeb.WEBServiceSAP.ClassesWEBService.JsonConversao();

        public RastreioTranSanches(RastreiaPedidoModel objRastreiaPedidoModel) : base(objRastreiaPedidoModel)
        {
            this.IDTransportador = 5;
        }

        public override string GravaDados()
        {
            string erro = RecuperaTokenLogin();

            if (erro == "")
            {
                string json = Chama_API_GET_Com_Autenticacao(MontaEnderecoApi());

                if (json.Length > 0 && json.Trim() != "[]")
                {
                    if (json.Substring(0, 5) != "Erro:")
                    {
                        json = jsonconv.CorrigeEstruraJsonRetornoAPITranSanches(json);

                        RetornoAPITranSanches objRetornoAPITranSanches = jsonconv.ConverteJSonParaObject<RetornoAPITranSanches>(json);

                        if (objRetornoAPITranSanches.cte.Count > 0)
                        {
                            foreach (RetornoAPITranSanches.Cte cte in objRetornoAPITranSanches.cte)
                            {
                                foreach (RetornoAPITranSanches.Cte.Ocorrencia ocorrencia in cte.ocorrencias)
                                {
                                    if (Convert.ToDateTime(this.DocDate) <= Convert.ToDateTime(ocorrencia.data_hora))
                                    {
                                        CarregaDadosOcorrencia(ocorrencia);

                                        CarregaPrevisaoEntrega(ocorrencia);

                                        GRAVA_HISTORICO_RASTREIO_PEDIDOS();
                                    }
                                }
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
                    return "Não teve retorno da API da TranSanches";
                }
            }

            return erro;
        }

        private string MontaEnderecoApi()
        {
            this.DocDate = Convert.ToDateTime(ConsultaInfPedidoSAP("DocDate")).ToString("yyyy-MM-dd");

            StringBuilder EnderecoAPICompleto = new StringBuilder();

            EnderecoAPICompleto.Append(this.EnderecoAPI.Replace("login", "cte/ocorrencias"));

            EnderecoAPICompleto.Append("?");

            EnderecoAPICompleto.Append("data_inicial=" + this.DocDate);

            EnderecoAPICompleto.Append("&");

            EnderecoAPICompleto.Append("data_final=" + this.DocDate);

            EnderecoAPICompleto.Append("&");

            EnderecoAPICompleto.Append("nota_fiscal=" + this.NumeroNotaFiscal);

            this.ChaveAPI = objRetornoTokenLoginAPITranSanches.access_token;

            return EnderecoAPICompleto.ToString();
        }

        private string RecuperaTokenLogin()
        {
            try
            {
                this.EnderecoAPI += "login";

                objRetornoTokenLoginAPITranSanches =
                jsonconv.ConverteJSonParaObject<RetornoTokenLoginAPITranSanches>(Chama_API_Json(MontaJsonTokenLogin()));
            }
            catch (Exception ex)
            {
                return "Não foi possível recuperar o token de login.";
            }

            return "";
        }

        private string MontaJsonTokenLogin()
        {
            ChamadaTokenLoginAPITranSanches objTokenLoginAPITranSanches = new ChamadaTokenLoginAPITranSanches();

            objTokenLoginAPITranSanches.tag = Tag;

            objTokenLoginAPITranSanches.senha = Senha;

            objTokenLoginAPITranSanches.usuario_sistema = UsuarioSistema;

            objTokenLoginAPITranSanches.senha_sistema = SenhaSistema;

            objTokenLoginAPITranSanches.empresa = Convert.ToInt32(Empresa);

            return jsonconv.ConverteObjectParaJSon<ChamadaTokenLoginAPITranSanches>(objTokenLoginAPITranSanches);
        }

        public class ChamadaTokenLoginAPITranSanches
        {
            public string tag { get; set; }

            public string senha { get; set; }

            public string usuario_sistema { get; set; }

            public string senha_sistema { get; set; }

            public int empresa { get; set; }
        }

        public class RetornoTokenLoginAPITranSanches
        {
            public string access_token { get; set; }

            public string refresh_token { get; set; }
        }

        private string ConsultaInfPedidoSAP(string retorno)
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("Select OINV.BPLId, ");
                stringSQL.AppendLine("OINV.CardCode, ");
                stringSQL.AppendLine("OINV.DocEntry, ");
                stringSQL.AppendLine("OBPL.TaxIdNum,  ");
                stringSQL.AppendLine("OINV.SeriesStr, ");
                stringSQL.AppendLine("Process.KeyNfe, ");
                stringSQL.AppendLine("ORDR.DocDate, ");
                stringSQL.AppendLine("ISNULL(ISNULL(CRD7.TaxId0,CRD7.TAxId1),OCRD.Fax) CNPJ ");

                stringSQL.AppendLine("from OINV ");
                stringSQL.AppendLine("INNER JOIN ORDR ");
                stringSQL.AppendLine("  ON OINV.CardCode=ORDR.CardCode ");
                stringSQL.AppendLine("INNER JOIN OBPL ");
                stringSQL.AppendLine("  ON OBPL.BPLId=OINV.BPLId ");
                stringSQL.AppendLine("INNER JOIN DBInvOne..Process Process ");
                stringSQL.AppendLine("  ON Process.DocEntry=OINV.DocEntry ");
                stringSQL.AppendLine("INNER JOIN OCRD ");
                stringSQL.AppendLine("  ON OCRD.CardCode=OINV.CardCode ");
                stringSQL.AppendLine("LEFT JOIN CRD7  ");
                stringSQL.AppendLine("  ON CRD7.CardCode=OCRD.CardCode and CRD7.[Address]='ENTREGA' ");

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
                        return row[retorno].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar a dados do SAP.");
            }

            return "";
        }

        public class RetornoAPITranSanches
        {
            public List<Cte> cte { get; set; }

            public class Cte
            {
                public int id { get; set; }
                public string chave_acesso { get; set; }
                public int serie { get; set; }
                public int numero { get; set; }
                public List<Ocorrencia> ocorrencias { get; set; }

                public class Ocorrencia
                {
                    public int id { get; set; }
                    public int codigo { get; set; }
                    public string descricao { get; set; }
                    public string data_hora { get; set; }
                    public NotaFiscal nota_fiscal { get; set; }

                    public class NotaFiscal
                    {
                        public string chave_acesso { get; set; }
                        public string serie { get; set; }
                        public string numero { get; set; }
                    }
                }
            }
        }

        private void CarregaPrevisaoEntrega(RetornoAPITranSanches.Cte.Ocorrencia ocorrencia)
        {
            foreach (RastreioPedidoOcorrencia rastreioPedidoOcorrencia in this.ListRastreioPedidoOcorrencias)
            {
                if (rastreioPedidoOcorrencia.CodigoOcorrencia == ocorrencia.codigo.ToString())
                {
                    if (rastreioPedidoOcorrencia.Descricao == "Emissão do conhecimento de frete")
                    {
                        PrevisaoEntrega = Convert.ToDateTime(ocorrencia.data_hora).AddDays(2).ToString("yyyy-MM-dd");
                    }
                }
            }

            if (PrevisaoEntrega == "" || PrevisaoEntrega == null)
            {
                PrevisaoEntrega = Convert.ToDateTime(this.DocDate).AddDays(2).ToString("yyyy-MM-dd");
            }
        }

        private void CarregaDadosOcorrencia(RetornoAPITranSanches.Cte.Ocorrencia ocorrencia)
        {
            this.DataHistorico = Convert.ToDateTime(ocorrencia.data_hora).ToString("yyyy-MM-dd");

            this.Historico = ocorrencia.descricao;

            this.CodigoOcorrencia = ocorrencia.codigo.ToString();
        }
    }
}