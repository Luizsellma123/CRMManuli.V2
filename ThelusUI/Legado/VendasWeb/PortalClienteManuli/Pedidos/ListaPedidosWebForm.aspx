<%@ Page Title="" Language="C#" MasterPageFile="~/PortalCliente.Master" AutoEventWireup="true" CodeBehind="ListaPedidosWebForm.aspx.cs" Inherits="VendasWeb.PortalClienteManuli.Pedidos.ListaPedidosWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">

    <div class="card">

        <div class="card-body">
            <h4 class="card-title">Lista de Pedidos</h4>
            <p class="card-description">Confira os pedidos da empresa.</p>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Empresa:</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Razão Social:</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="RazaoSocialDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Pedido:</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="PedidoTextBox" runat="server" CssClass="form-control" placeholder="Digite o número pedido."
                                onkeypress="mascara( this, mnum );"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Período:</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="DataInicialTextBox" runat="server" CssClass="form-control"
                                onkeypress="mascara( this, mdata );" type="date"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="DataFinalTextBox" runat="server" type="date" CssClass="form-control"
                                onkeypress="mascara( this, mdata );"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group row">
                        <label class="col-sm-3 col-form-label">Situação:</label>
                        <div class="col-sm-4">
                            <div class="form-check">
                                <label class="form-check-label">
                                    <asp:RadioButton ID="FaturadosRadioButton" runat="server" GroupName="membershipRadios" />
                                    Faturados
                                    <i class="input-helper"></i>
                                </label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-check">
                                <label class="form-check-label">
                                    <asp:RadioButton ID="TodosRadioButton" runat="server" GroupName="membershipRadios" Checked="true" />
                                    Todos
                              <i class="input-helper"></i>
                                </label>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group row">
                        <div class="col-sm-9">
                            <!--Botões de controle-->
                            <asp:Button ID="BuscarButton" runat="server" Text="Buscar Pedidos" CssClass="btn btn-primary mr-2" OnClick="BuscarButton_Click" />
                            <asp:Button ID="NovoPedidoButton" runat="server" Text="Novo Pedido" CssClass="btn btn-primary mr-2" OnClick="NovoPedidoButton_Click" />
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="table-responsive">
                    <asp:UpdatePanel ID="UpPedidosClientes" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridViewPedidosClientes" runat="server"
                                CssClass="table table-striped table-bordered" GridLines="None" HeaderStyle-Font-Size="Medium"
                                AutoGenerateColumns="False" OnPageIndexChanging="GridViewPedidosClientes_PageIndexChanging"
                                PageSize="5" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpCod" SortExpression="EmpCod" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpCodLabel" runat="server" Text='<%# Bind("EmpCod") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Número" SortExpression="EmpCod">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaNumLabel" runat="server" Text='<%# Bind("PedVendaNum") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome Cliente" SortExpression="EmpCod">
                                        <ItemTemplate>
                                            <asp:Label ID="EntNomeLabel" runat="server" Text='<%# Bind("EntNome") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data Pedido" SortExpression="EmpCod">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaDataLabel" runat="server" Text='<%# Bind("PedVendaData") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nota Fiscal" SortExpression="EmpCod">
                                        <ItemTemplate>
                                            <asp:Label ID="NFnumLabel" runat="server" Text='<%# Bind("NFnum") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Número OC" SortExpression="EmpCod">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaNumPedEntLabel" runat="server" Text='<%# Bind("PedVendaNumPedEnt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status" SortExpression="EmpCod">
                                        <ItemTemplate>
                                            <asp:Label ID="pedvendastatdescrLabel" runat="server" Text='<%# Bind("pedvendastatdescr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Pedido" SortExpression="EmpCod">
                                        <ItemTemplate>
                                            <asp:Label ID="PedVendaValTotalLabel" runat="server" Text='<%# string.Format("{0:C}", Eval("PedVendaValTotal")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Consulta">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonConsulta" runat="server" CssClass="btn btn-outline-info btn-icon-text" OnClick="LinkButtonConsulta_Click">
                                                            <i class="mdi mdi-image-filter-none btn-icon-prepend"></i>
                                                            Detalhes
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridViewPedidosClientes" EventName="PageIndexChanging" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

    <script src="<%=Page.ResolveClientUrl("~/PortalClienteManuli/js/formpickers.js")%>"></script>
</asp:Content>
