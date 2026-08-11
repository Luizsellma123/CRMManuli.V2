<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="manualLoginFlash.aspx.cs" Inherits="VendasWeb.flash.manualLoginFlash" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" 
            codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0"
            width="100" height="150">

      <param name="movie" value="nome_filmato.swf">
      <param name="quality" value="high">
      <embed src="Login.swf" quality="high" width="1000" height="850" 
             type="application/x-shockwave-flash" 
             pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash">
      </embed>
    </object>
</asp:Content>
