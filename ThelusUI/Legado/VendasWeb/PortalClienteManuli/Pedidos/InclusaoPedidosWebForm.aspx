<%@ Page Title="" Language="C#" MasterPageFile="~/PortalCliente.Master" AutoEventWireup="true" CodeBehind="InclusaoPedidosWebForm.aspx.cs" Inherits="VendasWeb.PortalClienteManuli.Pedidos.InclusaoPedidosWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div class="col-md-12 grid-margin stretch-card">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Inclusão de Pedidos</h4>
                <p class="card-description">Insira um novo pedido agora!</p>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Empresa:</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="EmpresaDropDownList" runat="server" CssClass="form-control" OnSelectedIndexChanged="EmpresaDropDownList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Razão Social:</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="RazaoSocialDropDownList" runat="server" CssClass="form-control" OnSelectedIndexChanged="RazaoSocialDropDownList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group row">
                            <div class="col-sm-9">
                                <!--Botões de controle-->
                                <asp:Button ID="SalvarButton" runat="server" Text="Salvar Pedidos" CssClass="btn btn-primary mr-2" OnClick="SalvarButton_Click" />
                                <asp:Button ID="RetornarButton" runat="server" Text="Cancela" CssClass="btn btn-primary mr-2" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpInclusaoPedidos" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewInclusaoPedidos" runat="server"
                                    CssClass="table table-striped table-bordered" GridLines="None" HeaderStyle-Font-Size="Medium"
                                    AutoGenerateColumns="False" OnPageIndexChanging="GridViewInclusaoPedidos_PageIndexChanging"
                                    PageSize="5" AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Produto" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="CodigoProdutoNumLabel" runat="server" Text='<%# Bind("ProdCodEstr") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descrição" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="DescricaoProdutoLabel" runat="server" Text='<%# Bind("ProdNome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Número OC" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:TextBox ID="NumeroOCTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantidade" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:TextBox ID="QuantidadeTextBox" CssClass="form-control" runat="server"
                                                    onkeypress="mascara( this, mnumEvirgula );"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unidade" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="UnidadeLabel" runat="server" Text='<%# Bind("ProdUnidMedCod") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CondPagCod" SortExpression="EmpCod" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="CondPagCodLabel" runat="server" Text='<%# Bind("CondPag") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Condição Pagamento" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="CondicaoLabel" runat="server" Text='<%# Bind("CondPagNome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TabPVCod" SortExpression="EmpCod" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="TabPVCodLabel" runat="server" Text='<%# Bind("TabPVCod") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UnitarioValor" SortExpression="EmpCod" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="UnitarioValorLabel" runat="server" Text='<%# Bind("ValorUnitario") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Valor Unitário" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="UnitarioLabel" runat="server" Text='<%# string.Format("{0:C}", Eval("ValorUnitario")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridViewInclusaoPedidos" EventName="PageIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
