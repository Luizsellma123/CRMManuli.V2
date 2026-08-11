using System;
using System.IO;
using System.Text;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Web;

namespace VendasWeb.Chamados
{
    public partial class ImportarChamadosWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass objUtilClass = new UtilClass();
        DataTable ImportacaoChamadosDataTable = new DataTable();
        ChamadoClass objChamado = new ChamadoClass();
        usuario Objusuario = new usuario();
        ExcelDataTableClass objExcelDataTableClass = new ExcelDataTableClass();
        ExcelClass objExcelClass = new ExcelClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                Session["ImportacaoChamadosDataTable"] = null;

                CarregaDadosNaTela();

                {
                    string aviso = "Nas colunas de solicitante e responsavel deve ser colocado o código do usuário (exemplo: moises.gonzalez).";

                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = objUtilClass.MenssagemAlerta(aviso, true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            //usuario ObjUsuario = new usuario();

            //ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            //EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            //EmpresaDropDownList.DataTextField = "NomeEmpresa";
            //EmpresaDropDownList.DataValueField = "IDEmpresa";
            //EmpresaDropDownList.DataBind();

            //if (Session["PedidoRastrear"] != null)
            //    objPedidoClass = (PedidoClass)Session["PedidoRastrear"];

            //EmpresaDropDownList.SelectedValue = objPedidoClass.EmpCod;
        }

        protected void SubirDadosLinkButton_Click(object sender, EventArgs e)
        {
            string erro = objExcelClass.VerificaArquivoExcel(ArquivoFileUpload);

            Session["ImportacaoChamadosDataTable"] = null;

            try
            {
                if (erro != "") throw new Exception(erro);

                string Caminho = HttpContext.Current.Server.MapPath(ArquivoFileUpload.FileName);

                if (File.Exists(Caminho) && ArquivoEstaEmUso(Caminho))
                    throw new IOException("O arquivo está sendo usado por outro processo.");

                ArquivoFileUpload.SaveAs(Caminho);

                DataTable DadosExcel = objExcelDataTableClass.LerExcel(Caminho);

                File.Delete(Caminho);

                erro = objExcelDataTableClass.Pre_Verificacao_Excel_Importacao_Chamados_DataTable(DadosExcel);

                if (erro != "") throw new Exception(erro);

                erro = MontaDataTableGridView(DadosExcel);

                if (erro != "") throw new Exception(erro);

                CarregaGridView();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro == "")
                ApresentaMensagem("", "Dados importados com sucesso. Verifique os dados e clique em 'Gravar Dados' para finalizar a importação.");
            else
                ApresentaMensagem(erro);
        }

        protected string MontaDataTableGridView(DataTable DadosExcel)
        {
            DataTable dt = new DataTable();

            {
                dt.Columns.Add("Data");
                dt.Columns.Add("Solicitante");
                dt.Columns.Add("IDSolicitante");
                dt.Columns.Add("Responsavel");
                dt.Columns.Add("IDResponsavel");
                dt.Columns.Add("Classificacao");
                dt.Columns.Add("IDClassificacao");
                dt.Columns.Add("Sistema");
                dt.Columns.Add("IDSistema");
                dt.Columns.Add("Status");
                dt.Columns.Add("IDStatus");
                dt.Columns.Add("Prioridade");
                dt.Columns.Add("IDPrioridade");
                dt.Columns.Add("Setor");
                dt.Columns.Add("IDSetor");
                dt.Columns.Add("Assunto");
                dt.Columns.Add("Descricao");
            }

            string erro = "";

            if (DadosExcel.Rows.Count > 0)
            {
                #region Campos

                string Data = "";

                string Solicitante = "";
                string IDSolicitante = "";

                string Responsavel = "";
                string IDResponsavel = "";

                string Classificacao = "";
                string IDClassificacao = "";

                string Status = "";
                string IDStatus = "";

                string Sistema = "";
                string IDSistema = "";

                string Prioridade = "";
                string IDPrioridade = "";

                string Setor = "";
                string IDSetor = "";

                string Assunto = "";

                string Descricao = "";

                #endregion

                int count = 1;

                foreach (DataRow row in DadosExcel.Rows)
                {
                    #region Recupera campos

                    Data = row["Data"]?.ToString() ?? "";

                    Solicitante = row["Solicitante"]?.ToString() ?? "";
                    IDSolicitante = ObterIdPorDescricao(
                        Solicitante,
                        objChamado.CarregaUsuarios(),
                        "CodUsuario",
                        "IDUsuario"
                    );

                    Responsavel = row["Responsavel"]?.ToString() ?? "";
                    IDResponsavel = ObterIdPorDescricao(
                        Responsavel,
                        objChamado.CarregaUsuariosSuporte(),
                        "CodUsuario",
                        "IDUsuario"
                    );

                    Classificacao = row["Classificacao"]?.ToString() ?? "";
                    IDClassificacao = ObterIdPorDescricao(
                        Classificacao,
                        objChamado.CarregaClassificacoes(),
                        "Descricao",
                        "IDClassificacao"
                    );

                    Status = row["Status"]?.ToString() ?? "";
                    IDStatus = ObterIdPorDescricao(
                        Status,
                        objChamado.CarregaStatus(),
                        "Descricao",
                        "IDStatus"
                    );

                    Prioridade = row["Prioridade"]?.ToString() ?? "";
                    IDPrioridade = ObterIdPorDescricao(
                        Prioridade,
                        objChamado.CarregaPrioridades(),
                        "Descricao",
                        "IDPrioridade"
                    );

                    Sistema = row["Sistema"]?.ToString() ?? "";
                    IDSistema = ObterIdPorDescricao(
                        Sistema,
                        objChamado.CarregaSistemas(),
                        "Descricao",
                        "IDSistema"
                    );

                    Setor = row["Setor"]?.ToString() ?? "";
                    IDSetor = ObterIdPorDescricao(
                        Setor,
                        Objusuario.ConsultaSetoresUsuario(),
                        "Descricao",
                        "IDSetor"
                    );

                    Assunto = row["Assunto"]?.ToString() ?? "";

                    Descricao = row["Descricao"]?.ToString() ?? "";

                    #endregion

                    #region Verifica campos

                    if (Data == ""
                      || IDSolicitante == ""
                      || IDResponsavel == ""
                      || IDClassificacao == ""
                      || IDStatus == ""
                      || IDPrioridade == ""
                      || IDSistema == ""
                      || IDSetor == ""
                      || Assunto == ""
                      || Descricao == "")
                    {
                        string erroIterativo = "";

                        if (Data == "")
                            erroIterativo += "Data não preenchida.";

                        if (IDSolicitante == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O solicitante '" + Solicitante + "' não foi encontrado. " + " (Linha " + count + ")";
                        }

                        if (IDResponsavel == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O responsável '" + Responsavel + "' não foi encontrado. " + " (Linha " + count + ")";
                        }

                        if (IDClassificacao == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "A classificação '" + Classificacao + "' não foi encontrada." + " (Linha " + count + ")";
                        }

                        if (IDStatus == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O status '" + Status + "' não foi encontrado." + " (Linha " + count + ")";
                        }

                        if (IDPrioridade == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "A prioridade '" + Prioridade + "' não foi encontrada." + " (Linha " + count + ")";
                        }

                        if (IDSistema == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O sistema '" + Sistema + "' não foi encontrado." + " (Linha " + count + ")";
                        }

                        if (IDSetor == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O setor '" + Setor + "' não foi encontrado." + " (Linha " + count + ")";
                        }

                        if (Assunto == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "Assunto não preenchido." + " (Linha " + count + ")";
                        }

                        if (Descricao == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "Descrição não preenchida." + " (Linha " + count + ")";
                        }

                        if (erro != "" && erroIterativo != "")
                            erro += " <br> ";

                        erro += erroIterativo;

                        count++;
                    }

                    #endregion

                    if (!(Data == ""
                     || IDSolicitante == ""
                     || IDResponsavel == ""
                     || IDClassificacao == ""
                     || IDStatus == ""
                     || IDPrioridade == ""
                     || IDSistema == ""
                     || IDSetor == ""
                     || Assunto == ""
                     || Descricao == ""))
                    {
                        dt.Rows.Add(
                              Data               // Data
                            , Solicitante        // Solicitante
                            , IDSolicitante      // IDSolicitante
                            , Responsavel        // Responsavel
                            , IDResponsavel      // IDResponsavel
                            , Classificacao      // Classificacao
                            , IDClassificacao    // IDClassificacao
                            , Sistema            // Sistema
                            , IDSistema          // IDSistema
                            , Status             // Status
                            , IDStatus           // IDStatus
                            , Prioridade         // Prioridade
                            , IDPrioridade       // IDPrioridade
                            , Setor              // Setor
                            , IDSetor            // IDSetor
                            , Assunto            // Assunto
                            , Descricao          // Descricao
                        );
                    }
                }
            }

            ImportacaoChamadosDataTable = dt;

            Session["ImportacaoChamadosDataTable"] = ImportacaoChamadosDataTable;

            return erro;
        }

        private string ObterIdPorDescricao(string valorProcurado, DataTable tabela, string colunaDescricao, string colunaId)
        {
            if (string.IsNullOrWhiteSpace(valorProcurado))
                return "";

            if (tabela == null || tabela.Rows.Count == 0)
                return "";

            foreach (DataRow row in tabela.Rows)
            {
                if (string.Equals(valorProcurado, row[colunaDescricao]?.ToString() ?? "", StringComparison.OrdinalIgnoreCase))
                {
                    return row[colunaId]?.ToString() ?? "";
                }
            }

            return "";
        }

        protected void CarregaGridView()
        {
            if (Session["ImportacaoChamadosDataTable"] != null)
                ImportacaoChamadosDataTable = (DataTable)Session["ImportacaoChamadosDataTable"];

            ImportacaoChamadosGridView.DataSource = ImportacaoChamadosDataTable;
            ImportacaoChamadosGridView.DataBind();
            ImportacaoChamadosMultiView.Visible = true;
        }

        protected void GravarDadosLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            DataTable ImportacaoChamados = (DataTable)Session["ImportacaoChamadosDataTable"];

            if (ImportacaoChamados != null)
            {
                if (ImportacaoChamados.Rows.Count > 0)
                {
                    int count = 1;

                    foreach (DataRow row in ImportacaoChamados.Rows)
                    {
                        string erroIterativo = "";

                        objChamado = new ChamadoClass();

                        objChamado.DataChamado = Convert.ToDateTime(row["Data"].ToString());

                        objChamado.IDUsuarioOperacao =
                                Convert.ToInt32(ObterIdPorDescricao(
                                    "Tecnologia.Informação",
                                    objChamado.CarregaUsuarios(),
                                    "CodUsuario",
                                    "IDUsuario"
                                ));

                        objChamado.IDUsuarioSolicitante = Convert.ToInt32(row["IDSolicitante"].ToString());

                        objChamado.IDUsuarioResponsavel = Convert.ToInt32(row["IDResponsavel"].ToString());

                        objChamado.IDClassificacao = Convert.ToInt32(row["IDClassificacao"].ToString());

                        objChamado.IDStatus = Convert.ToInt32(row["IDStatus"].ToString());

                        objChamado.IDSistema = Convert.ToInt32(row["IDSistema"].ToString());

                        objChamado.IDPrioridade = Convert.ToInt32(row["IDPrioridade"].ToString());

                        objChamado.IDSetor = Convert.ToInt32(row["IDSetor"].ToString());

                        objChamado.IDUsuarioKeyUser = ConsultaAdmSetor(objChamado.IDSetor);

                        objChamado.Assunto = row["Assunto"].ToString();

                        objChamado.descricao = row["Descricao"].ToString();

                        erroIterativo = objChamado.GravaDadosPrincipaisChamado();

                        if (erro != "") erro += " <br> ";

                        erro += erroIterativo;

                        if (erroIterativo != "")
                        {
                            erro += $" (Linha {count})";
                        }

                        count++;
                    }
                }
            }
            else
            {
                erro = "Antes de gravar precisa subir os dados.";
            }

            if (erro == "")
            {
                ApresentaMensagem("", "Dados gravados com sucesso.");

                LimparDadosLinkButton_Click(null, null);
            }
            else
                ApresentaMensagem(erro);
        }

