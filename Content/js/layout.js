$(document).ready(function() {
  $(".list-group-item").click(function() {
    var index = $(".list-group-item").index(this);
    $("a").each(function() {
      if($(".list-group-item").index(this) === index) {
        $(this).addClass("active");
      } else {
        $(this).removeClass("active");
      }
    });
    $(this).addClass("active");
    $(".panel-body").each(function() {
      if($(".panel-body").index(this) === index) {
        $(this).slideDown();
      } else {
        $(this).slideUp();
      }
    });
  });
});
