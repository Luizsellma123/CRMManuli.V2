using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class GerarGraficoClass
    {
        public string[] NomeVariaveis { get; set; }
        public string[] ValoresVariaveis { get; set; }
        public string TotalFaturamento { get; set; }
        public string TotalScoreSerasa { get; set; }

        /*variavel para gerar o gráfico*/
        public string grafico { get; set; }

        /*DataSets dos graficos*/
        public List<GerarGraficoDataSetClass> itemDataFaturamentoSetList { get; set; }

        private static readonly string[] Cores = {
        "red", "blue", "green", "yellow", "purple", "orange", "pink", "brown",
        "cyan", "magenta", "teal", "lime", "olive", "maroon", "navy", "indigo",
        "aquamarine", "lavender", "salmon", "turquoise", "violet", "tan", "fuchsia"
        };

        UtilClass objUtilClass = new UtilClass();

        public void GraficoFaturamento()
        {
            int cont = 0;

            //Inicia o grafico
            this.grafico = "<script>";

            this.grafico += "if($(\"#distribution-chart\").length) { ";
            this.grafico += "var areaData = { ";

            //this.grafico += "labels: [\"STRETCH\", \"FITA PP\", \"ESP. IND.\"], ";
            cont = 0;
            this.grafico += " labels: [";
            foreach (string nome in this.NomeVariaveis)
            {
                if (cont == 0)
                {
                    this.grafico += "'" + nome + "'";
                    cont++;
                }
                else
                {
                    this.grafico += ",'" + nome + "'";
                }
            }
            this.grafico += "],";


            //this.grafico += "datasets: [{ ";
            //this.grafico += "data: [164101.68, 176.40, 196.32], ";
            //this.grafico += "backgroundColor: [";
            //this.grafico += "\"#3da5f4\", \"#f1536e\", \"#fda006\"";
            //this.grafico += "], ";
            //this.grafico += "borderColor: \"rgba(0,0,0,0)\" ";
            //this.grafico += "}";
            //this.grafico += "]";

            //Abre DataSet
            this.grafico += " datasets: [";
            cont = 0;
            for (int i = 0; i < itemDataFaturamentoSetList.Count; i++)
            {
                if (cont == 0)
                {
                    this.grafico += itemDataFaturamentoSetList[i].dataSet;
                    cont++;
                }
                else
                {
                    this.grafico += "," + itemDataFaturamentoSetList[i].dataSet;
                }
            }
            //Fecha DataSet
            this.grafico += "]";


            this.grafico += "}; ";
            this.grafico += "var areaOptions = { ";
            this.grafico += "responsive: true,";
            this.grafico += "maintainAspectRatio: true,";
            this.grafico += "segmentShowStroke: false,";
            this.grafico += "cutoutPercentage: 72, ";
            this.grafico += "elements: { ";
            this.grafico += "arc: { ";
            this.grafico += "borderWidth: 4 ";
            this.grafico += "} ";
            this.grafico += "}, ";
            this.grafico += "legend: { ";
            this.grafico += "display: false ";
            this.grafico += "}, ";
            this.grafico += "tooltips: { ";
            this.grafico += "enabled: true ";
            this.grafico += "}, ";

            //this.grafico += "legendCallback: function (chart) { ";
            //this.grafico += "var text = []; ";
            //this.grafico += "text.push('<div class=\"distribution-chart\">'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[0] + '\"></div>'); ";
            //this.grafico += "text.push('<p>STRETCH</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[1] + '\"></div>'); ";
            //this.grafico += "text.push('<p>FITA PP</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[2] + '\"></div>'); ";
            //this.grafico += "text.push('<p>ESP. IND.</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "return text.join(\"\"); ";
            //this.grafico += "}, ";



            cont = 0;
            this.grafico += "legendCallback: function (chart) { ";
            this.grafico += "var text = []; ";
            this.grafico += "text.push('<div class=\"distribution-chart\">'); ";
            foreach (string nome in this.NomeVariaveis)
            {

                this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[" + cont + "] + '\"></div>'); ";
                this.grafico += "text.push('<p>" + nome + "</p>'); ";
                this.grafico += "text.push('</div>'); ";

                cont++;
            }
            this.grafico += "text.push('</div>'); ";
            this.grafico += "return text.join(\"\"); ";
            this.grafico += "}, ";

            this.grafico += "}; ";
            this.grafico += "var distributionChartPlugins = { ";
            this.grafico += "beforeDraw: function (chart) { ";
            this.grafico += "var width = chart.chart.width, ";
            this.grafico += "height = chart.chart.height, ";
            this.grafico += "ctx = chart.chart.ctx; ";
            this.grafico += "ctx.restore(); ";
            this.grafico += "var fontSize = .96; ";
            this.grafico += "ctx.font = \"600 \" + fontSize + \"em sans-serif\"; ";
            this.grafico += "ctx.textBaseline = \"middle\"; ";
            this.grafico += "ctx.fillStyle = \"#000\"; ";
            this.grafico += "var text = \"" + this.TotalFaturamento.ToString() + "\", ";
            this.grafico += "textX = Math.round((width - ctx.measureText(text).width) / 2), ";
            this.grafico += "textY = height / 2; ";
            this.grafico += "ctx.fillText(text, textX, textY); ";
            this.grafico += "ctx.save(); ";
            this.grafico += "} ";
            this.grafico += "}; ";
            this.grafico += "var distributionChartCanvas = $(\"#distribution-chart\").get(0).getContext(\"2d\"); ";
            this.grafico += "var distributionChart = new Chart(distributionChartCanvas, { ";
            this.grafico += "type: 'doughnut', ";
            this.grafico += "data: areaData, ";
            this.grafico += "options: areaOptions, ";
            this.grafico += "plugins: distributionChartPlugins ";
            this.grafico += "}); ";
            this.grafico += "document.getElementById('distribution-legend').innerHTML = distributionChart.generateLegend(); ";
            this.grafico += "} ";

            this.grafico += "</script>";
            //Finaliza o gráfico
        }

        public void incluiDataSetFaturamento(string[] itemValorVariaveis, string itemTituloLegenda, string[] background)
        {
            GerarGraficoDataSetClass novoItem = new GerarGraficoDataSetClass(itemValorVariaveis, itemTituloLegenda, background);

            //Verifica se esta instanciado
            if (this.itemDataFaturamentoSetList == null)
            {
                this.itemDataFaturamentoSetList = new List<GerarGraficoDataSetClass>();
            }
            this.itemDataFaturamentoSetList.Add(novoItem);
        }

        public void GraficoFaturamentoAnual()
        {
            int cont = 0;

            //Inicia o grafico
            this.grafico = "<script>";

            this.grafico += "if ($(\"#sale-report-chart\").length) { ";
            this.grafico += "var CurrentChartCanvas = $(\"#sale-report-chart\").get(0).getContext(\"2d\"); ";
            this.grafico += "var CurrentChart = new Chart(CurrentChartCanvas, { ";
            this.grafico += "type: 'bar', ";
            this.grafico += "data: ";
            this.grafico += "{ ";
            //this.grafico += "labels: [\"Jan\", \"Fev\", \"Mar\", \"Abr\", \"Mai\", \"Jun\", \"Jul\", \"Ago\", \"Set\", \"Out\", \"Nov\", \"Dez\"],";

            cont = 0;
            this.grafico += " labels: [";
            foreach (string nome in this.NomeVariaveis)
            {
                if (cont == 0)
                {
                    this.grafico += "'" + nome + "'";
                    cont++;
                }
                else
                {
                    this.grafico += ",'" + nome + "'";
                }
            }
            this.grafico += "],";

            this.grafico += "datasets: [{ ";
            this.grafico += "label: 'Faturamento', ";

            //this.grafico += "data: [28000, 9000, 15000, 20000, 5000, 15000, 26000, 15000, 26000, 20000, 28000, 20000], ";
            cont = 0;
            this.grafico += " data: [";
            foreach (string nome in this.ValoresVariaveis)
            {
                if (cont == 0)
                {
                    this.grafico += "'" + nome + "'";
                    cont++;
                }
                else
                {
                    this.grafico += ",'" + nome + "'";
                }
            }
            this.grafico += "],";

            this.grafico += "backgroundColor: [\"#3da5f4\", \"#e0f2ff\", \"#3da5f4\", \"#e0f2ff\", \"#3da5f4\", \"#e0f2ff\", \"#3da5f4\", \"#e0f2ff\", \"#3da5f4\", \"#e0f2ff\", \"#3da5f4\", \"#3da5f4\"] ";
            this.grafico += "} ";
            this.grafico += "] ";
            this.grafico += "}, ";
            this.grafico += "options: ";
            this.grafico += "{ ";
            this.grafico += "responsive: true, ";
            this.grafico += "maintainAspectRatio: true, ";
            this.grafico += "layout: ";
            this.grafico += "{ ";
            this.grafico += "padding: ";
            this.grafico += "{ ";
            this.grafico += "left: 0, ";
            this.grafico += "right: 0, ";
            this.grafico += "top: 0, ";
            this.grafico += "bottom: 0 ";
            this.grafico += "} ";
            this.grafico += "}, ";
            this.grafico += "scales: ";
            this.grafico += "{ ";
            this.grafico += "yAxes: [{ ";
            this.grafico += "display: true, ";
            this.grafico += "gridLines: ";
            this.grafico += "{ ";
            this.grafico += "drawBorder: false ";
            this.grafico += "}, ";
            this.grafico += "ticks: ";
            this.grafico += "{ ";
            this.grafico += "fontColor: \"#000\", ";
            this.grafico += "display: true, ";
            this.grafico += "padding: 20, ";
            this.grafico += "fontSize: 12, ";
            //this.grafico += "stepSize: 100000, ";
            this.grafico += "callback: function(value) { ";
            this.grafico += "var ranges = [ ";
            this.grafico += "{ divider: 1e6, suffix: 'M' }, ";
            this.grafico += "{ divider: 1e3, suffix: 'Mil' } ";
            this.grafico += "]; ";
            this.grafico += "function formatNumber(n) { ";
            this.grafico += "for (var i = 0; i < ranges.length; i++) ";
            this.grafico += "{ ";
            this.grafico += "if (n >= ranges[i].divider) ";
            this.grafico += "{ ";
            this.grafico += "return (n / ranges[i].divider).toString() + ranges[i].suffix; ";
            this.grafico += "} ";
            this.grafico += "} ";
            this.grafico += "return n; ";
            this.grafico += "} ";
            this.grafico += "return \"$\" + formatNumber(value); ";
            this.grafico += "} ";
            this.grafico += "} ";
            this.grafico += "}], ";
            this.grafico += "xAxes: [{ ";
            this.grafico += "stacked: false, ";
            this.grafico += "categoryPercentage: .6, ";
            this.grafico += "ticks: ";
            this.grafico += "{ ";
            this.grafico += "beginAtZero: true, ";
            this.grafico += "fontColor: \"#000\", ";
            this.grafico += "display: true, ";
            this.grafico += "padding: 20, ";
            this.grafico += "fontSize: 14 ";
            this.grafico += "}, ";
            this.grafico += "gridLines: ";
            this.grafico += "{ ";
            this.grafico += "color: \"rgba(0, 0, 0, 0)\", ";
            this.grafico += "display: true ";
            this.grafico += "}, ";
            this.grafico += "barPercentage: .7 ";
            this.grafico += "}] ";
            this.grafico += "}, ";
            this.grafico += "legend: ";
            this.grafico += "{ ";
            this.grafico += "display: false ";
            this.grafico += "}, ";
            this.grafico += "elements: ";
            this.grafico += "{ ";
            this.grafico += "point: ";
            this.grafico += "{ ";
            this.grafico += "radius: 0 ";
            this.grafico += "} ";
            this.grafico += "} ";
            this.grafico += "} ";
            this.grafico += "}); ";
            this.grafico += "} ";

            this.grafico += "</script>";
            //Finaliza o gráfico
        }

        public void GraficoLimiteCredito()
        {
            int cont = 0;

            //Inicia o grafico
            this.grafico = "<script>";

            this.grafico += "if($(\"#LimiteCredito-chart\").length) { ";
            this.grafico += "var areaData = { ";

            //this.grafico += "labels: [\"STRETCH\", \"FITA PP\", \"ESP. IND.\"], ";
            cont = 0;
            this.grafico += " labels: [";
            foreach (string nome in this.NomeVariaveis)
            {
                if (cont == 0)
                {
                    this.grafico += "'" + nome + "'";
                    cont++;
                }
                else
                {
                    this.grafico += ",'" + nome + "'";
                }
            }
            this.grafico += "],";


            //this.grafico += "datasets: [{ ";
            //this.grafico += "data: [164101.68, 176.40, 196.32], ";
            //this.grafico += "backgroundColor: [";
            //this.grafico += "\"#3da5f4\", \"#f1536e\", \"#fda006\"";
            //this.grafico += "], ";
            //this.grafico += "borderColor: \"rgba(0,0,0,0)\" ";
            //this.grafico += "}";
            //this.grafico += "]";

            //Abre DataSet
            this.grafico += " datasets: [";
            cont = 0;
            for (int i = 0; i < itemDataFaturamentoSetList.Count; i++)
            {
                if (cont == 0)
                {
                    this.grafico += itemDataFaturamentoSetList[i].dataSet;
                    cont++;
                }
                else
                {
                    this.grafico += "," + itemDataFaturamentoSetList[i].dataSet;
                }
            }
            //Fecha DataSet
            this.grafico += "]";


            this.grafico += "}; ";
            this.grafico += "var areaOptions = { ";
            this.grafico += "responsive: true,";
            this.grafico += "maintainAspectRatio: true,";
            this.grafico += "segmentShowStroke: false,";
            this.grafico += "cutoutPercentage: 72, ";
            this.grafico += "elements: { ";
            this.grafico += "arc: { ";
            this.grafico += "borderWidth: 4 ";
            this.grafico += "} ";
            this.grafico += "}, ";
            this.grafico += "legend: { ";
            this.grafico += "display: false ";
            this.grafico += "}, ";
            this.grafico += "tooltips: { ";
            this.grafico += "enabled: true ";
            this.grafico += "}, ";

            //this.grafico += "legendCallback: function (chart) { ";
            //this.grafico += "var text = []; ";
            //this.grafico += "text.push('<div class=\"distribution-chart\">'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[0] + '\"></div>'); ";
            //this.grafico += "text.push('<p>STRETCH</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[1] + '\"></div>'); ";
            //this.grafico += "text.push('<p>FITA PP</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[2] + '\"></div>'); ";
            //this.grafico += "text.push('<p>ESP. IND.</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "return text.join(\"\"); ";
            //this.grafico += "}, ";



            cont = 0;
            this.grafico += "legendCallback: function (chart) { ";
            this.grafico += "var text = []; ";
            this.grafico += "text.push('<div class=\"distribution-chart\">'); ";
            foreach (string nome in this.NomeVariaveis)
            {

                this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 5px solid ' + chart.data.datasets[0].backgroundColor[" + cont + "] + '\"></div>'); ";
                this.grafico += "text.push('<p>" + nome + "</p>'); ";
                this.grafico += "text.push('</div>'); ";

                cont++;
            }
            this.grafico += "text.push('</div>'); ";
            this.grafico += "return text.join(\"\"); ";
            this.grafico += "}, ";

            this.grafico += "}; ";
            this.grafico += "var distributionChartPlugins = { ";
            this.grafico += "beforeDraw: function (chart) { ";
            this.grafico += "var width = chart.chart.width, ";
            this.grafico += "height = chart.chart.height, ";
            this.grafico += "ctx = chart.chart.ctx; ";
            this.grafico += "ctx.restore(); ";
            //this.grafico += "var fontSize = 1.96; ";
            this.grafico += "var fontSize = 1; ";
            this.grafico += "ctx.font = \"600 \" + fontSize + \"em sans-serif\"; ";
            this.grafico += "ctx.textBaseline = \"middle\"; ";
            this.grafico += "ctx.fillStyle = \"#000\"; ";
            this.grafico += "var text = \"" + this.TotalFaturamento.ToString() + "\", ";
            this.grafico += "textX = Math.round((width - ctx.measureText(text).width) / 2), ";
            this.grafico += "textY = height / 2; ";
            this.grafico += "ctx.fillText(text, textX, textY); ";
            this.grafico += "ctx.save(); ";
            this.grafico += "} ";
            this.grafico += "}; ";
            this.grafico += "var distributionChartCanvas = $(\"#LimiteCredito-chart\").get(0).getContext(\"2d\"); ";
            this.grafico += "var distributionChart = new Chart(distributionChartCanvas, { ";
            this.grafico += "type: 'doughnut', ";
            this.grafico += "data: areaData, ";
            this.grafico += "options: areaOptions, ";
            this.grafico += "plugins: distributionChartPlugins ";
            this.grafico += "}); ";
            this.grafico += "document.getElementById('LimiteCredito-legend').innerHTML = distributionChart.generateLegend(); ";
            this.grafico += "} ";

            this.grafico += "</script>";
            //Finaliza o gráfico
        }

        public void GraficoScoreSerasa()
        {
            int cont = 0;

            //Inicia o grafico
            this.grafico = "<script>";

            this.grafico += "if($(\"#ScoreSerasa-chart\").length) { ";
            this.grafico += "var areaData = { ";

            //this.grafico += "labels: [\"STRETCH\", \"FITA PP\", \"ESP. IND.\"], ";
            cont = 0;
            this.grafico += " labels: [";
            foreach (string nome in this.NomeVariaveis)
            {
                if (cont == 0)
                {
                    this.grafico += "'" + nome + "'";
                    cont++;
                }
                else
                {
                    this.grafico += ",'" + nome + "'";
                }
            }
            this.grafico += "],";


            //this.grafico += "datasets: [{ ";
            //this.grafico += "data: [164101.68, 176.40, 196.32], ";
            //this.grafico += "backgroundColor: [";
            //this.grafico += "\"#3da5f4\", \"#f1536e\", \"#fda006\"";
            //this.grafico += "], ";
            //this.grafico += "borderColor: \"rgba(0,0,0,0)\" ";
            //this.grafico += "}";
            //this.grafico += "]";

            //Abre DataSet
            this.grafico += " datasets: [";
            cont = 0;
            for (int i = 0; i < itemDataFaturamentoSetList.Count; i++)
            {
                if (cont == 0)
                {
                    this.grafico += itemDataFaturamentoSetList[i].dataSet;
                    cont++;
                }
                else
                {
                    this.grafico += "," + itemDataFaturamentoSetList[i].dataSet;
                }
            }
            //Fecha DataSet
            this.grafico += "]";


            this.grafico += "}; ";
            this.grafico += "var areaOptions = { ";
            this.grafico += "responsive: true,";
            this.grafico += "maintainAspectRatio: true,";
            this.grafico += "segmentShowStroke: false,";
            this.grafico += "cutoutPercentage: 72, ";
            this.grafico += "elements: { ";
            this.grafico += "arc: { ";
            this.grafico += "borderWidth: 4 ";
            this.grafico += "} ";
            this.grafico += "}, ";
            this.grafico += "legend: { ";
            this.grafico += "display: false ";
            this.grafico += "}, ";
            this.grafico += "tooltips: { ";
            this.grafico += "enabled: true ";
            this.grafico += "}, ";

            //this.grafico += "legendCallback: function (chart) { ";
            //this.grafico += "var text = []; ";
            //this.grafico += "text.push('<div class=\"distribution-chart\">'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[0] + '\"></div>'); ";
            //this.grafico += "text.push('<p>STRETCH</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[1] + '\"></div>'); ";
            //this.grafico += "text.push('<p>FITA PP</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 3px solid ' + chart.data.datasets[0].backgroundColor[2] + '\"></div>'); ";
            //this.grafico += "text.push('<p>ESP. IND.</p>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "text.push('</div>'); ";
            //this.grafico += "return text.join(\"\"); ";
            //this.grafico += "}, ";



            cont = 0;
            this.grafico += "legendCallback: function (chart) { ";
            this.grafico += "var text = []; ";
            this.grafico += "text.push('<div class=\"distribution-chart\">'); ";
            foreach (string nome in this.NomeVariaveis)
            {

                this.grafico += "text.push('<div class=\"item\"><div class=\"legend-label\" style=\"border: 5px solid ' + chart.data.datasets[0].backgroundColor[" + cont + "] + '\"></div>'); ";
                this.grafico += "text.push('<p>" + nome + "</p>'); ";
                this.grafico += "text.push('</div>'); ";

                cont++;
            }
            this.grafico += "text.push('</div>'); ";
            this.grafico += "return text.join(\"\"); ";
            this.grafico += "}, ";

            this.grafico += "}; ";
            this.grafico += "var distributionChartPlugins = { ";
            this.grafico += "beforeDraw: function (chart) { ";
            this.grafico += "var width = chart.chart.width, ";
            this.grafico += "height = chart.chart.height, ";
            this.grafico += "ctx = chart.chart.ctx; ";
            this.grafico += "ctx.restore(); ";
            //this.grafico += "var fontSize = 1.96; ";
            this.grafico += "var fontSize = 1; ";
            this.grafico += "ctx.font = \"600 \" + fontSize + \"em sans-serif\"; ";
            this.grafico += "ctx.textBaseline = \"middle\"; ";
            this.grafico += "ctx.fillStyle = \"#000\"; ";
            this.grafico += "var text = \"" + this.TotalScoreSerasa.ToString() + "\", ";
            this.grafico += "textX = Math.round((width - ctx.measureText(text).width) / 2), ";
            this.grafico += "textY = height / 2; ";
            this.grafico += "ctx.fillText(text, textX, textY); ";
            this.grafico += "ctx.save(); ";
            this.grafico += "} ";
            this.grafico += "}; ";
            this.grafico += "var distributionChartCanvas = $(\"#ScoreSerasa-chart\").get(0).getContext(\"2d\"); ";
            this.grafico += "var distributionChart = new Chart(distributionChartCanvas, { ";
            this.grafico += "type: 'doughnut', ";
            this.grafico += "data: areaData, ";
            this.grafico += "options: areaOptions, ";
            this.grafico += "plugins: distributionChartPlugins ";
            this.grafico += "}); ";
            this.grafico += "document.getElementById('ScoreSerasa-legend').innerHTML = distributionChart.generateLegend(); ";
            this.grafico += "} ";

            this.grafico += "</script>";
            //Finaliza o gráfico
        }

        public void GraficoProjetos()
        {
            this.grafico = "";

            this.grafico += "<script>";

            //Seta para formato Brasil
            this.grafico += "anychart.format.inputLocale('pt-br'); ";
            this.grafico += "anychart.format.outputLocale('pt-br'); ";

            this.grafico += "anychart.onDocumentReady(function() { ";



        }

        public static string ObterCor(int indice)
        {
            return Cores[indice];
        }

        public List<string> RetornaLabels(DataTable Dados)
        {
            List<string> labels = new List<string>();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    if (!objUtilClass.ExisteNoList(labels, row["label"].ToString()))
                        labels.Add(row["label"].ToString());
                }
            }

            return labels;
        }

        public string RetornaTitulo(DataTable Dados)
        {
            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    return row["titulo"].ToString();
                }
            }

            return "";
        }

        public string MontaGraficoBarrasDinamico(string ElementId, DataTable Dados)
        {
            StringBuilder GraficoDinamico = new StringBuilder();

            List<string> labels = RetornaLabels(Dados);

            string titulo = RetornaTitulo(Dados);

            string constante = "Const" + ElementId;

            GraficoDinamico.AppendLine("const " + constante + " = document.getElementById('" + ElementId + "');");
            GraficoDinamico.AppendLine("");
            GraficoDinamico.AppendLine("new Chart(" + constante + ", {");
            GraficoDinamico.AppendLine("    type: 'bar',");
            GraficoDinamico.AppendLine("    data: {");

            int countlabel = 0;

            GraficoDinamico.Append("        labels: [");

            foreach (string label in labels)
            {
                GraficoDinamico.Append("'" + label + "'");

                countlabel++;

                if (countlabel != labels.Count) GraficoDinamico.Append(", ");
            }

            GraficoDinamico.Append("],");

            GraficoDinamico.AppendLine("");

            GraficoDinamico.AppendLine("        datasets: [");

            List<string> DescricaoList = new List<string>();

            int countDados = 0;

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    if (!VerificaDescricaoInserida(DescricaoList, row["Descricao"].ToString()))
                    {
                        GraficoDinamico.AppendLine("            {");
                        GraficoDinamico.AppendLine("                label: '" + row["Descricao"].ToString() + "',");
                        GraficoDinamico.AppendLine("                data: " + MontaTotalGraficoBarrasDinamico(row["Descricao"].ToString(), Dados) + ",");
                        GraficoDinamico.AppendLine("                backgroundColor: '" + ObterCor(countDados) + "',");
                        GraficoDinamico.AppendLine("                stack: 'Stack " + countDados + "' ");
                        GraficoDinamico.AppendLine("            }");

                        countDados++;

                        if (countDados != Dados.Rows.Count) GraficoDinamico.AppendLine("            ,");
                    }
                }
            }

            GraficoDinamico.AppendLine("         ]");
            GraficoDinamico.AppendLine("    },");
            GraficoDinamico.AppendLine("    options: {");
            GraficoDinamico.AppendLine("        scales: {");
            GraficoDinamico.AppendLine("            x: {");
            GraficoDinamico.AppendLine("                stacked: true,");
            GraficoDinamico.AppendLine("            },");
            GraficoDinamico.AppendLine("            y: {");
            GraficoDinamico.AppendLine("                beginAtZero: true");
            GraficoDinamico.AppendLine("            }");
            GraficoDinamico.AppendLine("        },");
            GraficoDinamico.AppendLine("        plugins: {");
            GraficoDinamico.AppendLine("            title: {");
            GraficoDinamico.AppendLine("                display: true,");
            GraficoDinamico.AppendLine("                text: '" + titulo + "',");
            GraficoDinamico.AppendLine("                font: {");
            GraficoDinamico.AppendLine("                    size: 15");
            GraficoDinamico.AppendLine("                }");
            GraficoDinamico.AppendLine("            },");
            GraficoDinamico.AppendLine("        },");
            GraficoDinamico.AppendLine("        responsive: true");
            GraficoDinamico.AppendLine("    }");
            GraficoDinamico.AppendLine("});");


            return GraficoDinamico.ToString();
        }

        private int RetornaCountDescricao(string Descricao, DataTable Dados)
        {
            int count = 0;

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    if (Descricao == row["Descricao"].ToString())
                        count++;
                }
            }

            return count;
        }

        private bool VerificaDescricaoInserida(List<string> DescricaoList, string Descricao)
        {
            if (objUtilClass.ExisteNoList(DescricaoList, Descricao))
            {
                return true;
            }
            else
            {
                DescricaoList.Add(Descricao);

                return false;
            }
        }

        private string MontaTotalGraficoBarrasDinamico(string Descricao, DataTable Dados)
        {
            StringBuilder Total = new StringBuilder();

            int i = 0, countDescricao = RetornaCountDescricao(Descricao, Dados);

            Total.Append("[");

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    if (Descricao == row["Descricao"].ToString())
                    {
                        Total.Append(row["Total"].ToString());

                        i++;

                        if (i != countDescricao) Total.Append(", ");
                    }
                }
            }

            Total.Append("]");

            return Total.ToString();
        }
    }
}