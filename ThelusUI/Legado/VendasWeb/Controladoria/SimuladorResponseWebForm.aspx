<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="SimuladorResponseWebForm.aspx.cs" Inherits="VendasWeb.Controladoria.SimuladorParametros.SimuladorResponseWebForm" %>
<%@ Register Src="~/usercontrol/WebUserControlControladoria.ascx" TagPrefix="uc1" TagName="WebUserControlControladoria" %>

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
                    <h3 class="panel-title">Manuli - Parâmetros Simulador</h3>
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
                               <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa :"></asp:Label>
                               </div>
                        </div>
                        <div class="col-md-4 col-sm-2">
                            <div class="form-group">
                                <input ID="EmpresaInput" runat="server" type="text" readonly="readonly" class="form-control"/>
                               </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                               <asp:Label ID="alcadaLabel" runat="server" Text="Alçada :"></asp:Label>
                               </div>
                        </div>
                        <div class="col-md-4 col-sm-2">
                            <div class="form-group">
                                <input ID="AlcadaInput" runat="server" type="text" readonly="readonly" class="form-control"/>
                               </div>
                        </div>
                    </div>                        
                <!--FIM DO CORPO DO PAINEL-->
            </div>
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="RetornarButton" class="btn btn-success btn-labeled fa fa-angle-double-left fa-lg"
                            CausesValidation="false" runat="server" onclick="RetornarButton_Click">Retornar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
      </div>
        <asp:MultiView ID="SimuladorMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="SimuladorView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                       
                            <asp:GridView ID="SimuladorGridView" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />                               
                                <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                        <asp:Label ID="IDGrid" runat="server" Text='<%# Bind("IDParametros") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Linha" ItemStyle-Width="85%">
                                        <ItemTemplate>
                                        <asp:Label ID="CodigoGrid" runat="server" Text='<%# Bind("Linha") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Percentual" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox cssclass="form-control" AutoPostBack="true" ID="PercentBox" runat="server" ontextchanged="PercentBox_TextChanged" Text='<%#Bind("Percentual")%>'></asp:TextBox>                                            
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
        <uc1:webusercontrolcontroladoria runat="server" ID="WebUserControlControladoria" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->


</asp:Content>
