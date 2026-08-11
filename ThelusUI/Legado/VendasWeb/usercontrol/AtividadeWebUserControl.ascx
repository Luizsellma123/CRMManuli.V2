<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AtividadeWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.AtividadeWebUserControl" %>

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

                    <asp:LinkButton ID="PrincipalLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        runat="server" title="Principal" CausesValidation="False" Enabled="false"
                        data-rel="tooltip" OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="HistoricoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-edit fa-3x"
                        runat="server" title="Historico" CausesValidation="False" Enabled="true"
                        data-rel="tooltip" OnClick="HistoricoLinkButton_Click">Historico</asp:LinkButton>

                    <asp:LinkButton ID="AnexoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x"
                        runat="server" title="Anexo" CausesValidation="False" Enabled="true"
                        data-rel="tooltip" OnClick="AnexoLinkButton_Click">Anexo</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
