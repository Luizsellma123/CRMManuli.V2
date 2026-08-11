<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="RelatorioFaturadosWebForm.aspx.cs" Inherits="VendasWeb.Clientes.RelatorioFaturadosWebForm" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>    

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="scrollbar-arrow-color;"></div>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"  ></cr:crystalreportviewer>
</asp:Content>    


