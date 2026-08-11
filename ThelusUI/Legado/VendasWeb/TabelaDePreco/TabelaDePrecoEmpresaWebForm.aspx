<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="TabelaDePrecoEmpresaWebForm.aspx.cs" Inherits="VendasWeb.TabelaDePreco.TabelaDePrecoEmpresaWebForm" %>

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
                    <h3 class="panel-title">Tabela de Preço - Empresas</h3>
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

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Empresa:"></asp:Label>
                            </div>
                        </div>

                         <div class="col-sm-5">
                            <div class="form-group">
                                 <asp:DropDownList ID="IDEmpresaDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                
                            </div>
                        </div>

                    </div>


                </div>



                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                            <asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                                runat="server" OnClick="GravarButton_Click" >Gravar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>

                        </div>

                    </div>
                </div>


            </div>

        </div>

        <asp:MultiView ID="EmpresaMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="EmpresaView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="EmpresaGridView" EmptyDataText="Nenhuma Empresa encontrada" AutoGenerateColumns="False"
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

                                    <asp:TemplateField HeaderText="ID Empresa" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDEmpresaLabel" runat="server" Text='<%# Bind("IDEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome Empresa">
                                        <ItemTemplate>
                                            <asp:Label ID="NomeEmpresaLabel" runat="server" Text='<%# Bind("NomeEmpresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CNPJ">
                                        <ItemTemplate>
                                            <asp:Label ID="CNPJLabel" runat="server" Text='<%# Bind("CNPJ") %>'></asp:Label>
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
