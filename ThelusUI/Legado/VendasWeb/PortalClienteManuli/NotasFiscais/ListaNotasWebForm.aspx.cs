using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using DanfeSharp;
using DanfeSharp.Model;
using System.IO;

namespace VendasWeb.PortalClienteManuli.NotasFiscais
{
    public partial class ListaNotasWebForm : System.Web.UI.Page
    {
        UsuarioPortalClass OBJusuario = new UsuarioPortalClass();
        UtilClass ObjUtilClass = new UtilClass();
        enviarEmailClass OBJEmail = new enviarEmailClass();
        PedidoClass PedidoClass = new GerencialVendas.PedidoClass();
        PortalClass OBJPortal = new PortalClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se tem usuário logado no Portal
            if (Session["usuarioPortal"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("LoginPortal.aspx");

            }

            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            if (!IsPostBack)
            {
                //Chama função para carregar dados na tela
                carregaDadosTela();
            }
        }

        public void carregaDadosTela()
        {
            //Recupera usuario da sessão
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            //Carrega Empresa
            EmpresaDropDownList.DataSource = OBJusuario.Empresas_Usuario();
            EmpresaDropDownList.DataTextField = "EmpNome";
            EmpresaDropDownList.DataValueField = "EmpCod";
            EmpresaDropDownList.DataBind();

            //Carrega Razão Social Cliente
            RazaoSocialDropDownList.DataSource = OBJusuario.Entidades_Usuario();
            RazaoSocialDropDownList.DataTextField = "EntNome";
            RazaoSocialDropDownList.DataValueField = "EntCod";
            RazaoSocialDropDownList.DataBind();

            //Carrega pedidos na tela
            carregaNotas();
        }

        public void carregaNotas()
        {
            DataTable RetornoDados = new DataTable();

            //Recupera usuario da sessão
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            //recupera notas entidade
            OBJusuario.EmpCod = EmpresaDropDownList.SelectedValue.ToString();
            OBJusuario.NFNum = NotaTextBox.Text.ToString();

            if (DataInicialTextBox.Text != "")
            {
                OBJusuario.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text);
            }
            else
            {
                OBJusuario.DataInicial = DateTime.Now.AddYears(-1);
            }

            if (DataFinalTextBox.Text != "")
            {
                OBJusuario.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text);
            }
            else
            {
                OBJusuario.DataFinal = DateTime.Now;
            }

            RetornoDados = OBJusuario.Notas_Entidade();

            GridViewNotasClientes.DataSource = RetornoDados;
            GridViewNotasClientes.DataBind();

