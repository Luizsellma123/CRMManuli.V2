<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGerencialVendas.ascx.cs" Inherits="VendasWeb.usercontrol.UCGerencialVendas" %>

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
                    <asp:LinkButton ID="HomeLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x"
                        runat="server" title="Principal" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="LinkButtonAtualizar" class="btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x"
                        runat="server" title="Enviar para Analise" CausesValidation="False" data-rel="tooltip"
                        OnClick="LinkButtonAtualizar_Click"> Atualizar Dados  </asp:LinkButton>

                    <asp:LinkButton ID="SimuladorPrecosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x"
                        runat="server" title="Simulador" CausesValidation="False" 
                        data-rel="tooltip" OnClick="SimuladorPrecosLinkButton_Click" Visible="false">Simulador</asp:LinkButton>

                    <asp:LinkButton ID="TrocaCarteiraLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-exchange fa-3x"
                        runat="server" title="Troca Carteira" CausesValidation="False" 
                        data-rel="tooltip" OnClick="TrocaCarteiraLinkButton_Click">Troca Carteira</asp:LinkButton>

                    <asp:LinkButton ID="AcompanhamentoPedidoLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-line-chart fa-3x"
                        runat="server" title="Pedido da Entidade" data-rel="tooltip" 
                        CausesValidation="False" OnClick="AcompanhamentoPedidoLinkButton_Click"> Acompanhamento Pedidos </asp:LinkButton>

                    <asp:LinkButton ID="TabelaPrecoLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-line-chart fa-3x"
                        runat="server" title="Tabela de Preço" data-rel="tooltip" 
                        CausesValidation="False" OnClick="TabelaPrecoLinkButton_Click"> Tabela Preço </asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>