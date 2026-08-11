<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="lstPedido.aspx.cs" Inherits="VendasWeb.listas.lstPedido" %>

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
            <asp:ListItem Value="1" Selected="True">Cód. Entidade</asp:ListItem>
            <asp:ListItem Value="2">Nome</asp:ListItem>
            <asp:ListItem Value="3">Número</asp:ListItem>
            <asp:ListItem Value="4">Nota Fiscal</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtFiltro" runat="server" CssClass="campo"></asp:TextBox>

        <!--Filtro Status-->
        <asp:Label ID="lblStatus" runat="server" Text="Label" CssClass="texto">Status:</asp:Label>
        <asp:DropDownList ID="drpListFiltroStat" runat="server" CssClass="campo">
        </asp:DropDownList>

        <!--Filtro Data Inicial-->
        <asp:Label ID="DataInicialLabel" runat="server" Text="Data Inicial: "></asp:Label>
        <asp:TextBox ID="DataInicialTextBox" TextMode="date" runat="server" CssClass="form-control" placeholder="Data inicial."></asp:TextBox>

        <!--Filtro Data Final-->
        <asp:Label ID="DataFinalLabel" runat="server" Text="Data Final: "></asp:Label>
        <asp:TextBox ID="DataFinalTextBox" TextMode="date" runat="server" CssClass="form-control" placeholder="Informe Número Esboço do SAP."></asp:TextBox>

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
            OnClick="btnFiltro_Click" />


        &nbsp;<asp:Button ID="VoltarButton" runat="server" Text="Voltar"
            CssClass="Botoes" Visible="false" OnClick="VoltarButton_Click" />
    </div>
    <br />
    <br />

    <asp:GridView ID="PedidosGridView" runat="server" Width="100%"
        EnableModelValidation="True" CellPadding="4" ForeColor="#333333"
        AllowPaging="True" DataKeyNames="PedVendaNum"
        OnPageIndexChanging="PedidosGridView_PageIndexChanging" PageSize="10"
        EmptyDataText="Nenhum pedido encontrado!"
        AutoGenerateColumns="False" CssClass="GridGis" Font-Size="Small"
        AllowSorting="True" OnSorting="PedidosGridView_Sorting">
        <AlternatingRowStyle BackColor="White" Font-Size="Small" />
        <Columns>
            <asp:TemplateField HeaderText="Consulta">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="ConsultaButton" runat="server" Width="25px" CssClass="btnEditar"
                        OnClick="ConsultaButton_Click" />
                </ItemTemplate>
                <ControlStyle Font-Size="Small" />
                <HeaderStyle Font-Size="Small" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Imprimir">
                <EditItemTemplate>
                    <asp:TextBox ID="ImprimirButton" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="ImprimirButton" runat="server" CssClass="btnImprimir" Width="25px"
                        OnClick="ImprimirButton_Click" />
                </ItemTemplate>
                <ControlStyle Font-Size="Small" />
                <HeaderStyle Font-Size="Small" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="ItemButton" runat="server" Width="25px" CssClass="btnSelect"
                        OnClick="ItemButton_Click" />
                </ItemTemplate>
                <ControlStyle Font-Size="Small" />
                <HeaderStyle Font-Size="Small" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Empresa" SortExpression="EmpCod">
                <ItemTemplate>
                    <asp:Label ID="EmpCodLabel" runat="server" Text='<%# Bind("EmpCod") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Font-Bold="False" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Pedido" SortExpression="PedVendaNum">
                <ItemTemplate>
                    <asp:Label ID="PedVendaNumLabel" runat="server" Text='<%# Bind("PedVendaNum") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Font-Bold="False" />
            </asp:TemplateField>
            <asp:BoundField DataField="PedVendaEntNomeDiv" HeaderText="Nome Cliente" SortExpression="PedVendaEntNomeDiv" />
            <asp:BoundField DataField="Nota" HeaderText="Nota Fiscal" SortExpression="Nota" />
            <asp:BoundField DataField="NFDATAEMIS" HeaderText="Faturamento" DataFormatString="{0:dd/MM/yyyy}" SortExpression="NFDATAEMIS" />
            <asp:BoundField DataField="PedVendaStatDescr" HeaderText="Status" SortExpression="PedVendaStatDescr" />
            <asp:BoundField DataField="PedVendaTipo" HeaderText="Tipo" SortExpression="PedVendaTipo" />
            <asp:BoundField DataField="PedVendaNumPedEnt" HeaderText="N° OC" SortExpression="PedVendaNumPedEnt" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" Font-Size="Small" />
        <EmptyDataRowStyle Font-Size="Small" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"
            Font-Size="Small" />
        <HeaderStyle BackColor="#02b4e3" Font-Bold="True" ForeColor="White"
            Font-Size="Small" CssClass="Rolagem" />
        <PagerStyle BackColor="#02b4e3" ForeColor="White" HorizontalAlign="Center"
            Font-Size="Small" />
        <RowStyle BackColor="#EFF3FB" Font-Size="Small" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"
            Font-Size="Small" />
    </asp:GridView>
    <br />

    <asp:GridView ID="ItemGridView" runat="server" Width="100%"
        EnableModelValidation="True" CellPadding="4" ForeColor="#333333"
        AllowPaging="True" DataKeyNames="PedVendaNum"
        OnPageIndexChanging="ItemGridView_PageIndexChanging" PageSize="5"
        EmptyDataText="Nenhum item encontrado!"
        AutoGenerateColumns="False" CssClass="GridGis" Font-Size="Small"
        AllowSorting="True" OnSorting="ItemGridView_Sorting">
        <AlternatingRowStyle BackColor="White" Font-Size="Small" />
        <Columns>
            <asp:BoundField DataField="ProdCodEstr" HeaderText="Cód. Produto" SortExpression="ProdCodEstr" />
            <asp:BoundField DataField="ProdNome" HeaderText="Produto" SortExpression="ProdNome" />
            <asp:BoundField DataField="ItPedVendaUnidMedCod" HeaderText="Unidade" SortExpression="ItPedVendaUnidMedCod" />
            <asp:BoundField DataField="ItPedVendaQtd" HeaderText="Quantidade" SortExpression="ItPedVendaQtd" />
            <asp:BoundField DataField="TabPVNome" HeaderText="Tabela" SortExpression="TabPVNome" />
            <asp:BoundField DataField="ItPedVendaValUnit" HeaderText="Valor" SortExpression="ItPedVendaValUnit" />
            <asp:BoundField DataField="EntProdCodSeq" HeaderText="Posição" SortExpression="EntProdCodSeq" />
            <asp:BoundField DataField="ItPedVendaValTot" HeaderText="Total" SortExpression="ItPedVendaValTot" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" Font-Size="Small" />
        <EmptyDataRowStyle Font-Size="Small" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"
            Font-Size="Small" />
        <HeaderStyle BackColor="#02b4e3" Font-Bold="True" ForeColor="White"
            Font-Size="Small" CssClass="Rolagem" />
        <PagerStyle BackColor="#02b4e3" ForeColor="White" HorizontalAlign="Center"
            Font-Size="Small" />
        <RowStyle BackColor="#EFF3FB" Font-Size="Small" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"
            Font-Size="Small" />
    </asp:GridView>

</asp:Content>
