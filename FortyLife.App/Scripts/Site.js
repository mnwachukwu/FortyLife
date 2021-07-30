$(document).ready(function () {
    $("#Set_Code").change(function () {
        var $this = $(this);
        var url = "/Spoilers?setCode=" + $this.val();
        window.location.href = url;
    });

    $("#SortId").change(function () {
        var $this = $(this);
        var url = "/Spoilers?setCode=" + $("#Set_Code").val() + "&sortId=" + $this.val();
        window.location.href = url;
    });

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

    $(".printings-button").click(function () {
        $("#lotus-modal").modal();
    });
});