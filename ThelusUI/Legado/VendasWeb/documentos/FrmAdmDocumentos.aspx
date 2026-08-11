<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmAdmDocumentos.aspx.cs" Inherits="VendasWeb.documentos.FrmAdmDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <center><b Class="text1">Lista e Controle de Documentos</b></center>
    <br />

    <div>

        <asp:LinkButton ID="NovoDocumentoLinkButton" class="btn btn-success" runat="server" title="Novo Documento" data-rel="tooltip" OnClick="NovoDocumentoLinkButton_Click"> <span class="glyphicon glyphicon-new-window" aria-hidden="true">Novo Documento</span> </asp:LinkButton>
        <br>

        <br>

        <asp:GridView ID="DocumentoGridView" runat="server" CssClass="lstTabela"
            AutoGenerateColumns="False">

            <Columns>

                <asp:TemplateField HeaderText="Documento ID" InsertVisible="False" SortExpression="UserDocumentoID">

                    <ItemTemplate>
                        <asp:Label ID="UserDocumentoIDLabel" runat="server" Text='<%# Bind("UserDocumentoID") %>'></asp:Label>
                    </ItemTemplate>


                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Nome" InsertVisible="False" SortExpression="Nome">

                    <ItemTemplate>
                        <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                    </ItemTemplate>


                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>




                <asp:TemplateField HeaderText="Remover">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>

                        <center>
                                                              <asp:LinkButton ID="RemoverButton" class="btn btn-danger" runat="server" CausesValidation="False"  
                                              OnClick="RemoverButton_Click"   data-rel="tooltip" >
                                                    <span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span> 

                                                </asp:LinkButton> 
                                                                 </center>

                    </ItemTemplate>

                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Url" Visible="False">

                    <ItemTemplate>
                        <asp:Label ID="UrlLabel" Text='<%# Bind("Url") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>


        </asp:GridView>


    </div>


        <br />    <br />


</asp:Content>
