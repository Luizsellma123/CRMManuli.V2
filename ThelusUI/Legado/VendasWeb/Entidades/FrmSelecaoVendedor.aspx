<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true"
    CodeBehind="FrmSelecaoVendedor.aspx.cs" Inherits="VendasWeb.Entidades.FrmSelecaoVendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="conteudo">
        <center>
            <b>
                <h3>
                    Seleção do Vendedor</h3>
            </b>
        </center>



        
                        <asp:GridView ID="VendedorGridView" EmptyDataText="Nenhum Vendedor Localizado" AutoGenerateColumns="False"
                            CssClass="lstTabela" Width="100%" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SELECIONAR" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        
                                        <asp:CheckBox ID="VendCodCheckBox" CssClass="checkbox" runat="server" AutoPostBack="true"
                                            oncheckedchanged="VendCodCheckBox_CheckedChanged" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="tabLstCab th" />
                                    <ItemStyle CssClass="text-align-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CÓDIGO" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="VendCodLabel" runat="server" Text='<%# Bind("IDVendedor") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="tabLstCab th" />
                                    <ItemStyle CssClass="text-align-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NOME VENDEDOR">
                                    <ItemTemplate>
                                        <asp:Label ID="VendNomeLabel" runat="server" Text='<%# Bind("NomeVendedor") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="tabLstCab th" Width="80%" />
                                    <ItemStyle CssClass="text-align-left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#003300" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>


    </div>
</asp:Content>
