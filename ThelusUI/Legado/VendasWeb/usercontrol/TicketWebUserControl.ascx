<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.TicketWebUserControl" %>

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
                        runat="server" title="Principal" CausesValidation="False" Enabled="true"
                        data-rel="tooltip" OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="ContatosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x"
                        runat="server" title="Contatos" CausesValidation="False" Enabled="true"
                        data-rel="tooltip" OnClick="ContatosLinkButton_Click">Contatos</asp:LinkButton>

                    <asp:LinkButton ID="AtividadesLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x"
                        runat="server" title="Atividades" CausesValidation="False" Enabled="true"
                        data-rel="tooltip" OnClick="AtividadesLinkButton_Click">Atividades</asp:LinkButton>

                    <asp:LinkButton ID="HistoricoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-edit fa-3x"
                        runat="server" title="Historico" CausesValidation="False" Enabled="true"
                        data-rel="tooltip" OnClick="HistoricoLinkButton_Click">Historico</asp:LinkButton>

                    <asp:LinkButton ID="AnexoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x"
                        runat="server" title="Anexo" CausesValidation="False" Enabled="true"
                        data-rel="tooltip" OnClick="AnexoLinkButton_Click">Anexo</asp:LinkButton>

                            <asp:LinkButton ID="NotasFiscaisLinkButton" runat="server" CausesValidation="False" data-rel="tooltip"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x" Enabled="true"
                        OnClick="NotasFiscaisLinkButton_Click">Notas Fiscais</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
