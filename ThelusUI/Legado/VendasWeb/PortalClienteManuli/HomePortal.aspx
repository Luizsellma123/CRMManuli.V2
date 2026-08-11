<%@ Page Title="" Language="C#" MasterPageFile="~/PortalCliente.Master" AutoEventWireup="true" CodeBehind="HomePortal.aspx.cs" Inherits="VendasWeb.PortalClienteManuli.HomePortal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">

    <div class="row">
        <div class="col-xl-12 grid-margin stretch-card">
            <div class="card">
                <div class="card-body pb-0">

                    <div class="form-group row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="RazaoSocialDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col-xl-3 grid-margin stretch-card">
            <div class="card">
                <div class="card-body pb-0">
                    <p class="card-title mb-xl-0">Faturamento Por Linha No Ano</p>
                    <p class="pb-2 text-muted">Quanto foi comprado no ano atual.</p>
                </div>

                <canvas id="distribution-chart"></canvas>
                <div class="card-body">
                    <div id="distribution-legend" class="distribution-chart-legend pt-4 pb-3"></div>
                    <button class="btn btn-outline-light text-dark d-block mx-auto mt-2">Veja Mais</button>
                </div>
            </div>
        </div>

        <div class="col-xl-9 grid-margin stretch-card">
            <div class="card">
                <div class="row">
                    <div class="col-md-7 col-lg-6 col-xl-7">
                        <div class="card-body h-100 d-flex flex-column">
                            <p class="card-title">Faturamento 12 Meses</p>
                            <p class="text-muted mb-4">Acompanhe abaixo mês a mês.</p>
                            <canvas id="sale-report-chart"></canvas>
                        </div>
                    </div>
                    <div class="col-md-5 col-lg-6 col-xl-5">
                        <div class="card-body">
                            <p class="card-title">Total Faturamento</p>
                            <p class="pb-2 text-muted">Confira abaixo o total do faturamento.</p>
                            <div class="d-flex flex-wrap justify-content-start mt-3 mr-4">
                                <div class="mr-3">
                                    <p class="mb-0">Total 12 Meses</p>
                                    <h4>
                                        <asp:Label ID="LabelTotalFaturamento" runat="server" Text="Label"></asp:Label></h4>
                                </div>
                            </div>
                            <div class="d-flex flex-wrap mb-5">
                                <button class="btn btn-outline-light mt-3 text-dark">Veja Mais</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<div class="row">
        <div class="col-xl-12 grid-margin stretch-card">
            <div class="card">
                <div class="card-body pb-0">

                    <div class="form-group row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="RazaoSocialDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-xl-3 grid-margin stretch-card">
            <div class="card">
                <div class="card-body pb-0">
                    <p class="card-title mb-xl-0">Limite de Crédito</p>
                    <p class="pb-2 text-muted">Seu limite em tempo real.</p>
                </div>

                <canvas id="LimiteCredito-chart"></canvas>
                <div class="card-body">
                    <div id="LimiteCredito-legend" class="distribution-chart-legend pt-4 pb-3"></div>
                    <button class="btn btn-outline-light text-dark d-block mx-auto mt-2">Veja Mais</button>
                </div>
            </div>
        </div>

        <div class="col-xl-9 grid-margin stretch-card">
            <div class="card">
                <div class="row">
                    <div class="col-md-12 col-lg-6 col-xl-12">
                        <div class="card-body">
                            <p class="card-title">Pedidos Pendentes</p>
                            <p class="pb-2 text-muted">Confira abaixo os pedidos da empresa.</p>
                            <div class="d-flex flex-wrap justify-content-start mt-3 mr-4">
                                <div class="mr-3">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="UpPedidosPendentes" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridViewPedidosPendentes" runat="server"
                                                    CssClass="table" GridLines="None" HeaderStyle-Font-Size="Medium"
                                                    AutoGenerateColumns="False" OnPageIndexChanging="GridViewPedidosPendentes_PageIndexChanging"
                                                    PageSize="3" AllowPaging="True">
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

                                                        <asp:TemplateField HeaderText="Data Pedido" SortExpression="EmpCod">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PedVendaDataLabel" runat="server" Text='<%# Bind("PedVendaData") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Bold="False" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total Pedido" SortExpression="EmpCod">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PedVendaValTotalLabel" runat="server" Text='<%# string.Format("{0:C}", Eval("PedVendaValTotal")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Bold="False" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status" SortExpression="EmpCod">
                                                            <ItemTemplate>
                                                                <asp:Label ID="pedvendastatdescrLabel" runat="server" Text='<%# Bind("pedvendastatdescr") %>'></asp:Label>
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
                                                <asp:AsyncPostBackTrigger ControlID="GridViewPedidosPendentes" EventName="PageIndexChanging" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                            <div class="d-flex flex-wrap mb-5">
                                <asp:LinkButton ID="PendentesLinkButton" CssClass="btn btn-outline-light text-dark d-block mx-auto mt-2" runat="server" OnClick="PendentesLinkButton_Click">Veja Mais</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Script Para Geração dos Relatórios -->

    <!--Total Faturametno -->
    <asp:Literal ID="LiteralGraficoFaturamento" runat="server"></asp:Literal>

    <!--Faturameto Anual -->
    <asp:Literal ID="LiteralGraficoFaturamentoAnual" runat="server"></asp:Literal>

    <!--Javascript da pagina-->
    <script src="<%=Page.ResolveClientUrl("~/PortalClienteManuli/js/HomeJavascript.js")%>"></script>

    <!--Javascript Limite Crédito-->
    <asp:Literal ID="LiteralGraficoLimiteCredito" runat="server"></asp:Literal>

</asp:Content>
