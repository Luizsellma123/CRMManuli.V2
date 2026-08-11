<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroClienteFinanceiroWebForm.aspx.cs" Inherits="VendasWeb.Clientes.CadastroClienteFinanceiroWebForm" %>

<%@ Register Src="~/usercontrol/UCCadastroCliente.ascx" TagPrefix="uc1" TagName="UCCadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <h3 class="panel-title">Cadastro Cliente - Financeiro</h3>
            </div>
            <!--Painel Aberto-->
            <!--Campos para escolha da carteira e do cliente-->

            <!-- END Painel Aberto-->
            <!--===================================================-->
            <!--Painel FILTROS-->
            <!--===================================================-->
            <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse' aria-expanded='true' style='height: 0px;'>"
                runat="server"></asp:Literal>

            <div class="panel-body">

                <asp:HiddenField ID="IDCliente" runat="server" />

                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label ID="CodigoCliente" runat="server" Text="Código:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="CodigoClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                     ControlToValidate="CodigoClienteTextBox" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-5">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="NomeClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                     ControlToValidate="NomeClienteTextBox" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label ID="LimiteCreditoLabel" runat="server" Text="Limite Crédito:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="LimiteCreditoTextBox" runat="server" onkeypress="mascara(this,mnumEvirgula );"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                     ControlToValidate="LimiteCreditoTextBox" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>

                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label ID="AutorizacaoCobrancaLabel" runat="server" Text="Aut. Cobrança:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-5">
                        <div class="form-group">
                            <asp:DropDownList ID="AutorizacaoCobrancaDropDownList" runat="server" CssClass="form-control">
                                
                                <asp:ListItem Selected="True" Value="Sim">Sim</asp:ListItem>
                                <asp:ListItem Value="Nao">Não</asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                     ControlToValidate="AutorizacaoCobrancaDropDownList" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>

                        </div>
                    </div>

                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
                </div>


                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label ID="PagamentoUnicoLabel" runat="server" Text="Pagamento Unico:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:DropDownList ID="PagamentoUnicoDropDownList" runat="server" CssClass="form-control">
                                
                                <asp:ListItem Selected="True" Value="Sim">Sim</asp:ListItem>
                                <asp:ListItem Value="Nao">Não</asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                     ControlToValidate="PagamentoUnicoDropDownList" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>

                        </div>
                    </div>

                  

                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
                </div>


                 <div class="row">

                      <hr />

                       <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label ID="CondicaoPagamentoLabel" runat="server" Text="Cond. Pagamento:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-10">
                        <div class="form-group">
                            <asp:DropDownList ID="CondicaoPagamentoDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                            </asp:DropDownList>
                            
                        </div>
                    </div>

                     </div>

            </div>


           
            <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                             <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                CausesValidation="false" runat="server" OnClick="BuscarButton_Click" Visible="false" >Buscar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                                 runat="server" OnClick="GravarButton_Click" Visible="false">Gravar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                 runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>
                           
                        </div>

                    </div>
                </div>

        </div>

    </div>

    <asp:MultiView ID="ClientesCondicaoPagamentoMultiView" runat="server" ActiveViewIndex="0" Visible="false">
        <asp:View ID="PSIUView" runat="server">
            <!-- TABELA -->
            <!--===================================================-->
            <div class="panel">
                <div class="panel-heading">
                </div>
                <!-- Foo Table - Filtering -->
                <!--===================================================-->
                <div class="panel-body">
                    <div class="table-responsive">

                        <asp:GridView ID="ClienteCondicaoPagamentoGridView" EmptyDataText="Não foram encontrados dados com esses filtros" AutoGenerateColumns="False"
                            runat="server" AllowPaging="false"
                            CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                            Style="border-collapse: collapse; max-width: 100%">
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

                                <asp:TemplateField HeaderText="IDCondPag" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="IDCondPagLabel" runat="server" Text='<%# Bind("IDCondPag") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Código">
                                    <ItemTemplate>
                                        <asp:Label ID="CodigoSAPLabel" runat="server" Text='<%# Bind("CodigoSAP") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nome Condicao">
                                    <ItemTemplate>
                                        <asp:Label ID="NomeCondicaoLabel" runat="server" Text='<%# Bind("NomeCondicao") %>'></asp:Label>
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

    <uc1:UCCadastroCliente runat="server" ID="UCCadastroCliente" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
