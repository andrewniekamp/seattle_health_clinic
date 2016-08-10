$(document).ready(function(){

  $(".patient-edit-button").click(function(){
    // $(this).closest(".row").next(".patient-update-form").toggle();
    $(this).parents(".row").next(".form-row").slideToggle();

  });
});
