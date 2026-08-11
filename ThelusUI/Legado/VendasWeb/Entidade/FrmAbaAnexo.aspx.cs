using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidade
{
    public partial class FrmAbaAnexo : System.Web.UI.Page
    {
        funcoes mdlFuncoes = new funcoes();
        clsEntidades ObjEntidadesClass = new clsEntidades();
        DocEntidadeClass DocEntidadeClass = new DocEntidadeClass();
        usuario ObjUsuarioClass = new usuario();
        criptografia mdlCriptografia = new criptografia();
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

                //Combo Tipo de Anexo
                USER_TB_Tipos_AnexosDropDownList.DataSource = DocEntidadeClass.Consulta_Tipos_Anexos();
                USER_TB_Tipos_AnexosDropDownList.DataTextField = "Nome";
                USER_TB_Tipos_AnexosDropDownList.DataValueField = "USER_TB_Tipos_AnexosID";
                USER_TB_Tipos_AnexosDropDownList.DataBind();
                USER_TB_Tipos_AnexosDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

                if (Session["clsEntidades"] != null)
                {
                    //Descarega a session da Entidade
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                    Atualizar_Grid();

                    if (ObjEntidadesClass.TipoOperacao == "Consultar")
                    {
                        BloqueiaCampos();
                    }
                }
            }
        }

        protected void IncluirDocButton_Click(object sender, EventArgs e)
        {
            string Validacao = "";

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            if (USER_TB_Tipos_AnexosDropDownList.SelectedItem.Text == "")
            {
                Validacao = "Informe um tipo de documento";
            }

            if (Validacao == "")
            {
                int AUXCodigo = 0;

                if (ObjEntidadesClass.ListDocEntidadeClass != null && ObjEntidadesClass.ListDocEntidadeClass.Count > 0)
                {
                    AUXCodigo = ObjEntidadesClass.ListDocEntidadeClass.OrderBy(C => C.DocEntSeq).First().DocEntSeq;
                }

                if (AUXCodigo < 0)
                {
                    DocEntidadeClass.DocEntSeq = AUXCodigo - 1;
                }
                else
                {
                    DocEntidadeClass.DocEntSeq = (AUXCodigo + 1);
                }

                DocEntidadeClass.ArquivoFileUpload = IncluirDocFileUpload;
                //DocEntidadeClass.DocEntPathArq = "\\\\192.168.0.2\\anexosCRM\\" + ObjEntidadesClass.EntCod + USER_TB_Tipos_AnexosDropDownList.SelectedItem.Text + ".PDF";
                DocEntidadeClass.DocEntPathArq = "\\\\192.168.0.2\\anexosCRM\\" + DateTime.Now.ToString("yyyyMMddHHmmssFFF")
                                                                                + "_"
                                                                                + Guid.NewGuid().ToString()
                                                                                + "_"
                                                                                + USER_TB_Tipos_AnexosDropDownList.SelectedItem.Text
                                                                                + ObjEntidadesClass.EntCod
                                                                                + ".PDF";
                DocEntidadeClass.UsuCod = Session["usuario"].ToString();
                DocEntidadeClass.USER_TB_Tipos_AnexosID = Convert.ToInt32(USER_TB_Tipos_AnexosDropDownList.SelectedValue);
                DocEntidadeClass.DocEntObs = USER_TB_Tipos_AnexosDropDownList.SelectedItem.Text;
                DocEntidadeClass.NomeTipoAnexo = USER_TB_Tipos_AnexosDropDownList.SelectedItem.Text;

                if (DocEntidadeClass.Salvar_Arquivo() == true)
                {
                    IncluirDocLabel.Text = DocEntidadeClass.ArquivoMsg;
                    IncluirDocLabel.ForeColor = System.Drawing.Color.Green;
                    IncluirDocLabel.Visible = true;

                    ObjEntidadesClass.AdicionarAnexo(DocEntidadeClass);
                }
                else
                {
                    IncluirDocLabel.Text = DocEntidadeClass.ArquivoMsg;
                    IncluirDocLabel.ForeColor = System.Drawing.Color.Red;
                    IncluirDocLabel.Visible = true;
                }

                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();
            }
            else
            {


                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Validacao, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }
        }

        public void Atualizar_Grid()
        {
            if (ObjEntidadesClass.ListDocEntidadeClass != null)
            {
                //Carrega Grid na Tela
                if (ObjEntidadesClass.ListDocEntidadeClass.Count > 0)
                {
                    DocumentosGridView.DataSource = ObjEntidadesClass.ListDocEntidadeClass.ToList();
                    DocumentosGridView.DataBind();
                }

                Session["clsEntidades"] = ObjEntidadesClass;
            }
        }




        protected void RemoverDocumentoButton_Click(object sender, EventArgs e)
        {
            DocEntidadeClass = new GerencialVendas.DocEntidadeClass();

            //Descarega a session da Entidade
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            DocEntidadeClass.DocEntSeq = Convert.ToInt32(((Label)((Control)sender).FindControl("DocEntSeqLabel")).Text);
            DocEntidadeClass.EntCod = ObjEntidadesClass.EntCod;
            ObjEntidadesClass.RemoverAnexo(DocEntidadeClass);

            //Apagnado arquivo do Diretorio
            string CaminhoLocal = ((Label)((Control)sender).FindControl("DocEntPathArqLabel")).Text;
            FileInfo fi = new System.IO.FileInfo(CaminhoLocal);
            try
            {
                fi.Delete();
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            ObjEntidadesClass.ListDocEntidadeClass[0].Remover_DocEntidade();

            //Recarrega o Grid
            Atualizar_Grid();
        }

        protected void SelecionarButton_Click(object sender, EventArgs e)
        {


            ObjEntidadesClass.DocEntPathArq = ((Label)((Control)sender).FindControl("DocEntPathArqLabel")).Text;
            ObjEntidadesClass.DocEntObs = ((Label)((Control)sender).FindControl("DocEntObsLabel")).Text;
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();


            string stipoArquivo = Path.GetExtension(ObjEntidadesClass.DocEntPathArq).ToLower();

            System.IO.FileStream fs = new System.IO.FileStream(ObjEntidadesClass.DocEntPathArq, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] ar = new byte[(int)fs.Length];
            fs.Read(ar, 0, (int)fs.Length);
            fs.Close();
            Response.AddHeader("content-disposition", "attachment;filename=" + ObjEntidadesClass.DocEntObs + stipoArquivo);
            Response.ContentType = "application/octectstream";
            Response.BinaryWrite(ar);
            Response.End();
        }

        public void carregaDadosNaTela()
        {
            //Consulta os Documentos
            ObjEntidadesClass.Consulta_Documentos_Entidade();
            DocumentosGridView.DataSource = ObjEntidadesClass.ListDocEntidadeClass;
            DocumentosGridView.DataBind();
            DocumentosGridView.Visible = true;
        }

        protected void ProximoPassoButton_Click(object sender, EventArgs e)
        {
            //Descarega a sessao
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            //Carrega os Dados da Tela
            //CarregaDadosDaTela();

            //Guarda os dados em Session
            /*Session["clsEntidades"] = ObjEntidadesClass;*/

            Response.Redirect("FrmFinalizaCadastroEntidade.aspx?indmnu=2");
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaConcorrencia.aspx?indmnu=2");

        }


        protected void BloqueiaCampos()
        {
            DadosAnexoMultView.Visible = false;
            DocumentosGridView.Columns[6].Visible = false;

            //ProximoPassoButton.Visible = false;
        }


    }
}