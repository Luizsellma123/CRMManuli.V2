<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndicadoresWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.IndicadoresWebUserControl" %>

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
                        runat="server" title="Home do SAC" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Home</asp:LinkButton>

                    <asp:LinkButton ID="TILinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x"
                        runat="server" title="TI" CausesValidation="False"
                        data-rel="tooltip" OnClick="TILinkButton_Click">Tecnologia Informação</asp:LinkButton>

                    <asp:LinkButton ID="SAMLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-ticket fa-3x"
                        runat="server" title="SAM" CausesValidation="False" Enabled="false"
                        data-rel="tooltip" OnClick="SAMLinkButton_Click">SAM - Atendimento Clientes</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
