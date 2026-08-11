<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="LiberaPedidoProducaoDetalheWebForm.aspx.cs" Inherits="VendasWeb.AdministracaoVendas.LiberaPedidoProducaoDetalheWebForm" %>

<%@ Register Src="~/usercontrol/AdmVendasWebUserControl.ascx" TagPrefix="uc1" TagName="AdmVendasWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/LiberaPedidoProducaoDetalheJavaScript.js?aux=1")%>" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField runat="server" ID="IDClienteHiddenField" />

    <asp:HiddenField runat="server" ID="IDEmpresaHiddenField" />

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
                    <h3 class="panel-title">Administração Vendas - Liberação Pedido</h3>
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

                    <%--  LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="PedidoSAPLabel" runat="server" Text="Pedido SAP:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="PedidoCRMLabel" runat="server" Text="Pedido CRM:"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="EmpresaTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="PedidoSAPTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="PedidoCRMTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="ClienteLabel" runat="server" Text="Cliente:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="UtilizacaoLabel" runat="server" Text="Utilização:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="LiberadoProducaoLabel" runat="server" Text="Liberado Produção:"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="UtilizacaoTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="LiberadoProducaoTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 3--%>
                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="DataLancamentoLabel" runat="server" Text="Data Lançamento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="DataEntregaLabel" runat="server" Text="Data Entrega:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="EmbarqueImediatoLabel" runat="server" Text="Embarque Imediato:"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DataLancamentoTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DataEntregaTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="EmbarqueImediatoTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 4--%>
                    <div class="row">

                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="VendedorLabel" runat="server" Text="Vendedor:"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="VendedorTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 5--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NovoHistoricoLabel" runat="server" Text="Novo Histórico:"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NovoHistoricoTextBox" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 7--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="HistoricoPedidoLabel" runat="server" Text="Histórico Pedido:"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="HistoricoPedidoTextBox" TextMode="MultiLine" runat="server" Rows="10" Enabled="false"></asp:TextBox>
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

                        <asp:LinkButton ID="VoltarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="VoltarButton_Click">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="AprovarButton" class="btn btn-success btn-labeled fa fa-check fa-lg"
                            CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="AprovarButton_Click">Aprovar</asp:LinkButton>

                        <asp:LinkButton ID="ReprovarButton" class="btn btn-danger btn-labeled fa fa-close  fa-lg"
                            CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="ReprovarButton_Click">Reprovar</asp:LinkButton>

                        <asp:LinkButton ID="RetornarVendedorLinkButton" class="btn btn-danger btn-labeled fa fa-close fa-lg"
                            CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="RetornarVendedorLinkButton_Click">Retornar Vendedor</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="MultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="View" runat="server">
                <div class="panel">

                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="TesteUpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="LiberaPedidoGridView" EmptyDataText="Não foi possível encontrar nenhuma Produto" AutoGenerateColumns="False"
                                        runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="GridView_PageIndexChanging" Visible="true">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Produto">
                                                <ItemTemplate>
                                                    <asp:Label ID="ProdutoGridViewLabel" runat="server" Text='<%# Bind("Produto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qtde.">
                                                <ItemTemplate>
                                                    <asp:Label ID="QuantidadeGridViewLabel" runat="server" Text='<%# Bind("Quantidade") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Detalhes">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="TesteUpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-primary fa fa-plus-square" CausesValidation="false" runat="server"
                                                                OnClientClick='<%# string.Format("RecuperaPedidoProdutoDetalhe( \""+Eval("IDEmpresa")+"\" , \""+Eval("NumeroPedidoSAP")+"\" , \""+Eval("NumeroPedidoCRM")+"\" , \""+Eval("CodigoItemSAP")+"\" , \""+Eval("Cliche")+"\")")%>'></asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="DetalhesLinkButton" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="LiberaPedidoGridView" />
                                </Triggers>
                            </asp:UpdatePanel>

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

    <div id="PedidoProdutoDetalheModal" class="modal fade bd-example-modal-xl">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: 15px;">
                    <h4 id="modalTitle" class="modal-title">Liberação Pedido - Detalhe</h4>
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
                                            <th style="width: 50%;">Cliente</th>
                                            <th style="width: 50%;">Empresa</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="ClienteModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="EmpresaModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 2--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Pedido CRM</th>
                                            <th style="width: 50%;">Status CRM</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="NumeroPedidoCRMModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="StatusPedidoCRMModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 3--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Vendedor</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="NomeVendedorModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 4--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Emissão</th>
                                            <th style="width: 50%;">Entrega</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="DataEmissaoModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="DataEntregaModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 5--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Embarque Imediato</th>
                                            <th style="width: 50%;">Produto</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="EmbarqueImediatoModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="ProdutoModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 6--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Clichê:                                            
                                                <asp:Label runat="server" ID="ClicheModalLabel"></asp:Label>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <th>
                                                <asp:Image ID="ImagemClicheModal" Style="width: 100%;"
                                                    alt="Minha Figura" runat="server" />
                                            </th>
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

    <uc1:AdmVendasWebUserControl runat="server" ID="AdmVendasWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

</asp:Content>
