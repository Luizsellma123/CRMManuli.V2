<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="ListaClienteSimuladorForm.aspx.cs" Inherits="VendasWeb.GerencialVendas.ListaClienteSimuladorForm" %>

<%@ Register Src="~/usercontrol/UCGerencialVendas.ascx" TagPrefix="uc1" TagName="UCGerencialVendas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <h3 class="panel-title">Gerencial Vendas - Lista Clientes Simulador Preço</h3>
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
                               <asp:Label ID="ClienteLabel" runat="server" Text="Cliente:"></asp:Label>
                             </div> 
                         </div>
                         <div class="col-sm-4">                           
                             <div class="form-group">
                                <asp:TextBox id="ClienteInput" runat="server" style="width:92%;"></asp:TextBox>
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
                        <asp:LinkButton ID="RetornarButton" class="btn btn-success btn-labeled fa fa-arrow-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>
                        <asp:LinkButton ID="BuscarButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" onclick="BuscarButton_Click">Buscar</asp:LinkButton>                             
                    </div>
                </div>
            </div>
        </div>
         <asp:MultiView ID="EntidadeMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="EntidadeView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Lista de clientes
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="EntidadeGridView" EmptyDataText="Não foi possível encontrar nenhum cliente" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="EntidadeGridView_PageIndexChanging"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Acessar" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="AcessarButton" class="btn btn-info fa fa-check-circle-o "
                                        CausesValidation="false" runat="server" onclick="AcessarButton_Click"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cliente " >
                                        <ItemTemplate>
                                            <asp:Label ID="EntidadeGrid" runat="server" text='<%#Eval("EntCod")+ " - " + Eval("EntNome")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Estado" >
                                        <ItemTemplate>
                                            <asp:Label ID="EstadoGrid" runat="server" text='<%#Eval("UfSigla")%>'></asp:Label>
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
    
    <uc1:UCGerencialVendas runat="server" id="UCGerencialVendas" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>

</asp:Content>
