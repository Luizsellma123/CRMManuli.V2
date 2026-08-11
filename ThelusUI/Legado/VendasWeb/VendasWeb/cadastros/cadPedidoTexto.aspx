<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="cadPedidoTexto.aspx.cs" Inherits="VendasWeb.cadastros.cadPedidoTexto" %>

<%@ Register Src="../usercontrol/cabecarioPedido.ascx" TagName="cabecarioPedido" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-9">
            <!--===================================================-->
            <!--Painel Carteiras e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">

                    <h3 class="panel-title">Cadastro Pedido - Dados Complementares</h3>
                </div>
                <div class="panel-body">
                    <uc1:cabecarioPedido ID="cabecarioPedido1" runat="server" />



                    <!-- Dados Observacao -->

                    <h5><asp:Label ID="ObservacaoConcorrenteLabel" runat="server" Text="Observação Nota Fiscal:" Width="100%"></asp:Label></h5>
                    <asp:TextBox ID="txtTextoLivre" runat="server" CssClass="form-control" TextMode="MultiLine" Width="100%" Height="200px"></asp:TextBox>

                    <br />

                    <!-- Dados Observacao -->
                    <h5><asp:Label ID="Label1" runat="server" Text="Novo Histórico" Width="100%"></asp:Label></h5>
                    <asp:TextBox ID="txtNovoHistorico" runat="server" CssClass="form-control" TextMode="MultiLine" Width="100%" Height="200px"></asp:TextBox>

                    <br />

                    <!-- Dados Observacao -->
                    <h5><asp:Label ID="Label2" runat="server" Text="Histórico" Width="100%"></asp:Label></h5>
                    <asp:TextBox ID="txtHistorico" runat="server" CssClass="form-control" TextMode="MultiLine" Width="100%" Height="200px"></asp:TextBox>

                    
                    
                    </div>
                    <!--===================================================-->
                    <!-- Panel Footer-->
                    <!-- -->
                    <!--===================================================-->
                    <div class="panel-footer">
                        <div class="row">
                            <div class="panel-control">



                                <asp:LinkButton ID="btnSalvar" class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg"
                                    runat="server" title="Salvar" data-rel="tooltip" CausesValidation="true" OnClick="btnSalvar_Click"> 
                                Salvar Dados
                                </asp:LinkButton>

                            </div>
                        </div>
                    </div>
        </div>
    </div>

            <!--===================================================-->
            <!--End Painel-->
            <!--===================================================-->
            <!----PAINEL----->
            <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
</asp:Content>
