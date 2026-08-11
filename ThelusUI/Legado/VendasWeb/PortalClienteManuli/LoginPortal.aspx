<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="LoginPortal.aspx.cs" Inherits="VendasWeb.LoginPortal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="pt-BR">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login | CRM Manuli Fitasa</title>
    <!--STYLESHEET-->
    <!--=================================================-->
    <!--Open Sans Font [ RECOMENDADO ] -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700&amp;subset=latin"
        rel="stylesheet">
    <!--Bootstrap Stylesheet [ NECESSÁRIO ]-->
    <link href="~/css/bootstrap.min.css?aux=1" rel="stylesheet">
    <!--CRM Manuli Fitasa Stylesheet [ NECESSÁRIO ]-->
    <link href="~/css/crm-manulifitasa.min.css?aux=1" rel="stylesheet">
    <!--Font Awesome [ RECOMENDADO ]-->
    <link href="~/plugins/font-awesome/css/font-awesome.min.css?aux=1" rel="stylesheet">
    <!-- Classe criada para personalizar a imagem de fundo [ AMOSTRA ]-->
    <style>
        .my-bg
        {
            background-image: url("../img/bg2.jpg");
        }
    </style>
    <!--SCRIPT-->
    <!--=================================================-->
    <!--Page Load Progress Bar [ OPCIONAL ]-->
    <link href="~/plugins/pace/pace.min.css?aux=1" rel="stylesheet">
    <script src="~/plugins/pace/pace.min.js?aux=1"></script>
    <!--

	NECESSÁRIO
	Isto deve estar incluído.

	RECOMENDADO
	Esta categoria deve ser incluída mas podem ser escolhidos quais plugins ou componentes utilizar.

	OPCIONAL
	Itens opcionais. Podem ser incluídos ou não.

	DEMONSTRAÇÃO
	Itens apenas para demonstração. Não devem ser incluídos na versão final.

	AMOSTRAS
	Amostras de como utilizar componentes ou scripts. Não devem ser incluídos na versão final.

	-->
</head>
<!--END HEAD-->
<body class="nifty-ready  pace-done">
    <div class="pace  pace-inactive">
        <div class="pace-progress" data-progress-text="100%" data-progress="99" style="width: 100%;">
            <div class="pace-progress-inner">
            </div>
        </div>
        <div class="pace-activity">
        </div>
    </div>
    <!-- CONTAINER -->
    <!--===================================================-->
    <div id="container" class="cls-container">
        <!-- BACKGROUND IMAGE -->
        <!--===================================================-->
        <div id="bg-overlay" class="bg-img my-bg">
        </div>
        <!-- HEADER -->
        <!--===================================================-->
        <div class="cls-header cls-header-lg">
            <div class="cls-brand">
                <img alt="CRM Manuli Fitasa" src="<%=Page.ResolveClientUrl("~/img/logomanuli.png")%>" class="brand-icon">
                <span class="brand-title">Portal Manuli Fitasa <span class="text-thin">:: Acesso</span></span>
            </div>
        </div>
        <!--===================================================-->
        <!-- LOGIN FORM -->
        <!--===================================================-->
        <form id="FormLogin" runat="server">
        <div class="cls-content">
            <div class="cls-content-sm panel">
                <div class="panel-body">
                    <p class="pad-btm">
                        Entre com a sua conta</p>
                    <form action="index.html">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-user"></i>
                            </div>
                            <asp:TextBox ID="UsuarioTextBox" runat="server" CssClass="form-control" placeholder="Usuário"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-asterisk"></i>
                            </div>
                            <asp:TextBox ID="SenhaTextBox" runat="server" TextMode="Password" class="form-control"
                                placeholder="Senha"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                     <div class="col-xs-8 text-left checkbox">
								<label class="form-checkbox form-icon form-text">
								<input type="checkbox"> Lembrar do Login
								</label>
							</div>
                          

                        <div class="col-xs-4">
                           
                            <div class="form-group text-right">
                                <asp:Button ID="Button1" runat="server" Text="Entrar" OnClick="Button1_Click" class="btn btn-success text-uppercase" />
                                <br />
                                <asp:Label ID="lblError" runat="server" Text="lblError" Visible="false" CssClass="textoErro"></asp:Label>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
            <div class="pad-ver">
                <a href="esqueci.aspx" class="btn-link mar-rgt">Esqueci minha senha</a>
                <!-- NOVO USUARIO -->
                <!--===================================================-->
                <!--<a href="#" class="btn-link mar-lft">Criar um novo usuário</a>-->
                <!-- END OF NOVO USUARIO -->
                <!--===================================================-->
            </div>
        </div>
        </form>
        <!--===================================================-->
    </div>
    <!--===================================================-->
    <!-- END OF CONTAINER -->
    <!--JAVASCRIPT-->
    <!--=================================================-->
    <!--jQuery [ NECESSÁRIO ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/jquery-2.1.1.min.js")%>"></script>
    <!--BootstrapJS [ RECOMENDADO ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/bootstrap.min.js")%>"></script>
    <!--Nifty Admin [ RECOMENDADO ]-->
    <script src="<%=Page.ResolveClientUrl("~/js/crm-manulifitasa.min.js")%>"></script>
    <!--

	NECESSÁRIO
	Isto deve estar incluído.

	RECOMENDADO
	Esta categoria deve ser incluída mas podem ser escolhidos quais plugins ou componentes utilizar.

	OPCIONAL
	Itens opcionais. Podem ser incluídos ou não.

	DEMONSTRAÇÃO
	Itens apenas para demonstração. Não devem ser incluídos na versão final.

	AMOSTRAS
	Amostras de como utilizar componentes ou scripts. Não devem ser incluídos na versão final.

	-->
</body>
</html>
