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

$(function () {
    $("#show-foil").click(function () {
        if (this.checked) {
            $("#foil-price").removeClass("d-none");
            $("#price").addClass("d-none");
            $("#foil-text").addClass("rainbow-text");
            $("#foil-text").addClass("color-text-flow");
            $("#foil-text").removeClass("text-muted");
        } else {
            $("#foil-price").addClass("d-none");
            $("#price").removeClass("d-none");
            $("#foil-text").removeClass("rainbow-text");
            $("#foil-text").removeClass("color-text-flow");
            $("#foil-text").addClass("text-muted");
        }
    });
});