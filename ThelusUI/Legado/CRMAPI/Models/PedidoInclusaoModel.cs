using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class PedidoInclusaoModel : ConexaoClass
    {
        public int BPL_IDAssignedToInvoice { get; set; }
        //public string DocObjectCode { get; set; }
        public string cod_cliente { get; set; }
        public string cod_esboco { get; set; }
        public int cod_vendedor { get; set; }
        public int cond_pag { get; set; }
        public int crm_cod_pedido { get; set; }
        public DateTime data_entrega { get; set; }
        public DateTime data_lancamento { get; set; }
        public string descricao { get; set; }
        public string num_ref_cliente { get; set; }
        public string obs_nf { get; set; }
        public string ped_cliente { get; set; }

        //Campos do CRM
        public int IDEmpresa { get; set; }
        public int IDPedido { get; set; }
        public string CodigoUsuarioCRM { get; set; }
        public string LiberadoProducaoClicheCRM { get; set; }

        public List<PedidoInclusaoLinhaModel> Document_Lines { get; set; }
        public List<PedidoInclusaoDespesasAdicionaisModel> DocumentsAdditionalExpenses { get; set; }
        public List<PedidoInclusaoExtensaoImpostosModel> TaxExtension { get; set; }

        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private string NumeroPedidoSAP { get; set; }
        private int NumeroEsbocoSAP { get; set; }
        private int IDUsuarioCRM { get; set; }
        private string UsuarioAcessoSAP { get; set; }
        private string SenhaUsuarioAcessoSAP { get; set; }
        private int IDStatus { get; set; }
        private DebugClass OBJDebug = new DebugClass();

        public string GravaPedido()
        {
            string erro = "";

            this.CarregaApplication();

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoInclusaoModel - GravaPedido()");
                OBJDebug.SetDescricao("Iniciando Gravacao Pedido");
                OBJDebug.GerarDadosDebug();

                OBJDebug.SetDescricao("PedidoInclusaoModel: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            //Limpar para evitar lixo
            OBJComunicacaoServiceLayerSAP.LimparCampos();

            //Mapeamento dos campos
            MapearCamposPedido();

            erro = OBJComunicacaoServiceLayerSAP.GravarEsbocoPedidoVendaSAP();

            if (erro == "")
            {
                this.NumeroEsbocoSAP = OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP;
                erro = AtualizaNumeroEsbocoSAP();

                if (erro == "")
                {
                    this.AtualizaImpostosEsbocoSAP();
                }
            }

            return erro;
        }

        public void MapearCamposPedido()
        {
            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoInclusaoModel - MapearCamposPedido() - Passo 1");
                OBJDebug.SetDescricao("OBJComunicacaoServiceLayerSAP: " + OBJDebug.SerializarObjeto(OBJComunicacaoServiceLayerSAP));
                OBJDebug.GerarDadosDebug();
            }

            //Limpar Campos do Cabeçalho
            //OBJComunicacaoServiceLayerSAP.LimparCampos();

            //Limpar Campos para evitar lixo
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.LimparDados();

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoInclusaoModel - MapearCamposPedido() - Passo 2");
                OBJDebug.SetDescricao("OBJComunicacaoServiceLayerSAP: " + OBJDebug.SerializarObjeto(OBJComunicacaoServiceLayerSAP));
                OBJDebug.GerarDadosDebug();
            }

            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.NumeroEsbocoSAP = this.cod_esboco;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.CodigoClienteSAP = this.cod_cliente;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.CodigoEmpresaSAP = this.BPL_IDAssignedToInvoice;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.CodigoVendedorSAP = this.cod_vendedor;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.CondicaoPagamentoSAP = this.cond_pag;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.NumeroPedidoCRM = this.crm_cod_pedido;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.DataEntrega = this.data_entrega;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.DataLancamento = this.data_lancamento;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.HistoricoPedido = this.descricao;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.NumeroReferenciaCliente = this.num_ref_cliente;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.ObservacaoNotaFiscal = this.obs_nf;
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.PedidoCliente = this.ped_cliente;

            //Verifica se objeto linha está instanciado esta instanciado
            if (OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoLinhas == null)
            {
                OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoLinhas = new List<ComunicacaoServiceLayerPedidoLinhasSAPClass>();
            }

            //Carrega linhas do pedido
            if (this.Document_Lines != null)
            {
                if (this.Document_Lines.Count > 0)
                {
                    foreach (PedidoInclusaoLinhaModel OBJPedidoLinhas in this.Document_Lines)
                    {
                        ComunicacaoServiceLayerPedidoLinhasSAPClass OBJComunicacaoServiceLayerPedidoLinhas = new ComunicacaoServiceLayerPedidoLinhasSAPClass();

                        OBJComunicacaoServiceLayerPedidoLinhas.CodigoDeposito = OBJPedidoLinhas.Dep;
                        OBJComunicacaoServiceLayerPedidoLinhas.Utilizacao = OBJPedidoLinhas.Usage;
                        OBJComunicacaoServiceLayerPedidoLinhas.CodigoArruela = OBJPedidoLinhas.arruela;
                        OBJComunicacaoServiceLayerPedidoLinhas.CodigoCliche = OBJPedidoLinhas.cliche_prod;
                        OBJComunicacaoServiceLayerPedidoLinhas.CodigoItem = OBJPedidoLinhas.cod_item;
                        OBJComunicacaoServiceLayerPedidoLinhas.CodigoUnidadeMedida = OBJPedidoLinhas.cod_uni_med;
                        OBJComunicacaoServiceLayerPedidoLinhas.PosicaoItem = OBJPedidoLinhas.nItem;
                        OBJComunicacaoServiceLayerPedidoLinhas.NaturezaDestinacao = OBJPedidoLinhas.nat_dest;
                        OBJComunicacaoServiceLayerPedidoLinhas.NomeUnidadeDeMedida = OBJPedidoLinhas.nome_uni_med;
                        OBJComunicacaoServiceLayerPedidoLinhas.Valorunitario = OBJPedidoLinhas.preco;
                        OBJComunicacaoServiceLayerPedidoLinhas.Quantidade = OBJPedidoLinhas.quantidade;
                        OBJComunicacaoServiceLayerPedidoLinhas.ObservacaoItem = OBJPedidoLinhas.texto_livre;
                        OBJComunicacaoServiceLayerPedidoLinhas.NumeroPedidoCliente = OBJPedidoLinhas.xPed;

                        OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoLinhas.Add(OBJComunicacaoServiceLayerPedidoLinhas);
                    }
                }
            }

            //Verifica se objeto despesas adicionais está instanciado esta instanciado
            if (OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoDespesasAdicionais == null)
            {
                OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoDespesasAdicionais = new List<ComunicacaoServiceLayerPedidoDespesasAdicionaisSAPClass>();
            }

            //Carrega despesas adicionais
            if (this.DocumentsAdditionalExpenses != null)
            {
                if (this.DocumentsAdditionalExpenses.Count > 0)
                {
                    foreach (PedidoInclusaoDespesasAdicionaisModel OBJPedidoDespesasAdicionais in this.DocumentsAdditionalExpenses)
                    {
                        ComunicacaoServiceLayerPedidoDespesasAdicionaisSAPClass OBJComunicacaoServiceLayerPedidoDespesasAdicionais = new ComunicacaoServiceLayerPedidoDespesasAdicionaisSAPClass();

                        OBJComunicacaoServiceLayerPedidoDespesasAdicionais.CodigoDespesa = OBJPedidoDespesasAdicionais.ExpenseCode;
                        OBJComunicacaoServiceLayerPedidoDespesasAdicionais.ValorDespesa = OBJPedidoDespesasAdicionais.valor_frete;

                        OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoDespesasAdicionais.Add(OBJComunicacaoServiceLayerPedidoDespesasAdicionais);
                    }
                }
            }

            //Verifica se objeto despesas adicionais está instanciado esta instanciado
            if (OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoExtensaoImpostos == null)
            {
                OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoExtensaoImpostos = new List<ComunicacaoServiceLayerPedidoExtensaoImpostosSAPClass>();
            }

            //Carrega Extensao de Impostos
            if (this.TaxExtension != null)
            {
                if (this.TaxExtension.Count > 0)
                {
                    foreach (PedidoInclusaoExtensaoImpostosModel OBJPedidoInclusaoExtensaoImpostos in this.TaxExtension)
                    {
                        ComunicacaoServiceLayerPedidoExtensaoImpostosSAPClass OBJComunicacaoServiceLayerPedidoExtensaoImpostos = new ComunicacaoServiceLayerPedidoExtensaoImpostosSAPClass();

                        OBJComunicacaoServiceLayerPedidoExtensaoImpostos.CodigoTransportadora = OBJPedidoInclusaoExtensaoImpostos.cod_transp;
                        OBJComunicacaoServiceLayerPedidoExtensaoImpostos.TipoFrete = OBJPedidoInclusaoExtensaoImpostos.tipo_frete;

                        OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.OBJPedidoExtensaoImpostos.Add(OBJComunicacaoServiceLayerPedidoExtensaoImpostos);
                    }
                }
            }

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoInclusaoModel - MapearCamposPedido()  - Passo 3");
                OBJDebug.SetDescricao("OBJComunicacaoServiceLayerSAP: " + OBJDebug.SerializarObjeto(OBJComunicacaoServiceLayerSAP));
                OBJDebug.GerarDadosDebug();
            }
        }

        public string AdicionaEsbocoPedido()
        {
            this.CarregaApplication();

            string erro = "";

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AdicionaEsbocoPedido() - Passo 1");
                OBJDebug.SetDescricao("Iniciando Adicionar Pedido");
                OBJDebug.GerarDadosDebug();

                OBJDebug.SetDescricao("PedidoModelClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            //Limpar campos para evitar lixo Cabecalho
            OBJComunicacaoServiceLayerSAP.LimparCampos();

            //Limpar Campos para evitar lixo
            OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.LimparDados();

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AdicionaEsbocoPedido() - Passo 2");
                OBJDebug.SetDescricao("OBJComunicacaoServiceLayerSAP: " + OBJDebug.SerializarObjeto(OBJComunicacaoServiceLayerSAP));
                OBJDebug.GerarDadosDebug();
            }

            //Seta Usuario conforme politica
            this.RetornaUsuarioPoliticaSAP();

            if (this.UsuarioAcessoSAP != "" && this.UsuarioAcessoSAP != null)
            {
                OBJComunicacaoServiceLayerSAP.OBJComunicacaoEspecificaListaServiceLayer.UsuarioAcessoSAP = this.UsuarioAcessoSAP;
                OBJComunicacaoServiceLayerSAP.OBJComunicacaoEspecificaListaServiceLayer.SenhaUsuarioAcessoSAP = this.SenhaUsuarioAcessoSAP;
                erro = OBJComunicacaoServiceLayerSAP.OBJComunicacaoEspecificaListaServiceLayer.ConectaUsuario();
            }

            if (erro == "")
            {
                if (this.UsuarioAcessoSAP == "" || this.UsuarioAcessoSAP == null)
                {
                    OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP = Convert.ToInt32(this.cod_esboco);
                    OBJComunicacaoServiceLayerSAP.DataEntregaPedido = this.CalculaPrazoProducao();
                    OBJComunicacaoServiceLayerSAP.DataLancamentoPedido = this.data_lancamento;
                    OBJComunicacaoServiceLayerSAP.LiberadoClicheProducaoPedido = this.LiberadoProducaoClicheCRM;

                    erro = OBJComunicacaoServiceLayerSAP.AdicionaPedido();
                }
                else
                {
                    //Instancia de acordo com o usuário logado
                    ComunicacaoEspecificaServiceLayerSAPClass OBJComunicacaoEspecificaServiceLayerSAP = OBJComunicacaoServiceLayerSAP.OBJComunicacaoEspecificaListaServiceLayer.OBJUsuarios
                            .FirstOrDefault(u => u.UsuarioAcessoSAP == this.UsuarioAcessoSAP);

                    OBJComunicacaoEspecificaServiceLayerSAP.EsbocoChaveSAP = Convert.ToInt32(this.cod_esboco);
                    OBJComunicacaoEspecificaServiceLayerSAP.DataEntregaPedido = this.CalculaPrazoProducao();
                    OBJComunicacaoEspecificaServiceLayerSAP.DataLancamentoPedido = this.data_lancamento;
                    OBJComunicacaoEspecificaServiceLayerSAP.LiberadoClicheProducaoPedido = this.LiberadoProducaoClicheCRM;

                    erro = OBJComunicacaoEspecificaServiceLayerSAP.AdicionaPedido();
                }

                if (erro == "")
                {
                    if (OBJDebug.GetGeraDebug())
                    {
                        OBJDebug.SetOperacao("PedidoModelClass - AdicionaEsbocoPedido() - Passo 3");
                        OBJDebug.SetDescricao("OBJComunicacaoServiceLayerSAP: " + OBJDebug.SerializarObjeto(OBJComunicacaoServiceLayerSAP));
                        OBJDebug.GerarDadosDebug();
                    }

                    this.NumeroPedidoSAP = OBJComunicacaoServiceLayerSAP.EsbocoNovoPedidoSAP;

                    //Atualiza numero do pedido SAP no CRM ou envia para análise financeira
                    if (this.NumeroPedidoSAP != "" && this.NumeroPedidoSAP != null)
                    {
                        erro = this.AtualizaPedidoSAPCRM();
                    }
                    else
                    {
                        this.IDStatus = 4;
                        erro = AtualizaSituacaoPedidoSAP();
                    }
                }
            }

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

        public string AtualizaPedidoSAPCRM()
        {
            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AtualizaPedidoSAPCRM() - Passo 1");
                OBJDebug.SetDescricao("PedidoModelClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_PEDIDO_APROVADO_FINANCEIRO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "@NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.IDPedido;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro no aprovação do pedido ao vendedor.";
                }
            }

            return erro;
        }

        public DateTime CalculaPrazoProducao()
        {
            DateTime DataEntrega = DateTime.Now;
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CALCULA_PRAZO_PRODUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataProducao", SqlDbType.DateTime, 0, ParameterDirection.Output, false, 0, 0, "DataProducao", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@DataProducao"].Value = DataEntrega;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    DataEntrega = Convert.ToDateTime(dbCommand.Parameters["@DataProducao"].Value);

                }
                catch (Exception ex)
                {
                    erro = "Erro no retorno do pedido ao vendedor.";
                }
            }

            return DataEntrega;
        }

        public string AtualizaNumeroEsbocoSAP()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PEDIDO_VENDA_DADOS_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroEsbocoSAP", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@NumeroEsbocoSAP"].Value = this.NumeroEsbocoSAP;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = 0;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    this.IDPedido = Convert.ToInt32(dbCommand.Parameters["@IDPedido"].Value);
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do pedido";
                }


                return erro;
            }
        }

        public string AtualizaImpostosEsbocoSAP()
        {
            string erro = "";
            DataTable OBJDataTable = new DataTable();

            OBJDataTable = this.RecuperaImpostosEsbocoPedidoSAP();

            try
            {

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        using (SqlConnection dbConnection = new SqlConnection(strConec))
                        {
                            //Abre Conexão com o banco de dados
                            dbConnection.Open();


                            SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_IMPOSTOS_PEDIDO", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;
                            dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                            dbCommand.Parameters.Add(new SqlParameter("@NumeroEsbocoSAP", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                            dbCommand.Parameters.Add(new SqlParameter("@TotalPedidoImpostos", SqlDbType.Decimal, 0, "TotalPedidoImpostos"));
                            dbCommand.Parameters.Add(new SqlParameter("@ValorImposto", SqlDbType.Decimal, 0, "ValorImposto"));
                            dbCommand.Parameters.Add(new SqlParameter("@PercentualImposto", SqlDbType.Decimal, 0, "PercentualImposto"));
                            dbCommand.Parameters.Add(new SqlParameter("@Imposto", SqlDbType.VarChar, 50, "Imposto"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 50, "CodigoProdutoSAP"));
                            dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                            dbCommand.Parameters["@NumeroPedidoSAP"].Value = 0;
                            dbCommand.Parameters["@NumeroEsbocoSAP"].Value = Convert.ToInt32(this.NumeroEsbocoSAP);
                            dbCommand.Parameters["@TotalPedidoImpostos"].Value = Convert.ToDecimal(row["DocTotal"]);
                            dbCommand.Parameters["@ValorImposto"].Value = Convert.ToDecimal(row["Imposto"]);
                            dbCommand.Parameters["@PercentualImposto"].Value = Convert.ToDecimal(row["PercentualImpostos"]);
                            dbCommand.Parameters["@Imposto"].Value = Convert.ToString(row["Name"]) ?? "";
                            dbCommand.Parameters["@CodigoProdutoSAP"].Value = Convert.ToString(row["ItemCode"]) ?? "";

                            dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                            dbCommand.ExecuteNonQuery();

                            erro = (string)dbCommand.Parameters["@vErro"].Value;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na atualização dos impostos.";
            }

            return erro;
        }

        public DataTable RecuperaImpostosEsbocoPedidoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select ODRF.DocEntry as DocNum, ODRF.DocTotal, DRF1.ItemCode, OSTT.Name, ";
            StringSQL += "((sum(TaxSum)/(case when DRF1.LineTotal=0 then 1 else DRF1.LineTotal end)) * 100) PercentualImpostos, sum(TaxSum) as Imposto ";
            StringSQL += "from ODRF ";
            StringSQL += "INNER JOIN DRF1 ON ODRF.DocEntry=DRF1.DocEntry ";
            StringSQL += "INNER JOIN DRF4 ON DRF4.DocEntry=DRF1.DocEntry and DRF4.LineNum=DRF1.LineNum ";
            StringSQL += "INNER JOIN OSTC ON OSTC.Code=DRF4.StcCode ";
            StringSQL += "INNER JOIN OTFC ON OSTC.TfcId=OTFC.AbsId ";
            StringSQL += "INNER JOIN TFC1 ON TFC1.TfcId=OTFC.AbsId ";
            StringSQL += "INNER JOIN OSTT ON OSTT.AbsId=TFC1.TypeId ";
            StringSQL += "INNER JOIN STC1 ON STC1.STCCode=OSTC.Code and ";
            StringSQL += "TFC1.FmlId=STC1.FmlId and DRF4.StcCode=STC1.STCCode and DRF4.StaCode=STC1.STACODE ";
            StringSQL += "WHERE ODRF.DocEntry=" + this.NumeroEsbocoSAP.ToString() + " ";
            StringSQL += "GROUP BY ODRF.DocEntry, ODRF.DocTotal, DRF1.LineTotal, DRF1.ItemCode, OSTT.Name ";

            OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        public void RetornaUsuarioSenhaSAP()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuarioSAP", SqlDbType.VarChar, 0, "CodigoUsuarioSAP"));

                    //dbCommand.Parameters["@IDUsuario"].Value = 0;
                    dbCommand.Parameters["@CodigoUsuarioSAP"].Value = this.UsuarioAcessoSAP;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.UsuarioAcessoSAP = row["CodigoUsuarioSAP"].ToString();
                                this.SenhaUsuarioAcessoSAP = row["SenhaUsuarioSAP"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string RetornaUsuarioPoliticaSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";
            string erro = "";

            StringSQL += "DECLARE @CardCode as VARCHAR(50) = (select CardCode FROM ODRF WHERE DocEntry=" + this.cod_esboco + ") ";
            StringSQL += "Declare @T Table (doc int,tipo varchar(10),parc int ,vencimento datetime,valor numeric(9,2)) ";
            StringSQL += " ";
            StringSQL += "if(@CardCode <> '') ";
            StringSQL += "BEGIN ";
            StringSQL += "Insert @T Exec [USP_IB_Controle_Contas_Receber_Abertas] @CardCode ";
            StringSQL += "END ";
            StringSQL += " ";
            StringSQL += "SELECT Case ISNULL(t.DIServer,'') ";
            StringSQL += "when '' then '' ";
            StringSQL += "when 'Inad' then 'Inadiplente' ";
            StringSQL += "when 'Bon' then 'Bonificacao' ";
            StringSQL += "when 'Avista' then 'Avista' ";
            StringSQL += "when 'Lim' then 'LimiteTomado' ";
            StringSQL += "else t.DIServer ";
            StringSQL += "end as UserDiServer ";
            StringSQL += "FROM ( ";
            StringSQL += "SELECT ";
            StringSQL += "CASE WHEN  GroupNum  IN (SELECT code FROM [@IB_BLOQCONDPGTO])  THEN 'Avista' ";
            StringSQL += "WHEN  (Select count(vencimento) from @T where vencimento < CONVERT (date, CURRENT_TIMESTAMP)  ) >= 1 THEN 'Inad' ";
            StringSQL += "WHEN  (SELECT COUNT(DocEntry)   FROM DRF1 l WHERE  (l.CFOPCode IN('5910','6910','7910') OR l.Usage='12')  AND l.docentry = c.docEntry) > 0 THEN 'Bon' ";
            StringSQL += "WHEN GroupNum=351 THEN  'Bon' ";
            StringSQL += "WHEN (Select (isnull(OCRD.CreditLine,0)- ISNULL(OCRD.Balance,0)-isnull(OCRD.OrdersBal,0))-c.DocTotal ";
            StringSQL += "FROM OCRD WHERE OCRD.CardCode=c.CardCode)<0 Then 'Lim' ";
            StringSQL += "END AS DIServer ";
            StringSQL += "FROM ";
            StringSQL += "ODRF c ";
            StringSQL += "WHERE c.DocEntry=" + this.cod_esboco + ") t ";

            OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.UsuarioAcessoSAP = Convert.ToString(row["UserDiServer"]) ?? "";
                    this.RetornaUsuarioSenhaSAP();
                }
            }


            return erro;
        }

        public string AtualizaSituacaoPedidoSAP()
        {
            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AtualizaSituacaoPedidoSAP() - Passo 1");
                OBJDebug.SetDescricao("PedidoModelClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SITUACAO_PEDIDO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    this.IDPedido = Convert.ToInt32(dbCommand.Parameters["@IDPedido"].Value);
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do pedido";
                }


                return erro;
            }
        }

        public string AtualizaIntegracaoPedido()
        {
            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AtualizaIntegracaoPedido() - Passo 1");
                OBJDebug.SetDescricao("PedidoModelClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            string erro = "";

            this.CarregaApplication();

            //Limpar para evitar lixo
            OBJComunicacaoServiceLayerSAP.LimparCampos();


            //Mapeamento dos campos
            MapearCamposPedido();

            //Limpa campos de esboço da chave
            OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP = 0;

            //Instancia classe pedido
            VendasWeb.pedido novoPedido = new VendasWeb.pedido();
            VendasWeb.GerencialVendas.PedidoClass PedidoClass = new VendasWeb.GerencialVendas.PedidoClass();
            VendasWeb.funcoesBD mdlFuncoesBD = new VendasWeb.funcoesBD();
            novoPedido.carregaDadosPedido(IDEmpresa.ToString(), IDPedido.ToString());

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AtualizaIntegracaoPedido() - Passo 2");
                OBJDebug.SetDescricao("novoPedido: " + OBJDebug.SerializarObjeto(novoPedido));
                OBJDebug.GerarDadosDebug();
            }

            //Verifica se já existe um pedido com aquele número de esboco e atualiza o pedido
            if (string.IsNullOrEmpty(novoPedido.NumeroEsbocoSAP) || novoPedido.NumeroEsbocoSAP == "0")
            {
                OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.CodigoEmpresaSAP = IDEmpresa;
                OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.NumeroPedidoCRM = IDPedido;
                erro = OBJComunicacaoServiceLayerSAP.RetornaNumeroEsbocoSAP();

                if (erro == "")
                {
                    if (OBJDebug.GetGeraDebug())
                    {
                        OBJDebug.SetOperacao("PedidoModelClass - AtualizaIntegracaoPedido() - Passo 3");
                        OBJDebug.SetDescricao("OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP: " + OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP.ToString());
                        OBJDebug.GerarDadosDebug();
                    }

                    if (OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP != 0)
                    {
                        novoPedido.NumeroEsbocoSAP = OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP.ToString();
                        this.NumeroEsbocoSAP = Convert.ToInt32(novoPedido.NumeroEsbocoSAP);
                        erro = this.AtualizaNumeroEsbocoSAP();
                    }
                }
            }

            //Verifica se já existe pedido atrelado com o esboço
            if (erro == "")
            {
                if (string.IsNullOrEmpty(novoPedido.NumeroPedidoSAP) || novoPedido.NumeroPedidoSAP == "0")
                {
                    OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP = OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP == 0 ? Convert.ToInt32(novoPedido.NumeroEsbocoSAP) : OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP;

                    OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.CodigoEmpresaSAP = IDEmpresa;
                    OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.NumeroPedidoCRM = IDPedido;
                    novoPedido.NumeroPedidoSAP = OBJComunicacaoServiceLayerSAP.RetornaNumeroPedidoSAP();

                    if (OBJDebug.GetGeraDebug())
                    {
                        OBJDebug.SetOperacao("PedidoModelClass - AtualizaIntegracaoPedido() - Passo 4");
                        OBJDebug.SetDescricao("novoPedido.NumeroPedidoSAP: " + novoPedido.NumeroPedidoSAP);
                        OBJDebug.GerarDadosDebug();
                    }

                    if (!string.IsNullOrEmpty(novoPedido.NumeroPedidoSAP))
                    {
                        this.NumeroPedidoSAP = novoPedido.NumeroPedidoSAP;
                        erro = this.AtualizaNumeroPedidoSAP();
                    }
                }
            }

            //Verifica se esta faltando Esboco ou Pedido e faz a atualizacao no SAP
            if (erro == "")
            {
                //if (string.IsNullOrEmpty(novoPedido.NumeroEsbocoSAP) || string.IsNullOrEmpty(novoPedido.NumeroPedidoSAP))
                if (string.IsNullOrEmpty(novoPedido.NumeroEsbocoSAP))
                {
                    if (OBJDebug.GetGeraDebug())
                    {
                        OBJDebug.SetOperacao("PedidoModelClass - AtualizaIntegracaoPedido() - Passo 5");
                        OBJDebug.SetDescricao("novoPedido: " + OBJDebug.SerializarObjeto(novoPedido));
                        OBJDebug.GerarDadosDebug();
                    }

                    //Envia para o SAP caso ESBOCO não exista
                    if (novoPedido.NumeroEsbocoSAP == "0" || novoPedido.NumeroEsbocoSAP == "" || novoPedido.NumeroEsbocoSAP == null)
                    {
                        erro = novoPedido.EnviaPedidoSAP();

                        OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.CodigoEmpresaSAP = IDEmpresa;
                        OBJComunicacaoServiceLayerSAP.OBJPedidoVenda.NumeroPedidoCRM = IDPedido;
                        erro = OBJComunicacaoServiceLayerSAP.RetornaNumeroEsbocoSAP();

                        if (erro == "")
                        {
                            if (OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP != 0)
                            {
                                novoPedido.NumeroEsbocoSAP = OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP.ToString();
                                this.NumeroEsbocoSAP = Convert.ToInt32(novoPedido.NumeroEsbocoSAP);
                                erro = this.AtualizaNumeroEsbocoSAP();
                            }
                        }
                    }
                }
            }

            //Se não deu erro recupera os impostos
            if (erro == "")
            {
                //Recupera impostos do pedido
                PedidoClass.EmpCod = IDEmpresa.ToString();
                PedidoClass.PedVendaNum = IDPedido.ToString();
                PedidoClass.NumeroPedidoSAP = string.IsNullOrEmpty(novoPedido.NumeroPedidoSAP) ? 0 : Convert.ToInt32(novoPedido.NumeroPedidoSAP);
                PedidoClass.NumeroEsbocoSAP = string.IsNullOrEmpty(novoPedido.NumeroEsbocoSAP) ? 0 : Convert.ToInt32(novoPedido.NumeroEsbocoSAP);
                PedidoClass.Consulta_Pedido();

                if (OBJDebug.GetGeraDebug())
                {
                    OBJDebug.SetOperacao("PedidoModelClass - AtualizaIntegracaoPedido() - Passo 6");
                    OBJDebug.SetDescricao("PedidoClass: " + OBJDebug.SerializarObjeto(PedidoClass));
                    OBJDebug.GerarDadosDebug();
                }

                //Somente faz este bloco se estiver autorizado e não existir número de SAP
                if (string.IsNullOrEmpty(novoPedido.NumeroPedidoSAP) || novoPedido.NumeroPedidoSAP == "0")
                {
                    //Verifica se Pedido não esta no financeiro
                    if(OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP != 0 && novoPedido.statusPedio == "3")
                    {
                        OBJComunicacaoServiceLayerSAP.RetornaStatusFinanceiroEsbocoSAP();
                        switch (OBJComunicacaoServiceLayerSAP.AprovacaoEsbocoStatusSAP)
                        {
                            case "W":
                                    this.IDStatus = 4;
                                    novoPedido.statusPedio = this.IDStatus.ToString();
                                    break;
                            default:
                                this.IDStatus = Convert.ToInt32(novoPedido.statusPedio);
                                break;
                        }

                        //Verifica se Status Precisa ser alterado para Análise Interna
                        if (this.IDStatus == 4)
                        {
                            AtualizaSituacaoPedidoSAP();
                        }
                    }

                    if (novoPedido.statusPedio == "3")
                    {
                        if (OBJDebug.GetGeraDebug())
                        {
                            OBJDebug.SetOperacao("PedidoModelClass - AtualizaIntegracaoPedido() - Passo 7");
                            OBJDebug.SetDescricao("novoPedido: " + OBJDebug.SerializarObjeto(novoPedido));
                            OBJDebug.GerarDadosDebug();
                        }

                        erro = mdlFuncoesBD.aprovaPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, CodigoUsuarioCRM, novoPedido.codigoEntidade.ToString());


                        //Se não der erro atualiza Histórico
                        if (erro == "")
                        {
                            //Atualiza o historico de acordo com historico CRM
                            erro = novoPedido.AtualizarHistoricoPedidoSAPAPI();

                            if (novoPedido.statusPedio == "3")
                            {
                                if (erro == "")
                                {
                                    erro = novoPedido.TransformaEsbocoPedido();
                                }
                            }

                        }

                    }
                }else
                {
                    //Se não der erro nos blocos anteriores verifica o Status do Pedido
                    if (erro == "")
                    {
                        PedidoModelClass OBJPedidoModel = new PedidoModelClass();

                        OBJPedidoModel.IDEmpresa = this.IDEmpresa;
                        OBJPedidoModel.IDPedido = this.IDPedido;
                        OBJPedidoModel.NumeroPedidoSAP = novoPedido.NumeroPedidoSAP;

                        if (OBJDebug.GetGeraDebug())
                        {
                            OBJDebug.SetOperacao("PedidoModelClass - AtualizaIntegracaoPedido() - Passo 8");
                            OBJDebug.SetDescricao("novoPedido: " + OBJDebug.SerializarObjeto(novoPedido));
                            OBJDebug.GerarDadosDebug();
                        }

                        erro = OBJPedidoModel.RetornaStatusPedidoSAP();

                        if (erro == "")
                        {
                            erro = OBJPedidoModel.AtualizaStatusPedidoCRM();
                        }
                    }
                }
            }

            return erro;
        }

        public string AtualizaNumeroPedidoSAP()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_NUMERO_PEDIDO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    this.IDPedido = Convert.ToInt32(dbCommand.Parameters["@IDPedido"].Value);
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do pedido";
                }


                return erro;
            }
        }

    }
}