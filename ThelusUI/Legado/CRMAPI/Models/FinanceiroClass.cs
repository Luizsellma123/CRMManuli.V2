using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CRMAPI.Classes;

namespace CRMAPI.Models
{
    public class FinanceiroClass : ConexaoClass
    {
        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();
        enviarEmail OBJMail = new enviarEmail();

        public string CodigoEmpresa { get; set; }
        public string NumeroPedidoCRM { get; set; }
        public string NumeroPedidoSAP { get; set; }
        public string NumeroEsbocoSAP { get; set; }
        public string ConsultaCliente { get; set; }
        public string StatusPedidos { get; set; }
        public string SituacaoPedido { get; set; }

        /*Variavel análise Esboco*/
        public int IDUsuarioSAP { get; set; }
        public string AnalisePedido { get; set; }
        public string HistoricoDetalhado { get; set; }
        public string Historico { get; set; } //Campo deve ser limitado a 254 caracteres
        public string HistoricoPedido { get; set; }
        public int IDMotivo { get; set; } //Campo deve ser limitado a 254 caracteres
        public int IDUsuarioCRM { get; set; }
        public int IDEmpresa { get; set; }
        public int IDPedido { get; set; }
        public int IDStatus { get; set; }
        public int IDCliente { get; set; }
        public string DataHistorico { get; set; }
        public string UsuarioAprovacao { get; set; }
        public string UsuarioAprovacaoSenha { get; set; }

        /*Variavel Nota Fiscal*/
        public int NumeroPrimarioNotaSAP { get; set; }

        /*Variavel DEBUG*/
        private DebugClass OBJDebug = new DebugClass();

        public string AtualizaAnalisarEsbocoPedido()
        {
            this.CarregaApplication();

            //Limpar dados
            OBJComunicacaoServiceLayerSAP.LimparCampos();

            string erro = "";

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("FinanceiroClass - AtualizaAnalisarEsbocoPedido()");
                OBJDebug.SetDescricao("Iniciando Analisar Esboco Pedido");
                OBJDebug.GerarDadosDebug();

                OBJDebug.SetDescricao("FinanceiroClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = this.RecuperaAutorizacoesEsbocoPedidosSAP();

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    OBJComunicacaoServiceLayerSAP.AprovacaoNumero = Convert.ToInt32(row["Codigo"]);
                    OBJComunicacaoServiceLayerSAP.AprovacaoUsuario = this.UsuarioAprovacao;
                    OBJComunicacaoServiceLayerSAP.AprovacaoUsuarioSenha = this.UsuarioAprovacaoSenha;
                    OBJComunicacaoServiceLayerSAP.AprovacaoHistorico = this.Historico; //Campo deve ser limitado a 254 caracteres
                    OBJComunicacaoServiceLayerSAP.AprovacaoDecisao = this.AnalisePedido; //Aprovado, Recusado ou Pendente

                    erro = OBJComunicacaoServiceLayerSAP.AprovarAutorizacao();
                }
            }

            return erro;
        }

