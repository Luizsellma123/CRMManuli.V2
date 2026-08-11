using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.Entidades
{
    public partial class FrmSelecaoEmpresa : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        criptografia mdlCriptografia = new criptografia();
        funcoes mdlFuncoes = new funcoes();
        clsEntidades ObjEntidadesClass = new clsEntidades();

        //Instancia classe pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();


            if (!IsPostBack)
            {

                //EmpresaGridView.DataSource = mdlFuncoes.Consulta_Empresa(Session["usuario"].ToString());
                //EmpresaGridView.DataBind();

                EmpresaDropDownList.DataSource = mdlFuncoes.Consulta_Empresa(Session["usuario"].ToString());
                EmpresaDropDownList.DataValueField = "IDEmpresa";
                EmpresaDropDownList.DataTextField = "NomeEmpresa";
                EmpresaDropDownList.DataBind();


                EmpresaDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                EmpresaDropDownList.Focus();
            }

        }


        
        protected void ProximoPassoButton_Click(object sender, EventArgs e)
        {

            //novoPedido.veioCRM = "sim";

            ObjEntidadesClass = new clsEntidades();
            ObjEntidadesClass = ((clsEntidades)Session["clsEntidades"]);

            /*Pega o codigo do Vendedor Selecionado*/
            ObjEntidadesClass.EmpCod = EmpresaDropDownList.SelectedValue;
            novoPedido.CodigoClienteSAP = ObjEntidadesClass.CodigoClienteSAP ?? "";

            Session["pedidoNovo"] = novoPedido;

            /*Chama Proxima Tela*/
            Response.Redirect("../cadastros/cadPedidoPrincipal.aspx?indmnu=2&codEmp=" + mdlCriptografia.Criptografar(ObjEntidadesClass.EmpCod, "#!$a36?@") + "&idEnt=" + mdlCriptografia.Criptografar(ObjEntidadesClass.IDCliente.ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("inclusao", "#!$a36?@"));

        }


    }
}