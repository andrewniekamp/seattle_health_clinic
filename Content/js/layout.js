$(document).ready(function() {
  $("#list-one").click(function(event) {
    $("#panel-one").slideDown();
    $("#panel-two").slideUp();
    $("#panel-three").slideUp();
  });
  $("#list-two").click(function(event) {
    $("#panel-one").slideUp();
    $("#panel-two").slideDown();
    $("#panel-three").slideUp();
  });
  $("#list-three").click(function(event) {
    $("#panel-one").slideUp();
    $("#panel-two").slideUp();
    $("#panel-three").slideDown();
  });
});
