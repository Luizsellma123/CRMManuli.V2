
// UI-Modals.js
// ====================================================================
// This file should not be included in your project.
// This is just a sample how to initialize plugins or components.
//
// - ThemeOn.net -


 $(document).ready(function() {

	// BOOTBOX - ALERT MODAL
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-alert').on('click', function(){
		bootbox.alert("Hello world!", function(){
			$.niftyNoty({
				type: 'info',
				icon : 'fa fa-info',
				message : 'Hello world callback',
				container : 'floating',
				timer : 3000
			});
		});
	});



	// BOOTBOX - CONFIRM MODAL
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-confirm').on('click', function(){
		bootbox.confirm("Are you sure?", function(result) {
			if (result) {
				$.niftyNoty({
					type: 'success',
					icon : 'fa fa-check',
					message : 'User confirmed dialog',
					container : 'floating',
					timer : 3000
				});
			}else{
				$.niftyNoty({
					type: 'danger',
					icon : 'fa fa-minus',
					message : 'User declined dialog.',
					container : 'floating',
					timer : 3000
				});
			};

		});
	});



	// BOOTBOX - PROMPT MODAL
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-prompt').on('click', function(){
		bootbox.prompt("What is your name?", function(result) {
			if (result) {
				$.niftyNoty({
					type: 'success',
					icon : 'fa fa-check',
					message : 'Hi ' + result,
					container : 'floating',
					timer : 3000
				});
			}else{
				$.niftyNoty({
					type: 'danger',
					icon : 'fa fa-minus',
					message : 'User declined dialog.',
					container : 'floating',
					timer : 3000
				});
			};
		});
	});



	// BOOTBOX - CUSTOM DIALOG
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-custom').on('click', function(){
		bootbox.dialog({
			message: "I am a custom dialog",
			title: "Custom title",
			buttons: {
				success: {
					label: "Success!",
					className: "btn-success",
					callback: function() {
						$.niftyNoty({
							type: 'success',
							icon : 'fa fa-check',
							message : '<strong>Well done!</strong> You successfully read this important alert message. ',
							container : 'floating',
							timer : 3000
						});
					}
				},

				danger: {
					label: "Danger!",
					className: "btn-danger",
					callback: function() {
						$.niftyNoty({
							type: 'danger',
							icon : 'fa fa-times',
							message : '<strong>Oh snap!</strong> Change a few things up and try submitting again.',
							container : 'floating',
							timer : 3000
						});
					}
				},

				main: {
					label: "Click ME!",
					className: "btn-primary",
					callback: function() {
						$.niftyNoty({
							type: 'primary',
							icon : 'fa fa-thumbs-o-up',
							message : "<strong>Heads up!</strong> This alert needs your attention, but it's not super important.",
							container : 'floating',
							timer : 3000
						});
					}
				}
			}
		});
	});



	// BOOTBOX - CUSTOM HTML CONTENTS
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-custom-h-content').on('click', function(){
		bootbox.dialog({
			title: "That html",
			message: '<div class="media"><div class="media-left"><img class="media-object img-lg img-circle" src="img/av3.png" alt="Profile picture"></div><div class="media-body"><h4 class="text-thin">You can also use <strong>html</strong></h4>Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin commodo. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.</div></div>',
			buttons: {
				confirm: {
					label: "Save"
				}
			}
		});
	});



	// BOOTBOX - CUSTOM HTML FORM
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-custom-h-form').on('click', function(){
		bootbox.dialog({
			title: "Formulário de Agendamento de Visita",
			message:'<div class="row"> ' + '<div class="col-md-12"> ' +
					'<form class="form-horizontal"> ' + '<div class="form-group"> ' +
					'<label class="col-md-4 control-label" for="name">Data:</label> ' +
					'<div class="col-md-4"> ' +
					'<p class="text-thin mar-btm">Início:</p><div id="demo-dp-component"><div class="input-group date"><input type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span></div></div>' +
					'<div class="input-group date"><input id="demo-tp-com" type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span></div>' +
					'</div> ' +
					'</div> ' + '<div class="form-group"> ' +
					'<label class="col-md-4 control-label" for="tipocliente">Condição de Visita</label> ' +
					'<div class="col-md-8"> <div class="form-block"> ' +
					'<label class="form-radio form-icon demo-modal-radio"><input type="radio" autocomplete="off" name="tipocliente" value="Cliente Novo" checked> Cliente Novo</label>' +
					'<label class="form-radio form-icon demo-modal-radio"><input type="radio" autocomplete="off" name="tipocliente" value="Recuperação de Inativo"> Recuperação de Inativo</label> </div>' +
					'<label class="form-radio form-icon demo-modal-radio"><input type="radio" autocomplete="off" name="tipocliente" value="Manutenção"> Manutenção </label> </div>' +
					'</div> </div>' + '</form> </div> </div><script></script>',
			buttons: {
				success: {
					label: "Salvar Agendamento",
					className: "btn-success",
					callback: function() {
						var name = $('#name').val();
						var answer = $("input[name='tipocliente']:checked").val();

						$.niftyNoty({
							type: 'purple',
							icon : 'fa fa-check',
							message : "Olá, " + name + ".<br> Você salvou um agendamento para: <strong>" + answer + "</strong>",
							container : 'floating',
							timer : 4000
						});
					}
				}
			}
		});

		$(".demo-modal-radio").niftyCheck();
	});

	// BOOTBOX - MODAL DE ROTINA DE ATENDIMENTO 1
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-rotina1').on('click', function(){
		bootbox.dialog({
			title: "Registrar Atendimento",
			size: "large",
			message:'<div class="row"><div class="col-md-12 pad-top bg-gray"><div class="row pad-lft pad-rgt" ><table class="table table-condensed table-responsive"><thead><tr class="bg-gray-light"><th>Código</th><th>CNPJ/CPF</th><th>Nome</th><th>Cidade</th><th>Último Contato</th><th>Situação Comercial</th></tr></thead><tbody><tr class="bg-gray-light"><td>0942719</td><td>11966536000182</td><td>DONIPLAST IND. E COM. DE FORRO LTDA-ME</td><td>GOIANIA</td><td>12/12/2012</td><td><span class="label label-table label-success">Ativo</span></td></tr></tbody></table><table class="table table-condensed table-responsive"><thead><tr class="bg-gray-light"><th>Código do Vendedor</th><th>Nome Vendedor</th><th>Classe</th><th>Telefone 1</th><th>Telefone 2</th></tr></thead><tbody><tr class="bg-gray-light"><td>0000304</td><td>KAREN RIBEIRO/GO</td><td>GOIANIA</td><td>00 00000-0000</td><td>00 00000-0000</td></tr></tbody></table><table class="table table-condensed table-responsive"><thead><tr class="bg-gray-dark"><th>Nome do contato</th><th>Telefone</th><th>E-mail</th></tr></thead><tbody><tr><td>JOSE DA SILVA</td><td>00 00000-0000</td><td>emaildocontatodocliente@gmail.com</td></tr></tbody></table></div></div></div><div class="row"><div class="col-md-12 bg-gray"><div class="row pad-lft pad-rgt" ><div class="timeline mar-btm pad-no" style="padding-bottom: 0px;"><div class="timeline-entry mar-no"> <div class="timeline-stat"> <div class="timeline-icon bg-purple"><i class="fa fa-warning fa-lg"></i> </div><div class="timeline-time"><b>01/01/2016 00:00:00</b></div></div><div class="timeline-label"> <p class="mar-no pad-btm"> <span class="badge badge-purple">Observações Antigas</span> por <a href="#" class="btn-link btn-md text-semibold"> Michel.Santos</a></p><div class="well well-xs mar-no"> CADASTRO ATUALIZADO EM :26/9/2012 - 11:34:47Identificação CGC/CNPJ:01.107.605/0001-49 Inscrição Estadual - CCE :10.280.338-2 Nome / Razão Social:RACIONAL EMBALAGENS LTDAEndereço Logradouro:RUA 6 QD 18 LT 03 Número:S/N Complemento:QD 18 LT 03 Bairro:POLO EMPRESARIAL GOIAS Município:APARECIDA DE GOIANIA UF:GO CEP:74985105 Telefone:6235182002Informações Complementares Atividade Econômica:1731100 - FABRICACAO DE EMBALAGENS DE PAPEL Regime de Apuração:NORMAL Situação Cadastral Vigente:HABILITADO - Data desta Situação Cadastral:23/03/2011Observações - Os dados acima são baseados em informações fornecidas pelo contribuinte, estando sujeitos a posterior confirmação pelo FISCO Data da Consulta: 26/09/2012 - 11:34:09</div></div></div></div></div></div></div><div class="row"><div class="col-xs-12 pad-btm bg-gray"><div class="col-sm-12 col-md-6 col-lg-4"><div class="form-group mar-no"><textarea id="demo-textarea-input" rows="6" class="form-control" placeholder="Escreva aqui a Descrição do Evento..."></textarea></div></div><div class="col-sm-12 col-md-6 col-lg-8"><div class="col-lg-6"><div class="pad-btm"><select class="selectpicker show-tick" data-placeholder="Escolha um evento..." title="Escolha um evento..." data-style="btn-default" data-live-search="true"><option value="1">Atendimento</option><option value="2">Visita Teste</option><option value="3">Negociação</option><option value="4">Venda Fechada</option><option value="5">Venda Perdida</option><option value="6">Outros</option><option value="7">Pedido</option><option value="8">Nota</option></select></div><div class="pad-btm"><select class="selectpicker pad-btm show-tick" data-placeholder="Escolha uma categoria..." title="Escolha uma categoria..." data-style="btn-default" data-live-search="true"><option value="1">Telefone</option><option value="2">E-mail</option><option value="3">Visita</option><option value="4">Online</option></select></div></div><div class="col-lg-6"><div class="col-md-12"><input name="ctl00$ContentPlaceHolder1$txtData" type="text" id="ctl00_ContentPlaceHolder1_txtData" style="width:100px;"><select name="ctl00$ContentPlaceHolder1$drpHora" id="ctl00_ContentPlaceHolder1_drpHora" class="campo" style="width:60px;"><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="6">6</option><option value="7">7</option><option value="8">8</option><option value="9">9</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option><option value="22">22</option><option value="23">23</option></select></div></div></div></div>',
			buttons: {
				danger: {
					label: "Cancelar",
					className: "btn btn-danger btn-labeled fa fa-times",
					callback: function() {
						$.niftyNoty({
							type: 'danger',
							icon : 'fa fa-times',
							message : '<strong>Registro cancelado</strong>',
							container : 'floating',
							timer : 3000
						});
					}
				},

				success: {
					label: "Inserir Atendimento no Histórico",
					className: "btn-success btn-labeled fa fa-check",
					callback: function() {
						$.niftyNoty({
							type: 'success',
							icon : 'fa fa-check',
							message : '<strong>Histórico atualizado!</strong>',
							container : 'floating',
							timer : 3000
						});
					}
				},
			}
		});
	});

	// BOOTBOX - MODAL DE ROTINA DE ATENDIMENTO 2
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-rotina2').on('click', function(){
		bootbox.dialog({
			title: "Registrar Atendimento",
			size: "large",
			message:'<div class="row bg-gray"><div class="col-md-4"><table class="table table-condensed"><thead><tr><th colspan="2">Entidade</th></tr></thead><tbody><tr ><td class="text-right">Nome:</td><td class="text-bold">BALCONY SUL COMERCIO DE ESTRUTURAS METALICAS LTDA</td></tr><tr ><td class="text-right">CNPJ:</td><td class="text-bold"> 06261459000125 </td></tr><tr ><td class="text-right">Código:</td><td class="text-bold">0942719</td></tr><tr ><td class="text-right">Cidade:</td><td class="text-bold">GOIANIA</td></tr><tr ><td class="text-right">Último Contato:</td><td class="text-bold"> 12/12/2012 </td></tr><tr ><td class="text-right">Situação Comercial:</td><td class="text-bold">Ativo</td></tr></tbody></table></div><div class="col-md-4"><table class="table table-condensed"><thead><tr><th colspan="2">Vendedor</th></tr></thead><tbody><tr ><tr ><td class="text-right">Nome do Vendedor:</td><td class="text-bold">KAREN RIBEIRO/GO</td></tr><tr ><td class="text-right">Código do Vendedor:</td><td class="text-bold">0942719</td></tr><tr ><td class="text-right">Classe:</td><td class="text-bold">GOIANIA</td></tr><tr ><td class="text-right">Telefone 1:</td><td class="text-bold">00 00000-0000</td></tr><tr><td class="text-right">Telefone 2:</td><td class="text-bold">00 00000-0000</td></tr></tbody></table></div><div class="col-md-4"><table class="table table-condensed"><thead><tr><th colspan="2">Contato do Cliente</th></tr></thead><tbody><tr class="bg-trans-dark"><td class="text-right">Nome do Contato:</td><td class="text-bold">José da Silva</td></tr><tr class="bg-trans-dark"><td class="text-right">Telefone do Contato:</td><td class="text-bold">413606-4512</td></tr><tr class="bg-trans-dark"><td class="text-right">Email do Contato:</td><td class="text-bold">comercial@balconysul.com.br</td></tr></tbody></table></div></div><div class="row"><div class="col-md-12 bg-gray"><div class="row pad-lft pad-rgt" ><div class="timeline mar-btm pad-no" style="padding-bottom: 0px;"><div class="timeline-entry mar-no"> <div class="timeline-stat"> <div class="timeline-icon bg-purple"><i class="fa fa-warning fa-lg"></i> </div><div class="timeline-time"><b>01/01/2016 00:00:00</b></div></div><div class="timeline-label"> <p class="mar-no pad-btm"> <span class="badge badge-purple">Observações Antigas</span> por <a href="#" class="btn-link btn-md text-semibold"> Michel.Santos</a></p><div class="well well-xs mar-no"> CADASTRO ATUALIZADO EM :26/9/2012 - 11:34:47Identificação CGC/CNPJ:01.107.605/0001-49 Inscrição Estadual - CCE :10.280.338-2 Nome / Razão Social:RACIONAL EMBALAGENS LTDAEndereço Logradouro:RUA 6 QD 18 LT 03 Número:S/N Complemento:QD 18 LT 03 Bairro:POLO EMPRESARIAL GOIAS Município:APARECIDA DE GOIANIA UF:GO CEP:74985105 Telefone:6235182002Informações Complementares Atividade Econômica:1731100 - FABRICACAO DE EMBALAGENS DE PAPEL Regime de Apuração:NORMAL Situação Cadastral Vigente:HABILITADO - Data desta Situação Cadastral:23/03/2011Observações - Os dados acima são baseados em informações fornecidas pelo contribuinte, estando sujeitos a posterior confirmação pelo FISCO Data da Consulta: 26/09/2012 - 11:34:09</div></div></div></div></div></div></div><div class="row"><div class="col-xs-12 pad-btm bg-gray"><div class="col-sm-12 col-md-6 col-lg-4"><div class="form-group mar-no"><textarea id="demo-textarea-input" rows="6" class="form-control" placeholder="Escreva aqui a Descrição do Evento..."></textarea></div></div><div class="col-sm-12 col-md-6 col-lg-8"><div class="col-lg-6"><div class="pad-btm"><select class="selectpicker show-tick" data-placeholder="Escolha um evento..." title="Escolha um evento..." data-style="btn-default" data-live-search="true"><option value="1">Atendimento</option><option value="2">Visita Teste</option><option value="3">Negociação</option><option value="4">Venda Fechada</option><option value="5">Venda Perdida</option><option value="6">Outros</option><option value="7">Pedido</option><option value="8">Nota</option></select></div><div class="pad-btm"><select class="selectpicker pad-btm show-tick" data-placeholder="Escolha uma categoria..." title="Escolha uma categoria..." data-style="btn-default" data-live-search="true"><option value="1">Telefone</option><option value="2">E-mail</option><option value="3">Visita</option><option value="4">Online</option></select></div></div><div class="col-lg-6"><div class="col-md-12"><input name="ctl00$ContentPlaceHolder1$txtData" type="text" id="ctl00_ContentPlaceHolder1_txtData" style="width:100px;"><select name="ctl00$ContentPlaceHolder1$drpHora" id="ctl00_ContentPlaceHolder1_drpHora" class="campo" style="width:60px;"><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="6">6</option><option value="7">7</option><option value="8">8</option><option value="9">9</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option><option value="22">22</option><option value="23">23</option></select></div></div></div></div>',
			buttons: {
				danger: {
					label: "Cancelar",
					className: "btn btn-danger btn-labeled fa fa-times",
					callback: function() {
						$.niftyNoty({
							type: 'danger',
							icon : 'fa fa-times',
							message : '<strong>Registro cancelado</strong>',
							container : 'floating',
							timer : 3000
						});
					}
				},

				success: {
					label: "Inserir Atendimento no Histórico",
					className: "btn-success btn-labeled fa fa-check",
					callback: function() {
						$.niftyNoty({
							type: 'success',
							icon : 'fa fa-check',
							message : '<strong>Histórico atualizado!</strong>',
							container : 'floating',
							timer : 3000
						});
					}
				},
			}
		});
	});

	// BOOTBOX - ZOOM IN/OUT ANIMATION
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	//
	// Animate.css
	// http://daneden.github.io/animate.css/
	// =================================================================
	$('#demo-bootbox-zoom').on('click', function(){
		bootbox.confirm({
			message : "<h4 class='text-thin'>Zoom In/Out</h4><p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.</p>",
			buttons: {
				confirm: {
					label: "Save"
				}
			},
			callback : function(result) {
				//Callback function here
			},
			animateIn: 'zoomInDown',
			animateOut : 'zoomOutUp'
		});
	});



	// BOOTBOX - BOUNCE IN/OUT ANIMATION
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	//
	// Animate.css
	// http://daneden.github.io/animate.css/
	// =================================================================
	$('#demo-bootbox-bounce').on('click', function(){
		bootbox.confirm({
			message : "<h4 class='text-thin'>Bounce</h4><p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.</p>",
			buttons: {
				confirm: {
					label: "Save"
				}
			},
			callback : function(result) {
				//Callback function here
			},
			animateIn: 'bounceIn',
			animateOut : 'bounceOut'
		});
	});



	// BOOTBOX - RUBBERBAND & WOBBLE ANIMATION
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	//
	// Animate.css
	// http://daneden.github.io/animate.css/
	// =================================================================
	$('#demo-bootbox-ruberwobble').on('click', function(){
		bootbox.confirm({
			message : "<h4 class='text-thin'>RubberBand & Wobble</h4><p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.</p>",
			buttons: {
				confirm: {
					label: "Save"
				}
			},
			callback : function(result) {
				//Callback function here
			},
			animateIn: 'rubberBand',
			animateOut : 'wobble'
		});
	});



	// BOOTBOX - FLIP IN/OUT ANIMATION
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	//
	// Animate.css
	// http://daneden.github.io/animate.css/
	// =================================================================
	$('#demo-bootbox-flip').on('click', function(){
		bootbox.confirm({
			message : "<h4 class='text-thin'>Flip</h4><p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.</p>",
			buttons: {
				confirm: {
					label: "Save"
				}
			},
			callback : function(result) {
				//Callback function here
			},
			animateIn: 'flipInX',
			animateOut : 'flipOutX'
		});
	});



	// BOOTBOX - LIGHTSPEED IN/OUT ANIMATION
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	//
	// Animate.css
	// http://daneden.github.io/animate.css/
	// =================================================================
	$('#demo-bootbox-lightspeed').on('click', function(){
		bootbox.confirm({
			message : "<h4 class='text-thin'>Light Speed</h4><p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.</p>",
			buttons: {
				confirm: {
					label: "Save"
				}
			},
			callback : function(result) {
				//Callback function here
			},
			animateIn: 'lightSpeedIn',
			animateOut : 'lightSpeedOut'
		});
	});



	// BOOTBOX - SWING & HINGE IN/OUT ANIMATION
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	//
	// Animate.css
	// http://daneden.github.io/animate.css/
	// =================================================================
	$('#demo-bootbox-swing').on('click', function(){
		bootbox.confirm({
			message : "<h4 class='text-thin'>Swing & Hinge</h4><p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.</p>",
			buttons: {
				confirm: {
					label: "Save"
				}
			},
			callback : function(result) {
				//Callback function here
			},
			animateIn: 'swing',
			animateOut : 'hinge'
		});
	});


 })
