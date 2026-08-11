<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="PedidoHistoricoWebForm.aspx.cs" Inherits="VendasWeb.cadastros.PedidoHistoricoWebForm" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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

                    <h3 class="panel-title">Pedido Venda - Histórico</h3>
                </div>
                <div class="panel-body">
                    
                    <div id="CabecarioPedido" class="detCabeccario">
                        <div id="btnentidade" style="margin-top: 1px; margin-right: 1px; float: right;">
                            <asp:LinkButton ID="btnAlteraEntidade" runat="server" CssClass="btn btn-success btn-labeled fa fa-arrow-left fa-lg"
                                OnClick="btnAlteraEntidade_Click">Voltar Detalhe</asp:LinkButton>
                        </div>
                        <asp:Label ID="lblEmpresa" runat="server" Text="EMPRESA:" CssClass="texto"></asp:Label>
                        <asp:Label ID="lblDescEmpresa" runat="server" Text="" CssClass="texto"></asp:Label><br />
                        <asp:Label ID="NumeroPedidoLabel" runat="server" Text="PEDIDO:" CssClass="texto"></asp:Label><asp:Literal ID="ltlNumPedido" runat="server"></asp:Literal><br />
                        <!--<asp:Label ID="lblNumPedido" runat="server" Text="Número:" CssClass="texto" ></asp:Label>
                        <asp:Label ID="lblDescNumPedido"  runat="server" Text="" CssClass="texto"></asp:Label><br /> -->
                        <asp:Label ID="lblnome" runat="server" Text="NOME:" CssClass="texto"></asp:Label>
                        <asp:Label ID="lblDescNome" runat="server" Text="" CssClass="texto"></asp:Label><br />
                        <!--<asp:Label ID="lblFantasia" runat="server" Text="FANTASIA:" CssClass="texto"></asp:Label>
                        <asp:Label ID="lblDescFantasia" runat="server" Text="" CssClass="texto"></asp:Label><br />-->
                        <asp:Label ID="lblCnpj" runat="server" Text="CNPJ/CPF:" CssClass="texto"></asp:Label>
                        <asp:Label ID="lblDescCnpj" runat="server" Text="" CssClass="texto"></asp:Label><br />
                        <asp:TextBox ID="txtIDEntidade" runat="server" Visible="false"></asp:TextBox>
                    </div>

                    <!-- Dados Observacao -->
                    <h5>
                        <asp:Label ID="Label1" runat="server" Text="Novo Histórico" Width="100%"></asp:Label></h5>
                    <asp:TextBox ID="txtNovoHistorico" runat="server" CssClass="form-control" TextMode="MultiLine" Width="100%" Height="200px"></asp:TextBox>

                    <br />

                    <!-- Dados Observacao -->
                    <h5>
                        <asp:Label ID="Label2" runat="server" Text="Histórico" Width="100%"></asp:Label></h5>
                    <asp:TextBox ID="txtHistorico" runat="server" CssClass="form-control" TextMode="MultiLine" Width="100%" Height="200px" Enabled="false"></asp:TextBox>



                </div>
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">



                            <asp:LinkButton ID="SalvarButton" class="btn btn-primary btn-labeled fa fa-floppy-o fa-lg"
                                runat="server" title="Salvar" data-rel="tooltip" CausesValidation="true" OnClick="SalvarButton_Click"> 
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
