<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ListaGruposSetorWebForm.aspx.cs" Inherits="VendasWeb.AdministracaoSistema.ListaGruposSetorWebForm" %>

<%@ Register Src="~/usercontrol/CadastroSetorWebUserControl.ascx" TagPrefix="uc1" TagName="CadastroSetorWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>

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
                    <h3 class="panel-title">Administração - Lista Grupos Setores</h3>
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
                                <asp:Label ID="IDSetorLabel" runat="server" Text="Setor :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="IDSetorTextBox" runat="server" ReadOnly></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="IDSetorTextBox" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="StatusLabel" runat="server" Text="Status :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="StatusTextBox" runat="server" ReadOnly></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <hr />
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="GrupoLabel" runat="server" Text="Grupo :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="GrupoDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                    </asp:DropDownList>

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
                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                                <asp:LinkButton ID="AdicionarLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                    CausesValidation="false" runat="server" OnClick="AdicionarLinkButton_Click">Adicionar</asp:LinkButton>

                                <asp:LinkButton ID="RetornarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BuscarButton" />
                                <asp:PostBackTrigger ControlID="AdicionarLinkButton" />
                                <asp:PostBackTrigger ControlID="RetornarButton" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="GruposSetorMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="GruposSetorView" runat="server">

                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos Setor
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="TesteUpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GruposSetorGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                        runat="server" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Excluir">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="ExcluirLinkButton" class="btn btn-danger fa fa-times"
                                                                CausesValidation="false" runat="server" OnClick="ExcluirLinkButton_Click"></asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="ExcluirLinkButton" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDGrupo" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDGrupoLabel" runat="server" Text='<%# Bind("IDGrupo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nome" ItemStyle-Width="90%">
                                                <ItemTemplate>
                                                    <asp:Label ID="NomeGrupoLabel" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
      
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
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

    <uc1:CadastroSetorWebUserControl runat="server" ID="CadastroSetorWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
