<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="VendasWeb.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .carousel-inner > .item > img, .carousel-inner > .item > a > img
        {
            width: 70%;
            margin: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pad-btm mar-btm text-center">
        <h2 class="text-thin mar-no">
            Bem vindo ao novo <a class="text-primary">Sistema Comercial</a>
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
                <ul class="list-group bord-no">
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
                    </li>
                    <li class="list-group-item mar-ver">
                        <div class="media-heading">
                            <span class="label label-warning">Novidade</span> <a class="h4 btn-link" href="Entidades/FrmCarteira.aspx?indmnu=5">Consulta
                                de carteiras</a>
                        </div>
                        <p>
                            A primeira funcionalidade a ser implementada é a de consulta dos clientes. Em breve,
                            todas as informações sobre o cliente estarão disponíveis para a equipe de vendas
                            de forma clara e acessível.</p>
                    </li>
                    
                    <img class="img-responsive" alt="Novo CRM" src="img/Apresentacao.jpg">
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
