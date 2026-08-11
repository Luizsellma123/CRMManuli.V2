<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="cadPedidoArte.aspx.cs" Inherits="VendasWeb.cadastros.cadPedidoArte" %>
<%@ Register src="../usercontrol/cabecarioPedido.ascx" tagname="cabecarioPedido" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <script type="text/javascript" language ="javascript">

        function abrirArte(codProd) {
            window.open("../cadastros/imagemCliches2.aspx?codProd=" + codProd, "Pagina", "status=no, width=800, height=400");
            return false;
        }
    </script> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:cabecarioPedido ID="cabecarioPedido1" runat="server" />
    
    <!--Filtro Nome/Codigo -->
            <div id="cabItens">
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
            
            <!-- Textbox para paginação -->
            <div id="lstPaginacao">
                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
            </div>
    
</asp:Content>
