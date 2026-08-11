<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="teste.aspx.cs" Inherits="VendasWeb.pageVideos.teste" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <object height="500px" width="850px" type ="video/avi" style="text-align:center;">
        <param name="src" value="../videos/teste.avi" />
    </object> 
</asp:Content>
