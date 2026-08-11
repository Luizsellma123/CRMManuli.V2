<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="FrmOrcamentoDetalhe.aspx.cs" Inherits="VendasWeb.AprovarOrcamento.FrmOrcamentoDetalhe" %>



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

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="NatOpTextoLabel" runat="server" Text="Destinação :"></asp:Label></h5>
                                <asp:Label ID="NatOpLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label13" runat="server" Text="Vendedor :"></asp:Label></h5>
                                <asp:Label ID="VendedorLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelInscricaoEstadual" runat="server" Text="Incrição Estadual :"></asp:Label></h5>
                                <asp:Label ID="InscricaoEstadualLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelTextoEstado" runat="server" Text="Estado :"></asp:Label></h5>
                                <asp:Label ID="EstadoLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelTextoCidade" runat="server" Text="Cidade :"></asp:Label></h5>
                                <asp:Label ID="CidadeLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelPagamento" runat="server" Text="Condição Pagamento :"></asp:Label></h5>
                                <asp:Label ID="CondicaoPagamentoLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>

                        <%--<div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelPrazoMedio" runat="server" Text="Prazo Médio :"></asp:Label></h5>
                                <asp:Label ID="PrazoMedioLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>--%>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelPrazoMedio" runat="server" Text="Classificação Com.:"></asp:Label>
                                </h5>
                                <asp:Label ID="ClassificacaoComercialLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelLimiteDisponivel" runat="server" Text="Limite Disponível:"></asp:Label></h5>
                                <asp:Label ID="LimiteDisponivelLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelLimiteCredito" runat="server" Text="Limite Crédito:"></asp:Label></h5>
                                <asp:Label ID="LimiteCreditoLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>


                    </div>

                    <div class="row">

                        <div class="col-sm-4" runat="server" visible="false">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label10" runat="server" Text="Aprovação :"></asp:Label></h5>
                                <asp:Label ID="AprovacaoLabel" runat="server" Text=""></asp:Label>


                            </div>
                        </div>





                        <div class="col-sm-2" runat="server" visible="false">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label11" runat="server" Text="Alçada :"></asp:Label></h5>
                                <asp:Label ID="AlcadaPrincipalLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2" runat="server" visible="false">
                            <h5>
                                <asp:Label ID="Label12" runat="server" Text="Concluido :"></asp:Label></h5>
                            <asp:Label ID="ConcluidoLabel" runat="server" Text=""></asp:Label>
                        </div>
                    </div>










                    <div class="row">

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelTotalPedido" runat="server" Text="Total Pedido :"></asp:Label></h5>
                                <asp:Label ID="LabelValorTotalPedido" runat="server" Text=""></asp:Label>
                            </div>
                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelValorFrete" runat="server" Text="Total Frete :"></asp:Label></h5>
                                <asp:Label ID="LabelTotalFrete" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelPercentualFrete" runat="server" Text="Percentual:"></asp:Label></h5>
                                <asp:Label ID="LabelPercentualValorFrete" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelTransportadora" runat="server" Text="Transportadora :"></asp:Label></h5>
                                <asp:Label ID="LabelTextoTransportadora" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-sm-2" runat="server" visible="false">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelLogistica" runat="server" Text="Logistica :"></asp:Label></h5>
                                <asp:Label ID="LabelTextoLogistica" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2" runat="server" visible="false">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelPesoBruto" runat="server" Text="Peso Bruto :"></asp:Label></h5>
                                <asp:Label ID="LabelTextoPesoBruto" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2" runat="server" visible="false">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelQuantidadeVolumes" runat="server" Text="Volumes :"></asp:Label></h5>
                                <asp:Label ID="LabelTextoQuantidadeVolumes" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

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
                                    <asp:Label ID="LabelFrete" runat="server" Text="Frete :"></asp:Label></h5>
                                <asp:Label ID="LabelFreteTexto" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelOrigem" runat="server" Text="Embarque :"></asp:Label></h5>
                                <asp:Label ID="LabelTextoOrigem" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="LabelEnquadramentoTributario" runat="server" Text="Enq. Tributario :"></asp:Label></h5>
                                <asp:Label ID="EnquadramentoTributarioLabel" runat="server" Text=""></asp:Label>

                            </div>
                        </div>

                    </div>
                    <div class="row">
                    </div>

                    <hr />

                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="Label15" runat="server" Text="Historico :"></asp:Label></h5>

                                <asp:TextBox ID="HistoricoTextBox" Width="506px" Height="90px" TextMode="MultiLine" runat="server" Enabled="false"></asp:TextBox>


                            </div>
                        </div>


                        <div class="col-sm-6">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="NovoHistoricoLabel" runat="server" Text="Novo Historico :" Visible="false"></asp:Label></h5>

                                <asp:TextBox ID="NovoHistoricoTextBox" Width="506px" Height="90px" Visible="false" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19"
                                    runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="NovoHistoricoTextBox" ErrorMessage="* Informe um Historico!"></asp:RequiredFieldValidator>

                            </div>
                        </div>



                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <h5>
                                    <asp:Label ID="HistoricoPedidoLabel" runat="server" Text="Historico Pedido :"></asp:Label>
                                </h5>
                                <asp:TextBox ID="HistoricoPedidoTextBox" Width="100%" Height="90px" TextMode="MultiLine" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
                <!--===================================================-->
                <!-- END LINHA 1 - Painel FILTROS-->





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

                            

                            <asp:LinkButton ID="AprovarLinkButton" class="btn btn-success btn-labeled fa fa-check fa-lg"
                                runat="server" OnClick="AprovarButton_Click" Visible="false">Aprovar</asp:LinkButton>


                            <asp:LinkButton ID="ReprovarLinkButton" class="btn btn-warning btn-labeled fa fa-close  fa-lg"
                                runat="server" OnClick="ReprovarButton_Click" Visible="false">Reprovar</asp:LinkButton>

                            <asp:LinkButton ID="RetornarVendedor" class="btn btn-warning btn-labeled fa fa-refresh  fa-lg"
                                runat="server" Visible="false" OnClick="RetornarVendedor_Click">Retornar Vendedor</asp:LinkButton>

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

                                    <asp:TemplateField HeaderText="Ex ICM" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            R$
                                            <asp:Label ID="PrecoTabelaExicmLabel" runat="server" Text='<%# Bind("PrecoTabelaExicm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit." ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            R$
                                            <asp:Label ID="ItPedVendaValUnitLabel" runat="server" Text='<%# Bind("ItPedVendaValUnit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="C/Icm" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            R$
                                            <asp:Label ID="PrecoTabelaOriginalLabel" runat="server" Text='<%# Bind("PrecoTabelaOriginal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Desc." ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="PercentualLabel" runat="server" Text='<%# Bind("Percentual") %>'></asp:Label>&nbsp;%
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="ItPedVendaQtdLabel" runat="server" Text='<%# Bind("ItPedVendaQtd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Estoque" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="EstoqueGridViewLabel" runat="server" Text='<%# Bind("Estoque") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Empenho" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpenhoGridViewLabel" runat="server" Text='<%# Bind("Empenho") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Saldo Estq." ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="SaldoEstqGridViewLabel" runat="server" Text='<%# Bind("SaldoEstq") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="M. Contribuição" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="MargemContribuicaoLabel" runat="server" Text='<%# Bind("MargemContribuicao") %>'></asp:Label>
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
