<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmAbaInformacoes.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaInformacoes" %>

<%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <link rel="stylesheet" type="text/css" href="../css/ListaPedido.css?aux=6" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="conteudo">

        <center><b>
            <h3>Cadastro de Informações</h3>
        </b></center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
        <br />

        <asp:Label ID="CategoriaLabel" runat="server" Text="">Categoria(CNAE):</asp:Label>
        <asp:DropDownList ID="CategoriaDropDownList" runat="server" Width="150px" CssClass="form-control">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="CategoriaRequiredFieldValidator" runat="server" Display="Dynamic" SetFocusOnError="True"
            ControlToValidate="CategoriaDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>


        <br />
        <br />

        <asp:Label Text="Condição de Recebimento:" runat="server" ID="CondicaoRecebimentoLabel"></asp:Label>
        <asp:DropDownList ID="CondicaoRecebimentoDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CondicaoRecebimentoDropDownList_SelectedIndexChanged" Width="250px"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" Display="Dynamic" SetFocusOnError="True"
            ControlToValidate="CondicaoRecebimentoDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>

        &nbsp;<asp:Label ID="OutraCondPagLabel" Text="Qual?" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="OutraCondPagTextBox" runat="server" Visible="false" Width="100px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" SetFocusOnError="True"
            ControlToValidate="OutraCondPagTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>



        &nbsp;<asp:Label ID="TipoDeCobrancaLabel" runat="server" Text="Tipo de Cobrança:"></asp:Label>
        <asp:DropDownList ID="TipoCobCodDropDownList" runat="server" Width="150px">
        </asp:DropDownList>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="True"
            ControlToValidate="TipoCobCodDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>


        <br />
        <br />

        <asp:Label Text="Previsão a ser faturado no mês: R$" runat="server" ID="Label1"></asp:Label>
        <asp:TextBox ID="UserPrevisaoFaturamentoMesTextBox" onkeypress="mascara( this, mvalor );" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" SetFocusOnError="True"
            ControlToValidate="UserPrevisaoFaturamentoMesTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>

        &nbsp;<asp:Label Text="Valor da primeira compra: R$" runat="server" ID="Label2"></asp:Label>
        <asp:TextBox ID="UserValorPrimeiraCompraTextBox" onkeypress="mascara( this, mvalor );" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" SetFocusOnError="True"
            ControlToValidate="UserValorPrimeiraCompraTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>

        <br />
        <br />
        <asp:Label Text="Valor Limite de Crédito:" runat="server" ID="EntValLimCredLabel" Visible="false"></asp:Label>
        <asp:TextBox ID="EntValLimCredTextBox" onkeypress="mascara( this, mvalor );" runat="server" Visible="false"></asp:TextBox>

        &nbsp;<asp:Label Text="Quantidade Dias Atraso:" runat="server" ID="ENTQTDDIASATRASOLabel" Visible="false"></asp:Label>
        <asp:TextBox ID="ENTQTDDIASATRASOTextBox" onkeypress="mascara( this, mnum );" AutoPostBack="true" runat="server" Visible="false"></asp:TextBox>

        <br />
        <br />
        <asp:Label Text="Como o Cliente chegou até a Manuli?" runat="server" ID="ComoChegouAteManuliLabel"></asp:Label>


        <asp:DropDownList ID="ComoChegouDropDownList" AutoPostBack="true" OnSelectedIndexChanged="ComoChegouRadioButtonList_SelectedIndexChanged" runat="server" Width="250px">
            <asp:ListItem Selected="True" Value="">SELECIONE</asp:ListItem>
            <asp:ListItem Value="INDICAÇÃO">INDICAÇÃO</asp:ListItem>
            <asp:ListItem Value="DISTRIBUIDOR">DISTRIBUIDOR</asp:ListItem>
            <asp:ListItem Value="VISITA DO VENDEDOR/REPRESENTANTE">VISITA DO VENDEDOR/REPRESENTANTE</asp:ListItem>
            <asp:ListItem Value="OUTROS">OUTROS</asp:ListItem>

        </asp:DropDownList>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Display="Dynamic" SetFocusOnError="True"
            ControlToValidate="ComoChegouDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>

        <br />
        <br />

        <asp:TextBox ID="OutrosTextBox" runat="server" Height="107px" placeholder="Qual a outra forma?" Visible="false" TextMode="MultiLine" Width="523px"></asp:TextBox>
    </div>
    <br />
    <br />

    <div>


        <asp:LinkButton ID="Passo5Button" class="btn btn-primary" runat="server" CausesValidation="False"
            OnClick="Passo5Button_Click" title="Próximo Passo" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"> Próximo Passo</span> 

        </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="AlterarButton" class="btn btn-warning" runat="server" Visible="false"
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
        <asp:LinkButton ID="AnexosButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
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


        &nbsp;<asp:LinkButton ID="LogisticaLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
            OnClick="LogisticaButton_Click" title="Observações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-transfer" aria-hidden="true"> Logistica</span> 
        
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


</asp:Content>
