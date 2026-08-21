<%@ page title="" language="C#" masterpagefile="~/NestedMasterPageCRM.master" autoeventwireup="true" codebehind="NegociacaoDetalheItensWebForm.aspx.cs" inherits="VendasWeb.Negociacao.NegociacaoDetalheItensWebForm" %>

<%@ register src="~/usercontrol/NegociacaoDetalheWebUserControl.ascx" tagprefix="uc1" tagname="FinanceiroWebUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../js/LiberacaoPedidosWebFormJS.js" type="text/javascript"></script>

    <script type="text/javascript">
        function calcularDesconto() {
            var txtExSimulador = document.getElementById('<%= txtExSimulador.ClientID %>');
            var txtSolicitado = document.getElementById('<%= txtSolicitado.ClientID %>');
            var txtDesconto = document.getElementById('<%= txtDesconto.ClientID %>');

            if (!txtExSimulador || !txtSolicitado || !txtDesconto) return;

            var valSimulador = parseFloat(txtExSimulador.value.replace(/\./g, '').replace(',', '.')) || 0;
            var valSolicitado = parseFloat(txtSolicitado.value.replace(/\./g, '').replace(',', '.')) || 0;

            if (valSimulador > 0 && valSolicitado > 0) {
                var percDesconto = (1 - (valSolicitado / valSimulador)) * 100;
                if (percDesconto < 0) percDesconto = 0;

                txtDesconto.value = percDesconto.toFixed(2).replace('.', ',') + '%';
            } else {
                txtDesconto.value = '0,00%';
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-9">
            <!--===================================================-->
            <!-- Painel Principal de Cadastro de Itens -->
            <!--===================================================-->
            <div class="panel panel-info">
                <!-- Panel heading -->
                <div class="panel-heading">
                    <div class="panel-control">
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                    </div>
                    <h3 class="panel-title">Negociação - Itens</h3>
                </div>

                <!-- Literal abre a <div id='filtros'> -->
                <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse in' aria-expanded='true'>" runat="server"></asp:Literal>

                <asp:UpdatePanel ID="updFormulario" runat="server" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="panel-body">

                            <!-- LINHA 1: Empresa -->
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Empresa:" AssociatedControlID="drpEmpresa"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <asp:DropDownList ID="drpEmpresa" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <!-- LINHA 2: Negociação / Situação -->
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Negociação:" AssociatedControlID="txtNegociacao"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNegociacao" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Situação:" AssociatedControlID="drpSituacao"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="drpSituacao" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <!-- LINHA 3: Produto -->
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Produto:" AssociatedControlID="drpProduto"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <asp:DropDownList ID="drpProduto" runat="server" CssClass="form-control selectpicker"
                                            data-live-search="true" data-style="btn-primary" title="Selecione um produto...">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <!-- LINHA 4: Quantidade / Ex. Simulador -->
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Quantidade:" AssociatedControlID="txtQuantidade"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtQuantidade" runat="server" CssClass="form-control" placeholder="0"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblExSimulador" runat="server" Text="Ex. Simulador:" AssociatedControlID="txtExSimulador"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtExSimulador" runat="server" CssClass="form-control" placeholder="0,00" onkeyup="calcularDesconto();"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <!-- LINHA 5: Solicitado / Desconto (%) -->
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Solicitado:" AssociatedControlID="txtSolicitado"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSolicitado" runat="server" CssClass="form-control" placeholder="0,00" onkeyup="calcularDesconto();"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Desconto (%):" AssociatedControlID="txtDesconto"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtDesconto" runat="server" CssClass="form-control" ReadOnly="true" placeholder="0,00%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </contenttemplate>
                </asp:UpdatePanel>

                <!-- Panel Footer de Ação do Item -->
                <div class="panel-footer text-right">
                    <asp:UpdatePanel ID="updBotoesItem" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                        <contenttemplate>
                            <asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                CausesValidation="false" runat="server" OnClick="RetornarLinkButton_Click">
                                Retornar</asp:LinkButton>

                            <asp:LinkButton ID="btnAdicionarItem" class="btn btn-warning btn-labeled fa fa-plus fa-lg"
                                runat="server" OnClick="btnAdicionarItem_Click">
                                Adicionar</asp:LinkButton>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
            <!-- Fecha a <div id='filtros'> iniciada no Literal -->
        </div>
        <!-- Fecha a <div class="panel panel-info"> -->

        <!--===================================================-->
        <!-- GridView de Itens Inseridos -->
        <!--===================================================-->
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:UpdatePanel ID="updGridItens" runat="server" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="table-responsive">
                            <asp:GridView ID="gridItensNegociacao" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-hover table-striped table-bordered"
                                DataKeyNames="IDItem"
                                OnRowDeleting="gridItensNegociacao_RowDeleting"
                                EmptyDataText="Nenhum item adicionado à negociação.">

                                <columns>
                                    <asp:TemplateField HeaderText="Ex." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                        <%-- Botão Excluir Padronizado --%>
                                        <itemtemplate>
                                            <asp:LinkButton ID="btnExcluirItem" runat="server"
                                                CommandName="Delete"
                                                CssClass="btn btn-primary fa fa-times-circle"
                                                ToolTip="Excluir Item">
                                            </asp:LinkButton>
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="DescricaoProduto" HeaderText="Produto" />
                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="QuantidadeConvertida" HeaderText="Convertida" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />

                                    <asp:BoundField DataField="ExSimuladorFinal" HeaderText="Ex Simulador" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="ExSimuladorFinalM2" HeaderText="Ex Simulador M2" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="ExSolicitado" HeaderText="Solicitado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="SolicitadoM2" HeaderText="Solicitado M2" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />

                                    <asp:BoundField DataField="PercentualDesconto" HeaderText="Desconto (%)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" />
                                </columns>
                            </asp:GridView>
                        </div>
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
    <!-- Fecha a col-sm-9 -->

    <!-- Menu Lateral UserControl -->
    <uc1:financeirowebusercontrol runat="server" id="NegociacaoDetalheWebUserControl" />

    </div>
    <!-- Fecha a row principal -->
</asp:Content>
