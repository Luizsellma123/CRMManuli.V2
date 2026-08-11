using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class CadastroClienteModel : ConexaoClass
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
        public int condicaoPagamento { get; set; }
        public string observacoes { get; set; }
        public string pagamentoUnico { get; set; }
        public string autorizacaoCobranca { get; set; }
        public double limiteCredito { get; set; }

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
        public int vendedor { get; set; }

        public List<ClienteFiscalModel> list_fiscal { get; set; }
        public List<ClienteFormaPagamentoModel> list_formaPagamento { get; set; }
        public List<ClienteContatoModel> list_contato { get; set; }
        public List<ClienteEnderecoModel> list_endereco { get; set; }

        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        public string SalvaDadosContatoCliente()
        {
            string erro = "";

            this.CarregaApplication();

            DataTable OBJDataTableClienteContatos = new DataTable();

            OBJDataTableClienteContatos = this.RecuperaDadosContatosCliente();

            //OBJComunicacaoServiceLayerSAP.CodigoClienteSAP = this.CodigoClienteSAP;
            //erro = OBJComunicacaoServiceLayerSAP.AdicionarContato(OBJDataTableClienteContatos);


            if (OBJDataTableClienteContatos.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTableClienteContatos.Rows)
                {
                    if (erro == "")
                    {
                        OBJComunicacaoServiceLayerSAP.CodigoClienteSAP = Convert.ToString(row["CodigoClienteSAP"]);
                        OBJComunicacaoServiceLayerSAP.CodigoClienteTipoContato = Convert.ToString(row["TipoContato"]);
                        OBJComunicacaoServiceLayerSAP.CodigoClienteLinha = Convert.ToInt32(row["LinhaCliente"]);
                        OBJComunicacaoServiceLayerSAP.CodigoClientePrimeiroNome = Convert.ToString(row["Nome"]);
                        OBJComunicacaoServiceLayerSAP.CodigoClienteUltimoNome = "";
                        OBJComunicacaoServiceLayerSAP.CodigoClienteEmail = Convert.ToString(row["Email"]);
                        OBJComunicacaoServiceLayerSAP.CodigoClienteTelefone1 = Convert.ToString(row["Telefone"]);
                        OBJComunicacaoServiceLayerSAP.InternalCode = Convert.ToInt32(row["InternalCode"]);

                        switch (Convert.ToString(row["Operacao"]))
                        {
                            case "ADD":
                                erro = OBJComunicacaoServiceLayerSAP.AdicionarContato();
                                break;
                            case "UPD":
                                //erro = OBJComunicacaoServiceLayerSAP.AtualizaContato();
                                break;
                            case "DEL":
                                erro = OBJComunicacaoServiceLayerSAP.ExcluirContato();
                                break;
                        }
                    }
                }
            }

            return erro;
        }

        public DataTable RecuperaDadosContatosCliente()
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_API_CLIENTE_CONTATOS", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 8000, "CodigoClienteSAP"));

                    dbCommand.Parameters["@CodigoClienteSAP"].Value = this.CodigoClienteSAP;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                //erro = "erro ao recuperar Ordem Serviço";
            }


            return outputTable;
        }


        public string Gravacliente()
        {
            string erro = "";

            this.CarregaApplication();

            //Mapeamento dos campos
            MapearCamposCliente();

            erro = OBJComunicacaoServiceLayerSAP.GravarClienteSAP();

            if (erro == "")
            {
                this.CodigoClienteSAP = OBJComunicacaoServiceLayerSAP.CodigoClienteSAP;
                erro = AlteraClienteCodigoSAP();
            }

            return erro;
        }

        public string Atualizacliente()
        {
            string erro = "";

            this.CarregaApplication();

            //Mapeamento dos campos
            MapearCamposCliente();

            erro = OBJComunicacaoServiceLayerSAP.AtualizaClienteSAP();

            return erro;
        }

        public void CarregaApplication()
        {
            //Atribui variavel Global para local Service Layer
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }
        }

        public void MapearCamposCliente()
        {
            if(this.CodigoClienteSAP != "" && this.CodigoClienteSAP != null)
            {
                OBJComunicacaoServiceLayerSAP.CodigoClienteSAP = this.CodigoClienteSAP;
            }

            //Limpar Campos para evitar lixo
            OBJComunicacaoServiceLayerSAP.OBJCliente.LimparDados();

            OBJComunicacaoServiceLayerSAP.OBJCliente.Series = 73;
            OBJComunicacaoServiceLayerSAP.OBJCliente.CardName = this.nome;
            OBJComunicacaoServiceLayerSAP.OBJCliente.CardType = this.cardType;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Phone1 = this.telefone;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Fax = this.cnpj;
            OBJComunicacaoServiceLayerSAP.OBJCliente.EmailAddress = this.email;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Notes = this.observacao;
            OBJComunicacaoServiceLayerSAP.OBJCliente.SalesPersonCode = this.vendedor;
            OBJComunicacaoServiceLayerSAP.OBJCliente.AliasName = this.nome_fantasia;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_IB_NAT_JURIDICA = this.natureza_juridica;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_TX_IndIEDest = this.indicador_ie;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_TX_IndNat = this.indicador_natureza;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_TX_IndFinal = this.indicador_op_consumidor;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_IB_Enquadr_Trib = this.enquadramento_tributario;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_IB_CartaIPI = this.carta_ipi;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_TX_SN = this.simples_nacional;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_TX_ProdRural = this.produtor_rural;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_IB_CPOM = this.cpom;
            OBJComunicacaoServiceLayerSAP.OBJCliente.PayTermsGrpCode = this.condicaoPagamento;
            OBJComunicacaoServiceLayerSAP.OBJCliente.FreeText = this.observacoes;
            OBJComunicacaoServiceLayerSAP.OBJCliente.SinglePayment = this.pagamentoUnico;
            OBJComunicacaoServiceLayerSAP.OBJCliente.CollectionAuthorization = this.autorizacaoCobranca;
            OBJComunicacaoServiceLayerSAP.OBJCliente.CreditLimit = this.limiteCredito;
            OBJComunicacaoServiceLayerSAP.OBJCliente.U_IB_DataCartaIPI = this.data_Carta_IPI;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties1 = this.caract_resina;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties2 = this.caract_fitaPP;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties3 = this.caract_tintasSolventes;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties4 = this.caract_aditivos;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties5 = this.caract_tubetesArruelas;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties6 = this.caract_maquinasEquipamentos;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties7 = this.caract_consultoriasCompany;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties8 = this.caract_consultorias;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties9 = this.caract_alugueis;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties10 = this.caract_servicosAduaneiros;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties11 = this.caract_advogados;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties12 = this.caract_manutencao;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties13 = this.caract_revenda;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties14 = this.caract_epis;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties15 = this.caract_treinamentos;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties16 = this.caract_recrutamentoSelecao;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties17 = this.caract_aguaLuzTelefoniaInternet;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties18 = this.caract_materiaisEscritorio;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties19 = this.caract_materiaisInformatica;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties20 = this.caract_computadoresImpressorasNotebooks;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties21 = this.caract_caixasPapelao;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties22 = this.caract_embalagensGeral;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties23 = this.caract_alimentacao;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties24 = this.caract_correiosEncomendas;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties25 = this.caract_construcaoCivilReformasMelhorias;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties26 = this.caract_viagens;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties27 = this.caract_beneficios;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties28 = this.caract_jornaisRevistasAssinaturas;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties29 = this.caract_publicidadeMarketingPropaganda;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties30 = this.caract_brindes;
            OBJComunicacaoServiceLayerSAP.OBJCliente.Properties31 = this.caract_outros;

            //Verifica se objeto linha está instanciado esta instanciado
            if (OBJComunicacaoServiceLayerSAP.OBJCliente.ContactEmployees == null)
            {
                OBJComunicacaoServiceLayerSAP.OBJCliente.ContactEmployees = new List<ComunicacaoServiceLayerClienteContatoClass>();
            }

            //Carrega linhas do pedido
            if (this.list_contato.Count > 0)
            {
                foreach (ClienteContatoModel OBJClienteContato in this.list_contato)
                {
                    ComunicacaoServiceLayerClienteContatoClass OBJComunicacaoServiceLayerClienteContato = new ComunicacaoServiceLayerClienteContatoClass();

                    OBJComunicacaoServiceLayerClienteContato.Name = OBJClienteContato.id;
                    OBJComunicacaoServiceLayerClienteContato.FirstName = OBJClienteContato.nome;
                    OBJComunicacaoServiceLayerClienteContato.Phone1 = OBJClienteContato.telefone;
                    OBJComunicacaoServiceLayerClienteContato.E_Mail = OBJClienteContato.email;

                    OBJComunicacaoServiceLayerSAP.OBJCliente.ContactEmployees.Add(OBJComunicacaoServiceLayerClienteContato);
                }
            }

            //Verifica se objeto despesas adicionais está instanciado esta instanciado
            if (OBJComunicacaoServiceLayerSAP.OBJCliente.BPAddresses == null)
            {
                OBJComunicacaoServiceLayerSAP.OBJCliente.BPAddresses = new List<ComunicacaoServiceLayerClienteEnderecoClass>();
            }

            //Carrega despesas adicionais
            if (this.list_endereco.Count > 0)
            {
                foreach (ClienteEnderecoModel OBJClienteEndereco in this.list_endereco)
                {
                    ComunicacaoServiceLayerClienteEnderecoClass OBJComunicacaoServiceLayerClienteEndereco = new ComunicacaoServiceLayerClienteEnderecoClass();

                    OBJComunicacaoServiceLayerClienteEndereco.AddressName = OBJClienteEndereco.id_endereco;
                    OBJComunicacaoServiceLayerClienteEndereco.AddressType = OBJClienteEndereco.tipo_endereco;
                    OBJComunicacaoServiceLayerClienteEndereco.Street = OBJClienteEndereco.rua;
                    OBJComunicacaoServiceLayerClienteEndereco.StreetNo = OBJClienteEndereco.numero;
                    OBJComunicacaoServiceLayerClienteEndereco.BuildingFloorRoom = OBJClienteEndereco.complemento;
                    OBJComunicacaoServiceLayerClienteEndereco.ZipCode = OBJClienteEndereco.cep;
                    OBJComunicacaoServiceLayerClienteEndereco.Block = OBJClienteEndereco.bairro;
                    OBJComunicacaoServiceLayerClienteEndereco.City = OBJClienteEndereco.cidade;
                    OBJComunicacaoServiceLayerClienteEndereco.State = OBJClienteEndereco.estado;
                    OBJComunicacaoServiceLayerClienteEndereco.County = OBJClienteEndereco.municipio;
                    OBJComunicacaoServiceLayerClienteEndereco.Country = OBJClienteEndereco.pais;
                    OBJComunicacaoServiceLayerClienteEndereco.TypeOfAddress = OBJClienteEndereco.tipo_logradouro;

                    OBJComunicacaoServiceLayerSAP.OBJCliente.BPAddresses.Add(OBJComunicacaoServiceLayerClienteEndereco);
                }
            }

            //Verifica se objeto despesas adicionais está instanciado esta instanciado
            if (OBJComunicacaoServiceLayerSAP.OBJCliente.BPPaymentMethods == null)
            {
                OBJComunicacaoServiceLayerSAP.OBJCliente.BPPaymentMethods = new List<ComunicacaoServiceLayerClientePagamentoClass>();
            }

            //Carrega Extensao de Impostos
            if (this.list_formaPagamento.Count > 0)
            {
                foreach (ClienteFormaPagamentoModel OBJClienteFormasPagamento in this.list_formaPagamento)
                {
                    ComunicacaoServiceLayerClientePagamentoClass OBJComunicacaoServiceLayerClientePagamento = new ComunicacaoServiceLayerClientePagamentoClass();

                    OBJComunicacaoServiceLayerClientePagamento.PaymentMethodCode = OBJClienteFormasPagamento.codFormaPagamento;

                    OBJComunicacaoServiceLayerSAP.OBJCliente.BPPaymentMethods.Add(OBJComunicacaoServiceLayerClientePagamento);
                }
            }

            //Verifica se objeto despesas adicionais está instanciado esta instanciado
            if (OBJComunicacaoServiceLayerSAP.OBJCliente.BPFiscalTaxIDCollection == null)
            {
                OBJComunicacaoServiceLayerSAP.OBJCliente.BPFiscalTaxIDCollection = new List<ComunicacaoServiceLayerClienteFiscalClass>();
            }

            //Carrega Extensao de Impostos
            if (this.list_fiscal.Count > 0)
            {
                foreach (ClienteFiscalModel OBJClienteFiscal in this.list_fiscal)
                {
                    ComunicacaoServiceLayerClienteFiscalClass OBJComunicacaoServiceLayerClienteFiscal = new ComunicacaoServiceLayerClienteFiscalClass();

                    OBJComunicacaoServiceLayerClienteFiscal.TaxId0 = OBJClienteFiscal.cnpj;
                    OBJComunicacaoServiceLayerClienteFiscal.Address = OBJClienteFiscal.address;
                    OBJComunicacaoServiceLayerClienteFiscal.TaxId1 = OBJClienteFiscal.inscricaoEstadual;
                    OBJComunicacaoServiceLayerClienteFiscal.CNAECode = OBJClienteFiscal.cnae;
                    OBJComunicacaoServiceLayerClienteFiscal.TaxId8 = OBJClienteFiscal.suframa;
                    OBJComunicacaoServiceLayerClienteFiscal.AddrType = OBJClienteFiscal.tipoEndereco;

                    OBJComunicacaoServiceLayerSAP.OBJCliente.BPFiscalTaxIDCollection.Add(OBJComunicacaoServiceLayerClienteFiscal);
                }
            }

        }

        public string AlteraClienteCodigoSAP()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_CLIENTE_CODIGO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 8000, "CodigoClienteSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDCliente"].Value = this.CodigoClienteCRM;
                    dbCommand.Parameters["@CodigoClienteSAP"].Value = this.CodigoClienteSAP;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro AlteraClienteCodigoSAP." + ex.Message;
                }
            }

            return erro;
        }

        public string AtualizaClienteVendedor()
        {
            string erro = "";

            this.CarregaApplication();

            //Mapeamento dos campos
            MapearCamposCliente();

            erro = OBJComunicacaoServiceLayerSAP.AtualizaClienteVendedorSAP();

            return erro;
        }

    }
}