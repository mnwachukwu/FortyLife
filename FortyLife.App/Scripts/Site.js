$(function () {
    $("#flip-button").click(function () {
        var cardImage = $("#card-image");
        var whichFace = $("#which-face");

        if (cardImage.hasClass("is-flipped")) {
            cardImage.removeClass("is-flipped");
            whichFace.text("Front");
        } else {
            cardImage.addClass("is-flipped");
            whichFace.text("Back");
        }
    });
});

$(document).ready(function () {
    $(".printings-button").click(function () {
        $("#lotus-modal").modal();
    });
});