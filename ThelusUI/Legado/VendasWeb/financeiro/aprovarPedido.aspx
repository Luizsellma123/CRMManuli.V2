<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="aprovarPedido.aspx.cs" Inherits="VendasWeb.financeiro.aprovarPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    
    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script language="javascript" src="../js/jsAprovarPedido.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 37px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecario Entidade -->
    <div id="entCabecario" class="detCabeccario">
        <asp:Label ID="lblEmpresa" runat="server" Text="EMPRESA:" CssClass="texto" ></asp:Label>
        <asp:Label ID="lblDescEmpresa"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Literal ID="ltlNumPedido" runat="server"></asp:Literal>
        <!--<asp:Label ID="lblNumPedido" runat="server" Text="Número:" CssClass="texto" ></asp:Label>
        <asp:Label ID="lblDescNumPedido"  runat="server" Text="" CssClass="texto"></asp:Label><br /> -->
        <asp:Label ID="lblnome" runat="server" Text="NOME:" CssClass="texto" ></asp:Label> 
        <asp:Label ID="lblDescNome"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Label ID="entidade"  runat="server" Text="Codigo Entidade: " CssClass="texto"></asp:Label>
        <asp:Label ID="txtIDEntidade"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Label ID="lblFantasia" runat="server" Text="FANTASIA:" CssClass="texto"></asp:Label>
        <asp:Label ID="lblDescFantasia"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Label ID="lblCnpj" runat="server" Text="CNPJ/CPF:" CssClass="texto"></asp:Label>
        <asp:Label ID="lblDescCnpj"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <br />
        <asp:Label ID="lblLimiteCredito"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Label ID="lblCadastro"  runat="server" Text="" CssClass="texto"></asp:Label>
        


        <div id="btnentidade" class="btnDireita">
            <asp:Button ID="btnAlteraEntidade" runat="server" Text="Seleciona Pedido" 
                CssClass="Botoes" onclick="btnAlteraEntidade_Click" />
        </div>
    </div>

    <div id="pedDados" class="detCorpo">
         <table>
                <tr>
                    <td><asp:Label ID="lblTipo" runat="server" Text="Tipo:" CssClass="texto"></asp:Label></td>
                    <td><asp:TextBox ID="txtTipo" runat="server" CssClass="campo" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="texto"></asp:Label></td>
                    <td><asp:TextBox ID="txtStatus" runat="server" CssClass="campo" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblDataEntrega" runat="server" Text="Data Saída:" CssClass="texto"></asp:Label></td>
                    <td><asp:TextBox ID="txtDataEntrega" runat="server" CssClass="campo" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:Label ID="lblDataEmissao" runat="server" Text="Data Emissão:" CssClass="texto"></asp:Label></td>
                    <td><asp:TextBox ID="txtDataEmissao" runat="server" CssClass="campo" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblNatureza" runat="server" Text="Natureza:" CssClass="texto"></asp:Label></td>
                    <td><asp:TextBox ID="txtNatureza" runat="server" CssClass="campo" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:Label ID="lblOperacao1" runat="server" Text="Operacao:" CssClass="texto"></asp:Label></td>
                    <td><asp:TextBox ID="txtOperacao" runat="server" CssClass="campo" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:Label ID="lblEspecie" runat="server" Text="Espécie:" CssClass="texto"></asp:Label></td>
                    <td><asp:TextBox ID="txtEspecie" runat="server" CssClass="campo" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblCondicao" runat="server" Text="Condição Pagamento:" CssClass="texto"></asp:Label></td>
                    <td colspan="2"><asp:TextBox ID="txtCondicao" runat="server" CssClass="campoExtended" ReadOnly="true"></asp:TextBox></td>
                </tr>


                
                 
               
         </table>
    </div>    

    
    <div id="lstItem" class="detCorpo">
        <table class="lstTabela">
            <tr class="tabLstCab">
                <td class="extendproduto"><asp:Label ID="lblProduto" runat="server" Text="Produto:"></asp:Label></td>
                <td class="style1"><asp:Label ID="lblUnidade" runat="server" Text="UND:"></asp:Label></td>
                <!-- <td><asp:Label ID="lblRevenda" runat="server" Text="Revenda:"></asp:Label></td> -->
                <td class="small"><asp:Label ID="lblQuantidade" runat="server" Text="Quantidade:"></asp:Label></td>
                <td class="grande"><asp:Label ID="lbltabela" runat="server" Text="Tabela:"></asp:Label></td>                
                <td class="small"><asp:Label ID="lblValorUnitario" runat="server" Text="Valor:"></asp:Label></td>
                <td><asp:Label ID="lblTotal" runat="server" Text="Total:"></asp:Label></td>
                <td><asp:Label ID="lblComposicao" runat="server" Text="Estq.:"></asp:Label></td>
            </tr>

            <!-- Items carregados dinamicamente -->
            <asp:Literal ID="ltlItems" runat="server"></asp:Literal>
        </table>
    </div>

    <div id="dadComplementares">
        <div id="dadHist" class="dadComple">
            <!-- Dados Histrico -->
            <table class="lstTabela"><tr class="tabLstCab"><td colspan="5" align="center">Histórico Financeiro</td></tr>
                <!-- <td align="center"><a href="#" class="imgeditent"><img src="../imagens/adiciona.png" alt="Alteração" border="0" /></a></td></tr> -->
                <tr ><td style=" width:480px; height:100px;" colspan="5">
               <!-- <asp:TextBox ID="txtHistoricoFinanceiro" runat="server" class="campo" TextMode="MultiLine" Width="480px" Height="90px"></asp:TextBox>-->
                <div  class="CampoOverflow">
                <asp:Label ID="lblFinanceiro" runat="server" Text=""></asp:Label>
                </div>
               </td></tr>
                
            </table>
        </div>

        <table class="lstTabela"><tr class="tabLstCab"><td colspan="5" align="center">
            Histórico do Pedido:</td></tr>
            <!-- <td align="center"><a href="#" class="imgeditent"><img src="../imagens/adiciona.png" alt="Alteração" border="0" /></a></td></tr> -->
            <tr><td colspan="5">
                <asp:TextBox ID="txtHistorico"  runat="server" class="campo" 
                    TextMode="MultiLine" Width="402px" Height="95px" ReadOnly="true"></asp:TextBox></td></tr>
        </table>
    </div>

    
    <br />
    <div id="Div1" class="dadComple">
            <!-- Dados Histrico -->
            <table class="lstTabela"><tr class="tabLstCab"><td colspan="5" align="center">Novo Histórico</td></tr>
                <!-- <td align="center"><a href="#" class="imgeditent"><img src="../imagens/adiciona.png" alt="Alteração" border="0" /></a></td></tr> -->
                <tr><td colspan="5"><asp:TextBox ID="txtNovoHistorico" runat="server" class="campo" TextMode="MultiLine" Width="480px" Height="90px"></asp:TextBox></td></tr>

                
                
            </table>
            <asp:Button ID="btnSalvarHistoricoNovo" runat="server" Text="Salvar Novo Historico" CssClass="Botoes" onclick="btnSalvarNovoHistorico_Click" />
        </div>

    <br /><br /><br /><br /><br /><br />
    <!-- Botões para navegação --> 
    <div id="botomPed">
        
        
        
       <asp:Button ID="btnAprovar" runat="server" Text="Aprovar" 
            CssClass="Botoes" onclick="btnAprovar_Click" />
       <asp:Button ID="btnOrcamento" runat="server" Text="Orçamento" 
            CssClass="Botoes" onclick="btnOrcamento_Click" />
      <asp:Button ID="btnFaturar" runat="server" Text="Faturar" 
            CssClass="Botoes" onclick="btnFaturar_Click" />
         
        <asp:Button ID="btnCancelar" runat="server" Text="Voltar" CssClass="Botoes" 
            onclick="btnCancelar_Click" />
        
    </div>

    <div id="dadosaUxiliares">
        <input name="idItem" id="idItem" type="hidden" value="" />
    </div>

</asp:Content>