        public string AtualizaAnalisarEsbocoNota()
        {
            this.CarregaApplication();

            string erro = "";

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = this.RecuperaAutorizacoesEsbocoNotasSAP();

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    OBJComunicacaoServiceLayerSAP.AprovacaoNumero = Convert.ToInt32(row["Codigo"]);
                    OBJComunicacaoServiceLayerSAP.AprovacaoUsuario = this.UsuarioAprovacao;
                    OBJComunicacaoServiceLayerSAP.AprovacaoUsuarioSenha = this.UsuarioAprovacaoSenha;
                    OBJComunicacaoServiceLayerSAP.AprovacaoHistorico = this.Historico; //Campo deve ser limitado a 254 caracteres
                    OBJComunicacaoServiceLayerSAP.AprovacaoDecisao = this.AnalisePedido; //Aprovado, Recusado ou Pendente

                    erro = OBJComunicacaoServiceLayerSAP.AprovarAutorizacao();
                }
            }

            return erro;
        }

        public DataTable RecuperaAutorizacoesEsbocoPedidosSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select WddCode Codigo, OWTM.Name Nome, OWTM.Remarks Descricao from ";
            StringSQL += "OWDD INNER JOIN OWTM ON OWDD.WtmCode = OWTM.WtmCode ";
            StringSQL += "where OWDD.ObjType = '17' and OWDD.[Status]='W' and OWDD.DraftEntry = '" + this.NumeroEsbocoSAP + "'";

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("FinanceiroClass - RecuperaAutorizacoesEsbocoPedidosSAP()");
                OBJDebug.SetDescricao("SQL: " + StringSQL);
                OBJDebug.GerarDadosDebug();
            }

            OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        public DataTable RecuperaAutorizacoesEsbocoNotasSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select WddCode Codigo, OWTM.Name Nome, OWTM.Remarks Descricao from ";
            StringSQL += "OWDD INNER JOIN OWTM ON OWDD.WtmCode = OWTM.WtmCode ";
            StringSQL += "where OWDD.ObjType = '13' and OWDD.[Status]='W' and OWDD.DraftEntry = '" + this.NumeroEsbocoSAP + "'";

            OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        public string AdicionaEsbocoPedido()
        {
            this.CarregaApplication();

            string erro = "";

            /***************Carega Historico Pedido*******************/
            //VendasWeb.pedido OBJPedido = new VendasWeb.pedido();
            //OBJPedido.carregaDadosPedido(IDEmpresa.ToString(), IDPedido.ToString());
            /***************FIM Carega Historico Pedido*******************/

            OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP = Convert.ToInt32(this.NumeroEsbocoSAP);
            OBJComunicacaoServiceLayerSAP.DataEntregaPedido = this.CalculaPrazoProducao();

            //OBJComunicacaoSAP.HistoricoPedidoSAP = OBJPedido.historicoAntigo;

            erro = OBJComunicacaoServiceLayerSAP.AdicionaPedido();

            if (erro == "")
            {
                this.NumeroPedidoSAP = OBJComunicacaoServiceLayerSAP.EsbocoNovoPedidoSAP;

                //Atualiza numero do pedido SAP no CRM
                erro = this.AtualizaPedidoSAPCRM();

                //Atualiza prazo de produção
                if (erro == "")
                {
                    erro = this.AtualizaDataProducaoPedidoCRM(OBJComunicacaoServiceLayerSAP.DataEntregaPedido);
                }
            }

            return erro;
        }

        public string AdicionaEsbocoNota()
        {
            this.CarregaApplication();

            string erro = "";

            OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP = Convert.ToInt32(this.NumeroEsbocoSAP);
            //erro = OBJComunicacaoServiceLayerSAP.AdicionaNota();

            if (erro == "")
            {
                this.NumeroPrimarioNotaSAP = Convert.ToInt32(OBJComunicacaoServiceLayerSAP.EsbocoNovaNotaSAP);
            }

            return erro;
        }

        public string AtualizaPedidoSAPCRM()
        {
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

        public string CancelaPedidosPeriodoCRM()
        {
            string erro = "";
            string NomeEmpresa = "";
            string NomeCliente = "";
            int IDEmpresaPedido = 0;
            int IDPedido = 0;
            int IDStatusPedido = 0;
            int IDUsuarioCancelamento = 0;
            int EsbocoSAP = 0;

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDOS_CANCELAR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            IDEmpresaPedido = Convert.ToInt32(row["IDEmpresa"]);
                            IDPedido = Convert.ToInt32(row["IDPedido"]);
                            EsbocoSAP = Convert.ToInt32(row["NumeroEsbocoSAP"]);
                            IDStatusPedido = Convert.ToInt32(row["IDStatus"]);
                            IDUsuarioCancelamento = Convert.ToInt32(row["IDUsuario"]);
                            NomeEmpresa = Convert.ToString(row["NomeEmpresa"]);
                            NomeCliente = Convert.ToString(row["NomeCliente"]);

                            erro = this.CancelaPedidoCRM(IDEmpresaPedido, IDPedido, IDStatusPedido, IDUsuarioCancelamento, NomeEmpresa, NomeCliente, EsbocoSAP);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = "erro ao recuperar pedidos";
            }

            return erro;
        }

        public string CancelaPedidoCRM(int IDEmpresaPedido, int IDPedido, int IDStatusPedido, int IDUsuarioCancelamento, string NomeEmpresa, string NomeCliente, int EsbocoSAP)
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_STATUS_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = IDEmpresaPedido;
                    dbCommand.Parameters["@IDPedido"].Value = IDPedido;
                    dbCommand.Parameters["@IDStatus"].Value = IDStatusPedido;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    if (erro == "")
                    {
                        CancelaAutorizacaoSAP(IDUsuarioCancelamento, IDEmpresaPedido, IDPedido, NomeEmpresa, NomeCliente, EsbocoSAP);
                    }

                }
                catch (Exception ex)
                {
                    erro = "Atualiza Status Pedido.";
                }


                return erro;
            }
        }

        public string CancelaAutorizacaoSAP(int IDUsuarioCancelamento, int IDEmpresaPedido, int IDPedido, string NomeEmpresa, string NomeCliente, int EsbocoSAP)
        {
            string erro = "";
            //Recupera usuário do SAP para aprovação
            IDUsuarioCRM = IDUsuarioCancelamento;
            this.RetornaUsuarioSenhaSAP();

            this.HistoricoDetalhado = "Pedido cancelado automaticamente devido a prazo em aberto superior ao permitido.";
            this.Historico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + "Cancelado Financeiro Automatico";
            this.IDMotivo = 6;
            this.AnalisePedido = "Reprovado";
            this.NumeroEsbocoSAP = EsbocoSAP.ToString();

            erro = this.AtualizaAnalisarEsbocoPedido();

            //Atualiza historico do pedido
            if (erro == "")
            {
                this.DataHistorico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                this.IDEmpresa = IDEmpresaPedido;
                this.IDPedido = IDPedido;
                erro = this.AtualizaHistoricoPedidoCRM();
            }

            if (erro == "")
            {
                //Fixo Status 7 --Cancelado
                this.IDStatus = 7;
                erro = this.RetornaPedidoVendedorCRM();
            }

            if (erro == "")
            {
                //Dispara E-mail para o vendedor
                try
                {
                    OBJMail.CodigoEmpresa = IDEmpresaPedido.ToString() + " - " + NomeEmpresa;
                    OBJMail.NumeroPedidoCRM = IDPedido.ToString();
                    OBJMail.NomeCliente = NomeCliente;
                    OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                    OBJMail.Situacao = "Reprovado Automatico";
                    OBJMail.Status = "Reprovado Financeiro Automatico.";
                    OBJMail.HistoricoDetalhado = this.HistoricoDetalhado;
                    if (this.Historico == "") { OBJMail.Historico = "Pedido Reprovado Automatico !"; } else { OBJMail.Historico = this.Historico; }
                    OBJMail.TituloEmail = "Reprovação Automática Pedido " + IDPedido.ToString() + ".";
                    OBJMail.UsuarioCRM = "Sistema.";
                    OBJMail.FormataTexto();

                    //OBJMail.RecuperaEmailDestinatario();
                    OBJMail.EmailDestinatario = OBJMail.RecuperaEmailAlteracaoFinanceiro();
                    //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                    //OBJMail.enviaEmailFormatado();
                    OBJMail.enviaEmailFormatadoAnexo();
                }
                catch (Exception ex)
                {
                    erro = "Erro ao enviar e-mail.";
                }
            }
            else
            {
                erro = "Erro ao enviar e-mail.";
            }

            return erro;
        }

        public string AvisoCancelaPedidosPeriodoCRM()
        {
            string erro = "";
            string NomeEmpresa = "";
            string NomeCliente = "";
            int IDEmpresaPedido = 0;
            int IDPedido = 0;
            int IDStatusPedido = 0;
            int IDUsuarioCRM = 0;
            int EsbocoSAP = 0;
            int DiasParaCancelas = 0;

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDOS_AVISO_CANCELAR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            IDEmpresaPedido = Convert.ToInt32(row["IDEmpresa"]);
                            IDPedido = Convert.ToInt32(row["IDPedido"]);
                            IDStatusPedido = Convert.ToInt32(row["IDStatus"]);
                            IDUsuarioCRM = Convert.ToInt32(row["UsuarioCRM"]);
                            NomeEmpresa = Convert.ToString(row["NomeEmpresa"]);
                            NomeCliente = Convert.ToString(row["NomeCliente"]);
                            DiasParaCancelas = Convert.ToInt32(row["DiasCancelar"]);

                            erro = this.AvisoCancelaPedidosSAP(IDEmpresaPedido, IDPedido, NomeEmpresa, NomeCliente, DiasParaCancelas, IDUsuarioCRM);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = "erro ao recuperar pedidos";
            }

            return erro;
        }

        public string AvisoCancelaPedidosSAP(int IDEmpresaPedido, int IDPedido, string NomeEmpresa, string NomeCliente, int DiasParaCancelars, int IDUsuarioCRM)
        {
            string erro = "";

            this.HistoricoDetalhado = "Pedido será cancelado automaticamente em " + DiasParaCancelars.ToString() + " dias, favor verificar pendências.";
            this.Historico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + "Aviso de Cancelamento Financeiro Automatico";
            this.IDMotivo = 6;

            //Atualiza historico do pedido
            if (erro == "")
            {
                this.DataHistorico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                this.IDEmpresa = IDEmpresaPedido;
                this.IDPedido = IDPedido;
                this.IDUsuarioCRM = IDUsuarioCRM;
                erro = this.AtualizaHistoricoPedidoAvisoCRM();
            }

            if (erro == "")
            {
                //Dispara E-mail para o vendedor
                try
                {
                    OBJMail.CodigoEmpresa = IDEmpresaPedido.ToString() + " - " + NomeEmpresa;
                    OBJMail.NumeroPedidoCRM = IDPedido.ToString();
                    OBJMail.NomeCliente = NomeCliente;
                    OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                    OBJMail.Situacao = "Aviso Cancelamento Automatico";
                    OBJMail.Status = "Aviso Cancelamento Financeiro Automatico.";
                    OBJMail.HistoricoDetalhado = this.HistoricoDetalhado;
                    if (this.Historico == "") { OBJMail.Historico = "Aviso Cancelamento Automatico !"; } else { OBJMail.Historico = this.Historico; }
                    OBJMail.TituloEmail = "Aviso Cancelamento Automática Pedido " + IDPedido.ToString() + ".";
                    OBJMail.UsuarioCRM = "Sistema.";
                    OBJMail.FormataTexto();

                    //OBJMail.RecuperaEmailDestinatario();
                    OBJMail.EmailDestinatario = OBJMail.RecuperaEmailAlteracaoFinanceiro();
                    //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                    //OBJMail.enviaEmailFormatado();
                    OBJMail.enviaEmailFormatadoAnexo();
                }
                catch (Exception ex)
                {
                    erro = "Erro ao enviar e-mail.";
                }
            }
            else
            {
                erro = "Erro ao enviar e-mail.";
            }

            return erro;
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

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioCRM;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.UsuarioAprovacao = row["CodigoUsuarioSAP"].ToString();
                                this.UsuarioAprovacaoSenha = row["SenhaUsuarioSAP"].ToString();
                                this.IDUsuarioSAP = Convert.ToInt32(row["IDUsuarioSAP"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string AtualizaHistoricoPedidoCRM()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_HISTORICO_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMotivo", SqlDbType.Int, 0, "IDMotivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataHistorico", SqlDbType.DateTime, 0, "DataHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@HistoricoDetalhado", SqlDbType.VarChar, 8000, "HistoricoDetalhado"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDMotivo"].Value = this.IDMotivo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioCRM;
                    dbCommand.Parameters["@DataHistorico"].Value = Convert.ToDateTime(this.DataHistorico);
                    dbCommand.Parameters["@HistoricoDetalhado"].Value = this.HistoricoDetalhado;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização do histórico.";
                }
            }

            return erro;
        }

        public string AtualizaHistoricoPedidoAvisoCRM()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_HISTORICO_PEDIDO_VENDA_AVISO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMotivo", SqlDbType.Int, 0, "IDMotivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataHistorico", SqlDbType.DateTime, 0, "DataHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@HistoricoDetalhado", SqlDbType.VarChar, 8000, "HistoricoDetalhado"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDMotivo"].Value = this.IDMotivo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioCRM;
                    dbCommand.Parameters["@DataHistorico"].Value = Convert.ToDateTime(this.DataHistorico);
                    dbCommand.Parameters["@HistoricoDetalhado"].Value = this.HistoricoDetalhado;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização do histórico.";
                }
            }

            return erro;
        }

        public string RetornaPedidoVendedorCRM()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_STATUS_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro no retorno do pedido ao vendedor.";
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

        public string AtualizaDataProducaoPedidoCRM(DateTime DataProducao)
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_PRAZO_PRODUCAO_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataEntrega", SqlDbType.DateTime, 0, "DataEntrega"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@DataEntrega"].Value = DataProducao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro atualizar data entrega pedido.";
                }


                return erro;
            }
        }

        public string ZerarLimitesClientes()
        {
            this.CarregaApplication();

            string erro = "";
            DataTable OBJDataTable = new DataTable();

            //Carrega quantidade de dias para cancelamento
            RecuperaDiasCancelamento();

            OBJDataTable = this.RecuperaClientesZerarLimite();

            erro = OBJComunicacaoServiceLayerSAP.ZeraLimiteClientes(OBJDataTable);

            return erro;
        }

        public DataTable RecuperaClientesZerarLimite()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";
            int NumeroDiasZeramento = 0;

            //recupera numero dias para zeramento
            NumeroDiasZeramento = RecuperaDiasCancelamento();

            if (NumeroDiasZeramento > 0)
            {

                StringSQL += "select top 1 CardCode, OCRD.CreditLine, OCRD.Free_Text as [FreeText] from OCRD ";
                StringSQL += "WHERE OCRD.CardType = 'C' ";
                StringSQL += "and OCRD.CreditLine > 1 ";
                StringSQL += "and NOT EXISTS ";
                StringSQL += "( ";
                StringSQL += "select * from OINV WHERE OINV.CANCELED not in ('C', 'Y') ";
                StringSQL += "and OINV.CardCode = OCRD.CardCode ";
                StringSQL += "and OINV.DocDate > DATEADD(DAY, " + (NumeroDiasZeramento * -1).ToString() + ", GETDATE()) ";
                StringSQL += ") ";
                StringSQL += "and NOT EXISTS ";
                StringSQL += "( ";
                StringSQL += "select * from ORDR WHERE ORDR.CANCELED not in ('C', 'Y') ";
                StringSQL += "and ORDR.DocStatus in ('O') ";
                StringSQL += "and ORDR.CardCode = OCRD.CardCode ";
                StringSQL += "and ORDR.DocDate > DATEADD(DAY, " + (NumeroDiasZeramento * -1).ToString() + ", GETDATE()) ";
                StringSQL += ") ";
                StringSQL += "and NOT EXISTS ";
                StringSQL += "( ";
                StringSQL += "select * from ACRD where ACRD.CardCode = OCRD.CardCode ";
                StringSQL += "and ACRD.UpdateDate > DATEADD(DAY, " + (NumeroDiasZeramento * -1).ToString() + ", GETDATE()) ";
                StringSQL += ") ";
                StringSQL += "and OCRD.CreateDate < DATEADD(DAY, " + (NumeroDiasZeramento * -1).ToString() + ", GETDATE()) ";
                StringSQL += "and OCRD.UpdateDate < DATEADD(DAY, " + (NumeroDiasZeramento * -1).ToString() + ", GETDATE()) ";

                OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(StringSQL);
            }

            return OBJDataTable;
        }

        public int RecuperaDiasCancelamento()
        {
            int NumeroDiasZeramento = 0;
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PARAMETROS_GERAIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Parametro", SqlDbType.VarChar, 8000, "Parametro"));

                    dbCommand.Parameters["@Parametro"].Value = "QUANTIDADEDIASZERARLIMITE";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                NumeroDiasZeramento = Convert.ToInt32(row["ValorNumerico"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return NumeroDiasZeramento;
        }

        public void CarregaApplication()
        {
            //Atribui variavel Global para local DI API
            //if (HttpContext.Current.Application["ApplicationComunicacaoSAP"] != null)
            //{
            //    OBJComunicacaoSAP = (ComunicacaoSAPClass)HttpContext.Current.Application["ApplicationComunicacaoSAP"];
            //}

            //Atribui variavel Global para local Service Layer
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }
        }

    }
}