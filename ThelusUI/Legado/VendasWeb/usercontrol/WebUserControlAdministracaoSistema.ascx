<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlAdministracaoSistema.ascx.cs" Inherits="VendasWeb.usercontrol.WebUserControlAdministracaoSistema" %>

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
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False" data-rel="tooltip"
                        OnClick="HomeLinkButton_Click">Home</asp:LinkButton>

                    <asp:LinkButton ID="LinkButtonAtualizar" class="btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x"
                        runat="server" title="Enviar para Analise" CausesValidation="False" data-rel="tooltip"
                        OnClick="LinkButtonAtualizar_Click">Atualizar Dados</asp:LinkButton>

                    <asp:LinkButton ID="CadastroUsuarioLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="CadastroUsuarioLinkButton_Click">Cadastro Usuário</asp:LinkButton>

                    <asp:LinkButton ID="RestartPoolCRMAPILinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x"
                        runat="server" title="Enviar para Analise" CausesValidation="False" data-rel="tooltip"
                        OnClick="RestartPoolCRMAPILinkButton_Click">Reiniciar CRMAPI</asp:LinkButton>

                    <asp:LinkButton ID="CadatroGruposLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="CadatroGruposLinkButton_Click">Cadastro Grupos</asp:LinkButton>

                    <asp:LinkButton ID="CadastroMenusLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="CadastroMenusLinkButton_Click">Cadastro Menus</asp:LinkButton>

                    <asp:LinkButton ID="CadastroSetoresLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="CadastroSetoresLinkButton_Click">Cadastro Setores</asp:LinkButton>

                    <asp:LinkButton ID="ModulosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x"
                        runat="server" title="Módulos" CausesValidation="False"
                        data-rel="tooltip" OnClick="ModulosLinkButton_Click">Módulos</asp:LinkButton>

                    <asp:LinkButton ID="ParametrosGeraisLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x"
                        runat="server" title="Parámetros Gerais" CausesValidation="False"
                        data-rel="tooltip" OnClick="ParametrosGeraisLinkButton_Click">Parâmetros Gerais</asp:LinkButton>

                    <asp:LinkButton ID="AtualizarStoredProceduresLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x"
                        runat="server" title="Atualizar Stored Procedures" CausesValidation="False" data-rel="tooltip"
                        OnClick="AtualizarStoredProceduresLinkButton_Click">Atualizar Stored Procedures</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
