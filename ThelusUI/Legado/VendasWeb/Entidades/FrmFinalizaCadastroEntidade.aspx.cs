using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{

    public partial class FrmFinalizaCadastroEntidade : System.Web.UI.Page
    {
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

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

            #region Cadastro Entidade
            //Se o codigo da entidade esiver em branco, trata-se de um novo cadastro
            if (ObjEntidadesClass.TipoOperacao == "Inclusão")
            {

                //Grava Entidade
                RetornoCadastro = ObjEntidadesClass.Incluir_Entidade();
                if (RetornoCadastro == "")
                {
                    FileUpload FU = new FileUpload();

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
                        for (int D = 0; D < ObjEntidadesClass.ListDocEntidadeClass.Count(); D++)
                        {
                            ObjEntidadesClass.ListDocEntidadeClass[D].UsuCod = Session["usuario"].ToString();
                            ObjEntidadesClass.ListDocEntidadeClass[D].EntCod = ObjEntidadesClass.EntCod;
                            ObjEntidadesClass.ListDocEntidadeClass[D].Incluir_DocEntidade();
                        }
                    }




                    #endregion


                    //Concatenando Retornos
                    RetornoCadastro = RetornoCadastro + "" + RetornoEmail + "" + RetornoContato;


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



                    ObjEntidadesClass.Envia_Email_Entidade();

                    ObjEntidadesClass = new GerencialVendas.clsEntidades();


                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }


            }

            #endregion

           
        }

    }

}