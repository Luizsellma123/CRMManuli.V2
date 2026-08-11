using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace VendasWeb
{
    public class tratamentoLog
    {

        public void gravaLogPedido(string caminho, pedido pedido)
        {

            int cont = 0;
            int quant = 0;
            string pastaServer = caminho + "\\LogSistema\\" + pedido.codigoEmpresa + "_" + pedido.vendedor + "_" + pedido.numeroPedido + ".txt";
            FileInfo infArquivo = new FileInfo(pastaServer);

            if (infArquivo.Exists)
            {
                infArquivo.Delete();
            }
            
            StreamWriter arquivo = new StreamWriter(pastaServer);

            arquivo.Write("Empresa: " + pedido.codigoEmpresa);
            arquivo.WriteLine();
            arquivo.Write("Pedido: " + pedido.numeroPedido);
            arquivo.WriteLine();
            arquivo.Write("Tipo : " + pedido.tipo);
            arquivo.WriteLine();
            arquivo.Write("drpStatus : " + pedido.statusPedio);
            arquivo.WriteLine();
            arquivo.Write("Data Saída: " + pedido.dataEntrega);
            arquivo.WriteLine();
            arquivo.Write("Data Emissao: " + pedido.dataEmissao);
            arquivo.WriteLine();
            arquivo.Write("Natureza: " + pedido.natureza);
            arquivo.WriteLine();
            arquivo.Write("Operacao: " + pedido.operacao);
            arquivo.WriteLine();
            arquivo.Write("Especie: " + pedido.especie);
            arquivo.WriteLine();
            arquivo.Write("Condicao Pagamento: " + pedido.condicao + " - " + pedido.nomeCondicao);
            arquivo.WriteLine();
            arquivo.Write("Tipo Frete: " + pedido.tipoFrete);
            arquivo.WriteLine();
            arquivo.Write("Transportadora: " + pedido.transportadora +" - "+ pedido.descricaoTransportadora);
            arquivo.WriteLine();
            arquivo.Write("Data Hora Inclusão: " + pedido.dataEmissao);
            arquivo.WriteLine();
            arquivo.WriteLine();

            quant = pedido.numeroItens();

            while (cont < quant && quant > 0)
            {
                arquivo.Write("Produto: " + pedido.itemPedidoList[cont].codigoProduto);
                arquivo.Write(" Revenda: " + pedido.itemPedidoList[cont].revenda);
                arquivo.Write(" Quantidade: " + pedido.itemPedidoList[cont].quantidade);
                arquivo.Write(" Tabela: " + pedido.itemPedidoList[cont].codigoTabela);
                arquivo.Write(" Valor Unitario: "+ pedido.itemPedidoList[cont].valorItem.ToString());
                arquivo.Write(" Total: "+ pedido.itemPedidoList[cont].valorTotal.ToString());
                arquivo.Write(" Unidade: "+pedido.itemPedidoList[cont].unidade);
                //arquivo.Write(items[cont, 7].ToString());
                arquivo.WriteLine();

                cont++;
            }

            arquivo.WriteLine();
            arquivo.Write("Historico: " + pedido.historicoAntigo + " - " + pedido.historico);
            arquivo.WriteLine();
            arquivo.Write("Observacao: " + pedido.observacao);
            arquivo.WriteLine();
            arquivo.WriteLine();

            arquivo.Write("Dados Logistica: ");
            arquivo.WriteLine();
            arquivo.Write("Quantidades: " +pedido.QuantidadeVolumes.ToString());
            arquivo.WriteLine();
            arquivo.Write("Especie: " + pedido.EspecieVolume.ToString());
            arquivo.WriteLine();
            arquivo.Write("Peso Liquido: " + pedido.PesoLiquido.ToString());
            arquivo.WriteLine();
            arquivo.Write("Peso Bruto: " + pedido.PesoBruto.ToString());
            arquivo.WriteLine();

            arquivo.Close();

        }

    }
}