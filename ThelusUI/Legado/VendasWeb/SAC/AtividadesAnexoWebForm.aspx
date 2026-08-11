<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AtividadesAnexoWebForm.aspx.cs" Inherits="VendasWeb.SAC.AtividadesAnexoWebForm" %>

<%@ Register Src="~/usercontrol/AtividadeWebUserControl.ascx" TagPrefix="uc1" TagName="AtividadeWebUserControl" %>

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
                    <h3 class="panel-title">SAC - Atividades - Anexos</h3>
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
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa:"></asp:Label>
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

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ClienteLabel" runat="server" Text="Cliente:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-8">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ClienteTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="TicketLabel" runat="server" Text="Ticket:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="TicketTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SetorLabel" runat="server" Text="Setor:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-7">
                            <div class="form-group">
                                <asp:DropDownList ID="SetorDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="AtividadeLabel" runat="server" Text="Atividade:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="AtividadeTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DescricaoLabel" runat="server" Text="Descrição:"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DescricaoTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ArquivoLabel" runat="server" Text="Arquivo:"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:FileUpload CssClass="filestyle" data-buttonName="btn-primary" ID="ArquivoFileUpload" runat="server"
                                TabIndex="-1" Style="position: absolute; clip: rect(0px, 0px, 0px, 0px);" />
                        </div>
                    </div>
                </div>

                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                        runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                    <asp:LinkButton ID="AdicionarButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                                        runat="server" OnClick="AdicionarButton_Click">Adicionar</asp:LinkButton>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="AdicionarButton" />
                                    <asp:AsyncPostBackTrigger ControlID="RetornarLinkButton" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>
            </div>

        </div>

        <asp:MultiView ID="AnexosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="AnexosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="AnexosGridView" EmptyDataText="Não foi possível encontrar nenhum anexo." AutoGenerateColumns="False"
                                        runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="AnexosGridView_PageIndexChanging">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Excluir">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ExcluirAnexoLinkButton" class="btn btn-danger fa fa-times"
                                                        CausesValidation="false" runat="server" OnClick="ExcluirAnexoLinkButton_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="DataLabel" runat="server" Text='<%# Bind("DataAnexo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Anexo">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDAnexoLabel" runat="server" Text='<%# Bind("IDAnexo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descrição ">
                                                <ItemTemplate>
                                                    <asp:Label ID="DescricaoLabel" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arquivo">
                                                <ItemTemplate>
                                                    <asp:Label ID="ArquivoLabel" runat="server" Text='<%# Bind("Arquivo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CaminhoDestino" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="CaminhoDestinoLabel" runat="server" Text='<%# Bind("CaminhoDestino") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Baixar">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="TesteUpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="BaixarLinkButton" class="btn btn-info fa fa-cloud-download"
                                                                CausesValidation="false" runat="server" OnClick="BaixarLinkButton_Click"></asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BaixarLinkButton" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:PostBackTrigger ControlID="AdicionarButton" />--%>
                                    <asp:AsyncPostBackTrigger ControlID="AnexosGridView" />
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

    <uc1:AtividadeWebUserControl runat="server" ID="AtividadeWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
