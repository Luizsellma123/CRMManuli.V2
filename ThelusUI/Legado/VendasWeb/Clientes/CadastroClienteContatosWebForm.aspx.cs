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
    public partial class CadastroClienteContatosWebForm : System.Web.UI.Page
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
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"false\">";


                if (Session["clienteClasse"] != null)
                {
                    //Descarega a session da Entidade
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];

                    //Carrega dados
                    CarregaDadosNaTela();

                    TrataAcesso();

                }

            }

        }

        public void CarregaDadosNaTela()
        {
            DataTable RetornoDados = new DataTable();

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
            OBJCliente.TipoContato = TipoContatoDropDownList.SelectedItem.Value;
            OBJCliente.NomeContato = ContatoTextBox.Text;
            OBJCliente.TelefoneContato = TelefoneTextBox.Text;
            OBJCliente.EmailContato = EmailTextBox.Text;

        }


        public void AtualizaGrid()
        {
            DataTable retornoDados = new DataTable();

            retornoDados = OBJCliente.CarregaContatosCliente();

            ClienteContatosGridView.DataSource = retornoDados;
            ClienteContatosGridView.DataBind();
            ClientesContatosMultiView.Visible = true;
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosDaTela();


                erro = OBJCliente.gravaDadosClienteContatos();


            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }

            if (erro == "")
            {

                LimpaCampos();
                CarregaDadosDaTela();
                AtualizaGrid();


                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Contato Incluido com Sucesso!", true);
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

            TipoContatoDropDownList.SelectedValue = "";
            ContatoTextBox.Text = "";
            TelefoneTextBox.Text = "";
            EmailTextBox.Text = "";

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

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                OBJCliente.IDContato = ((Label)((Control)sender).FindControl("IDContatoLabel")).Text;

                erro = OBJCliente.ExcluiDadosClienteContatos();


            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }



            if (erro == "")
            {
                LimpaCampos();
                CarregaDadosDaTela();
                AtualizaGrid();

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Contato Deletado com Sucesso!", true);
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



        public void TrataAcesso()
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
                    ClienteContatosGridView.Columns[0].Visible = true;
                    break;

                case 4: //Status Cliente Análise Financeira
                        //Verifica se esta no Grupo Análise Financeira
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
                    {
                        ClienteContatosGridView.Columns[0].Visible = true;
                        GravarButton.Visible = true;
                    }
                    else
                    {
                        ClienteContatosGridView.Columns[0].Visible = false;
                        GravarButton.Visible = false;
                    }

                    break;

                case 5: //Status Cliente Análise Fiscal
                    //Verifica se esta no Grupo Análise Fiscal
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
                    {
                        ClienteContatosGridView.Columns[0].Visible = true;
                        GravarButton.Visible = true;
                    }
                    else
                    {
                        ClienteContatosGridView.Columns[0].Visible = false;
                        GravarButton.Visible = false;
                    }

                    break;

                default:
                    GravarButton.Visible = false;
                    ClienteContatosGridView.Columns[0].Visible = false;
                    break;
            }
        }
    }
}