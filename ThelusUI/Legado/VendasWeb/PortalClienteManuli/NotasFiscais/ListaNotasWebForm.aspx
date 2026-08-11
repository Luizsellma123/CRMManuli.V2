<%@ Page Title="" Language="C#" MasterPageFile="~/PortalCliente.Master" AutoEventWireup="true" CodeBehind="ListaNotasWebForm.aspx.cs" Inherits="VendasWeb.PortalClienteManuli.NotasFiscais.ListaNotasWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div class="col-md-12 grid-margin stretch-card">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Lista Notas Fiscais</h4>
                <p class="card-description">Confira suas notas abaixo!</p>

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
                            <label class="col-sm-3 col-form-label">Nota Fiscal:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="NotaTextBox" runat="server" CssClass="form-control" placeholder="Digite o número pedido."
                                    onkeypress="mascara( this, mnum );"></asp:TextBox>
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
                                <asp:TextBox ID="DataFinalTextBox" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <!-- <div class="col-md-6">
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
                    </div> -->

                    <div class="col-md-6">
                        <div class="form-group row">
                            <div class="col-sm-9">
                                <!--Botões de controle-->
                                <asp:Button ID="BuscarButton" runat="server" Text="Buscar Notas" CssClass="btn btn-primary mr-2" OnClick="BuscarButton_Click" />
                            </div>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpNotasClientes" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewNotasClientes" runat="server"
                                    CssClass="table table-striped table-bordered" GridLines="None" HeaderStyle-Font-Size="Medium"
                                    AutoGenerateColumns="False" OnPageIndexChanging="GridViewNotasClientes_PageIndexChanging"
                                    PageSize="5" AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EmpCod" SortExpression="EmpCod" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="EmpCodLabel" runat="server" Text='<%# Bind("EmpCod") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nota Fiscal" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="NFnumLabel" runat="server" Text='<%# Bind("NFnum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nome Cliente" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="EntNomeLabel" runat="server" Text='<%# Bind("EntNome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Data Emissão" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="NFDataEmisLabel" runat="server" Text='<%# Bind("NFDataEmis") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pedido" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="PedVendaNumLabel" runat="server" Text='<%# Bind("PedVendaNum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Nota" SortExpression="EmpCod">
                                            <ItemTemplate>
                                                <asp:Label ID="NFValTotNotaLabel" runat="server" Text='<%# string.Format("{0:C}", Eval("NFValTotNota")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Consulta">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonConsulta" runat="server" CssClass="btn btn-outline-info btn-icon-text" OnClick="LinkButtonConsulta_Click">
                                                            <i class="mdi mdi-exit-to-app btn-icon-prepend"></i>
                                                            Detalhes
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nota Fiscal">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonSolicita" runat="server" CssClass="btn btn-outline-info btn-icon-text" OnClick="LinkButtonSolicita_Click">
                                                            <i class="mdi mdi-printer btn-icon-prepend"></i>
                                                            Gerar
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridViewNotasClientes" EventName="PageIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
