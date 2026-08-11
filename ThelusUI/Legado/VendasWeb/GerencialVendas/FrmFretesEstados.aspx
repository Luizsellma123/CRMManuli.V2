<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmFretesEstados.aspx.cs" Inherits="VendasWeb.GerencialVendas.FrmFretesEstados" %>
<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>

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
                    <h3 class="panel-title">
                        Fretes por Estado</h3>
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
                            <h5 class="text-bold">
                                Filtros
                            </h5>
                            <hr>
                        </div>
                    </div>
                    <!-- LINHA 1 - Painel FILTROS-->
                    <!--===================================================-->
                    <div class="row">
                       <div class="col-sm-2">
                            <div class="form-group">

                                <asp:Label ID="Label1" runat="server" Text="Origem :"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            
                             <div class="form-group">

                                <asp:DropDownList ID="OrigemDropDown" runat="server" CssClass="form-control" >
                                    <asp:ListItem Value="1" Selected="True">SIGLA UF</asp:ListItem>    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                             <div class="form-group">
                                  <asp:Label ID="Label2" runat="server" Text="Destino :"></asp:Label>
                             </div>
                        </div>

                        <div class="col-sm-2">
                            
                            <div class="form-group">
                                
                                

                                <asp:DropDownList ID="DestinoDropDown" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Selected="True">SIGLA UF</asp:ListItem>    
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
                        <asp:LinkButton ID="BuscarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" OnClick="BuscarLinkButton_Click1" runat="server">Buscar Estados</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
   
  
   
    <asp:MultiView ID="EstadosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="EstadosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Lista de Estados
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                       

                            <asp:GridView ID="EstadosGridView" EmptyDataText="Nenhum Estado Localizado" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="EstadoGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;" Height="282px">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="UF Origem " >
                                        <ItemTemplate>
                                            <asp:Label ID="OrigemCodLabel" runat="server" Text='<%# Bind("EstadoOrigem") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UF Destino " >
                                        <ItemTemplate>
                                            <asp:Label ID="DestinoNomeLabel" runat="server" Text='<%# Bind("EstadoDestino") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Percentual Desconto" >
                                        <ItemTemplate>
                                            
                                            <asp:TextBox ID="PercentualTextBox" AutoPostBack="true" 
                                                runat="server" Text='<%# Bind("PercentualDesconto") %>' 
                                                ontextchanged="PercentualTextBox_TextChanged"></asp:TextBox>
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

</asp:Content>
