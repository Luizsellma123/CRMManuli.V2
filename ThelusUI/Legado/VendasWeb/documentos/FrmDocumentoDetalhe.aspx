<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmDocumentoDetalhe.aspx.cs" Inherits="VendasWeb.documentos.FrmDocumentoDetalhe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<center><b Class="text1">Detalhe do Documento</b></center>
    <br />
    <div>


        <div>

            <fieldset>




                <div class="control-group">

                    <asp:Label ID="NomeLabel" runat="server" Text="Nome do Documento:" class="control-label"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
								            <asp:TextBox runat="server" ID="NomeTextBox" ></asp:TextBox>


                </div>





                <br />




            </fieldset>
        </div>



        <asp:FileUpload ID="DocumentoFileUpload" class="input-file uniform_on" runat="server" />

        <asp:Label ID="DocumentoValidaLabel" runat="server" CssClass="LabelValidacao"></asp:Label>
        <br /><br />
        


        <asp:LinkButton ID="CancelarLinkButton" class="btn btn-danger" runat="server" title="Retornar" data-rel="tooltip" OnClick="CancelarLinkButton_Click">
                                                        
                <span class="glyphicon glyphicon-remove-circle" aria-hidden="true">Cancelar</span>
        </asp:LinkButton>

        

        &nbsp;
        <asp:LinkButton ID="CarregarBannerLinkButton" class="btn btn-success" runat="server" OnClick="CarregarBannerButton_Click" title="Salvar Imagem" data-rel="tooltip">
                                            
            <span class="glyphicon glyphicon-ok-circle" aria-hidden="true">Salvar</span>
        </asp:LinkButton>




    </div>

        <br />    <br />


</asp:Content>
