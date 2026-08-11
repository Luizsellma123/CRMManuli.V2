<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="FrmMapsListaRota.aspx.cs" Inherits="VendasWeb.Entidades.FrmMapsListaRota" %>



<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
                        Lista de Rota em Criação</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->

             <!-- LINHA 1-->
                <div class="panel-body" >
                 <div class="table-responsive">
                 
             <asp:GridView ID="RoterizacaoGridView" runat="server" 
                                EmptyDataText="Nenhum Cliente foi Selecionado"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                            Style="border-collapse: collapse; max-width: 100%" AutoGenerateColumns="False" >
                            <PagerStyle CssClass="pagination-ys" />

                                <Columns>
                                  
                                    <asp:TemplateField HeaderText="Codigo" >
                                        <ItemTemplate>
                                            <asp:Label ID="EntCodLabel" runat="server" Text='<%# Bind("EntCod") %>'></asp:Label>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome">
                                        <ItemTemplate>
                                            <asp:Label ID="EntNomeLabel" runat="server" Text='<%# Bind("EntNome") %>'></asp:Label>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sequencia">
                                        <ItemTemplate>                                            
                                            <asp:TextBox ID="OrdenRoterizacaoTextBox" TextMode="number" runat="server" AutoPostBack="true" Text='<%# Bind("OrdenRoterizacao") %>' OnTextChanged="OrdenRoterizacaoTextBox_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remover" Visible="true">
                                        <ItemTemplate>
                                            <center>
                                                
                                                <asp:LinkButton ID="RemoverButton" class="btn btn-danger fa fa-trash fa-lg" 
                                    runat="server" CausesValidation="False"  title="Remover"
                                    data-rel="tooltip" OnClick="RemoverButton_Click"></asp:LinkButton>


                                            </center>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>


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


                             <asp:LinkButton ID="ExcluirLinkButton" class="btn btn-danger btn-labeled fa fa-trash fa-lg" CausesValidation="false"
                                    runat="server" title="Exclui o Planejamento da Rota" data-rel="tooltip" OnClick="ExcluirLinkButton_Click" > Excluir Planejamento</asp:LinkButton>


                              <asp:LinkButton ID="ExibirMapaLinkButton" class="btn btn-primary btn-labeled fa fa-car fa-lg" CausesValidation="false"
                                    runat="server" title="Exibir mapa com Rota Criada" data-rel="tooltip" OnClick="ExibirMapaLinkButton_Click" > Exibir no Mapa </asp:LinkButton>



                             

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->

        
       
    </div>
   




</asp:Content>
