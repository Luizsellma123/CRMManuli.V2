using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VendasWeb.GerencialVendas;
using System.Web.Services;
using VendasWeb.classes;

namespace VendasWeb
{
    public partial class Home : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsBanner ObjBanner = new clsBanner();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                //ListaBanners();
            }
            {
                //string dolar = "";
                //string euro = "";
                //BancoBrasil.FachadaWSSGSClient wsbc = new BancoBrasil.FachadaWSSGSClient();

                //dolar
                //var consulta = wsbc.getUltimoValorVO(1);
                //dolar = consulta.ultimoValor.svalor;
                //LabelDolar.Text = dolar;

                //Euro
                //consulta = wsbc.getUltimoValorVO(21619);
                //euro = consulta.ultimoValor.svalor;
                //LabelEuro.Text = euro;

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