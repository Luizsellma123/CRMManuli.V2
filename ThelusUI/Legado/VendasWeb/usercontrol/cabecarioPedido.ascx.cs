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
    public partial class cabecarioPedido : System.Web.UI.UserControl
    {
        //Instancia classe pedido
        pedido novoPedido = new pedido();
        criptografia mdlCriptografia = new criptografia(); 

        protected void Page_Load(object sender, EventArgs e)
        {           
            funcoes mdlfuncoes = new funcoes();
            //Recupera objeto pedido da sessao do usuário
            novoPedido = (pedido)Session["pedidoNovo"];

            if (novoPedido == null)
            {                
                Response.Redirect("../Home.aspx?indmnu=1");
            }

            string strSQL = "";
            string entNat = "";
            string strconec;

            strSQL += "select CC.ObservacaoSimples, CC.ObservacaoCompleta, CC.IDCliente, CC.NomeCliente, CC.NomeFantasia, ";
            strSQL += "CC.CNPJ, isnull(CNJ.Nome, '') as NaturezaJuridica from ";
            strSQL += "CRM_CLIENTE CC LEFT JOIN CRM_NATUREZA_JURIDICA CNJ ON CC.IDNatureza = CNJ.IDNatureza ";
            strSQL += "where IDCliente = '" + novoPedido.codigoEntidade.ToString() + "' ";

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
                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);
                dbConnection.Open();
                SqlDataReader drEntidade = dbCommand.ExecuteReader();

                if (drEntidade.Read())
                {
                    lblDescNome.Text = drEntidade["NomeCliente"].ToString();
                    lblDescFantasia.Text = drEntidade["NomeFantasia"].ToString();
                    lblDescCnpj.Text = drEntidade["CNPJ"].ToString();
                    txtIDEntidade.Text = novoPedido.codigoEntidade;
                    entNat = drEntidade["NaturezaJuridica"].ToString();
                }

                drEntidade.Close();

                strSQL = "";
                strSQL += "select IDEmpresa, convert(varchar(max),CodigoSAP) +' - '+NomeEmpresa as NomeEmpresa from CRM_EMPRESA_FILIAL where IDEmpresa='" + novoPedido.codigoEmpresa.ToString() + "'";

                dbCommand = new SqlCommand(strSQL, dbConnection);

                drEntidade = dbCommand.ExecuteReader();

                if (drEntidade.Read())
                {
                    lblDescEmpresa.Text = drEntidade["NomeEmpresa"].ToString();
                }

                drEntidade.Close();
            }
        }

        protected void btnAlteraEntidade_Click(object sender, EventArgs e)
        {


            String Origem = "";

            if (Session["Origem"] != null)
            {
                Origem = Session["Origem"].ToString();
                Session["Origem"] = null;
            }
            

            switch (Origem)
            {
                case "Cliche":
                case "DadosComplementar":
                case "Itens":
                case "Transportadora":
                    Response.Write("<script>window.location=\"../cadastros/cadPedidoPrincipal.aspx?indmnu=2&idEmp=" + mdlCriptografia.Criptografar(novoPedido.codigoEmpresa, "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar(novoPedido.tipoOperacao, "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(novoPedido.numeroPedido, "#!$a36?@") + " \";</script>");
                break;


                default:

                    Response.Write("<script>window.location=\"../Entidades/FrmCarteira.aspx?indmnu=2&idEmp=" + mdlCriptografia.Criptografar(novoPedido.codigoEmpresa, "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar(novoPedido.tipoOperacao, "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(novoPedido.numeroPedido, "#!$a36?@") + " \";</script>");
                    break;
            }


            
        }
    }
}