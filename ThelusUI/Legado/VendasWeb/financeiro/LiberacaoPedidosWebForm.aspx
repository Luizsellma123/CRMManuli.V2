<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="LiberacaoPedidosWebForm.aspx.cs" Inherits="VendasWeb.financeiro.LiberacaoPedidosWebForm" %>

<%@ Register Src="~/usercontrol/FinanceiroWebUserControl.ascx" TagPrefix="uc1" TagName="FinanceiroWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LblClasse" runat="server" Text="Empresa :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SituacaoLabel" runat="server" Text="Situação :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="SituacaoDropDownList" runat="server" CssClass="form-control">
                                        <asp:ListItem>Todos</asp:ListItem>
                                        <asp:ListItem>Pendentes</asp:ListItem>
                                        <asp:ListItem>Autorizados</asp:ListItem>
                                        <asp:ListItem>Recusados</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ClienteLabel" runat="server" Text="Cliente :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="ClienteTextBox" runat="server" CssClass="form-control" placeholder="Nome ou Código"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PedidoCRMLabel" runat="server" Text="Pedido CRM: "></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="PedidoCRMTextBox" runat="server" CssClass="form-control" placeholder="Informe Número Pedido do CRM."></asp:TextBox>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PedidoSAPLabel" runat="server" Text="Pedido SAP: "></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="PedidoSAPTextBox" runat="server" CssClass="form-control" placeholder="Informe Número Pedido do SAP."></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EsbocoLabel" runat="server" Text="Esboco SAP: "></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="NumeroEsbocoTextBox" runat="server" CssClass="form-control" placeholder="Informe Número Esboço do SAP."></asp:TextBox>
                                </div>
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
                        <asp:LinkButton ID="BuscarButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="PedidosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="PedidosView" runat="server">
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

                            <asp:GridView ID="PedidosGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="PedidosGridView_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Acessar">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="AcessarLinkButton" class="btn btn-info fa fa-arrow-right"
                                                        CausesValidation="false" runat="server" OnClick="AcessarButton_Click"></asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="AcessarLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Emp.">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpresaLabel" runat="server" Text='<%# Bind("Empresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CRM ">
                                        <ItemTemplate>
                                            <asp:Label ID="PedidoCRM" runat="server" Text='<%# Bind("PedidoCRM") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SAP ">
                                        <ItemTemplate>
                                            <asp:Label ID="PedidoSAPLabel" runat="server" Text='<%# Bind("PedidoSAP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Esboço" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="ChaveEsbocoLabel" runat="server" Text='<%# Bind("ChaveEsboco") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cliente">
                                        <ItemTemplate>
                                            <asp:Label ID="ClienteLabel" runat="server" Text='<%# Bind("NomeCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Situação ">
                                        <ItemTemplate>
                                            <asp:Label ID="SituacaoLabel" runat="server" Text='<%# Bind("Situacao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Historico: ">
                                        <ItemTemplate>
                                            <asp:Label ID="HistoricoLabel" runat="server" Text='<%# Bind("Historico") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Aberto">
                                        <ItemTemplate>
                                            <asp:Label ID="TotalPedidoLabel" runat="server" Text='<%# String.Format("{0:C}", Convert.ToDouble(Eval("TotalPedido"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
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
