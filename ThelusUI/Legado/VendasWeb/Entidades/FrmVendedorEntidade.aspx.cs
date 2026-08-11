using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{

    public partial class FrmVendedorEntidade : System.Web.UI.Page
    {
        usuario ObjUsuarioClass = new usuario();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        GerencialVendas.VendedorClass ObjVendedorClass = new GerencialVendas.VendedorClass();
        criptografia mdlCriptografia = new criptografia();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                VendedorMultView.Visible = false;

                if (Session["clsEntidades"] != null)
                {
                    //Descarrega session
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmVendedorEntidade");
                    }


                    //Carrega dados na Tela
                    CarregaDadosNaTela();



                    //Verifica a operação
                    switch (ObjEntidadesClass.TipoOperacao)
                    {

                        case "ADM_VENDAS":
                            AlterarButton.Visible = true;
                            LiberaNavegacao();

                            break;

                        case "ADM_FISCAL":
                            LiberaNavegacao();
                            BloqueiaCampos();
                            break;

                        case "ADM_FINANCEIRO":
                            LiberaNavegacao();
                            BloqueiaCampos();
                            break;

                        case "Cadastro Incompleto":
                            #region
                            LiberaNavegacao();
                            BloqueiaCampos();
                            VendedorGridView.Columns[3].Visible = true;


                            #endregion
                            break;

                        case "Cadastro Completo":
                            #region
                            LiberaNavegacao();
                            BloqueiaCampos();
                            VendedorGridView.Columns[3].Visible = true;
                            #endregion
                            break;

                        case "Consulta":
                            LiberaNavegacao();
                            BloqueiaCampos();
                            VendedorGridView.Columns[3].Visible = true;
                            break;


                    }


                }
            }



        }

        protected void btnListar_Click(object sender, EventArgs e)
        {


            switch (VendedorDropDownList.SelectedValue.ToString())
            {
                case "1":
                    ObjVendedorClass.VendCod = txtFiltro.Text;
                    break;

                case "2":
                    ObjVendedorClass.VendNome = txtFiltro.Text;
                    break;


            }


            VendedorGridView.DataSource = ObjVendedorClass.Consulta_Vendedor();
            VendedorGridView.DataBind();


            VendedorMultView.Visible = true;
        }

        protected void SelecionarCheckedChanged(object sender, EventArgs e)
        {
            //Pegando dados da Session
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            ObjVendedorClass.VendCod = ((Label)((Control)sender).FindControl("VendCodLabel")).Text;
            ObjVendedorClass.VendNome = ((Label)((Control)sender).FindControl("VendNomeLabel")).Text;
            ObjVendedorClass.VendEntPrinc = "Sim";
            ObjVendedorClass.VendEntPrincBit = true;

            ObjEntidadesClass.AlteraVendEntPrincipal(ObjVendedorClass);


            Session["clsEntidades"] = ObjEntidadesClass;


            Atualizar_Grid();
        }

        protected void AlterarButton_Click(object sender, EventArgs e)
        {

            string Retorno = "";
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];



            #region
            if (ObjEntidadesClass.ListVendEnt != null)
            {

                if (ObjEntidadesClass.ListVendEnt.Where(L => L.TipoOperacao != "Remover" && L.VendEntPrinc == "Sim").ToList().Count() > 0)
                {
                    //Percorre a lista de contatos
                    for (int i = 0; i < ObjEntidadesClass.ListVendEnt.Count; i++)
                    {
                        ObjEntidadesClass.ListVendEnt[i].UsuCod = Session["usuario"].ToString();
                        ObjEntidadesClass.ListVendEnt[i].EntCod = ObjEntidadesClass.EntCod;

                        //Se igual a incluir
                        if (ObjEntidadesClass.ListVendEnt[i].TipoOperacao == "Incluir")
                        {
                            Retorno += ObjEntidadesClass.ListVendEnt[i].Incluir_Vend_Ent();
                        }
                        else
                        {
                            //se alterar
                            if (ObjEntidadesClass.ListVendEnt[i].TipoOperacao == "Alterar")
                            {
                                Retorno += ObjEntidadesClass.ListVendEnt[i].Altera_Vend_Ent();
                            }
                            else
                            {
                                //Se Remover
                                if (ObjEntidadesClass.ListVendEnt[i].TipoOperacao == "Remover")
                                {
                                    Retorno += ObjEntidadesClass.ListVendEnt[i].Remove_Cond_Vend_Ent();
                                }
                            }

                        }
                    }
                }
                else
                {
                    Retorno = "Ao menos um Vendedor Selecionado como Principal deve estar vinculado a Entidade";

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

        protected void btnAdiciona_Click(object sender, EventArgs e)
        {

            VendedorMultView.Visible = false;

            //Pegando dados da Session
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            ObjVendedorClass = new GerencialVendas.VendedorClass();
            ObjVendedorClass.VendCod = ((Label)((Control)sender).FindControl("VendCodLabel")).Text;
            ObjVendedorClass.VendNome = ((Label)((Control)sender).FindControl("VendNomeLabel")).Text;
            ObjVendedorClass.TipoOperacao = "Incluir";
            ObjVendedorClass.VendEntPrinc = "Não";
            ObjVendedorClass.VendEntPrincBit = false;

            ObjEntidadesClass.AdicionarVendEnt(ObjVendedorClass);


            Session["clsEntidades"] = ObjEntidadesClass;


            Atualizar_Grid();
        }

        protected void RemoverButton_Click(object sender, EventArgs e)
        {
            if (Session["clsEntidades"] != null)
            {


                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                ObjVendedorClass = new GerencialVendas.VendedorClass();
                ObjVendedorClass.VendCod = ((Label)((Control)sender).FindControl("VendCodLabel")).Text;
                ObjVendedorClass.VendNome = ((Label)((Control)sender).FindControl("VendNomeLabel")).Text;
                ObjVendedorClass.TipoOperacao = "Remover";
                ObjVendedorClass.VendEntPrinc = "Não";
                ObjVendedorClass.VendEntPrincBit = false;

                ObjEntidadesClass.RemoveVendEnt(ObjVendedorClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();

            }
        }

        public void LiberaNavegacao()
        {
            PrincipalButton.Visible = true;
            EnderecoEntregaButton.Visible = true;
            InformacoesButton.Visible = true;
            AnexosButton.Visible = true;
            ObservacoesButton.Visible = true;
            HoldingLinkButton.Visible = true;
            FiscalLinkButton.Visible = true;
            ContatoButton.Visible = true;
            LogisticaLinkButton.Visible = true;
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

        public void BloqueiaCampos()
        {
            VendedorLabel.Visible = false;
            VendedorDropDownList.Visible = false;
            txtFiltro.Visible = false;
            BuscarButton.Visible = false;
            VendedorMultView.Visible = false;
        }

        public void CarregaDadosNaTela()
        {
            Atualizar_Grid();

        }

        public void Atualizar_Grid()
        {
            if (ObjEntidadesClass.ListVendEnt != null)
            {
                //Carrega Grid na Tela
                VendEntGridView.DataSource = ObjEntidadesClass.ListVendEnt.Where(VE => VE.TipoOperacao != "Remover");
                VendEntGridView.DataBind();
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