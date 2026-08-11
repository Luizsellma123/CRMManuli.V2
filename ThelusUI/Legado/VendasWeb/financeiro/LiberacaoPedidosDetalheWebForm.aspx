<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="LiberacaoPedidosDetalheWebForm.aspx.cs" Inherits="VendasWeb.financeiro.LiberacaoPedidosDetalheWebForm" %>

<%@ Register Src="~/usercontrol/FinanceiroWebUserControl.ascx" TagPrefix="uc1" TagName="FinanceiroWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../js/LiberacaoPedidosWebFormJS.js" type="text/javascript"></script>
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
                    <h3 class="panel-title">Financeiro - Lista Analise Pedidos</h3>
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

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelEmpresa" runat="server" Text="Empresa :"></asp:Label></h5>
                                <asp:Label ID="EmpresaLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelPedidoCRM" runat="server" Text="Pedido CRM:"></asp:Label></h5>
                                <asp:Label ID="PedidoCRMLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabePedidoSAP" runat="server" Text="Pedido SAP:"></asp:Label></h5>
                                <asp:Label ID="PedidoSAPLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelEsboco" runat="server" Text="Esboço SAP:"></asp:Label></h5>
                                <asp:Label ID="EsbocoLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>


                    </div>

                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelEntidade" runat="server" Text="Cliente :"></asp:Label></h5>
                                <asp:Label ID="ClienteLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelUtilizacao" runat="server" Text="Utilização :"></asp:Label></h5>
                                <asp:Label ID="UtilizacaoLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelTotalPedido" runat="server" Text="Total Aberto:"></asp:Label></h5>
                                <asp:Label ID="TotalPedidoLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelCondicaoPagamento" runat="server" Text="Condição Pagamento :"></asp:Label></h5>
                                <asp:Label ID="CondicaoPagamentoLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelLancamentoPedido" runat="server" Text="Data Lançamento:"></asp:Label></h5>
                                <asp:Label ID="DataLancamentoLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelDataEntrega" runat="server" Text="Data Entrega:"></asp:Label></h5>
                                <asp:Label ID="DataEntregaLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelDataDocumento" runat="server" Text="Data Documento:"></asp:Label></h5>
                                <asp:Label ID="DataDocumentoLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-8">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="MotivoLabel" runat="server" Text="Motivo :"></asp:Label>
                                </h5>
                                <asp:DropDownList ID="MotivoDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="DiasAbertoLabel" runat="server" Text="Dias Cancelamento:"></asp:Label>
                                </h5>
                                <asp:DropDownList ID="DiasCancelamentoDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="HistoricoLabel" runat="server" Text="Motivo Detalhado :"></asp:Label>
                                </h5>
                                <asp:TextBox ID="HistoricoTextBox" Width="100%" Height="90px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <span id="counter"></span>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="HistoricoPedidoLabel" runat="server" Text="Historico Pedido :"></asp:Label>
                                </h5>
                                <asp:TextBox ID="HistoricoPedidoTextBox" Width="100%" Height="90px" TextMode="MultiLine" runat="server" Enabled="false"></asp:TextBox>
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

                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="ContaCorrenteLinkButton" class="btn btn-success btn-labeled fa fa-user fa-lg"
                                    runat="server" OnClick="ContaCorrenteLinkButton_Click">Conta Corrente</asp:LinkButton>

                                <asp:LinkButton ID="AprovarLinkButton" class="btn btn-success btn-labeled fa fa-check fa-lg"
                                    runat="server" OnClick="AprovarLinkButton_Click">Aprovar</asp:LinkButton>

                                <asp:LinkButton ID="ReprovarLinkButton" class="btn btn-danger btn-labeled fa fa-close fa-lg"
                                    runat="server" OnClick="ReprovarLinkButton_Click">Reprovar</asp:LinkButton>

                                <asp:LinkButton ID="RetornarVendedorLinkButton" class="btn btn-warning btn-labeled fa fa-refresh  fa-lg"
                                    runat="server" OnClick="RetornarVendedorLinkButton_Click">Retornar Vendedor</asp:LinkButton>

                                <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success btn-labeled fa fa-floppy-o  fa-lg"
                                    runat="server" OnClick="SalvarLinkButton_Click">Salvar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                <asp:PostBackTrigger ControlID="ContaCorrenteLinkButton" />
                                <asp:PostBackTrigger ControlID="AprovarLinkButton" />
                                <asp:PostBackTrigger ControlID="ReprovarLinkButton" />
                                <asp:PostBackTrigger ControlID="RetornarVendedorLinkButton" />
                                <asp:PostBackTrigger ControlID="SalvarLinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="AprovacoesMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="AprovacoesView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Pedidos Liberar
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="AprovacoesGridView" EmptyDataText="Não foi possível encontrar nenhuma regra para autorizar." AutoGenerateColumns="False"
                                runat="server" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoLabel" runat="server" Text='<%# Bind("Codigo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome Regra">
                                        <ItemTemplate>
                                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Descrição Regra">
                                        <ItemTemplate>
                                            <asp:Label ID="DescricaoLabel" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
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

    <uc1:FinanceiroWebUserControl runat="server" ID="FinanceiroWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
