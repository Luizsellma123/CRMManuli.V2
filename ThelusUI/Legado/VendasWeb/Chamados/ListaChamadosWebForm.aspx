<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ListaChamadosWebForm.aspx.cs" Inherits="VendasWeb.Chamados.ListaChamadosWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlChamados.ascx" TagPrefix="uc1" TagName="WebUserControlChamados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/ChamadoPrincipalJavaScript.js?aux=1")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-sm-9">

            <asp:UpdatePanel ID="CamposUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

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
                            <h3 class="panel-title">Chamados - Lista Chamados</h3>
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
                                        <asp:Label ID="ChamadoLabel" runat="server" Text="Chamado :"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:TextBox ID="ChamadoTextBox" runat="server" CssClass="form-control" placeholder="Número ou Descrição."></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="SituacaoLabel" runat="server" Text="Status :"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="SolicitanteLabel" runat="server" Text="Solicitante :"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:DropDownList ID="SolicitanteDropDownList" runat="server" AutoPostBack="true"
                                                CssClass="form-control fstdropdown-select" OnSelectedIndexChanged="SolicitanteDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="ResponsavelLabel" runat="server" Text="Responsável :"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ResponsavelDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Setor:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <asp:DropDownList ID="SetorDropDownList" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="DataInicialLabel" runat="server" Text="Data Inicial: "></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:TextBox ID="DataInicialTextBox" TextMode="date" runat="server" CssClass="form-control" placeholder="Data inicial."></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="DataFinalLabel" runat="server" Text="Data Final: "></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:TextBox ID="DataFinalTextBox" TextMode="date" runat="server" CssClass="form-control" placeholder="Informe Número Esboço do SAP."></asp:TextBox>
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

                                <asp:LinkButton ID="GravarPreferenciasLinkButton" class="btn btn-success btn-labeled fa fa-cog fa-lg"
                                    CausesValidation="false" runat="server" OnClick="PreferenciasLinkButton_Click">Gravar preferências</asp:LinkButton>

                                <asp:LinkButton ID="BuscarButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                            </div>

                        </div>

                    </div>


                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="SolicitanteDropDownList" />
                    <asp:PostBackTrigger ControlID="GravarPreferenciasLinkButton" />
                    <asp:PostBackTrigger ControlID="BuscarButton" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

        <asp:MultiView ID="ChamadosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ChamadosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Chamados
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="ChamadosGridView" EmptyDataText="Não foi possível encontrar nenhum chamado." AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="ChamadosGridView_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Acessar">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="AcessarLinkButton" class="btn btn-info fa fa-arrow-right"
                                                        CausesValidation="false" runat="server" OnClick="AcessarLinkButton_Click"></asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="AcessarLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Chamado">
                                        <ItemTemplate>
                                            <asp:Label ID="IDChamadoLabel" runat="server" Text='<%# Bind("IDChamado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Abertura ">
                                        <ItemTemplate>
                                            <asp:Label ID="DataAberturaCRM" runat="server" Text='<%# Bind("DataAbertura") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Assunto ">
                                        <ItemTemplate>
                                            <asp:Label ID="AssuntoLabel" runat="server" Text='<%# Bind("Assunto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Solicitante">
                                        <ItemTemplate>
                                            <asp:Label ID="solicitanteLabel" runat="server" Text='<%# Bind("solicitante") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Setor ">
                                        <ItemTemplate>
                                            <asp:Label ID="SetorLabel" runat="server" Text='<%# Bind("Setor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Responsável ">
                                        <ItemTemplate>
                                            <asp:Label ID="ResponsavelLabel" runat="server" Text='<%# Bind("Responsavel") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="StatusChamadoLabel" runat="server" Text='<%# Bind("StatusChamado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Detalhes" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <center>
                                                <asp:UpdatePanel ID="DetalhesUpdatePanel" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-primary fa fa-plus-square" CausesValidation="false" runat="server"
                                                            OnClientClick='<%# string.Format("ConsultaChamadoPrincipal("+Eval("IDChamado")+")")%>'></asp:LinkButton>
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

    <div id="ChamadoPrincipalModal" class="modal fade bd-example-modal-xl">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">

                <div class="modal-header" style="margin-top: 15px;">
                    <h4 id="ChamadoPrincipalModalTitle" class="modal-title" style="color: black;">Chamado - Detalhes</h4>
                </div>

                <div id="ChamadoPrincipalModalBody" class="modal-body">

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Chamado:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NumeroChamadoModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Status:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="StatusChamadoModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Solicitante:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="SolicitanteModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Abertura:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="AberturaModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Classificação:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ClassificacaoModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Setor:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="SetorModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Sistema:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="SistemaModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Prioridade:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="PrioridadeModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Responsáveis:"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-12">
                            <div id="DivResponsaveisModal"></div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Assunto:"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="AssuntoModalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Descrição:"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DescricaoModalTextBox" TextMode="MultiLine" Height="200px" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Histórico:" ID="HistoricoLabel"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="HistoricoTextBox" TextMode="MultiLine" Height="100px" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                    <asp:LinkButton ID="HomologarLinkButton" class="btn btn-success"
                        CausesValidation="false" runat="server" OnClick="HomologarLinkButton_Click">Homologar</asp:LinkButton>
                </div>

            </div>
        </div>
    </div>

    <uc1:WebUserControlChamados runat="server" ID="WebUserControlChamados" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 

</asp:Content>
