<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmPedidoDetalhe.aspx.cs" Inherits="VendasWeb.cadastros.FrmPedidoDetalhe" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <div class="panel-control">
                        <!--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>-->
                    </div>
                    <h3 class="panel-title">Detalhes do Pedido</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->


                <div class="panel-body">
                    <div class="table-responsive">


                        <div class="col-md-12 pad-top bg-gray">
                            <div class="row pad-lft pad-rgt">
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Empresa</th>
                                            <th>Nome da Empresa</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <asp:label runat="server" id="EmpCod"></asp:label></td>
                                            <td><asp:Label runat="server" id="EmpNome"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Entidade</th>
                                            <th>CNPJ</th>
                                            <th>Nome Entidade</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td><asp:label runat="server" id="EntCod"></asp:label></td>
                                            <td><asp:label runat="server" id="EntCpfCgc"></asp:label></td>
                                            <td><asp:label runat="server" id="EntNome"></asp:label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Data Digitação</th>
                                            <th>Data Saida</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td><asp:label runat="server" id="PedVendaData"></asp:label></td>
                                            <td><asp:label runat="server" id="NFHoraSaida"></asp:label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Endereço</th>
                                            <th>Bairro</th>
                                            <th>Cidade</th>
                                            <th>UF</th>
                                            <th>Cep</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td><asp:label runat="server" id="EntEnderCompleto"></asp:label></td>
                                            <td><asp:label runat="server" id="EntBair"></asp:label></td>
                                            <td><asp:label runat="server" id="CidNome"></asp:label></td>
                                            <td><asp:label runat="server" id="UfSigla"></asp:label></td>
                                            <td><asp:label runat="server" id="EntCep"></asp:label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Condição Pagamento</th>
                                            <th>Natureza de Operação</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td><asp:label runat="server" id="CondPagCod"></asp:label> - <asp:label runat="server" id="CondPagPedVendaNome"></asp:label></td>
                                            <td><asp:label runat="server" id="PedVendaNatOpProd"></asp:label> - <asp:label runat="server" id="NatOpNome"></asp:label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Código Vendedor</th>
                                            <th>Nome Vendedor</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td><asp:label runat="server" id="VendCod"></asp:label></td>
                                            <td><asp:label runat="server" id="VendNome"></asp:label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Código</th>
                                            <th>Descrição</th>
                                            <th>UN</th>
                                            <th>Quantidade</th>
                                            <th>Valor Unitário</th>
                                            <th>Total S/IPI</th>
                                            <th>Total Geral</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <asp:label runat="server" id="ItensFormatados"></asp:label>
                                            
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Mercadoria</th>
                                            <th>IPI</th>
                                            <th>ICMS</th>
                                            <th>Diferimento</th>
                                            <th>ICMS Devido</th>
                                            <th>Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>R$ <asp:label runat="server" id="PedVendaValMerc"></asp:label></td>
                                            <td>R$ <asp:label runat="server" id="PedVendaValIpiCalc"></asp:label></td>
                                            <td>R$ <asp:label runat="server" id="PedVendaValIcms"></asp:label></td>
                                            <td>R$ <asp:label runat="server" id="IcmsDiferido"></asp:label></td>
                                            <td>R$ <asp:label runat="server" id="IcmsDevido"></asp:label></td>
                                            <td>R$ <asp:label runat="server" id="PedVendaValTotal"></asp:label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Frete</th>
                                            <th>Transportadora</th>
                                            <th>Nome</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td><asp:label runat="server" id="PedVendaStatFrete"></asp:label></td>
                                            <td><asp:label runat="server" id="EntCodTransp"></asp:label></td>
                                            <td><asp:label runat="server" id="EntNomeTransp"></asp:label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Observação</th>
                                            <th>Histórico</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            <td>
                                                <textarea runat="server" name="demo-textarea-input" rows="6" readonly="true" cols="250" class="form-control" placeholder="" id="PedVendaTexto"></textarea></td>
                                            <td>
                                                <textarea runat="server" name="demo-textarea-input" rows="6" readonly="true" cols="350" class="form-control" id="PedVendaTextoHist"></textarea></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-condensed table-responsive">
                                    <thead>
                                        <tr class="bg-gray-light">
                                            <th>Cliche</th>
                                            <th>Nome Cliche</th>
                                            <th>Detalhe</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="bg-gray-light">
                                            
                                            
                                            <th><asp:label runat="server" id="ClicheFormatados"></asp:label>
                                                <a href="#" class="imgedit">
                                                <img src="../imagens/search.png" alt="Consulta" border="0" onclick="javascript: return abrirArte( 99822 )"></a></th>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="panel-footer">
                    <div class="row">
                        <!-- Botões para navegação -->
                        <div class="panel-control">
                            <asp:LinkButton runat="server" Id="SairButton" cssclass="btn btn btn-danger btn-labeled fa fa-times" OnClick="SairButton_Click">Sair</asp:LinkButton>
                            <asp:LinkButton runat="server" Id="AcessarButton" cssclass="btn btn-success btn-labeled fa fa-check" OnClick="AcessarButton_Click">Acessar Pedido</asp:LinkButton>
                            <asp:LinkButton runat="server" Id="ImprimirButton" Cssclass="btn btn-primary btn-labeled fa fa-print" OnClick="ImprimirButton_Click">Imprimir</asp:LinkButton>
                            <asp:LinkButton runat="server" Id="ImprimirSemHistButton" cssclass="btn btn-warning btn-labeled fa fa-print" Text="Imprimir sem Historico" OnClick="ImprimirSemHistButton_Click"></asp:LinkButton>
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
    
<asp:HiddenField ID="EmpCodHiddenField" runat="server" />
 <asp:HiddenField ID="PedVendaNumHiddenField" runat="server" />
 <asp:HiddenField ID="TipoHiddenField" runat="server" />

    <script type="text/javascript">
        function acessapedido() {
              
            //Empresa
            document.getElementById("ctl00_ContentPlaceHolder1_EmpCodHiddenField").value=EmpCod;
            //Pega PedVendaNUm
            document.getElementById("ctl00_ContentPlaceHolder1_PedVendaNumHiddenField").value=PedVendaNum;
            //Tipo
            document.getElementById("ctl00_ContentPlaceHolder1_TipoHiddenField").value="Consulta";

            //Chama o Servidor 
            __doPostBack('btnSave', "")            
						
        }
    </script>
</asp:Content>
