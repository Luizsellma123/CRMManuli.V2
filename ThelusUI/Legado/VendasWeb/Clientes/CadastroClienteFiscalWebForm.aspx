<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPageCRM.master" AutoEventWireup="true" CodeBehind="CadastroClienteFiscalWebForm.aspx.cs" Inherits="VendasWeb.Clientes.CadastroClienteFiscalWebForm" %>

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
                    <h3 class="panel-title">Cadastro Cliente - Fiscal</h3>
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
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
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
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="NaturezaJuridicaLabel" runat="server" Text="Natureza Jurídica:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="NaturezaJuridicaDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="NaturezaJuridicaDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="IndicadorIELabel" runat="server" Text="Indicador IE:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:DropDownList ID="IndicadorIEDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                    <asp:ListItem Value="0">Padrão definido pelo sistema</asp:ListItem>
                                    <asp:ListItem Value="1">Contribuinte ICMS (informar a IE do destinatário)</asp:ListItem>
                                    <asp:ListItem Value="2">Contribuinte isento de Inscrição</asp:ListItem>
                                    <asp:ListItem Value="9">Não Contribuinte</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="IndicadorIEDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>
                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="IndicadorOPFinallLabel" runat="server" Text="Op. Consumidor:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="OperadorConsumidorDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                    <asp:ListItem Value="0">Normal</asp:ListItem>
                                    <asp:ListItem Value="1">Consumidor Final</asp:ListItem>
                                </asp:DropDownList>


                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="OperadorConsumidorDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="IndicadorNaturezaLabel" runat="server" Text="Ind. Natureza:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:DropDownList ID="IndicadorNaturezaDropDownList" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                        <asp:ListItem Value="01">Órgãos, Autarquias e Fundações Federais</asp:ListItem>
                                        <asp:ListItem Value="02">Entidades da Administração Pública Federal</asp:ListItem>
                                        <asp:ListItem Value="03">Pessoas Jurídicas de Direito Privado</asp:ListItem>
                                        <asp:ListItem Value="04">Sociedade Cooperativa</asp:ListItem>
                                        <asp:ListItem Value="05">Fabricante de Máquinas e Veículos</asp:ListItem>
                                        <asp:ListItem Value="99">Outros</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="IndicadorNaturezaDropDownList" Display="Dynamic" ErrorMessage="*"
                                        SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="EnquadramentoTributarioLabel" runat="server" Text="Enq. Tributário:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="EnquadramentoTributarioDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="EnquadramentoTributarioDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SimplesNacionalLabel" runat="server" Text="Simples Nacional:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:DropDownList ID="SimplesNacionalDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                    <asp:ListItem Value="1">Optante ME/EPP</asp:ListItem>
                                    <asp:ListItem Value="2">Não Optante</asp:ListItem>
                                    <asp:ListItem Value="3">Optante MEI</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                    ControlToValidate="SimplesNacionalDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CartaIPILabel" runat="server" Text="Carta IPI:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="CartaIPIDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                    <asp:ListItem Value="Não">Não</asp:ListItem>
                                    <asp:ListItem Value="Sim">Sim</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                    ControlToValidate="CartaIPIDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="DataCartaLabel" runat="server" Text="Data Carta:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="RecebimentoCartaTextBox" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <!--===================================================-->
                        <!-- END LINHA 1 - Painel FILTROS-->
                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="ProdutoRuralLabel" runat="server" Text="Prod. Rural:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ProdutorRuralDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                    <asp:ListItem Value="Não">Não</asp:ListItem>
                                    <asp:ListItem Value="Sim">Sim</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                    ControlToValidate="ProdutorRuralDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CPOMLabel" runat="server" Text="CPOM:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:DropDownList ID="CPOMDropDownList" runat="server" CssClass="form-control">

                                    <asp:ListItem Selected="True" Value="Sim">Sim</asp:ListItem>
                                    <asp:ListItem Value="Nao">Não</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                    ControlToValidate="CPOMDropDownList" Display="Dynamic" ErrorMessage="*"
                                    SetFocusOnError="True" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Natureza de Destinação:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:CheckBoxList ID="IDNaturezaDestinacaoCheckBoxList" runat="server"></asp:CheckBoxList>
                            </div>
                        </div>

                    </div>


                    <div class="row">

                        <hr />

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="SuframaLabel" runat="server" Text="Suframa:"></asp:Label>
                            </div>
                        </div>


                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="SuframaTextBox" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CodigoCNAELabel" runat="server" Text="Código CNAE:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">

                            <div class="form-group">
                                <asp:DropDownList ID="IDCNAEDropDownList" runat="server" CssClass="form-control fstdropdown-select">
                                </asp:DropDownList>

                            </div>

                        </div>

                    </div>

                    <div class="row">

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="InscricaoEstadualLabel" runat="server" Text="Inscrição:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="InscricaoEstadualTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="InscricaoEstadualTextBox" ErrorMessage="Preencher ISENTO" ValidationGroup="Fiscal"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="CNPJLabel" runat="server" Text="Número CNPJ:"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-5">
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="CNPJTextBox"
                                    onkeypress="mascara( this, cpfcnpj );" onblur="mascara( this, cpfcnpj );" onfocus="mascara( this, cpfcnpj );"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <hr />
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="AvisoLabel" runat="server" Text="Dúvidas quanto ao preenchimento da tela entrar em contato com o setor <b>FISCAL</b> da <b>Manuli</b> (41) 3212-6666 ou (41) 2169-6000."></asp:Label>
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
                                runat="server" OnClick="GravarButton_Click" Visible="false" ValidationGroup="Fiscal">Gravar</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="GravarNaturezaDestinacaoButton" class="btn btn-success btn-labeled fa fa-save fa-lg"
                                runat="server" OnClick="GravarNaturezaDestinacaoButton_Click" Visible="false">Atualiza Natureza Destinação</asp:LinkButton>

                            &nbsp;<asp:LinkButton ID="RetornarLinkButton" class="btn btn-danger btn-labeled fa fa-arrow-circle-left fa-lg"
                                runat="server" OnClick="RetornarButton_Click" CausesValidation="false">Retornar</asp:LinkButton>

                        </div>

                    </div>
                </div>

            </div>

        </div>


        <asp:MultiView ID="FiscalMultiView" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="FiscalView" runat="server">
                <!-- TABELA -->
                <!--===================================================-->
                <div class="panel">
                    <div class="panel-heading">
                    </div>
                    <!-- Foo Table - Filtering -->
                    <!--===================================================-->
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView ID="FiscalGridView" EmptyDataText="Não foram encontrados dados com esses filtros" AutoGenerateColumns="False"
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

                                    <asp:TemplateField HeaderText="IDCNAE" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IDCNAELabel" runat="server" Text='<%# Bind("IDCNAE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="CNAE">
                                        <ItemTemplate>
                                            <asp:Label ID="CodigoCNAESapLabel" runat="server" Text='<%# Bind("CodigoCNAESap") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Descrição">
                                        <ItemTemplate>
                                            <asp:Label ID="DescricaoCNAELabel" runat="server" Text='<%# Bind("DescricaoCNAE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CNPJ">
                                        <ItemTemplate>
                                            <asp:Label ID="CNPJLabel" runat="server" Text='<%# Bind("CNPJ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inscrição Estadual">
                                        <ItemTemplate>
                                            <asp:Label ID="InscricaoEstadualLabel" runat="server" Text='<%# Bind("InscricaoEstadual") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Suframa">
                                        <ItemTemplate>
                                            <asp:Label ID="SuframaLabel" runat="server" Text='<%# Bind("Suframa") %>'></asp:Label>
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
