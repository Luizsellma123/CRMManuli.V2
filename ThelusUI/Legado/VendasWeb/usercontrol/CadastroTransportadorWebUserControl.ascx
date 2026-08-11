<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroTransportadorWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.CadastroTransportadorWebUserControl" %>

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

                    <asp:LinkButton ID="FornecedorLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-truck fa-3x"
                        OnClick="FornecedorLinkButton_Click">Fornecedor</asp:LinkButton>

                    <asp:LinkButton ID="RegiaoLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-map-marker fa-3x"
                        OnClick="RegiaoLinkButton_Click">Região</asp:LinkButton>

                    <asp:LinkButton ID="ParametrosLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-cogs fa-3x"
                        OnClick="ParametrosLinkButton_Click">Parâmetros</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
