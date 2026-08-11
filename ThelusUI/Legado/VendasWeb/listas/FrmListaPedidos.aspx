<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true"
    CodeBehind="FrmListaPedidos.aspx.cs" Inherits="VendasWeb.listas.FrmListaPedidos" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script language="javascript" src="../js/cadArtePedido.js" type="text/javascript"></script>
    <script language="javascript" src="../js/ListaPedidosJavaScript.js?aux=1" type="text/javascript"></script>

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
                    <h3 class="panel-title">Selecionar Pedidos</h3>
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

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Empresa:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpEmpresa" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Status:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpListFiltroStat" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Tipo Filtro:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="drpListFiltroPri" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Selected="True">Cód. Cliente</asp:ListItem>
                                    <asp:ListItem Value="2">Nome</asp:ListItem>
                                    <asp:ListItem Value="3">Número CRM</asp:ListItem>
                                    <asp:ListItem Value="6">Número SAP</asp:ListItem>
                                    <asp:ListItem Value="4">Nota Fiscal</asp:ListItem>
                                    <asp:ListItem Value="5">Produto</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="FiltroLabel" runat="server" Text="Filtro:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataInicialLabel" runat="server" Text="Data Inicial: "></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="DataInicialTextBox" TextMode="date" runat="server" CssClass="form-control" placeholder="Data inicial."></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataFinalLabel" runat="server" Text="Data Final: "></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="DataFinalTextBox" TextMode="date" runat="server" CssClass="form-control" placeholder="Informe Número Esboço do SAP."></asp:TextBox>
                                </div>
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
                        <asp:LinkButton ID="btnListar" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Buscar Pedidos" data-rel="tooltip" OnClick="btnListar_Click"
                            CausesValidation="False"> 
             Buscar Pedidos </asp:LinkButton>
                        <asp:LinkButton ID="IncluirProdutoLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                            runat="server" title="Incluir Produto" data-rel="tooltip"
                            CausesValidation="False" OnClick="IncluirProdutoLinkButton_Click" Visible="false"> 
             Incluir Produto </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <!--===================================================-->
        <!--End Painel e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="PedidosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="PedidosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Pedidos
                        </h3>
                    </div>

                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="ListaPedidosGridView" EmptyDataText="Nenhum Pedido Localizado"
                                AutoGenerateColumns="False" runat="server" AllowPaging="True" OnPageIndexChanging="ListaPedidosGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%"
                                OnRowDataBound="ListaPedidosGridView_RowDataBound">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="IDEmpresa" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpCodLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="NumeroEsbocoSAP" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="NumeroEsbocoSAPLabel" runat="server" Text='<%# Bind("NumeroEsbocoSAP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ped. CRM">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaNumLabel" runat="server" Text='<%# Bind("IDPedido") %>'></asp:Label>
                                            <asp:Label ID="SituaCaoLabel" Visible="false" runat="server" Text='<%# Bind("DescricaoStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ped. SAP">
                                        <ItemTemplate>
                                            <asp:Label ID="NumeroPedidoSAPLabel" runat="server" Text='<%# Bind("NumeroPedidoSAP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Entidade">
                                        <ItemTemplate>
                                            <asp:Label ID="EntNomeLabel" runat="server" Text='<%# Bind("NomeCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entrada" SortExpression="PedVendaData">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaDataLabel" runat="server" Text='<%# Bind("DataLancamento", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Saida" SortExpression="NFHoraSaida">
                                        <ItemTemplate>
                                            <asp:Label ID="NFHoraSaidaLabel" runat="server" Text='<%# Bind("DataSaida", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>

                                    <%--                                    <asp:TemplateField HeaderText="Produto" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="ProdutoLabel" runat="server" Text='<%# Bind("Produto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaStatDescrLabel" runat="server" Text='<%# Bind("DescricaoStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nº OC">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaNumPedEntLabel" runat="server" Text='<%# Bind("NumeroPedidoCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Faturamento">
                                        <ItemTemplate>
                                            <asp:Label ID="DataFaturamentoLabel" runat="server" Text='<%# Bind("DataEmissao") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nota Fiscal">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="NumeroNotaFiscalUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="NumeroNotaFiscalLinkButton" runat="server" Text='<%# Bind("NumeroNotaFiscal") %>'
                                                        OnClick="RastrearLinkButton_Click"
                                                        OnClientClick='<%# string.Format("ConsultaNota("+Eval("IDEmpresa")+","+Eval("NumeroPrimarioNota")+","+Eval("NumeroNotaFiscal")+","+Eval("NumeroPedidoSAP")+")")%>'>LinkButton</asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="NumeroNotaFiscalLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Detalhe" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <center>
                                                <%--<asp:Button ID="btnVerDetalhe" runat="server" Text="Ver Detalhe"
                                                        CssClass="btn btn-danger" OnClick="btnVerDetalhe_Click" />--%>
                                                <asp:LinkButton ID="VerdetalheLinkButton" runat="server" class="btn btn-primary fa fa-plus-square" OnClick="btnVerDetalhe_Click"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Rastrear" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="RastrearLinkButton" runat="server" class="btn btn-primary fa fa-truck" OnClick="RastrearLinkButton_Click"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Atualizar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="AtualizarUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <%--<asp:Button ID="btnVerDetalhe" runat="server" Text="Ver Detalhe"
                                                        CssClass="btn btn-danger" OnClick="btnVerDetalhe_Click" />--%>
                                                <ContentTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="AtualizarLinkButton" runat="server" class="btn btn-primary fa fa-refresh" OnClick="AtualizarLinkButton_Click"></asp:LinkButton>
                                                    </center>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="AtualizarLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Cópia">
                                        <ItemTemplate>
                                            <contenttemplate>
                                                <center>
                                                    <%--<asp:Button ID="btnVerDetalhe" runat="server" Text="Ver Detalhe"
                                                        CssClass="btn btn-danger" OnClick="btnVerDetalhe_Click" />--%>
                                                    <asp:LinkButton ID="CopiaLinkButton" runat="server" class="btn btn-primary fa fa-clone" OnClick="CopiaLinkButton_Click"></asp:LinkButton>
                                                </center>
                                            </contenttemplate>
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

    <div id="fullReservaModal" class="modal fade bd-example-modal-xl">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: 15px;">
                    <h4 id="modalTitle" class="modal-title" runat="server"></h4>
                    <button type="button" class="close" data-dismiss="modal"><span>×</span> <span class="sr-only">Fechar</span></button>
                </div>

                <div id="modalBody" class="modal-body">
                    <div class="loader" id="LoadingDados"></div>

                    <div class="table-responsive" id="DadosModal">
                        <div class="col-md-12 pad-top bg-gray" style="padding-right: 15px;">
                            <div class="row pad-lft pad-rgt">
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Empresa</th>
                                            <th>Nome da Empresa</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="EmpCod"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="EmpNome"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Cliente</th>
                                            <th>CNPJ</th>
                                            <th>Nome Cliente</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="EntCod"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="EntCpfCgc"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="EntNome"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Data Digitação</th>
                                            <th>Data Saida</th>
                                            <th>Previsão entrega</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="PedVendaData"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="NFHoraSaida"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="PrevisaoEntrega"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Endereço</th>
                                            <th>Bairro</th>
                                            <th>Cidade</th>
                                            <th>UF</th>
                                            <th>Cep</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="EntEnderCompleto"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="EntBair"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="CidNome"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="UfSigla"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="EntCep"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Condição Pagamento</th>
                                            <th>Natureza de Operação</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <!--<asp:Label runat="server" ID="CondPagCod"></asp:Label>-->

                                                <asp:Label runat="server" ID="CondPagPedVendaNome"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="PedVendaNatOpProd"></asp:Label>
                                                -
                                                <asp:Label runat="server" ID="NatOpNome"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Código Vendedor</th>
                                            <th>Nome Vendedor</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="VendCod"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="VendNome"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <asp:Label runat="server" ID="ItensFormatados"></asp:Label>

                                <!--<table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Código</th>
                                            <th>Descrição</th>
                                            <th>UN</th>
                                            <th>Quantidade</th>
                                            <th>Valor Unitário</th>
                                            <th>Total S/IPI</th>
                                            <th>Total Geral</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            
                                        </tr>
                                    </tbody>
                                </table>-->

                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Mercadoria</th>
                                            <th>IPI</th>
                                            <th>ICMS</th>
                                            <th>Diferimento</th>
                                            <!--<th>ICMS Devido</th>-->
                                            <th>Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="PedVendaValMerc"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="PedVendaValIpiCalc"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="PedVendaValIcms"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="IcmsDiferido"></asp:Label></td>
                                            <!--<td><asp:Label runat="server" ID="IcmsDevido"></asp:Label></td>-->
                                            <td>
                                                <asp:Label runat="server" ID="PedVendaValTotal"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Frete</th>
                                            <th>Transportadora</th>
                                            <th>Nome</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="PedVendaStatFrete"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="EntCodTransp"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="EntNomeTransp"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Observação</th>
                                            <th>Histórico</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <textarea runat="server" name="demo-textarea-input" rows="6" readonly="true" cols="250" class="form-control" placeholder="" id="PedVendaTexto"></textarea></td>
                                            <td>
                                                <textarea runat="server" name="demo-textarea-input" rows="6" readonly="true" cols="350" class="form-control" id="PedVendaTextoHist"></textarea></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <!--<table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Histórico Liberações</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <textarea runat="server" name="demo-textarea-input" rows="6" readonly="true" cols="250" class="form-control" placeholder="" id="HistoricoLiberacoesTextarea"></textarea>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>-->

                                <!--<table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Cliche</th>
                                            <th>Nome Cliche</th>
                                            <th>Detalhe</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">


                                            <th>
                                                <asp:Label runat="server" ID="ClicheFormatados"></asp:Label>
                                                <a href="#" class="imgedit">
                                                    <img src="../imagens/search.png" alt="Consulta" border="0" onclick="javascript: return abrirArte( 99822 )"></a></th>
                                        </tr>
                                    </tbody>
                                </table>-->
                            </div>
                        </div>
                    </div>

                </div>

                <div class="modal-footer">

                    <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>

                    <asp:LinkButton ID="RastrearModalLinkButton" runat="server"
                        class="btn btn-success" OnClick="RastrearModalLinkButton_Click">Rastrear</asp:LinkButton>

                </div>
            </div>
        </div>
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
    <asp:HiddenField ID="HiddenFieldListaProdutos" runat="server" />

</asp:Content>
