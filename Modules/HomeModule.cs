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
        return View["index.cshtml"];
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

      Get["/home_view/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        return View["home_view.cshtml", model];
      };

      Get["/profile/{id}/personal"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        List<License> currentLicenses = currentEmployee.GetLicenses();
        model.Add("currentLicenses", currentLicenses);
        return View["profile_personal.cshtml", model];
      };

      Get["/profile/{id}/payroll"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        Payroll currentPayroll = Payroll.Find(parameters.id);
        model.Add("currentPayroll", currentPayroll);
        List<License> currentLicenses = currentEmployee.GetLicenses();
        model.Add("currentLicenses", currentLicenses);
        return View["profile_payroll.cshtml", model];
      };

      Get["/profile/{id}/scheduling"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        return View["profile_scheduling.cshtml", model];
      };

      Get["/profile/{id}/licensure"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        Payroll currentPayroll = Payroll.Find(parameters.id);
        model.Add("currentPayroll", currentPayroll);
        List<License> currentLicenses = currentEmployee.GetLicenses();
        model.Add("currentLicenses", currentLicenses);
        return View["profile_licensure.cshtml", model];
      };

      Get["/personnel/{id}/current"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        List<Employee> allEmployees = Employee.GetAll("employees");
        model.Add("allEmployees", allEmployees);
        return View["personnel_current.cshtml", model];
      };

      Get["/personnel/{id}/current/{employeeId}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        Employee viewEmployee = Employee.Find(parameters.employeeId);
        model.Add("viewEmployee", viewEmployee);
        List<License> viewLicenses = viewEmployee.GetLicenses();
        model.Add("viewLicenses", viewLicenses);
        return View["personnel_view_employee.cshtml", model];
      };

      Get["/personnel/{id}/records"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        // insert lines here
        return View["personnel_records.cshtml", model];
      };



      //finished routes go above, unfinished routes go below

      // this is how a minimum route should look like:
      // Get["/(menu name)/{id}/(sub-menu name)"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string,object>();
      //   Employee currentEmployee = Employee.Find(parameters.id);
      //   model.Add("currentEmployee", currentEmployee);
      //   return View["(menu name)_(sub-menu name)", model];
      // };

      Get["/add/patients"] = _ =>{
        return View["add_new_patient.cshtml"];
      };
      Get["/patients/{id}/add-new-record"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        List<Patient> allPatients = Patient.GetAll();
        List<Condition> allConditions = Condition.GetAll();
        List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
        List<PatientScheduling> allPatientSchedulings = PatientScheduling.GetAll();

        model.Add("conditionEval", allConditionEvals);
        model.Add("currentEmployee", currentEmployee);
        model.Add("conditions",allConditions);
        model.Add("patients",allPatients);
        model.Add("patientScheduling", allPatientSchedulings);
        return View["patients_add-new-record.cshtml", model];
      };

      Get["/patients/{id}/add-new-eval"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        List<Patient> allPatients = Patient.GetAll();
        List<Condition> allConditions = Condition.GetAll();
        List<Symptom> allSymptoms = Symptom.GetAll();
        List<Diagnosis> allDiagnosis = Diagnosis.GetAll();
        model.Add("currentEmployee", currentEmployee);
        // insert lines here
        model.Add("diagnosis", allDiagnosis);
        model.Add("symptoms",allSymptoms);
        model.Add("conditions",allConditions);
        model.Add("patients",allPatients);
        return View["patients_add-new-eval.cshtml", model];
      };

      // Get["/patients/{id}/administrative-records"] = parameters => {
      //   Dictionary<string,object> model = new Dictionary<string,object>();
      //   Employee currentEmployee = Employee.Find(parameters.id);
      //   List<Patient> allPatients = Patient.GetAll();
      //   List<Condition> allConditions = Condition.GetAll();
      //   List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
      //   model.Add("currentEmployee", currentEmployee);
      //   model.Add("conditionEval", allConditionEvals);
      //   model.Add("conditions",allConditions);
      //   model.Add("patients",allPatients);
      //   return View["patients_administrative-records.cshtml", model];
      // };



      Post["/patients/{id}/add-new-record/add/patients"] =parameters =>{
        Patient newPatient = new Patient(Request.Form["patient-name"], Request.Form["patient-address"]);
        newPatient.Save();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        Employee currentEmployee = Employee.Find(parameters.id);
        List<Patient> allPatients = Patient.GetAll();
        List<Condition> allConditions = Condition.GetAll();
        List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
        List<PatientScheduling> allPatientSchedulings = PatientScheduling.GetAll();
        // List<Employee> allEmployees = Employee.GetAll(TABLE NAME);
        if(!String.IsNullOrEmpty(newPatient.GetName()))
        {
          model.Add("message", newPatient.GetName() + " has been added.");
        }
        model.Add("conditionEval", allConditionEvals);
        model.Add("conditions",allConditions);
        model.Add("patients",allPatients);
        model.Add("patientScheduling", allPatientSchedulings);
        model.Add("currentEmployee", currentEmployee);

        return View["patients_add-new-record.cshtml",model];
      };

      // Get["/add/visit"] = _ =>{
      //   List<Patient> allPatients = Patient.GetAll();
      //   //List<Physician> allPhysicians = Physician.GetAll();
      //   List<Condition> allConditions = Condition.GetAll();
      //   Dictionary<string,object> model = new Dictionary<string,object>{};
      //   model.Add("patients",allPatients);
      //   //model.Add("physicians", allPhysicians);
      //   model.Add("conditions",allConditions);
      //   return View["add_visit.cshtml",model];
      // };

      Post["/add/visit"] = _ =>{
        //Temporary DoctorId used in constructor below
        ConditionEval newVisit = new ConditionEval(Request.Form["visit-patient-id"],1, Request.Form["visit-condition-id"],Request.Form["visit-date"]);
        newVisit.Save();
        return View["success.cshtml"];
      };

      // Get["/add/symptom"] = _ =>{
      //
      //   return View["add_new_symptom.cshtml"];
      // };

      Post["/add/symptom"] = _ =>{
        //Temporary DoctorId used in constructor below
        Symptom newSymptom = new Symptom(Request.Form["symptom-name"], Request.Form["symptom-classification"],Request.Form["visit-contagious"]);
        newSymptom.Save();
        return View["success.cshtml"];
      };
      //
      // Get["/add/diagnosis"] = _ =>{
      //   List<Patient> allPatients = Patient.GetAll();
      //   //List<Physician> allPhysicians = Physician.GetAll();
      //   List<Symptom> allSymptoms = Symptom.GetAll();
      //   Dictionary<string,object> model = new Dictionary<string,object>{};
      //   model.Add("patients",allPatients);
      //   //model.Add("physicians", allPhysicians);
      //   model.Add("symptoms",allSymptoms);
      //   return View["add_diagnosis.cshtml",model];
      // };

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

      // Get["/view/all/patients"] =_=>{
      //   List<Patient> allPatients = Patient.GetAll();
      //   Dictionary<string,object> model = new Dictionary<string,object>{};
      //   model.Add("patients",allPatients);
      //   return View["view_all_patients.cshtml",model];
      // };

      Get["/patients/{id}/current-patients"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string,object>();
        Employee currentEmployee = Employee.Find(parameters.id);
        List<Patient> allPatients = Patient.GetAll();
        model.Add("currentEmployee", currentEmployee);
        model.Add("patients",allPatients);
        return View["patients_current-patients.cshtml", model];
      };

      Get["/patients/{id}/add-new-record/view/patient/visit/{patientId}"] = parameters =>{
        Patient patientToUpdate = Patient.Find(parameters.patientId);
        List<ConditionEval> patientEvals = patientToUpdate.GetAllEval();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        Employee currentEmployee = Employee.Find(parameters.id);
        model.Add("currentEmployee", currentEmployee);
        model.Add("evals", patientEvals);
        model.Add("patient", patientToUpdate);
        model.Add("message", "Evaluation History");
        List<Patient> allPatients = Patient.GetAll();

        model.Add("patients", allPatients);

        List<Condition> allConditions = Condition.GetAll();
        List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
        List<PatientScheduling> allPatientSchedulings = PatientScheduling.GetAll();

        model.Add("conditionEval", allConditionEvals);
        model.Add("conditions",allConditions);
        model.Add("patientScheduling", allPatientSchedulings);


        return View["patients_add-new-record.cshtml",model];
      };

      Get["/patients/{id}/add-new-record/view/patient/appointments/{patientId}"] = parameters =>{
        Patient patientToUpdate = Patient.Find(parameters.patientId);
        List<PatientScheduling> patientSchedule=patientToUpdate.GetAllPatientScheduling();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        Employee currentEmployee = Employee.Find(parameters.id);
        List<Patient> allPatients = Patient.GetAll();
        List<Condition> allConditions = Condition.GetAll();
        List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
        List<PatientScheduling> allPatientSchedulings = PatientScheduling.GetAll();
        model.Add("patient", patientToUpdate);
        model.Add("appointmentForPatient", patientSchedule);
        model.Add("currentEmployee", currentEmployee);
        model.Add("message", "Appointment History");
        model.Add("patients", allPatients);
        model.Add("conditionEval", allConditionEvals);
        model.Add("conditions",allConditions);
        model.Add("patientScheduling", allPatientSchedulings);
        return View["patients_add-new-record.cshtml",model];
      };

      Patch["/patients/{id}/add-new-record/view/patient/edit/{patientId}"] = parameters =>{
        Patient patientToUpdate = Patient.Find(parameters.patientId);
        patientToUpdate.Update(Request.Form["edit-new-name"], Request.Form["edit-new-address"]);
        Dictionary<string,object> model = new Dictionary<string,object>{};
        Employee currentEmployee = Employee.Find(parameters.id);
        List<Patient> allPatients = Patient.GetAll();
        List<Condition> allConditions = Condition.GetAll();
        List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
        List<PatientScheduling> allPatientSchedulings = PatientScheduling.GetAll();
        model.Add("message", "Name changed to: " + Request.Form["edit-new-name"]);
        model.Add("conditionEval", allConditionEvals);
        model.Add("conditions",allConditions);
        model.Add("patients",allPatients);
        model.Add("patientScheduling", allPatientSchedulings);
        model.Add("currentEmployee", currentEmployee);
        // return View["success.cshtml"];

        return View["patients_add-new-record.cshtml",model];
      };

      Delete["/patients/{id}/add-new-record/view/patient/delete/{patientId}"] = parameters =>{
        Patient patientToDelete = Patient.Find(parameters.patientId);
        patientToDelete.Delete();
        Dictionary<string,object> model = new Dictionary<string,object>{};
        Employee currentEmployee = Employee.Find(parameters.id);
        List<Patient> allPatients = Patient.GetAll();
        List<Condition> allConditions = Condition.GetAll();
        List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
        List<PatientScheduling> allPatientSchedulings = PatientScheduling.GetAll();
        if(!String.IsNullOrEmpty(patientToDelete.GetName()))
        {
          model.Add("message", patientToDelete.GetName() + " and all associated information has been deleted.");
        }
        model.Add("conditionEval", allConditionEvals);
        model.Add("conditions",allConditions);
        model.Add("patients",allPatients);
        model.Add("patientScheduling", allPatientSchedulings);
        model.Add("currentEmployee", currentEmployee);

        return View["patients_add-new-record.cshtml",model];
      };

      // Get["/view/all/appointments"] =_=>{
      //   List<PatientScheduling> allPatientSchedulings = PatientScheduling.GetAll();
      //   Dictionary<string,object> model = new Dictionary<string,object>{};
      //   model.Add("patientScheduling", allPatientSchedulings);
      //   return View["success.cshtml"];
      // };

      // Get["/view/all/visits"] =_=>{
      //   List<ConditionEval> allConditionEvals = ConditionEval.GetAll();
      //   Dictionary<string,object> model = new Dictionary<string,object>{};
      //   model.Add("conditionEval", allConditionEvals);
      //   return View["success.cshtml"];
      // };

      //
      // Get["/add/appointment"] = _ =>{
      //   List<Patient> allPatients = Patient.GetAll();
      //   //List<Physician> allPhysicians = Physician.GetAll();
      //   Dictionary<string,object> model = new Dictionary<string,object>{};
      //   model.Add("patients",allPatients);
      //   //model.Add("physicians", allPhysicians);
      //   return View["add_appointment.cshtml",model];
      // };

      Post["/add/appointment"] = _ =>{
        //Attempting to integrate checkbox or radio dial for selecting diagnosis
        PatientScheduling newScheduling = new PatientScheduling(Request.Form["appointment-patient-id"],1, Request.Form["patient-appointment-note"],Request.Form["patient-appointment-date"]);
        newScheduling.Save();
        return View["success.cshtml"];
      };

      Get["/news"]= _ =>{
        string[] listSearch= new string[]{"diabetes", "heart health", "health fitness", "healthcare", "pediatrics"};
        Random myRandom = new Random();
        int randomNum = myRandom.Next(0,listSearch.Length-1);
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
