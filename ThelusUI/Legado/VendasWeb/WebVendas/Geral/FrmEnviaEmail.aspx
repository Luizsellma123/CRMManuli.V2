<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmEnviaEmail.aspx.cs" Inherits="VendasWeb.WebVendas.Geral.FrmEnviaEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <br>
    <center><b> Enviar Email</b></center>
    <br />

    <asp:Label ID="EmailRemetenteLabel" runat="server" Text="Remetente:" Width="100px"></asp:Label>
    
    
    <asp:TextBox ID="EmailRemetenteTextBox" runat="server" Height="21px"  Width="400px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="EmailRemetenteRegularExpressionValidator" 
        runat="server" ControlToValidate="EmailRemetenteTextBox" Display="Dynamic" 
        ErrorMessage="Email Invalido" ForeColor="Red" SetFocusOnError="True" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>

    <br />

    
    <asp:Label ID="EmailDestinatarioLabel" runat="server" Text="Destinatario:" Width="100px"></asp:Label>
 <asp:TextBox ID="EmailDestinatarioTextBox" runat="server" Height="21px"  Width="400px"></asp:TextBox>
 <asp:RegularExpressionValidator ID="EmailDestinatarioRegularExpressionValidator" 
        runat="server" ControlToValidate="EmailDestinatarioTextBox" Display="Dynamic" 
        ErrorMessage="Email Invalido" ForeColor="Red" SetFocusOnError="True" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>


    <br />
    
    <asp:Label ID="EmailCopiaLabel" runat="server" Text="Copiar:" Width="100px"></asp:Label>
    
    <asp:TextBox ID="EmailCopiaTextBox" runat="server" Height="21px"  Width="400px"></asp:TextBox>
    

    &nbsp;(Utilizar ; para separar os email em copia)<br /><br />
    <asp:Label ID="AssuntoLabel" runat="server" Text="Assunto Email:" Width="100px"></asp:Label>
    <asp:TextBox ID="AssuntoTextBox" runat="server" Height="21px"  Width="400px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="AssuntoRequiredFieldValidator" 
        runat="server" ControlToValidate="AssuntoTextBox" Display="Dynamic" 
        ErrorMessage="Campo Obrigatório" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>


    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:Label ID="DescricaoLabel" runat="server" Text="Descrição Email" Width="200px"></asp:Label>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="DescricaoTextBox" runat="server" Height="111px"  Width="400px"   TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="DescricaoRequiredFieldValidator" 
        runat="server" ControlToValidate="DescricaoTextBox" Display="Dynamic" 
        ErrorMessage="Campo Obrigatório" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>




    <br /><br />
    <asp:Label ID="AnexoLabel" runat="server" Text="Anexo:" ></asp:Label>


    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


    
    <asp:FileUpload ID="AnexoFileUpload"  runat="server"  Height="21px"  Width="400px"/>
    <br /><br />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:Button ID="CancelarButton" runat="server" Text="Voltar" Height="26px"  CausesValidation="false"
        Width="77px" onclick="CancelarButton_Click" />
    &nbsp;<asp:Button ID="EnviarEmailButton" runat="server" Text="Enviar" 
        Height="26px" Width="77px" onclick="EnviarEmailButton_Click" />


</asp:Content>
