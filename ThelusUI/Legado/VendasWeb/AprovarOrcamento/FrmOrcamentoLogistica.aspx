<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmOrcamentoLogistica.aspx.cs" Inherits="VendasWeb.AprovarOrcamento.FrmOrcamentoLogistica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <!-- LINHA 1-->
    <div class="row">

        <div class="col-sm-12">
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
                    <h3 class="panel-title">Aprovar Fretes Pedido</h3>
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
                    <!-- LINHA 1 - Painel FILTROS-->
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 class="text-bold">Filtros
                            </h5>
                            <hr>
                        </div>
                    </div>
                    <!-- LINHA 1 - Painel FILTROS-->
                    <!--===================================================-->

                    <div class="row">

                        <div class="col-sm-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LblEmpresa" runat="server" Text="Empresa :"></asp:Label></h5>
                                <asp:DropDownList ID="EmpresaDropDown" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-sm-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LblSituacao" runat="server" Text="Situação :"></asp:Label></h5>
                                <asp:DropDownList ID="SituacaoDropDown" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">Todos</asp:ListItem>
                                    <asp:ListItem  Value="Liberado">Liberado</asp:ListItem>
                                    <asp:ListItem Value="Pendente" Selected="True">Pendente</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>



                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelEntidade" runat="server" Text="Entidade :"></asp:Label></h5>
                                <asp:TextBox ID="EntidadeTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>


                    </div>

                    <div class="row">

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label1" runat="server" Text="Pedido :"></asp:Label></h5>
                                <asp:TextBox ID="PedVendaNumTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
                </div>




                <!-- END Painel FILTROS-->
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- Botões de buscar e limpar-->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">

                            <asp:LinkButton ID="BuscarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                                CausesValidation="false" OnClick="BuscarLinkButton_Click"  runat="server">Buscar</asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
        </div>


        <asp:MultiView ID="MultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="View" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Pedidos
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="OrcamentoGridView" EmptyDataText="Nenhum Orçamento Localizado" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="OrcamentoGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />

                                <Columns>


                                    <asp:TemplateField HeaderText="Acessar">
                                        <ItemTemplate>
                                            


                                              <asp:LinkButton ID="btnAcessar" class="btn btn-warning fa fa-external-link fa-lg"
                                CausesValidation="false" OnClick="btnAcessar_Click" runat="server"></asp:LinkButton>


                                        </ItemTemplate>
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Empresa" >
                                        <ItemTemplate>
                                            <asp:Label ID="EmpCodLabel" runat="server" Text='<%# Bind("EmpCod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Pedido">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaNumLabel" runat="server" Text='<%# Bind("PedVendaNum") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="UF">
                                        <ItemTemplate>
                                            <asp:Label ID="UfSiglaLabel" runat="server" Text='<%# Bind("UfSigla") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status Pedido">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaStatDescrLabel" runat="server" Text='<%# Bind("PedVendaStatDescr") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inclusão">
                                        <ItemTemplate>
                                            <asp:Label ID="DataPedidoLabel" runat="server" Text='<%# Bind("DataPedido") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Previsão">
                                        <ItemTemplate>
                                            <asp:Label ID="DataPrevisaoLabel" runat="server" Text='<%# Bind("DataPrevisao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Situação">
                                        <ItemTemplate>
                                            <center>
                                                <asp:Label ID="SitucaoLabel" runat="server" Text='<%# Bind("Situacao") %>'></asp:Label>
                                            </center>
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
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
