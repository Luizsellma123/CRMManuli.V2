using System;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Data;


namespace VendasWeb.Infraestrutura
{
    public partial class ProgramasWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        InfraestruturaClass objInfraestruturaClass = new InfraestruturaClass();

        protected void Page_Load(object sender, EventArgs e)
        { //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                AtualizarButton_Click(null, null);

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }
        }

        protected void AtualizarButton_Click(object sender, EventArgs e)
        {
            if (Session["objInfraestruturaClass"] != null)
            {
                objInfraestruturaClass = (InfraestruturaClass)Session["objInfraestruturaClass"];

                DataTable InfraestruturaDataTable = objInfraestruturaClass.CarregaProgramasInstaladosMaquina(NomeProgramaTextBox.Text);

                if (InfraestruturaDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in InfraestruturaDataTable.Rows)
                    {
                        MACTextBox.Text = row["MAC"].ToString();

                        IPTextBox.Text = row["IP"].ToString();

                        NomeTextBox.Text = row["Nome"].ToString();

                        UltimaAtualizacaoTextBox.Text = row["DataUltimaAtualizacao"].ToString();
                    }
                }

                InfraestruturaGridView.DataSource = InfraestruturaDataTable;
                InfraestruturaGridView.DataBind();
                InfraestruturaMultiView.Visible = true;
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/InfoPCWebForm.aspx?indmnu=5");
        }

        protected void InfraestruturaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            InfraestruturaGridView.PageIndex = e.NewPageIndex;
            AtualizarButton_Click(null, null);
        }
    }
}