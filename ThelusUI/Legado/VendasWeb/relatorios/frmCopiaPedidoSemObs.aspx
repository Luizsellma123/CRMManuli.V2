<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="frmCopiaPedidoSemObs.aspx.cs" Inherits="VendasWeb.relatorios.frmCopiaPedidoSemObs" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/relatorios.css?aux=6" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="empresa" runat="server" />
    <asp:HiddenField ID="pedido" runat="server" />
    <asp:HiddenField ID="operacao" runat="server" />

    <rsweb:ReportViewer ID="rptCopiaPedidoSemObs" runat="server" Font-Names="Arial" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
    Width="800px" Height="950px" CssClass="rptPrincipal" 
        SizeToReportContent="True">
        <LocalReport ReportPath="relatorios\relCopiaPedidoSemObs.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>


     
     <asp:Button ID="EnviarPorEmailButton"  runat="server" Text="" 
        CssClass="btnEmail" onclick="EnviarPorEmailButton_Click" Height="60px" Width="60px" 
          /> Enviar por Email

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:manuliConnectionString5 %>" 
    
        
        
        
        SelectCommand="SELECT * FROM [USER_VW_CabecarioCopiaPedido] WHERE (([EmpCod] = @EmpCod) AND ([PedVendaNum] = @PedVendaNum))">
        <SelectParameters>
            <asp:ControlParameter ControlID="empresa" Name="EmpCod" 
                PropertyName="Value" DefaultValue="0" Type="String" />
            <asp:ControlParameter ControlID="pedido" Name="PedVendaNum" 
                PropertyName="Value" DefaultValue="0" Type="String" />
        </SelectParameters>
</asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:manuliConnectionString5 %>" 
        
        
        SelectCommand="SELECT CAST(ROUND(ItPedVendaQtd, 2) as numeric(14, 2)) as ItPedVendaQtd, [ItPedVendaUnidMedCod], [ProdCodEstr], [ItPedVendaTexto], CAST(ROUND( [ItPedVendaValUnit], 2) as numeric(14, 2)) as ItPedVendaValUnit, CAST(ROUND([ItPedVendaValTot], 2) as numeric(14,2)) as ItPedVendaValTot, CAST(ROUND([ItPedVendaValFinal], 2) as numeric(14,2)) as ItPedVendaValFinal FROM [ITEM_PED_VENDA] WHERE (([EmpCod] = @EmpCod) AND ([PedVendaNum] = @PedVendaNum))">
        <SelectParameters>
            <asp:ControlParameter ControlID="empresa" DefaultValue="0" Name="EmpCod" 
                PropertyName="Value" Type="String" />
            <asp:ControlParameter ControlID="pedido" DefaultValue="0" Name="PedVendaNum" 
                PropertyName="Value" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>


   
    

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


</asp:Content>
