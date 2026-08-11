<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="RelatorioAtendimentosWebForm.aspx.cs" Inherits="VendasWeb.Controladoria.RelatorioAtendimentosWebForm" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Src="~/usercontrol/WebUserControlControladoria.ascx" TagPrefix="uc1" TagName="WebUserControlControladoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

                    <h3 class="panel-title">Seleção de Dados</h3>
                </div>
                <div class="panel-body">
                    <!--Painel Aberto-->
                    <!-- END Painel Aberto-->
                    <!-- END Painel-->

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ClienteLabel" runat="server" CssClass="text-thin" Text="">Cliente:</asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ClienteTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LabelCondicao" runat="server" CssClass="text-thin" Text="">Vendedor:</asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:MultiView ID="VendedorMultView" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="VendedorView" runat="server">
                                        <select class="selectpicker show-tick controladoria" style="width: 239px;" multiple data-placeholder="Escolha um vendedor..."
                                            title="Escolha um vendedor..." data-style="btn-primary" data-live-search="true"
                                            id="VendedoresSelect" runat="server">
                                        </select>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LabelStatus" runat="server" CssClass="text-thin" Text="">Status Cliente:</asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataInicialLabel" runat="server" CssClass="text-thin" Text="">Data Inicial:</asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DataInicialTextBox" TextMode="Date" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDataInicial" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="DataInicialTextBox" ErrorMessage="Informe uma data inicial!"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataFinalLabel" runat="server" CssClass="text-thin" Text="">Data Final:</asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DataFinalTextBox" TextMode="Date" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDataFinal" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="DataFinalTextBox" ErrorMessage="Informe uma data final!"></asp:RequiredFieldValidator>
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
                        <div class="panel-control">
                            <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:LinkButton ID="RelatorioPassoButton" class="btn btn-primary btn-labeled fa fa-refresh fa-lg"
                                        runat="server" title="Próxima Tela" data-rel="tooltip" CausesValidation="true" OnClick="RelatorioPassoButton_Click"> 
                                Gerar Relatório
                                    </asp:LinkButton>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="RelatorioPassoButton" />
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
        <uc1:WebUserControlControladoria runat="server" ID="WebUserControlControladoria" />
    </div>
</asp:Content>
