<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPrazosProducao.aspx.cs" Inherits="VendasWeb.cadastros.frmPrazosProducao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prazos</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Stretch:" CssClass="texto"></asp:Label></td>
                <td><asp:Label ID="Label2" runat="server" Text="5 dias úteis" CssClass="texto"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Stretch cortado:" CssClass="texto"></asp:Label></td>
                <td><asp:Label ID="Label4" runat="server" Text="7 dias úteis" CssClass="texto"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label5" runat="server" Text="Fita Impressa:" CssClass="texto"></asp:Label></td>
                <td><asp:Label ID="Label6" runat="server" Text="7 dias úteis" CssClass="texto"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label7" runat="server" Text="Fita Impressa Gomada:" CssClass="texto"></asp:Label></td>
                <td><asp:Label ID="Label8" runat="server" Text="10 dias úteis" CssClass="texto"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label9" runat="server" Text="Fita PP:" CssClass="texto"></asp:Label></td>
                <td><asp:Label ID="Label10" runat="server" Text="5 dias úteis" CssClass="texto"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label11" runat="server" Text="Especialidades:" CssClass="texto"></asp:Label></td>
                <td><asp:Label ID="Label12" runat="server" Text="3 dias úteis" CssClass="texto"></asp:Label></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
