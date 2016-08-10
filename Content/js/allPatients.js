$(document).ready(function(){

  $(".patient-edit-button").click(function(){
    $(this).parents(".row").next(".form-row").slideToggle("fast");
  });
});
