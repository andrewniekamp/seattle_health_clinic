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
        Patient newPatient = new Patient(Request.Form["patient-name"], Request.Form["patient-address"]);
        newPatient.Save();
        return View["success.cshtml"];
      };
      Get["/add/visit"] = _ =>{
        List<Patient> allPatients = Patient.GetAll();
        //List<Physician> allPhysicians = Physician.GetAll();
        List<Condition> allConditions = Condition.GetAll();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        model.Add("patients",allPatients);
        //model.Add("physicians", allPhysicians);
        model.Add("conditions",allConditions);
        return View["add_visit.cshtml",model];
      };
      Post["/add/visit"] = _ =>{
        //Temporary DoctorId used in constructor below
        ConditionEval newVisit = new ConditionEval(Request.Form["visit-patient-id"],1, Request.Form["visit-condition-id"],Request.Form["visit-date"]);
        newVisit.Save();
        return View["success.cshtml"];
      };
    }
  }
}
