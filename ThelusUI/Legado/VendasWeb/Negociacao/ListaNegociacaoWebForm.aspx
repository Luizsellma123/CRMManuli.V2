<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true"
    CodeBehind="ListaNegociacaoWebForm.aspx.cs" Inherits="VendasWeb.Negociacao.ListaNegociacaoWebForm" %>

<%@ Register Src="~/usercontrol/NegociacaoWebUserControl.ascx" TagPrefix="uc1" TagName="NegociacaoWebUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script language="javascript" src="../js/cadArtePedido.js" type="text/javascript"></script>
    <script language="javascript" src="../js/ListaPedidosJavaScript.js?aux=2" type="text/javascript"></script>
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
                <div class="panel-heading">
                    <div class="panel-control">
                        <button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay" data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                    <h3 class="panel-title">Lista Negociações</h3>
                </div>

                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>
                
                <div class="panel-body">
                    <!-- LINHA 1: Empresa -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Empresa:" AssociatedControlID="drpEmpresa"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="drpEmpresa" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 2: Usuário / Situação -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Usuário:" AssociatedControlID="drpUsuario"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpUsuario" runat="server" CssClass="form-control selectpicker"
                                    data-live-search="true" data-style="btn-primary" title="Escolha...">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Situação:" AssociatedControlID="drpListFiltroStat"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpListFiltroStat" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 3: Data Inicio / Fim -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataInicialLabel" runat="server" Text="Data Inicio:" AssociatedControlID="DataInicialTextBox"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="DataInicialTextBox" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataFinalLabel" runat="server" Text="Fim:" AssociatedControlID="DataFinalTextBox"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="DataFinalTextBox" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 4: Negociação / Frete -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Negociação:" AssociatedControlID="txtNegociacao"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="txtNegociacao" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Frete:" AssociatedControlID="drpFrete"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpFrete" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 5: Cliente -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="FiltroLabel" runat="server" Text="Cliente:" AssociatedControlID="txtFiltro"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Painel FILTROS-->
            <!--===================================================-->

            <!-- Panel Footer -->
            <!-- Botões de buscar -->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="btnListar" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Buscar Negociações" data-rel="tooltip" OnClick="btnListar_Click"
                            CausesValidation="False">
                            Buscar Negociações
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--===================================================-->
        <!--End Painel e Filtros (Fecha a col-sm-9)-->
        <!--===================================================-->

        <!-- LISTAGEM (GRIDVIEW) -->
        <asp:MultiView ID="NegociacaoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="NegociacaoView" runat="server">
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Negociações</h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="ListaNegociacoesGridView" EmptyDataText="Nenhuma Negociação Localizada"
                                AutoGenerateColumns="False" runat="server" AllowPaging="True"
                                OnPageIndexChanging="ListaNegociacaoGridView_PageIndexChanged"
                                DataKeyNames="IDEmpresa,IDNegociacao"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sel." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server"
                                                CssClass="btn btn-primary fa fa-pencil-square"
                                                ToolTip="Editar Negociação"
                                                CommandArgument='<%# Eval("IDNegociacao") %>'
                                                OnClick="btnEditar_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Negociação" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="IDNegociacaoLabel" runat="server" Text='<%# Eval("IDNegociacao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cliente" HeaderStyle-Width="45%">
                                        <ItemTemplate>
                                            <asp:Label ID="ClienteGridLabel" runat="server" Text='<%# Eval("ClienteGrid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="DataSolicitacaoLabel" runat="server" Text='<%# Eval("DataSolicitacao", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Situação" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="SituacaoLabel" runat="server" Text='<%# Eval("SituacaoDescricao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Detalhe" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="VerdetalheLinkButton" runat="server"
                                                CssClass="btn btn-primary fa fa-plus-square"
                                                ToolTip="Ver Detalhes Rápido"
                                                CommandArgument='<%# Eval("IDNegociacao") %>'
                                                OnClick="btnVerDetalhe_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </div> <!-- Fecha a Row Principal -->

    <!-- MODAL DETALHES -->
    <div id="fullReservaModal" class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span>&times;</span></button>
                    <h4 class="modal-title">Detalhes da Negociação nº <asp:Label ID="lblModalIDNegociacao" runat="server" Font-Bold="true"></asp:Label></h4>
                </div>

                <div class="modal-body">
                    <table class="table table-bordered table-condensed">
                        <thead>
                            <tr class="bg-primary text-white">
                                <th style="width: 50%;">Empresa / Filial</th>
                                <th style="width: 50%;">Solicitante</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><asp:Label ID="lblEmpresa" runat="server" /></td>
                                <td><asp:Label ID="lblSolicitante" runat="server" /></td>
                            </tr>
                        </tbody>
                    </table>

                    <table class="table table-bordered table-condensed">
                        <thead>
                            <tr class="bg-primary text-white">
                                <th style="width: 50%;">Cliente</th>
                                <th style="width: 20%;">Novo Cliente?</th>
                                <th style="width: 30%;">Vendedor</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><asp:Label ID="lblCliente" runat="server" /></td>
                                <td><asp:Label ID="lblClienteNovo" runat="server" /></td>
                                <td><asp:Label ID="lblVendedor" runat="server" /></td>
                            </tr>
                        </tbody>
                    </table>

                    <table class="table table-bordered table-condensed">
                        <thead>
                            <tr class="bg-primary text-white">
                                <th style="width: 50%;">Cidade / UF</th>
                                <th style="width: 50%;">Regime Tributário</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><asp:Label ID="lblCidadeUF" runat="server" /></td>
                                <td><asp:Label ID="lblRegimeTributario" runat="server" /></td>
                            </tr>
                        </tbody>
                    </table>

                    <table class="table table-bordered table-condensed">
                        <thead>
                            <tr class="bg-primary text-white">
                                <th>Condição Pagamento</th>
                                <th>Classificação Comercial</th>
                                <th>Tipo Frete</th>
                                <th>Validade</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><asp:Label ID="lblCondicaoPagamento" runat="server" /></td>
                                <td><asp:Label ID="lblClassificacaoComercial" runat="server" /></td>
                                <td><asp:Label ID="lblFrete" runat="server" /></td>
                                <td><asp:Label ID="lblValidade" runat="server" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <!----PAINEL----->
    <uc1:NegociacaoWebUserControl runat="server" ID="FinanceiroWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->

    <!-- Controles invisíveis para o Code-Behind -->
    <asp:HiddenField ID="EmpCodHiddenField" runat="server" />
    <asp:HiddenField ID="PedVendaNumHiddenField" runat="server" />
    <asp:HiddenField ID="TipoHiddenField" runat="server" />
    <asp:HiddenField ID="HiddenFieldListaProdutos" runat="server" />

</asp:Content>