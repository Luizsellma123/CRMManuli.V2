<%@ Page Title="" Language="C#" MasterPageFile="~/PortalCliente.Master" AutoEventWireup="true" CodeBehind="DetalhesPedidosWebForm.aspx.cs" Inherits="VendasWeb.PortalClienteManuli.Pedidos.DetalhesPedidosWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div class="col-md-12 grid-margin stretch-card">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Detalhe do Pedido</h4>
                <p class="card-description">Confira os dados do pedido abaixo!</p>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Empresa:</label>
                            <div class="col-sm-9">
                                <asp:Label ID="EmpresaLabel" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Pedido:</label>
                            <div class="col-sm-8">
                                <asp:Label ID="PedidoLabel" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Razão Social:</label>
                            <div class="col-sm-9">
                                <asp:Label ID="NomeEntidadeLabel" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Situação:</label>
                            <div class="col-sm-8">
                                <asp:Label ID="PedVendaStatDescrLabel" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Pagamento:</label>
                            <div class="col-sm-9">
                                <asp:Label ID="CondPagamentoLabel" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Total Pedido:</label>
                            <div class="col-sm-8">
                                <asp:Label ID="TotalPedidoLabel" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpItemPedidosClientes" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewItemPedidosClientes" runat="server"
                                    CssClass="table table-striped table-bordered" GridLines="None" HeaderStyle-Font-Size="Medium"
                                    AutoGenerateColumns="False" OnPageIndexChanging="GridViewItemPedidosClientes_PageIndexChanging"
                                    PageSize="5" AllowPaging="True">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Produto">
                                            <ItemTemplate>
                                                <asp:Label ID="ProdutoLabel" runat="server" Text='<%# Bind("Produto") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantidade:" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="ItPedVendaQtdLabel" runat="server" Text='<%# String.Format( "{0:0.00}" , Eval("ItPedVendaQtd")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unidade" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="ItPedVendaUnidMedCodDataLabel" runat="server" Text='<%# Bind("ItPedVendaUnidMedCod") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Valor Unitário" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="ItPedVendaValUnitLabel" runat="server" Text='<%# string.Format("{0:C}", Eval("ItPedVendaValUnit")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Sem IPI" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="TotalSemImpostosLabel" runat="server" Text='<%# string.Format("{0:C}", Eval("TotalSemImpostos")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="ItPedVendaValFinalLabel" runat="server" Text='<%# string.Format("{0:C}", Eval("ItPedVendaValFinal")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridViewItemPedidosClientes" EventName="PageIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
