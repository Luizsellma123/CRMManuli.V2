using System;
using CRMAPI.Classes.RastreioPedido;

namespace CRMAPI.Models
{
    public class RastreiaPedidoModel
    {
        public string IDEmpresa { get; set; }

        public string NumeroPedidoSAP { get; set; }

        public string NumeroNotaFiscal { get; set; }

        public string RastrearPedido()
        {
            string erro = "", transportadora = "";

            try
            {
                RastreioPedido objRastreioPedido = new RastreioPedido(this);

                RastreioPedidoClass objRastreioPedidoClass = new RastreioPedidoClass(this);

                objRastreioPedido.IDTransportador = Convert.ToInt32(objRastreioPedidoClass.RetornaIDTransportador());

                switch (objRastreioPedido.IDTransportador)
                {
                    case 1:
                        objRastreioPedido = new RastreioAguiaSul(this);
                        transportadora = "RastreioAguiaSul";
                        break;

                    case 2:
                        objRastreioPedido = new RastreioAlfa(this);
                        transportadora = "RastreioAlfa";
                        break;

                    case 3:
                        objRastreioPedido = new RastreioAyres(this);
                        transportadora = "RastreioAyres";
                        break;

                    case 4:
                        objRastreioPedido = new RastreioSaoMiguel(this);
                        transportadora = "RastreioSaoMiguel";
                        break;

                    case 5:
                        objRastreioPedido = new RastreioTranSanches(this);
                        transportadora = "RastreioTranSanches";
                        break;
                }                                

                if (objRastreioPedido.IDTransportador != 0)
                {
                    objRastreioPedido.CarregaDadosTransportadora();

                    objRastreioPedido.CarregaDadosTransportadoraOcorrencias();

                    return objRastreioPedido.GravaDados();
                }
                else
                {
                    return "Código da transportadora do pedido NumeroPedidoSAP: " + this.NumeroPedidoSAP + " não foi encontrado.";
                }
            }
            catch (Exception ex)
            {
                erro = "Erro no rastreio da transportadora " + transportadora + " : " + ex.Message;
            }

            return erro;
        }

    }
}