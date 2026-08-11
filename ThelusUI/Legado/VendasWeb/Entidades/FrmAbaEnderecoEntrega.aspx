<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true"
    CodeBehind="FrmAbaEnderecoEntrega.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaEnderecoEntrega" %>

<%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"
    TagPrefix="uc1" %>
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
                    Cadastro de Cliente - Endereço de Entrega</h3>
            </b>
        </center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
        <br />
        <asp:Literal ID="EnderEntSeqLiteral" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="EnderEntFoneSeqLiteral" runat="server" Visible="false"></asp:Literal>
        <asp:Label ID="EnderecoEntragaEoMesmoLabel" runat="server" Text="Endereço de Entrega é o mesmo?"></asp:Label>
        <br />
        <asp:DropDownList ID="EnderecoEntregaEoMesmosDropDownList" runat="server" OnSelectedIndexChanged="EnderecoEntregaEoMesmosDropDownList_SelectedIndexChanged"
            AutoPostBack="True">
            <asp:ListItem Selected="True">Sim</asp:ListItem>
            <asp:ListItem >Não</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:MultiView ID="EnderecoEntregaMultView" runat="server" ActiveViewIndex="0">
            <asp:View ID="EnderecoEntregaView" runat="server">
                <asp:Label ID="EntregaCnpjLabel" runat="server" Text="CNPJ/CPF:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="EntragaCnpjTextBox" runat="server" Width="151px" onkeypress="mascara( this, mnum );"
                    AutoPostBack="true" OnTextChanged="EntragaCnpjTextBox_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="EntragaCnpjTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="RazaoSocialEntregaLabel" runat="server" Text="Razão Social:"></asp:Label>
                &nbsp;&nbsp;
                <asp:TextBox ID="RazaoSocialEntregaTextBox" runat="server" Width="730px" CssClass="uppercase"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="RazaoSocialEntregaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="EmailEntregaLabel" runat="server" Text="Email:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="EmailEntregaTextBox" runat="server" Width="270px" CssClass="lowercase"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="EmailEntregaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="EmailEntregaTextBox"
                    Display="Dynamic" SetFocusOnError="True" ErrorMessage="Email Invalido" ForeColor="Red"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email Inválido</asp:RegularExpressionValidator>
                <br />
                <br />
                <asp:Label ID="CepEntregaLabel" runat="server" Text="CEP:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="CepEntregaTextBox" runat="server" Width="151px" AutoPostBack="true"
                    CausesValidation="False" onkeypress="mascara( this, mcep );" OnTextChanged="CepEntregaTextBox_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="CepEntregaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:Label ID="EnderecoEntregaLabel" runat="server" Text="Endereço:"></asp:Label>
                <asp:TextBox ID="EnderecoEntregaTextBox" runat="server" Width="380px" CssClass="uppercase"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="EnderecoEntregaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:Label ID="NumeroEnderecoEntregaLabel" runat="server" Text="Número:"></asp:Label>
                <asp:TextBox ID="NumeroEnderecoEntregaTextBox" runat="server" Width="50px" OnTextChanged="NumeroEnderecoEntregaTextBox_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="NumeroEnderecoEntregaTextBox" ErrorMessage="Preencher S/N para sem Número"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NumeroEnderecoEntregaTextBox"
                    Display="Dynamic" SetFocusOnError="True" ErrorMessage="Preencher S/N para sem Número"
                    ForeColor="Red" ValidationExpression="((\d+$)|([sS]+[/]+[nN]))$">Preencher S/N para sem Número</asp:RegularExpressionValidator>
                <br />
                <br />
                <asp:Label ID="BairroEnderecoEntregaLabel" runat="server" Text="Bairro:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="BairroEnderecoEntregaTextBox" runat="server" Width="151px" CssClass="uppercase"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="BairroEnderecoEntregaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:Label ID="UFEnderecoEntregaLabel" runat="server" Text="UF:"></asp:Label>
                <asp:TextBox ID="UFEnderecoEntregaTextBox" runat="server" Width="30px" MaxLength="2"
                    CssClass="uppercase" OnTextChanged="UFEnderecoEntregaTextBox_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="UFEnderecoEntregaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:Label ID="CidadeEnderecoEntregaLabel" runat="server" Text="Cidade:"></asp:Label>
                <asp:DropDownList ID="CidadeEnderecoEntregaDropDownList" runat="server" Width="151px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="CidadeEnderecoEntregaDropDownList"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:Label ID="ComplementoEnderecoEntregaLabel" runat="server" Text="Complemento:"></asp:Label>
                <asp:TextBox ID="ComplementoEnderecoEntregaTextBox" runat="server" Width="192px"
                    CssClass="uppercase"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="ResponsavelEnderecoLabel" runat="server" Text="Responsável:"></asp:Label>
                &nbsp;&nbsp;<asp:TextBox ID="ResponsavelEnderecoTextBox" runat="server" Width="362px"
                    CssClass="uppercase"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="ResponsavelEnderecoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="TelefoneResponsavelEnderecoLabel" runat="server" Text="Telefone:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="DDDTelefoneResponsavelEnderecoTextBox" onkeypress="mascara( this, mnum );"
                    runat="server" Width="50px" MaxLength="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="DDDTelefoneResponsavelEnderecoTextBox"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="TelefoneResponsavelEnderecoTextBox" runat="server" onkeypress="mascara( this, mnum );"
                    Width="151px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" Display="Dynamic"
                    SetFocusOnError="True" ControlToValidate="TelefoneResponsavelEnderecoTextBox"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:Label ID="RamalTelefoneResponsavelEnderecoLabel" runat="server" Text="Ramal:"></asp:Label>
                <asp:TextBox ID="RamalTelefoneResponsavelEnderecoTextBox" runat="server" Width="151px"></asp:TextBox>
                <br />
                <br />
    </div>
    </asp:View> </asp:MultiView>
    <br />
    <br />
    <div>
        <asp:LinkButton ID="Passo4Button" class="btn btn-primary" runat="server" OnClick="Passo4Button_Click"
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
        &nbsp;<asp:LinkButton ID="ContatoButton" class="btn btn-success" runat="server" Visible="false"
            CausesValidation="False" OnClick="ContatoButton_Click" title="Contato" data-rel="tooltip">
                            <span class="glyphicon glyphicon-user" aria-hidden="true"> Contato</span> 

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
</asp:Content>
