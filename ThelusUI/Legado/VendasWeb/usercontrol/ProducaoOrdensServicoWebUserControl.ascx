<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProducaoOrdensServicoWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.ProducaoOrdensServicoWebUserControl" %>

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
                        runat="server" title="Principal de Ordens Serviço" CausesValidation="False"
                        data-rel="tooltip" OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="IncluirProdutosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-plus-square fa-3x"
                        runat="server" title="Incluir Produtos" CausesValidation="False"
                        data-rel="tooltip" OnClick="IncluirProdutosLinkButton_Click">Incluir Produtos</asp:LinkButton>

                    <asp:LinkButton ID="EditarProdutosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x"
                        runat="server" title="Editar Produtos" CausesValidation="False"
                        data-rel="tooltip" OnClick="EditarProdutosLinkButton_Click">Editar Produtos</asp:LinkButton>

                    <asp:LinkButton ID="OrdensProducaoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-industry fa-3x"
                        runat="server" title="Ordens Produção" CausesValidation="False"
                        data-rel="tooltip" OnClick="OrdensProducaoLinkButton_Click">Ordens Produção</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
