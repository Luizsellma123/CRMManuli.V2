<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlChamados.ascx.cs" Inherits="VendasWeb.usercontrol.WebUserControlChamados" %>

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
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip">Home</asp:LinkButton>

                    <asp:LinkButton ID="NovoChamadoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x"
                        runat="server" title="Novo Chamado" OnClick="NovoChamadoLinkButton_Click" CausesValidation="False" data-rel="tooltip"> Novo Chamado  </asp:LinkButton>

                    <asp:LinkButton ID="ChamadosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x"
                        runat="server" title="Meus Chamados" CausesValidation="False" data-rel="tooltip" OnClick="ChamadosLinkButton_Click"> Chamados  </asp:LinkButton>

                  <%--  <asp:LinkButton ID="GerenciarChamadosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-cog fa-3x"
                        runat="server" title="Gerenciar Chamados" CausesValidation="False" Visible="false"
                        data-rel="tooltip">Gerenciar Chamados</asp:LinkButton>

                    <asp:LinkButton ID="SuporteChamadosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-cog fa-3x"
                        runat="server" title="Suporte Chamados" CausesValidation="False" 
                        data-rel="tooltip" Visible="false">Suporte Chamados</asp:LinkButton>--%>

                    <asp:LinkButton ID="ImportarChamadosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-upload fa-3x"
                        runat="server" title="Importar Chamados" CausesValidation="False" Visible="false"
                        OnClick="ImportarChamadosLinkButton_Click"
                        data-rel="tooltip">Importar Chamados</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
