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
    public class WSPedidoProdutoDetalhe
    {
        #region Campos Principais

        public int IDEmpresa { get; set; }

        public int NumeroPedidoSAP { get; set; }

        public int NumeroPedidoCRM { get; set; }

        public string CodigoItemSAP { get; set; }

        public string Cliente { get; set; }

        public string Empresa { get; set; }

        public string PedidoSAP { get; set; }

        public string StatusPedidoCRM { get; set; }

        public string PedidoCRM { get; set; }

        public string DataEmissao { get; set; }

        public string DataEntrega { get; set; }

        public string EmbarqueImediato { get; set; }

        public string Vendedor { get; set; }

        public string Produto { get; set; }

        public string Cliche { get; set; }

        public string ImagemCliche { get; set; }

        #endregion

        public void RecuperaPedidoProdutoDetalhe()
        {
            try
            {
                AdmVendas objAdmVendas = new AdmVendas();

                objAdmVendas.IDEmpresa = this.IDEmpresa;
                objAdmVendas.NumeroPedidoCRM = this.NumeroPedidoCRM;
                objAdmVendas.NumeroPedidoSAP = this.NumeroPedidoSAP;
                objAdmVendas.CodigoItemSAP = this.CodigoItemSAP;
                objAdmVendas.Cliche = this.Cliche;

                DataTable OBJDataTable = objAdmVendas.RetornaListaPedidoLiberacaoProducaoDetalheProdutosModal();

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        this.Cliente = Convert.ToString(row["Cliente"]);
                        this.Empresa = Convert.ToString(row["Empresa"]);
                        this.StatusPedidoCRM = Convert.ToString(row["StatusPedidoCRM"]);
                        this.DataEmissao = Convert.ToString(row["DataEmissao"]);
                        this.DataEntrega = Convert.ToString(row["DataEntrega"]);
                        this.EmbarqueImediato = Convert.ToString(row["EmbarqueImediato"]);
                        this.Vendedor = Convert.ToString(row["Vendedor"]);
                        this.Produto = Convert.ToString(row["Produto"]);
                        this.Cliche = Convert.ToString(row["Cliche"]);
                        this.ImagemCliche = Convert.ToString(row["ImagemCliche"]);

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