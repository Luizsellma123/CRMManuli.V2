<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="EmpenhoEstoqueWebForm.aspx.cs" Inherits="VendasWeb.Controladoria.EmpenhoEstoqueWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlControladoria.ascx" TagPrefix="uc1" TagName="WebUserControlControladoria" %>

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
                    <h3 class="panel-title">Controladoria - Empenho Estoque
                    </h3>
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
                                <asp:Label ID="ProdutoLabel" runat="server" Text="Produto:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox ID="ProdutoTextBox" runat="server" CssClass="form-control"
                                    placeholder="Código ou nome."></asp:TextBox>
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

                        <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="ControladoriaMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ControladoriaView" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="FooterUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="ControladoriaGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                        runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="ControladoriaGridView_PageIndexChanging" Visible="true"
                                        CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="IDProduto" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDProdutoLabel" runat="server" Text='<%# Bind("IDProduto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Produto">
                                                <ItemTemplate>
                                                    <asp:Label ID="ProdutoLabel" runat="server" Text='<%# Bind("Produto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Estoque" DataField="Estoque" />

                                            <asp:BoundField HeaderText="Empenhada" DataField="Empenhada" />

                                            <asp:BoundField HeaderText="Saldo" DataField="Saldo" />

                                            <asp:TemplateField HeaderText="Pedidos">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="PedidosLinkButton" class="btn btn-info fa fa-edit"
                                                            CausesValidation="false" runat="server" OnClick="PedidosLinkButton_Click">
                                                        </asp:LinkButton>
                                                    </center>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ControladoriaGridView" />
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

    <uc1:WebUserControlControladoria runat="server" ID="WebUserControlControladoria" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>

