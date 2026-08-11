using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WsHubClienteClass
    {
        public string CodigoClienteSAP { get; set; }
        public int CodigoClienteCRM { get; set; }

        public string cliente { get; set; }
        public string nome { get; set; }
        public string cardType { get; set; }
        public string telefone { get; set; }
        public string cnpj { get; set; }
        public string email { get; set; }
        public string observacao { get; set; }
        public string nome_fantasia { get; set; }
        public string natureza_juridica { get; set; }
        public string indicador_ie { get; set; }
        public string indicador_natureza { get; set; }
        public string indicador_op_consumidor { get; set; }
        public string enquadramento_tributario { get; set; }
        public string carta_ipi { get; set; }
        public string data_Carta_IPI { get; set; }
        public string simples_nacional { get; set; }
        public string produtor_rural { get; set; }
        public string cpom { get; set; }
        public string condicaoPagamento { get; set; }
        public string observacoes { get; set; }
        public string pagamentoUnico { get; set; }
        public string autorizacaoCobranca { get; set; }
        public decimal limiteCredito { get; set; }
        
        public string caract_resina { get; set; }
        public string caract_fitaPP { get; set; }
        public string caract_tintasSolventes { get; set; }
        public string caract_aditivos { get; set; }
        public string caract_tubetesArruelas { get; set; }
        public string caract_maquinasEquipamentos { get; set; }
        public string caract_consultoriasCompany { get; set; }
        public string caract_consultorias { get; set; }
        public string caract_alugueis { get; set; }
        public string caract_servicosAduaneiros { get; set; }
        public string caract_advogados { get; set; }
        public string caract_manutencao { get; set; }
        public string caract_revenda { get; set; }
        public string caract_epis { get; set; }
        public string caract_treinamentos { get; set; }
        public string caract_recrutamentoSelecao { get; set; }
        public string caract_aguaLuzTelefoniaInternet { get; set; }
        public string caract_materiaisEscritorio { get; set; }
        public string caract_materiaisInformatica { get; set; }
        public string caract_computadoresImpressorasNotebooks { get; set; }
        public string caract_caixasPapelao { get; set; }
        public string caract_embalagensGeral { get; set; }
        public string caract_alimentacao { get; set; }
        public string caract_correiosEncomendas { get; set; }
        public string caract_construcaoCivilReformasMelhorias { get; set; }
        public string caract_viagens { get; set; }
        public string caract_beneficios { get; set; }
        public string caract_jornaisRevistasAssinaturas { get; set; }
        public string caract_publicidadeMarketingPropaganda { get; set; }
        public string caract_brindes { get; set; }
        public string caract_outros { get; set; }
        public string vendedor { get; set; }
        

        public List<WsHubClienteFiscalClass> list_fiscal { get; set; }
        public List<WsHubClienteFormaPagamentoClass> list_formaPagamento { get; set; }
        public List<WsHubClienteContatoClass> list_contato { get; set; }
        public List<WsHubClienteEnderecoClass> list_endereco { get; set; }


        //Carrega objeto com dados em branco
        public WsHubClienteClass(){
            this.cliente="";
            this.nome = "";
            this.cardType = "";
            this.telefone = "";
            this.cnpj = "";
            this.email = "";
            this.observacao = "";
            this.nome_fantasia = "";
            this.natureza_juridica = "";
            this.indicador_ie = "";
            this.indicador_natureza = "";
            this.indicador_op_consumidor = "";
            this.enquadramento_tributario = "";
            this.carta_ipi = "";
            this.data_Carta_IPI = "";
            this.simples_nacional = "";
            this.produtor_rural = "";
            this.cpom = "";
            this.condicaoPagamento = "";
            this.observacoes = "";
            this.pagamentoUnico = "";
            this.autorizacaoCobranca = "";
            this.limiteCredito = 0;
            this.caract_resina = "";
            this.caract_fitaPP = "";
            this.caract_tintasSolventes = "";
            this.caract_aditivos = "";
            this.caract_tubetesArruelas = "";
            this.caract_maquinasEquipamentos = "";
            this.caract_consultoriasCompany = "";
            this.caract_consultorias = "";
            this.caract_alugueis = "";
            this.caract_servicosAduaneiros = "";
            this.caract_advogados = "";
            this.caract_manutencao = "";
            this.caract_revenda = "";
            this.caract_epis = "";
            this.caract_treinamentos = "";
            this.caract_recrutamentoSelecao = "";
            this.caract_aguaLuzTelefoniaInternet = "";
            this.caract_materiaisEscritorio = "";
            this.caract_materiaisInformatica = "";
            this.caract_computadoresImpressorasNotebooks = "";
            this.caract_caixasPapelao = "";
            this.caract_embalagensGeral = "";
            this.caract_alimentacao = "";
            this.caract_correiosEncomendas = "";
            this.caract_construcaoCivilReformasMelhorias = "";
            this.caract_viagens = "";
            this.caract_beneficios = "";
            this.caract_jornaisRevistasAssinaturas = "";
            this.caract_publicidadeMarketingPropaganda = "";
            this.caract_brindes = "";
            this.caract_outros = "";
            this.vendedor = "";
        }

        public string ExportaDadosCliente(int _IDCliente, string _Operacao)
        {
            string Retorno = "";

            WsHubClienteFiscalClass ObjWsHubClienteFiscalClass = new WsHubClienteFiscalClass();
            WsHubClienteFormaPagamentoClass ObjWsHubClienteFormaPagamentoClass = new WsHubClienteFormaPagamentoClass();
            WsHubClienteContatoClass ObjWsHubClienteContatoClass = new WsHubClienteContatoClass();
            WsHubClienteEnderecoClass ObjWsHubClienteEnderecoClass = new WsHubClienteEnderecoClass();

            /*Classes utilizadas para alteracao*/
            List<WsHubClienteFiscalClass> ListWsHubClienteFiscalClass = new List<WsHubClienteFiscalClass>();
            List<WsHubClienteEnderecoClass> ListWsHubClienteEnderecoClass = new List<WsHubClienteEnderecoClass>();

            clsConexao ObjclsConexao = new clsConexao();

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(ObjclsConexao.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXPORTA_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = _IDCliente;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.CodigoClienteCRM = _IDCliente;
                                this.CodigoClienteSAP = row["cliente"].ToString();

                                this.cliente = row["cliente"].ToString();
                                this.nome = row["nome"].ToString();
                                this.cardType = row["cardType"].ToString();
                                this.telefone = row["telefone"].ToString();
                                this.cnpj = row["cnpj"].ToString();
                                this.email = row["email"].ToString();
                                this.observacao = row["observacao"].ToString();
                                this.nome_fantasia = row["nome_fantasia"].ToString();
                                this.natureza_juridica = row["natureza_juridica"].ToString();
                                this.indicador_ie = row["indicador_ie"].ToString();
                                this.indicador_natureza = row["indicador_natureza"].ToString();
                                this.indicador_op_consumidor = row["indicador_op_consumidor"].ToString();
                                this.enquadramento_tributario = row["enquadramento_tributario"].ToString();
                                this.carta_ipi = row["carta_ipi"].ToString();
                                this.data_Carta_IPI = row["data_Carta_IPI"].ToString();
                                this.simples_nacional = row["simples_nacional"].ToString();
                                this.produtor_rural = row["produtor_rural"].ToString();
                                this.cpom = row["cpom"].ToString();
                                this.condicaoPagamento = row["condicaoPagamento"].ToString();
                                this.observacoes = row["observacoes"].ToString();
                                this.pagamentoUnico = row["pagamentoUnico"].ToString();
                                this.autorizacaoCobranca = row["autorizacaoCobranca"].ToString();
                                this.limiteCredito = Convert.ToDecimal(row["limiteCredito"]);
                                this.caract_resina = row["caract_resina"].ToString();
                                this.caract_fitaPP = row["caract_fitaPP"].ToString();
                                this.caract_tintasSolventes = row["caract_tintasSolventes"].ToString();
                                this.caract_aditivos = row["caract_aditivos"].ToString();
                                this.caract_tubetesArruelas = row["caract_tubetesArruelas"].ToString();
                                this.caract_maquinasEquipamentos = row["caract_maquinasEquipamentos"].ToString();
                                this.caract_consultoriasCompany = row["caract_consultoriasCompany"].ToString();
                                this.caract_consultorias = row["caract_consultorias"].ToString();
                                this.caract_alugueis = row["caract_alugueis"].ToString();
                                this.caract_servicosAduaneiros = row["caract_servicosAduaneiros"].ToString();
                                this.caract_advogados = row["caract_advogados"].ToString();
                                this.caract_manutencao = row["caract_manutencao"].ToString();
                                this.caract_revenda = row["caract_revenda"].ToString();
                                this.caract_epis = row["caract_epis"].ToString();
                                this.caract_treinamentos = row["caract_treinamentos"].ToString();
                                this.caract_recrutamentoSelecao = row["caract_recrutamentoSelecao"].ToString();
                                this.caract_aguaLuzTelefoniaInternet = row["caract_aguaLuzTelefoniaInternet"].ToString();
                                this.caract_materiaisEscritorio = row["caract_materiaisEscritorio"].ToString();
                                this.caract_materiaisInformatica = row["caract_materiaisInformatica"].ToString();
                                this.caract_computadoresImpressorasNotebooks = row["caract_computadoresImpressorasNotebooks"].ToString();
                                this.caract_caixasPapelao = row["caract_caixasPapelao"].ToString();
                                this.caract_embalagensGeral = row["caract_embalagensGeral"].ToString();
                                this.caract_alimentacao = row["caract_alimentacao"].ToString();
                                this.caract_correiosEncomendas = row["caract_correiosEncomendas"].ToString();
                                this.caract_construcaoCivilReformasMelhorias = row["caract_construcaoCivilReformasMelhorias"].ToString();
                                this.caract_viagens = row["caract_viagens"].ToString();
                                this.caract_beneficios = row["caract_beneficios"].ToString();
                                this.caract_jornaisRevistasAssinaturas = row["caract_jornaisRevistasAssinaturas"].ToString();
                                this.caract_publicidadeMarketingPropaganda = row["caract_publicidadeMarketingPropaganda"].ToString();
                                this.caract_brindes = row["caract_brindes"].ToString();
                                this.caract_outros = row["caract_outros"].ToString();
                                this.vendedor = row["vendedor"].ToString();


                                if (_Operacao == "Inclusão")
                                {
                                    //Consulta as Informações  Fiscais
                                    list_fiscal = ObjWsHubClienteFiscalClass.ExportaDadosClienteFiscal(_IDCliente);
                                }else
                                {
                                    //Utilizado para enviar em branco quando alteração
                                    list_fiscal = ListWsHubClienteFiscalClass;
                                }

                                //Consulta as Formas de Pagamentos
                                list_formaPagamento = ObjWsHubClienteFormaPagamentoClass.ExportaDadosClienteFormaPagamento(_IDCliente);

                                //Consulta os Contatos
                                list_contato = ObjWsHubClienteContatoClass.ExportaDadosClienteContato(_IDCliente);

                                if (_Operacao == "Inclusão")
                                {
                                    //Consulta Endereço
                                    list_endereco = ObjWsHubClienteEnderecoClass.ExportaDadosClienteEndereco(_IDCliente);
                                }
                                else
                                {
                                    //Utilizado para enviar em branco quando alteração
                                    list_endereco = ListWsHubClienteEnderecoClass;
                                }


                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Retorno = ex.Message;
            }


            return Retorno;

        }
        


    }
}