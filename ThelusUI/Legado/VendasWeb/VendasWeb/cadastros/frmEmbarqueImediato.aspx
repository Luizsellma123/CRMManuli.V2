<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEmbarqueImediato.aspx.cs" Inherits="VendasWeb.cadastros.frmEmbarqueImediato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Embarque Imediato" CssClass="texto"></asp:Label></td>
            </tr>
            <tr></tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="SIM:" CssClass="texto"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Caso exista material em estoque </ br> pedido é despachado antes da data." CssClass="texto"></asp:Label></td>
            </tr>
            <tr></tr>
            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="NÃO:" CssClass="texto"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label5" runat="server" Text="É respeitada a data de entrega do pedido." CssClass="texto"></asp:Label></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
