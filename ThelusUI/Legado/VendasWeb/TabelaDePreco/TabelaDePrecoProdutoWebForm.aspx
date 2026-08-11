<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="TabelaDePrecoProdutoWebForm.aspx.cs" Inherits="VendasWeb.TabelaDePreco.TabelaDePrecoProdutoWebForm" %>

<%@ Register Src="~/usercontrol/UCTabelaPreco.ascx" TagPrefix="uc1" TagName="UCTabelaPreco" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>

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
                    <h3 class="panel-title">Tabela de Preço - Produto</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text=""
                    runat="server"></asp:Literal>

                <div class="panel-body">


                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="IDTabela" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="IDTabelaTextBox" runat="server" Enabled="false"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NomeTextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="NomeTextBox" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>



                      <div class="row">
                          <hr />
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="PesquisarPorLabel" runat="server" Text="Pesquisar Por:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:DropDownList ID="PesquisarPorDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem  Selected="True">Nome</asp:ListItem>
                                    <asp:ListItem>Codigo SAP</asp:ListItem>
                                </asp:DropDownList>


                                
                            </div>

                        </div>

                          <div class="col-sm-4">
                            <div class="form-group">
                               <asp:TextBox class="form-control" ID="PesquisarPorTextBox" runat="server" ></asp:TextBox>
                            </div>

                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>



                </div>



                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                             <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                CausesValidation="false" runat="server" OnClick="BuscarButton_Click"  >Buscar</asp:LinkButton>

                            &nbsp;

                            <asp:LinkButton ID="NovoProdutoButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg"
                                runat="server" OnClick="NovoProdutoButton_Click">Adicionar Novo Produto</asp:LinkButton>

                            &nbsp;

                            <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>

                        </div>

                    </div>
                </div>


            </div>

        </div>

        <asp:MultiView ID="ProdutoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ProdutoView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="ProdutoGridView" EmptyDataText="Nenhum Produto encontrado" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="ProdutoGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%"
                                OnRowDataBound="ProdutoGridView_RowDataBound">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="DeleteButton" class="btn btn-danger fa fa-times fa-lg"
                                                    CausesValidation="false" runat="server" OnClick="DeleteButton_Click"></asp:LinkButton>

                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDProduto" Visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="IDProdutoLabel" runat="server" Text='<%# Bind("IDProduto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDTabela" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDTabelaLabel" runat="server" Text='<%# Bind("IDTabela") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Codigo SAP" >
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoProdutoSAPLabel" runat="server" Text='<%# Bind("CodigoProdutoSAP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Nome">
                                        <ItemTemplate>
                                            <asp:Label ID="NomeProdutoLabel" runat="server" Text='<%# Bind("NomeProduto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unid.">
                                        <ItemTemplate>
                                            <asp:Label ID="UnidadeVendaLabel" runat="server" Text='<%# Bind("UnidadeVenda") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Valor Unitário">
                                        <ItemTemplate>
                                            <asp:TextBox ID="ValorUnitarioTextBox" class="form-control" runat="server" Text='<%# Bind("ValorUnitario") %>'
                                                AutoPostBack="true" onkeypress="mascara( this, mnumEvirgula );"
                                                OnTextChanged="ValorUnitarioTextBox_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>


                                            <asp:DropDownList ID="StatusDropDownList" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="StatusDropDownList_SelectedIndexChanged">
                                                <asp:ListItem>Ativo</asp:ListItem>
                                                <asp:ListItem>Inativo</asp:ListItem>
                                            </asp:DropDownList>

                                        </ItemTemplate>
                                    </asp:TemplateField>



                                     <asp:TemplateField HeaderText="Log">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton ID="LogButton" class="btn btn-primary fa fa-table fa-lg"
                                                    CausesValidation="false" runat="server" OnClick="LogButton_Click"></asp:LinkButton>

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

    <uc1:UCTabelaPreco runat="server" ID="UCTabelaPreco" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
