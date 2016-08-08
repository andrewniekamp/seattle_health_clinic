using Nancy;
using System;
using System.Collections.Generic;

namespace SeattleHealthClinic
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/landing"] = _ => {
        return View["landing.cshtml"];
      };

      Post["/add/patients"] = _ =>{
        Patients newPatient = new Patient(Request.Form["patient-name"], Request.Form["patient-address"]);
        newPatient.Save();
        return View["success.cshtml"];
      };
    }
  }
}
