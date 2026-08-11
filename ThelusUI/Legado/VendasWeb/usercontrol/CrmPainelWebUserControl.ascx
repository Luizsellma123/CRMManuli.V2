<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CrmPainelWebUserControl.ascx.cs"
    Inherits="VendasWeb.usercontrol.CrmPainelWebUserControl" %>
<asp:UpdatePanel ID="PainelUpdatePanel" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <!-- COLUNA 2-->
        <div class="col-sm-3 bg-gray pad-ver">
            <!--BLOCO DE COMANDOS-->
            <!--===================================================-->
            <div class="row">
                <!--Block Level buttons-->
                <!--===================================================-->
                <div class="col-xs-12">
                <asp:LinkButton ID="PerfilComercialLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x disabled"
                        runat="server" title="Perfil Comercial da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="PerfilComercialLinkButton_Click" Visible="false"> Perfil Comercial  </asp:LinkButton>

                    <asp:LinkButton ID="NovaEntidadeButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square-o fa-3x"
                        runat="server" CausesValidation="False" OnClick="NovaEntidadeButton_Click" title="Cadastrar Novo Cliente"
                        data-rel="tooltip" Visible="false"> Novo Cadastro </asp:LinkButton>
                   
                     <asp:LinkButton ID="CadastroDetalheLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled"
                        runat="server" title="Detalhes dos dados da Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="CadastroDetalheLinkButton_Click"  Visible="false"> Editar Cadastro  </asp:LinkButton>
                    
                    <asp:LinkButton ID="AtendimentoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled"
                        runat="server" title="Atendimento" CausesValidation="False" data-rel="tooltip" Visible="false"> Atendimento   </asp:LinkButton>
                    
                    <asp:LinkButton ID="QuantidadeClientesLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled"
                        runat="server" title="Quantidade Clientes" CausesValidation="False" 
                        data-rel="tooltip" Onclick="QuantidadeClientesLinkButton_Click" Visible="false"> Quantidade Clientes  </asp:LinkButton>
                     
                    <asp:LinkButton ID="SimuladorLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x"
                        runat="server" title="Quantidade Clientes" CausesValidation="False" 
                        data-rel="tooltip" Onclick="SimuladorLinkButton_Click" > Simulador de Preços  </asp:LinkButton>
                    
                    <asp:LinkButton ID="ExpectativaLinkButton"  class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled"
                        runat="server" title="Expectativa" 
                        OnClick="ExpectativaLinkButton_Click" CausesValidation="False" data-rel="tooltip" Visible="false"> Expectativa Vendedor </asp:LinkButton>
                    
                    <asp:LinkButton ID="CadastroExpClassesLinkButton"  class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled"
                        runat="server" title="Expectativa" 
                        OnClick="CadastroExpClassesLinkButton_Click" CausesValidation="False" data-rel="tooltip" Visible="false"> Expectativa Classes </asp:LinkButton>
                    
                    <asp:LinkButton ID="CarteirasLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled"
                        runat="server" title="Carteiras" CausesValidation="False" data-rel="tooltip"
                        OnClick="CarteirasLinkButton_Click" Visible="false"> Carteiras  </asp:LinkButton>

                    <asp:LinkButton ID="ClassificacaoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled"
                        runat="server" title="Classificação" CausesValidation="False" data-rel="tooltip" 
                        OnClick="ClassificacaoLinkButton_Click" Visible="false"> Classificação  </asp:LinkButton>

                    <asp:LinkButton ID="RelatorioGerencialLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled"
                        runat="server" title="Relatório gerencial" CausesValidation="False" data-rel="tooltip"
                        OnClick="RelatorioGerencialLinkButton_Click" Visible="false"> Relatório gerencial  </asp:LinkButton>
                    
                    <asp:LinkButton ID="FretesCidadesGerencialLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x disabled"
                        runat="server" title="Fretes Cidades" CausesValidation="False" data-rel="tooltip"
                        OnClick="FretesCidadesLinkButton_Click" Visible="false"> Fretes Cidades  </asp:LinkButton>

                    <asp:LinkButton ID="FretesEstadosGerencialLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x disabled"
                        runat="server" title="Fretes Estados" CausesValidation="False" data-rel="tooltip"
                        OnClick="FretesEstadosLinkButton_Click" Visible="false"> Fretes Estados  </asp:LinkButton>

                    <asp:LinkButton ID="ContatoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x disabled"
                        runat="server" title="Contatos da Entidade" CausesValidation="False" data-rel="tooltip"
                        OnClick="ContatoLinkButton_Click" Visible="false"> Contato  </asp:LinkButton>
                    
                           <asp:LinkButton ID="GeolocalizacaoLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-map-marker fa-3x disabled"
                        runat="server" title="Geolocalização" CausesValidation="False"
                        data-rel="tooltip" OnClick="GeolocalizacaoLinkButton_Click" Visible="false"> Geolocalização  </asp:LinkButton>

                            <asp:LinkButton ID="RoterizacaoPainelLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-car fa-3x disabled"
                        runat="server" title="Roterização" CausesValidation="False"
                        data-rel="tooltip" OnClick="RoterizacaoPainelLinkButton_Click" Visible="false"> Roterização  </asp:LinkButton>

                    <asp:LinkButton ID="AnexosLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled"
                        runat="server" title="Anexos da Entidade" CausesValidation="False" data-rel="tooltip"
                        OnClick="AnexosLinkButton_Click" Visible="false"> Anexos  </asp:LinkButton>
                    
                    <asp:LinkButton ID="AnaliseLinkButton" class="btn btn-lg btn-block btn-info btn-labeled fa fa-external-link fa-3x disabled"
                        runat="server" title="Enviar para Analise" CausesValidation="False" data-rel="tooltip"
                        OnClick="AnaliseLinkButton_Click" Visible="false"> Enviar para Analise  </asp:LinkButton>
                    <!--================================================================-->
                    <!---Start RED---->
                    <!--================================================================-->
                    
                    <asp:LinkButton ID="CRMLinkButton" class="btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x disabled"
                        runat="server" title="Histórico de Atendimento" data-rel="tooltip" CausesValidation="False"
                        OnClick="CRMLinkButton_Click"> Histórico de Atendimento </asp:LinkButton>
                    
                    <asp:LinkButton ID="IncluirCarteiraLinkButton" class="btn btn-lg btn-block btn-danger btn-labeled fa fa-plus-circle fa-3x disabled"
                        runat="server" title="Incluir Carteira para a Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="IncluirCarteiraLinkButton_Click">
                                Incluir Carteira </asp:LinkButton>
                    
                    <asp:LinkButton ID="ExcluirCarteiraLinkButton" CausesValidation="False" class="btn btn-lg btn-block btn-danger btn-labeled fa fa-times-circle fa-3x disabled"
                        runat="server" title="Excluir Carteira para a Entidade" data-rel="tooltip" OnClick="ExcluirCarteiraLinkButton_Click"> Excluir Carteira </asp:LinkButton>
                    
                    <asp:LinkButton ID="CalendarioLinkButton" CausesValidation="False" class="btn btn-lg btn-block btn-danger btn-labeled fa fa-calendar fa-3x disabled"
                        runat="server" title="Calendario" data-rel="tooltip" Visible="false"> Calendario </asp:LinkButton>
                    <!--================================================================-->
                    <!---End RED---->
                    <!--================================================================-->
                    <!--================================================================-->
                    <!---Start Green---->
                    <!--================================================================-->
                    <asp:LinkButton ID="PedidoLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-cart-plus fa-3x disabled"
                        runat="server" title="Inserir Pedido para essa Entidade" CausesValidation="False"
                        data-rel="tooltip" OnClick="PedidoLinkButton_Click"> Novo Pedido </asp:LinkButton>
                    
                    <asp:LinkButton ID="ListaLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-list-alt fa-3x"
                        runat="server" title="Pedido da Entidade" data-rel="tooltip" CausesValidation="False"
                        OnClick="ListaLinkButton_Click"> Pedidos </asp:LinkButton>

                    <asp:LinkButton ID="AcompanhamentoPedidoLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-line-chart fa-3x"
                        runat="server" title="Pedido da Entidade" data-rel="tooltip" CausesValidation="False"
                        OnClick="AcompanhamentoPedidoLinkButton_Click"> Acompanhamento Pedidos </asp:LinkButton>

                    <asp:LinkButton ID="TabelaPrecoLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-line-chart fa-3x"
                        runat="server" title="Tabela de Preço" data-rel="tooltip" CausesValidation="False"
                        OnClick="TabelaPrecoLinkButton_Click"> Tabela Preço </asp:LinkButton>

                    <asp:LinkButton ID="ClientesAtivosLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-line-chart fa-3x"
                        runat="server" title="Clientes Ativos" data-rel="tooltip" CausesValidation="False"
                        OnClick="ClientesAtivosLinkButton_Click"> Clientes Ativos </asp:LinkButton>
                    
                    <%--<asp:LinkButton ID="PedidoProdutosLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-list-alt fa-3x"
                        runat="server" title="Pedido da Entidade" data-rel="tooltip" CausesValidation="False" 
                        OnClick="PedidoProdutosLinkButton_Click"> Pedidos por Produto </asp:LinkButton>--%>
                    
                    <asp:LinkButton ID="NotasLinkButton" class="btn btn-lg btn-block btn-success btn-labeled fa fa-file-text fa-3x disabled"
                        runat="server" title="Notas da Entidade" data-rel="tooltip" CausesValidation="False"
                        OnClick="NotasLinkButton_Click" Visible="false"> Notas </asp:LinkButton>
                    <!--================================================================-->
                    <!---End Green---->
                    <!--================================================================-->
                    <!--================================================================-->
                    <!---Start Blue---->
                    <!--================================================================-->
                    <asp:LinkButton ID="LogisticaLinkButton" CausesValidation="False" class="btn btn-lg btn-block btn-primary btn-labeled fa fa-truck fa-3x disabled"
                        runat="server" title="Logistica" data-rel="tooltip" OnClick="LogisticaLinkButton_Click" Visible="false"> Logistica </asp:LinkButton>
                    
                    <asp:LinkButton ID="EstoqueLinkButton" CausesValidation="False" class="btn btn-lg btn-block btn-primary btn-labeled fa fa-archive fa-3x disabled"
                        runat="server" title="Estoque" data-rel="tooltip" Visible="false"> Estoque </asp:LinkButton>
                    <!--================================================================-->
                    <!---Start Blue---->
                    <!--================================================================
                    <asp:LinkButton ID="DuplicatasLinkButton" CausesValidation="False" class="btn btn-lg btn-block btn-warning btn-labeled fa fa-list fa-3x disabled"
                        runat="server" title="Duplicatas" data-rel="tooltip" OnClick="DuplicatasLinkButton_Click"> Duplicatas </asp:LinkButton>
                    <asp:LinkButton ID="FiscalLinkButton" CausesValidation="False" class="btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x disabled"
                        runat="server" title="Fiscal" data-rel="tooltip" OnClick="FiscalLinkButton_Click"> Fiscal </asp:LinkButton>
                    <asp:LinkButton ID="FinanceiroLinkButton" CausesValidation="False" class="btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled"
                        runat="server" title="Financeiro" data-rel="tooltip" OnClick="FinanceiroLinkButton_Click"> Financeiro </asp:LinkButton>
                    ================================================================-->
                    <!---Start yellow---->
                    <!--================================================================-->
                    <!--================================================================-->
                    <!---Start yellow---->
                    <!--================================================================-->
                </div>
                <!--===================================================-->
            </div>
            <!--===================================================-->
            <!--END BLOCO DE COMANDOS-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
