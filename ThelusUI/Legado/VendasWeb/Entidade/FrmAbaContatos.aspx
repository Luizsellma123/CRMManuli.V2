<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.Master" AutoEventWireup="true"
    CodeBehind="FrmAbaContatos.aspx.cs" Inherits="VendasWeb.Entidade.FrmAbaContatos" %>

<%@ Register Src="../usercontrol/ControlEntidade.ascx" TagName="ControlEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel"
    TagPrefix="ucp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"     type="text/javascript"></script>
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
                        Cadastro de contato</h3>
                </div>
                <div class="table-responsive">
                    <div class="panel-body">
                        <!--Painel Aberto-->
                        <!-- END Painel Aberto-->
                        <!-- END Painel-->
                        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
                        <br />
                        <asp:Literal ID="ContatoLiteral" runat="server" Visible="true"></asp:Literal>
                        <br />
                        <asp:MultiView ID="DadosContatoMultView" runat="server" ActiveViewIndex="0" Visible="false">
                            <asp:View ID="DadosContatoView" runat="server">
                                <div class="row">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label ID="TipoContatoLabel" runat="server" CssClass="text-thin" Text="Tipo Contato:"
                                                Width="90px"></asp:Label></h5>
                                        <asp:DropDownList ID="TipoContatoDropDownList" AutoPostBack="true" runat="server"
                                            CssClass="form-control" Width="450px" OnSelectedIndexChanged="TipoContatoDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="COMERCIAL">COMERCIAL</asp:ListItem>
                                            <asp:ListItem Value="Financeiro">FINANCEIRO</asp:ListItem>
                                            <asp:ListItem Value="LOGISTICA">LOGISTICA</asp:ListItem>
                                            <asp:ListItem Value="MARKETING">MARKETING</asp:ListItem>
                                            <asp:ListItem Value="REFERÊNCIA COMERCIAL">REFERÊNCIA COMERCIAL</asp:ListItem>
                                            <asp:ListItem Value="XML">XML</asp:ListItem>
                                            <asp:ListItem Value="OUTRO">Outro</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label ID="OutroTipoContatoLabel" Text="Qual?" CssClass="text-thin" runat="server"
                                                Visible="false" Width="170px"></asp:Label></h5>
                                        <asp:TextBox ID="OutroTipoContatoTextBox" runat="server" CssClass="form-control"
                                            Visible="false" Width="450px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                            SetFocusOnError="True" ControlToValidate="OutroTipoContatoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label ID="EmpresaLabel" runat="server" CssClass="text-thin" Text="Empresa:"
                                                Width="90px"></asp:Label></h5>
                                        <asp:TextBox ID="EmpresaTextBox" runat="server" CssClass="form-control uppercase"
                                            Width="450px"></asp:TextBox>
                                        <br />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label ID="NomeContatoLabel" runat="server" CssClass="text-thin" Text="Nome:"
                                                Width="50px"></asp:Label></h5>
                                        <asp:TextBox ID="NomeContatoTextBox" runat="server" CssClass="form-control uppercase"
                                            Width="450px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                            SetFocusOnError="True" ControlToValidate="NomeContatoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label ID="EmailContatoLabel" runat="server" CssClass="text-thin" Text="Email:"
                                                Width="90px"></asp:Label></h5>
                                        <asp:TextBox ID="EmailContatoTextBox" runat="server" CssClass="form-control uppercase"
                                            Width="450px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                            SetFocusOnError="True" ControlToValidate="EmailContatoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator"
                runat="server" ControlToValidate="EmailContatoTextBox" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Email Invalido" ForeColor="Red"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email Inválido</asp:RegularExpressionValidator>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-1">
                                        
                                            <asp:Label ID="TelefoneContatoLabel" runat="server" Text="Telefone:" CssClass="text-thin"
                                                Width="90px"></asp:Label>
                                        <asp:TextBox ID="DDDTelefoneContatoTextBox" runat="server" CssClass="form-control"
                                            Width="60px" onkeypress="mascara( this, mnum );" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                            SetFocusOnError="True" ControlToValidate="DDDTelefoneContatoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:TextBox ID="TelefoneContatoTextBox" runat="server" CssClass="form-control" Width="150px"
                                                onkeypress="mascara( this, mnum );"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                                SetFocusOnError="True" ControlToValidate="TelefoneContatoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            
                                                <asp:Label ID="RamalContatoLabel" runat="server" CssClass="text-thin" Text="Ramal:"
                                                    Width="100px"></asp:Label>
                                            <asp:TextBox ID="RamalContatoTextBox" onkeypress="mascara( this, mnum );" CssClass="form-control"
                                                Width="50px" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic"
                                                SetFocusOnError="True" ControlToValidate="RamalContatoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label ID="CargoContatoLabel" runat="server" Text="Cargo:" CssClass="text-thin"
                                                Width="50px"></asp:Label></h5>
                                        <asp:TextBox ID="CargoContatoTextBox" CssClass="form-control uppercase" runat="server"
                                            Width="450px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                                            SetFocusOnError="True" ControlToValidate="CargoContatoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <br />

                           
                            </asp:View>
                        </asp:MultiView>

                              <asp:LinkButton ID="NovoContatoButton" class="btn btn-btn-primary btn-labeled fa fa-plus fa-lg" CausesValidation="false"
                                    runat="server" OnClick="NovoContato_Click" title="Novo Contato" data-rel="tooltip">
                                                    Novo Contato

                                </asp:LinkButton>
                                

                                <asp:LinkButton ID="AdicionarButton" class="btn btn-success btn-labeled fa fa-plus fa-lg" Visible="false"
                                    runat="server" OnClick="AdicionarButton_Click" title="Adicionar" data-rel="tooltip">
                                                    Adicionar

                                </asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="AlterarLinkButton" class="btn btn-warning btn-labeled fa fa-remove fa-lg" Visible="false"
                                    runat="server"  title="Alterar" data-rel="tooltip" OnClick="AlterarLinkButton_Click">
                                                    <span class="glyphicon glyphicons-ok" aria-hidden="true"> Alterar</span> 

                                </asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="CancelarButton" class="btn btn-danger btn-labeled fa fa-remove fa-lg" Visible="false"
                                    runat="server" CausesValidation="False" OnClick="CancelarButton_Click" title="Cancelar"
                                    data-rel="tooltip">
                                                    Cancelar

                                </asp:LinkButton>

                        <br />
                        <br />
                        <asp:GridView ID="ContatoGridView" runat="server" 
                        EmptyDataText="Nenhum Contato Cadastrado."
                        CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                            Style="border-collapse: collapse; max-width: 100%" AutoGenerateColumns="False">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="ENTCONTATOIDLabel" Text='<%# Bind("ENTCONTATOID") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa">
                                    <ItemTemplate>
                                        <asp:Label ID="EmpresaLabel" Text='<%# Bind("Empresa") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nome">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="NomeLabel" Text='<%# Bind("Nome") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="EmailLabel" Text='<%# Bind("Email") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DDD">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="DDDTelefoneLabel" Text='<%# Bind("DDDTelefone") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Telefone">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="TelefoneLabel" Text='<%# Bind("Telefone") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ramal">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="RamalLabel" Text='<%# Bind("Ramal") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="TipoContatoLabel" Text='<%# Bind("TipoContato") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cargo">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="CargoLabel" Text='<%# Bind("Cargo") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alterar">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="AlterarButton" class="btn btn-warning fa fa-pencil-square-o fa-lg" 
                                    runat="server" CausesValidation="False" OnClick="AlterarButton_Click_Grid" title="Alterar"
                                    data-rel="tooltip"></asp:LinkButton>


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remover">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <center>
                                            

                                            <asp:LinkButton ID="RemoverButton" class="btn btn-danger fa fa-trash fa-lg" 
                                    runat="server" CausesValidation="False" OnClick="RemoverButton_Click" title="Remover"
                                    data-rel="tooltip"></asp:LinkButton>


                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <asp:Literal ID="ENTCONTATOIDLiteral" runat="server" Visible="false"></asp:Literal>
                <br />
                <br />
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">
                            <asp:LinkButton ID="VoltarLinkButton" class="btn btn-warning btn-labeled fa fa-arrow-circle-left fa-lg"
                                CausesValidation="false" runat="server" title="Voltar" data-rel="tooltip" OnClick="VoltarButton_Click"> 
             Retornar </asp:LinkButton>
                            <asp:LinkButton ID="ProximoPassoButton" class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg"
                                CausesValidation="False" runat="server" title="Próxima Tela" data-rel="tooltip"
                                OnClick="ProximoPassoButton_Click"> 
             Próximo </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    </div>
</asp:Content>
