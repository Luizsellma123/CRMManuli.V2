using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace VendasWeb.PortalClienteManuli.contato
{
    public partial class ContatoWebForm : System.Web.UI.Page
    {
        enviarEmailClass OBJEmail = new enviarEmailClass();
        UtilClass ObjUtilClass = new UtilClass();
        PortalClass OBJPortal = new PortalClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            if (!IsPostBack)
            {
                //Carrega dados na tela
                carregaDados();
            }
        }

        protected void EnviarButton_Click(object sender, EventArgs e)
        {
            string descricao = "";
            string cliente = "";
            string FileExtension = "";

            try
            {
                if (file_upload.HasFile)
                {
                    FileExtension = Path.GetExtension(file_upload.PostedFile.FileName).Substring(1);
                }

                descricao = descricaoTextBox.Text.ToString();
                Attachment anexar = new Attachment(file_upload.PostedFile.InputStream, AttachmentTextBox.Text.ToString() + "." + FileExtension);
                cliente = ((Label)((UserControl)Master.FindControl("PortalClienteMenuWebUserControl")).FindControl("Labelnome")).Text;

                OBJEmail.ContatoClienteEmail(TituloTextBox.Text, descricao, cliente, TipoContatoDropDownList.SelectedValue.ToString(), anexar);

                string FaltaValores = "Solicitação encaminhada com sucesso !";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            catch (Exception ex)
            {
                string FaltaValores = "Ocorreu um problema na solicitação !";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        public void carregaDados()
        {
            //Carrega Contatos
            OBJPortal.NomeContato = "";
            TipoContatoDropDownList.DataSource = OBJPortal.ContatosPortal();
            TipoContatoDropDownList.DataTextField = "NomeContato";
            TipoContatoDropDownList.DataValueField = "Email";
            TipoContatoDropDownList.DataBind();
        }
    }
}