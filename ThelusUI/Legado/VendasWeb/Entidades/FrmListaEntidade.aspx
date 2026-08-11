<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmListaEntidade.aspx.cs" Inherits="VendasWeb.Entidades.FrmListaEntidade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
         
        <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
        
        
    </style>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



  <br />
    <asp:Label ID="lblEntidade" runat="server" Text="Pesquisar por:" CssClass="texto"></asp:Label>
        <asp:DropDownList ID="drpEntCod" runat="server" CssClass="campo">
            <asp:ListItem Value="1">NOME FANTASIA</asp:ListItem>
            <asp:ListItem Value="2" Selected="True">RAZÃO SOCIAL</asp:ListItem>
            <asp:ListItem Value="3">CÓDIGO DA ENTIDADE</asp:ListItem>
            <asp:ListItem Value="4">CNPJ</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtFiltroEntCod" runat="server" CssClass="campo" Width="400px"></asp:TextBox>
        &nbsp;<asp:LinkButton ID="btnListar" class="btn btn-primary" runat="server" title="Consultar Entidade"
            data-rel="tooltip" OnClick="btnListar_Click" CausesValidation="False"> <span class="glyphicon glyphicon-search"
             aria-hidden="true">Consultar</span> </asp:LinkButton>
     
    
    
    <br /><br />


<div class="DivGrid">
            <asp:GridView ID="EntidadeGridView" runat="server"  AllowPaging="True"
                AutoGenerateColumns="False" DataKeyNames="ENTCOD" 
                CaptionAlign="Top" 
                EmptyDataText="Nenhuma Entidade Localizada" 
                CssClass="GridGis" EnableModelValidation="True" 
                onpageindexchanging="EntidadeGridView_PageIndexChanging" CellPadding="4" 
                ForeColor="#333333" GridLines="None"  >
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Names="arial" Font-Size="8" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Names="arial" Font-Size="8" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Names="arial" Font-Size="8" />
                <RowStyle HorizontalAlign="center" BackColor="#EFF3FB" Font-Names="arial" Font-Size="8"/>
                <AlternatingRowStyle BackColor="White" />
                <Columns>                                     
                    <asp:TemplateField HeaderText="Codigo" SortExpression="ENTCOD" >
                        <EditItemTemplate>
                            <asp:TextBox ID="ENTCODTextBox" runat="server" Text='<%# Bind("ENTCOD") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="ENTCODLabel" runat="server" 
                                Text='<%# Bind("ENTCOD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StatEntDescr" HeaderText="Situação" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome Entidade" SortExpression="Nome" ControlStyle-CssClass="GridGis"  >                    
                    <ControlStyle CssClass="GridGis"></ControlStyle>
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="VendCod" HeaderText="Cod. Vendedor" SortExpression="VendCod" />
                    <asp:BoundField DataField="VendNome" HeaderText="Nome Vendedor" SortExpression="VendNome" />                    
                    <asp:BoundField DataField="DataCompra" HeaderText="Data Ultima Compra" SortExpression="DataCompra"  DataFormatString="{0:d}" />
                   
                    <asp:TemplateField HeaderText="Situação Compra" SortExpression="estatus">
                       
                        <ItemTemplate>
                            <asp:Label ID="StatEntComercialLabel" runat="server" Text='<%# Bind("StatEntComercial") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Historico (INCLUIR/VISUALIZAR)">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Hisotico") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            

                                <asp:LinkButton ID="HistoricoButton" class="btn btn-warning" runat="server" title="Historico da Entidade"
            data-rel="tooltip" OnClick="HistoricoButton_Click" CausesValidation="False"> <span class="glyphicon glyphicon-edit"
             aria-hidden="true"></span> </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                   
                  </Columns>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
    
    </div>


</asp:Content>
