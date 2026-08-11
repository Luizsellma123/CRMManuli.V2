<%@ Page Title="" Language="C#" MasterPageFile="~/PortalColaboradorMasterPage.Master" AutoEventWireup="true" CodeBehind="DocumentosGeraisWebForm.aspx.cs" Inherits="VendasWeb.PortalColaborador.DocumentosGerais.DocumentosGeraisWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
        <div class="col-sm-12">
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
                    <h3 class="panel-title">Documentos - Gerais</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse' aria-expanded='true' style='height: 0px;'>"
                    runat="server"></asp:Literal>

                <div class="panel-body">

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Descrição:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DescricaoTextBox" runat="server" placeholder="Informe a descrição."></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                            <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                        </div>

                    </div>
                </div>
            </div>

        </div>

        <asp:MultiView ID="DocumentosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="DocumentosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista de Documentos
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="DocumentosUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="DocumentosGridView" EmptyDataText="Não foi possível encontrar nenhum anexo." AutoGenerateColumns="False"
                                        runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="DocumentosGridView_PageIndexChanging">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>                                          

                                            <asp:TemplateField HeaderText="Data" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="DataCRM" runat="server" Text='<%# Bind("Data") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descrição">
                                                <ItemTemplate>
                                                    <asp:Label ID="DescricaoLabel" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arquivo">
                                                <ItemTemplate>
                                                    <asp:Label ID="ArquivoLabel" runat="server" Text='<%# Bind("Arquivo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CaminhoArquivo" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="CaminhoArquivoLabel" runat="server" Text='<%# Bind("CaminhoArquivo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Baixar" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BaixarLinkButton" class="btn btn-info fa fa-cloud-download"
                                                        CausesValidation="false" runat="server" OnClick="BaixarLinkButton_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="DocumentosGridView" />
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

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->

</asp:Content>
