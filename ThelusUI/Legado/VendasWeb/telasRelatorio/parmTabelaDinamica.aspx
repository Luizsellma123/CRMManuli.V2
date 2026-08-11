<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="parmTabelaDinamica.aspx.cs" Inherits="VendasWeb.telasRelatorio.parmTabelaDinamica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/jquery.calendario.css?aux=6" />

    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.calendario.js" type="text/javascript"></script>

    <script language="javascript" src="../js/tabelaDinamica.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="entCabecario" class="detCabeccario">
            <asp:Label ID="lblTitulo" runat="server" Text="Relatório Tabela Dinâmica" CssClass="textoTitulo"></asp:Label>
        </div>
    
    <table>
            <tr>
                <!--Filtro Empresa -->
                <td><asp:Label ID="lblEmpresa" runat="server" Text="Empresa:" CssClass="texto"></asp:Label></td>
                <td><asp:DropDownList ID="drpEmpresa" runat="server" CssClass="campo">
                </asp:DropDownList></td>     
            </tr>

            <tr>
                <!-- Data Inicial -->
                <td><asp:Label ID="lblDataInicial" runat="server" Text="Data Inicial:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtDataInicial" runat="server" CssClass="campo"></asp:TextBox>
                <a href="#" id="btnCalendar1"><img src="../imagens/calendar.png" alt="Alteração" border="0"/></a>
                <div id="erroDataInicial" class="validacaoErro"></div>
                </td>
            </tr>
            <tr>
                <!-- Data Final -->
                <td><asp:Label ID="lblDataFinal" runat="server" Text="Data Final:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtDataFinal" runat="server" CssClass="campo"></asp:TextBox>
                <a href="#" id="btnCalendar2"><img src="../imagens/calendar.png" alt="Alteração" border="0"/></a>
                <div id="erroDataFinal" class="validacaoErro"></div>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblEntidade" runat="server" Text="Entidade:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtEntidade" runat="server" CssClass="campo"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblNatureza" runat="server" Text="Natureza:" CssClass="texto"></asp:Label></td>
                <td><asp:TextBox ID="txtNatureza" runat="server" CssClass="campo"></asp:TextBox></td>
            </tr>
            
            <tr>
                <!--Filtro Linha -->
                <td><asp:Label ID="lblLinha" runat="server" Text="Linha Produto:" CssClass="texto"></asp:Label></td>
                <td><asp:DropDownList ID="drpLinhaProduto" runat="server" CssClass="campo">
                </asp:DropDownList></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblDescricao" runat="server" Text="Descrição :" CssClass="texto"></asp:Label></td>
                <td colspan="2"><asp:TextBox ID="txtDescricao" runat="server" CssClass="campoExtended"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblSubFamilia" runat="server" Text="Sub Família :" CssClass="texto"></asp:Label></td>
                <td colspan="2"><asp:TextBox ID="txtSubFamilia" runat="server" CssClass="campoExtended"></asp:TextBox></td>
            </tr>

        </table>

        <br />

        <asp:Button ID="btnGerar" runat="server" Text="Gerar Relatório" 
        CssClass="Botoes" onclick="btnGerar_Click" />

</asp:Content>
