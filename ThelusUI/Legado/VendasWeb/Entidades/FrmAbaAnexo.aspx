<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true"
    CodeBehind="FrmAbaAnexo.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaAnexo" %>

<%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <link rel="stylesheet" type="text/css" href="../css/ListaPedido.css?aux=6" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="conteudo">
        <center>
            <b>
                <h3>
                    Cadastro de Cliente - Anexos</h3>
            </b>
        </center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
        <span class="glyphicons glyphicon-paperclip"></span>
        <asp:Label ID="DescricaoLabel" runat="server" Text="Os anexos de documentos são para clientes que não possuem uma rede cadastrada.
                     – Duvidas entrar em contato com Adm Vendas"></asp:Label>
        <br />
        <asp:GridView ID="DocumentosGridView" runat="server" Visible="False" CssClass="lstTabela"
            Width="100%" EmptyDataText="Nenhum Documento Cadastrado para essa entidade."
            AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Baixar">
                    <ItemTemplate>
                        <center>
                            <asp:LinkButton ID="SelecionarButton" class="btn btn-primary" runat="server" CausesValidation="False"
                                OnClick="SelecionarButton_Click" data-rel="tooltip">
                            <span class="glyphicon glyphicon-cloud-download" aria-hidden="true"></span> 

                            </asp:LinkButton>
                        </center>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sequência" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="DocEntSeqLabel" runat="server" Text='<%# Bind("DocEntSeq") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nome do Anexo">
                    <ItemTemplate>
                        <asp:Label ID="DocEntObsLabel" runat="server" Text='<%# Bind("DocEntObs") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Usuario do Cadastro">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("UsuCod") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Data do Cadastro">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DocEntData", "{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Caminho Documento" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="DocEntPathArqLabel" runat="server" Text='<%# Bind("DocEntPathArq") %>'> ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remover Documento" Visible="False">
                    <ItemTemplate>
                        <center>
                            <asp:LinkButton ID="RemoverDocumentoButton" class="btn btn-danger" runat="server"
                                CausesValidation="False" OnClick="RemoverDocumentoButton_Click" data-rel="tooltip">
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
        <br />

          <asp:MultiView ID="DocumentosFixosMultView" runat="server" ActiveViewIndex="0">
            <asp:View ID="DocumentosFixosView" runat="server">

        <asp:CheckBox ID="AlteracaoContratualCheckBox" runat="server" Text="Contrato Social e ultima alteração"
            Enabled="false" />
        <br />
        <asp:Label ID="AlteracaoContratuaLabel" runat="server" CssClass="LabelValidacao"></asp:Label>
        <asp:FileUpload ID="AlteracaoContratualFileUpload" runat="server" />
        <asp:LinkButton ID="AlteracaoContratuaButton" class="btn btn-success" runat="server"
            CausesValidation="False" OnClick="AlteracaoContratuaButton_Click" data-rel="tooltip">
                               <span class="glyphicon glyphicon-cloud-upload" aria-hidden="true"> Carregar Arquivo </span> 
        </asp:LinkButton>
        <br />
        <br />
        <asp:CheckBox ID="CartaFaturamentoCheckBox" runat="server" Text="Carta de Faturamento Realizado dos últimos Anos (ASS. Pelo Contador)"
            Enabled="false" />
        <br />
        <asp:Label ID="CartaFaturamentoLabel" runat="server" CssClass="LabelValidacao"></asp:Label>
        <asp:FileUpload ID="CartaFaturamentoFileUpload" runat="server" />
        <asp:LinkButton ID="CartaFaturamentoButton" class="btn btn-success" runat="server"
            CausesValidation="False" OnClick="CartaFaturamentoButton_Click" data-rel="tooltip">
                               <span class="glyphicon glyphicon-cloud-upload" aria-hidden="true"> Carregar Arquivo </span> 
        </asp:LinkButton>
        <br />
        <br />
        <asp:CheckBox ID="UltimosBalancoCheckBox" runat="server" Text="2 Últimos Balanços"
            Enabled="false" />
        <br />
        <asp:Label ID="UltimosBalancoLabel" runat="server" CssClass="LabelValidacao"></asp:Label>
        <asp:FileUpload ID="UltimosBalancoFileUpload" runat="server" />
        <asp:LinkButton ID="UltimosBalancoButton" class="btn btn-success" runat="server"
            CausesValidation="False" OnClick="UltimosBalancoButton_Click" data-rel="tooltip">
                               <span class="glyphicon glyphicon-cloud-upload" aria-hidden="true"> Carregar Arquivo </span> 
        </asp:LinkButton>
        </asp:View>
        </asp:MultiView>

        
        <br />
        <asp:Label ID="OutrosDocumentosLabel" runat="server" Text="Outros documento?" CssClass="LabelValidacao"
            Visible="false"></asp:Label>
        <br />
        <asp:Label ID="NomeDocumentoIncluirLabel" runat="server" Text="Nome do Documento:"
            CssClass="LabelValidacao" Visible="false"></asp:Label>
        &nbsp;<asp:TextBox ID="NomeDocIncluirTextBox" runat="server" Visible="false" Width="305px"></asp:TextBox>
        <br />
        <asp:Label ID="IncluirDocLabel" runat="server" CssClass="LabelValidacao" Visible="false"></asp:Label>
        <asp:FileUpload ID="IncluirDocFileUpload" runat="server" Visible="false" />
        <asp:LinkButton ID="IncluirDocButton" class="btn btn-success" runat="server" CausesValidation="False"
            Visible="false" OnClick="IncluirDocButton_Click" data-rel="tooltip">
                               <span class="glyphicon glyphicon-cloud-upload" aria-hidden="true"> Salvar Arquivo </span> 
        </asp:LinkButton>
        <br />
        <br />
        <div>
            <asp:LinkButton ID="ProximoButton" class="btn btn-primary" runat="server" OnClick="ProximoPassoButton_Click"
                title="Próximo Passo" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"> Próximo Passo</span> 

            </asp:LinkButton>
            &nbsp;<asp:LinkButton ID="PrincipalButton" class="btn btn-success" runat="server" Visible="false"
                CausesValidation="False" OnClick="PrincipalButton_Click" title="Principal" data-rel="tooltip">
                            <span class="glyphicon glyphicon-compressed" aria-hidden="true"> Principal </span> 

            </asp:LinkButton>
            &nbsp;<asp:LinkButton ID="ContatoButton" class="btn btn-success" runat="server" Visible="false"
                CausesValidation="False" OnClick="ContatoButton_Click" title="Contato" data-rel="tooltip">
                            <span class="glyphicon glyphicon-user" aria-hidden="true"> Contato</span> 

            </asp:LinkButton>
            &nbsp;<asp:LinkButton ID="EnderecoEntregaButton" class="btn btn-success" runat="server"
                Visible="false" CausesValidation="False" OnClick="EnderecoEntregaButton_Click"
                title="Endereços de Entrega" data-rel="tooltip">
                            <span class="glyphicon glyphicon-list" aria-hidden="true"> End. Entrega</span> 

            </asp:LinkButton>
            &nbsp;<asp:LinkButton ID="FiscalLinkButton" class="btn btn-success" runat="server" Visible="false"
                CausesValidation="False" title="Fiscal" data-rel="tooltip" OnClick="FiscalLinkButton_Click">
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
            <asp:LinkButton ID="InformacoesButton" class="btn btn-success" runat="server" Visible="false"
                CausesValidation="False" OnClick="InformacoesButton_Click" title="Informações"
                data-rel="tooltip">
                            <span class="glyphicon glyphicon-folder-open" aria-hidden="true"> Informações</span> 

            </asp:LinkButton>
            &nbsp;<asp:LinkButton ID="ObservacoesButton" class="btn btn-success" runat="server" Visible="false"
                CausesValidation="False" OnClick="ObservacoesButton_Click" title="Observações"
                data-rel="tooltip">
                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"> Observações</span> 

            </asp:LinkButton>
            &nbsp;<asp:LinkButton ID="HoldingLinkButton" class="btn btn-success" runat="server" Visible="false"
                CausesValidation="False" OnClick="HoldingButton_Click" title="Observações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Holding</span> 

            </asp:LinkButton>
            &nbsp;<asp:LinkButton ID="LogisticaLinkButton" class="btn btn-success" runat="server" Visible="false"
                CausesValidation="False" OnClick="LogisticaButton_Click" title="Observações"
                data-rel="tooltip">
                            <span class="glyphicon glyphicon-transfer" aria-hidden="true"> Logistica</span> 
        
            </asp:LinkButton>
            &nbsp;<asp:LinkButton ID="VendedorLinkButton" class="btn btn-success" runat="server" Visible="false"
                CausesValidation="False" OnClick="VendedorButton_Click" title="Vendedor" data-rel="tooltip">
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
                OnClick="CancelarOperacaoButton_Click" title="Retornar a Lista de Entidade" CausesValidation="False"
                data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Retornar</span> 

            </asp:LinkButton>
        </div>
        <br />
    </div>
</asp:Content>
