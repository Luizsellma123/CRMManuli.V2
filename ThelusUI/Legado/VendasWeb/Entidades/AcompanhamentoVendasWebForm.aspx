<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AcompanhamentoVendasWebForm.aspx.cs" Inherits="VendasWeb.Entidades.AcompanhamentoVendasWebForm" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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

                    <h3 class="panel-title">Seleção de Dados</h3>
                </div>
                <div class="panel-body">
                    <!--Painel Aberto-->
                    <!-- END Painel Aberto-->
                    <!-- END Painel-->

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EmpresaLabel" runat="server" CssClass="text-thin" Text="">Empresa:</asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="EmpresaDropDownList" ErrorMessage="Selecione uma Empresa!"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LabelCondicao" runat="server" CssClass="text-thin" Text="">Seleção:</asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="SelecaoDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True">Todos</asp:ListItem>
                                    <asp:ListItem>Pendentes</asp:ListItem>
                                    <asp:ListItem>Faturados</asp:ListItem>
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

                        <div class="col-sm-3">
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

                        <div class="col-sm-3">
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



                            <asp:LinkButton ID="RelatorioPassoButton" class="btn btn-primary btn-labeled fa fa-refresh fa-lg"
                                runat="server" title="Próxima Tela" data-rel="tooltip" CausesValidation="true" OnClick="RelatorioPassoButton_Click"> 
                                Gerar Relatório
                            </asp:LinkButton>
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
