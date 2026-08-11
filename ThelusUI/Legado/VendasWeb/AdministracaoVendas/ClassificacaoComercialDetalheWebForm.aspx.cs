using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.AdministracaoVendas
{
    public partial class ClassificacaoComercialDetalheWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse objClienteClasse = new ClienteClasse();
        DataTable ClassificacaoComercialDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            CarregaCombos();

            if (Session["ClassificacaoComercialWebForm"] != null)
            {
                objClienteClasse = (ClienteClasse)Session["ClassificacaoComercialWebForm"];
            }

            ClassificacaoComercialDataTable = objClienteClasse.Carrega_Solicitacao_Classificacao_Comercial();

            if (ClassificacaoComercialDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ClassificacaoComercialDataTable.Rows)
                {
                    ClienteTextBox.Text = row["Cliente"].ToString();
                    CNPJTextBox.Text = row["CNPJ"].ToString();
                    ClassificadoTextBox.Text = row["Situacao"].ToString();
                    DataSolicitacaoTextBox.Text = row["DataSolicitacao"].ToString();
                    DataClassificacaoTextBox.Text = row["DataClassificacao"].ToString();
                    ClassificacaoTextBox.Text = row["Classificacao"].ToString();
                    VendedorTextBox.Text = row["NomeVendedor"].ToString();
                    ClassificacaoDropDownList.SelectedValue = row["IDClassificacaoComercial"].ToString();
                    HistoricoTextBox.Text = row["Historico"].ToString();
                }
            }
        }

        protected void CarregaCombos()
        {
            ClienteClasse objClienteClasseAux = new ClienteClasse();

            ClassificacaoDropDownList.DataSource = objClienteClasseAux.CarregaClassificacaoComercial();
            ClassificacaoDropDownList.DataTextField = "Descricao";
            ClassificacaoDropDownList.DataValueField = "IDClassificacaoComercial";
            ClassificacaoDropDownList.DataBind();
        }

        protected void ClassificarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            ClienteClasse objClienteClasseAux = new ClienteClasse(); ;

            try
            {
                if (ClassificacaoDropDownList.SelectedValue == "0") erro = "Escolha uma classificação comercial.";

                //Carrega dados do json
                if (erro == "")
                {
                    //Carrega CodigoSAP da Classificação Comercial
                    {
                        ClassificacaoComercialDataTable = objClienteClasseAux.CarregaClassificacaoComercial();

                        if (ClassificacaoComercialDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in ClassificacaoComercialDataTable.Rows)
                            {
                                if (ClassificacaoDropDownList.SelectedValue == row["IDClassificacaoComercial"].ToString())
                                {
                                    objClienteClasse.CodigoSAP = Convert.ToInt32(row["CodigoSAP"]);
                                    break;
                                }
                            }
                        }
                    }

                    //Carrega Codigo SAP do Cliente 
                    {
                        objClienteClasseAux = new ClienteClasse();

                        if (Session["ClassificacaoComercialWebForm"] != null)
                        {
                            objClienteClasseAux = (ClienteClasse)Session["ClassificacaoComercialWebForm"];
                        }

                        ClassificacaoComercialDataTable = objClienteClasseAux.Carrega_Solicitacao_Classificacao_Comercial();

                        if (ClassificacaoComercialDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in ClassificacaoComercialDataTable.Rows)
                            {
                                objClienteClasse.CodigoClienteSAP = row["CodigoClienteSAP"].ToString();
                                break;
                            }
                        }
                    }
                }

                if (erro == "") erro = objClienteClasse.AtualizaClassificacaoComercial();

                objClienteClasse.IDCliente = objClienteClasseAux.IDCliente;

                objClienteClasse.IDSolicitacao = objClienteClasseAux.IDSolicitacao;

                objClienteClasse.IDUsuario = 0;

                objClienteClasse.IDClassificacaoComercial = Convert.ToInt32(ClassificacaoDropDownList.SelectedValue);

                objClienteClasse.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);

                if (erro == "") erro = objClienteClasse.Grava_Solicitacao_Classificacao_Comercial();

                string classificacao = ClassificacaoDropDownList.SelectedItem.ToString();

                if (erro == "") erro = GravaHistoricoCliente(classificacao);

                if (erro == "") erro = EnviaEmailSolicitacao(classificacao);

                if (erro == "") erro = MudaStatusCliente();

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            ApresentaMensagem(erro);
        }

        protected string MudaStatusCliente()
        {            
            objClienteClasse.CodigoUsuario = Session["usuario"].ToString();            
            objClienteClasse.CarregaClienteTipoSolicitacaoStatus("Ativo");

            return objClienteClasse.AlteraStatusCliente();
        }

        protected string GravaHistoricoCliente(string classificacao)
        {
            try
            {
                HistoricosClass objHistorico = new HistoricosClass();

                objHistorico.IDCliente = objClienteClasse.IDCliente;
                objHistorico.IDTipoHistorico = 1;
                objHistorico.IDEvento = 8;
                objHistorico.IDCategoria = 1;
                objHistorico.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
                objHistorico.Historico = "Cliente classificado como " + classificacao;

                return objHistorico.GravaHistoricoCliente();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected string EnviaEmailSolicitacao(string classificacao)
        {
            try
            {
                enviarEmail OBJMail = new enviarEmail();

                OBJMail.Historico = "Cliente classificado como " + classificacao;

                ClienteClasse objClienteClasseAux = new ClienteClasse();

                usuario objUsuario = new usuario();

                ClassificacaoComercialDataTable = objClienteClasse.Carrega_Solicitacao_Classificacao_Comercial();

                if (ClassificacaoComercialDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in ClassificacaoComercialDataTable.Rows)
                    {
                        objUsuario.IDUsuario = Convert.ToInt32(row["IDUsuario"]);

                        OBJMail.FormataTextoClassificacaoComercial(row["Cliente"].ToString(), row["CNPJ"].ToString(),
                            row["DataSolicitacao"].ToString(), ClassificacaoTextBox.Text, row["NomeVendedor"].ToString());
                    }
                }

                OBJMail.TituloEmail = "Classificação comercial";

                OBJMail.EmailDestinatario = objUsuario.RecuperaEmailUsuario();

                OBJMail.enviaEmailFormatado();

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
                Session["Msg"] = "Operação realizada com sucesso.";
                RetornarButton_Click(null, null);
            }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/ClassificacaoComercialWebForm.aspx?indmnu=3");
        }
    }
}