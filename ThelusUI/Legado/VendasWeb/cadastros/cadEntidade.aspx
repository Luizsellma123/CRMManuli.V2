<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="cadEntidade.aspx.cs" Inherits="VendasWeb.cadastros.cadEntidade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecario Entidade -->
    <div id="entCabecario" class="detCabeccario">
        <asp:Label ID="lblnome" runat="server" Text="NOME:" CssClass="texto" ></asp:Label>
        <asp:Label ID="lblDescNome"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Label ID="lblFantasia" runat="server" Text="FANTASIA:" CssClass="texto"></asp:Label>
        <asp:Label ID="lblDescFantasia"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Label ID="lblCnpj" runat="server" Text="CNPJ/CPF:" CssClass="texto"></asp:Label>
        <asp:Label ID="lblDescCnpj"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:TextBox ID="txtIDEntidade" runat="server" Visible="false"></asp:TextBox>
    </div>

    <!-- dados telefones -->
    <div id="lstContatoEntidade">
        <asp:Literal ID="ltlContatoEntidade" runat="server"></asp:Literal>     
    </div>

    <!-- Dados Email -->
    <div id="lstWebEntidade">
        <asp:Literal ID="ltlWebEntidade" runat="server"></asp:Literal>    
    </div>

    <!-- Dados Histrico -->
    <div id="lstHistorico">
        <table class="lstTabela"><tr class="tabLstCab"><td colspan="4" align="center">Dados Histórico</td>
            <td align="center"><a href="#" class="imgeditent"><img src="../imagens/adiciona.png" alt="Alteração" border="0" /></a></td></tr>
            <tr><td colspan="5"><asp:TextBox ID="txtHistorico" runat="server" class="campo" TextMode="MultiLine" Width="530px" Height="100px" ReadOnly="true"></asp:TextBox></td></tr>
        </table><br />
    </div>

    <!-- Dados Observacao -->
    <div id="lstTextoLivre">
        <table class="lstTabela"><tr class="tabLstCab"><td colspan="4" align="center">Texto Livre</td>
            <td align="center"><a href="#" class="imgeditent"><img src="../imagens/adiciona.png" alt="Alteração" border="0" /></a></td></tr>
            <tr><td colspan="5"><asp:TextBox ID="txtTextoLivre" runat="server" class="campo" TextMode="MultiLine" Width="530px" Height="100px" ReadOnly="true"></asp:TextBox></td></tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Botoes" 
        onclick="btnCancelar_Click" />
</asp:Content>
