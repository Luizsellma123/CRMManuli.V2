<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="DashboardSupervisor.aspx.cs" Inherits="VendasWeb.Dashboard.DashboardSupervisor" %>
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
    
        <div class="col-sm-12">
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
                         DashBoard Manuli Fitasa - Supervisor </h3>
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
                        
                       <div class="col-sm-1">
                            <div class="form-group">
                              <asp:Label ID="SupervisorLabel" runat="server" Text="Supervisor:">
                              </asp:Label>
                            </div>
                        </div>

                        <div class="col-lg-3">
                                <asp:MultiView ID="SupervisorMultView" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="SupervisorView" runat="server">
                                        <div class="col-lg-5">
                                            <select class="selectpicker show-tick" multiple data-placeholder="Escolha um supervisor..."
                                                title="Escolha um supervisor..." data-style="btn-primary" data-live-search="true"
                                                id="SupervisorSelect" runat="server">
                                            </select>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        
                    </div>
                      <div class="row">
                       
                        <div class="col-sm-2">   
                            
                            <div class="form-group">
                               <asp:Label ID="InicalPeriodoLabel" runat="server" Text="Inicial :"></asp:Label>     
                            </div>
                            
                        </div>

                    
                   <div class="col-sm-2">   
                        <div class="form-group">
                            <div class="input-daterange input-group" id="Div1">
                                <asp:TextBox ID="TextBoxDataInicial" runat="server" CssClass="form-control" TextMode="Date" ></asp:TextBox>

                                <div id="demo-dp-component">
					                        <!-- <small class="text-muted">Agende o próximo evento</small> -->
				                 </div>

                            </div>
                        </div>
                    </div>

                                  
                        <div class="col-sm-2">   
                            <div class="form-group">
                               <asp:Label ID="PeridoFinalLabel"  runat="server" Text="Final:" ></asp:Label>
                            </div>
                        </div>

                   <div class="col-sm-2">   
                        <div class="form-group">
                            <div class="input-daterange input-group" id="Div2" >
                                <asp:TextBox ID="TextBoxDataFinal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                <div id="demo-dp-component">
					                        <!-- <small class="text-muted">Agende o próximo evento</small> -->
				                </div> 
                             </div>  
                        </div>
                    </div>

                </div>
                            
                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS--> 
                </div>
            



            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="LinkButtonClasses" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="LinkButtonClasses_Click" >Classes</asp:LinkButton>

                        <asp:LinkButton ID="GerarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="GerarLinkButton_Click">Vendedor</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonSupervisor" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="LinkButtonSupervisor_Click">Supervisor</asp:LinkButton>
                        
                        <asp:LinkButton ID="LinkButtonRegional" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="LinkButtonRegional_Click">Regional</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
  
   
    <asp:MultiView ID="AnalisesMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="AnaliseView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Lista de Analise
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                       

                        <asp:GridView ID="AnaliseGridView" EmptyDataText="Nenhuma Analise Localizada" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="AnaliseGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; text-align: right;">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    
                                     <asp:TemplateField HeaderText="Linhas" ItemStyle-HorizontalAlign="Left">
                                         <ItemTemplate>
                                             <asp:Label ID="LinhaProdutoLabel" runat="server" Text='<%# Bind("LinhaProduto") %>'></asp:Label>
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Faturamento $">
                                        <ItemTemplate>
                                           <asp:Label ID="FaturamentoLabel" runat="server" Text='<%# Bind("Faturamento","{0:C2}") %>'></asp:Label>  
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                
                                
                                      <asp:TemplateField HeaderText="Faturamento">
                                        <ItemTemplate>
                                          <asp:Label ID="FaturamentoLabel" runat="server" Text='<%# Bind("FaturamentoQuantidade","{0:N2}") %>'></asp:Label> 
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Percentual">
                                        <ItemTemplate>
                                          <asp:Label ID="FaturamentoPercentualLabel" runat="server" Text='<%# Bind("FaturamentoPercentual","{0:N2}") %>'></asp:Label>   
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pendentes" >
                                        <ItemTemplate>
                                           <asp:Label ID="PendentesQuantidadeLabel" runat="server" Text='<%# Bind("PendentesQuantidade","{0:N2}") %>'></asp:Label>  
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                   
                                     <asp:TemplateField HeaderText="Percentual" >

                                        <ItemTemplate>
                                           <asp:Label ID="PendentesPercentualLabel" runat="server" Text='<%# Bind("PendentesPercentual","{0:N2}") %>'></asp:Label>  
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Devoluções" >

                                        <ItemTemplate>
                                         <asp:Label ID="DevolucoesQuantidadeLabel" runat="server" Text='<%# Bind("DevolucoesQuantidade","{0:N2}") %>'></asp:Label>  
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Percentual" >

                                        <ItemTemplate>
                                          <asp:Label ID="DevolucoesPercentualLabel" runat="server" Text='<%# Bind("DevolucoesPercentual","{0:N2}") %>'></asp:Label>   
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Meta" >

                                        <ItemTemplate>
                                           <asp:Label ID="MetaExpectativaLabel" runat="server" Text='<%# Bind("Expectativa","{0:N2}") %>'></asp:Label>  
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Fat+Pend-Dev" >

                                        <ItemTemplate>
                                        <asp:Label ID="FaturadosPendDevLabel" runat="server" Text='<%# Bind("FaturadosPendDev","{0:N2}") %>'></asp:Label>     
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                     
                                        <asp:TemplateField HeaderText="Performance" >

                                        <ItemTemplate>
                                          <asp:Label ID="PerformanceLabel" runat="server" Text='<%# Bind("Performance","{0:N2}") %>'></asp:Label>   
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
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
