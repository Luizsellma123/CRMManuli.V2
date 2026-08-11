<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmVendedorEntidade.aspx.cs" Inherits="VendasWeb.Entidades.FrmVendedorEntidade" %>
<%@ Register src="../usercontrol/ControlEntidade.ascx" tagname="ControlEntidade" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <link rel="stylesheet" type="text/css" href="../css/ListaPedido.css?aux=6" />
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="conteudo">

        <center><b><h3>Cadastro de Cliente - Vendedores</h3></b></center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
        <br />
        <asp:Label ID="VendedorLabel" runat="server" Text="Pesquisar Vendedor por:" CssClass="texto"></asp:Label>
        <asp:DropDownList ID="VendedorDropDownList" runat="server" CssClass="campo">
            <asp:ListItem Value="1" Selected="True">CÓDIGO</asp:ListItem>
            <asp:ListItem Value="2">NOME</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtFiltro" runat="server" CssClass="campo" Width="300px"></asp:TextBox>



        <asp:LinkButton ID="BuscarButton" class="btn btn-primary" runat="server"
            OnClick="btnListar_Click" title="Buscar Holding" data-rel="tooltip" CausesValidation="False">
                            <span class="glyphicon glyphicon-search" aria-hidden="true"> Buscar</span> 

        </asp:LinkButton>
        <asp:MultiView ID="VendedorMultView" runat="server" ActiveViewIndex="0">
            <asp:View ID="VendedorView" runat="server">
                <div>
                    Selecione abaixo qual vendedor deseja adicionar:<br />


                    <div style="overflow: auto; max-height: 150px;">
                        <asp:GridView ID="VendedorGridView" EmptyDataText="Nenhum Vendedor Localizado" AutoGenerateColumns="False" CssClass="lstTabela" Width="100%" runat="server">
                            <Columns>


                                <asp:TemplateField HeaderText="ADICIONAR" HeaderStyle-Width="10%">

                                    <ItemTemplate>
                                        <center>
                                                       <asp:LinkButton ID="LinkButton1" class="btn btn-success" runat="server" title="Novo Documento" 
        data-rel="tooltip" OnClick="btnAdiciona_Click"> <span class="glyphicon glyphicon-ok"
             aria-hidden="true"></span> </asp:LinkButton>
                                                       
                                                        </center>

                                    </ItemTemplate>

                                    <HeaderStyle CssClass="tabLstCab th" />
                                    <ItemStyle CssClass="text-align-left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="CÓDIGO" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="VendCodLabel" runat="server" Text='<%# Bind("VendCod") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="tabLstCab th" />
                                    <ItemStyle CssClass="text-align-center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="NOME">

                                    <ItemTemplate>
                                        <asp:Label ID="VendNomeLabel" runat="server" Text='<%# Bind("VendNome") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="tabLstCab th" Width="80%" />
                                    <ItemStyle CssClass="text-align-left" />
                                </asp:TemplateField>


                            </Columns>

                            <FooterStyle BackColor="#003300" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </div>


                </div>
            </asp:View>
        </asp:MultiView>


    </div>


    <br />
    Lista de Vendedores da Entidade:<br />
    <div style="overflow: auto; max-height: 150px;">
        <asp:GridView ID="VendEntGridView" EmptyDataText="Nenhum Vendedor Localizado" AutoGenerateColumns="False" CssClass="lstTabela" Width="100%" runat="server">
            <Columns>


                <asp:TemplateField HeaderText="PRINCIPAL" HeaderStyle-Width="10%">

                    <ItemTemplate>
                        <center>
                                                       
                                                       <asp:RadioButton id="VendCodRadioButton" runat="server" AutoPostBack="True"  Checked='<%# Bind("VendEntPrincBit") %>'  
                                                             OnCheckedChanged="SelecionarCheckedChanged" ValidationGroup="Vendedores"></asp:RadioButton>
                                                        </center>

                    </ItemTemplate>

                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-left" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="CÓDIGO" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="VendCodLabel" runat="server" Text='<%# Bind("VendCod") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NOME">

                    <ItemTemplate>
                        <asp:Label ID="VendNomeLabel" runat="server" Text='<%# Bind("VendNome") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" Width="80%" />
                    <ItemStyle CssClass="text-align-left" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Remover">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>




                        <center>
                            <asp:LinkButton ID="RemoverButton" class="btn btn-danger" runat="server"  CausesValidation="False"
                                                                OnClick="RemoverButton_Click"   data-rel="tooltip" >
                                                               <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span> 

                                                          </asp:LinkButton>

                                                        </center>




                    </ItemTemplate>

                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>

            </Columns>

            <FooterStyle BackColor="#003300" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>




    <br />


    <div>



        <asp:LinkButton ID="AlterarButton" class="btn btn-warning" runat="server" Visible="false"
            OnClick="AlterarButton_Click" title="Alterar" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> Alterar</span> 

        </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="PrincipalButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
             OnClick="PrincipalButton_Click" title="Principal" data-rel="tooltip">
                            <span class="glyphicon glyphicon-compressed" aria-hidden="true"> Principal </span> 

         </asp:LinkButton>

        &nbsp;<asp:LinkButton ID="ContatoButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="ContatoButton_Click" title="Contato" data-rel="tooltip">
                            <span class="glyphicon glyphicon-user" aria-hidden="true"> Contato</span> 

    </asp:LinkButton>

        &nbsp;<asp:LinkButton ID="EnderecoEntregaButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="EnderecoEntregaButton_Click" title="Endereços de Entrega" data-rel="tooltip">
                            <span class="glyphicon glyphicon-list" aria-hidden="true"> End. Entrega</span> 

    </asp:LinkButton>



        &nbsp;<asp:LinkButton ID="FiscalLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        title="Fiscal" data-rel="tooltip" OnClick="FiscalLinkButton_Click">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Fiscal</span> 

    </asp:LinkButton>



        &nbsp;<asp:LinkButton ID="PedidosLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="PedidosButton_Click" title="Pedidos" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> Pedidos</span> 

    </asp:LinkButton>



        &nbsp;
    






    </div>

    <br />
    <div>
    
    <asp:LinkButton ID="InformacoesButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="InformacoesButton_Click" title="Informações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-folder-open" aria-hidden="true"> Informações</span> 

    </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="AnexosButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="AnexosButton_Click" title="Anexos" data-rel="tooltip">
                            <span class="glyphicon glyphicon-paperclip" aria-hidden="true"> Anexos</span> 

    </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="ObservacoesButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="ObservacoesButton_Click" title="Observações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"> Observações</span> 

    </asp:LinkButton>



        &nbsp;<asp:LinkButton ID="HoldingLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="HoldingButton_Click" title="Holding" data-rel="tooltip">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Holding</span> 

    </asp:LinkButton>

        &nbsp;<asp:LinkButton ID="LogisticaLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="LogisticaButton_Click" title="Logistica" data-rel="tooltip">
                            <span class="glyphicon glyphicon-transfer" aria-hidden="true"> Logistica</span> 
        
    </asp:LinkButton>
         &nbsp;<asp:LinkButton ID="DuplicataLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="DuplicatasButton_Click" title="Duplicatas" data-rel="tooltip">
                            <span class="glyphicon glyphicon-paperclip" aria-hidden="true"> Duplicatas</span> 
        
    </asp:LinkButton>

                &nbsp;<asp:LinkButton ID="NotasLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="NotasButton_Click" title="Notas" data-rel="tooltip">
                            <span class="glyphicon glyphicon-book" aria-hidden="true"> Notas</span> 
        
    </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="AgendaLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="AgendaButton_Click" title="Agenda" data-rel="tooltip">
                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"> Agenda</span> 
        
    </asp:LinkButton>



                &nbsp;<asp:LinkButton ID="CRMLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="CrmButton_Click" title="CRM" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> CRM</span> 
        
    </asp:LinkButton>








    </div>
    <br />
    <div>

        <asp:LinkButton ID="CancelarOperacaoLinkButton" class="btn btn-danger" runat="server" Visible="false"
            OnClick="CancelarOperacaoButton_Click" title="Retornar a Lista de Entidade" CausesValidation="False" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Retornar</span> 

        </asp:LinkButton>



    </div>





    <br />

</asp:Content>
