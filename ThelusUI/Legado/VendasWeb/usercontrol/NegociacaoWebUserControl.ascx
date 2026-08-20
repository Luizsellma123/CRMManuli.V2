<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NegociacaoWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.NegociacaoWebUserControl" %>

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
                    <asp:LinkButton ID="HomeLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Home do Financeiro" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Home</asp:LinkButton>
                    
                    <asp:LinkButton ID="Simulacao" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="~Lista Negociações." CausesValidation="False"
                        data-rel="tooltip">Lista Negociações</asp:LinkButton>

                    <asp:LinkButton ID="NegociacaoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Nova Negociação." CausesValidation="False"
                        data-rel="tooltip" OnClick="NegociacaoLinkButton_Click">Nova Negociação</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


