<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ContaCorrenteDetalheWebForm.aspx.cs" Inherits="VendasWeb.financeiro.ContaCorrenteDetalheWebForm" %>

<%@ Register Src="~/usercontrol/ContaCorrenteWebUserControl.ascx" TagPrefix="uc1" TagName="ContaCorrenteWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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
                    <h3 class="panel-title">Financeiro - Conta Corrente Clientes</h3>
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

                    <%--CABEÇALHO--%>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoLabel" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="CodigoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NomeLabel" runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="NomeTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CNPJLabel" runat="server" Text="CNPJ:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="CNPJTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="VendedorLabel" runat="server" Text="Vendedor:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="VendedorTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <br />

                    <%--END CABEÇALHO--%>

                    <br />

                    <%--DADOS GERAIS--%>

                    <div class="row">
                        <div class="col-sm">
                            <div class="form-group">
                                <b>
                                    <asp:Label ID="DadosGeraisLabel" runat="server" Text="Dados Gerais:"></asp:Label>
                                </b>
                            </div>
                        </div>

                        <div class="col-sm">
                            <div class="form-group">
                                <hr />
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LimiteCreditoLabel" runat="server" Text="Limite Crédito:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="LimiteCreditoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="limiteDisponivelLabel" runat="server" Text="Limite Disponível:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="limiteDisponivelTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CadastroLabel" runat="server" Text="Cadastro:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="CadastroTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="PedidosAbertosLabel" runat="server" Text="Pedidos Abertos:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="PedidosAbertosTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 3--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="UltimaCompraLabel" runat="server" Text="Última Compra:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="UltimaCompraTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="PedidosFaturadosLabel" runat="server" Text="Pedidos Faturados:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="PedidosFaturadosTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%-- END DADOS GERAIS--%>

                    <br />

                    <%--CONTAS RECEBER--%>

                    <div class="row">
                        <div class="col-sm">
                            <div class="form-group">
                                <b>
                                    <asp:Label ID="ContasReceberLabel" runat="server" Text="Contas Receber:"></asp:Label>
                                </b>
                            </div>
                        </div>

                        <div class="col-sm">
                            <div class="form-group">
                                <hr />
                            </div>
                        </div>
                    </div>


                    <%--  LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="AReceberLabel" runat="server" Text="A Receber:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="AReceberTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="RecebidoLabel" runat="server" Text="Recebido:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="RecebidoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="MediaAtrasoLabel" runat="server" Text="Média Atraso:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="MediaAtrasoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="MediaFaturamentoLabel" runat="server" Text="Média Faturamento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="MediaFaturamentoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%-- END CONTAS RECEBER--%>

                    <br />

                    <%--CONTAS RECEBER CURITIBA--%>

                    <div class="row">
                        <div class="col-sm">
                            <div class="form-group">
                                <b>
                                    <asp:Label ID="ContasReceberCuritibaLabel" runat="server" Text="Contas Receber Curitiba:"></asp:Label>
                                </b>
                            </div>
                        </div>
                        <div class="col-sm">
                            <div class="form-group">
                                <hr />
                            </div>
                        </div>
                    </div>


                    <%--  LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="AReceberCuritibaLabel" runat="server" Text="A Receber:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="AReceberCuritibaTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="RecebidoCuritibaLabel" runat="server" Text="Recebido:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="RecebidoCuritibaTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="MediaAtrasoCuritibaLabel" runat="server" Text="Média Atraso:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="MediaAtrasoCuritibaTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="MediaFaturamentoCuritibaLabel" runat="server" Text="Média Faturamento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="MediaFaturamentoCuritibaTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%-- END CONTAS RECEBER CURITIBA--%>

                    <br />

                    <%--CONTAS RECEBER MANAUS--%>

                    <div class="row">
                        <div class="col-sm">
                            <div class="form-group">
                                <b>
                                    <asp:Label ID="ContasReceberManausLabel" runat="server" Text="Contas Receber Manaus:"></asp:Label>
                                </b>
                            </div>
                        </div>
                        <div class="col-sm">
                            <div class="form-group">
                                <hr />
                            </div>
                        </div>
                    </div>


                    <%--  LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="AReceberManausLabel" runat="server" Text="A Receber:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="AReceberManausTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="RecebidoManausLabel" runat="server" Text="Recebido:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="RecebidoManausTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="MediaAtrasoManausLabel" runat="server" Text="Média Atraso:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="MediaAtrasoManausTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="MediaFaturamentoManausLabel" runat="server" Text="Média Faturamento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="MediaFaturamentoManausTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%-- END CONTAS RECEBER MANAUS--%>

                    <br />

                    <%--CONTAS PAGAR--%>

                    <div class="row">
                        <div class="col-sm">
                            <div class="form-group">
                                <b>
                                    <asp:Label ID="ContasPagarLabel" runat="server" Text="Contas Pagar:"></asp:Label></b>
                            </div>
                        </div>
                        <div class="col-sm">
                            <div class="form-group">
                                <hr />
                            </div>
                        </div>
                    </div>


                    <%--  LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="APagarLabel" runat="server" Text="A Pagar:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="APagarTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="APagarPagoLabel" runat="server" Text="Pago:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="APagarPagoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%--  LINHA 2--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="APMediaAtrasoLabel" runat="server" Text="Média Atraso:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="APMediaAtrasoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="APMediaFaturamentoLabel" runat="server" Text="Média Faturamento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="APMediaFaturamentoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%-- END CONTAS PAGAR--%>

                    <br />

                    <%--DEVOLUÇÕES--%>

                    <div class="row">
                        <div class="col-sm">
                            <div class="form-group">
                                <b>
                                    <asp:Label ID="DevolucoesLabel" runat="server" Text="Devoluções:"></asp:Label>
                                </b>
                            </div>
                        </div>
                        <div class="col-sm">
                            <div class="form-group">
                                <hr />
                            </div>
                        </div>
                    </div>


                    <%--  LINHA 1--%>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DevAPagarLabel" runat="server" Text="A Pagar:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="DevAPagarTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="DevPagoLabel" runat="server" Text="Pago:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="DevPagoTextoLabel" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <%-- END DEVOLUÇÕES--%>
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

                        <asp:LinkButton ID="voltarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="voltarButton_Click">Retornar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

    </div>

    <uc1:ContaCorrenteWebUserControl runat="server" ID="ContaCorrenteWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
