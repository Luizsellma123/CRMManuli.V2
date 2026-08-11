using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace VendasWeb.Banner
{
    public partial class frmBanner : System.Web.UI.Page
    {
        GerencialVendas.clsBanner ObjBanner = new GerencialVendas.clsBanner();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                atualiza_Grid();
            }
        }

        protected void NovoBannerLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmBannerDetalhe.aspx?indmnu=2");
        }

        protected void RemoverButton_Click(object sender, EventArgs e)
        {
            ObjBanner.BannerID = Convert.ToInt32(((Label)((Control)sender).FindControl("BannerIDLabel")).Text);
            ObjBanner.ImageUrl = ((Label)((Control)sender).FindControl("ImageUrlLabel")).Text;


            ObjBanner.Deleta_Banner();


            //Apagnado arquivo do Diretorio
            string CaminhoLocal = Server.MapPath("~") + @"\Banner\" + ObjBanner.ImageUrl;
            FileInfo fi = new System.IO.FileInfo(CaminhoLocal);
            try
            {
                fi.Delete();
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }



            atualiza_Grid();

        }

        protected void ImpressionsTextBox_TextChanged(object sender, EventArgs e)
        {
            ObjBanner.BannerID = Convert.ToInt32(((Label)((Control)sender).FindControl("BannerIDLabel")).Text);
            ObjBanner.Impressions = Convert.ToInt32(((TextBox)((Control)sender).FindControl("ImpressionsTextBox")).Text);
            ObjBanner.Atualiza_Sequencia_Banner();
        }

        public void atualiza_Grid()
        {
            BannerGridView.DataSource = ObjBanner.Mostra_Banner();
            BannerGridView.DataBind();
        }
    }
}