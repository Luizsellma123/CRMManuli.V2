<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="frmVendedores.aspx.cs" Inherits="VendasWeb.Entidades.frmVendedores" %>
<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
                <!-- LINHA 1-->
            <div class="row">
            
                
                
                <!-- COLUNA 1-->
                <div class="col-sm-9">
                   
                   
                    <%--<!--===================================================-->
                    <!--Painel Carteiras e Filtros-->
                    <!--===================================================-->
                    <div class="panel panel-info">
                        <!--Panel heading-->
                        <!--Título e controles para o painel de Filtros-->
                        <div class="panel-heading">
                            <div class="panel-control">
                                <button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                                    data-target="#filtros">
                                    <i class="fa fa-refresh"></i>
                                </button>
                                <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                                    <i class="fa fa-chevron-down"></i>
                                </button>
                                <button type="button" class="btn btn-default" data-dismiss="panel">
                                    <i class="fa fa-times"></i>
                                </button>
                            </div>
                            <h3 class="panel-title">
                                Selecionar Vendedor</h3>
                        </div>
                        <!--Painel Aberto-->
                        <!--Campos para escolha da carteira e do cliente-->
                        <div id="painel_aberto" class="">
                          
                        </div>
                        <!-- END Painel Aberto-->
                        <!--===================================================-->
                        <!--Painel FILTROS-->
                        <!--===================================================-->
                        <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>
                        <div class="panel-body">
                            <!-- LINHA 1 - Painel FILTROS-->
                            <div class="row">
                                <div class="col-xs-12">
                                    <h5 class="text-bold">
                                        Filtros</h5>
                                    <hr>
                                </div>
                                


                           <div class="col-lg-3">
                                <div class="form-group">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" >
                                        <asp:ListItem Value="1">NOME VENDEDOR</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">CÓD. VENDEDOR</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <asp:TextBox ID="txtFiltroVendCod" runat="server" placeholder="Procurar" class="form-control"></asp:TextBox>
                                </div>
                            </div>






                            </div>
                            <!--===================================================-->
                            <!-- END LINHA 1 - Painel FILTROS-->
                            <!-- LINHA 2 - Painel FILTROS-->
                       
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
                                  <asp:LinkButton ID="btnListar" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Buscar Vendedor" data-rel="tooltip" OnClick="btnListar_Click"> 
             Buscar Vendedor </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
--%>
                 



                <%--<!--===================================================-->
                <!--End Painel Carteiras e Filtros-->
                <!--===================================================-->
                <asp:MultiView ID="VendedoresMultiView" runat="server" ActiveViewIndex="0" Visible="false">
                    <asp:View ID="VendedoresView" runat="server">
                        <!-- TABELA -->
                        <!--===================================================-->
                        <div class="panel">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Vendedores
                                </h3>
                            </div>
                            <!-- Foo Table - Filtering -->
                            <!--===================================================-->
                            <div class="panel-body">
                                <div class="table-responsive">
                                  
                                   <asp:GridView ID="ListaVendedorGridView" EmptyDataText="Nenhum Vendedor Localizado"
                                AutoGenerateColumns="False" runat="server" EnableModelValidation="True" AllowPaging="True"
                                OnPageIndexChanging="ListaVendedorGridView_PageIndexChanged" PageSize="10" 
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                style="border-collapse:collapse;">

                                <PagerStyle CssClass="pagination-ys" />
                                
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Sel.">
                                        <ItemTemplate>
                                            <center>
                                                <asp:RadioButton ID="SelecionarRadioButton" runat="server" AutoPostBack="True" OnCheckedChanged="SelecionarCheckedChanged"  />
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="VendCodLabel" runat="server" Text='<%# Bind("VendCod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VendNome" HeaderText="Vendedor"></asp:BoundField>
                                    <asp:BoundField DataField="VendStat" HeaderText="Status"></asp:BoundField>
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
                
                
                --%>
                
                <br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />

            </div>
            <!----PAINEL----->
            <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
            
            <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
            <!--</div>-->
            <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
            <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
       





</asp:Content>
