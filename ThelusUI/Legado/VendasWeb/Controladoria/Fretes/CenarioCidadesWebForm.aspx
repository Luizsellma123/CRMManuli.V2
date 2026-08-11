<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="CenarioCidadesWebForm.aspx.cs" Inherits="VendasWeb.Controladoria.Fretes.CenarioCidadesWebForm" %>
<%@ Register Src="~/usercontrol/WebUserControlControladoria.ascx" TagPrefix="uc1" TagName="WebUserControlControladoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <h3 class="panel-title">Novo Cenário de Frete por Cidade</h3>
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
                        <div class="col-sm-2">
                            <div class="form-group">
                               <asp:Label ID="CenarioLabel" runat="server" Text="Nome Cenário:"></asp:Label>
                               </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="CenarioTextbox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>    
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-3">
                            <asp:CheckBox ID="PadraoCheck" runat="server"/>
                            <asp:Label Text="Definir como padrão" ID="PadraoLabel" runat="server" style="position:relative; bottom: 2px;"></asp:Label>
                        </div>
                        </div>
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:Label Text="Planilha: " runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                            <asp:FileUpload ID="DocumentoFileUpload" class="input-file uniform_on" runat="server" />
                            </div>
                        </div>                            
                      </div>
                    </div>
                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="RetornarButton" class="btn btn-success btn-labeled fa fa-arrow-left fa-lg"
                            CausesValidation="false" runat="server" onclick="RetornarButton_Click">Retornar</asp:LinkButton>
                        <asp:LinkButton ID="PadraoButton" class="btn btn-success btn-labeled fa fa-download fa-lg"
                            CausesValidation="false" runat="server" onclick="PadraoButton_Click">Planilha Padrão</asp:LinkButton>   
                        <asp:LinkButton ID="PlanilhaButton" class="btn btn-success btn-labeled fa fa-file-excel-o fa-lg"
                            CausesValidation="false" runat="server" OnClick="PlanilhaButton_Click">Carregar Planilha</asp:LinkButton>
                        <asp:LinkButton ID="BancoButton" class="btn btn-success btn-labeled fa fa-database fa-lg"
                            CausesValidation="false" runat="server" OnClick="BancoButton_Click">Salvar Banco</asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>
</div>
   <asp:MultiView ID="FreteMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="FreteView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                       
                            <asp:GridView ID="FreteGridView" AutoGenerateColumns="false"
                                runat="server" AllowPaging="True"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="FreteGridView_PageIndexChanged">
                                <PagerStyle CssClass="pagination-ys" />                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Empresa" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                        <asp:Label ID="EmpresaGrid" runat="server" Text='<%# Bind("Empresa") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cidade" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                         <asp:Label ID="CidadeGrid" runat="server" Text='<%# Bind("Cidade") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Valor Frete" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                         <asp:Label ID="FreteGrid" runat="server" Text='<%# Bind("ValorFrete") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               </Columns>
                            </asp:GridView>  
                            </div>
                            </div>
                <!-- End Foo Table - Filtering -->
                            </div>
                <!--===================================================-->
                <!-- END TABELA -->
            </asp:View>
        </asp:MultiView>  

    </div>
        <uc1:WebUserControlControladoria runat="server" ID="WebUserControlControladoria" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->

</asp:Content>
