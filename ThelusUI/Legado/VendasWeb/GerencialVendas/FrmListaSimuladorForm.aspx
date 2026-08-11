<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmListaSimuladorForm.aspx.cs" Inherits="VendasWeb.GerencialVendas.FrmListaSimuladorForm" %>

<%@ Register Src="~/usercontrol/UCGerencialVendas.ascx" TagPrefix="uc1" TagName="UCGerencialVendas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript">
    function pseudomascara(obj, e) {
        var tecla = (window.event) ? e.keyCode : e.which;
        if (tecla == 8 || tecla == 0)
            return true;
        if (tecla != 44 && tecla < 48 || tecla > 57)
            return false;
    }

</script>   

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
                    <h3 class="panel-title">Gerencial Vendas - Lista Simulador Preço</h3>
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
                                <asp:DropDownList ID="EmpresaDropDownList" autopostback="true" runat="server" style="width:93%;" CssClass="selectpicker show-tick"></asp:DropDownList>
                            </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SituacaoLabel" runat="server" Text="Situação :"></asp:Label>
                            </div>
                        </div>

                    <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                <asp:DropDownList ID="SituacaoDropDown" autopostback="true" runat="server" style="width:93%;" CssClass="selectpicker show-tick"></asp:DropDownList>
                            </div>
                            </div>
                        </div>
                        </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SimulacaoLabel" runat="server" Text="Simulação: "></asp:Label>
                            </div>
                        </div>

                    <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                   <input runat="server" style="width: 93%;" id="TextSimulacao" type="text" onkeypress="return pseudomascara( this , event ) ;" class="form-control" placeholder="Número Solicitação"/></div>                                                                     
                            </div>
                            </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ClienteLabel" runat="server" Text="Cliente :"></asp:Label>
                            </div>
                        </div>

                    <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                   <input runat="server" style="width: 93%;" id="ClienteText" type="text" class="form-control" placeholder="Nome ou Código"/></div>                                                                     
                            </div>
                            </div>
                        </div>
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
                        <asp:LinkButton ID="SimularButton" class="btn btn-success btn-labeled fa fa-pencil-square-o fa-lg"
                            CausesValidation="false" runat="server" OnClick="SimularButton_Click">Nova simulação</asp:LinkButton>                                       
                        <asp:LinkButton ID="BuscarButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            CausesValidation="false" runat="server" OnClick="BuscarButton_Click">Buscar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <asp:MultiView ID="SimulacoesMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="SimulacoesView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Simulação
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                       
                            <asp:GridView ID="SimulacoesGridView" EmptyDataText="Não foi possível encontrar nenhuma simulação" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" 
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%" OnPageIndexChanging="SimulacoesGridView_PageIndexChanged">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Acessar " >
                                        <ItemTemplate>
                                         <asp:LinkButton ID="AcessarButton" class="btn btn-info fa fa-arrow-right"
                                          CausesValidation="false" runat="server" OnClick="AcessarButton_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Simulação " >
                                        <ItemTemplate>
                                            <asp:Label ID="IdSimGrid" runat="server" Text='<%# Bind("IDSimulacao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome Cliente " >
                                        <ItemTemplate>
                                            <asp:Label ID="ClienteGrid" runat="server" Text='<%# Bind("NomeCliente") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data Simulacao " >
                                        <ItemTemplate>
                                            <asp:Label ID="DataGrid" runat="server" Text='<%# Bind("DataSimulacao") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Situação " >
                                        <ItemTemplate>
                                            <asp:Label ID="SituacaoGrid" runat="server" Text='<%# Bind("Situacao") %>'></asp:Label>
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
        
    <uc1:UCGerencialVendas runat="server" id="UCGerencialVendas" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
</asp:Content>