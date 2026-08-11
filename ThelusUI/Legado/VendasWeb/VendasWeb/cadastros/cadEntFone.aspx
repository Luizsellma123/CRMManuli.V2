<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="cadEntFone.aspx.cs" Inherits="VendasWeb.cadastros.cadEntFone" %>
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
    </div>

    <!-- Dados Inclusão/ALteração telefones -->
    <div id="detDados">
        <table border="0">
            <tr>
                <td><asp:Label ID="lblTipo" runat="server" Text="Tipo:" CssClass="texto"></asp:Label></td>
                <td><asp:DropDownList ID="drpTipo" runat="server" CssClass="campo">
                    <asp:ListItem Value="Bip">BIP</asp:ListItem>
                    <asp:ListItem Value="Celular">CELULAR</asp:ListItem>
                    <asp:ListItem Selected="True" Value="Comercial">COMERCIAL</asp:ListItem>
                    <asp:ListItem Value="Fax">FAX</asp:ListItem>
                    <asp:ListItem Value="Telex">TELEX</asp:ListItem>
                </asp:DropDownList><br /></td>                
            </tr>
            <tr>
                <td><asp:Label ID="lblDDI" runat="server" Text="Código do DDI:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtDDI" runat="server" CssClass="campo"></asp:TextBox></td>
                <td><asp:Label ID="lblDDD" runat="server" Text="Código do DDD:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtDDD" runat="server" CssClass="campo"></asp:TextBox></td>
                <td><asp:Label ID="lblNumero" runat="server" Text="Número:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtNumero" runat="server" CssClass="campo"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblRamal" runat="server" Text="Número Ramal:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtRamal" runat="server" CssClass="campo"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblPrincipal" runat="server" Text="Principal:" CssClass="texto"></asp:Label></td>
                <td><asp:DropDownList ID="drpPrincipal" runat="server" CssClass="campo">
                <asp:ListItem Value="Sim">SIM</asp:ListItem>
                <asp:ListItem Value="Não">NÃO</asp:ListItem>
                </asp:DropDownList></td>                
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="Botoes" 
            onclick="btnSalvar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Botoes" 
            onclick="btnCancelar_Click1" />
    </div>
    <asp:TextBox ID="txtIDEntidade" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
