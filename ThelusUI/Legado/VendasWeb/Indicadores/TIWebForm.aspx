<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="TIWebForm.aspx.cs" Inherits="VendasWeb.Indicadores.TIWebForm" %>

<%@ Register Src="~/usercontrol/IndicadoresWebUserControl.ascx" TagPrefix="uc1" TagName="IndicadoresWebUserControl" %>

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
                    <h3 class="panel-title">Indicadores - Tecnologia Informação</h3>
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
                                <asp:Label ID="ResponsavelLabel" runat="server" Text="Responsável:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ResponsavelDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SolicitanteLabel" runat="server" Text="Solicitante:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="SolicitanteDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataInicioLabel" runat="server" Text="Data Inicial:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DataInicialTextBox" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataFinalLabel" runat="server" Text="Data Final:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DataFinalTextBox" TextMode="Date" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <%--<div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="StatusLabel" runat="server" Text="Status:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SistemaLabel" runat="server" Text="Sistema:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="SistemaDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
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
                    <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="panel-control">
                                <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="RelatorioPDFLinkButton" class="btn btn-success btn-labeled fa fa-cloud-download fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RelatorioPDFLinkButton_Click">Relatorio PDF</asp:LinkButton>

                                <asp:LinkButton ID="BuscarLinkButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BuscarLinkButton_Click">Buscar</asp:LinkButton>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                            <asp:PostBackTrigger ControlID="RelatorioPDFLinkButton" />
                            <asp:PostBackTrigger ControlID="BuscarLinkButton" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="MultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="View" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="GridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="GridView_PageIndexChanging" Visible="true"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sistema">
                                        <ItemTemplate>
                                            <asp:Label ID="SistemaLabel" runat="server" Text='<%# Bind("Sistema") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Abertas">
                                        <ItemTemplate>
                                            <asp:Label ID="AbertasLabel" runat="server" Text='<%# Bind("Abertas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Finalizadas">
                                        <ItemTemplate>
                                            <asp:Label ID="FinalizadasLabel" runat="server" Text='<%# Bind("Finalizadas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Homologadas">
                                        <ItemTemplate>
                                            <asp:Label ID="HomologadasLabel" runat="server" Text='<%# Bind("Homologadas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Ano">
                                        <ItemTemplate>
                                            <asp:Label ID="TotalAnoLabel" runat="server" Text='<%# Bind("TotalAbertasAno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Finalizadas Ano">
                                        <ItemTemplate>
                                            <asp:Label ID="FinalizadasAnoLabel" runat="server" Text='<%# Bind("TotalFinalizadasAno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Homologadas Ano">
                                        <ItemTemplate>
                                            <asp:Label ID="HomologadasAnoLabel" runat="server" Text='<%# Bind("TotalHomologadasAno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
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

    <uc1:IndicadoresWebUserControl runat="server" ID="IndicadoresWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>

