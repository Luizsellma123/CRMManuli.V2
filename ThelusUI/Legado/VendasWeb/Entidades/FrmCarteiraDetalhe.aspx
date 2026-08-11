<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCarteiraDetalhe.aspx.cs" Inherits="VendasWeb.Entidades.FrmCarteiraDetalhe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
 <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />



    <div id="content">
        <div id="ListaPedidosItensDIV" class="ListaPedidosItensCSS" style="width:100%">
         <asp:GridView ID="DetalheEntidadeGridView" runat="server" AutoGenerateColumns="False" CssClass="lstTabela">
        <Columns>

            <asp:TemplateField HeaderText="Cód.Vendedor">
                
                <ItemTemplate>
                    <asp:Label ID="VendCodLabel" runat="server" Text='<%# Bind("VendCod") %>'></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="tabLstCabDois th" />
                 <ItemStyle CssClass="text-align-center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nome Vendedor" >
                <ItemTemplate>
                    <asp:Label ID="VendNomeLabel" runat="server" Text='<%# Bind("VendNome") %>'></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="tabLstCabDois th" Width="50%" />
                 <ItemStyle CssClass="text-align-center" />
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Classe" >
                <ItemTemplate>
                    <asp:Label ID="VendClasseDescrLabel" runat="server" Text='<%# Bind("VendClasseDescr") %>'></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="tabLstCabDois th" Width="30%" />
                 <ItemStyle CssClass="text-align-center" />
            </asp:TemplateField>



            <asp:TemplateField HeaderText="Telefone 1" >
                <ItemTemplate>
                    <asp:Label ID="Telefone1Label" runat="server" Text='<%# Bind("Telefone1") %>'></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="tabLstCabDois th" Width="10%" />
                 <ItemStyle CssClass="text-align-center" />
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Telefone 2" >
                <ItemTemplate>
                    <asp:Label ID="Telefone2Label" runat="server" Text='<%# Bind("Telefone2") %>'></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="tabLstCabDois th" Width="10%" />
                 <ItemStyle CssClass="text-align-center" />
            </asp:TemplateField>
            
        </Columns>
         <HeaderStyle CssClass="tabLstCabDois"  />
    </asp:GridView>

    </div>


    
    </div>
    </form>
</body>


</html>
