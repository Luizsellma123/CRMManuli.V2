using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{
    public partial class FrmMaps : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        MapsClass ObjMapsClass = new MapsClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                
                //Inicia tratatica para ver se das Entidade consultada existem alguma ainda sem mapeamento
                if (Session["EntCodMaps"] != null)
                {

                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = "";
                    string MsgInformativa = "";

                    ObjMapsClass.UsuCod = Session["usuario"].ToString();
                    ObjMapsClass.EntCod = Session["EntCodMaps"].ToString();
                    ObjMapsClass.Count_GeoCod_EntCod();

                    if (ObjMapsClass.GeoCodNaoMapeado > 0)
                    {
                        MsgInformativa = "<br> De " 
                                         + ObjMapsClass.GeoCodSolicitado.ToString() 
                                         + " Entidades consultadas, " 
                                         + ObjMapsClass.GeoCodNaoMapeado.ToString()
                                         + " esta(ão) sem mapeamento!";
                        

                    }


                    //Informativo de Status Filtrados
                    MsgInformativa += "<br> Filtro realizado com " + ObjMapsClass.TotalAtivo.ToString()
                                                             + " Ativo(s), " + ObjMapsClass.TotalInativo.ToString()
                                                             + " Inativo(s), " + ObjMapsClass.TotalProspectivo.ToString() + " Prospectivo(s)";
                    


                    //Informativo de Status Filtrados
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(MsgInformativa, true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;



                }
                //Fim tratatica para ver se das Entidade consultada existem alguma ainda sem mapeamento

                

                
                
            }



        }

        protected void VoltarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmCarteira.aspx?indmnu=2");
        }

        protected void MapaFullLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmMapsFull.aspx?indmnu=5");
            
        }



       
    }
}