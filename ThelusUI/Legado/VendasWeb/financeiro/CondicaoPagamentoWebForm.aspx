<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CondicaoPagamentoWebForm.aspx.cs" Inherits="VendasWeb.financeiro.CondicaoPagamentoWebForm" %>

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
                    <h3 class="panel-title">Financeiro - Condições Pagamento A Vista</h3>
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
                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="CondicaoPagmentoLabel" runat="server" Text="Condição Pagamento :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-9">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="CondicaoPagamentoTextBox" runat="server" CssClass="form-control" placeholder="Nome ou Código"></asp:TextBox>
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
                        <asp:UpdatePanel ID="BotoesAVistaPanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:LinkButton ID="BuscarButton" CssClass="btn btn-success btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BuscarButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="CondicoesMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="CondicoesView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Condições Pagamento A Vista
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="GridUpdatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                <ContentTemplate>

                                    <asp:GridView ID="CondicoesGridView" EmptyDataText="Não foi possível encontrar nenhuma condição de pagamento" AutoGenerateColumns="False"
                                        runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="CondicoesGridView_PageIndexChanging">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="IDCondicao" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDCondPagLabel" runat="server" Text='<%# Bind("IDCondPag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Condição Pagamento ">
                                                <ItemTemplate>
                                                    <asp:Label ID="NomeCondicaoLabel" runat="server" Text='<%# Bind("NomeCondicao") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Liberado Política">
                                                <ItemTemplate>

                                                    <asp:UpdatePanel ID="AtivoUpdatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <div class="col-xs-5 text-left checkbox">
                                                                <label class="form-checkbox form-icon form-text">
                                                                    <asp:CheckBox ID="LiberadoPoliticaCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("LiberadoPolitica")) %>' OnCheckedChanged="LiberadoPoliticaCheckBox_CheckedChanged" AutoPostBack="true" />
                                                                </label>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="LiberadoPoliticaCheckBox" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="É A Vista">
                                                <ItemTemplate>

                                                    <asp:UpdatePanel ID="CondicaoAVistaPanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <div class="col-xs-5 text-left checkbox">
                                                                <label class="form-checkbox form-icon form-text">
                                                                    <asp:CheckBox ID="CondicaoAVistaCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("CondicaoAVista")) %>' AutoPostBack="true" OnCheckedChanged="CondicaoAVistaCheckBox_CheckedChanged" />
                                                                </label>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="CondicaoAVistaCheckBox" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ItemTemplate>

                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="CondicoesGridView" />
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

    <uc1:FinanceiroWebUserControl runat="server" ID="FinanceiroWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 

</asp:Content>
