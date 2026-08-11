<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="TicketsWebForm.aspx.cs" Inherits="VendasWeb.SAC.TicketsWebForm" %>

<%@ Register Src="~/usercontrol/SACWebUserControl.ascx" TagPrefix="uc1" TagName="SACWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/TicketDetalheJavaScript.js")%>" type="text/javascript"></script>
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
                    <h3 class="panel-title">SAC - Ticket's</h3>
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
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TicketLabel" runat="server" Text="Ticket:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="TicketTextBox" runat="server" CssClass="form-control"
                                    placeholder="Informar o número."></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SituacaoLabel" runat="server" Text="Situação:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="SituacaoDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="AbInicialLabel" runat="server" Text="Ab. Inicial:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="AbInicialTextBox" runat="server" CssClass="form-control"
                                    TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="AbFinalLabel" runat="server" Text="Ab. Final:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="AbFinalTextBox" runat="server" CssClass="form-control"
                                    TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="FecInicialLabel" runat="server" Text="Fec. Inicial:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="FecInicialTextBox" runat="server" CssClass="form-control"
                                    TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="FecFinalLabel" runat="server" Text="Fec. Final:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="FecFinalTextBox" runat="server" CssClass="form-control"
                                    TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ClienteLabel" runat="server" Text="Cliente:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="ClienteTextBox" runat="server" CssClass="form-control"
                                    placeholder="Informar código, nome ou CNPJ."></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SolucaoLabel" runat="server" Text="Solução:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="SolucaoDropDownList" runat="server" CssClass="form-control">
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

                    <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="panel-control">

                                <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="RelatorioPDFLinkButton" class="btn btn-danger btn-labeled fa fa-cloud-download fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RelatorioPDFLinkButton_Click">PDF</asp:LinkButton>

                                <asp:LinkButton ID="RelatorioExcelLinkButton" class="btn btn-success btn-labeled fa fa-cloud-download fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RelatorioExcelLinkButton_Click">Excel</asp:LinkButton>

                                <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                                <asp:LinkButton ID="NovoLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                    CausesValidation="false" runat="server" OnClick="NovoLinkButton_Click">Novo</asp:LinkButton>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                            <asp:PostBackTrigger ControlID="RelatorioPDFLinkButton" />
                            <asp:PostBackTrigger ControlID="RelatorioExcelLinkButton" />
                            <asp:PostBackTrigger ControlID="BuscarButton" />
                            <asp:PostBackTrigger ControlID="NovoLinkButton" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="SACMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="SACView" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="SACGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="SACGridView_PageIndexChanging" Visible="true"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sel.">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="SelLinkButton" class="btn btn-info fa fa-edit"
                                                    CausesValidation="false" runat="server" OnClick="SelLinkButton_Click"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Empresa">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpresaLabel" runat="server" Text='<%# Bind("Empresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDEmpresa" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDEmpresaLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ticket">
                                        <ItemTemplate>
                                            <asp:Label ID="TicketLabel" runat="server" Text='<%# Bind("IDTicket") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data">
                                        <ItemTemplate>
                                            <asp:Label ID="DataLabel" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(Eval("DataSolicitacao"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cliente">
                                        <ItemTemplate>
                                            <asp:Label ID="ClienteLabel" runat="server" Text='<%# Bind("Cliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDCliente" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDClienteLabel" runat="server" Text='<%# Bind("IDCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CodigoCliente" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoClienteLabel" runat="server" Text='<%# Bind("CodigoCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Situação">
                                        <ItemTemplate>
                                            <asp:Label ID="SituacaoLabel" runat="server" Text='<%# Bind("Situacao") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Solução">
                                        <ItemTemplate>
                                            <asp:Label ID="SolucaoLabel" runat="server" Text='<%# Bind("Solucao") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Detalhes">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-primary fa fa-plus-square" CausesValidation="false" runat="server"
                                                            OnClientClick='<%# string.Format("ConsultaTicketDetalhe("+Eval("IDEmpresa")+","+Eval("IDTicket")+")")%>'></asp:LinkButton>
                                                    </center>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DetalhesLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
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

    <div id="fullReservaModal" class="modal fade bd-example-modal-xl">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: 15px;">
                    <h4 id="modalTitle" class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal"><span>×</span> <span class="sr-only">Fechar</span></button>
                </div>

                <div id="modalBody" class="modal-body">
                    <div class="loader" id="LoadingDados"></div>

                    <div class="table-responsive" id="DadosModal">
                        <div class="col-md-12 pad-top bg-gray" style="padding-right: 15px;">
                            <div class="row pad-lft pad-rgt">

                                <%--LINHA 1--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Empresa</th>
                                            <th style="width: 50%;">Cliente</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="EmpresaModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="ClienteModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 2--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Ticket</th>
                                            <th style="width: 50%;">Solicitante</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="TicketModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="SolicitanteModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 3--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Responsável</th>
                                            <th style="width: 50%;">Tratativa</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="ResponsavelModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="TratativaModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 4--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Situação</th>
                                            <th style="width: 50%;">Prioridade</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="SituacaoModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="PrioridadeModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 5--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Abertura</th>
                                            <th style="width: 50%;">Fechamento</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="AberturaModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="FechamentoModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 6--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Solução</th>
                                            <th style="width: 50%;">Ocorrência</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="SolucaoModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="OcorrenciaModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 7--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Vendedor</th>
                                            <th style="width: 50%;">Motivo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="VendedorModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="MotivoModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Descrição</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="DescricaoModalLabel" TextMode="MultiLine"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

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

    <uc1:SACWebUserControl runat="server" ID="SACWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>
