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
    public partial class CadastroClienteEnderecosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJCliente = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaComboEstado();

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

        public void CarregaComboEstado()
        {

            DataTable RetornoDados = new DataTable();
            ClienteClasse OBJClienteAux = new ClienteClasse();


            //Recupera Estados
            RetornoDados = OBJClienteAux.CarregaEstados();
            EstadoDropDownList.DataSource = RetornoDados;
            EstadoDropDownList.DataValueField = "IDEstado";
            EstadoDropDownList.DataTextField = "Nome";
            EstadoDropDownList.DataBind();
            EstadoDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

            //Fixa Municipios
            MunicipioDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

        }

        public void CarregaComboMunicipio()
        {

            DataTable RetornoDados = new DataTable();
            ClienteClasse OBJClienteAux = new ClienteClasse();


            //Recupera Municipios
            OBJClienteAux.IDEstado = EstadoDropDownList.SelectedValue;
            RetornoDados = OBJClienteAux.CarregaMunicipios();
            MunicipioDropDownList.DataSource = RetornoDados;
            MunicipioDropDownList.DataValueField = "IDMunicipio";
            MunicipioDropDownList.DataTextField = "NomeMunicipio";
            MunicipioDropDownList.DataBind();
            MunicipioDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

        }

        public void CarregaDadosNaTela()
        {

            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();

            IDCliente.Value = OBJCliente.IDCliente.ToString();

            NomeClienteTextBox.Text = OBJCliente.NomeCliente;

            if (OBJCliente.CodigoCliente != "")
            {
                CodigoClienteTextBox.Text = OBJCliente.CodigoCliente;
            }
            else
            {
                CodigoClienteTextBox.Text = OBJCliente.IDCliente.ToString();
            }

            //Atualiza dados do GRID
            AtualizaGrid();

        }

        public void CarregaDadosDaTela()
        {

            OBJCliente.CodigoUsuario = Session["usuario"].ToString();
            OBJCliente.TipoLogradouro = TipoLogradouroTextBox.Text;
            OBJCliente.Rua = NomeRuaTextBox.Text;
            OBJCliente.NumeroRua = NumeroTextBox.Text;
            OBJCliente.Complemento = ComplementoTextBox.Text;
            OBJCliente.CEP = CEPTextBox.Text.Replace("-", "");
            OBJCliente.Bairro = BairroTextBox.Text;
            OBJCliente.Cidade = CidadeTextBox.Text;
            OBJCliente.IDPais = "30";
            OBJCliente.DescricaoEndereco = DescricaoEnderecoDropDownList.SelectedItem.Value;

            OBJCliente.IDEstado = EstadoDropDownList.SelectedItem.Value;
            OBJCliente.IDMunicipio = MunicipioDropDownList.SelectedItem.Value;

        }

        public void AtualizaGrid()
        {
            DataTable retornoDados = new DataTable();

            retornoDados = OBJCliente.CarregaEnderecosCliente();

            ClienteEnderecosGridView.DataSource = retornoDados;
            ClienteEnderecosGridView.DataBind();
            ClientesEnderecosMultiView.Visible = true;
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

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosDaTela();

                erro = OBJCliente.gravaDadosClienteEnderecos();

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


                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Endereço Incluido com Sucesso!", true);
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

            TipoLogradouroTextBox.Text = "";
            NomeRuaTextBox.Text = "";
            NumeroTextBox.Text = "";
            ComplementoTextBox.Text = "";
            CEPTextBox.Text = "";
            BairroTextBox.Text = "";
            CidadeTextBox.Text = "";

            DescricaoEnderecoDropDownList.SelectedValue = "ENTREGA|COBRANÇA";
            EstadoDropDownList.SelectedValue = "";
            MunicipioDropDownList.SelectedValue = "";

        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                OBJCliente.IDEndereco = ((Label)((Control)sender).FindControl("IDEnderecoLabel")).Text;
                erro = OBJCliente.ExcluiDadosClienteEnderecos();

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

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Endereço Deletado com Sucesso!", true);
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

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroClienteWebForm.aspx?indmnu=2");
        }

        protected void EstadoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaComboMunicipio();
        }

        public void TrataAcesso()
        {
            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();

            if (OBJCliente.CodigoCliente != "")
            {
                ClienteEnderecosGridView.Columns[0].Visible = false;
                GravarButton.Visible = false;
            }
            else
            {
                usuario ObjusuarioAux = new usuario();

                ObjusuarioAux = new usuario();
                ObjusuarioAux.CodigoUsuario = Session["usuario"].ToString();
                ObjusuarioAux.ConsultaGrupos("Ativo");

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
                    case 3: //Status Cliente Inativo
                        GravarButton.Visible = true;
                        ClienteEnderecosGridView.Columns[0].Visible = true;
                        break;

                    case 4: //Status Cliente Análise Financeira
                            //Verifica se esta no Grupo Análise Financeira
                        if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
                        {
                            ClienteEnderecosGridView.Columns[0].Visible = true;
                            GravarButton.Visible = true;
                        }
                        else
                        {
                            ClienteEnderecosGridView.Columns[0].Visible = false;
                            GravarButton.Visible = false;
                        }

                        break;

                    case 5: //Status Cliente Análise Fiscal
                            //Verifica se esta no Grupo Análise Fiscal
                        if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
                        {
                            ClienteEnderecosGridView.Columns[0].Visible = true;
                            GravarButton.Visible = true;
                        }
                        else
                        {
                            ClienteEnderecosGridView.Columns[0].Visible = false;
                            GravarButton.Visible = false;
                        }

                        break;

                    default:
                        GravarButton.Visible = false;
                        ClienteEnderecosGridView.Columns[0].Visible = false;
                        break;
                }
            }
        }

        public void TrataAlteracaoStatusAnalise()
        {

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
                        GravarButton.Enabled = false;
                        this.ClienteEnderecosGridView.Columns[0].Visible = false;
                    }
                }
            }

        }


    }
}