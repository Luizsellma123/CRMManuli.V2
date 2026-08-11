<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformativoUtilizacaoWebForm.aspx.cs" Inherits="VendasWeb.cadastros.InformativoUtilizacaoWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="DESTINAÇÃO DA MERCADORIA" CssClass="texto" Font-Bold="true"></asp:Label></td>
                </tr>
                <tr></tr>
                <tr></tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="INSUMO:" CssClass="texto" Font-Bold="true"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Matéria prima, produtos intermediários e material de embalagem que serão utilizados no processo de industrialização." CssClass="texto"></asp:Label></td>
                </tr>
                <tr></tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="CONSUMO:" CssClass="texto" Font-Bold="true"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Compra para uso próprio/interno, sem nenhuma participação direta no processo de industrialização ou comercialização." CssClass="texto"></asp:Label></td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="REVENDA:" CssClass="texto" Font-Bold="true"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Compra para revender, material não sofrerá industrialização." CssClass="texto"></asp:Label></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
