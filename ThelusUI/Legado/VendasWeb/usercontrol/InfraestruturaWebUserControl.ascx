<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfraestruturaWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.InfraestruturaWebUserControl" %>

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
                        runat="server" title="Home" OnClick="HomeLinkButton_Click" CausesValidation="False"
                        data-rel="tooltip">Home</asp:LinkButton>

                    <asp:LinkButton ID="PainelLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-tachometer fa-3x"
                        runat="server" title="Painel" OnClick="PainelLinkButton_Click" CausesValidation="False"
                        data-rel="tooltip">Painel</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
