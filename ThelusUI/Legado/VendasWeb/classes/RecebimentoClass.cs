using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class RecebimentoClass : clsConexao
    {
        #region Campos

        public int IDEmpresa { get; set; }

        public int IDRecebimento { get; set; }

        public int IDUsuario { get; set; }

        public int IDUsuarioLogado { get; set; }

        public int IDStatus { get; set; }

        public int IDSetor { get; set; }

        public int IDFornecedor { get; set; }

        public int IDTipo { get; set; }

        public int IDEvento { get; set; }

        public int IDCategoria { get; set; }

        public bool Manual { get; set; }

        public string CNPJ { get; set; }

        public string NomeFornecedor { get; set; }

        public string NumeroNotaFiscal { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Observacao { get; set; }

        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public string Historico { get; set; }

        public string ArquivoAnexo { get; set; }

        public string ArquivoExtensao { get; set; }

        public string NomeArquivo { get; set; }

        public string DescricaoArquivo { get; set; }

        public int IDAnexo { get; set; }

        public string CaminhoPadraoAnexos { get; set; }

        SQLUtilClass objSQLUtil = new SQLUtilClass();

        #endregion

        #region Métodos

        public DataTable ConsultaEmpresasUsuario(int IDUsuario)
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = IDUsuario;

            return ObjUsuario.ListaEmpresasUsuario();
        }

        public DataTable CarregaUsuarios()
        {
            ChamadoClass objChamado = new ChamadoClass();

            return objChamado.CarregaUsuarios();
        }

        public DataTable ConsultaFornecedores()
        {
            ClienteClasse objCliente = new ClienteClasse();

            return objCliente.RetornaListaClienteFornecedor();
        }

        public DataTable ConsultaStatus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_RECEBIMENTO_STATUS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public DataTable ConsultaRecebimento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_RECEBIMENTO_DETALHE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRecebimento", SqlDbType.Int, 0, "IDRecebimento"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDRecebimento"].Value = this.IDRecebimento;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public bool UsuarioFazParteGrupoAdmRecebimentos(int IDUsuario, string CodigoUsuario)
        {
            usuario Objusuario = new usuario(IDUsuario);

            Objusuario.CodigoUsuario = CodigoUsuario;

            CrmGrupoUsuarioClass GruposUsuario = new CrmGrupoUsuarioClass();

            ParametroGeral objParametroGeral = new ParametroGeral();

            GruposUsuario = Objusuario.ConsultaGrupos("Ativo", objParametroGeral.RetornaValorNumericoParametro("GRUPOADMRECEBIMENTOS"));

            if (GruposUsuario != null)
                return true;
            else
                return false;
        }

        public DataTable ConsultaSetoresUsuario()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_RECEBIMENTO_SETORES_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioLogado;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
            }

            return outputTable;

        }

        public DataTable ConsultaUsuariosSetor()
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_REC_LST_USR_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioLogado", SqlDbType.Int, 0, "IDUsuarioLogado"));


                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@IDUsuarioLogado"].Value = this.IDUsuarioLogado;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;

        }

        public bool UsuarioAdmSetor(int IDUsuario, int IDSetor)
        {
            usuario Objusuario = new usuario(IDUsuario);

            DataTable SetoresUsuario = Objusuario.ConsultaSetoresUsuario();

            if (SetoresUsuario.Rows.Count > 0)
            {
                foreach (DataRow row in SetoresUsuario.Rows)
                {
                    if (Convert.ToInt32(row["IDSetor"]) == IDSetor)
                    {
                        return (row["Administrador"].ToString() == "True");
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public DataTable ConsultaListaRecebimentos()
        {
            var outputTable = new DataTable();

            try
            {
                using (var dbConnection = new SqlConnection(strConec))
                {
                    dbConnection.Open();

                    using (var dbCommand = new SqlCommand("CRM_SP_RETORNA_RECEBIMENTO", dbConnection))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        // 1. Usuário Logado na Aplicação (Obrigatório para segurança/RBAC)
                        dbCommand.Parameters.Add("@IDUsuarioLogado", SqlDbType.Int).Value = this.IDUsuarioLogado;

                        // 2. Filtro de Usuário Selecionado no Combo da Tela (Opcional - envia 0/NULL se não selecionado)
                        dbCommand.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = this.IDUsuario;

                        // Demais parâmetros da tela
                        dbCommand.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = this.IDEmpresa;
                        dbCommand.Parameters.Add("@IDStatus", SqlDbType.Int).Value = this.IDStatus;
                        dbCommand.Parameters.Add("@IDSetor", SqlDbType.Int).Value = this.IDSetor;
                        dbCommand.Parameters.Add("@IDFornecedor", SqlDbType.Int).Value = this.IDFornecedor;

                        dbCommand.Parameters.Add("@DataInicial", SqlDbType.DateTime).Value =
                            (this.DataInicial != DateTime.MinValue && this.DataInicial != null)
                                ? (object)this.DataInicial
                                : DBNull.Value;

                        dbCommand.Parameters.Add("@DataFinal", SqlDbType.DateTime).Value =
                            (this.DataFinal != DateTime.MinValue && this.DataFinal != null)
                                ? (object)this.DataFinal
                                : DBNull.Value;

                        using (var dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return outputTable;
        }

        public void CarregaRecebimento()
        {
            DataTable Recebimento = ConsultaRecebimento();

            if (Recebimento.Rows.Count > 0)
            {
                this.IDUsuario = Convert.ToInt32(Recebimento.Rows[0]["IDResponsavel"]);
                this.IDStatus = Convert.ToInt32(Recebimento.Rows[0]["IDStatus"]);
                this.IDSetor = Convert.ToInt32(Recebimento.Rows[0]["IDSetor"]);
                this.IDFornecedor = Convert.ToInt32(Recebimento.Rows[0]["IDFornecedor"]);
                this.Manual = Convert.ToBoolean(Recebimento.Rows[0]["ManualBit"]);
                this.CNPJ = Recebimento.Rows[0]["CNPJ"].ToString();
                this.NomeFornecedor = Recebimento.Rows[0]["Fornecedor"].ToString();
                this.NumeroNotaFiscal = Recebimento.Rows[0]["NF"].ToString();
                this.DataCriacao = Convert.ToDateTime(Recebimento.Rows[0]["DataRecebimento"]);
                this.Observacao = Recebimento.Rows[0]["Observacao"].ToString();
            }
        }

        public string GravaRecebimento()
        {
            string erro = "";

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_RECEBIMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRecebimento", SqlDbType.Int, 0, "IDRecebimento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioLogado", SqlDbType.Int, 0, "IDUsuarioLogado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDFornecedor", SqlDbType.Int, 0, "IDFornecedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Manual", SqlDbType.Bit, 0, "Manual"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 20, "CNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeFornecedor", SqlDbType.VarChar, 20, "NomeFornecedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroNotaFiscal", SqlDbType.VarChar, 20, "NumeroNotaFiscal"));
                    dbCommand.Parameters.Add(new SqlParameter("@Observacao", SqlDbType.VarChar, 500, "Observacao"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDRecebimento"].Value = this.IDRecebimento;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDUsuarioLogado"].Value = this.IDUsuarioLogado;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@IDFornecedor"].Value = this.IDFornecedor;
                    dbCommand.Parameters["@Manual"].Value = this.Manual;
                    dbCommand.Parameters["@CNPJ"].Value = this.CNPJ;
                    dbCommand.Parameters["@NomeFornecedor"].Value = this.NomeFornecedor;
                    dbCommand.Parameters["@NumeroNotaFiscal"].Value = this.NumeroNotaFiscal;
                    dbCommand.Parameters["@Observacao"].Value = this.Observacao;

                    string exec = objSQLUtil.MontarComandoExec(dbCommand);

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        erro = outputTable.Rows[0]["Erro"].ToString();

                        IDRecebimento = Convert.ToInt32(outputTable.Rows[0]["IDRecebimento"]);
                    }
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public DataTable ConsultaHistoricoEventos()
        {
            ParametroGeral objParametroGeral = new ParametroGeral();

            HistoricosClass objHistorico = new HistoricosClass();

            objHistorico.IDTipoHistorico = objParametroGeral.RetornaValorNumericoParametro("TIPOPADRAORECEBIMENTO");

            return objHistorico.RetornaEventos();
        }

        public DataTable ConsultaHistoricoEventosCategorias(int IDEvento)
        {
            ParametroGeral objParametroGeral = new ParametroGeral();

            HistoricosClass objHistorico = new HistoricosClass();

            objHistorico.IDTipoHistorico = objParametroGeral.RetornaValorNumericoParametro("TIPOPADRAORECEBIMENTO");

            objHistorico.IDEvento = IDEvento;

            return objHistorico.RetornaEventosCategorias();
        }

        public void ConsultaRecebimentoHistorico()
        {
            //Limpa para não trazer lixo
            this.Historico = "";

            DataTable OBJData = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_RECEBIMENTO_HISTORICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRecebimento", SqlDbType.Int, 0, "IDRecebimento"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDRecebimento"].Value = this.IDRecebimento;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        OBJData.Load(dataReader);

                        if (OBJData.Rows.Count > 0)
                        {
                            foreach (DataRow row in OBJData.Rows)
                            {
                                //Carrega icones
                                Historico += "<div class=\"timeline-entry\"> <div class=\"timeline-stat\"> ";
                                Historico += "<div class=\"" + row["TimeLineButonClass"].ToString() + "\"><i class=\"" + row["TimeLineIconClass"].ToString() + "\"></i> ";

                                //Carrega data
                                Historico += "</div><div class=\"timeline-time\"><b>" + row["DataHistorico"].ToString() + "</b></div> " + "</div><div class=\"timeline-label\"> ";

                                //Carrega Título Historico
                                Historico += "<p class=\"mar-no pad-btm\"> <span class=\"" + row["TimeLineTituloClass"].ToString() + "\">" + row["DescricaoEvento"].ToString() + " " + row["DescricaoCategoria"].ToString();

                                //Carrega Corpo Histórico
                                Historico += "</span> por <a href=\"#\" class=\"btn-link btn-md text-semibold\"> ";
                                Historico += row["CodigoUsuario"].ToString() + "</a></p>";
                                Historico += "<div class=\"well well-xs mar-no\"> ";
                                Historico += row["Historico"].ToString();
                                Historico += "</div>";

                                //Fecha Historico
                                Historico += "</div></div>";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string GravaRecebimentoHistorico()
        {
            ParametroGeral objParametroGeral = new ParametroGeral();

            this.IDTipo = objParametroGeral.RetornaValorNumericoParametro("TIPOPADRAORECEBIMENTO");

            string erro = "";

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_RECEBIMENTO_HISTORICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRecebimento", SqlDbType.Int, 0, "IDRecebimento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipo", SqlDbType.Int, 0, "IDTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 500, "Historico"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDRecebimento"].Value = this.IDRecebimento;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioLogado;
                    dbCommand.Parameters["@IDTipo"].Value = this.IDTipo;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;
                    dbCommand.Parameters["@IDCategoria"].Value = this.IDCategoria;
                    dbCommand.Parameters["@Historico"].Value = this.Historico;

                    string exec = objSQLUtil.MontarComandoExec(dbCommand);

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        erro = outputTable.Rows[0]["Erro"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public DataTable RecuperaDadosAnexos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_RECEBIMENTOS_ANEXOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRecebimento", SqlDbType.Int, 0, "IDRecebimento"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDRecebimento"].Value = this.IDRecebimento;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public string GravaArquivoServidor(FileUpload OBJArquivo)
        {
            string diretorio = "";
            string arquivo = "";
            string erro = "";
            string Pasta = "";
            string Extensao = "";
            int cont = 0;

            ParametroGeral objParametroGeral = new ParametroGeral();

            diretorio = objParametroGeral.RetornaValorStringParametro("CAMINHOPADRAOANEXOSRECEBIMENTOS");

            Pasta = "Recebimento_" + this.IDRecebimento.ToString();

            diretorio += "\\" + Pasta + "\\";

            arquivo = diretorio + OBJArquivo.FileName;

            try
            {
                if (OBJArquivo.HasFile == true)
                {
                    //Cria pasta para o chamado caso não exista
                    if (!Directory.Exists(diretorio))
                    {
                        Directory.CreateDirectory(diretorio);
                    }

                    if (!File.Exists(arquivo))
                    {
                        OBJArquivo.PostedFile.SaveAs(arquivo);
                    }
                    else
                    {
                        cont++;

                        while (cont != 0)
                        {
                            cont++;

                            Extensao = Path.GetExtension(arquivo);

                            arquivo = Path.GetFileNameWithoutExtension(arquivo);

                            arquivo = diretorio + arquivo + "_" + cont.ToString() + Extensao;

                            if (!File.Exists(arquivo))
                            {
                                OBJArquivo.SaveAs(arquivo);
                                cont = 0;
                            }
                        }
                    }

                    //Atribui dados do arquivo 
                    this.ArquivoAnexo = arquivo;

                    this.ArquivoExtensao = Path.GetExtension(arquivo);

                    this.NomeArquivo = OBJArquivo.FileName;
                }
                else
                {
                    erro = "Nenhum arquivo selecionado.";
                }
            }
            catch (Exception)
            {
                erro = "Erro ao salvar arquivo.";
            }


            return erro;
        }

        public string GravaDadosAnexos()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_RECEBIMENTOS_ANEXOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRecebimento", SqlDbType.Int, 0, "IDRecebimento"));
                    dbCommand.Parameters.Add(new SqlParameter("@Caminhodestino", SqlDbType.VarChar, 500, "Caminhodestino"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeArquivo", SqlDbType.VarChar, 500, "NomeArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@ExtensaoArquivo", SqlDbType.VarChar, 10, "ExtensaoArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoArquivo", SqlDbType.VarChar, 500, "DescricaoArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioLogado", SqlDbType.Int, 0, "IDUsuarioLogado"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDRecebimento"].Value = this.IDRecebimento;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Caminhodestino"].Value = this.ArquivoAnexo;
                    dbCommand.Parameters["@NomeArquivo"].Value = this.NomeArquivo;
                    dbCommand.Parameters["@ExtensaoArquivo"].Value = this.ArquivoExtensao;
                    dbCommand.Parameters["@DescricaoArquivo"].Value = this.DescricaoArquivo;
                    dbCommand.Parameters["@IDUsuarioLogado"].Value = this.IDUsuarioLogado;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch (Exception)
                {
                    ExcluiDadosAnexosServidor();

                    erro = "Erro na inserção do anexo.";
                }
            }

            return erro;
        }

        public string ExcluiDadosAnexosServidor()
        {
            ParametroGeral objParametroGeral = new ParametroGeral();

            string diretorio = objParametroGeral.RetornaValorStringParametro("CAMINHOPADRAOANEXOSRECEBIMENTOS");

            string Pasta = "Recebimento_" + this.IDRecebimento.ToString();

            diretorio += "\\" + Pasta + "\\";

            string arquivo = diretorio + this.NomeArquivo;

            try
            {
                if (File.Exists(arquivo)) File.Delete(arquivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public string ExcluiDadosAnexos()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_RECEBIMENTOS_ANEXOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRecebimento", SqlDbType.Int, 0, "IDRecebimento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioLogado", SqlDbType.Int, 0, "IDUsuarioLogado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnexo", SqlDbType.Int, 0, "IDAnexo"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDRecebimento"].Value = this.IDRecebimento;
                    dbCommand.Parameters["@IDUsuarioLogado"].Value = this.IDUsuarioLogado;
                    dbCommand.Parameters["@IDAnexo"].Value = this.IDAnexo;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch (Exception)
                {
                    erro = "Erro na inserção de dados do projeto do chamado.";
                }
            }

            return erro;
        }

        #endregion
    }
}