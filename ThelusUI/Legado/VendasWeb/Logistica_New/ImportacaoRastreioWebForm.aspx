<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ImportacaoRastreioWebForm.aspx.cs" Inherits="VendasWeb.Logistica_New.ImportacaoRastreioWebForm" %>

<%@ Register Src="~/usercontrol/LogisticaWebUserControl.ascx" TagPrefix="uc1" TagName="LogisticaWebUserControl" %>

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
                    <h3 class="panel-title">Logística - Rastreio Pedidos - Importação</h3>
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

                                <asp:LinkButton ID="SubirDadosLinkButton" class="btn btn-primary btn-labeled fa fa-cloud-upload fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="SubirDadosLinkButton_Click">Subir Dados</asp:LinkButton>

                                <asp:LinkButton ID="AtualizarLinkButton" class="btn btn-success btn-labeled fa fa-refresh fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="AtualizarLinkButton_Click">Atualizar</asp:LinkButton>

                                <asp:LinkButton ID="LimparDadosLinkButton" class="btn btn-danger btn-labeled fa fa-times fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="LimparDadosLinkButton_Click">Limpar Dados</asp:LinkButton>

                                <asp:LinkButton ID="ModeloLinkButton" class="btn btn-warning btn-labeled fa fa-table fa-lg"
                                    CausesValidation="false" runat="server" OnClick="ModeloLinkButton_Click">Modelo</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                <asp:PostBackTrigger ControlID="SubirDadosLinkButton" />
                                <asp:PostBackTrigger ControlID="AtualizarLinkButton" />
                                <asp:PostBackTrigger ControlID="LimparDadosLinkButton" />
                                <asp:PostBackTrigger ControlID="ModeloLinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="ImportacaoRastreioMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ImportacaoRastreioView" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="ImportacaoRastreioUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="ImportacaoRastreioGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação"
                                        AutoGenerateColumns="False" OnPageIndexChanging="ImportacaoRastreioGridView_PageIndexChanging" Visible="true"
                                        runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%"
                                        CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head
                                         table-no-inner-border table-hover table-condensed">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="IDEmpresa" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDEmpresaGridViewLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nota Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="NotaSerialGridViewLabel" runat="server" Text='<%# Bind("NOTA_FISCAL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDCliente" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDClienteGridViewLabel" runat="server" Text='<%# Bind("IDCliente") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cliente">
                                                <ItemTemplate>
                                                    <asp:Label ID="ClienteGridViewLabel" runat="server" Text='<%# Bind("Cliente") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDPedido">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDPedidoGridViewLabel" runat="server" Text='<%# Bind("IDPedido") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Previsão Entrega">
                                                <ItemTemplate>
                                                    <asp:Label ID="PrevisaoEntregaGridViewLabel" runat="server" Text='<%# Bind("PrevisaoEntrega") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Histórico">
                                                <ItemTemplate>
                                                    <asp:Label ID="HistoricoGridViewLabel" runat="server" Text='<%# Bind("Historico") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDEvento" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDEventoGridViewLabel" runat="server" Text='<%# Bind("IDEvento") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Evento">
                                                <ItemTemplate>
                                                    <asp:Label ID="EventoGridViewLabel" runat="server" Text='<%# Bind("Evento") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDCategoria" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDCategoriaGridViewLabel" runat="server" Text='<%# Bind("IDCategoria") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Categoria">
                                                <ItemTemplate>
                                                    <asp:Label ID="CategoriaGridViewLabel" runat="server" Text='<%# Bind("Categoria") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ImportacaoRastreioGridView" />
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

    <uc1:LogisticaWebUserControl runat="server" ID="LogisticaWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>
