<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AnaliseCreditoAdministracaoDetalheWebForm.aspx.cs" Inherits="VendasWeb.Clientes.AnaliseCreditoAdministracaoDetalheWebForm" %>

<%@ Register Src="~/usercontrol/AnaliseCreditoDetalheWebUserControl.ascx" TagPrefix="uc1" TagName="AnaliseCreditoDetalheWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="IDClienteHiddenField" runat="server" />

    <asp:HiddenField ID="IDAnaliseHiddenField" runat="server" />

    <asp:HiddenField ID="CPFCNPJHiddenField" runat="server" />

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
                    <h3 class="panel-title">Cadastro Cliente - Análise Crédito - Administração - Detalhe</h3>
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

                        <%-- Administrador --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Administrador:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="AdministradorTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- CPF, Identidade --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="CPF:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="CPFTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Identidade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="IdentidadeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Estado Civil, Naturalidade, Nacionalidade --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Estado Civil:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="EstadoCivilTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Naturalidade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="NaturalidadeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Nacionalidade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="NacionalidadeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Vínculo, Entrada, Mandato --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Vínculo:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="VinculoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Entrada:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="EntradaTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Mandato:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:TextBox ID="MandatoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Telefone, Cargo --%>
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
                                    <asp:Label runat="server" Text="Cargo:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="CargoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Endereço --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Endereço:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="EnderecoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
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
                                <asp:LinkButton ID="PefinLinkButton" ClientIDMode="Static"
                                    Width="100%" CausesValidation="false" runat="server">
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="ProtestoLinkButton" ClientIDMode="Static"
                                    Width="100%" CausesValidation="false" runat="server">
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="ChequeSustadoCanceladoLinkButton" ClientIDMode="Static"
                                    Width="100%" CausesValidation="false" runat="server">
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="ParticipacaoFalenciaLinkButton" ClientIDMode="Static"
                                    Width="100%" CausesValidation="false" runat="server">
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
                                <asp:LinkButton ID="RefinLinkButton" ClientIDMode="Static"
                                    Width="100%" CausesValidation="false" runat="server">
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="AcaoJudicialLinkButton" ClientIDMode="Static"
                                    Width="100%" CausesValidation="false" runat="server">
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="ChequeSemFundoLinkButton" ClientIDMode="Static"
                                    Width="100%" CausesValidation="false" runat="server">
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="DividaVencidaLinkButton" ClientIDMode="Static"
                                    Width="100%" CausesValidation="false" runat="server">
                                </asp:LinkButton>
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

    <uc1:AnaliseCreditoDetalheWebUserControl runat="server" ID="AnaliseCreditoDetalheWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>     
    
</asp:Content>

