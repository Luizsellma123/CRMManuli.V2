using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.usercontrol
{
    public partial class cabecarioLogistica : System.Web.UI.UserControl
    {
        //Instancia classe pedido
        pedido novoPedido = new pedido();
        criptografia mdlCriptografia = new criptografia();

        protected void Page_Load(object sender, EventArgs e)
        {
            funcoes mdlfuncoes = new funcoes();
            //Recupera objeto pedido da sessao do usuário
            novoPedido = (pedido)Session["pedidoNovo"];

            string strSQL = "";
            string entNat = "";
            string strconec;

            strSQL += "select EntTexto, EntTextoHist, EntCod, EntNome, EntNomeFant, EntCpfCgc, EntNat from ENTIDADE where EntCod =" + novoPedido.codigoEntidade.ToString() + ";";

            if (novoPedido.codigoEmpresa != "99")
            {
                strconec = mdlfuncoes.getString().ToString();  
            }
            else
            {
                strconec = mdlfuncoes.getString().ToString(); 
            }
            using (SqlConnection dbConnection = new SqlConnection(strconec))
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);

                SqlDataReader drEntidade = dbCommand.ExecuteReader();

                if (drEntidade.Read())
                {
                    lblDescNome.Text = drEntidade["Entnome"].ToString();
                    lblDescFantasia.Text = drEntidade["EntNomeFant"].ToString();
                    lblDescCnpj.Text = drEntidade["EntCpfCgc"].ToString();
                    txtIDEntidade.Text = novoPedido.codigoEntidade;
                    entNat = drEntidade["EntNat"].ToString();
                }

                drEntidade.Close();

                strSQL = "select EU.EmpCod, EU.EmpCod +' - '+EF.EmpNomeFant as EmpNome from EMP_FIL_USUARIO EU, EMPRESA_FILIAL EF where ";
                strSQL += "EU.EmpCod=EF.EmpCod and UsuCod = '" + Session["usuario"].ToString() + "' and EF.EmpCod='" + novoPedido.codigoEmpresa.ToString() + "'";

                dbCommand = new SqlCommand(strSQL, dbConnection);

                drEntidade = dbCommand.ExecuteReader();

                if (drEntidade.Read())
                {
                    lblDescEmpresa.Text = drEntidade["EmpNome"].ToString();
                }

                drEntidade.Close();
            }
        }

        protected void btnAlteraEntidade_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"../logistica/alteracaoPedido.aspx?indmnu=3&idEmp=" + mdlCriptografia.Criptografar(novoPedido.codigoEmpresa, "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar(novoPedido.tipoOperacao, "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(novoPedido.numeroPedido, "#!$a36?@") + " \";</script>");
        }
    }
}