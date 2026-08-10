
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
			title: "Agendamento de Visita",
			message:'<div class="row"><div class="col-xs-12 bg-gray"><div class="row pad-all"><select data-placeholder=“Selecione“ id="demo-chosen-select"><option value=“Entidade“>Nome da Entidade 1</option><option value=“Entidade“>Nome da Entidade 2</option><option value=“Entidade“>Nome da Entidade 3</option><option value=“Entidade“>Nome da Entidade 4</option></select></div><div class="row pad-lft pad-rgt" ><table class="table table-condensed"><tbody><tr class="bg-trans-dark"><td>Entidade</td><td class="text-bold">BALCONY SUL COMERCIO DE ESTRUTURAS METALICAS LTDA</td></tr><tr class="bg-trans-dark"><td>CNPJ</td><td class="text-bold"> 06261459000125 </td></tr><tr class="bg-trans-dark"><td>Endereço</td><td class="text-bold">Endereço: EINSTEN Número: 727 Bairro: VILA GUARANI <br/> CEP: 83408-040 UF: PR Cidade: COLOMBO Complemento: BLOCO 3</td></tr><tr><td>Nome do Contato</td><td class="text-bold">José da Silva</td></tr><tr><td>Telefone do Contato</td><td class="text-bold">413606-4512</td></tr><tr><td>Email do Contato</td><td class="text-bold">comercial@balconysul.com.br</td></tr></tbody></table></div></div></div><div class="row"><div class="col-xs-12 bg-gray"><div class="row"><div class="col-sm-4"><div class="form-group"><label class="control-label">Stretch</label><input type="text" placeholder="999.999kg" class="form-control"></div></div><div class="col-sm-4"><div class="form-group"><label class="control-label">Fita PP</label><input type="text" placeholder="999.999kg" class="form-control"></div></div><div class="col-sm-4"><div class="form-group"><label class="control-label">Fita Impressa</label><input type="text" placeholder="999.999kg" class="form-control"></div></div></div></div></div><div class="form-group pad-top"><label class="col-md-3 control-label">Condição de Visita</label><div class="col-md-9"><div class="radio"><label class="form-radio form-normal active form-text"><input type="radio" checked="" name="def-w-label"> Cliente Novo</label><label class="form-radio form-normal form-text"><input type="radio" name="def-w-label"> Recuperação de Inativo</label><label class="form-radio form-normal form-text"><input type="radio" name="def-w-label"> Manutenção</label></div></div></div><div class="row"><div class="col-sm-6 bg-gray mar-btm"><p class="text-thin mar-btm">Início</p><div id="demo-dp-component"><div class="input-group date"><input type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span></div><small class="text-muted">Data</small></div><div class="input-group date"><input id="demo-tp-com" type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span></div><small class="text-muted">Horário</small></div><div class="col-sm-6 bg-gray bord-lft mar-btm"><p class="text-thin mar-btm">Final</p><div id="demo-dp-component"><div class="input-group date"><input type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span></div><small class="text-muted">Data</small></div><div class="input-group date"><input id="demo-tp-com" type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span></div><small class="text-muted">Horário</small></div></div><div class="row"><div class="col-md-6"><div class="form-group"><select class="selectpicker"><option>Lembre-me 10 minutos antes</option><option>Lembre-me 1 hora antes</option><option>Lembre-me 1 dia antes</option><option>Lembre-me 1 semana antes</option></select></div></div><div class="col-md-6"><div class="form-group mar-no"><textarea id="demo-textarea-input" rows="2" class="form-control" placeholder="Coloque suas observações aqui.."></textarea></div></div></div>',
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

	// BOOTBOX - MODAL DE CALENDÁRIO
	// =================================================================
	// Require Bootbox
	// http://bootboxjs.com/
	// =================================================================
	$('#demo-bootbox-calendario').on('click', function(){
		bootbox.dialog({
			title: "Agendamento de Visita",
			message:'<div class="row"><div class="col-xs-12 bg-gray"><div class="row pad-all"><select data-placeholder=“Selecione“ id="demo-chosen-select"><option value=“Entidade“>Nome da Entidade 1</option><option value=“Entidade“>Nome da Entidade 2</option><option value=“Entidade“>Nome da Entidade 3</option><option value=“Entidade“>Nome da Entidade 4</option></select></div><div class="row pad-lft pad-rgt" ><table class="table table-condensed"><tbody><tr class="bg-trans-dark"><td class="text-right">Entidade:</td><td class="text-bold">BALCONY SUL COMERCIO DE ESTRUTURAS METALICAS LTDA</td></tr><tr class="bg-trans-dark"><td class="text-right">CNPJ:</td><td class="text-bold"> 06261459000125 </td></tr><tr class="bg-trans-dark"><td class="text-right">Endereço:</td><td class="text-bold">Endereço: EINSTEN Número: 727 Bairro: VILA GUARANI <br/> CEP: 83408-040 UF: PR Cidade: COLOMBO Complemento: BLOCO 3</td></tr><tr><td class="text-right">Nome do Contato:</td><td class="text-bold">José da Silva</td></tr><tr><td class="text-right">Telefone do Contato:</td><td class="text-bold">413606-4512</td></tr><tr><td class="text-right">Email do Contato:</td><td class="text-bold">comercial@balconysul.com.br</td></tr></tbody></table></div></div></div><div class="row"><div class="col-xs-12 bg-gray"><div class="row"><div class="col-sm-4"><div class="form-group"><label class="control-label">Stretch</label><input type="text" placeholder="999.999kg" class="form-control"></div></div><div class="col-sm-4"><div class="form-group"><label class="control-label">Fita PP</label><input type="text" placeholder="999.999kg" class="form-control"></div></div><div class="col-sm-4"><div class="form-group"><label class="control-label">Fita Impressa</label><input type="text" placeholder="999.999kg" class="form-control"></div></div></div></div></div><div class="row bg-gray pad-all"><div class="col-xs-12 bg-trans-dark"><div class="form-group pad-top"><label class="col-md-3 control-label">Condição de Visita</label><div class="col-md-9"><div class="radio"><label class="form-radio form-normal active form-text"><input type="radio" checked="" name="def-w-label"> Cliente Novo</label><label class="form-radio form-normal form-text"><input type="radio" name="def-w-label"> Recuperação de Inativo</label><label class="form-radio form-normal form-text"><input type="radio" name="def-w-label"> Manutenção</label></div></div></div></div></div><div class="row"><div class="col-sm-6 bg-gray mar-btm"><p class="text-thin mar-btm">Início</p><div id="demo-dp-component"><div class="input-group date"><input type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span></div><small class="text-muted">Data</small></div><div class="input-group date"><input id="demo-tp-com" type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span></div><small class="text-muted">Horário</small></div><div class="col-sm-6 bg-gray bord-lft mar-btm"><p class="text-thin mar-btm">Final</p><div id="demo-dp-component"><div class="input-group date"><input type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-calendar fa-lg"></i></span></div><small class="text-muted">Data</small></div><div class="input-group date"><input id="demo-tp-com" type="text" class="form-control"><span class="input-group-addon"><i class="fa fa-clock-o fa-lg"></i></span></div><small class="text-muted">Horário</small></div></div><div class="row pad-no"><div class="col-md-6 mar-no"><div class="form-group"><select class="selectpicker"><option>Lembre-me 10 minutos antes</option><option>Lembre-me 1 hora antes</option><option>Lembre-me 1 dia antes</option><option>Lembre-me 1 semana antes</option></select></div></div><div class="col-md-6"><div class="form-group mar-no"><textarea id="demo-textarea-input" rows="2" class="form-control" placeholder="Coloque suas observações aqui.."></textarea></div></div></div>',
			buttons: {
				danger: {
					label: "Excluir",
					className: "btn btn-danger btn-labeled fa fa-times",
					callback: function() {
						$.niftyNoty({
							type: 'danger',
							icon : 'fa fa-times',
							message : '<strong>Agendamento Excluído</strong>',
							container : 'floating',
							timer : 3000
						});
					}
				},

				success: {
					label: "Salvar Agendamento",
					className: "btn-success btn-labeled fa fa-check",
					callback: function() {
						$.niftyNoty({
							type: 'success',
							icon : 'fa fa-check',
							message : '<strong>Agendamento salvo!</strong>',
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
