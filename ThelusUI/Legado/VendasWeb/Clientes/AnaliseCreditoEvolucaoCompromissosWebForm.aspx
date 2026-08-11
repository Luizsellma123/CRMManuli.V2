<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AnaliseCreditoEvolucaoCompromissosWebForm.aspx.cs" Inherits="VendasWeb.Clientes.AnaliseCreditoEvolucaoCompromissosWebForm" %>

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
                    <h3 class="panel-title">Cadastro Cliente - Análise Crédito - Evolução Compromissos</h3>
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

                        <%-- Evolução de Compromissos --%>
                        <div id="EvolucaoDeCompromissosDiv" runat="server">

                            <div class="row">

                                <asp:Literal ID="GraficoEvolucaoCompromissoColunasLiteral" runat="server" />

                                <hr style="background-color: black; width: 99.5%; margin-right: 10%;" />

                                <asp:Literal ID="GraficoEvolucaoCompromissoMesAnoLiteral" runat="server" />

                                <br>

                                <asp:Literal ID="GraficoEvolucaoCompromissoDescricaoLiteral" runat="server" />

                            </div>

                            <style>
                                .GraficoColunas {
                                    width: 100%;
                                    height: 200px;
                                    /*border:1px solid black;*/
                                }

                                .GraficoColunasMesAno {
                                    width: 100%;
                                    height: 10px;
                                    /*border:1px solid black;*/
                                }

                                .Coluna {
                                    height: 100%;
                                    width: 90%;
                                }

                                .Porcentagem {
                                    width: 100%;
                                    position: relative;
                                    display: flex;
                                    align-items: center;
                                    justify-content: center;
                                    color: white;
                                }

                                .MesAno {
                                    width: 90%;
                                    height: 1px;
                                    display: flex;
                                    align-items: center;
                                    justify-content: center;
                                    color: black;
                                }

                                .Descricao {
                                    width: 95%;
                                    height: 100%;
                                    display: flex;
                                    align-items: center;
                                    justify-content: center;
                                    color: black;
                                    background-color: silver;
                                    border-radius: 3px;
                                }
                            </style>

                            <%--<script type="text/javascript">
                                // Obtém a largura da tabela
                                var tabela = document.querySelector('.GraficoColunas');
                                var larguraTabela = tabela.clientWidth;

                                // Calcula a largura de cada coluna
                                var larguraColuna = (larguraTabela / 13) - 10;

                                // Define a largura de cada coluna
                                var colunas = document.querySelectorAll('.Coluna');
                                colunas.forEach(function (coluna) {
                                    coluna.style.width = larguraColuna + 'px';
                                });
                            </script>--%>

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

