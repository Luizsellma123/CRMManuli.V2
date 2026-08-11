<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlChamado.ascx.cs" Inherits="VendasWeb.usercontrol.WebUserControlChamado" %>

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

                    <asp:LinkButton ID="PrincipalLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="ResponsaveisLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x disabled"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="ResponsaveisLinkButton_Click">Responsáveis</asp:LinkButton>

                    <asp:LinkButton ID="AnexosLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x disabled"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="AnexosLinkButton_Click">Anexos</asp:LinkButton>

                    <asp:LinkButton ID="HistoricoLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-binoculars fa-3x disabled"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="HistoricoLinkButton_Click">Histórico</asp:LinkButton>

                    <asp:LinkButton ID="ProjetoLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-trello fa-3x disabled"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="ProjetoLinkButton_Click">Projeto</asp:LinkButton>

                    <asp:LinkButton ID="ApontamentoHorasLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-clock-o fa-3x disabled"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="ApontamentoHorasLinkButton_Click">Apontamento Horas</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
