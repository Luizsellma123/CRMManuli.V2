using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace VendasWeb.Banner
{
    public partial class FrmBannerDetalhe : System.Web.UI.Page
    {

        protected byte[] Bannerimgbyte;


        funcoes ObjFuncoes = new funcoes();
        GerencialVendas.clsBanner ObjBanner = new GerencialVendas.clsBanner();


        protected void Page_Load(object sender, EventArgs e)
        {
            BannerValidaLabel.Visible = false;


        }

        protected void CarregarBannerButton_Click(object sender, EventArgs e)
        {
            BannerValidaLabel.Visible = false;

            #region CarregaImagen
            if (BannerFileUpload.HasFile)
            {
                //Pegamos informação do arquivo
                string stipoArquivo = Path.GetExtension(BannerFileUpload.PostedFile.FileName).ToLower();

                switch (stipoArquivo)
                {
                    case ".png":
                    case ".gif":
                    case ".jpg":
                    case ".jpeg":
                    case ".jpe":
                        try
                        {

                            string[] arquivos = Directory.GetFiles(Server.MapPath("~") + "\\Banner\\Imagens\\", BannerFileUpload.FileName);

                            if (arquivos.Count() == 0)
                            {

                                BannerFileUpload.SaveAs(Server.MapPath("~") + "\\Banner\\Imagens\\" + BannerFileUpload.FileName);

                                ObjBanner.ImageUrl = "Imagens\\" + BannerFileUpload.FileName;
                                ObjBanner.NavigateUrl = NavigateUrlTextBox.Text;
                                ObjBanner.AlternateText = AlternateTextBox.Text;
                                if (ImpressionsTextBox.Text != "")
                                {
                                    ObjBanner.Impressions = Convert.ToInt32(ImpressionsTextBox.Text);
                                }
                                else
                                {
                                    ObjBanner.Impressions = 0;
                                }

                                ObjBanner.Ativo = AtivoCheckBox.Checked;

                                //Salvando Banner
                                ObjBanner.Insere_Banner();


                                Response.Redirect("FrmBanner.aspx?indmnu=2");


                            }
                            else
                            {
                                BannerValidaLabel.Text = "Ja existe um arquivo chamado: " + BannerFileUpload.FileName;
                                BannerValidaLabel.ForeColor = System.Drawing.Color.Red;
                                BannerValidaLabel.Visible = true;

                            }




                        }
                        catch
                        {
                            BannerValidaLabel.Text = "Erro ao carregar o arquivo: " + BannerFileUpload.FileName;
                            BannerValidaLabel.ForeColor = System.Drawing.Color.Red;
                            BannerValidaLabel.Visible = true;
                        }
                        break;
                    default:
                        BannerValidaLabel.Text = "Erro Tipo de Arquivo invalido. Arquivos Validos: jpg,jpeg,jpe,gif,png";
                        BannerValidaLabel.ForeColor = System.Drawing.Color.Red;
                        BannerValidaLabel.Visible = true;
                        break;
                }

            }
            #endregion CarregaImagen

        }

        protected void CancelarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmBanner.aspx?indmnu=2");
        }


    }
}