<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="cadUsuario.aspx.cs" Inherits="VendasWeb.cadastros.cadUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/login.css?aux=2" />
    <script language="javascript" src="../js/cadUsuario.js?aux=1" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="tabelaLogin">
        <tr>
            <td><asp:Label ID="lblLogin" runat="server" CssClass="texto">Usuário :</asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtUsuario" runat="server" CssClass="campo"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblSenha" runat="server" CssClass="texto" >Senha :</asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtSenha" runat="server" CssClass="campo" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblEmail" runat="server" CssClass="texto">Email :</asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtEmail" runat="server" CssClass="campo"></asp:TextBox></td>
        </tr>
        <tr>
            <td><br /><asp:Label ID="lblError" runat="server" Text="lblError" Visible="false" CssClass="textoErro">
                Usuário ou senha incorretos favor verificar.
            </asp:Label></td>
        </tr>
        <tr>
            <td><asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="Botoes" 
                    onclick="btnSalvar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Botoes" 
                    onclick="btnCancelar_Click" /></td>
        </tr>
    </table>
</asp:Content>
