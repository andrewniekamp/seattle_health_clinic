$(document).ready(function() {
  $("#list-one").click(function(event) {
    $("#panel-one").slideDown();
    $("#panel-two").slideUp();
  });
  $("#list-two").click(function(event) {
    $("#panel-one").slideUp();
    $("#panel-two").slideDown();
  });
});
