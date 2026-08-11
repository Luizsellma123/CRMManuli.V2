<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="esqueci.aspx.cs" Inherits="VendasWeb.esqueci" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<html lang="pt-BR">
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Login | CRM Manuli Fitasa</title>


	<!--STYLESHEET-->
	<!--=================================================-->

	<!--Open Sans Font [ RECOMENDADO ] -->
 	<link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700&amp;subset=latin" rel="stylesheet">


	<!--Bootstrap Stylesheet [ NECESSÁRIO ]-->
	<link href="css/bootstrap.min.css" rel="stylesheet">


	<!--CRM Manuli Fitasa Stylesheet [ NECESSÁRIO ]-->
	<link href="css/crm-manulifitasa.min.css" rel="stylesheet">


    <!--Font Awesome [ RECOMENDADO ]-->
    <link href="plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet">


	<!-- Classe criada para personalizar a imagem de fundo [ AMOSTRA ]-->
	<style>
		.my-bg{
			background-image : url("img/bg2.jpg");
		}
	</style>


	


    <!--SCRIPT-->
    <!--=================================================-->

    <!--Page Load Progress Bar [ OPCIONAL ]-->
    <link href="plugins/pace/pace.min.css" rel="stylesheet">
    <script src="plugins/pace/pace.min.js"></script>



	
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

<body class="nifty-ready  pace-done"><div class="pace  pace-inactive"><div class="pace-progress" data-progress-text="100%" data-progress="99" style="width: 100%;">
  <div class="pace-progress-inner"></div>
</div>
<div class="pace-activity"></div></div>

	<!-- CONTAINER -->
	<!--===================================================-->		
	<div id="container" class="cls-container">

		<!-- BACKGROUND IMAGE -->
		<!--===================================================-->
		<div id="bg-overlay" class="bg-img my-bg"></div>
		
		
		<!-- HEADER -->
		<!--===================================================-->
		<div class="cls-header cls-header-lg">
			<div class="cls-brand">
					<img alt="CRM Manuli Fitasa" src="img/logomanuli.png" class="brand-icon">
					<span class="brand-title">CRM Manuli Fitasa <span class="text-thin">:: Acesso</span></span>
			</div>
		</div>
		<!--===================================================-->
		
		
		<!-- PASSWORD RESETTING FORM -->
		<!--===================================================-->
		<div class="cls-content">
			<div class="cls-content-sm panel">
				<div class="panel-body">
					<p class="pad-btm">Insira o seu e-mail cadastrado</p>
					<form id="FormEsqueci" runat="server">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon"><i class="fa fa-envelope"></i></div>
								
                                <asp:TextBox ID="txtUsuario" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
							</div>
						</div>
						<div class="form-group text-right">
							
                            <asp:Button ID="btnEnviar" runat="server" Text="Recuperar Senha" class="btn btn-success text-uppercase"
                    onclick="btnEnviar_Click" />


						</div>

                        <asp:Label ID="lblError" runat="server" Text="lblError" Visible="false" CssClass="textoErro">
                Email incorreto favor verificar.
            </asp:Label>
					</form>
				</div>
			</div>
			<div class="pad-ver">
				<a href="login.aspx?indmnu=0" class="btn-link mar-rgt">Voltar para o Login</a>
			</div>
		</div>
		<!--===================================================-->
		
		
		
	</div>
	<!--===================================================-->
	<!-- END OF CONTAINER -->


		
	<!--JAVASCRIPT-->
    <!--=================================================-->

    <!--jQuery [ NECESSÁRIO ]-->
    <script src="js/jquery-2.1.1.min.js"></script>


    <!--BootstrapJS [ RECOMENDADO ]-->
    <script src="js/bootstrap.min.js"></script>

    
    <!--Nifty Admin [ RECOMENDADO ]-->
    <script src="js/crm-manulifitasa.min.js"></script>

    
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
		

</body></html>