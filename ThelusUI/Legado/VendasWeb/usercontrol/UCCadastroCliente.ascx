<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCadastroCliente.ascx.cs" Inherits="VendasWeb.usercontrol.UCCadastroCliente" %>
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

                    <asp:LinkButton ID="EnderecosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-map-o fa-3x"
                        runat="server" title="Endereços" CausesValidation="False"
                        data-rel="tooltip" OnClick="EnderecosLinkButton_Click">Endereços</asp:LinkButton>

                    <asp:LinkButton ID="ContatosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x"
                        runat="server" title="Contatos" CausesValidation="False"
                        data-rel="tooltip" OnClick="ContatosLinkButton_Click">Contatos</asp:LinkButton>

                    <asp:LinkButton ID="ObservacaoCompletaLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x"
                        runat="server" title="Contatos" CausesValidation="False"
                        data-rel="tooltip" OnClick="ObservacaoCompletaLinkButton_Click">Observações Internas</asp:LinkButton>


                    <asp:LinkButton ID="FinanceiroLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-money fa-3x"
                        runat="server" title="Financeiro" CausesValidation="False"
                        data-rel="tooltip" OnClick="FinanceiroLinkButton_Click">Financeiro</asp:LinkButton>

                    <asp:LinkButton ID="LimiteCreditoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-usd fa-3x"
                        runat="server" title="Financeiro" CausesValidation="False"
                        data-rel="tooltip" OnClick="LimiteCreditoLinkButton_Click">Crédito Cliente</asp:LinkButton>

                    <asp:LinkButton ID="FiscalLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-bar-chart fa-3x"
                        runat="server" title="Fiscal" CausesValidation="False"
                        data-rel="tooltip" OnClick="FiscalLinkButton_Click">Fiscal</asp:LinkButton>

                    <asp:LinkButton ID="AnexosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x"
                        runat="server" title="Anexos" CausesValidation="False"
                        data-rel="tooltip" Visible="false">Anexos</asp:LinkButton>

                    <asp:LinkButton ID="SolicitacaoAlteracaoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-paper-plane fa-3x"
                        runat="server" title="Solicitação Alteração" CausesValidation="False"
                        data-rel="tooltip" OnClick="SolicitacaoAlteracaoLinkButton_Click">Solicitação Alteração</asp:LinkButton>

                    <asp:LinkButton ID="HistoricoClienteLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-edit fa-3x"
                        runat="server" title="Histórico Cliente" CausesValidation="False"
                        data-rel="tooltip" OnClick="HistoricoClienteLinkButton_Click">Histórico Cliente</asp:LinkButton>

                    <asp:LinkButton ID="ContasReceberLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-usd fa-3x"
                        runat="server" title="Financeiro" CausesValidation="False"
                        data-rel="tooltip" OnClick="ContasReceberLinkButton_Click">Contas Receber</asp:LinkButton>

                    <asp:LinkButton ID="EnviarAnalizeFinanceiroLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-arrow-circle-right fa-3x"
                        runat="server" title="Enviar Análise" CausesValidation="False" Visible="false"
                        data-rel="tooltip" OnClick="EnviarAnalizeFinanceiroLinkButton_Click">Enviar Análise Financeiro</asp:LinkButton>

                    <asp:LinkButton ID="EnviarAnalizeFiscalLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-arrow-circle-right fa-3x"
                        runat="server" title="Enviar Análise" CausesValidation="False" Visible="false"
                        data-rel="tooltip" OnClick="EnviarAnalizeFiscalLinkButton_Click">Enviar Análise Fiscal</asp:LinkButton>

                    <asp:LinkButton ID="AprovarLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-check fa-3x"
                        runat="server" title="Enviar Análise" CausesValidation="False" Visible="false"
                        data-rel="tooltip" OnClick="AprovarLinkButton_Click">Aprovar Cadastro</asp:LinkButton>

                    <asp:LinkButton ID="ReprovarLinkButton" class="btn btn-lg btn-block btn-danger btn-labeled fa fa-arrow-circle-left fa-3x"
                        runat="server" title="Enviar Análise" CausesValidation="False" Visible="false"
                        data-rel="tooltip" OnClick="ReprovarLinkButton_Click">Reprovar Cadastro</asp:LinkButton>

                    <asp:LinkButton ID="AnaliseCreditoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-money fa-3x"
                        runat="server" title="Análise Crédito" CausesValidation="False" 
                        data-rel="tooltip" OnClick="AnaliseCreditoLinkButton_Click">Análise Crédito</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
