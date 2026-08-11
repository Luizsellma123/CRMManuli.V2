<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AnaliseCreditoAnotacoesNegativasWebForm.aspx.cs" Inherits="VendasWeb.Clientes.AnaliseCreditoAnotacoesNegativasWebForm" %>

<%@ Register Src="~/usercontrol/AnaliseCreditoDetalheWebUserControl.ascx" TagPrefix="uc1" TagName="AnaliseCreditoDetalheWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
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
                    <h3 class="panel-title">Cadastro Cliente - Análise Crédito - Anotações Negativas </h3>
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

                        <%-- Informações sobre anotações negativas da  --%>
                        <div>

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

                                    <asp:LinkButton ID="AnotacoesNegativasPefinLinkButton" ClientIDMode="Static"
                                        Width="100%" class="btn btn-primary" CausesValidation="false" runat="server">
                                    </asp:LinkButton>

                                </div>

                                <div class="col-sm-3">

                                    <asp:LinkButton ID="AnotacoesNegativasProtestoLinkButton" ClientIDMode="Static"
                                        Width="100%" class="btn btn-primary" CausesValidation="false" runat="server">
                                    </asp:LinkButton>

                                </div>

                                <div class="col-sm-3">

                                    <asp:LinkButton ID="AnotacoesNegativasChequesLinkButton" ClientIDMode="Static"
                                        Width="100%" class="btn btn-primary" CausesValidation="false" runat="server">
                                    </asp:LinkButton>

                                </div>

                                <div class="col-sm-3">

                                    <asp:LinkButton ID="AnotacoesNegativasParticipacaoFalenciaLinkButton" ClientIDMode="Static"
                                        Width="100%" class="btn btn-primary" CausesValidation="false" runat="server">
                                    </asp:LinkButton>

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

                                    <asp:LinkButton ID="AnotacoesNegativasRefinLinkButton" ClientIDMode="Static"
                                        Width="100%" class="btn btn-primary" CausesValidation="false" runat="server">
                                    </asp:LinkButton>

                                </div>

                                <div class="col-sm-3">

                                    <asp:LinkButton ID="AnotacoesNegativasAcaoJudicialLinkButton" ClientIDMode="Static"
                                        Width="100%" class="btn btn-primary" CausesValidation="false" runat="server">
                                    </asp:LinkButton>

                                </div>

                                <div class="col-sm-3">

                                    <asp:LinkButton ID="AnotacoesNegativasRechequeLinkButton" ClientIDMode="Static"
                                        Width="100%" class="btn btn-primary" CausesValidation="false" runat="server">
                                    </asp:LinkButton>

                                </div>

                                <div class="col-sm-3">

                                    <asp:LinkButton ID="AnotacoesNegativasDividaVencidaLinkButton" ClientIDMode="Static"
                                        Width="100%" class="btn btn-primary" CausesValidation="false" runat="server">
                                    </asp:LinkButton>

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

        <asp:MultiView ID="ConcetreResumoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ConcetreResumoView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Resumo"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="ConcetreResumoGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="PefinMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="PefinView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Dividas em outros segmentos - até 5 ocorrências mais recentes"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="PefinGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="ProtestoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ProtestoView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Protesto - até 5 ocorrências mais recentes"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="ProtestoGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="ChequesMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ChequesView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Cheque - até 5 ocorrências mais recentes"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="ChequesGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="ParticipacaoFalenciaMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ParticipacaoFalenciaView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Cheques Sustado - até 5 ocorrências mais recentes"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="ParticipacaoFalenciaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="RefinMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="RefinView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Dividas em Instituições Financeiras(REFIN) - até 5 ocorrências mais recentes"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="RefinGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="AcaoJudicialMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="AcaoJudicialView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Ações Judiciais - até 5 ocorrências mais recentes"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="AcaoJudicialGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="RechequeMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="RechequeView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Recheque - até 5 ocorrências mais recentes"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="RechequeGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="DividaVencidaMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="DividaVencidaView" runat="server">
                <div class="panel">
                    <div class="panel-body">
                        <asp:Label runat="server" Text="Dividas em outros segmentos - até 5 ocorrências mais recentes"></asp:Label>
                        <div class="col-sm">
                            <br />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="DividaVencidaGridView" AutoGenerateColumns="true" runat="server" AllowPaging="true" Style="border-collapse: collapse; max-width: 100%"></asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

    </div>

    <uc1:AnaliseCreditoDetalheWebUserControl runat="server" ID="AnaliseCreditoDetalheWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>     
    
</asp:Content>
