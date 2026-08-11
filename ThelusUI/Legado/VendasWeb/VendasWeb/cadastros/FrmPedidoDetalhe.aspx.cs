using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.cadastros
{

    public partial class FrmPedidoDetalhe : System.Web.UI.Page
    {

    FiltroClass ObjFiltroClass = new FiltroClass();
    PedidoClass PedidoClass = new PedidoClass();    
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ControlPainel.Desabilitar_Botoes();

            if (!IsPostBack)
            {
                
                PedidoClass = (GerencialVendas.PedidoClass)Session["PedidoClass"];
                EmpCodHiddenField.Value = PedidoClass.EmpCod;
                PedVendaNumHiddenField.Value = PedidoClass.PedVendaNum;

                EntCod.Text = PedidoClass.EntCod;
                EntNome.Text = PedidoClass.EntNome;
                EmpCod.Text = PedidoClass.EmpCod;
                EmpNome.Text = PedidoClass.EmpNome;
                EntCpfCgc.Text = PedidoClass.EntCpfCgc;
                PedVendaData.Text = string.Format("{0:D}", PedidoClass.PedVendaData);
                NFHoraSaida.Text = string.Format("{0:D}", PedidoClass.NFHoraSaida);
                EntEnderCompleto.Text = PedidoClass.EntEnderCompleto;
                EntBair.Text = PedidoClass.EntBair;
                CidNome.Text = PedidoClass.CidNome;
                UfSigla.Text = PedidoClass.UfSigla;
                EntCep.Text = PedidoClass.EntCep;
                CondPagCod.Text = PedidoClass.CondPagCod;
                CondPagPedVendaNome.Text = PedidoClass.CondPagPedVendaNome;
                PedVendaNatOpProd.Text = PedidoClass.PedVendaNatOpProd;
                NatOpNome.Text = PedidoClass.NatOpNome;
                VendCod.Text = PedidoClass.VendCod;
                VendNome.Text = PedidoClass.VendNome;
                PedVendaValMerc.Text = string.Format("{0:C2}", PedidoClass.PedVendaValMerc);
                PedVendaValIpiCalc.Text = string.Format("{0:C2}", PedidoClass.PedVendaValIpiCalc);
                PedVendaValIcms.Text = string.Format("{0:C2}", PedidoClass.PedVendaValIcms);
                IcmsDiferido.Text = string.Format("{0:C2}", PedidoClass.IcmsDiferido);
                IcmsDevido.Text = string.Format("{0:C2}", PedidoClass.IcmsDevido);
                PedVendaValTotal.Text = string.Format("{0:C2}", PedidoClass.PedVendaValTotal);
                EntCodTransp.Text = PedidoClass.EntCodTransp;
                EntNomeTransp.Text = PedidoClass.EntNomeTransp;
                PedVendaStatFrete.Text = PedidoClass.PedVendaStatFrete;
                PedVendaTexto.InnerText = PedidoClass.PedVendaTexto;
                PedVendaTextoHist.InnerText = PedidoClass.PedVendaTextoHist;
                ItensFormatados.Text = PedidoClass.ItensFormatados;
                ClicheFormatados.Text = PedidoClass.ClicheFormatados;
            }
            
        }

        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            Session["EmpCod"] = EmpCodHiddenField.Value;
            Session["PedVendaNum"] = PedVendaNumHiddenField.Value;
            Session["Tipo"] = "Consulta";
            //Response.Redirect("../relatorios/frmCopiaPedido.aspx?indmnu=2");
            //Abrir Nova Guia
            Response.Redirect("~/relatorios/frmCopiaPedido.aspx?indmnu=2");
        }

        protected void ImprimirSemHistButton_Click(object sender, EventArgs e)
        {
            Session["EmpCod"] = EmpCodHiddenField.Value;
            Session["PedVendaNum"] = PedVendaNumHiddenField.Value;
            Session["Tipo"] = "Consulta";
            //Response.Redirect("../relatorios/frmCopiaPedido.aspx?indmnu=2");
            //Abrir Nova Guia
            Response.Redirect("~/relatorios/frmCopiaPedido.aspx?indmnu=2");
        }

        protected void AcessarButton_Click(object sender, EventArgs e)
        {
            Session["EmpCod"] = EmpCodHiddenField.Value;
            Session["PedVendaNum"] = PedVendaNumHiddenField.Value;
            Session["Tipo"] = "Consulta";
            Session["pedidoNovo"] = null;


            Response.Redirect("../cadastros/cadPedidoPrincipal.aspx?indmnu=2");
        }

        protected void SairButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/listas/FrmListaPedidos.aspx?indmnu=2");
        }
    }
}