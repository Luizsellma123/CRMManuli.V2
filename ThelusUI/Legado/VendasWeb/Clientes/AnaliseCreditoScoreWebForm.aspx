<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AnaliseCreditoScoreWebForm.aspx.cs" Inherits="VendasWeb.Clientes.AnaliseCreditoScoreWebForm" %>

<%@ Register Src="~/usercontrol/AnaliseCreditoDetalheWebUserControl.ascx" TagPrefix="uc1" TagName="AnaliseCreditoDetalheWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>

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
                                    <asp:Label runat="server" Text="Fantasia:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="FantasiaTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Probabilidade inadimplencia --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Prob. Inadim.:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="ProbInadimTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>


                        </div>

                        <%--Risco, Praticas--%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Risco:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="RiscoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Práticas:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="PraticasTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>


                        </div>

                    </div>

                    <div>

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

    <!--Javascript Score Serasa-->
    <asp:Literal ID="LiteralGraficoScoreSerasa" runat="server"></asp:Literal>

    <!--Javascript Limite Crédito-->
    <asp:Literal ID="LiteralGraficoLimiteCredito" runat="server"></asp:Literal>

    <uc1:AnaliseCreditoDetalheWebUserControl runat="server" ID="AnaliseCreditoDetalheWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>     
    
</asp:Content>
