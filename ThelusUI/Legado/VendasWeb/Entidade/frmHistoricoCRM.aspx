<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="frmHistoricoCRM.aspx.cs" Inherits="VendasWeb.Entidade.frmHistoricoCRM" %>
<%@ Register src="../usercontrol/CrmPainelWebUserControl.ascx" tagname="ControlPainel" tagprefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-9">
			<!--===================================================-->
			<!-- Painel com TABS -->
			<!--===================================================-->
			<div class="panel panel-primary">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
					<div class="panel-control">
					
						<!--Nav tabs-->
						<ul class="nav nav-tabs">
							<li class=""><a data-toggle="tab" href="#demo-tabs-box-1"><i class="fa fa-search fa-lg"></i> Buscar no Histórico</a>
							</li>
							<li class="active"><a data-toggle="tab" href="#demo-tabs-box-2"><i class="fa fa-plus-square fa-lg"></i> CADASTRAR NOVO HISTÓRICO</a>
							</li>
						</ul>
					
					</div>
					<h3 class="panel-title"><asp:Label ID="LblCliente" runat="server" Text="" Width="60" CssClass="texto"></asp:Label></h3>
                </div>

                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->
                <div class="panel-body">
                    <!--Tabs content-->
					<div class="tab-content">
                        <div id="demo-tabs-box-1"  class="tab-pane fade">
                            <!--LINHA 1 - Painel Aberto-->
                            <h4 class="text-thin">Escolha os critérios de busca</h4>
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:DropDownList ID="drpEventoFiltro" runat="server" CssClass="selectpicker show-tick" AutoPostBack="True" onselectedindexchanged="drpEventoFiltro_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-lg-3">
                                    <asp:Panel id="panel2" runat="server" CssClass ="selectpicker show-tick">
                                        <asp:UpdatePanel ID="UpdatePanelEventoFiltro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpCategoriaFiltro" runat="server" CssClass="selectpicker show-tick">
                                                </asp:DropDownList>
                                            </ContentTemplate>

                                            <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="drpEventoFiltro" EventName="SelectedIndexChanged" />
                                                
                                            </Triggers>

                                        </asp:UpdatePanel>
                                        </asp:Panel>
                                </div>

                                <div class="col-lg-4">
                                    <asp:Label ID="EspacoLabel" runat="server" Text="" width="40px"></asp:Label>
                                    <asp:LinkButton ID="BuscarButton" class="btn btn-sm btn-primary btn-labeled fa fa-search fa-sm"
                                        runat="server" title="Buscar Histórico" data-rel="tooltip" OnClick="BuscarButton_Click"> 
                                        Buscar no histórico </asp:LinkButton>
                                </div>
                            </div>
                            <!--END LINHA 1 - Painel Aberto-->
                            <!--===================================================-->
						</div>
						<div id="demo-tabs-box-2" class="tab-pane fade in active">
							<h4 class="text-thin">Inclua um novo Evento no Histórico</h4>
								<!--TAB2-->

                            <!--LINHA1 - TAB2-->
                            <div class="row">
                                <!--Descrição-->
                                <div class="col-sm-12 col-md-6 col-lg-4">
                                    <asp:TextBox ID="txtNovoHistorico" runat="server" class="form-control" TextMode="MultiLine" Width="100%" Height="100px"></asp:TextBox>
                                </div>
	                            <div class="col-sm-12 col-md-6 col-lg-8">	
		                            <!-- Evento e Categoria -->
		                            <div class="col-lg-6">
                                        <div>
                                            <asp:DropDownList ID="drpEvento" runat="server" CssClass="selectpicker show-tick" AutoPostBack="True" onselectedindexchanged="drpEvento_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                         <asp:Panel id="panel1" runat="server" CssClass ="selectpicker show-tick">
                                            <asp:UpdatePanel ID="UpdatePanelCategoria" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>

                                                    <asp:DropDownList ID="drpCategoria" runat="server" CssClass="selectpicker show-tick">
                                                    </asp:DropDownList>

                                                </ContentTemplate>

                                                <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpEvento" EventName="SelectedIndexChanged" />
                                                
                                                </Triggers>

                                            </asp:UpdatePanel>
                                            </asp:Panel>
                                        </div>
		                            </div>
		                            <!-- End Evento e Categoria -->
		                            <!--===================================================-->
		
		                            <!-- Agendamento e Botão -->
		                            <div class="col-lg-6">
			                            <!--DATA de AGENDAMENTO-->
                                        <div class="input-daterange input-group" id="Div2">
                                            <asp:TextBox ID="txtData" TextMode="Date" class="form-control" runat="server"></asp:TextBox><br /><br /><br /> <br />
                                            <asp:DropDownList ID="drpHora" runat="server" CssClass="campo" Width="60px"></asp:DropDownList>
				                            <div id="demo-dp-component">
					                            <!-- <small class="text-muted">Agende o próximo evento</small> -->
				                            </div>
			                            </div>
				                        <!-- Botão -->
				                        <div class="col-md-12 mar-top">
                                            <asp:LinkButton ID="SalvarButton" class="btn btn-sm btn-primary btn-labeled fa fa-search fa-sm"
                                                runat="server" title="Cadastrar" data-rel="tooltip" OnClick="SalvarButton_Click"> 
                                                Cadastrar </asp:LinkButton>
				                        </div>
		                            </div>
		                            <!-- End Agendamento e Botão -->
		                            <!--===================================================-->
                                </div>
                            </div>
                            <!--End LINHA1 - TAB2-->
                            <!--===================================================-->
							<!--END TAB2-->
							<!--===================================================-->
				        <div>
                    </div>
                </div>
            </div>
        </div>
    </div>
	<!--===================================================-->
	<!--End Painel com TABS -->
	<!--===================================================-->

	<div class="panel">
		<div class="panel-heading">
			<h3 class="panel-title">Histórico:</h3>
		</div>
		<div class="panel-body">
            <!-- Timeline do Histórico -->
		    <!--===================================================-->
		    <div class="timeline">
                <asp:Label ID="lblHistorico" runat="server" Text="" CssClass="texto"></asp:Label><br /><br />	
            </div>
        </div>
    <!--===================================================-->
    </div>
    <!-- End Foo Table - Filtering -->
    <!--===================================================-->
    <!-- END TABELA -->
        </div>
    <!----PAINEL----->
    <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
</div>
</asp:Content>
