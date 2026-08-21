<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NegociacaoDetalheWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.NegociacaoDetalheWebUserControl" %>
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
                    <asp:LinkButton ID="PrincipalLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x"
                        runat="server" title="Principal" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>
                    
                    <asp:LinkButton ID="Itens" class="btn btn-lg btn-block btn-info btn-labeled fa fa-cubes fa-3x"
                        runat="server" title="Lista Itens." CausesValidation="False"
                        data-rel="tooltip" OnClick="ItensLinkButton_Click">Lista Itens</asp:LinkButton>

                    <asp:LinkButton ID="HistoricoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-history fa-3x"
                        runat="server" title="Histórico" CausesValidation="False"
                        data-rel="tooltip" OnClick="HistoricoLinkButton_Click">Histórico</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>