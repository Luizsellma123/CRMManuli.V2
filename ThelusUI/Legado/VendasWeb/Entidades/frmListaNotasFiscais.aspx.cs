using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.cadastros
{
    public partial class frmListaNotasFiscais : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        GerencialVendas.NFClass NFClass = new GerencialVendas.NFClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "";

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                txtEntidade.Text = ((clsEntidades)Session["clsEntidades"]).EntCod;

                strSql += "select EU.EmpCod, EF.EmpNomeFant as EmpNome from EMP_FIL_USUARIO EU, EMPRESA_FILIAL EF where ";
                strSql += "EU.EmpCod=EF.EmpCod and UsuCod = '" + Session["usuario"].ToString() + "' and EU.EmpCod<>'1.99'";

                NFClass.PreencheDropList(chkListaEmpresa, strSql, "Todos");

                NFClass.EmpCod = "";
                Atualizar_Grid();
            }
        }

        public void Atualizar_Grid()
        {
            NFClass.EntCod = txtEntidade.Text;
            NFClass.NfNum = txtNF.Text;

            string dados = "";
            int aux = 0;

            for (int i = 0; i < chkListaEmpresa.Items.Count; i++)
            {
                if (chkListaEmpresa.Items[i].Selected)
                {

                    if (aux == 0)
                    {
                        dados = dados + chkListaEmpresa.Items[i].Value.ToString();
                        aux = aux + 1;
                    }
                    else
                    {
                        dados = dados + "," + chkListaEmpresa.Items[i].Value.ToString();

                        aux = aux + 1;
                    }

                    if (chkListaEmpresa.Items[i].Value.ToString() == "Todos")
                    {
                        dados = "Todos";
                    }

                }
            }

            if (dados == "Todos" || dados == "")
            {
                dados = "";
            }
            else
            {
                dados = "," + dados + ",";
            }

            NFClass.EmpCod = dados;

            NFGridView.DataSource = NFClass.Lista_NF();
            Session.Add("TEMP_SESSAO", NFGridView.DataSource);
            NFGridView.DataBind();
        }

       /* protected void NFGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NFGridView.PageIndex = e.NewPageIndex;
            Atualizar_Grid();
        }*/
        
        protected void ListarButton_Click(object sender, EventArgs e)
        {
            if (txtEntidade.Text == "" && txtNF.Text == "")
            {
                Response.Write("<script>alert(\"Informe uma nota fiscal ou uma entidade para consulta\");</script>");
                //Response.Write("<script>window.location=\"FrmAbaPrincipal.aspx?indmnu=32\";</script>");
            }
            else
            {
                Atualizar_Grid();
            }
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            if (Session["Retornar"] != null)
            {
                Response.Redirect(Session["Retornar"].ToString());
            }
            else
            {
                Response.Write("<script>alert(\"Não foi informado a página de origem. Entrar em contato com a TI\");</script>");
                //Response.Write("<script>window.location=\"FrmAbaPrincipal.aspx?indmnu=32\";</script>");
            }               
        }
    }
}