<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AtualizacaoCustosWebForm.aspx.cs" Inherits="VendasWeb.Controladoria.AtualizacaoCustosWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlControladoria.ascx" TagPrefix="uc1" TagName="WebUserControlControladoria" %>

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
                    <h3 class="panel-title">Atualização Custos</h3>
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
                        <div class="col-md-4 col-sm-2">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDown" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Selected="True">Manuli Curitiba</asp:ListItem>
                                    <asp:ListItem Value="2">Manuli Manaus</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>

                    <div class="row">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:FileUpload CssClass="filestyle" data-buttonName="btn-primary" ID="ArquivoFileUpload" runat="server" TabIndex="-1" Style="position: absolute; clip: rect(0px, 0px, 0px, 0px);" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="row">
                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="panel-control">

                                    <asp:LinkButton ID="AtualizarLinkButton" class="btn btn-success btn-labeled fa fa-refresh fa-lg"
                                        runat="server" CausesValidation="false" OnClick="AtualizarLinkButton_Click">Atualizar Custos</asp:LinkButton>

                                    <asp:LinkButton ID="CarregaCustosLinkButton" class="btn btn-primary btn-labeled fa fa-cloud-upload fa-lg"
                                        runat="server" CausesValidation="false" OnClick="CarregaCustosLinkButton_Click">Subir Dados</asp:LinkButton>

                                    &nbsp;<asp:LinkButton ID="ModeloButton" class="btn btn-warning btn-labeled fa fa-table fa-lg"
                                        CausesValidation="false" runat="server" OnClick="ModeloButton_Click">Modelo</asp:LinkButton>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ModeloButton" />
                                <asp:PostBackTrigger ControlID="CarregaCustosLinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>


        <asp:MultiView ID="CustosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="CustosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="CustosGridView" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="CustosGridView_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Código Produto" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoProdutoGrid" runat="server" Text='<%# Bind("CodigoProduto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome Produto">
                                        <ItemTemplate>
                                            <asp:Label ID="NomeProdutoGrid" runat="server" Text='<%# Bind("NomeProduto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Família" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Material" runat="server" Text='<%# Bind("Material") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Comp." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Comprimento" runat="server" Text='<%# Bind("Comprimento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Largura" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Largura" runat="server" Text='<%# Bind("Largura") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FC" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Largura" runat="server" Text='<%# Bind("FC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Convertido" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="FCConvertido" runat="server" Text='<%# Bind("FCConvertido") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Custo" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Custo" runat="server" Text='<%# String.Format("{0:C}", Eval("Custo")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Margem" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Custo" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Percentual")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Produção" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Custo" runat="server" Text='<%# Bind("PrazoProducao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="DISTRIBUIDOR" HeaderText="Dist." />

                                    <asp:BoundField DataField="INDUSTRIA" HeaderText="Ind." />

                                    <asp:BoundField DataField="REVENDA" HeaderText="Rev." />

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <!-- End Foo Table - Filtering -->
                </div>
                <!--===================================================-->
                <!-- END TABELA -->
            </asp:View>
        </asp:MultiView>
    </div>
    <uc1:WebUserControlControladoria runat="server" ID="WebUserControlControladoria" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->

</asp:Content>
