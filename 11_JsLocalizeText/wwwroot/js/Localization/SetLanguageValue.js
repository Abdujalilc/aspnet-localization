$(document).ready(function () {
    var culture = $("#cultureName").val();
    $(".lang").each(function (index, element) {
        $(this).text(resourceArray[culture][$(this).attr("key")]);
    });
});