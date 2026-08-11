<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="ListaRemessasWebForm.aspx.cs" Inherits="VendasWeb.financeiro.ListaRemessasWebForm" %>

<%@ Register Src="~/usercontrol/FinanceiroBancosWebUserControl.ascx" TagPrefix="uc1" TagName="FinanceiroBancosWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <h3 class="panel-title">Financeiro - Dados Bancários</h3>
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
                                <asp:Label ID="BancoLabel" runat="server" Text="Banco :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="BancoTextBox" runat="server" CssClass="form-control" placeholder="Nome ou código do banco." Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="AgenciaLabel" runat="server" Text="Agência :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="AgenciaTextBox" runat="server" CssClass="form-control" placeholder="Nome ou código da agência." Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ContaCorrenteLabel" runat="server" Text="Conta :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox ID="ContaCorrenteTextBox" runat="server" CssClass="form-control" placeholder="Nome ou código da conta." Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NumeroRemessaLabel" runat="server" Text="Remessa :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="NumeroRemessaTextBox" runat="server" CssClass="form-control" placeholder="Número da remessa."></asp:TextBox>
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
                        <asp:LinkButton ID="BuscarButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="RemessasMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="RemessasView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Remessas
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="RemessasGridView" EmptyDataText="Não foi possível encontrar nenhuma remessa" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="RemessasGridView_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sel.">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="SelecionarUpdatePanel" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <center>
                                                        <p>
                                                            <asp:RadioButton ID="SelecionarRadioButton" runat="server" AutoPostBack="True" OnCheckedChanged="SelecionarRadioButton_CheckedChanged" />
                                                        </p>
                                                    </center>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDBanco" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDBancoLabel" runat="server" Text='<%# Bind("IDBanco") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDAgencia" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDAgenciaLabel" runat="server" Text='<%# Bind("IDAgencia") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDConta" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDContaCorrenteLabel" runat="server" Text='<%# Bind("IDConta") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Banco">
                                        <ItemTemplate>
                                            <asp:Label ID="NomeBancoLabel" runat="server" Text='<%# Bind("NomeBanco") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Agência ">
                                        <ItemTemplate>
                                            <asp:Label ID="NomeAgenciaLabel" runat="server" Text='<%# Bind("NomeAgencia") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Conta ">
                                        <ItemTemplate>
                                            <asp:Label ID="ContaLabel" runat="server" Text='<%# Bind("DescricaoConta") %>'></asp:Label>
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

    <uc1:FinanceiroBancosWebUserControl runat="server" ID="FinanceiroBancosWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 

</asp:Content>
