<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ChamadoProjetoWebForm.aspx.cs" Inherits="VendasWeb.Chamados.ChamadoProjetoWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlChamado.ascx" TagPrefix="uc1" TagName="WebUserControlChamado" %>

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
                    <h3 class="panel-title">Chamado - Projeto</h3>
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

                    <asp:HiddenField ID="IDClienteHiddenField" runat="server" />
                    <asp:HiddenField ID="NomeClienteHiddenField" runat="server" />
                    <asp:HiddenField ID="NumeroCNPJHiddenField" runat="server" />

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Solicitante:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="SolicitanteDropDownList" runat="server" CssClass="form-control fstdropdown-select" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NumeroChamado" runat="server" Text="Número Projeto:"></asp:Label>
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
                                <asp:Label ID="ProjetoLabel" runat="server" Text="Projeto:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ProjetoDropDownList" runat="server" CssClass="form-control" Enabled="false">
                                    <asp:ListItem Text="Sim" Value="sim" />
                                    <asp:ListItem Text="Não" Value="nao" />
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="HorasPrevistasLabel" runat="server" Text="Horas Previstas:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="HorasPrevistasTextBox" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="HorasPrevistasTextBox" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="HorasLabel" runat="server" Text="Horas Realizadas:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="HorasRealizadasTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PrevisaoEntregaLabel" runat="server" Text="Previsão Entrega:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" TextMode="date" ID="PrevisaoEntregaTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PrioridadeProjetoLabel" runat="server" Text="Prioridade:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="PrioridadeProjetoDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="PrioridadeProjetoDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="HorasDedicadasLabel" runat="server" Text="Horas Desenvolvimento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="HorasDesenvolvimentoDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="HorasDesenvolvimentoDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label2" runat="server" Text="Descrição Detalhada :"></asp:Label>
                                </h5>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox ID="DescricaoProjetoTextBox" Width="100%" Height="150px" 
                                    TextMode="MultiLine" runat="server"></asp:TextBox>                                

                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="DescricaoProjetoTextBox" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">
                            <asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                runat="server" OnClick="GravarButton_Click">Gravar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" CausesValidation="false" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                        </div>

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
