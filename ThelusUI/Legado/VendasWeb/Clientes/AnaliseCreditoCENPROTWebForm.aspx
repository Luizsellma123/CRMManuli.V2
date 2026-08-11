<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="AnaliseCreditoCENPROTWebForm.aspx.cs" Inherits="VendasWeb.Clientes.AnaliseCreditoCENPROTWebForm" %>

<%@ Register Src="~/usercontrol/AnaliseCreditoDetalheWebUserControl.ascx" TagPrefix="uc1" TagName="AnaliseCreditoDetalheWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/bootstrap-filestyle.min.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/AnaliseCreditoCENPROTJavaScript.js?aux=8")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(":file").filestyle({ buttonName: "btn-primary" });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="IDClienteHiddenField" runat="server" />

    <asp:HiddenField ID="IDAnaliseHiddenField" runat="server" />

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
                    <h3 class="panel-title">Cadastro Cliente - Análise Crédito - CENPROT</h3>
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

                    <%-- Detalhes principais --%>
                    <div>

                        <%-- Análise, Data --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Análise:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="AnaliseTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Data:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="DataTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Código, Nome --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Código:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="CodigoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Nome:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox ID="NomeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%-- Fantasia --%>
                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Fantasia:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="FantasiaTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
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

                        <div class="col-sm-auto">

                            <asp:UpdatePanel ID="AprovarUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <%--<asp:LinkButton ID="CertificadoLinkButton" class="btn btn-warning btn-labeled fa fa-certificate fa-lg"
                                        CausesValidation="false" runat="server" OnClientClick="AnalisePedido(1);">Certificado</asp:LinkButton>--%>

                                    <asp:LinkButton ID="BuscarLinkButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                        OnClientClick="showProgress();" runat="server"
                                        OnClick="BuscarLinkButton_Click">Buscar</asp:LinkButton>

                                    <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                        CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="CertificadoLinkButton" />--%>
                                    <asp:PostBackTrigger ControlID="BuscarLinkButton" />
                                    <asp:PostBackTrigger ControlID="RetornarLinkButton" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="CENPROTMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="CENPROTView" runat="server">
                <div class="panel">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Lista Grupos
                        </h3>
                    </div>--%>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="CENPROTGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação"
                                AutoGenerateColumns="False" runat="server" AllowPaging="True" Style="border-collapse: collapse; max-width: 100%"
                                OnPageIndexChanging="CENPROTGridView_PageIndexChanging" Visible="true"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>

                                    <asp:TemplateField HeaderText="IDCliente" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDClienteLabel" runat="server" Text='<%# Bind("IDCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDAnalise" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDAnaliseLabel" runat="server" Text='<%# Bind("IDAnalise") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDCartorio" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDCartorioLabel" runat="server" Text='<%# Bind("IDCartorio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="UF" DataField="UF" HeaderStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Cartório" DataField="Cartorio" />

                                    <asp:BoundField HeaderText="Cidade" DataField="Cidade" />

                                    <asp:BoundField HeaderText="Qtde. Títulos" DataField="QtdeTitulos" />

                                    <asp:BoundField HeaderText="Municipio" DataField="Municipio" />

                                    <asp:BoundField HeaderText="Valor" DataField="Valor" />

                                    <asp:TemplateField HeaderText="Detalhes" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <center>
                                                <asp:UpdatePanel ID="DetalhesUpdatePanel" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="DetalhesLinkButton" class="btn btn-primary fa fa-plus-square" CausesValidation="false" runat="server"
                                                            OnClientClick='<%# string.Format("ConsultaCENPROTProtestos("+Eval("IDCliente")+","+Eval("IDAnalise")+","+Eval("IDCartorio")+")")%>'></asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="DetalhesLinkButton" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
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

    <div id="CertificadoDiv">

        <div id="CertificadoModal" class="modal fade bd-example-modal-xl">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header" style="margin-top: 15px;">
                        <h4 id="CertificadoModalTitle" class="modal-title"><strong>Certificado</strong></h4>
                    </div>

                    <div id="CertificadoModalBody" class="modal-body">

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Certificado:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="CertificadoTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Data Validade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="DataValidadeTextBox" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Senha:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <div class="form-group">
                                    <asp:TextBox ID="SenhaTextBox" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label ID="ArquivoLabel" runat="server" Text="Arquivo:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-10">
                                <asp:FileUpload CssClass="filestyle" data-buttonName="btn-primary" ID="ArquivoFileUpload" runat="server" TabIndex="-1" Style="position: absolute; clip: rect(0px, 0px, 0px, 0px);" />
                            </div>

                        </div>

                    </div>

                    <div class="modal-footer">

                        <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:LinkButton ID="AtualizarModalLinkButton"
                                    class="btn btn-success btn-labeled fa fa-refresh fa-lg"
                                    runat="server" OnClick="AtualizarModalLinkButton_Click">Atualizar</asp:LinkButton>

                                <asp:LinkButton runat="server"
                                    class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                    data-dismiss="modal">Retornar</asp:LinkButton>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="AtualizarModalLinkButton" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>

                </div>
            </div>
        </div>

    </div>

    <div id="ProtestosDiv">

        <div id="CENPROTProtestosModal" class="modal fade bd-example-modal-xl">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">

                    <div class="modal-header" style="margin-top: 15px;">
                        <h4 id="CENPROTProtestosModalTitle" class="modal-title" style="color: black;">Análise Crédito - CENPROT - Protestos</h4>
                    </div>

                    <div id="CENPROTProtestosModalBody" class="modal-body">

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Cartório:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="CartorioModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Código:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="CodigoModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row" visible="false">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Endereço:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="EnderecoModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Telefone:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TelefoneModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Cidade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="CidadeModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Bairro:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="BairroModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Quantidade:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="QuantidadeModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Total:"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TotalModalTextBox" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-2">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Protestos:"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-sm-12">
                                <div id="DivProtestosModal"></div>
                            </div>

                        </div>

                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                    </div>

                </div>
            </div>
        </div>

    </div>

    <uc1:AnaliseCreditoDetalheWebUserControl runat="server" ID="AnaliseCreditoDetalheWebUserControl" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>     
    
</asp:Content>
