<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.master" AutoEventWireup="true"
    CodeBehind="FrmCarteira.aspx.cs" Inherits="VendasWeb.cadastros.FrmCarteira" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <%--<script language="javascript" src="../js/FrmCarteiraJS.js" type="text/javascript"></script>--%>
    <script language="javascript" src="../js/DetalheClienteJavaScript.js" type="text/javascript"></script>
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
                    <h3 class="panel-title">Selecionar Clientes</h3>
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
                                        <asp:ListItem Value="3">CÓD.CLIENTE</asp:ListItem>
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
                    <!-- LINHA 1 - Painel FILTROS-->
                    <!--
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
                    -->
                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
                    <!-- LINHA 2 - Painel FILTROS-->
                    <!--
                    <asp:UpdatePanel ID="EstadoUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <hr>
                                <div class="col-sm-3">
                                    <h5>
                                        <asp:Label ID="UfLabel" runat="server" Text="Estado:" CssClass="text-thin"></asp:Label></h5>
                                    <asp:DropDownList ID="UfDropDownList" runat="server" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="UfDropDownList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <h5>
                                        <asp:Label ID="LabelCidade" runat="server" Text="Cidade:" CssClass="text-thin"></asp:Label></h5>
                                    <select class="selectpicker show-tick" multiple data-placeholder="Escolha uma Cidade..."
                                        title="Escolha uma Cidade..." data-style="btn-primary" data-live-search="true"
                                        id="CidadeSelect" runat="server">
                                    </select>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <hr>-->

                    <!--===================================================-->
                    <!-- END LINHA 2 - Painel FILTROS-->
                    <!-- LINHA 3 - Painel FILTROS-->
                    <!--
                    <asp:UpdatePanel ID="LinhaProdutoUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-3">
                                    <h5>
                                        <asp:Label ID="LinhaProdutoLabel" runat="server" Text="Linha do Produto:" CssClass="text-thin"></asp:Label></h5>
                                    <asp:DropDownList ID="LinhaProdutoDropDownList" runat="server" CssClass="form-control"
                                        Width="100px" AutoPostBack="true" OnSelectedIndexChanged="LinhaProdutoDropDownList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <h5>
                                        <asp:Label ID="ProdutoLabel" runat="server" Text="Produto:" CssClass="text-thin"></asp:Label></h5>
                                    <select class="selectpicker show-tick" multiple data-placeholder="Escolha um Produto..."
                                        width="100px" title="Escolha um Produto..." data-style="btn-primary" data-live-search="true"
                                        id="ProdutoSelect" runat="server">
                                    </select>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <hr>-->
                    <!--===================================================-->
                    <!-- END LINHA 3 - Painel FILTROS-->
                    <!--
                    <asp:UpdatePanel ID="ClasseUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-4">
                                    <h5>
                                        <asp:Label ID="VedClasseLabel" runat="server" Text="Classes:" CssClass="text-thin"></asp:Label></h5>

                                    <select class="selectpicker show-tick" multiple data-placeholder="Escolha uma Casse..."
                                        width="100px" title="Escolha uma Classe..." data-style="btn-primary" data-live-search="true"
                                        id="VendClasseDropDownList" runat="server">
                                    </select>

                                </div>

                                <div class="col-sm-4">
                                    <h5>
                                        <asp:Label ID="Label2" runat="server" Text="Categoria(CNAE):" CssClass="text-thin"></asp:Label></h5>

                                    <select class="selectpicker show-tick" multiple title="Selecione" data-style="btn-primary" 
                                        data-live-search="true" id="CategoriaDropDownList" runat="server">
                                    </select>

                                </div>

                                 <div class="col-sm-4">
                                    <h5>
                                        <asp:Label ID="Label3" runat="server" Text="Categoria Secundaria(CNAE):" CssClass="text-thin"></asp:Label></h5>

                                     <select class="selectpicker show-tick" multiple title="Selecione" data-style="btn-primary" 
                                          data-live-search="true" id="CategoriaSecundariaDropDownList" runat="server">
                                   </select>

                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr>-->
                    <!--
                     <asp:UpdatePanel ID="StatEntCompraUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <h5><asp:Label ID="Label4" runat="server" Text="Status de Compra:" CssClass="text-thin"></asp:Label></h5>                                    

                                 <select class="selectpicker show-tick" multiple title="Selecione" data-style="btn-primary" Width="450px"
                                          data-live-search="true" id="StatEntCompraDropDownList" runat="server">
                                   </select>                   
                           </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <hr />-->


                    <%--
                    <!-- LINHA 4 - Painel FILTROS -->
                    <div class="row">
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="FaturamentoMedioFitaLabel" runat="server" Text="Faturamento Média Fita :"
                                    CssClass="text-thin"></asp:Label></h5>
                            <asp:DropDownList ID="FaturamentoMedioFitaDropDownList" runat="server" CssClass="form-control"
                                Width="180px">
                                <asp:ListItem Value="">Todos</asp:ListItem>
                                <asp:ListItem Value="0">1 a 2000</asp:ListItem>
                                <asp:ListItem Value="1">2001 a 5000</asp:ListItem>
                                <asp:ListItem Value="2">5001 a 10000</asp:ListItem>
                                <asp:ListItem Value="3">10001 a 20000</asp:ListItem>
                                <asp:ListItem Value="4">20001 a 40000</asp:ListItem>
                                <asp:ListItem Value="5">40001 a 60000</asp:ListItem>
                                <asp:ListItem Value="6">60001 a 100000</asp:ListItem>
                                <asp:ListItem Value="7">100001 a 200000</asp:ListItem>
                                <asp:ListItem Value="8">200001 a 500000</asp:ListItem>
                                <asp:ListItem Value="9">500001 a 1000000</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="FaturamentoMedioStretchLabel" runat="server" Text="Faturamento Média Stretch :"
                                    CssClass="text-thin"></asp:Label></h5>
                            <asp:DropDownList ID="FaturamentoMedioStretchDropDownList" runat="server" CssClass="form-control"
                                Width="180px">
                                <asp:ListItem Value="">Todos</asp:ListItem>
                                <asp:ListItem Value="0">1 a 2000</asp:ListItem>
                                <asp:ListItem Value="1">2001 a 5000</asp:ListItem>
                                <asp:ListItem Value="2">5001 a 10000</asp:ListItem>
                                <asp:ListItem Value="3">10001 a 20000</asp:ListItem>
                                <asp:ListItem Value="4">20001 a 40000</asp:ListItem>
                                <asp:ListItem Value="5">40001 a 60000</asp:ListItem>
                                <asp:ListItem Value="6">60001 a 100000</asp:ListItem>
                                <asp:ListItem Value="7">100001 a 200000</asp:ListItem>
                                <asp:ListItem Value="8">200001 a 500000</asp:ListItem>
                                <asp:ListItem Value="9">500001 a 1000000</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <hr>
                    
                    <!--END LINHA 4 - Painel FILTROS-->--%>
                    <%--<!-- LINHA 5 - Painel FILTROS-->
                   
                    <div class="row">
                        <!--  Faixa de datas -->
                        <div class="col-xs-6 col-md-4">
                            <!--Bootstrap Datepicker : Range-->
                            <!--===================================================-->
                            <p>
                                Período de compra de</p>
                            <div id="Div1">
                                <div class="input-daterange input-group" id="Div2">
                                    <asp:TextBox ID="PeriodoCompraInicialTextBox" TextMode="Date" class="form-control"
                                        runat="server"></asp:TextBox>
                                    <span class="input-group-addon">até</span>
                                    <asp:TextBox ID="PeriodoCompraFinalTextBox" TextMode="Date" class="form-control"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <!--===================================================-->
                        </div>
                    </div>

                    <hr>
                    <!--===================================================-->
                    <!-- END LINHA 5 - Painel FILTROS-->--%>
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
                        <h3 class="panel-title">Clientes
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

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sel.">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="SelecionarUpdatePanel" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <center>
                                                        <p>
                                                            <asp:RadioButton ID="SelecionarRadioButton" runat="server" AutoPostBack="True" OnCheckedChanged="SelecionarCheckedChanged" />
                                                        </p>
                                                    </center>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="EntCod" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDClienteLabel" runat="server" Text='<%# Bind("IDCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CÓDIGO">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoLabel" runat="server" Text='<%# Bind("CodigoClienteSAP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CNPJ/CPF">
                                        <ItemTemplate>
                                            <asp:Label ID="EntCpfCgcLabel" runat="server" Text='<%# Bind("CNPJ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome">
                                        <HeaderStyle Width="100%" />
                                        <ItemTemplate>
                                            <asp:Label ID="EntNomeLabel" runat="server" Text='<%# Bind("NomeCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cidade">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("Cidade") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Último Contato" SortExpression="DataUltimoContato">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("UltimoContato") %>'></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Situ. Cadastro">
                                        <ItemTemplate>
                                            <asp:Label ID="StatEntDescrLabel" runat="server" Text='<%# Bind("SituacaoComercial") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Situ. Comercial">
                                        <ItemTemplate>
                                            <asp:Label ID="StatEntComercialLabel" runat="server" Text='<%# Bind("SituacaoComercial") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderStyle-Width="100%" HeaderText="Detalhe">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="VerdetalheLinkButton" runat="server" class="btn btn-primary fa fa-plus-square" 
                                                        data-id='<%# Eval("IDCliente")%>'></asp:LinkButton>
                                                    <!--<asp:Button ID="btnVerDetalhe" title="Ver detalhes do Cadastro do Cliente" runat="server" Text="Ver Detalhe"
                                                        CssClass="btn btn-danger" data-id='<%# Eval("IDCliente") %>' />-->

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="VerdetalheLinkButton" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100%" />
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Detalhes">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-primary fa fa-plus-square" CausesValidation="false" runat="server"
                                                        OnClientClick='<%# string.Format("ConsultaClienteDetalhe("+Eval("IDCliente")+")")%>'></asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DetalhesLinkButton" />
                                                </Triggers>
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

    <%-- MODAL--%>

    <div id="fullReservaModal" class="modal fade bd-example-modal-xl">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="margin-top: 15px;">
                    <h4 id="modalTitle" class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal"><span>×</span> <span class="sr-only">Fechar</span></button>
                </div>

                <div id="modalBody" class="modal-body">
                    <div class="loader" id="LoadingDados"></div>

                    <div class="table-responsive" id="DadosModal">
                        <div class="col-md-12 pad-top bg-gray" style="padding-right: 15px;">
                            <div class="row pad-lft pad-rgt">

                                <%--LINHA 1--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Cliente</th>
                                            <th style="width: 50%;">CNPJ</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="ClienteModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="CNPJModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 2--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Telefone</th>
                                            <th style="width: 50%;">Cidade</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="TelefoneModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="CidadeModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 3--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Vendedor</th>
                                            <th style="width: 50%;">Classe</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="VendedorModalLabel"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="ClasseModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--LINHA 4--%>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th style="width: 50%;">Último Histórico</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:Label runat="server" ID="UltimoHistoricoModalLabel"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>

                </div>

                <div class="modal-footer">

                    <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>

                </div>
            </div>
        </div>
    </div>

    <%--END MODAL--%>

    <!----PAINEL----->
    <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
            <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->

    <asp:HiddenField ID="OperacaoHiddenField" runat="server" />
    <asp:HiddenField ID="HistoricoHiddenField" runat="server" />
    <asp:HiddenField ID="EventoHiddenField" runat="server" />
    <asp:HiddenField ID="CategoriaHiddenField" runat="server" />
    <asp:HiddenField ID="DataHiddenField" runat="server" />
    <asp:HiddenField ID="HoraHiddenField" runat="server" />
    <asp:HiddenField ID="CodigoHiddenField" runat="server" />

    <%--Inicia Js Para tratar Looad Footable--%>
    <script type="text/javascript">


        /*
        $(function () {
        $('[id*=ListaEntidadeGridView]').footable({
        breakpoints: {
        phone: 480,
        //tablet: 1024
        tablet: 2024
        }

        });






        });

        */



        function Picker() {

            //Essa Função é necessaria quando utilizado Picker no footable.
            //Mapear todos os Picker da Tela que estiverem dentro de um Panel

            $("#<%=this.VendedoresSelect.ClientID%>").selectpicker();
            $("#<%=this.CidadeSelect.ClientID%>").selectpicker();
            $("#<%=this.ProdutoSelect.ClientID%>").selectpicker();


            /*
            $('[id*=ListaEntidadeGridView]').footable({
            breakpoints: {
            phone: 480,
            //tablet: 1024
            tablet: 2024
            }

            });
            */
        }




    </script>
    <%--Fim Js Para tratar Looad Footable--%>


    <!--Inicia Scrip para Tratar o combo no Modal-->
    <script type="text/javascript">


        function ShowCal(Codigo, Cnpj, Nome, Cidade, UltimoContato, SituacaoComercial,
                         VendCod, VendNome, VendClasseDescr, Telefone1, Telefone2,
                         ContatoNome, ContatoTelefone, ContatoEmail,
                         DataUltimoContato, UsuarioUltimoHistorico, UltimoHistorico, AcessoEntidade

        ) {



            var Contato = ''
            if (AcessoEntidade == "ADM" || AcessoEntidade == "ENTIDADE_VENDEDOR" || AcessoEntidade == "LIVRE") {
                Contato = '<table class="table table-condensed table-responsive"><thead><tr class="bg-gray-dark">'
                + '<th>Nome do contato</th>'
                + '<th>Telefone</th>'
                + '<th>E-mail</th>'
                + '</tr></thead>'
                + '<tbody><tr>'
                + '<td>' + ContatoNome + '</td>'
                + '<td>' + ContatoTelefone + '</td>'
                + '<td>' + ContatoEmail + '</td>'
                + '</tr></tbody></table>'
            }


            bootbox.dialog({
                title: "Registrar Atendimento",
                size: "large",
                message: '<div class="row"><div class="col-md-12 pad-top bg-gray"><div class="row pad-lft pad-rgt" >'
                       + '<table class="table table-condensed table-responsive"><thead><tr class="bg-gray-light">'
                       + '<th>Código</th>'
                       + '<th>CNPJ/CPF</th>'
                       + '<th>Nome</th>'
                       + '<th>Cidade</th>'
                       + '<th>Último Contato</th>'
                       + '<th>Situação Comercial</th>'
                       + '</tr></thead><tbody>'
                       + '<tr class="bg-gray-light">'
                       + '<td>  <label for="Codigo" id="Codigo" >' + Codigo + '</label></td>'
                       + '<td>' + Cnpj + '</td>'
                       + '<td>' + Nome + '</td>'
                       + '<td>' + Cidade + '</td>'
                       + '<td>' + UltimoContato + '</td>'
                       + '<td>' + SituacaoComercial + '</td>'
                       + '</tr></tbody></table>'

                       + '<table class="table table-condensed table-responsive"><thead><tr class="bg-gray-light">'
                       + '<th>Código do Vendedor</th>'
                       + '<th>Nome Vendedor</th>'
                       + '<th>Classe</th>'
                       + '<th>Telefone 1</th>'
                       + '<th>Telefone 2</th></tr></thead>'
                       + '<tbody><tr class="bg-gray-light">'
                       + '<td>' + VendCod + '</td>'
                       + '<td>' + VendNome + '</td>'
                       + '<td>' + VendClasseDescr + '</td>'
                       + '<td>' + Telefone1 + '</td>'
                       + '<td>' + Telefone2 + '</td>'
                       + '</tr></tbody></table>'

                       + Contato

                       + '</div></div></div>'
                       + '<div class="row">'
                       + '<div class="col-md-12 bg-gray">'
                       + '<div class="row pad-lft pad-rgt" >'
                       + '<div class="timeline mar-btm pad-no" style="padding-bottom: 0px;">'
                       + '<div class="timeline-entry mar-no"> '
                       + '<div class="timeline-stat">'
                       + '<div class="timeline-icon bg-purple">'
                       + '<i class="fa fa-warning fa-lg"></i> '
                       + '</div>'
                       + '<div class="timeline-time"><b>' + DataUltimoContato + '</b></div></div>'
                       + '<div class="timeline-label"> <p class="mar-no pad-btm">'
                       + '<span class="badge badge-purple">Observações Antigas</span>'
                       + 'por <a href="#" class="btn-link btn-md text-semibold"> ' + UsuarioUltimoHistorico + '</a></p>'
                       + '<div class="well well-xs mar-no"> '
                       + '' + UltimoHistorico + ''
                       + '</div></div></div></div></div></div></div><div class="row">'
                       + '<div class="col-xs-12 pad-btm bg-gray">'
                       + '<div class="col-sm-12 col-md-6 col-lg-4">'
                       + '<div class="form-group mar-no">'
                       + '<textarea id="demo-textarea-input" name="demo-textarea-input" rows="6" class="form-control" placeholder="Escreva aqui a Descrição do Evento..."></textarea>'
                       + '</div></div><div class="col-sm-12 col-md-6 col-lg-8">'
                       + '<div class="col-lg-6"><div class="pad-btm">'
                       + '<select name="combo" id="combo" onchange="selecionarEvento(this);" class="selectpicker show-tick" data-placeholder="Escolha um evento..." title="Escolha um evento..." data-style="btn-default" data-live-search="true"> '
                           + '<option value="0">Selecione</option>'
                           + '<option value="1">Atendimento</option>'
                           + '<option value="2">Visita Teste</option>'
                           + '<option value="3">Negociação</option>'
                           + '<option value="4">Venda Fechada</option>'
                           + '<option value="5">Venda Perdida</option>'
                           + '<option value="6">Outros</option>'
                           + '<option value="7">Pedido</option>'
                           + '<option value="8">Nota</option>'
                           + '<option value="9">Observações</option>'
                           + '<option value="10">Mudança</option>'
                        + '</select>'


                       + '</div><div class="pad-btm">'

                       + '<select id="cboCategoria" name="cboCategoria" onchange="selecionarCategoria(this);" class="selectpicker pad-btm show-tick" data-placeholder="Escolha uma categoria..." title="Escolha uma categoria..." data-style="btn-default" data-live-search="true"></select>'


                       + '</div></div><div class="col-lg-6"><div class="col-md-12">'
                       + '<input name="Data" id="Data"  type="date" style="width:130px;">'
                       + '<select name="Hora" id="Hora" class="campo" style="width:60px;"><option value="00">00</option><option value="01">01</option><option value="02">02</option><option value="03">03</option><option value="04">04</option><option value="05">05</option><option value="06">06</option><option value="07">07</option><option value="08">08</option><option value="09">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option><option value="22">22</option><option value="23">23</option></select>'
                       + '</div></div></div></div>',

                buttons: {
                    danger: {
                        label: "Cancelar",
                        className: "btn btn-danger btn-labeled fa fa-times",
                        callback: function () {
                            $.niftyNoty({
                                type: 'danger',
                                icon: 'fa fa-times',
                                message: '<strong>Registro cancelado</strong>',
                                container: 'floating',
                                timer: 3000
                            });
                        }
                    },


                    success: {
                        label: "Inserir Atendimento no Histórico Original",
                        className: "btn-success btn-labeled fa fa-check",
                        callback: function () {



                            //Pega o Valor do Historico
                            var NovoHistorico = $('#demo-textarea-input').val();
                            document.getElementById("ctl00_ContentPlaceHolder1_HistoricoHiddenField").value = NovoHistorico;


                            //Pega o Valor da Categoria
                            var cboCategoria = document.getElementById("cboCategoria");
                            document.getElementById("ctl00_ContentPlaceHolder1_CategoriaHiddenField").value = cboCategoria.options[cboCategoria.selectedIndex].value;



                            //Pega Data
                            var Data = $('#Data').val();
                            document.getElementById("ctl00_ContentPlaceHolder1_DataHiddenField").value = Data;


                            //Pega Hora
                            var Hora = document.getElementById("Hora");
                            document.getElementById("ctl00_ContentPlaceHolder1_HoraHiddenField").value = Hora.options[Hora.selectedIndex].value;


                            //Pega Codigo Entidade
                            var Codigo = $("#Codigo").text()
                            document.getElementById("ctl00_ContentPlaceHolder1_CodigoHiddenField").value = Codigo;


                            //Indicador para Gravar
                            document.getElementById("ctl00_ContentPlaceHolder1_OperacaoHiddenField").value = "Incluir";



                            var Erro;
                            Erro = "";

                            if (document.getElementById("ctl00_ContentPlaceHolder1_EventoHiddenField").value == "0") {
                                Erro = "Selecione um Evento!";
                            }


                            if (NovoHistorico == "") {
                                Erro = "Informe um Historico!";
                            }


                            if (Erro == "") {
                                $.niftyNoty({
                                    type: 'success',
                                    icon: 'fa fa-check',
                                    message: '<strong>Histórico atualizado!</strong>',
                                    container: 'floating',
                                    timer: 6000
                                });



                                //Chama o Servidor para Salvar
                                __doPostBack('btnSave', NovoHistorico)

                            }
                            else {
                                $.niftyNoty({
                                    type: 'danger',
                                    icon: 'fa fa-times',
                                    message: '<strong>' + Erro + '</strong>',
                                    container: 'floating',
                                    timer: 6000
                                });

                            }




                        }
                    },
                }
            });
        };



    </script>


    <script type="text/javascript">


        function selecionarEvento(CboEvento) {


            document.getElementById("ctl00_ContentPlaceHolder1_EventoHiddenField").value = CboEvento.options[CboEvento.selectedIndex].value;

            if (CboEvento.options[CboEvento.selectedIndex].value == 0) {
                alert("Selecione uma Categoria!");
            }



            if (CboEvento.options[CboEvento.selectedIndex].value == 1) {
                CodigoPai_1();
            }

            if (CboEvento.options[CboEvento.selectedIndex].value == 2) {
                CodigoPai_2();
            }


            if (CboEvento.options[CboEvento.selectedIndex].value == 3) {
                CodigoPai_3();
            }


            if (CboEvento.options[CboEvento.selectedIndex].value == 4) {
                CodigoPai_4();
            }


            if (CboEvento.options[CboEvento.selectedIndex].value == 5) {
                CodigoPai_5();
            }


            if (CboEvento.options[CboEvento.selectedIndex].value == 6) {
                CodigoPai_6();
            }

            if (CboEvento.options[CboEvento.selectedIndex].value == 7) {
                CodigoPai_7();
            }

            if (CboEvento.options[CboEvento.selectedIndex].value == 8) {
                CodigoPai_8();
            }

            if (CboEvento.options[CboEvento.selectedIndex].value == 9) {
                CodigoPai_9();
            }


            if (CboEvento.options[CboEvento.selectedIndex].value == 10) {
                CodigoPai_10();
            }



            /*var combo = document.getElementById("combo");

            for (var i = 0; i < combo.options.length; i++) {
              
            if (combo.options[i].value == categoria.options[categoria.selectedIndex].value) {
            combo.options[i].selected = "true";

            CodigoPai_1();

            break;
            }
            }
            */

        }



        function CodigoPai_1() {

            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }

            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Telefone";
            cboCategoria.add(opt0, cboCategoria.options[0]);

            var opt1 = document.createElement("option");
            opt1.value = "2";
            opt1.text = "E-mail";
            cboCategoria.add(opt1, cboCategoria.options[1]);

            var opt2 = document.createElement("option");
            opt2.value = "3";
            opt2.text = "Visita";
            cboCategoria.add(opt2, cboCategoria.options[2]);

            var opt3 = document.createElement("option");
            opt3.value = "4";
            opt3.text = "Online";
            cboCategoria.add(opt3, cboCategoria.options[3]);


        }




        function CodigoPai_2() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }


            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Demonstração";
            cboCategoria.add(opt0, cboCategoria.options[0]);

            var opt1 = document.createElement("option");
            opt1.value = "2";
            opt1.text = "Amostra";
            cboCategoria.add(opt1, cboCategoria.options[1]);

            var opt2 = document.createElement("option");
            opt2.value = "3";
            opt2.text = "Teste in loco";
            cboCategoria.add(opt2, cboCategoria.options[2]);

            var opt3 = document.createElement("option");
            opt3.value = "4";
            opt3.text = "Acompanhamento";
            cboCategoria.add(opt3, cboCategoria.options[3]);


        }



        function CodigoPai_3() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }


            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Fornecimento de Preço";
            cboCategoria.add(opt0, cboCategoria.options[0]);

            var opt1 = document.createElement("option");
            opt1.value = "2";
            opt1.text = "Fechamento";
            cboCategoria.add(opt1, cboCategoria.options[1]);

            var opt2 = document.createElement("option");
            opt2.value = "3";
            opt2.text = "Acompanhamento";
            cboCategoria.add(opt2, cboCategoria.options[2]);

        }





        function CodigoPai_4() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }


            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Observação";
            cboCategoria.add(opt0, cboCategoria.options[0]);

        }






        function CodigoPai_5() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }

            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Qualidade";
            cboCategoria.add(opt0, cboCategoria.options[0]);

            var opt1 = document.createElement("option");
            opt1.value = "2";
            opt1.text = "Preço concorrência";
            cboCategoria.add(opt1, cboCategoria.options[1]);

            var opt2 = document.createElement("option");
            opt2.value = "3";
            opt2.text = "Produto Específico";
            cboCategoria.add(opt2, cboCategoria.options[2]);

            var opt3 = document.createElement("option");
            opt3.value = "4";
            opt3.text = "Contrato";
            cboCategoria.add(opt3, cboCategoria.options[3]);

        }




        function CodigoPai_6() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }


            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Outros";
            cboCategoria.add(opt0, cboCategoria.options[0]);

        }



        function CodigoPai_7() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }

            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Análise de Crédito";
            cboCategoria.add(opt0, cboCategoria.options[0]);

            var opt1 = document.createElement("option");
            opt1.value = "2";
            opt1.text = "Aprovado";
            cboCategoria.add(opt1, cboCategoria.options[1]);

            var opt2 = document.createElement("option");
            opt2.value = "3";
            opt2.text = "Programado";
            cboCategoria.add(opt2, cboCategoria.options[2]);

            var opt3 = document.createElement("option");
            opt3.value = "4";
            opt3.text = "Depósito/Devendo";
            cboCategoria.add(opt3, cboCategoria.options[3]);

            var opt4 = document.createElement("option");
            opt4.value = "5";
            opt4.text = "Retira";
            cboCategoria.add(opt4, cboCategoria.options[4]);


            var opt5 = document.createElement("option");
            opt5.value = "6";
            opt5.text = "Expedição";
            cboCategoria.add(opt5, cboCategoria.options[5]);


            var opt6 = document.createElement("option");
            opt6.value = "7";
            opt6.text = "Faturar";
            cboCategoria.add(opt6, cboCategoria.options[6]);


            var opt7 = document.createElement("option");
            opt7.value = "8";
            opt7.text = "Faturado";
            cboCategoria.add(opt7, cboCategoria.options[7]);


            var opt8 = document.createElement("option");
            opt8.value = "9";
            opt8.text = "Produção";
            cboCategoria.add(opt8, cboCategoria.options[8]);

            var opt9 = document.createElement("option");
            opt9.value = "10";
            opt9.text = "Encerrado";
            cboCategoria.add(opt9, cboCategoria.options[9]);

            var opt10 = document.createElement("option");
            opt10.value = "11";
            opt10.text = "Agrupado";
            cboCategoria.add(opt10, cboCategoria.options[10]);


            var opt11 = document.createElement("option");
            opt11.value = "12";
            opt11.text = "Cancelado";
            cboCategoria.add(opt11, cboCategoria.options[11]);


            var opt12 = document.createElement("option");
            opt12.value = "13";
            opt12.text = "Orçamento";
            cboCategoria.add(opt12, cboCategoria.options[12]);

            var opt13 = document.createElement("option");
            opt13.value = "14";
            opt13.text = "NF Cancelada";
            cboCategoria.add(opt13, cboCategoria.options[13]);


            var opt14 = document.createElement("option");
            opt14.value = "15";
            opt14.text = "Projeto";
            cboCategoria.add(opt14, cboCategoria.options[14]);


            var opt15 = document.createElement("option");
            opt15.value = "16";
            opt15.text = "Desativado";
            cboCategoria.add(opt15, cboCategoria.options[15]);


        }




        function CodigoPai_8() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }

            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Emitida";
            cboCategoria.add(opt0, cboCategoria.options[0]);

            var opt1 = document.createElement("option");
            opt1.value = "2";
            opt1.text = "Cancelada";
            cboCategoria.add(opt1, cboCategoria.options[1]);

            var opt2 = document.createElement("option");
            opt2.value = "3";
            opt2.text = "Denegada";
            cboCategoria.add(opt2, cboCategoria.options[2]);

            var opt3 = document.createElement("option");
            opt3.value = "4";
            opt3.text = "Expedida";
            cboCategoria.add(opt3, cboCategoria.options[3]);

        }




        function CodigoPai_9() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }

            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Antigas";
            cboCategoria.add(opt0, cboCategoria.options[0]);

        }




        function CodigoPai_10() {


            var cboCategoria = document.getElementById("cboCategoria");
            while (cboCategoria.length) {
                cboCategoria.remove(0);
            }

            var opt0 = document.createElement("option");
            opt0.value = "1";
            opt0.text = "Carteira";
            cboCategoria.add(opt0, cboCategoria.options[0]);

        }




        /**
        * Exemplo Carregando a combobox
        */
        document.getElementById("btnCarregar").onclick = function () {
            var comboCidades = document.getElementById("cboCidades");

            var opt0 = document.createElement("option");
            opt0.value = "0";
            opt0.text = "";
            comboCidades.add(opt0, comboCidades.options[0]);

            var opt1 = document.createElement("option");
            opt1.value = "scs";
            opt1.text = "São Caetano do Sul";
            comboCidades.add(opt1, comboCidades.options[1]);

            var opt2 = document.createElement("option");
            opt2.value = "sa";
            opt2.text = "Santo André";
            comboCidades.add(opt2, comboCidades.options[2]);

            var opt3 = document.createElement("option");
            opt3.value = "sbc";
            opt3.text = "São Bernardo do Campo";
            comboCidades.add(opt3, comboCidades.options[3]);

        };

        /**
        * Descobrindo o valor selecionado
        */
        document.getElementById("btnInfo").onclick = function () {
            var comboCidades = document.getElementById("cboCidades");
            console.log("O indice é: " + comboCidades.selectedIndex);
            console.log("O texto é: " + comboCidades.options[comboCidades.selectedIndex].text);
            console.log("A chave é: " + comboCidades.options[comboCidades.selectedIndex].value);
        };


        /**
        * Selecionando um valor para a combobox
        */
        document.getElementById("btnAleatoriamente").onclick = function () {
            var comboCidades = document.getElementById("cboCidades");
            comboCidades.selectedIndex = Math.floor(Math.random() * comboCidades.length);
        };

        /**
        * Removendo elementos da combobox
        */
        document.getElementById("btnRemoverItem").onclick = function () {
            var comboCidades = document.getElementById("cboCidades");
            comboCidades.remove(0);
        };

        /**
        * Removendo todos os elementos
        */
        document.getElementById("btnRemoverTodos").onclick = function () {
            var comboCidades = document.getElementById("cboCidades");
            while (comboCidades.length) {
                comboCidades.remove(0);
            }
        };
    </script>
    <!--Finaliza Scrip para Tratar o combo no Modal-->

    <asp:TextBox ID="EntCodDetalhar" Text="" runat="server" Visible="false" ClientIDMode="Static" CssClass=""></asp:TextBox>


</asp:Content>
