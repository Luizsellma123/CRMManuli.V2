<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="lstPedidosLogistica.aspx.cs" Inherits="VendasWeb.listas.lstPedidosLogistica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="lstPedCabecario">
        <!--Filtro Empresa -->
        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:" CssClass="texto"></asp:Label>
        <asp:DropDownList ID="drpEmpresa" runat="server" CssClass="campo">
        </asp:DropDownList>      
        
        <!--Filtro Nome/Numero -->
        <asp:DropDownList ID="drpListFiltroPri" runat="server" CssClass="campo">
            <asp:ListItem Value="1" Selected="True">Nome</asp:ListItem>
            <asp:ListItem Value="2" >Número</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtFiltro" runat="server" CssClass="campo"></asp:TextBox>
        
        <!--Filtro Status-->
        <asp:Label ID="lblStatus" runat="server" Text="Label" CssClass="texto">Status:</asp:Label>
        <asp:DropDownList ID="drpListFiltroStat" runat="server" CssClass="campo">
            <asp:ListItem Value="06">Expedição</asp:ListItem>
            <asp:ListItem Value="09">Produção</asp:ListItem>
            <asp:ListItem Selected="True" Value="00">Todos</asp:ListItem>
        </asp:DropDownList>

        <!--Filtro Tipo -->
        <asp:Label ID="lslTipo" runat="server" Text="Label" CssClass="texto">Tipo:</asp:Label>
        <asp:DropDownList ID="drpListFiltroTipo" runat="server" CssClass="campo">
            <asp:ListItem Value="1">Total</asp:ListItem>
            <asp:ListItem Value="2">Parcial</asp:ListItem>
            <asp:ListItem Value="3">Programado</asp:ListItem>
            <asp:ListItem Selected="True" Value="4">Todos</asp:ListItem>
        </asp:DropDownList>

        <!-- Botao Para Aplicar consulta -->
        <asp:Button ID="btnFiltro" runat="server" Text="listar" CssClass="Botoes" 
            onclick="btnFiltro_Click"  />
        
        <!-- Botao Inclusão Pedido -->
        <!-- <asp:Button ID="btnInclusao" runat="server" Text="Incluir" CssClass="Botoes"  /> -->
    </div>

    <div id="lstPedidos">
        <asp:Literal ID="ltlTabelaPedidos" runat="server"></asp:Literal>    
    </div>

    <!-- Botões para navegação --> 
    <div id="botomnav">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btnAnt" 
            onclick="LinkButton1_Click"><img src="../imagens/back.png" alt="<< Anterior" border="0" /></asp:LinkButton>
        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btnProx" 
            onclick="LinkButton2_Click"><img src="../imagens/next.png" alt="Próximo >>" border="0" /></asp:LinkButton>
    </div>

    <!-- Textbox para paginação -->
    <div id="lstPaginacao">
        <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
    </div>
</asp:Content>
