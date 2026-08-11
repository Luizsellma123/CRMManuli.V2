using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb
{
    public partial class CRM : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal();", true);

            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    string UsuarioSession;
                    int IDUsuarioSession;
                    int IDLoginSession;

                    NomeUsuarioMasterLabel.Text = Session["usuario"].ToString();

                    //Tratativa para não cair a sessão do usuário exceto quando por mais de 1 hora de inatividade
                    UsuarioSession = Session["usuario"].ToString();
                    IDLoginSession = Convert.ToUInt16(Session["idLogin"].ToString());
                    IDUsuarioSession = Convert.ToUInt16(Session["IDUsuario"].ToString());

                }else
                {
                    //Verifica se tem usuário logado
                    ReadCookies();
                }
            }            
        }

        public void ReadCookies()
        {
            foreach (var cookie in Request.Cookies)
            {
                //Recupera usuario
                if (cookie.Equals("usuario"))
                {
                    Session["usuario"] = Request.Cookies[cookie.ToString()].Value;
                }

                //Recupera IDusuario
                if (cookie.Equals("IDUsuario"))
                {
                    Session["IDUsuario"] = Request.Cookies[cookie.ToString()].Value;
                }

                //Recupera idLogin
                if (cookie.Equals("idLogin"))
                {
                    Session["idLogin"] = Request.Cookies[cookie.ToString()].Value;
                }
            }
        }
    }
}