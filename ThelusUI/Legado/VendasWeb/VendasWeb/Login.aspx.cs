using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb
{
    public partial class Login : System.Web.UI.Page
    {

        funcoes mdlfuncoes = new funcoes();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                
                if (
                    ((url.ToUpper().Contains("177") == true) || (url.ToUpper().Contains("192") == true)) && (url.ToUpper().Contains("CRMMANULIDESENVOLVIMENTO") == false)
                 )
                {
                    Response.Redirect("acesso.aspx");
                }
            }
                
               

            
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (UsuarioTextBox.Text.Trim() != "")
            {
                if (Page.IsValid)
                {


                    //Response.Redirect("Home.aspx");
                    string controleAcesso = ValidaUsuario();

                    if (controleAcesso != "")
                    {
                        lblError.Visible = true;
                        lblError.Text = controleAcesso;
                        Session["idLogin"] = 0;
                    }
                    else
                    {
                        Session["idLogin"] = 1;

                        Response.Redirect("Home.aspx?indmnu=1");
                        //Response.Redirect("Entidade/Financeiro.aspx?indmnu=5");
                    }
                }
            }
        }

        public string ValidaUsuario()
        {
            string msgErro = "";
            int retUsuario;

            string sql = "Select count(*) as CNT from USUARIO where usucod ='" + UsuarioTextBox.Text.Trim() + "' and UsuSenhaInternet='" + SenhaTextBox.Text.ToString() + "' and UsuStat like 'Ativ%'";

            retUsuario = int.Parse(mdlfuncoes.ExecutaSqlReader(sql, "ValidaUsuario").ToString());

            if (retUsuario == 0)
            {
                msgErro = validaStatus();
            }
            else
            {
                msgErro = validaStatus();
            }

            if (retUsuario > 0)
            {
                sql = "select sum(CNT) as cont from (select COUNT(*) as CNT from GRP_X_USUARIO where GrpUsuCod ";
                sql += "like '%Vendas_GER%' and GrpUsuSuperv='T' and UsuCod = '" + UsuarioTextBox.Text.Trim() + "' ";
                sql += "union select COUNT(*) as CNT from USUARIO where UsuCod = '" + UsuarioTextBox.Text.Trim() + "' and UsuAdmin = 'T' ) as a";

                Session["nivel"] = mdlfuncoes.ExecutaSqlReader(sql, "ValidaUsuario").ToString();

                if ((string)Session["nivel"].ToString() == "" || (string)Session["nivel"].ToString() == "0" || Session["nivel"] == null)
                {
                    Session["nivel"] = mdlfuncoes.ExecutaSqlReader(sql, "ValidaUsuario").ToString();
                }

                msgErro = "";
            }
            return msgErro;
        }

        public string validaStatus()
        {
            string sql = "";
            string UsuStat = "";

            sql = "Select UsuStat from USUARIO where usucod ='" + UsuarioTextBox.Text.Trim() + "' and UsuSenhaInternet='" + SenhaTextBox.Text.ToString() + "'";
            UsuStat = mdlfuncoes.ExecutaSqlReader(sql, "validaStatus").ToString();

            if (UsuStat == "Ativo")
            {
                sql = "Select UsuCod from USUARIO where usucod ='" + UsuarioTextBox.Text.Trim() + "'";
                Session["usuario"] = mdlfuncoes.ExecutaSqlReader(sql, "validaStatus").ToString();
                return "";
            }
            else
            {
                if (UsuStat == "Desligado")
                {
                    return "Usuario Desativado";
                }
                else
                {
                    return "Usuario ou Senha Invalida";
                }
            }
        }


    }
}