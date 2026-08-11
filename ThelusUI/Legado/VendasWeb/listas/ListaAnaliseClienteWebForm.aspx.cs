using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;
using VendasWeb.classes;


namespace VendasWeb.listas
{
    public partial class ListaAnaliseClienteWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        VendedorClass ObjVendedorClass = new VendedorClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        clsEntidades ObjEntidadesClass = new clsEntidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;
            if (Session["Msg"] != null)
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;

            }

            if (!IsPostBack)
            {

                Session["RetornarNavegacaoPara"] = "../listas/ListaAnaliseClienteWebForm.aspx?indmnu=2";

                CarregaCombos();

                //Traz todas as carteiras como Default
                VendedoresSelect.SelectedIndex = 1;

                /*Tratar Abrir e fechar Div*/
                collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse\" runat=\"server\">";

                if (Session["ObjEntidadesClassFiltroAnalise"] != null)
                {
                    ObjEntidadesClass = (clsEntidades)Session["ObjEntidadesClassFiltroAnalise"];

                    Atualiza_Grid();

                    Session["ObjEntidadesClassFiltroAnalise"] = null;
                }

            }


        }






        protected void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            //Carrega Vendedores
            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = "S";
            Resultado = ObjVendedorClass.Consulta_Vendedor();

            VendedoresSelect.DataSource = Resultado;
            VendedoresSelect.DataTextField = "NomeVendedor";
            VendedoresSelect.DataValueField = "IDVendedor";
            VendedoresSelect.DataBind();

            //Carrega Status
            OBJCliente.CodigoUsuario = Session["usuario"].ToString();
            IDStatusDropDownList.DataSource = OBJCliente.CarregaStatusAnaliseCliente();
            IDStatusDropDownList.DataTextField = "DescricaoStatus";
            IDStatusDropDownList.DataValueField = "IDStatus";
            IDStatusDropDownList.DataBind();
            IDStatusDropDownList.Items.Insert(0, new ListItem("Selecione", ""));


        }


        protected void btnListar_Click(object sender, EventArgs e)
        {
            switch (drpEntCod.SelectedValue.ToString())
            {
                case "1":
                    ObjEntidadesClass.EntNomeFant = txtFiltroEntCod.Text;
                    break;

                case "2":
                    ObjEntidadesClass.EntNome = txtFiltroEntCod.Text;
                    break;

                case "3":
                    ObjEntidadesClass.EntCod = txtFiltroEntCod.Text;
                    break;

                case "4":
                    ObjEntidadesClass.EntCpfCgc = txtFiltroEntCod.Text;
                    break;
            }


            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            ObjEntidadesClass.StatEntCod = IDStatusDropDownList.SelectedValue;

            RecuperaDados_Select();
            Atualiza_Grid();
        }




        protected void RecuperaDados_Select()
        {

            ObjEntidadesClass.VendCod = "";

            for (int i = 0; i < VendedoresSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (VendedoresSelect.Items[i].Selected == true)
                {
                    if (i == 0)
                    {
                        ObjEntidadesClass.VendCod = VendedoresSelect.Items[i].Value;
                    }
                    else
                    {
                        ObjEntidadesClass.VendCod += "," + VendedoresSelect.Items[i].Value;
                    }
                }
            }


        }



        public void Atualiza_Grid()
        {

            ClientesMultiView.Visible = true;

            DataTable outputTable = new DataTable();
            outputTable = ObjEntidadesClass.Consulta_Entidade();

            ListaEntidadeGridView.DataSource = outputTable;
            ListaEntidadeGridView.DataBind();


            if (outputTable.Rows.Count > 0)
            {

                //Guarda Session de Filtro Realizado
                Session["ObjEntidadesClassFiltroAnalise"] = ObjEntidadesClass;



            }

        }


        protected void ListaEntidadeGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ListaEntidadeGridView.PageIndex = e.NewPageIndex;
            btnListar_Click(null, null);
        }




        protected void SelecionarCheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            //Desmarca todos os check
            foreach (GridViewRow OldGridView in ListaEntidadeGridView.Rows)
            {
                //Seta todos como falso
                ((RadioButton)OldGridView.FindControl("SelecionarRadioButton")).Checked = false;
            }

            //marcando o RadioButton selecionado
            RadioButton RadioButton = (RadioButton)sender;
            GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            ((RadioButton)GridViewRow.FindControl("SelecionarRadioButton")).Checked = true;


            /*Pega o codigo da Entidade Selecionada*/
            ObjEntidadesClass = new clsEntidades();
            ObjEntidadesClass.IDCliente = Convert.ToInt32(((Label)((Control)sender).FindControl("IDClienteLabel")).Text);
            ObjEntidadesClass.CodigoClienteSAP = ((Label)((Control)sender).FindControl("CodigoLabel")).Text;
            ObjEntidadesClass.EntCod = ObjEntidadesClass.IDCliente.ToString();

            /*Recupera dados do cliente*/
            OBJCliente.CodigoCliente = ObjEntidadesClass.CodigoClienteSAP;
            OBJCliente.IDCliente = Convert.ToInt32(ObjEntidadesClass.EntCod ?? "0");

            Session["clienteClasse"] = OBJCliente;

            /*Carrega em Session*/
            Session["clsEntidades"] = ObjEntidadesClass;


            //Grava Codigo Entidade para Apresentar no Maps
            Session["EntCodMaps"] = ObjEntidadesClass.EntCod;

            this.ControlPainel.refresh();


        }

    }
}