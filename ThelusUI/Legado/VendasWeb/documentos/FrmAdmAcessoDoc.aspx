<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmAdmAcessoDoc.aspx.cs" Inherits="VendasWeb.documentos.FrmAdmAcessoDoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <center><b Class="text1">Acesso aos Documentos por Usuario</b></center>
    <br />
    <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="text1"></asp:Label>
    <asp:DropDownList ID="drpUsuario" runat="server" CssClass="text1"   Width="265px">
    </asp:DropDownList>
    <asp:Label ID="lblCodigo" runat="server" Text="Documento:" CssClass="text1"></asp:Label>
    <asp:DropDownList ID="drpDocumento" runat="server" CssClass="text1" Width="265px">
    </asp:DropDownList>
    

    <asp:LinkButton ID="NovoDocumentoLinkButton" class="btn btn-primary" runat="server" title="Consultar Documentos" 
        data-rel="tooltip" OnClick="btnListar_Click1"> <span class="glyphicon glyphicon-search"
             aria-hidden="true">Consultar</span> </asp:LinkButton>


    &nbsp;<asp:LinkButton ID="LinkButton1" class="btn btn-success" runat="server" title="Novo Documento" 
        data-rel="tooltip" OnClick="btnSalvar_Click1"> <span class="glyphicon glyphicon-ok"
             aria-hidden="true">Adicionar</span> </asp:LinkButton>
    
    <br /><br />
     <asp:GridView ID="DocumentoGridView" runat="server" CssClass="lstTabela" EmptyDataText="Nada foi localizado para o filtro utilizado"
            AutoGenerateColumns="False">

            <Columns>

                <asp:TemplateField HeaderText="Documento ID" InsertVisible="False" SortExpression="UserDocXUsuarioID">

                    <ItemTemplate>
                        <asp:Label ID="UserDocXUsuarioIDLabel" runat="server" Text='<%# Bind("UserDocXUsuarioID") %>'></asp:Label>
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

                <asp:TemplateField HeaderText="Usuario" InsertVisible="False" SortExpression="UsuCod">

                    <ItemTemplate>
                        <asp:Label ID="UsuCodLabel" runat="server" Text='<%# Bind("UsuCod") %>'></asp:Label>
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




            </Columns>


        </asp:GridView>


</asp:Content>
