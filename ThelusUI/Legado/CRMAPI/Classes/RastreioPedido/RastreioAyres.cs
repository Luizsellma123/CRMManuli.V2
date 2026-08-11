using System;
using CRMAPI.Models;
using System.Web.UI.WebControls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CRMAPI.Classes.RastreioPedido
{
    public class RastreioAyres : RastreioPedido
    {
        public RastreioAyres(RastreiaPedidoModel objRastreiaPedidoModel) : base(objRastreiaPedidoModel)
        {
            this.IDTransportador = 3;
        }

        public override string GravaDados()
        {
            try
            {
                // Configurar as opções do Chrome para usar o modo headless
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");

                // Inicializar o ChromeDriver com as opções configuradas                                
                using (IWebDriver driver = new ChromeDriver(chromeOptions))
                {
                    // Navegar até a página desejada                
                    driver.Navigate().GoToUrl(this.EnderecoAPI);

                    // Localizar os elementos da página e interagir com eles
                    {
                        // Número do pedido:
                        IWebElement inputElement_NumeroDoPedido = driver.FindElement(By.Name("ewd_otp_identifier_number"));
                        inputElement_NumeroDoPedido.SendKeys(this.NumeroNotaFiscal);

                        // Endereço de email para o pedido:
                        IWebElement inputElement_EnderecoDeEmail = driver.FindElement(By.Name("ewd_otp_form_email"));
                        inputElement_EnderecoDeEmail.SendKeys(this.EmailAPI);
                    }

                    // CLica no botão "Rastrear"
                    {
                        IWebElement buttonElement = driver.FindElement(By.Name("ewd_otp_form_submit"));
                        buttonElement.Click();
                    }

                    // Aguardar um pouco para a página carregar após o clique
                    System.Threading.Thread.Sleep(2000);

                    int NumeroDoPedido = 0;
                    string NomeDoPedido = "", NotasDoPedido = "";

                    // Recuperar dados da tela
                    {
                        ReadOnlyCollection<IWebElement> Elements;

                        Elements = driver.FindElements(By.ClassName("ewd-otp-tracking-results-field"));

                        if (Elements.Count > 0)
                        {
                            foreach (IWebElement Element in Elements)
                            {
                                IWebElement label = Element.FindElement(By.ClassName("ewd-otp-tracking-results-label"));
                                string labelText = label.Text.Trim();

                                IWebElement Value = Element.FindElement(By.ClassName("ewd-otp-tracking-results-value"));
                                string ValueText = Value.Text.Trim();

                                switch (labelText)
                                {
                                    case "Número do pedido:":
                                        NumeroDoPedido = Convert.ToInt32(ValueText);
                                        break;

                                    case "Nome do pedido:":
                                        NomeDoPedido = ValueText;
                                        break;

                                    case "Notas do pedido:":
                                        NotasDoPedido = ValueText;
                                        break;
                                }

                            }
                        }

                        GravaPrevisao(NotasDoPedido);

                        Elements = driver.FindElements(By.ClassName("ewd-otp-status-label"));

                        if (Elements.Count > 0)
                        {
                            foreach (IWebElement Element in Elements)
                            {
                                if (Element.Text != "Status do pedido")
                                {
                                    IWebElement statuses = Element.FindElement(By.ClassName("ewd-otp-statuses"));

                                    string statusesText = statuses.Text.Trim();

                                    GravaStatus(NotasDoPedido, statusesText);
                                }
                            }
                        }
                    }

                    driver.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return "";
        }

        private void GravaPrevisao(string NotasDoPedido)
        {
            this.DataHistorico = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder Historico = new StringBuilder();

            string PrevisaoEntrega = "";

            {
                string notaStringPrevisao = "PREVISÃO DE ENTREGA DIA";

                PrevisaoEntrega = NotasDoPedido.Substring(notaStringPrevisao.Length, 6);

                PrevisaoEntrega = Convert.ToDateTime(PrevisaoEntrega).ToString("yyyy-MM-dd");
            }

            {
                Historico.AppendLine("Seu CT-e foi emitido e sua mercadoria está sendo preparada para transporte. <br>");

                Historico.Append("Previsão de chegada em " + Convert.ToDateTime(PrevisaoEntrega).ToString("dd/MM/yyyy"));
            }

            this.Historico = Historico.ToString();

            this.CodigoOcorrencia = "1";

            this.PrevisaoEntrega = PrevisaoEntrega;

            GRAVA_HISTORICO_RASTREIO_PEDIDOS();
        }

        private void GravaStatus(string NotasDoPedido, string StatusDoPedido)
        {
            this.DataHistorico = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder Historico = new StringBuilder();

            {
                if (StatusDoPedido == "Mercadoria Entregue")
                {
                    //exemplo apenas para pegar o lenght
                    string previsaoEntregaString = "PREVISÃO DE ENTREGA DIA 24/08. ";

                    Historico.AppendLine(NotasDoPedido.Substring(previsaoEntregaString.Length));

                    this.CodigoOcorrencia = "2";
                }
                else
                {
                    Historico.AppendLine(StatusDoPedido);

                    this.CodigoOcorrencia = "OUTROS";
                }
            }

            this.Historico = Historico.ToString();

            this.PrevisaoEntrega = "";

            GRAVA_HISTORICO_RASTREIO_PEDIDOS();
        }

    }
}