<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="PrazoProducaoProdutosWebForm.aspx.cs" Inherits="VendasWeb.Producao.PrazoProducaoProdutosWebForm" %>

<%@ Register Src="~/usercontrol/PrazoProducaoProdutosWebUserControl.ascx" TagPrefix="uc1" TagName="PrazoProducaoProdutosWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>

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
                    <h3 class="panel-title">Produção - Prazos Produção - Produtos</h3>
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

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ProdutoLabel" runat="server" Text="Produto:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ProdutoTextBox" runat="server"></asp:TextBox>
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

                        <asp:LinkButton ID="RetornarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClientClick="showProgress();"
                            OnClick="RetornarButton_Click">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClientClick="showProgress();"
                            OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="ProducaoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ProducaoView" runat="server">
                <div class="panel">

                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="ProducaoGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="ProducaoGridView_PageIndexChanging" Visible="true">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Excluir" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="ExcluirLinkButton" class="btn btn-danger fa fa-times"
                                                            CausesValidation="false" runat="server" OnClick="ExcluirLinkButton_Click"></asp:LinkButton>
                                                    </center>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="ExcluirLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDEmpresa" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDEmpresaLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Empresa">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpresaLabel" runat="server" Text='<%# Bind("Empresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

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

                                    <asp:TemplateField HeaderText="Produção" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" ID="ProducaoGridTextBox"
                                                onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                                Text='<%# Eval("QuantidadeDiasProducao") %>' OnTextChanged="ProducaoGridTextBox_TextChanged" runat="server"
                                                AutoPostBack="true"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Expedição" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" ID="ExpedicaoGridTextBox"
                                                onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                                Text='<%# Eval("QuantidadeDiasExpedicao") %>' OnTextChanged="ExpedicaoGridTextBox_TextChanged" runat="server"
                                                AutoPostBack="true"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Estoque" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" ID="EstoqueGridTextBox"
                                                onkeypress="mascara( this, mnumEvirgula );" onblur="mascara( this, mnumEvirgula );" onfocus="mascara( this, mnumEvirgula );"
                                                Text='<%# Eval("QuantidadeEstoque") %>' OnTextChanged="EstoqueGridTextBox_TextChanged" runat="server"
                                                AutoPostBack="true"></asp:TextBox>
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

    <uc1:PrazoProducaoProdutosWebUserControl runat="server" ID="PrazoProducaoProdutosWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

</asp:Content>
