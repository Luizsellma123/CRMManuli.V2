using System;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.IO;
using System.Text;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoCENPROTWebForm : System.Web.UI.Page
    {
        ClienteClasse ObjCliente = new ClienteClasse();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass ObjSessao = new SessionClass();
        criptografia objCriptografia = new criptografia();
        CENPROTClass objCENPROTClass = new CENPROTClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            ObjSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ApresentaMensagem(Session["Msg"].ToString());

                Session["Msg"] = null;
            }

            if (!IsPostBack)
            {
                if (Session["clienteClasse"] != null)
                {
                    ObjCliente = (ClienteClasse)Session["clienteClasse"];

                    IDClienteHiddenField.Value = ObjCliente.IDCliente.ToString();

                    IDAnaliseHiddenField.Value = ObjCliente.IDAnalise.ToString();

                    CarregaDadosNaTela();
                }

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }
        }

        protected void CarregaDadosNaTela()
        {
            CarregaDadosPrincipais();

            //CarregaDadosModal();

            Carrega_CENPROT_GridView();
        }

        protected void CarregaDadosPrincipais()
        {
            DataTable Dados = ObjCliente.CarregaAnaliseCreditoDetalhe();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    AnaliseTextBox.Text = row["IDAnalise"].ToString();
                    DataTextBox.Text = row["DataAnalise"].ToString();
                    CodigoTextBox.Text = ObjCliente.CodigoCliente;
                    NomeTextBox.Text = row["RAZAO"].ToString();
                    FantasiaTextBox.Text = row["NOMEFANTASIA"].ToString();
                }
            }
        }

        protected void CarregaDadosModal()
        {
            string erro = "";

            try
            {                
                objCENPROTClass = ObjCliente.Consulta_CENPROT_PARAMETROS();

                string dataValidade = "";

                dataValidade = Convert.ToDateTime(objCENPROTClass.PKCS12VALID).ToString("dd-MM-yyyy");

                CertificadoTextBox.Text = RetornaNomeEmpresacertificadoDesencriptado();

                DataValidadeTextBox.Text = dataValidade;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") ApresentaMensagem(erro);
        }

        protected string RetornaNomeEmpresacertificadoDesencriptado()
        {
            string caminhoAbsolutoArquivo = ObjCliente.DescriptografaCertificado();

            string senhaCertificado = ObjCliente.DescriptografaSenhaCertificado();

            string NomeEmpresa = "";

            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificate =
                new System.Security.Cryptography.X509Certificates.X509Certificate2(caminhoAbsolutoArquivo, senhaCertificado);

                NomeEmpresa = certificate.FriendlyName.Substring(0, certificate.FriendlyName.IndexOf(":"));

                File.Delete(caminhoAbsolutoArquivo);
            }
            catch (Exception ex)
            {
                File.Delete(caminhoAbsolutoArquivo);

                throw new Exception(ex.Message);
            }

            return NomeEmpresa;
        }

        protected void AtualizarModalLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            try
            {
                FileUpload objArquivo = ArquivoFileUpload;

                if (objArquivo.HasFile == true)
                {
                    FileInfo file = new FileInfo(objArquivo.FileName);

                    if (file.Extension != ".pfx")
                    {
                        erro = "Este arquivo não é um certificado.";
                    }
                    else if (SenhaTextBox.Text == "")
                    {
                        erro = "Informe a senha.";
                    }
                    else
                    {
                        string caminhoTemp = Server.MapPath("~/Temp/");

                        if (!Directory.Exists(caminhoTemp))
                        {
                            Directory.CreateDirectory(caminhoTemp);
                        }

                        string caminhoAbsolutoArquivo = Path.Combine(caminhoTemp, objArquivo.FileName);

                        objArquivo.SaveAs(caminhoAbsolutoArquivo);

                        try
                        {
                            string senhaCertificado = SenhaTextBox.Text;

                            System.Security.Cryptography.X509Certificates.X509Certificate2 certificate =
                            new System.Security.Cryptography.X509Certificates.X509Certificate2(caminhoAbsolutoArquivo, senhaCertificado);

                            string dataExpiracaoCertificado = certificate.GetExpirationDateString();

                            if (!(Convert.ToDateTime(dataExpiracaoCertificado) > DateTime.Now.Date))
                                throw new Exception("Certificado inválido ou vencido.");

                            string nomeEmpresa = certificate.FriendlyName.Substring(0, certificate.FriendlyName.IndexOf(":"));

                            ObjCliente.PKCS12CERT = ObjCliente.CriptografaCertificado(caminhoAbsolutoArquivo);

                            ObjCliente.PKCS12PASS = ObjCliente.CriptografaSenhaCertificado(senhaCertificado);

                            ObjCliente.PKCS12VALID = Convert.ToDateTime(dataExpiracaoCertificado).ToString("dd-MM-yyyy");

                            erro = ObjCliente.Grava_CRM_CENPROT_PARAMETROS();
                        }
                        catch (Exception ex)
                        {
                            erro = ex.Message;
                        }

                        File.Delete(caminhoAbsolutoArquivo);
                    }
                }
                else
                {
                    erro = "Escolha um arquivo";
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "")
            {
                Session["Msg"] = erro;

                Response.Redirect("~/Clientes/AnaliseCreditoCENPROTWebForm.aspx?indmnu=5");
            }
            else
            {
                ApresentaMensagem();
            }
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            CarregaIDs();

            //Valida PKCS12VALID
            //{
            //    objCENPROTClass = ObjCliente.Consulta_CENPROT_PARAMETROS();

            //    if (objCENPROTClass.PKCS12VALID != "" && objCENPROTClass.PKCS12VALID != null)
            //    {
            //        if (!(Convert.ToDateTime(objCENPROTClass.PKCS12VALID) > DateTime.Now.Date))
            //        {
            //            erro = "Certificado inválido ou vencido.";
            //        }
            //    }
            //    else
            //    {
            //        erro = "Não há parametros do CENPROT no banco de dados.";
            //    }
            //}

            if (erro == "") erro = ObjCliente.Consulta_CENPROT_CRMAPI();

            Carrega_CENPROT_GridView();

            if (erro != "") ApresentaMensagem(erro);
        }

        protected void CarregaIDs()
        {
            ObjCliente.IDCliente = Convert.ToInt32(IDClienteHiddenField.Value);

            ObjCliente.IDAnalise = Convert.ToInt32(IDAnaliseHiddenField.Value);
        }

        protected void Carrega_CENPROT_GridView()
        {
            CarregaIDs();

            DataTable Dados = ObjCliente.Carrega_CENPROT_GridView();

            CENPROTGridView.DataSource = Dados;
            CENPROTGridView.DataBind();
            CENPROTMultiView.Visible = true;
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoDetalheWebForm.aspx?indmnu=5");
        }

        protected void CENPROTGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CENPROTGridView.PageIndex = e.NewPageIndex;
            Carrega_CENPROT_GridView();
        }
    }
}