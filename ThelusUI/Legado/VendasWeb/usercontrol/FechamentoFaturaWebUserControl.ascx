<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FechamentoFaturaWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.FechamentoFaturaWebUserControl" %>

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

                    <asp:LinkButton ID="PrincipalLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="NotasFiscaisLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x" Enabled="false"
                        OnClick="NotasFiscaisLinkButton_Click">Notas Fiscais</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
