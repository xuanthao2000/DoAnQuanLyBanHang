////$(function () {

////    $('#AlertBox').hide();

////    setTimeout(function () {
////        $('#AlertBox').fadeIn('slow');
////    }, 1000);

////});
$(function () {
    $('#slide-menu').metisMenu();
    $('#AlertBox').RemoveClass('hide');
    $('#AlertBox').delay(1000).slideUp(500);
});