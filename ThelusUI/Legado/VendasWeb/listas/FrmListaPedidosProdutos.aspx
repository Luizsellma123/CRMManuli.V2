<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmListaPedidosProdutos.aspx.cs" Inherits="VendasWeb.listas.FrmListaPedidosProdutos" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="../js/cadArtePedido.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-9">
            <!--===================================================-->
            <!--Painel e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                    <h3 class="panel-title">Pedidos - Selecionar Produtos</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->
                <div id="painel_aberto" class="">
                </div>
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <!--Filtro Nome/Numero -->
                                <asp:DropDownList ID="TipoDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Selected="True">Cód. Produto</asp:ListItem>
                                    <asp:ListItem Value="2">Descrição</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:TextBox ID="FiltroTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="ListarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Buscar Produtos" data-rel="tooltip"
                            CausesValidation="False" OnClick="ListarLinkButton_Click"> 
                            Buscar Produtos </asp:LinkButton>

                        <asp:LinkButton ID="VoltarLinkButton" class="btn btn-success btn-labeled fa fa-mail-reply fa-lg"
                            runat="server" title="Retornar Lista" data-rel="tooltip"
                            CausesValidation="False" OnClick="VoltarLinkButton_Click"> 
                            Retornar Lista </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--===================================================-->
        <!--End Painel e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="ProdutosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ProdutosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Produtos
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="ListaProdutosGridView" EmptyDataText="Nenhum Pedidos Localizado"
                                AutoGenerateColumns="False" runat="server" AllowPaging="True" OnPageIndexChanging="ListaPedidosGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnSelectedIndexChanged="ListaProdutosGridView_SelectedIndexChanged">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpCod" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="ProdCodEstrCodLabel" runat="server" Text='<%# Bind("ProdCodEstr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Selecione" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ProdutoCheckBox" runat="server" OnCheckedChanged="ProdutoCheckBox_CheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="400px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Código Produto">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaNumLabel" runat="server" Text='<%# Bind("ProdCodEstr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="400px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome Do Produto">
                                        <ItemTemplate>
                                            <asp:Label ID="EntNomeLabel" runat="server" Text='<%# Bind("ProdNome") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
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
    <!----PAINEL----->
    <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    <!--Inicia Scrip para Tratar o combo no Modal-->


    <asp:HiddenField ID="EmpCodHiddenField" runat="server" />
    <asp:HiddenField ID="PedVendaNumHiddenField" runat="server" />
    <asp:HiddenField ID="TipoHiddenField" runat="server" />
</asp:Content>
