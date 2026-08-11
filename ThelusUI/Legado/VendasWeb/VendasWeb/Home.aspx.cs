using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VendasWeb.GerencialVendas;

namespace VendasWeb
{
    public partial class Home : System.Web.UI.Page
    {

        clsBanner ObjBanner = new clsBanner();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se usuário esta logado
            int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);
            if (Session["usuario"] == null && varmenu != 0 && varmenu < 99)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }


            if (!IsPostBack)
            {

                //ListaBanners();
            }
                   
        }




        public void ListaBanners()
        {/*
            int cout = 0;
            BannerLiteral.Text = "";
            IndicadoresLiteral.Text = "";


            #region Banner
            DataTable TabBanner = ObjBanner.Mostra_Banner();


            foreach (DataRow linha in TabBanner.Rows)
            {
                if (cout == 0)
                {
                    BannerLiteral.Text += "  <div class=\"item active\">";
                    IndicadoresLiteral.Text += " <li data-target=\"#myCarousel\" data-slide-to=\"0\" class=\"active\"></li>";
                    cout++;
                }
                else
                {
                    BannerLiteral.Text += "  <div class=\"item\">";
                    IndicadoresLiteral.Text += " <li data-target=\"#myCarousel\" data-slide-to=\"" + linha["Impressions"].ToString() + "\">";
                }


                BannerLiteral.Text += "  <a href=\"" + linha["NavigateUrl"].ToString() + "\"><img src=\"Banner\\" + linha["ImageUrl"].ToString() + "\" alt=\" " + linha["AlternateText"].ToString() + "\" /></a> ";
                BannerLiteral.Text += "  </div>";





            }




            #endregion


        */}
    }
}