<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="frmBanner.aspx.cs" Inherits="VendasWeb.Banner.frmBanner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <div>

        <asp:LinkButton ID="NovoBannerLinkButton" class="btn btn-success" runat="server" title="Novo Banner" data-rel="tooltip" OnClick="NovoBannerLinkButton_Click">
                                                        
             <span class="glyphicon glyphicon-new-window" aria-hidden="true"> Novo Banner</span>
        </asp:LinkButton>
        <br>
        
        <br>

        <asp:GridView ID="BannerGridView" runat="server" AllowPaging="True" CssClass="lstTabela"
            AutoGenerateColumns="False">

            <Columns>
                <asp:TemplateField>

                    <ItemTemplate>
                        <asp:Image ID="ImageUrlImage" runat="server" Height="350px"
                            ImageUrl='<%# Eval("ImageUrl") %>' Width="350px" />
                    </ItemTemplate>
                    <ItemStyle Height="30px" Width="30px" />

                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Banner ID" InsertVisible="False" SortExpression="BannerID">

                    <ItemTemplate>
                        <asp:Label ID="BannerIDLabel" runat="server" Text='<%# Bind("BannerID") %>'></asp:Label>
                    </ItemTemplate>


                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Sequencia">

                    <ItemTemplate>
                        <asp:TextBox ID="ImpressionsTextBox" class="span6 typeahead" runat="server" Text='<%# Bind("Impressions") %>'  AutoPostBack="True" OnTextChanged="ImpressionsTextBox_TextChanged"></asp:TextBox>

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
                        <asp:Label ID="ImageUrlLabel" Text='<%# Bind("ImageUrl") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>


        </asp:GridView>


    </div>


</asp:Content>
