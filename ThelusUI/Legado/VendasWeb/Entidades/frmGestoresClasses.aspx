<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="frmGestoresClasses.aspx.cs" Inherits="VendasWeb.Entidades.frmGestoresClasses" %>
<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-9">
            <!--===================================================-->
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
                        <asp:Label ID="VendNomeLabel" runat="server" Text="" width="40px"></asp:Label></h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha do vendedor-->
                <div id="painel_aberto" class="">
                    <div class="panel-body">
                        <!--LINHA 1 - Painel Aberto-->
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblClasseVendedor" runat="server" CssClass="text-thin" Text="Classe Vendedor:" Width="130"></asp:Label>
                                    <asp:DropDownList ID="ClasseVendedorDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="True"
                                        ControlToValidate="ClasseVendedorDropDownList" ErrorMessage="Selecione uma classe de vendedor!"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <!--END LINHA 1 - Painel Aberto-->
                        <!--===================================================-->
                    </div>
                </div>
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
           </div>     
                <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>

            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Salvar" data-rel="tooltip" OnClick="SalvarLinkButton_Click"> 
                            Salvar </asp:LinkButton>
                    </div>
                </div>
            </div>
        <!--===================================================-->
        <!--End Painel Carteiras e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="ClasseVendedoresMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ClasseVendedoresView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Classe Vendedores
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="ClasseVendedorGridView" EmptyDataText="Nenhuma classe de vendedor Localizada"
                                AutoGenerateColumns="False" runat="server" EnableModelValidation="True" AllowPaging="True"
                                OnPageIndexChanging="ClasseVendedorGridView_PageIndexChanged" PageSize="10" 
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                style="border-collapse:collapse;">

                                <PagerStyle CssClass="pagination-ys" />
                                                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoLabel" Text='<%# Bind("ID_User_TB_GestoresClasses") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="tabLstCab th" />
                                        <ItemStyle CssClass="text-align-center" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="VendClasseCod" HeaderText="Classe"></asp:BoundField>
                                    <asp:BoundField DataField="UsuCod" HeaderText="Usuário"></asp:BoundField>

                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ExcluirButton" runat="server" 
                                                ImageUrl="~/imagens/delete.png" onclick="ExcluirButton_Click" 
                                                CausesValidation="False" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="tabLstCab th" />
                                        <ItemStyle CssClass="text-align-center" />
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

   
   <!----PAINEL----->
   <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
   
       
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
