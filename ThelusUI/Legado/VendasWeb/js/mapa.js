var map;
var idInfoBoxAberto;
var infoBox = [];
var markers = [];
var markerSelecionado;
var idmarkerSelecionado;

var directionsDisplay; // Instanciaremos ele mais tarde, que será o nosso google.maps.DirectionsRenderer
var directionsService = new google.maps.DirectionsService();



function initialize() {

    directionsDisplay = new google.maps.DirectionsRenderer(); // Instanciando...

	var latlng = new google.maps.LatLng(-18.8800397, -47.05878999999999);
	
    var options = {
        zoom: 5,
		center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("mapa"), options);


    directionsDisplay.setMap(map); // Relacionamos o directionsDisplay com o mapa desejado
}


function abrirInfoBox(id, marker) {
	if (typeof(idInfoBoxAberto) == 'number' && typeof(infoBox[idInfoBoxAberto]) == 'object') {
		infoBox[idInfoBoxAberto].close();
	}

	infoBox[id].open(map, marker);
	idInfoBoxAberto = id;

	
}


function zoomPino(id, marker) {

   
    if (typeof (idInfoBoxAberto) == 'number' && typeof (infoBox[idInfoBoxAberto]) == 'object') {
        infoBox[idInfoBoxAberto].close();
    }

    infoBox[id].open(map, marker);
    idInfoBoxAberto = id;

    map.setZoom(15);//Nivel do Zoom

    

}




function carregarPontos() {

    $.getJSON('../JsonMaps.ashx', function (pontos) {
		
		var latlngbounds = new google.maps.LatLngBounds();
		
		$.each(pontos, function(index, ponto) {
			/*
			var marker = new google.maps.Marker({
				position: new google.maps.LatLng(ponto.Latitude, ponto.Longitude),
				title: "Meu ponto personalizado! :-D",
				icon: '../img/marcador.png'
			});
			*/

		    var marker = new google.maps.Marker({
		        position: new google.maps.LatLng(ponto.Latitude, ponto.Longitude),
		        draggable: true,
		        animation: google.maps.Animation.DROP,
		        title: ponto.Titulo,
		        icon: ponto.Icon, //Imagen PINO - Se n quiser usar icones descomente
                map: map //Comente se for fazer agrupamento

		    });


		    
            //Verifica se eh um Pino selecionado (verificando se contem a palavra chave)
		    if (marker.icon.indexOf("../images/pino_preto.png") == 0)
		    {
		        //Centraliza o Mapa nesse Pino
		        map.setCenter(new google.maps.LatLng(ponto.Latitude, ponto.Longitude));
		        map.setZoom(15);


		    }


			var myOptions = {
				content: "<p>" + ponto.Descricao + "</p>",
				pixelOffset: new google.maps.Size(-150, 0)
        	};

			infoBox[ponto.Id] = new InfoBox(myOptions);
			infoBox[ponto.Id].marker = marker;
			
			infoBox[ponto.Id].listener = google.maps.event.addListener(marker, 'click', function (e) {
			    abrirInfoBox(ponto.Id, marker);
			});

			infoBox[ponto.Id].listener = google.maps.event.addListener(marker, 'dblclick', function (e) {
			    zoomPino(ponto.Id, marker);
			});


			
			
			//markers.push(marker); //Para fazer agrupamento

		    //latlngbounds.extend(marker.position); //Para fazer agrupamento
			
			

		});

		
		        //var markerCluster = new MarkerClusterer(map, markers); //Para fazer agrupamento

        //map.fitBounds(latlngbounds); //Para fazer agrupamento


		

		
	});


    
    
    

}





function carregaRota() {


    var enderecoPartida;
    var enderecoChegada;
    var request;
    var waypointAux;

    //Chama Json que retorna Coordenada dos endereços a serem mapeados
    $.getJSON('../JsonMapsRota.ashx', function (pontos) {


        var latlngbounds = new google.maps.LatLngBounds();
		
        $.each(pontos, function(index, ponto) {

            enderecoPartida = new google.maps.LatLng(ponto.PartidaLatitude, ponto.PartidaLongitude); //Endereco de Partida
            enderecoChegada = new google.maps.LatLng(ponto.DestinoLatitude, ponto.DestinoLongitude); //Endereco de Destino

            wayPointArray = new Array();// Array para Alimentar d+ pontos que existirem entre a Partida de Destino
            
            //Percorre analisando se existem enderecos no Caminho
            for (var i = 0; i < ponto.PontosNoCaminho.length; i++) {                
                //se Existir adiciona ao List
                wayPointArray.push({ location: ({ lat: ponto.PontosNoCaminho[i].Latitude, lng: ponto.PontosNoCaminho[i].Longitude }), stopover: true });
                //wayPointArray.push({ location: ({ lat: ponto.PontosNoCaminho[i].Latitude, lng: ponto.PontosNoCaminho[i].Longitude }), stopover: false });//Caso nao queira que aparece a Letra
                
            }

            



            //Validacao para saber se entre a partida e o destino tem algum outro ponto, apenas para adicionar a funcao waypoints
            if (ponto.TotalAtivo == 2)
            {
                
                request = { // Novo objeto google.maps.DirectionsRequest, contendo:
                    origin: enderecoPartida, // origem
                    destination: enderecoChegada, // destino
                    travelMode: google.maps.TravelMode.DRIVING // meio de transporte, nesse caso, de carro
                };
            }
            else {
                
                request = { // Novo objeto google.maps.DirectionsRequest, contendo:
                    origin: enderecoPartida, // origem
                    destination: enderecoChegada, // destino
                    waypoints:wayPointArray,//[{ location: ({ lat: -25.460613, lng: -49.288702 }) }], //List de Outros pontos entre a Origem e Destino
                    travelMode: google.maps.TravelMode.DRIVING // meio de transporte, nesse caso, de carro
                };

            }
    
                

                directionsService.route(request, function (result, status) {
                    if (status == google.maps.DirectionsStatus.OK) { // Se deu tudo certo
                        directionsDisplay.setDirections(result); // Renderizamos no mapa o resultado

                        directionsDisplay.setPanel(document.getElementById("trajeto-texto"));
                    }
                });

        });
    });


}





initialize();


//carregarPontos();

