using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Chamados
{
    public partial class ChamadoAnexosWebForm : System.Web.UI.Page
    {
        ChamadoClass objChamado = new ChamadoClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();
        usuario Objusuario = new usuario();
        CrmGrupoUsuarioClass GruposUsuario = new CrmGrupoUsuarioClass();
        ParametroGeral objParametroGeral = new ParametroGeral();

        protected void Page_Load(object sender, EventArgs e)
        {
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ApresentaMensagem(Session["Msg"].ToString());
                Session.Remove("Msg");
            }

            if (Session["objChamado"] != null)
                objChamado = (ChamadoClass)Session["objChamado"];


            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaCombos();

                CarregaDadosNaTela();

                TrataBotoesDisponiveis();
            }

        }

        public void CarregaDadosNaTela()
        {
            //recupera dados principais da tela
            objChamado.RecuperaDadosPrincipais();

            SolicitanteDropDownList.SelectedValue = objChamado.IDUsuarioSolicitante.ToString();
            NumeroChamadoTextBox.Text = objChamado.NumeroChamado.ToString();

            //Carga Inicial
            CarregaDadosGrid();
        }

        public void CarregaCombos()
        {
            SolicitanteDropDownList.DataSource = objChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();
        }

        public void CarregaDadosGrid()
        {
            AnexosGridView.DataSource = objChamado.RecuperaDadosAnexos();
            AnexosGridView.DataBind();
            AnexosMultiView.Visible = true;
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = objChamado.GravaArquivoServidor(ArquivoFileUpload);

            objChamado.DescricaoArquivo = AssuntoBreveTextBox.Text;

            objChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);

            if (erro == "" && objChamado.DescricaoArquivo == "") erro = "Informe o assunto do anexo.";

            if (erro == "") erro = objChamado.GravaDadosAnexosChamado();

            if (erro == "")
            {
                AssuntoBreveTextBox.Text = "";

                ArquivoFileUpload.Dispose();

                CarregaDadosGrid();
            }

            ApresentaMensagem(erro);
        }

        protected void AnexosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AnexosGridView.PageIndex = e.NewPageIndex;
            CarregaDadosGrid();
        }

        protected void BaixarLinkButton_Click(object sender, EventArgs e)
        {
            string Caminho = Convert.ToString(((Label)((Control)sender).FindControl("CaminhoDestinoLabel")).Text);
            string NomeArquivo = Convert.ToString(((Label)((Control)sender).FindControl("NomeArquivoLabel")).Text);

            byte[] bytesInStream = System.IO.File.ReadAllBytes(Caminho);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename=" + NomeArquivo + "");
            Response.BinaryWrite(bytesInStream);
            Response.End();
        }

        protected void ExcluirAnexoLinkButton_Click(object sender, EventArgs e)
        {
            bool admSuporte = false;
            string erro = "";

            //Verifica se o usuário logado é administrador do grupo de suporte
            {
                ConsultaGrupoSuporteUsuario();

                if (GruposUsuario != null) admSuporte = Convert.ToBoolean(GruposUsuario.Administrador);
            }

            if ((SolicitanteDropDownList.SelectedValue == Session["IDUsuario"].ToString()) || (admSuporte))
            {
                objChamado.IDAnexo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDAnexoCRM")).Text);

                objChamado.NomeArquivo = ((Label)((Control)sender).FindControl("NomeArquivoLabel")).Text;

                objChamado.DescricaoArquivo = ((Label)((Control)sender).FindControl("DescricaoLabel")).Text;

                erro = objChamado.ExcluiDadosAnexosChamadoServidor();

                if (erro == "") erro = objChamado.ExcluiDadosAnexosChamado();
            }
            else
            {
                erro = "Apenas administradores do grupo de suporte podem excluir anexos deste chamado.";
            }

            if (erro == "") CarregaDadosGrid();
            else ApresentaMensagem(erro);
        }

        public void ApresentaMensagem(string erro = "")
        {
            if (erro == "")
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Operação realizada com sucesso", true);
            else
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoPrincipalWebForm.aspx?indmnu=5");
        }

        public void ConsultaGrupoSuporteUsuario()
        {
            //Consulta grupos do usuário logado

            if (Session["usuario"] != null)
                Objusuario.CodigoUsuario = Session["usuario"].ToString();

            //Grupo de Suporte
            GruposUsuario = Objusuario.ConsultaGrupos("Ativo", objParametroGeral.RetornaValorNumericoParametro("GRUPOCHAMADOSSUPORTE"));
        }

        public void TrataBotoesDisponiveis()
        {
            string descricaoStatus = "";

            //Pega a descricao do status
            {
                DataTable Status = objChamado.CarregaStatus();

                foreach (DataRow row in Status.Rows)
                {
                    if (objChamado.IDStatus.ToString() == row["IDStatus"].ToString())
                    {
                        descricaoStatus = row["Descricao"].ToString();

                        break;
                    }
                }
            }

            bool suporte = false;

            //Verifica se o usuário logado esta no grupo de suporte
            {
                ConsultaGrupoSuporteUsuario();

                suporte = (GruposUsuario != null);
            }

            if (descricaoStatus == "Finalizado" && !suporte) GravarButton.Enabled = false;
        }
    }
}