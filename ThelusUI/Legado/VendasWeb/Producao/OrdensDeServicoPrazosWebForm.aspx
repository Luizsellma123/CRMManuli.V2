<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="OrdensDeServicoPrazosWebForm.aspx.cs" Inherits="VendasWeb.Producao.OrdensDeServicoPrazosWebForm" %>

<%@ Register Src="~/usercontrol/ProducaoWebUserControl.ascx" TagPrefix="uc1" TagName="ProducaoWebUserControl" %>

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
                    <h3 class="panel-title">Produção - Prazos Produção</h3>
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

                    <%--  LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa :"></asp:Label>
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

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="GrupoLabel" runat="server" Text="Grupo :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="GrupoDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 3--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ProducaoLabel" runat="server" Text="Produção :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="ProducaoTextBox"
                                        onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ExpedicaoLabel" runat="server" Text="Expedição :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="ExpedicaoTextBox"
                                        onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                        runat="server"></asp:TextBox>
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

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="VoltarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="VoltarButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="NovoLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="NovoLinkButton_Click">Adicionar</asp:LinkButton>

                                <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="VoltarButton" />
                                <asp:PostBackTrigger ControlID="NovoLinkButton" />
                                <asp:PostBackTrigger ControlID="BuscarButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="OrdensServicoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="OrdensServicoView" runat="server">
                <div class="panel">

                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="OrdensServicoGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="OrdensServicoGridView_PageIndexChanging" Visible="true">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Ex." ItemStyle-Width="5%">
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

                                    <asp:TemplateField HeaderText="IDEmpresa" Visible="false" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="IDEmpresaLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Empresa" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpresaLabel" runat="server" Text='<%# Bind("Empresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDGrupo" Visible="false" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="IDGrupoLabel" runat="server" Text='<%# Bind("IDGrupo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grupo" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="GrupoGridLabel" runat="server" Text='<%# Bind("Grupo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Produção" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" ID="ProducaoGridTextBox"
                                                onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                                Text='<%# Eval("Producao") %>' OnTextChanged="ProducaoTextBox_TextChanged" runat="server"
                                                AutoPostBack="true"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Expedição" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" ID="ExpedicaoGridTextBox"
                                                onkeypress="mascara( this, mnum );" onblur="mascara( this, mnum );" onfocus="mascara( this, mnum );"
                                                Text='<%# Eval("Expedicao") %>' OnTextChanged="ExpedicaoTextBox_TextChanged" runat="server"
                                                AutoPostBack="true"></asp:TextBox>
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

    <uc1:ProducaoWebUserControl runat="server" ID="ProducaoWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

</asp:Content>
