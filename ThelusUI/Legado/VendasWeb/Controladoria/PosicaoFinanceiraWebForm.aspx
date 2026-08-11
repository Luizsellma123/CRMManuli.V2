<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="PosicaoFinanceiraWebForm.aspx.cs" Inherits="VendasWeb.Controladoria.PosicaoFinanceiraWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlControladoria.ascx" TagPrefix="uc1" TagName="WebUserControlControladoria" %>

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
                    <h3 class="panel-title">Controladoria - Posição Financeira</h3>
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
                                <asp:Label ID="PeriodoInicialLabel" runat="server" Text="Período Inicial:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="PeriodoInicialTextBox" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PeriodoFinalLabel" runat="server" Text="Período Final:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="PeriodoFinalTextBox" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="UsuarioLabel" runat="server" Text="Usuário:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="UsuarioTextBox" runat="server"></asp:TextBox>
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

                        <asp:UpdatePanel ID="PosicaoFinanceiraUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="RetornarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="NovaAnaliseLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                    CausesValidation="false" runat="server" ClientIDMode="Static"
                                    OnClientClick="MostraModalGeracaoPosicaoFinanceira(this.id);">Nova Análise</asp:LinkButton>

                                <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RetornarButton" />
                                <asp:AsyncPostBackTrigger ControlID="NovaAnaliseLinkButton" />
                                <asp:PostBackTrigger ControlID="BuscarButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="PosicaoFinanceiraMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="PosicaoFinanceiraView" runat="server">
                <div class="panel">

                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="PosicaoFinanceiraGridView" EmptyDataText="Não foi possível encontrar nenhuma Posição Financeira" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="PosicaoFinanceiraGridView_PageIndexChanging" Visible="true">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Posição" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="PosicaoGridViewLabel" runat="server" Text='<%# Bind("Posicao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Geração" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="GeracaoGridViewLabel" runat="server" Text='<%# Bind("Geracao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Usuário">
                                        <ItemTemplate>
                                            <asp:Label ID="UsuarioGridViewLabel" runat="server" Text='<%# Bind("Usuario") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Detalhes" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="DetalhesUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-info fa fa-edit"
                                                            CausesValidation="false" runat="server" OnClick="DetalhesLinkButton_Click"></asp:LinkButton>
                                                    </center>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DetalhesLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
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

    <uc1:WebUserControlControladoria runat="server" ID="WebUserControlControladoria" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

    <%--Modal--%>
    <div>

        <div id="GeracaoPosicaoFinanceiraModal" class="modal fade bd-example-modal-xl">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">

                    <div id="GeracaoPosicaoFinanceiraModalBody" class="modal-body">

                        <h4 class="m-t-0 header-title">Geração Posição Financeira:</h4>

                        <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Período Inicial:" Font-Size="Larger"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="PeriodoInicialModalTextBox" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Período Final:" Font-Size="Larger"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="PeriodoFinalModalTextBox" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="modal-footer">

                        <asp:LinkButton ID="RetornarModalLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" data-dismiss="modal">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="GerarModalLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                            CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="GerarModalLinkButton_Click">Gerar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <script>

            function MostraModalGeracaoPosicaoFinanceira(linkButtonId) {

                if (linkButtonId != null) {

                    $('#GeracaoPosicaoFinanceiraModal').modal();

                    callback();

                }
            }

        </script>

    </div>

</asp:Content>
