<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="cadPedidoPrincipal.aspx.cs" Inherits="VendasWeb.cadastros.cadPedidoPrincipal" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=5" />
    <link rel="stylesheet" type="text/css" href="../css/jquery.calendario.css?aux=5" />

    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/jquery.calendario.js" type="text/javascript"></script>

    <script language="javascript" src="../js/cadPedidoPrincipal1.js?aux=3" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 129px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-9">
            <!--===================================================-->
            <!--Painel Carteiras e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">

                    <h3 class="panel-title">Cadastro Pedido</h3>
                </div>
                <div class="panel-body">
                    <!-- Cabecario Entidade -->
                    <div id="entCabecario" class="detCabeccario">
                        <div id="btnentidade" style="margin-top:1px; margin-right:1px; float:right;">
                            <asp:LinkButton ID="btnAlteraEntidade" runat="server" CssClass="btn btn-success btn-labeled fa fa-refresh fa-lg"
                                OnClick="btnAlteraEntidade_Click">Alterar Entidade</asp:LinkButton>
                        </div>
                        <asp:Label ID="lblEmpresa" runat="server" Text="EMPRESA:" CssClass="text-thin"></asp:Label>
                        <asp:Label ID="lblDescEmpresa" runat="server" Text="" CssClass="text-thin"></asp:Label><br />
                        <asp:Literal ID="ltlNumPedido" runat="server"></asp:Literal>
                        <!--<asp:Label ID="lblNumPedido" runat="server" Text="Número:" CssClass="text-thin" ></asp:Label>
        <asp:Label ID="lblDescNumPedido"  runat="server" Text="" CssClass="text-thin"></asp:Label><br /> -->
                        <asp:Label ID="lblnome" runat="server" Text="NOME:" CssClass="text-thin"></asp:Label>
                        <asp:Label ID="lblDescNome" runat="server" Text="" CssClass="text-thin"></asp:Label><br />
                        <asp:Label ID="lblFantasia" runat="server" Text="FANTASIA:" CssClass="text-thin"></asp:Label>
                        <asp:Label ID="lblDescFantasia" runat="server" Text="" CssClass="text-thin"></asp:Label><br />
                        <asp:Label ID="lblCnpj" runat="server" Text="CNPJ/CPF:" CssClass="text-thin"></asp:Label>
                        <asp:Label ID="lblDescCnpj" runat="server" Text="" CssClass="text-thin"></asp:Label><br />
                        <asp:TextBox ID="txtIDEntidade" runat="server" Visible="false"></asp:TextBox>
                        <asp:Label ID="lblEntRgIe" runat="server" Text="" CssClass="text-thin" Visible="false"></asp:Label>
                    </div>
                    <div>
                        <div class="row">
                            <div class="col-sm-3">
                                <h5>
                                    <asp:Label ID="lblTipo" runat="server" Text="Tipo:" CssClass="text-thin"></asp:Label></h5>
                                <asp:DropDownList ID="drpTipo" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True">Total</asp:ListItem>
                                    <asp:ListItem>Programado</asp:ListItem>
                                    <asp:ListItem>Parcial</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                <h5>
                                    <asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="text-thin"></asp:Label></h5>

                                <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-3">
                                <h5>
                                    <asp:Label ID="lblDataEntrega" runat="server" Text="Data Saída:" CssClass="text-thin"></asp:Label></h5>
                                <div style="margin-right: 1px; margin-top: 1px; float: right;">
                                    <h4><a href="#" id="btnPrazos">
                                        <i class="red fa fa-truck"></i></a></h4>
                                </div>
                                <div style="margin-right: 1px; margin-top: 1px; float: right;">
                                    <h4><a href="#" id="btnCalendar1" shape="rect">
                                        <i class="fa fa-calendar"></i>
                                    </a></h4>
                                </div>
                                <asp:TextBox ID="txtDataEntrega" runat="server" Width="80%" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                            </div>
                            <div class="col-xs-3">
                                <h5>
                                    <asp:Label ID="lblDataEmissao" runat="server" Text="Data Emissão:" CssClass="text-thin"></asp:Label></h5>
                                <div style="margin-right: 15px; margin-top: 1px; float: right;">
                                    <h4>
                                        <a href="#" id="btnCalendar2">
                                            <i class="fa fa-calendar"></i></a>
                                    </h4>
                                </div>
                                <asp:TextBox ID="txtDataEmissao" CssClass="form-control" Width="80%" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="Label2" runat="server" Text="Vendedor Cadastrado:" CssClass="text-thin"></asp:Label></h5>

                            <asp:TextBox ID="txtVendedorCadastrado" CssClass="form-control" runat="server"
                                Width="155px" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblPedCliente" runat="server" Text="Numero da OC:" CssClass="text-thin"></asp:Label></h5>

                            <asp:TextBox ID="txtPedCliente" runat="server" CssClass="form-control" ReadOnly="false" Width="65px" MaxLength="40"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblNatureza" runat="server" Text="Natureza:" CssClass="text-thin"></asp:Label></h5>

                            <asp:DropDownList ID="drpNatureza" CssClass="form-control" runat="server">
                                <asp:ListItem Selected="True">Atacadista</asp:ListItem>
                                <asp:ListItem>Contrutora</asp:ListItem>
                                <asp:ListItem>Consumidor</asp:ListItem>
                                <asp:ListItem>Consumidor Contribuinte</asp:ListItem>
                                <asp:ListItem>Distribuidor</asp:ListItem>
                                <asp:ListItem>Entidade Governamental</asp:ListItem>
                                <asp:ListItem>Exportador</asp:ListItem>
                                <asp:ListItem>Fabricante</asp:ListItem>
                                <asp:ListItem>Importador</asp:ListItem>
                                <asp:ListItem>Importador</asp:ListItem>
                                <asp:ListItem>Prestador de Serl</asp:ListItem>
                                <asp:ListItem>Varejista</asp:ListItem>
                                <asp:ListItem>Transportador</asp:ListItem>
                                <asp:ListItem>Revendedor</asp:ListItem>
                                <asp:ListItem>Representante</asp:ListItem>
                                <asp:ListItem>Produtor Rural </asp:ListItem>
                                <asp:ListItem>Prestador de Serviços</asp:ListItem>
                                <asp:ListItem>Revendedor</asp:ListItem>
                                <asp:ListItem>Outros</asp:ListItem>
                                <asp:ListItem>Motorista</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblOperacao1" runat="server" Text="Operacao:" CssClass="text-thin"></asp:Label></h5>
                            <asp:DropDownList ID="drpOperacao1" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <%--<div>
                                <h5>
                                    <asp:Label ID="lblOperacao" runat="server" Text="Operação:" CssClass="text-thin"></asp:Label></h5>
                                
                                    <asp:DropDownList ID="drpOperacao" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True">Venda Líquida</asp:ListItem>
                                        <asp:ListItem>Venda Dólar</asp:ListItem>
                                        <asp:ListItem>Venda Triangular</asp:ListItem>
                                        <asp:ListItem>Venda Triangular Entrega</asp:ListItem>
                                        <asp:ListItem>Venda Excedido Limite de Crédito</asp:ListItem>
                                        <asp:ListItem>Bonificação</asp:ListItem>
                                        <asp:ListItem>Amostra</asp:ListItem>
                                        <asp:ListItem>Comodato</asp:ListItem>
                                        <asp:ListItem>Demonstração</asp:ListItem>
                                        <asp:ListItem>Exportação</asp:ListItem>
                                        <asp:ListItem>Exportação Indireta</asp:ListItem>
                                        <asp:ListItem>Consignação</asp:ListItem>
                                    </asp:DropDownList>
                            </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblEspecie" runat="server" Text="Espécie:" CssClass="text-thin"></asp:Label></h5>
                            <asp:DropDownList ID="drpEspecie" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblCondicao" runat="server" Text="Condição Pagamento:" CssClass="text-thin"></asp:Label></h5>

                            <asp:DropDownList ID="drpCondPag" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="Label1" runat="server" Text="Consumo:" CssClass="text-thin"></asp:Label></h5>

                            <asp:DropDownList ID="drpConsumo" runat="server" CssClass="form-control">
                                <asp:ListItem Selected="True">Selecione</asp:ListItem>
                                <asp:ListItem>Sim</asp:ListItem>
                                <asp:ListItem>Nao</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblTipFrete" runat="server" Text="Tipo Frete:" CssClass="text-thin"></asp:Label></h5>

                            <asp:DropDownList ID="drpTipoFrete" runat="server" CssClass="form-control">
                                <asp:ListItem Selected="True">Emitente</asp:ListItem>
                                <asp:ListItem>Destinatário</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblValorFrete" runat="server" Text="Valor Frete:" CssClass="text-thin"></asp:Label></h5>

                            <asp:TextBox ID="txtValorFrete" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblEmbarque" runat="server" Text="Embarque Imediato:" CssClass="text-thin"></asp:Label></h5>
                            <div style="margin-right: 1px; margin-top: 1px; float: right;">
                                <h4>
                                    <a href="#" id="btnEmbarque">
                                        <i class="fa fa-truck"></i></a>
                                </h4>
                            </div>

                            <asp:DropDownList ID="drpEmbarque" runat="server" CssClass="form-control" Width="90%">
                                <asp:ListItem Selected="True">Selecione</asp:ListItem>
                                <asp:ListItem>Sim</asp:ListItem>
                                <asp:ListItem>Nao</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="lblTransportadora" runat="server" Text="Transportadora:" CssClass="text-thin"></asp:Label></h5>
                            <div style="margin-right: 1px; margin-top: 0px; float: right;">
                                <asp:LinkButton ID="btnSearch" runat="server" Text="" OnClick="Button1_Click" CssClass="btnSearch"><i class="fa fa-search fa-lg "></i></asp:LinkButton>
                            </div>
                            <asp:TextBox ID="txtTransportadora" runat="server" Width="90%" CssClass="form-control"
                                OnTextChanged="txtTransportadora_TextChanged"></asp:TextBox>

                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <br />
                                <br />
                                <asp:Label ID="lblDescTransp" runat="server" Text="" CssClass="text-thin"></asp:Label></h5>
                        </div>

                    </div>

                    <div id="lstItem" class="table-responsive">
                        <table class="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                            style="border-collapse:collapse;border-collapse: collapse; max-width: 100%">
                            <tr>
                                <th scope="col" align="center">
                                    <asp:LinkButton ID="btnIncluir" runat="server" CssClass="btn btn-success fa fa-plus" Text=""
                                        OnClick="btnIncluir_Click"></asp:LinkButton></th>
                                <th scope="col" style="width:60%;">
                                    <asp:Label ID="lblProduto" CssClass="font-weight-bold" runat="server" Text="Produto"></asp:Label></th>
                                <th scope="col">
                                    <asp:Label ID="lblUnidade" CssClass="font-weight-bold" runat="server" Text="UND"></asp:Label></th>
                                <!-- <td><asp:Label ID="lblRevenda" runat="server" Text="Revenda:"></asp:Label></td> -->
                                <th scope="col">
                                    <asp:Label ID="lblQuantidade" CssClass="font-weight-bold" runat="server" Text="Quantidade"></asp:Label></th>
                                <th scope="col">
                                    <asp:Label ID="lbltabela" CssClass="font-weight-bold" runat="server" Text="Tabela"></asp:Label></th>
                                <th scope="col" style="width:200%;">
                                    <asp:Label ID="lblValorUnitario" CssClass="font-weight-bold" runat="server" Text="Valor"></asp:Label></th>

                                <th scope="col">
                                    <asp:Label ID="lblPosicao" CssClass="font-weight-bold" runat="server" Text="Posição"></asp:Label></th>

                                <th scope="col">
                                    <asp:Label ID="lblTotal" CssClass="font-weight-bold" runat="server" Text="Total"></asp:Label></th>

                                <th scope="col">
                                    <asp:Label ID="lblComposicao" CssClass="font-weight-bold" runat="server" Text="Composição"></asp:Label></th>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblDescProduto" runat="server" Text="" CssClass="text-thin"></asp:Label>
                                    <asp:Label ID="lblDescricaoProduto" runat="server" Text="" CssClass="text-thin"></asp:Label>
                                    <asp:TextBox ID="txtCompDescProduto" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblDescUnidade" runat="server" Text="" CssClass="text-thin"></asp:Label></td>
                                <!-- <td>
                 <asp:DropDownList ID="drpRevenda" runat="server" >
                    <asp:ListItem Selected="True" Value="0">Não</asp:ListItem>
                    <asp:ListItem Value="1">Sim</asp:ListItem>
                </asp:DropDownList></td> -->
                                <td>
                                    <asp:TextBox ID="txtQuantidade" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td>
                                    <asp:DropDownList ID="drpTabela" CssClass="form-control" runat="server">
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtValor" runat="server" CssClass="form-control"></asp:TextBox></td>

                                <td>
                                    <asp:TextBox ID="txtPosicao" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox></td>

                                <td align="center"><a href="#"><span>
                                    <asp:LinkButton ID="btnSalvar" runat="server" CssClass="btn btn-success fa fa-save" Text=""
                                        OnClick="btnSalvar_Click"></asp:LinkButton></span></a></td>

                                <td></td>
                            </tr>

                            <!-- Items carregados dinamicamente -->
                            <asp:Literal ID="ltlItems" runat="server"></asp:Literal>

                        </table>

                    </div>

                    <!-- Botões para navegação -->
                    <div id="botomPed">

                        <div id="dadComplementaresPedido">
                            <asp:LinkButton ID="btnDadosComplementares" runat="server"
                                Text="Dados Complementares" CssClass="btn btn-primary btn-labeled fa fa-plus-circle fa-lg"
                                OnClick="btnDadosComplementares_Click">Dados Complementares</asp:LinkButton>

                        </div>

                    </div>

                    <!-- Totais do pedido -->
                    <div id="dvTotais">
                        <div id="ddTotais">
                            <asp:Literal ID="ltlTotais" runat="server"></asp:Literal>
                        </div>
                    </div>

                    <div id="dadosaUxiliares">

                        <input name="idItem" id="idItem" type="hidden" value="" />
                        <asp:Label ID="lblProdutoAux" runat="server" Text="Label" Visible="false"></asp:Label>
                        <input name="idValorOriginal" id="idValorOriginal" type="hidden" runat="server" />

                    </div>
                </div>
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">
                            <asp:LinkButton ID="btnCancelarPedido" runat="server"
                                Class="btn btn-danger btn-labeled fa fa-close fa-lg" OnClick="Button2_Click" Visible="false">Cancelar Pedido</asp:LinkButton>

                            <asp:LinkButton ID="btnGerarCopia" runat="server"
                                Class="btn btn-primary btn-labeled fa fa-copy fa-lg" OnClick="btnGerarCopia_Click">Gerar Cópia</asp:LinkButton>

                            <asp:LinkButton ID="Button1" runat="server"
                                Class="btn btn-secundary btn-labeled fa fa-copy fa-lg" OnClick="Button1_Click1">Cópia Sem Histórico</asp:LinkButton>

                            <asp:LinkButton ID="btnSalvarPedido" runat="server"
                                class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg" OnClick="btnSalvarPedido_Click">Salvar Dados</asp:LinkButton>

                            <asp:LinkButton ID="btnAprovar" runat="server"
                                Class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg" OnClick="btnAprovar_Click" Visible="false">Aprovar Pedido</asp:LinkButton>

                            <asp:LinkButton ID="btnCancelar" runat="server" Class="btn btn-danger btn-labeled fa fa-times-circle fa-lg"
                                OnClick="btnCancelar_Click">Cancelar</asp:LinkButton>


                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->
        <!----PAINEL----->
        <ucp:ControlPainel ID="ControlPainel" runat="server" updatemode="Conditional" runat="server" />
        </div>
</asp:Content>
