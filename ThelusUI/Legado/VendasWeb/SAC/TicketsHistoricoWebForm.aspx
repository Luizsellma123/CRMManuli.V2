<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="TicketsHistoricoWebForm.aspx.cs" Inherits="VendasWeb.SAC.TicketsHistoricoWebForm" %>

<%@ Register Src="~/usercontrol/TicketWebUserControl.ascx" TagPrefix="uc1" TagName="TicketWebUserControl" %>

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
                    <h3 class="panel-title">SAC - Ticket - Histórico</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text=""
                    runat="server"></asp:Literal>

                <div class="panel-body">

                    <asp:HiddenField ID="IDChamado" runat="server" />

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
                                <asp:Label ID="ClienteLabel" runat="server" Text="Cliente:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-8">
                            <div class="form-group">
                                <asp:TextBox ID="ClienteTextBox" runat="server" CssClass="form-control"
                                    placeholder="Informar código, nome ou CNPJ.">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="TicketLabel" runat="server" Text="Ticket:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:TextBox ID="TicketTextBox" runat="server" CssClass="form-control"
                                    placeholder="Informar o número.">
                                </asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EventoLabel" runat="server" Text="Evento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="EventoDropDownList" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="EventoDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CategoriaLabel" runat="server" Text="Categoria:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="CategoriaDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">

                                <asp:Label ID="HistoricoLabel" runat="server" Text="Novo Histórico:"></asp:Label>

                                <asp:TextBox ID="HistoricoTextBox" Width="100%" Height="90px" TextMode="MultiLine"
                                    runat="server" CssClass="form-control"></asp:TextBox>
                                <span id="counter"></span>

                            </div>
                        </div>
                    </div>

                </div>



                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                            <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                            <asp:LinkButton ID="AdicionarButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                                runat="server" OnClick="AdicionarButton_Click">Adicionar</asp:LinkButton>

                        </div>

                    </div>
                </div>


            </div>

        </div>

        <div class="panel">
            <div class="panel-heading">
                <h3 class="panel-title">Histórico:</h3>
            </div>
            <div class="panel-body">
                <!-- Timeline do Histórico -->
                <!--===================================================-->
                <div class="timeline">
                    <asp:Literal ID="HitoricoLiteral" runat="server"></asp:Literal><br />
                    <br />
                </div>
            </div>
            <!--===================================================-->
        </div>


    </div>

    <uc1:TicketWebUserControl runat="server" ID="TicketWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->

</asp:Content>

