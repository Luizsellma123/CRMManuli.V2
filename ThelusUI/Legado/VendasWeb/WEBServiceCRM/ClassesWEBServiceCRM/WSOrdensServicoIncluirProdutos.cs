using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using VendasWeb.GerencialVendas;
using System.Web;
using VendasWeb.classes;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSOrdensServicoIncluirProdutos : GerencialVendas.clsConexao
    {

        #region Campos Principais

        public int IDEmpresa { get; set; }
        public int IDOrdemServico { get; set; }
        public int NumeroPedidosSAP { get; set; }
        public int IDITemSAP { get; set; }
        public int DocEntry { get; set; }

        public string Cliente { get; set; }
        public string Empresa { get; set; }
        public int NumeroPedidoSAP { get; set; }
        public string StatusPedidoSAP { get; set; }
        public int NumeroPedidoCRM { get; set; }
        public string StatusPedidoCRM { get; set; }
        public string DataEmissao { get; set; }
        public string DataEntrega { get; set; }
        public string EmbarqueImediato { get; set; }
        public string NomeVendedor { get; set; }
        public string Produto { get; set; }
        public string ProdutoRelacional { get; set; }
        public string HistoricoPedido { get; set; }
        public string Cliche { get; set; }
        public string ImagemCliche { get; set; }

        #endregion

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        public void RecuperaOrdensServicoIncluirProdutos()
        {
            DataTable OBJDataTable = new DataTable();

            funcoes mdlfuncsMan = new funcoes();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_ORDENS_SERVICO_INCLUIR_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDITemSAP", SqlDbType.Int, 0, "IDITemSAP"));

                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.DocEntry;
                    dbCommand.Parameters["@IDITemSAP"].Value = this.IDITemSAP;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        OBJDataTable.Load(dataReader);
                    }
                }

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        this.Cliente = Convert.ToString(row["Cliente"]);
                        this.Empresa = Convert.ToString(row["Empresa"]);
                        this.NumeroPedidoSAP = Convert.ToInt32(row["NumeroPedidoSAP"]);
                        this.StatusPedidoSAP = Convert.ToString(row["StatusPedidoSAP"]);
                        this.NumeroPedidoCRM = Convert.ToInt32(row["NumeroPedidoCRM"]);
                        this.StatusPedidoCRM = Convert.ToString(row["StatusPedidoCRM"]);
                        this.DataEmissao = Convert.ToString(row["DataEmissao"]);
                        this.DataEntrega = Convert.ToString(row["DataEntrega"]);
                        this.EmbarqueImediato = Convert.ToString(row["EmbarqueImediato"]);
                        this.NomeVendedor = Convert.ToString(row["NomeVendedor"]);
                        this.Produto = Convert.ToString(row["Produto"]);
                        this.ProdutoRelacional = Convert.ToString(row["ProdutoRelacional"]);
                        this.HistoricoPedido = Convert.ToString(row["HistoricoPedido"]);
                        this.Cliche = Convert.ToString(row["Cliche"]);
                        this.ImagemCliche = Convert.ToString(row["ImagemCliche"]);

                        //this.ImagemCliche = "2MC GOMADA.jpg";

                        string caminho = "\\\\192.168.0.2\\Sap\\Imagens\\";
                        string nome = ImagemCliche;

                        System.Drawing.Image img = Bitmap.FromFile(caminho + nome);

                        byte[] b = ConvertImageToByteArray(img, ImageFormat.Png);

                        this.ImagemCliche = "data:image/png;base64,";
                        this.ImagemCliche += Convert.ToBase64String(b);

                    }
                }

            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }
        }



        private static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, ImageFormat formatOfImage)
        {
            byte[] Ret;

            try
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }

            return Ret;
        }

    }
}