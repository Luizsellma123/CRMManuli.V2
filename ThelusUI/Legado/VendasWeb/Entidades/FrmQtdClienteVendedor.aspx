<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmQtdClienteVendedor.aspx.cs" Inherits="VendasWeb.Entidades.FrmQtdClienteVendedor" %>
<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>

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
                    <h3 class="panel-title">
                        Selecionar Vendedor</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->
                <div id="painel_aberto" class="">
                    <%--<div class="panel-body">
                        <!--LINHA 1 - Painel Aberto-->
                        <!--<div class="row">
                           
                        </div>-->
                        <!--END LINHA 1 - Painel Aberto-->
                        <!--===================================================-->
                    </div>--%>
                </div>
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
                            <h5 class="text-bold">
                                Filtros
                            </h5>
                            <hr>
                        </div>
                    </div>
                    <!-- LINHA 1 - Painel FILTROS-->
                    <!--===================================================-->
                    <div class="row">
                        <div class="col-sm-4">
                            
                            <select class="selectpicker show-tick" multiple data-placeholder="Escolha uma Classe..."
                                title="Escolha uma Classe..." data-style="btn-primary" data-live-search="true"
                                id="ClasseDropDownList" runat="server">
                            </select>
                            <asp:RequiredFieldValidator ID="ClasseRequiredFieldValidator" runat="server" Display="Dynamic"
                                SetFocusOnError="True" ControlToValidate="ClasseDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:DropDownList ID="drpFiltro" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Selected="True">NOME</asp:ListItem>
                                    <asp:ListItem Value="2">CÓD. VENDEDOR</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox ID="txtFiltro" runat="server" Width="300px" placeholder="Digite aqui ..." class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
                </div>
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
                            CausesValidation="false" runat="server" onclick="BuscarLinkButton_Click1" >Buscar Vendedores</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--===================================================-->
        <!--End Painel Carteiras e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="VendedoresMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="VendedoresView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Lista de Vendedores
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                       

                            <asp:GridView ID="VendedorGridView" EmptyDataText="Nenhum Vendedor Localizado" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="VendedorGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Código " >
                                        <ItemTemplate>
                                            <asp:Label ID="VendCodLabel" runat="server" Text='<%# Bind("VendCod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome " >
                                        <ItemTemplate>
                                            <asp:Label ID="VendNomeLabel" runat="server" Text='<%# Bind("VendNome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cod. Classe " >
                                        <ItemTemplate>
                                            <asp:Label ID="VendClasseCodLabel" runat="server" Text='<%# Bind("VendClasseCod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Classe " >
                                        <ItemTemplate>
                                            <asp:Label ID="VendClasseDescrLabel" runat="server" Text='<%# Bind("VendClasseDescr") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Quantidade Inativos" >
                                        <ItemTemplate>
                                            
                                            <asp:TextBox ID="QuantidadeInativosVendedorTextBox" AutoPostBack="true" 
                                                runat="server" Text='<%# Bind("QuantidadeInativosVendedor") %>' 
                                                ontextchanged="QuantidadeInativosVendedorTextBox_TextChanged"></asp:TextBox>
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
    
    <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->

     


  <%--Inicia Js Para tratar Looad Footable--%>
    <script type="text/javascript">

        function Picker() {

            //Essa Função é necessaria quando utilizado Picker no footable.
            //Mapear todos os Picker da Tela que estiverem dentro de um Panel

            $("#<%=this.ClasseDropDownList.ClientID%>").selectpicker();

        }




    </script>
    <%--Fim Js Para tratar Looad Footable--%>




</asp:Content>
