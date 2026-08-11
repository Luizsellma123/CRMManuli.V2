<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroClienteContatosWebForm.aspx.cs" Inherits="VendasWeb.Clientes.CadastroClienteContatosWebForm" %>

<%@ Register Src="~/usercontrol/UCCadastroCliente.ascx" TagPrefix="uc1" TagName="UCCadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">

        <div class="col-sm-9">
            <!--===================================================-->
            <!--Painel Carteiras e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <%--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>--%>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse" >
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--<button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>--%>
                    </div>
                    <h3 class="panel-title">Cadastro Cliente - Contatos</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text=""
                    runat="server"></asp:Literal>

                <div class="panel-body">

                    <asp:HiddenField ID="IDCliente" runat="server" />

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoCliente" runat="server" Text="Código:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CodigoClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                     ControlToValidate="CodigoClienteTextBox" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NomeClienteTextBox" runat="server" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                     ControlToValidate="NomeClienteTextBox" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TipoContatoLabel" runat="server" Text="Tipo Contato:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="TipoContatoDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                    <asp:ListItem>FINANCEIRO</asp:ListItem>
                                    <asp:ListItem>COMERCIAL1</asp:ListItem>
                                    <asp:ListItem>COMERCIAL2</asp:ListItem>
                                    <asp:ListItem>COMERCIAL3</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                     ControlToValidate="TipoContatoDropDownList" Display="Dynamic" ErrorMessage="*" 
                                     SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>


                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ContatoLabel" runat="server" Text="Pessoa Contato:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ContatoTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ContatoTextBoxRequiredFieldValidator" runat="server" Display="Dynamic" SetFocusOnError="True"
                        ControlToValidate="ContatoTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EmailLabel" runat="server" Text="Email:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="EmailTextBox" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="EmailTextBox1RegularExpressionValidator"
                                    runat="server" ControlToValidate="EmailTextBox" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Email Invalido" ForeColor="Red"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email Inválido</asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TelefoneLabel" runat="server" Text="Telefone:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="TelefoneTextBox" runat="server" onkeypress="mascara( this, mnum );"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>


                
                  <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                             <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                CausesValidation="false" runat="server" OnClick="BuscarButton_Click" Visible="false" >Buscar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                                 runat="server" OnClick="GravarButton_Click" Visible="false">Gravar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                 runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>
                           
                        </div>

                    </div>
                </div>


            </div>

        </div>

        <asp:MultiView ID="ClientesContatosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="PSIUView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="ClienteContatosGridView" EmptyDataText="Não foram encontrados dados com esses filtros" AutoGenerateColumns="False"
                                runat="server" AllowPaging="false"
                                CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                                Style="border-collapse: collapse; max-width: 100%">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <center>
                                            <asp:LinkButton ID="DeleteButton" class="btn btn-danger fa fa-times fa-lg"
                                                CausesValidation="false" runat="server" OnClick="DeleteButton_Click"></asp:LinkButton>

                                                </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IDContato" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDContatoLabel" runat="server" Text='<%# Bind("IDContato") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipo">
                                        <ItemTemplate>
                                            <asp:Label ID="TipoContatoLabel" runat="server" Text='<%# Bind("TipoContato") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pessoa Contato ">
                                        <ItemTemplate>
                                            <asp:Label ID="NomeLabel" runat="server" Text='<%# Bind("Nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Telefone ">
                                        <ItemTemplate>
                                            <asp:Label ID="TelefoneLabel" runat="server" Text='<%# Bind("Telefone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email ">
                                        <ItemTemplate>
                                            <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <!--===================================================-->
                </div>
                <!-- End Foo Table - Filtering -->
                <!--===================================================-->
                <!-- END TABELA -->
            </asp:View>
        </asp:MultiView>

    </div>

    <uc1:UCCadastroCliente runat="server" ID="UCCadastroCliente" />
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
    </div>
    <!-- Esse div fecha o div class="panel panel-info" quando rodado o projeto-->
    <!--NÂO REMOVER ESSE /DIV pois esta vinculado ao Literal ID="collapseLiteral" -->
</asp:Content>
