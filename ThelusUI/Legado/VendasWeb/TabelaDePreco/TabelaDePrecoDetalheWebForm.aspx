<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true" CodeBehind="TabelaDePrecoDetalheWebForm.aspx.cs" Inherits="VendasWeb.TabelaDePreco.TabelaDePrecoDetalheWebForm" %>

<%@ Register Src="~/usercontrol/UCTabelaPreco.ascx" TagPrefix="uc1" TagName="UCTabelaPreco" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
    
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
                <h3 class="panel-title">Tabela de Preço - Principal</h3>
            </div>
            <!--Painel Aberto-->
            <!--Campos para escolha da carteira e do cliente-->

            <!-- END Painel Aberto-->
            <!--===================================================-->
            <!--Painel FILTROS-->
            <!--===================================================-->
            <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse' aria-expanded='true' style='height: 0px;'>"
                runat="server"></asp:Literal>

            <div class="panel-body">

                <div class="row">
                    <div class="col-sm-2" runat="server" id="IDTabelaDivL" visible="false"> 
                        <div class="form-group">
                            <asp:Label ID="IDTabela" runat="server" Text="Código:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-3" runat="server" id="IDTabelaDivT" visible="false">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="IDTabelaTextBox" runat="server" Enabled="false"></asp:TextBox>
                            
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-5">
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="NomeTextBox" runat="server" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                     ControlToValidate="NomeTextBox" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="StatusDiv" visible="false">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Label ID="StatusLabel" runat="server" Text="Status:"></asp:Label>
                        </div>
                    </div>
                    

                    <div class="col-sm-3">
                        <div class="form-group">
                          <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="form-control">
                              <asp:ListItem Selected="True">Ativa</asp:ListItem>
                              <asp:ListItem>Inativa</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>

                </div>



            </div>


           
            <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                           <asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                                 runat="server" OnClick="GravarButton_Click" >Gravar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                 runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>
                           
                        </div>

                    </div>
                </div>

        </div>

    </div>

  

    </div>

    <uc1:UCTabelaPreco runat="server" ID="UCTabelaPreco" />

    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
