using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidade
{
    public partial class frmAbaPerfilDeConsumo : System.Web.UI.Page
    {
        clsEntidades ObjEntidadesClass = new clsEntidades();
        EntPerfilDeConsumoClass ObjEntPerfilDeConsumoClass = new EntPerfilDeConsumoClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

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

                if (Session["clsEntidades"] != null)
                {
                    //Descarrega session
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
                    Atualizar_Grid();

                    if (ObjEntidadesClass.TipoOperacao == "Consultar")
                    {
                        BloqueiaCampos();
                    }


                }
            }
        }

        public string CarregaDadosDaTela()
        {

            #region

            //classes.EntPerfilDeConsumoClass ObjEntPerfilDeConsumoClass = new classes.EntPerfilDeConsumoClass();

            ObjEntidadesClass.LinhaConsumoCliente = LinhaDropDownList.SelectedValue;
            ObjEntidadesClass.QuantidadeConsumoCliente = Convert.ToDouble(QuantidadeTextBox.Text);
            ObjEntidadesClass.DescricaoConsumoCliente = DescricaoTextBox.Text;

            #endregion

            return "";
        }

        protected void ProximoPassoButton_Click(object sender, EventArgs e)
        {
            //Descarega a sessao
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            //Carrega os Dados da Tela
            //CarregaDadosDaTela();

            //Guarda os dados em Session
            Session["clsEntidades"] = ObjEntidadesClass;

            Response.Redirect("FrmAbaFinanceiro.aspx?indmnu=2");
        }



        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAbaEntRelacionamento.aspx?indmnu=2");
        }

        //---------------------------------------------------------------------Mario

        protected void AdicionarButton_Click(object sender, EventArgs e)
        {
            string Validacao = "";

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            if (Validacao == "")
            {
                int AUXCodigo = 0;

                if (ObjEntidadesClass.ListEntPerfilDeConsumoClass != null)
                {
                    if (ObjEntidadesClass.ListEntPerfilDeConsumoClass.Count > 0)
                        AUXCodigo = ObjEntidadesClass.ListEntPerfilDeConsumoClass.OrderBy(C => C.Codigo).First().Codigo;
                }

                if (AUXCodigo < 0)
                {

                    ObjEntPerfilDeConsumoClass.Codigo = AUXCodigo - 1;
                }
                else
                {
                    ObjEntPerfilDeConsumoClass.Codigo = (AUXCodigo + 1) * -1;
                }

                ObjEntPerfilDeConsumoClass.EntCod = ObjEntidadesClass.EntCod;
                ObjEntPerfilDeConsumoClass.Linha = LinhaDropDownList.SelectedValue;
                ObjEntPerfilDeConsumoClass.Quantidade = Convert.ToDouble(QuantidadeTextBox.Text);
                ObjEntPerfilDeConsumoClass.Descricao = DescricaoTextBox.Text;

                ObjEntidadesClass.AdicionarPerfil(ObjEntPerfilDeConsumoClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();

                CancelarButton_Click(null, null);
            }
            else
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Validacao, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


            }
        }

        protected void RemoverButton_Click(object sender, EventArgs e)
        {
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                ObjEntPerfilDeConsumoClass = new GerencialVendas.EntPerfilDeConsumoClass();

                ObjEntPerfilDeConsumoClass.EntCod = ObjEntidadesClass.EntCod;
                ObjEntPerfilDeConsumoClass.Codigo = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);

                ObjEntidadesClass.RemoverPerfil(ObjEntPerfilDeConsumoClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();
            }
        }

        public void Atualizar_Grid()
        {
            if (ObjEntidadesClass.ListEntPerfilDeConsumoClass != null)
            {
                //Carrega Grid na Tela
                //ContatoGridView.DataSource = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoOperacao != "Remover" && C.TipoContato != "Responsavel").ToList();
                /*if (ObjEntidadesClass.ListEntPerfilDeConsumoClass.Count > 0)
                {*/
                    PerfilGridView.DataSource = ObjEntidadesClass.ListEntPerfilDeConsumoClass.ToList();
                    PerfilGridView.DataBind();
                //}

                Session["clsEntidades"] = ObjEntidadesClass;
            }
        }

        protected void NovoPerfil_Click(object sender, EventArgs e)
        {
            FormularioMultView.Visible = true;
            NovoButton.Visible = false;
            AdicionarButton.Visible = true;
            CancelarButton.Visible = true;
        }

        protected void CancelarButton_Click(object sender, EventArgs e)
        {
            FormularioMultView.Visible = false;
            NovoButton.Visible = true;
            AdicionarButton.Visible = false;
            CancelarButton.Visible = false;

            DescricaoTextBox.Text = "";
            QuantidadeTextBox.Text = "";
            LinhaDropDownList.SelectedValue = "";

        }


        protected void BloqueiaCampos()
        {
            NovoButton.Visible = false;
            PerfilGridView.Columns[5].Visible = false;
        }
      

       
    }
}