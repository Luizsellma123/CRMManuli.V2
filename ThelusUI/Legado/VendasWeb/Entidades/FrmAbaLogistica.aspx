<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true"
    CodeBehind="FrmAbaLogistica.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaLogistica" %>

<%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <link rel="stylesheet" type="text/css" href="../css/ListaPedido.css?aux=6" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div class="conteudo">
        <center><b><h3>Cadastro de Cliente - Logistica</h3></b></center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
        <br />

        <div class="Logistica">

            <asp:Label ID="EntTransporteOMesmoLabel" runat="server" Text="Transportador é a própria entidade?"></asp:Label>

            <asp:DropDownList ID="EntTransporteOMesmoDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EntTransporteOMesmoDropDownList_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="Sim">SIM</asp:ListItem>
                <asp:ListItem Value="Não">NÃO</asp:ListItem>
            </asp:DropDownList>



            &nbsp;&nbsp;
            <asp:Label ID="EntStatFreteVendaLabel" runat="server" Text="Frete Venda:"></asp:Label>

            &nbsp;

            <asp:DropDownList ID="EntStatFreteVendaDropDownList" runat="server">
                <asp:ListItem Value="Emitente">EMITENTE</asp:ListItem>
                <asp:ListItem Value="Destinatário">DESTINATÁRIO</asp:ListItem>
            </asp:DropDownList>

            <br><br>
                
                    <asp:MultiView ID="TransportadorasMultView" runat="server" ActiveViewIndex="0">
                        <asp:View ID="TransportadorasView" runat="server">
                            <div>
                                <asp:Label ID="lblEntidade" runat="server" Text="Pesquisar por:" CssClass="texto"></asp:Label>
                                <asp:DropDownList ID="drpEntCod" runat="server" CssClass="campo">
                                    <asp:ListItem Value="1">NOME FANTASIA</asp:ListItem>
                                    <asp:ListItem Value="2">RAZÃO SOCIAL</asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True">CÓDIGO DA ENTIDADE</asp:ListItem>
                                    <asp:ListItem Value="4">CNPJ</asp:ListItem>
                                    <asp:ListItem Value="5">Cidade</asp:ListItem>
                                    <asp:ListItem Value="6">UF</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtFiltroEntCod" runat="server" CssClass="campo" Width="300px"></asp:TextBox>



                                <asp:LinkButton ID="BuscarButton" class="btn btn-primary" runat="server"
                                    OnClick="btnListar_Click" title="Buscar Transportadora" data-rel="tooltip" CausesValidation="False">
                            <span class="glyphicon glyphicon-search" aria-hidden="true"> Buscar</span> 

                                </asp:LinkButton>

                                <br />
                                <br />
                                <div style="overflow: auto; max-height: 150px;">
                                    <asp:GridView ID="ListaEntidadeGridView" AutoGenerateColumns="False" CssClass="lstTabela" Width="100%" runat="server">
                                        <Columns>

                                            
                                            <asp:TemplateField HeaderText="SELECIONAR">

                                                <ItemTemplate>
                                                    <center>
                                                       
                                                       <asp:RadioButton id="EntTranspCodEntRadioButton" runat="server" AutoPostBack="True" OnCheckedChanged="SelecionarCheckedChanged" ValidationGroup="entidades"></asp:RadioButton>
                                                        </center>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="CÓDIGO">
                                                <ItemTemplate>
                                                    <asp:Label ID="EntCodLabel" runat="server" Text='<%# Bind("EntCod") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-center" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="EntCpfCgc" HeaderText="CNPJ/CPF">
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="EntNome" HeaderText="NOME">
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="EntNomeFant" HeaderText="NOME FANTASIA">
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="EntEnder" HeaderText="ENDEREÇO">
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="EntEnderNo" HeaderText="Nº">
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="EntBair" HeaderText="BAIRRO">
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:BoundField>


                                            <asp:BoundField DataField="CidNome" HeaderText="CIDADE">
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:BoundField>


                                            <asp:BoundField DataField="UfSigla" HeaderText="ESTADO">
                                                <HeaderStyle CssClass="tabLstCab th" />
                                                <ItemStyle CssClass="text-align-left" />
                                            </asp:BoundField>


                                        </Columns>

                                        <FooterStyle BackColor="#003300" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </div>


                            </div>
                        </asp:View>
                    </asp:MultiView>
                

            <br>
            


            <asp:Label ID="EntTranspCodEntLabel" runat="server" Text="Código da Transportadora:" ></asp:Label>
            <asp:TextBox ID ="EntTranspCodTextBox" runat="server" Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" Display="Dynamic" SetFocusOnError="True"
                    ControlToValidate="EntTranspCodTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>


            &nbsp;&nbsp;
              <asp:Label ID="UserShelfLifeLabel" runat="server" Text="Shelf Life:" ></asp:Label>
            <asp:TextBox ID ="UserShelfLifeTextBox" runat="server" onkeypress="mascara( this, mnum );" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="True"
                    ControlToValidate="UserShelfLifeTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            
            
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
        OnClick="HoldingButton_Click" title="Observações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Holding</span> 

    </asp:LinkButton>


                 &nbsp;<asp:LinkButton ID="VendedorLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="VendedorButton_Click" title="Vendedor" data-rel="tooltip">
                            <span class="glyphicon glyphicon-user" aria-hidden="true"> Vendedor</span> 
        
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

        </div>

    </div>

</asp:Content>
