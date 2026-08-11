<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="PedidosWebForm.aspx.cs" Inherits="VendasWeb.financeiro.PedidosWebForm" %>

<%@ Register Src="~/usercontrol/ContaCorrenteWebUserControl.ascx" TagPrefix="uc1" TagName="ContaCorrenteWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/PedidosJavaScript.js")%>" type="text/javascript"></script>

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
                    <h3 class="panel-title">Financeiro - Conta Corrente Clientes - Pedidos</h3>
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

                    <%--LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoLabel" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="CodigoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NomeLabel" runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="NomeTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%--LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CNPJLabel" runat="server" Text="CNPJ:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="CNPJTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="VendedorLabel" runat="server" Text="Vendedor:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="VendedorTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%--LINHA 3--%>
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--LINHA 4--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PedidoSAPLabel" runat="server" Text="Pedido SAP:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="PedidoSAPTextBox"
                                    onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PedidoCRMLabel" runat="server" Text="Pedido CRM:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="PedidoCRMTextBox"
                                    onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                </div>


            </div>

            <%--BUTTONS--%>
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                                <asp:LinkButton ID="RetornarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BuscarButton" />
                                <asp:PostBackTrigger ControlID="RetornarButton" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--  END BUTTONS--%>
        </div>

        <%--MULTIVIEW--%>
        <asp:MultiView ID="CCPedidosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="CCPedidosView" runat="server">

                <div class="panel">
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="CCPedidosTesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="CCPedidosGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                        runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="CCPedidosGridView_PageIndexChanging">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Empresa" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="EmpresaLabel" runat="server" Text='<%# Bind("Empresa") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pedido SAP" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="PedidoSAPGridLabel" runat="server" Text='<%# Bind("PedidoSAP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pedido CRM" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="PedidoCRMGridLabel" runat="server" Text='<%# Bind("PedidoCRM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Emissão" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="EmissaoLabel" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(Eval("DataEmissao"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Valor" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="TotalPedido" runat="server" Text='<%# String.Format("{0:C}", Convert.ToDouble(Eval("TotalPedido"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Detalhes">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-info fa fa-arrow-right" CausesValidation="false" runat="server"
                                                                OnClientClick='<%# string.Format("ConsultaNotaDetalhe("+Eval("PedidoSAP")+")")%>'></asp:LinkButton>
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
        <%--END MULTIVIEW--%>
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
                                            <th style="width: 50%;">Pedido SAP</th>
                                            <th style="width: 50%;">Pedido CRM</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="PedidoSAPModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="PedidoCRMModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 3--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Data Emissão</th>
                                            <th style="width: 50%;">Total Pedido</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="DataEmissaoModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="TotalPedidoModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 4--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Histórico</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="HistoricoModalLabel"></asp:Label></td>
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

    <uc1:ContaCorrenteWebUserControl runat="server" ID="ContaCorrenteWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
