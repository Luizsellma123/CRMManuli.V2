<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="frmRelatorioGerencial.aspx.cs" Inherits="VendasWeb.Entidades.frmRelatorioGerencial" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <asp:Label ID="VendNomeLabel" runat="server" Text="" width="40px"></asp:Label></h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha do vendedor-->
                <div id="painel_aberto" class="">
                    <div class="panel-body">
                        <!--LINHA 1 - Painel Aberto-->
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <select class="selectpicker show-tick" multiple data-placeholder="Escolha um vendedor..."
                                        title="Escolha um vendedor..." data-style="btn-primary" data-live-search="true"
                                        id="VendedoresSelect" runat="server">
                                    </select>
                                    <br /><br />

                                    <asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="text-thin" Width="130"></asp:Label>
                                    <asp:DropDownList ID="StatusDropDownList" runat="server">
                                        <asp:ListItem Value="A">Ativo</asp:ListItem>
                                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                                        <asp:ListItem Value="T" Selected="True">Todos</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" SetFocusOnError="True"
                                        ControlToValidate="StatusDropDownList" ErrorMessage="Selecione um Status!"></asp:RequiredFieldValidator>
                                    <br /><br />

                                    <asp:Label ID="lblUF" runat="server" Text="UF: " CssClass="text-thin" Width="130"></asp:Label>
                                    <asp:TextBox ID="UFTextBox" runat="server" Width="151px"></asp:TextBox>
                                    <br /><br />
                                    <asp:Label ID="lblRegiao" runat="server" Text="Região: " CssClass="text-thin" Width="130"></asp:Label>
                                    <asp:TextBox ID="RegiaoTextBox" runat="server" Width="151px"></asp:TextBox>
                                    <br /><br />
                                    <asp:Label ID="lblCidade" runat="server" Text="Cidade: " CssClass="text-thin" Width="130"></asp:Label>
                                    <asp:TextBox ID="CidadeTextBox" runat="server" Width="151px"></asp:TextBox>
                                    <br /><br />

                                    <asp:Label ID="lblClasseVendedor" runat="server" CssClass="text-thin" Text="Classe Vendedor:" Width="130"></asp:Label>
                                    <asp:DropDownList ID="ClasseVendedorDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <br /><br />

                                    <asp:Label ID="DataInicialLabel" runat="server" CssClass="text-thin" Text="Data Inicial:" Width="130"></asp:Label>
                                    <div class="input-daterange input-group" id="Div2">
                                        <asp:TextBox ID="DataInicialTextBox" TextMode="Date" class="form-control" runat="server"></asp:TextBox><br /><br />
				                        <div id="demo-dp-component">
					                        <!-- <small class="text-muted">Agende o próximo evento</small> -->
				                        </div>
			                        </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="True"
                                        ControlToValidate="DataInicialTextBox" ErrorMessage="Informe a data inicial!"></asp:RequiredFieldValidator>

                                    <asp:Label ID="DataFinalLabel" runat="server" CssClass="text-thin" Text="Data Final:" Width="130"></asp:Label>
                                    <div class="input-daterange input-group" id="Div1">
                                        <asp:TextBox ID="DataFinalTextBox" TextMode="Date" class="form-control" runat="server"></asp:TextBox><br /><br />
				                        <div id="Div3">
					                        <!-- <small class="text-muted">Agende o próximo evento</small> -->
				                        </div>
			                        </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" SetFocusOnError="True"
                                        ControlToValidate="DataFinalTextBox" ErrorMessage="Informe a data final!"></asp:RequiredFieldValidator>
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
           </div>     
           <asp:Literal ID="collapseLiteral" runat="server" Text=""></asp:Literal>

            <!-- END Painel FILTROS-->
            <!--===================================================-->
            <!-- Panel Footer-->
            <!-- Botões de buscar e limpar-->
            <!--===================================================-->
            <div class="panel-footer">
                <div class="row">
                    <div class="panel-control">
                        <asp:LinkButton ID="ListarLinkButton" class="btn btn-success btn-labeled fa fa-search fa-lg"
                            runat="server" title="Listar" data-rel="tooltip" OnClick="ListarLinkButton_Click"> 
                            Listar </asp:LinkButton>
                    </div>
                </div>
            </div>
        <!--===================================================-->
        <!--End Painel Carteiras e Filtros-->
        <!--===================================================-->
        <asp:MultiView ID="RelatorioGerencialMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="RelatorioGerencialView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Relatório Gerencial
                        </h3>
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="RelatorioGerencialGridView" EmptyDataText="Nenhuma Informação Localizada"
                                AutoGenerateColumns="False" runat="server" EnableModelValidation="True" AllowPaging="True"
                                OnPageIndexChanging="RelatorioGerencialGridView_PageIndexChanged" PageSize="10" 
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                style="border-collapse:collapse;" Visible="false">

                                <PagerStyle CssClass="pagination-ys" />
                                                                
                                <Columns>
                                    <asp:BoundField DataField="EntCod" HeaderText="Cód. Entidade"></asp:BoundField>
                                    <asp:BoundField DataField="Entidade" HeaderText="Entidade"></asp:BoundField>
                                    <asp:BoundField DataField="EntCpfCgc" HeaderText="CNPJ/CPF"></asp:BoundField>
                                    <asp:BoundField DataField="StatEntCod" HeaderText="Cód. Status Entidade"></asp:BoundField>
                                    <asp:BoundField DataField="StatEntDescr" HeaderText="Status Entidade"></asp:BoundField>
                                    <asp:BoundField DataField="Ent_Fantasia" HeaderText="Fantasia"></asp:BoundField>
                                    <asp:BoundField DataField="ENDERECO" HeaderText="Endereço"></asp:BoundField>
                                    <asp:BoundField DataField="VendCod" HeaderText="Cód. Vendedor"></asp:BoundField>
                                    <asp:BoundField DataField="VendNome" HeaderText="Vendedor"></asp:BoundField>
                                    <asp:BoundField DataField="Ent_Fone" HeaderText="Fone"></asp:BoundField>
                                    <asp:BoundField DataField="CidNomeComp" HeaderText="Cidade"></asp:BoundField>
                                    <asp:BoundField DataField="EntDataCad" HeaderText="Data Cadastro"></asp:BoundField>
                                    <asp:BoundField DataField="DataCompra" HeaderText="Data Compra"></asp:BoundField>
                                    <asp:BoundField DataField="Status_Compra" HeaderText="Status Compra"></asp:BoundField>
                                    <asp:BoundField DataField="ParcAberto" HeaderText="Parcela Aberta"></asp:BoundField>
                                    <asp:BoundField DataField="VendClasseCod" HeaderText="Cód. Classe Vendedor"></asp:BoundField>
                                    <asp:BoundField DataField="VendClasseDescr" HeaderText="Classe Vendedor"></asp:BoundField>

                                    <asp:BoundField DataField="CodigoEvento" HeaderText="Cód. Evento"></asp:BoundField>
                                    <asp:BoundField DataField="Evento" HeaderText="Evento"></asp:BoundField>
                                    <asp:BoundField DataField="CodigoCategoria" HeaderText="Cód. Categoria"></asp:BoundField>
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoria"></asp:BoundField>
                                    <asp:BoundField DataField="DataCad" HeaderText="Data Cadastro"></asp:BoundField>
                                    <asp:BoundField DataField="Historico" HeaderText="Histórico"></asp:BoundField>
                                    <asp:BoundField DataField="DataAgenda" HeaderText="Data Agenda"></asp:BoundField>
                                </Columns>
                            </asp:GridView>

                            <rsweb:ReportViewer ID="rptRelatorioGerencial" runat="server" 
                                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                                <LocalReport ReportPath="relatorios\rptRelatorioGerencial.rdlc">
                                </LocalReport>
                            </rsweb:ReportViewer>
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
    </div> <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
