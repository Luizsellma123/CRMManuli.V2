<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroClienteEnderecosWebForm.aspx.cs" Inherits="VendasWeb.Clientes.CadastroClienteEnderecosWebForm" %>

<%@ Register Src="~/usercontrol/UCCadastroCliente.ascx" TagPrefix="uc1" TagName="UCCadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/fstdropdown.js?aux=1")%>" type="text/javascript"></script>

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
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <%--<button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>--%>
                    </div>
                    <h3 class="panel-title">Cadastro Cliente - Endereços</h3>
                </div>
                <!--Painel Aberto-->
                <!--Campos para escolha da carteira e do cliente-->

                <!-- END Painel Aberto-->
                <!--===================================================-->
                <!--Painel FILTROS-->
                <!--===================================================-->
                <asp:Literal ID="PainelFiltrosLiteral" Text="<div id='filtros' class='collapse' aria-expanded='true' style='height: 0px;'>"
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
                                <asp:DropDownList ID="DescricaoEnderecoDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="ENTREGA|COBRANÇA" Selected="True">ENTREGA/COBRANÇA</asp:ListItem>
                                    <asp:ListItem>ENTREGA</asp:ListItem>
                                    <asp:ListItem>COBRANÇA</asp:ListItem>

                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="DescricaoEnderecoDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>

                        </div>


                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CEPLabel" runat="server" Text="CEP:"></asp:Label>
                            </div>
                        </div>


                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CEPTextBox" runat="server" onkeypress="mascara( this, mcep );"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CEPTextBoxRequiredFieldValidator" runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="CEPTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>


                            </div>
                        </div>

                    </div>


                    <div class="row">


                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="TipoLogradouroLabel" runat="server" Text="Tipo Logradouro:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="TipoLogradouroTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>


                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="RuaLabel" runat="server" Text="Nome Rua:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NomeRuaTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="NomeRuaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NumeroLabel" runat="server" Text="Número:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="NumeroTextBox" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="NumeroTextBoxRequiredFieldValidator" runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="NumeroTextBox" ErrorMessage="Preencher S/N para sem Número"></asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator ID="NumeroTextBoxRegularExpressionValidator"
                                    runat="server" ControlToValidate="NumeroTextBox" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Preencher S/N para sem Número" ForeColor="Red"
                                    ValidationExpression="((\d+$)|([sS]+[/]+[nN]))$">Preencher S/N para sem Número</asp:RegularExpressionValidator>

                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ComplementoLabel" runat="server" Text="Complemento:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="ComplementoTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">


                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="BairroLabel" runat="server" Text="Bairro:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="BairroTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="BairroTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CidadeLabel" runat="server" Text="Cidade:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CidadeTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="CidadeTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="EstadoLabel" runat="server" Text="Estado:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="EstadoDropDownList" runat="server"
                                            CssClass="form-control fstdropdown-select"
                                            AutoPostBack="true" OnSelectedIndexChanged="EstadoDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="EstadoDropDownList" Display="Dynamic" ErrorMessage="*"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="EstadoDropDownList" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="MunicipioLabel" runat="server" Text="Municipio:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="form-group">
                                <asp:UpdatePanel ID="MunicipioUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="MunicipioDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="MunicipioDropDownList" Display="Dynamic" ErrorMessage="*"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="MunicipioDropDownList" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>

                </div>


                <div class="panel-footer">
                    <div class="row">

                        <div class="panel-control">

                            <asp:LinkButton ID="BuscarButton" class="btn btn-primary btn-labeled fa fa-search fa-lg"
                                CausesValidation="false" runat="server" OnClick="BuscarButton_Click" Visible="false">Buscar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="GravarButton" class="btn btn-success btn-labeled fa fa-arrow-circle-down fa-lg"
                                runat="server" OnClick="GravarButton_Click" Visible="false">Gravar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>

                        </div>

                    </div>
                </div>
            </div>

        </div>

        <asp:MultiView ID="ClientesEnderecosMultiView" runat="server" ActiveViewIndex="0" Visible="false">
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

                            <asp:GridView ID="ClienteEnderecosGridView" EmptyDataText="Não foram encontrados dados com esses filtros" AutoGenerateColumns="False"
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

                                    <asp:TemplateField HeaderText="IDEndereco" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDEnderecoLabel" runat="server" Text='<%# Bind("IDEndereco") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipo">
                                        <ItemTemplate>
                                            <asp:Label ID="TipoEnderecoLabel" runat="server" Text='<%# Bind("DescricaoEndereco") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Endereço ">
                                        <ItemTemplate>
                                            <asp:Label ID="EnderecoLabel" runat="server" Text='<%# Bind("Endereco") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cidade ">
                                        <ItemTemplate>
                                            <asp:Label ID="CidadeLabel" runat="server" Text='<%# Bind("Cidade") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Estado ">
                                        <ItemTemplate>
                                            <asp:Label ID="EstadoLabel" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
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
