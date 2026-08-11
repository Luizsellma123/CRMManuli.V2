using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidade
{
    public partial class FrmAbaContatos : System.Web.UI.Page
    {
        usuario ObjUsuarioClass = new usuario();
        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        GerencialVendas.ContatoClass ObjContatoClass = new GerencialVendas.ContatoClass();
        criptografia mdlCriptografia = new criptografia();
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {            
            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            RamalContatoTextBox.CausesValidation = false;
            RequiredFieldValidator6.Visible = false;

            if (!IsPostBack)
            {
                //Valida Acesso
                OBJSessao.ValidaAcesso();

                if (Session["clsEntidades"] != null)
                {
                    //Descarrega session
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                    //Carrega dados na Tela
                    Atualizar_Grid();

                    if (ObjEntidadesClass.TipoOperacao == "Consultar")
                    {
                        BloqueiaCampos();
                    }
                }
            }
        }


        protected void TipoContatoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TipoContatoDropDownList.SelectedValue)
            {
                case "XML":
                    EmpresaLabel.Visible = false;
                    EmpresaTextBox.Visible = false;

                    NomeContatoTextBox.Visible = false;
                    NomeContatoLabel.Visible = false;

                    DDDTelefoneContatoTextBox.Visible = false;
                    TelefoneContatoLabel.Visible = false;
                    TelefoneContatoTextBox.Visible = false;
                    RamalContatoLabel.Visible = false;
                    RamalContatoTextBox.Visible = false;

                    CargoContatoLabel.Visible = false;
                    CargoContatoTextBox.Visible = false;

                    EmailContatoTextBox.Focus();
                    break;

                default:
                    EmpresaLabel.Visible = false;
                    EmpresaTextBox.Visible = false;

                    NomeContatoTextBox.Visible = true;
                    NomeContatoLabel.Visible = true;

                    DDDTelefoneContatoTextBox.Visible = true;
                    TelefoneContatoLabel.Visible = true;
                    TelefoneContatoTextBox.Visible = true;
                    RamalContatoLabel.Visible = true;
                    RamalContatoTextBox.Visible = true;

                    CargoContatoLabel.Visible = true;
                    CargoContatoTextBox.Visible = true;
                    NomeContatoTextBox.Focus();
                    break;
            }


            if (TipoContatoDropDownList.SelectedValue == "REFERÊNCIA COMERCIAL")
            {
                EmpresaLabel.Visible = true;
                EmpresaTextBox.Visible = true;
                EmpresaTextBox.Text = "";
                EmpresaLabel.Focus();
            }
            else
            {
                if (TipoContatoDropDownList.SelectedValue == "Outro")
                {
                    OutroTipoContatoLabel.Visible = true;
                    OutroTipoContatoTextBox.Visible = true;
                }
                else
                {
                    OutroTipoContatoLabel.Visible = false;
                    OutroTipoContatoTextBox.Visible = false;
                    OutroTipoContatoTextBox.Text = "";
                }

                /*EmpresaLabel.Visible = false;
                EmpresaTextBox.Visible = false;*/
                EmpresaTextBox.Text = "";
                NomeContatoTextBox.Focus();
            }
        }

        protected void NovoContato_Click(object sender, EventArgs e)
        {
            AdicionarButton.Visible = true;
            CancelarButton.Visible = true;
            NovoContatoButton.Visible = false;
            AlterarLinkButton.Visible = false;

            DadosContatoMultView.Visible = true;
            NomeContatoTextBox.Focus();
            TipoContatoDropDownList_SelectedIndexChanged(null, null);
        }

        protected void AdicionarButton_Click(object sender, EventArgs e)
        {
            ObjContatoClass = new GerencialVendas.ContatoClass();
            if (Session["clsEntidades"] != null)
            {
                //Descarrega session
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }
            int AUXENTCONTATOID = 0;

            if (ObjEntidadesClass.ListContatoClass != null)
            {
                if (ObjEntidadesClass.ListContatoClass.Count > 0)
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
            if (TipoContatoDropDownList.SelectedValue != "Outro")
                ObjContatoClass.TipoContato = TipoContatoDropDownList.SelectedValue;
            else
                ObjContatoClass.TipoContato = OutroTipoContatoTextBox.Text.ToString();
            ObjContatoClass.Cargo = CargoContatoTextBox.Text.ToString().ToUpper().Trim();
            ObjContatoClass.Ramal = RamalContatoTextBox.Text.ToString().Trim();
            ObjContatoClass.Empresa = EmpresaTextBox.Text.ToString().Trim();


            ObjEntidadesClass.AdicionarContato(ObjContatoClass);

            Session["clsEntidades"] = ObjEntidadesClass;

            Atualizar_Grid();

            CancelarButton_Click(null, null);

        }

        protected void CancelarButton_Click(object sender, EventArgs e)
        {

            EmpresaTextBox.Text = "";
            NomeContatoTextBox.Text = "";
            EmailContatoTextBox.Text = "";
            DDDTelefoneContatoTextBox.Text = "";
            TelefoneContatoTextBox.Text = "";
            CargoContatoTextBox.Text = "";
            RamalContatoTextBox.Text = "";


            AdicionarButton.Visible = false;
            CancelarButton.Visible = false;
            NovoContatoButton.Visible = true;
            AlterarLinkButton.Visible = false;
            DadosContatoMultView.Visible = false;

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
                /*if (ObjEntidadesClass.ListContatoClass.Count > 0)
                {*/
                ContatoGridView.DataSource = ObjEntidadesClass.ListContatoClass.ToList();
                ContatoGridView.DataBind();
                //}

                Session["clsEntidades"] = ObjEntidadesClass;
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
                                ObjEntidadesClass.ListContatoClass[t].ENTCONTATOID = Convert.ToInt32(ENTCONTATOIDLiteral.Text);
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
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Retorno, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                Session["clsEntidades"] = ObjEntidadesClass;

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Cadastro Atualizado com Sucesso!", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }


        public string CarregaDadosDaTela()
        {

            #region

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }


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

            ObjContatoClass = new GerencialVendas.ContatoClass();

            ObjContatoClass.Empresa = EmpresaTextBox.Text;
            ObjContatoClass.Nome = NomeContatoTextBox.Text;
            ObjContatoClass.Email = EmailContatoTextBox.Text;
            ObjContatoClass.DDDTelefone = DDDTelefoneContatoTextBox.Text;
            ObjContatoClass.Telefone = TelefoneContatoTextBox.Text;
            ObjContatoClass.Ramal = RamalContatoTextBox.Text;
            ObjContatoClass.Cargo = CargoContatoTextBox.Text;


            #endregion

            return "";
        }


        protected void AlterarButton_Click_Grid(object sender, EventArgs e)
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
            AdicionarButton.Visible = false;
            CancelarButton.Visible = true;
            AlterarLinkButton.Visible = true;
        }


        protected void ProximoPassoButton_Click(object sender, EventArgs e)
        {
            //Descarega a sessao
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            //Carrega os Dados da Tela
            //CarregaDadosDaTela();

            //Guarda os dados em Session
            Session["clsEntidades"] = ObjEntidadesClass;

            if (ObjEntidadesClass.ListContatoClass != null && ObjEntidadesClass.Origem == "Analise")
            {
                if (ObjEntidadesClass.ListContatoClass.Where(C => C.TipoContato == "Financeiro").Count() > 0)
                {
                    Response.Redirect("frmAbaEntRelacionamento.aspx?indmnu=2");
                }
                else
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Adicione ao menos um contato do tipo Financeiro!", true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }
            else
            if (ObjEntidadesClass.ListContatoClass != null && (ObjEntidadesClass.Origem == "Alterar" || ObjEntidadesClass.Origem == null))
            {
                if (ObjEntidadesClass.ListContatoClass.Where(C => C.TipoContato == "COMERCIAL").Count() > 0)
                {
                    Response.Redirect("frmAbaEntRelacionamento.aspx?indmnu=2");
                }
                else
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Adicione ao menos um contato do tipo Comercial!", true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }
            else
            {
                //Retorna Mensagem de Erro
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Adicione ao menos um contato do tipo Comercial!", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void AlterarLinkButton_Click(object sender, EventArgs e)
        {

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
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

        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaPrincipal.aspx?indmnu=2");
        }


        protected void BloqueiaCampos()
        {
            NovoContatoButton.Visible = false;
            ContatoGridView.Columns[9].Visible = false;
            ContatoGridView.Columns[10].Visible = false;
        }


    }
}