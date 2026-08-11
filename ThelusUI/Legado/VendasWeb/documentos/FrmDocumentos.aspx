<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmDocumentos.aspx.cs" Inherits="VendasWeb.documentos.FrmDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <center><b Class="text1">Documentos para Downloads</b></center>
    

    <br />

      <asp:GridView ID="DocumentoGridView" runat="server" CssClass="lstTabela" EmptyDataText=" =( Ainda não existem documentos disponivel"
            AutoGenerateColumns="False"  Width="100%" >

            <Columns >

                <asp:TemplateField HeaderText="Documento ID" InsertVisible="False"   HeaderStyle-Width="10%"  SortExpression="UserDocXUsuarioID">

                    <ItemTemplate>
                        <asp:Label ID="UserDocXUsuarioIDLabel" runat="server"   Text='<%# Bind("UserDocXUsuarioID") %>'></asp:Label>
                    </ItemTemplate>


                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Nome Documento" InsertVisible="False" SortExpression="Nome">

                    <ItemTemplate>
                        <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                    </ItemTemplate>


                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Url" InsertVisible="False" SortExpression="Url" Visible="False">

                    <ItemTemplate>
                        <asp:Label ID="UrlLabel" runat="server" Text='<%# Bind("Url") %>'></asp:Label>
                    </ItemTemplate>


                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>



              <asp:TemplateField HeaderText="Baixar"  HeaderStyle-Width="10%">

                    <ItemTemplate>
                        <center>
                         
                             <asp:LinkButton ID="SelecionarButton" class="btn btn-primary" runat="server"  CausesValidation="False"
                      OnClick="SelecionarButton_Click"   data-rel="tooltip" >
                            <span class="glyphicon glyphicon-cloud-download" aria-hidden="true"></span> 

                          </asp:LinkButton>
   



                        </center>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>




            </Columns>


        </asp:GridView>

   

        <br />    <br />

</asp:Content>
