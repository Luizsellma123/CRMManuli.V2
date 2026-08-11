<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ListaGruposUsuariosWebForm.aspx.cs" Inherits="VendasWeb.AdministracaoSistema.ListaGruposUsuariosWebForm" %>

<%@ Register Src="~/usercontrol/CadastroUsuarioWebUserControl.ascx" TagPrefix="uc1" TagName="CadastroUsuarioWebUserControl" %>

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
                    <h3 class="panel-title">Administração - Lista Grupos Usuários</h3>
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
                                <asp:Label ID="UsuárioLabel" runat="server" Text="Usuário :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="UsuarioTextBox" runat="server" CssClass="form-control" placeholder="Nome ou Código." Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NomeUsuarioLabel" runat="server" Text="Nome :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="NomeUsuarioTextBox" runat="server" CssClass="form-control" placeholder="Nome ou Código." Enabled="false"></asp:TextBox>
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
                        <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" Visible="false">Buscar</asp:LinkButton>

                        <asp:LinkButton ID="NovoUsuarioLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                            CausesValidation="false" runat="server" Visible="false">Novo</asp:LinkButton>

                        <asp:LinkButton ID="voltarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="voltarButton_Click">Retornar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="GruposUsuariosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="GruposUsuariosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos Usuários
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="TesteUpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GruposUsuariosGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                        runat="server" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Acessar" Visible="false">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="AcessarLinkButton" class="btn btn-info fa fa-edit"
                                                                CausesValidation="false" runat="server"></asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="AcessarLinkButton" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDGrupo" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDGrupoLabel" runat="server" Text='<%# Bind("IDGrupo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Grupo" ItemStyle-Width="90%">
                                                <ItemTemplate>
                                                    <asp:Label ID="NomeGrupoLabel" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ativo">
                                                <ItemTemplate>

                                                    <asp:UpdatePanel ID="AtivoUpdatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <div class="col-xs-5 text-left checkbox">
                                                                <label class="form-checkbox form-icon form-text">
                                                                    <asp:CheckBox ID="AtivoUsuarioCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Ativo")) %>' OnCheckedChanged="AtivoUsuarioCheckBox_CheckedChanged" AutoPostBack="true" />
                                                                </label>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="AtivoUsuarioCheckBox" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Administrador">
                                                <ItemTemplate>

                                                    <asp:UpdatePanel ID="AdministradorUpdatePanel" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="col-xs-5 text-left checkbox">
                                                                <label class="form-checkbox form-icon form-text">
                                                                    <asp:CheckBox ID="AdministradorUsuarioCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Administrador")) %>' OnCheckedChanged="AdministradorUsuarioCheckBox_CheckedChanged" AutoPostBack="true" />
                                                                </label>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="AdministradorUsuarioCheckBox" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
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

    <uc1:CadastroUsuarioWebUserControl runat="server" ID="CadastroUsuarioWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>
