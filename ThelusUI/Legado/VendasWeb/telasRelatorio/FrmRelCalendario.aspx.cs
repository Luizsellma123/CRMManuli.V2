using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.telasRelatorio
{
    public partial class FrmRelCalendario : System.Web.UI.Page
    {

        SessionClass OBJSessao = new SessionClass();

        CalendarEvent ObjCalendarEvent = new CalendarEvent();
        clasRelatorios ObjclasRelatorios = new clasRelatorios();
        GerencialVendas.ControleReportViwerClass ObjControleReportViwerClass = new GerencialVendas.ControleReportViwerClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            #region Registrando as Picker


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "Picker();", true);


            #endregion

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {

                Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                Session["Msg"] = null;
            }



            if (!IsPostBack)
            {

                /*Tratar Abrir e fechar Div*/
                collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse\" runat=\"server\">";


                #region Usuario
                ObjCalendarEvent.UsuCod = Session["usuario"].ToString();
                UsuarioSelect.DataSource = ObjCalendarEvent.Consulta_agenda_usuario_UsuCod();
                UsuarioSelect.DataTextField = "UsuCod";
                UsuarioSelect.DataValueField = "UsuCod";
                UsuarioSelect.DataBind();
                UsuarioSelect.Items.Insert(0, new ListItem("Todos", "TODOS"));
                #endregion

                #region Tipo Agendamento
                TipoAgendamentoSelect.DataSource = ObjCalendarEvent.Consulta_Tipos_Agendamentos();
                TipoAgendamentoSelect.DataTextField = "DescricaoTipoAgendamento";
                TipoAgendamentoSelect.DataValueField = "idTipoAgendamento";
                TipoAgendamentoSelect.DataBind();
                TipoAgendamentoSelect.Items.Insert(0, new ListItem("Todos", "TODOS"));
                #endregion



                ReportDataSource ReportDataSource = new ReportDataSource();
                ReportDataSource.Name = "user_sp_crm_relatorio_Agendamento";
                ReportDataSource.DataMember = "user_sp_crm_relatorio_Agendamento";
                ReportDataSource.Value = "";


                this.ReportViewer.LocalReport.DataSources.Clear();
                this.ReportViewer.LocalReport.DataSources.Add(ReportDataSource);
                this.ReportViewer.DataBind();

                ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "PDF");
                ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "WORD");




            }



        }


        protected void btnListar_Click(object sender, EventArgs e)
        {


            string UsuCod = Session["usuario"].ToString();
            DateTime DataInicio = Convert.ToDateTime(DataITextBox.Text);
            DateTime DataFinal = Convert.ToDateTime(DataFTextBox.Text);
            string EntNomeFant = "";
            string EntNome = "";
            string EntCod = "";
            string EntCpfCgc = "";

            string UsuCodFiltro = "";
            for (int i = 0; i < UsuarioSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (UsuarioSelect.Items[i].Selected == true)
                {
                    UsuCodFiltro += UsuarioSelect.Items[i].Value + ",";
                }
            }

            string IDAgendamentoFiltro = "";
            for (int i = 0; i < UsuarioSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (TipoAgendamentoSelect.Items[i].Selected == true)
                {
                    IDAgendamentoFiltro += TipoAgendamentoSelect.Items[i].Value + ",";
                }
            }



            switch (drpEntCod.SelectedValue.ToString())
            {
                case "1":
                    EntNomeFant = txtFiltro.Text;
                    break;

                case "2":
                    EntNome = txtFiltro.Text;
                    break;

                case "3":
                    EntCod = txtFiltro.Text;
                    break;

                case "4":
                    EntCpfCgc = txtFiltro.Text;
                    break;
            }




            ReportDataSource ReportDataSource = new ReportDataSource();
            DataTable DtDataTable;

            DtDataTable = ObjclasRelatorios.relatorioCalendario(UsuCod, UsuCodFiltro, DataInicio, DataFinal, IDAgendamentoFiltro, EntNomeFant, EntNome, EntCod, EntCpfCgc);
            ReportDataSource.Name = "user_sp_crm_relatorio_Agendamento";
            ReportDataSource.DataMember = "user_sp_crm_relatorio_Agendamento";
            ReportDataSource.Value = DtDataTable;


            this.ReportViewer.LocalReport.DataSources.Clear();
            this.ReportViewer.LocalReport.DataSources.Add(ReportDataSource);
            this.ReportViewer.DataBind();


        }

        protected void CancelarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/FrmCalendario.aspx?indmnu=3");
        }
    }
}