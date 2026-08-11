using System;
using System.Data;
using CRMAPI.Models;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace CRMAPI.Classes.RastreioPedido
{
    public class RastreioPedido
    {
        #region Campos

        public ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        public ConexaoClass objComunicacaoCRM = new ConexaoClass();

        public VendasWeb.WEBServiceSAP.ClassesWEBService.JsonConversao jsonconv = new VendasWeb.WEBServiceSAP.ClassesWEBService.JsonConversao();


        public string EnderecoAPI { get; set; }

        public string ChaveAPI { get; set; }

        public string EmailAPI { get; set; }

        public string Client { get; set; }

        public string Secret { get; set; }

        public string Tag { get; set; }

        public string Senha { get; set; }

        public string UsuarioSistema { get; set; }

        public string SenhaSistema { get; set; }

        public string Empresa { get; set; }


        public string IDEmpresa { get; set; }

        public string NumeroPedidoSAP { get; set; }

        public string NumeroNotaFiscal { get; set; }


        public int IDTransportador { get; set; }

        public string CodigoOcorrencia { get; set; }

        public string DataHistorico { get; set; }

        public string Historico { get; set; }

        public string Tipo { get; set; }

        public string PrevisaoEntrega { get; set; }


        public List<RastreioPedidoOcorrencia> ListRastreioPedidoOcorrencias { get; set; }

        #endregion

        #region Construtores

        public RastreioPedido()
        {

        }

        public RastreioPedido(RastreiaPedidoModel objRastreiaPedidoModel)
        {
            this.IDEmpresa = objRastreiaPedidoModel.IDEmpresa;

            this.NumeroPedidoSAP = objRastreiaPedidoModel.NumeroPedidoSAP;

            this.NumeroNotaFiscal = objRastreiaPedidoModel.NumeroNotaFiscal;

            this.Tipo = "I";
        }

        #endregion

        #region Métodos

        public virtual string GravaDados()
        {
            return "";
        }

        public string Chama_API(string EnderecoAPI)
        {
            ChamaAPIClass objChamaAPIClass = new ChamaAPIClass();

            objChamaAPIClass.EnderecoAPI = EnderecoAPI;

            return objChamaAPIClass.Chama_API();
        }

        public string Chama_API_Json(string Json)
        {
            ChamaAPIClass objChamaAPIClass = new ChamaAPIClass();

            objChamaAPIClass.EnderecoAPI = this.EnderecoAPI;

            objChamaAPIClass.Json = Json;

            return objChamaAPIClass.Chama_API_Json();
        }

        public string Chama_API_GET_Com_Autenticacao(string EnderecoAPI)
        {
            ChamaAPIClass objChamaAPIClass = new ChamaAPIClass();

            objChamaAPIClass.EnderecoAPI = EnderecoAPI;

            objChamaAPIClass.AuthorizationKey = ChaveAPI;

            return objChamaAPIClass.Chama_API_GET_Com_Autenticacao();
        }

        public string Chama_API_Json_Com_Autenticacao(string Json)
        {
            ChamaAPIClass objChamaAPIClass = new ChamaAPIClass();

            objChamaAPIClass.EnderecoAPI = this.EnderecoAPI;

            objChamaAPIClass.Json = Json;

            objChamaAPIClass.AuthorizationKey = ChaveAPI;

            return objChamaAPIClass.Chama_API_Json_Com_Autenticacao();
        }        

        public void GRAVA_HISTORICO_RASTREIO_PEDIDOS()
        {
            if (PrevisaoEntrega != "")
                Convert.ToDateTime(this.PrevisaoEntrega).ToString("yyyy-MM-dd");

            using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_HISTORICO_RASTREIO_PEDIDOS", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                dbCommand.Parameters.Add(new SqlParameter("@NumeroNotaFiscal", SqlDbType.Int, 0, "NumeroNotaFiscal"));

                dbCommand.Parameters.Add(new SqlParameter("@IDTipo", SqlDbType.Int, 0, "IDTipo"));
                dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));

                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                dbCommand.Parameters.Add(new SqlParameter("@DataHistorico", SqlDbType.VarChar, 8000, "DataHistorico"));
                dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 8000, "Historico"));
                dbCommand.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.VarChar, 8000, "Tipo"));

                dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                dbCommand.Parameters.Add(new SqlParameter("@CodigoOcorrencia", SqlDbType.VarChar, 8000, "CodigoOcorrencia"));

                dbCommand.Parameters.Add(new SqlParameter("@PrevisaoEntrega", SqlDbType.VarChar, 8000, "PrevisaoEntrega"));

                dbCommand.Parameters["@IDEmpresa"].Value = IDEmpresa;
                dbCommand.Parameters["@NumeroPedidoSAP"].Value = NumeroPedidoSAP;
                dbCommand.Parameters["@NumeroNotaFiscal"].Value = NumeroNotaFiscal;

                dbCommand.Parameters["@IDTipo"].Value = 0;
                dbCommand.Parameters["@IDEvento"].Value = 0;
                dbCommand.Parameters["@IDCategoria"].Value = 0;

                dbCommand.Parameters["@IDUsuario"].Value = 0;
                dbCommand.Parameters["@DataHistorico"].Value = DataHistorico;
                dbCommand.Parameters["@Historico"].Value = Historico;
                dbCommand.Parameters["@Tipo"].Value = Tipo;

                dbCommand.Parameters["@IDTransportador"].Value = IDTransportador;
                dbCommand.Parameters["@CodigoOcorrencia"].Value = CodigoOcorrencia;

                dbCommand.Parameters["@PrevisaoEntrega"].Value = PrevisaoEntrega;

                dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                dbCommand.ExecuteNonQuery();
            }
        }

        public string CarregaDadosTransportadora()
        {
            try
            {
                DataTable outputTable = new DataTable();

                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TRANSPORTADORA_RASTREIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));

                    dbCommand.Parameters["@IDTransportador"].Value = IDTransportador;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.EnderecoAPI = row["EnderecoAPI"].ToString();

                            this.ChaveAPI = row["ChaveAPI"].ToString();

                            this.EmailAPI = row["EmailAPI"].ToString();


                            this.Client = row["Client"].ToString();

                            this.Secret = row["Secret"].ToString();

                            this.Tag = row["Tag"].ToString();


                            this.Senha = row["Senha"].ToString();

                            this.UsuarioSistema = row["UsuarioSistema"].ToString();

                            this.SenhaSistema = row["SenhaSistema"].ToString();


                            this.Empresa = row["Empresa"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os dados da transportadora.");
            }

            return "";
        }

        public string CarregaDadosTransportadoraOcorrencias()
        {
            try
            {
                DataTable outputTable = new DataTable();

                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TRANSPORTADORA_RASTREIO_OCORRENCIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));

                    dbCommand.Parameters["@IDTransportador"].Value = IDTransportador;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        ListRastreioPedidoOcorrencias = new List<RastreioPedidoOcorrencia>();

                        foreach (DataRow row in outputTable.Rows)
                        {
                            RastreioPedidoOcorrencia objRastreioPedidoOcorrencia = new RastreioPedidoOcorrencia();

                            objRastreioPedidoOcorrencia.IDTipo = row["IDTipo"].ToString();

                            objRastreioPedidoOcorrencia.IDEvento = row["IDEvento"].ToString();

                            objRastreioPedidoOcorrencia.IDCategoria = row["IDCategoria"].ToString();

                            objRastreioPedidoOcorrencia.CodigoOcorrencia = row["CodigoOcorrencia"].ToString();

                            objRastreioPedidoOcorrencia.Descricao = row["Descricao"].ToString();

                            ListRastreioPedidoOcorrencias.Add(objRastreioPedidoOcorrencia);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os dados da transportadora.");
            }

            return "";
        }

        #endregion
    }
}
