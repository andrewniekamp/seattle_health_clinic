using Nancy;
using System;
using System.Collections.Generic;

namespace SeattleHealthClinic
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      //landing page
      Get["/"] = _ => {
        return View["landing.cshtml"];
      };

      Get["/login_status"] = _ => {
        
      };

      //home view, must pass through an employee object in each view!
      Get["/home_view/{id}"] = _ => {
        return View["index.cshtml"];
      };

      Get["/add/patients"] = _ =>{
        return View["add_new_patient.cshtml"];
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

      Get["/add/symptom"] = _ =>{

        return View["add_new_symptom.cshtml"];
      };

      Post["/add/symptom"] = _ =>{
        //Temporary DoctorId used in constructor below
        Symptom newSymptom = new Symptom(Request.Form["symptom-name"], Request.Form["symptom-classification"],Request.Form["visit-contagious"]);
        newSymptom.Save();
        return View["success.cshtml"];
      };

      Get["/add/diagnosis"] = _ =>{
        List<Patient> allPatients = Patient.GetAll();
        //List<Physician> allPhysicians = Physician.GetAll();
        List<Symptom> allSymptoms = Symptom.GetAll();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        model.Add("patients",allPatients);
        //model.Add("physicians", allPhysicians);
        model.Add("symptoms",allSymptoms);
        return View["add_diagnosis.cshtml",model];
      };

      Post["/add/diagnosis"] = _ =>{
        //Attempting to integrate checkbox or radio dial for selecting diagnosis
        Diagnosis newDiagnosis = new Diagnosis(Request.Form["diagnosis-patient-id"],1, Request.Form["symptom-id"],Request.Form["diagnosis-date"]);
        newDiagnosis.Save();
        return View["success.cshtml"];
      };


    }
  }
}
