<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="lstManuais.aspx.cs" Inherits="VendasWeb.listas.lstManuais" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="../js/lstManuais.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <a href="#" onclick="abreManualLogin();" class="texto">Manual Login</a>
           </td>
        </tr>
        <tr><td><br /></td></tr>
        <tr>
           <td>
                <a href="#" onclick="ManualConsultaInclusaoPedido();" class="texto">Manual Inclusão Pedido</a>
           </td>
        </tr>
        <tr><td><br /></td></tr>
        <tr>
           <td>
                <a href="#" onclick="ManualConsultaPedido();" class="texto">Manual Consultar Pedido</a>
           </td>
        </tr>
    </table>
</asp:Content>
