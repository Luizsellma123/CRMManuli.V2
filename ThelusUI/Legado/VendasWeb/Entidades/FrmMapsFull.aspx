<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmMapsFull.aspx.cs" Inherits="VendasWeb.Entidades.FrmMapsFull" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
        <link href="../css/CssMaps.css" rel="stylesheet" type="text/css" />

    <!--STYLESHEET-->
    <!--=================================================-->
    <!--Open Sans Font [ RECOMENDADO ] -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700&amp;subset=latin"
        rel="stylesheet">
    <!--Bootstrap Stylesheet [ NECESSÁRIO ]-->
    <link href="<%=Page.ResolveClientUrl("~/css/bootstrap.min.css")%>" rel="stylesheet">
    <!--CRM Manuli Fitasa Stylesheet [ NECESSÁRIO ]-->
    <link href="<%=Page.ResolveClientUrl("~/css/crm-manulifitasa.min.css")%>" rel="stylesheet">
    <!--Font Awesome [ RECOMENDADO ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/font-awesome/css/font-awesome.min.css?aux=2")%>"
        rel="stylesheet">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css">
    <!--Bootstrap Select [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/bootstrap-select/bootstrap-select.min.css")%>"
        rel="stylesheet">
    <!--Bootstrap Table [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/bootstrap-table/bootstrap-table.min.css")%>"
        rel="stylesheet">
    <!--Bootstrap Tags Input [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/bootstrap-tagsinput/bootstrap-tagsinput.css")%>"
        rel="stylesheet">
    <!--FooTable [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/fooTable/css/footable.core.css")%>"
        rel="stylesheet">
    <!--X-editable [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/x-editable/css/bootstrap-editable.css")%>"
        rel="stylesheet">
    <!--Chosen [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/chosen/chosen.min.css")%>" rel="stylesheet">
    <!--Bootstrap Datepicker [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/bootstrap-datepicker/bootstrap-datepicker.css")%>"
        rel="stylesheet">
    <!--Animate.css [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/animate-css/animate.min.css")%>"
        rel="stylesheet">
    <!--Morris.js [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/morris-js/morris.min.css")%>" rel="stylesheet">
    <!--Switchery [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/switchery/switchery.min.css")%>"
        rel="stylesheet">
    <!--Full Calendar [ OPTIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/fullcalendar/fullcalendar.css")%>"
        rel="stylesheet">
    <!--Demo [ DEMONSTRAÇÃO ]-->
    <link href="<%=Page.ResolveClientUrl("~/css/demo/demo.min.css")%>" rel="stylesheet">
    <!--CUSTOMIZAÇOES [NECESSARIO]--->
    <link href="<%=Page.ResolveClientUrl("~/css/Custom.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveClientUrl("~/css/AlertBootstrap.css")%>" rel="stylesheet">
    <!--SCRIPT-->
    <!--=================================================-->
    <!--Page Load Progress Bar [ OPCIONAL ]-->
    <link href="<%=Page.ResolveClientUrl("~/plugins/pace/pace.min.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveClientUrl("~/plugins/pace/pace.min.js")%>"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>


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
                        <%--<button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
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
                    <div id="mapa" style="height:800px; min-width: 150px; max-width: 2048px;"> </div>
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
                     <script src="../js/mapa.js"></script>
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




        <!--JAVASCRIPT-->
    <!--=================================================-->
    <!--jQuery [ NECESSÁRIO ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/jquery-2.1.1.min.js")%>"></script>
    <!--BootstrapJS [ RECOMENDADO ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/bootstrap.min.js")%>"></script>
    <!--Fast Click [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/fast-click/fastclick.min.js")%>"></script>
    <!-- Admin [ RECOMENDADO ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/crm-manulifitasa.min.js")%>"></script>
    <!--Switchery [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/switchery/switchery.min.js")%>"></script>
    <!--Bootstrap Select [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/bootstrap-select/bootstrap-select.min.js")%>"></script>
    <!--Bootstrap Tags Input [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/bootstrap-tagsinput/bootstrap-tagsinput.min.js")%>"></script>
    <!--X-editable [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/x-editable/js/bootstrap-editable.min.js")%>"></script>
    <!--Chosen [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/chosen/chosen.jquery.min.js")%>"></script>
    <!--Bootstrap Datepicker [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/bootstrap-datepicker/bootstrap-datepicker.js")%>"></script>

    <% if (Page.ToString().ToUpper() == "ASP.ENTIDADES_FRMCALENDARIO_ASPX") {%>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="<%=Page.ResolveClientUrl("~/js/demo/jquery.qtip-2.2.0.js")%>"></script>
    <%} %>
    <!--Bootbox Modals [ OPTIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/bootbox/bootbox.min.js")%>"></script>
    <!--Full Calendar [ OPTIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/fullcalendar/lib/moment.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/fullcalendar/lib/jquery-ui.custom.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/fullcalendar/fullcalendar.min.js")%>"></script>
    <%--<script src="<%=Page.ResolveClientUrl("~/plugins/fullcalendar/fullcalendar-2.0.3")%>"></script>--%>
    <script src="<%=Page.ResolveClientUrl("~/plugins/fullcalendar/lang/pt-br.js")%>"></script>
    <!--Calendário [ AMOSTRAS ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/demo/calendarscript.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/js/demo/calendario-amostras.js")%>"></script>
    <!--Modal [ AMOSTRAS ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/demo/calendario-modal.js")%>"></script>
    <!--Modal [ AMOSTRAS ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/demo/rotina-modal.js")%>"></script>
    <!--Bootstrap Table [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/bootstrap-table/bootstrap-table.min.js")%>"></script>
    <!--Bootstrap Table Extension [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/bootstrap-table/extensions/editable/bootstrap-table-editable.js")%>"></script>
    <!--Bootstrap Table AMOSTRA [ AMOSTRA ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/demo/tables-bs-table.js")%>"></script>
    <!--FooTable [ OPCIONAL ]-->
    <script src="<%=Page.ResolveClientUrl("~/plugins/fooTable/dist/footable.all.min.js")%>"></script>
    <!--FooTable Example [ AMOSTRA ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/demo/tables-footable.js")%>"></script>
    <!--Demo script [ DEMONSTRAÇÃO ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/demo/demo.min.js")%>"></script>
    <!--Form Component [ AMOSTRA ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/demo/form-component.js")%>"></script>


    </form>
</body>
</html>
