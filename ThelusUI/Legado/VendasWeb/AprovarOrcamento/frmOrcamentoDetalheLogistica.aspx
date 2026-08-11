<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="frmOrcamentoDetalheLogistica.aspx.cs" Inherits="VendasWeb.AprovarOrcamento.frmOrcamentoDetalheLogistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- LINHA 1-->
    <div class="row">

        <div class="col-sm-12">
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
                    <h3 class="panel-title">Libera Pedido Detalhe</h3>
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
                    <!-- LINHA 1 - Painel FILTROS-->
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 class="text-bold">Dados do Orçamento
                            </h5>
                            <hr>
                        </div>
                    </div>
                    <!-- LINHA 1 - Painel FILTROS-->
                    <!--===================================================-->

                    <div class="row">

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LblEmpresa" runat="server" Text="Empresa :"></asp:Label></h5>
                                <asp:Label ID="EmpresaLabel" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="HiddenFieldEmpCod" runat="server" />
                            </div>
                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LblSituacao" runat="server" Text="Situação :"></asp:Label></h5>
                                <asp:Label ID="SituacaoLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label7" runat="server" Text="Pedido :"></asp:Label></h5>
                                <asp:Label ID="PedVendaNumLabel" runat="server" Text=""></asp:Label>


                            </div>
                        </div>



                        <div class="col-sm-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label8" runat="server" Text="Status :"></asp:Label></h5>
                                <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>


                            </div>
                        </div>


                    </div>

                    <div class="row">


                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelEntidade" runat="server" Text="Entidade :"></asp:Label></h5>
                                <asp:Label ID="EntidadeLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>



                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label9" runat="server" Text="Natureza :"></asp:Label></h5>
                                <asp:Label ID="NaturezaLabel" runat="server" Text=""></asp:Label>


                            </div>
                        </div>



                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="PrevisaoLabel" runat="server" Text="Previsao :"></asp:Label></h5>
                                <asp:Label ID="PreisaoLabel" runat="server" Text=""></asp:Label>


                            </div>
                        </div>



                        <div class="col-sm-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label1" runat="server" Text="Estado :"></asp:Label></h5>
                                <asp:Label ID="EstadoLabel" runat="server" Text=""></asp:Label>


                            </div>
                        </div>



                    </div>

                    <div class="row">

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label10" runat="server" Text="Aprovação :"></asp:Label></h5>
                                <asp:Label ID="AprovacaoLabel" runat="server" Text=""></asp:Label>


                            </div>
                        </div>





                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label11" runat="server" Text="Alçada :"></asp:Label></h5>
                                <asp:Label ID="AlcadaPrincipalLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label12" runat="server" Text="Concluido :"></asp:Label></h5>
                                <asp:Label ID="ConcluidoLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>


                        <div class="col-sm-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label13" runat="server" Text="Vendedor :"></asp:Label></h5>
                                <asp:Label ID="VendedorLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>


                    </div>



                    <div class="row">

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label14" runat="server" Text="Aprovações :"></asp:Label></h5>

                                <asp:CheckBox ID="AlcadaSupervisorCheckBox" runat="server" Enabled="false"></asp:CheckBox>
                                <asp:Label ID="Label2" runat="server" Text="Supervisor"></asp:Label>
                                &nbsp;&nbsp;
                                
                              
                                <%--<asp:CheckBox ID="AlcadaRegionalCheckBox" runat="server" Enabled="false"></asp:CheckBox>--%>
                                <%--<asp:Label ID="Label3" runat="server" Text="Regional"></asp:Label>--%>

                                 &nbsp;&nbsp; 
                                <asp:CheckBox ID="AlcadaControladoriaCheckBox" runat="server" Enabled="false"></asp:CheckBox>
                                <asp:Label ID="Label4" runat="server" Text="Controladoria"></asp:Label>

                                <%-- &nbsp;&nbsp; --%>
                                <%-- <asp:CheckBox ID="AlcadaDiretoriaCheckBox" runat="server" Enabled="false"></asp:CheckBox> --%>
                                <%-- <asp:Label ID="Label5" runat="server" Text="Diretoria"></asp:Label> --%>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelLogistica" runat="server" Text="Logistica :"></asp:Label></h5>
                                <asp:Label ID="LabelTextoLogistica" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelTotal" runat="server" Text="Total:"></asp:Label></h5>
                                <asp:Label ID="LabelTextoTotal" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="HiddenFieldTotal" runat="server" />
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelFrete" runat="server" Text="Pagador Frete:"></asp:Label></h5>
                                <asp:Label ID="LabelPagadorFrete" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>

                    <hr />

                    <div class="row">

                        <div class="col-sm-5">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label15" runat="server" Text="Historico :"></asp:Label></h5>

                                <asp:TextBox ID="HistoricoTextBox" Width="400px" Height="90px" TextMode="MultiLine" runat="server" Enabled="false"></asp:TextBox>


                            </div>
                        </div>


                        <div class="col-sm-5">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="NovoHistoricoLabel" runat="server" Text="Novo Historico :"></asp:Label></h5>

                                <asp:TextBox ID="NovoHistoricoTextBox" Width="400px" Height="90px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19"
                                    runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="NovoHistoricoTextBox" ErrorMessage="* Informe um Historico!"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>

                    </div>

                    <hr />


                    <div class="row">
                        <div class="col-lg-5">
                            <h5>
                                <asp:Label ID="LabelTranportadora" runat="server" Text="Tranportadora:"></asp:Label></h5>
                            <asp:MultiView ID="TransportadoraMultView" runat="server" ActiveViewIndex="0">
                                <asp:View ID="TransportadoraView" runat="server">
                                    <div class="col-lg-10">
                                        <select class="selectpicker show-tick" data-placeholder="Escolha um vendedor..."
                                            title="Escolha a Transportadora..." data-style="btn-primary" data-live-search="true"
                                            id="transportadoraSelect" runat="server">
                                        </select>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                        <div class="col-lg-10">
                            <h5>
                                <asp:Label ID="LabelLocalEmbarque" runat="server" Text="Local Embarque:"></asp:Label></h5>

                            <asp:RadioButtonList ID="RadioButtonListLocalEmbarque" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Curitiba">&nbsp;&nbsp;Curitiba&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="São Paulo">&nbsp;&nbsp;São Paulo&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="Manaus">&nbsp;&nbsp;Manaus&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19"
                                runat="server" Display="Dynamic" SetFocusOnError="True" ValidationGroup="cotacao"
                                ControlToValidate="RadioButtonListLocalEmbarque" ErrorMessage="* Informe o local de embarque !"></asp:RequiredFieldValidator>

                        </div>

                        <div class="col-lg-6">
                            <h5>
                                <asp:Label ID="LabelQuantidadeProdutos" runat="server" Text="Quantidade Volumes:"></asp:Label></h5>
                            <div class="form-group">
                                <asp:TextBox ID="textoQuantidadeProdutos" runat="server" placeholder="Informe a quantidade de volumes." class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="textoQuantidadeProdutos" ErrorMessage="* Informe a quantidade de volumes!" ValidationGroup="cotacao"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <h5>
                                <asp:Label ID="LabelPesoBruto" runat="server" Text="Peso Bruto:"></asp:Label></h5>
                            <div class="form-group">
                                <asp:TextBox ID="TextBoxPesoBruto" runat="server" placeholder="Informe qual o peso bruto do pedido." class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="TextBoxPesoBruto" ErrorMessage="* Informe o peso bruto do pedido!" ValidationGroup="cotacao"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="updPainelValor" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="col-lg-6">
                                    <h5>
                                        <asp:Label ID="LabelValorFrete" runat="server" Text="Valor Frete:"></asp:Label></h5>
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBoxValorFrete" AutoPostBack="True" OnTextChanged="TextBoxValorFrete_TextChanged" runat="server" placeholder="Informe qual o valor do frete." class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                            runat="server" Display="Dynamic" SetFocusOnError="True"
                                            ControlToValidate="TextBoxValorFrete" ErrorMessage="* Informe o valor do frete!" ValidationGroup="aprovar"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <h5>
                                        <asp:Label ID="LabelPercentualFrete" runat="server" Text="Percentual Frete:"></asp:Label></h5>
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBoxPercentualFrete" AutoPostBack="True" runat="server" placeholder="Informe qual o percentual sobre o pedido, referente ao frete." class="form-control" OnTextChanged="TextBoxPercentualFrete_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                            runat="server" Display="Dynamic" SetFocusOnError="True"
                                            ControlToValidate="TextBoxPercentualFrete" ErrorMessage="* Informe o percentual do frete!" ValidationGroup="aprovar"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="TextBoxValorFrete" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>


                    <!--===================================================-->
                    <!-- END LINHA 1 - Painel FILTROS-->
                </div>




                <!-- END Painel FILTROS-->
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- Botões de buscar e limpar-->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">

                            <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">Retornar</asp:LinkButton>

                            <asp:LinkButton ID="LinkButtonSolicitarCotacao" ValidationGroup="cotacao" class="btn btn-success btn-labeled fa fa-shopping-cart fa-lg"
                                runat="server" OnClick="LinkButtonSolicitarCotacao_Click">Cotar Frete</asp:LinkButton>

                            <asp:LinkButton ID="AprovarLinkButton" ValidationGroup="aprovar" class="btn btn-success btn-labeled fa fa-check fa-lg"
                                runat="server" OnClick="AprovarLinkButton_Click">Aprovar</asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
        </div>


        <asp:MultiView ID="MultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="View" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lista Produtos
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="ItemGridView" EmptyDataText="Nenhum Item Localizado" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="ItemGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse;">
                                <PagerStyle CssClass="pagination-ys" />

                                <Columns>


                                    <asp:TemplateField HeaderText="Cod. Produto">
                                        <ItemTemplate>
                                            <asp:Label ID="ProdcodestrLabel" runat="server" Text='<%# Bind("Prodcodestr") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Produto">
                                        <ItemTemplate>
                                            <asp:Label ID="ProdNomeLabel" runat="server" Text='<%# Bind("ProdNome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ex ICM">
                                        <ItemTemplate>
                                            R$
                                            <asp:Label ID="PrecoTabelaExicmLabel" runat="server" Text='<%# Bind("PrecoTabelaExicm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit.">
                                        <ItemTemplate>
                                            R$
                                            <asp:Label ID="ItPedVendaValUnitLabel" runat="server" Text='<%# Bind("ItPedVendaValUnit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="C/Icm">
                                        <ItemTemplate>
                                            R$
                                            <asp:Label ID="PrecoTabelaOriginalLabel" runat="server" Text='<%# Bind("PrecoTabelaOriginal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Desc.">
                                        <ItemTemplate>
                                            <asp:Label ID="PercentualLabel" runat="server" Text='<%# Bind("Percentual") %>'></asp:Label>&nbsp;%

                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Quantidade">
                                        <ItemTemplate>
                                            <asp:Label ID="ItPedVendaQtdLabel" runat="server" Text='<%# Bind("ItPedVendaQtd") %>'></asp:Label>
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
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>
