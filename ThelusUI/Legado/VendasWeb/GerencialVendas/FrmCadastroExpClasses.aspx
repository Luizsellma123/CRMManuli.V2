<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmCadastroExpClasses.aspx.cs" Inherits="VendasWeb.GerencialVendas.FrmCadastroExpClasses" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>

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
                    <h3 class="panel-title">Cadastro Expectativas Classes</h3>
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
                        <div class="col-sm-2">
                            <div class="form-group">

                                <asp:Label ID="LblClasse" runat="server" Text="Classe :"></asp:Label>

                            </div>
                        </div>

                    <div class="col-sm-5">
                            <div class="form-group">
                                <select class="selectpicker show-tick"  data-placeholder="Escolha um vendedor..."
                                    title="Escolha um vendedor..." data-style="btn-primary" data-live-search="true"
                                    id="VendedoresSelect" runat="server">
                                </select>
                            </div>
                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">

                                <asp:Label ID="LblMes" runat="server" Text="Mês :"></asp:Label>

                            </div>
                        </div>
                        


                        <div class="col-sm-3">
                            <div class="form-group">

                                <asp:DropDownList ID="MesDropDown" runat="server" CssClass="form-control">
                                    <asp:ListItem>Janeiro</asp:ListItem>
                                    <asp:ListItem>Fevereiro</asp:ListItem>
                                    <asp:ListItem>Março</asp:ListItem>
                                    <asp:ListItem>Abril</asp:ListItem>
                                    <asp:ListItem>Maio</asp:ListItem>
                                    <asp:ListItem>Junho</asp:ListItem>
                                    <asp:ListItem>Julho</asp:ListItem>
                                    <asp:ListItem>Agosto</asp:ListItem>
                                    <asp:ListItem>Setembro</asp:ListItem>
                                    <asp:ListItem>Outubro</asp:ListItem>
                                    <asp:ListItem>Novembro</asp:ListItem>
                                    <asp:ListItem>Dezembro</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">

                                <asp:Label ID="LblNomeLinha" runat="server" Text="Nome Linha :"></asp:Label>

                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:DropDownList ID="NomeLinhaDropDown" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">

                                <asp:Label ID="LblAno" runat="server" Text="Ano :"></asp:Label>

                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="AnoDropDown" runat="server" CssClass="form-control">
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                </asp:DropDownList>
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
                        <asp:LinkButton ID="NovaLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" OnClick="NovaLinkButton_Click1" runat="server">Nova Expectativa</asp:LinkButton>
                    </div>

                    <div class="panel-control">
                        <asp:LinkButton ID="BuscarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" OnClick="BuscarLinkButton_Click1" runat="server">Buscar Expectativas</asp:LinkButton>
                    </div>


                </div>
            </div>
        </div>



        <asp:MultiView ID="ExpectativaMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ExpectativaView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Expectativas
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">


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

</asp:Content>
