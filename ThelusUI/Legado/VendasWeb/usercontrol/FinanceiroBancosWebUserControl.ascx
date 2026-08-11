<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FinanceiroBancosWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.FinanceiroBancosWebUserControl" %>

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

                    <asp:LinkButton ID="NovoBancoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Cadastro de Bancos." CausesValidation="False"
                        data-rel="tooltip">Bancos</asp:LinkButton>

                    <asp:LinkButton ID="AgenciasBancariasLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Cadastro de agências bancárias." CausesValidation="False"
                        data-rel="tooltip">Agências</asp:LinkButton>
                    
                    <asp:LinkButton ID="ContaCorrenteLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Cadastro de conta corrente." CausesValidation="False"
                        data-rel="tooltip">Contas</asp:LinkButton>
                    
                    <asp:LinkButton ID="NovaRemessaBancariaLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x disabled"
                        runat="server" title="Remessas Do Banco." CausesValidation="False"
                        data-rel="tooltip" OnClick="NovaRemessaBancariaLinkButton_Click">Nova Remessa</asp:LinkButton>

                    <asp:LinkButton ID="RemessasBancariasLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x"
                        runat="server" title="Remessas Do Banco." CausesValidation="False"
                        data-rel="tooltip" OnClick="RemessasBancariasLinkButton_Click">Remessas Bancárias</asp:LinkButton>

                    <asp:LinkButton ID="RetornoBancarioLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x disabled"
                        runat="server" title="Retorno Bancario." CausesValidation="False"
                        data-rel="tooltip" Visible="false">Retorno Bancário</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
