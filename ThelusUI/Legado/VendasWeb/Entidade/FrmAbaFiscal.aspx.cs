using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using VendasWeb.classes;

namespace VendasWeb.Entidade
{
    public partial class FrmAbaFiscal : System.Web.UI.Page
    {
        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        usuario ObjUsuarioClass = new usuario();
        criptografia mdlCriptografia = new criptografia();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Valida Acesso
                OBJSessao.ValidaAcesso();

                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                //Carrega Dados na Tela
                CarregaDadosNaTela();

                if (ObjEntidadesClass.TipoOperacao == "Consultar")
                {
                    BloqueiaCampos();
                }


            }
        }


        public string CarregaDadosDaTela()
        {

            #region

            ObjEntidadesClass.EntInscSuframa = SuframaTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.UserTipoTributacao = UserTipoTributacaoDropDownList.SelectedValue;
            ObjEntidadesClass.UserSuspencaoIPI = UserSuspencaoIPIDropDownList.SelectedValue;
            ObjEntidadesClass.UserDiferimentoICMS = UserDiferimentoICMSDropDownList.SelectedValue;
            ObjEntidadesClass.UserDiferimentoPIS = UserDiferimentoPISDropDownList.SelectedValue;
            ObjEntidadesClass.UserDiferimentoCOFINS = UserDiferimentoCOFINSDropDownList.SelectedValue;

            #endregion


            return "";
        }


        public string CarregaDadosNaTela()
        {

            #region

            if (ObjEntidadesClass.UserTipoTributacao != null)
                UserTipoTributacaoDropDownList.SelectedValue = ObjEntidadesClass.UserTipoTributacao;
            if (ObjEntidadesClass.UserSuspencaoIPI != null)
                UserSuspencaoIPIDropDownList.SelectedValue = ObjEntidadesClass.UserSuspencaoIPI;
            if (ObjEntidadesClass.UserDiferimentoICMS != null)
                UserDiferimentoICMSDropDownList.SelectedValue = ObjEntidadesClass.UserDiferimentoICMS;
            if (ObjEntidadesClass.UserDiferimentoPIS != null)
                UserDiferimentoPISDropDownList.SelectedValue = ObjEntidadesClass.UserDiferimentoPIS;
            if (ObjEntidadesClass.UserDiferimentoCOFINS != null)
                UserDiferimentoCOFINSDropDownList.SelectedValue = ObjEntidadesClass.UserDiferimentoCOFINS;
            if (ObjEntidadesClass.EntInscSuframa != null)
                SuframaTextBox.Text = ObjEntidadesClass.EntInscSuframa;

            if (ObjEntidadesClass.CidCod != null)
            {
                if (ObjEntidadesClass.Consulta_Cidade() == "Sim")
                {
                    SuframaMultView.Visible = true;
                }
                else
                {
                    SuframaMultView.Visible = false;
                }
            }

            #endregion

            return "";
        }



        protected void ProximoPasso_Click(object sender, EventArgs e)
        {
            //Descarega a sessao
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


            if (ObjEntidadesClass.TipoOperacao != "Consulta")
            {
                //Carrega os Dados da Tela
                CarregaDadosDaTela();
            }

            //Guarda os dados em Session
            Session["clsEntidades"] = ObjEntidadesClass;


            //Chama a proxima Tela
            Response.Redirect("FrmAbaConcorrencia.aspx?indmnu=2");
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaFinanceiro.aspx?indmnu=2");
        }


        protected void BloqueiaCampos()
        {
            #region
            UserTipoTributacaoDropDownList.Enabled = false;
            UserSuspencaoIPIDropDownList.Enabled = false;
            UserDiferimentoICMSDropDownList.Enabled = false;
            UserDiferimentoPISDropDownList.Enabled = false;
            UserDiferimentoCOFINSDropDownList.Enabled = false;
            SuframaTextBox.Enabled = false;

            
            #endregion
        }

        


    }
}