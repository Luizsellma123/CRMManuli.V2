<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SACWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.SACWebUserControl" %>

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

                    <asp:LinkButton ID="HomeLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        runat="server" title="Home do SAC" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Home</asp:LinkButton>

                    <asp:LinkButton ID="AtividadesLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x"
                        runat="server" title="Atividades" CausesValidation="False"
                        data-rel="tooltip" OnClick="AtividadesLinkButton_Click">Atividades</asp:LinkButton>

                    <asp:LinkButton ID="TicketsLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-ticket fa-3x"
                        runat="server" title="Ticket's" CausesValidation="False"
                        data-rel="tooltip" OnClick="TicketsLinkButton_Click">Ticket's</asp:LinkButton>

                    <asp:LinkButton ID="SituacoesAtividadesLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-tasks fa-3x"
                        runat="server" title="Situações Atividades" CausesValidation="False"
                        data-rel="tooltip" OnClick="SituacoesAtividadesLinkButton_Click">Situações Atividades</asp:LinkButton>

                    <asp:LinkButton ID="SituacoesTicketsLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-tasks fa-3x"
                        runat="server" title="Situações Ticket's" CausesValidation="False"
                        data-rel="tooltip" OnClick="SituacoesTicketsLinkButton_Click">Situações Ticket's</asp:LinkButton>

                    <asp:LinkButton ID="ClassificacaoLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-sort-amount-asc fa-3x"
                        runat="server" title="Classificação" CausesValidation="False" data-rel="tooltip"
                        OnClick="ClassificacaoLinkButton_Click">Classificação</asp:LinkButton>

                    <asp:LinkButton ID="PrioridadeLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-ol fa-3x"
                        runat="server" title="Prioridade" CausesValidation="False" data-rel="tooltip"
                        OnClick="PrioridadeLinkButton_Click">Prioridade</asp:LinkButton>

                    <asp:LinkButton ID="CadastroSolucaoLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="CadastroSolucaoLinkButton_Click">Cadastro Solução</asp:LinkButton>

                    <asp:LinkButton ID="CadastroTipoOcorrenciaLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="CadastroTipoOcorrenciaLinkButton_Click">Cadastro Tipo Ocorrência</asp:LinkButton>

                    <asp:LinkButton ID="CadastroMotivoLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-alt fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="CadastroMotivoLinkButton_Click">Cadastro Motivo</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
