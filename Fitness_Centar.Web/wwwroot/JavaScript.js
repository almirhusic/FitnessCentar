$(function () {
    if ($(".message").is(":visible")) {
        $(".message").delay(2500).fadeOut('slow');
    }
});

$(function () {
    $('.date-picker')
        .datepicker({
            format: 'dd/mm/yyyy'
        });
});

$(document).ready(function () {
    $('#example').DataTable();
});