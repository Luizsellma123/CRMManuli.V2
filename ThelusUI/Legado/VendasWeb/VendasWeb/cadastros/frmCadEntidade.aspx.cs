using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using VendasWeb.GerencialVendas;

namespace VendasWeb.cadastros
{
    public partial class frmCadEntidade : System.Web.UI.Page
    {
        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsEntidadesOud clsEntidades = new GerencialVendas.clsEntidadesOud();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
                tabMenu.Items[MultiView1.ActiveViewIndex].Selected = true;

                //Consulta tipo tratamento
                //drpTratamento.DataSource = clsEntidades.Lista_Tipo_Tratamento();
                drpTratamento.DataTextField = "TipoTratCod";
                drpTratamento.DataValueField = "TipoTratNome";
                drpTratamento.DataBind();

                //Consulta tipo logradouro
                //drpTipoLograd.DataSource = clsEntidades.Lista_Tipo_Logradouro();
                drpTipoLograd.DataTextField = "TipoLogradAbrev";
                drpTipoLograd.DataValueField = "TipoLogradNome";
                drpTipoLograd.DataBind();

                //Consulta status
                //drpStatus.DataSource = clsEntidades.Lista_Status();
                drpStatus.DataTextField = "StatEntDescr";
                drpStatus.DataValueField = "StatEntCod";
                drpStatus.DataBind();

                //Consulta Cidade
                //drpCidade.DataSource = clsEntidades.Lista_Cidade();
                drpCidade.DataTextField = "CidNome";
                drpCidade.DataValueField = "CidCod";
                drpCidade.DataBind();

                //Consulta Vendedor
                //drpVendedor.DataSource = clsEntidades.Lista_Vendedor();
                drpVendedor.DataTextField = "VendNome";
                drpVendedor.DataValueField = "VendCod";
                drpVendedor.DataBind();                

                //Consulta Vendedor
                drpCategoria.DataSource = clsEntidades.Lista_Categoria();
                drpCategoria.DataTextField = "CategNome";
                drpCategoria.DataValueField = "CategCodEstr";
                drpCategoria.DataBind();

