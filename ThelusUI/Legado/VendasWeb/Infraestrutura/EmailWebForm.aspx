<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="EmailWebForm.aspx.cs" Inherits="VendasWeb.Infraestrutura.EmailWebForm" %>

<%@ Register Src="~/usercontrol/InfraestruturaMaquinaWebUserControl.ascx" TagPrefix="uc1" TagName="InfraestruturaMaquinaWebUserControl" %>

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
                    <h3 class="panel-title">Infraestrutura - Informações da máquina</h3>
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

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label runat="server" Text="MAC:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox ID="MACTextBox" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label runat="server" Text="IP:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox ID="IPTextBox" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox ID="NomeTextBox" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Intervalo de Envio:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="IntervaloEnvioTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Limite de Uso CPU:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="LimiteUsoCPUPorcentagemTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Limite de Uso RAM:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="LimiteUsoRAMPorcentagemTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Limite de Uso Disco:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="LimiteUsoDiscoPorcentagemTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Email Remetente:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="EmailRemetenteTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Senha Email Remetente:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="EmailRemetenteSenhaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Email Host:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="EmailHostTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Email Port:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="EmailPortTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Email Destinatário:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="EmailDestinatarioTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Enviar Alertas:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:CheckBox ID="AlertarSimCheckBox" Text="Sim" runat="server" />
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:CheckBox ID="AlertarNaoCheckBox" Text="Não" runat="server" />
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
                            runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="SalvarAlteracoesButton" class="btn btn-primary btn-labeled fa fa-refresh fa-lg"
                            CausesValidation="false" runat="server" OnClick="SalvarAlteracoesButton_Click">Salvar Alterações</asp:LinkButton>

                    </div>

                </div>

            </div>

        </div>

    </div>

    <uc1:InfraestruturaMaquinaWebUserControl runat="server" ID="InfraestruturaMaquinaWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 

</asp:Content>
