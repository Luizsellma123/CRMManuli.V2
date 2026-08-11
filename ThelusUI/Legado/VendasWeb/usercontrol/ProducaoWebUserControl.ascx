<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProducaoWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.ProducaoWebUserControl" %>

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
                        runat="server" title="Home de Produção" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Home</asp:LinkButton>

                    <asp:LinkButton ID="OrdensDeServicoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-folder-open fa-3x"
                        runat="server" title="Ordens de Serviço" CausesValidation="False"
                        data-rel="tooltip" OnClick="OrdensDeServicoLinkButton_Click">Ordens de Serviço</asp:LinkButton>

                    <asp:LinkButton ID="ProdutosRelacionaisLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-random fa-3x"
                        runat="server" title="Produtos Relacionais" CausesValidation="False"
                        data-rel="tooltip" OnClick="ProdutosRelacionaisLinkButton_Click">Produtos Relacionais</asp:LinkButton>

                    <asp:LinkButton ID="StatusOrdemServicoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-info-circle fa-3x"
                        runat="server" title="Status Ordem Serviço" CausesValidation="False"
                        data-rel="tooltip" OnClick="StatusOrdemServicoLinkButton_Click">Status Ordem Serviço</asp:LinkButton>

                    <asp:LinkButton ID="PrioridadeOrdensServicoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-product-hunt fa-3x"
                        runat="server" title="Prioridade Ordens Serviço" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrioridadeOrdensServicoLinkButton_Click">Prioridade Ordens Serviço</asp:LinkButton>

                    <asp:LinkButton ID="PrazoProducaoGruposLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-clock-o fa-3x"
                        runat="server" title="Prazo Produção" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrazoProducaoGruposLinkButton_Click">Prazo Produção Grupos</asp:LinkButton>

                    <asp:LinkButton ID="PrazoProducaoProdutosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-clock-o fa-3x"
                        runat="server" title="Prazo Produção" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrazoProducaoProdutosLinkButton_Click">Prazo Produção Produtos</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
