using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using System.IO;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidade
{
    public partial class FrmAbaFinanceiro : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlFuncoes = new funcoes();
        clsEntidades ObjEntidadesClass = new clsEntidades();
        DocEntidadeClass DocEntidadeClass = new DocEntidadeClass();
        usuario ObjUsuarioClass = new usuario();
        criptografia mdlCriptografia = new criptografia();
        EntidadeCategoriaClass ObjEntidadeCategoriaClass = new EntidadeCategoriaClass();
        clsCondPag clsCondPag = new GerencialVendas.clsCondPag();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }


            if (!IsPostBack)
            {
                //Valida Acesso
                OBJSessao.ValidaAcesso();

                //Combo Condição de Recebimento
                CondicaoPagamentoDropDownList.DataSource = clsCondPag.Consulta_Condicao_Recebimento_CRM();
                CondicaoPagamentoDropDownList.DataTextField = "Condicao";
                CondicaoPagamentoDropDownList.DataValueField = "CondPagCod";
                CondicaoPagamentoDropDownList.DataBind();
                CondicaoPagamentoDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

                //Combo Tipo de Cobranca
                TipoCobCodDropDownList.DataSource = ObjEntidadesClass.Consulta_Tipo_Cobranca();
                TipoCobCodDropDownList.DataTextField = "TipoCobranca";
                TipoCobCodDropDownList.DataValueField = "TipoCobCod";
                TipoCobCodDropDownList.DataBind();
                TipoCobCodDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                TipoCobCodDropDownList.SelectedValue = "0000004";

                //Combo Categoria secundária               
                CategoriaSecundariaDropDownList.DataSource = ObjEntidadesClass.Consulta_Categoria_Entidade_Geral("Cliente");
                CategoriaSecundariaDropDownList.DataTextField = "Categoria";
                CategoriaSecundariaDropDownList.DataValueField = "CategCodEstr";
                CategoriaSecundariaDropDownList.DataBind();
                CategoriaSecundariaDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

                //CategoriaSecundariaDropDownList.Disabled = true;

                //Carrega Dados na Tela
                if (Session["clsEntidades"] != null)
                {
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
                    CarregaDadosNaTela();


                    if (ObjEntidadesClass.TipoOperacao == "Consultar")
                    {
                        BloqueiaCampos();
                    }

                }
                
            }
        }

        protected void CondicaoPagamentoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CondicaoPagamentoDropDownList.SelectedValue == "OUTRAS")
            {
                OutraCondPagLabel.Visible = true;
                OutraCondPagTextBox.Visible = true;
            }
            else
            {
                OutraCondPagLabel.Visible = false;
                OutraCondPagTextBox.Visible = false;
                OutraCondPagTextBox.Text = "";
            }
        }

        public string CarregaDadosDaTela()
        {
            #region

            //Condicao de Pagamento
            ObjEntidadesClass.CondPagCod = CondicaoPagamentoDropDownList.SelectedValue;


            //Tipo de Cobranca(Forma de Pagamento)
            ObjEntidadesClass.TipoCobCod = TipoCobCodDropDownList.SelectedValue;

            //Outra Condição de Recebimento
            ObjEntidadesClass.UserOutrosCondPagCod = OutraCondPagTextBox.Text.Trim();


            if (EntValLimCredTextBox.Text != "" && EntValLimCredTextBox.Text != null)
            {
                ObjEntidadesClass.EntValLimCred = Convert.ToDecimal(EntValLimCredTextBox.Text);
            }
            else
            {
                ObjEntidadesClass.EntValLimCred = 0;
            }


            ObjEntidadeCategoriaClass.EntCod = ObjEntidadesClass.EntCod;
            ObjEntidadeCategoriaClass.CategCodEstr = ObjEntidadesClass.CategCodEstr;
            ObjEntidadesClass.AdicionarCategoria(ObjEntidadeCategoriaClass);

            //Cartao CNPJ
            ObjEntidadesClass.UsuCartaoCnpj = UsuCartaoCNPJDropDownList.SelectedValue;

            //Sintegra
            ObjEntidadesClass.UsuSintegra = UsuSintegraDropDownList.SelectedValue;

            #endregion

            return "";
        }


        public string CarregaDadosNaTela()
        {          
            //Tipo de Cobranca(Forma de Pagamento)
            if (ObjEntidadesClass.TipoCobCod != null)
                TipoCobCodDropDownList.SelectedValue = ObjEntidadesClass.TipoCobCod;

            EntValLimCredTextBox.Text = ObjEntidadesClass.EntValLimCred.ToString();

            //Cartao CNPJ
            UsuCartaoCNPJDropDownList.SelectedValue = ObjEntidadesClass.UsuCartaoCnpj;

            //Sintegra
            UsuSintegraDropDownList.SelectedValue = ObjEntidadesClass.UsuSintegra;


            //Categoria CNAE 2
            Atualizar_Grid_Categoria();


            //Outra Condição de Recebimento
            if (ObjEntidadesClass.CondPagCod == "" || ObjEntidadesClass.CondPagCod == null)
            {
                if (ObjEntidadesClass.UserOutrosCondPagCod != "")
                {
                    CondicaoPagamentoDropDownList.SelectedValue = "OUTRAS";
                    OutraCondPagLabel.Text = ObjEntidadesClass.UserOutrosCondPagCod;
                    CondicaoPagamentoDropDownList_SelectedIndexChanged(null, null);

                }
            }
            /*else
            {
                CondicaoPagamentoDropDownList.SelectedValue = ObjEntidadesClass.CondPagCod;
                CondicaoPagamentoDropDownList_SelectedIndexChanged(null, null);
            }
            */

            Atualizar_Grid_Cond_Pag();

            return "";
        }

        protected void ProximoPasso_Click(object sender, EventArgs e)
        {
            //Descarega a sessao
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


            if (ObjEntidadesClass.TipoOperacao != "Consulta")
            {
                //Carrega os Dados da Tela
                CarregaDadosDaTela();
            }

            //Guarda os dados em Session
            Session["clsEntidades"] = ObjEntidadesClass;


            //Chama a proxima Tela
            Response.Redirect("FrmAbaFiscal.aspx?indmnu=2");       
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaPerfilDeConsumo.aspx?indmnu=2");
        }

        
        protected void AdicionarCategoriaButton_Click(object sender, EventArgs e)
        {
            string Validacao = "";

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            if (CategoriaSecundariaDropDownList.Value == "")
            {
                Validacao = "Selecione uma categoria";
            }

            if (Validacao == "")
            {
                int AUXCodigo = 0;

                if (ObjEntidadesClass.ListEntCategoriaClass != null && ObjEntidadesClass.ListEntCategoriaClass.Count > 0)
                {
                    AUXCodigo = ObjEntidadesClass.ListEntCategoriaClass.OrderBy(C => C.Codigo).First().Codigo;
                }

                if (AUXCodigo < 0)
                {

                    ObjEntidadeCategoriaClass.Codigo = AUXCodigo - 1;
                }
                else
                {
                    ObjEntidadeCategoriaClass.Codigo = (AUXCodigo + 1);
                }

                ObjEntidadeCategoriaClass.EntCod = ObjEntidadesClass.EntCod;
                ObjEntidadeCategoriaClass.CategCodEstr = CategoriaSecundariaDropDownList.Value;
                ObjEntidadeCategoriaClass.Categoria = CategoriaSecundariaDropDownList.Items[CategoriaSecundariaDropDownList.SelectedIndex].Text;
                ObjEntidadesClass.AdicionarCategoria(ObjEntidadeCategoriaClass);               

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid_Categoria();

                CategoriaSecundariaDropDownList.Value = "";
            }
            else
            {                
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Validacao, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RemoverCategoriaButton_Click(object sender, EventArgs e)
        {
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                ObjEntidadeCategoriaClass = new GerencialVendas.EntidadeCategoriaClass();

                ObjEntidadeCategoriaClass.EntCod = ObjEntidadesClass.EntCod;
                ObjEntidadeCategoriaClass.Codigo = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);

                ObjEntidadesClass.RemoverCategoria(ObjEntidadeCategoriaClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid_Categoria();
            }
        }

        public void Atualizar_Grid_Categoria()
        {
            if (ObjEntidadesClass.ListEntCategoriaClass != null)
            {
                //Carrega Grid na Tela
                //ContatoGridView.DataSource = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoOperacao != "Remover" && C.TipoContato != "Responsavel").ToList();
                CategoriaGridView.DataSource = ObjEntidadesClass.ListEntCategoriaClass.ToList();
                CategoriaGridView.DataBind();

                Session["clsEntidades"] = ObjEntidadesClass;
            }
        }

        protected void AdicionarCondPagLinkButton_Click(object sender, EventArgs e)
        {
            string Validacao = "";

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            if (CondicaoPagamentoDropDownList.SelectedValue == "")
            {
                Validacao = "Selecione uma Condição de pagamento";
            }

            if (Validacao == "")
            {
                int AUXCodigo = 0;

                if (ObjEntidadesClass.ListCondPag != null && ObjEntidadesClass.ListCondPag.Count > 0)
                {
                    AUXCodigo = ObjEntidadesClass.ListCondPag.OrderBy(C => C.Codigo).First().Codigo;
                }

                if (AUXCodigo < 0)
                {

                    clsCondPag.Codigo = AUXCodigo - 1;
                }
                else
                {
                    clsCondPag.Codigo = (AUXCodigo + 1) * -1;
                }

                clsCondPag.EntCod = ObjEntidadesClass.EntCod;
                clsCondPag.CondPagCod = CondicaoPagamentoDropDownList.SelectedValue;
                clsCondPag.Condicao = CondicaoPagamentoDropDownList.SelectedItem.Text;

                
                ObjEntidadesClass.AdicionarCondPag(clsCondPag);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid_Cond_Pag();
            }
            else
            {
                
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Validacao, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RemoverCondPagButton_Click(object sender, EventArgs e)
        {
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                clsCondPag = new GerencialVendas.clsCondPag();

                clsCondPag.EntCod = ObjEntidadesClass.EntCod;
                clsCondPag.Codigo = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);

                ObjEntidadesClass.RemoverCondPag(clsCondPag);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid_Cond_Pag();
            }
        }

        public void Atualizar_Grid_Cond_Pag()
        {
            if (ObjEntidadesClass.ListCondPag != null)
            {
                //Carrega Grid na Tela
                //ContatoGridView.DataSource = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoOperacao != "Remover" && C.TipoContato != "Responsavel").ToList();
                CondPagGridView.DataSource = ObjEntidadesClass.ListCondPag.ToList();
                CondPagGridView.DataBind();

                Session["clsEntidades"] = ObjEntidadesClass;
            }
        }


        protected void BloqueiaCampos()
        {
            TipoCobCodDropDownList.Enabled = false;
            EntValLimCredTextBox.Enabled = false;
            UsuCartaoCNPJDropDownList.Enabled = false;
            UsuSintegraDropDownList.Enabled = false;
            CategoriaSecundariaDropDownList.Disabled = true;
            CondicaoPagamentoDropDownList.Enabled = false;
            OutraCondPagLabel.Enabled = false;

            AdicionarCondPagLinkButton.Visible = false;
            AdicionarCategoriaButton.Visible = false;
            CategoriaGridView.Columns[4].Visible = false;
            CondPagGridView.Columns[4].Visible = false;            
        }
        
    }
}