using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class CadastroClienteFiscalWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";


                CarregaCombo();



                if (Session["clienteClasse"] != null)
                {
                    //Descarega a session da Entidade
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];

                    //Carrega dados
                    CarregaDadosNaTela();

                    TrataAcesso();



                }

                BloqueiaCamposConsultaSefaz();

            }

        }


        public void CarregaCombo()
        {
            DataTable RetornoDados = new DataTable();

            //Recupera Naturezas Juridicas
            RetornoDados = OBJCliente.CarregaNaturezasJuridicas();
            NaturezaJuridicaDropDownList.DataSource = RetornoDados;
            NaturezaJuridicaDropDownList.DataValueField = "IDNatureza";
            NaturezaJuridicaDropDownList.DataTextField = "CodigoSAP";
            NaturezaJuridicaDropDownList.DataBind();
            NaturezaJuridicaDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

            //Recupera Codigos CNAE
            RetornoDados = OBJCliente.CarregaCodigosCNAE();
            IDCNAEDropDownList.DataSource = RetornoDados;
            IDCNAEDropDownList.DataValueField = "IDCNAE";
            IDCNAEDropDownList.DataTextField = "DescricaoCNAE";
            IDCNAEDropDownList.DataBind();
            IDCNAEDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

            //Recupera NATUREZA DESTINACAO
            RetornoDados = OBJCliente.CarregaNaturezaDestinacao();
            IDNaturezaDestinacaoCheckBoxList.DataSource = RetornoDados;
            IDNaturezaDestinacaoCheckBoxList.DataValueField = "IDNaturezaDestinacao";
            IDNaturezaDestinacaoCheckBoxList.DataTextField = "Nome";
            IDNaturezaDestinacaoCheckBoxList.DataBind();

            //Recupera Enquadramento Tributario
            RetornoDados = OBJCliente.CarregaEnquadramentoTributario();
            EnquadramentoTributarioDropDownList.DataSource = RetornoDados;
            EnquadramentoTributarioDropDownList.DataValueField = "DescricaoEnquadramentoTriburario";
            EnquadramentoTributarioDropDownList.DataTextField = "DescricaoEnquadramentoTriburario";
            EnquadramentoTributarioDropDownList.DataBind();
            EnquadramentoTributarioDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

        }

        public void CarregaDadosNaTela()
        {


            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();

            IDCliente.Value = OBJCliente.IDCliente.ToString();
            if (OBJCliente.CodigoCliente != "")
            {
                CodigoClienteTextBox.Text = OBJCliente.CodigoCliente;
            }
            else
            {
                CodigoClienteTextBox.Text = OBJCliente.IDCliente.ToString();
            }
            NomeClienteTextBox.Text = OBJCliente.NomeCliente;

            NaturezaJuridicaDropDownList.SelectedValue = OBJCliente.IDNatureza.ToString();
            IndicadorIEDropDownList.SelectedValue = OBJCliente.IndicadorIndIEDest;
            OperadorConsumidorDropDownList.SelectedValue = OBJCliente.IndicadorOpConsumidor;
            IndicadorNaturezaDropDownList.SelectedValue = OBJCliente.IndicadorNatureza;
            EnquadramentoTributarioDropDownList.SelectedValue = OBJCliente.EnquadramentoTributario;
            SimplesNacionalDropDownList.SelectedValue = OBJCliente.SimplesNacional;
            CartaIPIDropDownList.SelectedValue = OBJCliente.CartaIPI;

            if (OBJCliente.DataRecebimentoCartaIPI.ToString() != "")
            {
                RecebimentoCartaTextBox.Text = Convert.ToDateTime(OBJCliente.DataRecebimentoCartaIPI).ToString("yyyy-MM-dd");
            }

            ProdutorRuralDropDownList.SelectedValue = OBJCliente.ProdutorRural;
            CPOMDropDownList.SelectedValue = OBJCliente.CPOM;


            #region Natureza Destinacao

            if (OBJCliente.ListaCrmClienteNaturezaDestinacaoClass != null)
            {

                foreach (CrmClienteNaturezaDestinacaoClass CND in OBJCliente.ListaCrmClienteNaturezaDestinacaoClass)
                {

                    foreach (ListItem li in IDNaturezaDestinacaoCheckBoxList.Items)
                    {

                        if (CND.IDNaturezaDestinacao.ToString() == li.Value)
                        {
                            li.Selected = true;
                        }

                    }

                }

            }



            #endregion



            //Atualiza dados do GRID
            AtualizaGrid();

        }


        public void CarregaDadosDaTela()
        {

            OBJCliente.CodigoUsuario = Session["usuario"].ToString();
            OBJCliente.IDNatureza = Convert.ToInt32(NaturezaJuridicaDropDownList.SelectedValue);
            OBJCliente.IndicadorIndIEDest = IndicadorIEDropDownList.SelectedValue;
            OBJCliente.IndicadorOpConsumidor = OperadorConsumidorDropDownList.SelectedValue;
            OBJCliente.IndicadorNatureza = IndicadorNaturezaDropDownList.SelectedValue;
            OBJCliente.EnquadramentoTributario = EnquadramentoTributarioDropDownList.SelectedValue.Replace("//", "/");
            OBJCliente.SimplesNacional = SimplesNacionalDropDownList.SelectedValue;
            OBJCliente.CartaIPI = CartaIPIDropDownList.SelectedValue;

            if (RecebimentoCartaTextBox.Text != "")
            {
                OBJCliente.DataRecebimentoCartaIPI = Convert.ToDateTime(RecebimentoCartaTextBox.Text);
            }

            OBJCliente.ProdutorRural = ProdutorRuralDropDownList.SelectedValue;
            OBJCliente.CPOM = CPOMDropDownList.SelectedValue;

            if (IDCNAEDropDownList.SelectedValue != "")
            {
                OBJCliente.IDCNAE = Convert.ToInt32(IDCNAEDropDownList.SelectedValue);
            }

            OBJCliente.CNPJ = CNPJTextBox.Text;
            OBJCliente.InscricaoEstadual = InscricaoEstadualTextBox.Text;
            OBJCliente.Suframa = SuframaTextBox.Text;


            #region Natureza Destinacao


            CrmClienteNaturezaDestinacaoClass ObjCrmClienteNaturezaDestinacaoClassAux = new CrmClienteNaturezaDestinacaoClass();

            OBJCliente.ListaCrmClienteNaturezaDestinacaoClass = new List<CrmClienteNaturezaDestinacaoClass>();

            foreach (ListItem li in IDNaturezaDestinacaoCheckBoxList.Items)
            {
                if (li.Selected == true)
                {
                    ObjCrmClienteNaturezaDestinacaoClassAux = new CrmClienteNaturezaDestinacaoClass();
                    ObjCrmClienteNaturezaDestinacaoClassAux.IDNaturezaDestinacao = Convert.ToInt32(li.Value);
                    ObjCrmClienteNaturezaDestinacaoClassAux.IDCliente = OBJCliente.IDCliente;

                    OBJCliente.ListaCrmClienteNaturezaDestinacaoClass.Add(ObjCrmClienteNaturezaDestinacaoClassAux);
                }
            }


            #endregion


        }




        public void AtualizaGrid()
        {
            DataTable retornoDados = new DataTable();


            retornoDados = OBJCliente.CarregaClienteCNAE();

            FiscalGridView.DataSource = retornoDados;
            FiscalGridView.DataBind();



            //Carrega Informações nos Campos da Tela para Fixar 1 Opção
            /*
             Essa função só existe pois atualmente só se pode add 1 CNAE.
             Caso seja liberado mais de um ela pode ser removida.
             */
            FiscalMultiView.Visible = false; //Ocultado GRID, Por enquanto n deve ser mostrado devido a regra 
            if (retornoDados.Rows.Count > 0)
            {
                int ContCNAE = 0;
                foreach (DataRow row in retornoDados.Rows)
                {
                    if (ContCNAE == 0)
                    {

                        IDCNAEDropDownList.SelectedValue = row["IDCNAE"].ToString();
                        CNPJTextBox.Text = row["CNPJ"].ToString();
                        InscricaoEstadualTextBox.Text = row["InscricaoEstadual"].ToString();
                        SuframaTextBox.Text = row["Suframa"].ToString();

                        ContCNAE += 1;
                    }
                }
            }

        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {


            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosDaTela();

                if (OBJCliente.CNPJ != "")
                {
                    string ValidacaoCpfCnpj = ObjUtilClass.Valida_CPF_CNPJ_CRM(OBJCliente.CNPJCliente, OBJCliente.IDCliente, "C");

                    if (ValidacaoCpfCnpj != "Valido")
                    {
                        erro = ValidacaoCpfCnpj;
                    }
                }

                if (OBJCliente.ListaCrmClienteNaturezaDestinacaoClass != null)
                {
                    if (OBJCliente.ListaCrmClienteNaturezaDestinacaoClass.Count() == 0)
                    {
                        erro = "Favor selecionar uma Natureza de Destinação!";
                    }
                }
                else
                {
                    erro = "Favor selecionar uma Natureza de Destinação!";
                }



                if (OBJCliente.CartaIPI == "Sim")
                {
                    if (OBJCliente.DataRecebimentoCartaIPI == null)
                    {
                        erro = "Favor informar a Data de Carta.";
                    }
                }





                if (erro == "")
                {
                    erro = OBJCliente.gravaDadosClienteFiscal();

                }

            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }


            if (erro == "")
            {

                TrataAlteracaoStatusAnalise();

                if (OBJCliente.IDCNAE > 0)
                {
                    LimpaCampos();
                    CarregaDadosDaTela();
                    AtualizaGrid();

                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Dados Atualizados e CNAE Incluido com Sucesso!", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                }
                else
                {
                    LimpaCampos();
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Dados Atualizados com Sucesso!", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }

            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

            }


        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosDaTela();

                AtualizaGrid();
            }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroClienteWebForm.aspx?indmnu=2");
        }


        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                OBJCliente.IDCNAE = Convert.ToInt32(((Label)((Control)sender).FindControl("IDCNAELabel")).Text);

                erro = OBJCliente.ExcluiDadosClienteCNAE();
            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }


            if (erro == "")
            {
                TrataAlteracaoStatusAnalise();
                LimpaCampos();
                CarregaDadosDaTela();
                AtualizaGrid();

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("CNAE Deletado com Sucesso!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();


            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

            }
        }


        public void LimpaCampos()
        {
            IDCNAEDropDownList.SelectedValue = "";
            CNPJTextBox.Text = "";
            InscricaoEstadualTextBox.Text = "";
            SuframaTextBox.Text = "";
        }



        public void TrataAcesso()
        {
            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();
            usuario ObjusuarioAux = new usuario();
            ObjusuarioAux = new usuario();
            ObjusuarioAux.CodigoUsuario = Session["usuario"].ToString();
            ObjusuarioAux.ConsultaGrupos("Ativo");

            if (OBJCliente.CodigoCliente != "")
            {
                GravarButton.Visible = false;

                //Verifica se usuário tem acesso ao campo gravar natureza destinação
                if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
                {
                    GravarNaturezaDestinacaoButton.Visible = true;
                }
            }
            else
            {

                BuscarButton.Visible = true;

                switch (OBJCliente.IDStatus)
                {
                    case 0: //Novo Cadastro
                        GravarButton.Visible = true;
                        break;

                    case 1: //Status Cliente Prospectivo
                        GravarButton.Visible = true;
                        break;

                    case 2: //Status Cliente Ativo
                        if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
                        {
                            GravarNaturezaDestinacaoButton.Visible = true;
                        }
                        break;

                    case 3: //Status Cliente Inativo
                        GravarButton.Visible = true;
                        FiscalGridView.Columns[0].Visible = true;
                        break;

                    case 4: //Status Cliente Análise Financeira
                        if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
                        {
                            FiscalGridView.Columns[0].Visible = true;
                            GravarButton.Visible = true;
                        }
                        else
                        {
                            FiscalGridView.Columns[0].Visible = false;
                            GravarButton.Visible = false;
                        }


                        break;

                    case 5: //Status Cliente Análise Fiscal
                            //Verifica se esta no Grupo Análise Fiscal
                        if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
                        {
                            FiscalGridView.Columns[0].Visible = true;
                            GravarButton.Visible = true;
                        }
                        else
                        {
                            FiscalGridView.Columns[0].Visible = false;
                            GravarButton.Visible = false;
                        }

                        break;

                    default:
                        GravarButton.Visible = false;
                        FiscalGridView.Columns[0].Visible = false;
                        break;
                }
            }
        }

        public void TrataAlteracaoStatusAnalise()
        {

            OBJCliente.carregaDadosPrincipais();

            switch (OBJCliente.IDStatus)
            {
                case 0: //Novo Cadastro
                    break;
                case 1: //Status Cliente Prospectivo
                    break;
                case 5: //Status Cliente Fiscal
                    break;

                case 4: //Status Cliente Análise Financeira
                    break;

                case 2: //Status Cliente Ativo
                case 3: //Status Cliente Inativo


                    //Enviar Cliente para Analise Fiscal
                    OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                    OBJCliente.IDStatus = 5; //Analise Fiscal
                    OBJCliente.AlteraStatusCliente();
                    break;

                default:
                    break;



            }
        }

        protected void GravarNaturezaDestinacaoButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosDaTela();

                if (OBJCliente.ListaCrmClienteNaturezaDestinacaoClass != null)
                {
                    if (OBJCliente.ListaCrmClienteNaturezaDestinacaoClass.Count() == 0)
                    {
                        erro = "Favor selecionar uma Natureza de Destinação!";
                    }
                }
                else
                {
                    erro = "Favor selecionar uma Natureza de Destinação!";
                }

                if (erro == "")
                {
                    erro = OBJCliente.gravaDadosNaturezaDestinacao();
                }
            }

            if (erro != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Dados Atualizados com Sucesso!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void BloqueiaCamposConsultaSefaz()
        {
            ClienteClasse objClienteClasse = new ClienteClasse();

            objClienteClasse.CodigoUsuario = Session["usuario"].ToString();

            if (objClienteClasse.RetornaBloqueiaCamposConsultaSefaz())
            {
                if (Session["clienteClasse"] != null)
                {
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];

                    OBJCliente.carregaDadosPrincipais();

                    if (OBJCliente.CNPJCliente.Length > 11)
                    {
                        SimplesNacionalDropDownList.Enabled = false;
                        SuframaTextBox.Enabled = false;
                        InscricaoEstadualTextBox.Enabled = false;
                        IDCNAEDropDownList.CssClass = "form-control";
                        IDCNAEDropDownList.Enabled = false;
                        CNPJTextBox.Enabled = false;
                    }
                }
            }

        }

    }
}