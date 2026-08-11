<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmSelecaoEmpresa.aspx.cs" Inherits="VendasWeb.Entidades.FrmSelecaoEmpresa" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />--%>
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

                    <h3 class="panel-title">Seleção da Empresa</h3>
                </div>
                <div class="panel-body">
                    <!--Painel Aberto-->
                    <!-- END Painel Aberto-->
                    <!-- END Painel-->

                    <br />
                    <br />
                    <h5>
                    <asp:Label ID="EmpresaLabel" runat="server" CssClass="text-thin" Text="">Empresa:</asp:Label></h5>
                    <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="selectpicker show-tick">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="EmpresaDropDownList" ErrorMessage="Selecione uma Empresa!"></asp:RequiredFieldValidator>
                    <br />
                    <br />

                </div>
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">



                            <asp:LinkButton ID="ProximoPassoButton" class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg"
                                runat="server" title="Próxima Tela" data-rel="tooltip" CausesValidation="true" OnClick="ProximoPassoButton_Click"> 
                                Avançar
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->
        <!----PAINEL----->
        <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
        </div>
</asp:Content>

