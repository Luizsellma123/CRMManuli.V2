<%@ page title="" language="C#" masterpagefile="~/NestedMasterPageCRM.master" autoeventwireup="true" codebehind="NegociacaoDetalheWebForm.aspx.cs" inherits="VendasWeb.Negociacao.NegociacaoDetalheWebForm" %>

<%@ register src="~/usercontrol/FinanceiroWebUserControl.ascx" tagprefix="uc1" tagname="FinanceiroWebUserControl" %>

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
                <div class="panel-heading">
                    <div class="panel-control">
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                    </div>
                    <h3 class="panel-title">Negociação - Detalhe</h3>
                </div>

                <!--Painel FILTROS / CAMPOS-->
                <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse in' aria-expanded='true'>"
                    runat="server"></asp:Literal>
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

                    <!-- LINHA 2: Negociação / Situação -->
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
                                <asp:Label runat="server" Text="Situação:" AssociatedControlID="drpSituacao"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpSituacao" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 3: Solicitante / Data -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Solicitante:" AssociatedControlID="drpSolicitante"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpSolicitante" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Data:" AssociatedControlID="txtData"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="txtData" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 4: Estado / Município / Cidade -->
                    <asp:UpdatePanel ID="updLocalizacao" runat="server" RenderMode="Inline">
                        <contenttemplate>
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Estado:" AssociatedControlID="drpEstado"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="drpEstado" runat="server"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="drpEstado_SelectedIndexChanged"
                                            CssClass="form-control selectpicker"
                                            data-live-search="true" data-style="btn-primary" title="Escolha...">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Município:" AssociatedControlID="drpMunicipio"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="drpMunicipio" runat="server"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="drpMunicipio_SelectedIndexChanged"
                                            CssClass="form-control selectpicker"
                                            data-live-search="true" data-style="btn-primary" title="Escolha...">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <!-- CAMPO DE CIDADE (Abaixo de Estado/Município) -->
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Cidade :" AssociatedControlID="txtCidade"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtCidade" runat="server" CssClass="form-control" placeholder="Cidade..."></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </contenttemplate>
                    </asp:UpdatePanel>

                    <!-- LINHA 5: Forma Pgto (Tela Inteira) -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Forma Pgto:" AssociatedControlID="txtFormaPagamento"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox ID="txtFormaPagamento" runat="server" CssClass="form-control" placeholder="Informe a forma de pagamento..."></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 6: Cliente (Bloco unificado com altura alinhada ao input) -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Cliente:" AssociatedControlID="txtCliente"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <div class="form-group">
                                <div style="display: flex; align-items: center; gap: 10px; width: 100%;">
                                    <!-- Caixa de texto principal -->
                                    <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" placeholder="Informe o cliente..." style="flex: 1;"></asp:TextBox>

                                    <!-- Bloco unificado com altura fixa idêntica aos inputs (34px) -->
                                    <div style="display: flex; align-items: center; gap: 10px; border: 1px solid #d2d6de; border-radius: 4px; padding: 0 10px; height: 34px; background-color: #fcfcfc;">
                                        <asp:Button ID="btnProcurarCliente" runat="server" Text="Procurar" CssClass="btn btn-primary" style="padding: 3px 10px; font-size: 12px;" CausesValidation="false" OnClick="btnProcurarCliente_Click" />
                                        <div style="white-space: nowrap;">
                                            <asp:CheckBox ID="chkNovo" runat="server" Text=" &nbsp;Cliente Novo" AutoPostBack="true" OnCheckedChanged="chkNovo_CheckedChanged" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 7: Regime / Vendedor -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Regime:" AssociatedControlID="drpRegime"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpRegime" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Vendedor:" AssociatedControlID="drpVendedor"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpVendedor" runat="server"
                                    CssClass="form-control selectpicker"
                                    data-live-search="true"
                                    data-style="btn-primary"
                                    title="Escolha um vendedor...">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 8: Clas. Comercial. -->
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Clas. Comercial.:" AssociatedControlID="drpClasComercial"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpClasComercial" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 9: Frete / Validade -->
                    <div class="row">
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
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Validade:" AssociatedControlID="drpValidade"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpValidade" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 10: Observação -->
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Observação:" AssociatedControlID="txtObservacao"></asp:Label>
                                <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!-- LINHA 11: Histórico -->
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Histórico:" AssociatedControlID="txtHistorico"></asp:Label>
                                <asp:TextBox ID="txtHistorico" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <!-- Panel Footer com todos os botões do protótipo -->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                            <contenttemplate>
                                <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">
                                    Retornar</asp:LinkButton>

                                <asp:LinkButton ID="RetornarNegociacaoLinkButton" class="btn btn-warning btn-labeled fa fa-rotate-left fa-lg"
                                    runat="server" OnClick="RetornarNegociacaoLinkButton_Click">
                                    Retornar Negociacao</asp:LinkButton>

                                <asp:LinkButton ID="PerderVendaLinkButton" class="btn btn-warning btn-labeled fa fa-times-circle fa-lg"
                                    runat="server" OnClick="PerderVendaLinkButton_Click">
                                    Perder Venda</asp:LinkButton>

                                <asp:LinkButton ID="ReprovarLinkButton" class="btn btn-danger btn-labeled fa fa-close fa-lg"
                                    runat="server" OnClick="ReprovarLinkButton_Click">
                                    Reprovar</asp:LinkButton>

                                <asp:LinkButton ID="AprovarLinkButton" class="btn btn-success btn-labeled fa fa-check fa-lg"
                                    runat="server" OnClick="AprovarLinkButton_Click">
                                    Aprovar</asp:LinkButton>

                                <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success btn-labeled fa fa-floppy-o fa-lg"
                                    runat="server" OnClick="SalvarLinkButton_Click">
                                    Gravar</asp:LinkButton>
                            </contenttemplate>
                            <triggers>
                                <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                <asp:PostBackTrigger ControlID="RetornarNegociacaoLinkButton" />
                                <asp:PostBackTrigger ControlID="PerderVendaLinkButton" />
                                <asp:PostBackTrigger ControlID="ReprovarLinkButton" />
                                <asp:PostBackTrigger ControlID="AprovarLinkButton" />
                                <asp:PostBackTrigger ControlID="SalvarLinkButton" />
                            </triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <!-- MultiView de Apurações / Opcionais se necessário -->
        <asp:MultiView ID="AprovacoesMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="AprovacoesView" runat="server">
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Pedidos Liberar</h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="AprovacoesGridView" EmptyDataText="Não foi possível encontrar nenhuma regra para autorizar." AutoGenerateColumns="False"
                                runat="server" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <pagerstyle cssclass="pagination-ys" />
                                <columns>
                                    <asp:TemplateField HeaderText="Código">
                                        <itemtemplate>
                                            <asp:Label ID="CodigoLabel" runat="server" Text='<%# Bind("Codigo") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome Regra">
                                        <itemtemplate>
                                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descrição Regra">
                                        <itemtemplate>
                                            <asp:Label ID="DescricaoLabel" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                </columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

    </div>

    <!-- Modal de Pesquisa de Cliente -->
    <div id="modalCliente" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Pesquisar Cliente</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updModalCliente" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
                            <!-- Filtro de Pesquisa alinhado com a grid -->
                            <div class="row" style="margin-bottom: 15px;">
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtFiltroCliente" runat="server" CssClass="form-control" placeholder="Digite o nome, código SAP ou CNPJ do cliente..."></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button ID="btnFiltrarModal" runat="server" Text="Pesquisar" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnFiltrarModal_Click" />
                                        </span>
                                    </div>
                                </div>
                            </div>

                            <!-- Grid de Resultados -->
                            <div class="table-responsive">
                                <!-- Grid de Resultados com Paginação -->
                                <div class="table-responsive">
                                    <asp:GridView ID="gridClientesModal" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-hover table-striped table-bordered"
                                        DataKeyNames="Id"
                                        AllowPaging="True"
                                        PageSize="10"
                                        OnPageIndexChanging="gridClientesModal_PageIndexChanging"
                                        OnSelectedIndexChanged="gridClientesModal_SelectedIndexChanged"
                                        EmptyDataText="Nenhum cliente encontrado.">

                                        <pagerstyle cssclass="pagination-ys" />

                                        <columns>
                                            <asp:CommandField ShowSelectButton="True" SelectText="Selecionar" ControlStyle-CssClass="btn btn-success btn-xs" />
                                            <asp:BoundField DataField="CodigoSAP" HeaderText="Cód. SAP" />
                                            <asp:BoundField DataField="Nome" HeaderText="Nome / Razão Social" />
                                            <asp:BoundField DataField="CNPJ" HeaderText="CNPJ" />
                                        </columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <uc1:financeirowebusercontrol runat="server" id="FinanceiroWebUserControl" />
</asp:Content>
