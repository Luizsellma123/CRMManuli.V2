using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidade
{
    public partial class frmAbaEntRelacionamento : System.Web.UI.Page
    {
        clsEntidades ObjEntidadesClass = new clsEntidades();
        EntRelacionamentoClass ObjEntRelacionamentoClass = new EntRelacionamentoClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

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

            GerencialVendas.EntRelacionamentoClass ObjEntRelacionamentoClass = new GerencialVendas.EntRelacionamentoClass();

            ObjEntRelacionamentoClass.Descricao = txtDescricao.Text;
            ObjEntRelacionamentoClass.Data = txtData.Text;

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

            Response.Redirect("FrmAbaPerfilDeConsumo.aspx?indmnu=2");
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAbaContatos.aspx?indmnu=2");
        }

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

                if (ObjEntidadesClass.ListEntRelacionamentoclass != null)
                {
                    if (ObjEntidadesClass.ListEntRelacionamentoclass.Count > 0)
                        AUXCodigo = ObjEntidadesClass.ListEntRelacionamentoclass.OrderBy(C => C.Codigo).First().Codigo;
                }

                if (AUXCodigo < 0)
                {

                    ObjEntRelacionamentoClass.Codigo = AUXCodigo - 1;
                }
                else
                {
                    ObjEntRelacionamentoClass.Codigo = (AUXCodigo + 1) * -1;
                }

                ObjEntRelacionamentoClass.EntCod = ObjEntidadesClass.EntCod;
                ObjEntRelacionamentoClass.Descricao = txtDescricao.Text;
                ObjEntRelacionamentoClass.Data = txtData.Text;

                ObjEntidadesClass.AdicionarRelacionamento(ObjEntRelacionamentoClass);

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

                ObjEntRelacionamentoClass = new GerencialVendas.EntRelacionamentoClass();

                ObjEntRelacionamentoClass.EntCod = ObjEntidadesClass.EntCod;
                ObjEntRelacionamentoClass.Codigo = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);

                ObjEntidadesClass.RemoverRelacionamento(ObjEntRelacionamentoClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();
            }
        }

        public void Atualizar_Grid()
        {
            if (ObjEntidadesClass.ListEntRelacionamentoclass != null)
            {
                //Carrega Grid na Tela
                /*if (ObjEntidadesClass.ListEntRelacionamentoclass.Count > 0)
                {*/
                RelacionamentoGridView.DataSource = ObjEntidadesClass.ListEntRelacionamentoclass.ToList();
                RelacionamentoGridView.DataBind();
                //}
                Session["clsEntidades"] = ObjEntidadesClass;
            }
        }

        protected void NovoRelacionamento_Click(object sender, EventArgs e)
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

            txtDescricao.Text = "";
            txtData.Text = "";

        }





        protected void BloqueiaCampos()
        {
            NovoButton.Visible = false;
            RelacionamentoGridView.Columns[4].Visible = false;
        }


    }
}