<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="lstConsultaPreco.aspx.cs" Inherits="VendasWeb.listas.lstConsultaPreco" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" type="text/css" href="../css/listas.css?aux=7" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <!-- Cabecario -->
    <div id="CabProdutos" class="lstCabecario">

        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:" CssClass="texto"></asp:Label>
            <asp:DropDownList ID="drpEmpresa" runat="server" CssClass="campo">
        </asp:DropDownList>

        <!--Filtro Nome/Codigo -->
        <asp:DropDownList ID="drpProdutos" runat="server" CssClass="campo">
            <asp:ListItem Value="1" Selected="True">Nome</asp:ListItem>
            <asp:ListItem Value="2">Código Estruturado</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtFiltroProd" runat="server" CssClass="campo"></asp:TextBox>
        <asp:Button ID="btnListar" runat="server" Text="listar" CssClass="Botoes" 
            onclick="btnListar_Click" />
    </div>

    <!-- Tabela montada dinamicamente -->
    <div id="lst_dad">
        <asp:Literal ID="ltlListaProdutos" runat="server"></asp:Literal>
    </div>

    <!-- Botões para navegação --> 
    <div id="botomnav">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btnAnt" 
            onclick="LinkButton1_Click"><img src="../imagens/back.png" alt="<< Anterior" border="0" /></asp:LinkButton>
        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btnProx" 
            onclick="LinkButton2_Click"><img src="../imagens/next.png" alt="Próximo >>" border="0" /></asp:LinkButton>        
    </div>
    
    <!-- TextBox utilizados para trabalhar a paginação --> 
    <div>
        <span ><asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox></span>
        <span ><asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox></span>
        <span ><asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox></span>
    </div> 
</asp:Content>
