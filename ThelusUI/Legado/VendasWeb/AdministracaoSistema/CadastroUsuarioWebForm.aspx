<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroUsuarioWebForm.aspx.cs" Inherits="VendasWeb.AdministracaoSistema.CadastroUsuarioWebForm" %>

<%@ Register Src="~/usercontrol/CadastroUsuarioWebUserControl.ascx" TagPrefix="uc1" TagName="CadastroUsuarioWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-sm-9">
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
                    <h3 class="panel-title">Cadastro Usuários - Principal</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse' aria-expanded='false' style='height: 0px;'>"
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
                                <asp:TextBox class="form-control" ID="CodigoUsuarioTextBox" runat="server" OnTextChanged="CodigoUsuarioTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                                <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Ativo">Ativo</asp:ListItem>
                                    <asp:ListItem Value="Desligado">Desligado</asp:ListItem>
                                </asp:DropDownList>
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
                                <asp:TextBox class="form-control" ID="SenhaNovaTextBox" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Repita Senha:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="SenhaNovaRepetirTextBox" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row" runat="server" visible="false">
                        <hr />

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="VendedorLabel" runat="server" Text="Vendedor:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="VendedorDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <!--===================================================-->

                <!-- END LINHA 1 - Painel FILTROS-->
            </div>
            <!-- 
            </div> -->

            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">

                        <asp:LinkButton ID="AdicionarVendedorLinkButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                            CausesValidation="false" runat="server" OnClick="AdicionarVendedorLinkButton_Click" Visible="false">Adicionar Vendedor</asp:LinkButton>

                        <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                            CausesValidation="false" runat="server" OnClick="SalvarLinkButton_Click">Gravar</asp:LinkButton>

                        <asp:LinkButton ID="voltarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="voltarButton_Click">Retornar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="UsuariosVendedoresMultiView" runat="server" ActiveViewIndex="0" Visible="true">
        <asp:View ID="PSIUView" runat="server">
            <!-- TABELA -->
            <!--===================================================-->
            <div class="panel" runat="server" visible="false">
                <div class="panel-heading">
                    <h3 class="panel-title">
                            Lista Vendedores do Usuário
                        </h3>
                </div>
                <!-- Foo Table - Filtering -->
                <!--===================================================-->
                <div class="panel-body">
                    <div class="table-responsive">

                        <asp:GridView ID="UsuariosVendedoresGridView" EmptyDataText="Não foram encontrados dados com esses filtros" AutoGenerateColumns="False"
                            runat="server" AllowPaging="false"
                            CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                            Style="border-collapse: collapse; max-width: 100%">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>

                                        <center>
                                        <asp:LinkButton ID="DeleteButton" class="btn btn-danger fa fa-times"
                                            CausesValidation="false" runat="server" OnClick="DeleteButton_Click"></asp:LinkButton>

                                            </center>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="IDVendedor" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="IDVendedorLabel" runat="server" Text='<%# Bind("IDVendedorNovo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nome Vendedor">
                                    <ItemTemplate>
                                        <asp:Label ID="NomeVendedorLabel" runat="server" Text='<%# Bind("VendNome") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!--===================================================-->
            </div>
            <!-- End Foo Table - Filtering -->
            <!--===================================================-->
            <!-- END TABELA -->
        </asp:View>
    </asp:MultiView>

    </div>

    <uc1:CadastroUsuarioWebUserControl runat="server" id="CadastroUsuarioWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>
