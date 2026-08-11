<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmCalendario.aspx.cs" Inherits="VendasWeb.Entidades.FrmCalendario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
 <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
 <%--<script language="javascript" src="../js/jsCalendario.js" type="text/javascript"></script>--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="row">
        <!-- Coluna 1-->
        <!-- ============================================ -->
        <div class="col-md-8 col-lg-9">
            <div class="panel">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Calendário</h3>
                </div>
                <div class="panel-body">
                    <!-- Calendar placeholder-->
                    <!-- ============================================ -->
                    <div id='demo-calendar'>
                    </div>
                    <!-- ============================================ -->
                </div>
            </div>
        </div>
        <!-- Coluna 2-->
        <!-- ============================================ -->
        <div class="col-md-4 col-lg-3">
            <div class="panel">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Agendamentos</h3>
                </div>
                <div class="panel-body">
                    <!-- Draggable Events -->
                    <!-- ============================================ -->
                    <div id="demo-external-events">
                        <asp:Literal ID="TiposAgendamentoLiteral" runat="server"></asp:Literal>
                    </div>
                    <!-- ============================================ -->
                    <hr />
                    <asp:LinkButton ID="RelatorioLinkButton" class="btn btn-block btn-purple fa fa-file-text-o fa-lg"
                        runat="server" title="Relatorio de Calendario" data-rel="tooltip" CausesValidation="False"
                        OnClick="RelatorioLinkButton_Click"> Relatório Calendario </asp:LinkButton>
                </div>
            </div>
            <div class="panel">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Filtro Calendario:</h3>
                </div>
                <div class="panel-body">
                    <!-- Draggable Events -->
                    <!-- ============================================ -->
                    <div id="DivUsuario">
                        <select class="selectpicker show-tick" title="Escolha um Usuario..." data-style="btn-primary"
                            data-live-search="true" id="UsuarioDropDownList" runat="server">
                        </select>
                        <br />
                        <asp:CheckBoxList ID="TipoAgendamentoCheckBoxList" runat="server">
                        </asp:CheckBoxList>
                        <br />
                        <asp:LinkButton ID="FiltrarLinkButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                            runat="server" title="Atualizar Filtro" data-rel="tooltip" CausesValidation="False"
                            OnClick="FiltrarLinkButton_Click"> Filtrar </asp:LinkButton>
                    </div>
                    <!-- ============================================ -->
                </div>
            </div>
        </div>
    </div>
    <div id="updatedialog" style="display: none;" title="Edita Agendamento">
        <label for="upMensagemLabel" id="upMensagemLabel">
        </label>
        <!--Inicio Hidden DE Update-->
        <span id="upVinculaEntidadeHidden" style="display: none;"></span>
        <!--Fim Hidden DE Update-->
        <div id="upComEntidadeDiv">
            <div id="upEntidadeDiv" class="row">
                <div class="col-xs-14 bg-gray">
                    <div class="row pad-lft pad-rgt">
                        <table class="table table-condensed">
                            <tbody>
                                <tr class="bg-trans-dark" id="trUpEntNome">
                                    <td class="text-right">
                                        Entidade:
                                    </td>
                                    <td class="text-bold">
                                        <span id="upEntNome"></span>
                                    </td>
                                </tr>
                                <tr class="bg-trans-dark">
                                    <td class="text-right">
                                        CNPJ:
                                    </td>
                                    <td class="text-bold">
                                        <span id="upEntCpfCgc"></span>
                                    </td>
                                </tr>
                                <tr class="bg-trans-dark">
                                    <td class="text-right">
                                        Endereço:
                                    </td>
                                    <td class="text-bold">
                                        <span id="upEndereco"></span>
                                    </td>
                                </tr>
                                <tr class="bg-trans-dark">
                                    <td class="text-right">
                                        Tipo de Cliente:
                                    </td>
                                    <td class="text-bold">
                                        <span id="upStatEntComercial"></span>
                                    </td>
                                </tr>
                                <tr class="bg-trans-dark">
                                    <td class="text-right">
                                        Status do Cliente:
                                    </td>
                                    <td class="text-bold">
                                        <span id="upEntStatDescr"></span>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="text-right">
                                        Nome do Contato:
                                    </td>
                                    <td class="text-bold">
                                        <span id="upContatoNome"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right">
                                        Telefone do Contato:
                                    </td>
                                    <td class="text-bold">
                                        <span id="upContatoTelefone"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right">
                                        Email do Contato:
                                    </td>
                                    <td class="text-bold">
                                        <span id="upContatoEmail"></span>
                                    </td>
                                </tr>


                                        <tr>
                                    <td class="text-right">
                                         Classe do Cliente:
                                    </td>
                                    <td class="text-bold">
                                        <select  name="upClasseCliente" id="upClasseCliente">
                                                <option value="SC">Sem Classificação </option>
                                                <option value="A">Classe - A</option>
                                                <option value="B">Classe - B</option>
                                                <option value="C">Classe - C</option>
                        
                                            </select>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

         

            <div id="upVisitaAnterioDiv" class="row bg-gray pad-all">
                <div class="col-xs-12 bg-trans-dark">
                    <div class="form-group pad-top">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">
                                    Visita Anterior:</label>
                                <span id="upDataUltimaVisita" class="text-bold"></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group pad-top">
                        <br />
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Ultima Ordem Adquirida:</label>
                                <span id="upNFHoraSaida" class="text-bold"></span>
                            </div>
                        </div>

                       
                        
                    </div>
                    <div class="form-group pad-top">
                    <br />
                        <div class="col-sm-12">
                              
                               <div id="upItensNF"></div>
                           
                         </div>
                    </div>
                    <div class="form-group pad-top">
                        <br />

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">
                                   Valor Total R$:</label>
                                <span id="upNFValTotNota" class="text-bold"></span>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Total de Venda Anual R$:</label>
                                <span id="upTotalVendaAnual" class="text-bold"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="upLinhaProdutoDiv" class="row bg-gray pad-all">
                <div class="col-xs-12 bg-trans-dark">
                    <div class="form-group pad-top">
                        <label class="col-md-4 control-label">
                            Estimativa Venda:</label>
                        <br />
                        <br />
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Stretch (Kg)</label>
                                <input type="text" id="upLinhaProdutoQuantidadeStretch" placeholder="999.999" class="form-control" />
                            </div>
                            <div class='radio'>
                                <label for="upEstimativaVendaStretch_1" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioStretch1' name='upEstimativaVendaStretch'
                                        value='30' checked='checked' />30 Dias</label>
                                <label for="upEstimativaVendaStretch_2" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioStretch2' name='upEstimativaVendaStretch'
                                        value='60' />60 Dias</label>
                                <label for="upEstimativaVendaStretch_3" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioStretch3' name='upEstimativaVendaStretch'
                                        value='90' />90 Dias</label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Fita PP (Kg)</label>
                                <input type="text" id="upLinhaProdutoQuantidadeFitaPP" placeholder="999.999" class="form-control" />
                            </div>
                            <div class='radio'>
                                <label for="upEstimativaVendaFitaPP_1" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioFitaPP1' name='upEstimativaVendaFitaPP'
                                        value='30' checked='checked' />30 Dias</label>
                                <label for="upEstimativaVendaFitaPP_2" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioFitaPP2' name='upEstimativaVendaFitaPP'
                                        value='60' />60 Dias</label>
                                <label for="upEstimativaVendaFitaPP_3" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioFitaPP3' name='upEstimativaVendaFitaPP'
                                        value='90' />90 Dias</label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Fita Impressa (Kg)</label>
                                <input type="text" id="upLinhaProdutoQuantidadeFitaImpressa" placeholder="999.999"
                                    class="form-control" /></div>
                            <div class='radio'>
                                <label for="upEstimativaVendaFitaImpressa_1" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioFitaImpressa1' name='upEstimativaVendaFitaImpressa'
                                        value='30' checked='checked' />30 Dias</label>
                                <label for="upEstimativaVendaFitaImpressa_2" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioFitaImpressa2' name='upEstimativaVendaFitaImpressa'
                                        value='60' />60 Dias</label>
                                <label for="upEstimativaVendaFitaImpressa_3" class="form-normal form-text">
                                    <input type='radio' id='upEstimativaVendaRadioFitaImpressa3' name='upEstimativaVendaFitaImpressa'
                                        value='90' />90 Dias</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="upAdicionaisDiv" class="row bg-gray pad-all">
                <div class="col-xs-12 bg-trans-dark">
                    <div class="form-group pad-top">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Valor Total Estimado da Viagem (Transporte+Alimentação+Hotel):</label>
                                <input type="text" id="upValorEstimadoViagem" placeholder="999.999" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <br />
                                <label class="control-label">
                                    Meio de Transporte:</label>
                                <input type="text" id="upMeioTransporte" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <br />
                                <label class="control-label">
                                    KM:</label>
                                <input type="text" id="upKm" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Com representante:</label>
                                <div>
                                    <label for="upComRepresentante_1" class="form-normal form-text">
                                        <input type='radio' id='upComRepresentanteRadio1' name='upComRepresentante' value='Sim' />Sim</label>
                                    <label for="upComRepresentante_2" class="form-normal form-text">
                                        <input type='radio' id='upComRepresentanteRadio2' name='upComRepresentante' value='Não'
                                            checked='checked' />Não</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="upCondicaoVisitaDiv" class="row bg-gray pad-all">
                <div class="col-xs-12 bg-trans-dark">
                    <div class="form-group pad-top">
                        <label class="col-md-4 control-label">
                            Condição de Visita:</label>
                        <div class="col-md-9">
                            <div class='radio'>
                                <label for="upradio_1" class="form-normal form-text">
                                    <input type='radio' id='upradio_1' name='upCondicaoVisita' value='Cliente Novo' checked='checked' />Cliente
                                    Novo</label>
                                <br />
                                <label for="upradio_2" class="form-normal form-text">
                                    <input type='radio' id='upradio_2' name='upCondicaoVisita' value='Recuperação de Inativo' />Recuperação
                                    de Inativo</label>
                                <br />
                                <label for="upradio_3" class="form-normal form-text">
                                    <input type='radio' id='upradio_3' name='upCondicaoVisita' value='Manutenção' />Manutenção</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6 bg-gray mar-btm">
                <p class="text-thin mar-btm">
                    Início</p>
                <div id="demo-dp-component">
                    <div class="input-group date">
                        <input id="upDataInicio" type="Date" class="form-control" />
                        <span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span>
                    </div>
                    <small class="text-muted">Data</small>
                </div>
                <div class="input-group date">
                    <input id="upHoraInicio" type="Time" class="form-control" />
                    <span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span>
                </div>
                <small class="text-muted">Horário</small>
            </div>
            <div class="col-sm-6 bg-gray bord-lft mar-btm">
                <p class="text-thin mar-btm">
                    Final</p>
                <div id="demo-dp-component">
                    <div class="input-group date">
                        <input id="upDataFim" type="date" class="form-control" />
                        <span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span>
                    </div>
                    <small class="text-muted">Data</small>
                </div>
                <div class="input-group date">
                    <input id="upHoraFim" type="Time" class="form-control" />
                    <span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span>
                </div>
                <small class="text-muted">Horário</small>
            </div>
        </div>
        <div class="row pad-no">
            <div class="col-md-6 mar-no">
                <small class="text-muted">Lembre-me</small>
                <div class="form-group">
                    <select name="upidLembreteUm" id="upidLembreteUm">
                        <option value="0.0067">10 minutos antes</option>
                        <option value="0.041">1 hora antes</option>
                        <option value="1">1 dia antes</option>
                        <option value="7">1 semana antes</option>
                    </select></div>


                    


                   
            </div>
            <div class="col-md-6">
                <small class="text-muted">Observações</small>
                <div class="form-group mar-no">
                    <textarea id="upDescricao" name="upDescricao" rows="4" class="form-control">
                    </textarea>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
    </div>
    <!--MODAL DE INSERCAO-->
    <div id="addDialog" style="display: none;" title="Inclui Agendamento">
        <label for="addMensagemLabel" id="addMensagemLabel">
        </label>
        <!--Inicio Hidden DE INSERCAO-->
        <span id="AddIdTipoAgendamentoHidden" style="display: none;"></span><span id="addEntCodHidden"
            style="display: none;"></span><span id="addVinculaEntidadeHidden" style="display: none;">
            </span>
        <!--Fim Hidden DE INSERCAO-->
        <div id="addComEntidadeDiv">
            <div id="addEntidadeDiv" class="row">
                <div class="col-xs-14 bg-gray">
                    <div class="row pad-lft pad-rgt">
                        <table class="table table-condensed">
                            <tbody>
                                <tr class="bg-trans-dark" id="tr1">
                                    <td class="text-right">
                                        Entidade:
                                    </td>
                                    <td class="text-bold">
                                        <span id="addEntNome"></span>
                                    </td>
                                </tr>
                                <tr class="bg-trans-dark">
                                    <td class="text-right">
                                        CNPJ:
                                    </td>
                                    <td class="text-bold">
                                        <span id="addEntCpfCgc"></span>
                                    </td>
                                </tr>
                                <tr class="bg-trans-dark">
                                    <td class="text-right">
                                        Endereço:
                                    </td>
                                    <td class="text-bold">
                                        <span id="addEndereco"></span>
                                    </td>
                                </tr>
                                <tr class="bg-trans-dark">
                                    <td class="text-right">
                                        Tipo de Cliente:
                                    </td>
                                    <td class="text-bold">
                                        <span id="addStatEntComercial"></span>
                                    </td>
                                </tr>
                                <tr class="bg-trans-dark">
                                    <td class="text-right">
                                        Status do Cliente:
                                    </td>
                                    <td class="text-bold">
                                        <span id="addEntStatDescr"></span>
                                    </td>
                                </tr>
                              
                                <tr>
                                    <td class="text-right">
                                        Nome do Contato:
                                    </td>
                                    <td class="text-bold">
                                        <span id="addContatoNome"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right">
                                        Telefone do Contato:
                                    </td>
                                    <td class="text-bold">
                                        <span id="addContatoTelefone"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right">
                                        Email do Contato:
                                    </td>
                                    <td class="text-bold">
                                        <span id="addContatoEmail"></span>
                                    </td>
                                </tr>

                                 <tr>
                                    <td class="text-right">
                                         Classe do Cliente:
                                    </td>
                                    <td class="text-bold">
                                         <select class="selectpicker" name="addClasseCliente" id="addClasseCliente">
                                            <option value="SC">Sem Classificação </option>
                                            <option value="A">Classe - A </option>
                                            <option value="B">Classe - B </option>
                                            <option value="C">Classe - C </option>
                                        </select>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>

         
         
            </div>
         
          <div id="addVisitaAnterioDiv" class="row bg-gray pad-all">
                <div class="col-xs-12 bg-trans-dark">
                    <div class="form-group pad-top">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">
                                    Visita Anterior:</label>
                                <span id="addDataUltimaVisita" class="text-bold"></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group pad-top">
                        <br />
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Ultima Ordem Adquirida:</label>
                                <span id="addNFHoraSaida" class="text-bold"></span>
                            </div>
                        </div>

                       
                        
                    </div>
                    <div class="form-group pad-top">
                    <br />
                        <div class="col-sm-12">
                              
                               <div id="addItensNF"></div>
                           
                         </div>
                    </div>
                    <div class="form-group pad-top">
                        <br />

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">
                                   Valor Total R$:</label>
                                <span id="addNFValTotNota" class="text-bold"></span>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Total de Venda Anual R$:</label>
                                <span id="addTotalVendaAnual" class="text-bold"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="addLinhaProdutoDiv" class="row bg-gray pad-all">
                <div class="col-xs-12 bg-trans-dark">
                    <div class="form-group pad-top">
                        <label class="col-md-4 control-label">
                            Estimativa Venda:</label>
                        <br />
                        <br />
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Stretch (Kg)</label>
                                <input type="text" id="addLinhaProdutoQuantidadeStretch" placeholder="999.999" class="form-control" />
                            </div>
                            <div class='radio'>
                                <label for="addEstimativaVendaStretch_1" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioStretch1' name='addEstimativaVendaStretch'
                                        value='30' checked='checked' />30 Dias</label>
                                <label for="addEstimativaVendaStretch_2" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioStretch2' name='addEstimativaVendaStretch'
                                        value='60' />60 Dias</label>
                                <label for="addEstimativaVendaStretch_3" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioStretch3' name='addEstimativaVendaStretch'
                                        value='90' />90 Dias</label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Fita PP (Kg)</label>
                                <input type="text" id="addLinhaProdutoQuantidadeFitaPP" placeholder="999.999" class="form-control" />
                            </div>
                            <div class='radio'>
                                <label for="addEstimativaVendaFitaPP_1" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioFitaPP1' name='addEstimativaVendaFitaPP'
                                        value='30' checked='checked' />30 Dias</label>
                                <label for="addEstimativaVendaFitaPP_2" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioFitaPP2' name='addEstimativaVendaFitaPP'
                                        value='60' />60 Dias</label>
                                <label for="addEstimativaVendaFitaPP_3" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioFitaPP3' name='addEstimativaVendaFitaPP'
                                        value='90' />90 Dias</label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Fita Impressa (Kg)</label>
                                <input type="text" id="addLinhaProdutoQuantidadeFitaImpressa" placeholder="999.999"
                                    class="form-control" /></div>
                            <div class='radio'>
                                <label for="addEstimativaVendaFitaImpressa_1" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioFitaImpressa1' name='addEstimativaVendaFitaImpressa'
                                        value='30' checked='checked' />30 Dias</label>
                                <label for="addEstimativaVendaFitaImpressa_2" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioFitaImpressa2' name='addEstimativaVendaFitaImpressa'
                                        value='60' />60 Dias</label>
                                <label for="addEstimativaVendaFitaImpressa_3" class="form-normal form-text">
                                    <input type='radio' id='addEstimativaVendaRadioFitaImpressa3' name='addEstimativaVendaFitaImpressa'
                                        value='90' />90 Dias</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="addAdicionaisDiv" class="row bg-gray pad-all">
                <div class="col-xs-12 bg-trans-dark">
                    <div class="form-group pad-top">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Valor Total Estimado da Viagem (Transporte+Alimentação+Hotel):</label>
                                <input type="text" id="addValorEstimadoViagem" placeholder="999.999" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <br />
                                <label class="control-label">
                                    Meio de Transporte:</label>
                                <input type="text" id="addMeioTransporte" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <br />
                                <label class="control-label">
                                    KM:</label>
                                <input type="text" id="addKm" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">
                                    Com representante:</label>
                                <div>
                                    <label for="addComRepresentante_1" class="form-normal form-text">
                                        <input type='radio' id='addComRepresentanteRadio1' name='addComRepresentante' value='Sim' />Sim</label>
                                    <label for="addComRepresentante_2" class="form-normal form-text">
                                        <input type='radio' id='addComRepresentanteRadio2' name='addComRepresentante' value='Não'
                                            checked='checked' />Não</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="addCondicaoVisitaDiv" class="row bg-gray pad-all">
                <div class="col-xs-12 bg-trans-dark">
                    <div class="form-group pad-top">
                        <label class="col-md-4 control-label">
                            Condição de Visita:</label>
                        <div class="col-md-9">
                            <div class='radio'>
                                <label for="addradio_1" class="form-normal form-text">
                                    <input type='radio' id='addradio_1' name='addCondicaoVisita' value='Cliente Novo'
                                        checked='checked' />Cliente Novo</label>
                                <br />
                                <label for="addradio_2" class="form-normal form-text">
                                    <input type='radio' id='addradio_2' name='addCondicaoVisita' value='Recuperação de Inativo' />Recuperação
                                    de Inativo</label>
                                <br />
                                <label for="addradio_3" class="form-normal form-text">
                                    <input type='radio' id='addradio_3' name='addCondicaoVisita' value='Manutenção' />Manutenção</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6 bg-gray mar-btm">
                <p class="text-thin mar-btm">
                    Início</p>
                <div id="Div1">
                    <div class="input-group date">
                        <input id="addDataInicio" type="date" class="form-control" />
                        <span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span>
                    </div>
                    <small class="text-muted">Data</small>
                </div>
                <div class="input-group date">
                    <input id="addHoraInicio" type="Time" class="form-control" />
                    <span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span>
                </div>
                <small class="text-muted">Horário</small>
            </div>
            <div class="col-sm-6 bg-gray bord-lft mar-btm">
                <p class="text-thin mar-btm">
                    Final</p>
                <div id="Div2">
                    <div class="input-group date">
                        <input id="addDataFim" type="date" class="form-control" />
                        <span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span>
                    </div>
                    <small class="text-muted">Data</small>
                </div>
                <div class="input-group date">
                    <input id="addHoraFim" type="Time" class="form-control" />
                    <span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span>
                </div>
                <small class="text-muted">Horário</small>
            </div>
        </div>
        <div class="row pad-no">
            <div class="col-md-6 mar-no">
                <small class="text-muted">Lembre-me</small>
                <div class="form-group">
                    <select class="selectpicker" name="addidLembreteUm" id="addidLembreteUm">
                        <option value="0.0067">10 minutos antes</option>
                        <option value="0.041">1 hora antes</option>
                        <option value="1">1 dia antes</option>
                        <option value="7">1 semana antes</option>
                    </select></div>
            </div>
            <div class="col-md-6">
                <small class="text-muted">Observações</small>
                <div class="form-group mar-no">
                    <textarea id="addDescricao" rows="4" class="form-control">
                    </textarea>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
    </div>
    <!--FIM MODAL DE INSERCAO-->
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
    <!--Inicio Hidden eh utilizado para quando selecionar Tipo de  Agendamento que Vincula Entidade-->
    <asp:HiddenField ID="idTipoAgendamentoVincularEntidadeHiddenField" runat="server" />
    <asp:HiddenField ID="EntCodHiddenField" runat="server" />
    <!--Fim Hidden eh utilizado para quando selecionar Tipo de Agendamento que Vincula Entidade-->

    <script>
        /*************************************************************************
        Start Custom Manuli
        ************************************************************************/



        function TipoAgendamentoCheckBox(idTipoAgendamento) {
            /*
            Essa funcao eh chamada quando clica nos Checks para Filtro da Agenda
            */
            document.getElementById("ctl00_ContentPlaceHolder1_idTipoAgendamentoCheckBoxHiddenField").value += idTipoAgendamento + ",";

        }

        function TipoAgendamentos(VinculaEntidade, idTipoAgendamento) {
            /*
            Essa funcao eh chamada quando clica no Tipo de Agendamento para definir qual Modal Abrir
            */

            if (VinculaEntidade.toString() == 'True') {
                //abre tela Entidade
                //alert("Chamar Tela para Buscar Entidade e Depois Retornar abrindo Modal");
                document.getElementById("ctl00_ContentPlaceHolder1_idTipoAgendamentoVincularEntidadeHiddenField").value = idTipoAgendamento;

                __doPostBack('ChamaServidor', idTipoAgendamento);
                //$('#addDialog').dialog('open');

            }
            else {

                $('#addDialog').dialog('open');
                $('#AddIdTipoAgendamentoHidden').text(idTipoAgendamento);
                $('#addVinculaEntidadeHidden').text(VinculaEntidade);
                $("#addEntNome").text('');
                $("#addEntCodHidden").text('');
                $("#addEntCpfCgc").text('');
                $("#addEndereco").text('');
                $("#addContatoNome").text('');
                $("#addContatoTelefone").text('');
                $("#addContatoEmail").text('');
                $("#addLinhaProdutoQuantidadeStretch").val('0');
                $("#addLinhaProdutoQuantidadeFitaPP").val('0');
                $("#addLinhaProdutoQuantidadeFitaImpressa").val('0');


                $("INPUT[name=addCondicaoVisita]").val(['Manutenção']);

                $("#addDataInicio").val('');
                $("#addHoraInicio").val('');
                $("#addDataFim").val('');
                $("#addHoraFim").val('');

                $('select[name=addidLembreteUm]').val('0.0067');
                $('select[name=addClasseCliente]').val('SC');
                
                $('#addDescricao').val('');


                //Limpa a Msg
                $('#addMensagemLabel').html('');




                $("INPUT[name=addEstimativaVendaFitaImpressa]").val(['30']);
                $("INPUT[name=addEstimativaVendaFitaPP]").val(['30']);
                $("INPUT[name=addEstimativaVendaStretch]").val(['30']);
                $("INPUT[name=addComRepresentante]").val(['Não']);
                $("#addMeioTransporte").val('');
                $("#addKm").val('0');
                $("#addValorEstimadoViagem").val('0');




                $("#addComEntidadeDiv").hide();






            }

        }








        /*************************************************************************
        End Custom Manuli
        ************************************************************************/




  

    </script>
</asp:Content>
