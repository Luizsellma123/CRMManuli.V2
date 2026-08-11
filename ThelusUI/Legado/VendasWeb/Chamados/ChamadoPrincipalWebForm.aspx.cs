using System;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Chamados
{
    public partial class ChamadoPrincipalWebForm : System.Web.UI.Page
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

            if (Session["OBJChamado"] != null)
            {
                objChamado = (ChamadoClass)Session["OBJChamado"];

                SalvaDadosAlteracao();
            }

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaCombos();

                CarregaDadosNaTela();

                TrataAcessos();
            }
        }

        public void SalvaDadosAlteracao()
        {
            objChamado.RecuperaDadosPrincipais();

            ChamadoClass objChamadoAux = new ChamadoClass();

            objChamadoAux.IDUsuarioSolicitante = objChamado.IDUsuarioSolicitante;

            objChamadoAux.IDUsuarioResponsavel = objChamado.IDUsuarioResponsavel;

            objChamadoAux.IDClassificacao = objChamado.IDClassificacao;

            objChamadoAux.IDStatus = objChamado.IDStatus;

            objChamadoAux.IDSistema = objChamado.IDSistema;

            objChamadoAux.IDPrioridade = objChamado.IDPrioridade;

            objChamadoAux.IDSetor = objChamado.IDSetor;

            Session["objChamadoAntigo"] = objChamadoAux;
        }

        public void ConsultaGruposUsuarioSuporte()
        {
            //Consulta grupos do usuário logado

            if (Session["usuario"] != null)
                Objusuario.CodigoUsuario = Session["usuario"].ToString();

            //Grupo de Suporte
            GruposUsuario = Objusuario.ConsultaGrupos("Ativo", objParametroGeral.RetornaValorNumericoParametro("GRUPOCHAMADOSSUPORTE"));
        }

        public bool ConsultaSetoresUsuarioAdm(string IDSetor)
        {
            //Consulta se o usuário logado é administrador do setor do chamado

            if (Session["IDUsuario"] != null)
                Objusuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());

            //Grupo de Suporte
            DataTable SetoresUsuario = Objusuario.ConsultaSetoresUsuario();

            if (SetoresUsuario.Rows.Count > 0)
            {
                foreach (DataRow row in SetoresUsuario.Rows)
                {
                    if (row["IDSetor"].ToString() == IDSetor)
                        return Convert.ToBoolean(row["Administrador"]);
                }
            }

            return false;
        }

        public int ConsultaAdmSetor(int IDSetor)
        {
            setor objSetor = new setor();

            objSetor.IDSetor = IDSetor;

            //Grupo de Suporte
            DataTable Setores = objSetor.RetornaUsuariosSetor();

            if (Setores.Rows.Count > 0)
            {
                foreach (DataRow row in Setores.Rows)
                {
                    if (Convert.ToBoolean(row["Administrador"]))
                        return Convert.ToInt32(row["IDUsuario"]);
                }
            }

            return 0;
        }

        public void CarregaCombos()
        {
            SolicitanteModalDropDownList.DataSource = objChamado.CarregaUsuarios();
            SolicitanteModalDropDownList.DataValueField = "IDUsuario";
            SolicitanteModalDropDownList.DataTextField = "Nome";
            SolicitanteModalDropDownList.DataBind();

            SolicitanteDropDownList.DataSource = objChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();

            ResponsavelDropDownList.DataSource = objChamado.CarregaUsuariosSuporte();
            ResponsavelDropDownList.DataValueField = "IDUsuario";
            ResponsavelDropDownList.DataTextField = "CodigoUsuario";
            ResponsavelDropDownList.DataBind();

            ClassificacaoDropDownList.DataSource = objChamado.CarregaClassificacoes();
            ClassificacaoDropDownList.DataValueField = "IDClassificacao";
            ClassificacaoDropDownList.DataTextField = "Descricao";
            ClassificacaoDropDownList.DataBind();

            StatusDropDownList.DataSource = objChamado.CarregaStatus();
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataBind();

            SistemaDropDownList.DataSource = objChamado.CarregaSistemas();
            SistemaDropDownList.DataValueField = "IDSistema";
            SistemaDropDownList.DataTextField = "Descricao";
            SistemaDropDownList.DataBind();

            PrioridadeDropDownList.DataSource = objChamado.CarregaPrioridades();
            PrioridadeDropDownList.DataValueField = "IDPrioridade";
            PrioridadeDropDownList.DataTextField = "Descricao";
            PrioridadeDropDownList.DataBind();

            HistoricosClass objHistoricosClass = new HistoricosClass();

            objHistoricosClass.IDTipoHistorico = objParametroGeral.RetornaValorNumericoParametro("TIPOPADRAOCHAMADOS");

            EventoDropDownList.DataSource = objHistoricosClass.RetornaEventos();
            EventoDropDownList.DataValueField = "IDEvento";
            EventoDropDownList.DataTextField = "Descricao";
            EventoDropDownList.DataBind();

            objHistoricosClass.IDEvento = Convert.ToInt32(EventoDropDownList.SelectedValue);

            CategoriaDropDownList.DataSource = objHistoricosClass.RetornaEventosCategorias();
            CategoriaDropDownList.DataValueField = "IDCategoria";
            CategoriaDropDownList.DataTextField = "Descricao";
            CategoriaDropDownList.DataBind();
        }

        protected void SolicitanteDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Objusuario.IDUsuario = Convert.ToInt32(SolicitanteDropDownList.SelectedValue == "" ? "0" : SolicitanteDropDownList.SelectedValue);

            SetorDropDownList.DataSource = Objusuario.ConsultaSetoresUsuario();
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataBind();
        }

        public string CarregaDadosDaTela(string IDButton)
        {
            objChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);

            if (NumeroChamadoTextBox.Text == "" || NumeroChamadoTextBox.Text == null)
                objChamado.NumeroChamado = 0;
            else
                objChamado.NumeroChamado = Convert.ToInt32(NumeroChamadoTextBox.Text);

            if (IDButton != "GravarModalLinkButton")
            {
                string erro = "";

                objChamado.IDUsuarioSolicitante = Convert.ToInt32(SolicitanteDropDownList.SelectedValue);
                erro = VerificaUsuarioAtivo(objChamado.IDUsuarioSolicitante);

                if (erro != "") return erro;

                objChamado.IDUsuarioResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue);
                erro = VerificaUsuarioAtivo(objChamado.IDUsuarioResponsavel);

                if (erro != "") return erro;

                objChamado.DataChamado = Convert.ToDateTime(DataTextBox.Text);
                objChamado.IDClassificacao = Convert.ToInt32(ClassificacaoDropDownList.SelectedValue);
                objChamado.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);
                objChamado.IDSistema = Convert.ToInt32(SistemaDropDownList.SelectedValue);
                objChamado.IDPrioridade = Convert.ToInt32(PrioridadeDropDownList.SelectedValue);

                objChamado.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue);

                if (objChamado.IDSetor == 0) return "Escolha o setor";

                objChamado.IDUsuarioKeyUser = ConsultaAdmSetor(objChamado.IDSetor);

                objChamado.Assunto = AssuntoBreveTextBox.Text;

                if (objChamado.Assunto == "") return "Informe o assunto";

                objChamado.descricao = DescricaoTextBox.Text;

                if (objChamado.descricao == "") return "Informe a descrição";
            }
            else
            {
                objChamado.IDUsuarioResponsavel = objParametroGeral.RetornaValorNumericoParametro("URESPONSAVELPADRAOCHAMADOS");

                objChamado.IDUsuarioKeyUser = Convert.ToInt32(Session["IDUsuario"]);

                objChamado.descricao = HistoricoTextBox.Text;

                if (objChamado.descricao == "") return "Informe o histórico";
            }

            objChamado.Evento = Convert.ToInt32(EventoDropDownList.SelectedValue);

            objChamado.Categoria = Convert.ToInt32(CategoriaDropDownList.SelectedValue);

            objChamado.CodigoUsuario = Session["usuario"].ToString();

            return "";
        }

        protected string VerificaUsuarioAtivo(int IDUsuario)
        {
            DataTable Usuarios = objChamado.CarregaUsuarios();

            foreach (DataRow row in Usuarios.Rows)
            {
                if (Convert.ToInt32(row["IDUsuario"]) == IDUsuario)
                {
                    if (row["Status"].ToString() == "Desligado")
                        return "O usuário " + row["Nome"].ToString() + " está desligado.";
                }
            }

            return "";
        }

        protected void GravarLinkButton_Click(object sender, EventArgs e)
        {
            string IDButton = ((System.Web.UI.Control)sender).ID;

            string erro = CarregaDadosDaTela(IDButton);

            if (erro == "")
            {
                if (IDButton != "GravarModalLinkButton")
                {
                    string InformacoesAlteracaoUsuario = ComparaInformacoesAlteracaoUsuario();

                    string InformacoesAlteracaoInformacoes = ComparaInformacoesAlteracaoInformacoes();

                    if (InformacoesAlteracaoInformacoes != "" || objChamado.NumeroChamado == 0)
                        erro = objChamado.GravaDadosPrincipaisChamado(InformacoesAlteracaoUsuario, InformacoesAlteracaoInformacoes);

                    if (erro == "") Session["objChamado"] = objChamado;
                }
                else
                {
                    string operacao = "";

                    if (AprovarLinkButton.Enabled == true)
                    {
                        operacao = "Aprovacao";
                        objChamado.DataAprovacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else if (ReprovarLinkButton.Enabled == true)
                    {
                        operacao = "Reprovacao";
                        objChamado.DataAprovacao = "Reprovado";
                    }

                    erro = objChamado.AprovacaoChamado(operacao);
                }
            }

            if (erro == "")
            {
                CarregaCombos();

                CarregaDadosNaTela();

                TrataAcessos();

                if (IDButton != "GravarModalLinkButton")
                    this.WebUserControlChamado.LiberaNavegacao();
            }

            ApresentaMensagem(erro);
        }

        protected string ComparaInformacoesAlteracaoUsuario()
        {
            StringBuilder informacoes = new StringBuilder("");

            informacoes.AppendLine("Alterado por " + Session["usuario"].ToString());

            informacoes.AppendLine(" <br> <br> ");

            return informacoes.ToString();
        }

        protected string ComparaInformacoesAlteracaoInformacoes()
        {
            ChamadoClass objChamadoAntigo = (ChamadoClass)Session["objChamadoAntigo"];

            ChamadoClass objChamadoNovo = objChamado;

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

        public void CarregaDadosNaTela()
        {
            if (objChamado.NumeroChamado != 0)
            {
                objChamado.RecuperaDadosPrincipais();

                NumeroChamadoTextBox.Text = objChamado.NumeroChamado.ToString();
                NumeroChamadoModalTextBox.Text = objChamado.NumeroChamado.ToString();
                SolicitanteDropDownList.SelectedValue = objChamado.IDUsuarioSolicitante.ToString();
                SolicitanteModalDropDownList.SelectedValue = objChamado.IDUsuarioSolicitante.ToString();
                ResponsavelDropDownList.SelectedValue = objChamado.IDUsuarioResponsavel.ToString();
                DataTextBox.Text = objChamado.DataChamado.ToString("yyyy-MM-dd");
                SistemaDropDownList.SelectedValue = objChamado.IDSistema.ToString();
                StatusDropDownList.SelectedValue = objChamado.IDStatus.ToString();
                ClassificacaoDropDownList.SelectedValue = objChamado.IDClassificacao.ToString();
                PrioridadeDropDownList.SelectedValue = objChamado.IDPrioridade.ToString();
                AssuntoBreveTextBox.Text = objChamado.Assunto ?? "";
                DescricaoTextBox.Text = objChamado.descricao ?? "";
                SetorDropDownList.SelectedValue = objChamado.IDSetor.ToString();
            }
            else
            {
                ConsultaGruposUsuarioSuporte();

                if (GruposUsuario != null)
                    ResponsavelDropDownList.SelectedValue = Session["IDUsuario"].ToString();

                SolicitanteDropDownList.SelectedValue = Session["IDUsuario"].ToString();
            }

            SolicitanteDropDownList_SelectedIndexChanged(null, null);

            TrataDadosPrincipais();

            Session["objChamado"] = objChamado;
        }

        public void TrataDadosPrincipais()
        {
            if (objChamado.NumeroChamado != 0)
            {
                NumeroChamadoTextBox.Text = objChamado.NumeroChamado.ToString();
                DataTextBox.Text = objChamado.DataChamado.ToString("yyyy-MM-dd");

                DataTextBox.Enabled = false;
                AssuntoBreveTextBox.Enabled = false;
                DescricaoTextBox.Enabled = false;
            }
            else
            {
                DataTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        public void TrataAcessos()
        {
            ConsultaGruposUsuarioSuporte();

            if (GruposUsuario != null)
            {
                if (!Convert.ToBoolean(GruposUsuario.Administrador))
                {
                    DataTextBox.Enabled = false;

                    if (objChamado.NumeroChamado != 0)
                    {
                        ClassificacaoDropDownList.Enabled = false;
                        SistemaDropDownList.Enabled = false;
                        PrioridadeDropDownList.Enabled = false;
                        SetorDropDownList.Enabled = false;
                    }

                    StatusDropDownList.Enabled = false;
                }
            }
            else
            {
                DataTextBox.Enabled = false;
                StatusDropDownList.Enabled = false;

                if (objChamado.NumeroChamado == 0)
                {
                    ClassificacaoDropDownList.SelectedValue = "3";
                    SolicitanteDropDownList.SelectedValue = Session["IDUsuario"].ToString();
                    ResponsavelDropDownList.SelectedValue = objChamado.IDUsuarioResponsavelPadrao.ToString();
                }
                else
                {
                    ClassificacaoDropDownList.Enabled = false;
                    SistemaDropDownList.Enabled = false;
                    PrioridadeDropDownList.Enabled = false;
                    GravarLinkButton.Visible = false;
                    SetorDropDownList.Enabled = false;
                }

                BloqueiaListsSolicitanteResponsavel();
            }

            TrataBotoesVisiveis();
        }

        public void BloqueiaListsSolicitanteResponsavel()
        {
            SolicitanteDropDownList.CssClass = "form-control";
            SolicitanteDropDownList.Enabled = false;

            ResponsavelDropDownList.CssClass = "form-control";
            ResponsavelDropDownList.Enabled = false;
        }

        public void TrataBotoesVisiveis()
        {
            //Verifica o IDStatus padrão da avalição de chamado pelo key-user
            int valorNumerico = objParametroGeral.RetornaValorNumericoParametro("STATUSPADRAOCHAMADOSKEYUSER");

            bool admSetor = false;

            bool admSuporte = false;

            bool statusAnaliseKeyUser = (valorNumerico == Convert.ToInt32(StatusDropDownList.SelectedValue == "" ? "0" : StatusDropDownList.SelectedValue));

            bool LiberadoAlteracao = true;

            if (statusAnaliseKeyUser)
            {
                //Verifica se o usuário logado é administrador do setor do chamado                
                admSetor = ConsultaSetoresUsuarioAdm(SetorDropDownList.SelectedValue);
            }

            //Verifica se o usuário logado é administrador do grupo de suporte
            {
                ConsultaGruposUsuarioSuporte();

                if (GruposUsuario != null)
                    admSuporte = Convert.ToBoolean(GruposUsuario.Administrador);
            }

            //Verifica se os botões estão liberados para todos
            {
                DataTable StatusDataTable = objChamado.CarregaStatus();

                if (StatusDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in StatusDataTable.Rows)
                    {
                        if (StatusDropDownList.SelectedItem.Text == row["Descricao"].ToString())
                        {
                            LiberadoAlteracao = (row["LiberadoAlteracao"].ToString() == "S");

                            break;
                        }
                    }
                }
            }

            if ((!statusAnaliseKeyUser) || (!LiberadoAlteracao) || ((!admSetor) && (!admSuporte)))
            {
                AprovarLinkButton.Visible = false;
                ReprovarLinkButton.Visible = false;
            }
            else
            {
                AprovarLinkButton.Visible = true;
                ReprovarLinkButton.Visible = true;
            }

            if (((objChamado.NumeroChamado != 0) && ((!admSetor) && (!admSuporte))) || !LiberadoAlteracao)
                GravarLinkButton.Enabled = false;
            else
                GravarLinkButton.Enabled = true;
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
            Response.Redirect("~/Chamados/ListaChamadosWebForm.aspx?indmnu=5");
        }

        protected void EsconderAprovarReprovarLinkButton_Click(object sender, EventArgs e)
        {
            switch (((System.Web.UI.Control)sender).ID)
            {
                case "AprovarLinkButton":
                    ReprovarLinkButton.Enabled = false;
                    break;

                case "ReprovarLinkButton":
                    AprovarLinkButton.Enabled = false;
                    break;
            }
        }
    }
}