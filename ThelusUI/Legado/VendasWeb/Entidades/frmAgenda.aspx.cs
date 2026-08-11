using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.Entidades
{
    public partial class frmAgenda : System.Web.UI.Page
    {
        GerencialVendas.AgendaClass AgendaClass = new GerencialVendas.AgendaClass();
        //classes.clsEntidades clsEntidades = new classes.clsEntidades();
        clsEntidades clsEntidades = new clsEntidades();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                clsEntidades = ((clsEntidades)Session["clsEntidades"]);
                clsEntidades.Mostra_Entidade();
                LblCliente.Text = clsEntidades.EntNome;
                lblCNPJ.Text = clsEntidades.EntCpfCgc;
                /*lblFone.Text = clsEntidades.ListEntWeb<EntFone>;
                lblEmail.Text = clsEntidades.EntEmail;
                lblContato.Text = clsEntidades.UserEntFoneNome;*/

                Atualizar_Grid();
            }
        }

        protected void SalvarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            AgendaClass.UsuCod = Session["usuario"].ToString();
            AgendaClass.Data = txtData.Text;
            AgendaClass.Historico = txtDescricao.Text;
            AgendaClass.EntCod = ((clsEntidades)Session["clsEntidades"]).EntCod;
            AgendaClass.Codigo = 0;
            erro = AgendaClass.Agenda_Inserir();

            if (erro == "")
            {
                Atualizar_Grid();
                Response.Write("<script>alert(\"Agenda inserida com sucesso.\");</script>");

            }
        }

        public void Atualizar_Grid()
        {
            AgendaClass.EntCod = ((clsEntidades)Session["clsEntidades"]).EntCod;
            AgendaGridView.DataSource = AgendaClass.Agenda_Listar();
            Session.Add("TEMP_SESSAO", AgendaGridView.DataSource);
            AgendaGridView.DataBind();
        }

        protected void ListarButton_Click(object sender, EventArgs e)
        {
            Atualizar_Grid();
        }

        protected void AgendaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AgendaGridView.PageIndex = e.NewPageIndex;
            Atualizar_Grid();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"FrmAbaPrincipal.aspx?indmnu=32\";</script>");
        }
    }
}