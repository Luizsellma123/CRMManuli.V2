<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="frmAbaPerfilDeConsumo.aspx.cs" Inherits="VendasWeb.Entidade.frmAbaPerfilDeConsumo" %>

<%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>
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
                        Perfil de Consumo</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel -->
                <!--===================================================-->
                <div class="table-responsive">
                    <div class="panel-body">
                        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
                        <br />
                        <asp:MultiView ID="FormularioMultView" runat="server" ActiveViewIndex="0" Visible="false">
                            <asp:View ID="FormularioView" runat="server">
                                <h5>
                                    <asp:Label ID="LinhaLabel" runat="server" Text="Linha:" CssClass="text-thin"></asp:Label></h5>
                                <asp:DropDownList ID="LinhaDropDownList" runat="server" CssClass="form-control" Width="350px">
                                    <asp:ListItem Value="Stretch">Stretch</asp:ListItem>
                                    <asp:ListItem Value="Fita PP">Fita PP</asp:ListItem>
                                    <asp:ListItem Value="Fita Impressa">Fita Impressa</asp:ListItem>
                                    <asp:ListItem Value="Máquinas">Máquinas</asp:ListItem>
                                    <asp:ListItem Value=" " Selected="True">Selecione</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="LinhaDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator><br />
                                <h5>
                                    <asp:Label ID="QuantidadeLabel" runat="server" CssClass="text-thin" Text="Quantidade:"></asp:Label></h5>
                                <asp:TextBox ID="QuantidadeTextBox" onkeypress="mascara( this, mvalor );" CssClass="form-control"
                                    runat="server" Width="350" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="QuantidadeTextBox" ErrorMessage="*"></asp:RequiredFieldValidator><br />
                                <h5>
                                    <asp:Label ID="DescricaoLabel" runat="server" Text="Descrição:" CssClass="text-thin"></asp:Label></h5>
                                <asp:TextBox ID="DescricaoTextBox" runat="server" class="form-control" TextMode="MultiLine"
                                    Width="700px" Height="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DescricaoTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <br />
                            </asp:View>
                        </asp:MultiView>

                        

                        <br />


                                <asp:LinkButton ID="NovoButton" class="btn btn-btn-primary btn-labeled fa fa-plus fa-lg" CausesValidation="false"
                                    runat="server" OnClick="NovoPerfil_Click" title="Novo Perfil" data-rel="tooltip">
                                                    Novo Perfil

                                </asp:LinkButton>
                                

                                <asp:LinkButton ID="AdicionarButton" class="btn btn-success btn-labeled fa fa-plus fa-lg"  Visible="false"
                                    runat="server" OnClick="AdicionarButton_Click" title="Adicionar" data-rel="tooltip">
                                                    Adicionar

                                </asp:LinkButton>

                                
                                 &nbsp;
                                <asp:LinkButton ID="CancelarButton" class="btn btn-danger btn-labeled fa fa-remove fa-lg" Visible="false"
                                    runat="server" CausesValidation="False" OnClick="CancelarButton_Click" title="Cancelar"
                                    data-rel="tooltip">
                                                    Cancelar

                                </asp:LinkButton>



                                <br />
                                <br />



                        <asp:GridView ID="PerfilGridView" runat="server" 
                        EmptyDataText="Nenhum Perfil de Consumo Cadastrado."
                        CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                            Style="border-collapse: collapse; max-width: 100%" AutoGenerateColumns="False">
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
                                <asp:TemplateField HeaderText="Linha Produto">
                                    <ItemTemplate>
                                        <asp:Label ID="LinhaProdLabel" Text='<%# Bind("Linha") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantidade">
                                    <ItemTemplate>
                                        <asp:Label ID="QtdeLabel" Text='<%# Bind("Quantidade") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descrição">
                                    <ItemTemplate>
                                        <asp:Label ID="DescLabel" Text='<%# Bind("Descricao") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remover">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <center>
                                            
                                            
    <asp:LinkButton ID="RemoverButton" class="btn btn-danger fa fa-trash fa-lg" 
                                    runat="server" CausesValidation="False" OnClick="RemoverButton_Click" title="Remover"
                                    data-rel="tooltip"></asp:LinkButton>
									

                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <!------------------------------------------------------------------->
                        <div id="demo-dp-component">
                            <!-- <small class="text-muted">Agende um lembrete</small> -->
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
                            <asp:LinkButton ID="VoltarLinkButton" class="btn btn-warning btn-labeled fa fa-arrow-circle-left fa-lg"
                                CausesValidation="false" runat="server" title="Voltar" data-rel="tooltip" OnClick="VoltarButton_Click"> 
             Retornar </asp:LinkButton>
                            <asp:LinkButton ID="ProximoPassoButton" class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg"
                                CausesValidation="False" runat="server" title="Próxima Tela" data-rel="tooltip"
                                OnClick="ProximoPassoButton_Click"> 
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
