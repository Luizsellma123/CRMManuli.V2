using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{

    public partial class FrmAbaAnexo : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        GerencialVendas.DocEntidadeClass DocEntidadeClass = new GerencialVendas.DocEntidadeClass();
        usuario ObjUsuarioClass = new usuario();
        criptografia mdlCriptografia = new criptografia();


        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                if (Session["clsEntidades"] != null)
                {
                    //Descarega a session da Entidade
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmAbaAnexo");
                    }



                    //Verifica a operação
                    switch (ObjEntidadesClass.TipoOperacao)
                    {

                        case "ADM_VENDAS":
                            carregaDadosNaTela();
                            LiberaNavegacao();
                            OcultarAnexos();
                            LiberaEdicao();
                            break;

                        case "ADM_FISCAL":
                            carregaDadosNaTela();
                            LiberaNavegacao();
                            OcultarAnexos();
                            break;

                        case "ADM_FINANCEIRO":
                            carregaDadosNaTela();
                            LiberaNavegacao();
                            OcultarAnexos();
                            LiberaEdicao();
                            break;


                        case "Cadastro Incompleto":
                            #region
                            carregaDadosNaTela();
                            LiberaNavegacao();
                            OcultarAnexos();
                            LiberaEdicao();

                            #endregion
                            break;


                        case "Cadastro Completo":
                            #region
                            carregaDadosNaTela();
                            LiberaNavegacao();
                            OcultarAnexos();
                            LiberaEdicao();
                            #endregion
                            break;

                        case "Consulta":
                            carregaDadosNaTela();
                            LiberaNavegacao();
                            OcultarAnexos();


                            //Se tiver em status "Cadastro Incompleto" libera campo para Finalizar Cadastro
                            if (ObjEntidadesClass.StatEntCod == "13")
                            {
                                LiberaEdicao();
                            }
                            else
                            {
                                BloqueiaCampos();
                            }



                            break;


                        default:

                            /*if (ObjEntidadesClass.NovaLoja == "Não")
                            {
                                OcultarAnexos();

                            }
                            else
                            {*/

                            DesocultarAnexos();

                            /*}*/

                            break;

                    }





                }




                




            }


        }



        #region Load Anexos


        protected void AlteracaoContratuaButton_Click(object sender, EventArgs e)
        {

            DocEntidadeClass = new GerencialVendas.DocEntidadeClass();

            if (Session["clsEntidades"] != null)
            {

                //Descarega a session da Entidade
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            }
            else
            {
                ObjEntidadesClass = new GerencialVendas.clsEntidades();
            }

            #region AlteracaoContratualFileUpload
            if (AlteracaoContratualFileUpload.HasFile)
            {

                string stipoArquivo = Path.GetExtension(AlteracaoContratualFileUpload.PostedFile.FileName).ToLower();
                DocEntidadeClass.DocEntObs = "CONTRATO SOCIAL E ULTIMA ALTERAÇÃO";

                switch (stipoArquivo.ToUpper())
                {
                    case ".PDF":

                        try
                        {

                            //obtem o tamanho do arquivo
                            int tamanho = AlteracaoContratualFileUpload.PostedFile.ContentLength;
                            //cria um array de bytes para armazenar os dados binários da imagem
                            byte[] imgbyte = new byte[tamanho];
                            //armazena a imagem selecinada na memória
                            HttpPostedFile img = AlteracaoContratualFileUpload.PostedFile;
                            //define os dados binários
                            img.InputStream.Read(imgbyte, 0, tamanho);

                            AlteracaoContratuaLabel.Text = "Arquivo: " + AlteracaoContratualFileUpload.FileName + " Carregado";
                            AlteracaoContratuaLabel.ForeColor = System.Drawing.Color.Green;
                            AlteracaoContratualCheckBox.Checked = true;



                            #region Salvando Arquivo

                            //Pegando Informações do Arquivo
                            FileInfo infoarquivo = new FileInfo(AlteracaoContratualFileUpload.FileName);
                            //Criando Caminho do arquivo
                            string pastaArquivo = "\\\\192.168.0.2\\anexosCRM\\" + ObjEntidadesClass.EntCod + "_CONTRATO_SOCIAL_E_ULTIMA_ALTERACAO.PDF";

                            //Pegando informações do caminho do arquivo criado
                            FileInfo arquivoServidor = new FileInfo(pastaArquivo);

                            //Verificando se o arquivo existe
                            if (arquivoServidor.Exists == true)
                            {
                                File.Delete(pastaArquivo);

                            }


                            //Salvamos o arquivo
                            AlteracaoContratualFileUpload.PostedFile.SaveAs(pastaArquivo);

                            //Focando o Carregamento do proximo arquivo
                            CartaFaturamentoFileUpload.Focus();

                            #endregion

                            /*Carregando Dados a Serem Salvos Futuramente*/
                            DocEntidadeClass.DocEntImage = imgbyte;
                            DocEntidadeClass.DocEntPathArq = pastaArquivo;
                            DocEntidadeClass.UsuCod = Session["usuario"].ToString();
                            ObjEntidadesClass.Remove_DocEntidade(DocEntidadeClass);
                            ObjEntidadesClass.Adiciona_DocEntidade(DocEntidadeClass);


                            Session["clsEntidades"] = ObjEntidadesClass;


                        }
                        catch
                        {
                            ObjEntidadesClass.Remove_DocEntidade(DocEntidadeClass);
                            AlteracaoContratuaLabel.Text = "Erro ao carregar o arquivo: " + AlteracaoContratualFileUpload.FileName;
                            AlteracaoContratuaLabel.ForeColor = System.Drawing.Color.Red;
                            AlteracaoContratuaLabel.Visible = true;
                        }
                        break;

                    default:
                        AlteracaoContratuaLabel.Text = "Erro Tipo de Arquivo invalido. Arquivos Validos: PDF";
                        AlteracaoContratuaLabel.ForeColor = System.Drawing.Color.Red;
                        AlteracaoContratuaLabel.Visible = true;
                        break;

                }


            }
            #endregion


        }

        protected void CartaFaturamentoButton_Click(object sender, EventArgs e)
        {

            DocEntidadeClass = new GerencialVendas.DocEntidadeClass();

            if (Session["clsEntidades"] != null)
            {

                //Descarega a session da Entidade
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            }
            else
            {
                ObjEntidadesClass = new GerencialVendas.clsEntidades();
            }



            #region CartaFaturamentoFileUpload
            if (CartaFaturamentoFileUpload.HasFile)
            {

                string stipoArquivo = Path.GetExtension(CartaFaturamentoFileUpload.PostedFile.FileName).ToLower();
                DocEntidadeClass.DocEntObs = "CARTA DE FATURAMENTO REALIZADO DOS DOIS ÚLTIMOS ANOS (ASS. PELO CONTADOR RESPONSÁVEL)";

                switch (stipoArquivo.ToUpper())
                {
                    case ".PDF":
                        try
                        {

                            //obtem o tamanho do arquivo
                            int tamanho = CartaFaturamentoFileUpload.PostedFile.ContentLength;
                            //cria um array de bytes para armazenar os dados binários da imagem
                            byte[] imgbyte = new byte[tamanho];
                            //armazena a imagem selecinada na memória
                            HttpPostedFile img = CartaFaturamentoFileUpload.PostedFile;
                            //define os dados binários
                            img.InputStream.Read(imgbyte, 0, tamanho);


                            CartaFaturamentoLabel.Text = "Arquivo: " + CartaFaturamentoFileUpload.FileName + " Carregado";
                            CartaFaturamentoLabel.ForeColor = System.Drawing.Color.Green;
                            CartaFaturamentoCheckBox.Checked = true;



                            #region Salvar Arquivo
                            //Pegando Informações do Arquivo
                            FileInfo infoarquivo = new FileInfo(CartaFaturamentoFileUpload.FileName);
                            //Criando Caminho do arquivo
                            string pastaArquivo = "\\\\192.168.0.2\\anexosCRM\\" + ObjEntidadesClass.EntCod + "_CARTA_DE_FATURAMENTO_REALIZADO_DOS_DOIS_ULTIMOS_ANOS.PDF";

                            //Pegando informações do caminho do arquivo criado
                            FileInfo arquivoServidor = new FileInfo(pastaArquivo);

                            //Verificando se o arquivo existe
                            if (arquivoServidor.Exists == true)
                            {
                                File.Delete(pastaArquivo);

                            }

                            //Salvamos o arquivo
                            CartaFaturamentoFileUpload.PostedFile.SaveAs(pastaArquivo);



                            #endregion


                            /*Carregando Dados a Serem Salvos Futuramente*/
                            DocEntidadeClass.DocEntImage = imgbyte;
                            DocEntidadeClass.DocEntPathArq = pastaArquivo;
                            DocEntidadeClass.UsuCod = Session["usuario"].ToString();
                            ObjEntidadesClass.Remove_DocEntidade(DocEntidadeClass);
                            ObjEntidadesClass.Adiciona_DocEntidade(DocEntidadeClass);



                        }
                        catch
                        {
                            ObjEntidadesClass.Remove_DocEntidade(DocEntidadeClass);
                            CartaFaturamentoLabel.Text = "Erro ao carregar o arquivo: " + CartaFaturamentoFileUpload.FileName;
                            CartaFaturamentoLabel.ForeColor = System.Drawing.Color.Red;
                            CartaFaturamentoLabel.Visible = true;
                        }
                        break;

                    default:
                        CartaFaturamentoLabel.Text = "Erro Tipo de Arquivo invalido. Arquivos Validos: PDF";
                        CartaFaturamentoLabel.ForeColor = System.Drawing.Color.Red;
                        CartaFaturamentoLabel.Visible = true;
                        break;
                }



            }
            #endregion


        }

        protected void UltimosBalancoButton_Click(object sender, EventArgs e)
        {

            DocEntidadeClass = new GerencialVendas.DocEntidadeClass();

            if (Session["clsEntidades"] != null)
            {

                //Descarega a session da Entidade
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            }
            else
            {
                ObjEntidadesClass = new GerencialVendas.clsEntidades();
            }


            #region UltimosBalancoFileUpload
            if (UltimosBalancoFileUpload.HasFile)
            {

                string stipoArquivo = Path.GetExtension(UltimosBalancoFileUpload.PostedFile.FileName).ToLower();
                DocEntidadeClass.DocEntObs = "2 ÚLTIMOS BALANÇOS";

                switch (stipoArquivo.ToUpper())
                {
                    case ".PDF":
                        try
                        {

                            //obtem o tamanho do arquivo
                            int tamanho = UltimosBalancoFileUpload.PostedFile.ContentLength;
                            //cria um array de bytes para armazenar os dados binários da imagem
                            byte[] imgbyte = new byte[tamanho];
                            //armazena a imagem selecinada na memória
                            HttpPostedFile img = UltimosBalancoFileUpload.PostedFile;
                            //define os dados binários
                            img.InputStream.Read(imgbyte, 0, tamanho);


                            UltimosBalancoLabel.Text = "Arquivo: " + UltimosBalancoFileUpload.FileName + " Carregado";
                            UltimosBalancoLabel.ForeColor = System.Drawing.Color.Green;
                            UltimosBalancoCheckBox.Checked = true;



                            #region FIUltimosBalanco

                            //Pegando Informações do Arquivo
                            FileInfo infoarquivo = new FileInfo(UltimosBalancoFileUpload.FileName);
                            //Criando Caminho do arquivo
                            string pastaArquivo = "\\\\192.168.0.2\\anexosCRM\\" + ObjEntidadesClass.EntCod + "_2_ULTIMOS_BALANCOS.PDF";

                            //Pegando informações do caminho do arquivo criado
                            FileInfo arquivoServidor = new FileInfo(pastaArquivo);

                            //Verificando se o arquivo existe
                            if (arquivoServidor.Exists == true)
                            {
                                File.Delete(pastaArquivo);

                            }

                            //Salvamos o arquivo
                            UltimosBalancoFileUpload.PostedFile.SaveAs(pastaArquivo);



                            #endregion

                            /*Carregando Dados a Serem Salvos Futuramente*/
                            DocEntidadeClass.DocEntImage = imgbyte;
                            DocEntidadeClass.DocEntPathArq = pastaArquivo;

                            DocEntidadeClass.UsuCod = Session["usuario"].ToString();
                            ObjEntidadesClass.Remove_DocEntidade(DocEntidadeClass);
                            ObjEntidadesClass.Adiciona_DocEntidade(DocEntidadeClass);


                        }
                        catch
                        {
                            ObjEntidadesClass.Remove_DocEntidade(DocEntidadeClass);
                            UltimosBalancoLabel.Text = "Erro ao carregar o arquivo: " + UltimosBalancoFileUpload.FileName;
                            UltimosBalancoLabel.ForeColor = System.Drawing.Color.Red;
                            UltimosBalancoLabel.Visible = true;
                        }
                        break;

                    default:
                        UltimosBalancoLabel.Text = "Erro Tipo de Arquivo invalido. Arquivos Validos: PDF";
                        UltimosBalancoLabel.ForeColor = System.Drawing.Color.Red;
                        UltimosBalancoLabel.Visible = true;
                        break;
                }



            }
            #endregion


        }


        #endregion Load Anexos

        protected void ProximoPassoButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("FrmAbaObservacoes.aspx?indmnu=2");
        }

        protected void PrincipalButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaPrincipal.aspx?indmnu=2");
        }

        protected void ContatoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaContatos.aspx?indmnu=2");
        }

        protected void EnderecoEntregaButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaEnderecoEntrega.aspx?indmnu=2");
        }

        protected void InformacoesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaInformacoes.aspx?indmnu=2");
        }

        protected void AnexosButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaAnexo.aspx?indmnu=2");
        }

        protected void ObservacoesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaObservacoes.aspx?indmnu=2");
        }


        public void carregaDadosNaTela()
        {
            //Consulta os Documentos
            ObjEntidadesClass.Consulta_Documentos_Entidade();
            DocumentosGridView.DataSource = ObjEntidadesClass.ListDocEntidadeClass;
            DocumentosGridView.DataBind();
            DocumentosGridView.Visible = true;

        }




        public void BloqueiaCampos()
        {
            ProximoButton.Visible = false;



        }

        public void LiberaNavegacao()
        {

            ProximoButton.Visible = false;
            PrincipalButton.Visible = true;
            ContatoButton.Visible = true;
            EnderecoEntregaButton.Visible = true;
            InformacoesButton.Visible = true;
            ObservacoesButton.Visible = true;
            FiscalLinkButton.Visible = true;
            HoldingLinkButton.Visible = true;
            LogisticaLinkButton.Visible = true;
            VendedorLinkButton.Visible = true;

            PedidosLinkButton.Visible = true;
            AgendaLinkButton.Visible = true;
            NotasLinkButton.Visible = true;


            //Verifica se o Usuario possui algum Vendedor //Funcao temporaria para OCultar campos
            if (ObjUsuarioClass.ConsultaVendedorUsuario(Session["usuario"].ToString()) != 0)
            {
                HoldingLinkButton.Visible = false;
                LogisticaLinkButton.Visible = false;
            }




        }


        public void OcultarAnexos()
        {
            DocumentosFixosMultView.Visible = false;



        }

        public void DesocultarAnexos()
        {
            DocumentosFixosMultView.Visible = true;

        }

        protected void SelecionarButton_Click(object sender, EventArgs e)
        {
            string DocEntPathArq = ((Label)((Control)sender).FindControl("DocEntPathArqLabel")).Text;
            string DocEntObs = ((Label)((Control)sender).FindControl("DocEntObsLabel")).Text;

            System.IO.FileStream fs = new System.IO.FileStream(DocEntPathArq, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] ar = new byte[(int)fs.Length];
            fs.Read(ar, 0, (int)fs.Length);
            fs.Close();
            Response.AddHeader("content-disposition", "attachment;filename=" + DocEntObs + ".pdf");
            Response.ContentType = "application/octectstream";
            Response.BinaryWrite(ar);
            Response.End();
        }




        public void LiberaEdicao()
        {
            OutrosDocumentosLabel.Visible = true;
            NomeDocumentoIncluirLabel.Visible = true;
            NomeDocIncluirTextBox.Visible = true;
            IncluirDocLabel.Visible = false;
            IncluirDocFileUpload.Visible = true;
            IncluirDocButton.Visible = true;

            DocumentosGridView.Columns[6].Visible = true;

        }


        protected void IncluirDocButton_Click(object sender, EventArgs e)
        {
            DocEntidadeClass = new GerencialVendas.DocEntidadeClass();
            IncluirDocLabel.Visible = false;

            if (Session["clsEntidades"] != null)
            {

                //Descarega a session da Entidade
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                if (IncluirDocFileUpload.HasFile)
                {

                    string stipoArquivo = Path.GetExtension(IncluirDocFileUpload.PostedFile.FileName).ToLower();
                    DocEntidadeClass.DocEntObs = NomeDocIncluirTextBox.Text;

                    switch (stipoArquivo.ToUpper())
                    {
                        case ".PDF":

                            try
                            {

                                //obtem o tamanho do arquivo
                                int tamanho = IncluirDocFileUpload.PostedFile.ContentLength;
                                //cria um array de bytes para armazenar os dados binários da imagem
                                byte[] imgbyte = new byte[tamanho];
                                //armazena a imagem selecinada na memória
                                HttpPostedFile img = IncluirDocFileUpload.PostedFile;
                                //define os dados binários
                                img.InputStream.Read(imgbyte, 0, tamanho);

                                IncluirDocLabel.Text = "Arquivo: " + IncluirDocFileUpload.FileName + " Carregado";
                                IncluirDocLabel.ForeColor = System.Drawing.Color.Green;




                                #region Salvando Arquivo

                                //Pegando Informações do Arquivo
                                FileInfo infoarquivo = new FileInfo(IncluirDocFileUpload.FileName);
                                //Criando Caminho do arquivo
                                string pastaArquivo = "\\\\192.168.0.2\\anexosCRM\\" + ObjEntidadesClass.EntCod + DocEntidadeClass.DocEntObs + "_.PDF";

                                //Pegando informações do caminho do arquivo criado
                                FileInfo arquivoServidor = new FileInfo(pastaArquivo);

                                //Verificando se o arquivo existe
                                if (arquivoServidor.Exists == true)
                                {
                                    File.Delete(pastaArquivo);

                                }


                                //Salvamos o arquivo
                                IncluirDocFileUpload.PostedFile.SaveAs(pastaArquivo);



                                #endregion

                                /*Carregando Dados a Serem Salvos*/
                                DocEntidadeClass.EntCod = ObjEntidadesClass.EntCod;
                                DocEntidadeClass.DocEntImage = imgbyte;
                                DocEntidadeClass.DocEntPathArq = pastaArquivo;
                                DocEntidadeClass.UsuCod = Session["usuario"].ToString();

                                //Adiciona a Lista
                                ObjEntidadesClass.Adiciona_DocEntidade(DocEntidadeClass);

                                //Salva no Banco o Arquivo
                                DocEntidadeClass.Incluir_DocEntidade();

                                //Limpa o Campo de Nome
                                NomeDocIncluirTextBox.Text = "";

                                //Recarrega o Grid na Tela
                                carregaDadosNaTela();


                                //Carrega a Session 
                                Session["clsEntidades"] = ObjEntidadesClass;


                            }
                            catch
                            {
                                ObjEntidadesClass.Remove_DocEntidade(DocEntidadeClass);
                                IncluirDocLabel.Text = "Erro ao carregar o arquivo: " + AlteracaoContratualFileUpload.FileName;
                                IncluirDocLabel.ForeColor = System.Drawing.Color.Red;
                                IncluirDocLabel.Visible = true;
                            }
                            break;

                        default:
                            IncluirDocLabel.Text = "Erro Tipo de Arquivo invalido. Arquivos Validos: PDF";
                            IncluirDocLabel.ForeColor = System.Drawing.Color.Red;
                            IncluirDocLabel.Visible = true;
                            break;

                    }


                }
            }

        }

        protected void RemoverDocumentoButton_Click(object sender, EventArgs e)
        {
            DocEntidadeClass = new GerencialVendas.DocEntidadeClass();

            if (Session["clsEntidades"] != null)
            {

                //Descarega a session da Entidade
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                DocEntidadeClass.UsuCod = Session["usuario"].ToString();
                DocEntidadeClass.DocEntSeq = Convert.ToInt32(((Label)((Control)sender).FindControl("DocEntSeqLabel")).Text);
                DocEntidadeClass.EntCod = ObjEntidadesClass.EntCod;
                DocEntidadeClass.Remover_DocEntidade();

                //Recarrega o Grid
                carregaDadosNaTela();
            }
        }

        protected void FiscalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaFiscal.aspx?indmnu=2");
        }

        protected void HoldingButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmHolding.aspx?indmnu=2");
        }

        protected void CancelarOperacaoButton_Click(object sender, EventArgs e)
        {
            Session.Remove("ObjEntidadesClass");

            if (Session["Retornar"] != null)
            {
                Response.Redirect(Session["Retornar"].ToString());
            }
            else
            {

                Response.Redirect("FrmCarteira.aspx?indmnu=2");
            }
        }

        protected void LogisticaButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaLogistica.aspx?indmnu=2");
        }


        protected void VendedorButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmVendedorEntidade.aspx?indmnu=2");
        }


        protected void CrmButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("FrmHistoricoCRM.aspx?indmnu=12");

        }

        protected void DuplicatasButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaDuplicata.aspx?indmnu=2");

        }

        protected void PedidosButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../listas/FrmListaPedidos.aspx?indmnu=2");
        }

        protected void NotasButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmListaNotasFiscais.aspx?indmnu=2");
        }

        protected void AgendaButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAgenda.aspx?indmnu=2");
        }



    }


}