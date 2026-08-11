<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="frmAgenda.aspx.cs" Inherits="VendasWeb.Entidades.frmAgenda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script language="javascript" src="../js/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="../js/jquery.maskedinput.js" type="text/javascript"></script>
    <script language="javascript" src="../js/jsFormataData.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=5" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblDescCliente" runat="server" Text="Cliente:" Width="60px" CssClass="texto"></asp:Label>
    <asp:Label ID="LblCliente" runat="server" Text="" Width="450px" CssClass="texto"></asp:Label>
    <asp:Label ID="Label6" runat="server" Text="" Width="40" CssClass="texto"></asp:Label>
    <asp:Label ID="lblDescContato" runat="server" Text="Contato:" Width="60px" CssClass="texto"></asp:Label>
    <asp:Label ID="lblContato" runat="server" Text="" Width="250px" CssClass="texto"></asp:Label>
    <br />
    <asp:Label ID="lblDescCNPJ" runat="server" Text="CNPJ:" Width="60px" CssClass="texto"></asp:Label>
    <asp:Label ID="lblCNPJ" runat="server" Text="" Width="450" CssClass="texto"></asp:Label>
    <asp:Label ID="Label7" runat="server" Text="" Width="40" CssClass="texto"></asp:Label>
    <asp:Label ID="lblDescFone" runat="server" Text="Telefone:" Width="60px" CssClass="texto"></asp:Label>
    <asp:Label ID="lblFone" runat="server" Text="" Width="250px" CssClass="texto"></asp:Label>
    <br />
    <asp:Label ID="Label8" runat="server" Text="" Width="550" CssClass="texto"></asp:Label>
    <asp:Label ID="lblDescEmail" runat="server" Text="E-mail:" Width="60px" CssClass="texto"></asp:Label>
    <asp:Label ID="lblEmail" runat="server" Text="" Width="250px" CssClass="texto"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblData" runat="server" Text="Data: "></asp:Label>
    <asp:TextBox ID="txtData" runat="server" Width="100"></asp:TextBox>   
    <asp:Label ID="lblDescricao" runat="server" Text="Descricao: "></asp:Label>
    <asp:TextBox ID="txtDescricao" runat="server" Width="500"></asp:TextBox>   
    <br />
    <br />
    <asp:Button ID="SalvarButton" runat="server" Text="Salvar" CssClass="Botoes" Width="90px" onclick="SalvarButton_Click" />
    <asp:Button ID="ListarButton" runat="server" Text="Listar" CssClass="Botoes" Width="90px" onclick="ListarButton_Click" />
    <asp:Button ID="VoltarButton" runat="server" Text="Voltar" CssClass="Botoes" Width="90px"  onclick="VoltarButton_Click" />
    <br />
    <br />

    <asp:GridView ID="AgendaGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Codigo" 
        CssClass="GridGis" EmptyDataText="Nenhum registro encontrado!" 
        Font-Size="Small" ForeColor="#333333" CellPadding="4" 
        EnableModelValidation="True" AllowPaging="True" 
        onpageindexchanging="AgendaGridView_PageIndexChanging" PageSize="13">
        <Columns>
            <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data"> <ItemStyle Wrap="false"/> </asp:BoundField>
            <asp:BoundField DataField="Historico" HeaderText="Descrição" SortExpression="Historico"> <ItemStyle Wrap="false"/> </asp:BoundField>                                  
        </Columns>
    </asp:GridView>
</asp:Content>
