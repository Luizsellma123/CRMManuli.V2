<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="UsuarioPerfilWebForm.aspx.cs" Inherits="VendasWeb.Usuarios.UsuarioPerfilWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <!--===================================================-->
            <!--Painel Carteiras e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <%--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>--%>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--<button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>--%>
                    </div>
                    <h3 class="panel-title">Cadastro Usuário - Perfil</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text=""
                    runat="server"></asp:Literal>

                <div class="panel-body">

                    <asp:HiddenField ID="IDUsuarioHiddenField" runat="server" />

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoUsuarioLabel" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CodigoUsuarioTextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="CodigoUsuarioTextBox" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="StatusLabel" runat="server" Text="Status:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="StatusTextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="StatusTextBox" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NomeLabel" runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NomeUsuarioTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="NomeUsuarioTextBox" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EmailLabel" runat="server" Text="Email:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="EmailTextBox" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="EmailTextBox1RegularExpressionValidator"
                                    runat="server" ControlToValidate="EmailTextBox" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Email Invalido" ForeColor="Red"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email Inválido</asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TelefoneLabel" runat="server" Text="Telefone:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="TelefoneTextBox" runat="server" onkeypress="mascara( this, mtel );"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NovaSenhaLabel" runat="server" Text="Nova Senha:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="SenhaNovaTextBox" onkeydown="checkEnter(event)" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Repita Senha:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="SenhaNovaRepetirTextBox" onkeydown="checkEnter(event)" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                            <asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                runat="server" OnClick="GravarButton_Click">Atualizar</asp:LinkButton>

                        </div>

                    </div>
                </div>


            </div>

        </div>
    </div>

    <script type="text/javascript">
        function checkEnter(event) {
            // Verificar se a tecla pressionada é Enter (keyCode 13)
            if (event.keyCode === 13) {
                // Impedir o comportamento padrão do Enter (opcional)
                event.preventDefault();

                // Simular o clique no LinkButton
                document.getElementById('<%= GravarButton.ClientID %>').click();
         }
     }
    </script>

</asp:Content>
