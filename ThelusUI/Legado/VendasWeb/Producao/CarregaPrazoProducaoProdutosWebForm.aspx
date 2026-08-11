<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CarregaPrazoProducaoProdutosWebForm.aspx.cs" Inherits="VendasWeb.Producao.CarregaPrazoProducaoProdutosWebForm" %>

<%@ Register Src="~/usercontrol/PrazoProducaoProdutosWebUserControl.ascx" TagPrefix="uc1" TagName="PrazoProducaoProdutosWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/bootstrap-filestyle.min.js")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(":file").filestyle({ buttonName: "btn-primary" });
    </script>
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
                    <h3 class="panel-title">Produção - Prazos Produção - Produtos - Carrega Prazos</h3>
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
                                <asp:Label runat="server" Text="Empresa:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ArquivoLabel" runat="server" Text="Arquivo:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-10">
                            <asp:FileUpload CssClass="filestyle" data-buttonName="btn-primary" ID="ArquivoFileUpload" runat="server" TabIndex="-1" Style="position: absolute; clip: rect(0px, 0px, 0px, 0px);" />
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

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="AtualizaPrazosLinkButton" class="btn btn-success btn-labeled fa fa-refresh fa-lg" Enabled="false"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="AtualizaPrazosLinkButton_Click">Atualiza Prazos</asp:LinkButton>

                                <asp:LinkButton ID="SubirDadosLinkButton" class="btn btn-primary btn-labeled fa fa-cloud-upload fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="SubirDadosLinkButton_Click">Subir Dados</asp:LinkButton>

                                <asp:LinkButton ID="ModeloLinkButton" class="btn btn-warning btn-labeled fa fa-table fa-lg"
                                    CausesValidation="false" runat="server" OnClick="ModeloLinkButton_Click">Modelo</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                <asp:PostBackTrigger ControlID="AtualizaPrazosLinkButton" />
                                <asp:PostBackTrigger ControlID="SubirDadosLinkButton" />
                                <asp:PostBackTrigger ControlID="ModeloLinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="ProducaoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ProducaoView" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="TesteUpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="ProducaoGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação"
                                        AutoGenerateColumns="False" OnPageIndexChanging="ProducaoGridView_PageIndexChanging" Visible="true"
                                        runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%"
                                        CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head
                                         table-no-inner-border table-hover table-condensed">

                                        <PagerStyle CssClass="pagination-ys" />

                                        <Columns>

                                            <asp:TemplateField HeaderText="IDEmpresa" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDEmpresaLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Empresa" ItemStyle-Width="35%">
                                                <ItemTemplate>
                                                    <asp:Label ID="EmpresaLabel" runat="server" Text='<%# Bind("Empresa") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Produto" ItemStyle-Width="35%">
                                                <ItemTemplate>
                                                    <asp:Label ID="ProdutoLabel" runat="server" Text='<%# Bind("Produto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Produção">
                                                <ItemTemplate>
                                                    <asp:Label ID="ProducaoLabel" runat="server" Text='<%# Bind("Producao") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Expedição">
                                                <ItemTemplate>
                                                    <asp:Label ID="ExpedicaoLabel" runat="server" Text='<%# Bind("Expedicao") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Estoque">
                                                <ItemTemplate>
                                                    <asp:Label ID="EstoqueLabel" runat="server" Text='<%# Bind("Estoque") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ProducaoGridView" />
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

    <uc1:PrazoProducaoProdutosWebUserControl runat="server" ID="PrazoProducaoProdutosWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>
