<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmHistoricoPSIU.aspx.cs" Inherits="VendasWeb.FrmHistoricoPSIU" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                    <h3 class="panel-title">Histórico PSIU</h3>
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
                        <div class="col-md-1 col-sm-6">
                            <div class="form-group">
                               <asp:Label cssClass="form-control" ID="NomeLabel" runat="server" Text="Nome:"></asp:Label>
                               </div>
                        </div>

                        <div class="col-md-2 col-sm-6">
                            <div class="form-group">
                                <div class="form-group">                                     
                                    <input runat="server" id="NomeArquivoText" type="text"/></div>                                                                     
                            </div>
                        </div>                        
                        <div class="col-md-1 col-sm-12"></div>
                        <div class="col-md-1 col-sm-6">
                            <div class="form-group">
                               <asp:Label ID="LabelPeriodo" runat="server" Text="Período:"></asp:Label>
                               </div>
                        </div>
                        
                        <div class="col-md-2 col-sm-6">
                            <div class="form-group">
                                    <div class="input-daterange input-group" id="DivData1">                                                                     
                                    <asp:TextBox class="form-control" ID="DateTextbox" textmode="date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                            </div>


                        <div class="col-md-1 col-sm-0"></div>
                        <div class="col-md-1 col-sm-6">
                            <div class="form-group">
                               <asp:Label ID="LabelAte" runat="server" Text="Até:"></asp:Label>
                               </div>
                        </div>
                        <div class="col-md-1 col-sm-6">
                            <div class="form-group">
                                    <div class="input-daterange input-group" id="DivData2">                                                                     
                                    <asp:TextBox class="form-control" ID="DateUntillTextbox" textmode="date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                                                    <div class="col-sm-1"></div>

                
                    </div>                        
                        </div>
                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->

            </div>
            <div class="panel-footer">
                <div class="row">

                    <div class="panel-control">
                        <asp:LinkButton ID="BuscarButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>

         </div>
    <asp:MultiView ID="PSIUMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="PSIUView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                       
                            <asp:GridView ID="PSIUGridView" EmptyDataText="Não foram encontrados documentos com esses filtros" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="PSIUGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />                               
                                <Columns>

                                    <asp:TemplateField HeaderText="Data " ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="DataLabel" runat="server" Text='<%# Bind("Data") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome " ItemStyle-Width="50%">
                                        <ItemTemplate>                                            
                                        <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("NomeDocumento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Baixar" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                        <center><asp:LinkButton ID="BaixarButton" class="btn btn-success fa fa-download fa-lg"
                                        CausesValidation="false" runat="server" OnClick="BaixarButton_Click"></asp:LinkButton></center>                                      </ItemTemplate>
                                    </asp:TemplateField>

                <asp:TemplateField HeaderText="Url" InsertVisible="False" SortExpression="Url" Visible="False">

                    <ItemTemplate>
                        <asp:Label ID="UrlLabel" runat="server" Text='<%# Bind("Endereco") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                    </asp:TemplateField>

                <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="Url" Visible="False">

                    <ItemTemplate>
                        <asp:Label ID="IDLabel" runat="server" Text='<%# Bind("IDDocumento") %>'></asp:Label>
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
    </div>


</asp:Content>
