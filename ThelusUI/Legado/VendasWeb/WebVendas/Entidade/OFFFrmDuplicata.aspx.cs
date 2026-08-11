using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.WebVendas.Entidade
{
    public partial class FrmDuplicata : System.Web.UI.Page
    {

        criptografia mdlCriptografia = new criptografia();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se usuário esta logado
            int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);
            if (Session["usuario"] == null && varmenu != 0 && varmenu < 99)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }


            if (!IsPostBack)
            {
              string EntCod =  mdlCriptografia.Descriptografar(Request.QueryString["idEnt"], "#!$a36?@");
              LblEntidade.Text = "Cód. entidade: " + EntCod.ToString();
              //Carrregando informacoes na tela para consulta
               lblFinanceiro.Text = carregaItemsFinanceiro(EntCod);
            }
        }

        public string carregaItemsFinanceiro(string EntCod)
        {
            funcoes mdlFuncoes = new funcoes();
            string retorno = "";
            string strSQL = "";

            strSQL += "SELECT A.EMPCOD, ";
            strSQL += " case B.DOCFINTIPOLANC  when 'PAG' then 'PAGAR' when 'REC' then 'RECEBER'  end as DOCFINTIPOLANC";
            strSQL += " ,A.PARCDOCFINDUPNUM, A.ParcDocFinValor,CONVERT(nvarchar(10), A.PARCDOCFINDATAEMISSAO, 103) as PARCDOCFINDATAEMISSAO, CONVERT(nvarchar(10), A.PARCDOCFINDATAVENC, 103) as PARCDOCFINDATAVENC, CONVERT(nvarchar(10), A.PARCDOCFINDATAPRORROG, 103) as PARCDOCFINDATAPRORROG,CONVERT(nvarchar(10), A.PARCDOCFINDATAPAG, 103) as PARCDOCFINDATAPAG,";
            strSQL += " DATEDIFF(day,A.PARCDOCFINDATAVENC, A.PARCDOCFINDATAPAG ) as atraso ";
            strSQL += " FROM PARC_DOC_FIN A, DOC_FIN B ";

            strSQL += " where (B.EMPCOD = A.EMPCOD AND B.DOCFINCHV = A.DOCFINCHV )and  B.EMPCOD ";
            strSQL += " IN ('1','1.1','1.2','1.3','1.4','1.99','2','2.1') and B.ENTCOD = '" + EntCod + "'";
            strSQL += " AND B.DOCFINPROJECAO = 'Não'";
            strSQL += " order by B.DOCFINTIPOLANC, A.PARCDOCFINDUPNUM";

            SqlConnection dbConnection = new SqlConnection();
            using (dbConnection = new SqlConnection(mdlFuncoes.getString().ToString()))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();
                    using (SqlDataReader drPedido = dbCommand.ExecuteReader())
                    {
                        if (drPedido.HasRows)
                        {
                            //Inicio da tabela
                            retorno += "<table class=\"\">";

                            //cabeçario da tabela
                            retorno += "<tr class=\"\">";                            
                            retorno += "<td>Empresa:</td>";
                            retorno += "<td>Tipo:</td>";
                            retorno += "<td>Documento:</td>";
                            retorno += "<td>Valor:</td>";
                            retorno += "<td>Emissão:</td>";
                            retorno += "<td>Vencimento:</td>";
                            retorno += "<td>Prorrogação:</td>";
                            retorno += "<td>Pagamento:</td>";
                            retorno += "<td>Atraso:</td>";

                            retorno += "</tr>";

                            while (drPedido.Read())
                            {
                                retorno += "<td>" + drPedido["EMPCOD"] + "</td>";
                                retorno += "<td>" + drPedido["DOCFINTIPOLANC"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDUPNUM"] + "</td>";
                                retorno += "<td>" + drPedido["ParcDocFinValor"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDATAEMISSAO"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDATAVENC"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDATAPRORROG"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDATAPAG"] + "</td>";
                                retorno += "<td>" + drPedido["atraso"] + "</td>";

                                retorno += "</tr>";
                            }

                            //Fim tabela
                            retorno += "</table><br />";
                        }
                    }
                }
            }

            if (retorno == "")
            {
                retorno = "Nenhum Historico localizado!";
            }

            return retorno;
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"../../cadastros/FrmCarteira.aspx?indmnu=2\";</script>");
        }

       

    }
}