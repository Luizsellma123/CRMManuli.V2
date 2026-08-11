using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb
{
    public class usuario : GerencialVendas.clsConexao
    {

        public string CodigoUsuario { get; set; }

        public List<ClasseMenus> ListaMenus { get; set; }
        public List<CrmGrupoUsuarioClass> ListaCrmGrupoUsuarioClass { get; set; }
        public List<UsuarioVendedoresClass> ListaVendedorClass { get; set; }

        public int IDUsuario { get; set; }
        public int IDEmpresa { get; set; }
        public int IDVendedor { get; set; }
        public int IDSetor { get; set; }
        public int IDTipoVendedor { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string Status { get; set; }
        public string Senha { get; set; }

        public string Filtro { get; set; }
        public string Operacao { get; set; }
        public string OperacaoDois { get; set; }

        public int IDGrupo { get; set; }
        public int IDMenu { get; set; }
        public bool Ativo { get; set; }
        public bool Administrador { get; set; }
        public bool AdministradorSetor { get; set; }

        public int IDUsuarioSAP { get; set; }
        public string NomeUsuarioSAP { get; set; }
        public string CodigoUsuarioSAP { get; set; }
        public string SenhaUsuarioSAP { get; set; }
        public string OperacaoSAP { get; set; }

        public usuario()
        {
            //Verifica se esta instanciado
            if (this.ListaVendedorClass == null)
            {
                this.ListaVendedorClass = new List<UsuarioVendedoresClass>();
            }
        }

        public usuario(int IDUsuario)
        {
            this.IDUsuario = IDUsuario;
        }

        public string gravaUsuario()
        {

            string strSql = "";
            Boolean teste;
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "UPDATE USUARIO SET UsuSenhaInternet='" + this.senha.ToString() + "', UsuEmail='" + this.email.ToString() + "' WHERE UsuCod='" + this.nome.ToString() + "'";

                teste = mdlFuncoes.ExecutaSQL(strSql);

                if (teste != true)
                {
                    return "Erro ao cadastrar usuário.";
                }
                else
                {
                    return "";
                }
            }
        }

        public string consultaUsuario(string Usunome)
        {
            string strSql = "";
            int cont = 0;
            funcoesBD mdlFuncoes = new funcoesBD();

            cont = this.consultaSenha(Usunome);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                if (cont == 0)
                {
                    strSql = "select count(*) as CNT from USUARIO WHERE UsuCod='" + Usunome.ToString() + "'";

                    cont = (int)Convert.ToInt32(mdlFuncoes.ExecutaSqlReader(strSql));

                    if (cont <= 0)
                    {
                        return "Usuário não cadastrado.";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "Usuário já possui senha cadastrada.";
                }
            }
        }

        public int consultaSenha(string Usunome)
        {
            string strSql = "";
            int cont = 0;
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select count(*) as CNT from USUARIO WHERE UsuCod='" + Usunome.ToString() + "' and UsuSenhaInternet<>'' ";

                cont = (int)Convert.ToInt32(mdlFuncoes.ExecutaSqlReader(strSql));
            }

            return cont;

        }

        public string consultaEmail(string Usunome)
        {
            string strSql = "";
            string email = "";
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select UsuEmail from USUARIO WHERE UsuCod='" + Usunome.ToString() + "' and UsuSenhaInternet<>'' ";

                email = (string)mdlFuncoes.ExecutaSqlReader(strSql).ToString();
            }
            return email;
        }

        public string consultaValorSenha(string Usunome)
        {
            string strSql = "";
            string senha = "";
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select UsuSenhaInternet from USUARIO WHERE UsuCod='" + Usunome.ToString() + "' and UsuSenhaInternet<>'' ";

                senha = mdlFuncoes.ExecutaSqlReader(strSql);
            }

            return senha;

        }

        public string consultaValorUsuario(string Usunome)
        {
            string strSql = "";
            int cont = 0;

            funcoesBD mdlFuncoes = new funcoesBD();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select count(*) as CNT from USUARIO WHERE UsuCod='" + Usunome.ToString() + "'";

                cont = (int)Convert.ToInt32(mdlFuncoes.ExecutaSqlReader(strSql));

                if (cont <= 0)
                {
                    return "Usuário não cadastrado.";
                }
                else
                {
                    return "";
                }
            }
        }


        public int ConsultaVendedorUsuario(string UsuCod)
        {
            string strSql = "";
            int VendCod = 0;

            funcoesBD mdlFuncoes = new funcoesBD();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select CCU.IDVendedor from CRM_CADASTRO_USUARIO CCU INNER JOIN CRM_VENDEDOR CV ON CCU.IDVendedor=CV.IDVendedor where CCU.CodigoUsuario = '" + UsuCod + "'";

                VendCod = Convert.ToInt32(mdlFuncoes.ExecutaSqlReader(strSql) ?? "0");

                return VendCod;
            }
        }

        public void ConsultaMenus()
        {
            DataTable outputTable = new DataTable();
            //ClasseMenus OBJMenu = new ClasseMenus();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_MONTA_MENU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 100, "CodigoUsuario"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                ClasseMenus OBJMenu = new ClasseMenus();
                                OBJMenu.NomeMenu = row["Nome"].ToString();
                                OBJMenu.Endereco = row["Endereco"].ToString();
                                OBJMenu.Administrador = row["Administrador"].ToString();
                                OBJMenu.IconeCSS = row["IconeCSS"].ToString();

                                //Verifica se esta instanciado
                                if (this.ListaMenus == null)
                                {
                                    this.ListaMenus = new List<ClasseMenus>();
                                }
                                this.ListaMenus.Add(OBJMenu);
                            }
                        }
                    }
                }
            }
            catch
            {

            }

        }


        public void ConsultaGrupos(string _Status)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_GRUPOS_USUARIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 100, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 100, "Status"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@Status"].Value = _Status;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                CrmGrupoUsuarioClass ObjCrmGrupoUsuarioClass = new CrmGrupoUsuarioClass();
                                ObjCrmGrupoUsuarioClass.IDGrupo = Convert.ToInt32(row["IDGrupo"].ToString());
                                ObjCrmGrupoUsuarioClass.Nome = row["Nome"].ToString();
                                ObjCrmGrupoUsuarioClass.Status = row["Status"].ToString();
                                ObjCrmGrupoUsuarioClass.Administrador = row["Administrador"].ToString();

                                //Verifica se esta instanciado
                                if (this.ListaCrmGrupoUsuarioClass == null)
                                {
                                    this.ListaCrmGrupoUsuarioClass = new List<CrmGrupoUsuarioClass>();
                                }

                                this.ListaCrmGrupoUsuarioClass.Add(ObjCrmGrupoUsuarioClass);
                            }
                        }
                    }
                }
            }
            catch
            {

            }

        }

        public CrmGrupoUsuarioClass ConsultaGrupos(string _Status, int IDGrupo)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_GRUPOS_USUARIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 100, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 100, "Status"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@Status"].Value = _Status;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                if (row["IDGrupo"].ToString() == IDGrupo.ToString())
                                {
                                    CrmGrupoUsuarioClass ObjCrmGrupoUsuarioClass = new CrmGrupoUsuarioClass();
                                    ObjCrmGrupoUsuarioClass.IDGrupo = Convert.ToInt32(row["IDGrupo"].ToString());
                                    ObjCrmGrupoUsuarioClass.Nome = row["Nome"].ToString();
                                    ObjCrmGrupoUsuarioClass.Status = row["Status"].ToString();
                                    ObjCrmGrupoUsuarioClass.Administrador = row["Administrador"].ToString();

                                    return ObjCrmGrupoUsuarioClass;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public int AcessoCadastroCliente()
        {
            int cont = 0;

            string strSql = "";
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                //strSql = "select(select count(*) from CRM_VENDEDOR CV INNER JOIN CRM_CADASTRO_USUARIO CCU ON CV.IDUsuario = CCU.IDUsuario where CV.IDClasse = 5 and CCU.CodigoUsuario = '" + this.CodigoUsuario.ToString() + "' and CCU.IDUsuario not in (18)) +(select count(*) from CRM_GRUPOS_USUARIOS CG INNER JOIN CRM_CADASTRO_USUARIO CCU ON CG.IDUsuario = CCU.IDUsuario where CG.IDGrupo in ('4','7','8') and CCU.CodigoUsuario = '" + this.CodigoUsuario.ToString() + "' and CCU.IDUsuario not in (18))";
                //cont = Convert.ToInt32(mdlFuncoes.ExecutaSqlReader(strSql) ?? "0");

                cont = 1;

            }

            return cont;

        }

        public void CarregaDadosPrincipais()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DADOS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDUsuario = Convert.ToInt32(row["IDUsuario"]);
                                this.nome = row["Nome"].ToString();
                                this.email = row["Email"].ToString();
                                this.telefone = row["Telefone"].ToString();
                                this.Status = row["Status"].ToString();
                                this.Senha = row["Senha"].ToString();
                            }

                            //Recupera os vendedores
                            //Consulta_Vendedores_Usuario();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public string AtualizaDadosUsuario()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_DADOS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 8000, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Senha", SqlDbType.VarChar, 8000, "Senha"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@Nome"].Value = this.nome;
                    dbCommand.Parameters["@Email"].Value = this.email;
                    dbCommand.Parameters["@Telefone"].Value = this.telefone;
                    dbCommand.Parameters["@Senha"].Value = this.senha;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao do usuário.";
                }
            }

            return erro;
        }

        public int ValidaPeriodos()
        {
            int cont = 0;

            string strSql = "";
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select count(*) from CRM_SIMULADOR_PERIODO WHERE '" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + "' Between DataInicio and DataFim";
                cont = Convert.ToInt32(mdlFuncoes.ExecutaSqlReader(strSql) ?? "0");

            }

            return cont;

        }

        public DataTable RetornaUsuarios()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIOS_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }


            catch
            {

            }

            return outputTable;
        }

        public DataTable RetornaEmpresas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMPRESAS_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }

            catch
            {

            }

            return outputTable;
        }

        public DataTable ListaUsuarios()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_USUARIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "@Status"));

                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@Status"].Value = this.Status;

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

        public DataTable Consulta_Vendedores()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_CODIGOS_VENDEDORES_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public DataTable Consulta_Setores()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_SETORES", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public DataTable Consulta_TiposVendedores()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_TIPO_VENDEDOR", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public void Consulta_Vendedores_Usuario()
        {
            DataTable outputTable = new DataTable();

            //Limpa caso tenha lixo de memória
            this.ListaVendedorClass.Clear();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_VENDEDOR_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        UsuarioVendedoresClass OBJVendedor = new UsuarioVendedoresClass();
                        OBJVendedor.IDVendedorNovo = Convert.ToInt32(row["IDVendedor"]);
                        OBJVendedor.VendNome = row["NomeVendedor"].ToString();

                        //Verifica se esta instanciado
                        if (this.ListaVendedorClass == null)
                        {
                            this.ListaVendedorClass = new List<UsuarioVendedoresClass>();
                        }
                        this.ListaVendedorClass.Add(OBJVendedor);
                    }
                }
            }

        }

        public DataTable ConsultaVendedoresUsuario()
        {
            DataTable outputTable = new DataTable();

            //Limpa caso tenha lixo de memória
            this.ListaVendedorClass.Clear();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_VENDEDOR_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
            }

            return outputTable;

        }

        public DataTable ConsultaSetoresUsuario()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_SETORES_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
            }

            return outputTable;

        }

        public DataTable ConsultaTipoVendedorUsuario()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_TIPO_VENDEDOR_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
            }

            return outputTable;

        }

        public string INCLUI_Vendedor_Lista(UsuarioVendedoresClass OBJVendedor)
        {
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIO_VENDEDOR", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDvendedor", SqlDbType.Int, 0, "@IDvendedor"));

                dbCommand.Parameters["@IDvendedor"].Value = OBJVendedor.IDVendedorNovo;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = "Vendedor já relacionado ao usuário " + row["CodigoUsuario"].ToString() + ".";
                    }
                }
            }

            if (erro == "")
            {
                //Verifica se esta instanciado
                if (this.ListaVendedorClass == null)
                {
                    this.ListaVendedorClass = new List<UsuarioVendedoresClass>();
                }

                //Verfica se usuário já existe na lista, caso não exista adiciona
                if (this.ListaVendedorClass.Count(x => x.IDVendedorNovo == OBJVendedor.IDVendedorNovo) <= 0)
                {
                    this.ListaVendedorClass.Add(OBJVendedor);
                }
            }

            return erro;
        }

        public string GravaDadosUsuario()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            DataTable DadosLista = new DataTable();

            //Recupera dados da lista e converte em DataTable
            DadosLista = ObjUtilClass.ConvertToDataTable(this.ListaVendedorClass);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DADOS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 8000, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Senha", SqlDbType.VarChar, 8000, "Senha"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Vendedores", SqlDbType.Structured, 0, "@Vendedores"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@Nome"].Value = this.nome;
                    dbCommand.Parameters["@Email"].Value = this.email;
                    dbCommand.Parameters["@Telefone"].Value = this.telefone;
                    dbCommand.Parameters["@Senha"].Value = this.senha;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;
                    dbCommand.Parameters["@Vendedores"].Value = DadosLista;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao do usuário.";
                }
            }

            return erro;
        }

        public string GravaDadosPrincipaisUsuario()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DADOS_USUARIO_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDUsuario", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 8000, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Senha", SqlDbType.VarChar, 8000, "Senha"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@Nome"].Value = this.nome;
                    dbCommand.Parameters["@Email"].Value = this.email;
                    dbCommand.Parameters["@Telefone"].Value = this.telefone;
                    dbCommand.Parameters["@Senha"].Value = this.senha;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.IDUsuario = (int)dbCommand.Parameters["@IDUsuario"].Value;
                    this.Operacao = "Alteracao";

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao do usuário.";
                }
            }

            return erro;
        }

        public string GravaDadosVendedorUsuario()
        {
            DataTable outputTable = new DataTable();
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            //Nas operações de inclusão e alteração precisa validar se o usuário já não está na lista
            if (this.OperacaoDois == "inclusao" || this.OperacaoDois == "alteracao")
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIO_VENDEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDvendedor", SqlDbType.Int, 0, "@IDvendedor"));

                    dbCommand.Parameters["@IDvendedor"].Value = this.IDVendedor;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = "Vendedor já relacionado ao usuário " + row["CodigoUsuario"].ToString() + ".";
                        }
                    }
                }
            }

            if (erro == "")
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    dbConnection.Open();
                    try
                    {
                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_VENDEDOR_USUARIO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDVendedor", SqlDbType.Int, 0, "IDVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                        dbCommand.Parameters["@IDVendedor"].Value = this.IDVendedor;
                        dbCommand.Parameters["@Operacao"].Value = this.OperacaoDois;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                    catch (Exception ex)
                    {
                        erro = "Erro na atualizacao do vendedor do usuário.";
                    }
                }
            }

            return erro;
        }

        public string GravaDadosSetoresUsuario()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SETOR_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Administrador", SqlDbType.Int, 0, "Administrador"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@Operacao"].Value = this.OperacaoDois;
                    dbCommand.Parameters["@Administrador"].Value = this.AdministradorSetor;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao do setor do usuário.";
                }
            }

            return erro;
        }

        public string GravaDadosTipoVendorUsuario()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TIPO_VENDEDOR_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoVendedor", SqlDbType.Int, 0, "IDTipoVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDTipoVendedor"].Value = this.IDTipoVendedor;
                    dbCommand.Parameters["@Operacao"].Value = this.OperacaoDois;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao do tipo de vendedor do usuário.";
                }
            }

            return erro;
        }

        public DataTable RetornaGruposUsuario()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_GRUPOS_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public DataTable RetornaMenusUsuario()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_MENUS_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public DataTable RetornaEmpresasUsuario()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_RETORNA_EMPRESAS_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }

        }

        public string GravaGruposUsuario()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            DataTable DadosLista = new DataTable();

            //Recupera dados da lista e converte em DataTable
            DadosLista = ObjUtilClass.ConvertToDataTable(this.ListaVendedorClass);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_GRUPOS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Int, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Administrador", SqlDbType.Int, 0, "Administrador"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;
                    dbCommand.Parameters["@Ativo"].Value = Convert.ToInt32(this.Ativo);
                    dbCommand.Parameters["@Administrador"].Value = Convert.ToInt32(this.Administrador);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos menus do usuário.";
                }
            }

            return erro;
        }

        public string GravaMenusUsuario()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            DataTable DadosLista = new DataTable();

            //Recupera dados da lista e converte em DataTable
            DadosLista = ObjUtilClass.ConvertToDataTable(this.ListaVendedorClass);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_MENUS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Int, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Administrador", SqlDbType.Int, 0, "Administrador"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDMenu"].Value = this.IDMenu;
                    dbCommand.Parameters["@Ativo"].Value = Convert.ToInt32(this.Ativo);
                    dbCommand.Parameters["@Administrador"].Value = Convert.ToInt32(this.Administrador);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos menus do usuário.";
                }
            }

            return erro;
        }

        public string GravaSetoresUsuario()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            DataTable DadosLista = new DataTable();

            //Recupera dados da lista e converte em DataTable
            DadosLista = ObjUtilClass.ConvertToDataTable(this.ListaVendedorClass);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_USUARIOS_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Int, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Administrador", SqlDbType.Int, 0, "Administrador"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@Ativo"].Value = Convert.ToInt32(this.Ativo);
                    dbCommand.Parameters["@Administrador"].Value = Convert.ToInt32(this.Administrador);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos setores do usuário.";
                }
            }

            return erro;
        }

        public string GravaUsuariosGrupo()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            DataTable DadosLista = new DataTable();

            //Recupera dados da lista e converte em DataTable
            DadosLista = ObjUtilClass.ConvertToDataTable(this.ListaVendedorClass);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_USUARIOS_GRUPO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Int, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Administrador", SqlDbType.Int, 0, "Administrador"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;
                    dbCommand.Parameters["@Ativo"].Value = Convert.ToInt32(this.Ativo);
                    dbCommand.Parameters["@Administrador"].Value = Convert.ToInt32(this.Administrador);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos menus do usuário.";
                }
            }

            return erro;
        }

        public string GravaUsuariosMenu()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            DataTable DadosLista = new DataTable();

            //Recupera dados da lista e converte em DataTable
            DadosLista = ObjUtilClass.ConvertToDataTable(this.ListaVendedorClass);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_USUARIOS_MENUS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Int, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Administrador", SqlDbType.Int, 0, "Administrador"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDMenu"].Value = this.IDMenu;
                    dbCommand.Parameters["@Ativo"].Value = Convert.ToInt32(this.Ativo);
                    dbCommand.Parameters["@Administrador"].Value = Convert.ToInt32(this.Administrador);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos menus do usuário.";
                }
            }

            return erro;
        }

        public string AdicionaEmpresasUsuario()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_INCLUI_EMPRESAS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = IDEmpresa;
                    dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@VErro"].Value;

                }
                catch (Exception ex)
                {

                }

                return erro;
            }

        }

        public void ExcluiEmpresasUsuario()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_EXCLUI_EMPRESAS_USUARIO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDEmpresa"].Value = IDEmpresa;
                dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();
            }

        }

        public DataTable ListaEmpresasUsuario()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_EMPRESAS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.VarChar, 8000, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable ListaVendedores()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_VENDEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        public void CarregaDadosUsuarioSAP()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DADOS_USUARIO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.VarChar, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDUsuarioSAP = Convert.ToInt32(row["IDUsuarioSAP"]);
                                this.CodigoUsuarioSAP = row["CodigoUsuarioSAP"].ToString();
                                this.NomeUsuarioSAP = row["NomeUsuarioSAP"].ToString();
                                this.SenhaUsuarioSAP = row["SenhaUsuarioSAP"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public string GravaDadosUsuarioSAP()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DADOS_USUARIO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioCRM", SqlDbType.Int, 0, "IDUsuarioCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioSAP", SqlDbType.Int, 0, "IDUsuarioSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuarioSAP", SqlDbType.VarChar, 8000, "CodigoUsuarioSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeUsuarioSAP", SqlDbType.VarChar, 8000, "NomeUsuarioSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@SenhaUsuarioSAP", SqlDbType.VarChar, 8000, "SenhaUsuarioSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuarioCRM"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDUsuarioSAP"].Value = this.IDUsuarioSAP;
                    dbCommand.Parameters["@CodigoUsuarioSAP"].Value = this.CodigoUsuarioSAP;
                    dbCommand.Parameters["@NomeUsuarioSAP"].Value = this.NomeUsuarioSAP;
                    dbCommand.Parameters["@SenhaUsuarioSAP"].Value = this.SenhaUsuarioSAP;
                    dbCommand.Parameters["@Operacao"].Value = this.OperacaoSAP;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.OperacaoSAP = "alteracao";

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public DataTable RetornaListaVendedores()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_VENDEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        public string RetornaVendedorDoCliente(string CodigoCliente)
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_VENDEDOR_DO_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoCliente", SqlDbType.VarChar, 8000, "CodigoCliente"));

                    dbCommand.Parameters["@CodigoCliente"].Value = CodigoCliente;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["IDVendedor"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return "0";
        }

        public string RecuperaEmailUsuario()
        {
            string Email = "";
            string erro = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMAIL_USUARIO_ID", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                Email = row["Email"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return Email;
        }

        public int RecuperaUsuarioEmail()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIO_EMAIL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));

                    dbCommand.Parameters["@Email"].Value = this.email;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                return Convert.ToInt32(row["IDUsuario"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return 0;
        }

        public string RecuperaUsuarioPeloEmail()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RECUPERA_CRM_CADASTRO_USUARIO_CodigoUsuario", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));

                    dbCommand.Parameters["@Email"].Value = this.email;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                return row["CodigoUsuario"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return "";
        }

        public string RecuperaSenhaPeloEmail()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RECUPERA_CRM_CADASTRO_USUARIO_Senha", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));

                    dbCommand.Parameters["@Email"].Value = this.email;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                return row["Senha"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return "";
        }

        public string RecuperaUsuarioPeloCodigo()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RECUPERA_CRM_CADASTRO_USUARIO_CodigoUsuario", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));

                    dbCommand.Parameters["@Email"].Value = this.email;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                return row["CodigoUsuario"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return "";
        }
    }
}