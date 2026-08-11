using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.Producao
{
    public partial class OrdensDeServicoIncluirProdutosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao ObjProducao = new producao();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            if (!IsPostBack)
            {
                Session["OrdensServicoProdutosDataTable"] = null;
                CarregaDadosNaTela();

                PedidoSAPTextBox.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_BuscarLinkButton').click();return false;}} else {return true}; ");
                PedidoCRMTextBox.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_BuscarLinkButton').click();return false;}} else {return true}; ");

            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void BloqueiaDesbloqueiaButtons()
        {
            if (ObjProducao.StatusPrioridade == "bloqueado")
            {
                IncluirPedidosButton.Enabled = false;
                this.OrdensServicoGridView.Columns[0].Visible = false;
                this.OrdensServicoGridView.Columns[6].Visible = false;
                this.OrdensServicoGridView.Columns[7].Visible = false;
            }
            else
            {
                IncluirPedidosButton.Enabled = true;

            }
        }

        public void CarregaDadosDaTela()
        {
            if (EmpresaDropDownList.SelectedValue != null && EmpresaDropDownList.SelectedValue != "")
            {
                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            }
            else
            {
                ObjProducao.IDEmpresa = 0;
            }

            if (DataInicialTextBox.Text != null && DataInicialTextBox.Text != "")
            {
                ObjProducao.DataInicial = (Convert.ToDateTime(DataInicialTextBox.Text)).ToString("yyyy-MM-dd");
            }
            else
            {
                ObjProducao.DataInicial = "";
            }

            if (DataFinalTextBox.Text != null && DataFinalTextBox.Text != "")
            {
                ObjProducao.DataFinal = (Convert.ToDateTime(DataFinalTextBox.Text)).ToString("yyyy-MM-dd");
            }
            else
            {
                ObjProducao.DataFinal = "";
            }

            if (PedidoSAPTextBox.Text != null && PedidoSAPTextBox.Text != "")
            {
                ObjProducao.NumeroPedidoSAP = Convert.ToInt32(PedidoSAPTextBox.Text);
            }
            else
            {
                ObjProducao.NumeroPedidoSAP = 0;
            }

            if (PedidoCRMTextBox.Text != null && PedidoCRMTextBox.Text != "")
            {
                ObjProducao.NumeroPedidoCRM = Convert.ToInt32(PedidoCRMTextBox.Text);
            }
            else
            {
                ObjProducao.NumeroPedidoCRM = 0;
            }

            if (ClienteTextBox.Text != null && ClienteTextBox.Text != "")
            {
                ObjProducao.Cliente = ClienteTextBox.Text.ToString();
            }
            else
            {
                ObjProducao.Cliente = "";
            }

            if (StatusDropDownList.SelectedValue != null && StatusDropDownList.SelectedValue != "")
            {
                ObjProducao.Status = StatusDropDownList.SelectedValue.ToString();
            }
            else
            {
                ObjProducao.Status = "";
            }
        }

        public void CarregaDadosNaTela()
        {
            ValidaDadosSessao();

            //EMPRESA
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.SelectedValue = ObjProducao.IDEmpresa.ToString();
            EmpresaDropDownList.Enabled = false;

            StatusDropDownList.Items.Insert(0, new ListItem("Todos", ""));
            StatusDropDownList.SelectedValue = "O";

            IDOrdemServico.Value = ObjProducao.OrdemServico.ToString();
            OrdemServicoTextBox.Text = ObjProducao.OrdemServico.ToString();
            OrdemServicoTextBox.Enabled = false;
        }

        public void AtualizaGrid()
        {
            DataTable OBJDataTable = new DataTable();

            if (Session["OrdensServicoProdutosDataTable"] != null)
            {
                OBJDataTable = (DataTable)Session["OrdensServicoProdutosDataTable"];

                OrdensServicoGridView.DataSource = OBJDataTable;
                OrdensServicoGridView.DataBind();
                OrdensServicoMultiView.Visible = true;
            }

            Session["OrdensServicoProdutosDataTable"] = OBJDataTable;
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            Session["OrdensServicoProdutosDataTable"] = null;

            CarregaDadosDaTela();

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.RecuperaListaOrdensServicoProdutos();

            OrdensServicoGridView.DataSource = OBJDataTable;
            OrdensServicoGridView.DataBind();
            OrdensServicoMultiView.Visible = true;

            Session["OrdensServicoProdutosDataTable"] = OBJDataTable;
        }

        protected void OrdensServicoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OrdensServicoGridView.PageIndex = e.NewPageIndex;
            AtualizaGrid();
        }

        protected void IncluirPedidosButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            int cont = 0;

            DataTable OBJDataTable = new DataTable();

            if (Session["OrdensServicoProdutosDataTable"] != null)
            {
                OBJDataTable = (DataTable)Session["OrdensServicoProdutosDataTable"];
            }

            ValidaDadosSessao();

            ObjProducao.CodigoUsuario = Session["Usuario"].ToString();

            foreach (DataRow linha in OBJDataTable.Rows)
            {
                if (Convert.ToBoolean(linha["Selecionado"]))
                {
                    ObjProducao.IDProduto = Convert.ToInt32(linha["IDProduto"]);
                    erro += ObjProducao.ValidaDepositoPadraoProdutosRelacionais();
                }
            }

            if (erro == "")
            {
                foreach (DataRow linha in OBJDataTable.Rows)
                {
                    if (Convert.ToBoolean(linha["Selecionado"]))
                    {
                        //if (linha["LiberadoProducaoADM"].ToString() != "Sim")
                        if (!linha["LiberadoProducaoADM"].ToString().Equals("Sim"))
                            erro = "O pedido de número " + Convert.ToInt32(linha["NumeroPedidoSAP"]) + "(SAP) não está liberado para produção.";

                        if (erro != "") break;
                    }
                }
            }

            if (erro == "")
            {
                foreach (DataRow linha in OBJDataTable.Rows)
                {
                    if (Convert.ToBoolean(linha["Selecionado"]))
                    {
                        ObjProducao.NumeroPedidoSAP = Convert.ToInt32(linha["NumeroPedidoSAP"]);
                        ObjProducao.IDITemSAP = Convert.ToInt32(linha["IDITemSAP"]);
                        ObjProducao.IDProduto = Convert.ToInt32(linha["IDProduto"]);
                        ObjProducao.QuantidadePedido = Convert.ToDecimal(linha["Quantidade"]);
                        ObjProducao.Planejada = Convert.ToDecimal(linha["Planejada"]);
                        ObjProducao.Estoque = Convert.ToBoolean(linha["Estoque"]);
                        ObjProducao.DataEntrega = Convert.ToDateTime(linha["DataEntrega"].ToString()).ToString("yyyy-MM-dd");
                        cont += 1;
                        erro = ObjProducao.GravaOrdensProducao();

                        if (erro != "") break;
                    }
                }
            }
            else
            {
                Session["Msg"] = erro;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }

            if (erro != "")
            {
                Session["Msg"] = erro;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else if (cont > 0)
            {
                Session["Msg"] = "Sucesso na inclusão.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
                BuscarLinkButton_Click(sender, e);
            }

            if (cont <= 0 && erro == "")
            {
                Session["Msg"] = "Selecione algum pedido";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }

        }

        protected void SelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ObjProducao.Selecionado = ((CheckBox)((Control)sender).FindControl("SelCheckBox")).Checked;

            ObjProducao.IDITemSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("ItemLabel")).Text);
            ObjProducao.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("PedidoLabel")).Text);

            DataTable OBJDataTable = new DataTable();

            if (Session["OrdensServicoProdutosDataTable"] != null)
            {
                OBJDataTable = (DataTable)Session["OrdensServicoProdutosDataTable"];
            }

            foreach (System.Data.DataColumn col in OBJDataTable.Columns)
            {
                col.ReadOnly = false;
            }

            if (ObjProducao.Selecionado == true)
            {
                OBJDataTable.AsEnumerable().Where(row => row.Field<int>(("IDITemSAP")) == ObjProducao.IDITemSAP
                && row.Field<int>(("NumeroPedidoSAP")) == ObjProducao.NumeroPedidoSAP).Select(b => b["Selecionado"] = true).ToList();
            }
            if (ObjProducao.Selecionado == false)
            {
                OBJDataTable.AsEnumerable().Where(row => row.Field<int>(("IDITemSAP")) == ObjProducao.IDITemSAP
                && row.Field<int>(("NumeroPedidoSAP")) == ObjProducao.NumeroPedidoSAP).Select(b => b["Selecionado"] = false).ToList();
            }

            Session["OrdensServicoProdutosDataTable"] = OBJDataTable;

            AtualizaGrid();
        }

        protected void PlanejadaTextBox_TextChanged(object sender, EventArgs e)
        {
            ObjProducao.Planejada = Convert.ToDecimal(((TextBox)((Control)sender).FindControl("PlanejadaTextBox")).Text);

            ObjProducao.IDITemSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("ItemLabel")).Text);
            ObjProducao.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("PedidoLabel")).Text);

            DataTable OBJDataTable = new DataTable();

            if (Session["OrdensServicoProdutosDataTable"] != null)
            {
                OBJDataTable = (DataTable)Session["OrdensServicoProdutosDataTable"];
            }

            foreach (System.Data.DataColumn col in OBJDataTable.Columns)
            {
                col.ReadOnly = false;
            }

            if (ObjProducao.Planejada > 0)
            {
                OBJDataTable.AsEnumerable().Where(row => row.Field<int>(("IDITemSAP")) == ObjProducao.IDITemSAP
                && row.Field<int>(("NumeroPedidoSAP")) == ObjProducao.NumeroPedidoSAP).Select(b => b["Planejada"] = ObjProducao.Planejada).ToList();
            }

            Session["OrdensServicoProdutosDataTable"] = OBJDataTable;

            AtualizaGrid();
        }

        protected void EstqCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ObjProducao.Estoque = ((CheckBox)((Control)sender).FindControl("EstqCheckBox")).Checked;

            ObjProducao.IDITemSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("ItemLabel")).Text);
            ObjProducao.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("PedidoLabel")).Text);

            DataTable OBJDataTable = new DataTable();

            if (Session["OrdensServicoProdutosDataTable"] != null)
            {
                OBJDataTable = (DataTable)Session["OrdensServicoProdutosDataTable"];
            }

            foreach (System.Data.DataColumn col in OBJDataTable.Columns)
            {
                col.ReadOnly = false;
            }

            if (ObjProducao.Estoque == true)
            {
                OBJDataTable.AsEnumerable().Where(row => row.Field<int>(("IDITemSAP")) == ObjProducao.IDITemSAP
                && row.Field<int>(("NumeroPedidoSAP")) == ObjProducao.NumeroPedidoSAP).Select(b => b["Estoque"] = true).ToList();
            }
            if (ObjProducao.Estoque == false)
            {
                OBJDataTable.AsEnumerable().Where(row => row.Field<int>(("IDITemSAP")) == ObjProducao.IDITemSAP
                && row.Field<int>(("NumeroPedidoSAP")) == ObjProducao.NumeroPedidoSAP).Select(b => b["Estoque"] = false).ToList();
            }

            Session["OrdensServicoProdutosDataTable"] = OBJDataTable;

            AtualizaGrid();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoPrincipalWebForm.aspx?indmnu=3");
        }

        protected void ValidaDadosSessao()
        {
            if (Session["OrdensDeServico"] != null)
            {
                ObjProducao = (producao)Session["OrdensDeServico"];
            }
            else
            {
                Session["Msg"] = "A sua sessão expirou.";

                Response.Redirect("~/Producao/OrdensDeServicoWebForm.aspx?indmnu=3");
            }
        }

    }
}