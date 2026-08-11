using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class ControlEntidade : System.Web.UI.UserControl
    {

        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();


        protected void Page_Load(object sender, EventArgs e)
        {



            if (Session["clsEntidades"] != null)
            {
                //Descarega a session da Entidade
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                lblDescCnpj.Text = ObjEntidadesClass.EntCpfCgc.ToString();
                if (ObjEntidadesClass.EntCod != null)
                {
                    lblEntidade.Text = ObjEntidadesClass.EntCod.ToString() + "-" + ObjEntidadesClass.EntNome.ToString();
                    lblDescEntidadeNome.Text = ObjEntidadesClass.EntNomeFant.ToString();
                }


            }




        }


    }
}