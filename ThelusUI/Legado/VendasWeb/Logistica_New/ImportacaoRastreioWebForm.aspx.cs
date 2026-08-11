using System;
using System.IO;
using System.Text;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Logistica_New
{
    public partial class ImportacaoRastreioWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass objUtilClass = new UtilClass();
        PedidoClass objPedidoClass = new PedidoClass();
        DataTable ImportacaoRastreioDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                Session["ImportacaoRastreioDataTable"] = null;

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

            if (Session["PedidoRastrear"] != null)
                objPedidoClass = (PedidoClass)Session["PedidoRastrear"];

            EmpresaDropDownList.SelectedValue = objPedidoClass.EmpCod;
        }

        protected void SubirDadosLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            string extensionArquivo = "";
            string Caminho = "";

            Session["ImportacaoRastreioDataTable"] = null;

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
                    DataTable DadosExcel = ReadExcel(Caminho);

                    File.Delete(Caminho);

                    erro = MontaDataTableGridView(DadosExcel);

                    CarregaGridView();
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            ApresentaMensagem(erro);
        }

        public DataTable ReadExcel(string path)
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

        protected string MontaDataTableGridView(DataTable DadosExcel)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IDEmpresa");
            dt.Columns.Add("NOTA_FISCAL");
            dt.Columns.Add("IDCliente");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("IDPedido");
            dt.Columns.Add("PrevisaoEntrega");
            dt.Columns.Add("Historico");
            dt.Columns.Add("IDEvento");
            dt.Columns.Add("Evento");
            dt.Columns.Add("IDCategoria");
            dt.Columns.Add("Categoria");

            string erro = "";

            if (DadosExcel.Rows.Count > 0)
            {
                string IDEmpresa = "";
                string NOTA_FISCAL = "";
                string IDCliente = "";
                string Cliente = "";
                string IDPedido = "";
                string PrevisaoEntrega = "";
                string Historico = "";
                string Evento = "";
                string IDEvento = "";
                string Categoria = "";
                string IDCategoria = "";

                int count = 1;

                foreach (DataRow row in DadosExcel.Rows)
                {
                    #region Recupera campos

                    IDEmpresa = EmpresaDropDownList.SelectedValue;

                    NOTA_FISCAL = row["NOTA_FISCAL"].ToString().Trim();

                    if (NOTA_FISCAL == "") break;

                    if (NOTA_FISCAL != "" && IDEmpresa != "")
                        IDCliente = RecuperaIDCliente(NOTA_FISCAL, IDEmpresa);

                    if (IDCliente != "")
                        Cliente = RecuperaCliente(IDCliente);

                    if (IDCliente != "" && NOTA_FISCAL != "")
                        IDPedido = RecuperaIDPedido(IDCliente, NOTA_FISCAL);

                    PrevisaoEntrega = row["PrevisaoEntrega"].ToString().Trim();

                    Historico = row["Historico"].ToString().Trim();

                    Evento = RecuperaEvento(row["Evento"].ToString().Trim());

                    if (Evento != "")
                        IDEvento = RecuperaIDEvento(Evento);

                    if (IDEvento != "")
                        Categoria = RecuperaCategoria(IDEvento, row["Categoria"].ToString().Trim());

                    if (IDEvento != "" && Categoria != "")
                        IDCategoria = RecuperaIDCategoria(IDEvento, Categoria);

                    #endregion

                    if (NOTA_FISCAL == ""
                     || IDCliente == ""
                     || Cliente == ""
                     || IDPedido == ""
                     || PrevisaoEntrega == ""
                     || Historico == ""
                     || Evento == ""
                     || IDEvento == ""
                     || Categoria == ""
                     || IDCategoria == "")
                    {
                        string erroIterativo = "";

                        if (IDCliente == "" || Cliente == "")
                            erroIterativo += "Cliente da nota fiscal " + NOTA_FISCAL + " da empresa " +
                                EmpresaDropDownList.SelectedItem.Text + " não foi encontrado.";

                        if (IDPedido == "" && erroIterativo == "")
                            erroIterativo += "Pedido da nota fiscal " + NOTA_FISCAL + " e do cliente " +
                                Cliente + " não foi encontrado ou não está no CRM.";

                        if (PrevisaoEntrega == "" && erroIterativo == "")
                            erroIterativo += "Previsão de entrega em branco na linha " + count + ".";
                        else
                        {
                            try
                            {
                                PrevisaoEntrega = Convert.ToDateTime(PrevisaoEntrega).ToString("dd-MM-yyyy HH:mm:ss");

                                if (Historico == "")
                                    Historico = "Atualizado Manual. Previsão entrega: " + PrevisaoEntrega;
                            }
                            catch
                            {
                                erroIterativo += "Previsão de entrega com formato errado na linha " + count + ". O certo é (dd-MM-aaaa HH:mm:ss)";
                            }                            
                        }

                        if (Evento != "" && IDEvento == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";

                            erroIterativo += "Evento " + Evento + " não encontrado.";
                        }

                        if (Categoria != "" && IDCategoria == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";

                            erroIterativo += "Categoria " + Categoria + " não encontrada.";
                        }

                        if (erro != "" && erroIterativo != "")
                            erro += " <br> ";

                        erro += erroIterativo;
                    }

                    if (!(NOTA_FISCAL == ""
                         || IDCliente == ""
                         || Cliente == ""
                         || IDPedido == ""
                         || PrevisaoEntrega == ""
                         || Historico == ""
                         || Evento == ""
                         || IDEvento == ""
                         || Categoria == ""
                         || IDCategoria == ""))
                    {
                        dt.Rows.Add(
                          IDEmpresa
                        , NOTA_FISCAL
                        , IDCliente
                        , Cliente
                        , IDPedido
                        , PrevisaoEntrega
                        , Historico
                        , IDEvento
                        , Evento
                        , IDCategoria
                        , Categoria
                        );
                    }
                }
            }

            ImportacaoRastreioDataTable = dt;

            Session["ImportacaoRastreioDataTable"] = ImportacaoRastreioDataTable;

            if (erro != "") erro += " <br> Foram adicionados " + dt.Rows.Count + " registros.";

            return erro;
        }

        #region Recupera dados para montar GridView

        protected string RecuperaCampoDataTableConsultaSAPSQL(string stringSQL, string campo)
        {
            ComunicacaoSAPClass objComunicacaoSAPClass = new ComunicacaoSAPClass();

            DataTable ConsultaSQL = objComunicacaoSAPClass.RetornaDadosConsultaSAP(stringSQL);

            if (ConsultaSQL.Rows.Count > 0)
            {
                foreach (DataRow row in ConsultaSQL.Rows)
                {
                    return row[campo].ToString();
                }
            }

            return "";
        }

        protected string RecuperaIDCliente(string NOTA_FISCAL, string IDEmpresa)
        {
            StringBuilder stringSQL = new StringBuilder();

            stringSQL.AppendLine("SELECT CardCode FROM OINV ");

            stringSQL.AppendLine("WHERE Serial = " + NOTA_FISCAL + " AND BPLId = " + IDEmpresa + " ");

            return objPedidoClass.RetornaIDCliente(RecuperaCampoDataTableConsultaSAPSQL(stringSQL.ToString(), "CardCode"));
        }

        protected string RecuperaCliente(string IDCliente)
        {
            if (IDCliente != "")
                return objPedidoClass.RetornaCodigoENomeCliente(Convert.ToInt32(IDCliente));
            else
                return "";
        }

        protected string RecuperaIDPedido(string IDCliente, string NOTA_FISCAL)
        {
            return objPedidoClass.RetornaIDPedido(IDCliente, NOTA_FISCAL);
        }

        protected string RecuperaEvento(string Evento)
        {
            if (Evento.Trim() == "")
            {
                HistoricosClass objHistoricosClass = new HistoricosClass();

                objHistoricosClass.IDTipoHistorico = 6;

                objHistoricosClass.IDEvento = 1;

                return objHistoricosClass.RetornaEventoDescricao();
            }

            return Evento;
        }

        protected string RecuperaIDEvento(string Evento)
        {
            HistoricosClass objHistoricosClass = new HistoricosClass();

            objHistoricosClass.IDTipoHistorico = 6;

            objHistoricosClass.Descricao = Evento;

            return objHistoricosClass.RetornaEventoIDEvento();
        }

        protected string RecuperaCategoria(string IDEvento, string Categoria)
        {
            if (Categoria.Trim() == "")
            {
                HistoricosClass objHistoricosClass = new HistoricosClass();

                objHistoricosClass.IDTipoHistorico = 6;

                objHistoricosClass.IDEvento = Convert.ToInt32(IDEvento);

                objHistoricosClass.IDCategoria = 6;

                return objHistoricosClass.RetornaEventoCategoriaDescricao();
            }

            return Categoria;
        }

        protected string RecuperaIDCategoria(string IDEvento, string Categoria)
        {
            HistoricosClass objHistoricosClass = new HistoricosClass();

            objHistoricosClass.IDTipoHistorico = 6;

            objHistoricosClass.IDEvento = Convert.ToInt32(IDEvento);

            objHistoricosClass.Descricao = Categoria;

            return objHistoricosClass.RetornaEventoCategoriaIDCategoria();
        }

        #endregion

        protected void CarregaGridView()
        {
            if (Session["ImportacaoRastreioDataTable"] != null)
                ImportacaoRastreioDataTable = (DataTable)Session["ImportacaoRastreioDataTable"];

            ImportacaoRastreioGridView.DataSource = ImportacaoRastreioDataTable;
            ImportacaoRastreioGridView.DataBind();
            ImportacaoRastreioMultiView.Visible = true;
        }

        protected void AtualizarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            DataTable ImportacaoRastreio = (DataTable)Session["ImportacaoRastreioDataTable"];

            if (ImportacaoRastreio.Rows.Count > 0)
            {
                PedidoClass objPedidoClass = new PedidoClass();

                foreach (DataRow row in ImportacaoRastreio.Rows)
                {
                    string erroIterativo = "";

                    objPedidoClass.IDEmpresa = Convert.ToInt32(row["IDEmpresa"]);
                    objPedidoClass.IDPedido = Convert.ToInt32(row["IDPedido"]);
                    objPedidoClass.NumeroNotaFiscal = row["NOTA_FISCAL"].ToString();

                    objPedidoClass.IDTipo = 6;
                    objPedidoClass.IDEvento = Convert.ToInt32(row["IDEvento"]);
                    objPedidoClass.IDCategoria = Convert.ToInt32(row["IDCategoria"]);

                    objPedidoClass.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

                    objPedidoClass.Historico = row["Historico"].ToString();

                    objPedidoClass.PrevisaoEntrega = Convert.ToDateTime(row["PrevisaoEntrega"]).ToString("yyyy-MM-dd HH:mm:ss");

                    erroIterativo = objPedidoClass.GravaHistoricoPedidosImportacao();

                    if (erro != "") erro += " <br> ";

                    erro += erroIterativo;
                }

                ApresentaMensagem(erro);
            }
        }

        protected void LimparDadosLinkButton_Click(object sender, EventArgs e)
        {
            Session["ImportacaoRastreioDataTable"] = null;

            ImportacaoRastreioDataTable = new DataTable();

            CarregaGridView();
        }

        protected void ModeloLinkButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            //Get properties using reflection.
            DataSet ds = new DataSet("New_DataSet");

            DataTable dt = new DataTable("ModeloImportacao");

            dt.Columns.Add("NOTA_FISCAL");
            dt.Columns.Add("Historico");
            dt.Columns.Add("PrevisaoEntrega");
            dt.Columns.Add("Evento");
            dt.Columns.Add("Categoria");

            //Resolve problema: O Excel encontrou conteúdo ilegível / Invalid or corrupt file (unreadable content)
            for (int i = 0; i < 100; i++)
            {
                dt.Rows.Add(" ", " ", " ", " ", " ");
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

        protected void ImportacaoRastreioGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ImportacaoRastreioGridView.PageIndex = e.NewPageIndex;
            CarregaGridView();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/RastreioPedidosWebForm.aspx?indmnu=5");
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = objUtilClass.MenssagemErro(erro, true);
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = objUtilClass.MenssagemSucesso(erro, true);
            }

           ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }
    }
}