        protected void LimparDadosLinkButton_Click(object sender, EventArgs e)
        {
            Session["ImportacaoChamadosDataTable"] = null;

            ImportacaoChamadosDataTable = new DataTable();

            CarregaGridView();
        }

        protected void ModeloLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable("ModeloImportacao");

                dt.Columns.Add("Data");
                dt.Columns.Add("Solicitante");
                dt.Columns.Add("Responsavel");
                dt.Columns.Add("Classificacao");
                dt.Columns.Add("Sistema");
                dt.Columns.Add("Status");
                dt.Columns.Add("Prioridade");
                dt.Columns.Add("Setor");
                dt.Columns.Add("Assunto");
                dt.Columns.Add("Descricao");

                System.IO.MemoryStream stream = objExcelDataTableClass.BaixarExcelDeDataTable(dt);

                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition",
                    string.Format("attachment;filename=Modelo_Importacao_Chamados.xls",
                    DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));

                stream.WriteTo(Response.OutputStream);

                Response.End();
            }
            catch (Exception ex)
            {
                ApresentaMensagem(ex.Message);
            }
        }

        private bool ArquivoEstaEmUso(string caminho)
        {
            try
            {
                using (FileStream stream = new FileStream(caminho, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false; // Conseguiu abrir com exclusividade: não está em uso
                }
            }
            catch (IOException)
            {
                return true; // Não conseguiu abrir: está em uso
            }
        }

        protected void ImportacaoChamadosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ImportacaoChamadosGridView.PageIndex = e.NewPageIndex;
            CarregaGridView();
        }

        protected void ApresentaMensagem(string erro = "", string sucesso = "")
        {
            if (erro != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = objUtilClass.MenssagemErro(erro, true);
            }
            else if (sucesso != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = objUtilClass.MenssagemSucesso(sucesso, true);
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = objUtilClass.MenssagemSucesso(erro, true);
            }

           ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        public int ConsultaAdmSetor(int IDSetor)
        {
            setor objSetor = new setor();

            objSetor.IDSetor = IDSetor;

            //Grupo de Suporte
            DataTable Setores = objSetor.RetornaUsuariosSetor();

            if (Setores.Rows.Count > 0)
            {
                foreach (DataRow row in Setores.Rows)
                {
                    if (Convert.ToBoolean(row["Administrador"]))
                        return Convert.ToInt32(row["IDUsuario"]);
                }
            }

            return 0;
        }
    }
}