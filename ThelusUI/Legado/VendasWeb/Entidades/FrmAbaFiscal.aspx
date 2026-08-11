<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmAbaFiscal.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaFiscal" %>

<%@ Register src="../usercontrol/ControlEntidade.ascx" tagname="ControlEntidade" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <link rel="stylesheet" type="text/css" href="../css/ListaPedido.css?aux=6" />
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="conteudo">
        <center><b><h3>Cadastro de Cliente - Fiscal</h3></b></center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
        <br />

        <div class="Fiscal">

            <div style="width: 540px; float: left;">

                <asp:Label ID="Label20" runat="server" Text="Optante Simples:"></asp:Label>
                &nbsp;<asp:DropDownList ID="OptanteSimplesDropDownList" runat="server">
                    <asp:ListItem Selected="True" Value="">SELECIONE</asp:ListItem>
                    <asp:ListItem Value="Sim">Sim</asp:ListItem>
                    <asp:ListItem Value="Não">Não</asp:ListItem>
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" Display="Dynamic" SetFocusOnError="True"
                    ControlToValidate="OptanteSimplesDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <br />
                &nbsp;<asp:Label ID="Label21" runat="server" Text="Qual a Finalidade do Produto?"></asp:Label>
                &nbsp;<asp:DropDownList ID="FinalidadeProdutoDropDownList" runat="server">
                    <asp:ListItem Selected="True" Value="">SELECIONE</asp:ListItem>
                    <asp:ListItem Value="TRANSFORMAÇÃO">TRANSFORMAÇÃO</asp:ListItem>
                    <asp:ListItem Value="REVENDA">REVENDA</asp:ListItem>
                    <asp:ListItem Value="CONSUMO">CONSUMO</asp:ListItem>
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" Display="Dynamic" SetFocusOnError="True"
                    ControlToValidate="FinalidadeProdutoDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
            </div>
            <br />
            
            <div class="sidebar">

                <center><b>Legenda</b></center>

                <b>TRANSFORMAÇÃO</b>  - Compra o produto Manuli como uso para transformação de outro produdo.<br />
                <b>REVENDA</b>  - Compra o produto Manuli somente para revender.<br />
                <b>CONSUMO</b>  - Compra produto Manuli para consutmo próprio.
         
            </div>


            <br />


            <div style="width: 540px; float: left;">
                <asp:Label ID="Label22" runat="server" Text="Natureza Jurídica:"></asp:Label>
                &nbsp;<asp:DropDownList ID="NaturezaJuridicaDropDownList" runat="server" OnSelectedIndexChanged="NaturezaJuridicaDropDownList_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="">SELECIONE</asp:ListItem>
                    <asp:ListItem Value="Fabricante">FABRICANTE</asp:ListItem>
                    <asp:ListItem Value="Revendedor">REVENDEDOR</asp:ListItem>
                    <asp:ListItem Value="Representante">REPRESENTANTE</asp:ListItem>
                    
                    <asp:ListItem Value="Consumidor Contribuinte">CONTRIBUINTE</asp:ListItem>
                    <asp:ListItem Value="Consumidor">CONSUMIDOR</asp:ListItem>
                    <asp:ListItem Value="Entidade Governamental">GOVERNO</asp:ListItem>
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" Display="Dynamic" SetFocusOnError="True"
                    ControlToValidate="NaturezaJuridicaDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>

                &nbsp;<asp:Label ID="ClassificacaoLabel" runat="server" Text="Classificação:" Visible="false"></asp:Label>
                &nbsp;<asp:DropDownList ID="ClassificacaoDropDownList" runat="server" Visible="false" CausesValidation="false">
                    <asp:ListItem Selected="True" Value="">SELECIONE</asp:ListItem>
                    <asp:ListItem Value="Federal">FEDERAL</asp:ListItem>
                    <asp:ListItem Value="Municipal">MUNICIPAL</asp:ListItem>
                    <asp:ListItem Value="Estadual">ESTADUAL</asp:ListItem>

                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Display="Dynamic" SetFocusOnError="True"
                    ControlToValidate="ClassificacaoDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>








            </div>


            <div class="sidebar">

                <center><b>Legenda</b></center>

                <b>FABRICANTE:</b>  Fábrica de Salgados, Indústria de alimentos;<br />
                <b>CONTRIBUINTE:</b>  Rede Fast Food, Rotisserie, Pizzaria, Hotel, Cozinha Industrial, Similares;<br />
                <b>REVENDEDOR:</b>  Distribuidor, Broker, Varejo, Atacadista;<br />
                <b>GOVERNO:</b>  Venda Direta ou a Distribuidor para atender licitação Merenda Escolar.
                <b>CONSUMIDOR:</b>  Consumidor Final.
         
            </div>





        </div>





    </div>

    <br />
    <br />
    <div>


        &nbsp;<asp:LinkButton ID="Passo2Button" class="btn btn-primary" runat="server" CausesValidation="False"
                         OnClick="Passo2Button_Click" title="Próximo Passo" data-rel="tooltip">
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


        &nbsp;<asp:LinkButton ID="InformacoesButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="InformacoesButton_Click" title="Informações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-folder-open" aria-hidden="true"> Informações</span> 

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

        <asp:LinkButton ID="CancelarOperacaoLinkButton" class="btn btn-danger" runat="server"  Visible="false"
            OnClick="CancelarOperacaoButton_Click" title="Retornar a Lista de Entidade" CausesValidation="False" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Retornar</span> 

        </asp:LinkButton>
    </div>

    <br />



</asp:Content>
