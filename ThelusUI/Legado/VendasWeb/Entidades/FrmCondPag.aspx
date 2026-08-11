<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmCondPag.aspx.cs" Inherits="VendasWeb.Entidades.FrmCondPag" %>
<%@ Register src="../usercontrol/ControlEntidade.ascx" tagname="ControlEntidade" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <link rel="stylesheet" type="text/css" href="../css/ListaPedido.css?aux=6" />
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<div class="conteudo">

        <center><b><h3>Condição de Pagamento</h3></b></center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />

        <br />

        
        <asp:GridView ID="CondPagGridView" runat="server" CssClass="lstTabela" Width="100%"
            AutoGenerateColumns="False">
            <Columns>

                <asp:TemplateField HeaderText="Código">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="CondPagCodLabel" Text='<%# Bind("CondPagCod") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Nome">

                    <EditItemTemplate>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="CondPagNomeLabel" Text='<%# Bind("CondPagNome") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>




                   <asp:TemplateField HeaderText="Selecionar"
                    SortExpression="ProdCodEstr">
                    <ItemTemplate>

                        <center> <asp:CheckBox ID="SelecionarCheckBox" runat="server"  /> </center>

                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>


            </Columns>
        </asp:GridView>

            

           <div style="text-align: right;">
            
              <asp:LinkButton ID="FinalizarLinkButton" class="btn btn-success" runat="server" 
        OnClick="SelecionarButton_Click" title="Finalizar Seleção" data-rel="tooltip">
                            <span class="glyphicon glyphicon-ok" aria-hidden="true"> Finalizar Seleção</span> 

    </asp:LinkButton>

                    &nbsp;<asp:LinkButton ID="RetornarButton" class="btn btn-danger" runat="server" 
                OnClick="RetornarButton_Click" title="Retornar" CausesValidation="False" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Cancelar</span> 

            </asp:LinkButton>

               </div>

        <br />
        <br />


        </div>

</asp:Content>
