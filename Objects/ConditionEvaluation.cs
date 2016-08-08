using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SeattleHealthClinic
{
  public class ConditionEval
  {
    private int _id;
    private int _patientId;
    private int _conditionId;
    private int _doctorId;
    private DateTime _statusDate;

    public ConditionEval (int patientId, int conditionId, int doctorId, DateTime statusDate, int id = 0)
    {
      _id = id;
      _patientId = patientId;
      _conditionId = conditionId;
      _doctorId=doctorId;
      _statusDate = statusDate;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetPatientId()
    {
      return _patientId;
    }

    public int GetDoctorId()
    {
      return _doctorId;
    }

    public void SetPatientId(int newPatientId)
    {
      _patientId = newPatientId;
    }

    public int GetConditionId()
    {
      return _conditionId;
    }
    public void SetConditionId(int newConditionId)
    {
      _conditionId = newConditionId;
    }

    public string GetConditionEvalDate()
    {
      return _statusDate.ToString("MM/dd/yyyy");
    }
    public void SetPatientId(DateTime newConditionEvalDate)
    {
      _statusDate = newConditionEvalDate;
    }

    public override bool Equals(System.Object otherConditionEval)
    {
      if (!(otherConditionEval is ConditionEval))
      {
        return false;
      }
      else
      {
        ConditionEval newConditionEval = (ConditionEval) otherConditionEval;
        bool idEquality = this.GetId() == newConditionEval.GetId();
        bool patientIdEquality = this.GetPatientId() == newConditionEval.GetPatientId();
        bool conditionIdEquality = this.GetConditionId() == newConditionEval.GetConditionId();
        bool doctorIdEquality = this.GetDoctorId() == newConditionEval.GetDoctorId();
        bool statusDateEquality = this.GetConditionEvalDate() == newConditionEval.GetConditionEvalDate();

        return (idEquality && patientIdEquality && conditionIdEquality && statusDateEquality&&doctorIdEquality);
      }
    }

    public static List<ConditionEval> GetAll()
    {
      List<ConditionEval> allConditionEvals = new List<ConditionEval>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients_conditions ORDER BY status_date;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int performanceId = rdr.GetInt32(0);
        int patientId = rdr.GetInt32(1);
        int conditionId = rdr.GetInt32(2);
        int doctorId = rdr.GetInt32(3);
        DateTime statusDate = rdr.GetDateTime(4);

        ConditionEval newConditionEval = new ConditionEval(patientId, conditionId,doctorId, statusDate, performanceId);
        allConditionEvals.Add(newConditionEval);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allConditionEvals;
    }

    public string GetConditionName()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT status FROM conditions WHERE id = @ConditionId;", conn);
      SqlParameter conditionIdParameter = new SqlParameter();
      conditionIdParameter.ParameterName = "@ConditionId";
      conditionIdParameter.Value = this.GetConditionId();

      cmd.Parameters.Add(conditionIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      string foundConditionName = "";

      while (rdr.Read())
      {
        foundConditionName = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundConditionName;
    }

    public string GetPatientName()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT name FROM patients WHERE id = @PatientId;", conn);
      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@PatientId";
      patientIdParameter.Value = this.GetPatientId();

      cmd.Parameters.Add(patientIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      string foundPatientName = "";

      while (rdr.Read())
      {
        foundPatientName = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundPatientName;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patients_conditions (patient_id, condition_id,  doctor_id, status_date) OUTPUT INSERTED.id VALUES (@PatientId, @ConditionId, @DoctorId, @ConditionEvalDate);", conn);

      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@PatientId";
      patientIdParameter.Value = this.GetPatientId();
      cmd.Parameters.Add(patientIdParameter);

      SqlParameter conditionIdParameter = new SqlParameter();
      conditionIdParameter.ParameterName = "@ConditionId";
      conditionIdParameter.Value = this.GetConditionId();
      cmd.Parameters.Add(conditionIdParameter);

      SqlParameter doctorIdParameter = new SqlParameter();
      doctorIdParameter.ParameterName = "@DoctorId";
      doctorIdParameter.Value = this.GetDoctorId();
      cmd.Parameters.Add(doctorIdParameter);

      SqlParameter statusDateParameter = new SqlParameter();
      statusDateParameter.ParameterName = "@ConditionEvalDate";
      statusDateParameter.Value = this.GetConditionEvalDate();
      cmd.Parameters.Add(statusDateParameter);

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

    public static ConditionEval Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients_conditions WHERE id = @ConditionEvalId;", conn);
      SqlParameter performanceIdParameter = new SqlParameter();
      performanceIdParameter.ParameterName = "@ConditionEvalId";
      performanceIdParameter.Value = id.ToString();

      cmd.Parameters.Add(performanceIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundConditionEvalId = 0;
      int foundPatientId = 0;
      int foundConditionId = 0;
      int founddoctorId = 0;

      DateTime foundConditionEvalDate = DateTime.MinValue;

      while (rdr.Read())
      {
        foundConditionEvalId = rdr.GetInt32(0);
        foundPatientId = rdr.GetInt32(1);
        foundConditionId = rdr.GetInt32(2);
        founddoctorId = rdr.GetInt32(3);
        foundConditionEvalDate = rdr.GetDateTime(4);
      }

      ConditionEval foundConditionEval = new ConditionEval(foundPatientId, foundConditionId, founddoctorId, foundConditionEvalDate, foundConditionEvalId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundConditionEval;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM patients_conditions;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
