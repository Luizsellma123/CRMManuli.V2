<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="PeriodoPedidosWebForm.aspx.cs" Inherits="VendasWeb.Controladoria.PeriodoPedidosWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlControladoria.ascx" TagPrefix="uc1" TagName="WebUserControlControladoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <h3 class="panel-title">Períodos Inclusão Pedidos</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->
                <div id="painel_aberto" class="">
                    <%--<div class="panel-body">
                        <!--LINHA 1 - Painel Aberto-->
                        <div class="row">
                            <div class="col-lg-5">
                                <asp:MultiView ID="VendedorMultView" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="VendedorView" runat="server">
                                        <div class="col-lg-5">
                                            <select class="selectpicker show-tick" multiple data-placeholder="Escolha um vendedor..."
                                                title="Escolha um vendedor..." data-style="btn-primary" data-live-search="true"
                                                id="VendedoresSelect" runat="server">
                                            </select>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <asp:DropDownList ID="drpEntCod" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">NOME FANTASIA</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">RAZÃO SOCIAL</asp:ListItem>
                                        <asp:ListItem Value="3">CÓD.ENTIDADE</asp:ListItem>
                                        <asp:ListItem Value="4">CNPJ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <asp:TextBox ID="txtFiltroEntCod" runat="server" placeholder="Procurar" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!--END LINHA 1 - Painel Aberto-->
                        <!--===================================================-->
                    </div>--%>
                </div>
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>
                <%--<div class="panel-body">
                    <!-- LINHA 1 - Painel FILTROS-->
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 class="text-bold">Filtros</h5>
                            <hr>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="StatusEntidadeLabel" runat="server" Text="Status de Cadastro:" CssClass="text-thin"></asp:Label></h5>
                            <asp:DropDownList ID="StatusEntidadeDropDownList" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <h5>
                                <asp:Label ID="StatusComercialLabel" runat="server" Text="Status Comercial:" CssClass="text-thin"></asp:Label></h5>
                            <asp:DropDownList ID="StatusComercialDropDownList" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <hr />
                </div>--%>
            </div>
            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <%--<div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="btnListar" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Buscar Cliente" data-rel="tooltip"
                            CausesValidation="False"> 
             Buscar Cliente </asp:LinkButton>
                    </div>
                </div>
            </div>--%>
        </div>
        <!--===================================================-->
        <!--End Painel Carteiras e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="PedidosPeriodoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="PedidosPeriodoView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista de Períodos
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="PedidosPeriodoGridView" EmptyDataText="Nenhum Período Localizado"
                                AutoGenerateColumns="False" runat="server"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="EmpCod" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpCodLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome Empresa">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpNomeFantLabel" runat="server" Text='<%# Bind("NomeEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Data Inicial">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="DataInicialLabel" runat="server" Text='<%# Bind("DataInicial") %>'></asp:Label>--%>
                                            <asp:TextBox AutoPostBack="true" ID="DataInicialTextBox" TextMode="Date" OnTextChanged="DataInicialTextBox_TextChanged" runat="server" Text='<%# Eval("DataInicial", "{0:yyyy-MM-dd}")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data Final">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="DataFinalLabel" runat="server" Text='<%# Bind("DataFinal") %>'></asp:Label>--%>
                                            <asp:TextBox AutoPostBack="true" ID="DataFinalTextBox" TextMode="Date" OnTextChanged="DataFinalTextBox_TextChanged" runat="server" Text='<%# Eval("DataFinal", "{0:yyyy-MM-dd}")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <!--===================================================-->
                </div>
                <!-- End Foo Table - Filtering -->
                <!--===================================================-->
                <!-- END TABELA -->
            </asp:View>
        </asp:MultiView>
    </div>
    <!----PAINEL----->
    <uc1:WebUserControlControladoria runat="server" ID="WebUserControlControladoria" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
