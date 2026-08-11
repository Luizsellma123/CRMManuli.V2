<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnaliseCreditoDetalheWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.AnaliseCreditoDetalheWebUserControl" %>

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

                    <asp:LinkButton ID="PrincipalLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="PrincipalLinkButton_Click">Principal</asp:LinkButton>

                    <asp:LinkButton ID="ScoreSerasaLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-line-chart fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="ScoreSerasaLinkButton_Click">Score Serasa</asp:LinkButton>

                    <asp:LinkButton ID="GrafiasSemelhantesLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-clone fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="GrafiasSemelhantesLinkButton_Click">Grafias Semelhantes</asp:LinkButton>

                    <asp:LinkButton ID="QuadroSociosLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="QuadroSociosLinkButton_Click">Quadro Socios</asp:LinkButton>

                    <asp:LinkButton ID="AdministracaoLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-cogs fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="AdministracaoLinkButton_Click">Administração</asp:LinkButton>

                    <asp:LinkButton ID="ConsultaSerasaLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-search fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="ConsultaSerasaLinkButton_Click">Consulta Serasa</asp:LinkButton>

                    <asp:LinkButton ID="HistoricoPagamentosLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-history fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="HistoricoPagamentosLinkButton_Click">Histórico Pagamentos</asp:LinkButton>

                    <asp:LinkButton ID="EvolucaoCompromissosLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-line-chart fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="EvolucaoCompromissosLinkButton_Click">Evolução Compromissos</asp:LinkButton>

                    <asp:LinkButton ID="ReferenciaisDeNegociosLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-building fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="ReferenciaisDeNegociosLinkButton_Click">Referenciais de Negócios</asp:LinkButton>

                    <asp:LinkButton ID="AnotacoesNegativasLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-exclamation-triangle fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="AnotacoesNegativasLinkButton_Click">Anotações Negativas</asp:LinkButton>

                    <asp:LinkButton ID="CENPROTLinkButton"
                        class="btn btn-lg btn-block btn-info btn-labeled fa fa-check-square fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="CENPROTLinkButton_Click">CENPROT</asp:LinkButton>

                    <asp:LinkButton ID="AnaliseCreditoLinkButton"
                        class="btn btn-lg btn-block btn-danger btn-labeled fa fa-arrow-circle-left fa-3x"
                        runat="server" CausesValidation="False" data-rel="tooltip"
                        OnClick="AnaliseCreditoLinkButton_Click">Análise Crédito</asp:LinkButton>

                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
