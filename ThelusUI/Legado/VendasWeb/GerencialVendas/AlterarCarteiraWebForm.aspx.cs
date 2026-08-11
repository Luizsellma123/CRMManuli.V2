using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.GerencialVendas
{
    public partial class AlterarCarteiraWebForm : System.Web.UI.Page
    {
        VendedorClass ObjVendedorClass = new VendedorClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
                
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

                //Verificando se deve mandar alerta
                if (Session["Msg"] != null)
                {

                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                    Session.Remove("Msg");
                }

                //Carrega vendedores conforme autorização
                CarregaCombos();
            }
        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            if (Session["clienteClasse"] != null)
            {
                //Descarega o id do Cliente
                ObjVendedorClass.IDCliente = ((ClienteClasse)Session["clienteClasse"]).IDCliente;
            }


            Resultado = ObjVendedorClass.Consulta_Vendedor_Cliente();
            VendedorDropDownList.DataSource = Resultado;
            VendedorDropDownList.DataValueField = "IDVendedor";
            VendedorDropDownList.DataTextField = "NomeVendedor";
            VendedorDropDownList.DataBind();

            VendedorDestinoDropDownList.DataSource = Resultado;
            VendedorDestinoDropDownList.DataValueField = "IDVendedor";
            VendedorDestinoDropDownList.DataTextField = "NomeVendedor";
            VendedorDestinoDropDownList.DataBind();
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            ObjVendedorClass.VendCod = VendedorDropDownList.SelectedValue.ToString();
            ObjVendedorClass.IDVendedorNovo = Convert.ToInt32(VendedorDestinoDropDownList.SelectedValue);
            ObjVendedorClass.UsuCod = Session["usuario"].ToString();

            erro = ObjVendedorClass.AtualizaCarteiraVendedorSAP();

            if (erro == "")
            {
                erro = "Carteira alterada com sucesso!";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }
    }
}
