<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ListaProjetosWebForm.aspx.cs" Inherits="VendasWeb.Chamados.ListaProjetosWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
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
                    <h3 class="panel-title">Chamados - Lista Projetos</h3>
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
                                    <asp:DropDownList ID="SolicitanteDropDownList" runat="server" CssClass="form-control fstdropdown-select">
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
                                <asp:Label ID="SetorLabel" runat="server" Text="Setor :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="SetorDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PrioridadeProjetoLabel" runat="server" Text="Prioridade :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="PrioridadeDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
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

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="OrdenacaoLabel" runat="server" Text="Ordenação :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="OrdenacaoDropDownList" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Chamado" Value="CC.IDChamado" />
                                        <asp:ListItem Text="Solicitante" Value="CCU.IDUsuario" />
                                        <asp:ListItem Text="Responsável" Value="CCU2.IDUsuario" />
                                        <asp:ListItem Text="Status" Value="CCST.IDStatus" />
                                        <asp:ListItem Text="Abertura" Value="CC.DataAbertura" />
                                        <asp:ListItem Text="Setor" Value="CCS.Descricao" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TipoOrdenacaoLabel" runat="server" Text="Tipo :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="TipoOdenacaoDropDownList" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Maior Para Menor" Value="DESC" />
                                        <asp:ListItem Text="Menor Para Maior" Value="ASC" />
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
                                <asp:LinkButton ID="GraficoLinkButton" class="btn btn-success btn-labeled fa fa-bar-chart fa-lg"
                                    CausesValidation="false" runat="server" OnClick="GraficoLinkButton_Click">Grafico</asp:LinkButton>

                                <asp:LinkButton ID="RecalcularDatasLinkButton" class="btn btn-success btn-labeled fa fa-refresh fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RecalcularDatasLinkButton_Click">Recalcular Datas</asp:LinkButton>

                                <asp:LinkButton ID="BuscarButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RecalcularDatasLinkButton" />
                                <asp:PostBackTrigger ControlID="BuscarButton" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="ChamadosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ChamadosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Projetos
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

                                    <asp:TemplateField HeaderText="Previsão ">
                                        <ItemTemplate>
                                            <asp:Label ID="PrevisaoLabel" runat="server" Text='<%# Bind("PrevisaoEntrega") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prioridade ">
                                        <ItemTemplate>
                                            <asp:Label ID="PrioridadeLabel" runat="server" Text='<%# Bind("PrioridadeProjeto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Horas Prev">
                                        <ItemTemplate>
                                            <asp:Label ID="PrevisaoHorasLabel" runat="server" Text='<%# Bind("HorasPrevistas") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Horas Real">
                                        <ItemTemplate>
                                            <asp:Label ID="RealizadasHorasLabel" runat="server" Text='<%# Bind("HorasRealizadas") %>'></asp:Label>
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

                                    <asp:TemplateField HeaderText="Histórico">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="AcessarLinkButton" class="btn btn-info fa fa-binoculars"
                                                        CausesValidation="false" runat="server" OnClick="AcessarLinkButton_Click"></asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="AcessarLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Anexo">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="AnexoLinkButton" class="btn btn-info fa fa-paperclip"
                                                        CausesValidation="false" runat="server" OnClick="AnexoLinkButton_Click"></asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="AnexoLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
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

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 

</asp:Content>
