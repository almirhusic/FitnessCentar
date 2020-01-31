"use strict";

var $connection_1 = new signalR.HubConnectionBuilder().withUrl("/ModulClan/Helper/FollowHub").build();

$(".follow").prop("disabled", true);

$connection_1.start().then(function () {
    $(".follow").prop("disabled", false);
}).catch(function (err) {
    return console.error(err.toString());
});

$connection_1.on("ReceiveMessage", function (pratitelj, zapraceni, action) {
    if (action === "follow") {
        var userId = $('.follow-' + zapraceni).attr('data-pratiteljid');
        if (userId == pratitelj) {
            $('.follow-' + zapraceni).text('Following');
            $('.follow-' + zapraceni).addClass('following following-' + zapraceni).removeClass('follow follow-' + zapraceni);
        }
    } else {
        var userId = $('.following-' + zapraceni).attr('data-pratiteljid');
        if (userId == pratitelj) {
            $('.following-' + zapraceni).text('Follow');
            $('.following-' + zapraceni).addClass('follow follow-' + zapraceni).removeClass('following following-' + zapraceni);
        }
    }
});

$(".btnFollow").bind("click", function (event) {
    var pratitelj = $(this).attr("data-pratiteljid");
    var zapraceni = $(this).attr("data-zapraceniid");

    $connection_1.invoke("Follow", pratitelj, zapraceni).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});