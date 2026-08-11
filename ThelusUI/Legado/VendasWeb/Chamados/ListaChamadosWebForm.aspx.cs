using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Chamados
{
    public partial class ListaChamadosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ChamadoClass objChamado = new ChamadoClass();
        usuario Objusuario = new usuario();
        CrmGrupoUsuarioClass GruposUsuario = new CrmGrupoUsuarioClass();
        ParametroGeral objParametroGeral = new ParametroGeral();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaCombos();

                CarregaDadosGrid();
            }

            if (Session["MsgSucesso"] != null)
            {
                ApresentaMensagem(Session["MsgSucesso"].ToString(), "Sucesso");

                Session["MsgSucesso"] = null;
            }

            if (Session["MsgErro"] != null)
            {
                ApresentaMensagem(Session["MsgErro"].ToString(), "Erro");

                Session["MsgErro"] = null;
            }

            ConsultaGruposUsuarioSuporte();

            if (GruposUsuario != null)
            {
                HomologarLinkButton.Visible = true;
                HistoricoLabel.Visible = true;
                HistoricoTextBox.Visible = true;
            }
            else
            {
                HomologarLinkButton.Visible = false;
                HistoricoLabel.Visible = false;
                HistoricoTextBox.Visible = false;
            }
        }

        public void ConsultaGruposUsuarioSuporte()
        {
            //Consulta grupos do usuário logado

            if (Session["usuario"] != null)
                Objusuario.CodigoUsuario = Session["usuario"].ToString();

            //Grupo de Suporte
            GruposUsuario = Objusuario.ConsultaGrupos("Ativo", objParametroGeral.RetornaValorNumericoParametro("GRUPOCHAMADOSSUPORTE"));
        }

        protected void CarregaPreferenciasUsuario()
        {
            objChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);

            DataTable Preferencias = objChamado.CarregaListaPreferenciasUsuario();

            if (Preferencias.Rows.Count > 0)
            {
                foreach (DataRow row in Preferencias.Rows)
                {
                    SolicitanteDropDownList.SelectedValue = row["IDSolicitante"].ToString();

                    ResponsavelDropDownList.SelectedValue = row["IDResponsavel"].ToString();

                    StatusDropDownList.SelectedValue = row["IDStatus"].ToString();
                }
            }
        }

        protected void ChamadosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ChamadosGridView.PageIndex = e.NewPageIndex;
            CarregaDadosGrid();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosGrid();
        }

        public void CarregaDadosGrid()
        {
            objChamado.Chamado = ChamadoTextBox.Text;
            objChamado.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);
            objChamado.IDUsuarioSolicitante = Convert.ToInt32(SolicitanteDropDownList.SelectedValue);
            objChamado.IDUsuarioResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue);
            objChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);
            objChamado.IDSetor = SetorDropDownList.SelectedValue == "" ? 0 : Convert.ToInt32(SetorDropDownList.SelectedValue);

            //Se data não for preenchida pega a primeira data do sistema
            if (DataInicialTextBox.Text == "")
            {
                objChamado.DataInicial = Convert.ToDateTime("01-01-1900");
            }
            else
            {
                objChamado.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text);
            }

            //Se data final não for preenchida pega data atual
            if (DataFinalTextBox.Text == "")
            {
                objChamado.DataFinal = DateTime.Now;
            }
            else
            {
                objChamado.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text);
            }

            ChamadosGridView.DataSource = objChamado.CarregaListaChamados();
            ChamadosGridView.DataBind();
            ChamadosMultiView.Visible = true;
        }

        public void ConsultaGruposUsuario()
        {
            //Consulta grupos do usuário logado

            if (Session["usuario"] != null)
                Objusuario.CodigoUsuario = Session["usuario"].ToString();

            //Grupo de Suporte
            GruposUsuario = Objusuario.ConsultaGrupos("Ativo", objParametroGeral.RetornaValorNumericoParametro("GRUPOCHAMADOSSUPORTE"));
        }

        public void CarregaCombos()
        {
            SolicitanteDropDownList.DataSource = objChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();
            SolicitanteDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            ResponsavelDropDownList.DataSource = objChamado.CarregaUsuariosSuporte();
            ResponsavelDropDownList.DataValueField = "IDUsuario";
            ResponsavelDropDownList.DataTextField = "CodigoUsuario";
            ResponsavelDropDownList.DataBind();
            ResponsavelDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            StatusDropDownList.DataSource = objChamado.CarregaStatus();
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataBind();
            StatusDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            ConsultaGruposUsuario();

            if (GruposUsuario != null)
                ResponsavelDropDownList.SelectedValue = Session["IDUsuario"].ToString();
            else
                SolicitanteDropDownList.SelectedValue = Session["IDUsuario"].ToString();

            CarregaPreferenciasUsuario();

            SolicitanteDropDownList_SelectedIndexChanged(null, null);
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            objChamado.NumeroChamado = Convert.ToInt32(((Label)((Control)sender).FindControl("IDChamadoLabel")).Text ?? "0");

            Session["objChamado"] = objChamado;

            Response.Redirect("~/Chamados/ChamadoPrincipalWebForm.aspx?indmnu=5");
        }

        protected void SolicitanteDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            usuario Objusuario = new usuario();

            Objusuario.IDUsuario = Convert.ToInt32(SolicitanteDropDownList.SelectedValue);

            SetorDropDownList.DataSource = Objusuario.ConsultaSetoresUsuario();
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataBind();

            if (SolicitanteDropDownList.SelectedValue == "0")
                SetorDropDownList.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void PreferenciasLinkButton_Click(object sender, EventArgs e)
        {
            objChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);
            objChamado.IDUsuarioSolicitante = Convert.ToInt32(SolicitanteDropDownList.SelectedValue);
            objChamado.IDUsuarioResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue);
            objChamado.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);

            ApresentaMensagem(objChamado.GravaListaPreferenciasUsuario());
        }

        public void ApresentaMensagem(string mensagem = "", string tipo = "Sucesso")
        {
            if (tipo == "Sucesso" && mensagem == "")
                mensagem = "Operação realizada com sucesso.";

            if (tipo == "Sucesso")
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(mensagem, true);
            else
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(mensagem, true);

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        protected void HomologarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            //id no html PageContent_PageContent_ChamadoGridView_HistoricoTextBox_0 para pegar o texto
            string MensagemHomologacao = ((TextBox)((Control)sender).NamingContainer.FindControl("HistoricoTextBox")).Text;

            if (string.IsNullOrEmpty(MensagemHomologacao)) erro = "É obrigatório informar uma mensagem de homologação.";

            if (erro == "")
            {
                int IDChamado = Convert.ToInt32(NumeroChamadoModalTextBox.Text);

                ChamadoClass objChamadoAntigo = new ChamadoClass
                {
                    NumeroChamado = IDChamado
                };

                {
                    objChamadoAntigo.RecuperaDadosPrincipais();

                    objChamadoAntigo.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);
                }

                ChamadoClass objChamadosClass = new ChamadoClass
                {
                    NumeroChamado = IDChamado
                };

                {
                    objChamadosClass.RecuperaDadosPrincipais();

                    objChamadosClass.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);

                    objChamadosClass.IDStatus = objChamadoAntigo.RetornaChamadosIDStatusHomologando();
                }

                string InformacoesAlteracaoUsuario = ComparaInformacoesAlteracaoUsuario();

                string InformacoesAlteracaoInformacoes = ComparaInformacoesAlteracaoInformacoes(objChamadoAntigo, objChamadosClass);

                objChamadosClass.Historico = MensagemHomologacao;

                if (InformacoesAlteracaoInformacoes != "" || objChamado.NumeroChamado == 0)
                    erro = objChamadosClass.GravaDadosPrincipaisChamado(InformacoesAlteracaoUsuario, InformacoesAlteracaoInformacoes);
            }

            if (erro != "")
                Session["MsgErro"] = erro;
            else
                Session["MsgSucesso"] = "Homologação realizada com sucesso.";

            Response.Redirect("~/Chamados/ListaChamadosWebForm.aspx?indmnu=5");
        }

        protected string ComparaInformacoesAlteracaoUsuario()
        {
            StringBuilder informacoes = new StringBuilder("");

            informacoes.AppendLine("Alterado por " + Session["usuario"].ToString());

            informacoes.AppendLine(" <br> <br> ");

            return informacoes.ToString();
        }

        protected string ComparaInformacoesAlteracaoInformacoes(ChamadoClass objChamadoAntigo, ChamadoClass objChamadoNovo)
        {
            StringBuilder informacoes = new StringBuilder("");

            #region Solicitante

            if (objChamadoAntigo.IDUsuarioSolicitante != objChamadoNovo.IDUsuarioSolicitante)
            {
                informacoes.AppendLine("Solicitante: ");

                DataTable Solicitantes = objChamado.CarregaUsuarios();

                foreach (DataRow row in Solicitantes.Rows)
                {
                    if (objChamadoAntigo.IDUsuarioSolicitante.ToString() == row["IDUsuario"].ToString())
                    {
                        informacoes.Append(row["Nome"].ToString());

                        break;
                    }
                }

                informacoes.Append(" --> ");

                foreach (DataRow row in Solicitantes.Rows)
                {
                    if (objChamadoNovo.IDUsuarioSolicitante.ToString() == row["IDUsuario"].ToString())
                    {
                        informacoes.AppendLine(row["Nome"].ToString());

                        break;
                    }
                }
            }

            #endregion

            #region Responsável

            if (objChamadoAntigo.IDUsuarioResponsavel != objChamadoNovo.IDUsuarioResponsavel)
            {
                if (informacoes.ToString() != "") informacoes.AppendLine(" <br> <br> ");

                informacoes.AppendLine("Responsável principal: ");

                DataTable Responsaveis = objChamado.CarregaUsuarios();

                foreach (DataRow row in Responsaveis.Rows)
                {
                    if (objChamadoAntigo.IDUsuarioResponsavel.ToString() == row["IDUsuario"].ToString())
                    {
                        informacoes.Append(row["Nome"].ToString());

                        break;
                    }
                }

                informacoes.Append(" --> ");

                foreach (DataRow row in Responsaveis.Rows)
                {
                    if (objChamadoNovo.IDUsuarioResponsavel.ToString() == row["IDUsuario"].ToString())
                    {
                        informacoes.AppendLine(row["Nome"].ToString());

                        break;
                    }
                }
            }

            #endregion

            #region Classificacao

            if (objChamadoAntigo.IDClassificacao != objChamadoNovo.IDClassificacao)
            {
                if (informacoes.ToString() != "") informacoes.AppendLine(" <br> <br> ");

                informacoes.AppendLine("Classificação: ");

                DataTable Classificacoes = objChamado.CarregaClassificacoes();

                foreach (DataRow row in Classificacoes.Rows)
                {
                    if (objChamadoAntigo.IDClassificacao.ToString() == row["IDClassificacao"].ToString())
                    {
                        informacoes.Append(row["Descricao"].ToString());

                        break;
                    }
                }

                informacoes.Append(" --> ");

                foreach (DataRow row in Classificacoes.Rows)
                {
                    if (objChamadoNovo.IDClassificacao.ToString() == row["IDClassificacao"].ToString())
                    {
                        informacoes.AppendLine(row["Descricao"].ToString());

                        break;
                    }
                }
            }

            #endregion                        

            #region Status

            if (objChamadoAntigo.IDStatus != objChamadoNovo.IDStatus)
            {
                if (informacoes.ToString() != "") informacoes.AppendLine(" <br> <br> ");

                informacoes.AppendLine("Status: ");

                DataTable Status = objChamado.CarregaStatus();

                foreach (DataRow row in Status.Rows)
                {
                    if (objChamadoAntigo.IDStatus.ToString() == row["IDStatus"].ToString())
                    {
                        informacoes.Append(row["Descricao"].ToString());

                        break;
                    }
                }

                informacoes.Append(" --> ");

                foreach (DataRow row in Status.Rows)
                {
                    if (objChamadoNovo.IDStatus.ToString() == row["IDStatus"].ToString())
                    {
                        informacoes.AppendLine(row["Descricao"].ToString());

                        break;
                    }
                }
            }

            #endregion                        

            #region Sistema

            if (objChamadoAntigo.IDSistema != objChamadoNovo.IDSistema)
            {
                if (informacoes.ToString() != "") informacoes.AppendLine(" <br> <br> ");

                informacoes.AppendLine("Sistema: ");

                DataTable Sistemas = objChamado.CarregaSistemas();

                foreach (DataRow row in Sistemas.Rows)
                {
                    if (objChamadoAntigo.IDSistema.ToString() == row["IDSistema"].ToString())
                    {
                        informacoes.Append(row["Descricao"].ToString());

                        break;
                    }
                }

                informacoes.Append(" --> ");

                foreach (DataRow row in Sistemas.Rows)
                {
                    if (objChamadoNovo.IDSistema.ToString() == row["IDSistema"].ToString())
                    {
                        informacoes.AppendLine(row["Descricao"].ToString());

                        break;
                    }
                }
            }

            #endregion

            #region Prioridade

            if (objChamadoAntigo.IDPrioridade != objChamadoNovo.IDPrioridade)
            {
                if (informacoes.ToString() != "") informacoes.AppendLine(" <br> <br> ");

                informacoes.AppendLine("Prioridade: ");

                DataTable Prioridades = objChamado.CarregaPrioridades();

                foreach (DataRow row in Prioridades.Rows)
                {
                    if (objChamadoAntigo.IDPrioridade.ToString() == row["IDPrioridade"].ToString())
                    {
                        informacoes.Append(row["Descricao"].ToString());

                        break;
                    }
                }

                informacoes.Append(" --> ");

                foreach (DataRow row in Prioridades.Rows)
                {
                    if (objChamadoNovo.IDPrioridade.ToString() == row["IDPrioridade"].ToString())
                    {
                        informacoes.AppendLine(row["Descricao"].ToString());

                        break;
                    }
                }
            }

            #endregion

            #region Setor

            if (objChamadoAntigo.IDSetor != objChamadoNovo.IDSetor && objChamadoAntigo.IDSetor != 0 && objChamadoNovo.IDSetor != 0)
            {
                if (informacoes.ToString() != "") informacoes.AppendLine(" <br> <br> ");

                informacoes.AppendLine("Setor: ");

                Objusuario.IDUsuario = Convert.ToInt32(SolicitanteDropDownList.SelectedValue);

                DataTable Setores = Objusuario.ConsultaSetoresUsuario();

                foreach (DataRow row in Setores.Rows)
                {
                    if (objChamadoAntigo.IDSetor.ToString() == row["IDSetor"].ToString())
                    {
                        informacoes.Append(row["Descricao"].ToString());

                        break;
                    }
                }

                informacoes.Append(" --> ");

                foreach (DataRow row in Setores.Rows)
                {
                    if (objChamadoNovo.IDSetor.ToString() == row["IDSetor"].ToString())
                    {
                        informacoes.AppendLine(row["Descricao"].ToString());

                        break;
                    }
                }
            }

            #endregion            

            return informacoes.ToString();
        }
    }
}