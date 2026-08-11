<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="cadPedidoListaArte.aspx.cs" Inherits="VendasWeb.cadastros.cadPedidoListaArte" %>
<%@ Register src="../usercontrol/cabecarioPedido.ascx" tagname="cabecarioPedido" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />

    <script language="javascript" src="../js/cadArtePedido.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<uc1:cabecarioPedido ID="cabecarioPedido1" runat="server" />

<div id="entCabecario" class="detCabeccarioBottom">
    <asp:Label ID="lblCabProduto" runat="server" Text="Produto :" CssClass="texto"></asp:Label>
    <asp:Label ID="lblCabDescProduto" runat="server" Text="" CssClass="texto"></asp:Label>
</div>

<div id="lstItem" class="detCorpo">
        <table class="lstTabela">
            <tr class="tabLstCab">
                <td align="center">
                    <asp:Button ID="btnIncluir" runat="server" Text="" CssClass="btAdiciona" 
                        onclick="btnIncluir_Click" /></td>
                <td class="extend"><asp:Label ID="lblProduto" runat="server" Text="Produto:"></asp:Label></td>
                <td class="small"><asp:Label ID="lblUnidade" runat="server" Text="UND:"></asp:Label></td>
                <!-- <td><asp:Label ID="lblRevenda" runat="server" Text="Revenda:"></asp:Label></td>
                <td class="small"><asp:Label ID="lblQuantidade" runat="server" Text="Quantidade:"></asp:Label></td>
                <td><asp:Label ID="lbltabela" runat="server" Text="Tabela:"></asp:Label></td>                
                <td class="small"><asp:Label ID="lblValorUnitario" runat="server" Text="Valor:"></asp:Label></td>-->
                <td><asp:Label ID="lblTotal" runat="server" Text="Salvar"></asp:Label></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label ID="lblDescProduto" runat="server" Text="" CssClass="texto"></asp:Label></td>
                <td class="small"><asp:Label ID="lblDescUnidade" runat="server" Text="" CssClass="texto"></asp:Label></td>
                <!--<td>
                 <asp:DropDownList ID="drpRevenda" runat="server" CssClass="campo">
                    <asp:ListItem Selected="True" Value="0">Não</asp:ListItem>
                    <asp:ListItem Value="1">Sim</asp:ListItem>
                </asp:DropDownList></td>
                <td class="small"><asp:TextBox ID="txtQuantidade" runat="server" CssClass="campoSmall"></asp:TextBox></td>
                <td><asp:DropDownList ID="drpTabela" runat="server" CssClass="campo">
                </asp:DropDownList></td> 
                <td class="small"><asp:TextBox ID="txtValor" runat="server" CssClass="campoSmall"></asp:TextBox></td>-->
                <td align="center"><a href="#"><span>
                    <asp:Button ID="btnSalvar" runat="server" Text="" 
                        CssClass="btnSalvar" onclick="btnSalvar_Click"/></span></a></td>
            </tr>
            
            <!-- Items carregados dinamicamente -->
            <asp:Literal ID="ltlItems" runat="server"></asp:Literal>
        </table>
        
    </div>

    <div id="dadosaUxiliares">
    <input name="idItem" id="idItem" type="hidden" value="" />
    <asp:Label ID="lblProdutoAux" runat="server" Text="Label" Visible="false"></asp:Label>

    </div>

</asp:Content>
