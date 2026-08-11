<%@ Page Title="" Language="C#" MasterPageFile="~/PortalCliente.Master" AutoEventWireup="true" CodeBehind="UsuarioPortalDetalheWebForm.aspx.cs" Inherits="VendasWeb.PortalClienteManuli.UsuarioPortalDetalheWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div class="col-md-12 grid-margin stretch-card">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Dados Usuário</h4>
                <p class="card-description">Atualize seus dados.</p>

                <div class="form-group">
                    <label for="exampleInputUsername1">E-mail:</label>
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control" placeholder="Digite seu e-mail"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="exampleInputUsername1">Telefone:</label>
                    <asp:TextBox ID="TelefoneTextBox" runat="server" CssClass="form-control" placeholder="Digite seu telefone" onkeypress="mascara( this, mtel );"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="exampleInputUsername1">Alterar Senha:</label>
                    <asp:TextBox TextMode="Password" ID="SenhaTextBox" runat="server" CssClass="form-control" placeholder="Digite sua senha"></asp:TextBox>
                </div>
                
                <!--Botões de controle-->
                <asp:Button ID="SalvarButton" runat="server" Text="Salvar" CssClass="btn btn-primary mr-2" OnClick="SalvarButton_Click" />
                <asp:Button ID="VoltarButton" runat="server" Text="Voltar" CssClass="btn btn-light" OnClick="VoltarButton_Click" />

            </div>
        </div>
    </div>
</asp:Content>
