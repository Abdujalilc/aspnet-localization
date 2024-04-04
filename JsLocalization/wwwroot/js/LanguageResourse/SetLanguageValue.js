$(document).ready(function () {
    var lang = $("#languageValue").val();
    $(".lang").each(function (index, element) {
        $(this).text(arrLang[lang][$(this).attr("key")]);
    });
});