<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmBannerDetalhe.aspx.cs" Inherits="VendasWeb.Banner.FrmBannerDetalhe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div>

         <asp:FileUpload ID="BannerFileUpload" class="input-file uniform_on" runat="server" />
        
        

         <br />
                                                <asp:Label ID="BannerValidaLabel" runat="server" CssClass="LabelValidacao"></asp:Label>
        <br/><br/>

        <div>

              <fieldset>

                                        <div class="control-group">
                                            
                                            <asp:Label ID="AtivoLabel" runat="server" Text="Ativo:" class="control-label"  Visible="false"   ></asp:Label>
								            
                                                
								            
                                                <asp:CheckBox ID="AtivoCheckBox" Checked="true" runat="server"  Visible="false"  placeholder="Situação do Banner (Ativado/Inativado)" />
								            

                                        </div>


                                          <div class="control-group">
                                            
                                            <asp:Label ID="NavigateUrlLabel" runat="server" Text="Navegar para:" class="control-label"   ></asp:Label>
								            &nbsp;&nbsp;&nbsp;&nbsp;
								            <asp:TextBox runat="server" ID="NavigateUrlTextBox" class="span6 typeahead"></asp:TextBox>
								            

                                        </div>


                                        <div class="control-group">
                                            
                                            <asp:Label ID="AlternateLabel" runat="server" Text="Texto Alternativo:" class="control-label"   ></asp:Label>
								            <asp:TextBox runat="server" ID="AlternateTextBox" class="span6 typeahead"></asp:TextBox>
								            

                                        </div>


                                        <div class="control-group">
                                            
                                            <asp:Label ID="ImpressionsLabel" runat="server" Text="Sequência:" class="control-label"   ></asp:Label>
								            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								            <asp:TextBox runat="server" ID="ImpressionsTextBox"  class="span6 typeahead"></asp:TextBox>
								            

                                        </div>


                  <br />

                  <asp:LinkButton ID="CancelarLinkButton" class="btn  btn-danger" runat="server"   title="Retornar" data-rel="tooltip" OnClick="CancelarLinkButton_Click" >
                                                        
                                                        <span class="glyphicon glyphicon-remove-circle" aria-hidden="true">Cancelar</span>
            </asp:LinkButton>

                  &nbsp;
                    <asp:LinkButton ID="CarregarBannerLinkButton" class="btn btn-success" runat="server"  OnClick="CarregarBannerButton_Click" title="Salvar Imagem" data-rel="tooltip" >
                                                        
                        <span class="glyphicon glyphicon-ok-circle" aria-hidden="true">Salvar</span>
            </asp:LinkButton>

                  <br /><br />


                                  
                                    </fieldset>
        </div>


    </div>



</asp:Content>
