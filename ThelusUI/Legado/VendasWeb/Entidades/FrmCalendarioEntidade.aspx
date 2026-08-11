<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmCalendarioEntidade.aspx.cs" Inherits="VendasWeb.Entidades.FrmCalendarioEntidade" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Inicia Js Para Footable--%>
    <%--<script type="text/javascript" src="../template/footable/js/footable.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <%--Fim Js Para Footable--%>
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
                        Selecionar Clientes</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->
                <div id="painel_aberto" class="">
                    <div class="panel-body">
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
                    </div>
                </div>
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>
                <div class="panel-body">
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
                        <asp:LinkButton ID="btnListar" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Buscar Cliente" data-rel="tooltip" OnClick="btnListar_Click"
                            CausesValidation="False"> 
             Buscar Cliente </asp:LinkButton>


              &nbsp;&nbsp;


              <asp:LinkButton ID="CancelarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-left fa-lg"
                            runat="server" title="Cancelar" data-rel="tooltip" 
                            CausesValidation="False" 
            onclick="CancelarLinkButton_Click"> 
             Cancelar </asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>
        <!--===================================================-->
        <!--End Painel Carteiras e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="ClientesMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ClientesView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Clientes
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="ListaEntidadeGridView" EmptyDataText="Nenhum Cliente Localizado"
                                AutoGenerateColumns="False" runat="server" AllowPaging="True" OnPageIndexChanging="ListaEntidadeGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="EntCod" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="EntCodLabel" runat="server" Text='<%# Bind("EntCod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CÓDIGO">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoLabel" runat="server" Text='<%# Bind("EntCod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CNPJ/CPF">
                                        <ItemTemplate>
                                            <asp:Label ID="EntCpfCgcLabel" runat="server" Text='<%# Bind("EntCpfCgc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome">
                                        <HeaderStyle Width="100%" />
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("NOME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cidade">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("CidNome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Situ. Cadastro">
                                        <ItemTemplate>
                                            <asp:Label ID="StatEntDescrLabel" runat="server" Text='<%# Bind("StatEntDescr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Situ. Comercial">
                                        <ItemTemplate>
                                            <asp:Label ID="StatEntComercialLabel" runat="server" Text='<%# Bind("StatEntComercial") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Selecionar">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <center>
                                                        <p>
                                                            <asp:RadioButton ID="SelecionarRadioButton" runat="server" AutoPostBack="True" OnCheckedChanged="SelecionarCheckedChanged" />
                                                        </p>
                                                    </center>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
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
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
