using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class GerarGraficoDataSetClass
    {
        public string[] valorVariaveis { get; set; }
        public string TituloLegenda { get; set; }
        public string[] backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string pointBackgroundColor { get; set; }

        /*string DataSet*/
        public string dataSet { get; set; }

        public GerarGraficoDataSetClass(string[] itemValorVariaveis, string itemTituloLegenda, string[] background)
        {
            this.valorVariaveis = itemValorVariaveis;
            this.TituloLegenda = itemTituloLegenda;
            this.backgroundColor = background;

            Monta_DataSet();
        }

        public void Monta_DataSet()
        {
            int cont = 0;

            this.dataSet += " {";
            //this.dataSet += " label: '" + this.TituloLegenda + "',";
            //this.dataSet += " backgroundColor: " + this.backgroundColor + ",";
            this.dataSet += " borderColor: 'rgba(0, 0, 0, 0)',";
            //this.dataSet += " pointBackgroundColor: " + this.pointBackgroundColor + ",";

            //Cria os valores das variveis
            cont = 0;
            this.dataSet += " data: [";
            foreach (string valor in this.valorVariaveis)
            {
                if (cont == 0)
                {
                    this.dataSet += valor;
                    cont++;
                }
                else
                {
                    this.dataSet += "," + valor;
                }
            }
            this.dataSet += "], ";

            //Cria os valores das variveis
            cont = 0;
            this.dataSet += " backgroundColor: [";
            foreach (string valor in this.backgroundColor)
            {
                if (cont == 0)
                {
                    this.dataSet += valor;
                    cont++;
                }
                else
                {
                    this.dataSet += "," + valor;
                }
            }
            this.dataSet += "]";


            this.dataSet += "}";
        }
    }
}