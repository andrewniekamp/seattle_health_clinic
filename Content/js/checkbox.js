$(document).ready(function(){
  var result =[];
  $("input:checkbox[name=symptom]").change(function() {
    console.log("I am being checked");
    console.log($(this).is(":checked"));

    if($(this).is(":checked"))
    {
      result.push($(this).val());
    }else{
      if(result.indexOf($(this).val())!==-1){
        result.splice(result.indexOf($(this).val()),1);
      }
    }
    console.log(result);
    $("#hiddenInput").attr("value",result.toString());
  })


});
