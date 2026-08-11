<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="lstLiberarPedido.aspx.cs" Inherits="VendasWeb.listas.LiberarPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/jquery.calendario.css?aux=6" />
    <link rel="stylesheet" type="text/css" href ="<%=Page.ResolveClientUrl("~/css/liberarPedido.css?aux=2")%>" />
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />

    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.calendario.js" type="text/javascript"></script>

    <script language="javascript" src="../js/lstLiberar.js" type="text/javascript"></script>
    <style type="text/css">
        .campo
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- Empresa -->
    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:" CssClass="texto"></asp:Label>
    <asp:DropDownList ID="drpEmpresa" runat="server" CssClass="campo" OnSelectedIndexChanged="drpEmpresa_SelectedIndexChanged">
    </asp:DropDownList>

    <!--Filtro Data -->
    <asp:Label ID="lblDataCancemento" runat="server" Text="Data:" CssClass="texto"></asp:Label>
    <asp:TextBox ID="txtDataCancelamento" runat="server" CssClass="campo"></asp:TextBox>
    <a href="#" id="btnCalendar1"><img src="../imagens/calendar.png" alt="Alteração" border="0"/></a>

    <!--Filtro Status -->
    <asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="texto"></asp:Label>
    <asp:DropDownList ID="drpStatus" runat="server" CssClass="campo">
        <asp:ListItem>Liberado</asp:ListItem>
        <asp:ListItem Selected="True">Pendente</asp:ListItem>
        <asp:ListItem>Cancelado</asp:ListItem>
    </asp:DropDownList>

     <!--Alçada -->
    <asp:Label ID="lblAlcada" runat="server" Text="Alcada:" CssClass="texto"></asp:Label>
    <asp:DropDownList ID="drpAlcada" runat="server"  CssClass="campo">
    </asp:DropDownList>


    <!--Filtro Pedido -->
    <asp:Label ID="lblPedido" runat="server" Text="Pedido:" CssClass="texto"></asp:Label>
    <asp:TextBox ID="txtPedido" runat="server" CssClass="campo"></asp:TextBox>

    <!-- Botao Listar -->
    <asp:Button ID="btnListar" runat="server" Text="Listar"  CssClass="Botoes" onclick="btnListar_Click" />
    <br />
    <br />
    
    <asp:GridView ID="LiberarPedidosGridview" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="4" 
        ForeColor="#333333" GridLines="None" 
        onpageindexchanging="LiberarPedidosGridview_PageIndexChanging" 
        EmptyDataText="Nenhum pedido encontrado!" PageSize="6" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Liberar">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="Liberar" runat="server" onclick="Liberar_Click" 
                        CssClass="Botao_Selecionar" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cancelar">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="cancelarButton" runat="server" CssClass="Botao_Cancelar" 
                        onclick="cancelarButton_Click"
                        OnClientClick="return confirmar();" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Selecionar">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="Editar" runat="server" onclick="PesquisarButton_Click" 
                        CssClass="Botao_Editar" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Pedido">
                <EditItemTemplate>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="PedVendaNumLabel" runat="server" Text='<%# Bind("PedVendaNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="PrevisaoEntrega" HeaderText="Previsão Faturamento" SortExpression="PrevisaoEntrega" />
            <asp:BoundField DataField="Ufsigla" HeaderText="Estado" SortExpression="Ufsigla" />
             <asp:TemplateField HeaderText="Alçada">
                <EditItemTemplate>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="AlcadaLabel" runat="server" Text='<%# Bind("Alcada") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="DataCancelamento" HeaderText="Data Aprovação" />

            <asp:BoundField DataField="OutrosDados" HeaderText="Outros Dados" />

            <asp:TemplateField HeaderText="Status">
                <EditItemTemplate>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="StatusLabel" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
    
    <br />
    <!--Filtro Motivo -->
    <asp:Label ID="lblMotivo" runat="server" Text="Motivo:" CssClass="texto"></asp:Label><br />
    <asp:TextBox ID="txtMotivo" runat="server" CssClass="campo" TextMode="MultiLine"  Height="156px" Width="901px"></asp:TextBox>
    
    <br />

    <asp:GridView ID="LiberaPedidoGridItensview" runat="server" 
        AutoGenerateColumns="False" CellPadding="4" 
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Prodcodestr" HeaderText="Produto" />
            <asp:BoundField DataField="ProdNome" HeaderText="Nome" />
            <asp:BoundField DataField="PrecoTabelaExicm" HeaderText="Valor Ex ICM" DataFormatString="{0:C2}" />
            <asp:BoundField DataField="ItPedVendaValUnit" HeaderText="Valor Unitário" DataFormatString="{0:C2}" />
            <asp:BoundField DataField="PrecoTabelaOriginal" HeaderText="Tabela C/ ICM" DataFormatString="{0:C2}" />
            <asp:BoundField DataField="Percentual" HeaderText="Desconto" DataFormatString="{0:P2}" />
            <asp:BoundField DataField="ItPedVendaQtd" HeaderText="Quantidade" DataFormatString="{0:N2}" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>

</asp:Content>
