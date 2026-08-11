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
    public partial class FrmAgendaVisitaDetalhe : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        public usuario ObjUsuarioClass = new usuario();
        public funcoes mdlFuncoes = new funcoes();
        public clsEntidades ObjEntidadesClass = new clsEntidades();
        public AgendaVisitaClass ObjAgendaVisitaClass = new AgendaVisitaClass();
        public ProdutoVisitaClass ObjProdutoVisita = new ProdutoVisitaClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                if (Session["ObjAgendaVisitaClass"] != null)
                {
                    ObjAgendaVisitaClass = (GerencialVendas.AgendaVisitaClass)Session["ObjAgendaVisitaClass"];
                    CarregaDadosNaTela();
                }


                //Combo vendedor
                #region Combo Vendedor
                mdlFuncoes.Usucod = Session["usuario"].ToString();
                VendCodDropDownList.DataSource = mdlFuncoes.Consulta_Vendedor(Session["usuario"].ToString());
                VendCodDropDownList.DataTextField = "VendNome";
                VendCodDropDownList.DataValueField = "VendCod";
                VendCodDropDownList.DataBind();
                VendCodDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                VendCodDropDownList.Focus();

                /*
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                string vendCod = ObjEntidadesClass.Lista_Vendedor_Logado();
                VendCodDropDownList.SelectedValue = vendCod;

                if (vendCod != "")
                    VendCodDropDownList.Enabled = false;*/


                #endregion

            }

        }

        protected void Cnpj_CpfTextBox_TextChanged(object sender, EventArgs e)
        {

            ObjEntidadesClass = new clsEntidades();
            ObjEntidadesClass.EntCpfCgc = Cnpj_CpfTextBox.Text.Trim().Replace("-", "");
            ObjEntidadesClass.EntCpfCgc = ObjEntidadesClass.EntCpfCgc.Replace(".", "");
            ObjEntidadesClass.EntCpfCgc = ObjEntidadesClass.EntCpfCgc.Replace("/", "");

            ObjEntidadesClass.Mostra_Entidade_EntCpfCgc();

            if (ObjEntidadesClass.EntCod != "" && ObjEntidadesClass.EntCod != null)
            {
                EntCodLiteral.Text = ObjEntidadesClass.EntCod;
                EntNomeTextBox.Text = ObjEntidadesClass.EntNome;
                UFTextBox.Text = ObjEntidadesClass.UFSIGLA;
                CidNomeCompTextBox.Text = ObjEntidadesClass.CidNomeComp;

                if (ObjEntidadesClass.ListEntFone != null)
                {
                    if (ObjEntidadesClass.ListEntFone.Count > 0)
                    {
                        TelefoneTextBox.Text = ObjEntidadesClass.ListEntFone.First().EntFoneDDD + " " + ObjEntidadesClass.ListEntFone.First().EntFoneNum;
                    }
                }


                switch (ObjEntidadesClass.StatEntComercial.ToUpper())
                {
                    case "INATIVO":
                        CondicaoClienteRadioButtonList.SelectedValue = "Inativo";
                        break;
                    default:
                        CondicaoClienteRadioButtonList.SelectedValue = "Manutenção";
                        break;
                }


            }

            EntNomeTextBox.Focus();

        }

        public void CarregaDadosNaTela()
        {

            AgendaStatusDropDownList.SelectedValue = ObjAgendaVisitaClass.AgendaStatus;
            Cnpj_CpfTextBox.Text = ObjAgendaVisitaClass.EntCpfCgc;
            EntCodLiteral.Text = ObjAgendaVisitaClass.EntCod;
            EntNomeTextBox.Text = ObjAgendaVisitaClass.EntNome;
            UFTextBox.Text = ObjAgendaVisitaClass.UfSigla;
            CidNomeCompTextBox.Text = ObjAgendaVisitaClass.CidNomeComp;
            DataVisitaTextBox.Text = ObjAgendaVisitaClass.DataVisita.ToString("yyyy-MM-dd");
            TelefoneTextBox.Text = ObjAgendaVisitaClass.Telefone;
            ObservacaoTextBox.Text = ObjAgendaVisitaClass.Observacao;
            CondicaoClienteRadioButtonList.SelectedValue = ObjAgendaVisitaClass.CondicaoCliente;


            if (ObjAgendaVisitaClass.VendCod != "")
            {
                VendCodDropDownList.SelectedValue = ObjAgendaVisitaClass.VendCod;
            }


            Atualizar_Gid();

            if (ObjAgendaVisitaClass.AgendaStatus.ToUpper() == "FINALIZADA")
            {
                SalvarLinkButton.Visible = false;
                NovoLinkButton.Visible = false;
                ProdutoVisitaGridView.Columns[9].Visible = false;
            }

        }

        public void CarregaDadosDaTela()
        {

            if (Session["ObjAgendaVisitaClass"] != null)
            {
                ObjAgendaVisitaClass = (AgendaVisitaClass)Session["ObjAgendaVisitaClass"];
            }
            else
            {
                ObjAgendaVisitaClass = new AgendaVisitaClass();
            }


            if (ObjAgendaVisitaClass.AGENDA_VISITA_ID > 0)
            {
                ObjAgendaVisitaClass.TipoOperacao = "Alterar";
            }
            else
            {

                ObjAgendaVisitaClass.TipoOperacao = "Incluir";
            }

            ObjAgendaVisitaClass.UsuCod = Session["usuario"].ToString();
            ObjAgendaVisitaClass.AgendaStatus = AgendaStatusDropDownList.SelectedValue;
            ObjAgendaVisitaClass.EntCpfCgc = Cnpj_CpfTextBox.Text;
            ObjAgendaVisitaClass.EntCod = EntCodLiteral.Text;
            ObjAgendaVisitaClass.EntNome = EntNomeTextBox.Text;
            ObjAgendaVisitaClass.UfSigla = UFTextBox.Text.ToUpper();
            ObjAgendaVisitaClass.CidNomeComp = CidNomeCompTextBox.Text;
            ObjAgendaVisitaClass.DataVisita = Convert.ToDateTime(DataVisitaTextBox.Text);
            ObjAgendaVisitaClass.VendCod = VendCodDropDownList.SelectedValue;
            ObjAgendaVisitaClass.Telefone = TelefoneTextBox.Text;
            ObjAgendaVisitaClass.Observacao = ObservacaoTextBox.Text;
            ObjAgendaVisitaClass.CondicaoCliente = CondicaoClienteRadioButtonList.SelectedValue;



        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string Retorno = "";
            string Msg = "";
            CarregaDadosDaTela();


            if (ObjAgendaVisitaClass.TipoOperacao == "Alterar")
            {
                Retorno = ObjAgendaVisitaClass.ALTERA_AGENDA();
                Msg = "Agenda alterada com Sucesso!";

            }
            else
            {
                Retorno = ObjAgendaVisitaClass.INSERE_AGENDA();
                Msg = "Agenda incluida com Sucesso!";
            }


            if (ObjAgendaVisitaClass.ListProdutoVisita != null)
            {
                if (ObjAgendaVisitaClass.ListProdutoVisita.Count > 0)
                {


                    //Percorre a lista 
                    for (int PV = 0; PV < ObjAgendaVisitaClass.ListProdutoVisita.Count; PV++)
                    {

                        ObjAgendaVisitaClass.ListProdutoVisita[PV].AGENDA_VISITA_ID = ObjAgendaVisitaClass.AGENDA_VISITA_ID;



                        switch (ObjAgendaVisitaClass.ListProdutoVisita[PV].TipoOperacao)
                        {
                            case "Incluir":
                                Retorno += ObjAgendaVisitaClass.ListProdutoVisita[PV].INSERE_PRODUTO_AGENDA();
                                break;

                            case "Alterar":
                                Retorno += ObjAgendaVisitaClass.ListProdutoVisita[PV].ALTERA_PRODUTO_AGENDA();
                                break;

                            case "Remover":
                                Retorno += ObjAgendaVisitaClass.ListProdutoVisita[PV].DELETA_PRODUTO_AGENDA();
                                break;
                        }


                    }




                }

            }





            if (Retorno == "")
            {
                Session["ObjAgendaVisitaClass"] = null;
                Session["Msg"] = Msg;
                Response.Redirect("FrmAgendaVisita.aspx?indmnu=5");

            }
            else
            {
                Response.Write("<script>alert(\"" + Retorno + "\");</script>");
            }
        }

        protected void Atualizar_Gid()
        {

            if (ObjAgendaVisitaClass.ListProdutoVisita != null)
            {
                ProdutoVisitaGridView.DataSource = ObjAgendaVisitaClass.ListProdutoVisita.Where(PV => PV.TipoOperacao != "Remover").ToList();
                ProdutoVisitaGridView.DataBind();
            }


        }

        protected void ProdutoVisitaGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ProdutoVisitaGridView.PageIndex = e.NewPageIndex;
            Atualizar_Gid();
        }



        protected void VoltarLinkButton_Click(object sender, EventArgs e)
        {
            Session["ObjAgendaVisitaClass"] = null;
            Response.Redirect("FrmAgendaVisita.aspx?indmnu=5");
        }






        protected void DetalheButton_Click(object sender, EventArgs e)
        {

            ObjProdutoVisita = new ProdutoVisitaClass();

            //Carregando dados do Grid
            ObjProdutoVisita.PRODUTO_VISITA_ID = Convert.ToInt32(((Label)((Control)sender).FindControl("PRODUTO_VISITA_IDLabel")).Text);

            if (ObjProdutoVisita.PRODUTO_VISITA_ID < 0)
            {
                ObjAgendaVisitaClass = (GerencialVendas.AgendaVisitaClass)Session["ObjAgendaVisitaClass"];
                ObjProdutoVisita = (ProdutoVisitaClass)ObjAgendaVisitaClass.ListProdutoVisita.Where(PV => PV.PRODUTO_VISITA_ID == ObjProdutoVisita.PRODUTO_VISITA_ID).First();
            }
            else
            {
                ObjProdutoVisita.MOSTRA_PRODUTO_AGENDA();
            }

            Session["ObjProdutoVisita"] = ObjProdutoVisita;

            //Redireciona
            Response.Redirect("FrmAgendaVisitaDetalheProdutoVisita.aspx?indmnu=5");

        }



        protected void RemoverButton_Click(object sender, EventArgs e)
        {

            ObjProdutoVisita = new ProdutoVisitaClass();

            //Carregando dados do Grid
            ObjProdutoVisita.PRODUTO_VISITA_ID = Convert.ToInt32(((Label)((Control)sender).FindControl("PRODUTO_VISITA_IDLabel")).Text);


            if (Session["ObjAgendaVisitaClass"] != null)
            {
                ObjAgendaVisitaClass = (GerencialVendas.AgendaVisitaClass)Session["ObjAgendaVisitaClass"];
                ObjAgendaVisitaClass.Remover_ProdutoVisita(ObjProdutoVisita);
                Atualizar_Gid();
            }

        }


        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            Session["ObjProdutoVisita"] = null;

            CarregaDadosDaTela();
            Session["ObjAgendaVisitaClass"] = ObjAgendaVisitaClass;

            Response.Redirect("FrmAgendaVisitaDetalheProdutoVisita.aspx?indmnu=5");
        }

    }
}