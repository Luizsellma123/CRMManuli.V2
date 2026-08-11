<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.master" AutoEventWireup="true" CodeBehind="SimuladorFreteWebForm.aspx.cs" Inherits="VendasWeb.Logistica_New.SimuladorFreteWebForm" %>

<%@ Register Src="../usercontrol/LogisticaWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>

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
                    <h3 class="panel-title">Simulador de fretes</h3>
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

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDownList" OnSelectedIndexChanged="EmpresaDropDownList_SelectedIndexChanged"
                                    AutoPostBack="true" runat="server" CssClass="form-control fstdropdown-select">
                                    <asp:ListItem Selected="True" Value="1">1 - MANULI CTBA</asp:ListItem>
                                    <asp:ListItem Value="2">2 - MANULI SP</asp:ListItem>
                                    <asp:ListItem Value="3">3 - MANULI AM</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <br />

                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" Text="Produto(s):"></asp:Label>
                        </div>
                    </div>

                    <hr />

                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:DropDownList ID="ProdutoDropDownList" runat="server" CssClass="form-control fstdropdown-select"
                                    title="Escolha um produto">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LabelQuantidade" runat="server" Text="Quantidade :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="QuantidadeTextBox" runat="server" CssClass="form-control" onkeypress="return pseudomascara( this , event ) ;"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:LinkButton ID="AdicionarProdutoLinkButton" class="btn btn-primary btn-labeled fa fa-plus" Height="30px"
                                        CausesValidation="false" runat="server" OnClick="AdicionarProdutoLinkButton_Click">Adicionar</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div runat="server" class="row" id="ProdutosRow">

                        <asp:MultiView ID="ProdutosMultiView" runat="server" ActiveViewIndex="0">
                            <asp:View ID="ProdutosView" runat="server">

                                <div class="panel-body">
                                    <div class="table-responsive">

                                        <asp:UpdatePanel ID="TesteUpdatePanel3" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <asp:GridView ID="ProdutosGridView" EmptyDataText="Nenhum produto inserido" AutoGenerateColumns="False"
                                                    runat="server" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                                    Style="border-collapse: collapse; max-width: 100%">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Excluir" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <center>
                                                                            <asp:LinkButton ID="ExcluirLinkButton" class="btn btn-danger fa fa-times"
                                                                                CausesValidation="false" runat="server" OnClick="ExcluirLinkButton_Click">
                                                                            </asp:LinkButton>
                                                                        </center>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="ExcluirLinkButton" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="IDLocalProdutoLabelGridView" runat="server" Text='<%# Bind("IDLocalProduto") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Código Produto" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CodigoProdutoLabelGridView" runat="server" Text='<%# Bind("CodigoProduto") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Produto">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NomeProdutoLabelGridView" runat="server" Text='<%# Bind("NomeProduto") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qtd." ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="TesteUpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <center>
                                                                            <asp:TextBox ID="QuantidadeTextBoxGridView" runat="server" Text='<%# Bind("QuantidadeProduto") %>'
                                                                                CssClass="form-control" onkeypress="return pseudomascara( this , event ) ;"
                                                                                OnTextChanged="QuantidadeTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        </center>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="QuantidadeTextBoxGridView" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qtd. Conv." ItemStyle-Width="10%" Visible="false">
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:Label ID="QuantidadeConvertidaProdutoLabelGridView" runat="server" Text='<%# Bind("QuantidadeConvertidaProduto") %>'></asp:Label>
                                                                </center>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Peso (kg)">
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:Label ID="PesoProdutoLabelGridView" runat="server" Text='<%# Bind("PesoProduto") %>'></asp:Label>
                                                                </center>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ProdutosGridView" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <!--===================================================-->

                            </asp:View>
                        </asp:MultiView>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Qtd. Total:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="QuantidadeTotalTextBox" runat="server" Enabled="false"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <%--<div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Qtd. Total Conv.:"></asp:Label>
                            </div>
                        </div>--%>

                        <%--<div class="col-sm-2">
                            <div class="form-group">--%>
                        <asp:TextBox ID="QuantidadeTotalConvertidaTextBox" runat="server" Enabled="false"
                            CssClass="form-control" Visible="false"></asp:TextBox>
                        <%--</div>
                        </div>--%>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Peso Total (kg):"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="PesoTotalTextBox" runat="server" Enabled="false"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <br />

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PrecoLabel" runat="server" Text="Valor da Nota:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox ID="ValorNotaTextBox" runat="server" CssClass="form-control" onkeypress="return pseudomascara( this , event ) ;"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <br />

                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" Text="Tipo de Frete e Destino:"></asp:Label>
                        </div>
                    </div>

                    <hr />

                    <div class="row">

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

                    </div>

                    <div class="row">

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

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Município:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:UpdatePanel ID="MunicipioUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="MunicipioDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="MunicipioDropDownList" />
                                    </Triggers>
                                </asp:UpdatePanel>
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

                        <asp:LinkButton ID="RetornarButton" class="btn btn-danger btn-labeled fa fa-arrow-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="SimularButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="SimularButton_Click">Simular</asp:LinkButton>

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

                            <asp:GridView ID="SimulacaoGridView" EmptyDataText="Não há transportadoras disponíveis para esta região" AutoGenerateColumns="False"
                                runat="server" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Transportadora">
                                        <ItemTemplate>
                                            <asp:Label ID="TransportadoraLabelGridView" runat="server" Text='<%# Bind("Transportadora") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Valor Frete">
                                        <ItemTemplate>
                                            <asp:Label ID="ValorFreteLabelGridView" runat="server" Text='<%# Bind("ValorFrete") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DataEntrega">
                                        <ItemTemplate>
                                            <asp:Label ID="DataEntregaLabelGridView" runat="server" Text='<%# Bind("DataEntrega") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prazo Entrega">
                                        <ItemTemplate>
                                            <asp:Label ID="PrazoEntregaLabelGridView" runat="server" Text='<%# Bind("PrazoEntrega") %>'></asp:Label>
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
