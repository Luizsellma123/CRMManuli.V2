<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="lstTransportadora.aspx.cs" Inherits="VendasWeb.listas.lstTransportadora" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>
<%@ Register Src="../usercontrol/cabecarioPedido.ascx" TagName="cabecarioPedido" TagPrefix="uc1" %>
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

                    <h3 class="panel-title">Seleção da Transportadora</h3>
                </div>
                <div class="panel-body">
                    <!--Painel Aberto-->
                    <!-- END Painel Aberto-->
                    <!-- END Painel-->
                    <uc1:cabecarioPedido ID="cabecarioPedido1" runat="server" />

                    <!-- Div cabeçario -->
                    <div id="row">
                        <br />
                        <div class="col-sm-3">
                            <!--Filtro Nome/Codigo -->
                            <%--<h5>
                                <asp:Label ID="lblEntidade" runat="server" Text="Entidade:" CssClass="texto"></asp:Label></h5>--%>
                            <asp:DropDownList ID="drpEntCod" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1" Selected="True">Nome</asp:ListItem>
                                <asp:ListItem Value="2">Código</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-3">
                            <asp:TextBox ID="txtFiltroEntCod" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-success btn-labeled fa fa-table fa-lg"
                                OnClick="btnListar_Click">Listar</asp:LinkButton>
                        </div>
                    </div>

                    <!-- Tabela montada dinamicamente -->
                    <div id="lst_dad">
                        <asp:Literal ID="ltlListaEntidade" runat="server"></asp:Literal>
                    </div>


                </div>
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                            <div id="botomnav">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btnAnt"
                                    OnClick="LinkButton1_Click"><img src="../imagens/back.png" alt="<< Anterior" border="0" /></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btnProx"
                                    OnClick="LinkButton2_Click"><img src="../imagens/next.png" alt="Próximo >>" border="0" /></asp:LinkButton>
                            </div>
                        <div class="panel-control">
                            <!-- TextBox utilizados para trabalhar a paginação -->
                            <!-- Botões para navegação -->
                            <span>
                                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox></span>
                            <span>
                                <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox></span>
                            <span>
                                <asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->
        <!----PAINEL----->
        <ucp:ControlPainel ID="ControlPainel" runat="server" updatemode="Conditional" runat="server" />
    </div>
</asp:Content>
