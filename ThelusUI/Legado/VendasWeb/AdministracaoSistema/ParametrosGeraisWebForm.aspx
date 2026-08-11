<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ParametrosGeraisWebForm.aspx.cs" Inherits="VendasWeb.AdministracaoSistema.ParametrosGeraisWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlAdministracaoSistema.ascx" TagPrefix="uc1" TagName="WebUserControlAdministracaoSistema" %>

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
                    <h3 class="panel-title">Administração - Lista Parâmetros</h3>
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
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ParametroLabel" runat="server" Text="Parâmetro:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="ParametroTextBox" runat="server" CssClass="form-control" placeholder="Nome ou Código."></asp:TextBox>
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
                        <asp:UpdatePanel ID="FooterUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="BuscarLinkButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BuscarLinkButton_Click">Buscar</asp:LinkButton>

                                <asp:LinkButton ID="NovoLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                    CausesValidation="false" runat="server" OnClick="NovoLinkButton_Click">Novo</asp:LinkButton>

                                <asp:LinkButton ID="RetornarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BuscarLinkButton" />
                                <asp:PostBackTrigger ControlID="NovoLinkButton" />
                                <asp:PostBackTrigger ControlID="RetornarButton" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <!-- TABELA -->
            <!--===================================================-->
            <asp:MultiView ID="ParametrosGeraisMultiView" runat="server" ActiveViewIndex="0" Visible="false">
                <asp:View ID="ParametrosGeraisView" runat="server">
                    <div class="panel">
                        <!-- Foo Table - Filtering -->
                        <!--===================================================-->
                        <div class="panel-body">
                            <div class="table-responsive">

                                <asp:GridView ID="ParametrosGeraisGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                    runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                    Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="ParametrosGeraisGridView_PageIndexChanging" Visible="true">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sel.">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="SelUpdatePanel" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="SelLinkButton" class="btn btn-info fa fa-edit"
                                                            CausesValidation="false" runat="server" OnClick="SelLinkButton_Click"></asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="SelLinkButton" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10px" />
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

                                        <asp:TemplateField HeaderText="IDParametro" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="IDParametroLabel" runat="server" Text='<%# Bind("IDParametro") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nome" HeaderStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IDModulo" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="IDModuloLabel" runat="server" Text='<%# Bind("IDModulo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modulo">
                                            <ItemTemplate>
                                                <asp:Label ID="ModuloLabel" runat="server" Text='<%# Bind("Modulo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descrição" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="DescricaoLabel" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="50%" HeaderText="Valor Txt.">
                                            <ItemTemplate>
                                                <asp:Label ID="ValorTextoLabel" runat="server" Text='<%# Bind("ValorTexto") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Valor Num.">
                                            <ItemTemplate>
                                                <asp:Label ID="ValorNumericoLabel" runat="server"
                                                    Text='<%# String.Format("{0:0.00}", Convert.ToDouble(Eval("ValorNumerico"))) %>'></asp:Label>
                                                <%--<asp:Label ID="ValorNumericoLabel" runat="server" Text='<%# Bind("ValorNumerico") %>'></asp:Label>--%>
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
    </div>

    <uc1:WebUserControlAdministracaoSistema runat="server" ID="WebUserControlAdministracaoSistema" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
