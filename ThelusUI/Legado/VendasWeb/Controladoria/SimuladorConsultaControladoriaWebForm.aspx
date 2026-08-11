<%@ page title="" language="C#" masterpagefile="~/CRM.Master" autoeventwireup="true" codebehind="SimuladorConsultaControladoriaWebForm.aspx.cs" inherits="VendasWeb.GerencialVendas.SimuladorConsultaControladoriaWebForm" %>

<%@ register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>


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
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <script language="javascript">
        function pseudomascara(obj, e) {
            var tecla = (window.event) ? e.keyCode : e.which;
            if (tecla == 8 || tecla == 0)
                return true;
            if (tecla != 44 && tecla < 48 || tecla > 57)
                return false;
        }

    </script>
    <!-- LINHA 1-->
    <div class="row">

        <div class="col-sm-9">
            <!--===================================================-->
            <!--Painel Carteiras e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <%--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>--%>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--<button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>--%>
                    </div>
                    <h3 class="panel-title">Simulador de preços</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse' aria-expanded='false' style='height: 0px;'>"
                    runat="server"></asp:Literal>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LblClasse" runat="server" Text="Empresa :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="EmpresaDropDown" OnSelectedIndexChanged="EmpresaDropDown_SelectedIndexChanged" AutoPostBack="true" runat="server" Style="width: 93%;" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="1">1 - MANULI CTBA</asp:ListItem>
                                        <asp:ListItem Value="2">2 - MANULI SP</asp:ListItem>
                                        <asp:ListItem Value="3">3 - MANULI AM</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EstadoLabel" runat="server" Text="Estado:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="EstadoDropDown" runat="server" Style="width: 93%;" CssClass="form-control "
                                        AutoPostBack="true" OnSelectedIndexChanged="EstadoDropDown_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ProdutoLabel" runat="server" Text="Produto :"></asp:Label>
                            </div>
                        </div>


                        <div class="col-sm-10 col-md-4">
                            <select class="selectpicker show-tick" data-placeholder="Escolha um produto"
                                title="Escolha um produto" data-style="btn-primary" data-live-search="true"
                                id="ProdutoSelect" runat="server">
                            </select>
                        </div>
                        <div class="col-sm-1 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LabelQuantidade" runat="server" Text="Quantidade :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <input runat="server" style="width: 93%;" id="QuantidadeText" type="text" onkeypress="return pseudomascara( this , event ) ;" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="VendedorLabel" runat="server" Text="Nível Vendedor :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="VendedorDropDown" AutoPostBack="true" runat="server" Style="width: 93%;" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-1 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LabelICMS" runat="server" Text="Ex-ICMS :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <input runat="server" style="width: 93%;" id="TextICMS" type="text" onkeypress="return pseudomascara( this , event ) ;" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="LocalLabel" runat="server" Text="Tabela:"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="FaturamentoDropDown" AutoPostBack="true" runat="server" Style="width: 93%;" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-1 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="PrecoLabel" runat="server" Text="Preço final :"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <input runat="server" style="width: 93%;" disabled="disabled" id="PrecoInput" type="text" onkeypress="return pseudomascara( this , event ) ;" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Class. Comercial:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ClassificacaoComercialDropDownList" AutoPostBack="true" runat="server" Style="width: 93%;" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Frete:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="FreteDropDownList" AutoPostBack="true" runat="server" Style="width: 93%;" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label Text="À vista: " ID="AvistaLabel" runat="server" Style="position: relative; bottom: 2px;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:CheckBox ID="AvistaCheckBox" runat="server" />
                            </div>
                        </div>

                    </div>

                    <%--<div class="row">
                         <div class="col-sm-2">
                            <asp:Label Text="Novo Cliente: " ID="PadraoLabel" runat="server" style="position:relative; bottom: 2px;"></asp:Label>
                        </div>                     
                        <div class="form-group">
                        <div class="col-sm-4">
                            <asp:CheckBox ID="ClienteCheck" runat="server" OnCheckedChanged="ClienteCheck_CheckedChanged" AutoPostBack="true"/>
                        </div>                              
                       </div>      
                    </div>--%>
                    <%--  <br />--%>
                    <%--<div class="row">
                         <div class="col-sm-2">
                            <div class="form-group">
                               <asp:Label ID="ClienteLabel" runat="server" Text="Cliente :"></asp:Label>
                             </div> 
                         </div>
                         <div class="col-sm-10">                           
                             <div class="form-group">
                                <asp:TextBox id="ClienteInput" runat="server" style="width:92%;" ReadOnly="true"></asp:TextBox>
                                <asp:LinkButton ID="PlusButton" class="btn fa fa-plus-circle fa-lg"
                                CausesValidation="false" runat="server" OnClick="PlusButton_Click"></asp:LinkButton> 
                             </div>        
                            </div> 
                       </div> --%>
                    <%-- <div class="row">
                         <div class="col-sm-2">
                            <div class="form-group">
                               <asp:Label ID="ObservacaoInput" runat="server" Text="Histórico :"></asp:Label>
                             </div> 
                         </div>
                         <div class="col-sm-10">                           
                             <div class="form-group">
                                <asp:TextBox ID="ObservBox" style="height:100px; width:97.5%;" textmode="MultiLine" runat="server"></asp:TextBox></div>                                                                     
                            </div>
                    </div>--%>
                </div>



                <!--===================================================-->

                <!-- END LINHA 1 - Painel FILTROS-->
            </div>
            <!-- 
            </div> -->



            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">

                    <div class="panel-control">
                        <asp:LinkButton ID="RetornarButton" class="btn btn-success btn-labeled fa fa-arrow-left fa-lg"
                            CausesValidation="false" runat="server" OnClick="RetornarButton_Click">Retornar</asp:LinkButton>
                        
                        <asp:LinkButton ID="SimularButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="SimularButton_Click">Simular</asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>

        <asp:MultiView ID="SimuladorMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="SimuladorView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Simulação
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="SimulacaoGridView" EmptyDataText="A simulação não foi possível" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" OnPageIndexChanging="SimulacaoGridView_PageIndexChanged"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Aprovação ">
                                        <ItemTemplate>
                                            <asp:Label ID="AlcadaGrid" runat="server" Text='<%# Bind("Aprovacao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome do produto ">
                                        <ItemTemplate>
                                            <asp:Label ID="ProdutoGrid" runat="server" Text='<%# Bind("NomeProduto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipo de material ">
                                        <ItemTemplate>
                                            <asp:Label ID="MaterialGrid" runat="server" Text='<%# Bind("TipoMaterial") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Peso ">
                                        <ItemTemplate>
                                            <asp:Label ID="PesolGrid" runat="server" Text='<%# Bind("Peso") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ICMS ">
                                        <ItemTemplate>
                                            <asp:Label ID="ICMSGrid" runat="server" Text='<%# Eval("ICMS","{0:N}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Margem ">
                                        <ItemTemplate>
                                            <center>
                                                <asp:Label ID="IconelGrid" runat="server" Text='<%# Bind("MargemSimulacao") %>'></asp:Label></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Situação ">
                                        <ItemTemplate>
                                            <center>
                                                <asp:Label ID="IconelGrid" runat="server" Text='<%# Bind("Icone") %>'></asp:Label></center>
                                        </ItemTemplate>
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

    <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->


    </div>



</asp:content>
