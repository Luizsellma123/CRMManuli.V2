<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AnaliseCreditoDetalheWebForm.aspx.cs" Inherits="VendasWeb.Clientes.AnaliseCreditoDetalheWebForm" %>

<%@ Register Src="~/usercontrol/AnaliseCreditoDetalheWebUserControl.ascx" TagPrefix="uc1" TagName="AnaliseCreditoDetalheWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/AnaliseCreditoDetalheJavaScript.js?aux=4")%>" type="text/javascript"></script>

    <link rel="stylesheet" href="../PortalClienteManuli/vendors/css/vendor.bundle.base.css">
    <link rel="stylesheet" href="../PortalClienteManuli/vendors/css/vendor.bundle.addons.css">
    <link rel="Stylesheet" href="../css/chart.css" />

    <!--<script src="../..\vendors\js\vendor.bundle.base.js"></script>-->
    <script src="<%=Page.ResolveClientUrl("~/PortalClienteManuli/vendors/js/vendor.bundle.base.js")%>"></script>
    <!--<script src="../..\vendors\js\vendor.bundle.addons.js"></script>-->
    <script src="<%=Page.ResolveClientUrl("~/PortalClienteManuli/vendors/js/vendor.bundle.addons.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/PortalClienteManuli/js/off-canvas.js")%>"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="IDClienteHiddenField" runat="server" />

    <asp:HiddenField ID="IDAnaliseHiddenField" runat="server" />

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
                    <h3 class="panel-title">Cadastro Cliente - Análise Crédito - Detalhes</h3>
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

                    <%-- Detalhes principais --%>
                    <div>

                        <%-- Análise, Data --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Análise:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="AnaliseTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Data:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="DataTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Código, Nome --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Código:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="CodigoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Nome:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="NomeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Fantasia --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="CNPJ:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="CNPJTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Fantasia --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Fantasia:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="FantasiaTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Situação CNPJ --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Situação CNPJ:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="SituacaoCNPJTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Endereço Completo --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="End. Completo:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="EnderecoCompletoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Telefone, Site --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Telefone:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="TelefoneTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Site:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="SiteTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Tipo Sociedade --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Tipo Sociedade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="TipoSociedadeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Registro, Realizado, NIRE --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Registro:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="RegistroTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Realizado:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="RealizadoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="NIRE:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="NIRETextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Antecessora --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Antecessora:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="AntecessoraTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Fundação --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Fundação:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="FundacaoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Inscrição Estadual, Opção Tributária --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Inscrição Est.:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="InscricaoEstadualTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Opção Tributária:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="OpcaoTributariaTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Ramo Atividade --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Ramo Atividade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="RamoAtividadeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Atividade Serasa --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Atividade Serasa:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="AtividadeSerasaTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Importação, Exportação, Score --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Importação:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="ImportacaoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Exportação:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="ExportacaoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Score:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="ScoreTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- CNAE --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="CNAE:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="CNAETextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Filiais --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Filiais:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="FiliaisTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Grafias Semelhantes --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Grafias Semel.:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <asp:UpdatePanel ID="GrafiasSemelhantesUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="GrafiasSemelhantesLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalGrafiasSemelhantes(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GrafiasSemelhantesLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Score Serasa e Limite de Crédito --%>
                    <div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Score Serasa e Limite de Crédito:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-6">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Score Serasa (0 - 1000)"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Limite de Crédito"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-6">
                                <div class="col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <canvas id="ScoreSerasa-chart"></canvas>
                                        <div class="card-body">
                                            <div id="ScoreSerasa-legend" class="distribution-chart-legend pt-4 pb-3"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="col-xl-3 grid-margin stretch-card">
                                    <div class="card">
                                        <canvas id="LimiteCredito-chart"></canvas>
                                        <div class="card-body">
                                            <div id="LimiteCredito-legend" class="distribution-chart-legend pt-4 pb-3"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Interpretação:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="InterpretacaoTextBox" Height="100px" class="form-control" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row" id="FraseAlertaDiv" runat="server" visible="false">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Frase Alerta:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:Label ID="FraseAlertaLabel" Style="font-size: 14px; color: red; font-weight: bold;" runat="server"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Informações sobre anotações negativas da empresa --%>
                    <div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Informações sobre anotações negativas da empresa:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Pefin:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Protesto:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Cheques:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Participação Falência:"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasEmpresaPefinUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasEmpresaPefinLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasEmpresa(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasEmpresaPefinLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasEmpresaProtestoUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasEmpresaProtestoLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasEmpresa(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasEmpresaProtestoLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasEmpresaChequesUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasEmpresaChequesLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasEmpresa(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasEmpresaChequesLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasEmpresaParticipacaoFalenciaUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasEmpresaParticipacaoFalenciaLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasEmpresa(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasEmpresaParticipacaoFalenciaLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Refin:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Ação Judicial:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Recheque:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Dívida Vencida:"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasEmpresaUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasEmpresaRefinLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasEmpresa(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasEmpresaRefinLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasEmpresaAcaoJudicial" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasEmpresaAcaoJudicialLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasEmpresa(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasEmpresaAcaoJudicialLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasEmpresaRechequeUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasEmpresaRechequeLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasEmpresa(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasEmpresaRechequeLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasEmpresaDividaVencidaUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasEmpresaDividaVencidaLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasEmpresa(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasEmpresaDividaVencidaLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Total Pendências:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="AnotacoesNegativasEmpresaTotalPendenciasTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Quantidade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="AnotacoesNegativasEmpresaQuantidadeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Informações sobre anotações negativas dos socios e/ou administradores --%>
                    <div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Informações sobre anotações negativas dos sócios e/ou administradores:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Pefin:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Protesto:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Cheque Sustado/Cancelado:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Participação Falência:"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasSociosAdmPefinUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasSociosAdmPefinLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasSociosAdm(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasSociosAdmPefinLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasSociosAdmProtestoUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasSociosAdmProtestoLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasSociosAdm(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasSociosAdmProtestoLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasSociosAdmChequeSustadoCanceladoUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasSociosAdmChequeSustadoCanceladoLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasSociosAdm(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasSociosAdmChequeSustadoCanceladoLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasSociosAdmParticipacaoFalenciaUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasSociosAdmParticipacaoFalenciaLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasSociosAdm(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasSociosAdmParticipacaoFalenciaLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Refin:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Ação Judicial:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Cheque sem fundo:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Dívida Vencida:"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasSociosAdmRefinUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasSociosAdmRefinLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasSociosAdm(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasSociosAdmRefinLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasSociosAdmAcaoJudicialUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasSociosAdmAcaoJudicialLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasSociosAdm(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasSociosAdmAcaoJudicialLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasSociosAdmChequeSemFundoUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasSociosAdmChequeSemFundoLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasSociosAdm(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasSociosAdmChequeSemFundoLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-sm-3">

                                <asp:UpdatePanel ID="AnotacoesNegativasSociosAdmDividaVencidaUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <center>
                                            <asp:LinkButton ID="AnotacoesNegativasSociosAdmDividaVencidaLinkButton" ClientIDMode="Static"
                                                Width="100%" class="btn btn-primary" CausesValidation="false" runat="server"
                                                OnClientClick="MostraModalAnotacoesNegativasSociosAdm(this.id,this.text);">
                                            </asp:LinkButton>
                                        </center>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AnotacoesNegativasSociosAdmDividaVencidaLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Total Pendências:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="AnotacoesNegativasSociosAdmTotalPendenciasTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Quantidade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="AnotacoesNegativasSociosAdmQuantidadeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Quadro Social --%>
                    <div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Quadro Social:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-8">
                                <asp:LinkButton ID="QuadroSocialCapitalSocialLinkButton" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                            <div class="col-sm-4">
                                <asp:LinkButton ID="QuadroSocialRealizadoLinkButton" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-4">
                                <asp:LinkButton ID="QuadroSocialOrigemLinkButton" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                            <div class="col-sm-4">
                                <asp:LinkButton ID="QuadroSocialControleLinkButton" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                            <div class="col-sm-4">
                                <asp:LinkButton ID="QuadroSocialNaturezaLinkButton" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Informações sobre consultas --%>
                    <div runat="server" id="InformacoesSobreConsultasDiv">

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Informações sobre consultas:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <asp:Literal ID="GraficoInfSobConColunasLiteral" runat="server" />

                            <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                            <asp:Literal ID="GraficoInfSobConMesAnoLiteral" runat="server" />

                        </div>

                        <style>
                            .GraficoColunas {
                                width: 100%;
                                height: 200px;
                                /*border:1px solid black;*/
                            }

                            .GraficoColunasMesAno {
                                width: 100%;
                                height: 10px;
                                /*border:1px solid black;*/
                            }

                            .Coluna {
                                height: 100%;
                                width: 90%;
                            }

                            .Porcentagem {
                                width: 100%;
                                position: relative;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                color: white;
                            }

                            .MesAno {
                                width: 90%;
                                height: 1px;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                color: black;
                            }
                        </style>

                        <script type="text/javascript">
                            // Obtém a largura da tabela
                            var tabela = document.querySelector('.GraficoColunas');
                            var larguraTabela = tabela.clientWidth;

                            // Calcula a largura de cada coluna
                            var larguraColuna = (larguraTabela / 13) - 10;

                            // Define a largura de cada coluna
                            var colunas = document.querySelectorAll('.Coluna');
                            colunas.forEach(function (coluna) {
                                coluna.style.width = larguraColuna + 'px';
                            });
                        </script>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Últimas 5 consultas Realizadas --%>
                    <div id="UltimasConsultasRealizadasDiv" runat="server">

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Últimas 5 consultas Realizadas:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <asp:GridView ID="UltimasConsultasRealizadasGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Histórico de pagamentos --%>
                    <div id="HistoricoDePagamentosDiv" runat="server">

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Histórico de pagamentos:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div id="QuantidadeDeTitulosDiv" runat="server">

                            <div class="row">
                                <div class="col-sm">
                                    <div class="form-group">
                                        <strong>
                                            <asp:Label runat="server" Text="Quantidade de títulos:"></asp:Label>
                                        </strong>
                                    </div>
                                </div>
                            </div>

                            <asp:Label runat="server" ID="QuantidadeDeTitulosRowsCountLabel" Visible="false"></asp:Label>

                            <div class="row">

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="QuantidadeDeTitulosLinkButton1" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="QuantidadeDeTitulosLinkButton2" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="QuantidadeDeTitulosLinkButton3" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm">
                                    <div class="form-group">
                                        <br />
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="QuantidadeDeTitulosLinkButton4" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="QuantidadeDeTitulosLinkButton5" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="QuantidadeDeTitulosLinkButton6" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm">
                                    <div class="form-group">
                                        <br />
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div id="MercadoValoresEmReaisDiv" runat="server">

                            <div class="row">
                                <div class="col-sm">
                                    <div class="form-group">
                                        <strong>
                                            <asp:Label runat="server" Text="Mercado (valores em reais):"></asp:Label>
                                        </strong>
                                    </div>
                                </div>
                            </div>

                            <asp:Label runat="server" ID="MercadoValoresEmReaisRowsCountLabel" Visible="false"></asp:Label>

                            <div class="row">

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="MercadoLinkButton1" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="MercadoLinkButton2" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="MercadoLinkButton3" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm">
                                    <div class="form-group">
                                        <br />
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="MercadoLinkButton4" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="MercadoLinkButton5" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-4">
                                    <asp:LinkButton ID="MercadoLinkButton6" Width="100%"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm">
                                    <div class="form-group">
                                        <br />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <%-- Evolução de Compromissos --%>
                    <div id="EvolucaoDeCompromissosDiv" runat="server">

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Evolução de compromissos:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <asp:Literal ID="GraficoEvolucaoCompromissoColunasLiteral" runat="server" />

                            <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                            <asp:Literal ID="GraficoEvolucaoCompromissoMesAnoLiteral" runat="server" />

                            <br>

                            <asp:Literal ID="GraficoEvolucaoCompromissoDescricaoLiteral" runat="server" />

                        </div>

                        <style>
                            .Descricao {
                                width: 95%;
                                height: 100%;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                color: black;
                                background-color: silver;
                                border-radius: 3px;
                            }
                        </style>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Referenciais de negócios (valores em reais) --%>
                    <div id="ReferenciaisDeNegociosValoresEmReiasDiv" runat="server">

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Referenciais de negócios (valores em reais):"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <asp:GridView ID="ReferenciasDeNegociosGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>

                    <%-- Relacionamento com fornecedores --%>
                    <div id="RelacionamentoComFornecedoresDiv" runat="server">

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <strong>
                                        <asp:Label runat="server" Style="font-size: 12px;" Text="Relacionamento com fornecedores:"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-3">
                                <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton1" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton2" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton3" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton4" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-3">
                                <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton5" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton6" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                            <div class="col-sm-6">
                                <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton7" Width="100%" class="btn btn-primary"
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <br />
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

                        <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <%--Modais--%>
    <div>

        <div id="GrafiasSemelhantesModal" class="modal fade bd-example-modal-xl">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header" style="margin-top: 15px;">
                        <h4 id="GrafiasSemelhantesModalTitle" class="modal-title"></h4>
                    </div>

                    <div id="GrafiasSemelhantesModalBody" class="modal-body">

                        <asp:GridView ID="GrafiasSemelhantesGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>

                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="AnotacoesNegativasEmpresaModal" class="modal fade bd-example-modal-xl">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header" style="margin-top: 15px;">
                        <h4 id="AnotacoesNegativasEmpresaModalTitle" class="modal-title"></h4>
                    </div>

                    <div id="AnotacoesNegativasEmpresaModalBody" class="modal-body">

                        <div class="table-responsive" id="ConcetreResumoEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Resumo"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="ConcetreResumoEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="ConcetreResumoEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                        <div class="table-responsive" id="PefinEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Dividas em outros segmentos - até 5 ocorrências mais recentes"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="PefinEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="PefinEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                        <div class="table-responsive" id="ProtestoEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Protesto - até 5 ocorrências mais recentes"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="ProtestoEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="ProtestoEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                        <div class="table-responsive" id="ChequesEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Cheque - até 5 ocorrências mais recentes"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="ChequesEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="ChequesEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                        <div class="table-responsive" id="ParticipacaoFalenciaEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Cheques Sustado - até 5 ocorrências mais recentes"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="ParticipacaoFalenciaEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="ParticipacaoFalenciaEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                        <div class="table-responsive" id="RefinEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Dividas em Instituições Financeiras(REFIN) - até 5 ocorrências mais recentes"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="RefinEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="RefinEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                        <div class="table-responsive" id="AcaoJudicialEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Ações Judiciais - até 5 ocorrências mais recentes"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="AcaoJudicialEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="AcaoJudicialEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                        <div class="table-responsive" id="RechequeEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Recheque - até 5 ocorrências mais recentes"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="RechequeEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="RechequeEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                        <div class="table-responsive" id="DividaVencidaEmpresaDiv" style="display: none;">
                            <asp:Label runat="server" Text="Dividas em outros segmentos - até 5 ocorrências mais recentes"></asp:Label>
                            <div class="col-sm">
                                <br />
                            </div>
                            <asp:GridView ID="DividaVencidaEmpresaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                            <asp:Label ID="DividaVencidaEmpresaGridViewRows" Style="display: none;" runat="server"></asp:Label>
                        </div>

                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="AnotacoesNegativasSociosAdmModal" class="modal fade bd-example-modal-xl">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header" style="margin-top: 15px;">
                        <h4 id="AnotacoesNegativasSociosAdmModalTitle" class="modal-title"></h4>
                    </div>

                    <div id="AnotacoesNegativasSociosAdmModalBody" class="modal-body">

                        <div class="table-responsive">

                            <asp:GridView ID="AnotacoesNegativasSociosAdmGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="AnotacoesNegativasSociosAdmGridView_PageIndexChanging"
                                Visible="true" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:BoundField DataField="Nome" HeaderText="Nome" />

                                    <asp:BoundField DataField="VinculoNome" HeaderText="Vínculo" />

                                    <asp:TemplateField HeaderText="Vinculo" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="VinculoAnotacoesNegativasSociosAdmGridViewLabel" runat="server" Text='<%# Bind("Vinculo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pefin" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LinkButton1" CssClass='<%# Bind("PefinButtonCssClass") %>' CausesValidation="false"
                                                    runat="server" Text='<%# Bind("Pefin") %>' OnClick="RedirecionaAnotacoesNegativasSociosAdmGridView"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Protesto" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LinkButton2" CssClass='<%# Bind("ProtestoButtonCssClass") %>' CausesValidation="false"
                                                    runat="server" Text='<%# Bind("Protesto") %>' OnClick="RedirecionaAnotacoesNegativasSociosAdmGridView"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cheque Sust./Canc." ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LinkButton3" CssClass='<%# Bind("ChequeSustadoCanceladoButtonCssClass") %>' CausesValidation="false"
                                                    runat="server" Text='<%# Bind("ChequeSustadoCancelado") %>' OnClick="RedirecionaAnotacoesNegativasSociosAdmGridView"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Participacao Falência" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LinkButton4" CssClass='<%# Bind("ParticipacaoFalenciaButtonCssClass") %>' CausesValidation="false"
                                                    runat="server" Text='<%# Bind("ParticipacaoFalencia") %>' OnClick="RedirecionaAnotacoesNegativasSociosAdmGridView"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Refin" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LinkButton5" CssClass='<%# Bind("RefinButtonCssClass") %>' CausesValidation="false"
                                                    runat="server" Text='<%# Bind("Refin") %>' OnClick="RedirecionaAnotacoesNegativasSociosAdmGridView"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ação Judicial" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LinkButton6" CssClass='<%# Bind("AcaoJudicialButtonCssClass") %>' CausesValidation="false"
                                                    runat="server" Text='<%# Bind("AcaoJudicial") %>' OnClick="RedirecionaAnotacoesNegativasSociosAdmGridView"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cheque S/ Fundo" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LinkButton7" CssClass='<%# Bind("ChequeSemFundoButtonCssClass") %>' CausesValidation="false"
                                                    runat="server" Text='<%# Bind("ChequeSemFundo") %>' OnClick="RedirecionaAnotacoesNegativasSociosAdmGridView"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dívida Vencida" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LinkButton8" CssClass='<%# Bind("DividaVencidaButtonCssClass") %>' CausesValidation="false"
                                                    runat="server" Text='<%# Bind("DividaVencida") %>' OnClick="RedirecionaAnotacoesNegativasSociosAdmGridView"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <!--Javascript Score Serasa-->
    <asp:Literal ID="LiteralGraficoScoreSerasa" runat="server"></asp:Literal>

    <!--Javascript Limite Crédito-->
    <asp:Literal ID="LiteralGraficoLimiteCredito" runat="server"></asp:Literal>

    <uc1:AnaliseCreditoDetalheWebUserControl runat="server" ID="AnaliseCreditoDetalheWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>     
    
</asp:Content>

