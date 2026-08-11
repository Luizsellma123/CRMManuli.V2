<%@ Page Title="" Language="C#" MasterPageFile="~/Crm.Master" AutoEventWireup="true" CodeBehind="FrmAbaDuplicata.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaDuplicata" %>


<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<center><b><h3>Duplicatas</h3></b></center>

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
                    <div class="panel-control">
                        <button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                    <h3 class="panel-title">
                        Dados do Cliente</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel -->
                <!--===================================================-->
                <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-3 col-lg-3">
                            <h5 class="text-bold">
                                <asp:Label ID="Label1" runat="server" Text="Nome:" CssClass="text-thin"></asp:Label></h5>
                            <asp:Label ID="EntNomeLabel" runat="server" Text="" CssClass="text-thin"></asp:Label>
                        </div>
                        <div class="col-xs-5 col-lg-5">
                            <h5 class="text-bold">
                                <asp:Label ID="Label2" runat="server" Text="Endereço:" CssClass="text-thin"></asp:Label></h5>
                            <asp:Label ID="EnderecoLabel" runat="server" Text="" CssClass="text-thin"></asp:Label>
                        </div>
                        <div class="col-xs-3 col-lg-3">
                            <h5 class="text-bold">
                                <asp:Label ID="Label3" runat="server" Text="Status Comercial:" CssClass="text-thin"></asp:Label></h5>
                            <asp:Label ID="StatusComercialLabel" runat="server" Text="" CssClass="text-thin"></asp:Label>
                        </div>
                    </div>
                  

                </div>
            </div>
            <!-- END Painel-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- -->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                    </div>
                </div>
            </div>
        </div>
        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->
        <asp:MultiView ID="DuplicataMultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="DuplicataView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                  
                    <!-- DUPLICATAS -->
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Duplicatas
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="DuplicatasGridView" EmptyDataText="Nada foi encontrado" AutoGenerateColumns="False"
                                runat="server" EnableModelValidation="True" AllowPaging="True" OnPageIndexChanging="DuplicatasGridView_PageIndexChanged"
                                PageSize="10" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:BoundField DataField="EMPCOD" HeaderText="Empresa"></asp:BoundField>
                                    <asp:BoundField DataField="DOCFINTIPOLANC" HeaderText="Tipo"></asp:BoundField>
                                    <asp:BoundField DataField="PARCDOCFINDUPNUM" HeaderText="Documento"></asp:BoundField>
                                    <asp:BoundField DataField="ParcDocFinValor" HeaderText="Valor"></asp:BoundField>
                                    <asp:BoundField DataField="PARCDOCFINDATAEMISSAO" HeaderText="Emissão"></asp:BoundField>
                                    <asp:BoundField DataField="PARCDOCFINDATAVENC" HeaderText="Vencimento"></asp:BoundField>
                                    <asp:BoundField DataField="PARCDOCFINDATAPRORROG" HeaderText="Prorrogação"></asp:BoundField>
                                    <asp:BoundField DataField="PARCDOCFINDATAPAG" HeaderText="Pagamento"></asp:BoundField>
                                    <asp:BoundField DataField="atraso" HeaderText="Atraso"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <!-- FIM FORMAS DE PAGAMENTO -->
                </div>
                <!-- End Foo Table - Filtering -->
                <!--===================================================-->
                <!-- END TABELA -->
            </asp:View>
        </asp:MultiView>
    </div>


    <!----PAINEL----->
   <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
   

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
