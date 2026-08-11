<%@ Page Title="" Language="C#" MasterPageFile="~/CRM.master" AutoEventWireup="true" CodeBehind="FrmAbaPrincipal.aspx.cs" Inherits="VendasWeb.Entidade.FrmAbaPrincipal" %>

<%@ Register Src="../usercontrol/CrmPainelWebUserControl.ascx" TagName="ControlPainel" TagPrefix="ucp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>

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
                    <h3 class="panel-title">Cadastro de Cliente</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->


                <div class="panel-body">
                    <div class="table-responsive">


                        <div id="Dados">

                            <div id="DataCadastroDiv" class="detCabeccario" style="display: none;" runat="server">
                                <asp:Label ID="DataCadastroLabel" Text="" runat="server"></asp:Label><br />
                                <asp:Label ID="StatusLabel1" Text="Status de Cadastro:" runat="server"></asp:Label><asp:Label ID="StatusLabel" Text="" runat="server"></asp:Label><br />
                                <asp:Label ID="EntCodLabel1" Text="Código da Entidade:" runat="server" Visible="true"></asp:Label><asp:Label ID="EntCodLabel" Text="" runat="server" Visible="true"></asp:Label>
                            </div>
                            <br />

                            <!--Filtro Vendedor-->

                            <h5>
                                <asp:Label ID="VendCodLabel" runat="server" CssClass="text-thin" Text="" Width="130">Escolher Vendedor:</asp:Label></h5>
                            <asp:DropDownList ID="VendCodDropDownList" runat="server" CssClass="form-control" Width="450px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="VendCodDropDownList" ErrorMessage="Selecione um Vendedor!"></asp:RequiredFieldValidator>
                            <br />


                            <h5>
                                <asp:Label ID="CategoriaLabel" runat="server" CssClass="text-thin" Text="" Width="130" Visible="false">Escolher categoria:</asp:Label></h5>
                            <asp:DropDownList ID="CategoriaDropDownList" runat="server" CssClass="form-control" Width="450px" Visible="false">
                            </asp:DropDownList>


                            <h5>
                                <asp:Label ID="CnpjCpfLabel" runat="server" CssClass="text-thin" Text="CNPJ/CPF:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="Cnpj_CpfTextBox" class="form-control" runat="server" AutoPostBack="true" onkeypress="mascara( this, cpfcnpj );" onblur="mascara( this, cpfcnpj );" onfocus="mascara( this, cpfcnpj );" OnTextChanged="Cnpj_CpfTextBox_TextChanged" Width="450px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Cnpj_CpfTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />


                            <h5>
                                <asp:Label ID="razaoSocialLabel" runat="server" CssClass="text-thin" Text="Razão Social:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="razaoSocialTextBox" runat="server" MaxLength="100" class="uppercase form-control" Width="450px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="razaoSocialTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />

                            <h5>
                                <asp:Label ID="NomeFantasiaLabel" runat="server" CssClass="text-thin" Text="Nome Fantasia:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="NomeFantasiaTextBox" runat="server" MaxLength="40" class="form-control" Width="450px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NomeFantasiaTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />

                            <h5>
                                <asp:Label ID="CepLabel" runat="server" CssClass="text-thin" Text="CEP:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="CepTextBox" runat="server" AutoPostBack="true" CausesValidation="False" onkeypress="mascara( this, mcep );" OnTextChanged="CepTextBox_TextChanged" class="form-control" Width="450px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="CepTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />

                            <h5>
                                <asp:Label ID="EnderecoLabel" runat="server" CssClass="text-thin" Text="Endereço:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="EnderecoTextBox" runat="server" class="form-control" Width="450px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="EnderecoTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />

                            <h5>
                                <asp:Label ID="NumeroLabel" runat="server" CssClass="text-thin" Text="Número:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="NumeroTextBox" runat="server" OnTextChanged="NumeroTextBox_TextChanged" class="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="NumeroTextBox" Display="Dynamic" ErrorMessage="Preencher S/N para sem Número" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="NumeroTextBox" Display="Dynamic" ErrorMessage="Preencher S/N para sem Número" ForeColor="Red" SetFocusOnError="True" ValidationExpression="((\d+$)|([sS]+[/]+[nN]))$">Preencher S/N para sem Número</asp:RegularExpressionValidator>
                            <br />

                            <h5>
                                <asp:Label ID="BairroLabel" runat="server" CssClass="text-thin" Text="Bairro:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="BairroTextBox" runat="server" class="form-control" Width="450px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="BairroTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />

                            <h5>
                                <asp:Label ID="UFLabel" runat="server" CssClass="text-thin" Text="UF:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="UFTextBox" runat="server" MaxLength="2" OnTextChanged="UFTextBox_TextChanged" class="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="UFTextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />

                            <h5>
                                <asp:Label ID="CidadeLabel" runat="server" CssClass="text-thin" Text="Cidade:" Width="130"></asp:Label></h5>
                            <asp:DropDownList ID="CidadeDropDownList" runat="server" CssClass="form-control" Width="450px" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="CidadeDropDownList" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />

                            <h5>
                                <asp:Label ID="ComplementoLabel" runat="server" CssClass="text-thin" Text="Complemento:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="ComplementoTextBox" runat="server" class="form-control" Width="450px"></asp:TextBox>
                            <br />

                            <h5>
                                <asp:Label ID="InscricaoEstadualLabel" runat="server" CssClass="text-thin" Text="Inscrição Estadual:" Width="130"></asp:Label></h5>
                            <asp:TextBox ID="InscricaoEstadualTextBox" runat="server" AutoPostBack="True" OnTextChanged="InscricaoEstadualTextBox_TextChanged" class="form-control" Width="450px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="InscricaoEstadualTextBox" Display="Dynamic" ErrorMessage="Preencher ISENTO" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />



                        </div>


                    </div>
                </div>
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">


                            <asp:LinkButton ID="VoltarLinkButton" class="btn btn-warning btn-labeled fa fa-arrow-circle-left fa-lg" CausesValidation="false"
                                runat="server" title="Voltar" data-rel="tooltip" OnClick="VoltarButton_Click" Visible="false"> 
             Retornar </asp:LinkButton>

                            <asp:LinkButton ID="ProximoPassoButton" class="btn btn-primary btn-labeled fa fa-arrow-circle-right fa-lg"
                                runat="server" title="Próxima Tela" data-rel="tooltip" OnClick="ProximoPassoButton_Click"> 
             Próximo </asp:LinkButton>




                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->

        <!----PAINEL----->
        <ucp:ControlPainel ID="ControlPainel" runat="server" UpdateMode="Conditional" runat="server" />
    </div>


</asp:Content>
