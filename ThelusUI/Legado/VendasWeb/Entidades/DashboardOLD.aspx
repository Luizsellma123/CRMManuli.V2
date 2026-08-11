<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="DashboardOLD.aspx.cs" Inherits="VendasWeb.Entidades.Dashboard" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- LINHA 1-->
    <div class="row">
        <!--COLUNA 1-->
        <!--===================================================-->
        <div class="col-lg-7">
            <!--Título da Página-->
            <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
            <div id="page-title">
                <h1 class="page-header text-overflow">
                    Dashboard</h1>
            </div>
            <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
            <!--Breadcrumb-->
            <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
            <ol class="breadcrumb">
                <!--<li><a href="#">Visualizando dados de:</a></li>
					<li><a href="#">Regional XXX</a></li>
					<li class="active">Nome do Representante</li>-->
            </ol>
            <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
            <!--End breadcrumb-->
            <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
            <!--End Título da Página-->
            <!--Filtro de Dashboard-->
            <div class="panel panel-default">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <button type="button" class="btn btn-default collapsed" data-target="#ctl00_ContentPlaceHolder1_filtros"
                            data-toggle="collapse" aria-expanded="false">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                    </div>
                    <h3 class="panel-title">
                        Alterar Dados de Dashboard</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <div id="filtros" class="collapse in"  runat="server">
                    <div class="panel-body">
                        <!-- LINHA 1 - Painel FILTROS-->
                        <!--===================================================-->
                        <div class="row">
                            <div class="col-sm-5">
                                <h5>
                                    <asp:Label ID="GestorLabel" runat="server" Text="Gestor:" CssClass="text-thin"></asp:Label></h5>
                                <select class="selectpicker show-tick" multiple data-placeholder="Escolha um Gestor..."
                                    title="Escolha um Gestor..." data-style="btn-primary" data-live-search="true"
                                    id="GestorDropDownList" runat="server">
                                </select>
                                <asp:RequiredFieldValidator ID="GestorRequiredFieldValidator" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="GestorDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-4">
                                <br />
                                <br />
                                <asp:LinkButton ID="GestorLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="GestorDropDownList_SelectedIndexChanged">Buscar Classes</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-5">
                                <h5>
                                    <asp:Label ID="ClasseLabel" runat="server" Text="Classe:" CssClass="text-thin"></asp:Label></h5>
                                <select class="selectpicker show-tick" multiple data-placeholder="Escolha uma Classe..."
                                    title="Escolha uma Classe..." data-style="btn-primary" data-live-search="true"
                                    id="ClasseDropDownList" runat="server">
                                </select>
                                <asp:RequiredFieldValidator ID="ClasseRequiredFieldValidator" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="ClasseDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-4">
                                <br />
                                <br />
                                <asp:LinkButton ID="ClasseLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                                    CausesValidation="false" runat="server" OnClick="ClasseDropDownList_SelectedIndexChanged">Buscar Vendedores</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <h5>
                                    <asp:Label ID="VendedorLabel" runat="server" Text="Vendedor:" CssClass="text-thin"></asp:Label></h5>
                                <select class="selectpicker show-tick" multiple data-placeholder="Escolha um Vendedor..."
                                    title="Escolha um Vendedor..." data-style="btn-primary" data-live-search="true"
                                    id="VendedorDropDownList" runat="server">
                                </select>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="VendedorDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>
                </div>
                <!-- END Painel FILTROS-->
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- Botões de buscar e limpar-->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">
                            <asp:LinkButton ID="BuscarLinkButton" class="btn btn-dark btn-labeled fa fa-search fa-lg"
                                runat="server" OnClick="BuscarLinkButton_Click">Buscar</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <!--End Filtro de Dashboard-->
            <!--Volume de Vendas-->
            <!--===================================================-->
            <%--<div class="panel panel-bordered panel-info">
                <div class="panel-heading">
                    <div class="panel-control">
                        <button class="demo-panel-ref-btn btn btn-default" data-target="#volume" data-toggle="collapse"
                            aria-expanded="true">
                            Mostrar / Ocultar painel <i class="fa fa-chevron-down"></i>
                        </button>
                        <div class="btn-group">
                            <button data-toggle="dropdown" class="dropdown-toggle btn btn-primary">
                                <i class="fa fa-line-chart fa-lg"></i>Mais Informações
                            </button>
                            <ul class="dropdown-menu dropdown-menu-right">
                                <li><a href="#">Ativos</a></li>
                                <li><a href="#">Inativos</a></li>
                                <li><a href="#">Prospectivos</a></li>
                                <li class="divider"></li>
                                <li><a href="#">Comparativo entre períodos</a></li>
                            </ul>
                        </div>
                    </div>
                    <h3 class="panel-title">
                        Volume de Vendas</h3>
                </div>
                <div class="table-responsive">
                    <table class="table table-hover table-vcenter">
                        <thead>
                            <tr>
                                <th class="min-width">
                                    Período
                                </th>
                                <th>
                                </th>
                                <th class="text-center">
                                    Stretch
                                </th>
                                <th class="text-center">
                                    Fita PP
                                </th>
                                <th class="text-center">
                                    Fita Impressa
                                </th>
                                <th class="text-center">
                                    Máquinas
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-light"><i class="fa fa-clock-o fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Hoje</span>
                                    <br>
                                    <small class="text-muted">22/04/2016</small>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold">99.000 kg</span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold">99.000 m²</span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold">99.000 m²</span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold">99.000 un</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray"><i class="fa fa-calendar-times-o fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Acumulado</span>
                                    <br>
                                    <small class="text-muted">de 01/04 a 22/04</small>
                                </td>
                                <td class="text-center">
                                    <span class="text-success text-semibold">99.000 kg</span>
                                </td>
                                <td class="text-center">
                                    <span class="text-danger text-semibold">99.000 m²</span>
                                </td>
                                <td class="text-center">
                                    <span class="text-warning text-semibold">99.000 m²</span>
                                </td>
                                <td class="text-center">
                                    <span class="text-success text-semibold">99.000 un</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-calendar fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Mês anterior</span>
                                    <br>
                                    <small class="text-muted">Março/2016</small>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold">99.000 kg</span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold">99.000 m²</span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold">99.000 m²</span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold">99.000 un</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-info"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com a Expectativa</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado Atual com a Expectativa calculada</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-primary"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Mês Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Mês Atual com Mês Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Ano Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Ano Atual com o Ano Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>--%>
            <!--===================================================-->
            <!--End Volume de Vendas-->


            <!--Entrada de Pedidos-->
            <!--===================================================-->
            <div class="panel panel-bordered panel-success">
                <div class="panel-heading">
                    <div class="panel-control">
                        <button class="demo-panel-ref-btn btn btn-default" data-target="#ctl00_ContentPlaceHolder1_posicaoEntrada"
                            data-toggle="collapse" type="button" aria-expanded="true">
                            Mostrar / Ocultar painel <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--
                        <div class="btn-group">
                            <button data-toggle="dropdown" class="dropdown-toggle btn btn-mint" aria-expanded="false">
                                <i class="fa fa-line-chart fa-lg"></i>Mais Informações
                            </button>
                            <ul class="dropdown-menu dropdown-menu-right">
                                <li><a href="#">Semestral</a></li>
                                <li><a href="#">Anual</a></li>
                                <li><a href="#">Mensal</a></li>
                                <li class="divider"></li>
                                <li><a href="#">Comparativo entre períodos</a></li>
                            </ul>
                        </div>--%>
                    </div>
                    <h3 class="panel-title">
                        Entrada de Pedidos</h3>
                </div>
                <div class="table-responsive">
                    <div id="posicaoEntrada" class="collapse" aria-expanded="true" runat="server">
                        <table class="table table-hover table-vcenter">
                            <thead>
                                <tr>
                                    <th class="min-width">
                                        Período
                                    </th>
                                    <th>
                                    </th>
                                    <th class="text-center">
                                        Stretch
                                    </th>
                                    <th class="text-center">
                                        Fita PP
                                    </th>
                                    <th class="text-center">
                                        Fita Impressa
                                    </th>
                                    <th class="text-center">
                                        Máquinas
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-light"><i class="fa fa-clock-o fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Hoje</span>
                                        <br>
                                        <small class="text-muted">
                                            <asp:Label ID="EntradaHojeLabel" runat="server" Text="dd/MM/yyyy"></asp:Label>
                                        </small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaHojeStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaHojeFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaHojeFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaHojeMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-gray"><i class="fa fa-calendar-times-o fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Acumulado</span>
                                        <br>
                                        <small class="text-muted">
                                            <asp:Label ID="EntradaAcumuladoLabel" runat="server" Text="de dd/MM a dd/MM"></asp:Label>
                                        </small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaAcumuladoStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaAcumuladoFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaAcumuladoFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaAcumuladoMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-calendar fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Mês anterior</span>
                                        <br>
                                        <small class="text-muted">
                                            <asp:Label ID="EntradaMesAnteriorLabel" runat="server" Text="MM/yyyy"></asp:Label>
                                        </small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaMesAnteriorStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaMesAnteriorFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaMesAnteriorFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="EntradaMesAnteriorMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                </tr>
                                <%-- <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-mint"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com a Expectativa</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado Atual com a Expectativa calculada</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                                <%-- <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-success"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Mês Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Mês Atual com Mês Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                                <%--<tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Ano Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Ano Atual com o Ano Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!--===================================================-->
            <!--End Entrada de Pedidos-->



            <!--Pedidos Pendentes-->
            <!--===================================================-->
            <div class="panel panel-bordered panel-dark">
                <div class="panel-heading">
                    <div class="panel-control">
                        <button class="demo-panel-ref-btn btn btn-default" data-target="#ctl00_ContentPlaceHolder1_PedidosPendentes"
                            data-toggle="collapse" type="button" aria-expanded="true">
                            Mostrar / Ocultar painel <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--<div class="btn-group">
                            <button data-toggle="dropdown" class="dropdown-toggle btn btn-default" aria-expanded="false">
                                <i class="fa fa-line-chart fa-lg"></i>Mais Informações
                            </button>
                            <ul class="dropdown-menu dropdown-menu-right">
                                <li><a href="#">Semestral</a></li>
                                <li><a href="#">Anual</a></li>
                                <li><a href="#">Mensal</a></li>
                                <li class="divider"></li>
                                <li><a href="#">Comparativo entre períodos</a></li>
                            </ul>
                        </div>--%>
                    </div>
                    <h3 class="panel-title">
                        Pedidos Pendentes</h3>
                </div>
                <div class="table-responsive">
                    <div id="PedidosPendentes" class="collapse" aria-expanded="true" runat="server">
                        <table class="table table-hover table-vcenter">
                            <thead>
                                <tr>
                                    <th class="min-width">
                                        Período
                                    </th>
                                    <th>
                                    </th>
                                    <th class="text-center">
                                        Stretch
                                    </th>
                                    <th class="text-center">
                                        Fita PP
                                    </th>
                                    <th class="text-center">
                                        Fita Impressa
                                    </th>
                                    <th class="text-center">
                                        Máquinas
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-light"><i class="fa fa-clock-o fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Hoje</span>
                                        <br>
                                        <small class="text-muted">
                                            <asp:Label ID="PedidosPendentesHojeLabel" runat="server" Text="dd/MM/yyyy"></asp:Label>
                                        </small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesHojeStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesHojeFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesHojeFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesHojeMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-gray"><i class="fa fa-calendar-times-o fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Acumulado</span>
                                        <br>
                                        <small class="text-muted">
                                            <asp:Label ID="PedidosPendentesAcumuladoLabel" runat="server" Text="de dd/MM a dd/MM"></asp:Label>
                                        </small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesAcumuladoStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesAcumuladoFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesAcumuladoFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesAcumuladoMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-calendar fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Mês anterior</span>
                                        <br>
                                        <small class="text-muted">
                                            <asp:Label ID="PedidosPendentesMesAnteriorLabel" runat="server" Text="xxxx/yyyy"></asp:Label>
                                        </small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesMesAnteriorStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesMesAnteriorFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesMesAnteriorFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PedidosPendentesMesAnteriorMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                </tr>
                                <%-- <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-mint"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com a Expectativa</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado Atual com a Expectativa calculada</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                                <%-- <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-success"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Mês Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Mês Atual com Mês Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                                <%--<tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Ano Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Ano Atual com o Ano Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!--===================================================-->
            <!--End Pedidos Pendentes-->


            <!--Pedidos Faturados-->
            <!--===================================================-->
            <div class="panel panel-bordered panel-warning">
                <div class="panel-heading">
                    <div class="panel-control">
                    <button class="demo-panel-ref-btn btn btn-default" data-target="#ctl00_ContentPlaceHolder1_PedidosFaturados"
                            data-toggle="collapse" type="button" aria-expanded="true">
                            Mostrar / Ocultar painel <i class="fa fa-chevron-down"></i>
                        </button>
                       <%-- <div class="btn-group">
                            <button data-toggle="dropdown" class="dropdown-toggle btn btn-danger" aria-expanded="false">
                                <i class="fa fa-line-chart fa-lg"></i>Mais Informações
                            </button>
                            <ul class="dropdown-menu dropdown-menu-right">
                                <li><a href="#">Semestral</a></li>
                                <li><a href="#">Anual</a></li>
                                <li><a href="#">Mensal</a></li>
                                <li class="divider"></li>
                                <li><a href="#">Comparativo entre períodos</a></li>
                            </ul>
                        </div>--%>
                    </div>
                    <h3 class="panel-title">
                        Pedidos Faturados</h3>
                </div>
                <div class="table-responsive">
                <div id="PedidosFaturados" class="collapse" aria-expanded="true" runat="server">
                    <table class="table table-hover table-vcenter">
                        <thead>
                            <tr>
                                <th class="min-width">
                                    Período
                                </th>
                                <th>
                                </th>
                                <th class="text-center">
                                    Stretch
                                </th>
                                <th class="text-center">
                                    Fita PP
                                </th>
                                <th class="text-center">
                                    Fita Impressa
                                </th>
                                <th class="text-center">
                                    Máquinas
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-light"><i class="fa fa-clock-o fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Hoje</span>
                                    <br>
                                    <small class="text-muted"><asp:Label ID="PedidosFaturadosHojeLabel" runat="server" Text="dd/MM/yyyy"></asp:Label> </small>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosHojeStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosHojeFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosHojeFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosHojeMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray"><i class="fa fa-calendar-times-o fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Acumulado</span>
                                    <br>
                                    <small class="text-muted"><asp:Label ID="PedidosFaturadosAcumuladoLabel" runat="server" Text="de dd/MM a dd/MM"></asp:Label> </small>
                                </td>
                                <td class="text-center">
                                   <span class="text text-semibold"><asp:Label ID="PedidosFaturadosAcumuladoStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosAcumuladoFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosAcumuladoFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosAcumuladoMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-calendar fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Mês anterior</span>
                                    <br>
                                    
                                    <small class="text-muted"><asp:Label ID="PedidosFaturadosMesAnteriorLabel" runat="server" Text="xxxx/yyyy"></asp:Label> </small>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosMesAnteriorStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosMesAnteriorFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosMesAnteriorFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="PedidosFaturadosMesAnteriorMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-mint"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com a Expectativa</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado Atual com a Expectativa calculada</small>
                                </td>
                                <td class="text-center">
                                    <asp:Label ID="ExpectativaStretchLabel" runat="server" Text="NDA"></asp:Label>
                                    <%--<i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">- 23%</span>--%>
                                </td>
                                <td class="text-center">
                                    <asp:Label ID="ExpectativaFitaPPLabel" runat="server" Text="NDA"></asp:Label>
                                    <%--<i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark"> 23%</span>--%>
                                </td>
                                <td class="text-center">
                                    <asp:Label ID="ExpectativaFitaImpressaLabel" runat="server" Text="NDA"></asp:Label>
                                    <%--<i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark"> + 3%</span>--%>
                                </td>
                                <td class="text-center">
                                    <asp:Label ID="ExpectativaMaquinasLabel" runat="server" Text="NDA"></asp:Label>
                                   <%-- <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark"> - 23%</span>--%>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-success"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Mês Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Mês Atual com Mês Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Ano Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Ano Atual com o Ano Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                        </tbody>
                      
                    </table>
                    </div>
                </div>
            </div>
            <!--===================================================-->
            <!--End Pedidos Faturados-->


            <!--Devoluções-->
            <!--===================================================-->
            <div class="panel panel-bordered panel-danger">
                <div class="panel-heading">
                    <div class="panel-control">
                    <button class="demo-panel-ref-btn btn btn-default" data-target="#ctl00_ContentPlaceHolder1_Devolucoes"
                            data-toggle="collapse" type="button" aria-expanded="true">
                            Mostrar / Ocultar painel <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--<div class="btn-group">
                            <button data-toggle="dropdown" class="dropdown-toggle btn btn-warning" aria-expanded="false">
                                <i class="fa fa-line-chart fa-lg"></i>Mais Informações
                            </button>
                            <ul class="dropdown-menu dropdown-menu-right">
                                <li><a href="#">Volume de Vendas Semestral</a></li>
                                <li><a href="#">Volume de Vendas Anual</a></li>
                                <li><a href="#">Volume de Vendas Mensal</a></li>
                                <li class="divider"></li>
                                <li><a href="#">Comparativo entre períodos</a></li>
                            </ul>
                        </div>--%>
                    </div>
                    <h3 class="panel-title">
                        Devoluções</h3>
                </div>
                <div class="table-responsive">
                <div id="Devolucoes" class="collapse" aria-expanded="true" runat="server">
                    <table class="table table-hover table-vcenter">
                        <thead>
                            <tr>
                                <th class="min-width">
                                    Período
                                </th>
                                <th>
                                </th>
                                <th class="text-center">
                                    Stretch
                                </th>
                                <th class="text-center">
                                    Fita PP
                                </th>
                                <th class="text-center">
                                    Fita Impressa
                                </th>
                                <th class="text-center">
                                    Máquinas
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-light"><i class="fa fa-clock-o fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Hoje</span>
                                    <br>
                                    <small class="text-muted"><asp:Label ID="DevolucoesHojeLabel" runat="server" Text="dd/MM/yyyy"></asp:Label> </small>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesHojeStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesHojeFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesHojeFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesHojeMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray"><i class="fa fa-calendar-times-o fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Acumulado</span>
                                    <br>
                                    <small class="text-muted"><asp:Label ID="DevolucoesAcumuladoLabel" runat="server" Text="de dd/MM a dd/MM"></asp:Label> </small>
                                </td>
                                <td class="text-center">
                                   <span class="text text-semibold"><asp:Label ID="DevolucoesAcumuladoStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesAcumuladoFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesAcumuladoFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesAcumuladoMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-calendar fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Mês anterior</span>
                                    <br>
                                    
                                    <small class="text-muted"><asp:Label ID="DevolucoesMesAnteriorLabel" runat="server" Text="xxxx/yyyy"></asp:Label> </small>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesMesAnteriorStretchLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesMesAnteriorFitaPPLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesMesAnteriorFitaImpressaLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                                <td class="text-center">
                                    <span class="text text-semibold"><asp:Label ID="DevolucoesMesAnteriorMaquinasLabel" runat="server" Text="NDA"></asp:Label></span>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-mint"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com a Expectativa</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado Atual com a Expectativa calculada</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                            <%-- <tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-success"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Mês Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Mês Atual com Mês Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td class="text-center">
                                    <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-percent fa-lg">
                                    </i></span>
                                </td>
                                <td>
                                    <span class="text-semibold">Comparativo com Ano Anterior</span>
                                    <br>
                                    <small class="text-muted">Compara Acumulado do Ano Atual com o Ano Anterior</small>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">
                                        23%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-dot-circle-o text-warning"></i><span class="label label-warning text-dark">
                                        + 3%</span>
                                </td>
                                <td class="text-center">
                                    <i class="fa fa-lg fa-arrow-circle-down text-danger"></i><span class="label label-danger text-dark">
                                        - 23%</span>
                                </td>
                            </tr>--%>
                        </tbody>
                    </table>
                    </div>
                </div>
            </div>
            <!--===================================================-->
            <!--End Devoluções-->



            <!--Posição de Carteira-->
            <!--===================================================-->
            <div class="panel panel-bordered panel-purple">
                <div class="panel-heading">
                    <div class="panel-control">
                        <button class="demo-panel-ref-btn btn btn-default" data-target="#ctl00_ContentPlaceHolder1_posicao"
                            data-toggle="collapse" type="button" aria-expanded="true">
                            Mostrar / Ocultar painel <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--<div class="btn-group">
                            <button data-toggle="dropdown" class="dropdown-toggle btn btn-pink">
                                <i class="fa fa-line-chart fa-lg"></i>Mais Informações
                            </button>
                            <ul class="dropdown-menu dropdown-menu-right">
                                <li><a href="#">Ativos</a></li>
                                <li><a href="#">Inativos</a></li>
                                <li><a href="#">Prospectivos</a></li>
                                <li class="divider"></li>
                                <li><a href="#">Comparativo entre períodos</a></li>
                            </ul>
                        </div>--%>
                    </div>
                    <h3 class="panel-title">
                        Posição de Carteira</h3>
                </div>
                <div id="posicao" class="collapse" aria-expanded="true" runat="server">
                    <div class="table-responsive">
                        <table class="table table-hover table-vcenter mar-no">
                            <thead>
                                <tr>
                                    <th class="min-width">
                                        Período
                                    </th>
                                    <th>
                                    </th>
                                    <th class="text-center">
                                        Mês Atual
                                    </th>
                                    <th class="text-center">
                                        Média dos últimos 3 meses
                                    </th>
                                    <th class="text-center">
                                        Posição Atual
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="PosicaoCarteiraLiteral" runat="server" Text=""></asp:Literal>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-success"><i class="fa fa-user fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Ativos</span>
                                        <br>
                                        <small class="text-muted">Clientes com movimentação</small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcAtivoLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcMediaAtivoLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <asp:Literal ID="PcPaAtivoLiteral" runat="server" Text="NDA"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-danger"><i class="fa fa-user-times fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Inativos</span>
                                        <br>
                                        <small class="text-muted">Clientes sem movimentação</small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcInativoLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcMediaInativoLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <asp:Literal ID="PcPaInativoLiteral" runat="server" Text="NDA"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-gray-dark"><i class="fa fa-user-secret fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Prospectivos</span>
                                        <br>
                                        <small class="text-muted">Clientes em Prospecção</small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcProspectivoLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcMediaProspectivoLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <asp:Literal ID="PcPaProspectivoLiteral" runat="server" Text="NDA"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-warning"><i class="fa fa-user-plus fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Novos</span>
                                        <br>
                                        <small class="text-muted">Clientes Novos</small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcNovoLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcMediaNovoLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <asp:Literal ID="PcPaNovoLiteral" runat="server" Text="NDA"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <span class="icon-wrap icon-wrap-sm icon-circle bg-purple"><i class="fa fa-users fa-lg">
                                        </i></span>
                                    </td>
                                    <td>
                                        <span class="text-semibold">Total</span>
                                        <br>
                                        <small class="text-muted">Total de Clientes na Carteira</small>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcTotalAtualLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <span class="text text-semibold">
                                            <asp:Label ID="PcTotalMediaLabel" runat="server" Text="NDA"></asp:Label></span>
                                    </td>
                                    <td class="text-center">
                                        <!--<i class="fa fa-lg fa-arrow-circle-up text-success"></i><span class="label label-success text-dark">23%</span>-->
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!--===================================================-->
            <!--End Posição de Carteira-->
        </div>
        <!--End COLUNA 1-->
        <!--===================================================-->
        <!--COLUNA 2-->
        <!--===================================================-->
        <div class="col-lg-5">
            <div class="row">
                <div class="col-sm-6 col-lg-6">
                    <div class="col-xs-12 pad-no">
                        <div class="panel media">
                            <div class="media-left pad-all">
                                <span class="icon-wrap icon-wrap-sm icon-circle bg-primary"><i class="fa fa-database fa-2x">
                                </i></span>
                            </div>
                            <div class="media-body pad-top">
                                <p class="text-thin mar-no text-sm">
                                    <span class="text-primary text-bold text-lg">Stretch</span> Preço Médio:
                                </p>
                                <p class="text-lg mar-no text-thin">
                                    R$ <span class="text-2x text-bold text-primary">9,00</span>/kg</p>
                            </div>
                            <div class="progress progress-xs progress-dark-base mar-no">
                                <div style="width: 23%" class="progress-bar progress-bar-primary" aria-valuemax="100"
                                    aria-valuemin="0" aria-valuenow="23" role="progressbar">
                                </div>
                            </div>
                            <div class="panel-footer">
                                <p class="mar-no text-sm">
                                    <span class="label label-danger">- 23%</span> Menor que o ideal (<span class="text-bold">9,90</span>)</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 pad-no">
                        <div class="panel media">
                            <div class="media-left pad-all">
                                <span class="icon-wrap icon-wrap-sm icon-circle bg-pink"><i class="fa fa-map-o fa-2x">
                                </i></span>
                            </div>
                            <div class="media-body pad-top">
                                <p class="text-thin mar-no text-sm">
                                    <span class="text-pink text-bold text-lg">Fita PP</span> Preço Médio:
                                </p>
                                <p class="text-lg mar-no text-thin">
                                    R$ <span class="text-2x text-bold text-pink">9,00</span>/m²</p>
                            </div>
                            <div class="progress progress-xs progress-dark-base mar-no">
                                <div style="width: 23%" class="progress-bar progress-bar-pink" aria-valuemax="100"
                                    aria-valuemin="0" aria-valuenow="23" role="progressbar">
                                </div>
                            </div>
                            <div class="panel-footer">
                                <p class="mar-no text-sm">
                                    <span class="label label-danger">- 23%</span> Menor que o ideal (<span class="text-bold">9,90</span>)</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 pad-no">
                        <div class="panel media">
                            <div class="media-left pad-all">
                                <span class="icon-wrap icon-wrap-sm icon-circle bg-purple"><i class="fa fa-map fa-2x">
                                </i></span>
                            </div>
                            <div class="media-body pad-top">
                                <p class="text-thin mar-no text-sm">
                                    <span class="text-purple text-bold text-lg">Fita Imp</span> Preço Médio:
                                </p>
                                <p class="text-lg mar-no text-thin">
                                    R$ <span class="text-2x text-bold text-purple">9,00</span>/m²</p>
                            </div>
                            <div class="progress progress-xs progress-dark-base mar-no">
                                <div style="width: 23%" class="progress-bar progress-bar-purple" aria-valuemax="100"
                                    aria-valuemin="0" aria-valuenow="23" role="progressbar">
                                </div>
                            </div>
                            <div class="panel-footer">
                                <p class="mar-no text-sm">
                                    <span class="label label-danger">- 23%</span> Menor que o ideal (<span class="text-bold">9,90</span>)</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-lg-6">
                    <div class="col-xs-12 pad-no">
                        <div class="panel media panel-primary panel-colorful">
                            <div class="media-left pad-all">
                                <span class="icon-wrap icon-wrap-sm icon-circle bg-light text-primary"><i class="fa fa-database fa-2x">
                                </i></span>
                            </div>
                            <div class="media-body pad-top">
                                <p class="text-thin mar-no text-lg">
                                    <span class="text-primary text-bold text-lg">Stretch </span>
                                    <button class="btn btn-sm btn-primary btn-hover-info add-tooltip" data-placement="top"
                                        data-toggle="tooltip" data-original-title="Expectativa = Entrada de Pedidos + Pendentes + Faturados - Devoluções">
                                        Expectativa</button></p>
                                <p class="text-lg mar-no text-thin">
                                    R$ <span class="text-lg text-bold text-primary">99.009,00</span></p>
                            </div>
                            <div class="progress progress-xs progress-dark-base mar-no">
                                <div style="width: 23%" class="progress-bar progress-bar-light" aria-valuemax="100"
                                    aria-valuemin="0" aria-valuenow="23" role="progressbar">
                                </div>
                            </div>
                            <div class="panel-footer">
                                <p class="mar-no text-sm">
                                    <span class="label label-danger">- 23%</span> Ideal: (<span class="text-bold">99.999,90</span>)</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 pad-no">
                        <div class="panel media panel-pink panel-colorful">
                            <div class="media-left pad-all">
                                <span class="icon-wrap icon-wrap-sm icon-circle bg-light text-pink"><i class="fa fa-map-o fa-2x">
                                </i></span>
                            </div>
                            <div class="media-body pad-top">
                                <p class="text-thin mar-no text-sm">
                                    <span class="text-pink text-bold text-lg">Fita PP</span> Expectativa</p>
                                <p class="text-lg mar-no text-thin">
                                    R$ <span class="text-lg text-bold text-pink">99.009,00</span></p>
                            </div>
                            <div class="progress progress-xs progress-dark-base mar-no">
                                <div style="width: 23%" class="progress-bar progress-bar-light" aria-valuemax="100"
                                    aria-valuemin="0" aria-valuenow="23" role="progressbar">
                                </div>
                            </div>
                            <div class="panel-footer">
                                <p class="mar-no text-sm">
                                    <span class="label label-danger">- 23%</span> Ideal: (<span class="text-bold">99.999,90</span>)</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 pad-no">
                        <div class="panel media panel-purple panel-colorful">
                            <div class="media-left pad-all">
                                <span class="icon-wrap icon-wrap-sm icon-circle bg-light text-purple"><i class="fa fa-map fa-2x">
                                </i></span>
                            </div>
                            <div class="media-body pad-top">
                                <p class="text-thin mar-no text-sm">
                                    <span class="text-purple text-bold text-lg">Stretch</span> Expectativa</p>
                                <p class="text-lg mar-no text-thin">
                                    R$ <span class="text-lg text-bold text-purple">99.009,00</span></p>
                            </div>
                            <div class="progress progress-xs progress-dark-base mar-no">
                                <div style="width: 23%" class="progress-bar progress-bar-light" aria-valuemax="100"
                                    aria-valuemin="0" aria-valuenow="23" role="progressbar">
                                </div>
                            </div>
                            <div class="panel-footer">
                                <p class="mar-no text-sm">
                                    <span class="label label-danger">- 23%</span> Ideal: (<span class="text-bold">99.999,90</span>)</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="panel panel-danger panel-colorful">
                        <div class="pad-all media">
                            <div class="media-left">
                                <span class="icon-wrap icon-wrap-xs"><i class="fa fa-exclamation-triangle fa-fw fa-2x">
                                </i></span>
                            </div>
                            <div class="media-body">
                                <p class="h3 text-thin media-heading">
                                    10%</p>
                                <small class="text-uppercase">Inadimplência</small>
                            </div>
                        </div>
                        <div class="progress progress-xs progress-dark-base mar-no">
                            <div style="width: 10%" class="progress-bar progress-bar-light" aria-valuemax="100"
                                aria-valuemin="0" aria-valuenow="10" role="progressbar">
                            </div>
                        </div>
                        <div class="pad-all text-right">
                            <small><span class="text-semibold"><i class="fa fa-arrow-up fa-fw"></i>10%</span> Maior
                                que o valor ideal (0%)</small>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="panel panel-info panel-colorful">
                        <div class="pad-all media">
                            <div class="media-left">
                                <span class="icon-wrap icon-wrap-xs"><i class="fa fa-comments fa-fw fa-2x"></i></span>
                            </div>
                            <div class="media-body">
                                <p class="h3 text-thin media-heading">
                                    36 Atendimentos</p>
                                <small class="text-uppercase">Contatos com CLientes</small>
                            </div>
                        </div>
                        <div class="progress progress-xs progress-dark-base mar-no">
                            <div style="width: 20%" class="progress-bar progress-bar-light" aria-valuemax="100"
                                aria-valuemin="0" aria-valuenow="20" role="progressbar">
                            </div>
                        </div>
                        <div class="pad-all text-right">
                            <small><span class="text-semibold"><i class="fa fa-lg fa-arrow-circle-up text-success">
                            </i><span class="label label-success text-dark">20%</span> </span>Maior que o valor
                                ideal (XXX VALOR IDEAL)</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
