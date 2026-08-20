<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="DetalheWebForm.aspx.cs" Inherits="VendasWeb.Recebimento.DetalheWebForm" %>

<%@ Register Src="~/usercontrol/RecebimentoDetalheWebUserControl.ascx" TagPrefix="uc1" TagName="RecebimentoDetalheWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js?aux=3")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/RecebimentoPrincipalJavaScript.js?aux=2")%>" type="text/javascript"></script>

    <script type="text/javascript">
        // Executa tanto no carregamento inicial quanto após atualizações do UpdatePanel
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            if (typeof setFstDropdown === 'function') {
                setFstDropdown();
            }
        });
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
                    <h3 class="panel-title">Recebimentos - Detalhes</h3>
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

                            <%-- Empresa - Status --%>
                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Empresa:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control fstdropdown-select">
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
                                        <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <%-- Recebimento - Data --%>
                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Nº Recebimento:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="IDRecebimentoTextBox" Enabled="false"
                                            TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Data:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="DataTextBox" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <%-- Setor - Usuário --%>
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
                                        <asp:Label runat="server" Text="Usuário:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:DropDownList ID="UsuariosDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <%-- Manual - Fornecedor --%>
                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Manual:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ManualCheckBox" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="ManualCheckBox_CheckedChanged" />
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Fornecedor:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4" id="DivFornecedorDropDownList" runat="server">
                                    <div class="form-group">
                                        <asp:DropDownList ID="FornecedorDropDownList" runat="server" CssClass="form-control fstdropdown-select"
                                            AutoPostBack="true" OnSelectedIndexChanged="FornecedorDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-4" id="DivFornecedorTextBox" runat="server">
                                    <div class="form-group">
                                        <asp:TextBox ID="FornecedorTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <%-- CNPJ - NF --%>
                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="CNPJ:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="CNPJTextBox" runat="server" CssClass="form-control"
                                            onkeypress="mascara( this, cpfcnpj );" onblur="mascara( this, cpfcnpj );" onfocus="mascara( this, cpfcnpj );"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="NF:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="NFTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <%-- Observacao --%>
                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Observação:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <asp:TextBox ID="ObservacaoTextBox" runat="server" TextMode="MultiLine" Height="100px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="UsuariosDropDownList" />
                            <asp:PostBackTrigger ControlID="ManualCheckBox" />
                            <asp:PostBackTrigger ControlID="FornecedorDropDownList" />
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

                                    <asp:LinkButton ID="GravarLinkButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                        runat="server" OnClick="GravarLinkButton_Click">Gravar</asp:LinkButton>

                                    <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                        runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                    <asp:PostBackTrigger ControlID="GravarLinkButton" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

    <uc1:RecebimentoDetalheWebUserControl runat="server" ID="RecebimentoDetalheWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

</asp:Content>
