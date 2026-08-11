using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidade
{
    public partial class frmAbaConcorrencia : System.Web.UI.Page
    {
        clsEntidades ObjEntidadesClass = new clsEntidades();
        EntConcorrenciaClass ObjEntConcorrenciaClass = new EntConcorrenciaClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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



                if (Session["clsEntidades"] != null)
                {
                    //Descarrega session
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

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

            //classes.EntConcorrenciaClass ObjEntConcorrenciaClass = new classes.EntConcorrenciaClass();

            ObjEntidadesClass.NomeConcorrente = NomeConcorrenteTextBox.Text;
            ObjEntidadesClass.ObservacaoConcorrente = ObservacaoConcorrenteTextBox.Text;

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

            Response.Redirect("FrmAbaAnexo.aspx?indmnu=2");
        }


        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaFiscal.aspx?indmnu=2");
        }


        protected void NovaOcorrencia_Click(object sender, EventArgs e)
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

            ObservacaoConcorrenteTextBox.Text = "";
            NomeConcorrenteTextBox.Text = "";


        }




        protected void AdicionarButton_Click(object sender, EventArgs e)
        {
            string Validacao = "";

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            if (NomeConcorrenteTextBox.Text == "")
            {
                Validacao = "Informe o nome do concorrente";
            }

            if (Validacao == "")
            {
                int AUXCodigo = 0;

                if (ObjEntidadesClass.ListEntConcorrenciaClass != null)
                {
                    if (ObjEntidadesClass.ListEntConcorrenciaClass.Count > 0)
                        AUXCodigo = ObjEntidadesClass.ListEntConcorrenciaClass.OrderBy(C => C.Codigo).First().Codigo;
                }

                if (AUXCodigo < 0)
                {

                    ObjEntConcorrenciaClass.Codigo = AUXCodigo - 1;
                }
                else
                {
                    ObjEntConcorrenciaClass.Codigo = (AUXCodigo + 1) * -1;
                }

                ObjEntConcorrenciaClass.EntCod = ObjEntidadesClass.EntCod;
                ObjEntConcorrenciaClass.NomeConcorrente = NomeConcorrenteTextBox.Text;
                ObjEntConcorrenciaClass.ObservacaoConcorrente = ObservacaoConcorrenteTextBox.Text;


                ObjEntidadesClass.AdicionarConcorrencia(ObjEntConcorrenciaClass);

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

                ObjEntConcorrenciaClass = new GerencialVendas.EntConcorrenciaClass();

                ObjEntConcorrenciaClass.EntCod = ObjEntidadesClass.EntCod;
                ObjEntConcorrenciaClass.Codigo = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);

                ObjEntidadesClass.RemoverConcorrencia(ObjEntConcorrenciaClass);

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();


            }
        }



        public void Atualizar_Grid()
        {
            if (ObjEntidadesClass.ListEntConcorrenciaClass != null)
            {


                /*if (ObjEntidadesClass.ListEntRelacionamentoclass.Count > 0)
                {*/
                //Carrega Grid na Tela
                //ContatoGridView.DataSource = ObjEntidadesClass.ListContatoClass.Where(C => C.TipoOperacao != "Remover" && C.TipoContato != "Responsavel").ToList();
                ConcorrenciaGridView.DataSource = ObjEntidadesClass.ListEntConcorrenciaClass.ToList();
                ConcorrenciaGridView.DataBind();

                //}

                Session["clsEntidades"] = ObjEntidadesClass;
            }
        }



        protected void BloqueiaCampos()
        {
            NovoButton.Visible = false;
            ConcorrenciaGridView.Columns[4].Visible = false;
        }


    }
}