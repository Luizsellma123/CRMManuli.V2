<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true"
    CodeBehind="FrmAbaContatos.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaContatos" %>

<%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <link rel="stylesheet" type="text/css" href="../css/ListaPedido.css?aux=6" />
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="conteudo">
        <center>
            <b>
                <h3>
                    Cadastro de Cliente - Contatos</h3>
            </b>
        </center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
        <br />
        <asp:Literal ID="ContatoLiteral" runat="server" Visible="true"></asp:Literal>
        <br />
        <br />
        <asp:MultiView ID="DadosContatoMultView" runat="server" ActiveViewIndex="0">
            <asp:View ID="DadosContatoView" runat="server">
                <asp:Label ID="TipoContatoLabel" runat="server" Text="Tipo Contato:"></asp:Label>
                <asp:DropDownList ID="TipoContatoDropDownList" AutoPostBack="true" runat="server"
                    OnSelectedIndexChanged="TipoContatoDropDownList_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="COMERCIAL">COMERCIAL</asp:ListItem>
                    <asp:ListItem>FINANCEIRO</asp:ListItem>
                    <asp:ListItem>LOGISTICA</asp:ListItem>
                    <asp:ListItem>MARKETING</asp:ListItem>
                    <asp:ListItem>REFERÊNCIA COMERCIAL</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa:"></asp:Label>
                <asp:TextBox ID="EmpresaTextBox" runat="server" CssClass="uppercase" Width="239px"></asp:TextBox>
                &nbsp;<asp:Label ID="NomeContatoLabel" runat="server" Text="Nome:"></asp:Label>
                <asp:TextBox ID="NomeContatoTextBox" runat="server" CssClass="uppercase" Width="239px"></asp:TextBox>
                &nbsp;<asp:Label ID="EmailContatoLabel" runat="server" Text="Email:"></asp:Label>
                <asp:TextBox ID="EmailContatoTextBox" runat="server" CssClass="uppercase" Width="273px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="TelefoneContatoLabel" runat="server" Text="Telefone:"></asp:Label>
                <asp:TextBox ID="DDDTelefoneContatoTextBox" runat="server" Width="40px" onkeypress="mascara( this, mnum );"
                    MaxLength="2"></asp:TextBox>
                <asp:TextBox ID="TelefoneContatoTextBox" runat="server" onkeypress="mascara( this, mnum );"></asp:TextBox>
                &nbsp;<asp:Label ID="RamalContatoLabel" runat="server" Text="Ramal:"></asp:Label>
                <asp:TextBox ID="RamalContatoTextBox" onkeypress="mascara( this, mnum );" runat="server"></asp:TextBox>
                &nbsp;<asp:Label ID="CargoContatoLabel" runat="server" Text="Cargo:"></asp:Label>
                <asp:TextBox ID="CargoContatoTextBox" CssClass="uppercase" runat="server"></asp:TextBox>
                <asp:LinkButton ID="AdcionarButton" class="btn btn-success" runat="server" CausesValidation="False"
                    OnClick="AdcionarButton_Click" title="Adicionar" data-rel="tooltip">
                                                    <span class="glyphicon glyphicons-ok" aria-hidden="true"> Adicionar</span> 

                </asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="AlterarLinkButton" class="btn btn-warning" runat="server" CausesValidation="False"
                    title="Alterar" data-rel="tooltip" OnClick="AlterarLinkButton_Click">
                                                    <span class="glyphicon glyphicons-ok" aria-hidden="true"> Alterar</span> 

                </asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="CancelarButton" class="btn btn-danger" runat="server" CausesValidation="False"
                    OnClick="CancelarButton_Click" title="Cancelar" data-rel="tooltip">
                                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"> Cancelar</span> 

                </asp:LinkButton>
            </asp:View>
        </asp:MultiView>
        <br />
        <asp:LinkButton ID="NovoContatoButton" class="btn btn-success" runat="server" CausesValidation="False"
            OnClick="NovoContato_Click" title="Novo Contato" data-rel="tooltip">
                                                    <span class="glyphicon glyphicon-list-alt" aria-hidden="true"> Novo Contato</span> 

        </asp:LinkButton>
        <asp:GridView ID="ContatoGridView" runat="server" CssClass="lstTabela" Width="100%"
            AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="ID" Visible="False">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ENTCONTATOIDLabel" Text='<%# Bind("ENTCONTATOID") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Empresa">
                    <ItemTemplate>
                        <asp:Label ID="EmpresaLabel" Text='<%# Bind("Empresa") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nome">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="NomeLabel" Text='<%# Bind("Nome") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="EmailLabel" Text='<%# Bind("Email") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DDD">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="DDDTelefoneLabel" Text='<%# Bind("DDDTelefone") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Telefone">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="TelefoneLabel" Text='<%# Bind("Telefone") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ramal">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="RamalLabel" Text='<%# Bind("Ramal") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="TipoContatoLabel" Text='<%# Bind("TipoContato") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cargo">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="CargoLabel" Text='<%# Bind("Cargo") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Alterar">
                    <ItemTemplate>
                        <asp:LinkButton ID="AlterarButton" class="btn btn-warning" runat="server" CausesValidation="False"
                            data-rel="tooltip" OnClick="AlterarButton_Click1">
                                                               <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> 

                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remover">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <center>
                            <asp:LinkButton ID="RemoverButton" class="btn btn-danger" runat="server" CausesValidation="False"
                                OnClick="RemoverButton_Click" data-rel="tooltip">
                                                               <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span> 

                            </asp:LinkButton>
                        </center>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <br />
    <div>
        <!-- caso queira liberar o botao proximo passo, apenas remova o visible="false" -->
        <asp:LinkButton ID="Passo3Button" class="btn btn-primary" runat="server" OnClick="Passo3Button_Click"
            title="Próximo Passo" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"> Próximo Passo</span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="AlterarButton" class="btn btn-warning" runat="server" Visible="false"
            OnClick="AlterarButton_Click" title="Alterar" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> Alterar</span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="PrincipalButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="PrincipalButton_Click" title="Principal"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-compressed" aria-hidden="true"> Principal </span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="EnderecoEntregaButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="EnderecoEntregaButton_Click"
            title="Endereços de Entrega" data-rel="tooltip">
                            <span class="glyphicon glyphicon-list" aria-hidden="true"> End. Entrega</span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="FiscalLinkButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" title="Fiscal" data-rel="tooltip" OnClick="FiscalLinkButton_Click">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Fiscal</span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="InformacoesButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="InformacoesButton_Click" title="Informações"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-folder-open" aria-hidden="true"> Informações</span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="PedidosLinkButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="PedidosButton_Click" title="Pedidos"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> Pedidos</span> 

        </asp:LinkButton>
        &nbsp;
    </div>
    <br />
    <div>
        <asp:LinkButton ID="AnexosButton" class="btn btn-success" runat="server" Visible="false"
            CausesValidation="False" OnClick="AnexosButton_Click" title="Anexos" data-rel="tooltip">
                            <span class="glyphicon glyphicon-paperclip" aria-hidden="true"> Anexos</span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="ObservacoesButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="ObservacoesButton_Click" title="Observações"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"> Observações</span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="HoldingLinkButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="HoldingButton_Click" title="Observações"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Holding</span> 

        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="LogisticaLinkButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="LogisticaButton_Click" title="Observações"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-transfer" aria-hidden="true"> Logistica</span> 
        
        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="VendedorLinkButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="VendedorButton_Click" title="Vendedor"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-user" aria-hidden="true"> Vendedor</span> 
        
        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="DuplicataLinkButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="DuplicatasButton_Click" title="Duplicatas"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-paperclip" aria-hidden="true"> Duplicatas</span> 
        
        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="NotasLinkButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="NotasButton_Click" title="Notas"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-book" aria-hidden="true"> Notas</span> 
        
        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="AgendaLinkButton" class="btn btn-success" runat="server"
            Visible="false" CausesValidation="False" OnClick="AgendaButton_Click" title="Agenda"
            data-rel="tooltip">
                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"> Agenda</span> 
        
        </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="CRMLinkButton" class="btn btn-success" runat="server" Visible="false"
            CausesValidation="False" OnClick="CrmButton_Click" title="CRM" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> CRM</span> 
        
        </asp:LinkButton>
    </div>
    <br />
    <div>
        <asp:LinkButton ID="CancelarOperacaoLinkButton" class="btn btn-danger" runat="server"
            Visible="false" OnClick="CancelarOperacaoButton_Click" title="Retornar a Lista de Entidade"
            CausesValidation="False" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Retornar</span> 

        </asp:LinkButton>
    </div>
    <br />
    <asp:Literal ID="ENTCONTATOIDLiteral" runat="server" Visible="false"></asp:Literal>
</asp:Content>
