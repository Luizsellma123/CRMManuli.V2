<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="TabelaDePrecoProdutoLogWebForm.aspx.cs" Inherits="VendasWeb.TabelaDePreco.TabelaDePrecoProdutoLogWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">

        <div class="col-sm-14">
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
                    <h3 class="panel-title">Tabela de Preço - Logs</h3>
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
                                <asp:Label ID="Label1" runat="server" Text="Nome Tabela:"></asp:Label>
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

                    <div  class="row">

                        
                           <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodProdutoLabel2" runat="server" Text="Código SAP:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CodigoProdutoSAPTextBox" runat="server" Enabled="false"></asp:TextBox>

                            </div>
                        </div>


                           <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Nome Produto:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NomeProdutoTextBox" runat="server" Enabled="false"></asp:TextBox>
                                
                            </div>
                        </div>

                    </div>



                    <div class="row">


                        <div class="table-responsive">

                            <asp:GridView ID="LogProdutoGridView" EmptyDataText="Nenhum Log Foi encontrado" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                  

                                    <asp:TemplateField HeaderText="IDProduto" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDProdutoLabel" runat="server" Text='<%# Bind("IDProduto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Valor Unitario">
                                        <ItemTemplate>
                                            <asp:Label ID="ValorUnitarioLabel" runat="server" Text='<%# Bind("ValorUnitario") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="StatusLabel" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipo Alteracao">
                                        <ItemTemplate>
                                            <asp:Label ID="TipoAlteracaoLabel" runat="server" Text='<%# Bind("TipoAlteracao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Usuario">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoUsuarioLabel" runat="server" Text='<%# Bind("CodigoUsuario") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Data">
                                        <ItemTemplate>
                                            <asp:Label ID="DataAlteracaoLabel" runat="server" Text='<%# Bind("DataAlteracao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>


                    </div>






                </div>



                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">


                            <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>

                        </div>

                    </div>
                </div>


            </div>

        </div>



    </div>


    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
