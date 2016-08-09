using Nancy;
using System;
using System.Collections.Generic;
using newsApi;
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

      // home view, must pass through an employee object in each view!
      Get["/login_status"] = _ => {
        if (Employee.VerifyLogin(Request.Query["login-email"], Request.Query["login-password"]))
        {
          Dictionary<string,object> model = new Dictionary<string,object>();
          Employee currentEmployee = Employee.FindEmail(Request.Query["login-email"]);
          model.Add("currentEmployee", currentEmployee);
          return View["login_success.cshtml", model];
        }
        else
        {
          return View["login_invalid.cshtml"];
        }
      };

      //home view, must pass through an employee object in each view!
      Get["/home_view/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        return View["index.cshtml", model];
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
        string symptomsFromForm = Request.Form["symptom-array"].ToString();
        string[] symptoms = symptomsFromForm.Split(',');
        foreach (var symptomString in symptoms)
        {
          int symptomId =int.Parse(symptomString);
          Diagnosis newDiagnosis = new Diagnosis(Request.Form["diagnosis-patient-id"],1,symptomId,Request.Form["diagnosis-date"]);
          newDiagnosis.Save();
        }
        return View["success.cshtml"];
      };

      Get["/view/all/patients"] =_=>{
        List<Patient> allPatients = Patient.GetAll();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        model.Add("patients",allPatients);
        return View["view_all_patients.cshtml",model];
      };
      Get["/view/patient/visit/{id}"] = parameters =>{
        Patient patientToUpdate = Patient.Find(parameters.id);
        List<ConditionEval> patientEvals = patientToUpdate.GetAllEval();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        model.Add("evals", patientEvals);
        model.Add("patient", patientToUpdate);

        return View["patients_evals.cshtml",model];
      };
      Patch["/view/patient/edit/{id}"] = parameters =>{
        Patient patientToUpdate = Patient.Find(parameters.id);
        patientToUpdate.Update(Request.Form["edit-new-name"], Request.Form["edit-new-address"]);
        return View["sucess.cshtml"];
      };
      Delete["/view/patient/delete/{id}"] = parameters =>{
        Patient patientToDelete = Patient.Find(parameters.id);
        patientToDelete.Delete();
        return View["sucess.cshtml"];
      };

      Get["/view/all/appointments"] =_=>{
        List<PatientScheduling> allPatientSchedulings = PatientScheduling.GetAll();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        model.Add("patientScheduling", allPatientSchedulings);
        return View["sucess.cshtml"];
      };

      Get["/view/all/visits"] =_=>{
        List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        model.Add("conditionEval", allConditionEvals);
        return View["sucess.cshtml"];
      };


      Get["/add/appointment"] = _ =>{
        List<Patient> allPatients = Patient.GetAll();
        //List<Physician> allPhysicians = Physician.GetAll();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        model.Add("patients",allPatients);
        //model.Add("physicians", allPhysicians);
        return View["add_appointment.cshtml",model];
      };

      Post["/add/appointment"] = _ =>{
        //Attempting to integrate checkbox or radio dial for selecting diagnosis
        PatientScheduling newScheduling = new PatientScheduling(Request.Form["appointment-patient-id"],1, Request.Form["patient-appointment-note"],Request.Form["patient-appointment-date"]);
        newScheduling.Save();
        return View["success.cshtml"];
      };

      Get["/news"]= _ =>{
        string[] listSearch= new string[]{"diabetes", "heart health", "health fitness", "healthcare", "pediatrics"};
        Random myRandom = new Random();
        int randomNum = myRandom.Next(0,5);
       List<NewsResult> allResult =  HealthNews.GetNews(listSearch[randomNum]);
      //  foreach(var s in allResult)
      //  {
      //    Console.WriteLine(s.Headline);
      //    Console.WriteLine(s.ConvertHeadline());
       //
      //  }
       return View["news.cshtml",allResult];

      };
    }
  }
}