                if (Session["EntCod"] != null)
                {
                    clsEntidades.EntCod = Session["EntCod"].ToString();
                    clsEntidades.Mostra_Entidade();

                    txtEntCod.Text = clsEntidades.EntCod;
                    txtEntNome.Text = clsEntidades.EntNome;
                    txtEntFantasia.Text = clsEntidades.EntNomeFant;
                    txtCep.Text = clsEntidades.EntCep;
                    txtRua.Text = clsEntidades.EntEnder;
                    txtNumero.Text = clsEntidades.EntEnderNo;
                    txtComplemento.Text = clsEntidades.EntEnderComp;
                    txtBairro.Text = clsEntidades.EntBair;
                    drpCidade.SelectedValue = clsEntidades.CidCod;
                    txtCaixaPostal.Text = clsEntidades.EntCxaPost;
                    drpTipoInsc.SelectedValue = clsEntidades.EntTipoFJ;
                    txtCNPJ.Text = clsEntidades.EntCpfCgc;
                    txtInscricaoEstadual.Text = clsEntidades.EntRgIe;
                    drpNatureza.SelectedValue = clsEntidades.EntNat;
                    drpStatus.SelectedValue = clsEntidades.StatEntCod;
                    //drpStatus.SelectedItem.Text = clsEntidades.EntStatDescr;
                    /*txtEmail.Text = clsEntidades.EntWebEMail;
                    txtSite.Text = clsEntidades.EntWebWWW;
                    txtFone.Text = clsEntidades.EntFoneNum;
                    txtDDD.Text = clsEntidades.EntFoneDDD;*/
                    drpCategoria.SelectedValue = clsEntidades.CategCodEstr;
                    drpVendedor.SelectedValue = clsEntidades.VendCod;

                    if (clsEntidades.VendCod != "")
                        drpVendedor.Enabled = false;

                    if (clsEntidades.CategCodEstr != "")
                        drpCategoria.Enabled = false;
                }
                else
                {
                    clsEntidades.UsuCod = Session["usuario"].ToString();
                    string vendCod = "";// clsEntidades.Lista_Vendedor_Logado();

                    if (vendCod != "")
                        drpVendedor.Enabled = false;

                    drpVendedor.SelectedValue = vendCod;

                    string categCodEstr =  clsEntidades.Lista_Categoria_Usuario_Logado();

                    if (categCodEstr != "")
                        drpCategoria.Enabled = false;

                    drpCategoria.SelectedValue = categCodEstr;
                }
            }
        }

        protected void tabMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "t1":
                    MultiView1.ActiveViewIndex = 0;
                    txtEntCod.Focus();
                    break;

                case "t2":
                    MultiView1.ActiveViewIndex = 1;
                    drpNatureza.Focus();
                    break;

                case "t3":
                    MultiView1.ActiveViewIndex = 2;
                    txtRegiao.Focus();
                    break;
            }
        }

        protected void SalvarButton_Click(object sender, EventArgs e)
        {
            if (txtCNPJ.Text != "0")
            {
                clsEntidades.TipoTratCod = drpTratamento.SelectedValue;
                clsEntidades.EntNome = txtEntNome.Text;
                clsEntidades.EntNomeFant = txtEntFantasia.Text;
                clsEntidades.EntCep = txtCep.Text;
                clsEntidades.EntLograd = drpTipoLograd.SelectedValue;
                clsEntidades.EntEnder = txtRua.Text;
                clsEntidades.EntEnderNo = txtNumero.Text;
                clsEntidades.EntEnderComp = txtComplemento.Text;
                clsEntidades.EntBair = txtBairro.Text;
                clsEntidades.CidCod = drpCidade.SelectedItem.Value;
                clsEntidades.CidCod = drpCidade.SelectedValue;
                //clsEntidades.CidNome = drpCidade.SelectedItem.Text;
                clsEntidades.EntCxaPost = txtCaixaPostal.Text;
                clsEntidades.EntTipoFJ = drpTipoInsc.Text;
                clsEntidades.EntCpfCgc = txtCNPJ.Text;
                clsEntidades.StatEntCod = drpStatus.SelectedItem.Value;
                clsEntidades.EntStatDescr = drpStatus.SelectedItem.Text;
                clsEntidades.EntNat = drpNatureza.SelectedValue;
                clsEntidades.VendCod = drpVendedor.SelectedValue;
                clsEntidades.EntRgIe = txtInscricaoEstadual.Text;
                //clsEntidades.EntWebWWW = txtSite.Text;
                //clsEntidades.EntWebEMail = txtEmail.Text;
                //clsEntidades.EntFoneDDD = txtDDD.Text;
                //clsEntidades.EntFoneNum = txtFone.Text;
                clsEntidades.CategCodEstr = drpCategoria.SelectedValue;
                //clsEntidades.Entidade_Inserir();

                Response.Write("<script>alert(\"Entidade cadastrada com sucesso.\");</script>");

                Limpar_Campos();
            }
        }

        protected void txtCNPJ_TextChanged(object sender, EventArgs e)
        {

            string Retorno = "";
            Retorno = mdlFuncoes.Valida_CPF_CNPJ(txtCNPJ.Text,"");

            if (Retorno != "Valido")
            {
                //ValidaLabel.Text = "CPF/CNPJ" + " " + txtCNPJ.Text.ToString() + "<br> " + Retorno;
                Response.Write("<script>alert(\"CPF-CNPJ " + txtCNPJ.Text.ToString() + " " + Retorno.ToString() + "\");</script>");
                txtCNPJ.Text = "0";
                txtCNPJ.Focus();
            }
        }

        protected void Limpar_Campos()
        {
            txtEntNome.Text="";
            txtEntFantasia.Text="";
            txtCep.Text="";
            txtRua.Text="";
            txtNumero.Text="";
            txtComplemento.Text="";
            txtBairro.Text="";
            drpCidade.SelectedIndex=0;
            txtCaixaPostal.Text="";
            txtCNPJ.Text="";
            txtInscricaoEstadual.Text="";
            txtSite.Text="";
            txtEmail.Text="";
            txtDDD.Text="";
            txtFone.Text = "";

            tabMenu.Items[MultiView1.ActiveViewIndex].Selected = true;
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"frmCarteira.aspx?indmnu=31\";</script>");
        }

        protected void EnviarAnaliseButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}