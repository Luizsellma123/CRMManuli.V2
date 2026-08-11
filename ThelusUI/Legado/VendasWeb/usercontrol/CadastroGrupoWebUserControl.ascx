<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroGrupoWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.CadastroGrupoWebUserControl" %>

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
                        runat="server" title="Dados Principais" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="UsuariosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        runat="server" title="Usuarios" CausesValidation="False"
                        data-rel="tooltip" OnClick="UsuariosLinkButton_Click">Usuários</asp:LinkButton>

                    <asp:LinkButton ID="MenusLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        runat="server" title="Menus" CausesValidation="False"
                        data-rel="tooltip" OnClick="MenusLinkButton_Click">Menus</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
