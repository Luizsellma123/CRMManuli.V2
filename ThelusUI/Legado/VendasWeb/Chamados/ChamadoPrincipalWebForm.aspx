<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ChamadoPrincipalWebForm.aspx.cs" Inherits="VendasWeb.Chamados.ChamadoPrincipalWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlChamado.ascx" TagPrefix="uc1" TagName="WebUserControlChamado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/ChamadoPrincipalJavaScript.js?aux=7")%>" type="text/javascript"></script>
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
                    <h3 class="panel-title">Cadastro Chamado</h3>
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

                    <asp:UpdatePanel ID="CamposUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="SolicitanteLabel" runat="server" Text="Solicitante:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="SolicitanteDropDownList" runat="server" AutoPostBack="true"
                                            CssClass="form-control fstdropdown-select" OnSelectedIndexChanged="SolicitanteDropDownList_SelectedIndexChanged">
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
                                        <asp:Label ID="ResponsavelLabel" runat="server" Text="Responsável:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ResponsavelDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="DataLabel" runat="server" Text="Data:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" ID="DataTextBox" runat="server" TextMode="date"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="ClassificacaoLabel" runat="server" Text="Classificação:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ClassificacaoDropDownList" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="StatusLabel" runat="server" Text="Status:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="SistemaLabel" runat="server" Text="Sistema:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="SistemaDropDownList" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="PrioridadeLabel" runat="server" Text="Prioridade:"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="PrioridadeDropDownList" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
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
                                        <asp:Label ID="AssuntoLabel" runat="server" Text="Assunto:"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" ID="AssuntoBreveTextBox" runat="server" placeholder="Informe o assunto."></asp:TextBox>
                                    </div>
                                </div>

                                <!--===================================================-->
                                <!-- END LINHA 1 - Painel FILTROS-->
                            </div>

                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="DescricaoLabel" runat="server" Text="Descrição:"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" Width="100%" Height="90px" ID="DescricaoTextBox" runat="server" TextMode="MultiLine" placeholder="Descreva o chamado detalhadamente."></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="SolicitanteDropDownList" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>

                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                            <div class="col-sm-auto">

                                <asp:UpdatePanel ID="AprovarUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <asp:LinkButton ID="AprovarLinkButton" class="btn btn-success btn-labeled fa fa-check-square fa-lg"
                                            runat="server"  OnClick="EsconderAprovarReprovarLinkButton_Click"
                                            OnClientClick="AnalisePedido();">Aprovar</asp:LinkButton>

                                        <asp:LinkButton ID="ReprovarLinkButton" class="btn btn-danger btn-labeled fa fa-times-circle fa-lg"
                                            runat="server" OnClick="EsconderAprovarReprovarLinkButton_Click"
                                            OnClientClick="AnalisePedido();">Reprovar</asp:LinkButton>

                                        <asp:LinkButton ID="GravarLinkButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                            runat="server" OnClick="GravarLinkButton_Click">Gravar</asp:LinkButton>

                                        <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                            runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AprovarLinkButton" />
                                        <asp:AsyncPostBackTrigger ControlID="ReprovarLinkButton" />
                                        <asp:PostBackTrigger ControlID="GravarLinkButton" />
                                        <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

    <div id="ChamadoDiv">

        <div id="ChamadoModal" class="modal fade bd-example-modal-xl">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header" style="margin-top: 15px;">
                        <h4 id="ChamadoModalTitle" class="modal-title"><strong>Chamados - Principal Aprovar</strong></h4>
                    </div>

                    <div id="ChamadoModalBody" class="modal-body">

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Solicitante:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="SolicitanteModalDropDownList" runat="server"
                                        CssClass="form-control" Enabled="false">
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
                                    <asp:TextBox class="form-control" ID="NumeroChamadoModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Evento:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="EventoDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Categoria:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="CategoriaDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Histórico:"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-12">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="HistoricoTextBox" Height="100px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="modal-footer">

                        <asp:LinkButton ID="GravarModalLinkButton"
                            class="btn btn-success btn-labeled fa fa-save fa-lg"
                            runat="server" OnClick="GravarLinkButton_Click">Gravar</asp:LinkButton>

                        <asp:LinkButton runat="server"
                            class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            data-dismiss="modal">Retornar</asp:LinkButton>

                    </div>

                </div>
            </div>
        </div>

    </div>

    <uc1:WebUserControlChamado runat="server" ID="WebUserControlChamado" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
