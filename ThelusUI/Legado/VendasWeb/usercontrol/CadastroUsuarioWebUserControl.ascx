<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroUsuarioWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.CadastroUsuarioWebUserControl" %>

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

                    <asp:LinkButton ID="EmpresasLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-user-plus fa-3x"
                        runat="server" title="Empresas do Usuário" CausesValidation="False"
                        data-rel="tooltip" OnClick="EmpresasLinkButton_Click">Empresas</asp:LinkButton>

                    <asp:LinkButton ID="VendedoresLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-user-plus fa-3x"
                        runat="server" title="Vendedores do Usuário" CausesValidation="False"
                        data-rel="tooltip" OnClick="VendedoresLinkButton_Click">Vendedores</asp:LinkButton>

                    <asp:LinkButton ID="SetoresLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x"
                        runat="server" title="Setores do Usuário" CausesValidation="False"
                        data-rel="tooltip" OnClick="SetoresLinkButton_Click">Setores</asp:LinkButton>

                    <asp:LinkButton ID="TiposVendedorLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-exchange fa-3x"
                        runat="server" title="Tipos de vendedor do Usuário" CausesValidation="False"
                        data-rel="tooltip" OnClick="TiposVendedorLinkButton_Click">Tipos Vendedor</asp:LinkButton>

                    <asp:LinkButton ID="GruposLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x"
                        runat="server" title="Grupos do Usuário" CausesValidation="False"
                        data-rel="tooltip" OnClick="GruposLinkButton_Click">Grupos</asp:LinkButton>

                    <asp:LinkButton ID="MenusLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x"
                        runat="server" title="Menus do Usuário" CausesValidation="False"
                        data-rel="tooltip" OnClick="MenusLinkButton_Click">Menus</asp:LinkButton>

                    <asp:LinkButton ID="SAPLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-user-o fa-3x"
                        runat="server" title="Usuário do SAP" CausesValidation="False"
                        data-rel="tooltip" OnClick="SAPLinkButton_Click">SAP</asp:LinkButton>                                      

                    <asp:LinkButton ID="AtualizarLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x"
                        runat="server" title="Atualizar Dados" CausesValidation="False"
                        data-rel="tooltip" OnClick="AtualizarLinkButton_Click">Atualizar Dados</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
