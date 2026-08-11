<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlControladoria.ascx.cs" Inherits="VendasWeb.usercontrol.WebUserControlControladoria" %>
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
                    <asp:LinkButton ID="HomeLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Home</asp:LinkButton>

                    <asp:LinkButton ID="PerfilComercialLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-clock-o fa-3x"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="PerfilComercialLinkButton_Click">Período Pedidos</asp:LinkButton>

                    <asp:LinkButton ID="PeriodoSimulacaoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-clock-o fa-3x"
                        runat="server" title="Perído Válido Silmulação" CausesValidation="False"
                        data-rel="tooltip" OnClick="PeriodoSimulacaoLinkButton_Click">Período Simulação</asp:LinkButton>

                    <asp:LinkButton ID="FreteLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-truck fa-3x"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="FreteLinkButton_Click" Visible="false">Fretes</asp:LinkButton>

                    <asp:LinkButton ID="LeberarPedidosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-alt fa-3x"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" Visible="false">Liberar Pedidos</asp:LinkButton>

                    <asp:LinkButton ID="CadastroPSIULinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x"
                        runat="server" OnClick="CadastroPSIULinkButton_Click" title="Cadastro de PSIU" CausesValidation="False"
                        data-rel="tooltip" Visible="false">Cadastro PSIU</asp:LinkButton>

                    <asp:LinkButton ID="SimuladorParametros" class="btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x"
                        runat="server" title="Simulador de parâmetros" CausesValidation="False"
                        data-rel="tooltip" OnClick="SimuladorParametros_Click" Visible="false">Parâmetros Simulador</asp:LinkButton>

                    <asp:LinkButton ID="ListaSimulador" class="btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x"
                        runat="server" title="Lista de simulações de preços" CausesValidation="False"
                        data-rel="tooltip" OnClick="ListaSimulador_Click">Simulações de preço</asp:LinkButton>

                    <asp:LinkButton ID="Simulacao" class="btn btn-lg btn-block btn-info btn-labeled fa fa-money fa-3x"
                        runat="server" title="Faz uma simulação de preços" CausesValidation="False"
                        data-rel="tooltip" OnClick="Simulacao_Click">Simular Preço</asp:LinkButton>

                    <asp:LinkButton ID="AtualizacaoCustosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x"
                        runat="server" title="Atualiza Custos Mensais" CausesValidation="False"
                        data-rel="tooltip" OnClick="AtualizacaoCustosLinkButton_Click">Atualizar Custos</asp:LinkButton>

                    <asp:LinkButton ID="ConsultaCustosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-question-circle-o fa-3x"
                        runat="server" title="Consulta Custos Mensais" CausesValidation="False"
                        data-rel="tooltip" OnClick="ConsultaCustosLinkButton_Click">Consultar Custos</asp:LinkButton>

                    <asp:LinkButton ID="EmpenhoEstoqueLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-archive fa-3x"
                        runat="server" title="Empenho Estoque" CausesValidation="False"
                        data-rel="tooltip" OnClick="EmpenhoEstoqueLinkButton_Click">Empenho Estoque</asp:LinkButton>

                    <asp:LinkButton ID="RelatorioAtendimentoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-line-chart fa-3x"
                        runat="server" title="Consulta Custos Mensais" CausesValidation="False"
                        data-rel="tooltip" OnClick="RelatorioAtendimentoLinkButton_Click">Relatório Atendimento</asp:LinkButton>

                    <asp:LinkButton ID="PosicaoFinanceiraLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-line-chart fa-3x"
                        runat="server" title="Consulta Custos Mensais" CausesValidation="False"
                        data-rel="tooltip" OnClick="PosicaoFinanceiraLinkButton_Click">Posição Financeira</asp:LinkButton>

                    <asp:LinkButton ID="SimuladorFreteLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-truck fa-3x"
                        OnClick="SimuladorFreteLinkButton_Click">Simulador Frete</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
