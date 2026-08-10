jQuery(function ($) {

    $('ul#nav-menu > li > a').click(function () {
        $(this).next().slideToggle();
    });
});

function selecionarLinha(item) {
    var obj = window.event.srcElement;
    if (obj.tagName == "INPUT" && obj.type == "text") {
        obj = obj.parentElement.parentElement;
        oldRowColor = "RED";
    }
}