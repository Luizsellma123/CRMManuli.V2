<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AnaliseCreditoWebForm.aspx.cs" Inherits="VendasWeb.Clientes.AnaliseCreditoWebForm" %>

<%@ Register Src="~/usercontrol/UCCadastroCliente.ascx" TagPrefix="uc1" TagName="UCCadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
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
                    <h3 class="panel-title">Cadastro Cliente - Análise Crédito</h3>
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
                                <asp:Label ID="CodigoCliente" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CodigoClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NomeClienteLabel" runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NomeClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PeriodoInicialLabel" runat="server" Text="Período Inicial:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="PeriodoInicialTextBox" runat="server" CssClass="form-control"
                                    TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PeriodoFinalLabel" runat="server" Text="Período Final:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="PeriodoFinalTextBox" runat="server" CssClass="form-control"
                                    TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NumeroAnaliseLabel" runat="server" Text="Num. Análise:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox ID="NumeroAnaliseTextBox" runat="server" CssClass="form-control"></asp:TextBox>
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

                        <asp:LinkButton ID="NovoAnaliseLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                            CausesValidation="false" runat="server" OnClick="NovoAnaliseLinkButton_Click">Nova Análise</asp:LinkButton>

                        <asp:LinkButton ID="ContaCorrenteLinkButton" class="btn btn-warning btn-labeled fa fa-user fa-lg"
                            runat="server" OnClick="ContaCorrenteLinkButton_Click">Conta Corrente</asp:LinkButton>

                        <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                        <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="ClienteMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ClienteView" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="ClienteGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="ClienteGridView_PageIndexChanging" Visible="true"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Análise">
                                        <ItemTemplate>
                                            <center>
                                                <asp:Label ID="IDAnaliseLabel" runat="server" Text='<%# Bind("IDAnalise") %>'></asp:Label>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data">
                                        <ItemTemplate>
                                            <asp:Label ID="DataLabel" runat="server" Text='<%# Bind("DataAnalise") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Usuário">
                                        <ItemTemplate>
                                            <asp:Label ID="UsuarioLabel" runat="server" Text='<%# Bind("Usuario") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Score">
                                        <ItemTemplate>
                                            <asp:Label ID="ScoreLabel" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Relatório">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="RelatorioUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="RelatorioLinkButton" class="btn btn-primary fa fa-chevron-down"
                                                            CausesValidation="false" runat="server"></asp:LinkButton>
                                                    </center>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="RelatorioLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Detalhes" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-primary fa fa-plus-square"
                                                    CausesValidation="false" runat="server" OnClick="DetalhesLinkButton_Click"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
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

    <div id="fullReservaModal" class="modal fade bd-example-modal-xl">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: 15px;">
                    <h4 id="modalTitle" class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal"><span>×</span> <span class="sr-only">Fechar</span></button>
                </div>

                <div id="modalBody" class="modal-body">
                    <div class="loader" id="LoadingDados"></div>

                    <div class="table-responsive" id="DadosModal">
                        <div class="col-md-12 pad-top bg-gray" style="padding-right: 15px;">
                            <div class="row pad-lft pad-rgt">

                                <%--LINHA 1--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Empresa</th>
                                            <th style="width: 50%;">Cliente</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="EmpresaModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="ClienteModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>

                </div>

                <div class="modal-footer">

                    <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>

                </div>
            </div>
        </div>
    </div>

    <uc1:UCCadastroCliente runat="server" ID="UCCadastroCliente" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 

</asp:Content>
