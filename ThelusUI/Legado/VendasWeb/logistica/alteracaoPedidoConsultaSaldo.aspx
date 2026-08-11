<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="alteracaoPedidoConsultaSaldo.aspx.cs" Inherits="VendasWeb.logistica.alteracaoPedidoConsultaSaldo" %>
<%@ Register src="../usercontrol/cabecarioLogistica.ascx" tagname="cabecarioLogistica" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:cabecarioLogistica ID="cabecarioLogistica1" runat="server" />

    <!-- Tabela montada dinamicamente -->
    <div id="lst_dad">
        <asp:Literal ID="ltlListaProdutos" runat="server"></asp:Literal>
    </div>

</asp:Content>
