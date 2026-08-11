<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmAbaFinanceiro.aspx.cs" Inherits="VendasWeb.Entidade.FrmAbaFinanceiro" %>


    <%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    

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
                        <!--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>-->
                    </div>
                    <h3 class="panel-title">
                        Financeiro</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel -->
                <!--===================================================-->
                <div id="filtros" class="collapse in" runat="server">
                    <div class="panel-body">
                        
                              
                            <div class="row">
                            <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
                        <br />
                        </div>
                                    <div class="row">
                                <h5><asp:Label ID="Label5" runat="server" Text="Valor Limite de Crédito :" CssClass="text-thin" Width="170px"></asp:Label></h5>
                                <asp:TextBox ID="EntValLimCredTextBox" onkeypress="mascara( this, mvalor );" CssClass="form-control" Width="450px" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="EntValLimCredTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <br />
                                    </div>                                                  

                                    <div class="row">
                                <h5><asp:Label ID="UsuSintegraLabel" runat="server" Text="Sintegra:" CssClass="text-thin" Width="170px" Visible="false"></asp:Label></h5>
                                <asp:DropDownList ID="UsuSintegraDropDownList" runat="server" CssClass="form-control" Width="450px" Visible="false">
                                    <asp:ListItem Value="SIM" Selected="True">SIM</asp:ListItem>
                                    <asp:ListItem Value="NÃO">NÃO</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                </div>

                                <div class="row">
                                <h5><asp:Label ID="CategoriaSecundariaLabel" runat="server" Text="Categorias(CNAE) :" CssClass="text-thin" Width="170px"></asp:Label></h5>
                                

                                <select class="selectpicker show-tick" title="Escolha um CNAE..." data-style="btn-primary" Width="450px"
                                          data-live-search="true" id="CategoriaSecundariaDropDownList" runat="server">
                                   </select>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="CategoriaSecundariaDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <br />

                                    </div>
                                
                                <div class="row">
                                  <asp:LinkButton ID="AdicionarCategoriaButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg" CausesValidation="false"
                                    runat="server" title="Adicionar" data-rel="tooltip" OnClick="AdicionarCategoriaButton_Click"> Adicionar Categoria </asp:LinkButton>

                                    <br />
                                    </div>

                                <div class="row">

                                <asp:GridView ID="CategoriaGridView" runat="server" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%"
                                    AutoGenerateColumns="False">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cód. Entidade" Visible="false">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="EntCodLabel" Text='<%# Bind("EntCod") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Codigo" Visible="false">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="CodigoLabel" Text='<%# Bind("Codigo") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cód. Categoria" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CategCodEstrLabel" Text='<%# Bind("Categcodestr") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="CNAE">
                                            <ItemTemplate>
                                                <asp:Label ID="CategoriaLabel" Text='<%# Bind("Categoria") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Remover">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <center>
                                                    
                                                    <asp:LinkButton ID="RemoverCategoriaButton" class="btn btn-danger fa fa-trash fa-lg" 
                                    runat="server" CausesValidation="False" OnClick="RemoverCategoriaButton_Click" title="Remover"
                                    data-rel="tooltip"></asp:LinkButton>


                                                </center>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </div>
                                <!------------------------------------------------------------------->

                                <br />
                                <div class="row">
                                <h5><asp:Label Text="Condição de Pagamento:" runat="server" ID="CondicaoPagamentoLabel" CssClass="text-thin" Width="170px"></asp:Label></h5>
                                <asp:DropDownList ID="CondicaoPagamentoDropDownList" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="CondicaoPagamentoDropDownList_SelectedIndexChanged"
                                    CssClass="form-control" Width="450px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="CondicaoPagamentoDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator><br />

                                    </div>

                                    <div class="row">
                                <h5><asp:Label ID="OutraCondPagLabel" Text="Qual?" runat="server" Visible="false" CssClass="text-thin" Width="170px"></asp:Label></h5>
                                <asp:TextBox ID="OutraCondPagTextBox" runat="server" Visible="false" CssClass="form-control" Width="450px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="OutraCondPagTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>


                                      <br />
                                      </div>

                                      <div class="row">
                                
                                  <asp:LinkButton ID="AdicionarCondPagLinkButton" class="btn btn-success btn-labeled fa fa-plus-circle fa-lg" CausesValidation="false"
                                    runat="server" title="Adicionar" data-rel="tooltip" OnClick="AdicionarCondPagLinkButton_Click"> Adicionar Cond. Pag. </asp:LinkButton>

                                     


                                    
                                    </div>

                               <div class="row">

                                <asp:GridView ID="CondPagGridView" runat="server" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%"
                                    AutoGenerateColumns="False">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cód. Entidade" Visible="false">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="EntCodLabel" Text='<%# Bind("EntCod") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Codigo" Visible="false">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="CodigoLabel" Text='<%# Bind("Codigo") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cód. Cond. Pag." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="CondPagCodLabel" Text='<%# Bind("CondPagCod") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Cond. Pag.">
                                            <ItemTemplate>
                                                <asp:Label ID="CondicaoLabel" Text='<%# Bind("Condicao") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Remover">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <center>
                                                    
                                                    <asp:LinkButton ID="RemoverCondPagButton" class="btn btn-danger fa fa-trash fa-lg" 
                                    runat="server" CausesValidation="False" OnClick="RemoverCondPagButton_Click" title="Remover"
                                    data-rel="tooltip"></asp:LinkButton>

                                                </center>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                                </div>

                                <br />

                                <div class="row">
                                <h5><asp:Label ID="TipoDeCobrancaLabel" runat="server" Text="Tipo de Cobrança:" CssClass="text-thin" Width="170px"></asp:Label></h5>
                                <asp:DropDownList ID="TipoCobCodDropDownList" runat="server" CssClass="form-control" Width="450px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="TipoCobCodDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <br />
                                    </div>


                                    <div class="row">
                                       <h5><asp:Label ID="UsuCartaoCNPJLabel" runat="server" Text="Cartão CNPJ:" CssClass="text-thin" Width="170px" Visible= "false"></asp:Label></h5>
                                <asp:DropDownList ID="UsuCartaoCNPJDropDownList" runat="server" CssClass="form-control" Width="450px" Visible= "false">
                                    <asp:ListItem Value="SIM" Selected="True">SIM</asp:ListItem>
                                    <asp:ListItem Value="NÃO">NÃO</asp:ListItem>
                                </asp:DropDownList>

                                      </div>       
                            
                        
                    </div>
                </div>
                <!-- END Painel-->
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">
                          <asp:LinkButton ID="VoltarLinkButton" class="btn btn-warning btn-labeled fa fa-arrow-circle-left fa-lg" CausesValidation="false"
                                    runat="server" title="Voltar" data-rel="tooltip" OnClick="VoltarButton_Click"
                                    > 
             Retornar </asp:LinkButton>

                        <asp:LinkButton ID="ProximoPassoButton" class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg" CausesValidation="false"
                                    runat="server" title="Próxima Tela" data-rel="tooltip" OnClick="ProximoPasso_Click"
                                    > 
             Próximo </asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
            <!--===================================================-->
            <!--End Painel-->
            <!--===================================================-->
        </div>
        <!----PAINEL----->
        <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    </div>
</asp:Content>
