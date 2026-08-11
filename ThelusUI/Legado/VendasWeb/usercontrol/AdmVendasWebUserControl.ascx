<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdmVendasWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.AdmVendasWebUserControl" %>

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
                        runat="server" title="Home" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Home</asp:LinkButton>

                    <asp:LinkButton ID="LiberaPedidoProducaoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-folder-open fa-3x"
                        runat="server" title="Liberar Pedidos da Produção" CausesValidation="False"
                        data-rel="tooltip" OnClick="LiberaPedidoProducaoLinkButton_Click">Liberar Pedidos da Produção</asp:LinkButton>

                    <asp:LinkButton ID="TabelaPrecoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-table fa-3x"
                        runat="server" title="Tabela de Preço" CausesValidation="False"
                        data-rel="tooltip" OnClick="TabelaPrecoLinkButton_Click">Tabela de Preço</asp:LinkButton>

                    <asp:LinkButton ID="ClassificacaoComercialLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-table fa-3x"
                        runat="server" title="Classificação Comercial" CausesValidation="False"
                        data-rel="tooltip" OnClick="ClassificacaoComercialLinkButton_Click">Classificação Comercial</asp:LinkButton>

                    <asp:LinkButton ID="PrazosProduçãoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-table fa-3x"
                        runat="server" title="Novo Botão" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrazosProduçãoLinkButton_Click">Prazos Produção Grupos</asp:LinkButton>


                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
