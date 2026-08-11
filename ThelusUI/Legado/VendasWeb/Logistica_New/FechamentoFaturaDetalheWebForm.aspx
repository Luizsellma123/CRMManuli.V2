<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="FechamentoFaturaDetalheWebForm.aspx.cs" Inherits="VendasWeb.Logistica_New.FechamentoFaturaDetalheWebForm" %>

<%@ Register Src="~/usercontrol/FechamentoFaturaWebUserControl.ascx" TagPrefix="uc1" TagName="FechamentoFaturaWebUserControl" %>

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

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:DropDownList ID="EmpresaDropDownList" runat="server" Enabled="false" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <asp:HiddenField ID="IDEmpresaHiddenField" runat="server" />

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Status:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Fechamento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="FechamentoTextBox" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <asp:HiddenField ID="IDFechamentoHiddenField" runat="server" />

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="CNPJ:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="CNPJTextBox" runat="server" CssClass="form-control"
                                    onkeypress="mascara( this, cnpj );" onblur="mascara( this, cnpj );" onfocus="mascara( this, cnpj );">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Vencimento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="VencimentoTextBox" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Data:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="DataTextBox" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Valor Fatura:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="ValorFaturaTextBox" runat="server" CssClass="form-control" AutoPostBack="true"
                                    onkeypress="mascara( this, moeda );" onblur="mascara( this, moeda );" onfocus="mascara( this, moeda );"
                                    OnTextChanged="CarregaDiferenca"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Identificado:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="IdentificadoTextBox" runat="server" CssClass="form-control" Enabled="false">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Diferença:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="DiferencaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Fatura:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox ID="FaturaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Usuário:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="UsuarioDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
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

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                <asp:LinkButton ID="CancelarLinkButton" class="btn btn-danger btn-labeled fa fa-ban fa-lg" Enabled="false"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="CancelarLinkButton_Click">Cancelar</asp:LinkButton>

                                <asp:LinkButton ID="LimparDadosLinkButton" class="btn btn-danger btn-labeled fa fa-times fa-lg" Enabled="false"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="LimparDadosLinkButton_Click">Limpar Dados</asp:LinkButton>

                                <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success btn-labeled fa fa-floppy-o fa-lg"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="SalvarLinkButton_Click">Salvar</asp:LinkButton>

                                <asp:LinkButton ID="EnviarSAPLinkButton" class="btn btn-primary btn-labeled fa fa-sort-amount-asc fa-lg" Enabled="false"
                                    CausesValidation="false" runat="server" OnClientClick="showProgress();" OnClick="EnviarSAPLinkButton_Click">Enviar SAP</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                <asp:PostBackTrigger ControlID="CancelarLinkButton" />
                                <asp:PostBackTrigger ControlID="LimparDadosLinkButton" />
                                <asp:PostBackTrigger ControlID="SalvarLinkButton" />
                                <asp:PostBackTrigger ControlID="EnviarSAPLinkButton" />
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
                                                            CausesValidation="false" runat="server" OnClick="ExcluirGridViewLinkButton_Click">
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
