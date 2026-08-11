<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmAbaFiscal.aspx.cs" Inherits="VendasWeb.Entidade.FrmAbaFiscal" %>


    <%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />


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
                    <div class="panel-control">
                        <!--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>-->
                    </div>
                    <h3 class="panel-title">
                        Fiscal</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel -->
                <!--===================================================-->
                <div class="table-responsive">
                    <div class="panel-body">

                    
                            <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
                        <br />

                        <div class="row">
                            <asp:MultiView ID="SuframaMultView" runat="server" ActiveViewIndex="0">
                                <asp:View ID="SuframaView" runat="server">
                                    <div class="col-xs-3 col-lg-3">
                                        <h5 class="text-bold">
                                            <asp:Label ID="SuframaLabel" runat="server" Text="Suframa Nº:" CssClass="text-thin"></asp:Label>
                                        </h5>
                                        <asp:TextBox ID="SuframaTextBox" runat="server" onkeypress="mascara( this, mnum );"
                                            Width="151px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="SuframaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <div class="col-xs-3 col-lg-4">
                                <h5 class="text-bold">
                                    <asp:Label ID="UserTipoTributacaoLabel" runat="server" Text="Tipo de Tributação :"
                                        CssClass="text-thin"></asp:Label></h5>
                                <asp:DropDownList ID="UserTipoTributacaoDropDownList" runat="server" CssClass="selectpicker show-tick" Width="60px" >
                                    <asp:ListItem Value="Real" Selected="True">Real</asp:ListItem>
                                    <asp:ListItem Value="Presumido">Presumido</asp:ListItem>
                                    <asp:ListItem Value="Simples">Simples</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-3 col-lg-4">
                                <h5 class="text-bold">
                                <asp:Label ID="UserSuspencaoIPILabel" runat="server" Text="Suspenção IPI :" CssClass="text-thin"></asp:Label></h5>
                                <asp:DropDownList ID="UserSuspencaoIPIDropDownList" runat="server" CssClass="selectpicker show-tick" Width="60px">
                                    <asp:ListItem Value="SIM">SIM</asp:ListItem>
                                    <asp:ListItem Value="NÃO" Selected="True">NÃO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-xs-3 col-lg-4">
                                <h5 class="text-bold">
                                    <asp:Label ID="UserDiferimentoICMSLabel" runat="server" Text="Diferimento ICMS :"
                                        CssClass="text-thin"></asp:Label></h5>
                                <asp:DropDownList ID="UserDiferimentoICMSDropDownList" runat="server" CssClass="selectpicker show-tick" Width="90px">
                                    <asp:ListItem Value="TOTAL">TOTAL</asp:ListItem>
                                    <asp:ListItem Value="PARCIAL">PARCIAL</asp:ListItem>
                                    <asp:ListItem Value="NENHUM" Selected="True">NENHUM</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-3 col-lg-4">
                                <h5 class="text-bold">
                                    <asp:Label ID="UserDiferimentoPISLabel" runat="server" Text="Diferimento PIS :" CssClass="text-thin"></asp:Label></h5>
                                <asp:DropDownList ID="UserDiferimentoPISDropDownList" runat="server" CssClass="selectpicker show-tick" Width="60px">
                                    <asp:ListItem Value="SIM">SIM</asp:ListItem>
                                    <asp:ListItem Value="NÃO" Selected="True">NÃO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-3 col-lg-4">
                                <h5 class="text-bold">
                                    <asp:Label ID="UserDiferimentoCOFINSLabel" runat="server" Text="Diferimento Cofins :"
                                        CssClass="text-thin"></asp:Label></h5>
                                <asp:DropDownList ID="UserDiferimentoCOFINSDropDownList" runat="server" CssClass="selectpicker show-tick" Width="60px">
                                    <asp:ListItem Value="SIM">SIM</asp:ListItem>
                                    <asp:ListItem Value="NÃO" Selected="True">NÃO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END Painel-->
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">


                        <asp:LinkButton ID="VoltarLinkButton" class="btn btn-warning btn-labeled fa fa-arrow-circle-left fa-lg" CausesValidation="false"
                                    runat="server" title="Voltar" data-rel="tooltip" OnClick="VoltarButton_Click"
                                    > 
             Retornar </asp:LinkButton>

                        <asp:LinkButton ID="ProximoPassoButton" class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg" CausesValidation="false"
                                    runat="server" title="Próxima Tela" data-rel="tooltip" OnClick="ProximoPasso_Click"
                                    > 
             Próximo </asp:LinkButton>


                          
                        </div>
                    </div>
                </div>
            </div>
            <!--===================================================-->
            <!--End Painel-->
            <!--===================================================-->
        </div>
        <!----PAINEL----->
        <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    </div>
</asp:Content>
