<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AlterarCarteiraWebForm.aspx.cs" Inherits="VendasWeb.GerencialVendas.AlterarCarteiraWebForm" %>

<%@ Register Src="~/usercontrol/UCGerencialVendas.ascx" TagPrefix="uc1" TagName="UCGerencialVendas" %>

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
                    <h3 class="panel-title">Alteração de Carteira</h3>
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
                                <asp:Label ID="VendedorLabel" runat="server" Text="Vendedor Origem:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="VendedorDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="VendedorDestinoLabel" runat="server" Text="Vendedor Destino:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="VendedorDestinoDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">
                            <asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                runat="server" OnClick="GravarButton_Click" >Gravar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" CausesValidation="false">Retornar</asp:LinkButton>

                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

    <uc1:UCGerencialVendas runat="server" id="UCGerencialVendas" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
