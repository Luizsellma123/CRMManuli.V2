<%@ Page Title="Agenda Visita" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmAgendaVisita.aspx.cs" Inherits="VendasWeb.Entidades.FrmAgendaVisita" %>

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
        <div class="col-sm-15">
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
                        Agenda de Visita</h3>
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
                                Filtros</h5>
                            <hr>
                        </div>
                    </div>
                    <!-- LINHA 1 - Painel FILTROS-->
                    <!--===================================================-->
                    <div class="row">
                        <div class="col-sm-5">
                            <h5>
                                <asp:Label ID="GestorLabel" runat="server" Text="Gestor:" CssClass="text-thin"></asp:Label></h5>
                            <select class="selectpicker show-tick" multiple data-placeholder="Escolha um Gestor..."
                                title="Escolha um Gestor..." data-style="btn-primary" data-live-search="true"
                                id="GestorDropDownList" runat="server">
                            </select>
                            <asp:RequiredFieldValidator ID="GestorRequiredFieldValidator" runat="server" Display="Dynamic"
                                SetFocusOnError="True" ControlToValidate="GestorDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-4">
                            <br />
                            <br />
                            <asp:LinkButton ID="GestorLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                                CausesValidation="false" runat="server" OnClick="GestorDropDownList_SelectedIndexChanged">Buscar Classes</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <h5>
                                <asp:Label ID="ClasseLabel" runat="server" Text="Classe:" CssClass="text-thin"></asp:Label></h5>
                            <select class="selectpicker show-tick" multiple data-placeholder="Escolha uma Classe..."
                                title="Escolha uma Classe..." data-style="btn-primary" data-live-search="true"
                                id="ClasseDropDownList" runat="server">
                            </select>
                            <asp:RequiredFieldValidator ID="ClasseRequiredFieldValidator" runat="server" Display="Dynamic"
                                SetFocusOnError="True" ControlToValidate="ClasseDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-4">
                            <br />
                            <br />
                            <asp:LinkButton ID="ClasseLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                                CausesValidation="false" runat="server" OnClick="ClasseDropDownList_SelectedIndexChanged">Buscar Vendedores</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <h5>
                                <asp:Label ID="VendedorLabel" runat="server" Text="Vendedor:" CssClass="text-thin"></asp:Label></h5>
                            <select class="selectpicker show-tick" multiple data-placeholder="Escolha um Vendedor..."
                                title="Escolha um Vendedor..." data-style="btn-primary" data-live-search="true"
                                id="VendedorDropDownList" runat="server">
                            </select>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                SetFocusOnError="True" ControlToValidate="VendedorDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
                    <div class="row">
                        <div class="col-sm-5">
                            <h5>
                                <asp:Label ID="AgendaStatusLabel" runat="server" CssClass="text-thin" Text="">Status:</asp:Label></h5>
                            <asp:DropDownList ID="AgendaStatusDropDownList" runat="server" CssClass="selectpicker show-tick">
                                <asp:ListItem Selected="True" Value="">Todos</asp:ListItem>
                                <asp:ListItem>Agendada</asp:ListItem>
                                <asp:ListItem>Em Atendimento</asp:ListItem>
                                <asp:ListItem>Finalizada</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <h5>
                                <label>
                                    Data Inicial:</label></h5>
                            <asp:TextBox class="" ID="DataITextBox" TextMode="Date" runat="server" Width="150px"></asp:TextBox>
                            <h5>
                                <label>
                                    &nbsp;&nbsp; Data Final:</label></h5>
                            <asp:TextBox class="" ID="DataFTextBox" TextMode="Date" runat="server" Width="150px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <br />
                            <br />
                            <asp:LinkButton ID="BuscarLinkButton" class="btn btn-primary" runat="server" title="Salvar"
                                data-rel="tooltip" OnClick="BuscarLinkButton_Click">
                                <span class="glyphicon glyphicon-search" aria-hidden="true"> Buscar</span>
                            </asp:LinkButton>
                        </div>
                    </div>
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
                        <asp:LinkButton ID="NovaLinkButton" class="btn btn-success" runat="server" title="Nova Agenda"
                            CausesValidation="false" data-rel="tooltip" OnClick="NovaLinkButton_Click"> Nova Agenda &raquo;</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--===================================================-->
        <!--End Painel Carteiras e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="AgendasMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="AgendasView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Lista de Agendas
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:LinkButton ID="ImprimirLinkButton" class="btn btn-primary" runat="server" title="Imprimir"
                                data-rel="tooltip" OnClick="ImprimirLinkButton_Click">
                                <span class="glyphicon glyphicon-print center" aria-hidden="true"> Imprimir</span>
                            </asp:LinkButton>
                            <br /><br />
                            <asp:GridView ID="AgendaGridView" EmptyDataText="Nenhuma Agenda Localizada" AutoGenerateColumns="False"
                                runat="server" EnableModelValidation="True" AllowPaging="True" OnPageIndexChanging="AgendaGridView_PageIndexChanged"
                                PageSize="10" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Detalhes/Editar">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="DetalheButton" class="btn btn-primary" runat="server" OnClick="DetalheButton_Click"
                                                    title="Editar/Visualizar" data-rel="tooltip">
                                                            <span class="glyphicon glyphicon-edit center"></span>

                                                </asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Imprimir">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="ImprimirLinkButton" class="btn btn-warning" runat="server" OnClick="ImprimirButton_Click"
                                                    title="Imprimir Agenda" data-rel="tooltip">
                                                            <span class="glyphicon glyphicon-print center"></span>

                                                </asp:LinkButton>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Código " Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Agenda_Visita_IDLabel" runat="server" Text='<%# Bind("Agenda_Visita_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AgendaStatus" HeaderText="Status"></asp:BoundField>
                                    <asp:BoundField DataField="DataVisita" HeaderText="Data Visita" SortExpression="DataVisita"
                                        DataFormatString="{0:d}"></asp:BoundField>
                                    <asp:BoundField DataField="VendCod" HeaderText="Cód. Vendedor"></asp:BoundField>
                                    <asp:BoundField DataField="VendNome" HeaderText="Vendedor"></asp:BoundField>
                                    <asp:BoundField DataField="UsuCod" HeaderText="Usuario"></asp:BoundField>
                                    <asp:BoundField DataField="EntCod" HeaderText="Cód. Entidade" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="EntNome" HeaderText="Entidade"></asp:BoundField>
                                    <asp:BoundField DataField="EntCpfCgc" HeaderText="CPF/CNPJ"></asp:BoundField>
                                    <asp:BoundField DataField="CidNomeComp" HeaderText="Cidade"></asp:BoundField>
                                    <asp:BoundField DataField="UfSigla" HeaderText="UF"></asp:BoundField>
                                    <asp:BoundField DataField="Telefone" HeaderText="Telefone"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="ObservacaoTextBox" runat="server" Text='<%# Bind("Observacao") %>'
                                                class="form-control" Width="250px" Height="100px" TextMode="MultiLine"></asp:TextBox>
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
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
