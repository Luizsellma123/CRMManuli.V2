<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="frmClassificacaoEntidade.aspx.cs" Inherits="VendasWeb.Entidades.frmClassificacaoEntidade" %>

<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%--Inicia Js Para Footable--%>
    <%--<script type="text/javascript" src="../template/footable/js/footable.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <%--Fim Js Para Footable--%>
    
    
       
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
                                <button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                                    data-target="#filtros">
                                    <i class="fa fa-refresh"></i>
                                </button>
                                <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                                    <i class="fa fa-chevron-down"></i>
                                </button>
                                <button type="button" class="btn btn-default" data-dismiss="panel">
                                    <i class="fa fa-times"></i>
                                </button>
                            </div>
                            <h3 class="panel-title">
                                Selecionar Clientes</h3>
                        </div>
                        <!--Painel Aberto-->
                        <!--Campos para escolha da carteira e do cliente-->
                        <div id="painel_aberto" class="">
                            <div class="panel-body">
                                <!--LINHA 1 - Painel Aberto-->
                                <div class="row">
                                    <div class="col-lg-5">
                                        <asp:MultiView ID="VendedorMultView" runat="server" ActiveViewIndex="0">
                                            <asp:View ID="VendedorView" runat="server">
                                                <div class="col-lg-5">
                                                    <select class="selectpicker show-tick" multiple data-placeholder="Escolha um vendedor..."
                                                        title="Escolha um vendedor..." data-style="btn-primary" data-live-search="true"
                                                        id="VendedoresSelect" runat="server">
                                                    </select>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <asp:DropDownList ID="drpEntCod" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">NOME FANTASIA</asp:ListItem>
                                                <asp:ListItem Value="2" Selected="True">RAZÃO SOCIAL</asp:ListItem>
                                                <asp:ListItem Value="3">CÓD.ENTIDADE</asp:ListItem>
                                                <asp:ListItem Value="4">CNPJ</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtFiltroEntCod" runat="server" placeholder="Procurar" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <!--END LINHA 1 - Painel Aberto-->
                                <!--===================================================-->
                            </div>
                        </div>
                        <!-- END Painel Aberto-->
                        <!--===================================================-->
                        <!--Painel FILTROS-->
                        <!--===================================================-->
                        <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>
                        <div class="panel-body">
                            <!-- LINHA 1 - Painel FILTROS-->
                            <div class="row">
                                <div class="col-xs-12">
                                    <h5 class="text-bold">
                                        Filtros</h5>
                                    <hr>
                                </div>
                                <div class="col-sm-3">
                                    <h5>
                                        <asp:Label ID="StatusEntidadeLabel" runat="server" Text="Status de Cadastro:" CssClass="text-thin"></asp:Label></h5>
                                    <asp:DropDownList ID="StatusEntidadeDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <h5>
                                        <asp:Label ID="StatusComercialLabel" runat="server" Text="Status Comercial:" CssClass="text-thin"></asp:Label></h5>
                                    <asp:DropDownList ID="StatusComercialDropDownList" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <!--===================================================-->
                            <!-- END LINHA 1 - Painel FILTROS-->
                            <!-- LINHA 2 - Painel FILTROS-->
                            <asp:UpdatePanel ID="EstadoUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <hr>
                                        <div class="col-sm-3">
                                            <h5>
                                                <asp:Label ID="UfLabel" runat="server" Text="Estado:" CssClass="text-thin"></asp:Label></h5>
                                            <asp:DropDownList ID="UfDropDownList" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="UfDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <h5>
                                                <asp:Label ID="LabelCidade" runat="server" Text="Cidade:" CssClass="text-thin"></asp:Label></h5>
                                            <select class="selectpicker show-tick" multiple data-placeholder="Escolha uma Cidade..."
                                                title="Escolha uma Cidade..." data-style="btn-primary" data-live-search="true"
                                                id="CidadeSelect" runat="server">
                                            </select>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <hr>
                            <!--===================================================-->
                            <!-- END LINHA 2 - Painel FILTROS-->
                            <!-- LINHA 3 - Painel FILTROS-->
                            <asp:UpdatePanel ID="LinhaProdutoUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <h5>
                                                <asp:Label ID="LinhaProdutoLabel" runat="server" Text="Linha do Produto:" CssClass="text-thin"></asp:Label></h5>
                                            <asp:DropDownList ID="LinhaProdutoDropDownList" runat="server" CssClass="form-control"
                                                Width="100px" AutoPostBack="true" OnSelectedIndexChanged="LinhaProdutoDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <h5>
                                                <asp:Label ID="ProdutoLabel" runat="server" Text="Produto:" CssClass="text-thin"></asp:Label></h5>
                                            <select class="selectpicker show-tick" multiple data-placeholder="Escolha um Produto..."
                                                width="100px" title="Escolha um Produto..." data-style="btn-primary" data-live-search="true"
                                                id="ProdutoSelect" runat="server">
                                            </select>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <hr>
                    <hr>
                    <!--===================================================-->
                    <!-- END LINHA 5 - Painel FILTROS-->--%>
                        </div>
                    </div>
                    <!-- END Painel FILTROS-->
                    <!--===================================================-->
                    <!-- Panel Footer-->
                    <!-- Botões de buscar e limpar-->
                    <!--===================================================-->
                    <div class="panel-footer">
                        <div class="row">
                            <div class="panel-control">
                                <asp:LinkButton ID="btnListar" class="btn btn-success btn-labeled fa fa-search fa-lg"
                                    runat="server" title="Buscar Cliente" data-rel="tooltip" OnClick="btnListar_Click"
                                    CausesValidation="False"> 
             Buscar Cliente </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

                 
                <!--===================================================-->
                <!--End Painel Carteiras e Filtros-->
                <!--===================================================-->
                <asp:MultiView ID="ClientesMultiView" runat="server" ActiveViewIndex="0" Visible="false">
                    <asp:View ID="ClientesView" runat="server">
                        <!-- TABELA -->
                        <!--===================================================-->
                        <div class="panel">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Clientes
                                </h3>
                            </div>
                            <!-- Foo Table - Filtering -->
                            <!--===================================================-->
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="ListaEntidadeGridView" EmptyDataText="Nenhum Cliente Localizado"
                                        AutoGenerateColumns="False" runat="server" AllowPaging="True" OnPageIndexChanging="ListaEntidadeGridView_PageIndexChanged"
                                        CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                        Style="border-collapse: collapse; max-width: 100%" >
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="EntCod" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="EntCodLabel" runat="server" Text='<%# Bind("EntCod") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CNPJ/CPF">
                                                <ItemTemplate>
                                                    <asp:Label ID="EntCpfCgcLabel" runat="server" Text='<%# Bind("EntCpfCgc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nome">
                                                <HeaderStyle Width="100%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("NOME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Classificacao">
                                                <HeaderStyle Width="100%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="ClassificacaoLabel" runat="server" Text='<%# Bind("Classificacao") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            

                                            <asp:TemplateField HeaderStyle-Width="100%" HeaderText="Detalhe">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="TesteUpdatePanel" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>

                                                    <asp:Button ID="btnVerDetalhe" runat="server" Text="Classificar"
                                                      
                                                      onClientClick=<%# string.Format("ShowCal('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",Eval("EntCod"),Eval("EntCpfCgc"),Eval("NOME"),Eval("CidNome"),Eval("DataUltimoContato"),Eval("StatEntComercial"),Eval("VendCod"),Eval("VendNome"),Eval("VendClasseDescr"),Eval("Telefone1"),Eval("Telefone2"),Eval("ContatoNome"),Eval("ContatoTelefone"),Eval("ContatoEmail"),Eval("DataUltimoContato"),Eval("UsuarioUltimoHistorico"),Eval("UltimoHistorico"),Eval("AcessoEntidade")) %>
                                                        CssClass = "btn btn-danger" />

                                                        
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                 </ItemTemplate>
                                                <HeaderStyle Width="100%" />
                                            </asp:TemplateField>

                                            
                                        
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <!--===================================================-->
                        </div>
                        <!-- End Foo Table - Filtering -->
                        <!--===================================================-->
                        <!-- END TABELA -->
                    </asp:View>
                </asp:MultiView>
            </div>
            <!----PAINEL----->
            <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
            <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
            </div>
            <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
            <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->

 <asp:HiddenField ID="OperacaoHiddenField" runat="server" />
 <asp:HiddenField ID="HistoricoHiddenField" runat="server" />
 <asp:HiddenField ID="EventoHiddenField" runat="server" />
 <asp:HiddenField ID="CategoriaHiddenField" runat="server" />
 <asp:HiddenField ID="DataHiddenField" runat="server" />
 <asp:HiddenField ID="HoraHiddenField" runat="server" />
 <asp:HiddenField ID="CodigoHiddenField" runat="server" />
 <asp:HiddenField ID="ClassificacaoHiddenField" runat="server" />

    <%--Inicia Js Para tratar Looad Footable--%>
    <script type="text/javascript">


        /*
        $(function () {
        $('[id*=ListaEntidadeGridView]').footable({
        breakpoints: {
        phone: 480,
        //tablet: 1024
        tablet: 2024
        }

        });






        });

        */



        function Picker() {

            //Essa Função é necessaria quando utilizado Picker no footable.
            //Mapear todos os Picker da Tela que estiverem dentro de um Panel

            $("#<%=this.VendedoresSelect.ClientID%>").selectpicker();
            $("#<%=this.CidadeSelect.ClientID%>").selectpicker();
            $("#<%=this.ProdutoSelect.ClientID%>").selectpicker();


            /*
            $('[id*=ListaEntidadeGridView]').footable({
            breakpoints: {
            phone: 480,
            //tablet: 1024
            tablet: 2024
            }

            });
            */
        }




    </script>
    <%--Fim Js Para tratar Looad Footable--%>

  
    <!--Inicia Scrip para Tratar o combo no Modal-->
      <script type="text/javascript">


            function ShowCal(Codigo,Cnpj,Nome,Cidade,UltimoContato,SituacaoComercial,
                             VendCod,VendNome,VendClasseDescr,Telefone1,Telefone2,
                             ContatoNome,ContatoTelefone,ContatoEmail,
                             DataUltimoContato,UsuarioUltimoHistorico,UltimoHistorico,AcessoEntidade
            
            ){



            var Contato = ''
            if(AcessoEntidade == "ADM" || AcessoEntidade == "ENTIDADE_VENDEDOR" || AcessoEntidade == "LIVRE")
              {
                   Contato = '<table class="table table-condensed table-responsive"><thead><tr class="bg-gray-dark">'
                   +'<th>Nome do contato</th>'
                   +'<th>Telefone</th>'
                   +'<th>E-mail</th>'
                   +'</tr></thead>'
                   +'<tbody><tr>'
                   +'<td>'+ContatoNome+'</td>'
                   +'<td>'+ContatoTelefone+'</td>'
                   +'<td>'+ContatoEmail+'</td>'
                   +'</tr></tbody></table>'
             }


             bootbox.dialog({
                 title: "Alterar Classificação",
                 size: "large",
                 message: '<div class="row"><div class="col-md-12 pad-top bg-gray"><div class="row pad-lft pad-rgt" >'
                   + '<table class="table table-condensed table-responsive"><thead><tr class="bg-gray-light">'
                   + '<th>Código</th>'
                   + '<th>CNPJ/CPF</th>'
                   + '<th>Nome</th>'
                   + '<th>Cidade</th>'
                   + '<th>Último Contato</th>'
                   + '<th>Situação Comercial</th>'
                   + '</tr></thead><tbody>'
                   + '<tr class="bg-gray-light">'
                   + '<td>  <label for="Codigo" id="Codigo" >' + Codigo + '</label></td>'
                   + '<td>' + Cnpj + '</td>'
                   + '<td>' + Nome + '</td>'
                   + '<td>' + Cidade + '</td>'
                   + '<td>' + UltimoContato + '</td>'
                   + '<td>' + SituacaoComercial + '</td>'
                   + '</tr></tbody></table>'

                   + '<table class="table table-condensed table-responsive"><thead><tr class="bg-gray-light">'
                   + '<th>Código do Vendedor</th>'
                   + '<th>Nome Vendedor</th>'
                   + '<th>Classe</th>'
                   + '<th>Telefone 1</th>'
                   + '<th>Telefone 2</th></tr></thead>'
                   + '<tbody><tr class="bg-gray-light">'
                   + '<td>' + VendCod + '</td>'
                   + '<td>' + VendNome + '</td>'
                   + '<td>' + VendClasseDescr + '</td>'
                   + '<td>' + Telefone1 + '</td>'
                   + '<td>' + Telefone2 + '</td>'
                   + '</tr></tbody></table>'

                 /*+ Contato

                 +'</div></div></div>'
                 +'<div class="row">'
                 +'<div class="col-md-12 bg-gray">'
                 +'<div class="row pad-lft pad-rgt" >'
                 +'<div class="timeline mar-btm pad-no" style="padding-bottom: 0px;">'
                 +'<div class="timeline-entry mar-no"> '
                 +'<div class="timeline-stat">'
                 +'<div class="timeline-icon bg-purple">'
                 +'<i class="fa fa-warning fa-lg"></i> '
                 +'</div>'
                 +'<div class="timeline-time"><b>'+DataUltimoContato+'</b></div></div>'
                 +'<div class="timeline-label"> <p class="mar-no pad-btm">'
                 +'<span class="badge badge-purple">Observações Antigas</span>'
                 +'por <a href="#" class="btn-link btn-md text-semibold"> '+UsuarioUltimoHistorico+'</a></p>'
                 +'<div class="well well-xs mar-no"> '
                 +''+UltimoHistorico+''
                 +'</div></div></div></div></div></div></div><div class="row">'*/
                   + '<div class="col-xs-12 pad-btm bg-gray">'
                   + '<div class="col-sm-12 col-md-6 col-lg-4">'
                   + '<div class="form-group mar-no">'
                   + '<textarea id="demo-textarea-input" name="demo-textarea-input" rows="6" class="form-control" placeholder="Escreva aqui a Descrição do Evento..."></textarea>'
                   + '</div></div><div class="col-sm-12 col-md-6 col-lg-8">'
                   + '<div class="col-lg-6"><div class="pad-btm">'

                 + '<select name="combo" id="combo" onchange="selecionarEvento(this);" class="selectpicker show-tick" data-placeholder="Escolha um evento..." title="Escolha um evento..." data-style="btn-default" data-live-search="true"> '
                 + '<option value="0">Selecione</option>'
                 +'<option value="9">Observações</option>'
                 + '</select>'


                  /* + '<select name="combo" id="combo" onchange="selecionarEvento(this);" class="selectpicker show-tick" data-placeholder="Observações" title="Observações" visible="false" data-style="btn-default" data-live-search="true"> '
                       + '<option value="9" selected="true">Observações</option>'
                    + '</select>'*/

                   + '</div><div class="pad-btm">'

                  +'<select id="cboCategoria" name="cboCategoria" onchange="selecionarCategoria(this);" class="selectpicker pad-btm show-tick" data-placeholder="Escolha uma categoria..." title="Escolha uma categoria..." data-style="btn-default" data-live-search="true"></select>'   


                  /* + '<select id="cboCategoria" name="cboCategoria" onchange="selecionarCategoria(this);" class="selectpicker pad-btm show-tick" data-placeholder="" title="" data-style="btn-default" data-live-search="true"> '
                       + '<option value="2">Mensal</option>'
                       + '<option value="3">Semestral</option>'
                       + '<option value="4">Anual</option>'
                   + '</select>'*/

                   + '</div></div></div>',

                 buttons: {
                     danger: {
                         label: "Cancelar",
                         className: "btn btn-danger btn-labeled fa fa-times",
                         callback: function () {
                             $.niftyNoty({
                                 type: 'danger',
                                 icon: 'fa fa-times',
                                 message: '<strong>Registro cancelado</strong>',
                                 container: 'floating',
                                 timer: 3000
                             });
                         }
                     },


                     success: {
                         label: "Salvar",
                         className: "btn-success btn-labeled fa fa-check",
                         callback: function () {



                             //Pega o Valor do Historico
                             var NovoHistorico = $('#demo-textarea-input').val();
                             document.getElementById("ctl00_ContentPlaceHolder1_HistoricoHiddenField").value = NovoHistorico;

                             //Pega o Valor da Categoria
                             var cboCategoria = document.getElementById("cboCategoria");
                             document.getElementById("ctl00_ContentPlaceHolder1_CategoriaHiddenField").value = cboCategoria.options[cboCategoria.selectedIndex].value;
                             document.getElementById("ctl00_ContentPlaceHolder1_ClassificacaoHiddenField").value = cboCategoria.options[cboCategoria.selectedIndex].text;


                             //Pega Codigo Entidade
                             var Codigo = $("#Codigo").text()
                             document.getElementById("ctl00_ContentPlaceHolder1_CodigoHiddenField").value = Codigo;


                             //Indicador para Gravar
                             document.getElementById("ctl00_ContentPlaceHolder1_OperacaoHiddenField").value = "Incluir";



                             var Erro;
                             Erro = "";

                             if (document.getElementById("ctl00_ContentPlaceHolder1_EventoHiddenField").value == "0") {
                                 Erro = "Selecione um Evento!";
                             }


                             if (NovoHistorico == "") {
                                 Erro = "Informe um Historico!";
                             }


                             if (Erro == "") {
                                 $.niftyNoty({
                                     type: 'success',
                                     icon: 'fa fa-check',
                                     message: '<strong>Histórico atualizado!</strong>',
                                     container: 'floating',
                                     timer: 6000
                                 });



                                 //Chama o Servidor para Salvar
                                 __doPostBack('btnSave', NovoHistorico)

                             }
                             else {
                                 $.niftyNoty({
                                     type: 'danger',
                                     icon: 'fa fa-times',
                                     message: '<strong>' + Erro + '</strong>',
                                     container: 'floating',
                                     timer: 6000
                                 });

                             }
                         }
                     }
                 }
             });
	  };


    
    </script>


      <script type="text/javascript">


          function selecionarEvento(CboEvento) {


              document.getElementById("ctl00_ContentPlaceHolder1_EventoHiddenField").value = CboEvento.options[CboEvento.selectedIndex].value;

              if (CboEvento.options[CboEvento.selectedIndex].value == 0) {
                  alert("Selecione uma Categoria!");
              }


              if (CboEvento.options[CboEvento.selectedIndex].value == 9) {
                  CodigoPai_9();
              }
          }




          function CodigoPai_9() {


              var cboCategoria = document.getElementById("cboCategoria");
              while (cboCategoria.length) {
                  cboCategoria.remove(0);
              }

              var opt0 = document.createElement("option");
              opt0.value = "2";
              opt0.text = "Mensal";
              cboCategoria.add(opt0, cboCategoria.options[0]);

              var opt0 = document.createElement("option");
              opt0.value = "3";
              opt0.text = "Semestral";
              cboCategoria.add(opt0, cboCategoria.options[0]);

              var opt0 = document.createElement("option");
              opt0.value = "4";
              opt0.text = "Anual";
              cboCategoria.add(opt0, cboCategoria.options[0]);

          }


          /**
          * Exemplo Carregando a combobox
          */
          document.getElementById("btnCarregar").onclick = function () {
              var comboCidades = document.getElementById("cboCidades");

              var opt0 = document.createElement("option");
              opt0.value = "0";
              opt0.text = "";
              comboCidades.add(opt0, comboCidades.options[0]);

              var opt1 = document.createElement("option");
              opt1.value = "scs";
              opt1.text = "São Caetano do Sul";
              comboCidades.add(opt1, comboCidades.options[1]);

              var opt2 = document.createElement("option");
              opt2.value = "sa";
              opt2.text = "Santo André";
              comboCidades.add(opt2, comboCidades.options[2]);

              var opt3 = document.createElement("option");
              opt3.value = "sbc";
              opt3.text = "São Bernardo do Campo";
              comboCidades.add(opt3, comboCidades.options[3]);

          };

          /**
          * Descobrindo o valor selecionado
          */
          document.getElementById("btnInfo").onclick = function () {
              var comboCidades = document.getElementById("cboCidades");
              console.log("O indice é: " + comboCidades.selectedIndex);
              console.log("O texto é: " + comboCidades.options[comboCidades.selectedIndex].text);
              console.log("A chave é: " + comboCidades.options[comboCidades.selectedIndex].value);
          };


          /**
          * Selecionando um valor para a combobox
          */
          document.getElementById("btnAleatoriamente").onclick = function () {
              var comboCidades = document.getElementById("cboCidades");
              comboCidades.selectedIndex = Math.floor(Math.random() * comboCidades.length);
          };

          /**
          * Removendo elementos da combobox
          */
          document.getElementById("btnRemoverItem").onclick = function () {
              var comboCidades = document.getElementById("cboCidades");
              comboCidades.remove(0);
          };

          /**
          * Removendo todos os elementos
          */
          document.getElementById("btnRemoverTodos").onclick = function () {
              var comboCidades = document.getElementById("cboCidades");
              while (comboCidades.length) {
                  comboCidades.remove(0);
              }
          };








</script>
    <!--Finaliza Script para Tratar o combo no Modal-->
</asp:Content>
