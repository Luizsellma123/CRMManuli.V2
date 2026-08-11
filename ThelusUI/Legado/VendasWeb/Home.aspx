<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="VendasWeb.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .carousel-inner > .item > img, .carousel-inner > .item > a > img {
            width: 70%;
            margin: auto;
        }
    </style>

    <style>
        .imagemHome {
            width: 100%;
            float: left;
            margin-right: 10px;
        }

        .teste table{
            width: 100%;
            border: 1px solid #ddd;
        }

        .teste table tr td{
            border: 1px solid #ddd;
            background-color: transparent;
            font-family: 'Open Sans','Helvetica Neue',Helvetica,Arial,sans-serif;
            font-size: 12px;
            font-weight: normal;
            color: #5f5f5f;
        }

        @media (max-width: 1335px) {
            .imagemHome {
                width: 100%;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pad-btm mar-btm text-center">
        <h2 class="text-thin mar-no">Bem vindo ao novo <a class="text-primary">Sistema Comercial</a>
        </h2>
    </div>
    <div class="tab-base">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs">
        </ul>
        <!-- Tabs Content -->
        <div class="tab-content">
            
            <!-- DEFAULT SEARCH LAYOUT -->
            <!--===================================================-->
            <div class="tab-pane fade active in" id="demo-search-tab-1">

                <%--<a href="imagens/Ppt2.jpg" target="_blank">
                    <img border="0" alt="Notificações"
                        class="imagemHome"
                        src="imagens/Ppt.jpg" class="img-responsive">
                </a>--%>
                <!--UOL Widgets - widgets.uol.com.br -->
                <!-- <h3 class="panel-title" style="background: #212121; color: #ffffff; font-weight: bold;">Indicadores</h3> -->
                <%--<div class="teste"><script type="text/javascript" src="http://www.debit.com.br/resumogratuito.php?info=inflacao"></script></div>--%>

                </br>
                <%--<iframe src="https://sslecal2.forexprostools.com?columns=exc_flags,exc_currency,exc_importance,exc_actual,exc_forecast,exc_previous&category=_economicActivity,_inflation,_credit,_confidenceIndex,_Bonds&countries=32&calType=day&timeZone=12&lang=12" width="550" height="425" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0"></iframe>--%>
                <!--//UOL Widgets-->

                <iframe src="https://br.widgets.investing.com/live-currency-cross-rates?theme=darkTheme&pairs=1473,1617,2103" width="100%" height="300px" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0"></iframe>
                </br>
                <iframe src="https://br.widgets.investing.com/live-commodities?theme=darkTheme&pairs=8833,8849" width="100%" height="200px" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0"></iframe>

                 <ul class="list-group bord-no">
                    <%-- 
                     <li class="list-group-item mar-ver">
                        <div class="media-heading">
                            <a class="h4 btn-link" href="#">Novas funcionalidades</a>
                        </div>
                        <p>
                            O novo Sistema Comercial está sendo desenvolvido e irá trazer mais agilidade em
                            vários processos. Nos próximos meses iremos implantar novas funcionalidades para
                            facilitar ainda mais o acesso aos dados e, com isso, estimular as vendas.</p>
                    </li>
                    <li class="list-group-item mar-ver media">
                        <div class="">
                        </div>
                        <div class="media-body">
                            <div class="media-heading">
                                <a class="h4 btn-link" href="#">Agilidade e Facilidade na Visualização de Dados</a>
                            </div>
                            <p>
                                O novo Sistema foi desenhado pensando na facilidade de utilização. A interface traz
                                toda a informação para mais perto do usuário final, seja no seu computador, laptop,
                                tablet ou até mesmo diretamente no seu celular.</p>
                        </div>
                    </li>--%>
                    <li class="list-group-item mar-ver">
                        <div class="media-heading">
                            <span class="label label-warning">Novidade</span> <a class="h4 btn-link" href="documentos\ArquivosWeb\Manual_Cadastro_Cliente.pdf" target="_blank">Cadastro de Clientes</a>
                        </div>
                        <p>
                            Desenvolvido de forma a integrar com o sistema SAP e trazer maior agilidade e praticidade aos usuários o cadastro de cliente conta uma interface inovadora e de fácil utilização, aproveita esta nova funcionalidade que foi criada para aumentar a agilidade dos processos. Basta clicar no link acima para ter acesso ao manual que te ensinará passo a passo como utilizar esta poderosa ferramenta.</p>
                    </li>
                    
                    <%--<img class="img-responsive" alt="Novo CRM" src="img/Apresentacao.jpg">--%>
                    </ul>
            </div>
            <!--===================================================-->
        </div>
    </div>
    <%--     <div id="myCarousel" class="carousel slide"  data-ride="carousel">
    <!-- Indicators -->
       <ol class="carousel-indicators">
          
           <asp:Literal ID="IndicadoresLiteral" runat="server"></asp:Literal>
       </ol>
      
    <!-- Wrapper for slides -->
    <div class="carousel-inner" role="listbox">
      
        <asp:Literal ID="BannerLiteral" runat="server"></asp:Literal>


    </div>

    <!-- Left and right controls -->
    <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
      <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
      <span class="sr-only">Anterior</span>
    </a>
    <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
      <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
      <span class="sr-only">Proximo</span>
    </a>
  </div>--%>

</asp:Content>
