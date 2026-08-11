<%@ Page Title="" Language="C#" MasterPageFile="~/PortalCliente.Master" AutoEventWireup="true" CodeBehind="ListaParcelasWebForm.aspx.cs" Inherits="VendasWeb.PortalClienteManuli.Financeiro.ListaParcelasWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div class="col-md-12 grid-margin stretch-card">
        <div class="card">

            <div class="card-body">
                <h4 class="card-title">Lista de Parcelas</h4>
                <p class="card-description">Confira as parcelas da empresa.</p>

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
                            <label class="col-sm-3 col-form-label">Documento:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="DocumentoTextBox" runat="server" CssClass="form-control" placeholder="Digite o número documento."></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Período:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="DataInicialTextBox" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="DataFinalTextBox" runat="server" CssClass="form-control" type="date"></asp:TextBox>
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
                                        <asp:RadioButton ID="TodasRadioButton" runat="server" GroupName="membershipRadios" />
                                        Todas
                                    <i class="input-helper"></i>
                                    </label>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-check">
                                    <label class="form-check-label">
                                        <asp:RadioButton ID="AbertasRadioButton" runat="server" GroupName="membershipRadios" Checked="true" />
                                        Em Aberto
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
                                <asp:Button ID="BuscarButton" runat="server" Text="Buscar Parcelas" CssClass="btn btn-primary mr-2" OnClick="BuscarButton_Click" />
                            </div>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpParcelasClientes" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewParcelasClientes" runat="server"
                                    CssClass="table table-striped table-bordered" GridLines="None" HeaderStyle-Font-Size="Medium"
                                    AutoGenerateColumns="False" OnPageIndexChanging="GridViewParcelasClientes_PageIndexChanging"
                                    PageSize="5" AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EmpCod" SortExpression="EmpCod" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="EmpCodLabel" runat="server" Text='<%# Bind("EmpCod") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pedido" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="PedVendaNumLabel" runat="server" Text='<%# Bind("PedVendaNum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nota Fiscal" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="NotaFiscalLabel" runat="server" Text='<%# Bind("NFNum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Número Parcela" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="ParcDocFinDupNumLabel" runat="server" Text='<%# Bind("ParcDocFinDupNum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vencimento" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="ParcDocFinDataVencLabel" runat="server" Text='<%# Bind("ParcDocFinDataVenc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Valor Parcela" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="ParcDocFinValOrigLabel" runat="server" Text='<%# string.Format("{0:C}", Eval("ParcDocFinValOrig")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Situação" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="SituacaoLabel" runat="server" Text='<%# Bind("Situacao") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Boletos">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonConsulta" runat="server" CssClass="btn btn-outline-info btn-icon-text" OnClick="LinkButtonConsulta_Click">
                                                            <i class="mdi mdi-email-outline btn-icon-prepend"></i>
                                                            Solicitar
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridViewParcelasClientes" EventName="PageIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
