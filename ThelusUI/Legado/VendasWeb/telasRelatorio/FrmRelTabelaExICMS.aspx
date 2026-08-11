<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmRelTabelaExICMS.aspx.cs" Inherits="VendasWeb.telasRelatorio.FrmRelTabelaExICMS" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




  <div style="border:1px solid #fff; width:800px; text-align:right; height: 800px;">
        <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="800px" height= "800px" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <localreport reportpath="relatorios\RptRelTabelaExICMS.rdlc">
            </localreport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
    </div>

</asp:Content>
