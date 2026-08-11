<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ContaCorrenteWebForm.aspx.cs" Inherits="VendasWeb.financeiro.ContaCorrenteWebForm" %>

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
                    <h3 class="panel-title">Financeiro - Conta Corrente Clientes</h3>
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
                        <div class="col-lg-4">
                            <div class="form-group">
                                <!--Filtro Nome/Numero -->
                                <asp:DropDownList ID="FiltroDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Selected="True">Razão Social</asp:ListItem>
                                    <asp:ListItem Value="2">Cód. Cliente</asp:ListItem>
                                    <asp:ListItem Value="3">Nome Fantasia</asp:ListItem>
                                    <asp:ListItem Value="4">CNPJ/CPF</asp:ListItem>

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

        <asp:MultiView ID="ContaCorrenteMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ContaCorrenteView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="ContaCorrenteGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="ContaCorrenteGridView_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Acessar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AcessarLinkButton" class="btn btn-info fa fa-arrow-right"
                                                CausesValidation="false" runat="server" OnClick="AcessarLinkButton_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Vendedor" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="VendedorLabel" runat="server" Text='<%# Bind("Vendedor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoLabel" runat="server" Text='<%# Bind("CardCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome">
                                        <ItemTemplate>
                                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("CardName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CNPJ/CPF" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="CNPJouCPFLabel" runat="server" Text='<%# Bind("CNPJ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cidade" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="CidadeLabel" runat="server" Text='<%# Bind("Cidade") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Aprovados">
                                        <ItemTemplate>
                                            <asp:Label ID="AprovadosLabel" runat="server" Text='<%# String.Format("{0:C}", Convert.ToDouble(Eval("OrdersBal"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Limite">
                                        <ItemTemplate>
                                            <asp:Label ID="LimiteLabel" runat="server" Text='<%# String.Format("{0:C}", Convert.ToDouble(Eval("LimiteCredito"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Faturados">
                                        <ItemTemplate>
                                            <asp:Label ID="FaturadosLabel" runat="server" Text='<%# String.Format("{0:C}", Convert.ToDouble(Eval("PedidosFaturados"))) %>'></asp:Label>
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
