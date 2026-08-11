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
    public partial class Dashboard : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        public funcoes mdlFuncoes = new funcoes();
        public UtilClass ObjUtil = new UtilClass();
        public DashBoardClass ObjDashBoardClass = new DashBoardClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {



                #region Combo de Filtro por Gestor/Classe/Vendedor
                ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
                ObjDashBoardClass.Consulta_Filtro_Gestor();

                switch (ObjDashBoardClass.Acesso)
                {
                    case "Total":
                        Carrega_Combo_Gestor();
                        break;
                    case "Gestor":
                        Carrega_Combo_Gestor();
                        break;
                    case "Vendedor":
                        Carrega_Combo_Vendedor();
                        GestorLabel.Visible = false;
                        GestorDropDownList.Visible = false;
                        GestorLinkButton.Visible = false;
                        GestorRequiredFieldValidator.Visible = false;


                        ClasseLabel.Visible = false;
                        ClasseDropDownList.Visible = false;
                        ClasseLinkButton.Visible = false;
                        ClasseRequiredFieldValidator.Visible = false;
                        break;
                }





                #endregion






            }

        }


        #region Necessario Combo de Filtro por Gestor/Classe/Vendedor

        protected void Carrega_Combo_Gestor()
        {
            ObjDashBoardClass = new DashBoardClass();

            ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
            GestorDropDownList.DataSource = ObjDashBoardClass.Consulta_Gestores();
            GestorDropDownList.DataTextField = "UsuCod";
            GestorDropDownList.DataValueField = "UsuCod";
            GestorDropDownList.DataBind();

            GestorDropDownList.Items.Insert(0, new ListItem("Todos", "0000000"));
            ClasseDropDownList.Items.Insert(0, new ListItem("Click em Buscar Classe!", ""));
            VendedorDropDownList.Items.Insert(0, new ListItem("Busque a Classe antes!", ""));


        }

        protected void GestorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjDashBoardClass = new DashBoardClass();

            ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
            ObjDashBoardClass.UsuCodAux = ObjUtil.RecuperaDados_Select(GestorDropDownList);
            ClasseDropDownList.DataSource = ObjDashBoardClass.Consulta_Classes_Gestores();
            ClasseDropDownList.DataTextField = "VendClasseDescr";
            ClasseDropDownList.DataValueField = "VendClasseCod";
            ClasseDropDownList.DataBind();
            ClasseDropDownList.Items.Insert(0, new ListItem("Todas", "0000000"));
            ClasseDropDownList.Focus();

            VendedorDropDownList.Items.Insert(0, new ListItem("Click em Buscar Vendedor", ""));

            filtros.Attributes["class"] = "collapse in";
        }

        protected void Carrega_Combo_Vendedor()
        {
            mdlFuncoes.Usucod = Session["usuario"].ToString();
            VendedorDropDownList.DataSource = mdlFuncoes.Consulta_Vendedor(Session["usuario"].ToString());
            VendedorDropDownList.DataTextField = "VendNome";
            VendedorDropDownList.DataValueField = "VendCod";
            VendedorDropDownList.DataBind();

            VendedorDropDownList.Items.Insert(0, new ListItem("Todos", "0000000"));

            VendedorDropDownList.Focus();
        }

        protected void ClasseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            ObjDashBoardClass = new DashBoardClass();
            ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
            ObjDashBoardClass.UsuCodAux = ObjUtil.RecuperaDados_Select(GestorDropDownList);
            ObjDashBoardClass.VendClasseCod = ObjUtil.RecuperaDados_Select(ClasseDropDownList);
            VendedorDropDownList.DataSource = ObjDashBoardClass.Consulta_Vendedor_Classes();
            VendedorDropDownList.DataTextField = "VendNome";
            VendedorDropDownList.DataValueField = "VendCod";
            VendedorDropDownList.DataBind();
            VendedorDropDownList.Items.Insert(0, new ListItem("Todos", "0000000"));
            VendedorDropDownList.Focus();

            filtros.Attributes["class"] = "collapse in";
        }

        #endregion



        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            ObjDashBoardClass = new DashBoardClass();

            ObjDashBoardClass.VendCod = ObjUtil.RecuperaDados_Select(VendedorDropDownList);//VendedorDropDownList.SelectedValue;
            ObjDashBoardClass.UsuCod = Session["usuario"].ToString();
            ObjDashBoardClass.UsuCodAux = ObjUtil.RecuperaDados_Select(GestorDropDownList);
            ObjDashBoardClass.VendClasseCod = ObjUtil.RecuperaDados_Select(ClasseDropDownList);


            filtros.Attributes["class"] = "collapse";
            posicaoEntrada.Attributes["class"] = "collapse in";
            posicao.Attributes["class"] = "collapse in";
            PedidosPendentes.Attributes["class"] = "collapse in";
            PedidosFaturados.Attributes["class"] = "collapse in";
            Devolucoes.Attributes["class"] = "collapse in";

            Cria_Tabela_Posicao_Carteira();
            Cria_Tabela_Pedidos_Status_Entrada();
            Cria_Tabela_Pedidos_Status_Pendentes();
            Cria_Tabela_Pedidos_Status_Faturados();
            Cria_Tabela_Pedidos_Status_Devolucoes();

        }

        public void Cria_Tabela_Posicao_Carteira()
        {
            DataTable outputTable = new DataTable();
            outputTable = ObjDashBoardClass.Consulta_Posicao_Carteira();


            if (outputTable.Rows.Count > 0)
            {
                PosicaoCarteiraLiteral.Text = "";
                foreach (DataRow row in outputTable.Rows)
                {

                    PcAtivoLabel.Text = row["Ativo"].ToString();
                    PcInativoLabel.Text = row["Inativo"].ToString();
                    PcProspectivoLabel.Text = row["Prospectivo"].ToString();
                    PcNovoLabel.Text = row["Novo"].ToString();
                    PcTotalAtualLabel.Text = Convert.ToString(
                                               Convert.ToInt32(row["Ativo"].ToString()) +
                                               Convert.ToInt32(row["Inativo"].ToString()) +
                                               Convert.ToInt32(row["Prospectivo"].ToString()) +
                                               Convert.ToInt32(row["Novo"].ToString())
                                              );

                    PcMediaAtivoLabel.Text = row["MediaAtivo"].ToString();
                    PcMediaInativoLabel.Text = row["MediaInativo"].ToString();
                    PcMediaProspectivoLabel.Text = row["MediaProspectivo"].ToString();
                    PcMediaNovoLabel.Text = row["MediaNovo"].ToString();
                    PcTotalMediaLabel.Text = Convert.ToString(
                                               Convert.ToInt32(row["MediaAtivo"].ToString()) +
                                               Convert.ToInt32(row["MediaInativo"].ToString()) +
                                               Convert.ToInt32(row["MediaProspectivo"].ToString()) +
                                               Convert.ToInt32(row["MediaNovo"].ToString())
                                              );




                    if (Convert.ToInt32(row["MediaAtivo"]) >= 0)
                    {
                        PcPaAtivoLiteral.Text = " <span class=\"label label-success text-dark\"> "
                                              + row["MediaAtivo"].ToString() + "%"
                                              + " </span>"
                                              + " <i class=\"fa fa-lg fa-arrow-circle-up text-success\"></i>";
                    }
                    else
                    {

                        PcPaAtivoLiteral.Text = " <span class=\"label label-danger text-dark\"> "
                                              + row["MediaAtivo"].ToString() + "%"
                                              + " </span>"
                                              + " <i class=\"fa fa-lg fa-arrow-circle-down text-danger\"></i>";

                    }


                    if (Convert.ToInt32(row["MediaInativo"]) >= 0)
                    {
                        PcPaInativoLiteral.Text = " <span class=\"label label-success text-dark\"> "
                                              + row["MediaInativo"].ToString() + "%"
                                              + " </span>"
                                              + " <i class=\"fa fa-lg fa-arrow-circle-up text-success\"></i>";
                    }
                    else
                    {

                        PcPaInativoLiteral.Text = " <span class=\"label label-danger text-dark\"> "
                                              + row["MediaInativo"].ToString() + "%"
                                              + " </span>"
                                              + " <i class=\"fa fa-lg fa-arrow-circle-down text-danger\"></i>";

                    }


                    if (Convert.ToInt32(row["MediaProspectivo"]) >= 0)
                    {
                        PcPaProspectivoLiteral.Text = " <span class=\"label label-success text-dark\"> "
                                              + row["MediaProspectivo"].ToString() + "%"
                                              + " </span>"
                                              + " <i class=\"fa fa-lg fa-arrow-circle-up text-success\"></i>";
                    }
                    else
                    {

                        PcPaProspectivoLiteral.Text = " <span class=\"label label-danger text-dark\"> "
                                              + row["MediaProspectivo"].ToString() + "%"
                                              + " </span>"
                                              + "<i class=\"fa fa-lg fa-arrow-circle-down text-danger\"></i>";

                    }

                    if (Convert.ToInt32(row["MediaNovo"]) >= 0)
                    {
                        PcPaNovoLiteral.Text = " <span class=\"label label-success text-dark\"> "
                                              + row["MediaNovo"].ToString() + "%"
                                              + " </span>"
                                              + "<i class=\"fa fa-lg fa-arrow-circle-up text-success\"></i>";
                    }
                    else
                    {

                        PcPaNovoLiteral.Text = " <span class=\"label label-danger text-dark\"> "
                                              + row["MediaNovo"].ToString() + "%"
                                              + " </span>"
                                              + "<i class=\"fa fa-lg fa-arrow-circle-down text-danger\"></i>";

                    }







                }
            }


        }


        public void Cria_Tabela_Pedidos_Status_Entrada()
        {
            DataTable outputTable = new DataTable();

            ObjDashBoardClass.TipoPedidoStatus = "Entradas";
            outputTable = ObjDashBoardClass.Consulta_Pedidos_Status();


            DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime hoje = DateTime.Now;
            EntradaHojeLabel.Text = hoje.ToString("dd/MM/yyyy");
            
            EntradaAcumuladoLabel.Text = "de " + primeiroDia.ToString("dd/MM/yyyy") + " a " + hoje.ToString("dd/MM/yyyy");
            EntradaMesAnteriorLabel.Text = primeiroDia.ToString("MM/yyyy");


            if (outputTable.Rows.Count > 0)
            {

                foreach (DataRow row in outputTable.Rows)
                {


                    switch (row["UserLinhaProdutoLista"].ToString())
                    {

                        case "STRETCH":
                            EntradaHojeStretchLabel.Text = row["TotalHoje"].ToString() + " Kg";
                            EntradaAcumuladoStretchLabel.Text = row["TotalMesAtual"].ToString() + " Kg";
                            EntradaMesAnteriorStretchLabel.Text = row["TotalMesAnterior"].ToString() + " Kg";

                            break;


                        case "FITA PP":
                            EntradaHojeFitaPPLabel.Text = row["TotalHoje"].ToString() + " m²";
                            EntradaAcumuladoFitaPPLabel.Text = row["TotalMesAtual"].ToString() + " m²";
                            EntradaMesAnteriorFitaPPLabel.Text = row["TotalMesAnterior"].ToString() + " m²";
                            break;

                        case "FITA IMP":
                            EntradaHojeFitaImpressaLabel.Text = row["TotalHoje"].ToString() + " m²";
                            EntradaAcumuladoFitaImpressaLabel.Text = row["TotalMesAtual"].ToString() + " m²";
                            EntradaMesAnteriorFitaImpressaLabel.Text = row["TotalMesAnterior"].ToString() + " m²";
                            break;


                        case "MAQ E EQUIP":
                            EntradaHojeMaquinasLabel.Text = row["TotalHoje"].ToString() + " un";
                            EntradaAcumuladoMaquinasLabel.Text = row["TotalMesAtual"].ToString() + " un";
                            EntradaMesAnteriorMaquinasLabel.Text = row["TotalMesAnterior"].ToString() + " un";
                            break;


                    }








                }
            }


        }

        public void Cria_Tabela_Pedidos_Status_Pendentes()
        {
            DataTable outputTable = new DataTable();

            ObjDashBoardClass.TipoPedidoStatus = "Pendentes";
            outputTable = ObjDashBoardClass.Consulta_Pedidos_Status();


            DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime hoje = DateTime.Now;


            PedidosPendentesHojeLabel.Text = hoje.ToString("dd/MM/yyyy");
            PedidosPendentesAcumuladoLabel.Text = "de " + primeiroDia.ToString("dd/MM/yyyy") + " a " + hoje.ToString("dd/MM/yyyy");
            PedidosPendentesMesAnteriorLabel.Text = primeiroDia.ToString("MM/yyyy");


            if (outputTable.Rows.Count > 0)
            {

                foreach (DataRow row in outputTable.Rows)
                {


                    switch (row["UserLinhaProdutoLista"].ToString())
                    {

                        case "STRETCH":
                            PedidosPendentesHojeStretchLabel.Text = row["TotalHoje"].ToString() + " Kg";
                            PedidosPendentesAcumuladoStretchLabel.Text = row["TotalMesAtual"].ToString() + " Kg";
                            PedidosPendentesMesAnteriorStretchLabel.Text = row["TotalMesAnterior"].ToString() + " Kg";

                            break;


                        case "FITA PP":
                            PedidosPendentesHojeFitaPPLabel.Text = row["TotalHoje"].ToString() + " m²";
                            PedidosPendentesAcumuladoFitaPPLabel.Text = row["TotalMesAtual"].ToString() + " m²";
                            PedidosPendentesMesAnteriorFitaPPLabel.Text = row["TotalMesAnterior"].ToString() + " m²";
                            break;

                        case "FITA IMP":
                            PedidosPendentesHojeFitaImpressaLabel.Text = row["TotalHoje"].ToString() + " m²";
                            PedidosPendentesAcumuladoFitaImpressaLabel.Text = row["TotalMesAtual"].ToString() + " m²";
                            PedidosPendentesMesAnteriorFitaImpressaLabel.Text = row["TotalMesAnterior"].ToString() + " m²";
                            break;


                        case "MAQ E EQUIP":
                            PedidosPendentesHojeMaquinasLabel.Text = row["TotalHoje"].ToString() + " un";
                            PedidosPendentesAcumuladoMaquinasLabel.Text = row["TotalMesAtual"].ToString() + " un";
                            PedidosPendentesMesAnteriorMaquinasLabel.Text = row["TotalMesAnterior"].ToString() + " un";
                            break;


                    }








                }
            }


        }

        public void Cria_Tabela_Pedidos_Status_Faturados()
        {
            DataTable outputTable = new DataTable();

            ObjDashBoardClass.TipoPedidoStatus = "Faturados";
            outputTable = ObjDashBoardClass.Consulta_Pedidos_Status();


            DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime hoje = DateTime.Now;

            PedidosFaturadosHojeLabel.Text = hoje.ToString("dd/MM/yyyy");


            PedidosFaturadosAcumuladoLabel.Text = "de " + primeiroDia.ToString("dd/MM/yyyy") + " a " + hoje.ToString("dd/MM/yyyy");
            PedidosFaturadosMesAnteriorLabel.Text = primeiroDia.ToString("MM/yyyy");


            if (outputTable.Rows.Count > 0)
            {

                foreach (DataRow row in outputTable.Rows)
                {


                    switch (row["UserLinhaProdutoLista"].ToString())
                    {

                        case "STRETCH":
                            PedidosFaturadosHojeStretchLabel.Text = row["TotalHoje"].ToString() + " Kg";
                            PedidosFaturadosAcumuladoStretchLabel.Text = row["TotalMesAtual"].ToString() + " Kg";
                            PedidosFaturadosMesAnteriorStretchLabel.Text = row["TotalMesAnterior"].ToString() + " Kg";

                            if (Convert.ToDecimal(row["ExpectativaTotalMesAtual"].ToString()) < 0)
                            {
                                ExpectativaStretchLabel.Text = "<i class=\"fa fa-lg fa-arrow-circle-down text-danger\"></i><span class=\"label label-danger text-dark\">" + row["ExpectativaTotalMesAtual"].ToString() + " %</span>";
                            }
                            else
                            {
                                ExpectativaStretchLabel.Text = "<i class=\"fa fa-lg fa-arrow-circle-up text-success\"></i><span class=\"label label-success text-dark\"> " + row["ExpectativaTotalMesAtual"].ToString() + "%</span>";
                            }

                            break;


                        case "FITA PP":
                            PedidosFaturadosHojeFitaPPLabel.Text = row["TotalHoje"].ToString() + " m²";
                            PedidosFaturadosAcumuladoFitaPPLabel.Text = row["TotalMesAtual"].ToString() + " m²";
                            PedidosFaturadosMesAnteriorFitaPPLabel.Text = row["TotalMesAnterior"].ToString() + " m²";


                            if (Convert.ToDecimal(row["ExpectativaTotalMesAtual"].ToString()) < 0)
                            {
                                ExpectativaFitaPPLabel.Text = "<i class=\"fa fa-lg fa-arrow-circle-down text-danger\"></i><span class=\"label label-danger text-dark\">" + row["ExpectativaTotalMesAtual"].ToString() + " %</span>";
                            }
                            else
                            {
                                ExpectativaFitaPPLabel.Text = "<i class=\"fa fa-lg fa-arrow-circle-up text-success\"></i><span class=\"label label-success text-dark\"> " + row["ExpectativaTotalMesAtual"].ToString() + "%</span>";
                            }

                            break;

                        case "FITA IMP":
                            PedidosFaturadosHojeFitaImpressaLabel.Text = row["TotalHoje"].ToString() + " m²";
                            PedidosFaturadosAcumuladoFitaImpressaLabel.Text = row["TotalMesAtual"].ToString() + " m²";
                            PedidosFaturadosMesAnteriorFitaImpressaLabel.Text = row["TotalMesAnterior"].ToString() + " m²";


                            if (Convert.ToDecimal(row["ExpectativaTotalMesAtual"].ToString()) < 0)
                            {
                                ExpectativaFitaImpressaLabel.Text = "<i class=\"fa fa-lg fa-arrow-circle-down text-danger\"></i><span class=\"label label-danger text-dark\">" + row["ExpectativaTotalMesAtual"].ToString() + " %</span>";
                            }
                            else
                            {
                                ExpectativaFitaImpressaLabel.Text = "<i class=\"fa fa-lg fa-arrow-circle-up text-success\"></i><span class=\"label label-success text-dark\"> " + row["ExpectativaTotalMesAtual"].ToString() + "%</span>";
                            }

                            break;


                        case "MAQ E EQUIP":
                            PedidosFaturadosHojeMaquinasLabel.Text = row["TotalHoje"].ToString() + " un";
                            PedidosFaturadosAcumuladoMaquinasLabel.Text = row["TotalMesAtual"].ToString() + " un";
                            PedidosFaturadosMesAnteriorMaquinasLabel.Text = row["TotalMesAnterior"].ToString() + " un";


                            if (Convert.ToDecimal(row["ExpectativaTotalMesAtual"].ToString()) < 0)
                            {
                                ExpectativaMaquinasLabel.Text = "<i class=\"fa fa-lg fa-arrow-circle-down text-danger\"></i><span class=\"label label-danger text-dark\">" + row["ExpectativaTotalMesAtual"].ToString() + " %</span>";
                            }
                            else
                            {
                                ExpectativaMaquinasLabel.Text = "<i class=\"fa fa-lg fa-arrow-circle-up text-success\"></i><span class=\"label label-success text-dark\"> " + row["ExpectativaTotalMesAtual"].ToString() + "%</span>";
                            }

                            break;


                    }








                }
            }


        }

        public void Cria_Tabela_Pedidos_Status_Devolucoes()
        {
            DataTable outputTable = new DataTable();

            ObjDashBoardClass.TipoPedidoStatus = "Devoluções";
            outputTable = ObjDashBoardClass.Consulta_Pedidos_Status();


            DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime hoje = DateTime.Now;

            DevolucoesHojeLabel.Text = hoje.ToString("dd/MM/yyyy");


            DevolucoesAcumuladoLabel.Text = "de " + primeiroDia.ToString("dd/MM/yyyy") + " a " + hoje.ToString("dd/MM/yyyy");
            DevolucoesMesAnteriorLabel.Text = primeiroDia.ToString("MM/yyyy");


            if (outputTable.Rows.Count > 0)
            {

                foreach (DataRow row in outputTable.Rows)
                {


                    switch (row["UserLinhaProdutoLista"].ToString())
                    {

                        case "STRETCH":
                            DevolucoesHojeStretchLabel.Text = row["TotalHoje"].ToString() + " Kg";
                            DevolucoesAcumuladoStretchLabel.Text = row["TotalMesAtual"].ToString() + " Kg";
                            DevolucoesMesAnteriorStretchLabel.Text = row["TotalMesAnterior"].ToString() + " Kg";

                            break;


                        case "FITA PP":
                            DevolucoesHojeFitaPPLabel.Text = row["TotalHoje"].ToString() + " m²";
                            DevolucoesAcumuladoFitaPPLabel.Text = row["TotalMesAtual"].ToString() + " m²";
                            DevolucoesMesAnteriorFitaPPLabel.Text = row["TotalMesAnterior"].ToString() + " m²";
                            break;

                        case "FITA IMP":
                            PedidosFaturadosHojeFitaImpressaLabel.Text = row["TotalHoje"].ToString() + " m²";
                            PedidosFaturadosAcumuladoFitaImpressaLabel.Text = row["TotalMesAtual"].ToString() + " m²";
                            PedidosFaturadosMesAnteriorFitaImpressaLabel.Text = row["TotalMesAnterior"].ToString() + " m²";
                            break;


                        case "MAQ E EQUIP":
                            DevolucoesHojeMaquinasLabel.Text = row["TotalHoje"].ToString() + " un";
                            DevolucoesAcumuladoMaquinasLabel.Text = row["TotalMesAtual"].ToString() + " un";
                            DevolucoesMesAnteriorMaquinasLabel.Text = row["TotalMesAnterior"].ToString() + " un";
                            break;


                    }








                }
            }


        }


    }
}