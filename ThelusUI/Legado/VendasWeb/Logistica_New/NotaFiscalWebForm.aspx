<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="NotaFiscalWebForm.aspx.cs" Inherits="VendasWeb.Logistica_New.NotaFiscalWebForm" %>

<%@ Register Src="~/usercontrol/FechamentoFaturaWebUserControl.ascx" TagPrefix="uc1" TagName="FechamentoFaturaWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/bootstrap-filestyle.min.js")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(":file").filestyle({ buttonName: "btn-primary" });
    </script>
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
                    <h3 class="panel-title">Fechamento - Fatura Principal</h3>
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
                                <asp:Label runat="server" Text="Empresa:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDownList" runat="server" Enabled="false" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Fechamento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="FechamentoTextBox" TextMode="Number" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ArquivoLabel" runat="server" Text="Arquivo:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-10">
                            <asp:FileUpload CssClass="filestyle" data-buttonName="btn-primary" ID="ArquivoFileUpload" runat="server" TabIndex="-1" Style="position: absolute; clip: rect(0px, 0px, 0px, 0px);" />
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

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="SubirDadosLinkButton" class="btn btn-primary btn-labeled fa fa-cloud-upload fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="SubirDadosLinkButton_Click">Subir Dados</asp:LinkButton>

                                <asp:LinkButton ID="LimparDadosLinkButton" class="btn btn-danger btn-labeled fa fa-times fa-lg" Enabled="false"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="LimparDadosLinkButton_Click">Limpar Dados</asp:LinkButton>

                                <asp:LinkButton ID="ModeloLinkButton" class="btn btn-warning btn-labeled fa fa-table fa-lg"
                                    CausesValidation="false" runat="server" OnClick="ModeloLinkButton_Click">Modelo</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                <asp:PostBackTrigger ControlID="SubirDadosLinkButton" />
                                <asp:PostBackTrigger ControlID="LimparDadosLinkButton" />
                                <asp:PostBackTrigger ControlID="ModeloLinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>

        <!-- TABELA -->
        <!--===================================================-->
        <asp:MultiView ID="MultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="View" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:UpdatePanel ID="TesteUpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="GridView" EmptyDataText="Não foi possível encontrar nenhuma simulação"
                                        AutoGenerateColumns="False" OnPageIndexChanging="GridView_PageIndexChanging" Visible="true"
                                        runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%"
                                        CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head
                                         table-no-inner-border table-hover table-condensed">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Exc.">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:LinkButton ID="ExcluirGridViewLinkButton" class="btn btn-danger fa fa-times"
                                                            CausesValidation="false" runat="server" OnClick="ExcluirGridViewLinkButton_Click1">
                                                        </asp:LinkButton>
                                                    </center>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <asp:Label ID="EmpresaGridViewLabel" runat="server" Text='<%# Bind("Empresa") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IDNota" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="IDNotaGridViewLabel" runat="server" Text='<%# Bind("IDNota") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nota Fiscal">
                                                <ItemTemplate>
                                                    <asp:Label ID="NotaFiscalGridViewLabel" runat="server" Text='<%# Bind("NotaFiscal") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:Label ID="ValorGridViewLabel" runat="server" Text='<%# String.Format("{0:0.00}", Convert.ToDouble(Eval("Valor"))) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Identificado">
                                                <ItemTemplate>
                                                    <asp:Label ID="IdentificadoGridViewLabel" runat="server" Text='<%# Bind("Identificado") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Importado">
                                                <ItemTemplate>
                                                    <asp:Label ID="ImportadoGridViewLabel" runat="server" Text='<%# Bind("Importado") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="GridView" />
                                </Triggers>
                            </asp:UpdatePanel>

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

    <uc1:FechamentoFaturaWebUserControl runat="server" ID="FechamentoFaturaWebUserControl" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div> 
</asp:Content>
