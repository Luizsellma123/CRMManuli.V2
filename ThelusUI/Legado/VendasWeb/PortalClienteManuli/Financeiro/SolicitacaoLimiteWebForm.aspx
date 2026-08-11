<%@ Page Title="" Language="C#" MasterPageFile="~/PortalCliente.Master" AutoEventWireup="true" CodeBehind="SolicitacaoLimiteWebForm.aspx.cs" Inherits="VendasWeb.PortalClienteManuli.Financeiro.SolicitacaoLimiteWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">

    <div class="col-md-12 grid-margin stretch-card">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Solicitação de Limite</h4>
                <p class="card-description">Entre em contato com setor financeiro.</p>

                <div class="form-group">
                    <label for="exampleInputUsername">Título:</label>
                    <asp:TextBox ID="TituloTextBox" runat="server" CssClass="form-control" placeholder="Digite o assunto."></asp:TextBox>
                    <asp:RequiredFieldValidator ID="TituloTextBoxRequiredFieldValidator" runat="server" ControlToValidate="TituloTextBox" Display="Dynamic" ErrorMessage="*Assunto obrigatório." SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>

                <asp:TextBox ID="descricaoTextBox" runat="server" TextMode="MultiLine" placeholder="Descreva sua solicitação."></asp:TextBox>
                <asp:RequiredFieldValidator ID="descricaoTextBoxRequiredFieldValidator" runat="server" ControlToValidate="descricaoTextBox" Display="Dynamic" ErrorMessage="*Descrição obrigatória." SetFocusOnError="True"></asp:RequiredFieldValidator>

                <div class="custom_file_upload">
                    <asp:TextBox ID="AttachmentTextBox" CssClass="file" runat="server" placeholder="Digite o nome do arquivo."></asp:TextBox>
                    <div class="file_upload">
                            <!--<input type="file" id="file_upload" name="file_upload">-->
                            <asp:FileUpload ID="file_upload" name="file_upload" runat="server" />
                    </div>
                </div>

                <!--Botões de controle-->
                <asp:Button ID="EnviarButton" runat="server" Text="Enviar Solicitação" CssClass="btn btn-primary mr-2" OnClick="EnviarButton_Click"  />
                <asp:Button ID="VoltarButton" runat="server" Text="Voltar" CssClass="btn btn-light" />

            </div>
        </div>
    </div>

    <script src="<%=Page.ResolveClientUrl("~/PortalClienteManuli/js/editorDemo.js")%>"></script>

</asp:Content>
