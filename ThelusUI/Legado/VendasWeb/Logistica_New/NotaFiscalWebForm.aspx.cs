using System;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Data.OleDb;

namespace VendasWeb.Logistica_New
{
    public partial class NotaFiscalWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass objSessao = new SessionClass();
        LogisticaClass objLogistica = new LogisticaClass();
        DataTable FechamentoFaturaNotasDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

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

            if (Session["Logistica"] != null)
                objLogistica = (LogisticaClass)Session["Logistica"];

            EmpresaDropDownList.SelectedValue = objLogistica.IDEmpresa.ToString();
            FechamentoTextBox.Text = objLogistica.Fechamento.ToString();

            CarregaGridView();
        }

        protected void CarregaDadosDaTela()
        {
            objLogistica.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            objLogistica.Fechamento = Convert.ToInt32(FechamentoTextBox.Text);
        }

        protected void CarregaGridView()
        {
            CarregaDadosDaTela();

            FechamentoFaturaNotasDataTable = objLogistica.RetornaListaFechamentoFaturaNotas();

            GridView.DataSource = FechamentoFaturaNotasDataTable;
            GridView.DataBind();
            MultiView.Visible = true;

            if (FechamentoFaturaNotasDataTable.Rows.Count == 1)
            {
                foreach (DataRow row in FechamentoFaturaNotasDataTable.Rows)
                {
                    if (row["Empresa"].ToString() != "Não Identificado")
                        LimparDadosLinkButton.Enabled = true;
                    else
                    {
                        BloqueiaExcluirGridViewLinkButton();
                        LimparDadosLinkButton.Enabled = false;
                    }
                }
            }
            else
            {
                LimparDadosLinkButton.Enabled = true;
            }
        }

        protected void ModeloLinkButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            //Get properties using reflection.
            DataSet ds = new DataSet("New_DataSet");

            DataTable dt = new DataTable("ModeloImportacao");
            //dt.Columns.Add("IDCusto");
            //dt.Columns.Add("Empresa");
            dt.Columns.Add("NotaFiscal");
            //dt.Columns.Add("Valor");
            //dt.Columns.Add("Identificado");
            //dt.Columns.Add("Importado");

            //Resolve problema: O Excel encontrou conteúdo ilegível / Invalid or corrupt file (unreadable content)
            for (int i = 0; i < 100; i++)
            {
                dt.Rows.Add(" ");
            }

            ds.Tables.Add(dt);

            MemoryStream stream = new MemoryStream();
            ExcelLibrary.DataSetHelper.CreateWorkbook(stream, ds);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Modelo.xls", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));

            stream.WriteTo(Response.OutputStream);

            Response.End();
        }

        protected void SubirDadosLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            string extensionArquivo = "";
            string Caminho = "";

            try
            {
                //Verifica se tem arquivo anexo para enviar
                if (ArquivoFileUpload.HasFile == true)
                {
                    extensionArquivo = System.IO.Path.GetExtension(ArquivoFileUpload.FileName);

                    if (extensionArquivo != ".xls" && extensionArquivo != ".xlsx")
                        erro = "Somente permitido com a extensão .xls ou .xlsx !";
                    else
                    {
                        Caminho = Server.MapPath(ArquivoFileUpload.FileName);
                        ArquivoFileUpload.SaveAs(Caminho);
                    }
                }
                else
                {
                    erro = "Favor selecionar um arquivo.";
                }

                if (erro == "")
                {
                    DataTable DadosCSV = READExcel(Caminho);

                    File.Delete(Caminho);

                    erro = GravaCSV(DadosCSV);

                    CarregaGridView();
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            ApresentaMensagem(erro);
        }

        public DataTable READExcel(string path)
        {
            Microsoft.Office.Interop.Excel.Application objXL = null;
            Microsoft.Office.Interop.Excel.Workbook objWB = null;
            objXL = new Microsoft.Office.Interop.Excel.Application();
            objWB = objXL.Workbooks.Open(path);
            Microsoft.Office.Interop.Excel.Worksheet objSHT = objWB.Worksheets[1];

            int rows = objSHT.UsedRange.Rows.Count;
            int cols = objSHT.UsedRange.Columns.Count;
            DataTable dt = new DataTable();
            int noofrow = 1;

            for (int c = 1; c <= cols; c++)
            {
                string colname = objSHT.Cells[1, c].Text;
                dt.Columns.Add(colname);
                noofrow = 2;
            }

            for (int r = noofrow; r <= rows; r++)
            {
                DataRow dr = dt.NewRow();
                for (int c = 1; c <= cols; c++)
                {
                    dr[c - 1] = objSHT.Cells[r, c].Text;
                }

                dt.Rows.Add(dr);
            }

            objWB.Close();
            objXL.Quit();
            return dt;
        }

        public string GravaCSV(DataTable Excel)
        {
            string erro = "";

            if (Session["Logistica"] != null)
                objLogistica = (LogisticaClass)Session["Logistica"];

            CarregaIDUsuarioDaSessao();

            if (Excel.Rows.Count > 0)
            {
                foreach (DataRow row in Excel.Rows)
                {
                    string NotaFiscal = row["NotaFiscal"].ToString();

                    if (NotaFiscal != "" && NotaFiscal != " ")
                    {
                        objLogistica.NumeroNota = Convert.ToInt32(NotaFiscal);

                        DataTable ConsultaSAP = objLogistica.RetornaDadosSAPFechamentoFaturaNotas();

                        if (ConsultaSAP.Rows.Count > 0)
                        {
                            objLogistica.Identificado = 1;

                            foreach (DataRow dataRow in ConsultaSAP.Rows)
                            {
                                objLogistica.CodigoClienteSAP = dataRow["CardCode"].ToString();
                                objLogistica.PrimarioNotaSAP = Convert.ToInt32(dataRow["DocEntry"]);
                                objLogistica.ValorNota = Convert.ToDecimal(dataRow["InsTotal"]);
                            }

                            erro += objLogistica.GravaFechamentoFaturaNotas();
                        }
                        else
                        {
                            erro += "Dados da nota " + objLogistica.NumeroNota.ToString() + " não encontrados no SAP. <br>";

                            //objLogistica.Identificado = 0;
                        }

                        //if (erro != "") break;
                    }
                }
            }

            return erro;
        }

        protected void LimparDadosLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            objLogistica.IDNota = 0;

            CarregaIDUsuarioDaSessao();

            string erro = objLogistica.ExcluiFechamentoFaturaNotas();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void ExcluirGridViewLinkButton_Click1(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            CarregaIDUsuarioDaSessao();

            objLogistica.IDNota = Convert.ToInt32(((Label)((Control)sender).FindControl("IDNotaGridViewLabel")).Text);

            string erro = objLogistica.ExcluiFechamentoFaturaNotas();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;

            CarregaGridView();
        }

        protected void BloqueiaExcluirGridViewLinkButton()
        {
            foreach (GridViewRow row in GridView.Rows)
            {
                LinkButton btn = row.FindControl("ExcluirGridViewLinkButton") as LinkButton;

                btn.Enabled = false;
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/FechamentoFaturaDetalheWebForm.aspx?indmnu=5");
        }

        protected void ApresentaMensagem(string erro)
        {
            if (erro != "")
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void CarregaIDUsuarioDaSessao()
        {
            if (Session["IDUsuario"] != null)
                objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());
        }
    }
}