jQuery(function ($) {

    var caracteres = 100;
    $("#counter").html("Você ainda tem <strong>" + caracteres + "</strong> caracteres.");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ObservacaoBreveTextBox").keyup(function () {
        if ($(this).val().length > caracteres) {
            $(this).val($(this).val().substr(0, caracteres));
        }
        var quedan = caracteres - $(this).val().length;
        $("#counter").html("Você ainda tem <strong>" + quedan + "</strong> caracteres.");
        if (quedan <= 10) {
            $("#counter").css("color", "red");
        }
        else {
            $("#counter").css("color", "black");
        }
    });
});