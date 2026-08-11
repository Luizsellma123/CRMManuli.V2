<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PosicaoDiariaWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.PosicaoDiariaWebUserControl" %>

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
                        runat="server" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Home</asp:LinkButton>

                    <asp:LinkButton ID="FaturadosLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x"
                        runat="server" CausesValidation="False"
                        data-rel="tooltip" OnClick="FaturadosLinkButton_Click">Faturados</asp:LinkButton>

                    <asp:LinkButton ID="PendentesLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x"
                        runat="server" CausesValidation="False"
                        data-rel="tooltip" OnClick="PendentesLinkButton_Click">Pendentes</asp:LinkButton>

                    <asp:LinkButton ID="DevolucoesLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x"
                        runat="server" CausesValidation="False"
                        data-rel="tooltip" OnClick="DevolucoesLinkButton_Click">Devoluções</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
