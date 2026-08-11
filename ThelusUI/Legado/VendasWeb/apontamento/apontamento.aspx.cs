using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.apontamento
{
    public partial class apontamento : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsApontamento novoApontamento;
        funcoes mdlfuncoes = new funcoes();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se usuário esta logado
            int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);

            //Valida Acesso
            OBJSessao.ValidaAcesso();


            if (!IsPostBack)
            {
                btnSalvar.Attributes.Add("onclick", "javascript:return validaCampos();");

                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.DataBind();

                FuncCodDelet.Value = "0";
                //Recebe o Funcionario caso tenha sido selacionado na Tela de Lista de Funcionario
                string FuncCod = Request.QueryString["FuncCod"];
                string FuncNome = Request.QueryString["FuncNome"];
                if (FuncCod != "" && FuncCod != null)
                {
                    if (Session["Apontamento"] != null)
                    {
                        novoApontamento = (clsApontamento)Session["Apontamento"];
                        novoApontamento.addFunc(FuncCod, FuncNome);
                        carregaDadosNaTela();
                        ltlItems.Text = novoApontamento.carregaFunc();
                    }
                }
                else
                {
                    novoApontamento = new clsApontamento();
                    novoApontamento.usuCod = Session["usuario"].ToString();
                    txtDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtHoraFinal.Text = DateTime.Now.ToString("HH:mm");
                    txtHoraInicial.Text = DateTime.Now.ToString("HH:mm");
                }
            }
            else
            {
                if (FuncCodDelet.Value != "0")
                {
                    if (Session["Apontamento"] != null)
                    {
                        novoApontamento = (clsApontamento)Session["Apontamento"];
                        novoApontamento.removeFunc(FuncCodDelet.Value);
                        FuncCodDelet.Value = "0";
                        carregaDadosNaTela();
                        ltlItems.Text = novoApontamento.carregaFunc();
                    }
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (carregaDadosObjeto())
            {
                float qtdBoa = Convert.ToInt32(txtQtdBoa.Text);
                float qtdRefugada = Convert.ToInt32(txtQtdRefugada.Text);
                float qtdReprocesso = Convert.ToInt32(txtQtdReprocesso.Text);
                float qtdRetalho = Convert.ToInt32(txtQtdRetalho.Text);

                string msg = "";
                int contErro = 0;

                /*Soma a Quantidade Total*/
                novoApontamento.QUANTIDADETOTAL = qtdBoa + qtdRefugada + qtdReprocesso;


                //Verifica se Existe ao menos 1 funcionario Selecionado
                if (novoApontamento.Listafuncionario.Count > 0)
                {
                    //Consulta Codigo OP
                    msg = novoApontamento.geraCodigoOrdemOP();
                    if (msg == "")
                    {
                        //Salva OP
                        novoApontamento.salvarOrdProduc();

                        //Enquanto Tiver Funcionario na OP insere no Apontamento
                        for (int i = 0; i < novoApontamento.Listafuncionario.Count; i++)
                        {
                            msg = novoApontamento.salvarOrdProducFunc(i);
                        }

                        if (msg == "")
                        {
                            //Insere Produto no Apontamento
                            if (novoApontamento.InserirOperOrdProducProc())
                            {
                                if (novoApontamento.BaixaReqMatOper() == false)
                                {
                                    msg = "Erro Funcao BaixaReqMatOper";
                                    contErro = 1;
                                }
                            }
                            else
                            {
                                msg = "Erro Funcao InserirOperOrdProducProc";
                                contErro = 1;
                            }
                        }
                        else
                        {
                            contErro = 1;
                        }

                        //Limpa Tale
                        Session.Remove("Apontamento");
                        limpaCampoTela();
                    }
                    else
                    {
                        contErro = 1;
                    }
                }
                else
                {
                    Response.Write("<script>alert(\"Selecione ao menos 1 Funcionario!\");</script>");
                }

                if (contErro != 0)
                {
                    Response.Write("<script>alert(\"" + msg + "\");</script>");
                }

                //novoApontamento.validaQuantidadeOP();
            }
        }

        protected void txtOrdemProducao_TextChanged(object sender, EventArgs e)
        {
            if (novoApontamento == null)
            {
                novoApontamento = new clsApontamento();
            }

            string Empresa = drpEmpresa.SelectedValue;
            string numeroOrdemOp = txtOrdemProducao.Text;

            novoApontamento.EmpCod = Empresa;
            novoApontamento.OrdProducNum = numeroOrdemOp;

            novoApontamento.horaInicial = txtHoraInicial.Text;
            novoApontamento.horaFinal = txtHoraFinal.Text;
            novoApontamento.dataInicial = txtDataInicial.Text;
            novoApontamento.dataFinal = txtDataFinal.Text;

            //Consulta Ordem de Producao
            if (novoApontamento.consultaDadosOrdemProducao())
            {
                carregaDadosNaTela();
            }
            else
            {
                //Caso nao Localize envia alerta de erro
                Response.Write("<script>alert(\"Dados Complementares nao Localizado não Localizada!\");</script>");
            }

        }

        public void carregaDadosNaTela()
        {
            txtOrdemProducao.Text = novoApontamento.OrdProducNum;
            txtProduto.Text = novoApontamento.ProdCodEstr;
            txtPlanejamento.Text = novoApontamento.PlanProducNum;
            txtSequencia.Text = novoApontamento.ProdOperSeq;
            txtOperacao.Text = novoApontamento.OperCod;
            txtQtdNecessaria.Text = novoApontamento.qtdNecessaria;
            txtAtividade.Text = novoApontamento.AtivGrpCodEstr;
            lblAtividadeText.Text = novoApontamento.AtivGrpNome;
            txtCentroControle.Text = novoApontamento.CCtrlCodEstr;
            lblCentroControleText.Text = novoApontamento.CCtrlNome;
            txtHoraInicial.Text = novoApontamento.horaInicial;
            txtHoraFinal.Text = novoApontamento.horaFinal;
            txtDataInicial.Text = novoApontamento.dataInicial;
            txtDataFinal.Text = novoApontamento.dataFinal;

            txtQtdBoa.Text = novoApontamento.boa.ToString();
            txtQtdRefugada.Text = novoApontamento.Refugada.ToString();
            txtQtdReprocesso.Text = novoApontamento.Reprocesso.ToString();
            txtQtdRetalho.Text = novoApontamento.Retalho.ToString();

            drpStatus.SelectedValue = novoApontamento.ORDPRODUCSTAT;
            drpTipoOperacao.SelectedValue = novoApontamento.tipoOperacao;
            drpEmpresa.SelectedValue = novoApontamento.EmpCod;

            Session["Apontamento"] = novoApontamento;
        }

        public bool carregaDadosObjeto()
        {
            if (novoApontamento == null)
            {
                if (Session["Apontamento"] != null)
                {
                    novoApontamento = (clsApontamento)Session["Apontamento"];
                }
            }

            if (novoApontamento != null)
            {
                novoApontamento.usuCod = Session["usuario"].ToString();
                novoApontamento.OrdProducNum = txtOrdemProducao.Text;
                novoApontamento.ProdCodEstr = txtProduto.Text;
                novoApontamento.PlanProducNum = txtPlanejamento.Text;
                novoApontamento.ProdOperSeq = txtSequencia.Text;
                novoApontamento.OperCod = txtOperacao.Text;
                novoApontamento.qtdNecessaria = txtQtdNecessaria.Text;
                novoApontamento.AtivGrpCodEstr = txtAtividade.Text;
                novoApontamento.AtivGrpNome = lblAtividadeText.Text;
                novoApontamento.CCtrlCodEstr = txtCentroControle.Text;
                novoApontamento.CCtrlNome = lblCentroControleText.Text;
                novoApontamento.horaInicial = txtHoraInicial.Text;
                novoApontamento.horaFinal = txtHoraFinal.Text;
                novoApontamento.dataInicial = txtDataInicial.Text;
                novoApontamento.dataFinal = txtDataFinal.Text;

                novoApontamento.boa = Convert.ToInt32(txtQtdBoa.Text);
                novoApontamento.Refugada = Convert.ToInt32(txtQtdRefugada.Text);
                novoApontamento.Reprocesso = Convert.ToInt32(txtQtdReprocesso.Text);
                novoApontamento.Retalho = Convert.ToInt32(txtQtdRetalho.Text);

                novoApontamento.ORDPRODUCSTAT = drpStatus.SelectedValue;
                novoApontamento.tipoOperacao = drpTipoOperacao.SelectedValue;
                novoApontamento.EmpCod = drpEmpresa.SelectedValue;

                Session["Apontamento"] = novoApontamento;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            carregaDadosObjeto();
            Response.Redirect("../listas/lstFuncionario.aspx?indmnu=3");
        }

        public void limpaCampoTela()
        {
            txtOrdemProducao.Text = "";
            txtProduto.Text = "";
            txtPlanejamento.Text = "";
            txtSequencia.Text = "";
            txtOperacao.Text = "";
            txtQtdNecessaria.Text = "";
            txtAtividade.Text = "";
            lblAtividadeText.Text = "";
            txtCentroControle.Text = "";
            lblCentroControleText.Text = "";
            txtDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtHoraFinal.Text = DateTime.Now.ToString("HH:mm");
            txtHoraInicial.Text = DateTime.Now.ToString("HH:mm");

            txtQtdBoa.Text = "0";
            txtQtdRefugada.Text = "0";
            txtQtdReprocesso.Text = "0";
            txtQtdRetalho.Text = "0";
            ltlItems.Text = "";
        }

        protected void tbnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("Apontamento");
            limpaCampoTela();
        }
    }
}