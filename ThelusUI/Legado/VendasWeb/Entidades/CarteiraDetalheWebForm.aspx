<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarteiraDetalheWebForm.aspx.cs" Inherits="VendasWeb.Entidades.CarteiraDetalheWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="contentDetalhe" Style="background: #ffffff;" >
        <form id="formDetalhe" runat="server">
                <div>
                    <asp:GridView ID="ListaClienteGridView" EmptyDataText="Nenhum Cliente Localizado"
                                AutoGenerateColumns="False" runat="server" 
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Código" visible="false">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="num_itemLabel" runat="server" Text=' <%# Bind("CodigoClienteSAP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CNPJ/CPF">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="des_eveitLabel" runat="server" Text=' <%# Bind("CNPJ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nome Cliente">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="val_totalLabel" runat="server" Text=' <%# Bind("NomeCliente") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cidade">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="val_totalLabel" runat="server" Text=' <%# Bind("Cidade") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Último Histórico">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="val_totalLabel" runat="server" Text=' <%# Bind("Historico") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" Visible="false">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="val_totalLabel" runat="server" Text=' <%# Bind("StatusCliente") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="VendedorGridView" EmptyDataText="Nenhum Cliente Localizado"
                                AutoGenerateColumns="False" runat="server" 
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Vendedor">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="CodigoVendedorSAPLabel" runat="server" Text=' <%# Bind("CodigoVendedorSAP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nome Vendedor">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="NomeVendedorLabel" runat="server" Text=' <%# Bind("NomeVendedor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Classe">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="NomeClasseLabel" runat="server" Text=' <%# Bind("NomeClasse") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Telefone">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="TelefoneLabel" runat="server" Text=' <%# Bind("Telefone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
