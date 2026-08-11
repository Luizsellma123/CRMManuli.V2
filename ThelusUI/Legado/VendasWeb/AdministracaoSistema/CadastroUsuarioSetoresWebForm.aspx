<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroUsuarioSetoresWebForm.aspx.cs" Inherits="VendasWeb.AdministracaoSistema.CadastroUsuarioSetoresWebForm" %>

<%@ Register Src="~/usercontrol/CadastroUsuarioWebUserControl.ascx" TagPrefix="uc1" TagName="CadastroUsuarioWebUserControl" %>

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
                    <h3 class="panel-title">Cadastro Usuários - Setores</h3>
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

                    <asp:HiddenField ID="IDUsuarioHiddenField" runat="server" />

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoUsuarioLabel" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CodigoUsuarioTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="StatusLabel" runat="server" Text="Status:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control" Enabled="false">
                                    <asp:ListItem Value="Ativo">Ativo</asp:ListItem>
                                    <asp:ListItem Value="Desligado">Desligado</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <hr />

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SetorLabel" runat="server" Text="Setor:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="SetorDropDownList" runat="server" CssClass="form-control fstdropdown-select">
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
                    <div class="panel-control">
                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:LinkButton ID="AdicionaSetorLinkButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                                    CausesValidation="false" runat="server" OnClick="AdicionaSetorLinkButton_Click">Adiciona Setor</asp:LinkButton>

                                <asp:LinkButton ID="voltarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="voltarButton_Click">Retornar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="AdicionaSetorLinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="UsuarioSetoresMultiView" runat="server" ActiveViewIndex="0" Visible="true">
            <asp:View ID="PSIUView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Setores do Usuário
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="GridUpdatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                <ContentTemplate>

                                    <asp:GridView ID="UsuarioSetoresGridView" EmptyDataText="Não foram encontrados dados com esses filtros" AutoGenerateColumns="False"
                                        runat="server" AllowPaging="false"
                                        CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Excluir">
                                                <ItemTemplate>

                                                    <center>
                                                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="DeleteButton" class="btn btn-danger fa fa-times"
                                                                    CausesValidation="false" OnClick="DeleteButton_Click" runat="server"></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="DeleteButton" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDSetor" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDSetorLabel" runat="server" Text='<%# Bind("IDSetor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descrição Setor">
                                                <ItemTemplate>
                                                    <asp:Label ID="DescricaoSetorLabel" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Administrador">
                                                <ItemTemplate>

                                                    <asp:UpdatePanel ID="AdministradorPanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <div class="col-xs-5 text-left checkbox">
                                                                <label class="form-checkbox form-icon form-text">
                                                                    <asp:CheckBox ID="AdministradorCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Administrador")) %>' AutoPostBack="true" OnCheckedChanged="AdministradorCheckBox_CheckedChanged" />
                                                                </label>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="AdministradorCheckBox" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="UsuarioSetoresGridView" />
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

    <uc1:CadastroUsuarioWebUserControl runat="server" ID="CadastroUsuarioWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

</asp:Content>
