<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ChamadoResponsaveisWebForm.aspx.cs" Inherits="VendasWeb.Chamados.ChamadoResponsaveisWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlChamado.ascx" TagPrefix="uc1" TagName="WebUserControlChamado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
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
                    <h3 class="panel-title">Cadastro Chamado - Responsáveis</h3>
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
                                <asp:Label ID="SolicitanteLabel" runat="server" Text="Solicitante:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="SolicitanteDropDownList" runat="server" CssClass="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Número:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NumeroChamadoTextBox" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ResponsavelLabel" runat="server" Text="Responsavel:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="ResponsavelDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                </div>

            </div>

            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <div class="col-sm-auto">

                            <asp:LinkButton ID="AdicionarLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                runat="server" OnClick="AdicionarLinkButton_Click">Adicionar</asp:LinkButton>

                            <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                        </div>

                    </div>
                </div>
            </div>

        </div>

        <asp:MultiView ID="ChamadosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ChamadosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="ChamadosUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="ChamadosGridView" EmptyDataText="Não foi possível encontrar nenhum chamado." AutoGenerateColumns="False"
                                        runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="ChamadosGridView_PageIndexChanging">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <%-- <asp:TemplateField HeaderText="Excluir" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:UpdatePanel ID="ExcluirUpdatePanel" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="ExcluirLinkButton" class="btn btn-danger fa fa-times"
                                                                    CausesValidation="false" runat="server" OnClick="ExcluirLinkButton_Click"></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="ExcluirLinkButton" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Excluir" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="ExcluirLinkButton" class="btn btn-danger fa fa-times"
                                                            CausesValidation="false" runat="server" OnClick="ExcluirLinkButton_Click"></asp:LinkButton>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDResponsavel" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDResponsavelLabel" runat="server" Text='<%# Bind("IDResponsavel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Responsável">
                                                <ItemTemplate>
                                                    <asp:Label ID="ResponsavelLabel" runat="server" Text='<%# Bind("Responsavel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Principal" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="PrincipalPanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <center>
                                                                <div class="col-xs-5 text-left checkbox">
                                                                    <label class="form-checkbox form-icon form-text">
                                                                        <asp:CheckBox ID="PrincipalCheckBox" runat="server"
                                                                            Checked='<%# Convert.ToBoolean(Eval("Principal")) %>' AutoPostBack="true"
                                                                            OnCheckedChanged="PrincipalCheckBox_CheckedChanged" />
                                                                    </label>
                                                                </div>
                                                            </center>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="PrincipalCheckBox" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Principal" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:CheckBox ID="PrincipalCheckBox" runat="server"
                                                            Checked='<%# Convert.ToBoolean(Eval("Principal")) %>' AutoPostBack="true"
                                                            OnCheckedChanged="PrincipalCheckBox_CheckedChanged" />
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ChamadosGridView" />
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

    <uc1:WebUserControlChamado runat="server" ID="WebUserControlChamado" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>

