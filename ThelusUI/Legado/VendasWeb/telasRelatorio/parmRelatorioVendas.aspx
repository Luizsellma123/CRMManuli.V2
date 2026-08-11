<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="parmRelatorioVendas.aspx.cs" Inherits="VendasWeb.telasRelatorio.parmRelatorioVendas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/jquery.calendario.css?aux=6" />

    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.calendario.js" type="text/javascript"></script>

    <script language="javascript" src="../js/relatorioVendas.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="entCabecario" class="detCabeccario">
            <asp:Label ID="lblTitulo" runat="server" Text="Relatório De Vendas" CssClass="textoTitulo"></asp:Label>
        </div>
        
        <table>
            <tr>
                <!--Filtro Empresa -->
                <td><asp:Label ID="lblEmpresa" runat="server" Text="Empresa:" CssClass="texto"></asp:Label></td>
                <td><asp:DropDownList ID="drpEmpresa" runat="server" CssClass="campo">
                </asp:DropDownList></td>     
            </tr>
            <tr>
                <td><asp:Label ID="lblCodigoVendedor" runat="server" Text="Código Vendedor:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtVendedor" runat="server" CssClass="campo"></asp:TextBox><div id="errorVendedor" class="validacaoErro"></div></td>
                
            </tr>

            <tr>
                <!-- Data Inicial -->
                <td><asp:Label ID="lblDataInicial" runat="server" Text="Data Inicial:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtDataInicial" runat="server" CssClass="campo"></asp:TextBox>
                <a href="#" id="btnCalendar1"><img src="../imagens/calendar.png" alt="Alteração" border="0"/></a>
                <div id="erroDataInicial" class="validacaoErro">
                </td>
            </tr>
            <tr>
                <!-- Data Final -->
                <td><asp:Label ID="lblDataFinal" runat="server" Text="Data Final:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtDataFinal" runat="server" CssClass="campo"></asp:TextBox>
                <a href="#" id="btnCalendar2"><img src="../imagens/calendar.png" alt="Alteração" border="0"/></a>
                <div id="erroDataFinal" class="validacaoErro">
                </td>
            </tr>
            <tr>
            <td colspan="2">
                <asp:CheckBoxList ID="chkList" runat="server" Height="274px" RepeatColumns="2" 
                      style="margin-left: 0px" Width="376px" CssClass="texto">
              </asp:CheckBoxList>
            </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnGerar" runat="server" Text="Gerar Relatório" 
            CssClass="Botoes" onclick="btnGerar_Click" />
        
</asp:Content>
