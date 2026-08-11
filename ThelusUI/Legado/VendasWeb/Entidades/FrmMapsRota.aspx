<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master"  AutoEventWireup="true" CodeBehind="FrmMapsRota.aspx.cs" Inherits="VendasWeb.Entidades.FrmMapsRota" %>


<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/CssMaps.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
<asp:Label ID="MenssagemMasterLabel" runat="server" Text="" Visible="false"></asp:Label>


  <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-8">
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
                        Maps Rota</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->

             <!-- LINHA 1-->
                <div class="panel-body" >
                 <div class="table-responsive" id="ImgMapa">
                 
                   <center>
                    <div id="mapa" style="height:500px; min-width: 150px; max-width: 800px;"> </div>
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
                    <script> carregaRota();</script>
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

        

          <!-- COLUNA 1-->
        <div class="col-sm-4">
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
                    <h3 class="panel-title">
                        Rota Detalhada</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->

                <div class="table-responsive">
                 <div class="panel-body" >   
                     <div id="trajeto-texto"  style="overflow: auto; height:500px;">  </div>
                </div>
                </div>
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
               


                     <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">



                              <asp:LinkButton ID="ImprimirRotaDetalhadaLinkButton" class="btn btn-success btn-labeled fa fa-print fa-lg" CausesValidation="false"
                                    runat="server" title="Voltar" data-rel="tooltip" OnClientClick="JavaScript: printPartOfPage('trajeto-texto');" > Imprimir </asp:LinkButton>

                            
                        </div>
                    </div>
                </div>

            </div>
        </div>







    </div>
   


<script type="text/javascript">
 <!--
    function printPartOfPage(elementId) {
        var printContent = document.getElementById(elementId);
        var windowUrl = 'about:blank';
        var uniqueName = new Date();
        var windowName = 'Print' + uniqueName.getTime();
        var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');

        printWindow.document.write(printContent.innerHTML);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    }
    // -->
 </script>

 
   


</asp:Content>
