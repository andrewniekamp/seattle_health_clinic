$(document).ready(function() {
  //collapsable panel
  $(".list-group-item, .panel-heading").click(function() {
    var index = $(".list-group-item, .panel-heading").index(this);
    var numItems = $(".list-group-item").length;
    if (index >= numItems)
    {
      index -= numItems;
    }
    $("a").each(function() {
      if($(".list-group-item").index(this) === index) {
        $(this).addClass("active");
      } else {
        $(this).removeClass("active");
      }
    });
    $(".panel-body").each(function() {
      if($(".panel-body").index(this) === index) {
        $(this).slideDown();
      } else {
        $(this).slideUp();
      }
    });
    $(".panel").each(function() {
      if($(".panel").index(this) === index) {
        $(this).addClass("panel-primary");
      } else {
        $(this).removeClass("panel-primary");
      }
    });
  });

  //table in personnel_current.cshtml
  $("tr:odd").each(function() {
    $(this).addClass("active");
  });
});
