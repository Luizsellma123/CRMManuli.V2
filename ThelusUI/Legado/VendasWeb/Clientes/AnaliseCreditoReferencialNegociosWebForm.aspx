<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AnaliseCreditoReferencialNegociosWebForm.aspx.cs" Inherits="VendasWeb.Clientes.AnaliseCreditoReferencialNegociosWebForm" %>

<%@ Register Src="~/usercontrol/AnaliseCreditoDetalheWebUserControl.ascx" TagPrefix="uc1" TagName="AnaliseCreditoDetalheWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="IDClienteHiddenField" runat="server" />

    <asp:HiddenField ID="IDAnaliseHiddenField" runat="server" />

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
                    <h3 class="panel-title">Cadastro Cliente - Análise Crédito - Referencial Negócios</h3>
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

                    <%-- Detalhes principais --%>
                    <div>

                        <%-- Análise, Data --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Análise:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="AnaliseTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Data:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="DataTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Código, Nome --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Código:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="CodigoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Nome:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="NomeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Fantasia --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Fantasia:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="FantasiaTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Relacionamento com fornecedores --%>
                        <div id="RelacionamentoComFornecedoresDiv" runat="server">

                            <div class="row">
                                <div class="col-sm">
                                    <div class="form-group">
                                        <strong>
                                            <asp:Label runat="server" Style="font-size: 12px;" Text="Relacionamento com fornecedores:"></asp:Label>
                                        </strong>
                                    </div>
                                </div>
                                <div class="col-sm">
                                    <div class="form-group">
                                        <hr />
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-3">
                                    <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton1" Width="100%" class="btn btn-primary"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-3">
                                    <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton2" Width="100%" class="btn btn-primary"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-3">
                                    <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton3" Width="100%" class="btn btn-primary"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-3">
                                    <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton4" Width="100%" class="btn btn-primary"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm">
                                    <div class="form-group">
                                        <br />
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-3">
                                    <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton5" Width="100%" class="btn btn-primary"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-3">
                                    <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton6" Width="100%" class="btn btn-primary"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

                                <div class="col-sm-6">
                                    <asp:LinkButton ID="RelacionamentoComFornecedoresLinkButton7" Width="100%" class="btn btn-primary"
                                        CausesValidation="false" runat="server"></asp:LinkButton>
                                </div>

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

                        <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="ReferencialNegociosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ReferencialNegociosView" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="ReferencialNegociosGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação"
                                AutoGenerateColumns="true" runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%"
                                OnPageIndexChanging="ReferencialNegociosGridView_PageIndexChanging" Visible="true"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                <PagerStyle CssClass="pagination-ys" />
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

    <uc1:AnaliseCreditoDetalheWebUserControl runat="server" ID="AnaliseCreditoDetalheWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>     
    
</asp:Content>
