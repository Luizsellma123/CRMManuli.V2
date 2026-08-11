using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.classes;

namespace VendasWeb
{
    public partial class cadPedido : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlfuncoes = new funcoes();
        criptografia mdlCriptografia = new criptografia();

        //Instancia OBJETO Pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.DataBind();

                LinkButton1.Visible = false;
                LinkButton2.Visible = false;

                novoPedido.veioCRM = "nao";
                Session["pedidoNovo"] = novoPedido;
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "0";
            TextBox2.Text = "0";
            ltlListaEntidade.Text = gerLista(14);
        }

        public string gerLista(int quant)
        {
            int indexPage = 0;
            int fimPage = 0;
            int numPad = 0;

            if (quant > 0)
            {
                indexPage = Convert.ToInt32(TextBox1.Text);
                fimPage = Convert.ToInt32(TextBox1.Text);
                fimPage = fimPage + 14;
                indexPage = indexPage + 1;
            }
            else
            {
                indexPage = Convert.ToInt32(TextBox2.Text);
                fimPage = Convert.ToInt32(TextBox2.Text);
                indexPage = indexPage - 14;
                fimPage = fimPage - 1;
            }

            if (indexPage <= 0 || indexPage == 1)
            {
                LinkButton1.Visible = false;
            }
            else
            {
                LinkButton1.Visible = true;
            }

            numPad = numergoRegistros();

            if (fimPage >= numPad)
            {
                LinkButton2.Visible = false;
            }
            else
            {
                LinkButton2.Visible = true;
            }
            return linhasEntidade(indexPage, fimPage);
        }

        public string linhasEntidade(int indexPage, int fimPage)
        {
            string descLinhas = "";
            string strSQL = "";
            string codEmp = drpEmpresa.SelectedItem.Value;
            string strconec;

            strSQL = sqlConsulta(indexPage, fimPage);           

            strconec = mdlfuncoes.getString().ToString();                 

            using (SqlConnection dbConnection = new SqlConnection(strconec))
            {
                dbConnection.Open();
                string TextoDiasSemCompra = "";
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    using (SqlDataReader drEntidade = dbCommand.ExecuteReader())
                    {
                        if (drEntidade.HasRows)
                        {
                            //Inicio da tabela
                            descLinhas += "<table class=\"lstTabela\">";

                            //cabeçario da tabela
                            descLinhas += "<tr class=\"tabLstCab\">";
                            descLinhas += "<td>Selecione:</td>";
                            descLinhas += "<td>Duplicata:</td>";
                            descLinhas += "<td>Código:</td>";
                            descLinhas += "<td width='600px'>Nome:</td>";
                            descLinhas += "<td width='50px'>Bairro</td>";
                            descLinhas += "<td width='50px'>Cidade</td>";
                            descLinhas += "<td>CNPJ/CPF:</td>";
                            descLinhas += "<td>Vendedor Cadastrado:</td>";
                            descLinhas += "<td>Dias sem Compra:</td>";
                            

                            descLinhas += "</tr>";

                            while (drEntidade.Read())
                            {
                               

                                if(drEntidade["DiasSemCompra"].ToString() != "") 
                                {
                                    TextoDiasSemCompra = drEntidade["DiasSemCompra"].ToString();
                                
                                }
                                else

                                {
                                    TextoDiasSemCompra = "<font color='#ff0000;'>S/C</>";
                                }

                                descLinhas += "<td class=\"edicao\"><a href=\"../cadastros/cadPedidoPrincipal.aspx?indmnu=2&codEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idEnt=" + mdlCriptografia.Criptografar(drEntidade["EntCod"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("inclusao", "#!$a36?@") + "\" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                                descLinhas += "<td class=\"edicao\"><a href=\"../WebVendas/Entidade/FrmDuplicata.aspx?indmnu=2&codEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idEnt=" + mdlCriptografia.Criptografar(drEntidade["EntCod"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("inclusao", "#!$a36?@") + "\" class=\"imgedit\"><img src=\"../imagens/atention.png\" alt=\"Duplicata\" border=\"0\" /></a></td>";
                                descLinhas += "<td class=\"codigo\">" + drEntidade["EntCod"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drEntidade["EntNome"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drEntidade["EntBair"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drEntidade["CidNome"] + "</td>";
                                descLinhas += "<td>" + drEntidade["EntCpfCgc"] + "</td>";
                                descLinhas += "<td>" + drEntidade["UsuCod"] + "</td>";
                                descLinhas += "<td><center>" + TextoDiasSemCompra + "</center></td>";
                                
                               
                                
                                descLinhas += "</tr>";
                            }

                            //Fim tabela
                            descLinhas += "</table><br />";
                        }
                    }
                }
            }

            TextBox1.Text = fimPage.ToString();
            TextBox2.Text = indexPage.ToString();
            return descLinhas;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ltlListaEntidade.Text = gerLista(-14);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ltlListaEntidade.Text = gerLista(+14);
        }

        public int numergoRegistros()
        {
            int numPad = 0;
            string codEmp = drpEmpresa.SelectedItem.Value;
            
            if (txtFiltroEntCod.Text == "" || txtFiltroEntCod.Text == null)
            {
                if (Convert.ToInt32(Session["nivel"]) == 0)
                {
                    numPad = Convert.ToInt32((mdlfuncoes.Consulta_Quantidade_Entidade_Vendedor(Session["usuario"].ToString())).ToString());
                }
                else
                {
                    numPad = Convert.ToInt32((mdlfuncoes.Consulta_Quantidade_Entidade()).ToString());
                }
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpEntCod.SelectedItem.Value);
                string valorConsulta = txtFiltroEntCod.Text;
                if (tipoConsulta == 1)
                {
                    if (Convert.ToInt32(Session["nivel"]) == 0)
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from ENTIDADE E INNER JOIN VEND_ENT VE ON E.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod where E.EntNome like'" + valorConsulta + "%' and E.EntStatDescr = 'Ativo' and Ven.UsuCod='" + Session["usuario"].ToString() + "'", "numergoRegistros")).ToString());
                    }
                    else
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from ENTIDADE where EntNome like'" + valorConsulta + "%' and EntStatDescr = 'Ativo'", "numergoRegistros")).ToString());
                    }
                }
                else
                {
                    if (Convert.ToInt32(Session["nivel"]) == 0)
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from ENTIDADE E INNER JOIN VEND_ENT VE ON E.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod where E.EntCod like'" + valorConsulta + "%' and E.EntStatDescr = 'Ativo' and Ven.UsuCod='" + Session["usuario"].ToString() + "'", "numergoRegistros")).ToString());
                    }
                    else
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from ENTIDADE where EntCod like'" + valorConsulta + "%' and EntStatDescr = 'Ativo'", "numergoRegistros")).ToString());
                    }
                }
            }

            return numPad;
        }

        public string sqlConsulta(int indexPage, int fimPage)
        {
            string strSQL = "";
            string codEmp = drpEmpresa.SelectedItem.Value;

            if (txtFiltroEntCod.Text == "" || txtFiltroEntCod.Text == null)
            {
                if (Convert.ToInt32(Session["nivel"]) == 0)
                {
                    strSQL += geraConsultaEntidade1(indexPage, fimPage);
                }
                else
                {
                    strSQL += geraConsultaEntidade2(indexPage, fimPage);
                }
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpEntCod.SelectedItem.Value);
                string valorConsulta = txtFiltroEntCod.Text;
                if (tipoConsulta == 1)
                { 
                    if (Convert.ToInt32(Session["nivel"]) == 0)
                    {
                        strSQL += geraConsultaEntidade3(indexPage, fimPage, valorConsulta);
                    }
                    else
                    {
                        strSQL += geraConsultaEntidade4(indexPage, fimPage, valorConsulta);
                    }
                }
                else
                {
                    if (Convert.ToInt32(Session["nivel"]) == 0)
                    {
                        strSQL += geraConsultaEntidade5(indexPage, fimPage, valorConsulta);
                    }
                    else
                    {
                        strSQL += geraConsultaEntidade6(indexPage, fimPage, valorConsulta);
                    }
                }
            }
            return strSQL;
        }

        public string geraConsultaEntidade1(int indexPage, int fimPage)
        {
            string strSQL = "";

            strSQL += "select EntCod, EntNome, EntBair, EntCpfCgc, Cidnome ,UsuCod, reg, (case when  DiasSemCompra  > 90 then '<font color=''#ff0000;''>' + cast(DiasSemCompra as varchar(max)) + '</>' Else cast(DiasSemCompra as varchar(max)) END ) as DiasSemCompra from ";
            strSQL += " (select ve.UsuCod,E.EntCod, E.EntNome, E.EntBair, E.EntCpfCgc, C.Cidnome,  ROW_NUMBER() OVER(ORDER BY E.EntCod) as reg,DATEDIFF(day, DataCompra,GETDATE())  as DiasSemCompra   from Entidade E ";
            strSQL += " INNER JOIN CIDADE C ON C.CidCod=E.CidCod ";
            strSQL += " INNER join VEND_ENT vend ON vend.EntCod = E.EntCod ";
            strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
            strSQL += " LEFT outer JOIN USER_ULTIMA_COMPRA ped WITH(NOLOCK)  ON  ped.EntCod = e.EntCod "; 
            
            strSQL += " WHERE E.EntStatDescr = 'Ativo' and Ve.usuCod='" + Session["usuario"].ToString() + "' ) a WHERE reg between " + indexPage + " and " + fimPage + ";";
           
            return strSQL;
        }

        public string geraConsultaEntidade2(int indexPage, int fimPage)
        {
            string strSQL = "";

            strSQL += "select EntCod, EntNome, EntBair, EntCpfCgc, Cidnome ,UsuCod, reg,(case when  DiasSemCompra  > 90 then '<font color=''#ff0000;''>' + cast(DiasSemCompra as varchar(max)) + '</>' Else cast(DiasSemCompra as varchar(max)) END ) as DiasSemCompra from ";
            strSQL += " (select ve.UsuCod,E.EntCod, E.EntNome, E.EntBair, E.EntCpfCgc, C.Cidnome,  ROW_NUMBER() OVER(ORDER BY E.EntCod) as reg,DATEDIFF(day, DataCompra,GETDATE())  as DiasSemCompra   from Entidade E ";
            strSQL += " INNER JOIN CIDADE C ON C.CidCod=E.CidCod ";
            strSQL += " left join VEND_ENT vend ON vend.EntCod = E.EntCod ";
            strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
            strSQL += " LEFT outer JOIN USER_ULTIMA_COMPRA ped WITH(NOLOCK)  ON  ped.EntCod = e.EntCod "; 
            strSQL += " WHERE E.EntStatDescr = 'Ativo') a WHERE reg between " + indexPage + " and " + fimPage + ";";

            return strSQL;
        }

        public string geraConsultaEntidade3(int indexPage, int fimPage,string valorConsulta)
        {
            string strSQL = "";

            strSQL += "select EntCod, EntNome, EntBair, EntCpfCgc, Cidnome ,UsuCod, reg,(case when  DiasSemCompra  > 90 then '<font color=''#ff0000;''>' + cast(DiasSemCompra as varchar(max)) + '</>' Else cast(DiasSemCompra as varchar(max)) END ) as DiasSemCompra from ";
            strSQL += " (select ve.UsuCod,E.EntCod, E.EntNome, E.EntBair, E.EntCpfCgc, C.Cidnome,  ROW_NUMBER() OVER(ORDER BY E.EntCod) as reg,DATEDIFF(day, DataCompra,GETDATE())  as DiasSemCompra   from Entidade E ";
            strSQL += " INNER JOIN CIDADE C ON C.CidCod=E.CidCod ";
            strSQL += " INNER JOIN VEND_ENT vend ON E.EntCod=vend.EntCod ";
            strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
            strSQL += " LEFT outer JOIN USER_ULTIMA_COMPRA ped WITH(NOLOCK)  ON  ped.EntCod = e.EntCod "; 
            strSQL += " WHERE E.EntNome like'" + valorConsulta + "%' and E.EntStatDescr = 'Ativo' and ve.UsuCod='" + Session["usuario"].ToString() + "') a WHERE reg between " + indexPage + " and " + fimPage + ";";

            return strSQL;
        }

        public string geraConsultaEntidade4(int indexPage, int fimPage, string valorConsulta)
        {
            string strSQL = "";

            strSQL += "select EntCod, EntNome, EntBair, EntCpfCgc, Cidnome ,UsuCod, reg,(case when  DiasSemCompra  > 90 then '<font color=''#ff0000;''>' + cast(DiasSemCompra as varchar(max)) + '</>' Else cast(DiasSemCompra as varchar(max)) END ) as DiasSemCompra from ";
            strSQL += " (select ve.UsuCod,E.EntCod, E.EntNome, E.EntBair, E.EntCpfCgc, C.Cidnome,  ROW_NUMBER() OVER(ORDER BY E.EntCod) as reg, DATEDIFF(day, DataCompra,GETDATE())  as DiasSemCompra   from Entidade E ";
            strSQL += " INNER JOIN CIDADE C ON C.CidCod=E.CidCod ";
            strSQL += " left join VEND_ENT vend ON vend.EntCod = E.EntCod ";
            strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
            strSQL += " LEFT outer JOIN USER_ULTIMA_COMPRA ped WITH(NOLOCK)  ON  ped.EntCod = e.EntCod "; 
            strSQL += " WHERE E.EntNome like'" + valorConsulta + "%' and E.EntStatDescr = 'Ativo') a WHERE reg between " + indexPage + " and " + fimPage + ";";

            return strSQL;
        }

        public string geraConsultaEntidade5(int indexPage, int fimPage, string valorConsulta)
        {
            string strSQL = "";

            strSQL += "select EntCod, EntNome, EntBair, EntCpfCgc, Cidnome ,UsuCod, reg,(case when  DiasSemCompra  > 90 then '<font color=''#ff0000;''>' + cast(DiasSemCompra as varchar(max)) + '</>' Else cast(DiasSemCompra as varchar(max)) END ) as DiasSemCompra from ";
            strSQL += " (select ve.UsuCod,E.EntCod, E.EntNome, E.EntBair, E.EntCpfCgc, C.Cidnome,  ROW_NUMBER() OVER(ORDER BY E.EntCod) as reg, DATEDIFF(day, DataCompra,GETDATE())  as DiasSemCompra   from Entidade E ";
            strSQL += " INNER JOIN CIDADE C ON C.CidCod=E.CidCod ";
            strSQL += " INNER join VEND_ENT vend ON vend.EntCod = E.EntCod ";
            strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
            strSQL += " LEFT outer JOIN USER_ULTIMA_COMPRA ped WITH(NOLOCK)  ON  ped.EntCod = e.EntCod "; 
            strSQL += " WHERE E.EntCod like'" + valorConsulta + "%' and E.EntStatDescr = 'Ativo' and ve.UsuCod='" + Session["usuario"].ToString() + "') a WHERE reg between " + indexPage + " and " + fimPage + ";";
            
            return strSQL;
        }

        public string geraConsultaEntidade6(int indexPage, int fimPage, string valorConsulta)
        {
            string strSQL = "";

            strSQL += "select EntCod, EntNome, EntBair, EntCpfCgc, Cidnome ,UsuCod, reg,(case when  DiasSemCompra  > 90 then '<font color=''#ff0000;''>' + cast(DiasSemCompra as varchar(max)) + '</>' Else cast(DiasSemCompra as varchar(max)) END ) as DiasSemCompra from ";
            strSQL += " (select ve.UsuCod,E.EntCod, E.EntNome, E.EntBair, E.EntCpfCgc, C.Cidnome,  ROW_NUMBER() OVER(ORDER BY E.EntCod) as reg, DATEDIFF(day, DataCompra,GETDATE())  as DiasSemCompra   from Entidade E ";
            strSQL += " INNER JOIN CIDADE C ON C.CidCod=E.CidCod ";
            strSQL += " left join VEND_ENT vend ON vend.EntCod = E.EntCod ";
            strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
            strSQL += " LEFT outer JOIN USER_ULTIMA_COMPRA ped WITH(NOLOCK)  ON  ped.EntCod = e.EntCod "; 
            strSQL += " WHERE E.EntCod like'" + valorConsulta + "%' and E.EntStatDescr = 'Ativo') a WHERE reg between " + indexPage + " and " + fimPage + ";";
  
            return strSQL;
        }
    }
}