"use strict";

/* =============================== KOMENTARI ============================== */

var $conn = new signalR.HubConnectionBuilder().withUrl("/ModulClan/Helper/KomentarHub").build();

$(".commentButton").prop("disabled", true);

$conn.start().then(function () {
    $(".commentButton").prop("disabled", false);
}).catch(function (err) {
    return console.error(err.toString());
});

$conn.on("ReceiveMessage", function (userId, postId, brKomentara, obj) {
    var j = postId + 498;
    var pId = postId + 148;
    var d = formatDate(obj.datumObjaveKomentara);
    var x = '<div id="komentar-' + obj.komentariObjavaClanovaId + '" style="display: block;"><div class="komentar"><div class="user-info"><span class="user">' + obj.clan.ime + " " + obj.clan.prezime + '</span> <span class="publish-date">' + d + '</span><a href="#' + postId + '" data-kid=' + obj.komentariObjavaClanovaId + ' data-uid= ' + userId + ' data-pid= ' + pId + ' class="delete-comment pull-right margin-l-15"><i class="fa fa-close"></i></a></div><div class="comment-text"><span>' + obj.sadrzajKomentara + '</span></div></div></div>'
    var y = '<div id="komentar-' + obj.komentariObjavaClanovaId + '" style="display: block;"><div class="komentar"><div class="user-info"><span class="user">' + obj.clan.ime + " " + obj.clan.prezime + '</span> <span class="publish-date">' + d + '</span></div><div class="comment-text"><span>' + obj.sadrzajKomentara + '</span></div></div></div>'
    var currentUserId = $('#post-id-' + pId).attr("data-uid");

    $('[name="komentar-' + j + '"]').val("");
    $('.brKomentara-' + pId).html('(' + brKomentara + ')');

    if (parseInt(userId) == parseInt(currentUserId)) {
        $('#komentari-section-' + pId).append(x);
        $('#komentari-section-' + pId).slideToggle();
    } else {
        $('#komentari-section-' + pId).append(y);
    }
});

$conn.on("ReceiveMsg", function () {
    var c = $('.notification-custom').html();
    if ($('.notification-custom').is(':empty')) {
        $('.notification-custom').html('1');
    } else {
        $('.notification-custom').html(++c);
    }
});

$(".commentButton").bind("click", function (event) {
    var userId = $(this).attr("data-userid");
    var postId = $(this).attr("data-postid");
    var j = parseInt(postId) + 498;
    var sadrzaj = $('input[name="komentar-' + j + '"]').val();

    if (sadrzaj == "") {
        sadrzaj = null;
    }

    if (sadrzaj != null) {
        $conn.invoke("Komentiraj", userId, postId, sadrzaj).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }
});

$('.showComments').bind("click", function (event) {
    var x = $(this).attr("data-id");
    $("#komentari-section-" + x).slideToggle();
});

/* ============================= Brisanje komentara ================================= */
$(document).on("click", ".delete-comment", function () {
    var kId = $(this).attr("data-kid");
    var uId = $(this).attr("data-uid");
    var pId = $(this).attr("data-pid");

    $conn.invoke("ObrisiKomentar", kId, uId, pId).catch(function (err) {
        return console.error(err.toString());
    });
});

$conn.on("ReceiveMessageObrisi", function (kId, uId, pId, brKomentara) {
    $("#komentar-" + kId).remove();
    $('.brKomentara-' + pId).html('(' + brKomentara + ')');
});

function formatDate(_currentDate) {
    var currentDate = new Date(_currentDate);
    var year = currentDate.getFullYear();
    var month = currentDate.getMonth();
    var day = currentDate.getDate();
    var hours = currentDate.getHours();
    var minutes = currentDate.getMinutes();

    if (parseInt(month) < 10) {
        month = "0" + month;
    }
    if (parseInt(day) < 10) {
        day = "0" + day;
    }
    if (parseInt(hours) < 10) {
        hours = "0" + hours;
    }
    if (parseInt(minutes) < 10) {
        minutes = "0" + minutes;
    }

    return (day + "." + month + "." + year + " " + hours + ":" + minutes);
}
