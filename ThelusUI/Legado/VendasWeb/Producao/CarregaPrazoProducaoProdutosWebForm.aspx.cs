using System;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Linq;

namespace VendasWeb.Producao
{
    public partial class CarregaPrazoProducaoProdutosWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass objSessao = new SessionClass();
        producao objProducao = new producao();
        DataTable Excel = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();

                Session["DataTableExcel"] = null;
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
        }

        protected void CarregaGridView(string metodo = "")
        {
            if (Session["DataTableExcel"] != null && metodo != "SubirDadosLinkButton_Click")
                Excel = (DataTable)Session["DataTableExcel"];

            ProducaoGridView.DataSource = Excel;
            ProducaoGridView.DataBind();
            ProducaoMultiView.Visible = true;

            if (Excel.Rows.Count > 0) AtualizaPrazosLinkButton.Enabled = true;

            Session["DataTableExcel"] = Excel;
        }

        protected void AtualizaPrazosLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "", msgErro = "";
            int sucesso = 0;

            DataTable Excel = new DataTable();

            if (Session["DataTableExcel"] != null)
                Excel = (DataTable)Session["DataTableExcel"];

            if (Excel.Rows.Count > 0)
            {
                erro = objProducao.Exclui_PrazoProducaoProdutos();

                if (erro == "")
                {
                    string prazoProducao = "", prazoExpedicao = "", qtdEstoque = "";

                    foreach (DataRow row in Excel.Rows)
                    {
                        objProducao.IDEmpresa = Convert.ToInt32(row["IDEmpresa"]);

                        objProducao.CodigoProduto = row["Produto"].ToString();

                        erro = ValidaProduto(objProducao.CodigoProduto);
                        if (erro != "")
                            msgErro += erro + " <br>";

                        if (erro == "")
                        {
                            prazoProducao = row["Producao"].ToString();
                            erro = ValidaInt(prazoProducao);
                            if (erro != "")
                                msgErro += "O valor do prazo de produção do produto " + objProducao.CodigoProduto + erro + " <br>";
                            else
                                objProducao.PrazoProducao = Convert.ToInt32(prazoProducao);
                        }

                        if (erro == "")
                        {
                            prazoExpedicao = row["Expedicao"].ToString();
                            erro = ValidaInt(prazoExpedicao);
                            if (erro != "")
                                msgErro += "O valor do prazo de expedição do produto " + objProducao.CodigoProduto + erro + " <br>";
                            else
                                objProducao.PrazoExpedicao = Convert.ToInt32(prazoExpedicao);
                        }

                        if (erro == "")
                        {
                            qtdEstoque = row["Estoque"].ToString();
                            erro = ValidaInt(qtdEstoque);
                            if (erro != "")
                                msgErro += "O valor da quantidade de estoque do produto " + objProducao.CodigoProduto + erro + " <br>";
                            else
                                objProducao.QuantidadeEstoque = Convert.ToInt32(qtdEstoque);
                        }

                        if (erro == "")
                        {
                            erro = objProducao.GravaPrazoProducaoProdutos();
                            if (erro != "")
                                msgErro += erro + " <br>";
                        }

                        if (erro == "") sucesso++;
                    }
                }
            }

            if (erro == "")
            {
                objProducao.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
                erro = objProducao.GravaProdutoEstoqueEmpenho();
                if (erro != "")
                    msgErro += erro + " <br>";
            }

            ApresentaMensagemSucesso("Foram incluidos " + sucesso + " produtos com sucesso. <br> <br> " + msgErro);
        }

        protected string ValidaProduto(string produto)
        {
            CrmProdutoClass objCrmProdutoClass = new CrmProdutoClass();

            objCrmProdutoClass.CodigoProdutoSAP = produto;

            if (objCrmProdutoClass.RetornaProdutoPorCodigoProdutoSAP().Rows.Count == 0)
                return "Produto " + produto + " não encontrado.";

            return "";
        }

        protected string ValidaInt(string inteiro)
        {
            try
            {
                Convert.ToInt32(inteiro);
            }
            catch
            {
                return " precisa ser um número inteiro.";
            }

            return "";
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
                        Caminho = Server.MapPath("Importacoes");

                        if (!(Directory.Exists(Caminho))) Directory.CreateDirectory(Caminho);

                        Caminho += "\\" + ArquivoFileUpload.FileName;

                        if (System.IO.File.Exists(Caminho)) System.IO.File.Delete(Caminho);

                        ArquivoFileUpload.SaveAs(Caminho);
                    }
                }
                else
                {
                    erro = "Favor selecionar um arquivo.";
                }

                if (erro == "")
                {
                    Excel = READExcel(Caminho);

                    System.IO.File.Delete(Caminho);

                    erro = VerificaExcel();

                    if (erro == "")
                        CarregaGridView("SubirDadosLinkButton_Click");
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro != "") ApresentaMensagemErro(erro);
            else ApresentaMensagemSucesso("Dados subidos com sucesso.");
        }

        protected string VerificaExcel()
        {
            string erro = "";
            string[] ProdutosVerificados = new string[Excel.Rows.Count];
            int countMax = 0;

            if (Excel.Rows.Count > 0)
            {
                foreach (DataRow row in Excel.Rows)
                {
                    int quantidadeProduto = 0;

                    if (PrecisaVerificar(row["Produto"].ToString(), ProdutosVerificados, countMax))
                    {
                        foreach (DataRow row2 in Excel.Rows)
                        {
                            if (row["Produto"].ToString() == row2["Produto"].ToString())
                                quantidadeProduto++;
                        }

                        if (quantidadeProduto > 1)
                            erro += "O produto " + row["Produto"].ToString() + " está repetido várias vezes. <br>";

                        ProdutosVerificados[countMax] = row["Produto"].ToString();
                        countMax++;
                    }
                }
            }
            else
            {
                erro = "O Excel está vazio.";
            }

            return erro;
        }

        protected bool PrecisaVerificar(string Produto, string[] ProdutosVerificados, int countMax)
        {
            for (int i = 0; i < countMax; i++)
            {
                if (Produto == ProdutosVerificados[i].ToString())
                    return false;
            }

            return true;
        }

        protected DataTable READExcel(string path)
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
                dt.Columns.Add(objSHT.Cells[1, c].Text);
                noofrow = 2;
            }

            dt.Columns.Add("IDEmpresa");
            dt.Columns.Add("Empresa");

            bool Break = false;

            for (int r = noofrow; r <= rows; r++)
            {
                DataRow dr = dt.NewRow();

                for (int c = 1; c <= cols + 2; c++)
                {
                    if (objSHT.Cells[r, 1].Text == "" || objSHT.Cells[r, 1].Text == " ")
                    {
                        Break = true;
                        break;
                    }

                    string row = "";

                    if (c == cols + 1)
                        row = EmpresaDropDownList.SelectedValue;
                    else if (c == cols + 2)
                        row = EmpresaDropDownList.SelectedValue + " - " + EmpresaDropDownList.SelectedItem.Text;
                    else
                        row = objSHT.Cells[r, c].Text;

                    row = row.Trim();

                    dr[c - 1] = row;
                }

                if (Break) break;

                dt.Rows.Add(dr);
            }

            objWB.Close();
            objXL.Quit();
            return dt;
        }

        protected void ModeloLinkButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            //Get properties using reflection.
            DataSet ds = new DataSet("New_DataSet");

            DataTable dt = new DataTable("ModeloImportacao");
            dt.Columns.Add("Produto");
            dt.Columns.Add("Producao");
            dt.Columns.Add("Expedicao");
            dt.Columns.Add("Estoque");

            //Resolve problema: O Excel encontrou conteúdo ilegível / Invalid or corrupt file (unreadable content)
            for (int i = 0; i < 100; i++)
            {
                dt.Rows.Add(" ", " ", " ", " ");
            }

            ds.Tables.Add(dt);

            MemoryStream stream = new MemoryStream();

            ExcelLibrary.DataSetHelper.CreateWorkbook(stream, ds);

            string nomeModelo = "Modelo Importação Prazo Produção Produtos";

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment;filename=" + nomeModelo + ".xls", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));

            stream.WriteTo(Response.OutputStream);

            Response.End();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/PrazoProducaoProdutosWebForm.aspx?indmnu=3");
        }

        protected void ApresentaMensagemErro(string msg)
        {
            ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(msg, true);
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        protected void ApresentaMensagemSucesso(string msg)
        {
            ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(msg, true);
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        protected void ProducaoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProducaoGridView.PageIndex = e.NewPageIndex;

            CarregaGridView("ProducaoGridView_PageIndexChanging");
        }
    }
}