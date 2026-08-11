using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb
{
    public class clsApontamento : GerencialVendas.clsConexao
    {

        public static SqlConnection dbConnection = new SqlConnection();

        funcoes mdlfuncoes = new funcoes();

        //public funcoes mdlfuncoes { get; set; }
        public string usuCod { get; set; }
        public string EmpCod { get; set; }
        public string OrdProducNum { get; set; }
        public string ProdOperSeq { get; set; }
        public string MenorProdOperSeq { get; set; }
        public string AtivGrpCodEstr { get; set; }
        public string AtivGrpNome { get; set; }
        public string CCtrlCodEstr { get; set; }
        public string OperCod { get; set; }
        public string CCtrlNome { get; set; }
        public string ORDPRODUCSTAT { get; set; }
        public string ProdCodEstr { get; set; }
        public string PlanProducNum { get; set; }
        public string PRODNOME { get; set; }
        public string PRODCODALT { get; set; }
        public float soma { get; set; }
        public float resta { get; set; }
        public float boa { get; set; }
        public float Refugada { get; set; }
        public float Reprocesso { get; set; }
        public float Retalho { get; set; }
        public string qtdNecessaria { get; set; }
        public float QUANTIDADETOTAL { get; set; }
        public string PRODGRADECORCOD { get; set; }  
        public string dataInicial { get; set; }
        public string horaInicial { get; set; }
        public string dataFinal { get; set; }
        public string horaFinal { get; set; }
        public string OperOrdProducSeq { get; set; }
        public string tipoOperacao { get; set; }
        public string TIPOLANCCOD { get; set; }
        public string OPERORDPRODUCTIPO { get; set; }
        public DateTime OperOrdProducDataHoraFim { get; set; }
        public List<clsFuncionario> Listafuncionario { get; set; }        

        public bool consultaDadosOrdemProducao()
        {
            string strSQL = "";
            DataTable dadosTable = new DataTable();

            strSQL += "SELECT op.OrdProducNum, Poa.ProdOperSeq, Poa.AtivGrpCodEstr,Atg.AtivGrpNome, Poa.CCtrlCodEstr,  ";
            strSQL += "Pop.OperCod, Cct.CCtrlNome, op.ORDPRODUCSTAT, op.ProdCodEstr, op.PlanProducNum, p.PRODNOME,  ";
            strSQL += "p.PRODCODALT,op.OrdProducQtdNec  ";
            strSQL += "FROM ORD_PRODUC op with (NOLOCK)  ";
            strSQL += "Join PRODUTO p with (NOLOCK) on (p.PRODCODESTR = op.PRODCODESTR)   ";
            strSQL += "left join PROD_OPER_ATIV Poa on Poa.EmpCod = op.EmpCod and Poa.ProdCodEstr = op.ProdCodEstr ";
            strSQL += "left join PROD_OPER Pop on Pop.ProdCodEstr = op.PRODCODESTR ";
            strSQL += "left join ATIVIDADE_GRUPO Atg with (NOLOCK) on Poa.AtivGrpCodEstr = Atg.AtivGrpCodEstr ";
            strSQL += "left Join  Centro_Ctrl Cct with (NOLOCK) on Cct.CCtrlCodEstr = Poa.CCtrlCodEstr ";

            strSQL += " where op.ORDPRODUCNUM = '" + this.OrdProducNum + "'";
            strSQL += " and op.EmpCod = '" + this.EmpCod + "' and op.ORDPRODUCSTAT = 'Liberada' ";

            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaDadosOrdemProducao - clsApontamento.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.ProdOperSeq = row["ProdOperSeq"].ToString();
                    this.AtivGrpCodEstr = row["AtivGrpCodEstr"].ToString();
                    this.AtivGrpNome = row["AtivGrpNome"].ToString();
                    this.CCtrlCodEstr = row["CCtrlCodEstr"].ToString();
                    this.OperCod = row["OperCod"].ToString();
                    this.CCtrlNome = row["CCtrlNome"].ToString();

                    this.ORDPRODUCSTAT = row["ORDPRODUCSTAT"].ToString();
                    this.ProdCodEstr = row["ProdCodEstr"].ToString();
                    this.PlanProducNum = row["PlanProducNum"].ToString();
                    this.PRODNOME = row["PRODNOME"].ToString();
                    this.PRODCODALT = row["PRODCODALT"].ToString();

                    this.qtdNecessaria = row["OrdProducQtdNec"].ToString();
                }

                return true;
            }
            else
            {
                return false;
            }           
        }

        public string geraCodigoOrdemOP()
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {                
                try
                {                   
                    //Chama procedure para buscar número do pedido
                    using (SqlCommand dbCommand = new SqlCommand("gerar_codigo", dbConnection))
                    {
                        dbConnection.Open();

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 20, "empresa"));
                        dbCommand.Parameters.Add(new SqlParameter("@tabela", SqlDbType.VarChar, 31, "tabela"));
                        dbCommand.Parameters.Add(new SqlParameter("@codigo", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "codigo", DataRowVersion.Default, null));

                        dbCommand.Parameters[0].Value = this.EmpCod;
                        dbCommand.Parameters[1].Value = "OPER_ORD_PRODUC";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        this.OperOrdProducSeq = ((int)dbCommand.Parameters["@codigo"].Value).ToString();
                    }
                    return "";
                }
                catch
                {
                    return "Erro ao Gerar Codigo OP";
                }
            }
        }

        public bool salvarOrdProduc()
        {
            string strSQL = "";
            DataTable dadosTable = new DataTable();

            try
            {
                string DataIFormatada = this.mdlfuncoes.FormataData(this.dataInicial) + " " + this.horaInicial;
                string DataFFormatada = this.mdlfuncoes.FormataData(this.dataFinal) + " " + this.horaFinal;

                strSQL += " INSERT INTO OPER_ORD_PRODUC (EmpCod, OrdProducNum, ProdCodEstr, ProdOperSeq, OperOrdProducSeq,";
                strSQL += " CCtrlCodEstr, OperOrdProducStat, OperOrdProducDataHoraInic, OperOrdProducDataHoraFim, ";
                strSQL += " OperOrdProducQtdBoa, OperOrdProducQtdRefug, OperOrdProducQtdReproc, OperCod, UsuCod, OperOrdProducApont, OperOrdProducPesoUnitProd, AtivGrpCodEstr) ";
                strSQL += " VALUES ('" + this.EmpCod + "', '" + this.OrdProducNum + "','" + this.ProdCodEstr + "','" + this.ProdOperSeq + "','" + this.OperOrdProducSeq + "'";
                strSQL += " ,'" + this.CCtrlCodEstr + "','" + this.ORDPRODUCSTAT + "','" + DataIFormatada + "','" + DataFFormatada + "', ";
                strSQL += " '" + this.boa + "','" + this.Refugada + "','" + this.Reprocesso + "','" + this.OperCod + "','" + this.usuCod + "','" + this.tipoOperacao + "','0','" + this.AtivGrpCodEstr + "') ";

                dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "salvarOrdProduc - clsApontamento.cs");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string salvarOrdProducFunc(int i)
        {
            string strSQL = "";
            DataTable dadosTable = new DataTable();

            try
            {
                strSQL += " insert into OPER_ORD_PRODUC_FUNC (EmpCod, OrdProducNum, ProdCodEstr, ProdOperSeq, OperOrdProducSeq, FuncCod, OperOrdProducFuncApont)";
                strSQL += " values ('" + this.EmpCod + "','" + this.OrdProducNum + "','" + this.ProdCodEstr + "' , '" + this.ProdOperSeq + "', ";
                strSQL += " '" + this.OperOrdProducSeq + "','" + this.Listafuncionario[i].FuncCod + "', 'Sim')";

                dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "salvarOrdProducFunc - clsApontamento.cs");

                return "";
            }
            catch
            {
                return "Erro ao Inserir Funcionario";
            }
        }

        public bool InserirOperOrdProducProc()
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("INSERT_OPER_ORD_PRODUC_PROC", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@VEMPCOD", SqlDbType.VarChar, 30, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VORDPRODUCNUM", SqlDbType.VarChar, 30, "OrdProducNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@VOPERORDPRODUCSEQ", SqlDbType.Int, 0, "OperOrdProducSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@VPRODUTO", SqlDbType.VarChar, 30, "ProdCodEstr"));

                    dbCommand.Parameters["@VEMPCOD"].Value = this.EmpCod;
                    dbCommand.Parameters["@VORDPRODUCNUM"].Value = this.OrdProducNum;
                    dbCommand.Parameters["@VOPERORDPRODUCSEQ"].Value = this.OperOrdProducSeq;
                    dbCommand.Parameters["@VPRODUTO"].Value = this.ProdCodEstr;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 99999;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    dataReader.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool BaixaReqMatOper()
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("BAIXA_REQ_MAT_OPER", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@ORD_PRODUC", SqlDbType.VarChar, 10, "OrdProducNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODIGO_EMPRESA", SqlDbType.VarChar, 30, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VOPERORDPRODUCSEQ", SqlDbType.Int, 0, "OperOrdProducSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@USUCOD", SqlDbType.VarChar, 31, "usuCod"));

                    dbCommand.Parameters["@ORD_PRODUC"].Value = this.OrdProducNum;
                    dbCommand.Parameters["@CODIGO_EMPRESA"].Value = this.EmpCod;
                    dbCommand.Parameters["@VOPERORDPRODUCSEQ"].Value = this.OperOrdProducSeq;
                    dbCommand.Parameters["@USUCOD"].Value = this.usuCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 99999;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    dataReader.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }
     
        /*Inicio Função Mario*/
        public string validaQuantidadeOP()
        {
            string msg = "";
            //Pega a Meno Sequencia da OP
            if (menorSequenciaOP())
            {
                //Calcula a Menor sequencia da OP
                if (calculaMenorSequenciaOP())
                {
                    //Calcula a Sequencia da OP normal
                    if (calculaSequenciaOP())
                    {

                        this.resta = this.resta - this.soma;
                        this.soma = this.soma + this.boa + this.Refugada + this.Reprocesso;


                        if (this.soma > this.resta)
                        {
                            msg = "Quantidade MÁXIMA QUE PODE SER APONTADA";
                            return msg;
                        }
                        else
                        {
                            //Caso Valide returna nada
                            msg = "";
                            return msg;
                        }

                    }
                    else
                    {
                        msg = "Erro ao Calcular a quantidade da Sequncia da OP";
                        return msg;
                    }

                }
                else
                {
                    msg = "Erro ao Calcular a quantidade da menor Sequencia da OP";
                    return msg;
                }

            }
            else
            {
                msg = "Erro ao pegar a Menor Sequencia da OP";
                return msg;
            }
        }

        public bool menorSequenciaOP()
        {

            string strSQL = "";
            DataTable dadosTable = new DataTable();

            //Primeiro consulta a Meno Sequencia da OP
            strSQL += "select top 1 ProdOperSeq FROM PLAN_OPER_ORD_PRODUC ";
            strSQL += " where ORDPRODUCNUM = '"+ this.OrdProducNum +"' and ProdOperSeq < '"+ this.ProdOperSeq +"'";
            strSQL += " order by ProdOperSeq desc";

            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "menorSequenciaOP - clsApontamento.cs");

            if (dadosTable.Rows.Count != 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    //Recebe a Menor Sequencia da Op
                    this.MenorProdOperSeq = row["ProdOperSeq"].ToString();
                }

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool calculaMenorSequenciaOP()
        {
            string strSQL = "";
            this.resta = 0;
            DataTable dadosTable = new DataTable();

            //Primeiro consulta a Meno Sequencia da OP
            strSQL += "select OperOrdProducQtdBoa FROM OPER_ORD_PRODUC ";
            strSQL += " where ORDPRODUCNUM = '" + this.OrdProducNum + "' and ProdOperSeq = '" + this.MenorProdOperSeq + "'";
            strSQL += " order by ProdOperSeq desc";
            
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "calculaMenorSequenciaOP - clsApontamento.cs");

            if (dadosTable.Rows.Count != 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    //Recebe a Menor Sequencia da Op
                    this.resta += Convert.ToInt32(row["OperOrdProducQtdBoa"].ToString());
                }

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool calculaSequenciaOP()
        {

            string strSQL = "";
            this.soma = 0;
            DataTable dadosTable = new DataTable();

            strSQL += "select OperOrdProducQtdBoa FROM OPER_ORD_PRODUC ";
            strSQL += " where ORDPRODUCNUM = '" + this.OrdProducNum + "' and ProdOperSeq = '" + this.ProdOperSeq + "'";
            strSQL += " order by ProdOperSeq desc";

            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "calculaSequenciaOP - clsApontamento.cs");

            if (dadosTable.Rows.Count != 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {

                    this.soma += Convert.ToInt32(row["OperOrdProducQtdBoa"].ToString());
                }

                return true;
            }
            else
            {
                return false;
            }

        }
        /*Fim Funcoes Mario*/

        
        /*Carregar Lista de Funcionarios*/
        public string carregaFunc()
        {

            string descLinhas = "";


            //Inicio da tabela
            descLinhas += "<table class=\"lstTabela\">";

            
            for (int i = 0; i < this.Listafuncionario.Count; i++)
            {
                descLinhas += "<td CssClass=\"btAdiciona\"  Width=\"25px\"> <a href=\"#\"> <img src=\"../imagens/delete.png\" alt=\"Selecao\" border=\"0\" onclick=\"javascript: return excluir('" + Listafuncionario[i].FuncCod + "')\" /></a></td>";
                descLinhas += "<td class=\"small\" style=\"width: 153px\">" + Listafuncionario[i].FuncCod + "</td>";
                descLinhas += "<td class=\"extendproduto\" style=\"width: 808px\">" + Listafuncionario[i].FuncNome + "</td>";
                descLinhas += "</tr>";

            }

            //Fim tabela
            descLinhas += "</table><br />";



            return descLinhas;
        }
        /********************************/

        /*Fucao para Add e Remover Funcionario na Lista*/
        public void addFunc(string FuncCod, string FuncNome)
        {
            clsFuncionario novoFunc = new clsFuncionario();
            novoFunc.FuncNome = FuncNome;
            novoFunc.FuncCod = FuncCod;
            int contExiste = 0;

            if (this.Listafuncionario != null)
            {

                if (this.Listafuncionario.Count > 0)
                {
                    for (int i = 0; i < this.Listafuncionario.Count; i++)
                    {
                        if (this.Listafuncionario[i].FuncCod == FuncCod)
                        {
                            contExiste = 1;
                        }
                    }
                }
                if (contExiste == 0)
                {
                    this.Listafuncionario.Add(novoFunc);
                }
            }
            else
            {
                this.Listafuncionario = new List<clsFuncionario>();
                Listafuncionario.Add(novoFunc);
            }
        }
        public void removeFunc(string FuncCod)
        {
            if (this.Listafuncionario != null)
            {

                if (this.Listafuncionario.Count > 0)
                {
                    for (int i = 0; i < this.Listafuncionario.Count; i++)
                    {
                        if (this.Listafuncionario[i].FuncCod == FuncCod)
                        {
                            this.Listafuncionario.Remove(this.Listafuncionario[i]);
                        }
                    }
                }

            }

        }
        /*Fim Fucao para Add e Remover Funcionario na Lista*/

       /* public string FormataData(string data)
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
        }*/
    }
}