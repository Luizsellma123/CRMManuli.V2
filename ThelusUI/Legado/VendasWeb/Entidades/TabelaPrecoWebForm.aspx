<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="TabelaPrecoWebForm.aspx.cs" Inherits="VendasWeb.Entidades.TabelaPrecoWebForm" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        #ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_CrystalReportViewer1__UI {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-9">
            <!--===================================================-->
            <!--Painel Carteiras e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">

                    <h3 class="panel-title">Seleção de Dados - Tabela Preço</h3>
                </div>
                <div class="panel-body">
                    <!--Painel Aberto-->
                    <!-- END Painel Aberto-->
                    <!-- END Painel-->

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TabelaPrecoLabel" runat="server" CssClass="text-thin" Text="">Tabela Preço:</asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:DropDownList ID="TabelaDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="TabelaDropDownList" ErrorMessage="Selecione uma Empresa!"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                </div>
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="panel-control">
                                    <asp:LinkButton ID="RelatorioTabelaButton" class="btn btn-primary btn-labeled fa fa-refresh fa-lg"
                                        runat="server" title="Próxima Tela" data-rel="tooltip" CausesValidation="true" OnClick="RelatorioTabelaButton_Click"> 
                                Gerar Relatório
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="LinkButton" class="btn btn-primary btn-labeled fa fa-arrow-circle-down fa-lg"
                                        runat="server" title="Próxima Tela" data-rel="tooltip" CausesValidation="true" OnClick="LinkButton_Click"> 
                                Tabela PDF
                                    </asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RelatorioTabelaButton" />
                                <asp:PostBackTrigger ControlID="LinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                </div>
            </div>

            <div class="table-responsive">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
            </div>

        </div>

        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->
        <!----PAINEL----->
        <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    </div>
</asp:Content>
