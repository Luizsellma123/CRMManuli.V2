<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FinanceiroWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.FinanceiroWebUserControl" %>

<asp:UpdatePanel ID="PainelUpdatePanel" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <!-- COLUNA 2-->
        <div class="col-sm-3 bg-gray pad-ver">
            <!--BLOCO DE COMANDOS-->
            <!--===================================================-->
            <div class="row">
                <!--Block Level buttons-->
                <!--===================================================-->
                <div class="col-xs-12">
                    <asp:LinkButton ID="HomeLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Home do Financeiro" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Home</asp:LinkButton>
                    
                    <asp:LinkButton ID="Simulacao" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Liberação de pedidos financeiro." CausesValidation="False"
                        data-rel="tooltip" OnClick="Simulacao_Click">Analisar Pedidos</asp:LinkButton>

                    <asp:LinkButton ID="PedidosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Liberação de pedidos financeiro." CausesValidation="False"
                        data-rel="tooltip" OnClick="PedidosLinkButton_Click">Lista Pedidos</asp:LinkButton>

                    <asp:LinkButton ID="ControleBancariaLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-usd fa-3x"
                        runat="server" title="Controle Bancario." CausesValidation="False"
                        data-rel="tooltip" OnClick="ControleBancariaLinkButton_Click">Bancos</asp:LinkButton>

                    <asp:LinkButton ID="CondicaoPagamentoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-credit-card-alt fa-3x"
                        runat="server" title="Condições A Vista." CausesValidation="False"
                        data-rel="tooltip" OnClick="CondicaoPagamentoLinkButton_Click">Condições Pagamento</asp:LinkButton>

                    <asp:LinkButton ID="ContaCorrenteLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-user fa-3x"
                        runat="server" title="Conta Corrente." CausesValidation="False"
                        data-rel="tooltip" OnClick="ContaCorrenteLinkButton_Click">Conta Corrente</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


