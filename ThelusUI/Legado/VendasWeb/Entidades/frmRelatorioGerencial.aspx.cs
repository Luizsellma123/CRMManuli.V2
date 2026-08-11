using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace VendasWeb.Entidades
{
    public partial class frmRelatorioGerencial : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        VendedorClass ObjVendedorClass = new VendedorClass();
        GerencialVendas.OfficeClass ObjOfficeClass = new GerencialVendas.OfficeClass();
        GerencialVendas.ControleReportViwerClass ObjControleReportViwerClass = new GerencialVendas.ControleReportViwerClass();
        clsEntidades ObjEntidadesClass = new clsEntidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                //VendNomeLabel.Text = Session["VendNome"].ToString();

                //Combo vendedor
                #region Combo classe
                ObjVendedorClass.UsuCod = Session["usuario"].ToString();
                ClasseVendedorDropDownList.DataSource = ObjVendedorClass.Listar_Classes_Vendedores();
                ClasseVendedorDropDownList.DataTextField = "VendClasseDescr";
                ClasseVendedorDropDownList.DataValueField = "vendClasseCod";
                ClasseVendedorDropDownList.DataBind();
                ClasseVendedorDropDownList.Items.Insert(0, new ListItem("Todos", "Todos"));
                #endregion

                //Atualiza combo com os vendedores
                Atualiza_Select_Vendedores();

                ReportDataSource rptDados = new ReportDataSource();

                rptDados.Name = "dtRelatorioGerencial";
                rptDados.DataMember = "dtRelatorioGerencial";
                rptDados.Value = "";

                //Atribui ReportDataSource aos dados do sistema
                this.rptRelatorioGerencial.LocalReport.DataSources.Add(rptDados);
                this.rptRelatorioGerencial.DataBind();

                //relatorios.DisableUnwantedExportFormat(rptRelatorioGerencial, "Excel");
                ObjControleReportViwerClass.DisableUnwantedExportFormat(rptRelatorioGerencial, "PDF");
                ObjControleReportViwerClass.DisableUnwantedExportFormat(rptRelatorioGerencial, "WORD");
            }
        }

        protected void atualizarGrid()
        {
            //RelatorioGerencialMultiView.Visible = true;
            string Retorno = "";

            //ObjVendedorClass.VendCod = Session["VendCod"].ToString();
            ObjVendedorClass.Status = StatusDropDownList.SelectedValue;
            ObjVendedorClass.UF = UFTextBox.Text;
            ObjVendedorClass.Regiao = RegiaoTextBox.Text;
            ObjVendedorClass.Cidade = CidadeTextBox.Text;
            ObjVendedorClass.VendClasseCod = ClasseVendedorDropDownList.SelectedValue;
            ObjVendedorClass.DataInicial = DataInicialTextBox.Text;
            ObjVendedorClass.DataFinal = DataFinalTextBox.Text;

            if (ClasseVendedorDropDownList.SelectedValue == "Todos")
            {
                ObjVendedorClass.UsuCod = Session["usuario"].ToString();
                ObjVendedorClass.VendClasseCod = ObjVendedorClass.Concatena_classe_Vendedor();
            }            

            RelatorioGerencialGridView.DataSource = ObjVendedorClass.Listar_Relatorio_Gerencial();
            RelatorioGerencialGridView.DataBind();

            ObjOfficeClass.GridView1 = RelatorioGerencialGridView;
            Retorno = ObjOfficeClass.ExportDataSetToExcel();

            StatusDropDownList.SelectedValue = "";
            UFTextBox.Text = "";
            RegiaoTextBox.Text = "";
            CidadeTextBox.Text = "";
            ClasseVendedorDropDownList.SelectedValue = "Todos";
            DataInicialTextBox.Text = "";
            DataFinalTextBox.Text = "";

            StatusDropDownList.Focus();

            Response.Write("<script>alert(\"" + Retorno + "\");</script>");

        }

        protected void ListarLinkButton_Click(object sender, EventArgs e)
        {
            RelatorioGerencialMultiView.Visible = true;
            
            //atualizarGrid();

            //ObjVendedorClass.VendCod = Session["VendCod"].ToString();
            ObjVendedorClass.Status = StatusDropDownList.SelectedValue;
            ObjVendedorClass.UF = UFTextBox.Text;
            ObjVendedorClass.Regiao = RegiaoTextBox.Text;
            ObjVendedorClass.Cidade = CidadeTextBox.Text;
            ObjVendedorClass.VendClasseCod = ClasseVendedorDropDownList.SelectedValue;
            ObjVendedorClass.DataInicial = DataInicialTextBox.Text;
            ObjVendedorClass.DataFinal = DataFinalTextBox.Text;

            if (ClasseVendedorDropDownList.SelectedValue == "Todos")
            {
                ObjVendedorClass.UsuCod = Session["usuario"].ToString();
                ObjVendedorClass.VendClasseCod = ObjVendedorClass.Concatena_classe_Vendedor();
            }

            RecuperaDados_Select();            

            DataTable tabRelatorioGerencial;
            ReportDataSource rptDados = new ReportDataSource();

            tabRelatorioGerencial = ObjVendedorClass.Listar_Relatorio_Gerencial();

            //Preenche ReportDataSource Cabecalho
            rptDados.Name = "dtRelatorioGerencial";
            rptDados.DataMember = "dtRelatorioGerencial";
            rptDados.Value = tabRelatorioGerencial;

            //Atribui ReportDataSource aos dados do sistema

            this.rptRelatorioGerencial.LocalReport.DataSources.Clear();
            this.rptRelatorioGerencial.LocalReport.DataSources.Add(rptDados);
            this.rptRelatorioGerencial.DataBind();
        }

        protected void Atualiza_Select_Vendedores()
        {
            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = "S";
            VendedoresSelect.DataSource = ObjVendedorClass.Consulta_Vendedor();
            VendedoresSelect.DataTextField = "VendNome";
            VendedoresSelect.DataValueField = "VendCod";
            VendedoresSelect.DataBind();
        }

        protected void RecuperaDados_Select()
        {
            ObjVendedorClass.VendCod = "";

            for (int i = 0; i < VendedoresSelect.Items.Count; i++)
            {
                //verifica se o check ta marcado ou nao
                if (VendedoresSelect.Items[i].Selected == true)
                {
                    ObjVendedorClass.VendCod += VendedoresSelect.Items[i].Value + ",";
                }
            }
        }

        protected void RelatorioGerencialGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            RelatorioGerencialGridView.PageIndex = e.NewPageIndex;
            atualizarGrid();
        }
    }
}