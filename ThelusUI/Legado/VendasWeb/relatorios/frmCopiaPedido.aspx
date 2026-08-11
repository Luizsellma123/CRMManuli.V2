<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="frmCopiaPedido.aspx.cs" Inherits="VendasWeb.relatorios.frmCopiaPedido" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/relatorios.css?aux=6" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   
    


    <rsweb:ReportViewer ID="rptCopiaPedidos" runat="server" Font-Names="Arial" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
    Width="800px" Height="950px" CssClass="rptPrincipal" 
        SizeToReportContent="True" >
        <LocalReport ReportPath="relatorios\relCopiaPedido.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>


     <asp:Button ID="EnviarPorEmailButton"  runat="server" Text="" 
        CssClass="btnEmail" onclick="EnviarPorEmailButton_Click" Height="60px" Width="60px" 
          /> Enviar por Email
    
    

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>





</asp:Content>
