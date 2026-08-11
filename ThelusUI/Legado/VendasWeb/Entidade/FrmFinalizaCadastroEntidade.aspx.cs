using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidade
{
    public partial class FrmFinalizaCadastroEntidade : System.Web.UI.Page
    {
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Valida Acesso
                OBJSessao.ValidaAcesso();

                if (Session["clsEntidades"] != null)
                {
                    //Descarega a session da Entidade
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                    //Salva Dados
                    SalvarEntidade();
                }
            }
        }

        public void SalvarEntidade()
        {
            string RetornoCadastro = "";
            string RetornoEmail = "";
            string RetornoContato = "";
            string RetornoCategoria = "";
            string RetornoConcorrencia = "";
            string RetornoPerfil = "";
            string RetornoRelacionamento = "";
            string RetornoCondPag = "";

            #region Cadastro Entidade
            //Se o codigo da entidade esiver em branco, trata-se de um novo cadastro
            if (ObjEntidadesClass.TipoOperacao == "Inclusão")
            {
                //Grava Entidade
                RetornoCadastro = ObjEntidadesClass.Incluir_Entidade();
                if (RetornoCadastro == "")
                {
                    FileUpload FU = new FileUpload();

                    #region Gravar Relacionamento
                    if (ObjEntidadesClass.ListEntRelacionamentoclass != null)
                    {
                        if (ObjEntidadesClass.ListEntRelacionamentoclass.Count > 0)
                        {

                            for (int t = 0; t < ObjEntidadesClass.ListEntRelacionamentoclass.Count; t++)
                            {
                                ObjEntidadesClass.DescricaoRelacionamento = ObjEntidadesClass.ListEntRelacionamentoclass[t].Descricao;
                                ObjEntidadesClass.DataRelacionamento = ObjEntidadesClass.ListEntRelacionamentoclass[t].Data;
                                ObjEntidadesClass.ListEntRelacionamentoclass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoRelacionamento += ObjEntidadesClass.ListEntRelacionamentoclass[t].Inserir_Relacionamento();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Email XML
                    if (ObjEntidadesClass.ListEntWeb != null)
                    {
                        if (ObjEntidadesClass.ListEntWeb.Count > 0)
                        {

                            for (int i = 0; i < ObjEntidadesClass.ListEntWeb.Count; i++)
                            {

                                ObjEntidadesClass.ListEntWeb[i].EntCod = ObjEntidadesClass.EntCod;
                                RetornoEmail += ObjEntidadesClass.ListEntWeb[i].Incluir_Email();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Telefone de Contato
                    if (ObjEntidadesClass.ListContatoClass != null)
                    {
                        if (ObjEntidadesClass.ListContatoClass.Count > 0)
                        {

                            for (int t = 0; t < ObjEntidadesClass.ListContatoClass.Count; t++)
                            {
                                ObjEntidadesClass.ListContatoClass[t].UsuCod = Session["usuario"].ToString();
                                ObjEntidadesClass.ListContatoClass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoContato += ObjEntidadesClass.ListContatoClass[t].Incluir_Contato();
                            }
                        }
                    }
                    #endregion

                    #region Incluindo Endereco de Entrega
                    if (ObjEntidadesClass.EntLocEntregaOMesmo != null)
                    {
                        if (ObjEntidadesClass.EntLocEntregaOMesmo == "Não")
                        {
                            ObjEntidadesClass.EnderecoEntregaClass.EntCod = ObjEntidadesClass.EntCod;
                            ObjEntidadesClass.EnderecoEntregaClass.Incluir_Endereco_Entrega();

                        }
                    }
                    #endregion

                    #region Incluir Anexos

                    if (ObjEntidadesClass.ListDocEntidadeClass != null)
                    {
                        for (int t = 0; t < ObjEntidadesClass.ListDocEntidadeClass.Count(); t++)
                        {

                            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                            ObjEntidadesClass.DocEntPathArq = ObjEntidadesClass.ListDocEntidadeClass[t].DocEntPathArq;
                            ObjEntidadesClass.DocEntObs = ObjEntidadesClass.ListDocEntidadeClass[t].DocEntObs;
                            ObjEntidadesClass.DocEntImage = ObjEntidadesClass.ListDocEntidadeClass[t].DocEntImage;

                            ObjEntidadesClass.ListDocEntidadeClass[t].EntCod = ObjEntidadesClass.EntCod;
                            ObjEntidadesClass.ListDocEntidadeClass[t].Incluir_DocEntidade();
                        }
                    }


                    #endregion

                    #region Gravar CNAEs Secundarias
                    if (ObjEntidadesClass.ListEntCategoriaClass != null)
                    {
                        if (ObjEntidadesClass.ListEntCategoriaClass.Count > 0)
                        {

                            for (int t = 0; t < ObjEntidadesClass.ListEntCategoriaClass.Count; t++)
                            {
                                ObjEntidadesClass.CategCodEstr = ObjEntidadesClass.ListEntCategoriaClass[t].CategCodEstr;
                                ObjEntidadesClass.ListEntCategoriaClass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoCategoria += ObjEntidadesClass.ListEntCategoriaClass[t].Incluir_Categoria();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Concorrencia
                    if (ObjEntidadesClass.ListEntConcorrenciaClass != null)
                    {
                        if (ObjEntidadesClass.ListEntConcorrenciaClass.Count > 0)
                        {

                            for (int t = 0; t < ObjEntidadesClass.ListEntConcorrenciaClass.Count; t++)
                            {
                                ObjEntidadesClass.NomeConcorrente = ObjEntidadesClass.ListEntConcorrenciaClass[t].NomeConcorrente;
                                ObjEntidadesClass.ObservacaoConcorrente = ObjEntidadesClass.ListEntConcorrenciaClass[t].ObservacaoConcorrente;
                                ObjEntidadesClass.ListEntConcorrenciaClass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoConcorrencia += ObjEntidadesClass.ListEntConcorrenciaClass[t].Inserir_Concorrencia();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Perfil
                    if (ObjEntidadesClass.ListEntPerfilDeConsumoClass != null)
                    {
                        if (ObjEntidadesClass.ListEntPerfilDeConsumoClass.Count > 0)
                        {

                            for (int t = 0; t < ObjEntidadesClass.ListEntPerfilDeConsumoClass.Count; t++)
                            {
                                ObjEntidadesClass.LinhaConsumoCliente = ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].Linha;
                                ObjEntidadesClass.QuantidadeConsumoCliente = ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].Quantidade;
                                ObjEntidadesClass.DescricaoConsumoCliente = ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].Descricao;
                                ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoPerfil += ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].Inserir_Perfil_Consumo();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Cond. Pag
                    if (ObjEntidadesClass.ListCondPag != null)
                    {
                        if (ObjEntidadesClass.ListCondPag.Count > 0)
                        {
                            for (int t = 0; t < ObjEntidadesClass.ListCondPag.Count; t++)
                            {
                                ObjEntidadesClass.CondPagCod = ObjEntidadesClass.ListCondPag[t].CondPagCod;
                                ObjEntidadesClass.ListCondPag[t].UsuCod = Session["usuario"].ToString();
                                ObjEntidadesClass.ListCondPag[t].EntCod = ObjEntidadesClass.EntCod;
                                ObjEntidadesClass.ListCondPag[t].CondPagEntValAte = 0;
                                RetornoCondPag += ObjEntidadesClass.ListCondPag[t].Incluir_Cond_Pag_Ent();
                            }
                        }
                    }
                    #endregion

                    //Concatenando Retornos
                    RetornoCadastro = RetornoCadastro + "" + RetornoEmail + "" + RetornoContato + "" + RetornoCategoria + "" + RetornoConcorrencia + "" + RetornoRelacionamento + "" + RetornoPerfil + "" + RetornoCondPag;
                }

                //Verifica se teve algum erro nos cadastros
                if (RetornoCadastro != "")
                {
                    //Elimina Entidade Com erro
                    ObjEntidadesClass.Exclui_Entidade();
                    //ObjEntidadesClass.EntCod = "";
                    Session["clsEntidades"] = ObjEntidadesClass;

                    //Retorna Mensagem de Erro
                    Session["Msg"] = RetornoCadastro;

                    Response.Write("<script>alert(\"" + RetornoCadastro.ToString() + "\");</script>");
                    Response.Redirect("FrmAbaPrincipal.aspx?indmnu=2");
                }
                else
                {
                    Session["Msg"] = "Entidade " + ObjEntidadesClass.EntCod + " <br>Cadastrada com Sucesso.";

                    //Texto Email
                    if (ObjEntidadesClass.EntTipoFJ == "Física")
                    {
                        ObjEntidadesClass.OperacaoEmail = "CONSUMO";
                    }
                    else
                    {
                        ObjEntidadesClass.OperacaoEmail = "";//Branco ele vai consulta o Email que estiver cadastrado no Status da Entidade
                    }

                    ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                    ObjEntidadesClass.DescricaoEmail = "Novo Cadastro Realizado - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                    ObjEntidadesClass.Texto = " Cadastro realizado por : " + Session["usuario"].ToString() + "<BR>";
                    ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                    ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                    ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                    ObjEntidadesClass.Texto += " Data : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                    //ObjEntidadesClass.Envia_Email_Entidade();

                    ObjEntidadesClass = new GerencialVendas.clsEntidades();

                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }
            }
            else
            if (ObjEntidadesClass.TipoOperacao == "Alterar")
            {
                //Grava Entidade
                if (ObjEntidadesClass.Origem == "Analise")
                    ObjEntidadesClass.AssuntoEmail = "Análise - Cliente antes de alterar " + ObjEntidadesClass.EntCod.ToString();
                else
                    ObjEntidadesClass.AssuntoEmail = "Alteração - Cliente antes de alterar" + ObjEntidadesClass.EntCod.ToString();

                ObjEntidadesClass.Envia_Email_Alteracao_Entidade();
                RetornoCadastro = ObjEntidadesClass.Incluir_Entidade();
                if (RetornoCadastro == "")
                {
                    #region Gravar Telefone de Contato
                    if (ObjEntidadesClass.ListContatoClass != null)
                    {
                        if (ObjEntidadesClass.ListContatoClass.Count > 0)
                        {
                            ObjEntidadesClass.ListContatoClass[0].Excluir_Contato();
                            for (int t = 0; t < ObjEntidadesClass.ListContatoClass.Count; t++)
                            {
                                ObjEntidadesClass.ListContatoClass[t].UsuCod = Session["usuario"].ToString();
                                ObjEntidadesClass.ListContatoClass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoContato += ObjEntidadesClass.ListContatoClass[t].Incluir_Contato();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Relacionamento
                    if (ObjEntidadesClass.ListEntRelacionamentoclass != null)
                    {
                        if (ObjEntidadesClass.ListEntRelacionamentoclass.Count > 0)
                        {
                            ObjEntidadesClass.ListEntRelacionamentoclass[0].Relacionamento_Excluir_Todos();
                            for (int t = 0; t < ObjEntidadesClass.ListEntRelacionamentoclass.Count; t++)
                            {
                                ObjEntidadesClass.DescricaoRelacionamento = ObjEntidadesClass.ListEntRelacionamentoclass[t].Descricao;
                                ObjEntidadesClass.DataRelacionamento = ObjEntidadesClass.ListEntRelacionamentoclass[t].Data;
                                ObjEntidadesClass.ListEntRelacionamentoclass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoRelacionamento += ObjEntidadesClass.ListEntRelacionamentoclass[t].Inserir_Relacionamento();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Perfil
                    if (ObjEntidadesClass.ListEntPerfilDeConsumoClass != null)
                    {
                        if (ObjEntidadesClass.ListEntPerfilDeConsumoClass.Count > 0)
                        {
                            ObjEntidadesClass.ListEntPerfilDeConsumoClass[0].EntCod = ObjEntidadesClass.EntCod;
                            ObjEntidadesClass.ListEntPerfilDeConsumoClass[0].Perfil_Consumo_Excluir_Todos();
                            for (int t = 0; t < ObjEntidadesClass.ListEntPerfilDeConsumoClass.Count; t++)
                            {
                                ObjEntidadesClass.LinhaConsumoCliente = ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].Linha;
                                ObjEntidadesClass.QuantidadeConsumoCliente = ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].Quantidade;
                                ObjEntidadesClass.DescricaoConsumoCliente = ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].Descricao;
                                ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoPerfil += ObjEntidadesClass.ListEntPerfilDeConsumoClass[t].Inserir_Perfil_Consumo();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Concorrencia
                    if (ObjEntidadesClass.ListEntConcorrenciaClass != null)
                    {
                        if (ObjEntidadesClass.ListEntConcorrenciaClass.Count > 0)
                        {
                            ObjEntidadesClass.ListEntConcorrenciaClass[0].Concorrencia_Excluir_Todas();
                            for (int t = 0; t < ObjEntidadesClass.ListEntConcorrenciaClass.Count; t++)
                            {
                                ObjEntidadesClass.NomeConcorrente = ObjEntidadesClass.ListEntConcorrenciaClass[t].NomeConcorrente;
                                ObjEntidadesClass.ObservacaoConcorrente = ObjEntidadesClass.ListEntConcorrenciaClass[t].ObservacaoConcorrente;
                                ObjEntidadesClass.ListEntConcorrenciaClass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoConcorrencia += ObjEntidadesClass.ListEntConcorrenciaClass[t].Inserir_Concorrencia();
                            }
                        }
                    }
                    #endregion

                    #region Gravar CNAEs Secundarias
                    if (ObjEntidadesClass.ListEntCategoriaClass != null)
                    {
                        if (ObjEntidadesClass.ListEntCategoriaClass.Count > 0)
                        {
                            ObjEntidadesClass.ListEntCategoriaClass[0].Excluir_Categoria_Todas();
                            for (int t = 0; t < ObjEntidadesClass.ListEntCategoriaClass.Count; t++)
                            {
                                ObjEntidadesClass.CategCodEstr = ObjEntidadesClass.ListEntCategoriaClass[t].CategCodEstr;
                                ObjEntidadesClass.ListEntCategoriaClass[t].EntCod = ObjEntidadesClass.EntCod;
                                RetornoCategoria += ObjEntidadesClass.ListEntCategoriaClass[t].Incluir_Categoria();
                            }
                        }
                    }
                    #endregion

                    #region Gravar Cond. Pag
                    if (ObjEntidadesClass.ListCondPag != null)
                    {
                        if (ObjEntidadesClass.ListCondPag.Count > 0)
                        {
                            ObjEntidadesClass.ListCondPag[0].Remove_Cond_Pag_Todas();
                            for (int t = 0; t < ObjEntidadesClass.ListCondPag.Count; t++)
                            {
                                ObjEntidadesClass.CondPagCod = ObjEntidadesClass.ListCondPag[t].CondPagCod;
                                ObjEntidadesClass.ListCondPag[t].UsuCod = Session["usuario"].ToString();
                                ObjEntidadesClass.ListCondPag[t].EntCod = ObjEntidadesClass.EntCod;
                                ObjEntidadesClass.ListCondPag[t].CondPagEntValAte = 0;
                                RetornoCondPag += ObjEntidadesClass.ListCondPag[t].Incluir_Cond_Pag_Ent();
                            }
                        }
                    }
                    #endregion

                    #region Incluir Anexos

                    if (ObjEntidadesClass.ListDocEntidadeClass != null)
                    {
                        if (ObjEntidadesClass.ListDocEntidadeClass.Count > 0)
                        {
                            ObjEntidadesClass.ListDocEntidadeClass[0].EntCod = ObjEntidadesClass.EntCod;
                            ObjEntidadesClass.ListDocEntidadeClass[0].Doc_Excluir_Todos();
                            for (int t = 0; t < ObjEntidadesClass.ListDocEntidadeClass.Count(); t++)
                            {

                                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                                ObjEntidadesClass.DocEntPathArq = ObjEntidadesClass.ListDocEntidadeClass[t].DocEntPathArq;
                                ObjEntidadesClass.DocEntObs = ObjEntidadesClass.ListDocEntidadeClass[t].DocEntObs;
                                ObjEntidadesClass.DocEntImage = ObjEntidadesClass.ListDocEntidadeClass[t].DocEntImage;

                                ObjEntidadesClass.ListDocEntidadeClass[t].EntCod = ObjEntidadesClass.EntCod;
                                ObjEntidadesClass.ListDocEntidadeClass[t].Incluir_DocEntidade();
                            }
                        }
                    }


                    #endregion

                    if (ObjEntidadesClass.Origem == "Analise")
                        ObjEntidadesClass.AssuntoEmail = "Análise de Novo Cliente " + ObjEntidadesClass.EntCod.ToString();
                    else
                        ObjEntidadesClass.AssuntoEmail = "Alteração no cadastro da entidade " + ObjEntidadesClass.EntCod.ToString();

                    ObjEntidadesClass.Envia_Email_Alteracao_Entidade();
                }
            }
            #endregion
        }
    }
}