using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VendasWeb
{
    public class funcoes : GerencialVendas.clsConexao
    {

        public string Usucod { get; set; }

        public Boolean ExecutaSQL(string paramQuery)
        {
            Boolean blnResultado;
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                using (SqlCommand dbCommand = new SqlCommand(paramQuery, dbConnection))
                {
                    try
                    {
                        dbCommand.Connection.Open();

                        dbCommand.ExecuteNonQuery();

                        blnResultado = true;
                    }
                    catch (Exception)
                    {
                        blnResultado = false;
                    }
                }
            }
            return blnResultado;
        }

        public Boolean ConfirmaResposta()
        {
            Boolean Resultado;
            Resultado = false;

            if (MessageBox.Show("Confirma cancelamento pedido?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Resultado = true;
            }

            return Resultado;
        }

        public string ExecutaSqlReader(string paramSQL, string Metodo)
        {
            string strValue = "0";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {                
                using (SqlCommand dbCommand = new SqlCommand(paramSQL, dbConnection))
                {
                    dbConnection.Open();
                    try
                    {
                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                strValue = Convert.ToString(dataReader[0]);
                            }
                        }
                    }
                    catch
                    {
                        strValue = "FAVOR VERIFICAR A CONSULTA do método " + Metodo;
                    }
                }
            }
            return strValue;
        }

        public void PreencheDropList(System.Web.UI.WebControls.CheckBoxList drpList, string paramSQL, string itemAdicional)
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {                
                using (SqlDataAdapter DataAdapter = new SqlDataAdapter(paramSQL, dbConnection))
                {
                    dbConnection.Open();
                    DataSet dtsetCombo = new DataSet();
                    drpList.Items.Clear();

                    //Roda a query
                    DataAdapter.Fill(dtsetCombo, "tbUsuarioMenu");
                    drpList.DataMember = "tbUsuarioMenu";
                    drpList.DataTextField = dtsetCombo.Tables[0].Columns[1].ColumnName;
                    drpList.DataValueField = dtsetCombo.Tables[0].Columns[0].ColumnName;
                    drpList.DataSource = dtsetCombo;
                    drpList.DataBind();

                    if (itemAdicional != "" || itemAdicional == null)
                    {
                        drpList.Items.Add(itemAdicional);
                        drpList.Items[drpList.Items.Count - 1].Value = drpList.Items.Count.ToString();

                        //Colocado Validação para Que So coloque o Index em 0 quando tiver mais de 1 Item
                        if (drpList.Items.Count > 1)
                        {
                            drpList.SelectedIndex = drpList.Items.Count - 1;
                        }
                    }
                }
            }            
        }

        //Metodo Para Retornar Condicoes de Pagamento
        public DataTable Consulta_Condicao_Pagamento(string codigoEntidade, string EmpCod)
        {
            string strSql = "";
            strSql = "select CondPagCod, CondPagNome from crk_WebRep_Cond_Pag('" + codigoEntidade.ToString() + "', 0, '" + EmpCod.ToString() + "')";

            return Executa_DataTable(strSql, "Consulta_Condicao_Pagamento");
        }

        public DataTable Consulta_ListaStatus_Ped_Venda()
        {
            string strSql = "";
            strSql = "select StatPedVendaCod, StatPedVendaDescr from STAT_PED_VENDA";

            return Executa_DataTable(strSql, "Consulta_ListaStatus_Ped_Venda");
        }

        public DataTable Consulta_Tab_PV_Vendedor(string codigoEmpresa, string vendCod)
        {
            string strSql = "";       

            //Alteracao feita para sempre trazer tabela de preco da empresa 1 se nao for empresa 2
            if (codigoEmpresa != "2")
            {
                strSql = "select tabpvcod, tabpvnome from crk_WebRep_TabPreco_Vendedor('" + vendCod.ToString() + "','1')";
            }
            else
            {
                strSql = "select tabpvcod, tabpvnome from crk_WebRep_TabPreco_Vendedor('" + vendCod.ToString() + "','" + codigoEmpresa.ToString() + "')";
            }

            return Executa_DataTable(strSql, "Consulta_Tab_PV_Vendedor");
        }

        public DataTable Consulta_Operacao_Ped_Venda()
        {
            string strSql = "";

            strSql = "select TIPOFATOPERACAO, TIPOFATOPERACAO from TIPO_FATURAMENTO GROUP BY TIPOFATOPERACAO";

            return Executa_DataTable(strSql, "Consulta_Operacao_Ped_Venda");
        }

        public DataTable Consulta_Especie_Ped_Venda()
        {
            string strSql = "";

            strSql = "select TIPOFATESPECIE, TIPOFATESPECIE from TIPO_FATURAMENTO GROUP BY TIPOFATESPECIE";

            return Executa_DataTable(strSql, "Consulta_Especie_Ped_Venda");
        }

        public DataTable Consulta_Empresa(string usuario)
        {
            string strSql = "";

            strSql += "select EU.EmpCod, EU.EmpCod +' - '+EF.EmpNomeFant as EmpNome from EMP_FIL_USUARIO EU, EMPRESA_FILIAL EF where ";
            strSql += "EU.EmpCod=EF.EmpCod and UsuCod = '" + usuario.ToString() + "' and EU.EmpCod<>'1.99' ";

            return Executa_DataTable(strSql, "Consulta_Empresa");
        }

        public DataTable Consultar_Empresas()
        {
            string strSql = "";

            strSql += "select EmpCod, EmpCod +' - '+EmpNomeFant as EmpNome from EMPRESA_FILIAL ";

            return Executa_DataTable(strSql, "Consulta_Empresa");
        }

        public DataTable Consulta_Linha_Produto()
        {
            string strSql = "";

            strSql = "select coalesce(LinhaProduto, '') as LinhaProduto, coalesce(LinhaProduto, '') as DescProduto from USER_LINHA_PRODUTO ";
            strSql += "where coalesce(LinhaProduto, '*')<>'*' group by LinhaProduto";

            return Executa_DataTable(strSql, "Consulta_Linha_Produto");
            
        }

        public String Consulta_Vendedor_Entidade(string codigoEntidade)
        {
            string strSql = "";
            string CodVendedor = "";
            strSql = "select VendCod from VEND_ENT where EntCod='" + codigoEntidade.ToString() + "'";

            CodVendedor = ExecutaSqlReader(strSql, "Consulta_Vendedor_Entidade");

            return CodVendedor;
        }

        public String Consulta_CodNome_Produto(string ProdCodEstr)
        {
            string strSql = "";
            string DescProd = "";
            strSql = "select ProdCodEstr + ' - ' + ProdNome as ProdNome from PRODUTO where ProdCodEstr ='" + ProdCodEstr + "'";

            DescProd = ExecutaSqlReader(strSql, "Consulta_CodNome_Produto");
            return DescProd;
        }

        public String Consulta_Nome_Produto(string ProdCodEstr)
        {
            string strSql = "";
            string DescProd = "";
            strSql = "select ProdNome from PRODUTO where ProdCodEstr ='" + ProdCodEstr + "'";

            DescProd = ExecutaSqlReader(strSql, "Consulta_Nome_Produto");
            return DescProd;
        }

        public String Consulta_Unidade_Medida(string ProdCodEstr)
        {
            string strSql = "";
            string UnidMed = "";
            strSql = "select ProdUnidMedCod from PROD_UNID_MED where ProdCodEstr ='" + ProdCodEstr + "' and ProdUnidMedPos='1'";

            UnidMed = ExecutaSqlReader(strSql, "Consulta_Unidade_Medida");
            return UnidMed;
        }

        public String Consulta_Nome_Transportadora(string EntCod)
        {
            string strSql = "";
            string NomeTransp = "";
            strSql = "select EntNome from ENTIDADE where EntCod ='" + EntCod.ToString() + "'";

            NomeTransp = ExecutaSqlReader(strSql, "Consulta_Nome_Transportadora");
            return NomeTransp;
        }

        public String Consulta_CodVendedorAtivo_Usuario(string Usucod)
        {
            string strSql = "";
            string VendCod = "";
            strSql = "select VendCod from VENDEDOR where UsuCod = '" + Usucod.ToString() + "' and VendStat != 'Desligado'";

            VendCod = ExecutaSqlReader(strSql, "Consulta_CodVendedorAtivo_Usuario");
            return VendCod;
        }

        public String Consulta_Status_Ped_Venda(string codigoEmpresa, string numeroPedido)
        {
            string strSql = "";
            string Status = "";
            strSql = "select StatPedVendaCod from PED_VENDA where EmpCod ='" + codigoEmpresa.ToString() + "' and PedVendaNum='" + numeroPedido.ToString() + "'";

            Status = ExecutaSqlReader(strSql, "Consulta_Status_Ped_Venda");
            return Status;
        }

        public Int32 Consulta_Quantidade_Entidade()
        {
            string strSql = "";
            Int32 Quantidade = 0;
            strSql = "select count(*) as CNT from ENTIDADE WHERE EntStatDescr = 'Ativo'";

            Quantidade = Convert.ToInt32(ExecutaSqlReader(strSql, "Consulta_Quantidade_Entidade"));
            return Quantidade;
        }


        public DataTable Consulta_Usuario()
        {
            string strSql = "";
            strSql = "select UsuCod, Usucod from USUARIO with(nolock) where UsuStat='ativo'";

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand(strSql, dbConnection);

                //Deixa o Timeout da consulta com cerca de 4 minutos
                dbCommand.CommandTimeout = 99999;

                SqlDataReader dataReader = dbCommand.ExecuteReader();

                outputTable.Load(dataReader);

                dataReader.Close();
            }

            return outputTable;

        }

        public Int32 Consulta_Quantidade_Entidade_Vendedor(string UsuCod)
        {
            string strSql = "";
            Int32 Quantidade = 0;
            strSql = "select count(*) as CNT from ENTIDADE E INNER JOIN VEND_ENT VE ON E.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod Where E.EntStatDescr = 'Ativo' and Ven.UsuCod='" + UsuCod.ToString() + "'";

            Quantidade = Convert.ToInt32(ExecutaSqlReader(strSql, "Consulta_Quantidade_Entidade_Vendedor"));
            return Quantidade;
        }

        public DataTable Executa_DataTable(String strSql, string Metodo)
        {
            DataTable outputTable = new DataTable();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand(strSql, dbConnection))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Deixa o Timeout da consulta com cerca de 4 minutos
                        dbCommand.CommandTimeout = 340;

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no método " + Metodo);
                }
                return outputTable;
            }
        }

        public string ExecutaSqlReader(string paramSQL)
        {

            string strValue = "0";

            //Cria Pool de Conexão
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                SqlCommand dbCommand = new SqlCommand(paramSQL, dbConnection);


                //Abre a conexao
                dbConnection.Open();

                //Deixa o Timeout da consulta com cerca de 4 minutos
                dbCommand.CommandTimeout = 99999;


                SqlDataReader dataReader = dbCommand.ExecuteReader();

                if (dataReader.Read())
                {

                    strValue = Convert.ToString(dataReader[0]);

                }

                dataReader.Close();
            }


            return strValue;


        }

        public string FormataData(string data)
        {
            if (data != "")
            {
                string[] DataDig = data.Split('/');
                string Dia = DataDig[0];
                string Mes = DataDig[1];
                string Ano = DataDig[2];

                return Ano + '-' + Mes + '-' + Dia;
            }
            else 
            {
                return "";
            }
        }

        public int FormataDataComparacao(string data)
        {
            if (data != "")
            {
                string[] DataDig = data.Split('/');
                string Dia = DataDig[0];
                string Mes = DataDig[1];
                string Ano = DataDig[2];

                data = Ano + Mes + Dia;

                return Convert.ToInt32(data);
            }
            else
            {
                return 0;
            }
        }




        public DataTable Consulta_CEP(string CEP)
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_consulta_CEP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CEPCOD", SqlDbType.VarChar, 30, "CEPCOD"));


                    dbCommand.Parameters["@CEPCOD"].Value = CEP;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }

        public DataTable Mostra_Cidade(string CidCod)
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_Mostra_Cidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 30, "CidCod"));


                    dbCommand.Parameters["@CidCod"].Value = CidCod;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }


        public DataTable Gera_Codigo(string Tabela)
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_GERA_CODIGO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Tabela", SqlDbType.VarChar, 230, "Tabela"));


                    dbCommand.Parameters["@Tabela"].Value = Tabela;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }


        public DataTable Consulta_Cidade(string UfSigla)
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_consulta_Cidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UfSigla", SqlDbType.VarChar, 5, "UfSigla"));


                    dbCommand.Parameters["@UfSigla"].Value = UfSigla;



                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }


        public DataTable Consulta_Estado()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_consulta_Estado", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {

            }

            return outputTable;
        }


        public DataTable Consulta_Regime_Especial()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_REGIME_ESPECIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }


        public string Valida_CPF_CNPJ(string CPF_CNPJ, string EntCod)
        {

            DataTable outputTable = new DataTable();
            string Retorno = "";

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_VALIDA_CPF_CNPJ", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TEXTO", SqlDbType.VarChar, 30, "TEXTO"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 150, "EntCod"));


                    dbCommand.Parameters["@TEXTO"].Value = CPF_CNPJ;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["Validacao"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Validação do CPF/CNPJ";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Validação do CPF/CNPJ. Contactar o Suporte.";
            }

            return Retorno;

        }


        public bool Valida_Email(string email)
        {

            Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            if (rg.IsMatch(email))
            {
                return true; //Email Valido
            }
            else
            {
                return false; //Email Invalido
            }

        }


        public string ConverteByteImageUrl(byte[] Imagem)
        {
            if (Imagem != null)
            {
                try
                {

                    //Converte Byte[] para Imagem
                    string base64String = Convert.ToBase64String(Imagem, 0, Imagem.Length);
                    return "data:image/jpeg;base64," + base64String;
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }

        public string consultaEmail(string usuario)
        {
            string email = "";
            string strSQL = "";


            strSQL = "select UsuEmail from USUARIO with(nolock) where UsuCod='" + usuario.ToString() + "'";

            email = ExecutaSqlReader(strSQL);

            return email;
        }

        public DataTable Consulta_Vendedor(string usuCod)
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_consulta_usuario_vendedor", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                dbCommand.Parameters["@UsuCod"].Value = usuCod;

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
                dataReader.Close();
            }
            return outputTable;
        }

        public DataTable Consulta_Gestores_Classe()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_CRM_User_TB_GestoresClasses_Listar", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }

        }
    }
}