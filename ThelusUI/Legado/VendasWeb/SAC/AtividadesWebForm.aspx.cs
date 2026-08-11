using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.SAC
{
    public partial class AtividadesWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();
        setor ObjSetor = new setor();
        grupos objGrupo = new grupos();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

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
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            objGrupo.Status = "";
            objGrupo.Filtro = "Atendimento Cliente";

            CarregaSetor();

            ObjSAC.Tela = "Lista";
            ObjSAC.Filtro = "";
            SituacaoDropDownList.DataSource = ObjSAC.RetornaListaSituacaoTickets();
            SituacaoDropDownList.DataTextField = "Descricao";
            SituacaoDropDownList.DataValueField = "Codigo";
            SituacaoDropDownList.DataBind();
            SituacaoDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            BuscarButton_Click(null, null);
        }

        private void CarregaSetor()
        {
            string Administrador = VerificaAdministrador();

            if (Administrador == "1")
            {
                ObjSetor.Filtro = "";
                ObjSetor.Status = "";
            }
            else
            {
                ObjSetor.Filtro = "AtividadesWebForm";
                ObjSetor.Status = Session["IDUsuario"].ToString();
            }

            SetorDropDownList.DataSource = ObjSetor.ListaSetores();
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataBind();
            SetorDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

        }

        protected string VerificaAdministrador()
        {
            string Administrador = "";

            objGrupo.Status = "";
            objGrupo.Filtro = "Atendimento Cliente";

            DataTable ValidaAcessoDataTable = new DataTable();

            ValidaAcessoDataTable = objGrupo.ListaGrupos();

            if (ValidaAcessoDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ValidaAcessoDataTable.Rows)
                {
                    objGrupo.IDGrupo = Convert.ToInt32(row["IDGrupo"].ToString());
                }
            }

            objGrupo.Filtro = Session["IDUsuario"].ToString();

            ValidaAcessoDataTable = objGrupo.ListaUsuariosGrupos();

            if (ValidaAcessoDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ValidaAcessoDataTable.Rows)
                {
                    Administrador = row["Administrador"].ToString();
                }
            }

            return Administrador;
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            ObjSAC.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue ?? "0");
            ObjSAC.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue ?? "0");

            if (TicketTextBox.Text == "" || TicketTextBox.Text == null) ObjSAC.IDTicket = 0;
            else ObjSAC.IDTicket = Convert.ToInt32(TicketTextBox.Text);

            CarregaClienteDaTela();

            ObjSAC.Solicitante = SolicitanteTextBox.Text ?? "";

            CarregaAtividadeDaTela();

            ObjSAC.IDSituacao = Convert.ToInt32(SituacaoDropDownList.SelectedValue ?? "0");
            ObjSAC.DataInicio = (DataInicioTextBox.Text != "") ? (Convert.ToDateTime(DataInicioTextBox.Text).ToString("yyyy-MM-dd")) : "";
            ObjSAC.DataFim = (DataFimTextBox.Text != "") ? (Convert.ToDateTime(DataFimTextBox.Text).ToString("yyyy-MM-dd")) : "";

            ObjSAC.Administrador = VerificaAdministrador();

            ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());

            ObjSAC.Tela = "Lista";
            SACGridView.DataSource = ObjSAC.RetornaListaAtividades();
            SACGridView.DataBind();
            SACMultiView.Visible = true;
        }

        protected void CarregaAtividadeDaTela()
        {
            ObjSAC.IDAtividade = 0;
            ObjSAC.Atividade = "";

            string erro = "";

            try
            {
                ObjSAC.IDAtividade = Convert.ToInt32(AtividadeTextBox.Text);
            }
            catch
            {
                erro = "erro";
            }

            if (erro == "erro") ObjSAC.Atividade = AtividadeTextBox.Text;
        }

        protected void SelLinkButton_Click(object sender, EventArgs e)
        {
            ObjSAC.Operacao = "Alteracao";
            ObjSAC.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjSAC.IDTicket = Convert.ToInt32(((Label)((Control)sender).FindControl("TicketLabel")).Text);
            ObjSAC.IDAtividade = Convert.ToInt32(((Label)((Control)sender).FindControl("AtividadeLabel")).Text);
            Session["AtividadesDetalhe"] = ObjSAC;
            Response.Redirect("~/SAC/AtividadesDetalheWebForm.aspx?indmnu=5");
        }

        protected void SACGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SACGridView.PageIndex = e.NewPageIndex;
            CarregaDadosNaTela();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/HomeSACWebForm.aspx?indmnu=5");
        }

        protected void CarregaClienteDaTela()
        {
            string erro = "erro";

            ObjSAC.Cliente = "";

            string ClienteAux = ClienteTextBox.Text;

            ClienteAux = ClienteAux.Replace("/", "");

            ClienteAux = ClienteAux.Replace(".", "");

            ClienteAux = ClienteAux.Replace("-", "");

            if (ClienteAux.Length == 14)
            {
                try
                {
                    erro = "";
                    UInt64 teste = Convert.ToUInt64(ClienteAux);
                    ObjSAC.Cliente = ObjUtilClass.FormataCNPJCPF(ClienteAux);
                }
                catch
                {
                    erro = "erro";
                }
            }

            if (erro == "erro")
            {
                ObjSAC.Cliente = ClienteTextBox.Text;
            }



        }

    }
}