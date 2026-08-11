using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{
    public partial class FrmAbaContatos : System.Web.UI.Page
    {
        usuario ObjUsuarioClass = new usuario();
        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        GerencialVendas.ContatoClass ObjContatoClass = new GerencialVendas.ContatoClass();
        criptografia mdlCriptografia = new criptografia();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Valida Acesso
                OBJSessao.ValidaAcesso();

                DadosContatoMultView.Visible = false;

                if (Session["clsEntidades"] != null)
                {
                    //Descarrega session
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmAbaContatos");
                    }

                    //Carrega dados na Tela
                    Atualizar_Grid();


                    //Verifica a operação
                    switch (ObjEntidadesClass.TipoOperacao)
                    {

                        case "ADM_VENDAS":
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            break;

                        case "ADM_FISCAL":
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            break;

                        case "ADM_FINANCEIRO":
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            break;

                        case "Cadastro Incompleto":
                            #region
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            #endregion
                            break;


                        case "Cadastro Completo":
                            #region
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            #endregion
                            break;

                        case "Consulta":
                            LiberaNavegacao();

                            //Se tiver em status "Cadastro Incompleto" libera campo para Finalizar Cadastro
                            if (ObjEntidadesClass.StatEntCod == "13")
                            {
                                AlterarButton.Visible = true;
                            }
                            else
                            {
                                BloqueiaCampos();
                            }

                            break;
                    }
                }
            }
        }

        protected void TipoContatoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TipoContatoDropDownList.SelectedValue == "REFERÊNCIA COMERCIAL")
            {
                EmpresaLabel.Visible = true;
                EmpresaTextBox.Visible = true;
                EmpresaTextBox.Text = "";
                EmpresaLabel.Focus();
            }
            else
            {
                EmpresaLabel.Visible = false;
                EmpresaTextBox.Visible = false;
                EmpresaTextBox.Text = "";
                NomeContatoTextBox.Focus();
            }
        }


        protected void NovoContato_Click(object sender, EventArgs e)
        {
            AdcionarButton.Visible = true;
            DadosContatoMultView.Visible = true;
            AlterarLinkButton.Visible = false;
            NomeContatoTextBox.Focus();
            TipoContatoDropDownList_SelectedIndexChanged(null, null);
            NovoContatoButton.Visible = false;
        }

        protected void AdcionarButton_Click(object sender, EventArgs e)
        {
            string Validacao = "";

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            if (TipoContatoDropDownList.SelectedValue != "REFERÊNCIA COMERCIAL")
            {
                if (EmailContatoTextBox.Text.ToString().ToUpper() == "")
                {
                    Validacao = "Informe o Email antes de Salvar!";

                }
                else
                {

                    if (mdlFuncoes.Valida_Email(EmailContatoTextBox.Text.ToString()) == false)
                    {
                        Validacao = "Email Invalido";
                    }
                }
            }

            if (DDDTelefoneContatoTextBox.Text.ToString() == "")
            {
                Validacao = "Informe o DDD antes de Salvar!";
            }


            if (TelefoneContatoTextBox.Text.ToString() == "")
            {
                Validacao = "Informe o Telefone antes de Salvar!";
            }


            if (TipoContatoDropDownList.SelectedValue == "REFERÊNCIA COMERCIAL")
            {
                if (EmpresaTextBox.Text == "")
                {
                    Validacao = "Informe a Empresa.";
                }
            }


            if (NomeContatoTextBox.Text == "")
            {
                Validacao = "Informe o nome do Contato";
            }


            if (Validacao == "")
            {
                ObjContatoClass = new GerencialVendas.ContatoClass();

                int AUXENTCONTATOID = 0;

                if (ObjEntidadesClass.ListContatoClass != null)
                {
                    AUXENTCONTATOID = ObjEntidadesClass.ListContatoClass.OrderBy(C => C.ENTCONTATOID).First().ENTCONTATOID;
                }

                if (AUXENTCONTATOID < 0)
                {

                    ObjContatoClass.ENTCONTATOID = AUXENTCONTATOID - 1;
                }
                else
                {
                    ObjContatoClass.ENTCONTATOID = (AUXENTCONTATOID + 1) * -1;
                }

                ObjContatoClass.TipoOperacao = "Incluir";

                ObjContatoClass.Nome = NomeContatoTextBox.Text.ToString().ToUpper().Trim();
                ObjContatoClass.Email = EmailContatoTextBox.Text.ToString().ToUpper().Trim();
                ObjContatoClass.DDDTelefone = DDDTelefoneContatoTextBox.Text.ToString().Trim();
                ObjContatoClass.Telefone = TelefoneContatoTextBox.Text.ToString().Trim();
                ObjContatoClass.TipoContato = TipoContatoDropDownList.SelectedValue;
                ObjContatoClass.Cargo = CargoContatoTextBox.Text.ToString().ToUpper().Trim();
                ObjContatoClass.Ramal = RamalContatoTextBox.Text.ToString().Trim();
                ObjContatoClass.Empresa = EmpresaTextBox.Text.ToString().Trim();



                ObjEntidadesClass.AdicionarContato(ObjContatoClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();

                CancelarButton_Click(null, null);
            }
            else
            {
                Response.Write("<script>alert(\""+Validacao+"\");</script>");                
            }
        }

        protected void CancelarButton_Click(object sender, EventArgs e)
        {
            /*NomeContatoLabel.Visible = false;
            NomeContatoTextBox.Visible = false;
            EmailContatoLabel.Visible = false;
            EmailContatoTextBox.Visible = false;
            TelefoneContatoLabel.Visible = false;
            DDDTelefoneContatoTextBox.Visible = false;
            TelefoneContatoTextBox.Visible = false;
            TipoContatoLabel.Visible = false;
            TipoContatoDropDownList.Visible = false;
            CargoContatoLabel.Visible = false;
            CargoContatoTextBox.Visible = false;
            RamalContatoLabel.Visible = false;
            RamalContatoTextBox.Visible = false;
            AdcionarButton.Visible = false;
            CancelarButton.Visible = false;*/


            NomeContatoTextBox.Text = "";
            EmailContatoTextBox.Text = "";
            DDDTelefoneContatoTextBox.Text = "";
            TelefoneContatoTextBox.Text = "";
            CargoContatoTextBox.Text = "";
            RamalContatoTextBox.Text = "";
            DadosContatoMultView.Visible = false;
            NomeContatoTextBox.Focus();
            NovoContatoButton.Visible = true;
            NovoContatoButton.Focus();
        }

        protected void RemoverButton_Click(object sender, EventArgs e)
        {
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                ObjContatoClass = new GerencialVendas.ContatoClass();
                ObjContatoClass.ENTCONTATOID = Convert.ToInt32(((Label)((Control)sender).FindControl("ENTCONTATOIDLabel")).Text);
                ObjContatoClass.TipoOperacao = "Remover";
                ObjContatoClass.Nome = ((Label)((Control)sender).FindControl("NomeLabel")).Text;
                ObjContatoClass.Email = ((Label)((Control)sender).FindControl("EmailLabel")).Text;
                ObjContatoClass.DDDTelefone = ((Label)((Control)sender).FindControl("DDDTelefoneLabel")).Text;
                ObjContatoClass.Telefone = ((Label)((Control)sender).FindControl("TelefoneLabel")).Text;
                ObjContatoClass.TipoContato = ((Label)((Control)sender).FindControl("TipoContatoLabel")).Text;

                ObjEntidadesClass.RemoverContato(ObjContatoClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();
            }
        }

        public void Atualizar_Grid()
        {
            if (ObjEntidadesClass.ListContatoClass != null)
            {
                //Carrega Grid na Tela
                //ContatoGridView.DataSource = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoOperacao != "Remover" && C.TipoContato != "Responsavel").ToList();
                ContatoGridView.DataSource = ObjEntidadesClass.ListContatoClass.ToList();
                ContatoGridView.DataBind();

                Session["clsEntidades"] = ObjEntidadesClass;
            }
        }

        public void BloqueiaCampos()
        {
            ContatoGridView.Columns[9].Visible = false;
            ContatoGridView.Columns[10].Visible = true;
            ContatoLiteral.Visible = false;
            NovoContatoButton.Visible = false;
            Passo3Button.Visible = false;
        }


        protected void Passo3Button_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            int TipoContatoComercial = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoContato == "COMERCIAL").Count();
            int TipoContatoFinanceiro = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoContato == "FINANCEIRO").Count();
            int TipoContatoLogistica = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoContato == "LOGISTICA").Count();
            int TipoContatoMarketing = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoContato == "MARKETING").Count();
            int TipoContatoReferenciaComercial = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoContato == "REFERÊNCIA COMERCIAL").Count();

            if (ObjEntidadesClass.TipoOperacao != "Consulta")
            {
                if (TipoContatoComercial > 0)
                {
                    Response.Redirect("FrmAbaFiscal.aspx?indmnu=2");
                }
                else
                {
                    Response.Write("<script>alert(\"Por favor verificar os Contatos.Ao menos um do Tipo Comercial deve ser preenchido!\");</script>");
                }
            }
            else
            {
                Response.Redirect("FrmAbaEnderecoEntrega.aspx?indmnu=2");
            }
        }

        protected void AlterarButton_Click(object sender, EventArgs e)
        {
            string Retorno = "";
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            //Pega o usuario que esta alterando
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            #region Gravar Telefone de Contato
            if (ObjEntidadesClass.ListContatoClass != null)
            {
                if (ObjEntidadesClass.ListContatoClass.Count > 0)
                {
                    //Percorre a lista de contatos
                    for (int t = 0; t < ObjEntidadesClass.ListContatoClass.Count; t++)
                    {
                        ObjEntidadesClass.ListContatoClass[t].UsuCod = Session["usuario"].ToString();
                        ObjEntidadesClass.ListContatoClass[t].EntCod = ObjEntidadesClass.EntCod;

                        //Se igual a incluir
                        if (ObjEntidadesClass.ListContatoClass[t].TipoOperacao == "Incluir")
                        {
                            Retorno += ObjEntidadesClass.ListContatoClass[t].Incluir_Contato();
                        }
                        else
                        {
                            //se alterar
                            if (ObjEntidadesClass.ListContatoClass[t].TipoOperacao == "Alterar")
                            {
                                Retorno += ObjEntidadesClass.ListContatoClass[t].Alterar_Contato();
                            }
                            else
                            {
                                //Se Remover
                                if (ObjEntidadesClass.ListContatoClass[t].TipoOperacao == "Remover")
                                {
                                    Retorno += ObjEntidadesClass.ListContatoClass[t].Remove_Contato();
                                }
                            }

                        }
                    }
                }
            }
            #endregion

            //Verifica se a alteração não esta sendo feita em uma entidade ja ativa, se estiver vai enviar para Cadastro Incompleto
            ObjEntidadesClass.Alterar_Status_Entidade_Cadastro_Incompleto();

            if (Retorno != "")
            {
                Response.Write("<script>alert(\"" + Retorno + "\");</script>");
            }
            else
            {
                Session["clsEntidades"] = ObjEntidadesClass;
                Response.Write("<script>alert(\"Cadastro Atualizado com Sucesso!\");</script>");
            }
        }

        public void LiberaNavegacao()
        {
            Passo3Button.Visible = false;

            PrincipalButton.Visible = true;
            EnderecoEntregaButton.Visible = true;
            InformacoesButton.Visible = true;
            AnexosButton.Visible = true;
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
            Session.Remove("clsEntidades");

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


        protected void AlterarButton_Click1(object sender, EventArgs e)
        {
            //Indicação de qual Contato Alterar
            ENTCONTATOIDLiteral.Text = ((Label)((Control)sender).FindControl("ENTCONTATOIDLabel")).Text;

            NomeContatoTextBox.Focus();
            NomeContatoTextBox.Text = ((Label)((Control)sender).FindControl("NomeLabel")).Text;

            EmailContatoTextBox.Text = ((Label)((Control)sender).FindControl("EmailLabel")).Text;
            DDDTelefoneContatoTextBox.Text = ((Label)((Control)sender).FindControl("DDDTelefoneLabel")).Text;
            TelefoneContatoTextBox.Text = ((Label)((Control)sender).FindControl("TelefoneLabel")).Text;
            TipoContatoDropDownList.SelectedValue = ((Label)((Control)sender).FindControl("TipoContatoLabel")).Text;
            CargoContatoTextBox.Text = ((Label)((Control)sender).FindControl("RamalLabel")).Text;
            RamalContatoTextBox.Text = ((Label)((Control)sender).FindControl("CargoLabel")).Text;
            TipoContatoDropDownList_SelectedIndexChanged(null, null);
            EmpresaTextBox.Text = ((Label)((Control)sender).FindControl("EmpresaLabel")).Text;

            DadosContatoMultView.Visible = true;
            NovoContatoButton.Visible = false;
            AdcionarButton.Visible = false;
        }

        protected void AlterarLinkButton_Click(object sender, EventArgs e)
        {
            string Validacao = "";

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            if (TipoContatoDropDownList.SelectedValue != "REFERÊNCIA COMERCIAL")
            {
                if (EmailContatoTextBox.Text.ToString().ToUpper() == "")
                {
                    Validacao = "Informe o Email antes de Salvar!";
                }
                else
                {
                    if (mdlFuncoes.Valida_Email(EmailContatoTextBox.Text.ToString()) == false)
                    {
                        Validacao = "Email Invalido";
                    }
                }
            }

            if (DDDTelefoneContatoTextBox.Text.ToString() == "")
            {
                Validacao = "Informe o DDD antes de Salvar!";
            }


            if (TelefoneContatoTextBox.Text.ToString() == "")
            {
                Validacao = "Informe o Telefone antes de Salvar!";
            }


            if (TipoContatoDropDownList.SelectedValue == "REFERÊNCIA COMERCIAL")
            {
                if (EmpresaTextBox.Text == "")
                {
                    Validacao = "Informe a Empresa.";
                }
            }


            if (NomeContatoTextBox.Text == "")
            {
                Validacao = "Informe o nome do Contato";
            }

            if (Validacao == "")
            {
                ObjContatoClass = new GerencialVendas.ContatoClass();

                ObjContatoClass.ENTCONTATOID = Convert.ToInt32(ENTCONTATOIDLiteral.Text);

                ObjContatoClass.Nome = NomeContatoTextBox.Text.ToString().ToUpper();
                ObjContatoClass.Email = EmailContatoTextBox.Text.ToString().ToUpper();
                ObjContatoClass.DDDTelefone = DDDTelefoneContatoTextBox.Text.ToString();
                ObjContatoClass.Telefone = TelefoneContatoTextBox.Text.ToString();
                ObjContatoClass.TipoContato = TipoContatoDropDownList.SelectedValue;
                ObjContatoClass.Cargo = CargoContatoTextBox.Text.ToString().ToUpper();
                ObjContatoClass.Ramal = RamalContatoTextBox.Text.ToString();
                ObjContatoClass.Empresa = EmpresaTextBox.Text.ToString();

                ObjEntidadesClass.AlteraContato(ObjContatoClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();

                CancelarButton_Click(null, null);
            }
            else
            {
                Response.Write("<script>alert(\"" + Validacao + "\");</script>");
            }
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