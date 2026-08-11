<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="SituacoesAtividadesWebForm.aspx.cs" Inherits="VendasWeb.SAC.SituacoesAtividadesWebForm" %>

<%@ Register Src="~/usercontrol/SACWebUserControl.ascx" TagPrefix="uc1" TagName="SACWebUserControl" %>

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
                    <h3 class="panel-title">SAC - Situação Atividades</h3>
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
                                <asp:Label ID="SituacaoLabel" runat="server" Text="Situação:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="SituacaoTextBox" runat="server" CssClass="form-control"
                                        placeholder="Nome ou Código."></asp:TextBox>
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

                        <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                        <asp:LinkButton ID="NovoLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                            CausesValidation="false" runat="server" OnClick="NovoLinkButton_Click">Novo</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="SACMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="SACView" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="SACGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="SACGridView_PageIndexChanging" Visible="true"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="EditarLinkButton" class="btn btn-info fa fa-edit"
                                                    CausesValidation="false" runat="server" OnClick="EditarLinkButton_Click"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <center>
                                                <asp:Label ID="CodigoLabel" runat="server" Text='<%# Bind("Codigo") %>'></asp:Label>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Descrição">
                                        <ItemTemplate>
                                            <asp:Label ID="DescricaoLabel" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bloqueado Alteração">
                                        <ItemTemplate>
                                            <center>
                                                <asp:Label ID="BloqueadoAlteracaoLabel" runat="server" Text='<%# Bind("Bloqueado") %>'></asp:Label>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="140px" />
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

    <uc1:SACWebUserControl runat="server" ID="SACWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>
