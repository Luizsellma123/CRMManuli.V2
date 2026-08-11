<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroClienteWebForm.aspx.cs" Inherits="VendasWeb.Clientes.CadastroClienteWebForm" %>

<%@ Register Src="~/usercontrol/UCCadastroCliente.ascx" TagPrefix="uc1" TagName="UCCadastroCliente" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/CadastroClienteWebFormJS.js")%>" type="text/javascript"></script>
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
                    <h3 class="panel-title">Cadastro Cliente</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse' aria-expanded='true' style='height: 0px;'>"
                    runat="server"></asp:Literal>

                <div class="panel-body">

                    <asp:HiddenField ID="IDClienteHiddenField" runat="server" />
                    <asp:HiddenField ID="NomeClienteHiddenField" runat="server" />
                    <asp:HiddenField ID="NumeroCNPJHiddenField" runat="server" />

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoCliente" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CodigoClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NomeClienteTextBox" runat="server" placeholder="Informe o nome do cliente."></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="FantasiaLabel" runat="server" Text="Nome Fantasia:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NomeFantasiaTextBox" runat="server" placeholder="Informe o nome fantasia do cliente."></asp:TextBox>
                            </div>
                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NumeroCNPJLabel" runat="server" Text="CNPJ:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NumeroCNPJTextBox"
                                    onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                    runat="server" placeholder="Informe o número CNPJ." OnTextChanged="NumeroCNPJTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NumeroCNPJTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="EmailLabel" runat="server" Text="E-mail:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="EmailTextBox" runat="server" placeholder="Informe o E-mail para Nota Fiscal Eletrônica."></asp:TextBox>
                                <asp:RegularExpressionValidator ID="EmailTextBox1RegularExpressionValidator"
                                    runat="server" ControlToValidate="EmailTextBox" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Email Invalido" ForeColor="Red"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email Inválido</asp:RegularExpressionValidator>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TelefoneLabel" runat="server" Text="Telefone:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="TelefoneTextBox" runat="server" onkeypress="mascara( this, mnum );" placeholder="Informe o telefone da empresa."></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="VendedorLabel" runat="server" Text="Vendedor:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:DropDownList ID="VendedorDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ObservacaoLabel" runat="server" Text="Observacao Breve:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ObservacaoBreveTextBox" runat="server" placeholder="Informe uma observação."></asp:TextBox>
                                <span id="counter"></span>
                            </div>
                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>
                </div>


                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">
                            <asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                runat="server" OnClick="GravarButton_Click">Gravar</asp:LinkButton>

                            <asp:LinkButton ID="BuscaSefazLinkButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                runat="server" OnClick="BuscaSefazLinkButton_Click">Busca Sefaz</asp:LinkButton>

                            <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>

                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

    <uc1:UCCadastroCliente runat="server" ID="UCCadastroCliente" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->


</asp:Content>
