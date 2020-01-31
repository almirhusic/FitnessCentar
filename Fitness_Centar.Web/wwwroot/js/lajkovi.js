"use strict";

/* ============================= LIKE ================================ */
var $connection = new signalR.HubConnectionBuilder().withUrl("/ModulClan/Helper/LajkHub").build();

$(".likeButton").prop("disabled", true);

$connection.start().then(function () {
    $(".likeButton").prop("disabled", false);
}).catch(function (err) {
    return console.error(err.toString());
});

$connection.on("ReceiveMessage", function (brLajkova, postId, userId, lajkovi) {
    var x = postId + 1594;
    var s = "#post-" + x;
    var currentUserId = $(s).attr("data-userid");
    var currentPostId = $(s).attr("data-postid");
    if (currentUserId == userId) {
        if ($(s).hasClass("liked")) {
            $(s).removeClass("liked");
        } else {
            $(s).addClass("liked");
        }
    }
    $(s + "-brLajkova").text(brLajkova);

    if (currentPostId == postId) {
        $(".l-" + x).html(lajkovi);
    }
});

$connection.on("ReceiveMsg", function () {
    var c = $('.notification-custom').html();
    if ($('.notification-custom').is(':empty')) {
        $('.notification-custom').html('1');
    } else {
        $('.notification-custom').html(++c);
    }
});

$(".likeButton").bind("click", function (event) {
    var userId = $(this).attr("data-userid");
    var postId = $(this).attr("data-postid");

    $connection.invoke("Lajkaj", userId, postId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


$("div.broj-lajkova").hover(function () {
    if ($(this).siblings("span.lajkClanovi").html() != 0) {
        $(this).siblings("span.lajkClanovi").addClass("show");
    }
}, function () {
    $(this).siblings("span.lajkClanovi").removeClass("show");
});