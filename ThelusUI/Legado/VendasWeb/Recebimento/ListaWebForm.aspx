<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ListaWebForm.aspx.cs" Inherits="VendasWeb.Recebimento.ListaWebForm" %>

<%@ Register Src="~/usercontrol/RecebimentoWebUserControl.ascx" TagPrefix="uc1" TagName="RecebimentoWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/RecebimentoPrincipalJavaScript.js?aux=2")%>" type="text/javascript"></script>
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
                    <h3 class="panel-title">Recebimentos - Lista</h3>
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

                    <asp:UpdatePanel ID="CamposUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Empresa:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Setor:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="SetorDropDownList" runat="server" CssClass="form-control fstdropdown-select"
                                            AutoPostBack="true" OnSelectedIndexChanged="SetorDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Status:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Usuário:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:DropDownList ID="UsuariosDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Data Inicial:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="DataInicialTextBox" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Data Final:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="DataFinalTextBox" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Fornecedor:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:DropDownList ID="FornecedorDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="SetorDropDownList" />
                        </Triggers>
                    </asp:UpdatePanel>

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

                        <div class="col-sm-auto">

                            <asp:UpdatePanel ID="AprovarUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:LinkButton ID="NovoLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                        CausesValidation="false" runat="server" OnClick="NovoLinkButton_Click">Novo</asp:LinkButton>

                                    <asp:LinkButton ID="BuscarLinkButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                        CausesValidation="false" runat="server" OnClick="BuscarLinkButton_Click">Buscar</asp:LinkButton>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="NovoLinkButton" />
                                    <asp:PostBackTrigger ControlID="BuscarLinkButton" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>
            </div>

        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="RecebimentoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="RecebimentoView" runat="server">
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista de Recebimentos
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="RecebimentoGridView" EmptyDataText="Não foi possível encontrar nenhum recebimento" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="RecebimentoGridView_PageIndexChanging" Visible="true"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sel.">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="SelecionarGridViewLinkButton" class="btn btn-info fa fa-edit"
                                                    CausesValidation="false" runat="server" OnClick="SelecionarGridViewLinkButton_Click">
                                                </asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDEmpresa" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDEmpresaGridViewLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nº Rec.">
                                        <ItemTemplate>
                                            <asp:Label ID="IDRecebimentoGridViewLabel" runat="server" Text='<%# Bind("IDRecebimento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nº NF">
                                        <ItemTemplate>
                                            <asp:Label ID="NFGridViewLabel" runat="server" Text='<%# Bind("NF") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data Rec.">
                                        <ItemTemplate>
                                            <asp:Label ID="DataRecebimentoGridViewLabel" runat="server" Text='<%# Bind("DataRecebimento") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Fornecedor">
                                        <ItemTemplate>
                                            <asp:Label ID="FornecedorGridViewLabel" runat="server" Text='<%# Bind("Fornecedor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Responsável">
                                        <ItemTemplate>
                                            <asp:Label ID="ResponsavelGridViewLabel" runat="server" Text='<%# Bind("Responsavel") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Situação">
                                        <ItemTemplate>
                                            <asp:Label
                                                ID="StatusTicketLabel"
                                                runat="server"
                                                Text='<%# Bind("Situacao") %>'
                                                ForeColor="White"
                                                CssClass='<%# "bg-" + Eval("CorStatus") %>'
                                                Style="padding: 4px 8px; border-radius: 4px; display: inline-block;">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Detalhes" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <center>
                                                <asp:UpdatePanel ID="DetalhesUpdatePanel" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-primary fa fa-plus-square" CausesValidation="false" runat="server"
                                                            OnClientClick='<%# string.Format("ConsultaRecebimentoPrincipal("+Eval("IDEmpresa")+","+Eval("IDRecebimento")+")")%>'></asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="DetalhesLinkButton" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </center>
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

    <div id="RecebimentoPrincipalModal" class="modal fade bd-example-modal-xl">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

            <div class="modal-header" style="margin-top: 15px;">
                <h4 id="RecebimentoPrincipalModalTitle" class="modal-title" style="color: black;">Recebimento - Detalhes</h4>
            </div>

            <div id="RecebimentoPrincipalModalBody" class="modal-body">

                <div class="row">

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Empresa:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="EmpresaModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Recebimento:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="NumeroRecebimentoModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="row">

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Situacao:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="SituacaoModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Data:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="DataRecebimentoModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="row">

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Responsavel:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="ResponsavelModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Setor:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="SetorModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="row">

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="CNPJ:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="CNPJModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Fornecedor:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="FornecedorModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="row">

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Nota Fiscal:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-10">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="NFModalTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="row">

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Observação:"></asp:Label>
                        </div>
                    </div>

                </div>

                <div class="row">

                    <div class="col-sm-12">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="ObservacaoModalTextBox" TextMode="MultiLine" Height="200px" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </div>

            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
            </div>

        </div>
    </div>
</div>

    <uc1:RecebimentoWebUserControl runat="server" ID="RecebimentoWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

</asp:Content>
