using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.WEBServiceSAP;

namespace VendasWeb.usercontrol
{
    public partial class UCCadastroCliente : System.Web.UI.UserControl
    {
        ClienteClasse OBJCliente = new ClienteClasse();
        usuario Objusuario = new usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Recupera usuario
            if (Session["usuario"] != null)
            {
                Objusuario.CodigoUsuario = Session["usuario"].ToString();
            }

            if (!IsPostBack)
            {

                if (Session["clienteClasse"] != null)
                {
                    //Libera acesso 
                    LiberaNavegacao();

                    //Descarega a session da Entidade
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];
                    OBJCliente.carregaDadosPrincipais();

                    Objusuario = new usuario();
                    Objusuario.CodigoUsuario = Session["usuario"].ToString();
                    Objusuario.ConsultaGrupos("Ativo");


                    #region Trata Acesso Menu

                    switch (OBJCliente.IDStatus)
                    {
                        case 1: //Status Cliente Prospectivo
                            EnviarAnalizeFinanceiroLinkButton.Visible = true;
                            break;

                        case 2: //Status Cliente Ativo
                            break;

                        case 3: //Status Cliente Inativo
                            break;

                        case 4: //Status Cliente Análise Financeira
                            //Verifica se esta no Grupo Análise Financeira
                            if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
                            {
                                ReprovarLinkButton.Visible = true;

                                if (OBJCliente.CodigoCliente == "")
                                {
                                    EnviarAnalizeFiscalLinkButton.Visible = true;
                                }
                                else
                                {
                                    ReprovarLinkButton.Visible = true;
                                    AprovarLinkButton.Visible = true;
                                }
                            }
                            break;

                        case 5: //Status Cliente Análise Fiscal
                            //Verifica se esta no Grupo Análise Fiscal
                            if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
                            {
                                ReprovarLinkButton.Visible = true;
                                AprovarLinkButton.Visible = true;
                            }
                            break;

                        default:
                            EnviarAnalizeFinanceiroLinkButton.Visible = true;
                            break;
                    }

                    #endregion


                }
                else
                {
                    BloqueiaNavegacao();
                }


            }
        }

        public void BloqueiaNavegacao()
        {
            LinkButtonAtualizar.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x disabled";
            EnderecosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-map-o fa-3x disabled";
            ContatosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x disabled";
            ObservacaoCompletaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x disabled";
            FinanceiroLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-money fa-3x disabled";
            LimiteCreditoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-usd fa-3x disabled";
            FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bar-chart fa-3x disabled";
            SolicitacaoAlteracaoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paper-plane fa-3x disabled";
            HistoricoClienteLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-edit fa-3x disabled";
            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled";
            ContasReceberLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-usd fa-3x disabled";
            AnaliseCreditoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-money fa-3x disabled";
            ObservacaoCompletaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x disabled";
        }

        public void LiberaNavegacao()
        {


            EnderecosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-map-o fa-3x";
            ContatosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x";
            //ObservacaoCompletaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x";
            FinanceiroLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-money fa-3x";
            FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bar-chart fa-3x";
            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x";

            //Consulta grupos
            Objusuario.ConsultaGrupos("Ativo");

            //Aba Fiscal somente liberada para usuarios do grupo fiscal
            if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
            {
                FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bar-chart fa-3x";
            }
            else
            {
                //FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bar-chart fa-3x disabled";
                FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bar-chart fa-3x";
            }

            //Aba Observação somente liberado para usuário Fiscal ou Financeiro
            if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0 || Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
            {
                ObservacaoCompletaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x";
            }
            else
            {
                ObservacaoCompletaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x disabled";
            }

            //Botão liberado apenas para quem faz parte do grupo de financeiro
            if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
            {
                AnaliseCreditoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-money fa-3x";
            }
            else
            {
                AnaliseCreditoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-money fa-3x disabled";
            }

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=2");
        }

        protected void EnderecosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteEnderecosWebForm.aspx?indmnu=2");
        }

        protected void ContatosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteContatosWebForm.aspx?indmnu=2");
        }

        protected void FinanceiroLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteFinanceiroWebForm.aspx?indmnu=2");
        }

        protected void FiscalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteFiscalWebForm.aspx?indmnu=2");
        }

        protected void ReprovarLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["clienteClasse"] != null)
            {

                OBJCliente = (ClienteClasse)Session["clienteClasse"];
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                OBJCliente.IDStatus = 1; //Prospectivo
                OBJCliente.AlteraStatusCliente();
                OBJCliente.carregaDadosPrincipais();

                //Envia e-mail 
                OBJCliente.EmailDescricaoTipoSolicitacao = "Cliente " + OBJCliente.CodigoCliente != "" ? OBJCliente.CodigoCliente + " Aprovado" : OBJCliente.IDCliente.ToString() + " Reprovado";
                OBJCliente.EmailDescricao = "Cliente " + OBJCliente.CodigoCliente != "" ? OBJCliente.CodigoCliente + " - " + OBJCliente.NomeCliente + " Reprovado." : OBJCliente.IDCliente.ToString() + " - " + OBJCliente.NomeCliente + " Reprovado.";
                OBJCliente.EnviaEmailVendedor();

                Session["Msg"] = "Cliente Reprovado com Sucesso!";

                Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=2");
            }
        }

        protected void EnviarAnalizeFinanceiroLinkButton_Click(object sender, EventArgs e)
        {
            string Erro = "";

            if (Session["clienteClasse"] != null)
            {
                OBJCliente = (ClienteClasse)Session["clienteClasse"];
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                OBJCliente.IDStatus = 4; //Analise Financeira

                Erro = OBJCliente.ValidaCadastroAnaliseCliente(OBJCliente.IDCliente, "Enviar Análise Financeiro");

                if (Erro == "")
                {
                    OBJCliente.AlteraStatusCliente();

                    //Envia e-mail 
                    OBJCliente.EmailDescricaoTipoSolicitacao = "Cliente Enviado para Analise Financeira";
                    OBJCliente.EmailDescricao = "Cliente Enviado para Analise Financeira com Sucesso!";
                    OBJCliente.EmailTipoSolicitacao = 4;
                    OBJCliente.EnviaEmail();

                    //Envia e-mail Vendedor
                    OBJCliente.EnviaEmailVendedor();

                    Session["Msg"] = "Cliente Enviado para Analise Financeira com Sucesso!";

                }
                else
                {
                    Session["Msg"] = Erro;
                }


                Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=2");
            }
        }

        protected void EnviarAnalizeFiscalLinkButton_Click(object sender, EventArgs e)
        {

            string Erro = "";

            if (Session["clienteClasse"] != null)
            {
                OBJCliente = (ClienteClasse)Session["clienteClasse"];
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                OBJCliente.IDStatus = 5; //Analise Fiscal

                Erro = OBJCliente.ValidaCadastroAnaliseCliente(OBJCliente.IDCliente, "Enviar Análise Fiscal");

                if (Erro == "")
                {
                    OBJCliente.AlteraStatusCliente();

                    //Envia e-mail 
                    OBJCliente.EmailDescricaoTipoSolicitacao = "Cliente Enviado para Análise Fiscal";
                    OBJCliente.EmailDescricao = "Cliente Enviado para Análise Fiscal com Sucesso!";
                    OBJCliente.EmailTipoSolicitacao = 3;
                    OBJCliente.EnviaEmail();

                    //Envia e-mail Vendedor
                    OBJCliente.EnviaEmailVendedor();

                    Session["Msg"] = "Cliente Enviado para Analise Fiscal com Sucesso!";

                }
                else
                {
                    Session["Msg"] = Erro;
                }

                Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=2");
            }
        }

        protected void AprovarLinkButton_Click(object sender, EventArgs e)
        {
            string Erro = "";

            if (Session["clienteClasse"] != null)
            {

                //ComunicacaoSAP WsComunicacaoSAP = new ComunicacaoSAP();
                string RetornoWs = "";

                OBJCliente = (ClienteClasse)Session["clienteClasse"];
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                OBJCliente.carregaDadosPrincipais();


                Erro = OBJCliente.ValidaCadastroAnaliseCliente(OBJCliente.IDCliente, "Aprovar Cadastro");

                if (Erro == "")
                {

                    //Chama API HUB 
                    if (OBJCliente.CodigoCliente == "")
                    {
                        RetornoWs = OBJCliente.PostCliente(OBJCliente.IDCliente, "Inclusão");
                    }
                    else
                    {
                        RetornoWs = OBJCliente.PostCliente(OBJCliente.IDCliente, "Alteração");
                    }


                    //Verifica Retorno API HUB
                    if (RetornoWs == "")
                    {
                        //Se retorno Ok, atualiza o Status
                        OBJCliente.IDStatus = 2; //Ativo
                        OBJCliente.AlteraStatusCliente();
                        OBJCliente.carregaDadosPrincipais();

                        //Envia e-mail 
                        OBJCliente.EmailDescricaoTipoSolicitacao = "Cliente " + OBJCliente.CodigoCliente != "" ? OBJCliente.CodigoCliente + " Aprovado" : OBJCliente.IDCliente.ToString() + " Aprovado";
                        OBJCliente.EmailDescricao = "Cliente " + OBJCliente.CodigoCliente != "" ? OBJCliente.CodigoCliente + " - " + OBJCliente.NomeCliente + " Aprovado Com Sucesso" : OBJCliente.IDCliente.ToString() + " - " + OBJCliente.NomeCliente + " Aprovado Com Sucesso";
                        OBJCliente.EnviaEmailVendedor();

                        Session["Msg"] = "Cliente Aprovado com Sucesso!";
                    }
                    else
                    {
                        Session["Msg"] = RetornoWs;
                    }

                }
                else
                {
                    Session["Msg"] = Erro;
                }


                Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=2");
            }
        }

        protected void ObservacaoCompletaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteObservacaoWebForm.aspx?indmnu=2");
        }

        protected void SolicitacaoAlteracaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteSolicitacaoAlteracaoWebForm.aspx?indmnu=2");
        }

        protected void LinkButtonAtualizar_Click(object sender, EventArgs e)
        {
            OBJCliente.AtualizacaoGeral();
            Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=2");
        }

        protected void LimiteCreditoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteLimiteCreditoWebForm.aspx?indmnu=2");
        }

        protected void HistoricoClienteLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteHistoricoWebForm.aspx?indmnu=2");
        }

        protected void ContasReceberLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteContasReceberWebForm.aspx?indmnu=2");
        }

        protected void AnaliseCreditoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/AnaliseCreditoWebForm.aspx?indmnu=2");
        }
    }
}