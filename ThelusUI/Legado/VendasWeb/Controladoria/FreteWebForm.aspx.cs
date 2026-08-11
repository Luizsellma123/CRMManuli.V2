using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.Controladoria
{
    public partial class FreteWebForm : System.Web.UI.Page
    {
        FretesClass frete = new FretesClass();
        funcoes mdlFuncoes = new funcoes();
        usuario ObjUsuarioClass = new usuario();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                EmpresaDropDown.DataSource = mdlFuncoes.Consulta_Empresa(Session["usuario"].ToString());
                EmpresaDropDown.DataValueField = "EmpCod";
                EmpresaDropDown.DataTextField = "EmpNome";
                EmpresaDropDown.DataBind();

                EmpresaDropDown.Items.Insert(0, new ListItem("Selecione", ""));
                EmpresaDropDown.Focus();
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {           
            string empcod = EmpresaDropDown.SelectedValue;
            string usucod = (string)Session["usuario"];
            FreteGridView.DataSource = frete.Consulta_Empresa(usucod, empcod);
            FreteGridView.DataBind();
            FreteMultiView.Visible = true;
        }

        protected void CidadeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Fretes/CenarioCidadesWebForm.aspx?indmnu=3");
        }

        protected void EstadoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Fretes/CenarioEstadosWebForm.aspx?indmnu=3");
        }
    }
}