            //Registra PostBack do gridView
            RegisterPostBackControl();
        }

        protected void GridViewNotasClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewNotasClientes.PageIndex = e.NewPageIndex;
            carregaNotas();
        }

        protected void LinkButtonConsulta_Click(object sender, EventArgs e)
        {
            PedidoClass = new PedidoClass();
            PedidoClass.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            PedidoClass.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            PedidoClass.Consulta_Pedido();
            PedidoClass.NfNum = ((Label)((Control)sender).FindControl("NFnumLabel")).Text;
            Session["PedidoClass"] = PedidoClass;

            Response.Redirect("~/PortalClienteManuli/NotasFiscais/DetalheNotasWebForm.aspx");
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            carregaNotas();
        }

        protected void LinkButtonSolicita_Click(object sender, EventArgs e)
        {
            /*
            string nota = "";
            string empresa = "";
            string cliente = "";

            //Carrega Email
            OBJPortal.NomeContato = "Notas";
            OBJPortal.ContatoSetorPortal();

            try
            {
                nota = ((Label)((Control)sender).FindControl("NFnumLabel")).Text;
                empresa = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text + " - " + EmpresaDropDownList.SelectedItem.Text.ToString();
                cliente = RazaoSocialDropDownList.SelectedValue.ToString() + " - " + RazaoSocialDropDownList.SelectedItem.Text;

                OBJEmail.SolicitacaoNotaEmail("Solicitação Nota Fiscal", nota, empresa, cliente, OBJPortal.EmailSetor);

                string FaltaValores = "Solicitação encaminhada com sucesso !";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            catch
            {
                string FaltaValores = "Ocorreu um problema na solicitação !";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }

            */

            OBJPortal.Empresa = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            OBJPortal.NotaFiscal = ((Label)((Control)sender).FindControl("NFnumLabel")).Text;


            OBJPortal.BuscaXMLNota();

            //string arquivo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\"><NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\"><infNFe Id=\"NFe13190114269557000137550010000098111278691862\" versao=\"4.00\"><ide><cUF>13</cUF><cNF>27869186</cNF><natOp>Compra para industrializacao</natOp><mod>55</mod><serie>1</serie><nNF>9811</nNF><dhEmi>2019-01-02T14:07:00-02:00</dhEmi><dhSaiEnt>2019-01-02T14:07:00-02:00</dhSaiEnt><tpNF>0</tpNF><idDest>3</idDest><cMunFG>1302603</cMunFG><tpImp>1</tpImp><tpEmis>1</tpEmis><cDV>2</cDV><tpAmb>1</tpAmb><finNFe>1</finNFe><indFinal>0</indFinal><indPres>1</indPres><procEmi>0</procEmi><verProc>R161.106 17/12/2018</verProc></ide><emit><CNPJ>14269557000137</CNPJ><xNome>MANULI DA AMAZONIA IND DE EMBALAGENS LTD</xNome><xFant>MANULI AM 06300</xFant><enderEmit><xLgr>Av BURITI</xLgr><nro>3670</nro><xCpl>Distrito Industrial</xCpl><xBairro>DISTRITO INDUSTRIAL</xBairro><cMun>1302603</cMun><xMun>MANAUS</xMun><UF>AM</UF><CEP>69075000</CEP><cPais>1058</cPais><xPais>BRASIL</xPais><fone>9230263399</fone></enderEmit><IE>063007460</IE><CRT>3</CRT></emit><dest><idEstrangeiro>886-2-2827-7996</idEstrangeiro><xNome>ALPHA BETA GLOBAL TAPES AND ADHESIVES CO., LTD.</xNome><enderDest><xLgr>WEN LIN NORTH ROAD</xLgr><nro>216</nro><xBairro>INDUSTRIAL ZONE</xBairro><cMun>9999999</cMun><xMun>EXTERIOR</xMun><UF>EX</UF><cPais>1619</cPais><xPais>TAIWAN</xPais><fone>886228277996</fone></enderDest><indIEDest>9</indIEDest><email>comex@manulifitasa.com.br</email></dest><det nItem=\"1\"><prod><cProd>20.10.20.61.0001</cProd><cEAN>SEM GTIN</cEAN><xProd>JUMBO FITA ADESIVA ACRILICA (TR) 38 MICRAS</xProd><NCM>39199090</NCM><CFOP>3101</CFOP><uCom>M2</uCom><qCom>599355.0000</qCom><vUnCom>0.2742940000</vUnCom><vProd>164399.48</vProd><cEANTrib>SEM GTIN</cEANTrib><uTrib>M2</uTrib><qTrib>599355.0000</qTrib><vUnTrib>0.2742940000</vUnTrib><vFrete>7533.44</vFrete><vSeg>189.11</vSeg><vOutro>1728.77</vOutro><indTot>1</indTot><DI><nDI>1822237809</nDI><dDI>2018-12-04</dDI><xLocDesemb>MANAUS</xLocDesemb><UFDesemb>AM</UFDesemb><dDesemb>2018-12-04</dDesemb><tpViaTransp>7</tpViaTransp><tpIntermedio>1</tpIntermedio><UFTerceiro>AM</UFTerceiro><cExportador>0022283</cExportador><adi><nAdicao>1</nAdicao><nSeqAdic>1</nSeqAdic><cFabricante>ALPHA BETA GLOBAL TAPES AND ADHESIVES CO., LTD.</cFabricante></adi></DI></prod><imposto><ICMS><ICMS51><orig>1</orig><CST>51</CST></ICMS51></ICMS><IPI><cEnq>999</cEnq><IPINT><CST>05</CST></IPINT></IPI><II><vBC>0.00</vBC><vDespAdu>1728.77</vDespAdu><vII>0.00</vII><vIOF>0</vIOF></II><PIS><PISOutr><CST>71</CST><vBC>0.00</vBC><pPIS>0.0000</pPIS><vPIS>0.00</vPIS></PISOutr></PIS><COFINS><COFINSOutr><CST>71</CST><vBC>0.00</vBC><pCOFINS>0.0000</pCOFINS><vCOFINS>0.00</vCOFINS></COFINSOutr></COFINS></imposto></det><total><ICMSTot><vBC>0.00</vBC><vICMS>0.00</vICMS><vICMSDeson>0.00</vICMSDeson><vFCP>0.00</vFCP><vBCST>0.00</vBCST><vST>0.00</vST><vFCPST>0.00</vFCPST><vFCPSTRet>0.00</vFCPSTRet><vProd>164399.48</vProd><vFrete>7533.44</vFrete><vSeg>189.11</vSeg><vDesc>0.00</vDesc><vII>0.00</vII><vIPI>0.00</vIPI><vIPIDevol>0.00</vIPIDevol><vPIS>0.00</vPIS><vCOFINS>0.00</vCOFINS><vOutro>1728.77</vOutro><vNF>173850.80</vNF></ICMSTot></total><transp><modFrete>0</modFrete><transporta><CNPJ>07374179000196</CNPJ><xNome>OLIVA PINTO LOGISTICA LTDA.</xNome><xEnder>R JAVARI 1165  DISTRITO INDUSTRIAL I 69075110</xEnder><xMun>MANAUS</xMun><UF>AM</UF></transporta><vol><qVol>48</qVol><esp>VOL</esp><pesoL>22536.000</pesoL><pesoB>22776.000</pesoB></vol></transp><pag><detPag><tPag>99</tPag><vPag>173850.80</vPag></detPag></pag><infAdic><infCpl>NF ENTRADA REF ALPHA BETA FATURA GL-1890183 PO#4606 USD 42.554,21 TX 3,8633 DI 18/2223780-9 - DESPESAS ACESSORIAS - SISCOMEX R$ 214,50; II R$27.781,83; IPI R$30.212,74; PIS R$3.646,36; COFINS R$16.755,91; THC R$1.514,27</infCpl></infAdic></infNFe><Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" /><Reference URI=\"#NFe13190114269557000137550010000098111278691862\"><Transforms><Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" /><Transform Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><DigestValue>G37Vbfp4Os4VoCjTxtKz1ippKWY=</DigestValue></Reference></SignedInfo><SignatureValue>P63q0DFDHr33URtSUf6TrLsAofljMeILo0m3OeB42a5ouUVcnXcAeSprzrCPbXcZ3FNEMSZ3A8arF+5DAXt968V4AmcMT4pEEYxBA7hRs1bGwa+h75Rye9e/239CBlmXPYWtC8Pa+IiT6chFT7jL8GqBosdygxWDVHbxcv/L9tA5OV/HmR9OaEEjIeBiqdKLVhsPWVXgLrBrJiRye72SNtWkzTh75R8EL6Y2TU/D6TRadzaugerat1W3XcO8a6uz6/aCsVY2nsFEvKzImRAwuqtEycvAyuF55AZsSwLe1psyWhvXNezBOsyH+XGr7uz6rErNVcqWEBjfZSsq4iyqlg==</SignatureValue><KeyInfo><X509Data><X509Certificate>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</X509Certificate></X509Data></KeyInfo></Signature></NFe><protNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\"><infProt Id=\"Id113191094007798\"><tpAmb>1</tpAmb><verAplic>AM4.00</verAplic><chNFe>13190114269557000137550010000098111278691862</chNFe><dhRecbto>2019-01-02T12:27:45-04:00</dhRecbto><nProt>113191094007798</nProt><digVal>G37Vbfp4Os4VoCjTxtKz1ippKWY=</digVal><cStat>100</cStat><xMotivo>Autorizado o uso da NF-e</xMotivo></infProt></protNFe></nfeProc>";

            try
            {
                DanfeViewModel model = DanfeViewModel.CreateFromXmlString(OBJPortal.XMLNotaFiscal);
                using (DanfeDocumento danfe = new DanfeDocumento(model))
                {
                    danfe.Gerar();

                    MemoryStream ms = new MemoryStream();

                    danfe.Salvar(ms);
                    ms.Flush();

                    byte[] bytes = new byte[ms.Length];
                    bytes = ms.ToArray();

                    Response.AddHeader("Content-disposition", "attachment; filename=NotaFiscal.pdf"); //Informa o nome do arquivo.extensão

                    //Response.AddHeader("Content-disposition", "inline; filename=Boleto.pdf"); //Informa o nome do arquivo.extensão
                    Response.ContentType = "application/pdf"; //Informa o Mime Type do Arquivo
                    Response.BinaryWrite(bytes);

                }
            }
            catch
            {
                //Response.Write("<script>alert('Nota fiscal ainda não está na base nacional, favor aguardar.')</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Nota fiscal ainda não está na base nacional, favor aguardar.');", true);
            }
        }

        public void RegisterPostBackControl()
        {
            foreach (GridViewRow row in GridViewNotasClientes.Rows)
            {
                LinkButton lnkFull = row.FindControl("LinkButtonSolicita") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
            }
        }
    }
}