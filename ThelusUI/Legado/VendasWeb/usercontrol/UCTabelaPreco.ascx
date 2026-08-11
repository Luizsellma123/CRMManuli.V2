<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCTabelaPreco.ascx.cs" Inherits="VendasWeb.usercontrol.UCTabelaPreco" %>

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
                    <asp:LinkButton ID="HomeLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x"
                        runat="server" title="Principal" CausesValidation="False"
                        data-rel="tooltip" OnClick="HomeLinkButton_Click">Principal</asp:LinkButton>



                    <asp:LinkButton ID="EmpresaLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-industry fa-3x"
                        runat="server" title="Empresas" CausesValidation="False"
                        data-rel="tooltip" OnClick="EmpresaLinkButton_Click">Empresas</asp:LinkButton>


                    <asp:LinkButton ID="ProdutoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x"
                        runat="server" title="Produtos" CausesValidation="False"
                        data-rel="tooltip" OnClick="ProdutoLinkButton_Click">Produtos</asp:LinkButton>

                    <asp:LinkButton ID="LinkButtonAtualizar" class="btn btn-lg btn-block btn-info btn-labeled fa fa-refresh fa-3x"
                        runat="server" title="Enviar para Analise" CausesValidation="False" data-rel="tooltip"
                        OnClick="LinkButtonAtualizar_Click"> Atualizar Dados  </asp:LinkButton>



                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" runat="server">
    <ProgressTemplate>
        <div style="text-align: center;">
            <img src="<%=Page.ResolveClientUrl("~/imagens/gif_aguarde3.gif")%>" style="vertical-align: middle" alt="Processing" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<ajaxToolkit:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="PainelUpdatePanel"
    BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" X="-3"
    Y="-3" />

