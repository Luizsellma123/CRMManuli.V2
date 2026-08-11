<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmSimuladorVendedorBkp.aspx.cs" Inherits="VendasWeb.Entidades.FrmSimuladorVendedorBkp" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function pseudomascara(obj, e) {
            var tecla = (window.event) ? e.keyCode : e.which;
            if (tecla == 8 || tecla == 0)
                return true;
            if (tecla != 44 && tecla < 48 || tecla > 57)
                return false;
        }
    </script>

    <!-- LINHA 1-->
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
                    <h3 class="panel-title">Simulador de preços</h3>
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

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LblClasse" runat="server" Text="Empresa :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDownList" OnSelectedIndexChanged="EmpresaDropDownList_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control fstdropdown-select">
                                    <asp:ListItem Selected="True" Value="1">1 - MANULI CTBA</asp:ListItem>
                                    <asp:ListItem Value="2">2 - MANULI SP</asp:ListItem>
                                    <asp:ListItem Value="3">3 - MANULI AM</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LabelQuantidade" runat="server" Text="Quantidade :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="QuantidadeTextBox" runat="server" CssClass="form-control" onkeypress="return pseudomascara( this , event ) ;"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ProdutoLabel" runat="server" Text="Produto :"></asp:Label>
                            </div>
                        </div>


                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="ProdutoDropDownList" runat="server" CssClass="form-control fstdropdown-select"
                                    title="Escolha um produto">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="VendedorLabel" runat="server" Text="Nível Vendedor :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="VendedorDropDownList" AutoPostBack="true" runat="server" CssClass="form-control fstdropdown-select">
                                        <asp:ListItem Selected="True" Value="">Selecione</asp:ListItem>
                                        <asp:ListItem Value="Vendedor">Vendedor</asp:ListItem>
                                        <asp:ListItem Value="Representante">Representante</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LabelICMS" runat="server" Text="Ex-ICMS :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="ICMSTextBox" runat="server" CssClass="form-control" onkeypress="return pseudomascara( this , event ) ;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LocalLabel" runat="server" Text="Tabela:"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="FaturamentoDropDownList" runat="server" CssClass="form-control fstdropdown-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PrecoLabel" runat="server" Text="Preço final :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="PrecoInputTextBox" runat="server" CssClass="form-control" onkeypress="return pseudomascara( this , event ) ;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Class. Comercial:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ClassificacaoComercialDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Frete:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="FreteDropDownList" runat="server" CssClass="form-control fstdropdown-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="FreteDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label Text="Novo Cliente: " ID="PadraoLabel" runat="server" Style="position: relative; bottom: 2px;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:CheckBox ID="NovoClienteCheck" runat="server" OnCheckedChanged="NovoClienteCheck_CheckedChanged" AutoPostBack="true" />
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label Text="À vista: " ID="AvistaLabel" runat="server" Style="position: relative; bottom: 2px;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:CheckBox ID="AvistaCheckBox" runat="server" />
                            </div>
                        </div>

                    </div>

                    <br />

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ClienteLabel" runat="server" Text="Cliente :"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-9">
                            <div class="form-group">
                                <asp:TextBox ID="ClienteInput" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:LinkButton ID="PlusButton" class="btn btn-info fa fa-plus" Style="width: 100%;"
                                    CausesValidation="false" runat="server" OnClick="PlusButton_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" Text="Destino e Transportador:"></asp:Label>
                        </div>
                    </div>

                    <hr />

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="País:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="PaisDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                    <asp:ListItem Value="30" Text="Brasil" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Estado:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:UpdatePanel ID="EstadoUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="EstadoDropDownList" runat="server" CssClass="form-control fstdropdown-select"
                                            AutoPostBack="true" OnSelectedIndexChanged="EstadoDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="EstadoDropDownList" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Município:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:UpdatePanel ID="MunicipioUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="MunicipioDropDownList" runat="server" CssClass="form-control fstdropdown-select"
                                            AutoPostBack="true" OnSelectedIndexChanged="MunicipioDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="MunicipioDropDownList" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Transportador:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="TransportadorDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <%--<div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:LinkButton ID="CalcularFreteLinkButton" class="btn btn-block btn-info btn-labeled fa fa-truck fa-3x"
                                    CausesValidation="false" runat="server" Text="Calcular Frete"
                                    OnClick="CalcularFreteLinkButton_Click" Visible="false"></asp:LinkButton>
                            </div>
                        </div>

                    </div>--%>

                    <asp:HiddenField runat="server" ID="ValorFreteHiddenField" />

                    <asp:HiddenField runat="server" ID="PrevisaoEntregaHiddenField" />

                    <%--<div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Valor Frete:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="ValorFreteTextBox" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Previsão Entrega:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="PrevisaoEntregaTextBox" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>--%>

                    <br />

                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" Text="Caculadora Desconto:"></asp:Label>
                        </div>
                    </div>

                    <hr />

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Valor Item:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="ValorItemTextBox" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Desconto (%):"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="DescontoTextBox" runat="server" CssClass="form-control" onkeypress="return pseudomascara( this , event ) ;"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Valor C/ desconto:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="ValorComDescontoTextBox" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:LinkButton ID="CalcularDescontoLinkButton" class="btn btn-block btn-info btn-labeled fa fa-truck fa-3x"
                                    CausesValidation="false" runat="server" Text="Calcular Desconto"
                                    OnClick="CalcularDescontoLinkButton_Click"></asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <br />

                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" Text="Histórico:"></asp:Label>
                        </div>
                    </div>

                    <hr />

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:TextBox ID="ObservBox" Style="height: 100px; width: 100%;" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>

                <!--===================================================-->

                <!-- END LINHA 1 - Painel FILTROS-->
            </div>
            <!-- 
            </div> -->

            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">

                    <div class="panel-control">

                        <asp:LinkButton ID="RetornarButton" class="btn btn-success btn-labeled fa fa-arrow-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>

                        <%--<a id="CalculoFrete" class="btn btn-success btn-labeled fa fa-truck fa-lg" href="BaseFretes/CalculosTransportadorasNovo.html" target="_blank">Cotacao Frete</a>--%>

                        <asp:LinkButton ID="CopiaLinkButton" class="btn btn-success btn-labeled fa fa-copy fa-lg"
                            CausesValidation="false" runat="server" OnClick="CopiaLinkButton_Click">Copiar</asp:LinkButton>

                        <asp:LinkButton ID="SimularButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="SimularButton_Click">Simular</asp:LinkButton>

                        <asp:LinkButton ID="AnaliseButton" class="btn btn-success btn-labeled fa fa-envelope fa-lg"
                            CausesValidation="false" runat="server" OnClick="AnaliseButton_Click" Visible="false">Enviar Análise</asp:LinkButton>

                        <asp:LinkButton ID="SalvaSimulacaoLinkButton" class="btn btn-success btn-labeled fa fa-floppy-o fa-lg"
                            CausesValidation="false" runat="server" OnClick="SalvaSimulacaoButton_Click">Salvar Simulação</asp:LinkButton>

                    </div>

                </div>
            </div>

        </div>

        <asp:MultiView ID="SimuladorMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="SimuladorView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Simulação
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="SimulacaoGridView" EmptyDataText="A simulação não foi possível" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="SimulacaoGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Aprovação ">
                                        <ItemTemplate>
                                            <asp:Label ID="AlcadaGrid" runat="server" Text='<%# Bind("Aprovacao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome do produto ">
                                        <ItemTemplate>
                                            <asp:Label ID="ProdutoGrid" runat="server" Text='<%# Bind("NomeProduto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipo de material ">
                                        <ItemTemplate>
                                            <asp:Label ID="MaterialGrid" runat="server" Text='<%# Bind("TipoMaterial") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Peso ">
                                        <ItemTemplate>
                                            <asp:Label ID="PesolGrid" runat="server" Text='<%# Bind("Peso") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ICMS ">
                                        <ItemTemplate>
                                            <asp:Label ID="ICMSGrid" runat="server" Text='<%# Eval("ICMS","{0:N}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Vlr. CIF ">
                                        <ItemTemplate>
                                            <asp:Label ID="ValorCIFGrid" runat="server" Text='<%# Eval("PrecoCIF","{0:N}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Vlr. FOB ">
                                        <ItemTemplate>
                                            <asp:Label ID="ValorFOBGrid" runat="server" Text='<%# Eval("PrecoFOB","{0:N}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Situação ">
                                        <ItemTemplate>
                                            <center>
                                                <asp:Label ID="IconelGrid" runat="server" Text='<%# Bind("Icone") %>'></asp:Label></center>
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

    </div>

</asp:Content>
