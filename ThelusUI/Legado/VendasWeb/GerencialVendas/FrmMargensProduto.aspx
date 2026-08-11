<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmMargensProduto.aspx.cs" Inherits="VendasWeb.GerencialVendas.FrmMargensProduto" %>
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
                        Margens Produtos</h3>
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

                                <asp:checkbox ID="CheckBox1" runat="server" CssClass="form-control" Text="Todos" >   
                                </asp:checkbox>
                            </div>
                          </div>

                         <div class="col-sm-2">
                            <div class="form-group">

                                <asp:checkbox ID="CheckBox2" runat="server" CssClass="form-control" Text="Vigentes" >   
                                </asp:checkbox>
                            </div>
                        </div>

                       <div class="col-sm-5">
                            <div class="form-group">

                                <asp:checkbox ID="CheckBox3" runat="server" CssClass="form-control" Text="Fora de Vigência" > 
                                </asp:checkbox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       <div class="col-sm-5">
                            <div class="form-group">

                                <asp:DropDownList ID="EmpresaDropDown" runat="server" CssClass="form-control" >    
                                </asp:DropDownList>
                            </div>
                        </div>
                       
                        <div class="col-sm-2">   
                            <div class="form-group">
                            
                                <asp:DropDownList ID="TipoDropDown" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Selected="True">Nome</asp:ListItem>
                                    <asp:ListItem Value="2" >Código</asp:ListItem>    
                                </asp:DropDownList>
                            </div>
                        </div>
                    
                   <div class="col-sm-2">   
                        <div class="form-group">
                            
                                <asp:textbox ID="txtValor" runat="server" CssClass="form-control"></asp:textbox>
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
                        <asp:LinkButton ID="NovoLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" OnClick="NovoLinkButton_Click1" runat="server">Novo Produto</asp:LinkButton>
                    </div>
                
                     <div class="panel-control">
                        <asp:LinkButton ID="BuscarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" OnClick="BuscarLinkButton_Click1" runat="server">Buscar Produtos</asp:LinkButton>
                    </div>
                
                
                </div>
            </div>
        </div>
   
  
   
    <asp:MultiView ID="ProdutosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ProdutosView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Lista de Produtos
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                       

                            <asp:GridView ID="ProdutosGridView" EmptyDataText="Nenhum Produto Localizado" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="ProdutosGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; margin-top: 0px;"  Width="282px">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Codigo">
                                        <ItemTemplate>                                          
                                           <asp:Button ID="btnEncerrar" runat="server" Text="Encerrar"
                                            CssClass="btn btn-danger" OnClick="btnEncerrar_Click" />                                 
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Codigo">
                                        <ItemTemplate>
                                            <asp:Label ID="ProdCodLabel" runat="server" Text='<%# Bind("ProdCodEstr") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome Produto">
                                        <ItemTemplate>
                                            <asp:Label ID="ProdNomeLabel" runat="server" Text='<%# Bind("ProdNome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Vigência">
                                        <ItemTemplate>
                                            <asp:Label ID="DataVigenciaLabel" runat="server" Text='<%# Bind("DataVigencia") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Custo ">
                                        <ItemTemplate>
                                            <asp:Label ID="CustoProdutoeLabel" runat="server" Text='<%# Bind("CustoProduto") %>'></asp:Label>
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
