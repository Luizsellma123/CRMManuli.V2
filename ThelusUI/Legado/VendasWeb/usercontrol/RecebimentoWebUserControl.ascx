<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecebimentoWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.RecebimentoWebUserControl" %>

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

                    <asp:LinkButton ID="ListaLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x"
                        runat="server" title="Lista" CausesValidation="False"
                        data-rel="tooltip" OnClick="ListaLinkButton_Click">Lista</asp:LinkButton>

                    <asp:LinkButton ID="NovoLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-plus-square fa-3x"
                        OnClick="NovoLinkButton_Click">Novo Recebimento</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
