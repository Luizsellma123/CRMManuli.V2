<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmRelCalendario.aspx.cs" Inherits="VendasWeb.telasRelatorio.FrmRelCalendario" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Inicia Js Para Footable--%>
    <%--<script type="text/javascript" src="../template/footable/js/footable.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <%--Fim Js Para Footable--%>
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
                        <button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                    <h3 class="panel-title">
                        Relatorio Calendario</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->
                <div id="painel_aberto" class="">
                    <div class="panel-body">
                        <!--LINHA 1 - Painel Aberto-->
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <select class="selectpicker show-tick" multiple data-placeholder="Escolha um Usuario..."
                                        title="Escolha um Usuario..." data-style="btn-primary" data-live-search="true"
                                        id="UsuarioSelect" runat="server">
                                    </select>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <select class="selectpicker show-tick" multiple data-placeholder="Escolha um Tipo de Agendamento..."
                                        title="Escolha um Tipo de Agendamento..." data-style="btn-primary" data-live-search="true"
                                        id="TipoAgendamentoSelect" runat="server">
                                    </select>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="drpEntCod" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">NOME FANTASIA</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">RAZÃO SOCIAL</asp:ListItem>
                                        <asp:ListItem Value="3">CÓD.ENTIDADE</asp:ListItem>
                                        <asp:ListItem Value="4">CNPJ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtFiltro" runat="server" placeholder="Digite aqui..." class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                     <h5>
                                <label>
                                    Data Inicial:</label></h5>
                            <asp:TextBox class="" ID="DataITextBox" TextMode="Date" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <h5>
                                <label>
                                    Data Final:</label></h5>
                            <asp:TextBox class="" ID="DataFTextBox" TextMode="Date" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                          
                        <!--END LINHA 1 - Painel Aberto-->
                        <!--===================================================-->
                    </div>
                </div>
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>
                <div class="panel-body">
                </div>
            </div>
            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="btnListar" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Buscar" data-rel="tooltip" OnClick="btnListar_Click"
                            CausesValidation="False"> 
             Buscar </asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="CancelarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-left fa-lg"
                            runat="server" title="Cancelar" data-rel="tooltip" CausesValidation="False" OnClick="CancelarLinkButton_Click"> 
             Cancelar </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--===================================================-->
        <!--End Painel Carteiras e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="RelatorioMultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="RelatorioView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Resultado
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" Font-Names="Verdana"
                                Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                                WaitMessageFont-Size="14pt" Height="600px">
                                <LocalReport ReportPath="relatorios\RptRelCalendario.rdlc">
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </div>
                    </div>
                    <!--===================================================-->
                </div>
                <!-- End Foo Table - Filtering -->
                <!--===================================================-->
                <!-- END TABELA -->
            </asp:View>
        </asp:MultiView>
    </div>
    <!----PAINEL----->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
