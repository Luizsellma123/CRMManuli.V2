<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrazoProducaoProdutosWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.PrazoProducaoProdutosWebUserControl" %>

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
                        runat="server" title="Principal" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="CarregaPrazosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-clock-o fa-3x"
                        runat="server" title="Carrega Prazos" CausesValidation="False"
                        data-rel="tooltip" OnClick="CarregaPrazosLinkButton_Click">Carrega Prazos</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
