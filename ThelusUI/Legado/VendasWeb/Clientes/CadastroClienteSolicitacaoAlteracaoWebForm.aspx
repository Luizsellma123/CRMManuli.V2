<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroClienteSolicitacaoAlteracaoWebForm.aspx.cs" Inherits="VendasWeb.Clientes.CadastroClienteSolicitacaoAlteracaoWebForm" %>

<%@ Register Src="~/usercontrol/UCCadastroCliente.ascx" TagPrefix="uc1" TagName="UCCadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/bootstrap-filestyle.min.js")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(":file").filestyle({ buttonName: "btn-primary" });
    </script>
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
                    <h3 class="panel-title">Cadastro Cliente - Solicitação Alteração</h3>
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

                    <asp:HiddenField ID="IDCliente" runat="server" />

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
                                <asp:TextBox class="form-control" ID="NomeClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="NomeClienteTextBox" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:DropDownList ID="TipoSolicitacaoDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="TipoSolicitacaoDropDownList" Display="Dynamic" ErrorMessage="* Selecione um tipo de solicitação."
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DescricaoObservacaoTextBox" runat="server" TextMode="MultiLine" Rows="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="DescricaoObservacaoTextBox" Display="Dynamic" ErrorMessage="* Informe a solicitação."
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:FileUpload CssClass="filestyle" data-buttonName="btn-primary" ID="ArquivoFileUpload" runat="server" TabIndex="-1" Style="position: absolute; clip: rect(0px, 0px, 0px, 0px);" />
                        </div>
                    </div>

                </div>


                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:LinkButton ID="EnviarEmailButton" class="btn btn-success btn-labeled fa fa-envelope fa-lg"
                                        runat="server" OnClick="EnviarEmailButton_Click">Enviar</asp:LinkButton>

                                    &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                        runat="server" CausesValidation="false" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="EnviarEmailButton" />
                                    <asp:AsyncPostBackTrigger ControlID="RetornarLinkButton" />
                                </Triggers>
                            </asp:UpdatePanel>

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
