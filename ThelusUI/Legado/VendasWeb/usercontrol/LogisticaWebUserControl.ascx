<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogisticaWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.LogisticaWebUserControl" %>

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

                    <asp:LinkButton ID="HomeLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        OnClick="HomeLinkButton_Click">Home</asp:LinkButton>

                    <asp:LinkButton ID="LinkButtonAtualizar" class="btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x"
                        runat="server" title="Enviar para Analise" CausesValidation="False" data-rel="tooltip"
                        OnClick="LinkButtonAtualizar_Click">Atualizar Dados</asp:LinkButton>

                    <asp:LinkButton ID="FechamentoFaturaLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-alt fa-3x" Enabled="false"
                        OnClick="FechamentoFaturaLinkButton_Click">Fechamento Fatura</asp:LinkButton>

                    <asp:LinkButton ID="StatusFechamentoFaturaLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-info-circle fa-3x" Enabled="false"
                        OnClick="StatusFechamentoFaturaLinkButton_Click">Status Fechamento Fatura</asp:LinkButton>

                    <asp:LinkButton ID="RastreioPedidosLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-truck fa-3x" Enabled="false"
                        OnClick="RastreioPedidosLinkButton_Click">Rastreio Pedidos</asp:LinkButton>

                    <asp:LinkButton ID="CadastroTransportadorLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-truck fa-3x" Enabled="false"
                        OnClick="CadastroTransportadorLinkButton_Click">Cadastro Transportador</asp:LinkButton>

                    <asp:LinkButton ID="SimuladorFreteLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-truck fa-3x" Enabled="false"
                        OnClick="SimuladorFreteLinkButton_Click">Simulador Frete</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
