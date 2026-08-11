<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmAgendaVisitaDetalheProdutoVisita.aspx.cs" Inherits="VendasWeb.Entidades.FrmAgendaVisitaDetalheProdutoVisita" %>

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
    <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-15">
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
                        Produto Detalhes</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="LinhaProdutoLabel" runat="server" Text="Linha do Produto:" CssClass="text-thin"></asp:Label></h5>
                            <asp:DropDownList ID="LinhaProdutoDropDownList" runat="server" CssClass="form-control"
                                Width="180px" AutoPostBack="true" OnSelectedIndexChanged="LinhaProdutoDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic"
                                SetFocusOnError="True" ControlToValidate="LinhaProdutoDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>

                        

                        

                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="ProdutoLabel" runat="server" Text="Produto:" CssClass="text-thin"></asp:Label></h5>
                            <asp:DropDownList ID="ProdutoDropDownList" runat="server" CssClass="form-control" 
                                Width="180px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                SetFocusOnError="True" ControlToValidate="ProdutoDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="Label4" runat="server" CssClass="text-thin" Text="Classe de quantidade:"  Width="130"></asp:Label></h5>
                            <asp:RadioButtonList ID="ClasseQtdRadioButtonList" runat="server" >
                                <asp:ListItem Selected="True" Value="A"></asp:ListItem>
                                <asp:ListItem Value="B"></asp:ListItem>
                                <asp:ListItem Value="C"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <br />
                        <br />
                    </div>
                    <div class="row">
                        Prazo para potencial atendimento:
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="Label1" runat="server" CssClass="text-thin" Text="Mês corrente:" Width="130"></asp:Label></h5>
                            <asp:RadioButtonList ID="PrazoPotencialMesCorrenteRadioButtonList" runat="server" >
                                <asp:ListItem Selected="True" Text="1 - Alta" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2 - Média" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3 - Baixa" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="Label2" runat="server" CssClass="text-thin" Text="1 Mês:" Width="130"></asp:Label></h5>
                            <asp:RadioButtonList ID="PrazoPotencialMes1RadioButtonList" runat="server" >
                                <asp:ListItem Selected="True" Text="1 - Alta" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2 - Média" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3 - Baixa" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="Label3" runat="server" CssClass="text-thin" Text="3 Mês:" Width="130"></asp:Label></h5>
                            <asp:RadioButtonList ID="PrazoPotencialMes3RadioButtonList" runat="server" >
                                <asp:ListItem Selected="True" Text="1 - Alta" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2 - Média" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3 - Baixa" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-sm-3">
                            <h5>
                                <asp:Label ID="Label5" runat="server" CssClass="text-thin" Text="Maior que 3 Meses:" Width="130"></asp:Label></h5>
                            <asp:RadioButtonList ID="PrazoPotencialMesSuperiorRadioButtonList" runat="server"
                                >
                                <asp:ListItem Selected="True" Text="1 - Alta" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2 - Média" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3 - Baixa" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <br />
                    <br />
                </div>
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">
                            <asp:LinkButton ID="VoltarLinkButton" class="btn btn-danger" runat="server" title="Voltar/Cancelar"
                                CausesValidation="false" data-rel="tooltip" OnClick="VoltarLinkButton_Click">
        
    <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Voltar</span>
    
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success" runat="server" title="Salvar"
                                data-rel="tooltip" OnClick="SalvarLinkButton_Click">
        
    <span class="glyphicon glyphicon-floppy-saved" aria-hidden="true"> Salvar</span>
    
                            </asp:LinkButton>
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
