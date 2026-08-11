<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmPerfilComercial.aspx.cs" Inherits="VendasWeb.Entidades.FrmPerfilComercial" %>

<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<center><b><h3>Perfil Comercial</h3></b></center>

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
                    <br />
                    <div class="row">
                        <div class="col-xs-3 col-lg-3">
                            <h5 class="text-bold">
                                <asp:Label ID="Label5" runat="server" Text="Vendedor:" CssClass="text-thin"></asp:Label></h5>
                            <asp:Label ID="VendNomeLabel" runat="server" Text="" CssClass="text-thin"></asp:Label>
                        </div>
                        <div class="col-xs-3 col-lg-3">
                            <h5 class="text-bold">
                                <asp:Label ID="Label4" runat="server" Text="Data de Cadastro:" CssClass="text-thin"></asp:Label></h5>
                            <asp:Label ID="EntDataCadLabel" runat="server" Text="" CssClass="text-thin"></asp:Label>
                        </div>
                        <div class="col-xs-3 col-lg-3">
                            <h5 class="text-bold">
                                <asp:Label ID="Label6" runat="server" Text="Data última Compra:" CssClass="text-thin"></asp:Label></h5>
                            <asp:Label ID="NFDataEmisLabel" runat="server" Text="" CssClass="text-thin"></asp:Label>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                      <div class="col-xs-3 col-lg-3">
                        <h5 class="text-bold">
                            <asp:Label ID="Label7" runat="server" Text="Limite de crédito total:" CssClass="text-thin"></asp:Label></h5>
                        <asp:Label ID="EntValLimCredLabel" runat="server" Text="" CssClass="text-thin"></asp:Label>
                    </div>
                    <div class="col-xs-3 col-lg-3">
                        <h5 class="text-bold">
                            <asp:Label ID="Label8" runat="server" Text="Limite de crédito disponivel:" CssClass="text-thin"></asp:Label></h5>
                        <asp:Label ID="SaldoLimiteClienteLabel" runat="server" Text="" CssClass="text-thin"></asp:Label>
                    </div>
                    <br /><br /><br />


                    <!-- Total vendido por família no semestre -->
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Totais por família Semestral
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="TotalFamiliaSemestreGridView" EmptyDataText="Nenhuma Família Localizada"
                                AutoGenerateColumns="False" runat="server" EnableModelValidation="True" AllowPaging="True"
                                OnPageIndexChanging="TotalFamiliaSemestreGridView_PageIndexChanged" PageSize="10" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:BoundField DataField="LinhaProduto" HeaderText="Linha Produto"></asp:BoundField>
                                    <asp:BoundField DataField="ItPedVendaValTot" HeaderText="Valor Total"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <!-- Total vendido por família no semestre -->
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Totais por família da Entidade
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="TotalFamiliaEternidadeGridView" EmptyDataText="Nenhuma Família Localizada"
                                AutoGenerateColumns="False" runat="server" EnableModelValidation="True" AllowPaging="True"
                                OnPageIndexChanging="TotalFamiliaEternidadeGridView_PageIndexChanged" PageSize="10" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:BoundField DataField="LinhaProduto" HeaderText="Linha Produto"></asp:BoundField>
                                    <asp:BoundField DataField="ItPedVendaValTot" HeaderText="Valor Total"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
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
        <asp:MultiView ID="PerfilComercialMultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="PerfilComercialView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                  
                    <!-- PRODUTOS -->
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Produtos
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="ListaProdutosGridView" EmptyDataText="Nenhum Produto Localizado"
                                AutoGenerateColumns="False" runat="server" EnableModelValidation="True" AllowPaging="True"
                                OnPageIndexChanging="ListaProdutosGridView_PageIndexChanged" PageSize="10" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:BoundField DataField="ProdCodEstr" HeaderText="Cod. Produto"></asp:BoundField>
                                    <asp:BoundField DataField="ProdNome" HeaderText="Nome Prod."></asp:BoundField>
                                    <asp:BoundField DataField="FrequenciaCompra" HeaderText="Frequência de Compra"></asp:BoundField>
                                    <asp:BoundField DataField="VolumeMedio" HeaderText="Volume Médio"></asp:BoundField>
                                    <asp:BoundField DataField="PrecoUltimaCompra" HeaderText="Valor Ultima Compra"></asp:BoundField>
                                    <asp:BoundField DataField="ValorTabPV" HeaderText="Valor de Tabela Ultima Compra"></asp:BoundField>
                                    <asp:BoundField DataField="DataCompra" HeaderText="Data Ultima Compra"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <!-- FIM PRODUTOS -->
                    <!-- FORMAS DE PAGAMENTO -->
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Formas de Pagamento
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="CondPagEntCondGridView" EmptyDataText="Nenhuma Condição Localizada"
                                AutoGenerateColumns="False" runat="server" EnableModelValidation="True" AllowPaging="True"
                                OnPageIndexChanging="CondPagEntCondGridView_PageIndexChanged" PageSize="10" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:BoundField DataField="CondPagCod" HeaderText="Código"></asp:BoundField>
                                    <asp:BoundField DataField="CondPagPedVendaNome" HeaderText="Nome"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <!-- FIM FORMAS DE PAGAMENTO -->
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
