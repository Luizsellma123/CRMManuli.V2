<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="PosicaoFinanceiraResumoWebForm.aspx.cs" Inherits="VendasWeb.Controladoria.PosicaoFinanceiraResumoWebForm" %>

<%@ Register Src="~/usercontrol/PosicaoDiariaWebUserControl.ascx" TagPrefix="uc1" TagName="PosicaoDiariaWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/chart.min.js")%>" type="text/javascript"></script>

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
                    <h3 class="panel-title">Controladoria - Posição Financeira</h3>
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

                    <%--  LINHA 1 --%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Posição:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox Enabled="false" class="form-control" ID="PosicaoTextBox" runat="server"></asp:TextBox>
                                </div>
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
                                    <asp:TextBox Enabled="false" class="form-control" ID="UsuarioTextBox" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2 --%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Período Inicial:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox Enabled="false" class="form-control" ID="PeriodoInicialTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Período Final:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox Enabled="false" class="form-control" ID="PeriodoFinalTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 3 --%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Geração:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox Enabled="false" class="form-control" ID="GeracaoTextBox" runat="server"></asp:TextBox>
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

                        <asp:UpdatePanel ID="PosicaoFinanceiraUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="RetornarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="BaixarLinkButton" class="btn btn-success btn-labeled fa fa-download fa-lg"
                                    CausesValidation="false" runat="server" OnClick="BaixarLinkButton_Click">Baixar</asp:LinkButton>

                                <asp:LinkButton ID="EnviarEmailLinkButton" class="btn btn-success btn-labeled fa fa-envelope fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="EnviarEmailLinkButton_Click">Enviar E-mail</asp:LinkButton>

                                <asp:LinkButton ID="NovaAnaliseLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                    CausesValidation="false" runat="server" ClientIDMode="Static"
                                    OnClientClick="MostraModalGeracaoPosicaoFinanceira(this.id);">Nova Análise</asp:LinkButton>
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RetornarButton" />
                                <asp:PostBackTrigger ControlID="BaixarLinkButton" />
                                <asp:PostBackTrigger ControlID="EnviarEmailLinkButton" />
                                <asp:AsyncPostBackTrigger ControlID="NovaAnaliseLinkButton" />                                
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

    </div>

    <uc1:PosicaoDiariaWebUserControl runat="server" ID="PosicaoDiariaWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

    <%--Graficos--%>
    <div>

        <%-- Grafico Consolidado Faturamento + Pendentes  --%>
        <div>
            <div class="row">
                <div class="col-sm-9">

                    <h4 class="m-t-0 header-title">Consolidado Faturamento + Pendentes:</h4>

                    <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                    <div class="col-sm-12">
                        <canvas id="GraficoConsFatuPendTotal" style="background-color: white;"></canvas>
                    </div>                    

                    <div class="col-sm-12">
                        <canvas id="GraficoConsFatuPendQtdTotal" style="background-color: white;"></canvas>
                    </div>

                </div>

                <asp:Literal ID="ConsFatuPendScriptLiteral" runat="server"></asp:Literal>
            </div>

            <div class="row">
                <br>
            </div>

        </div>

        <%-- Grafico Consolidado Faturamento --%>
        <div>
            <div class="row">
                <div class="col-sm-9">

                    <h4 class="m-t-0 header-title">Consolidado Faturamento:</h4>

                    <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                    <div class="col-sm-12">
                        <canvas id="GraficoConsFatuTotal" style="background-color: white;"></canvas>
                    </div>

                    <div class="col-sm-12">
                        <canvas id="GraficoConsFatuQtdTotal" style="background-color: white;"></canvas>
                    </div>

                </div>

                <asp:Literal ID="ConsFatuScriptLiteral" runat="server"></asp:Literal>

            </div>

            <div class="row">
                <br>
            </div>

        </div>

        <%-- Valor Médio (AVG) --%>
        <div>
            <div class="row">
                <div class="col-sm-9">

                    <h4 class="m-t-0 header-title">Valor Médio (AVG):</h4>

                    <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                    <div class="col-sm-12">
                        <canvas id="GraficoCustoMedio" style="background-color: white;"></canvas>
                    </div>

                    <div class="col-sm-12">
                        <canvas id="GraficoConsCustoMedio" style="background-color: white;"></canvas>
                    </div>

                </div>

                <asp:Literal ID="ValorMedioScriptLiteral" runat="server"></asp:Literal>

            </div>

            <div class="row">
                <br>
            </div>

        </div>

        <%-- Faturamento --%>
        <div>
            <div class="row">
                <div class="col-sm-9">

                    <h4 class="m-t-0 header-title">Faturamento:</h4>

                    <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                    <div class="col-sm-12">
                        <canvas id="GraficoFaturamento" style="background-color: white;"></canvas>
                    </div>

                    <div class="col-sm-12">
                        <canvas id="GraficoFaturamentoQtd" style="background-color: white;"></canvas>
                    </div>

                </div>

                <asp:Literal ID="FaturamentoScriptLiteral" runat="server"></asp:Literal>

            </div>

            <div class="row">
                <br>
            </div>

        </div>

        <%-- Pendentes --%>
        <div>
            <div class="row">
                <div class="col-sm-9">

                    <h4 class="m-t-0 header-title">Pendentes:</h4>

                    <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                    <div class="col-sm-12">
                        <canvas id="GraficoPendentes" style="background-color: white;"></canvas>
                    </div>

                    <div class="col-sm-12">
                        <canvas id="GraficoPendentesQtd" style="background-color: white;"></canvas>
                    </div>

                </div>

                <asp:Literal ID="PendentesScriptLiteral" runat="server"></asp:Literal>

            </div>

            <div class="row">
                <br>
            </div>

        </div>

        <%-- Devoluções --%>
        <div>
            <div class="row">
                <div class="col-sm-9">

                    <h4 class="m-t-0 header-title">Devoluções:</h4>

                    <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                    <div class="col-sm-12">
                        <canvas id="GraficoDevolucoes" style="background-color: white;"></canvas>
                    </div>

                    <div class="col-sm-12">
                        <canvas id="GraficoDevolucoesQtd" style="background-color: white;"></canvas>
                    </div>

                </div>

                <asp:Literal ID="DevolucoesScriptLiteral" runat="server"></asp:Literal>

            </div>

            <div class="row">
                <br>
            </div>

        </div>

    </div>

    <%--Modal--%>
    <div>

        <div id="GeracaoPosicaoFinanceiraModal" class="modal fade bd-example-modal-xl">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">

                    <div id="GeracaoPosicaoFinanceiraModalBody" class="modal-body">

                        <h4 class="m-t-0 header-title">Geração Posição Financeira:</h4>

                        <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Período Inicial:" Font-Size="Larger"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="PeriodoInicialModalTextBox" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Período Final:" Font-Size="Larger"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="PeriodoFinalModalTextBox" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="modal-footer">

                        <asp:LinkButton class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" data-dismiss="modal">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="GerarModalLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                            CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="GerarModalLinkButton_Click">Gerar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <script>

            function MostraModalGeracaoPosicaoFinanceira(linkButtonId) {

                if (linkButtonId != null) {

                    $('#GeracaoPosicaoFinanceiraModal').modal();

                    callback();

                }
            }

        </script>

    </div>

</asp:Content>
