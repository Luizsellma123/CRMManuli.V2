<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="CadastroClienteLimiteCreditoWebForm.aspx.cs" Inherits="VendasWeb.Clientes.CadastroClienteLimiteCreditoWebForm" %>

<%@ Register Src="~/usercontrol/UCCadastroCliente.ascx" TagPrefix="uc1" TagName="UCCadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <div class="row">
        <div class="col-sm-9">
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
                    <h3 class="panel-title">Cadastro Cliente - Limite Crédito</h3>
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

                    <asp:HiddenField ID="IDCliente" runat="server" />

                    <div class="row">
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

            </div>


            <div class="panel-footer">
                <div class="row">

                    <div class="panel-control">

                        &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            runat="server" CausesValidation="false" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                    </div>

                </div>
            </div>
        </div>
    </div>

    <!--Javascript Limite Crédito-->
    <asp:Literal ID="LiteralGraficoLimiteCredito" runat="server"></asp:Literal>

    <uc1:UCCadastroCliente runat="server" ID="UCCadastroCliente" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->


</asp:Content>
