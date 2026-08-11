using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmCarteiraDetalhe : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!this.IsPostBack)
            {
                ObjEntidadesClass.EntCod = Request.QueryString["EntCod"].ToString();
                DetalheEntidadeGridView.DataSource = ObjEntidadesClass.Consulta_Entidade_Detalhe();
                DetalheEntidadeGridView.DataBind();
            }
        }


    }
}