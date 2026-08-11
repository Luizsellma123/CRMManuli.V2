<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContaCorrenteWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.ContaCorrenteWebUserControl" %>

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
                        runat="server" title="Principal do Conta Corrente" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="ContasReceberLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-plus-square fa-3x"
                        runat="server" title="Contas Receber" CausesValidation="False"
                        data-rel="tooltip" OnClick="ContasReceberLinkButton_Click">Contas Receber</asp:LinkButton>

                    <asp:LinkButton ID="ContasPagarLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-minus-square fa-3x"
                        runat="server" title="Contas Pagar" CausesValidation="False"
                        data-rel="tooltip" OnClick="ContasPagarLinkButton_Click">Contas Pagar</asp:LinkButton>

                    <asp:LinkButton ID="DevolucoesLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-reply-all fa-3x"
                        runat="server" title="Devoluções" CausesValidation="False"
                        data-rel="tooltip" OnClick="DevolucoesLinkButton_Click">Devoluções</asp:LinkButton>

                    <asp:LinkButton ID="PedidosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-share-square fa-3x"
                        runat="server" title="Pedidos" CausesValidation="False"
                        data-rel="tooltip" OnClick="PedidosLinkButton_Click">Pedidos</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
