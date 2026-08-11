<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmAbaAnexo.aspx.cs" Inherits="VendasWeb.Entidade.FrmAbaAnexo" %>


<%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-9">
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
                        Anexos</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel -->
                <!--===================================================-->
                <div class="table-responsive" >
                    <div class="panel-body">
                        
                            <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
                        <br />

                        <asp:MultiView ID="DadosAnexoMultView" runat="server" ActiveViewIndex="0" >
                            <asp:View ID="DadosAnexoView" runat="server">
                                <h5>
                                    <asp:Label ID="USER_TB_Tipos_AnexosLabel" runat="server" Text="Tipo Documento :"
                                        CssClass="text-thin"></asp:Label></h5>
                                <asp:DropDownList ID="USER_TB_Tipos_AnexosDropDownList" runat="server" CssClass="form-control" Width="200px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    SetFocusOnError="True" ControlToValidate="USER_TB_Tipos_AnexosDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                               
                                <asp:Label ID="IncluirDocLabel" runat="server" CssClass="LabelValidacao" Visible="false"></asp:Label>
                        
                            
                            <br /><br />

                                <asp:FileUpload ID="IncluirDocFileUpload" runat="server" CssClass="form-control" Width="200px" />
                                 
                                 <br /><br />
                                 

                                  <asp:LinkButton ID="IncluirDocButton" class="btn btn-success btn-labeled fa fa-cloud-upload fa-lg" 
                                    runat="server"  OnClick="IncluirDocButton_Click" title="Salvar arquivo"
                                    data-rel="tooltip"> Salvar Arquivo </asp:LinkButton>

                                </asp:View>
                            </asp:MultiView>
                            
                        <br /><br />
                        
                            <asp:GridView ID="DocumentosGridView" runat="server" 
                                EmptyDataText="Nenhum Documento Cadastrado."
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                            Style="border-collapse: collapse; max-width: 100%" AutoGenerateColumns="False">
                            <PagerStyle CssClass="pagination-ys" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Baixar">
                                        <ItemTemplate>
                                            <center>                                               
                                                 <asp:LinkButton ID="SelecionarButton" class="btn btn-primary fa fa-cloud-download fa-lg" 
                                    runat="server" CausesValidation="False" OnClick="SelecionarButton_Click" title="Baixar este Arquivo"
                                    data-rel="tooltip"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sequência" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="DocEntSeqLabel" runat="server" Text='<%# Bind("DocEntSeq") %>'></asp:Label>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nome do Anexo">
                                        <ItemTemplate>
                                            <asp:Label ID="DocEntObsLabel" runat="server" Text='<%# Bind("DocEntObs") %>'></asp:Label>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Usuario do Cadastro">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("UsuCod") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Data do Cadastro">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("DocEntData", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Caminho Documento" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="DocEntPathArqLabel" runat="server" Text='<%# Bind("DocEntPathArq") %>'> ></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Imagem" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="DocEntImageLabel" runat="server" Text='<%# Bind("DocEntImage") %>'> ></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remover Documento" Visible="true">
                                        <ItemTemplate>
                                            <center>                                                
                                                <asp:LinkButton ID="RemoverDocumentoButton" class="btn btn-danger fa fa-trash fa-lg" 
                                    runat="server" CausesValidation="False" OnClick="RemoverDocumentoButton_Click" title="Remover"
                                    data-rel="tooltip"></asp:LinkButton>
                                            </center>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                        
                    </div>
                </div>
                <!-- END Painel-->
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">


                        
                        <asp:LinkButton ID="VoltarLinkButton" class="btn btn-warning btn-labeled fa fa-arrow-circle-left fa-lg" CausesValidation="false"
                                    runat="server" title="Voltar" data-rel="tooltip" OnClick="VoltarButton_Click"
                                    > 
             Retornar </asp:LinkButton>

                        <asp:LinkButton ID="ProximoPassoButton" class="btn btn-primary btn-labeled fa fa-check-circle-o fa-lg" CausesValidation="false"
                                    runat="server" title="Próxima Tela" data-rel="tooltip" OnClick="ProximoPassoButton_Click"
                                    > 
             Finalizar </asp:LinkButton>

                        
                        </div>
                    </div>
                </div>
            </div>
            <!--===================================================-->
            <!--End Painel-->
            <!--===================================================-->
        </div>
        <!----PAINEL----->
        <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    </div>
</asp:Content>
