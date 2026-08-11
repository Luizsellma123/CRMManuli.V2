<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroParametroGeralWebForm.aspx.cs" Inherits="VendasWeb.AdministracaoSistema.CadastroParametroGeralWebForm" %>

<%@ Register Src="~/usercontrol/WebUserControlAdministracaoSistema.ascx" TagPrefix="uc1" TagName="WebUserControlAdministracaoSistema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-sm-9">
            <!--===================================================-->
            <!--Painel Carteiras e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <%--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>--%>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--<button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>--%>
                    </div>
                    <h3 class="panel-title">Cadastro - Parâmetro Gerais</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse' aria-expanded='false' style='height: 0px;'>"
                    runat="server"></asp:Literal>
                <div class="panel-body">

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Empresa:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ParametroLabel" runat="server" Text="Parâmetro:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ParametroTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ModuloLabel" runat="server" Text="Módulo:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="ModuloDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DescricaoLabel" runat="server" Text="Descrição:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="DescricaoTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ValoresLabel" runat="server" Text="Valores:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <hr />
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TextoLabel" runat="server" Text="Texto:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="TextoTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NumericoLabel" runat="server" Text="Numérico:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="NumericoTextBox" runat="server"
                                        onkeypress="mascara( this, mnumEvirgula );" onblur="mascara( this, mnumEvirgula );" onfocus="mascara( this, mnumEvirgula );"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

                <!--===================================================-->

                <!-- END LINHA 1 - Painel FILTROS-->
            </div>
            <!-- 
            </div> -->

            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:UpdatePanel ID="FooterUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                    CausesValidation="false" runat="server" OnClick="SalvarLinkButton_Click">Salvar</asp:LinkButton>

                                <asp:LinkButton ID="RetornarButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="SalvarLinkButton" />
                                <asp:PostBackTrigger ControlID="RetornarButton" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>

    </div>

    <uc1:WebUserControlAdministracaoSistema runat="server" ID="WebUserControlAdministracaoSistema" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
