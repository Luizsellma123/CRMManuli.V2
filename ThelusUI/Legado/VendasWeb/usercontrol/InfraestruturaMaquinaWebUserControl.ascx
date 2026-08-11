<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfraestruturaMaquinaWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.InfraestruturaMaquinaWebUserControl" %>

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

                    <asp:LinkButton ID="InformacoesGeraisLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-info-circle fa-3x"
                        runat="server" title="Informações Gerais" OnClick="InformacoesGeraisLinkButton_Click" CausesValidation="False"
                        data-rel="tooltip">Informações Gerais</asp:LinkButton>

                    <asp:LinkButton ID="RAMLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-hdd-o fa-3x"
                        runat="server" title="RAM" OnClick="RAMLinkButton_Click" CausesValidation="False"
                        data-rel="tooltip">RAM</asp:LinkButton>

                    <asp:LinkButton ID="DiscosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-hdd-o fa-3x"
                        runat="server" title="Discos" OnClick="DiscosLinkButton_Click" CausesValidation="False"
                        data-rel="tooltip">Discos</asp:LinkButton>

                    <asp:LinkButton ID="ProcessosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-tachometer fa-3x"
                        runat="server" title="Processos" OnClick="ProcessosLinkButton_Click" CausesValidation="False"
                        data-rel="tooltip">Processos</asp:LinkButton>

                    <asp:LinkButton ID="ProgramasLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-cogs fa-3x"
                        runat="server" title="Programas" OnClick="ProgramasLinkButton_Click" CausesValidation="False"
                        data-rel="tooltip">Programas</asp:LinkButton>

                    <asp:LinkButton ID="EmailLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-envelope fa-3x"
                        runat="server" title="Email" OnClick="EmailLinkButton_Click" CausesValidation="False"
                        data-rel="tooltip">Email</asp:LinkButton>


                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
