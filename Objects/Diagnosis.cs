using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SeattleHealthClinic
{
  public class Diagnosis
  {
    private int _id;
    private int _patientId;
    private int _doctorId;
    private int _symptomId;
    private DateTime _diagnosisDate;

    public Diagnosis (int patientId, int doctorId, int symptomId, DateTime diagnosisDate, int id = 0)
    {
      _id = id;
      _patientId = patientId;
      _doctorId=doctorId;
      _symptomId = symptomId;
      _diagnosisDate = diagnosisDate;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetPatientId()
    {
      return _patientId;
    }

    public Patient GetPatient()
    {
      Patient onePatient= Patient.Find(_patientId);
      return onePatient;
    }


    public int GetDoctorId()
    {
      return _doctorId;
    }

    public void SetPatientId(int newPatientId)
    {
      _patientId = newPatientId;
    }

    public int GetSymptomId()
    {
      return _symptomId;
    }
    public Symptom GetSymptom()
    {
      Symptom oneSymptom = Symptom.Find(_symptomId);
      return oneSymptom;
    }
    public void SetSymptomId(int newSymptomId)
    {
      _symptomId = newSymptomId;
    }

    public string GetDiagnosisDate()
    {
      return _diagnosisDate.ToString("MM/dd/yyyy");
    }
    public void SetPatientId(DateTime newDiagnosisDate)
    {
      _diagnosisDate = newDiagnosisDate;
    }

    public override bool Equals(System.Object otherDiagnosis)
    {
      if (!(otherDiagnosis is Diagnosis))
      {
        return false;
      }
      else
      {
        Diagnosis newDiagnosis = (Diagnosis) otherDiagnosis;
        bool idEquality = this.GetId() == newDiagnosis.GetId();
        bool patientIdEquality = this.GetPatientId() == newDiagnosis.GetPatientId();
        bool symptomIdEquality = this.GetSymptomId() == newDiagnosis.GetSymptomId();
        bool doctorIdEquality = this.GetDoctorId() == newDiagnosis.GetDoctorId();
        bool diagnosisDateEquality = this.GetDiagnosisDate() == newDiagnosis.GetDiagnosisDate();

        return (idEquality && patientIdEquality && symptomIdEquality && diagnosisDateEquality&&doctorIdEquality);
      }
    }

    public static List<Diagnosis> GetAll()
    {
      List<Diagnosis> allDiagnosis = new List<Diagnosis>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM diagnosis ORDER BY patient_id;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int performanceId = rdr.GetInt32(0);
        int patientId = rdr.GetInt32(1);
        int doctorId = rdr.GetInt32(2);
        int symptomId = rdr.GetInt32(3);
        DateTime diagnosisDate = rdr.GetDateTime(4);

        Diagnosis newDiagnosis = new Diagnosis(patientId, doctorId, symptomId, diagnosisDate, performanceId);
        allDiagnosis.Add(newDiagnosis);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allDiagnosis;
    }


    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO diagnosis (patient_id, symptom_id,  doctor_id, diagnosis_date) OUTPUT INSERTED.id VALUES (@PatientId, @SymptomId, @DoctorId, @DiagnosisDate);", conn);

      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@PatientId";
      patientIdParameter.Value = this.GetPatientId();
      cmd.Parameters.Add(patientIdParameter);

      SqlParameter symptomIdParameter = new SqlParameter();
      symptomIdParameter.ParameterName = "@SymptomId";
      symptomIdParameter.Value = this.GetSymptomId();
      cmd.Parameters.Add(symptomIdParameter);

      SqlParameter doctorIdParameter = new SqlParameter();
      doctorIdParameter.ParameterName = "@DoctorId";
      doctorIdParameter.Value = this.GetDoctorId();
      cmd.Parameters.Add(doctorIdParameter);

      SqlParameter diagnosisDateParameter = new SqlParameter();
      diagnosisDateParameter.ParameterName = "@DiagnosisDate";
      diagnosisDateParameter.Value = this.GetDiagnosisDate();
      cmd.Parameters.Add(diagnosisDateParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Diagnosis Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM diagnosis WHERE id = @DiagnosisId;", conn);
      SqlParameter performanceIdParameter = new SqlParameter();
      performanceIdParameter.ParameterName = "@DiagnosisId";
      performanceIdParameter.Value = id.ToString();

      cmd.Parameters.Add(performanceIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundDiagnosisId = 0;
      int foundPatientId = 0;
      int foundSymptomId = 0;
      int founddoctorId = 0;

      DateTime foundDiagnosisDate = DateTime.MinValue;

      while (rdr.Read())
      {
        foundDiagnosisId = rdr.GetInt32(0);
        foundPatientId = rdr.GetInt32(1);
        foundSymptomId = rdr.GetInt32(2);
        founddoctorId = rdr.GetInt32(3);
        foundDiagnosisDate = rdr.GetDateTime(4);
      }

      Diagnosis foundDiagnosis = new Diagnosis(foundPatientId, foundSymptomId, founddoctorId, foundDiagnosisDate, foundDiagnosisId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundDiagnosis;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM diagnosis;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
