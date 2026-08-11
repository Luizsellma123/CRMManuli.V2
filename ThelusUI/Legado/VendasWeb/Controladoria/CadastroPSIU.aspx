<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="CadastroPSIU.aspx.cs" Inherits="VendasWeb.Controladoria.CadastroPSIU" %>
<%@ Register Src="~/usercontrol/WebUserControlControladoria.ascx" TagPrefix="uc1" TagName="WebUserControlControladoria" %>


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
                    <h3 class="panel-title">Cadastro PSIU</h3>
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
                               <asp:Label ID="LabelPeriodo" runat="server" Text="Período :"></asp:Label>
                               </div>
                        </div>
                        
                        <div class="col-sm-2">
                            <div class="form-group">
                                    <div class="input-daterange input-group" id="DivData1">                                                                     
                                    <asp:TextBox class="form-control" ID="DateTextbox" textmode="date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                            </div>

                        <div class="col-sm-2"></div>

                        <div class="col-sm-1">
                            <div class="form-group">
                               <asp:Label ID="LabelAte" runat="server" Text="Até:"></asp:Label>
                               </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                    <div class="input-daterange input-group" id="DivData2">                                                                     
                                    <asp:TextBox class="form-control" ID="DateUntillTextbox" textmode="date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                
                    </div>
                        </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                               <asp:Label ID="NomeLabel" runat="server" Text="Nome do arquivo :"></asp:Label>
                               </div>
                        </div>
                        
                        <div class="col-sm-2">
                            <div class="form-group">
                                <div class="form-group">                                     
                                    <input runat="server" id="NomeArquivoText" class="form-control" style="width:153px" type="text"/></div>                                                                     
                            </div>
                        </div>

                        <div class="col-sm-2"></div>

                        <div class="col-sm-1">
                            <div class="form-group">
                               <asp:Label ID="Label2" runat="server" Text="Arquivo:"></asp:Label>
                               </div>
                        </div>
                        <div class="col-sm-1">
                            <div class="form-group">
                            <asp:FileUpload ID="DocumentoFileUpload" class="input-file uniform_on" runat="server" />
                            </div>
                
                    </div>

                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
                </div>
            </div>
            <div class="panel-footer">
                <div class="row">

                    <div class="panel-control">
                        <asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                            CausesValidation="false" runat="server" OnClick="GravarButton_Click">Gravar</asp:LinkButton>
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
                                    <asp:TemplateField HeaderText="Excluir" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="DeleteButton" class="btn btn-danger fa fa-times fa-lg"
                                        CausesValidation="false" runat="server" OnClick="DeleteButton_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data " ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="DataLabel" runat="server" Text='<%# Bind("Data") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome " ItemStyle-Width="55%">
                                        <ItemTemplate>                                            
                                        <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("NomeDocumento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Baixar" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="BaixarButton" class="btn btn-success fa fa-download fa-lg"
                                        CausesValidation="false" runat="server" OnClick="BaixarButton_Click"></asp:LinkButton>                                        </ItemTemplate>
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
        <uc1:WebUserControlControladoria runat="server" ID="WebUserControlControladoria" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->





</asp:Content>

