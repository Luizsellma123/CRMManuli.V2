<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FinanceiroBancosRemessasWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.FinanceiroBancosRemessasWebUserControl" %>

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

                    <asp:LinkButton ID="TitulosBancoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x disabled"
                        runat="server" title="Editar títulos de Bancos." CausesValidation="False"
                        data-rel="tooltip">Editar Títulos</asp:LinkButton>

                    <asp:LinkButton ID="AdicionarTitulosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x disabled"
                        runat="server" title="Gerar remessa bancária." CausesValidation="False"
                        data-rel="tooltip">Adicionar Títulos</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

