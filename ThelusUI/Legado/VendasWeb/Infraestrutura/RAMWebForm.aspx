<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="RAMWebForm.aspx.cs" Inherits="VendasWeb.Infraestrutura.RAMWebForm" %>

<%@ Register Src="~/usercontrol/InfraestruturaMaquinaWebUserControl.ascx" TagPrefix="uc1" TagName="InfraestruturaMaquinaWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
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
                    <h3 class="panel-title">Infraestrutura - Informações da máquina</h3>
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

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label runat="server" Text="MAC:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox ID="MACTextBox" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label runat="server" Text="IP:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox ID="IPTextBox" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox ID="NomeTextBox" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Uso de RAM:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="UsoRAMTextBox" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>   
                        
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Ultima Atualização:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="UltimaAtualizacaoTextBox" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                            runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                        <asp:LinkButton ID="AtualizarButton" class="btn btn-primary btn-labeled fa fa-refresh fa-lg"
                            CausesValidation="false" runat="server" OnClick="AtualizarButton_Click">Atualizar</asp:LinkButton>

                    </div>

                </div>

            </div>

        </div>

        <asp:MultiView ID="InfraestruturaMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="InfraestruturaView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista RAM´s
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="InfraestruturaGridView" EmptyDataText="Não foi possível encontrar nenhum chamado." AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="InfraestruturaGridView_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>                                                                      

                                    <asp:TemplateField HeaderText="Modelo">
                                        <ItemTemplate>
                                            <asp:Label ID="ModeloLabel" runat="server" Text='<%# Bind("Modelo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Capacidade">
                                        <ItemTemplate>
                                            <asp:Label ID="CapacidadeLabel" runat="server" Text='<%# Bind("Capacidade") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Velocidade">
                                        <ItemTemplate>
                                            <asp:Label ID="VelocidadeLabel" runat="server" Text='<%# Bind("Velocidade") %>'></asp:Label>
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

    <uc1:InfraestruturaMaquinaWebUserControl runat="server" ID="InfraestruturaMaquinaWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 

</asp:Content>
