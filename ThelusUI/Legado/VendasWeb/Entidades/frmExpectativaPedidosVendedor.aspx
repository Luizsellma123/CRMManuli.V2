<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="frmExpectativaPedidosVendedor.aspx.cs" Inherits="VendasWeb.Entidades.frmExpectativaPedidosVendedor" %>
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
                        <asp:Label ID="VendNomeLabel" runat="server" Text="" width="40px"></asp:Label>
                    </h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha do vendedor-->
                <div id="painel_aberto" class="">
                    <div class="panel-body">
                        <!--LINHA 1 - Painel Aberto-->
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">

                                    <asp:Label ID="lblAno" runat="server" CssClass="text-thin" Text="Ano:" Width="130"></asp:Label>
                                    <asp:DropDownList ID="AnoDropDownList" runat="server" CssClass="selectpicker show-tick" Width="60px" >
                                        <asp:ListItem Value="2016" Selected="True">2016</asp:ListItem>
                                        <asp:ListItem Value="2017">2017</asp:ListItem>
                                        <asp:ListItem Value="2018">2018</asp:ListItem>
                                        <asp:ListItem Value="2019">2019</asp:ListItem>
                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                    </asp:DropDownList>
                                    <br /><br />
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
                        <asp:LinkButton ID="ListarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Listar" data-rel="tooltip" OnClick="ListarLinkButton_Click"> 
                            Listar </asp:LinkButton>
                    </div>
                </div>
            </div>
        <!--===================================================-->
        <!--End Painel Carteiras e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="ExpectativaMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="ExpectativaView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Expectativas
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="ListaExpectativaGridView" EmptyDataText="Nenhuma Expectativa Localizada"
                                AutoGenerateColumns="False" runat="server" EnableModelValidation="True" AllowPaging="True"
                                OnPageIndexChanging="ListaExpectativaGridView_PageIndexChanged" PageSize="10" 
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                style="border-collapse:collapse;">

                                <PagerStyle CssClass="pagination-ys" />
                                
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoLabel" Text='<%# Bind("Codigo") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="tabLstCab th" />
                                        <ItemStyle CssClass="text-align-center" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="UserLinhaProdutoLista" HeaderText="Linha"></asp:BoundField>

                                    <asp:TemplateField HeaderText="Janeiro" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdJaneiroTextBox" runat="server" Text='<%# Bind("QtdJaneiro") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fevereiro" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdFevereiroTextBox" runat="server" Text='<%# Bind("QtdFevereiro") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Março" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdMarcoTextBox" runat="server" Text='<%# Bind("QtdMarco") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Abril" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdAbrilTextBox" runat="server" Text='<%# Bind("QtdAbril") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Maio" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdMaioTextBox" runat="server" Text='<%# Bind("QtdMaio") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Junho" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdJunhoTextBox" runat="server" Text='<%# Bind("QtdJunho") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Julho" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdJulhoTextBox" runat="server" Text='<%# Bind("QtdJulho") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Agosto" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdAgostoTextBox" runat="server" Text='<%# Bind("QtdAgosto") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Setembro" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdSetembroTextBox" runat="server" Text='<%# Bind("QtdSetembro") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Outubro" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdOutubroTextBox" runat="server" Text='<%# Bind("QtdOutubro") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Novembro" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdNovembroTextBox" runat="server" Text='<%# Bind("QtdNovembro") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dezembro" ControlStyle-CssClass="GridItem" HeaderStyle-CssClass="GridHeader">
                                        <ItemTemplate>
                                            <asp:TextBox ID="QtdDezembroTextBox" runat="server" Text='<%# Bind("QtdDezembro") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="GridItem"></ControlStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Alterar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="AlterarLinkButton" runat="server" 
                                                ImageUrl="~/imagens/edit.png" onclick="AlterarLinkButton_Click" 
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
