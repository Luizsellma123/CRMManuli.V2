<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroUsuariosSAPWebForm.aspx.cs" Inherits="VendasWeb.AdministracaoSistema.CadastroUsuariosSAPWebForm" %>

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
                    <h3 class="panel-title">Cadastro Usuários - Usuários SAP</h3>
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

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoUsuarioLabel" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CodigoUsuarioTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="StatusLabel" runat="server" Text="Status:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control" Enabled="false">
                                    <asp:ListItem Value="Ativo">Ativo</asp:ListItem>
                                    <asp:ListItem Value="Desligado">Desligado</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DadosSAPLabel" runat="server" Text="DADOS SAP:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <hr />
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="IDLabel" runat="server" Text="ID:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="IDTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoLabel" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CodigoTextBox" runat="server"></asp:TextBox>
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
                                <asp:TextBox class="form-control" ID="NomeTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SenhaLabel" runat="server" Text="Senha:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="SenhaTextBox" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="RepitaSenhaLabel" runat="server" Text="Repita Senha:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="RepitaSenhaTextBox" runat="server" TextMode="Password"></asp:TextBox>
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
                        <asp:UpdatePanel ID="FooterUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                    CausesValidation="false" runat="server" OnClick="SalvarLinkButton_Click">Gravar</asp:LinkButton>

                                <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="SalvarLinkButton" />
                                <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <uc1:CadastroUsuarioWebUserControl runat="server" ID="CadastroUsuarioWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

</asp:Content>

