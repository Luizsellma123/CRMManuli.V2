<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmMaps.aspx.cs" Inherits="VendasWeb.Entidades.FrmMaps" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/CssMaps.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   



  <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-12">
            <!--===================================================-->
            <!--Painel  e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info ">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <!--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>-->
                       <%-- <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>--%>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                    <h3 class="panel-title">
                        Maps</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->

             <!-- LINHA 1-->
                <div class="panel-body" >
                 <div class="table-responsive">
                 
                   <center>
                    <div id="mapa" style="height:500px; min-width: 150px; max-width: 1048px;"> </div>
                    </center>
                    <script src="../js/jquery.min.js"></script>
                    <!-- Maps API Javascript -->
                    <script src="http://maps.googleapis.com/maps/api/js?key=AIzaSyAQ6WwK2FETcMSwC0Be5fB290h1-yojs3I&amp;sensor=false"></script>
                    <!-- Caixa de informação -->
                    <script src="../js/infobox.js"></script>
                    <!-- Agrupamento dos marcadores -->
                    <!--<script src="../js/markerclusterer.js"></script>-->
                    <script src="https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/markerclusterer.js">
                    </script>
    
                    <!-- Arquivo de inicialização do mapa -->
                    <script src="../js/mapa.js?indmnu=6"></script>
                    <script> carregarPontos();</script>
                </div>
                </div>


                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">



                            <asp:LinkButton ID="VoltarButton" class="btn btn-warning btn-labeled fa fa-arrow-circle-left fa-lg" CausesValidation="false"
                                    runat="server" title="Voltar" data-rel="tooltip" OnClick="VoltarLinkButton_Click"> Retornar </asp:LinkButton>





                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->

        <!----PAINEL----->
       
    </div>
   




     <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-6">
            <!--===================================================-->
            <!--Painel  e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info ">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <!--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>-->
                        <%--<button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>--%>
                    </div>
                    <h3 class="panel-title">
                        Legenda</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->

             <!-- LINHA 1-->
                <div class="panel-body" >
                 <div class="table-responsive">
                 
                     
                     <div>
                         <div class="fc-event fc-list ui-draggable ui-draggable-handle" data-class="danger">Clientes Inativos</div>
                         <div class="fc-event fc-list ui-draggable ui-draggable-handle" data-class="blue">Clientes Ativos </div>
                         <div class="fc-event fc-list ui-draggable ui-draggable-handle" data-class="warning">Clientes Prospectivos</div>
                         <div class="fc-event fc-list ui-draggable ui-draggable-handle" data-class="success">Clientes reativados nos últimos 30 dias</div>
                         <div class="fc-event fc-list ui-draggable ui-draggable-handle" data-class="purple">Clientes perdidos recentes</div>

                     </div>


                      <hr />
                    
                     



                       <asp:LinkButton ID="MapaFullLinkButton" class="btn btn-primary btn-labeled fa fa-arrows-alt fa-lg"
                                    runat="server" title="Abrir Mapa tela Cheia" data-rel="tooltip" target="_blank" OnClientClick="window.open('FrmMapsFull.aspx?indmnu=5');"
                                    CausesValidation="False"> 
             Expandir Mapa </asp:LinkButton>



                      
                </div>
                </div>


                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">

                            
                       

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->

        <!----PAINEL----->
       
    </div>
   


</asp:Content>
