<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="frmListaNotasFiscais.aspx.cs" Inherits="VendasWeb.cadastros.frmListaNotasFiscais" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style=" width:100%; height:300px;">
        <div style=" padding:5px; width:30%; float:left;">
            Empresa
            <asp:CheckBoxList ID="chkListaEmpresa" runat="server"></asp:CheckBoxList>
        </div>
        <div style=" padding:5px; width:65%; height:181px; float:left; ">
            <asp:Label ID="LblNF" runat="server" Text="Nota fiscal: " Width="100"></asp:Label>
            <asp:TextBox ID="txtNF" runat="server" Width="100"></asp:TextBox>    
            <br /><br />

            <asp:Label ID="lblEntidade" runat="server" Text="Entidade: " Width="100"></asp:Label>
            <asp:TextBox ID="txtEntidade" runat="server" Width="100"></asp:TextBox>    
            <br /><br />
            <asp:Label ID="lblBotao" runat="server" Text="" Width="100"></asp:Label>
            <asp:Button ID="ListarButton" runat="server" Text="Listar" CssClass="Botoes" Width="90px"  onclick="ListarButton_Click" />
            <asp:Button ID="VoltarButton" runat="server" Text="Voltar" CssClass="Botoes" Width="90px"  onclick="VoltarButton_Click" />
        </div>

        <br />
        <br />

        <div class="DivGrid" style="overflow: auto; max-height: 100%;  width:100%;">
            <asp:GridView ID="NFGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="NfNum" 
                CssClass="campo" EmptyDataText="Nenhum registro encontrado!" 
                Font-Size="Small" ForeColor="#333333" CellPadding="4"  Width="100%"
                EnableModelValidation="True" AllowPaging="False">
                <Columns>
                    <asp:BoundField DataField="EmpCod" HeaderText="Empresa" SortExpression="EmpCod"> <ItemStyle Wrap="false"/> </asp:BoundField>
                    <asp:BoundField DataField="EntCpfCgc" HeaderText="CPF\CNPJ" SortExpression="EntCpfCgc"> <ItemStyle Wrap="false"/> </asp:BoundField>                                  
                    <asp:BoundField DataField="EntNome" HeaderText="Entidade" SortExpression="EntNome"> <ItemStyle Wrap="false"/> </asp:BoundField>
                    <asp:BoundField DataField="NFNum" HeaderText="Número Nota" SortExpression="NFNum"> <ItemStyle Wrap="false"/> </asp:BoundField>
                    <asp:BoundField DataField="NFDataEmis" HeaderText="Data Emissão" SortExpression="NFDataEmis"> <ItemStyle Wrap="false"/> </asp:BoundField>
                    <asp:BoundField DataField="NFValTotNota" HeaderText="Valor total" SortExpression="NFValTotNota"> <ItemStyle Wrap="false"/> </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" Font-Size="Small" />
                <EmptyDataRowStyle Font-Size="Small" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                    Font-Size="Small" />
                <HeaderStyle BackColor="#02b4e3" Font-Bold="True" ForeColor="White" 
                    Font-Size="Small" CssClass="Rolagem"/>
                <PagerStyle BackColor="#02b4e3" ForeColor="White" HorizontalAlign="Center" 
                    Font-Size="Small" />
                <RowStyle BackColor="#EFF3FB" Font-Size="Small" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" 
                    Font-Size="Small" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
